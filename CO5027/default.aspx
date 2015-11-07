<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CO5027._default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="headerContentPlaceHolder" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <section>
        <h2>Photographs</h2>

        <div class="photos">
            <a class="item" href="#" title="More info">
                <figure>
                    <img alt="Some image" src="files/images/image1.jpg" height="1600" width="2560" />
                    <figcaption>Description about image<span>1000x100</span></figcaption>
                </figure>
            </a>
            <!-- /.item -->

            <a class="item" href="#" title="More info">
                <figure>
                    <img alt="Some image" src="files/images/image2.png" height="1200" width="1920" />
                    <figcaption>Description about image<span>543x4895</span></figcaption>
                </figure>
            </a>
            <!-- /.item -->

            <a class="item" href="#" title="More info">
                <figure>
                    <img alt="Some image" src="files/images/image3.jpg" height="1333" width="1999" />
                    <figcaption>Description about image<span>893x2078</span></figcaption>
                </figure>
            </a>
            <!-- /.item -->

            <a class="item" href="#" title="More info">
                <figure>
                    <img alt="Some image" src="files/images/image4.jpg" height="2736" width="3648" />
                    <figcaption>Description about image<span>3648x2736</span></figcaption>
                </figure>
            </a>
            <!-- /.item -->

            <a class="item" href="#" title="More info">
                <figure>
                    <img alt="Some image" src="files/images/image5.jpg" height="2592" width="4608" />
                    <figcaption>Description about image<span>2592x4608</span></figcaption>
                </figure>
            </a>
            <!-- /.item -->
        </div>

    </section>
</asp:Content>
