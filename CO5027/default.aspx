<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CO5027._default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="headerContentPlaceHolder" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <section>
        <h2>Catergories</h2>
        <p>Main page content</p>
    </section>
    <section class="photos">
        <h2>Photographs</h2>

        <a class="item" href="#" title="More info">
            <figure>
                <img alt="Some image" src="files/images/image1.jpg" height="1600" width="2560" />
                <figcaption>Description about image</figcaption>
            </figure>
            <small>1000x100</small>
            <p>From £5.00</p>
        </a>
        <!-- /.item -->

        <a class="item" href="#" title="More info">
            <figure>
                <img alt="Some image" src="files/images/image2.png" height="1200" width="1920" />
                <figcaption>Description about image</figcaption>
            </figure>
            <small>532x345</small>
            <p>Link to buy image</p>
        </a>
        <!-- /.item -->

        <a class="item" href="#" title="More info">
            <figure>
                <img alt="Some image" src="files/images/image3.jpg" height="1333" width="1999" />
                <figcaption>Description about image</figcaption>
            </figure>
            <small>876x954</small>
            <p>Find out more information</p>
        </a>
        <!-- /.item -->

    </section>
</asp:Content>
