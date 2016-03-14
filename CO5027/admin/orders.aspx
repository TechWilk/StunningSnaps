<%@ Page Title="Manage orders | StunningSnaps" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="orders.aspx.cs" Inherits="CO5027.admin.orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <section class="page">
        <h2>Manage Orders</h2>
        <asp:Repeater ID="rptOrders" runat="server" OnItemCommand="rptOrders_ItemCommand">
            <ItemTemplate>
                <div class="order">
                    <p><strong>Order: <%# Eval("Id") %></strong></p>
                    <p>Date: <%# Eval("DateStamp") %></p>
                    <p>Price: <%# Eval("TotalCost", "£{0}") %></p>
                    <%# ((bool)Eval("Cancelled")) ? "<p class=\"warning\">Cancelled<p>" : "" %>
                    <asp:Button ID="btnCancelOrder" runat="server" Text="Cancel Order" CommandName="CancelOrder" CommandArgument='<%# Eval("Id") %>' Visible='<%# !(bool)Eval("Cancelled") %>'/>
                    <asp:Repeater ID="rptOrderProducts" runat="server" DataSource='<%# Eval("OrderedProducts") %>' OnItemCommand="rptOrderedProducts_ItemCommand">
                        <ItemTemplate>
                            <div class="product">
                                <p><strong><%# Server.HtmlEncode((string)Eval("Product.Name")) %></strong></p>
                                <p>Remaining Downloads: <%# ((int)Eval("DownloadsAllowed") - (int)Eval("DownloadCount")) %></p>
                                <asp:Button ID="btnAddDownload" runat="server" Text="Add Download" CommandName="AddDownload" CommandArgument='<%# Eval("Id") %>' Visible='<%# !(bool)Eval("Order.Cancelled") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Literal ID="litNoOrders" runat="server" />
    </section>
</asp:Content>
