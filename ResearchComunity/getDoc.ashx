<%@ WebHandler Language="C#" Class="getDoc" %>

using System;
using System.Web;
using System.Data.SqlClient;

public class getDoc : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType ="application/pdf";
        int id = -1;
        if (context.Request.QueryString["b"] != null)
        {

            try
            {
                id = GlobalVariables.getInt(GlobalVariables.Hash.Decrypt(context.Request.QueryString["b"]));

            }
            catch (Exception ex) { }

        }
        byte[] DocByteArray = null;
        SqlCommand comm = new SqlCommand();
        comm.Connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
        comm.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
        comm.CommandText = "Select Doc from Document where ID=@id";
        try
        {
            comm.Connection.Open();
            SqlDataReader rd = comm.ExecuteReader();
            if (rd.Read())
            {
                DocByteArray = (byte[])rd["Doc"];
            }
        }
        catch (Exception ex) { }
        finally
        {
            comm.Connection.Close();
            comm.Dispose();
        }

        if (DocByteArray != null)
            context.Response.BinaryWrite(DocByteArray);
       
        
        
        
    }
        
    public bool IsReusable {
        get {
            return false;
        }
    }

}