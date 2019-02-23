<%@ Page Language="C#" MasterPageFile="~/PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="InscriptionProduit.aspx.cs" Inherits="Pages_InscriptionProduit" %>

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

   .text-danger {
      color:#dc3545!important
   }

   .border-danger {
      border-color:#dc3545!important
   }

   .border-success {
      border-color:#28a745!important
   }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBody" Runat="Server">
<div class="container-fluid containerForm">
   <div class="row">
      <div class="message text-center">
         <asp:Panel ID="divMessage" runat="server" Visible="False">
            <asp:Label ID="lblMessage" runat="server" />
         </asp:Panel>
      </div>
   </div>
   <h1 class="h3 mb-3 font-weight-normal">Veuillez entrer les informations d'un produit</h1>
   <div class="form-group">
      <label for="tbNo">Numéro</label>
      <asp:TextBox ID="tbNo" runat="server" CssClass="form-control" Enabled="false" />
   </div>
   <div class="form-group">
      <label id="lblCategorie" for="ddlCategorie" runat="server" visible="false">Catégorie</label>
      <asp:DropDownList Id="ddlCategorie" CssClass="form-control" runat="server">
         <asp:ListItem Value="">Sélectionnez la catégorie</asp:ListItem>
      </asp:DropDownList>
      <asp:Label ID="errCategorie" runat="server" CssClass="text-danger hidden" />
   </div>
   <div class="row">
      <div class="form-group col-sm-6">
         <label id="lblNom" for="tbNom" runat="server" visible="false">Nom</label>
         <asp:TextBox ID="tbNom" runat="server" CssClass="form-control" placeholder="Nom" MaxLength="50" />
         <asp:Label ID="errNom" runat="server" CssClass="text-danger hidden" />
      </div>
      <div class="form-group col-sm-6">
         <label id="lblPrixDemande" for="tbPrixDemande" runat="server" visible="false">Prix demandé</label>
         <div class="input-group">
            <asp:TextBox ID="tbPrixDemande" runat="server" TextMode="Number" step="0.01" CssClass="form-control" placeholder="Prix demandé" />
            <span class="input-group-addon">$</span>
         </div>
         <asp:Label ID="errPrixDemande" runat="server" CssClass="text-danger hidden" />
      </div>
   </div>
   <div class="form-group">
      <label id="lblDescription" for="tbDescription" runat="server" visible="false">Description</label>
      <asp:TextBox ID="tbDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" placeholder="Description" />
      <asp:Label ID="errDescription" runat="server" CssClass="text-danger hidden" />
   </div>
   <div class="form-group">
      <div class="input-group">
         <label id="lblImage" for="imgTeleverse" runat="server" visible="false">Image</label>
         <asp:Image ID="imgTeleverse" runat="server" CssClass="thumbnail img-responsive" style="max-width: 200px" ImageUrl="~/static/images/image_placeholder.png" />
         <asp:FileUpload ID="fImage" runat="server" CssClass="hidden" accept="image/png, image/jpeg" />
         <asp:Label ID="errImage" runat="server" CssClass="text-danger hidden" />
         <input id="btnSelectionnerImage" type="button" class="btn btn-block Orange" value="Sélectionner une image" runat="server" visible="false" />
         <input id="btnChangerImage" type="button" class="btn btn-block Orange" value="Changer l'image" runat="server" visible="false" />
         <asp:Button ID="btnTeleverserImage" runat="server" CssClass="hidden" OnClick="btnTeleverserImage_Click" />
      </div>
   </div>
   <asp:Panel ID="divDateCreation" runat="server" CssClass="form-group" Visible="false">
      <label for="tbDateCreation">Date de création</label>
      <asp:TextBox ID="tbDateCreation" runat="server" CssClass="form-control" Enabled="false" />
   </asp:Panel>
   <div class="row">
      <asp:Panel ID="divNbItems" runat="server" CssClass="form-group col-sm-6">
         <label id="lblNbItems" for="tbNbItems" runat="server" visible="false">Quantité</label>
         <asp:TextBox ID="tbNbItems" runat="server" TextMode="Number" CssClass="form-control" placeholder="Quantité" />
         <asp:Label ID="errNbItems" runat="server" CssClass="text-danger hidden" />
      </asp:Panel>
      <asp:Panel ID="divPrixVente" runat="server" CssClass="form-group col-sm-6">
         <label id="lblPrixVente" for="tbPrixVente" runat="server" visible="false">Prix de vente</label>
         <div class="input-group">
            <asp:TextBox ID="tbPrixVente" runat="server" TextMode="Number" step="0.01" CssClass="form-control" placeholder="Prix de vente" />
            <span class="input-group-addon">$</span>
         </div>
         <asp:Label ID="errPrixVente" runat="server" CssClass="text-danger hidden" />
      </asp:Panel>
   </div>
   <asp:Panel ID="divDateVente" runat="server" CssClass="form-group hidden">
      <label id="lblDateVente" for="tbDateVente" runat="server">Sélectionnez une date d’expiration du prix de vente</label>
      <asp:TextBox ID="tbDateVente" runat="server" TextMode="Date" CssClass="form-control" />
      <asp:Label ID="errDateVente" runat="server" CssClass="text-danger hidden" />
   </asp:Panel>
   <div class="row">
      <div class="form-group col-sm-6">
         <label id="lblPoids" for="tbPoids" runat="server" visible="false">Poids</label>
         <div class="input-group">
            <asp:TextBox ID="tbPoids" runat="server" TextMode="Number" step="0.1" CssClass="form-control" placeholder="Poids" />
            <span class="input-group-addon">lbs</span>
         </div>
         <asp:Label ID="errPoids" runat="server" CssClass="text-danger hidden" />
      </div>
      <div class="form-group col-sm-6"> 
         <label id="lblDisponibilite" for="radioBtn" runat="server" visible="false">Disponibilité</label>
         <div class="input-group">
    	      <label id="lblDisponibiliteAjout" runat="server">Disponibilité&nbsp</label>
            <div id="radioBtn" class="btn-group">
    		      <asp:HyperLink ID="btnOui" runat="server" CssClass="btn Orange active" Text="Oui" data-toggle="rbDisponibilite" data-title="O" />
    			   <asp:HyperLink ID="btnNon" runat="server" CssClass="btn Orange notActive" Text="Non" data-toggle="rbDisponibilite" data-title="N" />
    		   </div>
    		   <asp:HiddenField ID="rbDisponibilite" runat="server" Value="O" />   
    	   </div>
      </div>
   </div>
   <asp:Button ID="btnInscription" runat="server" CssClass="btn btn-lg Orange btn-block" Text="Inscrire le produit" Visible="false" OnClick="btnInscription_Click" />
   <asp:Button ID="btnRetour" runat="server" CssClass="btn btn-lg Orange btn-block" Text="Retour" Visible="false" OnClick="btnRetour_Click" />
   <asp:Button ID="btnModifier" runat="server" CssClass="btn btn-lg Orange btn-block" Text="Modifier le produit" Visible="false" OnClick="btnModifier_Click" />
   <asp:Button ID="btnSupprimer" runat="server" CssClass="btn btn-lg Orange btn-block" Text="Supprimer le produit" Visible="false" OnClick="btnSupprimer_Click" />
   
</div>
<script type="text/javascript">
   $(document).ready(function () {
      var exprTexte = /^[\w\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF\s!"$%?&()\-;:«»°,'.]+$/;
      var exprMontant = /^\d+(\.\d{2})?$/;
      var exprNbItems = /^\d+$/;
      var dateAujourdhui = new Date();
      dateAujourdhui.setHours(0, 0, 0, 0);
      var exprPoids = /^\d+(\.\d)?$/;
      if ($('#contentBody_btnModifier').length) {
         $('#contentBody_tbNom').removeAttr('placeholder');
         $('#contentBody_tbPrixDemande').removeAttr('placeholder');
         $('#contentBody_tbDescription').removeAttr('placeholder');
         $('#contentBody_tbNbItems').removeAttr('placeholder');
         $('#contentBody_tbPrixVente').removeAttr('placeholder');
         $('#contentBody_tbPoids').removeAttr('placeholder');
      }
      $('#contentBody_ddlCategorie').focusout(function () {
         if ($('#contentBody_ddlCategorie').val() == '') {
            $('#contentBody_ddlCategorie').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errCategorie').text('Vous devez sélectionner une catégorie').removeClass('hidden');
         } else {
            $('#contentBody_ddlCategorie').removeClass('border-danger').addClass('border-success');
            $('#contentBody_errCategorie').text('').addClass('hidden');
         }
      });
      $('#contentBody_tbNom').focusout(function () {
         if ($('#contentBody_tbNom').val() == '') {
            $('#contentBody_tbNom').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errNom').text('Le nom ne peut pas être vide').removeClass('hidden');
         } else if (!exprTexte.test($('#contentBody_tbNom').val())) {
            $('#contentBody_tbNom').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errNom').text('Le nom n\'est pas valide').removeClass('hidden');
         } else {
            $('#contentBody_tbNom').removeClass('border-danger').addClass('border-success');
            $('#contentBody_errNom').text('').addClass('hidden');
         }
      });
      $('#contentBody_tbPrixDemande').focusout(function () {
         if ($('#contentBody_tbPrixDemande').val() == '') {
            $('#contentBody_tbPrixDemande').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errPrixDemande').text('Le prix demandé ne peut pas être vide').removeClass('hidden');
         } else if (!exprMontant.test($('#contentBody_tbPrixDemande').val())) {
            $('#contentBody_tbPrixDemande').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errPrixDemande').text('Le prix demandé doit être un nombre positif').removeClass('hidden');
         } else if ($('#contentBody_tbPrixDemande').val() > 214748.36) {
            $('#contentBody_tbPrixDemande').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errPrixDemande').text('Le prix demandé doit être inférieur à 214 748,37 $').removeClass('hidden');
         } else {
            $('#contentBody_tbPrixDemande').removeClass('border-danger').addClass('border-success');
            $('#contentBody_errPrixDemande').text('').addClass('hidden');
         }
      });
      $('#contentBody_tbDescription').focusout(function () {
         if ($('#contentBody_tbDescription').val() == '') {
            $('#contentBody_tbDescription').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errDescription').text('La description ne peut pas être vide').removeClass('hidden');
         } else if (!exprTexte.test($('#contentBody_tbDescription').val())) {
            $('#contentBody_tbDescription').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errDescription').text('La description n\'est pas valide').removeClass('hidden');
         } else {
            $('#contentBody_tbDescription').removeClass('border-danger').addClass('border-success');
            $('#contentBody_errDescription').text('').addClass('hidden');
         }
      });
      $('#contentBody_fImage').change(function () {
         $('#contentBody_btnTeleverserImage').click();
      });
      $('#contentBody_btnSelectionnerImage,#contentBody_btnChangerImage').click(function () {
         $('#contentBody_fImage').click();
      });
      $('#contentBody_tbNbItems').focusout(function () {
         if ($('#contentBody_tbNbItems').val() == '') {
            $('#contentBody_tbNbItems').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errNbItems').text('La quantité ne peut pas être vide').removeClass('hidden');
         } else if (!exprNbItems.test($('#contentBody_tbNbItems').val())) {
            $('#contentBody_tbNbItems').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errNbItems').text('La quantité doit être un entier').removeClass('hidden');
         } else if ($('#contentBody_tbNbItems').val() > 32767) {
            $('#contentBody_tbNbItems').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errNbItems').text('La quantité ne peut pas dépasser 32 767 items').removeClass('hidden');
         } else {
            $('#contentBody_tbNbItems').removeClass('border-danger').addClass('border-success');
            $('#contentBody_errNbItems').text('').addClass('hidden');
         }
      });
      $('#contentBody_tbPrixVente').focusout(function () {
         if ($('#contentBody_tbPrixVente').val() != '') {
            if (!exprMontant.test($('#contentBody_tbPrixVente').val())) {
               $('#contentBody_tbPrixVente').removeClass('border-success').addClass('border-danger');
               $('#contentBody_errPrixVente').text('Le prix de vente doit être un nombre positif').removeClass('hidden');
            } else if ($('#contentBody_tbPrixVente').val() > 214748.36) {
               $('#contentBody_tbPrixVente').removeClass('border-success').addClass('border-danger');
               $('#contentBody_errPrixVente').text('Le prix de vente doit être inférieur à 214 748,37 $').removeClass('hidden');
            } else {
               $('#contentBody_tbPrixVente').removeClass('border-danger').addClass('border-success');
               $('#contentBody_errPrixVente').text('').addClass('hidden');
            }
         } else {
            $('#contentBody_tbPrixVente').removeClass('border-danger').addClass('border-success');
            $('#contentBody_errPrixVente').text('').addClass('hidden');
         }
      });
      $('#contentBody_tbPrixVente').change(function () {
         if ($('#contentBody_tbPrixVente').val() == '') {
            $('#contentBody_tbDateVente').removeClass('border-danger').removeClass('border-success');
            $('#contentBody_errDateVente').text('').addClass('hidden');
            $('#contentBody_divDateVente').addClass('hidden');
         }  
         else
            $('#contentBody_divDateVente').removeClass('hidden');
      });
      $('#contentBody_tbDateVente').focusout(function () {
         if ($('#contentBody_tbDateVente').val() == '') {
            $('#contentBody_tbDateVente').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errDateVente').text('Vous devez sélectionner une date d\'expiration du prix de vente').removeClass('hidden');
         } else if (new Date($('#contentBody_tbDateVente').val() + ' 00:00:00') <= dateAujourdhui) {
            $('#contentBody_tbDateVente').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errDateVente').text('La date d\'expiration du prix de vente doit être supérieure à la date d\'aujourd\'hui').removeClass('hidden');
         } else {
            $('#contentBody_tbDateVente').removeClass('border-danger').addClass('border-success');
            $('#contentBody_errDateVente').text('').addClass('hidden');
         }
      });
      $('#contentBody_tbPoids').focusout(function () {
         if ($('#contentBody_tbPoids').val() == '') {
            $('#contentBody_tbPoids').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errPoids').text('Le poids ne peut pas être vide').removeClass('hidden');
         } else if (!exprPoids.test($('#contentBody_tbPoids').val())) {
            $('#contentBody_tbPoids').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errPoids').text('Le poids doit être un entier ou un nombre décimal avec un chiffre après la virgule').removeClass('hidden');
         } else {
            $('#contentBody_tbPoids').removeClass('border-danger').addClass('border-success');
            $('#contentBody_errPoids').text('').addClass('hidden');
         }
      });
      $('#contentBody_btnInscription,#contentBody_btnModifier').click(function () {
         var binPageValide = true;
         if ($('#contentBody_ddlCategorie').val() == '') {
            $('#contentBody_ddlCategorie').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errCategorie').text('Vous devez sélectionner une catégorie').removeClass('hidden');
            binPageValide = false;
         }
         if ($('#contentBody_tbNom').val() == '' || !exprTexte.test($('#contentBody_tbNom').val())) {
            $('#contentBody_tbNom').removeClass('border-success').addClass('border-danger');
            if ($('#contentBody_tbNom').val() == '')
               $('#contentBody_errNom').text('Le nom ne peut pas être vide').removeClass('hidden');
            else
               $('#contentBody_errNom').text('Le nom n\'est pas valide').removeClass('hidden');
            binPageValide = false;
         }
         if ($('#contentBody_tbPrixDemande').val() == '' || !exprMontant.test($('#contentBody_tbPrixDemande').val()) || $('#contentBody_tbPrixDemande').val() > 214748.36) {
            $('#contentBody_tbPrixDemande').removeClass('border-success').addClass('border-danger');
            if ($('#contentBody_tbPrixDemande').val() == '')
               $('#contentBody_errPrixDemande').text('Le prix demandé ne peut pas être vide').removeClass('hidden');
            else if (!exprMontant.test($('#contentBody_tbPrixDemande').val()))
               $('#contentBody_errPrixDemande').text('Le prix demandé doit être un nombre positif').removeClass('hidden');
            else
               $('#contentBody_errPrixDemande').text('Le prix demandé doit être inférieur à 214 748,37 $').removeClass('hidden');
            binPageValide = false;
         }
         if ($('#contentBody_tbDescription').val() == '' || !exprTexte.test($('#contentBody_tbDescription').val())) {
            $('#contentBody_tbDescription').removeClass('border-success').addClass('border-danger');
            if ($('#contentBody_tbDescription').val() == '')
               $('#contentBody_errDescription').text('La description ne peut pas être vide').removeClass('hidden');
            else
               $('#contentBody_errDescription').text('La description n\'est pas valide').removeClass('hidden');
            binPageValide = false;
         }
         if ($('#contentBody_imgTeleverse').attr('src') == '../static/images/image_placeholder.png') {
            $('#contentBody_imgTeleverse').removeClass('border-success').addClass('border-danger');
            if ($('#contentBody_errImage').text() == '')
               $('#contentBody_errImage').text('Vous devez sélectionner une image').removeClass('hidden');
            binPageValide = false;
         }
         else {
            if ($('#contentBody_btnInscription').length)
               $('#contentBody_imgTeleverse').removeClass('border-danger').addClass('border-success');
            $('#contentBody_errImage').text('').addClass('hidden');
         }
         if ($('#contentBody_tbNbItems').val() == '' || !exprNbItems.test($('#contentBody_tbNbItems').val()) || $('#contentBody_tbNbItems').val() > 32767) {
            $('#contentBody_tbNbItems').removeClass('border-success').addClass('border-danger');
            if ($('#contentBody_tbNbItems').val() == '')
               $('#contentBody_errNbItems').text('La quantité ne peut pas être vide').removeClass('hidden');
            else if (!exprNbItems.test($('#contentBody_tbNbItems').val()))
               $('#contentBody_errNbItems').text('La quantité doit être un entier').removeClass('hidden');
            else
               $('#contentBody_errNbItems').text('La quantité ne peut pas dépasser 32 767 items').removeClass('hidden');
            binPageValide = false;
         }
         if ($('#contentBody_tbPrixVente').val() != '') {
            if (!exprMontant.test($('#contentBody_tbPrixVente').val()) || $('#contentBody_tbPrixVente').val() > 214748.36) {
               $('#contentBody_tbPrixVente').removeClass('border-success').addClass('border-danger');
               if (!exprMontant.test($('#contentBody_tbPrixVente').val()))
                  $('#contentBody_errPrixVente').text('Le prix de vente doit être un nombre positif').removeClass('hidden');
               else
                  $('#contentBody_errPrixVente').text('Le prix de vente doit être inférieur à 214 748,37 $').removeClass('hidden');
               binPageValide = false;
            }
            if ($('#contentBody_tbDateVente').val() == '') {
               $('#contentBody_tbDateVente').removeClass('border-success').addClass('border-danger');
               $('#contentBody_errDateVente').text('Vous devez sélectionner une date d\'expiration du prix de vente').removeClass('hidden');
               binPageValide = false;
            } else if (new Date($('#contentBody_tbDateVente').val() + ' 00:00:00') <= dateAujourdhui) {
               $('#contentBody_tbDateVente').removeClass('border-success').addClass('border-danger');
               $('#contentBody_errDateVente').text('La date d\'expiration du prix de vente doit être supérieure à la date d\'aujourd\'hui').removeClass('hidden');
               binPageValide = false;
            }
            else if ($('#contentBody_btnInscription').length)
               $('#contentBody_tbDateVente').removeClass('border-danger').addClass('border-success');
         }
         if ($('#contentBody_tbPoids').val() == '' || !exprPoids.test($('#contentBody_tbPoids').val())) {
            $('#contentBody_tbPoids').removeClass('border-success').addClass('border-danger');
            if ($('#contentBody_tbPoids').val() == '')
               $('#contentBody_errPoids').text('Le poids ne peut pas être vide').removeClass('hidden');
            else
               $('#contentBody_errPoids').text('Le poids doit être un entier ou un nombre décimal avec un chiffre après la virgule').removeClass('hidden');
            binPageValide = false;
         }
         return binPageValide;
      });
   });
</script>
</asp:Content>