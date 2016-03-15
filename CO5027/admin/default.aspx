<%@ Page EnableEventValidation="false" Title="Manage images | StunningSnaps" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CO5027.admin._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <section class="page">
        <h2>Manage Photos</h2>

        <div class="links">
            <a href="~/admin/add.aspx" runat="server">Add image</a>
            <a href="~/admin/orders.aspx" runat="server">View orders</a>
        </div>

        <asp:Repeater ID="rptPhotos" runat="server" OnItemCommand="rptPhotos_ItemCommand">
            <HeaderTemplate>
                <ul class="photos">
            </HeaderTemplate>
            <ItemTemplate>
                <li class='<%# ((bool)Eval("Archived")) ? "archived" : ""  %>'>
                    <img src="<%# Eval("Id","../files/images/watermarked/{0}-3.jpg") %>" alt="<%# HttpUtility.HtmlEncode(Eval("Description")) %>" width="<%# Eval("InitialWidth") %>" height="<%# Eval("InitialHeight") %>" />
                    <div class="info">
                        <h3><%# HttpUtility.HtmlEncode(Eval("Name")) %></h3>
                        <p class="description"><%# HttpUtility.HtmlEncode(Eval("Description")) %></p>
                        <div class="links">
                            <a href='<%# Eval("Id", ResolveUrl("~/admin/add.aspx?id=") + "{0}") %>'>Edit</a>
                        </div>
                    </div>
                    <asp:Button ID="btnArchive" runat="server" Text='<%# ((bool)Eval("Archived")) ? "Unarchive" : "Archive"  %>' CommandArgument='<%# Eval("Id") %>' />
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>


        <h3>Reprocess images</h3>
        <p>If any pictures are missing watermarks, or some download options are not working, reprocess all the images.  This may take some time.</p>

        <asp:Button ID="btnReprocessImages" runat="server" Text="Reprocess All Images" OnClick="btnReprocessImages_Click" />
    </section>
</asp:Content>
