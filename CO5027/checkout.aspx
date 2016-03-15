<%@ Page EnableEventValidation="false" Title="Checkout | StunningSnaps" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="checkout.aspx.cs" Inherits="CO5027.checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <section class="page">
        <asp:Panel ID="pnlBasket" runat="server">
            <h2>Basket</h2>
            <asp:Panel ID="pnlBasketItems" runat="server">
                <asp:Repeater ID="rptBasket" runat="server" OnItemCommand="rptBasket_ItemCommand">
                    <HeaderTemplate>
                        <ul class="photos basket">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <img src='<%# ResolveUrl(Eval("ProductId", "~/files/images/watermarked/{0}-3.jpg")) %>'
                                alt="<%# Server.HtmlEncode((string)Eval("ProductDescription")) %>"
                                width="<%# Eval("ImageWidth") %>"
                                height="<%# Eval("ImageHeight") %>" />
                            <div class="info">
                                <h3><%# Server.HtmlEncode((string)Eval("ProductName")) %></h3>
                                <span>(<%#((int)Eval("InitialWidth")).ToString() + " x " + ((int)Eval("InitialHeight")).ToString() %>)</span>
                                <span>£<%# Server.HtmlEncode(((decimal)Eval("Price")).ToString("0.00")) %></span>
                            </div>
                            <asp:Button ID="btnRemove" runat="server" Text="Remove" CommandArgument='<%# Eval("Id") %>' />
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:Button ID="btnContinue" runat="server" Text="Continue to Checkout" OnClick="btnContinue_Click" />
            </asp:Panel>
            <asp:Literal ID="litBasketMessage" runat="server"></asp:Literal>
        </asp:Panel>
        <asp:Panel ID="pnlCheckout" runat="server" Visible="False">
            <h2>Checkout</h2>
            <div>
                <asp:Literal ID="litConfirmOrderDetails" runat="server"></asp:Literal>
                <p>Please confirm the order details and place the order.</p>
            </div>
            <asp:Literal ID="litConfirmMessage" runat="server"></asp:Literal>
            <asp:Button ID="btnConfirmOrder" runat="server" Text="Place Order" OnClick="btnConfirmOrder_Click" />
        </asp:Panel>
        <asp:Panel ID="pnlCancel" runat="server" Visible="False">
            <h2>Cancel Order</h2>
            <asp:Literal ID="litCancelMessage" runat="server"></asp:Literal>
        </asp:Panel>
    </section>
</asp:Content>
