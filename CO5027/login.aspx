<%@ Page Title="Login | StunningSnaps" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="CO5027.user.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <section class="page">
        <asp:Panel ID="pnlLogin" runat="server">
            <h2>Login</h2>
            <div class="login">
                <div>
                    <asp:Literal runat="server" ID="litError" />
                </div>
                <div>
                    <asp:Label runat="server" AssociatedControlID="txtLoginUsername">User name</asp:Label>
                    <asp:TextBox runat="server" ID="txtLoginUsername" />
                    <asp:RequiredFieldValidator ID="LoginUsernameRequiredFieldValidator" runat="server" ErrorMessage="Username is required" ValidationGroup="Login" ControlToValidate="txtLoginUsername"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <asp:Label runat="server" AssociatedControlID="txtLoginPassword">Password</asp:Label>
                    <asp:TextBox runat="server" ID="txtLoginPassword" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="LoginPasswordRequiredFieldValidator" runat="server" ErrorMessage="Password is required" ValidationGroup="Login" ControlToValidate="txtLoginPassword"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <asp:Button ID="btnLogin" runat="server" Text="Log in" OnClick="btnLogin_Click" ValidationGroup="Login" />
                    <asp:Button ID="btnRegisterShow" runat="server" Text="Register" OnClick="btnRegisterShow_Click" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlRegister" runat="server" Visible="False">
            <h2>Register</h2>
            <div class="register">
                <div>
                    <asp:Label runat="server" AssociatedControlID="txtRegisterUsername">Username</asp:Label>
                    <asp:TextBox runat="server" ID="txtRegisterUsername" />
                    <asp:RequiredFieldValidator ID="RegisterUsernameRequiredFieldValidator" runat="server" ErrorMessage="Username is required" ValidationGroup="Register" ControlToValidate="txtRegisterUsername"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <asp:Label runat="server" AssociatedControlID="txtRegisterPassword">Password</asp:Label>
                    <asp:TextBox runat="server" ID="txtRegisterPassword" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="RegisterPasswordRequiredFieldValidator" runat="server" ErrorMessage="Password is required" ValidationGroup="Register" ControlToValidate="txtRegisterPassword"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <asp:Label runat="server" AssociatedControlID="txtRegisterConfirmPassword">Confirm password</asp:Label>
                    <asp:TextBox runat="server" ID="txtRegisterConfirmPassword" TextMode="Password" />
                    <asp:CompareValidator ID="RegisterConfirmPasswordCompareValidator" runat="server" ErrorMessage="Passwords do not match, please try again." ValidationGroup="Register" ControlToValidate="txtRegisterPassword" ControlToCompare="txtRegisterConfirmPassword"></asp:CompareValidator>
                </div>
                <div>
                    <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" ValidationGroup="Register" />
                </div>

            </div>
        </asp:Panel>
        <asp:Panel ID="pnlEdit" runat="server" Visible="False">
            <h2>Edit</h2>
            <div class="edit">
                <div>
                    <asp:Label runat="server" AssociatedControlID="txtEditPassword">Password</asp:Label>
                    <asp:TextBox runat="server" ID="txtEditPassword" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="EditPasswordRequiredFieldValidator" runat="server" ErrorMessage="Password is required" ValidationGroup="Edit" ControlToValidate="txtEditPassword"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <asp:Label runat="server" AssociatedControlID="txtEditConfirmPassword">Confirm password</asp:Label>
                    <asp:TextBox runat="server" ID="txtEditConfirmPassword" TextMode="Password" />
                    <asp:CompareValidator ID="EditConfirmPasswordCompareValidator" runat="server" ErrorMessage="Passwords do not match, please try again." ValidationGroup="Edit" ControlToValidate="txtEditPassword" ControlToCompare="txtEditConfirmPassword"></asp:CompareValidator>
                </div>
                <div>
                    <asp:Button ID="btnEdit" runat="server" Text="Change Password" OnClick="btnEdit_Click" ValidationGroup="Edit" />
                </div>
            </div>
        </asp:Panel>
    </section>
</asp:Content>
