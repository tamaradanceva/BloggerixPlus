<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="background:url('images/contactus.png');width:1366px;height:500px;top:0px">
    <div style="width:300px;float:left; margin-top:1%;margin-left:20%;background-color:#fcfcfc;border-radius:5px;padding:0px 30px;padding-bottom:10px;opacity:0.8;">
    <h2 style="color:cornflowerblue;">Contact us</h2> 
   <form role="form">
   <div class="form-group">
    <label for="intputName" style="font-size:small;color:cornflowerblue;">Name</label>
    <asp:TextBox ID="txtName" CssClass="form-control" runat="server" placeholder="(required)" ></asp:TextBox>
  </div>
  <div class="form-group">
    <label for="exampleInputEmail1" style="font-size:small;color:cornflowerblue;">Email</label>
      <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" placeholder="xxx@something.com" ></asp:TextBox>
  </div>
       
  <div class="form-group">
    <label for="intputSubject" style="font-size:small;color:cornflowerblue;">Subject</label>
        <asp:TextBox ID="txtSubject" CssClass="form-control" runat="server"  ></asp:TextBox>
  </div>
   <div class="form-group">
    <label for="intputSubject" style="font-size:small;color:cornflowerblue;">Message </label>
       <asp:TextBox ID="txtMessage" CssClass="form-control" runat="server" placeholder="(required)" TextMode="MultiLine" height="20%"></asp:TextBox>
  </div>

    </form>
       <asp:Button ID="Button_Message" runat="server" Text="Send" OnClick="Button_Message_Click" width="100" style="border-radius:3px;" CssClass="btn-primary fa-align-center bt" />
        <asp:Label ID="lblMessage" runat="server" Text="" Visible="false"></asp:Label>
    </div>
 </div>
</asp:Content>

