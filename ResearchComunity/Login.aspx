<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/Login.css" rel="Stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.2.0/angular.js"></script>
    <script language=javascript>
    var validationApp = angular.module('validationApp', []);

        // create angular controller
        validationApp.controller('mainController', function ($scope) {

            
            // function to submit the form after all validation has occurred			
            $scope.submitForm = function (isValid) {

                // check to make sure the form is completely valid
                if (isValid) {
                    
                }

            };
        });
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container" >
<div class="row" >
<div class="col-sm-6 col-md-4 col-md-offset-1">
<h1 class="text-center login-title">Login form</h1>
<div class="account-wall">
<img class="profile-img" src="https://lh5.googleusercontent.com/-b0-k99FZlyE/AAAAAAAAAAI/AAAAAAAAAAA/eu7opA4byxI/photo.jpg?sz=120"
alt="" />
 <ng-form name="userForm"  class="form-signin" novalidate id="userForm" ng-app="validationApp" ng-controller="mainController">
<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
<div class="form-group " ng-class="{ 'has-error' : userForm.emailaddress.$invalid && !userForm.emailaddress.$pristine }">
<asp:TextBox  class="form-control username" name="emailaddress" placeholder="Email" required autofocus ID="email" runat="server" ng-model="user.emailaddress" ng-pattern="/[-0-9a-zA-Z.+_]+@[-0-9a-zA-Z.+_]+\.[a-zA-Z]{2,4}/"></asp:TextBox>
<p ng-show="userForm.emailaddress.$invalid && !userForm.emailaddress.$pristine" class="help-block">Enter valid e-mail</p>
</div>
<asp:TextBox  TextMode="Password" class="form-control password" placeholder="Password" required id="pass" runat="server"></asp:TextBox>

    <asp:Button class="btn btn-lg btn-primary btn-block" ID="Button1" 
    runat="server" Text="Submit" onclick="Button1_Click" />
   

 <label class="checkbox pull-left">
 &nbsp;&nbsp;&nbsp;&nbsp;
     <asp:CheckBox ID="RememberMe"  Text="Remember me" runat="server" />
</label>
<a href="#" class="pull-right need-help">Need help? </a><span class="clearfix"></span>
<a href="Registration.aspx" class="text-left new-account">Create an account </a>
<a href="#" class="text-left new-account">Forgot my password </a>
</ng-form>
</div>

</div>
</div>
</div>

</asp:Content>

