<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="GererCommandes.aspx.cs" Inherits="Pages_GererCommandes" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <!-- Pour ajouter des imports dans le head -->
    <link rel="stylesheet" href="../static/style/catalogueVendeur.css">
    <link rel="stylesheet" href="../static/style/sideBarNav.css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="Server">

    <script>
        $(document).ready(function () {

            $("#menu-toggle").click(function(e) {
                e.preventDefault();
                $("#wrapper").toggleClass("active");
            });
            /*Scroll Spy*/
            $('body').scrollspy({ target: '#spy', offset: 80 });
        });      
    </script>
    <!-- Contenu de la page -->
    <div id="wrapper">
        <!-- Sidebar -->
        <div id="sidebar-wrapper">
            <nav id="spy">
                <ul runat="server" class="sidebar-nav nav" id="ulSideBar">                    
                </ul>
            </nav>
        </div>
        <!-- Page content -->
        <div id="page-content-wrapper">
            <div class="content-header marginFluid">
                <h1 id="home" class="valignMessage">
                    <a id="menu-toggle" href="#" class="glyphicon glyphicon-align-justify btn-menu toggle">
                        <i class="fa fa-bars"></i>                        
                    </a>    
                    Gestion des commandes
                </h1>
            </div>
            <div class="page-content inset" data-spy="scroll" data-target="#spy">
                <asp:PlaceHolder ID="phDynamique" runat="server" />
            </div>
        </div>
    </div>  
</asp:Content>
