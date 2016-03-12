<%@ Page Title="Photo | StunningSnaps" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="photo.aspx.cs" Inherits="CO5027.photo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <section class="page">
        <h2>Photo</h2>
        <asp:Literal ID="litPhotoInfo" runat="server"></asp:Literal>
        <asp:Button ID="btnAddToBasket" runat="server" Text="Add to Basket" OnClick="btnAddToBasket_Click" />
        <img id="imgPhoto" runat="server" />
    </section>
</asp:Content>
