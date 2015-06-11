using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class users_Search : System.Web.UI.Page
{
    PagedDataSource pds = new PagedDataSource();
    public int CurrentPage
    {

        get
        {
            if (this.ViewState["CurrentPage"] == null)
                return 0;
            else
                return Convert.ToInt16(this.ViewState["CurrentPage"].ToString());
        }

        set
        {
            this.ViewState["CurrentPage"] = value;
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlPageSize.Items.Add("3");
            ddlPageSize.Items.Add("5");
            ddlPageSize.Items.Add("7");
            ddlPageSize.Items.Add("10");
            ddlPageSize.SelectedIndex = 0;
            dateWhere.SelectedIndex = 1;
            if (Session["UserID"] == null)
            {
                Response.Redirect("../Login.aspx");
            }


        }
        

    }

    
    /*Get list of blogs
    * author user id (param id) 
    */
    private void BindGrid(int id)
    {
        SqlCommand comm = new SqlCommand();
        comm.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
        string where = "";
        if (titleOrDescr.Text != "")
        {
            comm.Parameters.AddWithValue("@title", titleOrDescr.Text);
            where += "( Title Like '%'+ @title + '%' or Description Like '%'+ @title + '%' ) ";
        }
        else
        {
            //comm.Parameters.AddWithValue("@title", "");
            where += "( Title Like '%' or Description Like '%' ) ";
        }
        if (author.Text != "")
        {
            comm.Parameters.AddWithValue("@author", author.Text);
            where += "and Name Like '%'+ @author + '%' ";
        }
        if (Request.Form[txtDate.UniqueID] != "")
        {
            comm.Parameters.AddWithValue("@date", Request.Form[txtDate.UniqueID]);
            if (dateWhere.SelectedIndex == 0)
            {
                where += "and CONVERT(VARCHAR(4),DATEPART(YEAR, Date))=CONVERT(VARCHAR(4),DATEPART(YEAR, @date)) and CONVERT(VARCHAR(4),DATEPART(MONTH, Date))=CONVERT(VARCHAR(4),DATEPART(Month, @date)) and CONVERT(VARCHAR(4),DATEPART(DAY, Date))=CONVERT(VARCHAR(4),DATEPART(DAY,@date)) ";
            }
            if (dateWhere.SelectedIndex == 1)
            {
                where += "and CONVERT(VARCHAR(4),DATEPART(YEAR, Date))<=CONVERT(VARCHAR(4),DATEPART(YEAR, @date)) and CONVERT(VARCHAR(4),DATEPART(MONTH, Date))<=CONVERT(VARCHAR(4),DATEPART(Month, @date)) and CONVERT(VARCHAR(4),DATEPART(DAY, Date))<=CONVERT(VARCHAR(4),DATEPART(DAY,@date)) ";
           
            }
            if (dateWhere.SelectedIndex == 2)
            {
                where += "and CONVERT(VARCHAR(4),DATEPART(YEAR, Date))>=CONVERT(VARCHAR(4),DATEPART(YEAR, @date)) and CONVERT(VARCHAR(4),DATEPART(MONTH, Date))>=CONVERT(VARCHAR(4),DATEPART(Month, @date)) and CONVERT(VARCHAR(4),DATEPART(DAY, Date))>=CONVERT(VARCHAR(4),DATEPART(DAY,@date)) ";
           
            }
        }
        if (Tags.Text != "")
        {
            String[] tags = Tags.Text.Split(',');

            if (tags.Length > 0)
            {

                if (type.SelectedIndex == 0)
                {
                    where += " and Blog.ID in (Select BlogTagging.BlogID from BlogTagging, Tag where Blog.ID=BlogTagging.BlogID and BlogTagging.TagID=Tag.ID ";
                    for (int i = 0; i < tags.Length; i++)
                    {
                        comm.Parameters.AddWithValue("@tag" + i.ToString(), tags[i]);
                        if (i == 0)
                            where += "and ( TagName LIKE '%' + @tag" + i.ToString() + " + '%' ";
                        else
                        {
                            where += "or TagName LIKE '%' + @tag" + i.ToString() + " + '%' ";
                        }
                    }
                    where += ")) ";
                }
                if (type.SelectedIndex == 1) {
                    where += " and Doc.ID in (Select DocTagging.DocumentID from DocTagging, Tag where Doc.ID=DocTagging.DocumentID and DocTagging.TagID=Tag.ID ";
                    for (int i = 0; i < tags.Length; i++)
                    {
                        comm.Parameters.AddWithValue("@tag" + i.ToString(), tags[i]);
                        if (i == 0)
                            where += "and ( TagName LIKE '%' + @tag" + i.ToString() + " + '%' ";
                        else
                        {
                            where += "or TagName LIKE '%' + @tag" + i.ToString() + " + '%' ";
                        }
                    }
                    where += ")) ";
                
                
                }
                if (type.SelectedIndex == 2) {
                    where += " and Poll.ID in (Select PollTagging.PollID from PollTagging, Tag where Poll.ID=PollTagging.PollID and PollTagging.TagID=Tag.ID ";
                    for (int i = 0; i < tags.Length; i++)
                    {
                        comm.Parameters.AddWithValue("@tag" + i.ToString(), tags[i]);
                        if (i == 0)
                            where += "and ( TagName LIKE '%' + @tag" + i.ToString() + " + '%' ";
                        else
                        {
                            where += "or TagName LIKE '%' + @tag" + i.ToString() + " + '%' ";
                        }
                    }
                    where += ")) ";
                
                }

            }
        }



        string sql1 = "  Select UserInfo.Name +' ' + UserInfo.Surname as Name, UserInfo.ID as UserID, Blog.Title, Blog.Description, Blog.ID as BlogID, Blog.Date as Date, Blog.Title as Text, Blog.Title as TypeOfContent from UserInfo, BlogPost as Blog where UserInfo.ID=Blog.UserID and " + where + " Order By Blog.Date Desc; Select TagName, BlogID from BlogTagging, Tag where Tag.ID= BlogTagging.TagID ; ";
        string sql2 = "  Select UserInfo.Name +' ' + UserInfo.Surname as Name, UserInfo.ID as UserID, Doc.Title, Doc.Description, Doc.ID as DocID, Doc.Date as Date, Doc.Title as Text, Doc.Title as TypeOfContent from UserInfo, Document as Doc where UserInfo.ID=Doc.UserID and "+where +" Order By Doc.Date Desc; Select TagName, DocumentID from DocTagging, Tag where Tag.ID= DocTagging.TagID ; ";
        string sql3 = "  Select UserInfo.Name +' ' + UserInfo.Surname as Name, UserInfo.ID as UserID, Poll.Title, Poll.Description, Poll.ID as PollID, Poll.Date as Date, Poll.Title as Text, Poll.Title as TypeOfContent from UserInfo, Poll where UserInfo.ID=Poll.UserID and "+where+" Order By Poll.Date Desc; Select TagName, PollID from PollTagging, Tag where Tag.ID= PollTagging.TagID ;  ";


        SqlDataAdapter da = new SqlDataAdapter();
        if (type.SelectedIndex == 0)
        {
            comm.CommandText = sql1;
            da = new SqlDataAdapter(comm);
        }
        else if (type.SelectedIndex == 1)
        {
            comm.CommandText = sql2;
            da = new SqlDataAdapter(comm);
        }
        else if (type.SelectedIndex == 2)
        {
            comm.CommandText = sql3;
            da = new SqlDataAdapter(comm);
        }
        DataSet ds = new DataSet();
        da.Fill(ds);

        if (type.SelectedIndex == 0)
        {
            if (ds.Tables[0].Rows.Count != 0)
            {
                try
                {
                    ds.Relations.Add(new DataRelation("BlogType", ds.Tables[0].Columns["BlogID"], ds.Tables[1].Columns["BlogID"]));
                }
                catch (Exception ex)
                {

                }
            }
        }
        else if (type.SelectedIndex == 1)
        {
            if (ds.Tables[0].Rows.Count != 0)
            {
                try
                {
                    ds.Relations.Add(new DataRelation("DocType", ds.Tables[0].Columns["DocID"], ds.Tables[1].Columns["DocumentID"]));
                }
                catch(Exception ex){
                
                }
            }
        }
        else if (type.SelectedIndex == 2)
        {
            if (ds.Tables[0].Rows.Count != 0)
            {
                try
                {
                    ds.Relations.Add(new DataRelation("PollType", ds.Tables[0].Columns["PollID"], ds.Tables[1].Columns["PollID"]));
                }
                catch (Exception ex) { 
                }
            }
        }

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ds.Tables[0].Rows[i]["Text"] = GlobalVariables.Hash.Encrypt(GlobalVariables.getArray(Int32.Parse(ds.Tables[0].Rows[i].ItemArray[4].ToString())));
            ds.Tables[0].Rows[i]["TypeOfContent"] = type.SelectedItem.Text.ToLower();
        }
        pds.DataSource = ds.Tables[0].DefaultView;
        // pds.DataSource = dt.DefaultView;
        pds.AllowPaging = true;
        pds.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
        pds.CurrentPageIndex = CurrentPage;
        LinkButton2.Enabled = !pds.IsLastPage;
        LinkButton1.Enabled = !pds.IsFirstPage;


        DataList1.DataSource = pds;
        DataList1.DataBind();
        doPaging();
    }


    private void doPaging()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PageIndex");
        dt.Columns.Add("PageText");
        for (int i = 0; i < pds.PageCount; i++)
        {
            DataRow dr = dt.NewRow();
            dr[0] = i;
            dr[1] = i + 1;
            dt.Rows.Add(dr);

        }

        dlPaging.DataSource = dt;
        dlPaging.DataBind();
    }


    protected void dlPaging_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName.Equals("lnkbtnPaging"))
        {
            CurrentPage = Convert.ToInt16(e.CommandArgument.ToString());
            if (Session["UserID"] != null)
            {
                BindGrid((int)Session["UserID"]);
            }
            else
                Response.Redirect("../Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e) {
        CurrentPage = 0;
        if (Session["UserID"] != null)
        {
            BindGrid((int)Session["UserID"]);
        }
        else
            Response.Redirect("../Login.aspx");
    
    }
    protected void lnkbtnPrevious_Click(object sender, EventArgs e)
    {
        CurrentPage -= 1;
        if (Session["UserID"] != null)
        {
            BindGrid((int)Session["UserID"]);
        }
        else
            Response.Redirect("../Login.aspx");
    }

    protected void lnkbtnNext_Click(object sender, EventArgs e)
    {
        CurrentPage += 1;
        if (Session["UserID"] != null)
        {
            BindGrid((int)Session["UserID"]);
        }
        else
            Response.Redirect("../Login.aspx");
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        CurrentPage = 0;

        if (Session["UserID"] != null)
        {
            BindGrid((int)Session["UserID"]);
        }
        else
            Response.Redirect("../Login.aspx");
    }

    protected void dlPaging_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        LinkButton lnkbtnPage = (LinkButton)e.Item.FindControl("lnkbtnPaging");
        if (lnkbtnPage.CommandArgument.ToString() == CurrentPage.ToString())
        {
            lnkbtnPage.Enabled = false;
            lnkbtnPage.Font.Bold = true;
        }
    }

    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {

            DataRowView drv = e.Item.DataItem as DataRowView;

            DataList innerDataList = e.Item.FindControl("ContentTags") as DataList;

            if (type.SelectedIndex == 0)
            {
                /*int idd= Int32.Parse(drv.Row.ItemArray[6].ToString());
                DataTable dt = new DataTable();
                SqlConnection conn= new SqlConnection(ConfigurationManager.ConnectionStrings["Connresearch"].ConnectionString);

                SqlDataAdapter ad = new SqlDataAdapter("Select TagName, BlogID from BlogTagging, Tag where Tag.ID= BlogTagging.TagID and BlogID="+idd, conn);
                try
                {
                    ad.Fill(dt);
                }
                catch (Exception ex)
                {
                }
                finally {
                    ad.Dispose();
                }
                //"Select TagName, BlogID from BlogTagging, Tag where Tag.ID= BlogTagging.TagID";*/
                try
                {
                    innerDataList.DataSource = drv.CreateChildView("BlogType");
                }
                catch(Exception ex){}
                //innerDataList.DataSource = dt;
            }
            else if (type.SelectedIndex == 1)
            {
                try
                {
                    innerDataList.DataSource = drv.CreateChildView("DocType");
                }
                catch(Exception ex){
                }
            }
            else if (type.SelectedIndex == 2)
            {
                try
                {
                    innerDataList.DataSource = drv.CreateChildView("PollType");
                }
                catch(Exception ex){
                    
                }
            }

            innerDataList.DataBind();

        }

    }



    protected void ddlChoice_SelectedIndexChanged(object sender, EventArgs e)
    {
        CurrentPage = 0;


        if (Session["UserID"] != null)
        {
            BindGrid((int)Session["UserID"]);
        }
        else
            Response.Redirect("../Login.aspx");
    }
}