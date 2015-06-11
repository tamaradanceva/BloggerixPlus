<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="blog.aspx.cs" Inherits="Blogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/blog.css" type="text/css" rel="Stylesheet"/>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Body -->
    <div class="container">
        <div class="row">
            <div class="col-md-8 col-md-offset-1">
                <asp:DataList ID="dlBlog" runat="server" OnItemDataBound="dlBlog_ItemDataBound">
                    <ItemTemplate>
                        <div class="row">
                            <h2>
                                <a href="#">
                                    <%# Eval("Title") %></a>
                            </h2>
                            <p class="lead">
                                by <a href="#">
                                    <%# Eval("Name") %></a>
                            </p>
                            <asp:DataList ID="BlogTags" runat="server" RepeatDirection="Horizontal">
                                <ItemTemplate>
                                    <a href="#" style="padding: 5px; border-style: inset"><span class="glyphicon glyphicon-tag">
                                    </span>
                                        <%# Eval("TagName") %></a> &nbsp;
                                </ItemTemplate>
                            </asp:DataList>
                            <br />
                            <div>
                                &nbsp;&nbsp;<span class="glyphicon glyphicon-time"></span><%# Eval("Date") %></div>
                        </div>
                        <hr>
                        <p>
                            <span>Description: </span>
                            <%# Eval("Description") %></p>
                        <p class="lead">
                            <%# Eval("Text") %></p>
                        <hr />
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                <ul class="pager">
                    <li class="previous"><a href="TechNews.aspx">&larr; Back to posts</a></li>
                </ul>
                <div class="well" style="height: 185px">
                    <h4>
                        Leave a comment</h4>
                    <form role="form" class="clearfix">
                    <div class="col-md-12 form-group">
                        <label class="sr-only" for="email">
                            Comment</label>
                        <textarea class="form-control" id="comment" runat="server" placeholder="Comment"></textarea>
                    </div>
                    <div class="col-md-12 form-group text-right">
                        <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Submit" 
                            UseSubmitBehavior="true" onclick="Button1_Click" />
                    </div>
                    </form>
                </div>
                <hr />
                <ul id="comments" class="comments">
                    <asp:DataList ID="dlComments" runat="server">
                        <ItemTemplate>
                            <li class="comment">
                                <div class="clearfix">
                                    <h4 class="pull-left">
                                        <%# Eval("Name") %></h4>
                                    <p class="pull-right">
                                        <%# Eval("Date") %></p>
                                </div>
                                <p>
                                    <em>
                                        <%# Eval("Text") %></em>
                                </p>
                            </li>
                        </ItemTemplate>
                    </asp:DataList>
                </ul>
            </div>
            <div class="col-md-3">
                <div class="well text-center">
                    <p class="lead">
                        Don't want to miss updates? Please click the below button!
                    </p>
                    <Asp:Button class="btn btn-primary btn-lg" id="subscribe" UseSubmitBehavior="true" runat="server" Text="Subscribe to my feed" OnClick="subscribe_Click" >
                        </asp:Button>
                </div>
                <!-- Latest Posts -->
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4>
                            Latest Posts</h4>
                    </div>
                    <ul class="list-group">
                        <asp:DataList ID="LatestBlogs" runat="server">
                            <ItemTemplate>
                                <li class="list-group-item"><a href="blog.aspx?b=<%# Eval("Text") %>">
                                    <%# Eval("Title") %></a></li>
                            </ItemTemplate>
                        </asp:DataList>
                    </ul>
                </div>
                <!-- Tags -->
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4>
                            Tags</h4>
                    </div>
                    <div class="panel-body">
                        <ul class="list-inline">
                            <asp:DataList ID="TagsPanel" runat="server">
                            <ItemTemplate>
                            <li><a href="#"><%# Eval("TagName") %></a></li>
                            </ItemTemplate>
                            </asp:DataList>
                            
                        </ul>
                    </div>
                </div>
                <!-- Recent Comments -->
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4>
                            Recent Comments</h4>
                    </div>
                    <ul class="list-group">
                        <asp:DataList ID="dlRecentComments" runat="server">
                            <ItemTemplate>
                                <li class="list-group-item"><a href="#">
                                    <%# Eval("Text") %>
                                    - <em>
                                        <%# Eval("Name") %></em></a></li>
                            </ItemTemplate>
                        </asp:DataList>
                    </ul>
                </div>
            </div>
        </div>
</asp:Content>
