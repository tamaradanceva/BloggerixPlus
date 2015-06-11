using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class poll : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //get id with decrypt, za sega fixed
        //int id = 1;
        //string hash = "";

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
            pub = " and PublicPoll='true'";
        }
        comm.CommandText = "Select Poll.ID as PollID, Title, Name +' '+ Surname as Name, Description, Poll.Title as Text, Poll.Date as Date from Poll, UserInfo where Poll.UserID=UserInfo.ID and Poll.ID=@id" + pub;

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


            dlPoll.DataSource = ds.DefaultView;
            dlPoll.DataBind();



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


        comm.CommandText = "Select PollComment.Date as Date, Name +' '+ Surname as Name, PollComment.Comment as Text from PollComment, UserInfo where PollComment.UserID=UserInfo.ID and PollComment.PollID=@id order by PollComment.Date DESC; Select TOP 4 PollComment.Date as Date, Name +' '+ Surname as Name, PollComment.Comment as Text from PollComment, UserInfo where PollComment.UserID=UserInfo.ID and PollComment.PollID=@id order by PollComment.Date DESC";

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


        comm.CommandText = "Select Top 6 Poll.ID as PollID, Poll.Title, Poll.Title as Text from Poll, UserInfo where Poll.UserID=UserInfo.ID and UserInfo.ID=(Select Poll.UserID from Poll where Poll.ID=@id) order by Poll.Date DESC; ";

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
        LatestPolls.DataSource = ds.DefaultView;
        LatestPolls.DataBind();

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
            comm.CommandText = "Select TagName, PollID from PollTagging, Tag where Tag.ID=PollTagging.TagID and PollID=@id;";
            DataTable ds = new DataTable();

            SqlCommand comm1 = new SqlCommand("Select Question.ID, Question.Text as Label, Type from Question where PollID=@id", new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString));
            comm1.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            SqlDataAdapter da1 = new SqlDataAdapter(comm1);
            DataTable ds1 = new DataTable();

            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(comm);
                da.Fill(ds);
                da1.Fill(ds1);
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
                comm1.Connection.Close();
                comm1.Connection.Dispose();
                comm1.Dispose();
            }

            DataList innerDataList = e.Item.FindControl("PollTags") as DataList;
            DataList innerDataList1 = e.Item.FindControl("dlQuestions") as DataList;

            innerDataList.DataSource = ds.DefaultView;
            TagsPanel.DataSource = ds.DefaultView;

            innerDataList1.DataSource = ds1.DefaultView;

            innerDataList.DataBind();
            TagsPanel.DataBind();
            innerDataList1.DataBind();

        }
    }

    protected void dlQuestions_DataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType== ListItemType.AlternatingItem)
        {
            DataRowView drv = e.Item.DataItem as DataRowView;

           
            int questionID = Int32.Parse(drv.Row.ItemArray[0].ToString());


            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
            SqlCommand comm = new SqlCommand();
     
            comm.Parameters.Add("@qid", System.Data.SqlDbType.Int).Value = questionID;
            comm.Connection = conn;
            comm.CommandText = "Select Answer.Text as Label, ID from Answer where QuestionID=@qid;";
            DataTable ds = new DataTable();
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(comm);
                da.Fill(ds);
                DataList innerDataList = e.Item.FindControl("dlAnswers") as DataList;

                innerDataList.DataSource = ds.DefaultView;
                innerDataList.DataBind();
            }
            catch (Exception ex) { }
            finally {
            conn.Close();
            conn.Dispose();
            comm.Dispose();
            }

            if (drv.Row.ItemArray[2].ToString() == "checkbox") {
                
                for (int i = 0; i < ds.Rows.Count;i++ ){
                    CheckBox ch1 = new CheckBox();
                    ch1.Text =ds.Rows[i]["Label"].ToString();
                    ch1.Style.Add("padding","20px");
                    ch1.ID = "qid" + questionID + "aid" + ds.Rows[i]["ID"].ToString();
                    e.Item.Controls.Add(ch1);
                    
                    if (ds.Rows[i]["Label"].ToString() == "Other") {
                        TextBox txt = new TextBox();
                       // HiddenField hf = new HiddenField();
                        //hf.Value = "qid" + questionID + "aid" + ds.Rows[i]["ID"].ToString();
                        txt.ID = "txtOther";
                        e.Item.Controls.Add(txt);
                       // e.Item.Controls.Add(hf);
                    }

                }
                    
            }
            else if (drv.Row.ItemArray[2].ToString() == "radio") {
                RadioButtonList rbl = new RadioButtonList();
                rbl.DataSource = ds;
                rbl.DataTextField = "Label";
                bool b = false;
                int aid = -1;
                for (int i = 0; i < ds.Rows.Count; i++)
                {
                   
                    rbl.Items.Add(new ListItem(ds.Rows[i]["Label"].ToString(),"qid" + questionID + "aid" + ds.Rows[i]["ID"].ToString()));
                    if (ds.Rows[i]["Label"].ToString() == "Other") {
                        b = true;
                        aid = Int32.Parse(ds.Rows[i]["ID"].ToString());
                    }
                }
                e.Item.Controls.Add(rbl);
                if (b) {
                 //   HiddenField hf = new HiddenField();
                 //   hf.Value ="qid" + questionID + "aid" + ds.Rows[aid]["ID"].ToString();
                    TextBox txt = new TextBox();
                    txt.ID = "txtOther";
                    e.Item.Controls.Add(txt);
                 //   e.Item.Controls.Add(hf);
                }
            }


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
            comm.Parameters.Add("@pollid", System.Data.SqlDbType.Int).Value = id;
            comm.Parameters.Add("@userid", System.Data.SqlDbType.Int).Value = userid;
            comm.Connection = conn;
            comm.CommandText = "INSERT INTO Subscription(subscriberID,userID) VALUES(@userid,(select Poll.UserID from Poll where Poll.Id=@pollid))";

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
            comm.Parameters.Add("@pollid", System.Data.SqlDbType.Int).Value = id;
            comm.Parameters.AddWithValue("@comment", commentBlog);
            comm.Parameters.Add("@userid", System.Data.SqlDbType.Int).Value = userid;
            comm.Connection = conn;
            comm.CommandText = "INSERT INTO PollComment(BlogID,UserID,Comment,Date) VALUES(@pollid,@userid,@comment,GETDATE())";
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
    protected void Button2_Click(object sender, EventArgs e)
    {

        for (int i = 0; i < dlPoll.Controls.Count; i++) { 
        int a=0;
        }

    }
}