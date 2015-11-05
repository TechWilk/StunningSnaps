<%@ Page Title="Contact | SuperSnaps" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="contact.aspx.cs" Inherits="CO5027.contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <section class="columns two">
        <h2>Contact</h2>
        <div>
            <h3>Contact information</h3>
            <p>Telephone:<span>01244 321123</span></p>
            <p>
                Address: <span>123 Some street<br />
                    Some Place<br />
                    This Town<br />
                    POST CODE</span>
            </p>
        </div>
        <div>
            <h3>Email</h3>
            <div>
                <asp:Label ID="lblName" runat="server" Text="Name" AssociatedControlID="txtName"></asp:Label>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="lblEmail" runat="server" Text="Email" AssociatedControlID="txtEmail"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            </div>

            <div>
                <asp:Label ID="lblEmailConfirm" runat="server" Text="Confirm email" AssociatedControlID="txtEmailConfirm"></asp:Label>
                <asp:TextBox ID="txtEmailConfirm" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="lblMessage" runat="server" Text="Message" AssociatedControlID="txtMessage"></asp:Label>
                <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>

            <asp:Button ID="btnSendEmail" runat="server" Text="Send" OnClick="btnSendEmail_Click" />
            <asp:Literal ID="litResponseMessage" runat="server"></asp:Literal>
        </div>

    </section>
    <section>
        <h2>Map</h2>
    </section>
</asp:Content>
