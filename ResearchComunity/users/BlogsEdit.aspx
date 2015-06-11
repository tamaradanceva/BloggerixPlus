<%@ Page Title="" Language="C#" MasterPageFile="~/users/MasterUsers.master" EnableEventValidation="true" AutoEventWireup="true" CodeFile="BlogsEdit.aspx.cs" Inherits="users_BlogsEdit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2> Add new blog </h2>
    <hr />
    <form role="form">
         <div class="form-group">
            <label for="txtTitle">Title</label>
             <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" width="700px" > </asp:TextBox>
          </div>
          <div class="form-group">
              <label for="exampleInputPassword1">Description</label>
              <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" width="700px"> </asp:TextBox>
         </div>
       
    <label for="txtMain">Content</label>
    <CKEditor:CKEditorControl ID="txtMain" runat="server" Width="700px"></CKEditor:CKEditorControl><br />
    <asp:Button ID="submitBlog" runat="server" OnClick="submitBlog_Click" cssClass="form-control" Text="Create" Width="700px" />
    <asp:CheckBox ID="chkPublic" runat="server" Text="Public" CssClass="checkbox-inline" />
    <asp:Label runat="server" ID="lblError" Text="The blog was not created" Visible="false"></asp:Label>
    </form>
</asp:Content>

