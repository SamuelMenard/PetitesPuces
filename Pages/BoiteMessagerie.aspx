<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="BoiteMessagerie.aspx.cs" Inherits="Pages_BoiteMessagerie" %>

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
                <h1>Boîte de méssagerie</h1> 
                <p>Sélectionnez des destinataires en cliquant sur leur nom</p> 
          </div>

            

            <br />
            <br />

            <asp:PlaceHolder id="phDynamique" runat="server" />

    </div>
</asp:Content>
