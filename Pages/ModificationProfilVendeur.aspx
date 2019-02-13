<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="ModificationProfilVendeur.aspx.cs" Inherits="Pages_ModificationProfilVendeur" %>

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
            <br />
         </asp:Panel>
      </div>
   </div>
   <h1 class="h3 mb-3 font-weight-normal">Veuillez entrer vos informations</h1>
   <div class="form-group">
      <asp:TextBox ID="tbNomEntreprise" runat="server" CssClass="form-control" placeholder="Nom de l'entreprise" MaxLength="50" />
      <asp:Label ID="errNomEntreprise" runat="server" CssClass="text-danger hidden" />
   </div> 
   <div class="row">
      <div class="form-group col-sm-6">
         <asp:TextBox ID="tbPrenom" runat="server" CssClass="form-control" placeholder="Prénom" MaxLength="50" />
         <asp:Label ID="errPrenom" runat="server" CssClass="text-danger hidden" />
      </div>
      <div class="form-group col-sm-6">
         <asp:TextBox ID="tbNom" runat="server" CssClass="form-control" placeholder="Nom" MaxLength="50" />
         <asp:Label ID="errNom" runat="server" CssClass="text-danger hidden" />
      </div>
   </div>
   <div class="form-group">
      <asp:TextBox ID="tbAdresse" runat="server" CssClass="form-control" placeholder="Adresse" MaxLength="50" />
      <asp:Label ID="errAdresse" runat="server" CssClass="text-danger hidden" />
   </div>
   <div class="form-group">
      <asp:TextBox ID="tbVille" runat="server" CssClass="form-control" placeholder="Ville" MaxLength="50" />
      <asp:Label ID="errVille" runat="server" CssClass="text-danger hidden" />
   </div>
   <div class="form-group">  
      <asp:DropDownList Id="ddlProvince" CssClass="form-control" runat="server">
         <asp:ListItem Value="">Sélectionnez la province</asp:ListItem>
         <asp:ListItem Value="QC"> Québec </asp:ListItem>
         <asp:ListItem Value="ON"> Ontario </asp:ListItem>
         <asp:ListItem Value="NB"> Nouveau-Brunswick </asp:ListItem>
      </asp:DropDownList>
      <asp:Label ID="errProvince" runat="server" CssClass="text-danger hidden" />
   </div>
   <div class="form-group">
      <asp:TextBox ID="tbCodePostal" runat="server" CssClass="form-control" placeholder="Code Postal" MaxLength="7" />
      <asp:Label ID="errCodePostal" runat="server" CssClass="text-danger hidden" />
   </div>
   <div class="form-group">
      <select class="form-control" disabled>
         <option value="">Canada</option>
      </select>
   </div>
   <div class="form-group">
      <asp:TextBox ID="tbTelephone1" runat="server" CssClass="form-control" placeholder="Téléphone 1" MaxLength="20" />
      <asp:Label ID="errTelephone1" runat="server" CssClass="text-danger hidden" />
      <asp:TextBox ID="tbTelephone2" runat="server" CssClass="form-control" placeholder="Téléphone 2 (facultatif)" MaxLength="20" />
      <asp:Label ID="errTelephone2" runat="server" CssClass="text-danger hidden" />
   </div>
   <div class="form-group">
      <asp:TextBox ID="tbCourriel" runat="server" CssClass="form-control" placeholder="Courriel" MaxLength="100" Enabled="False" />
   </div>
   <div class="form-group">
      <div class="input-group">
         <asp:TextBox ID="tbPoidsMaxLivraison" runat="server" TextMode="Number" CssClass="form-control" placeholder="Poids de livraison maximum" />
         <span class="input-group-addon">lbs</span>
      </div>
      <asp:Label ID="errPoidsMaxLivraison" runat="server" CssClass="text-danger hidden" />
   </div>
   <div class="form-group">
      <div class="input-group">
         <asp:TextBox ID="tbLivraisonGratuite" runat="server" TextMode="Number" step="0.01" CssClass="form-control" placeholder="Montant pour avoir la livraison gratuite" />
         <span class="input-group-addon">$</span>
      </div>
      <asp:Label ID="errLivraisonGratuite" runat="server" CssClass="text-danger hidden" /> 
   </div>
   <div class="form-group text-center">
      <label class="checkbox-inline"><asp:CheckBox ID="cbTaxes" runat="server" />Exemption taxes</label>
   </div>
   <hr />
   <h1 class="h4 mb-3 font-weight-normal">Configuration du magasin</h1>
   <div class="row">
      <div class="col-sm-6">
         <div class="input-group">
            <asp:Image ID="imgTeleverse" runat="server" CssClass="thumbnail img-responsive" style="max-width: 100px" ImageUrl="~/static/images/image_placeholder.png" />
            <asp:FileUpload ID="fImage" runat="server" CssClass="hidden" accept="image/png, image/jpeg" />
            <asp:Label ID="errImage" runat="server" CssClass="text-danger hidden" /> 
            <asp:Button ID="btnTeleverserImage" runat="server" CssClass="hidden" OnClick="btnTeleverserImage_Click" />
         </div>
      </div>
      <div class="col-sm-6">
         <div class="form-group">
            Couleur de fond <input id="cpCouleurFond" runat="server" type="color" />
         </div>
         <div class="form-group">
            Couleur de texte <input id="cpCouleurTexte" runat="server" type="color" />
         </div>
      </div>
   </div>
   <div class="row">
      <div class="form-group col-sm-6">
         <input id="btnSelectionnerImage" type="button" class="btn Orange" value="Sélectionner une image" runat="server" visible="false" />
         <input id="btnChangerImage" type="button" class="btn Orange" value="Changer l'image" runat="server" visible="false" />
      </div>
      <div class="form-group col-sm-6">
         <asp:Button ID="btnRemiseAZero" runat="server" CssClass="btn Orange" Text="Remettre les valeurs par défaut" OnClick="btnRemiseAZero_Click" />
      </div>
   </div>
   <asp:Button ID="btnModifierProfil" runat="server" CssClass="btn btn-lg Orange btn-block" Text="Modifier le profil" OnClick="btnModifierProfil_Click" />
</div>
<script>
   $(document).ready(function () {
      var exprNomEntreprise = /^[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*$/;
      var exprNomOuPrenom = /^[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF]+(([-'\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF])*$/;
      var exprAdresse = /^(\d+-)?\d+([a-zA-Z]|\s\d\/\d)?\s[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*\s[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*$/;
      var exprCodePostal = /^[A-Z]\d[A-Z]\s?\d[A-Z]\d$/i;
      var exprTelephone = /^((\([0-9]{3}\)\s|[0-9]{3}[\s-])[0-9]{3}-[0-9]{4}|[0-9]{10})$/;
      var exprPoids = /^\d+$/;
      var exprMontant = /^\d+\.\d{2}$/;
      $("#contentBody_tbNomEntreprise").focusout(function () {
         if ($("#contentBody_tbNomEntreprise").val() == '') {
            $("#contentBody_tbNomEntreprise").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errNomEntreprise").text('Le nom de l\'entreprise ne peut pas être vide').removeClass('hidden');
         } else if (!exprNomEntreprise.test($("#contentBody_tbNomEntreprise").val())) {
            $("#contentBody_tbNomEntreprise").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errNomEntreprise").text('Le nom de l\'entreprise n\'est pas dans un format valide').removeClass('hidden');
         } else {
            $("#contentBody_tbNomEntreprise").removeClass("border-danger").addClass("border-success");
            $("#contentBody_errNomEntreprise").text('').addClass('hidden');
         }
      });
      $("#contentBody_tbNom").focusout(function () {
         if ($("#contentBody_tbNom").val() == '') {
            $("#contentBody_tbNom").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errNom").text('Le nom ne peut pas être vide').removeClass('hidden');
         } else if (!exprNomOuPrenom.test($("#contentBody_tbNom").val())) {
            $("#contentBody_tbNom").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errNom").text('Le nom n\'est pas dans un format valide').removeClass('hidden');
         } else {
            $("#contentBody_tbNom").removeClass("border-danger").addClass("border-success");
            $("#contentBody_errNom").text('').addClass('hidden');
         }
      });
      $("#contentBody_tbPrenom").focusout(function () {
         if ($("#contentBody_tbPrenom").val() == '') {
            $("#contentBody_tbPrenom").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errPrenom").text('Le prénom ne peut pas être vide').removeClass('hidden');
         } else if (!exprNomOuPrenom.test($("#contentBody_tbPrenom").val())) {
            $("#contentBody_tbPrenom").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errPrenom").text('Le prénom n\'est pas dans un format valide').removeClass('hidden');
         } else {
            $("#contentBody_tbPrenom").removeClass("border-danger").addClass("border-success");
            $("#contentBody_errPrenom").text('').addClass('hidden');
         }
      });
      $("#contentBody_tbAdresse").focusout(function () {
         if ($("#contentBody_tbAdresse").val() == '') {
            $("#contentBody_tbAdresse").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errAdresse").text('L\'adresse ne peut pas être vide').removeClass('hidden');
         } else if (!exprAdresse.test($("#contentBody_tbAdresse").val())) {
            $("#contentBody_tbAdresse").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errAdresse").text('L\'adresse n\'est pas dans un format valide').removeClass('hidden');
         } else {
            $("#contentBody_tbAdresse").removeClass("border-danger").addClass("border-success");
            $("#contentBody_errAdresse").text('').addClass('hidden');
         }
      });
      $("#contentBody_tbVille").focusout(function () {
         if ($("#contentBody_tbVille").val() == '') {
            $("#contentBody_tbVille").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errVille").text('La ville ne peut pas être vide').removeClass('hidden');
         } else if (!exprNomOuPrenom.test($("#contentBody_tbVille").val())) {
            $("#contentBody_tbVille").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errVille").text('La ville n\'est pas dans un format valide').removeClass('hidden');
         } else {
            $("#contentBody_tbVille").removeClass("border-danger").addClass("border-success");
            $("#contentBody_errVille").text('').addClass('hidden');
         }
      });
      $("#contentBody_ddlProvince").focusout(function () {
         if ($("#contentBody_ddlProvince").val() == '') {
            $("#contentBody_ddlProvince").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errProvince").text('Vous devez sélectionner une province').removeClass('hidden');
         } else {
            $("#contentBody_ddlProvince").removeClass("border-danger").addClass("border-success");
            $("#contentBody_errProvince").text('').addClass('hidden');
         }
      });
      $("#contentBody_tbCodePostal").focusout(function () {
         if ($("#contentBody_tbCodePostal").val() == '') {
            $("#contentBody_tbCodePostal").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errCodePostal").text('Le code postal ne peut pas être vide').removeClass('hidden');
         } else if (!exprCodePostal.test($("#contentBody_tbCodePostal").val())) {
            $("#contentBody_tbCodePostal").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errCodePostal").text('Le code postal n\'est pas dans un format valide').removeClass('hidden');
         } else {
            $("#contentBody_tbCodePostal").removeClass("border-danger").addClass("border-success");
            $("#contentBody_errCodePostal").text('').addClass('hidden');
         }
      });
      $("#contentBody_tbTelephone1").focusout(function () {
         if ($("#contentBody_tbTelephone1").val() == '') {
            $("#contentBody_tbTelephone1").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errTelephone1").text('Le téléphone 1 ne peut pas être vide').removeClass('hidden');
         } else if (!exprTelephone.test($("#contentBody_tbTelephone1").val())) {
            $("#contentBody_tbTelephone1").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errTelephone1").text('Le téléphone 1 n\'est pas dans un format valide').removeClass('hidden');
         } else {
            $("#contentBody_tbTelephone1").removeClass("border-danger").addClass("border-success");
            $("#contentBody_errTelephone1").text('').addClass('hidden');
         }
      });
      $("#contentBody_tbTelephone2").focusout(function () {
         if ($("#contentBody_tbTelephone2").val() != '' && !exprTelephone.test($("#contentBody_tbTelephone2").val())) {
            $("#contentBody_tbTelephone2").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errTelephone2").text('Le téléphone 2 n\'est pas dans un format valide').removeClass('hidden');
         } else {
            $("#contentBody_tbTelephone2").removeClass("border-danger").addClass("border-success");
            $("#contentBody_errTelephone2").text('').addClass('hidden');
         }
      });
      $("#contentBody_tbPoidsMaxLivraison").focusout(function () {
         if ($("#contentBody_tbPoidsMaxLivraison").val() == '') {
            $("#contentBody_tbPoidsMaxLivraison").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errPoidsMaxLivraison").text('Le poids de livraison maximum ne peut pas être vide').removeClass('hidden');
         } else if (!exprPoids.test($("#contentBody_tbPoidsMaxLivraison").val())) {
            $("#contentBody_tbPoidsMaxLivraison").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errPoidsMaxLivraison").text('Le poids de livraison maximum doit être un entier').removeClass('hidden');
         } else {
            $("#contentBody_tbPoidsMaxLivraison").removeClass("border-danger").addClass("border-success");
            $("#contentBody_errPoidsMaxLivraison").text('').addClass('hidden');
         }
      });
      $("#contentBody_tbLivraisonGratuite").focusout(function () {
         if ($("#contentBody_tbLivraisonGratuite").val() == '') {
            $("#contentBody_tbLivraisonGratuite").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errLivraisonGratuite").text('Le montant pour avoir la livraison gratuite ne peut pas être vide').removeClass('hidden');
         } else if (!exprMontant.test($("#contentBody_tbLivraisonGratuite").val())) {
            $("#contentBody_tbLivraisonGratuite").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errLivraisonGratuite").text('Le montant pour avoir la livraison gratuite doit être un nombre décimal avec deux chiffres après la virgule').removeClass('hidden');
         } else if ($("#contentBody_tbLivraisonGratuite").val() > 214748.36) {
            $("#contentBody_tbLivraisonGratuite").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errLivraisonGratuite").text('Le montant pour avoir la livraison gratuite doit être inférieur à 214 748,37 $').removeClass('hidden');
         } else {
            $("#contentBody_tbLivraisonGratuite").removeClass("border-danger").addClass("border-success");
            $("#contentBody_errLivraisonGratuite").text('').addClass('hidden');
         }
      });
      if ($("#contentBody_imgTeleverse").attr('src') != '../static/images/image_magasin.jpg')
         $("#contentBody_btnRemiseAZero").removeAttr('disabled');
      else if ($("#contentBody_cpCouleurFond").val() == '#ffffff' && $("#contentBody_cpCouleurTexte").val() == '#000000')
            $("#contentBody_btnRemiseAZero").attr('disabled', 'disabled');
      $("#contentBody_fImage").change(function () {
         $("#contentBody_btnTeleverserImage").click();
      });
      $("#contentBody_btnSelectionnerImage,#contentBody_btnChangerImage").click(function () {
         $("#contentBody_fImage").click();
      });
      $("#contentBody_cpCouleurFond").change(function () {
         if ($("#contentBody_cpCouleurFond").val() != '#ffffff')
            $("#contentBody_btnRemiseAZero").removeAttr('disabled');
         else if ($("#contentBody_imgTeleverse").attr('src') == '../static/images/image_magasin.jpg' && $("#contentBody_cpCouleurTexte").val() == '#000000')
            $("#contentBody_btnRemiseAZero").attr('disabled', 'disabled');
      });
      $("#contentBody_cpCouleurTexte").change(function () {
         if ($("#contentBody_cpCouleurTexte").val() != '#000000')
            $("#contentBody_btnRemiseAZero").removeAttr('disabled');
         else if ($("#contentBody_imgTeleverse").attr('src') == '../static/images/image_magasin.jpg' && $("#contentBody_cpCouleurFond").val() == '#ffffff')
            $("#contentBody_btnRemiseAZero").attr('disabled', 'disabled');
      });
      $("#contentBody_btnModifierProfil").click(function () {
         var binPageValide = true;
         if ($("#contentBody_tbNomEntreprise").val() == '' || !exprNomEntreprise.test($("#contentBody_tbNomEntreprise").val())) {
            $("#contentBody_tbNomEntreprise").removeClass("border-success").addClass("border-danger");
            if ($("#contentBody_tbNomEntreprise").val() == '')
               $("#contentBody_errNomEntreprise").text('Le nom de l\'entreprise ne peut pas être vide').removeClass('hidden');
            else
               $("#contentBody_errNomEntreprise").text('Le nom de l\'entreprise n\'est pas dans un format valide').removeClass('hidden');
            binPageValide = false;
         }
         if ($("#contentBody_tbNom").val() == '' || !exprNomOuPrenom.test($("#contentBody_tbNom").val())) {
            $("#contentBody_tbNom").removeClass("border-success").addClass("border-danger");
            if ($("#contentBody_tbNom").val() == '')
               $("#contentBody_errNom").text('Le nom ne peut pas être vide').removeClass('hidden');
            else
               $("#contentBody_errNom").text('Le nom n\'est pas dans un format valide').removeClass('hidden');
            binPageValide = false;
         }
         if ($("#contentBody_tbPrenom").val() == '' || !exprNomOuPrenom.test($("#contentBody_tbPrenom").val())) {
            $("#contentBody_tbPrenom").removeClass("border-success").addClass("border-danger");
            if ($("#contentBody_tbPrenom").val() == '')
               $("#contentBody_errPrenom").text('Le prénom ne peut pas être vide').removeClass('hidden');
            else
               $("#contentBody_errPrenom").text('Le prénom n\'est pas dans un format valide').removeClass('hidden');
            binPageValide = false;
         }
         if ($("#contentBody_tbAdresse").val() == '' || !exprAdresse.test($("#contentBody_tbAdresse").val())) {
            $("#contentBody_tbAdresse").removeClass("border-success").addClass("border-danger");
            if ($("#contentBody_tbAdresse").val() == '')
               $("#contentBody_errAdresse").text('L\'adresse ne peut pas être vide').removeClass('hidden');
            else
               $("#contentBody_errAdresse").text('L\'adresse n\'est pas dans un format valide').removeClass('hidden');
            binPageValide = false;
         }
         if ($("#contentBody_tbVille").val() == '' || !exprNomOuPrenom.test($("#contentBody_tbVille").val())) {
            $("#contentBody_tbVille").removeClass("border-success").addClass("border-danger");
            if ($("#contentBody_tbVille").val() == '')
               $("#contentBody_errVille").text('La ville ne peut pas être vide').removeClass('hidden');
            else
               $("#contentBody_errVille").text('La ville n\'est pas dans un format valide').removeClass('hidden');
            binPageValide = false;
         }
         if ($("#contentBody_ddlProvince").val() == '') {
            $("#contentBody_ddlProvince").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errProvince").text('Vous devez sélectionner une province').removeClass('hidden');
            binPageValide = false;
         }
         if ($("#contentBody_tbCodePostal").val() == '' || !exprCodePostal.test($("#contentBody_tbCodePostal").val())) {
            $("#contentBody_tbCodePostal").removeClass("border-success").addClass("border-danger");
            if ($("#contentBody_tbCodePostal").val() == '')
               $("#contentBody_errCodePostal").text('Le code postal ne peut pas être vide').removeClass('hidden');
            else
               $("#contentBody_errCodePostal").text('Le code postal n\'est pas dans un format valide').removeClass('hidden');
            binPageValide = false;
         }
         if ($("#contentBody_tbTelephone1").val() == '' || !exprTelephone.test($("#contentBody_tbTelephone1").val())) {
            $("#contentBody_tbTelephone1").removeClass("border-success").addClass("border-danger");
            if ($("#contentBody_tbTelephone1").val() == '')
               $("#contentBody_errTelephone1").text('Le téléphone 1 ne peut pas être vide').removeClass('hidden');
            else
               $("#contentBody_errTelephone1").text('Le téléphone 1 n\'est pas dans un format valide').removeClass('hidden');
            binPageValide = false;
         }
         if ($("#contentBody_tbTelephone2").val() != '' && !exprTelephone.test($("#contentBody_tbTelephone2").val())) {
            $("#contentBody_tbTelephone2").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errTelephone2").text('Le téléphone 2 n\'est pas dans un format valide').removeClass('hidden');
            binPageValide = false;
         }
         if ($("#contentBody_tbPoidsMaxLivraison").val() == '' || !exprPoids.test($("#contentBody_tbPoidsMaxLivraison").val())) {
            $("#contentBody_tbPoidsMaxLivraison").removeClass("border-success").addClass("border-danger");
            if ($("#contentBody_tbPoidsMaxLivraison").val() == '')
               $("#contentBody_errPoidsMaxLivraison").text('Le poids de livraison maximum ne peut pas être vide').removeClass('hidden');
            else
               $("#contentBody_errPoidsMaxLivraison").text('Le poids de livraison maximum doit être un entier').removeClass('hidden');
            binPageValide = false;
         }
         if ($("#contentBody_tbLivraisonGratuite").val() == '' || !exprMontant.test($("#contentBody_tbLivraisonGratuite").val()) || $("#contentBody_tbLivraisonGratuite").val() > 214748.36) {
            $("#contentBody_tbLivraisonGratuite").removeClass("border-success").addClass("border-danger");
            if ($("#contentBody_tbLivraisonGratuite").val() == '')
               $("#contentBody_errLivraisonGratuite").text('Le montant pour avoir la livraison gratuite ne peut pas être vide').removeClass('hidden');
            else if (!exprMontant.test($("#contentBody_tbLivraisonGratuite").val()))
               $("#contentBody_errLivraisonGratuite").text('Le montant pour avoir la livraison gratuite doit être un nombre décimal avec deux chiffres après la virgule').removeClass('hidden');
            else
               $("#contentBody_errLivraisonGratuite").text('Le montant pour avoir la livraison gratuite doit être inférieur à 214 748,37 $').removeClass('hidden');
            binPageValide = false;
         }
         return binPageValide;
      });
   });
</script>
</asp:Content>

