<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="SuppressionProduit.aspx.cs" Inherits="Pages_SuppressionProduit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
     <link rel="stylesheet" href="../static/style/catalogueVendeur.css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <script type = "text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Voulez vous supprimer le(s) produit(s) sélectionné(s)?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <!-- Contenu de la page -->
    <div class="row">
       <div class="message text-center">
          <asp:Panel ID="divMessage" runat="server" Visible="False">
             <asp:Label ID="lblMessage" runat="server" />
          </asp:Panel>
       </div>
    </div>
    <asp:PlaceHolder id="phDynamique" EnableViewState="false" runat="server" />
</asp:Content>
