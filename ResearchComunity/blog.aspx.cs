using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Net;

public partial class Blogs : System.Web.UI.Page
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
        else {
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
            pub = " and PublicBlog='true'";
        }
        comm.CommandText = "Select Blog.ID as BlogID, Title, Name +' '+ Surname as Name, Description, Blog.Text as Text, Blog.Date as Date from BlogPost as Blog, UserInfo where Blog.UserID=UserInfo.ID and Blog.ID=@id" + pub;

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
            ds.Rows[0]["Text"] = WebUtility.HtmlDecode(ds.Rows[0]["Text"].ToString());
            dlBlog.DataSource = ds.DefaultView;
            dlBlog.DataBind();
            


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

    private void BindComments(int id) {

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
        SqlCommand comm = new SqlCommand();
        comm.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
        comm.Connection = conn;


        comm.CommandText = "Select BlogComment.Date as Date, Name +' '+ Surname as Name, BlogComment.Comment as Text from BlogComment, UserInfo where BlogComment.UserID=UserInfo.ID and BlogComment.BlogID=@id order by BlogComment.Date DESC; Select TOP 4 BlogComment.Date as Date, Name +' '+ Surname as Name, BlogComment.Comment as Text from BlogComment, UserInfo where BlogComment.UserID=UserInfo.ID and BlogComment.BlogID=@id order by BlogComment.Date DESC";

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

    private void BindRecentPosts(int id){
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
        SqlCommand comm = new SqlCommand();
        comm.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
        comm.Connection = conn;


        comm.CommandText = "Select Top 6 Blog.ID as BlogID, Blog.Title, Blog.Title as Text from BlogPost as Blog, UserInfo where Blog.UserID=UserInfo.ID and UserInfo.ID=(Select BlogPost.UserID from BlogPost where BlogPost.ID=@id) order by Blog.Date DESC; ";

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
        LatestBlogs.DataSource = ds.DefaultView;
        LatestBlogs.DataBind();
    
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
            comm.CommandText = "Select TagName, BlogID from BlogTagging, Tag where Tag.ID= BlogTagging.TagID and BlogID=@id;";
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

            DataList innerDataList = e.Item.FindControl("BlogTags") as DataList;

            innerDataList.DataSource = ds.DefaultView;
            TagsPanel.DataSource = ds.DefaultView;

            innerDataList.DataBind();
            TagsPanel.DataBind();

        }
    }


    protected void subscribe_Click(object sender, EventArgs e) {
        if (Session["UserID"] != null)
        {
            int[] idd = GlobalVariables.Hash.Decrypt(Request.QueryString["b"]);
            int id = GlobalVariables.getInt(idd);
            int userid = Int32.Parse(Session["UserID"].ToString());
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add("@blogid", System.Data.SqlDbType.Int).Value = id;
            comm.Parameters.Add("@userid", System.Data.SqlDbType.Int).Value = userid;
            comm.Connection = conn;
            comm.CommandText = "INSERT INTO Subscription(subscriberID,userID) VALUES(@userid,(select BlogPost.UserID from BlogPost where BlogPost.Id=@blogid))";
           
            conn.Open();
            try
            {
                int res = comm.ExecuteNonQuery();
            }
            catch (Exception eee)
            {

            }
            finally
            {
                conn.Close();
                comm.Dispose();
                conn.Dispose();
            }
           


        }
    
    }

    //submit comment
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Session["UserID"] != null)
        {
            int [] idd = GlobalVariables.Hash.Decrypt(Request.QueryString["b"]);
            int id = GlobalVariables.getInt(idd);
            int userid = Int32.Parse(Session["UserID"].ToString());
            string commentBlog = comment.InnerText;


            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add("@blogid", System.Data.SqlDbType.Int).Value = id;
            comm.Parameters.AddWithValue("@comment",commentBlog);
            comm.Parameters.Add("@userid", System.Data.SqlDbType.Int).Value = userid;
            comm.Connection = conn;
            comm.CommandText = "INSERT INTO BlogComment(BlogID,UserID,Comment,Date) VALUES(@blogid,@userid,@comment,GETDATE())";
            conn.Open();
            int res = comm.ExecuteNonQuery();
            conn.Close();
            comm.Dispose();
            conn.Dispose();
            if (res == 1) {
                BindComments(id);
            }
            comment.InnerText = "";

        }
        else {
            Response.Redirect("Login.aspx");
        }
    }
}