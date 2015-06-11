using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class masterPages_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button_SignUp(object sender, EventArgs e)
    {
        Response.Redirect("Registration.aspx");
    }
    protected void Button_TechNews(object sender, EventArgs e)
    {
        Response.Redirect("TechNews.aspx");
    }
    protected void Button_Login(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }
}