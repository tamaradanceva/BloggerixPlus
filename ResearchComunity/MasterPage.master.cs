using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class masterPages_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Page.Header.DataBind();
        if (Session["UserName"] != null)
        {
            signin.Visible = false;

        }
        else
        {
            signedin.Visible = false;
        }
    }
}
