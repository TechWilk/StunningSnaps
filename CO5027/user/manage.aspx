<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manage.aspx.cs" Inherits="CO5027.user.manage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <section class="page">
        <h2>Manage Photos</h2>

        <asp:Button ID="btnReprocessImages" runat="server" Text="Reprocess Images" OnClick="btnReprocessImages_Click" />
    </section>
</asp:Content>
