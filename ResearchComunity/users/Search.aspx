<%@ Page Title="" Language="C#" MasterPageFile="~/users/MasterUsers.master" AutoEventWireup="true"
    CodeFile="Search.aspx.cs" Inherits="users_Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"
        type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("[id$=txtDate]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: '../images/calendar.png',
                dateFormat: 'yy-mm-dd'
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <!-- Page Heading -->
        <div class="row">
            <div class="col-lg-12">
                <h1 class="page-header">
                    Search
                </h1>
                <ol class="breadcrumb">
                    <li><i class="fa fa-dashboard"></i><a href="MyDashboard.aspx">Dashboard</a> </li>
                    <li class="active"><i class="fa fa-search"></i>Search </li>
                </ol>
            </div>
        </div>
        <div class="form-group row ">
            <div class="col-md-2">
                <label for="titleOrDescr">
                    Search by title or description:
                </label>
            </div>
            <div class="col-md-7">
                <div>
                    <asp:TextBox runat="server" name="titleOrDescr" ID="titleOrDescr" class="form-control"
                        EnableViewState="true" />
                </div>
            </div>
        </div>
        <div class="form-group row ">
            <div class="col-md-2">
                <label for="author">
                    Search by author name:</label>
            </div>
            <div class="col-md-7">
                <div>
                    <asp:TextBox runat="server" name="author" ID="author" class="form-control" EnableViewState="true" />
                </div>
            </div>
        </div>
        <div class="form-group row ">
            <div class="col-md-2">
                <label for="type">
                    Search by type:</label>
            </div>
            <div class="col-md-7">
                <div>
                    <asp:DropDownList ID="type" runat="server" EnableViewState="true">
                       
                        <asp:ListItem>Blog</asp:ListItem>
                        <asp:ListItem>Document</asp:ListItem>
                        <asp:ListItem>Poll</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="form-group row ">
            <div class="col-md-3">
                
                <label for="date" style="padding:7px">
                    Search by date:</label>
                <asp:DropDownList ID="dateWhere" runat="server">
                    <asp:ListItem >On</asp:ListItem>
                     <asp:ListItem >Before </asp:ListItem>
                      <asp:ListItem >After</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-7">
                <div>
                    <asp:TextBox ID="txtDate" runat="server" ReadOnly="true" EnableViewState="True"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="form-group row ">
            <div class="col-md-2">
                <label for="date" style="padding:7px">
                    Search by tags:</label>
               
            </div>
            <div class="col-md-7">
                <div>
                    <asp:TextBox ID="Tags" placeholder="exampletag1, exampletag2 ..." runat="server" EnableViewState="True"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="input-group">
        <asp:LinkButton ID="btnSearch" class="btn btn-info" runat="server" OnClick="btnSearch_Click" PostBackUrl="Search.aspx"><span class="glyphicon glyphicon-search"></span> Search</asp:LinkButton>
            
        </div>
        <hr />
        <div class="panel pull-left">
            <table>
                
                <tr>
                    <td style="padding: 5px">
                        <asp:LinkButton ID="LinkButton1" class="btn btn-inverse " runat="server" OnClick="lnkbtnPrevious_Click" ><span class="glyphicon glyphicon-arrow-left"></span> Previous</asp:LinkButton>
                    </td>
                    <td style="padding: 5px">
                        <small>Posts per page:</small>
                        <asp:DropDownList ID="ddlPageSize" runat="server" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged"
                            EnableViewState="True" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                    <td style="padding: 5px">
                        <asp:DataList ID="dlPaging" runat="server" OnItemCommand="dlPaging_ItemCommand" OnItemDataBound="dlPaging_ItemDataBound"
                            RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtnPaging" Style="padding: 3px" runat="server" CommandArgument='<%# Eval("PageIndex") %>'
                                    CommandName="lnkbtnPaging" Text='<%# Eval("PageText") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                    <td style="padding: 5px">
                        <asp:LinkButton ID="LinkButton2" class="btn btn-inverse " runat="server" OnClick="lnkbtnNext_Click"><span class="glyphicon glyphicon-arrow-right"></span> Next</asp:LinkButton>
                    </td>
                </tr>
            </table>
            <hr/>
            <div class="row">
                <div class="col-lg-12">
                    <asp:DataList ID="DataList1" runat="server" OnItemDataBound="DataList1_ItemDataBound">
                        <ItemTemplate>
                            <h2>
                                <a href="../<%# Eval("TypeOfContent")%>.aspx?b=<%# Eval("Text") %>">
                                    <%# Eval("Title") %></a>
                            </h2>
                            <h3>
                            by &nbsp;
                                <a href="#">
                                    <%# Eval("Name") %></a>
                            </h3>
                            <p>
                                <span class="glyphicon glyphicon-time"></span>Posted on
                                <%# Eval("Date") %></p>
                            <p>
                                <%# Eval("Description") %></p>
                            <br>
                                <asp:DataList ID="ContentTags" runat="server" RepeatDirection="Horizontal">
                                    <ItemTemplate>
                                        <a href="#" style="padding: 5px; border-style: inset"><span class="glyphicon glyphicon-tag">
                                        </span>
                                            <%# Eval("TagName") %></a> &nbsp;
                                    </ItemTemplate>
                                </asp:DataList>
                            </br>
                            <a class="btn btn-primary" href="href="../<%# Eval("TypeOfContent")%>.aspx?b=<%# Eval("Text") %>">View Content<span class="glyphicon glyphicon-chevron-right">
                            </span></a>
                            <hr />
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>
        <div class="panel pull-right">
            <asp:DataList ID="dlComments" runat="server">
                <ItemTemplate>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
</asp:Content>
