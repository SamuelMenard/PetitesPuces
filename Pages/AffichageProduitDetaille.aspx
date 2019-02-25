<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="AffichageProduitDetaille.aspx.cs" Inherits="Pages_AffichageProduitDetaille" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
     <link rel="stylesheet" href="../static/style/catalogueVendeur.css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">    
     <!-- Contenu de la page -->
      <asp:Panel runat="server" ID="_base" CssClass="marginFluid">
     <asp:Panel runat="server" ID="messageAction" >

    </asp:Panel>
     <div class="row">
         <div class="message text-left">
            <asp:Panel ID="divCourriel" runat="server" Visible="False">
               <div class="row">
                  <div class="col-md-6">
                     Expéditeur
                     <asp:TextBox ID="tbExpediteur" runat="server" CssClass="form-control" Enabled="false" style="margin-bottom: 10px" />
                  </div>
                  <div class="col-md-6">
                     Destinataire
                     <asp:TextBox ID="tbDestinataire" runat="server" CssClass="form-control" Enabled="false" style="margin-bottom: 10px" />
                  </div>
               </div>
               Sujet
               <asp:TextBox ID="tbSujet" runat="server" CssClass="form-control" Enabled="false" style="margin-bottom: 10px" />
               Corps
               <asp:TextBox ID="tbCorps" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine" Rows="9" style="resize: none;" />
            </asp:Panel>
            <br />
         </div>
      </div>
    <asp:PlaceHolder id="phDynamique" runat="server" />
  </asp:Panel>
</asp:Content>
