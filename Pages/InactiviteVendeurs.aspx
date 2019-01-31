<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="InactiviteVendeurs.aspx.cs" Inherits="Pages_InactiviteVendeurs" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
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
        </div>

        <br />

        <asp:PlaceHolder id="phDynamique" runat="server" />

        <div id="divMessage" class="alert alert-warning" visible="false" runat="server">
          <strong>Il n'y a aucune nouvelle demande de vendeur</strong>
        </div>
    </div>
</asp:Content>
