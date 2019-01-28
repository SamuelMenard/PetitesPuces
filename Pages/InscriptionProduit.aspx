<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="InscriptionProduit.aspx.cs" Inherits="Pages_InscriptionProduit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
   .containerForm {
      width: 100%;
      max-width: 500px;
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
   <h1 class="h3 mb-3 font-weight-normal">Veuillez entrer les informations d'un produit</h1>
   <div class="form-group">
      <asp:DropDownList Id="ddlCategorie" CssClass="form-control" runat="server">
         <asp:ListItem Value="">Sélectionnez la catégorie</asp:ListItem>
      </asp:DropDownList>
      <asp:Label ID="errCategorie" runat="server" CssClass="text-danger" style="display: none;" />
   </div>
   <div class="row">
      <div class="form-group col-sm-6">
         <asp:TextBox ID="tbNom" runat="server" CssClass="form-control" placeholder="Nom" MaxLength="50" />
         <asp:Label ID="errNom" runat="server" CssClass="text-danger" style="display: none;" />
      </div>
      <div class="form-group col-sm-6">
         <div class="input-group">
            <asp:TextBox ID="tbPrixDemande" runat="server" TextMode="Number" step="0.01" CssClass="form-control" placeholder="Prix demandé" />
            <span class="input-group-addon">$</span>
         </div>
         <asp:Label ID="errPrixDemande" runat="server" CssClass="text-danger" style="display: none;" />
      </div>
   </div>
   <div class="form-group">
      <asp:TextBox ID="tbDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" placeholder="Description" />
      <asp:Label ID="errDescription" runat="server" CssClass="text-danger" style="display: none;" />
   </div>
   <div class="form-group">
      <label for="fImage">Sélectionnez une image</label>
      <asp:FileUpload ID="fImage" runat="server" CssClass="form-control" accept="image/png, image/jpeg" />
      <asp:Label ID="errImage" runat="server" CssClass="text-danger" style="display: none;" />
   </div>
   <div class="row">
      <div class="form-group col-sm-6">
         <asp:TextBox ID="tbNbItems" runat="server" TextMode="Number" CssClass="form-control" placeholder="Quantité" />
         <asp:Label ID="errNbItems" runat="server" CssClass="text-danger" style="display: none;" />
      </div>
      <div class="form-group col-sm-6">
         <div class="input-group">
            <asp:TextBox ID="tbPrixVente" runat="server" TextMode="Number" step="0.01" CssClass="form-control" placeholder="Prix de vente" />
            <span class="input-group-addon">$</span>
         </div>
         <asp:Label ID="errPrixVente" runat="server" CssClass="text-danger" style="display: none;" />
      </div>
   </div>
   <div class="form-group">
      <label for="tbDateVente">Sélectionnez une date d’expiration du prix de vente</label>
      <asp:TextBox ID="tbDateVente" runat="server" TextMode="Date" CssClass="form-control" />
      <asp:Label ID="errDateVente" runat="server" CssClass="text-danger" style="display: none;" />
   </div>
   <div class="row">
      <div class="form-group col-sm-6">
         <div class="input-group">
            <asp:TextBox ID="tbPoids" runat="server" TextMode="Number" step="0.1" CssClass="form-control" placeholder="Poids" />
            <span class="input-group-addon">lbs</span>
         </div>
         <asp:Label ID="errPoids" runat="server" CssClass="text-danger" style="display: none;" />
      </div>
      <div class="form-group col-sm-6"> 
         <div class="input-group">
    			<label>Disponibilité 
            <div id="radioBtn" class="btn-group">
    				<a class="btn Orange active" data-toggle="rbDisponibilite" data-title="O">Oui</a>
    				<a class="btn Orange notActive" data-toggle="rbDisponibilite" data-title="N">Non</a>
    		   </div>
    			<asp:HiddenField ID="rbDisponibilite" runat="server" Value="O" />
            </label>
    		</div>
      </div>
   </div>
   <asp:Button ID="btnInscription" runat="server" CssClass="btn btn-lg Orange btn-block" Text="Inscrire le produit" OnClick="btnInscription_Click" />
</div>
<script type="text/javascript">
   $(document).ready(function () {
      var exprTexte = /^[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-' ][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*$/;
      var exprMontant = /^\d+\.\d{2}$/;
      var exprNbItems = /^\d+$/;
      var dateAujourdhui = new Date();
      dateAujourdhui.setHours(0, 0, 0, 0);
      var exprPoids = /^\d+(\.\d)?$/;
      $("#contentBody_ddlCategorie").focusout(function () {
         if ($("#contentBody_ddlCategorie").val() == '') {
            $("#contentBody_ddlCategorie").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errCategorie").text('Vous devez sélectionner une catégorie').show();
         } else {
            $("#contentBody_ddlCategorie").removeClass("border-danger").addClass("border-success");
            $("#contentBody_errCategorie").text('').hide();
         }
      });
      $("#contentBody_tbNom").focusout(function () {
         if ($("#contentBody_tbNom").val() == '') {
            $("#contentBody_tbNom").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errNom").text('Le nom ne peut pas être vide').show();
         } else if (!exprTexte.test($("#contentBody_tbNom").val())) {
            $("#contentBody_tbNom").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errNom").text('Le nom n\'est pas dans un format valide').show();
         } else {
            $("#contentBody_tbNom").removeClass("border-danger").addClass("border-success");
            $("#contentBody_errNom").text('').hide();
         }
      });
      $("#contentBody_tbPrixDemande").focusout(function () {
         if ($("#contentBody_tbPrixDemande").val() == '') {
            $("#contentBody_tbPrixDemande").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errPrixDemande").text('Le prix demandé ne peut pas être vide').show();
         } else if (!exprMontant.test($("#contentBody_tbPrixDemande").val())) {
            $("#contentBody_tbPrixDemande").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errPrixDemande").text('Le prix demandé doit être un nombre décimal avec deux chiffres après la virgule').show();
         } else if ($("#contentBody_tbPrixDemande").val() > 214748.36) {
            $("#contentBody_tbPrixDemande").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errPrixDemande").text('Le prix demandé doit être inférieur à 214 748,37 $').show();
         } else {
            $("#contentBody_tbPrixDemande").removeClass("border-danger").addClass("border-success");
            $("#contentBody_errPrixDemande").text('').hide();
         }
      });
      $("#contentBody_tbDescription").focusout(function () {
         if ($("#contentBody_tbDescription").val() == '') {
            $("#contentBody_tbDescription").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errDescription").text('La description ne peut pas être vide').show();
         } else if (!exprTexte.test($("#contentBody_tbDescription").val())) {
            $("#contentBody_tbDescription").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errDescription").text('La description n\'est pas dans un format valide').show();
         } else {
            $("#contentBody_tbDescription").removeClass("border-danger").addClass("border-success");
            $("#contentBody_errDescription").text('').hide();
         }
      });
      $("#contentBody_fImage").focusout(function () {
         alert($("#contentBody_fImage").val());
      });
      $("#contentBody_tbNbItems").focusout(function () {
         if ($("#contentBody_tbNbItems").val() == '') {
            $("#contentBody_tbNbItems").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errNbItems").text('La quantité ne peut pas être vide').show();
         } else if (!exprNbItems.test($("#contentBody_tbNbItems").val())) {
            $("#contentBody_tbNbItems").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errNbItems").text('La quantité doit être un entier').show();
         } else if ($("#contentBody_tbNbItems").val() > 32767) {
            $("#contentBody_tbNbItems").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errNbItems").text('La quantité ne peut pas dépasser 32 767 items').show();
         } else {
            $("#contentBody_tbNbItems").removeClass("border-danger").addClass("border-success");
            $("#contentBody_errNbItems").text('').hide();
         }
      });
      $("#contentBody_tbPrixVente").focusout(function () {
         if ($("#contentBody_tbPrixVente").val() == '') {
            $("#contentBody_tbPrixVente").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errPrixVente").text('Le prix de vente ne peut pas être vide').show();
         } else if (!exprMontant.test($("#contentBody_tbPrixVente").val())) {
            $("#contentBody_tbPrixVente").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errPrixVente").text('Le prix de vente doit être un nombre décimal avec deux chiffres après la virgule').show();
         } else if ($("#contentBody_tbPrixVente").val() > 214748.36) {
            $("#contentBody_tbPrixVente").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errPrixVente").text('Le prix de vente doit être inférieur à 214 748,37 $').show();
         } else {
            $("#contentBody_tbPrixVente").removeClass("border-danger").addClass("border-success");
            $("#contentBody_errPrixVente").text('').hide();
         }
      });
      $("#contentBody_tbDateVente").focusout(function () {
         var dateExpiration = new Date($("#contentBody_tbDateVente").val() + ' 00:00:00');
         if (dateExpiration.getFullYear() <= dateAujourdhui.getFullYear() && dateExpiration.getMonth() <= dateAujourdhui.getMonth() && dateExpiration.getDate() <= dateAujourdhui.getDate()) {
            $("#contentBody_tbDateVente").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errDateVente").text('La date d\'expiration du prix de vente doit être supérieure à la date d\'aujourd\'hui').show();
         } else {
            $("#contentBody_tbDateVente").removeClass("border-danger").addClass("border-success");
            $("#contentBody_errDateVente").text('').hide();
         }
      });
      $("#contentBody_tbPoids").focusout(function () {
         if ($("#contentBody_tbPoids").val() == '') {
            $("#contentBody_tbPoids").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errPoids").text('Le poids ne peut pas être vide').show();
         } else if (!exprPoids.test($("#contentBody_tbPoids").val())) {
            $("#contentBody_tbPoids").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errPoids").text('Le poids doit être un entier ou un nombre décimal avec un chiffre après la virgule').show();
         } else if ($("#contentBody_tbPoids").val() > 66) {
            $("#contentBody_tbPoids").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errPoids").text('Le poids de l\'article ne peut pas dépasser le poids de livraison maximum permis de 66 lbs').show();
         } else {
            $("#contentBody_tbPoids").removeClass("border-danger").addClass("border-success");
            $("#contentBody_errPoids").text('').hide();
         }
      });
      /*$("#contentBody_btnInscription").click(function () {
         var binPageValide = true;
         if ($("#contentBody_ddlCategorie").val() == '') {
            $("#contentBody_ddlCategorie").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errCategorie").text('Vous devez sélectionner une catégorie').show();
            binPageValide = false;
         }
         if ($("#contentBody_tbNom").val() == '' || !exprTexte.test($("#contentBody_tbNom").val())) {
            $("#contentBody_tbNom").removeClass("border-success").addClass("border-danger");
            if ($("#contentBody_tbNom").val() == '')
               $("#contentBody_errNom").text('Le nom ne peut pas être vide').show();
            else
               $("#contentBody_errNom").text('Le nom n\'est pas dans un format valide').show();
            binPageValide = false;
         }
         if ($("#contentBody_tbPrixDemande").val() == '' || !exprMontant.test($("#contentBody_tbPrixDemande").val()) || $("#contentBody_tbPrixDemande").val() > 214748.36) {
            $("#contentBody_tbPrixDemande").removeClass("border-success").addClass("border-danger");
            if ($("#contentBody_tbPrixDemande").val() == '')
               $("#contentBody_errPrixDemande").text('Le prix demandé ne peut pas être vide').show();
            else if (!exprMontant.test($("#contentBody_tbPrixDemande").val()))
               $("#contentBody_errPrixDemande").text('Le prix demandé doit être un nombre décimal avec deux chiffres après la virgule').show();
            else
               $("#contentBody_errPrixDemande").text('Le prix demandé doit être inférieur à 214 748,37 $').show();
            binPageValide = false;
         }
         if ($("#contentBody_tbDescription").val() == '' || !exprTexte.test($("#contentBody_tbDescription").val())) {
            $("#contentBody_tbDescription").removeClass("border-success").addClass("border-danger");
            if ($("#contentBody_tbDescription").val() == '')
               $("#contentBody_errDescription").text('La description ne peut pas être vide').show();
            else
               $("#contentBody_errDescription").text('La description n\'est pas dans un format valide').show();
            binPageValide = false;
         }
         if ($("#contentBody_tbNbItems").val() == '' || !exprNbItems.test($("#contentBody_tbNbItems").val()) || $("#contentBody_tbNbItems").val() > 32767) {
            $("#contentBody_tbNbItems").removeClass("border-success").addClass("border-danger");
            if ($("#contentBody_tbNbItems").val() == '')
               $("#contentBody_errNbItems").text('La quantité ne peut pas être vide').show();
            else if (!exprNbItems.test($("#contentBody_tbNbItems").val()))
               $("#contentBody_errNbItems").text('La quantité doit être un entier').show();
            else
               $("#contentBody_errNbItems").text('La quantité ne peut pas dépasser 32 767 items').show();
            binPageValide = false;
         }
         if ($("#contentBody_tbPrixVente").val() == '' || !exprMontant.test($("#contentBody_tbPrixVente").val()) || $("#contentBody_tbPrixVente").val() > 214748.36) {
            $("#contentBody_tbPrixVente").removeClass("border-success").addClass("border-danger");
            if ($("#contentBody_tbPrixVente").val() == '')
               $("#contentBody_errPrixVente").text('Le prix de vente ne peut pas être vide').show();
            else if (!exprMontant.test($("#contentBody_tbPrixVente").val()))
               $("#contentBody_errPrixVente").text('Le prix de vente doit être un nombre décimal avec deux chiffres après la virgule').show();
            else
               $("#contentBody_errPrixVente").text('Le prix de vente doit être inférieur à 214 748,37 $').show();
            binPageValide = false;
         }
         var dateExpiration = new Date($("#contentBody_tbDateVente").val() + ' 00:00:00');
         if (dateExpiration.getFullYear() <= dateAujourdhui.getFullYear() && dateExpiration.getMonth() <= dateAujourdhui.getMonth() && dateExpiration.getDate() <= dateAujourdhui.getDate()) {
            $("#contentBody_tbDateVente").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errDateVente").text('La date d\'expiration du prix de vente doit être supérieure à la date d\'aujourd\'hui').show();
            binPageValide = false;
         }
         if ($("#contentBody_tbPoids").val() == '' || !exprPoids.test($("#contentBody_tbPoids").val()) || $("#contentBody_tbPoids").val() > 66) {
            $("#contentBody_tbPoids").removeClass("border-success").addClass("border-danger");
            if ($("#contentBody_tbPoids").val() == '')
               $("#contentBody_errPoids").text('Le poids ne peut pas être vide').show();
            else if (!exprPoids.test($("#contentBody_tbPoids").val()))
               $("#contentBody_errPoids").text('Le poids doit être un entier ou un nombre décimal avec un chiffre après la virgule').show();
            else
               $("#contentBody_errPoids").text('Le poids de l\'article ne peut pas dépasser le poids de livraison maximum permis de 66 lbs').show();
            binPageValide = false;
         }
         return binPageValide;
      });*/
      $('#radioBtn a').on('click', function () {
         var sel = $(this).data('title');
         var tog = $(this).data('toggle');
         $('#contentBody_' + tog).prop('value', sel);

         $('a[data-toggle="' + tog + '"]').not('[data-title="' + sel + '"]').removeClass('active').addClass('notActive');
         $('a[data-toggle="' + tog + '"][data-title="' + sel + '"]').removeClass('notActive').addClass('active');
      });
   });
</script>
</asp:Content>

