<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="GererPanierInactifs.aspx.cs" Inherits="Pages_GererPanierInactifs" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
     <link rel="stylesheet" href="../static/style/catalogueVendeur.css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">    
     <!-- Contenu de la page -->
     <script>
          $(document).ready(function () {
              $(".trigger").click(function(){
                $(this).next(".panel").slideToggle("medium");
             });           
         });   

         function PanierDeleteConfirmation() {
            return confirm("Êtes-vous certain de vouloir supprimer ce panier?");
        }
     </script>
    <div class="container-fluid marginFluid">
    <div class="row text-center">
        <div class="col-md-12">
            <h1>Gestion des paniers inactifs</h1>
        </div>
        </div>
     <div class="row text-center">
        <div class="col-md-12 text-center bot15">
            Nombre de mois inactifs : 
             <asp:DropDownList id="ddlMois" 
                        runat="server" OnSelectedIndexChanged="nbMoisChange" AutoPostBack="true" EnableViewState="true" >
                          <asp:ListItem Value="0">1</asp:ListItem>
                          <asp:ListItem Value="1">2</asp:ListItem>                         
                          <asp:ListItem Value="2">3</asp:ListItem>
                         <asp:ListItem Value="3">4</asp:ListItem>
                         <asp:ListItem Value="4">5</asp:ListItem>
                          <asp:ListItem Value="5">6</asp:ListItem>
                          <asp:ListItem Value="6">7+</asp:ListItem>                         
                    </asp:DropDownList>
        </div>
    </div>
    </div>
    <asp:PlaceHolder id="phDynamique" runat="server" />
</asp:Content>
