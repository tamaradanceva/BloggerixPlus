using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class users_MySubscriptions : System.Web.UI.Page
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
            ddlChoice.SelectedIndex = 0;
            Choice.Text = "Latest blogs";
            if (Session["UserID"] != null)
            {
                BindGrid((int)Session["UserID"]);
            }
            else
                Response.Redirect("../Login.aspx");
        }
        else
        {
            BindGrid((int)Session["UserID"]);
        }
    }


    /*Get list of blogs
    * author user id (param id) 
    */
    private void BindGrid(int id)
    {
        string sql1 = "  Select TOP 20 UserInfo.Name +' ' + UserInfo.Surname as Name, UserInfo.ID as UserID,TypeOfContent, Action, Blog.Title, Blog.Description, Blog.ID as BlogID, Blog.Date as Date, Blog.Title as Text from Logger, UserInfo, BlogPost as Blog where Logger.UserID!=" + id + "and Logger.UserID in (Select userID from Subscription where subscriberID="+id+") and Logger.UserID=UserInfo.ID and Logger.ContentID=Blog.ID and TypeOfContent='blog' Order By Logger.Date Desc; Select TagName, BlogID from BlogTagging, Tag where Tag.ID= BlogTagging.TagID ; ";
        string sql2 = "  Select TOP 20 UserInfo.Name +' ' + UserInfo.Surname as Name, UserInfo.ID as UserID, TypeOfContent, Action, Doc.Title, Doc.Description, Doc.ID as DocID, Doc.Date as Date, Doc.Title as Text from Logger, UserInfo, Document as Doc where Logger.UserID!=" + id + "and Logger.UserID in (Select userID from Subscription where subscriberID=" + id + ") and Logger.UserID=UserInfo.ID and Logger.ContentID=Doc.ID and TypeOfContent='document' Order By Logger.Date Desc; Select TagName, DocumentID from DocTagging, Tag where Tag.ID= DocTagging.TagID ; ";
        string sql3 = "  Select TOP 20 UserInfo.Name +' ' + UserInfo.Surname as Name, UserInfo.ID as UserID, TypeOfContent, Action, Poll.Title, Poll.Description, Poll.ID as PollID, Poll.Date as Date, Poll.Title as Text from Logger, UserInfo, Poll where Logger.UserID!=" + id + "and Logger.UserID in (Select userID from Subscription where subscriberID=" + id + ") and Logger.UserID=UserInfo.ID and Logger.ContentID=Poll.ID and TypeOfContent='poll' Order By Logger.Date Desc; Select TagName, PollID from PollTagging, Tag where Tag.ID= PollTagging.TagID ;  ";
        SqlDataAdapter da = new SqlDataAdapter();
        if (ddlChoice.SelectedIndex == 0)
        {
            da = new SqlDataAdapter(sql1, ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
        }
        else if (ddlChoice.SelectedIndex == 1)
        {
            da = new SqlDataAdapter(sql2, ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
        }
        else if (ddlChoice.SelectedIndex == 2)
        {
            da = new SqlDataAdapter(sql3, ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
        }
        DataSet ds = new DataSet();
        da.Fill(ds);

        if (ddlChoice.SelectedIndex == 0)
        {
            if (ds.Tables[0].Rows.Count != 0)
                ds.Relations.Add(new DataRelation("BlogType", ds.Tables[0].Columns["BlogID"], ds.Tables[1].Columns["BlogID"]));
        }
        else if (ddlChoice.SelectedIndex == 1)
        {
            if (ds.Tables[0].Rows.Count != 0)
                ds.Relations.Add(new DataRelation("DocType", ds.Tables[0].Columns["DocID"], ds.Tables[1].Columns["DocumentID"]));
        }
        else if (ddlChoice.SelectedIndex == 2)
        {
            if (ds.Tables[0].Rows.Count != 0)
                ds.Relations.Add(new DataRelation("PollType", ds.Tables[0].Columns["PollID"], ds.Tables[1].Columns["PollID"]));
        }

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ds.Tables[0].Rows[i]["Text"] = GlobalVariables.Hash.Encrypt(GlobalVariables.getArray(Int32.Parse(ds.Tables[0].Rows[i].ItemArray[6].ToString())));
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

            if (ddlChoice.SelectedIndex == 0)
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
                innerDataList.DataSource = drv.CreateChildView("BlogType");
                //innerDataList.DataSource = dt;
            }
            else if (ddlChoice.SelectedIndex == 1)
            {
                innerDataList.DataSource = drv.CreateChildView("DocType");
            }
            else if (ddlChoice.SelectedIndex == 2)
            {
                innerDataList.DataSource = drv.CreateChildView("PollType");
            }

            innerDataList.DataBind();

        }

    }



    protected void ddlChoice_SelectedIndexChanged(object sender, EventArgs e)
    {
        CurrentPage = 0;
        Choice.Text = "Latest " + ddlChoice.SelectedItem.Text;

        if (Session["UserID"] != null)
        {
            BindGrid((int)Session["UserID"]);
        }
        else
            Response.Redirect("../Login.aspx");
    }
}