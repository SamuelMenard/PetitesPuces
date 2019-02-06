<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="DetailsDemandeVendeur.aspx.cs" Inherits="Pages_DetailsDemandeVendeur" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
    <link rel="stylesheet" href="../static/style/fichevendeur.css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
    <div class="container">
        <div class="jumbotron">
                <h1>Détails du vendeur</h1>  
        </div>
        <asp:PlaceHolder id="phErreur" runat="server" />
        <asp:PlaceHolder id="phDynamique" runat="server" />
    </div>
    
</asp:Content>
