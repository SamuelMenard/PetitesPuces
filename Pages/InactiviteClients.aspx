<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="InactiviteClients.aspx.cs" Inherits="Pages_InactiviteClients" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
    <link rel="stylesheet" href="../static/style/checkbox.css">
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
                <h1>Clients inactifs</h1> 
                <p>Appuyez sur le (X) rouge pour désactiver le compte client</p> 
        </div>

        <div class="row">
            <div class="col-sm-1 mb-3">
                <asp:Button ID="btnRetourPanier" CssClass="btn btn-warning" Text="Retour" runat="server" OnClick="retourDashboard_click" />
            </div>
            <div class="col-sm-2 mb-3">
                <asp:DropDownList id="mois" CssClass="form-control" OnSelectedIndexChanged="ddlChanged" AutoPostBack="true"
                    runat="server">
                        <asp:ListItem Value="1"> 1 mois </asp:ListItem>
                        <asp:ListItem Value="3"> 3 mois </asp:ListItem>
                        <asp:ListItem Value="6"> 6 mois </asp:ListItem>
                        <asp:ListItem Value="12"> 1 an </asp:ListItem>
                        <asp:ListItem Value="24"> 2 ans </asp:ListItem>
                        <asp:ListItem Value="36"> 3 ans </asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-sm-3 mb-3">
                <div class="input-group">
                  <span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span>
                  <input id="tbSearch" type="text" class="form-control" name="tbSearch" placeholder="Recherche" onkeyup="search()">
                </div>
            </div>

            <div class="col-sm-2 mb-3">
                <div class="input-group">
                  <asp:Button ID="btnSelectionnerTout" CssClass="btn btn-warning" Text="Sélectionner tout" runat="server" OnClick="selectiontout_click" />
                </div>
            </div>

            <div class="col-sm-2 mb-3">
                <div class="input-group">
                  <asp:Button ID="btnSupprimerSelection" CssClass="btn btn-warning" Text="Supprimer les sélections" runat="server" OnClick="supprimerselections_click" />
                </div>
            </div>
        </div>

        <br />

        <asp:PlaceHolder id="phDynamique" runat="server" />

        <div id="divMessage" class="alert alert-warning" visible="false" runat="server">
          <strong>Il n'y a aucun client inactif depuis la période sélectionné</strong>
        </div>
    </div>
    
</asp:Content>
