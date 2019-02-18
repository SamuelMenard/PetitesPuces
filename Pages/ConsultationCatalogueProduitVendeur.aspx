<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="ConsultationCatalogueProduitVendeur.aspx.cs" Inherits="Pages_ConsultationCatalogueProduitVendeur" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
     <link rel="stylesheet" href="../static/style/catalogueVendeur.css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">    
     <!-- Contenu de la page -->
    <asp:Panel runat="server" ID="messageAction" >

    </asp:Panel>
    <asp:Panel runat="server" ID="_base" CssClass="panel panel-default container">
         <asp:Panel runat="server" ID="_searchFilter_" CssClass="clearfix topBotPad center">
             <asp:Panel runat="server" ID="_colFullRow_" CssClass="col-sm-12">
                 <asp:TextBox runat="server" ID="_tbsearchText" CssClass="left15"></asp:TextBox>
                 <asp:LinkButton ID="btnSearch" OnClick="lbSearch"
                        runat="server" 
                        CssClass="btn btn-default left15">
                <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                      </asp:LinkButton>
                 
                      <asp:DropDownList ID="ddlCategorie"
                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="categorieIndexChange" EnableViewState="true"  >                         
                    </asp:DropDownList>
                     

                       <asp:LinkButton ID="btnTrierNoProduit_" 
                        runat="server" OnClick="triParNoProduit" 
                           CssClass="btn btn-default left15">Numéro de produit
                <span aria-hidden="true" class="glyphicon glyphicon-sort"></span>
                      </asp:LinkButton>

                       <asp:LinkButton ID="btnTrierDate_" 
                        runat="server" OnClick="triParDate" 
                        CssClass="btn btn-default left15">Date de parution
                <span aria-hidden="true" class="glyphicon glyphicon-sort"></span>
                      </asp:LinkButton>

                      <asp:Label runat="server" ID="_nbParPage" Text="Produits par page" CssClass="left15"> </asp:Label>
                     <asp:DropDownList id="ddlNbParPage" 
                        runat="server" OnSelectedIndexChanged="nbPageChange" AutoPostBack="true" EnableViewState="true" >
                          <asp:ListItem Value="0">5</asp:ListItem>
                          <asp:ListItem Value="1">10</asp:ListItem>                         
                          <asp:ListItem Selected="True" Value="2">15</asp:ListItem>
                          <asp:ListItem Value="3">20</asp:ListItem>
                          <asp:ListItem Value="4">25</asp:ListItem>
                          <asp:ListItem Value="5">50</asp:ListItem>
                          <asp:ListItem Value="6">Tous</asp:ListItem>
                    </asp:DropDownList>
           
                </asp:Panel>
             </asp:Panel>
    <asp:PlaceHolder id="phDynamique" runat="server" />
        <asp:Panel runat="server" CssClass="row text-center">
        <asp:Panel runat="server" aria-label="Page navigation example col-md-12">
         
        <ul runat="server" class="pagination" id="ulPages">    
            
        </ul>

      </asp:Panel>
      </asp:Panel>
    </asp:Panel>
</asp:Content>



   
  