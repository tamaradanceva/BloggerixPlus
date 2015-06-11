<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="masterPages_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="margin-left:auto;margin-right:auto;">
    <div id="carousel-example-generic" class="carousel slide" data-ride="carousel" >
  <!-- Indicators -->
  <ol class="carousel-indicators">
    <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
    <li data-target="#carousel-example-generic" data-slide-to="1"></li>
    <li data-target="#carousel-example-generic" data-slide-to="2"></li>
  </ol>

  <!-- Wrapper for slides -->
  <div class="carousel-inner">
    <div class="item active">
      <img src="images/header1.png" alt="...">
      <div class="carousel-caption">
        <h2 style="color:white;font-size:5em"> Share your thoughs</h2>
          <p style="color:white;font-size:2em">Join our community</p>
          <p >.</p>
          <asp:Button ID="btnDefaultSign" runat="server" CssClass="btn-primary" style="border-radius:5px;font-size:2em;width:20%;font-family:sans-serif" OnClick="Button_SignUp" Text="Sign up" />
          <p >.</p>
          <br />
      </div>
        
    </div>
    <div class="item">
      <img src="images/header2.png" alt="...">
      <div class="carousel-caption">
        <h2 style="color:white;font-size:5em">Want some quick tech news ?</h2>
          <p style="color:white;font-size:2em">Try the  public TechNews area</p>
          <p >.</p>
          <asp:Button ID="btnTechNews" runat="server" CssClass="btn-primary" style="border-radius:5px;font-size:2em;width:20%;font-family:sans-serif" OnClick="Button_TechNews" Text="TechNews" />
          <p >.</p>
      </div>
    </div>
      <div class="item">
      <img src="images/header3.png" alt="...">
      <div class="carousel-caption">
       <h2 style="color:white;font-size:5em"> Allready have an account </h2>
          <p style="color:white;font-size:2em">Login now, and start blogerixING </p>
          <p >.</p>
          <asp:Button ID="btnLogin" runat="server" CssClass="btn-primary" style="border-radius:5px;font-size:2em;width:20%;font-family:sans-serif" OnClick="Button_Login" Text="Login" />
          <p >.</p>
      </div>
    </div>
  </div>

  <!-- Controls -->
  <a class="left carousel-control" href="#carousel-example-generic" role="button" data-slide="prev">
    <span class="glyphicon glyphicon-chevron-left"></span>
  </a>
  <a class="right carousel-control" href="#carousel-example-generic" role="button" data-slide="next">
    <span class="glyphicon glyphicon-chevron-right"></span>
  </a>
</div>
    <br />
     <div class="jumbotron" style="width:80%;margin:auto;">
      <h2>Hello BloggerixERS</h2>
      <p>Take a deeper look on your possibilites on Bloggerix.Check out the public TechNews area. Register and try the full feauture mode of BlogerixPlus </p>
      <p><a class="btn btn-primary btn-lg" role="button">Learn more</a></p> 
    </div>
    <div class="jumbotron" style="width:80%;margin:auto;">
      <h2>Create and edit your blogs</h2>
      <p>Make your own blog posts.  Make them public or private. Preview other users's blogs. </p>
      <p><a class="btn btn-primary btn-lg" role="button">Learn more</a></p>
    </div> 
    <div class="jumbotron" style="width:80%;margin:auto;">
      <h2>Upload and preview your usefull documents</h2>
      <p>Upload all of your documents. Make them accesible to other users. Full search document mode enabled. </p>
      <p><a class="btn btn-primary btn-lg" role="button">Learn more</a></p>
    </div>
        <div class="jumbotron" style="width:80%;margin:auto;">
      <h2>Make subscription's to other users</h2>
      <p>Easy to connect with other users, just one click away. </p>
      <p><a class="btn btn-primary btn-lg" role="button">Learn more</a></p>
    </div>
    <div class="jumbotron" style="width:80%;margin:auto;">
      <h2>Make yours, or participate in other polls</h2>
      <p>Got some intresting poll idea, feel free to ask your subscribiers, or " the public " for their opinion.</p>
      <p><a class="btn btn-primary btn-lg" role="button">Learn more</a></p>
    </div>
</div>

</asp:Content>

