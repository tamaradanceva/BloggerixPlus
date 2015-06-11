<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" type="text/css" href="css/about-us.css">
    <link rel="stylesheet" type="text/css" href="css/framework.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="margin:0;padding:0">
    <div><img src="images/aboutusjpg.jpg" width="1350px" height="300px"/></div>
    <div style="margin-left:10%;">
        <div style="margin-bottom:2%;"> 
            <div style="float:left" class="panel panel-default">
                 <div class="panel-heading">
                    <div class="panel-title"><h2>What is BlogerixPlus</h2></div>
                  </div>
                 <div class="panel-body">
                   <div style="width:30%;float:left;">
                      <p>BloggerixPlus is a web app for creating blogs, polls or document posts. Also it includes editing, managing and review other people ' s posts.</p>
                      <p>This project its stil in its early stage, and theres many improvments that can be made.Some of them are: </p>
                       <ul style="list-style-type:none;" class="list-group"> 
                            <li class="list-group-item">Making notifications active</li>
                            <li class="list-group-item">Full profile page</li>
                           <li class="list-group-item">Better and richer polls </li>
                           <li class="list-group-item">Search mode implementarion </li>
                           <li class="list-group-item">And many others ... </li>                       
                       <hr />
                       
                        <h4>Main feautures</h4>
                            <ul style="list-style-type:none;" class="list-group"> 
                            <li class="list-group-item">Stay in touch with technology, with TechNews</li>
                            <li class="list-group-item">Create and edit your own blogs</li>
                            <li class="list-group-item">Upload and share your documents</li>
                            <li class="list-group-item">Make subscription's to other users</li>
                            <li class="list-group-item">Make yours, or participate in other polls</li>
                         </ul>
                        <hr />
                       
                   </div>
                    <div style="margin-left:3%; width:50%;float:left">
                            <img src="images/ssblog.png" class="img-thumbnail"/> 
                            <img src="images/ssdoc.png" class="img-thumbnail"/> 
                    </div>
                 </div>
            </div>
         </div>
    </div>
</div>
</asp:Content>

