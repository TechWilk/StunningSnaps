<%@ Page Title="User | StunningSnaps" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CO5027.user._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <section class="page">
        <h2>User</h2>
        <p><a href="~/login.aspx" runat="server">Change Password</a></p>
        <h3>Orders</h3>
        <asp:Repeater ID="rptOrders" runat="server">
            <HeaderTemplate>
                <div class="orders">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="order">
                    <p><strong>Order: <%# Eval("Id") %></strong></p>
                    <p>Date: <%# Eval("DateStamp") %></p>
                    <p>Paid: <%# Eval("TotalCost","£{0}") %></p>
                    <asp:Repeater ID="rptOrderedProducts" runat="server" DataSource='<%# Eval("OrderedProducts") %>'>
                        <ItemTemplate>
                            <div class="product">
                                <p><strong><%# Server.HtmlEncode((string)Eval("Product.Name")) %></strong></p>
                                <p>Remaining Downloads: <%# (int)Eval("DownloadsAllowed") - (int)Eval("DownloadCount") %></p>
                                <%# ((int)Eval("DownloadsAllowed") - (int)Eval("DownloadCount") > 0) ? ("<a href=\"" + Eval("ProductId",ResolveUrl("~/user/download.aspx?id={0}")) + "\">Download</a>") : "" %>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </ItemTemplate>
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>

    </section>
</asp:Content>
