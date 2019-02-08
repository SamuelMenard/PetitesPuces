<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="AccueilInternaute.aspx.cs" Inherits="Pages_AccueilInternaute" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
    <link rel="stylesheet" href="../static/style/panier.css">
    <link rel="stylesheet" href="../static/style/leftFixedNavBar.css">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
        
    <div class="container-fluid">
        <div class="row">
            <nav class="col-sm-2" id="myScrollspy">
                <ul class="nav nav-pills nav-stacked" id="ulSideBar" data-spy="affix" data-offset-top="50" runat="server">
                </ul>
            </nav>
            <div class="col-md-8">
                <div id="categories">
                    <div class="jumbotron">
                        <h1>Nos catégories</h1> 
                        <p>Explorer nos catégories et allez jeter un coup d'oeil à nos merveilleux vendeurs.</p> 
                      </div>
                    <asp:PlaceHolder id="categoriesDynamique" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
