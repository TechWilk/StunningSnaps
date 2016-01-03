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
            <div class="email">
                <h3>Email</h3>
                <asp:Panel ID="pnlContactForm" runat="server">
                    <div>
                        <asp:Label ID="lblName" runat="server" Text="Name" AssociatedControlID="txtName"></asp:Label>
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ErrorMessage="Name is required." ControlToValidate="txtName" runat="server" Display="Dynamic" />
                    </div>
                    <div>
                        <asp:Label ID="lblEmail" runat="server" Text="Email" AssociatedControlID="txtEmail"></asp:Label>
                        <asp:TextBox ID="txtEmail" placeholder="your@email.address" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ErrorMessage="Email address is required." ControlToValidate="txtEmail" runat="server" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server" ErrorMessage="Invalid email address. Please enter a valid email address." Display="Dynamic" ControlToValidate="txtEmail" ValidationExpression="\w+(.+\w+)*[@]\w+([.]\w+)+"></asp:RegularExpressionValidator>
                    </div>

                    <div>
                        <asp:Label ID="lblEmailConfirm" runat="server" Text="Confirm email" AssociatedControlID="txtEmailConfirm"></asp:Label>
                        <asp:TextBox ID="txtEmailConfirm" placeholder="your@email.address" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ErrorMessage="Email address is required." ControlToValidate="txtEmailConfirm" runat="server" Display="Dynamic" />
                        <asp:CompareValidator ID="EmailCompareValidator" runat="server" ErrorMessage="Email addresses do not match. Please retype them." Display="Dynamic" ControlToCompare="txtEmail" ControlToValidate="txtEmailConfirm"></asp:CompareValidator>
                    </div>
                    <div>
                        <asp:Label ID="lblMessage" runat="server" Text="Message" AssociatedControlID="txtMessage"></asp:Label>
                        <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ErrorMessage="Message is required." ControlToValidate="txtMessage" runat="server" Display="Dynamic" />
                    </div>

                    <asp:Button ID="btnSendEmail" runat="server" Text="Send" OnClick="btnSendEmail_Click" />
                </asp:Panel>
                <asp:Literal ID="litResponseMessage" runat="server"></asp:Literal>
            </div>
            <div class="map">
                <h2>Map</h2>
                <img src="files/images/map.png" height="905" width="1715" alt="Map with location of SuperSnaps pinpointed" />
            </div>
        </div>
    </section>
</asp:Content>
