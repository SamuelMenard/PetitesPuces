<%@ Page Language="C#" MasterPageFile="~/PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="InscriptionClientVendeur.aspx.cs" Inherits="Pages_InscriptionClientVendeur" %>

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
   <div class="row">
      <div class="message text-left">
         <asp:Panel ID="divCourriel" runat="server" Visible="False">
            <div class="row">
               <div class="col-sm-6">
                  Expéditeur
                  <asp:TextBox ID="tbExpediteur" runat="server" CssClass="form-control" Enabled="false" style="margin-bottom: 10px" />
               </div>
               <div class="col-sm-6">
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
   <asp:Panel ID="divInscription" runat="server">
      <h1 class="h3 mb-3 font-weight-normal">Veuillez entrer vos informations</h1>
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
         <asp:TextBox ID="tbCourriel" runat="server" CssClass="form-control" placeholder="Courriel" MaxLength="100" />
         <asp:Label ID="errCourriel" runat="server" CssClass="text-danger hidden" />
         <asp:TextBox ID="tbConfirmationCourriel" runat="server" CssClass="form-control" placeholder="Confimation courriel" MaxLength="100" />
         <asp:Label ID="errConfirmationCourriel" runat="server" CssClass="text-danger hidden" />
      </div>
      <div class="form-group">
         <asp:TextBox ID="tbMotPasse" runat="server" TextMode="Password" CssClass="form-control" placeholder="Mot de passe" MaxLength="50" />
         <asp:Label ID="errMotPasse" runat="server" CssClass="text-danger hidden" />
         <asp:TextBox ID="tbConfirmationMotPasse" runat="server" TextMode="Password" CssClass="form-control" placeholder="Confimation mot de passe" MaxLength="50" />
         <asp:Label ID="errConfirmationMotPasse" runat="server" CssClass="text-danger hidden" />
      </div>
      <asp:Button ID="btnInscription" runat="server" CssClass="btn btn-lg Orange btn-block" Text="S'inscrire" OnClick="btnInscription_Click" />
   </asp:Panel>
</div>
<script>
   $(document).ready(function () {
      var exprNomOuPrenom = /^[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF]+(([-'\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF])*$/;
      var exprAdresse = /^(\d+-)?\d+([a-zA-Z]|\s\d\/\d)?\s[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*\s[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*$/;
      var exprCodePostal = /^[A-Z]\d[A-Z][\s-]?\d[A-Z]\d$/i;
      var exprTelephone = /^((\([0-9]{3}\)\s|[0-9]{3}[\s-])[0-9]{3}-[0-9]{4}|[0-9]{10})$/;
      var exprCourriel = /^[a-zA-Z0-9]+([-._][a-zA-Z0-9]+)*@[a-zA-Z0-9]+([-._][a-zA-Z0-9]+)*\.[a-z]+$/;
      var exprMotPasse = /(?=^[a-zA-Z0-9]*[a-z])(?=^[a-zA-Z0-9]*[A-Z])(?=^[a-zA-Z0-9]*[0-9])(?=^[a-zA-Z0-9]{8,}$)/;
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
            $("#contentBody_errAdresse").text('L\'adresse n\'est pas dans un format valide. Référez-vous aux directives d\'adressage de Poste Canada à l\'adresse : https://www.canadapost.ca/tools/pg/manual/PGaddress-f.asp?ecid=murl10006450#1437041').removeClass('hidden');
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
      $("#contentBody_tbCourriel").focusout(function () {
         if ($("#contentBody_tbCourriel").val() == '') {
            $("#contentBody_tbCourriel").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errCourriel").text('Le courriel ne peut pas être vide').removeClass('hidden');
         } else if (!exprCourriel.test($("#contentBody_tbCourriel").val())) {
            $("#contentBody_tbCourriel").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errCourriel").text('Le courriel n\'est pas dans un format valide').removeClass('hidden');
         } else {
            $("#contentBody_tbCourriel").removeClass("border-danger").addClass("border-success");
            $("#contentBody_errCourriel").text('').addClass('hidden');
            if ($("#contentBody_tbConfirmationCourriel").val() != '' && exprCourriel.test($("#contentBody_tbConfirmationCourriel").val())) {
               if ($("#contentBody_tbConfirmationCourriel").val() != $("#contentBody_tbCourriel").val()) {
                  $("#contentBody_tbConfirmationCourriel").removeClass("border-success").addClass("border-danger");
                  $("#contentBody_errConfirmationCourriel").text('La confirmation du courriel ne correspond pas au courriel').removeClass('hidden');
               } else {
                  $("#contentBody_tbConfirmationCourriel").removeClass("border-danger").addClass("border-success");
                  $("#contentBody_errConfirmationCourriel").text('').addClass('hidden');
               }
            }
         }
      });
      $("#contentBody_tbConfirmationCourriel").focusout(function () {
         if ($("#contentBody_tbConfirmationCourriel").val() == '') {
            $("#contentBody_tbConfirmationCourriel").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errConfirmationCourriel").text('La confirmation du courriel ne peut pas être vide').removeClass('hidden');
         } else if (!exprCourriel.test($("#contentBody_tbConfirmationCourriel").val())) {
            $("#contentBody_tbConfirmationCourriel").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errConfirmationCourriel").text('La confirmation du courriel n\'est pas dans un format valide').removeClass('hidden');
         } else if ($("#contentBody_tbConfirmationCourriel").val() != $("#contentBody_tbCourriel").val()) {
            $("#contentBody_tbConfirmationCourriel").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errConfirmationCourriel").text('La confirmation du courriel ne correspond pas au courriel').removeClass('hidden');
         } else {
            $("#contentBody_tbConfirmationCourriel").removeClass("border-danger").addClass("border-success");
            $("#contentBody_errConfirmationCourriel").text('').addClass('hidden');
         }
      });
      $("#contentBody_tbMotPasse").focusout(function () {
         if ($("#contentBody_tbMotPasse").val() == '') {
            $("#contentBody_tbMotPasse").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errMotPasse").text('Le mot de passe ne peut pas être vide').removeClass('hidden');
         } else if (!exprMotPasse.test($("#contentBody_tbMotPasse").val())) {
            $("#contentBody_tbMotPasse").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errMotPasse").text('Le mot de passe doit contenir au moins 8 charactères dont une lettre minuscule, une lettre majuscule et un chiffre').removeClass('hidden');
         } else {
            $("#contentBody_tbMotPasse").removeClass("border-danger").addClass("border-success");
            $("#contentBody_errMotPasse").text('').addClass('hidden');
            if ($("#contentBody_tbConfirmationMotPasse").val() != '') {
               if ($("#contentBody_tbConfirmationMotPasse").val() != $("#contentBody_tbMotPasse").val()) {
                  $("#contentBody_tbConfirmationMotPasse").removeClass("border-success").addClass("border-danger");
                  $("#contentBody_errConfirmationMotPasse").text('La confirmation du mot de passe ne correspond pas au mot de passe').removeClass('hidden');
               } else {
                  $("#contentBody_tbConfirmationMotPasse").removeClass("border-danger").addClass("border-success");
                  $("#contentBody_errConfirmationMotPasse").text('').addClass('hidden');
               }
            }
         }
      });
      $("#contentBody_tbConfirmationMotPasse").focusout(function () {
         if ($("#contentBody_tbConfirmationMotPasse").val() == '') {
            $("#contentBody_tbConfirmationMotPasse").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errConfirmationMotPasse").text('La confirmation du mot de passe ne peut pas être vide').removeClass('hidden');
         } else if ($("#contentBody_tbConfirmationMotPasse").val() != $("#contentBody_tbMotPasse").val()) {
            $("#contentBody_tbConfirmationMotPasse").removeClass("border-success").addClass("border-danger");
            $("#contentBody_errConfirmationMotPasse").text('La confirmation du mot de passe ne correspond pas au mot de passe').removeClass('hidden');
         } else {
            $("#contentBody_tbConfirmationMotPasse").removeClass("border-danger").addClass("border-success");
            $("#contentBody_errConfirmationMotPasse").text('').addClass('hidden');
         }
      });
      $("#contentBody_btnInscription").click(function () {
         var binPageValide = true;
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
               $("#contentBody_errAdresse").text('L\'adresse n\'est pas dans un format valide. Référez-vous aux directives d\'adressage de Poste Canada à l\'adresse : https://www.canadapost.ca/tools/pg/manual/PGaddress-f.asp?ecid=murl10006450#1437041').removeClass('hidden');
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
         if ($("#contentBody_tbCourriel").val() == '' || !exprCourriel.test($("#contentBody_tbCourriel").val())) {
            $("#contentBody_tbCourriel").removeClass("border-success").addClass("border-danger");
            if ($("#contentBody_tbCourriel").val() == '')
               $("#contentBody_errCourriel").text('Le courriel ne peut pas être vide').removeClass('hidden');
            else
               $("#contentBody_errCourriel").text('Le courriel n\'est pas dans un format valide').removeClass('hidden');
            binPageValide = false;
         }
         if ($("#contentBody_tbConfirmationCourriel").val() == '' || !exprCourriel.test($("#contentBody_tbConfirmationCourriel").val()) || $("#contentBody_tbConfirmationCourriel").val() != $("#contentBody_tbCourriel").val()) {
            $("#contentBody_tbConfirmationCourriel").removeClass("border-success").addClass("border-danger");
            if ($("#contentBody_tbConfirmationCourriel").val() == '')
               $("#contentBody_errConfirmationCourriel").text('La confirmation du courriel ne peut pas être vide').removeClass('hidden');
            else if (!exprCourriel.test($("#contentBody_tbConfirmationCourriel").val()))
               $("#contentBody_errConfirmationCourriel").text('La confirmation du courriel n\'est pas dans un format valide').removeClass('hidden');
            else
               $("#contentBody_errConfirmationCourriel").text('La confirmation du courriel ne correspond pas au courriel').removeClass('hidden');                                   
            binPageValide = false;
         }
         if ($("#contentBody_tbMotPasse").val() == '' || !exprMotPasse.test($("#contentBody_tbMotPasse").val())) {
            $("#contentBody_tbMotPasse").removeClass("border-success").addClass("border-danger");
            if ($("#contentBody_tbMotPasse").val() == '')
               $("#contentBody_errMotPasse").text('Le mot de passe ne peut pas être vide').removeClass('hidden');
            else
               $("#contentBody_errMotPasse").text('Le mot de passe doit contenir au moins 8 charactères dont une lettre minuscule, une lettre majuscule et un chiffre').removeClass('hidden');
            binPageValide = false;
         }
         if ($("#contentBody_tbConfirmationMotPasse").val() == '' || $("#contentBody_tbConfirmationMotPasse").val() != $("#contentBody_tbMotPasse").val()) {
            $("#contentBody_tbConfirmationMotPasse").removeClass("border-success").addClass("border-danger");
            if ($("#contentBody_tbConfirmationMotPasse").val() == '')
               $("#contentBody_errConfirmationMotPasse").text('La confirmation du mot de passe ne peut pas être vide').removeClass('hidden');
            else
               $("#contentBody_errConfirmationMotPasse").text('La confirmation du mot de passe ne correspond pas au mot de passe').removeClass('hidden');
            binPageValide = false;
         }
         return binPageValide;
      });
   });
</script>
</asp:Content>

