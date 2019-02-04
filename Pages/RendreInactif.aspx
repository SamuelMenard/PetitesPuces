<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="RendreInactif.aspx.cs" Inherits="Pages_RendreInactif" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
    <script>
        function search() {
            var input, row, filter, h4;
            input = document.getElementById("tbSearch");
            filter = input.value.toUpperCase();
            row = document.getElementById("contentBody_row_utilisateurs");
            span = row.getElementsByTagName("span");

            for (i = 0; i < span.length; i++) {
                txtValue = span[i].innerHTML;
                var id = span[i].id.replace("contentBody_lbl_", "");
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
                <h1>Désactiver un compte client ou vendeur</h1> 
                <p>Vous pouvez désactiver le compte d'un client ou d'un vendeur 
                    instantanément. Attention ! En supprimant un utilisateur vous allez aussi détruire leurs paniers, commandes, etc...</p> 
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
                <div class="col-sm-2 mb-3">
                    <asp:DropDownList id="typeUtilisateur" CssClass="form-control" OnSelectedIndexChanged="type_changed" AutoPostBack="true"
                        runat="server">
                            <asp:ListItem Value="C"> Clients </asp:ListItem>
                            <asp:ListItem Value="V"> Vendeurs </asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <br />
            <br />

            <asp:PlaceHolder id="phDynamique" runat="server" />

    </div>
</asp:Content>
