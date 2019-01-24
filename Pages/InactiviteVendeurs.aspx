<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="InactiviteVendeurs.aspx.cs" Inherits="Pages_InactiviteVendeurs" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
    <h1>Gestion des vendeurs inactifs</h1>
    <br />
    <br />

    <div class="row">
        <div class="col-sm-1 mb-3">
            <asp:Button ID="btnRetourPanier" CssClass="btn btn-warning" Text="Retour" runat="server" OnClick="retourDashboard_click" />
        </div>
        <div class="col-sm-2 mb-3">
            <asp:DropDownList id="annee" CssClass="form-control" OnSelectedIndexChanged="ddlChanged" AutoPostBack="true"
                runat="server">
                    <asp:ListItem Value="1"> 1 an </asp:ListItem>
                    <asp:ListItem Value="2"> 2 ans </asp:ListItem>
                    <asp:ListItem Value="3"> 3 ans </asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>

    <br />

    <asp:PlaceHolder id="phDynamique" runat="server" />
</asp:Content>
