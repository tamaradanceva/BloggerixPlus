using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class users_AboutMe : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] != null)
        {
            Image1.ImageUrl = "getImage.ashx?i=" + GlobalVariables.Hash.Encrypt(GlobalVariables.getArray(Int32.Parse(Session["UserID"].ToString())));
            
        }
        else {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void btnSaveTab1_Click(object sender, EventArgs e)
    {
        int user = (int)Session["UserID"];
        string name = txtName.Text;
        string surname = lastname.Text;
        string alias = txtAlias.Text;
        string address = txtAddress.Text;
        string tel = telephone.Text;

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        List<string> listCols = new List<string>();
        if (name != "")
        {
            listCols.Add("Name");
            command.Parameters.AddWithValue("@Name", name);
        }

        if (surname != "")
        {
            listCols.Add("Surname");
            command.Parameters.AddWithValue("@Surname", surname);
        }
        if (alias != "")
        {
            listCols.Add("Alias");
            command.Parameters.AddWithValue("@Alias", alias);
        }
        if (address != "")
        {
            listCols.Add("Address");
            command.Parameters.AddWithValue("@Address", address);
        }
        if (tel != "")
        {
            listCols.Add("Telephone");
            command.Parameters.AddWithValue("@Telephone", tel);
        }
        string strCols = "";
        for (int i = 0; i < listCols.Count; i++)
        {
            strCols += listCols.ElementAt(i);
            strCols += "=@" + listCols.ElementAt(i);
            if (i != (listCols.Count - 1))
            {
                strCols += ", ";
            }
        }
  
        command.CommandText = "UPDATE UserInfo  SET " + strCols + " WHERE ID=" + user;
                                                     
        connection.Open();
        int succes = command.ExecuteNonQuery();
        connection.Close();
        if (succes > 0 )
        {
            lblMsg.Text = "Changes were succesfull saved";
            lblMsg.Visible = true;
        }
    }
    protected void btnSaveTab2_Click(object sender, EventArgs e)
    {
        int user = (int)Session["UserID"];
        string listErrors = String.Empty;
        Byte[] imgByte = null;
        if (FileUpload1.HasFile && FileUpload1.PostedFile != null)
        {
            //To create a PostedFile
            HttpPostedFile File = FileUpload1.PostedFile;
            //Create byte Array with file len
            imgByte = new Byte[File.ContentLength];
            //force the control to load data in array
            File.InputStream.Read(imgByte, 0, File.ContentLength);
        }

        if (imgByte != null)
        {
            string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);

            if ((ext.Equals(".png") || ext.Equals(".jpg")) == false)
            {
                listErrors += "The image file has to be of type jpg or png ";
                lblMsg.Text = listErrors;
                lblMsg.Visible = true;
                return;
            }
            if (imgByte.Length > 5242880)
            {
                listErrors += "\nThe image size exceeds 5MB";
                lblMsg.Text = listErrors;
                lblMsg.Visible = true;
                return;

            }
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            command.Parameters.AddWithValue("@Image", imgByte);
            command.CommandText = "UPDATE UserInfo  SET Image=@Image WHERE ID=" + user;
            connection.Open();
            int succes = command.ExecuteNonQuery();
            connection.Close();
            if (succes > 0)
            {
                lblMsg.Text = "Changes were succesfull saved";
                lblMsg.Visible = true;
            }
            else
            {
                lblMsg.Text = "Oh snap! There was an error processing your request : ( ";
                lblMsg.Visible = true;
            }

        }
    }
    protected void btnSaveTab3_Click(object sender, EventArgs e)
    {
        int user = (int)Session["UserID"];
        string job = txtJob.Text;
        string research = txtResearch.Text;
        string statement = txtStatement.Text;

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        List<string> listCols = new List<string>();
        if (job != "")
        {
            listCols.Add("JobDescription");
            command.Parameters.AddWithValue("@JobDescription", job);
        }

        if (research != "")
        {
            listCols.Add("FieldsOfResearch");
            command.Parameters.AddWithValue("@FieldsOfResearch", research);
        }
        if (statement != "")
        {
            listCols.Add("Statement");
            command.Parameters.AddWithValue("@Statement", statement);
        }
        string strCols = "";
        for (int i = 0; i < listCols.Count; i++)
        {
            strCols += listCols.ElementAt(i);
            strCols += "=@" + listCols.ElementAt(i);
            if (i != (listCols.Count - 1))
            {
                strCols += ", ";
            }
        }

        command.CommandText = "UPDATE UserInfo  SET " + strCols + " WHERE ID=" + user;

        connection.Open();
        int succes = command.ExecuteNonQuery();
        connection.Close();
        if (succes > 0)
        {
            lblMsg.Text = "Changes were succesfull saved";
            lblMsg.Visible = true;
        }
        else
        {
            lblMsg.Text = "Oh snap! There was an error processing your request : ( ";
            lblMsg.Visible = true;
        }
    }
    protected void btnSaveTab5_Click(object sender, EventArgs e)
    {
        int user = (int)Session["UserID"];
        string hobbies = txtHobbies.Text;
        string skills = txtSkills.Text;

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        List<string> listCols = new List<string>();
        if (hobbies != "")
        {
            listCols.Add("Hobbies");
            command.Parameters.AddWithValue("@Hobbies", hobbies);
        }
        if (skills != "")
        {
            listCols.Add("Skills");
            command.Parameters.AddWithValue("@Skills", skills);
        }

        string strCols = "";
        for (int i = 0; i < listCols.Count; i++)
        {
            strCols += listCols.ElementAt(i);
            strCols += "=@" + listCols.ElementAt(i);
            if (i != (listCols.Count - 1))
            {
                strCols += ", ";
            }
        }

        command.CommandText = "UPDATE UserInfo  SET " + strCols + " WHERE ID=" + user;
        int succes = 0;
        try
        {
            connection.Open();
            succes = command.ExecuteNonQuery();
        }
        catch (Exception ee)
        {
            lblMsg.Text = "Oh snap! There was an error processing your request : ( ";
            lblMsg.Visible = true;
        }
        finally
        {
            connection.Close();
        }
        
        if (succes > 0)
        {
            lblMsg.Text = "Changes were succesfull saved";
            lblMsg.Visible = true;
        }
    }
}