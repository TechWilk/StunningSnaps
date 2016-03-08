<%@ Page Title="Checkout | StunningSnaps" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="checkout.aspx.cs" Inherits="CO5027.checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <section class="page">
        <h2>Checkout</h2>
        <asp:Repeater ID="rptBasket" runat="server">
            <HeaderTemplate>
                <table>
                    <th>
                        <td>Photo</td>
                        <td>Description</td>
                        <td>Size</td>
                        <td>Price</td>
                    </th>
                </table>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><img src="<%# Eval("Images.Product.Id", "~/files/images/watermarked/{0}.jpg") %>"
                             alt="<%# Eval("Images.Product.Description") %>"
                             width="100<%# Eval("Images.Width") %>"
                             height="100<%# Eval("Images.Height") %>" /></td>
                    <td><%# Eval("Images.Product.Name") %></td>
                    <td><%# Eval("Images.Size.Name") %></td>
                    <td><%# Eval("Images.Size.Price") %></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </section>
</asp:Content>
