<%@ Page Title="" Language="C#" MasterPageFile="~/users/MasterUsers.master" AutoEventWireup="true"
    CodeFile="MyBlogs.aspx.cs" Inherits="users_MyBlogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <!-- Page Heading -->
        <div class="row">
            <div class="col-lg-12">
                <h1 class="page-header">
                    My Blogs
                </h1>
                <ol class="breadcrumb">
                    <li><i class="fa fa-dashboard"></i><a href="MyDashboard.aspx">Dashboard</a> </li>
                    <li class="active"><i class="fa fa-pencil-square"></i>My Blogs </li>
                </ol>
                <a href="Edit.aspx?t=blog" class="btn btn-info"><span class=" glyphicon glyphicon-plus-sign"></span> Add new blog </a>
            </div>
        </div>
        <div class="panel pull-left">
            <table>
                <tr>
                    <td style="padding: 5px">
                        <asp:LinkButton ID="LinkButton1" class="btn btn-inverse " runat="server" OnClick="lnkbtnPrevious_Click"><span class="glyphicon glyphicon-arrow-left"></span> Previous</asp:LinkButton>
                    </td>
                    <td style="padding: 5px">
                        <small>Posts per page:</small>
                        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
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
                    <td>
                        <div class="input-group">
                            <asp:TextBox class="form-control" placeholder="Search" name="srch-term" ID="search"
                                runat="server" />
                            <div class="input-group-btn">
                                <button class="btn btn-default" type="submit" id="btnSearch" runat="server" onserverclick="btnSearch_Click">
                                    <i class="glyphicon glyphicon-search"></i>
                                </button>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <div class="row">
                <div class="col-lg-12">
                    <asp:DataList ID="DataList1" runat="server" 
                        onitemdatabound="DataList1_ItemDataBound">
                        <ItemTemplate>
                            <h2>
                                <a href="../blog.aspx?b=<%# Eval("Text") %>">
                                    <%# Eval("Title") %></a>
                            </h2>
                            <p class="lead">
                                by <a href="#">
                                    <%# Eval("Name") %></a>
                            </p>
                            <p>
                                
                                <span class="glyphicon glyphicon-time">
                                </span>Posted on
                                <%# Eval("Date") %></p>
                            <p>
                                <%# Eval("Description") %></p>
                            <asp:DataList ID="BlogTags" runat="server" RepeatDirection="Horizontal" >
                            <ItemTemplate>
                           <a href="#" style="padding:5px; border-style:inset"><span class="glyphicon glyphicon-tag"></span><%# Eval("TagName") %></a> &nbsp; 
                            </ItemTemplate>
                            </asp:DataList>
                            </br>
                            <a class="btn btn-primary" href ="../blog.aspx?b=<%# Eval("Text") %>" >View Blog<span class="glyphicon glyphicon-chevron-right">
                            </span></a>&nbsp;&nbsp;&nbsp;&nbsp<a href="Edit.aspx?t=blog&b=<%# Eval("Text") %>" class="btn btn-primary"><i class="glyphicon glyphicon-edit">
                            </i>Edit </a>&nbsp;&nbsp;&nbsp;&nbsp
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
