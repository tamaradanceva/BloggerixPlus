using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class users_BlogsEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null)
        {
            string action = Request.QueryString["action"].ToString();
            if (action == "new")
            {
                submitBlog.Text = "Create new blog";
            }
            else if (action == "edit")
            {
                if (Request.QueryString["b"] == null)
                {
                    Response.Redirect("../TechNews.aspx");
                }
                submitBlog.Text = "Edit blog";
                fillBlog();
                
            }
            else if (action == "Delete")
            {
                if (Request.QueryString["b"] == null)
                {
                    Response.Redirect("../TechNews.aspx");
                }
                submitBlog.Text = "Delete blog";
                
            }
        }
    }
    protected void fillBlog() {

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
        SqlCommand comm = new SqlCommand();
        comm.Connection = conn;
        comm.CommandText = "select * from BlogPost where id=@id";
        int user = GlobalVariables.getInt(GlobalVariables.Hash.Decrypt(Request.QueryString["b"]));
        comm.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = user;

        SqlDataAdapter da = new SqlDataAdapter(comm);
        DataTable dt = new DataTable();
        da.Fill(dt);

        txtTitle.Text=dt.Rows[0]["Title"].ToString();
        txtDescription.Text = dt.Rows[0]["Description"].ToString();
        txtMain.Text = WebUtility.HtmlDecode(dt.Rows[0]["Text"].ToString());
        if (dt.Rows[0]["PublicBlog"] == "true")
            chkPublic.Checked = true;
        else chkPublic.Checked = false;

        conn.Close();
        conn.Dispose();
        comm.Dispose();


    
    }


    protected void submitBlog_Click(object sender, EventArgs e)
    
    {
        bool allOk;

        string title = txtTitle.Text;
        string descr = txtDescription.Text;
        string text = WebUtility.HtmlEncode(txtMain.Text);
        string isPublic = chkPublic.Checked.ToString();
        DateTime time = DateTime.Now;
        //int user = 2;
        int user = GlobalVariables.getInt(GlobalVariables.Hash.Decrypt(Request.QueryString["b"]));

        if (submitBlog.Text == "Create new blog")
        {
           
            allOk = true;
            if (allOk)
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                command.CommandText = "INSERT INTO BlogPost (UserID,Title,Description,Text,PublicBlog,Date) VALUES " +
                                      "( @user, @title ,@descr, @text, '" + isPublic + "' , @time )";
                command.Parameters.AddWithValue("@user", user);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@descr", descr);
                command.Parameters.AddWithValue("@text", text);

                command.Parameters.AddWithValue("@time", time);

                connection.Open();
                command.ExecuteNonQuery();

                // lblError.Visible = true;

                connection.Close();
            }

        }
        else if (submitBlog.Text == "Edit blog")
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            command.CommandText = "UPDATE BlogPost SET Title=@title, Description=@description,Text=@text, PublicBlog='"+isPublic+"' WHERE ID=@id";
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@description", descr);
            command.Parameters.AddWithValue("@text", text);
            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = user;
            connection.Open();
            int rez=command.ExecuteNonQuery();

            // lblError.Visible = true;

            connection.Close();
            connection.Dispose();
            command.Dispose();
        
        }

    }
}