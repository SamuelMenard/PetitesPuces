<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="ConnexionVendeur.aspx.cs" Inherits="Pages_ConnexionVendeur" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
     <link rel="stylesheet" href="../static/style/catalogueVendeur.css">
    <link rel="stylesheet" href="../static/style/leftFixedNavBar.css">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">     
    <script>
    $(document).ready(function() {
    $(".trigger").click(function(){
            $(this).next(".panel").slideToggle("medium");
        });
    });
    </script>
     <!-- Contenu de la page -->

      <div class="container-fluid">
        <div class="row">       
            <div class="col-md-12">                              
                 <asp:PlaceHolder id="phDynamique" runat="server" />                
            </div>
        </div>
    </div>


</asp:Content>
