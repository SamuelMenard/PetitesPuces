<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="AffichageProduitDetaille.aspx.cs" Inherits="Pages_AffichageProduitDetaille" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
     <link rel="stylesheet" href="../static/style/catalogueVendeur.css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">    
     <!-- Contenu de la page -->
      <asp:Panel runat="server" ID="_base" CssClass="marginFluid">
     <asp:Panel runat="server" ID="messageAction" >

    </asp:Panel>
    <asp:PlaceHolder id="phDynamique" runat="server" />
  </asp:Panel>
</asp:Content>
