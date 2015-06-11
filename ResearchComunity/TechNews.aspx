<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TechNews.aspx.cs" Inherits="TechNews" %>
<%@ Register TagPrefix="uc" TagName="newsFeed" 
    Src="~/userControls/NewsFeed.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
 .img-circle {
background-image: url('userControls/tw.jpg');
}
a:visited
{
color:GrayText;
} 
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc:newsFeed id="newsFeed" runat="server"  />
</asp:Content>

