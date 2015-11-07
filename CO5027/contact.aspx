<%@ Page Title="Contact | SuperSnaps" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="contact.aspx.cs" Inherits="CO5027.contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <section class="page">
        <h2>Contact</h2>
        <div class="columns two">
            <div class="contact-details">
                <h3>Contact information</h3>
                <p>Telephone: <span>01244 321123</span></p>
                <p>
                    Address: <span>SuperSnaps Studio<br />
                        23 City Road<br />
                        Chester<br />
                        CH1&nbsp;3AE</span>
                </p>
                <p>The office is open 9am - 5pm weekdays.</p>
                <p>We aim to respond to all contact within 48 hours.</p>
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
            <div class="map">
                <h2>Map</h2>
                <img src="files/images/map.png" height="905" width="1715" alt="Map with location of SuperSnaps pinpointed" />
            </div>
        </div>
    </section>
</asp:Content>
