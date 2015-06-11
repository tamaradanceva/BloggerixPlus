<%@ WebHandler Language="C#" Class="LogOut" %>

using System;
using System.Web;

public class LogOut : IHttpHandler, System.Web.SessionState.IReadOnlySessionState{
    
    public void ProcessRequest (HttpContext context) {
        context.Session["UserID"] = null;
        context.Session["UserName"] = null;
        context.Session["UserAlias"] = null;
        context.Session["UserEmail"] = null;
        context.Response.Redirect("../Default.aspx");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}