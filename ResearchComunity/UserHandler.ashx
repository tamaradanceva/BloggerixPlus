<%@ WebHandler Language="C#" Class="UserHandler" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public class UserHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        
        context.Response.ContentType = "application/json";
        
        string get = context.Request.QueryString["get"];

        string result = "";
        int id = Int32.Parse(context.Request.QueryString["id"]);
        System.Diagnostics.Debugger.Break();
        if (get == "userInfo") {
            if (id != -1 && id>0) {
                result = getUserInfo(id);
            }
        }
        
        context.Response.Write("Result for id="+id+":<br />"+result);
       // context.Response.Write(result);
         
        
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    private string getUserInfo(int id) {
       
        string ConnString = ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString;
        string SqlString = "Select Name, Surname, Address, Telephone, JoinDate, Skills, Hobbies, JobDescription, SocialMedia, FieldsOfResearch, Statement, Subscribers From UserInfo Where ID = "+id;
        

        User user = new User();
        using (SqlConnection conn = new SqlConnection(ConnString))
        {
            using (SqlCommand cmd = new SqlCommand(SqlString, conn))
            {
                cmd.CommandType = CommandType.Text;
                conn.Open();
               // cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = id;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user.Name=reader["Name"].ToString();
                        user.Surname = reader["Surname"].ToString();
                        user.Address = reader["Address"].ToString();
                        user.Telephone = reader["Telephone"].ToString();
                        user.JoinDate = reader["JoinDate"].ToString();
                        user.Skills = reader["Skills"].ToString();
                        user.Hobbies = reader["Hobbies"].ToString();
                        user.JobDescription = reader["JobDescription"].ToString();
                        user.SocialMedia = reader["SocialMedia"].ToString();
                        user.FieldsOfResearch = reader["FieldsOfResearch"].ToString();
                        user.Statement = reader["Statement"].ToString();
                        user.Subscribers = Int32.Parse(reader["Subscribers"].ToString());
                        
                    }
                }
            }
        }
        var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        string jsonString = javaScriptSerializer.Serialize(user);
      
        
        return jsonString;
    
    }

}

public class User {

    public string Name;
    public string Surname;
    public string Address;
    public string Telephone;
    public string JoinDate;
    public string Skills;
    public string Hobbies;
    public string JobDescription;
    public string SocialMedia;
    public string FieldsOfResearch;
    public string Statement;
    public int Subscribers;
}