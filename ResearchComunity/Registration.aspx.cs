using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data;
using System.Net.Mail;
using System.Net;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        /*
        this.EnableViewState = true;
        if (HttpContext.Current.Request.HttpMethod == "POST") {
            lblStatus.Text = "Request http method is post!";
        }
         * */

    }


    /*  FUNCTION EXECUTED ON REGISTER BUTTON CLICK
     *  Server side validation to make sure that all requiered fields are filled in, the terms of agreement checkbox is checked ,
     *  all fields are in the required format (password, email, telephone), image size (max 5MB), and image type (jpg & png only)
     *  Insert user into the database (table UserInfo) via dynamically built parameterised insert statement
     *  Post back form with status and errors if any in which case user is not registered until the entered data is valid
     *  Ger user ID  and call function to send activation link if user is successfully inserted
     */
    protected void Submit_Click(object sender, EventArgs e)
    {
        //check
        Regex regemail = new Regex(@"[-0-9a-zA-Z.+_]+@[-0-9a-zA-Z.+_]+\.[a-zA-Z]{2,4}");
        Regex regpass = new Regex(@"(?=^.{8,}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s)[0-9a-zA-Z!@#$%^&*()]*$");

        // list of errors
        List<String> listErrors = new List<string>();

        Boolean valid = true;
        if (name.Text != "" && lastname.Text != "" && emailaddress.Text != "" && password.Text != "" && checkTerms.Checked == true)
        {

            if (!regemail.IsMatch(emailaddress.Text))
            {
                listErrors.Add("The email has to be in the specified format");
                valid = false;
            }
            if (!regpass.IsMatch(password.Text) || !(password.Text == passAgain.Text))
            {
                listErrors.Add("The password has to be in the specified format");
                valid = false;
                if (!(password.Text == passAgain.Text))
                {
                    listErrors.Add("Passwords have to match");
                    valid = false;
                }
            }
        }
        else
        {
            listErrors.Add("All fields with the * suffix are required and have to be filled in the specified format.");
            valid = false;
            if (checkTerms.Checked == false)
            {
                listErrors.Add("You must agree with out terms in order to register");
                valid = false;
            }
        }
        if (valid)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            List<string> listCols = new List<string>();
            listCols.Add("Name");
            command.Parameters.AddWithValue("@Name", name.Text);
            listCols.Add("Surname");
            command.Parameters.AddWithValue("@Surname", lastname.Text);
            listCols.Add("password");
            command.Parameters.AddWithValue("@password", password.Text);
            listCols.Add("EmailLogin");
            command.Parameters.AddWithValue("@EmailLogin", emailaddress.Text);
            listCols.Add("JoinDate");
            command.Parameters.AddWithValue("@JoinDate", DateTime.Now);
            listCols.Add("EmailPub");
            command.Parameters.AddWithValue("@EmailPub", false);
            listCols.Add("Subscribers");
            command.Parameters.Add("@Subscribers", SqlDbType.Int).Value = 0;
            if (address.Text != "")
            {
                listCols.Add("Address");
                command.Parameters.AddWithValue("@Address", address.Text);
            }
            Regex regtelephone = new Regex(@"^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$");
            if (telephone.Text != "")
            {
                if (regtelephone.IsMatch(telephone.Text))
                {
                    listCols.Add("Telephone");
                    command.Parameters.AddWithValue("@Telephone", telephone.Text);
                }
                else
                {
                    listErrors.Add("The telephone number has to be in the specified format");
                    lblStatus.Text += "Unsuccessful registration due to incorrect data <br/>";
                    lblStatus.Style.Add(HtmlTextWriterStyle.Color, "red");
                    lblStatus.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
                    for (int i = 0; i < listErrors.Count; i++)
                    {
                        lblStatus.Text += "* " + listErrors.ElementAt(i) + " <br/> ";
                    }
                    listErrors.Clear();
                    return;
                }
            }

            // get image
            Byte[] imgByte = null;
            if (imgUpload.HasFile && imgUpload.PostedFile != null)
            {
                //To create a PostedFile
                HttpPostedFile File = imgUpload.PostedFile;
                //Create byte Array with file len
                imgByte = new Byte[File.ContentLength];
                //force the control to load data in array
                File.InputStream.Read(imgByte, 0, File.ContentLength);
            }

            if (imgByte != null)
            {
                string ext = System.IO.Path.GetExtension(imgUpload.PostedFile.FileName);

                if ((ext.Equals(".png") || ext.Equals(".jpg")) == false)
                {
                    listErrors.Add("The image file has to be of type jpg or png ");
                    lblStatus.Text += "Unsuccessful registration due to incorrect data <br/>";
                    lblStatus.Style.Add(HtmlTextWriterStyle.Color, "red");
                    lblStatus.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
                    for (int i = 0; i < listErrors.Count; i++)
                    {
                        lblStatus.Text += "* " + listErrors.ElementAt(i) + " <br/> ";
                    }
                    listErrors.Clear();
                    return;
                }
                if (imgByte.Length > 5242880)
                {
                    listErrors.Add("The image size exceeds 5MB");
                    lblStatus.Text += "Unsuccessful registration due to incorrect data <br/>";
                    lblStatus.Style.Add(HtmlTextWriterStyle.Color, "red");
                    lblStatus.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
                    for (int i = 0; i < listErrors.Count; i++)
                    {
                        lblStatus.Text += "* " + listErrors.ElementAt(i) + " <br/> ";
                    }
                    listErrors.Clear();
                    return;

                }

                listCols.Add("Image");
                command.Parameters.AddWithValue("@Image", imgByte);

            }

            if (txtJob.Text != "")
            {
                listCols.Add("JobDescription");
                command.Parameters.AddWithValue("@JobDescription", txtJob.Text);
            }

            if (txtResearch.Text != "")
            {
                listCols.Add("FieldsOfResearch");
                command.Parameters.AddWithValue("@FieldsOfResearch", txtResearch.Text);
            }
            if (txtStatement.Text != "")
            {
                listCols.Add("Statement");
                command.Parameters.AddWithValue("@Statement", txtStatement.Text);
            }
            if (txtHobbies.Text != "")
            {
                listCols.Add("Hobbies");
                command.Parameters.AddWithValue("@Hobbies", txtHobbies.Text);
            }
            if (skills.Text != "")
            {
                listCols.Add("Skills");
                command.Parameters.AddWithValue("@Skills", skills.Text);
            }
            if (txtSocial.Text != "")
            {

                listCols.Add("SocialMedia");
                command.Parameters.AddWithValue("@SocialMedia", txtSocial.Text);
            }

            string strCols = "";
            for (int i = 0; i < listCols.Count; i++)
            {
                strCols += listCols.ElementAt(i);
                if (i != (listCols.Count - 1))
                {
                    strCols += ", ";
                }
            }
            string strVals = "";
            for (int i = 0; i < listCols.Count; i++)
            {
                strVals += "@" + listCols.ElementAt(i);
                if (i != (listCols.Count - 1))
                {
                    strVals += ", ";
                }
            }

            command.CommandText = "INSERT INTO UserInfo (" + strCols + ") VALUES (" + strVals + ")";

            int result = -1;
            try
            {
                connection.Open();
                result = command.ExecuteNonQuery();

            }
            catch (Exception exception)
            {
                lblStatus.Text = "Unsuccessful registration, try again. If the problem persists, contact us.";
                lblStatus.Style.Add(HtmlTextWriterStyle.Color, "red");
                lblStatus.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
            }
            finally
            {
                connection.Close();
            }
            if (result == 1)
            {
                //hide form1
                tabContent.Visible = false;
                //show status
                lblSuccessReg.Text = "You have been successfully registered. An activation link will be sent to your email shortly. Once you activate your account , you will be able to sign in.";
                lblSuccessReg.Style.Add(HtmlTextWriterStyle.FontSize, "15px");
                lblSuccessReg.Style.Add(HtmlTextWriterStyle.Color, "Green");
                lblSuccessReg.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");

                // get user id just inserted

                int id = -1;
                string ConnString = ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString;
                string SqlString = "Select ID From UserInfo Where EmailLogin = '"+emailaddress.Text.Trim()+"'";
                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    using (SqlCommand cmd = new SqlCommand(SqlString, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                id=(int)reader["ID"];
                            }
                        }
                    }
                }
                //send activation link with param user id
                SendActivationLink(id);

            }
            else
            {
                lblStatus.Text = "Unsuccessful registration, try again. If the problem persists, contact us.";
                lblStatus.Style.Add(HtmlTextWriterStyle.Color, "red");
                lblStatus.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
            }

        }
        else
        {
            // not valid 
            lblStatus.Text += "Unsuccessful registration due to incorrect data <br/>";
            lblStatus.Style.Add(HtmlTextWriterStyle.Color, "red");
            lblStatus.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
            for (int i = 0; i < listErrors.Count; i++)
            {
                lblStatus.Text += "* " + listErrors.ElementAt(i) + " <br/> ";
            }
        }
        listErrors.Clear();
    }

    /*  SEND ACTIVATION LINK TO USER EMAIL
     *  Generate activation code using GUID, insert into table UserActivation userid and activation code
     *  and send message via smtp client (set up host info : server, credentials, ssl and port)
     */
    private void SendActivationLink(int userId)
    {

        string constr = ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString;
        string activationCode = Guid.NewGuid().ToString();
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO UserActivation VALUES(@UserId, @ActivationCode)"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@ActivationCode", activationCode);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            using (MailMessage mm = new MailMessage("tamaradanceva@yahoo.com", emailaddress.Text))
            {
                mm.Subject = "BloggerixPlus Account Activation ";
                string body = "Hello " + name.Text + lastname.Text + ",";
                body += "<br /><br />Please click the following link to activate your account";
                body += "<br /><a href = '" + Request.Url.AbsoluteUri.Replace("Registration.aspx", "Activation.aspx?ActivationCode=" + activationCode) + "'>Click here to activate your account.</a>";
                body += "<br /><br />Thanks";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = " smtp.mail.yahoo.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("tamaradanceva@yahoo.com", "newLozinka1");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }



    }

}