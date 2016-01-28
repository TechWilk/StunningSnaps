<%@ Page Title="User | StunningSnaps" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CO5027.user._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <section class="page">
        <h2>User</h2>
        <ul>
            <li>
                <a href="~/user/add.aspx" runat="server">Add image</a>

            </li>
            <li>
                <a href="~/user/manage.aspx" runat="server">Manage Images</a>

            </li>
        </ul>

    </section>
</asp:Content>
