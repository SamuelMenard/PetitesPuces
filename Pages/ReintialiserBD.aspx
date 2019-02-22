<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="ReintialiserBD.aspx.cs" Inherits="Pages_ReintialiserBD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
   .containerForm {
      width: 100%;
      max-width: 500px;
   }

   .message {
      width: 100%;
      max-width: 500px;
      padding-left: 15px;
      padding-right: 15px;
   }

   .Orange {
      color: white;
      background-color : orange;
   }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBody" Runat="Server">
<div class="container-fluid containerForm">
   <div class="row">
      <div class="message text-center">
         <asp:Panel ID="divMessage" runat="server" Visible="False">
            <asp:Label ID="lblMessage" runat="server" />
            <br />
         </asp:Panel>
      </div>
   </div>
   <div class="row">
      <div class="table-responsive">
         <asp:Table ID="tabEtatTables" runat="server" CssClass="table table-striped table-bordered table-condensed">
            <asp:TableHeaderRow runat="server">
               <asp:TableHeaderCell runat="server" Text="Tables"></asp:TableHeaderCell>
               <asp:TableHeaderCell runat="server" Text="État"></asp:TableHeaderCell>
            </asp:TableHeaderRow>
         </asp:Table>
      </div>
   </div>
   <div class="row">
      <asp:Button ID="btnViderBD" runat="server" CssClass="btn btn-lg Orange btn-block" Text="Vider la base de données" Visible="false" OnClick="btnViderBD_Click" />
      <asp:Button ID="btnImporterDonnees" runat="server" CssClass="btn btn-lg Orange btn-block" Text="Importer les données du jeu d'essai" Visible="false" OnClick="btnImporterDonnees_Click" />
   </div>
</div>
</asp:Content>

