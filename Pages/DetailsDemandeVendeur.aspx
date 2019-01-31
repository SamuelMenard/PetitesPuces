<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="DetailsDemandeVendeur.aspx.cs" Inherits="Pages_DetailsDemandeVendeur" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
    <link rel="stylesheet" href="../static/style/fichevendeur.css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
    <div class="container">
        <div class="jumbotron">
                <h1>Fiche du vendeur</h1> 
                <p>Appuyez sur le (X) rouge pour refuser une demande ou sur le (✔) vert pour accepter la demande</p> 
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Button ID="btnRetourDash" CssClass="btn btn-warning" Text="Retour" runat="server" OnClick="retourDashboard_click" />
            </div>
        </div>
        
        <br />
        <br />

        <asp:PlaceHolder id="phDynamique" runat="server" />
    </div>
    
</asp:Content>
