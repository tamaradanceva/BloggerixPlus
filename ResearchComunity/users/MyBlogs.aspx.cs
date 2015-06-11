using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class users_MyBlogs : System.Web.UI.Page
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
            if (Session["UserID"] != null)
            {
                BindGrid((int)Session["UserID"]);
            }
            else
                Response.Redirect("../Login.aspx");
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        int id = -1;
        if (Session["UserID"] != null)
        {
            id=(int)Session["UserID"];
        }
        else
            Response.Redirect("../Login.aspx");

        if (id != -1)
        {
            SqlConnection conn= new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@Search",search.Text);
            string sql = "Select Blog.ID as BlogID, Title, Name +' '+ Surname as Name, Description, Blog.Date, Blog.Title as Text  from BlogPost as Blog, UserInfo where Blog.UserID=UserInfo.ID and UserInfo.ID=" + id + " and ( Title Like '%'+ @Search + '%' or Description Like '%'+ @Search + '%' )  Order By Blog.Date; Select TagName, BlogID from BlogTagging, Tag where Tag.ID= BlogTagging.TagID ; ";
            comm.CommandText = sql;
            SqlDataAdapter da = new SqlDataAdapter(comm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if(ds.Tables[0].Rows.Count>0)
            ds.Relations.Add(new DataRelation("BlogTags", ds.Tables[0].Columns["BlogID"], ds.Tables[1].Columns["BlogID"]));

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ds.Tables[0].Rows[i]["Text"] = GlobalVariables.Hash.Encrypt(GlobalVariables.getArray(Int32.Parse(ds.Tables[0].Rows[i].ItemArray[0].ToString())));
            }

            pds.DataSource = ds.Tables[0].DefaultView;
            pds.AllowPaging = true;
            pds.PageSize = Convert.ToInt16(ddlPageSize.SelectedValue);
            pds.CurrentPageIndex = CurrentPage;
            LinkButton2.Enabled = !pds.IsLastPage;
            LinkButton1.Enabled = !pds.IsFirstPage;


            DataList1.DataSource = pds;
            DataList1.DataBind();
            doPaging();
        }
    }


    /*Get list of blogs
    * author user id (param id) 
    */
    private void BindGrid(int id)
    {
        string sql = "Select Blog.ID as BlogID, Title, Name +' '+ Surname as Name, Description, Blog.Date, Blog.Text from BlogPost as Blog, UserInfo where Blog.UserID=UserInfo.ID and UserInfo.ID=" + id + " Order By Blog.Date; Select TagName, BlogID from BlogTagging, Tag where Tag.ID= BlogTagging.TagID ;";
        SqlDataAdapter da = new SqlDataAdapter(sql, ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
        //DataTable dt = new DataTable();
        //da.Fill(dt);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            try
            {
                ds.Relations.Add(new DataRelation("BlogTags", ds.Tables[0].Columns["BlogID"], ds.Tables[1].Columns["BlogID"]));
            }
            catch (Exception ex) { 
            }
            
        }
        pds.DataSource = ds.Tables[0].DefaultView;

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ds.Tables[0].Rows[i]["Text"] = GlobalVariables.Hash.Encrypt(GlobalVariables.getArray(Int32.Parse(ds.Tables[0].Rows[i].ItemArray[0].ToString())));
        }
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
            Response.Redirect("../Login.aspx"); ;
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

            DataList innerDataList = e.Item.FindControl("BlogTags") as DataList;

            innerDataList.DataSource = drv.CreateChildView("BlogTags");

            innerDataList.DataBind();

        }
    }
}