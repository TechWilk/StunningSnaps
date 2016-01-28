<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CO5027._default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="headerContentPlaceHolder" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <section>
        <h2>Photographs</h2>

        <div class="photos">

            <asp:Repeater ID="photosRepeater" runat="server" DataSourceID="CO5027SqlDataSource">
                <ItemTemplate>
                    <a class="item" href="<%# Eval("Id","photo.aspx?id={0}") %>" title="More info">
                        <figure>
                            <img src="<%# Eval("Id","files/images/{0}.jpg") %>" alt="<%# Eval("Description") %>" width="<%# Eval("InitialWidth") %>" height="<%# Eval("InitialHeight") %>"/>
                            <figcaption><%# Eval("Name") %><span><%# Eval("InitialWidth",Eval("InitialHeight","{0}")+"x{0}") %></span></figcaption>
                        </figure>
                    </a>
                </ItemTemplate>
            </asp:Repeater>

            <asp:SqlDataSource ID="CO5027SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:CO5027ConnectionString %>" SelectCommand="SELECT * FROM [Products] WHERE ([Archived] = @Archived)">
                <SelectParameters>
                    <asp:Parameter DefaultValue="False" Name="Archived" Type="Boolean" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>

    </section>
</asp:Content>
