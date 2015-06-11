using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Contact : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    protected void Button_Message_Click(object sender, EventArgs e)
    {
        string errorMsg = "";
        Regex regemail = new Regex(@"[-0-9a-zA-Z.+_]+@[-0-9a-zA-Z.+_]+\.[a-zA-Z]{2,4}");
        //saving to database 
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "INSERT INTO Message (Name, Email, Subject, Message ) VALUES ('name','subject','email','message')";
        try
        {
            using (MailMessage mm = new MailMessage("tamaradanceva@yahoo.com", "tamaradanceva@yahoo.com"))
            {
                mm.Subject = "BloggerixPlus Messagge";
                string body = "";
                body += "<br />" + txtMessage.Text;
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
        catch(Exception ee)
        {

        }
        finally
        {

        }
       
        

    }
}