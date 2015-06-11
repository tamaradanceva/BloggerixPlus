<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsFeed.ascx.cs" Inherits="WebUserControl" %>
<div class="container">
    <div id="rssFeed1" style="font-family: Arial, Helvetica, sans-serif; font-weight: bold;>
        <asp:DataList ID="DataList1" runat="server" DataSourceID="XmlDataSource1" >
            <ItemTemplate>
                <div class="row">
                    <div class="col-md-4 col-sm-4 .col-lg-4 text-center">
                        <a class="story-img" href="http://techworld.com" target="_blank" style="background-image: url('tw.jpg'); margin-top:25%;display:block; ">
                            <img src='<%#XPath("enclosure/@url")%>' alt='<%#XPath("image")%>' style="width: 170px;
                                height: 170px" class="img-circle">
                        </a>
                    </div>
                    <div class="col-md-8 col-sm-8 .col-lg-8">
                        <h3>
                            <%# XPath("title") %></h3>
                        <div class="row">
                            <div class="col-xs-9">
                                <p class="text-justify">
                                    <%# XPath("description") %></p>
                                <p class="lead">
                                    <a href="<%# XPath("link") %>" target="_blank" style="padding:15px; border-color: Black; border-bottom-width:medium; border-bottom-style:inset;text-decoration:none">
                                            Read More</a></p>
                                <p class="list-inline">
                                    <%# XPath("pubDate") %>
                                </p>
                                
                            </div>
                            <br>
                            <br>
                        </div>
                    </div>
                </div>
                <hr />
            </ItemTemplate>
            
        </asp:DataList>
    </div>
    <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="http://rss.feedsportal.com/c/270/f/3547/index.rss"
        XPath="rss/channel/item"></asp:XmlDataSource>
</div>
