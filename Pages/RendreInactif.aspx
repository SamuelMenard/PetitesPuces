<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="RendreInactif.aspx.cs" Inherits="Pages_RendreInactif" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
    <h1>Rendre inactif un client ou un vendeur</h1>
    <br />
    <br />

    <div class="row">
        <div class="col-sm-1 mb-3">
            <asp:Button ID="btnRetourPanier" CssClass="btn btn-warning" Text="Retour" runat="server" OnClick="retourDashboard_click" />
        </div>
        <div class="col-sm-3 mb-3">
            <div class="input-group">
                <asp:TextBox ID="tbSearch" Text="" CssClass="form-control" runat="server"/>
                <div class="input-group-btn">
                  <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-default" OnClick="recherche_click">
                    <i class="glyphicon glyphicon-search"></i>
                  </asp:LinkButton>
                </div>
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
</asp:Content>
