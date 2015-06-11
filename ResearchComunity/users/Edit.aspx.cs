using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class users_BlogsEdit : System.Web.UI.Page
{
    int user;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        user = -1;
        if(Session["UserID"] != null)
            user = (int)Session["UserID"];
        lblError.Visible = false;
        lblOk.Visible = false;
        lblErrorD.Visible = false;
        LblErrorP.Visible = false;
        lblOkD.Visible = false;
        lblOkP.Visible = false;

        string type = Request.QueryString["t"];
          if (type == "blog")
          {
             
              if (Request.QueryString["b"] != null)
              {
                  pnlBlog.Visible = true;
                  btnDeleteBlog.Visible = true;
                  lblTitleBlog.InnerText = "Edit your blog";
                  int blogID = GlobalVariables.getInt(GlobalVariables.Hash.Decrypt(Request.QueryString["b"].ToString()));
                  if (blogID > 0)
                  {
                      bindBlog(blogID, user);
                      submitBlog.Text = "Edit blog";
                  }
                  else {
                      Response.Redirect("MyBlogs.aspx");
                  }
              }
              else {
                  pnlBlog.Visible = true;
                  lblTitleBlog.InnerText = "Create new blog";
              }

          }
          else if (type == "poll")
          {
              if (Request.QueryString["b"] != null)
              {
                  pnlPoll.Visible = true;
                  btnDeletePoll.Visible = true;
                  lblTitlePoll.InnerText = "Edit your poll";
                  submitPoll.Text = "Save changes";
              }
              else
              {
                  lblTitlePoll.InnerText = "Add new poll";
                  pnlPoll.Visible = true;
              }
          }
          else if (type == "doc")
          {
              if (Request.QueryString["b"] != null)
              {
                  int[] idd = GlobalVariables.Hash.Decrypt(Request.QueryString["b"]);
                  int docId = GlobalVariables.getInt(idd);
                  lblTitlePanel.InnerText = "Edit your document";
                  submitDoc.Text = "Save changes";
                  btnDeleteDoc.Visible = true;
                  if (docId > 0)
                  {
                      if (user > 0)
                      {
                          pnlDoc.Visible = true;
                          bindUserEdit(docId);
                      }

                  }
              }
              else
              {
                  pnlDoc.Visible = true;
                  lblTitlePanel.InnerText = "Add new document";
              }

          }

    }

    private void bindBlog(int bid, int uid){

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
        SqlCommand comm = new SqlCommand();
        comm.Connection = conn;
        comm.CommandText = "select * from BlogPost where id=@id and UserID=@uid";
        int user = GlobalVariables.getInt(GlobalVariables.Hash.Decrypt(Request.QueryString["b"]));
        comm.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = bid;
        comm.Parameters.Add("@uid", System.Data.SqlDbType.Int).Value = uid;
        SqlDataAdapter da = new SqlDataAdapter(comm);
        DataTable dt = new DataTable();
        da.Fill(dt);

        txtTitle.Text = dt.Rows[0]["Title"].ToString();
        txtDescription.Text = dt.Rows[0]["Description"].ToString();
        txtMain.Text = WebUtility.HtmlDecode(dt.Rows[0]["Text"].ToString());
        if (dt.Rows[0]["PublicBlog"] == "true")
            chkPublic.Checked = true;
        else chkPublic.Checked = false;

        conn.Close();
        conn.Dispose();
        comm.Dispose();
        
    }

    private void bindUserEdit(int docId)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
        SqlCommand comm = new SqlCommand();
        comm.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = docId;
        comm.Connection = conn;
        comm.CommandText = "SELECT ID, Title, Description,PublicFile FROM Document WHERE ID=@id";
        SqlDataReader reader;
        string title ="";
        string desc = "";
        string isPublic = "";
            conn.Open();
            reader = comm.ExecuteReader();
            if (reader.Read() != null)
            {
                 title = reader["Title"].ToString();
                 desc = reader["Description"].ToString();
                 isPublic = reader["PublicFile"].ToString();
            }
            conn.Close();
            txtTitleD.Text = title;
            txtDescD.Text = desc;
            if (isPublic == "true")
                chkPublicD.Checked = true;           
    }
    protected void submitBlog_Click(object sender, EventArgs e)
    {
        
        bool allOk;
        int created = 0;
        string title = txtTitle.Text;
        string descr = txtDescription.Text;
        string text = WebUtility.HtmlEncode(txtMain.Text);
        string isPublic = chkPublic.Checked.ToString();
        DateTime time = DateTime.Now;
        string action = "created";
        allOk = true;
        int bid = -1;
        if (allOk)
        {

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            if (Request.QueryString["b"] != null)
            {
                action = "edited";
                bid = GlobalVariables.getInt(GlobalVariables.Hash.Decrypt(Request.QueryString["b"].ToString()));
                command.CommandText = "UPDATE BlogPost SET Title=@title,Description=@descr,Text=@text,PublicBlog='"+isPublic.ToString()+"' where ID="+bid;
            }
            else
            {
                command.CommandText = "INSERT INTO BlogPost (UserID,Title,Description,Text,PublicBlog,Date) VALUES " +
                                      "( @user, @title ,@descr, @text, '" + isPublic + "' , @time )";
            }
            
            command.Parameters.AddWithValue("@user", user);
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@descr", descr);
            command.Parameters.AddWithValue("@text", text);          
            command.Parameters.AddWithValue("@time", time);
            //try
           // {
                connection.Open();
                created = command.ExecuteNonQuery();
           // }
           // catch (Exception ex)
            {
                //lblError.Visible = true;
            }
         //   finally
            {
                connection.Close();
            }

            if (created > 0)
            {
                if (bid < 1)
                {
                    int BID = 0;
                    command.CommandText = "SELECT TOP 1 ID from BlogPost";
                    SqlDataReader reader;
                    connection.Open();
                    reader = command.ExecuteReader();
                    if (reader.Read() != null)
                        BID = (int)reader["ID"];
                    connection.Close();
                    if(BID > 0)
                    logSubmit(Request.QueryString["t"].ToString(), action, BID, time);
                }
                else
                {
                    lblOk.Visible = true;
                    logSubmit(Request.QueryString["t"].ToString(), action, bid, time);
                }

            }
        }
       
            
    }
    protected void submitPoll_Click(object sender, EventArgs e)
    {
        bool allOk;
        int created = 0;
        string title = txtTitleP.Text;
        string descr = TxtDescP.Text;
        string isPublic = chkIsPublicP.Checked.ToString();
        DateTime time = DateTime.Now;
        allOk = true;
        if (allOk)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            command.CommandText = "INSERT INTO Poll (UserID,Title,Description,PublicPoll,Date) VALUES " +
                                  "( @user, @title ,@descr,  '" + isPublic  + "' , @time )";
            command.Parameters.AddWithValue("@user", user);
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@descr", descr);
            command.Parameters.AddWithValue("@time", time);
            try
            {
                connection.Open();
                created = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LblErrorP.Visible = true;
            }
            finally
            {
                connection.Close();
            }
            if (created > 0)
                lblOkP.Visible = true;
        }
        
    }
    protected void submitDoc_Click(object sender, EventArgs e)
    {
        string title = txtTitleD.Text;
        string desc = txtDescD.Text;
        string isPublic = chkPublicD.Checked.ToString();
        DateTime time = DateTime.Now;

        int created = 0;

        if (Request.QueryString["b"] != null)
        {
            int[] idd = GlobalVariables.Hash.Decrypt(Request.QueryString["b"]);
            int docId = GlobalVariables.getInt(idd);
       
            if (!FileUpload1.HasFile)
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@descr", desc);
                command.Parameters.AddWithValue("@time", time);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = docId;
                command.CommandText = "UPDATE Document SET Title=@title,Description=@descr,PublicFile='" + isPublic + "',Date=@time WHERE  ID=@id";
                                         
               
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
            
        
            }
            else
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
                SqlCommand comm = new SqlCommand();
                comm.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = docId;
                comm.Connection = conn;
                comm.CommandText = "DELETE FROM Document WHERE ID=@id";
                conn.Open();
                comm.ExecuteNonQuery();
                conn.Close();
                created = insertDoc();

            }

        }
        else
        {
           created = insertDoc();
        }
       
            if (created > 0)
                lblOkD.Visible = true;
            else
                lblErrorD.Visible = true;
        
            Response.Redirect("MyDocuments.aspx");
     }
    protected int insertDoc()
    {

        string title = txtTitleD.Text;
        string desc = txtDescD.Text;
        bool allOk;
        int created = 0;
        string isPublic = chkPublicD.Checked.ToString();
        DateTime time = DateTime.Now;

        if (FileUpload1.HasFile)
        {
            string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            string documentType = string.Empty;

            switch (fileExtension)
            {
                case ".pdf":
                    documentType = "application/pdf";
                    break;
                case ".xls":
                    documentType = "application/vnd.ms-excel";
                    break;
                case ".xlsx":
                    documentType = "application/vnd.ms-excel";
                    break;
                case ".doc":
                    documentType = "application/vnd.ms-word";
                    break;
                case ".docx":
                    documentType = "application/vnd.ms-word";
                    break;
            }
            //Calculate size of file to be uploaded
            int fileSize = FileUpload1.PostedFile.ContentLength;

            //Create array and read the file into it
            byte[] documentBinary = new byte[fileSize];
            FileUpload1.PostedFile.InputStream.Read(documentBinary, 0, fileSize);

            // Create SQL Connection   
            allOk = true;
            if (allOk)
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                command.CommandText = "INSERT INTO Document (UserID,Title,Description,PublicFile,Date,Doc,Type,DocName) VALUES " +
                                      "( @user, @title ,@descr,  '" + isPublic + "' , @time, @doc, @type, @docName )";
                command.Parameters.AddWithValue("@user", user);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@descr", desc);
                command.Parameters.AddWithValue("@time", time);
                command.CommandType = CommandType.Text;

                SqlParameter DocName = new SqlParameter("@docName", SqlDbType.VarChar, 50);
                DocName.Value = fileName.ToString();
                command.Parameters.Add(DocName);

                SqlParameter Type = new SqlParameter("@type", SqlDbType.VarChar, 50);
                Type.Value = documentType.ToString();
                command.Parameters.Add(Type);

                SqlParameter uploadedDocument = new SqlParameter("@doc", SqlDbType.Binary, fileSize);
                uploadedDocument.Value = documentBinary;
                command.Parameters.Add(uploadedDocument);

                connection.Open();
                created = command.ExecuteNonQuery();
                connection.Close();           
            }
        }
        return created;
    }
    protected void btnDeleteDoc_Click(object sender, EventArgs e)
    {
        int[] idd = GlobalVariables.Hash.Decrypt(Request.QueryString["b"]);
        int docId = GlobalVariables.getInt(idd);
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
        SqlCommand comm = new SqlCommand();
        comm.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = docId;
        comm.Connection = conn;
        comm.CommandText = "DELETE FROM Document WHERE ID=@id";
        conn.Open();
        comm.ExecuteNonQuery();
        conn.Close();
        Response.Redirect("MyDocuments.aspx");
    }
    protected void btnDeleteBlog_Click(object sender, EventArgs e)
    {
        int[] idd = GlobalVariables.Hash.Decrypt(Request.QueryString["b"]);
        int docId = GlobalVariables.getInt(idd);
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
        SqlCommand comm = new SqlCommand();
        comm.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = docId;
        comm.Connection = conn;
        comm.CommandText = "DELETE FROM BlogPost WHERE ID=@id";
        conn.Open();
        try
        {
            comm.ExecuteNonQuery();    
        }
        catch(Exception eee)
        {

        }
        finally
        {
            conn.Close();
        }       
        Response.Redirect("MyBlogs.aspx");
    }
    protected void btnDeletePoll_Click(object sender, EventArgs e)
    {
        int[] idd = GlobalVariables.Hash.Decrypt(Request.QueryString["b"]);
        int docId = GlobalVariables.getInt(idd);
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
        SqlCommand comm = new SqlCommand();
        comm.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = docId;
        comm.Connection = conn;
        comm.CommandText = "DELETE FROM Poll WHERE ID=@id";
        conn.Open();
        comm.ExecuteNonQuery();
        conn.Close();
        Response.Redirect("MyPolls.aspx");
    }
    protected void logSubmit(string contentType, string action, int contentID, DateTime date)
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
        SqlCommand command = new SqlCommand();
        command.Connection = connection;

        command.CommandText = "INSERT into Logger (UserID,TypeOfContent,Action,ContentID,Date) VALUES " +
                                      "( @user, @contentType ,@action, @contentId, @date )";
        command.Parameters.AddWithValue("@user", user);
        command.Parameters.AddWithValue("@contentType", contentType);
        command.Parameters.AddWithValue("@action", action);
        command.Parameters.AddWithValue("@contentId", contentID);
        command.Parameters.AddWithValue("@date", date);

        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }
}