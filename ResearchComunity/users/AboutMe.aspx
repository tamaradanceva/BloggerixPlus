<%@ Page Title="" Language="C#" MasterPageFile="~/users/MasterUsers.master" AutoEventWireup="true"
    CodeFile="AboutMe.aspx.cs" Inherits="users_AboutMe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.2.0/angular.js" t></script>
    <link rel="Stylesheet" href="../css/TabsInfo.css" />
    <script src="../js/TabsInfo.js" language="javascript"></script>
    <script>
        // create angular app
        var validationApp = angular.module('validationApp', []);

        // create angular controller
        validationApp.controller('mainController', function ($scope) {

            $scope.checked = false;
            // function to submit the form after all validation has occurred			
            $scope.submitForm = function (isValid) {

                // check to make sure the form is completely valid
                if (isValid) {
                    alert('our form is amazing');
                }

            };

        });  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <!-- Page Heading -->
        <div class="row">
            <div class="col-lg-12">
                <h1 class="page-header">
                    About Me
                </h1>
                <div>
                    <asp:Label runat="server" ID="lblMsg" Visible="false" CssClass="alert alert-info"></asp:Label></div>
                <ol class="breadcrumb">
                    <li><i class="fa fa-dashboard"></i><a href="MyDashboard.aspx">Dashboard</a> </li>
                    <li class="active"><i class="fa fa-user"></i>About Me </li>
                </ol>
            </div>
        </div>
        <ul id="tabs">
            <li><a href="#" name="tab1">General info</a></li>
            <li><a href="#" name="tab2">Image and CV</a></li>
            <li><a href="#" name="tab3">Profession</a></li>
            <li><a href="#" name="tab4">Additional info</a></li>
        </ul>
        <div id="content">
            <div id="tab1">
                <div class="form-group row ">
                    <div class="col-md-2">
                        <label for="name">
                            Name*</label>
                    </div>
                    <div class="col-md-7">
                        <div>
                            <asp:TextBox runat="server" name="name" ID="txtName" class="form-control" placeholder="Enter your name"
                                EnableViewState="true" ViewStateMode="Enabled" />
                        </div>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-md-2">
                        <label for="lastname">
                            Last Name*
                        </label>
                    </div>
                    <div>
                        <div class="col-md-7">
                            <asp:TextBox runat="server" class="form-control" ID="lastname" name="lastname" placeholder="Enter Last Name"
                                EnableViewState="true" ViewStateMode="Enabled" />
                        </div>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-md-2">
                        <label for="alias">
                            Alias:
                        </label>
                    </div>
                    <div>
                        <div class="col-md-7">
                            <asp:TextBox runat="server" class="form-control" ID="txtAlias" name="alias" placeholder="Enter alias"
                                EnableViewState="true" ViewStateMode="Enabled" />
                        </div>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-md-2">
                        <label for="address">
                            Address:
                        </label>
                    </div>
                    <div>
                        <div class="col-md-7">
                            <asp:TextBox runat="server" class="form-control" ID="txtAddress" name="address" placeholder="Enter address"
                                EnableViewState="true" ViewStateMode="Enabled" />
                        </div>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-md-2">
                        <label for="telephone">
                            Telephone:
                        </label>
                    </div>
                    <div>
                        <div class="col-md-7">
                            <asp:TextBox runat="server" class="form-control" ID="telephone" name="telephone"
                                placeholder="Enter telephone" EnableViewState="true" ViewStateMode="Enabled" />
                            <p class="help-block">
                                Format: International phone ex (+44)(0)20-12341234 , 02012341234 , +44 (0) 1234-1234
                            </p>
                            <hr />
                            <asp:Button runat="server" ID="btnSaveTab1" OnClick="btnSaveTab1_Click" CssClass="btn btn-primary"
                                Text="Save changes" />
                        </div>
                    </div>
                </div>
                <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank">Open terms in new tab</asp:HyperLink>
                <!-- tab 2222222222222222222222222222222222222222222222222222222222222222222222  -->
            </div>
            <div id="tab2">
                <h2>
                    My profile picture
                </h2>
                <div class="pull-left">
                    <asp:Image ID="Image1" runat="server" />
                </div>
                <div class="pull-right">
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <asp:Button runat="server" ID="btnSaveTab2" OnClick="btnSaveTab2_Click" CssClass="btn btn-primary form-inline"
                        Text="Save changes" />
                </div>
            </div>
            <!-- tab 3333333333333333333333333333333  -->
            <div id="tab3">
                <div class="form-group">
                    <label for="txtJob" class="col-md-2">
                        Job description:
                    </label>
                    <div class="col-md-10" style="padding: 10px">
                        <asp:TextBox class="form-control" ID="txtJob" placeholder="Write about your general or current work experience"
                            runat="server" TextMode="MultiLine" Height="200" Width="500px" EnableViewState="true"
                            ViewStateMode="Enabled"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="txtResearch" class="col-md-2">
                        Fields of research:
                    </label>
                    <div class="col-md-10" style="padding: 10px">
                        <asp:TextBox class="form-control" ID="txtResearch" placeholder="Write about your past or current research studies, or about your upcoming edeavours"
                            runat="server" TextMode="MultiLine" Height="200" Width="500px" EnableViewState="true"
                            ViewStateMode="Enabled"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group" style="background-color:White;">
                    <label for="txtStatement" class="col-md-2" style="padding: 10px">
                        Statement:
                    </label>
                    <div class="col-md-10" style="padding: 10px">
                        <asp:TextBox class="form-control" ID="txtStatement" placeholder="Write about your driving force"
                            runat="server" TextMode="MultiLine" Height="200px" Width="500px" EnableViewState="true"
                            ViewStateMode="Enabled"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group" style="background-color:White;">
                <div class="col-md-12">
                <asp:Button runat="server" ID="btnSaveTab3" OnClick="btnSaveTab3_Click" CssClass="btn btn-primary"
                    Text="Save changes" />
                    <hr />
                    </div>
                    </div>
            </div>
            <!-- tab 4444444444444444444444444444  -->
            <div id="tab4">
                <div class="form-group">
                    <label for="hobbies" class="col-md-2">
                        Hobbies and interests:
                    </label>
                    <div class="col-md-10" style="padding: 10px">
                        <asp:TextBox Style="padding: 10px" class="form-control" ID="txtHobbies" placeholder="Write about your leisure time activities"
                            runat="server" TextMode="MultiLine" Height="200px" Width="500px" EnableViewState="true"
                            ViewStateMode="Enabled"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="skills" class="col-md-2">
                        Skills:
                    </label>
                    <div class="col-md-10" style="padding: 10px">
                        <asp:TextBox Style="padding: 10px" class="form-control" ID="txtSkills" placeholder="Write about your strengths both as a person and a professional"
                            runat="server" TextMode="MultiLine" Height="200px" Width="500px" EnableViewState="true"
                            ViewStateMode="Enabled"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                <div class="col-md-12">
                <asp:Button runat="server" ID="btnSaveTab4_Click" OnClick="btnSaveTab5_Click" CssClass="btn btn-primary"
                    Text="Save changes" />
                    <hr />
                    </div>
                    </div>
            </div>
        </div>
    </div>
</asp:Content>
