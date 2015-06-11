<%@ WebHandler Language="C#" Class="getImage" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

public class getImage : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "image/jpeg";
        int id = -1;
        if (context.Request.QueryString["i"] != null) {

            try
            {
                id = GlobalVariables.getInt(GlobalVariables.Hash.Decrypt(context.Request.QueryString["i"]));
                
            }
            catch(Exception ex){}

            byte[] ImageByteArray = null;
            SqlCommand comm = new SqlCommand();
            comm.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnResearch"].ConnectionString);
            comm.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            comm.CommandText = "Select Image from UserInfo where ID=@id";
            try
            {
                comm.Connection.Open();
                SqlDataReader rd = comm.ExecuteReader();
                if (rd.Read())
                {
                    ImageByteArray = (byte[])rd["Image"];
                }
            }
            catch (Exception ex) { }
            finally {
                comm.Connection.Close();
                comm.Dispose();
            }
            if (ImageByteArray != null) {
                System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(ImageByteArray, false);
                System.Drawing.Image imgFromGB = System.Drawing.Image.FromStream(memoryStream);
                imgFromGB.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            
            }
            
            
        }
        

        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}