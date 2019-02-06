﻿<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="Base - Copy.aspx.cs" Inherits="Pages_Base" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">    
     <!-- Contenu de la page -->
    <asp:Panel runat="server" ID="_base" CssClass="panel panel-default container">
         <asp:Panel runat="server" ID="_searchFilter_" CssClass="clearfix topBotPad center">
             <asp:Panel runat="server" ID="_colFullRow_" CssClass="col-sm-12">
                 <asp:TextBox runat="server" ID="_tbsearchText" CssClass="left15"></asp:TextBox>
                 <asp:LinkButton ID="btnSearch" 
                        runat="server" 
                        CssClass="btn btn-default left15">
                <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                      </asp:LinkButton>
                 
                      <asp:DropDownList ID="ddlCategorie"
                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="categorieIndexChange" EnableViewState="true"  >                         
                    </asp:DropDownList>
                     

                       <asp:LinkButton ID="btnTrierNoProduit_" 
                        runat="server" 
                           CssClass="btn btn-default left15">Numéro de produit
                <span aria-hidden="true" class="glyphicon glyphicon-sort"></span>
                      </asp:LinkButton>

                       <asp:LinkButton ID="btnTrierDate_" 
                        runat="server" 
                        CssClass="btn btn-default left15">Date de parution
                <span aria-hidden="true" class="glyphicon glyphicon-sort"></span>
                      </asp:LinkButton>

                      <asp:Label runat="server" ID="_nbParPage" Text="Produits par page" CssClass="left15"> </asp:Label>
                     <asp:DropDownList id="ddlNbParPage" CssClass="left15"
                        runat="server" AutoPostBack="true">
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
    </asp:Panel>
</asp:Content>