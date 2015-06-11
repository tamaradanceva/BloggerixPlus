using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class document : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //get id with decrypt, za sega fixed
        //int id = 1;
        string hash = "";

        if (Request.QueryString["b"] != null)
        {

            int[] idd = GlobalVariables.Hash.Decrypt(Request.QueryString["b"]);
            int id = GlobalVariables.getInt(idd);


            if (id > 0)
            {
                if (Session["UserID"] != null)
                {
                    BindBlogInfo(id, false);

                }
                else
                {
                    BindBlogInfo(id, true);

                }
                BindComments(id);
                BindRecentPosts(id);

            }
            else
            {
                Response.Redirect("TechNews.aspx");
            }
        }
        else
        {
            Response.Redirect("TechNews.aspx");
        }
    }

    private void BindBlogInfo(int id, bool checkPublic)
    {

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
        SqlCommand comm = new SqlCommand();
        comm.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
        comm.Connection = conn;
        string pub = "";
        if (checkPublic == true)
        {
            pub = " and PublicFile='true'";
        }
        comm.CommandText = "Select Doc.ID as DocID, Title, Name +' '+ Surname as Name, Description, Doc.Title as Text, Doc.Date as Date from Document as Doc, UserInfo where Doc.UserID=UserInfo.ID and Doc.ID=@id" + pub;

        DataTable ds = new DataTable();

        try
        {
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(ds);
            
            if (ds.Rows.Count == 0)
            {
                Response.Redirect("Login.aspx");
            }
            ds.Rows[0]["Text"] = GlobalVariables.Hash.Encrypt(GlobalVariables.getArray(Int32.Parse(ds.Rows[0].ItemArray[0].ToString())));


            dlDoc.DataSource = ds.DefaultView;
            dlDoc.DataBind();



        }
        catch (Exception ex)
        {
            Response.Redirect("TechNews.aspx");
        }
        finally
        {
            conn.Close();
            comm.Dispose();
            conn.Dispose();
        }

    }

    private void BindComments(int id)
    {

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
        SqlCommand comm = new SqlCommand();
        comm.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
        comm.Connection = conn;


        comm.CommandText = "Select DocComment.Date as Date, Name +' '+ Surname as Name, DocComment.Comment as Text from DocComment, UserInfo where DocComment.UserID=UserInfo.ID and DocComment.DocumentID=@id order by DocComment.Date DESC; Select TOP 4 DocComment.Date as Date, Name +' '+ Surname as Name, DocComment.Comment as Text from DocComment, UserInfo where DocComment.UserID=UserInfo.ID and DocComment.DocumentID=@id order by DocComment.Date DESC";

        DataSet ds = new DataSet();

        try
        {
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            Response.Redirect("TechNews.aspx");
        }
        finally
        {
            conn.Close();
            comm.Dispose();
            conn.Dispose();
        }


        dlComments.DataSource = ds.Tables[0].DefaultView;
        dlComments.DataBind();
        dlRecentComments.DataSource = ds.Tables[1].DefaultView;
        dlRecentComments.DataBind();
    }

    private void BindRecentPosts(int id)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
        SqlCommand comm = new SqlCommand();
        comm.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
        comm.Connection = conn;


        comm.CommandText = "Select Top 6 Doc.ID as DocID, Doc.Title, Doc.Title as Text from Document as Doc, UserInfo where Doc.UserID=UserInfo.ID and UserInfo.ID=(Select Document.UserID from Document where Document.ID=@id) order by Doc.Date DESC; ";

        DataTable ds = new DataTable();

        try
        {
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(ds);
            for (int i = 0; i < ds.Rows.Count; i++)
            {
                ds.Rows[i]["Text"] = GlobalVariables.Hash.Encrypt(GlobalVariables.getArray(Int32.Parse(ds.Rows[i].ItemArray[0].ToString())));
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("TechNews.aspx");
        }
        finally
        {
            conn.Close();
            comm.Dispose();
            conn.Dispose();
        }
        LatestDocs.DataSource = ds.DefaultView;
        LatestDocs.DataBind();

    }

    protected void dlBlog_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {

            DataRowView drv = e.Item.DataItem as DataRowView;

            int id = Int32.Parse(drv.Row.ItemArray[0].ToString());

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            comm.Connection = conn;
            comm.CommandText = "Select TagName, DocumentID from DocTagging, Tag where Tag.ID=DocTagging.TagID and DocumentID=@id;";
            DataTable ds = new DataTable();
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(comm);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Response.Redirect("TechNews.aspx");
            }
            finally
            {
                conn.Close();
                comm.Dispose();
                conn.Dispose();
            }

            DataList innerDataList = e.Item.FindControl("DocTags") as DataList;

            innerDataList.DataSource = ds.DefaultView;
            TagsPanel.DataSource = ds.DefaultView;

            innerDataList.DataBind();
            TagsPanel.DataBind();

        }
    }


    protected void subscribe_Click(object sender, EventArgs e)
    {
        if (Session["UserID"] != null)
        {
            int[] idd = GlobalVariables.Hash.Decrypt(Request.QueryString["b"]);
            int id = GlobalVariables.getInt(idd);
            int userid = Int32.Parse(Session["UserID"].ToString());
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add("@docid", System.Data.SqlDbType.Int).Value = id;
            comm.Parameters.Add("@userid", System.Data.SqlDbType.Int).Value = userid;
            comm.Connection = conn;
            comm.CommandText = "INSERT INTO Subscription(subscriberID,userID) VALUES(@userid,(select Doc.UserID from Document as Doc where Doc.Id=@docid))";

            conn.Open();
            int res = comm.ExecuteNonQuery();
            conn.Close();
            comm.Dispose();
            conn.Dispose();


        }

    }

    //submit comment
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Session["UserID"] != null)
        {
            int[] idd = GlobalVariables.Hash.Decrypt(Request.QueryString["b"]);
            int id = GlobalVariables.getInt(idd);
            int userid = Int32.Parse(Session["UserID"].ToString());
            string commentBlog = comment.InnerText;


            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add("@docid", System.Data.SqlDbType.Int).Value = id;
            comm.Parameters.AddWithValue("@comment", commentBlog);
            comm.Parameters.Add("@userid", System.Data.SqlDbType.Int).Value = userid;
            comm.Connection = conn;
            comm.CommandText = "INSERT INTO DocComment(DocumentID,UserID,Comment,Date) VALUES(@docid,@userid,@comment,GETDATE())";
            conn.Open();
            int res = comm.ExecuteNonQuery();
            conn.Close();
            comm.Dispose();
            conn.Dispose();
            if (res == 1)
            {
                BindComments(id);
            }
            comment.InnerText = "";

        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }
}