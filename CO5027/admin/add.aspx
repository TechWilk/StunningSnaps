<%@ Page Title="Add image | StunningSnaps" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="add.aspx.cs" Inherits="CO5027.admin.add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <section class="page">
        <h2>Add Image</h2>

        <div>
            <asp:Panel ID="pnlUploadControl" runat="server">
                <div>
                    <asp:Label ID="lblPictureUpload" runat="server" Text="Upload picture" AssociatedControlID="fUplPictureUpload"></asp:Label>
                    <asp:FileUpload ID="fUplPictureUpload" runat="server" accept="image/*" />
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlInputFields" runat="server">
                <div>
                    <asp:Label ID="lblName" runat="server" Text="Name of image" AssociatedControlID="txtName"></asp:Label>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Name is required" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <asp:Label ID="lblDescription" runat="server" Text="Describe the image" AssociatedControlID="txtDescription"></asp:Label>
                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ErrorMessage="Description is required" ControlToValidate="txtDescription"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <asp:Label ID="lblPrice" runat="server" Text="Price of image (£)" AssociatedControlID="txtPrice"></asp:Label>
                    <asp:TextBox ID="txtPrice" runat="server" TextMode="Number">5</asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPrice" runat="server" ErrorMessage="Price is required" ControlToValidate="txtPrice"></asp:RequiredFieldValidator>
                </div>
                <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
            </asp:Panel>
            <asp:Literal ID="litFeedback" runat="server"></asp:Literal>
        </div>
    </section>
</asp:Content>
