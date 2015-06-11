using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;
using System.Configuration;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
       // string roles;
        string username = email.Text.Trim();
        UserInfo user=UserManagement.CheckUser(username, pass.Text.Trim());
        if (user !=null)
        {

            //These session values are just for demo purpose to show the user details on master page
            Session["UserID"] = user.id;
            Session["UserName"] = user.name;
            Session["UserAlias"] = user.alias;
            Session["UserEmail"] = user.email;

            //roles = DBHelper.GetUserRoles(username);
            //Session["Roles"] = roles;

            //Let us now set the authentication cookie so that we can use that later.
            FormsAuthentication.SetAuthCookie(username, false);

            //Login successful lets put him to requested page
            /*string returnUrl = Request.QueryString["ReturnUrl"] as string;

            if (returnUrl != null)
            {
                Response.Redirect(returnUrl);
            }
            else
            {*/
                //no return URL specified so lets kick him to home page
                Response.Redirect("~/users/MyDashboard.aspx");
            //}
        }
        else
        {
            Label1.Text = "Login Failed, your credentials are not valid";
            Label1.Style.Add(HtmlTextWriterStyle.Color, "red");
            Label1.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
            Label1.Style.Add(HtmlTextWriterStyle.FontSize, "15px");
        }
    }
}