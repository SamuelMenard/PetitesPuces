<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="NouveauxProduits.aspx.cs" Inherits="Pages_NouveauxProduits" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
    <link rel="stylesheet" href="../static/style/panier.css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
    <div class="alert alert-warning">
      <strong>Attention!</strong> Vous devez vous connecter afin d'accéder à l'entièreté du catalogue.
    </div>

    <asp:PlaceHolder id="phDynamique" runat="server" />

    <br />
    <br />
    <asp:Button ID="btnRetourPanier" CssClass="btn btn-warning" Text="Retour" runat="server" OnClick="retour_click" />
</asp:Content>
