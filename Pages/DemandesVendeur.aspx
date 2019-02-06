<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="DemandesVendeur.aspx.cs" Inherits="Pages_DemandesVendeur" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
    <script>
        function search() {
            var input, row, filter, h4;
            input = document.getElementById("tbSearch");
            filter = input.value.toUpperCase();
            row = document.getElementById("contentBody_rowDemandeurs");
            h4 = row.getElementsByTagName("h4");

            for (i = 0; i < h4.length; i++) {
                txtValue = h4[i].innerHTML;
                var idVendeur = h4[i].id.replace("h4_", "");

                var idDIV = "contentBody_colInfos_" + idVendeur;
                var div = document.getElementById(idDIV);

                if (txtValue.toUpperCase().indexOf(filter) > -1) {

                    console.log(div.id);
                    div.style.display = "";
                } else {
                    div.style.display = "none";
                }
            }
        }
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
    <div class="container">
        <div class="jumbotron">
                <h1>Demandes de vendeurs</h1> 
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Button ID="btnRetourPanier" CssClass="btn btn-warning" Text="Retour" runat="server" OnClick="retourDashboard_click" />
            </div>
            <div class="col-sm-3 mb-3">
                <div class="input-group">
                  <span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span>
                  <input id="tbSearch" type="text" class="form-control" name="tbSearch" placeholder="Recherche" onkeyup="search()">
                </div>
            </div>
        </div>
        
        <br />
        <br />

        <div id="divAccepte" class="alert alert-success" visible="false" runat="server">
          <strong>Le vendeur est maintenant actif !</strong>
        </div>

        <div id="divRefuse" class="alert alert-success" visible="false" runat="server">
          <strong>Le vendeur a été refusé avec succès</strong>
        </div>

        <asp:PlaceHolder id="phDynamique" runat="server" />

        <div id="divMessage" class="alert alert-warning" visible="false" runat="server">
          <strong>Il n'y a aucune nouvelle demande de vendeur</strong>
        </div>
    </div>
    
</asp:Content>
