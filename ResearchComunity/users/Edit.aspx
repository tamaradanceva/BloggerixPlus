<%@ Page Title="Edit.aspx" Language="C#" MasterPageFile="~/users/MasterUsers.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="users_BlogsEdit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script>
    function save() {
        return confirm('Are you sure you want to continue?');
    }
    function update() {
        for (instance in CKEDITOR.instances) {
            CKEDITOR.instances[instance].updateElement();
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- new blog -->
    <asp:Panel runat="server" ID="pnlBlog" Visible="false">
        <h2> <label runat="server" id="lblTitleBlog"></label> </h2>
        <hr />
        <asp:Button ID="btnDeleteBlog" Text="Delete this blog" Visible="false" runat="server" OnClick="btnDeleteBlog_Click" CssClass="btn btn-danger" />
        <div style="margin-bottom:2%"> <asp:Label runat="server" ID="lblOk" CssClass="alert alert-success" Visible="false" text="Well done! You successfully created your blog"></asp:Label></div>
        <div style="margin-bottom:2%;"><asp:Label runat="server" ID="lblError" CssClass="alert alert-danger" Visible="false" Text="Oh snap! There was error procesing your request." ></asp:Label></div>
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
        <CKEditor:CKEditorControl ID="txtMain" runat="server" EnableViewState="true" Width="700px"></CKEditor:CKEditorControl><br />
        <asp:Button ID="submitBlog" runat="server" OnClick="submitBlog_Click" OnClientClick="return update();"  cssClass="form-control" Text="Create" Width="700px" />
        <asp:CheckBox ID="chkPublic" runat="server" Text="Public" CssClass="checkbox-inline" />
        
        </form>
    </asp:Panel>
    <!-- new poll -->
    <asp:Panel runat="server" ID="pnlPoll" Visible="false">
        <h2> <label runat="server" id="lblTitlePoll"></label> </h2>
        <hr />
         <asp:Button ID="btnDeletePoll" Text="Delete this poll" Visible="false" runat="server" OnClick="btnDeletePoll_Click" CssClass="btn btn-danger" />
        <div style="margin-bottom:2%"> <asp:Label runat="server" ID="lblOkP" CssClass="alert alert-success" Visible="false" text="Well done! You successfully created your poll"></asp:Label></div>
       <div style="margin-bottom:2%;"><asp:Label runat="server" ID="LblErrorP" CssClass="alert alert-danger" Visible="false" Text="Oh snap! There was error procesing your request." ></asp:Label></div>
         <form role="form">
             <div class="form-group">
                <label for="txtTitleP">Title</label>
                 <asp:TextBox ID="txtTitleP" runat="server" CssClass="form-control" width="700px" > </asp:TextBox>
              </div>
              <div class="form-group">
                  <label for="txtDescP">Description</label>
                  <asp:TextBox ID="TxtDescP" runat="server" CssClass="form-control" TextMode="MultiLine" width="700px"> </asp:TextBox>
             </div> 
        <asp:Button ID="submitPoll" runat="server" OnClick="submitPoll_Click" cssClass="form-control" Text="Create" Width="700px" />
        <asp:CheckBox ID="chkIsPublicP" runat="server" Text="Public" CssClass="checkbox-inline" />
     
        </form>
    </asp:Panel>
    <!-- new document -->
    <asp:Panel runat="server" ID="pnlDoc" Visible="false">
        <h2><label runat="server" id="lblTitlePanel"></label></h2> 
        <hr />
        <asp:Button ID="btnDeleteDoc" Text="Delete this document" Visible="false" runat="server" OnClick="btnDeleteDoc_Click" CssClass="btn btn-danger" />
        <div style="margin-bottom:2%"> <asp:Label runat="server" ID="lblOkD" CssClass="alert alert-success" Visible="false" text="Well done! You successfully add your document"></asp:Label></div>
       <div style="margin-bottom:2%;"><asp:Label runat="server" ID="lblErrorD" CssClass="alert alert-danger" Visible="false" Text="Oh snap! There was error procesing your request." ></asp:Label></div>
         <form role="form">
             <div class="form-group">
                <label for="txtTitleD">Title</label>
                 <asp:TextBox ID="txtTitleD" runat="server" CssClass="form-control" width="700px" > </asp:TextBox>
              </div>
              <div class="form-group">
                  <label for="txtDescD">Description</label>
                  <asp:TextBox ID="txtDescD" runat="server" CssClass="form-control" TextMode="MultiLine" width="700px"> </asp:TextBox>
             </div>
             <div class="form-group">
                  <label for="txtDescD">Description</label>
                  <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control"  width="700px" style="padding:3px;"> </asp:FileUpload>
             </div>  
        <asp:Button ID="submitDoc" runat="server" OnClick="submitDoc_Click" cssClass="form-control" Text="Add document" Width="700px" />
        <asp:CheckBox ID="chkPublicD" runat="server" Text="Public" CssClass="checkbox-inline" />
        
        </form>
    </asp:Panel>
   
    
</asp:Content>

