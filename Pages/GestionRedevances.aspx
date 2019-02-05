<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="GestionRedevances.aspx.cs" Inherits="Pages_GestionRedevances" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
    <link rel="stylesheet" href="../static/style/fichevendeur.css">
    <script>
        function search() {
            var input, row, filter, h4;
            input = document.getElementById("tbSearch");
            filter = input.value.toUpperCase();
            row = document.getElementById("contentBody_row_utilisateurs");
            h4 = row.getElementsByTagName("h4");

            for (i = 0; i < h4.length; i++) {
                txtValue = h4[i].innerHTML;
                var id = h4[i].id.replace("h4_", "");
                var idDIV = "contentBody_col_" + id;

                if (id != "") {
                    var div = document.getElementById(idDIV);
                    if (txtValue.toUpperCase().indexOf(filter) > -1) {
                    
                        div.style.display = "";
                    } else {
                        div.style.display = "none";
                    }
                }
            }
        }
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
    <div class="container">
        <div class="jumbotron">
                <h1>Gestion des redevances des vendeurs</h1> 
                <p>Vous pouvez modifier le pourcentage de redevance en modifiant la valeur sous le nom du vendeur</p> 
        </div>

        <div class="row">
            <div class="col-sm-1 mb-3">
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

        <asp:PlaceHolder id="phDynamique" runat="server" />

        <div id="divMessage" class="alert alert-warning" visible="false" runat="server">
          <strong>Il n'y a aucun taux de redevance que vous pouvez modifier</strong>
        </div>
    </div>
    
</asp:Content>
