<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="NouveauxProduits.aspx.cs" Inherits="Pages_NouveauxProduits" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
    <link rel="stylesheet" href="../static/style/panier.css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
    <div class="container">
        <div class="jumbotron">
                <h1>Nouveaux produits</h1> 
                <p>Voici la liste des 15 nouveaux produits. Connectez-vous pour avoir accès à l'ensemble des produits !</p> 
        </div>

        <asp:Button ID="btnRetourPanier" CssClass="btn btn-warning" Text="Retour" runat="server" OnClick="retour_click" />

        <br />
        <br />

    <asp:PlaceHolder id="phDynamique" runat="server" />

    </div>
    
</asp:Content>
