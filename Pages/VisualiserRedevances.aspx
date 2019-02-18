<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="VisualiserRedevances.aspx.cs" Inherits="Pages_VisualiserRedevances" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
    <div class="container">
            <div class="jumbotron">
                <h1>Encaisser les redevances</h1> 
                <p>Voici la liste des redevances encaissables</p> 
          </div>

            <div class="row">
                <div class="col-md-1">
                    <asp:Button ID="btnRetour" CssClass="btn btn-warning" Text="Retour" runat="server" OnClick="retourDashboard_click" />
                </div>
                <div class="col-md-1">
                    <asp:LinkButton ID="btnTrierParDateVendeursCroissant" CssClass="btn btn-warning" OnClick="dateCroissant_click" runat="server">
                        Date <span class="glyphicon glyphicon-sort-by-attributes"></span>
                    </asp:LinkButton>
                </div>
                <div class="col-md-1">
                    <asp:LinkButton ID="btnTrierParDateVendeursDecroissant" CssClass="btn btn-warning" OnClick="dateDecroissant_click" runat="server">
                        Date <span class="glyphicon glyphicon-sort-by-attributes-alt"></span>
                    </asp:LinkButton>
                </div>
                <div class="col-md-2">
                    <asp:LinkButton ID="btnTrierParVentesVendeurCroissant" CssClass="btn btn-warning" OnClick="redevancesCroissant_click" runat="server">
                        Redevances <span class="glyphicon glyphicon-sort-by-attributes"></span>
                    </asp:LinkButton>
                </div>
                <div class="col-md-2">
                    <asp:LinkButton ID="btnTrierParVentesVendeurDecroissant" CssClass="btn btn-warning" OnClick="redevancesDecroissant_click" runat="server">
                        Redevances <span class="glyphicon glyphicon-sort-by-attributes-alt"></span>
                    </asp:LinkButton>
                </div>
            </div>

        <br/>

        <asp:PlaceHolder id="phTab" runat="server" />
    </div>
</asp:Content>
