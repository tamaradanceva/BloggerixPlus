<%@ Page Title="" Language="C#" MasterPageFile="~/users/MasterUsers.master" AutoEventWireup="true"
    CodeFile="MySubscriptions.aspx.cs" Inherits="users_MySubscriptions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <!-- Page Heading -->
        <div class="row">
            <div class="col-lg-12">
                <h1 class="page-header">
                    My Subscriptions
                </h1>
                <ol class="breadcrumb">
                    <li><i class="fa fa-dashboard"></i><a href="MyDashboard.aspx">Dashboard</a> </li>
                    <li class="active"><i class="fa fa-bookmark"></i>My Subscriptions </li>
                </ol>
            </div>
        </div>
        <div class="panel pull-left">
            <table>
                <tr>
                    <td>
                        <span style="font-weight: bold; font-size: 17px;">Choose to see latest: </span>
                        <asp:DropDownList ID="ddlChoice" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlChoice_SelectedIndexChanged"
                            Height="50px" Style="font-size: 17px; font-weight: bold">
                            <asp:ListItem>Blogs</asp:ListItem>
                            <asp:ListItem>Documents</asp:ListItem>
                            <asp:ListItem>Polls</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <h2>
                            <asp:Label ID="Choice" runat="server" Text=""></asp:Label>
                        </h2>
                    </td>
                </tr>
                <tr>
                    <td style="padding: 5px">
                        <asp:LinkButton ID="LinkButton1" class="btn btn-inverse " runat="server" OnClick="lnkbtnPrevious_Click"><span class="glyphicon glyphicon-arrow-left"></span> Previous</asp:LinkButton>
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
            </hr>
            <div class="row">
                <div class="col-lg-12">
                    <asp:DataList ID="DataList1" runat="server" OnItemDataBound="DataList1_ItemDataBound">
                        <ItemTemplate>
                            <span class="lead" style="font-size: 15px"><a href="">
                                <%# Eval("Name") %></a> &nbsp;
                                <%# Eval("Action") %>
                                &nbsp;
                                <%# Eval("TypeOfContent")%>
                            </span>
                            <h4>
                                <a href="../<%# Eval("TypeOfContent")%>.aspx?b=<%# Eval("Text") %>">
                                    <%# Eval("Title") %></a>
                            </h4>
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
                            <a class="btn btn-primary" href="../<%# Eval("TypeOfContent")%>.aspx?b=<%# Eval("Text") %>">View Content<span class="glyphicon glyphicon-chevron-right">
                            </span>s
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
