<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>
<html>
<head>
    <title>Create new account</title>
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.2.0/angular.js"></script>
    
    <!-- Bootstrap -->
    <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css">
    <!-- custom styles -->
    <link href="css/custom.css" rel="stylesheet">
    <link href="css/prettify.css" rel="stylesheet">
    <!-- Custom CSS -->
    <style>
        body
        {
            padding-top: 70px; /* Required padding for .navbar-fixed-top. Remove if using .navbar-static-top. Change if height of navigation changes. */
        }
        .required:after
        {
            content: "*";
            padding-left: 1%;
        }
        
        .form-control
        {
            width: 97%;
            float: left;
        }
    </style>
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    
    <!-- JS -->
    
    <script>
        // create angular app
        var validationApp = angular.module('validationApp', []);

        // create angular controller
        validationApp.controller('mainController', function ($scope) {

            $scope.checked = true;
            // function to submit the form after all validation has occurred			
            $scope.submitForm = function (isValid) {

                // check to make sure the form is completely valid
                if (isValid) {
                    
                }

            };

        });
        validationApp.directive('match', function () {
            return {
                require: 'ngModel',
                restrict: 'A',
                scope: {
                    match: '='
                },
                link: function (scope, elem, attrs, ctrl) {
                    scope.$watch(function () {
                        return (ctrl.$pristine && angular.isUndefined(ctrl.$modelValue)) || scope.match === ctrl.$modelValue;
                    }, function (currentValue) {
                        ctrl.$setValidity('match', currentValue);
                    });
                }
            };
        });
        

    
    </script>
    <script>
        $(document).ready(function () {
            
            $("form").submit(function (e) {
                alert("form submit caught");
                e.preventDefault();
            });
        });	
    </script>
</head>
<body >
   
    <!-- Navigation -->
    <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container">
            <!-- Brand and toggle get grouped for better mobile display -->
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="#">BloggerixPlus</a>
            </div>
            <!-- Collect the nav links, forms, and other content for toggling -->
            <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                <ul class="nav navbar-nav">
                    <li>
                        <a href="#">About</a>
                    </li>
                    <li>
                        <a href="#">TechNews</a>
                    </li>
                    <li>
                        <a href="#">Browse</a>
                    </li>
                    <li>
                        <a href="#">Contact</a>
                    </li>
                </ul>
            </div>
            <!-- /.navbar-collapse -->
        </div>
        <!-- /.container -->
    </nav>
    <div class="jumbotron" style="padding: 10px">
        <img alt="Banner" src="images/Banner.png" height="150px" width="1000px" />
    </div>
    <section>
    
    <div class='container'>
        <div class="h2">
            <h2>
                Registration form</h2>
        </div>
        <div id="rootwizard" >
            <ul>
                <li><a href="#tab1" data-toggle="tab"><span class="label">1</span> General information</a></li>
                <li><a href="#tab2" data-toggle="tab"><span class="label">2</span> Upload image</a></li>
                <li><a href="#tab3" data-toggle="tab"><span class="label">3</span> Profession</a></li>
                <li><a href="#tab4" data-toggle="tab"><span class="label">4</span> Additional info</a></li>
                <li><a href="#tab5" data-toggle="tab"><span class="label">5</span> Find me on</a></li>
                <li><a href="#tab6" data-toggle="tab"><span class="label">6</span> Submit</a></li>
            </ul>

    <form novalidate runat="server"   >
     <asp:Label ID="lblSuccessReg" runat="server" Text=""></asp:Label>
    <p> <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label></p>
    <ng-form name="userForm" novalidate id="userForm" ng-app="validationApp" ng-controller="mainController">
            <div class="tab-content" runat="server" id="tabContent">
                <div class="tab-pane" id="tab1">
                    <div class="container" style="margin: 10px;">

        
        <table>
        <!-- NAME -->
        <tr class="form-group" ng-class="{ 'has-error' : userForm.name.$invalid && !userForm.name.$pristine }">
            <td>
            <label for="name" class="col-md-2">Name*</label>
            </td>
            <td>
            <div class="col-md-10">
            <asp:TextBox runat="server" name="name" ID="name" class="form-control" ng-model="user.name" required placeholder="Enter your name" EnableViewState="true" ViewStateMode="Enabled" />
            <p ng-show="userForm.name.$invalid && !userForm.name.$pristine" class="help-block" >You name is required.</p>
            </div>
            </td>
        </tr>
                            <tr class="form-group" ng-class="{ 'has-error' : userForm.lastname.$invalid && !userForm.lastname.$pristine }">
                                <td>
                                    <label for="lastname" class="col-md-2">
                                        Last Name*
                                    </label>
                                </td>
                                <td>
                                    <div class="col-md-10">
                                        <asp:TextBox runat="server"  class="form-control" ID="lastname" name="lastname" ng-model="user.lastname" placeholder="Enter Last Name" required EnableViewState="true" ViewStateMode="Enabled"/>
                                        <p ng-show="userForm.lastname.$invalid && !userForm.lastname.$pristine" class="help-block">You last name is required.</p>
                                    </div>
                                </td>
                                
                            </tr>
                            <tr class="form-group " ng-class="{ 'has-error' : userForm.emailaddress.$invalid && !userForm.emailaddress.$pristine }">
                                <td>
                                    <label for="emailaddress" class="col-md-2">
                                        Email address*
                                    </label>
                                </td>
                                <td>
                                    <div class="col-md-10">
                                        <asp:TextBox runat="server"  class="form-control" ID="emailaddress" name="emailaddress" placeholder="Enter email address" ng-model="user.emailaddress" ng-pattern="/[-0-9a-zA-Z.+_]+@[-0-9a-zA-Z.+_]+\.[a-zA-Z]{2,4}/" EnableViewState="true" ViewStateMode="Enabled"
                                             required />
                                            
                                        <p class="help-block">
                                            Example: yourname@domain.com
                                        </p>
                                        <p ng-show="userForm.emailaddress.$invalid && !userForm.emailaddress.$pristine" class="help-block">Enter valid e-mail</p>
                                    </div>
                                </td>
                                
                            </tr>
                            <tr class="form-group" ng-class="{ 'has-error' : userForm.password.$invalid && !userForm.password.$pristine }" >
                                <td>
                                    <label for="password" class="col-md-2">
                                        Password*
                                    </label>
                                </td>
                                <td>
                                    <div class="col-md-10">
                                        <asp:TextBox runat="server" TextMode="Password" class="form-control" ID="password" name="password" ng-model="user.password" ng-pattern="/(?=^.{8,}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s)[0-9a-zA-Z!@#$%^&*()]*$/" placeholder="Enter Password"
                                              required />
                                        <p class="help-block">
                                            Password must have at least 8 characters with at least one Capital letter, at least one lower case letter and at least one number.
                                        </p>
                                         <p ng-show="userForm.password.$invalid && !userForm.password.$pristine" class="help-block">Your password is not strong enoguh, enter valid password</p>
                                    </div>
                                </td>
                                

                            </tr>
                            <tr class="form-group" ng-class="{ 'has-error' : userForm.passAgain.$invalid && !userForm.passAgain.$pristine }">
                                <td>
                                    <label for="passAgain" class="col-md-2">
                                        Repeat password*
                                    </label>
                                </td>
                                <td>
                                    <div class="col-md-10">
                                        <asp:TextBox runat="server" TextMode="Password" class="form-control" ID="passAgain" name="passAgain" ng-model="user.passAgain" placeholder="Enter Password"
                                             required match="user.password"/>
                                             <p ng-show="userForm.passAgain.$invalid && !userForm.passAgain.$pristine" class="help-block">Password must match</p>
                                            
                                    </div>
                                </td>
                              
                            </tr>
                            <tr class="form-group">
                                <td>
                                    <label for="address" class="col-md-2">
                                        Address:
                                    </label>
                                </td>
                                <td>
                                    <div class="col-md-10">
                                        <asp:TextBox runat="server" class="form-control" ID="address" placeholder="Enter address"  runat="server"
                                            />
                                    </div>
                                </td>
                            </tr>
                            <tr class="form-group" ng-class="{ 'has-error' : userForm.telephone.$invalid && !userForm.telephone.$pristine }">
                                <td>
                                    <label for="telephone" class="col-md-2">
                                        Telephone:
                                    </label>
                                </td>
                                <td>
                                    <div class="col-md-10">
                                        <asp:TextBox runat="server"  EnableViewState="true" ViewStateMode="Enabled" class="form-control" ID="telephone" name="telephone" placeholder="Enter telephone number" ng-pattern="/^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$/"
                                            />
                                        <p class="help-block">

                                            Format: International phone ex (+44)(0)20-12341234 , 02012341234 , +44 (0) 1234-1234
                                        </p>
                                        <p ng-show="userForm.telephone.$invalid && !userForm.telephone.$pristine" class="help-block">Enter valid telephone number</p>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="tab-pane" id="tab2">
                    <asp:FileUpload ID="imgUpload" runat="server" EnableViewState="true"  ViewStateMode="Enabled" />
                    <p class="help-block">
                        Only jpg and png supported, maximum size of 5MB
                    </p>
                    <br />
                    <asp:Label ID="lblErrorImage" runat="server" Text=""></asp:Label>
                </div>
                <div class="tab-pane" id="tab3">
                    <table>
                        <tr class="form-group">
                            <td>
                                <label for="txtJob" class="col-md-2">
                                    Job description:
                                </label>
                            </td>
                            <td>
                                <div class="col-md-10" style="padding: 10px">
                                    <asp:TextBox class="form-control" ID="txtJob" placeholder="Write about your general or current work experience"
                                        runat="server" TextMode="MultiLine" Height="200" Width="500px" EnableViewState="true"  ViewStateMode="Enabled"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr class="form-group">
                            <td>
                                <label for="txtResearch" class="col-md-2">
                                    Fields of research:
                                </label>
                            </td>
                            <td>
                                <div class="col-md-10" style="padding: 10px">
                                    <asp:TextBox class="form-control" ID="txtResearch" placeholder="Write about your past or current research studies, or about your upcoming edeavours"
                                        runat="server" TextMode="MultiLine" Height="200" Width="500px" EnableViewState="true"  ViewStateMode="Enabled"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr class="form-group">
                            <td>
                                <label for="txtStatement" class="col-md-2" style="padding: 10px">
                                    Statement:
                                </label>
                            </td>
                            <td>
                                <div class="col-md-10" style="padding: 10px">
                                    <asp:TextBox class="form-control" ID="txtStatement" placeholder="Write about your driving force"
                                        runat="server" TextMode="MultiLine" Height="200px" Width="500px" EnableViewState="true" ViewStateMode="Enabled"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="tab-pane" id="tab4">
                    <table>
                        <tr class="form-group">
                            <td>
                                <label for="hobbies" class="col-md-2">
                                    Hobbies and interests:
                                </label>
                            </td>
                            <td>
                                <div class="col-md-10" style="padding: 10px">
                                    <asp:TextBox Style="padding: 10px" class="form-control" ID="txtHobbies" placeholder="Write about your leisure time activities"
                                        runat="server" TextMode="MultiLine" Height="200px" Width="500px" EnableViewState="true" ViewStateMode="Enabled"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr class="form-group">
                            <td>
                                <label for="skills" class="col-md-2">
                                    Skills:
                                </label>
                            </td>
                            <td>
                                <div class="col-md-10" style="padding: 10px">
                                    <asp:TextBox Style="padding: 10px" class="form-control" ID="skills" placeholder="Write about your strengths both as a person and a professional"
                                        runat="server" TextMode="MultiLine" Height="200px" Width="500px" EnableViewState="true" ViewStateMode="Enabled"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="tab-pane" id="tab5">
                    <table>
                        <tr>
                            <td style="padding: 10px">
                                <a href="http://facebook.com">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="images/logoFB.jpg" /></a>
                            </td>
                            <td style="padding: 10px">
                                <a href="http://twitter.com">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="images/logoTwitter.jpg" /></a>
                            </td>
                            <td style="padding: 10px">
                                <a href="http://linkedin.com">
                                    <asp:Image ID="Image4" runat="server" ImageUrl="images/logoLinkedin.png" /></a>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr class="form-group">
                            <td>
                                <label for="txtSocial" class="col-md-2">
                                    Social media links:
                                </label>
                            </td>
                            <td>
                                <div class="col-md-10" style="padding: 10px">
                                    <asp:TextBox Style="padding: 10px" class="form-control" ID="txtSocial" placeholder="Share your Facebook, Twitter, LinkedIN or other website's profile link. You can also share your website if you have one or any other web resources you might think may relevant"
                                        runat="server" TextMode="MultiLine" Height="200px" Width="500px" EnableViewState="true" ViewStateMode="Enabled"></asp:TextBox>
                                    <p class="help-block">
                                        Separate links by a comma
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="tab-pane" id="tab6">
                    <br />
                    <object data="TermsOfAgreement.pdf" type="application/pdf" width="100%" height="500px">
                        <p>
                            It appears you don't have a PDF plugin for this browser. No biggie... you can <a
                                href="TermsOfAgreement.pdf">click here to download the PDF file.</a></p>
                    </object>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="TermsOfAgreement.pdf"
                        Target="_blank">Open terms in new tab</asp:HyperLink>
                    <br />
                    <div class="form-group" ng-class="{ 'has-error' : userForm.checkTerms.$invalid && !userForm.checkTerms.$pristine }" >
                    <div class="col-md-10">
                    <asp:CheckBox ID="checkTerms" name="checkTerms" runat="server" EnableViewState="true" ViewStateMode="Enabled" ng-model="checked" Text=" I have read and accept the terms of agreement"/>
                    <p ng-show="userForm.checkTerms.$invalid && !userForm.checkTerms.$pristine" class="help-block">You must accept our terms in order to register.</p>
                    </div>
                    </div>
                    <br />
                   <asp:Button UseSubmitBehavior="true" ID="Submit" class="btn btn-primary" 
                        runat="server"  Text="Register" ng-disabled="user.checked==false" 
                        ng-click="submitForm(userForm.$valid)" width="220px" onclick="Submit_Click"></asp:Button>

                    
                </div>
                <ul class="pager wizard">
                    <li class="previous first" style="display: none;"><a href="#">First</a></li>
                    <li class="previous"><a href="#">Previous</a></li>
                    <li class="next last" style="display: none;"><a href="#">Last</a></li>
                    <li class="next"><a href="#">Next</a></li>
                </ul>
            </div>
            </ng-form>
 

            </form>
            
        </div>
    </div>
   
    </section>
    <script src="http://code.jquery.com/jquery-latest.js"></script>
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"></script>
    <script src="js/jquery.bootstrap.wizard.js"></script>
    <script src="js/prettify.js"></script>
    <script>
        $(document).ready(function () {
            $('#rootwizard').bootstrapWizard({ 'tabClass': 'bwizard-steps' });
            window.prettyPrint && prettyPrint()
            $('#form1').submit(false);
            $('#userForm').submit(false);
            
        });	
    </script>
</body>
</html>
