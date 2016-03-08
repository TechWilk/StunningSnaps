<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="manage.aspx.cs" Inherits="CO5027.user.manage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <section class="page">
        <h2>Manage Photos</h2>

        <asp:Repeater ID="rptPhotos" runat="server" OnItemCommand="rptPhotos_ItemCommand">
            <HeaderTemplate>
                <table>
                    <th>
                        <td></td>
                        <td>Name</td>
                        <td>Archived</td>
                    </th>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <img src="<%# Eval("Id","../files/images/watermarked/{0}-sm.jpg") %>" alt="<%# Eval("Description") %>" width="<%# Eval("InitialWidth") %>" height="<%# Eval("InitialHeight") %>" />
                    </td>
                    <td>
                        <%# Eval("Name") %>
                    </td>
                    <td>
                        <asp:Button ID="btnArchive" runat="server" Text='<%# ((bool)Eval("Archived")) ? "Unarchive" : "Archive"  %>' CommandArgument='<%# Eval("Id") %>' />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>


        <h3>Reprocess images</h3>
        <p>If any pictures are missing watermarks, or some download options are not working, reprocess all the images.  This may take some time.</p>

        <asp:Button ID="btnReprocessImages" runat="server" Text="Reprocess All Images" OnClick="btnReprocessImages_Click" />
    </section>
</asp:Content>
