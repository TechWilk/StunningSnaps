<%@ Page Title="User | StunningSnaps" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CO5027.user._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <section class="page">
        <h2>User</h2>
        <ul>
            <li><a href="~/user/add.aspx" runat="server">Add image</a></li>
            <li><a href="~/user/manage.aspx" runat="server">Manage Images</a></li>
            <li><a href="~/user/orders.aspx" runat="server">View Orders</a></li>
            <li><a href="~/login.aspx" runat="server">Change Password</a></li>
        </ul>

        <asp:Repeater ID="rptOrders" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Date</th>
                        <th>Price</th>
                        <th>Photos</th>
                        <th>Remaining Downloads</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("Date") %></td>
                    <td><%# Eval("Price","£{0}") %></td>
                    <td><%# Eval("Photos") %></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

    </section>
</asp:Content>
