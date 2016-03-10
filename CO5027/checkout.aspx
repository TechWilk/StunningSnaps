<%@ Page Title="Checkout | StunningSnaps" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="checkout.aspx.cs" Inherits="CO5027.checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <asp:Panel ID="pnlBasket" runat="server">
        <section class="page">
            <h2>Basket</h2>
            <asp:Repeater ID="rptBasket" runat="server">
                <HeaderTemplate>
                    <table>
                        <th>
                            <td>Photo</td>
                            <td>Description</td>
                            <td>Price</td>
                        </th>
                    </table>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <img src='<%# ResolveUrl(Eval("ProductId", "~/files/images/watermarked/{0}-3.jpg")) %>'
                                alt="<%# Eval("ProductDescription") %>"
                                width="<%# Eval("ImageWidth") %>"
                                height="<%# Eval("ImageHeight") %>" /></td>
                        <td><%# Eval("ProductName") %></td>
                        <td><%# Eval("Price") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Button ID="btnContinue" runat="server" Text="Continue to Checkout" OnClick="btnContinue_Click" />
        </section>
    </asp:Panel>
    <asp:Panel ID="pnlCheckout" runat="server" Visible="False">
        <section class="page">
            <h2>Checkout</h2>
                <asp:Button ID="btnConfirmOrder" runat="server" Text="Place Order" OnClick="btnConfirmOrder_Click" />
        </section>
    </asp:Panel>
</asp:Content>
