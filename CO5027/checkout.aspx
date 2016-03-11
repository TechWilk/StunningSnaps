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
                        <table>
                            <tr>
                                <th>Photo</th>
                                <th>Description</th>
                                <th>Price</th>
                                <th>Remove</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <img src='<%# ResolveUrl(Eval("ProductId", "~/files/images/watermarked/{0}-3.jpg")) %>'
                                    alt="<%# Server.HtmlEncode((string)Eval("ProductDescription")) %>"
                                    width="<%# Eval("ImageWidth") %>"
                                    height="<%# Eval("ImageHeight") %>" /></td>
                            <td><%# Server.HtmlEncode((string)Eval("ProductName")) %></td>
                            <td>£<%# Server.HtmlEncode(((decimal)Eval("Price")).ToString("0.00")) %></td>
                            <td>
                                <asp:Button ID="btnRemove" runat="server" Text="Remove" CommandArgument='<%# Eval("Id") %>' /></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:Button ID="btnContinue" runat="server" Text="Continue to Checkout" OnClick="btnContinue_Click" />
            </asp:Panel>
            <asp:Literal ID="litBasketMessage" runat="server"></asp:Literal>
        </asp:Panel>
        <asp:Panel ID="pnlCheckout" runat="server" Visible="False">
            <h2>Checkout</h2>
            <asp:Literal ID="litConfirmMessage" runat="server"></asp:Literal>
            <asp:Button ID="btnConfirmOrder" runat="server" Text="Place Order" OnClick="btnConfirmOrder_Click" />
        </asp:Panel>
        <asp:Panel ID="pnlCancel" runat="server" Visible="False">
            <h2>Cancel Order</h2>
            <asp:Literal ID="litCancelMessage" runat="server"></asp:Literal>
        </asp:Panel>
    </section>
</asp:Content>
