using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for UserManagement
/// </summary>
public class UserManagement
{
	
		//Validate the user from DB
    public static UserInfo CheckUser(string username, string password)
    {
        DataTable result = null;
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select ID, Name, Surname, Alias, EmailLogin, password from UserInfo where EmailLogin = @uname";
                    cmd.Parameters.Add(new SqlParameter("@uname", username));

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        result = new DataTable();
                        da.Fill(result);
                    }

                    if (password.Trim() == result.Rows[0]["password"].ToString().Trim())
                    {
                        //user id found and password is matched too so lets do something now
                        UserInfo userInfo = new UserInfo();
                        userInfo.id = Int32.Parse(result.Rows[0]["ID"].ToString());
                        userInfo.name = result.Rows[0]["Name"].ToString() + " " + result.Rows[0]["Surname"].ToString();
                        userInfo.alias= result.Rows[0]["Alias"].ToString();
                        userInfo.email= result.Rows[0]["EmailLogin"].ToString();
                        //return Int32.Parse(result.Rows[0]["ID"].ToString());
                        return userInfo;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //Pokemon exception handling
            return null;
        }

        //user id not found, lets treat him as a guest        
        return null;
    }


    //Get the Roles for this particular user
    public static string GetUserRoles(string username)
    {
        DataTable result = null;
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    // seuste ne e dodadeno roles 
                    cmd.CommandText = "select roles from UserInfo where username = @uname";
                    cmd.Parameters.Add(new SqlParameter("@uname", username));

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        result = new DataTable();
                        da.Fill(result);
                    }

                    if (result.Rows.Count == 1)
                    {
                        return result.Rows[0]["roles"].ToString().Trim();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //Pokemon exception handling
        }

        //user id not found, lets treat him as a guest        
        return "guest";
    }
} 

public class UserInfo {
    public string name {get; set;}
    public int id {get; set;}
    public string email { get; set; }
    public string alias { get; set; }

}