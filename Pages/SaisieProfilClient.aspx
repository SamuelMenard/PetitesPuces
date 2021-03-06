﻿<%@ Page Language="C#" MasterPageFile="~/PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="SaisieProfilClient.aspx.cs" Inherits="Pages_SaisieProfilClient" %>

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
   <div class="row">
      <div class="form-group col-sm-6">
         <label for="tbPrenom">Prénom</label>
         <asp:TextBox ID="tbPrenom" runat="server" CssClass="form-control" MaxLength="50" />
         <asp:Label ID="errPrenom" runat="server" CssClass="text-danger hidden" />
      </div>
      <div class="form-group col-sm-6">
         <label for="tbNom">Nom</label>
         <asp:TextBox ID="tbNom" runat="server" CssClass="form-control" MaxLength="50" />
         <asp:Label ID="errNom" runat="server" CssClass="text-danger hidden" />
      </div>
   </div>
   <div class="form-group">
      <label for="tbAdresse">Adresse</label>
      <asp:TextBox ID="tbAdresse" runat="server" CssClass="form-control" MaxLength="50" />
      <asp:Label ID="errAdresse" runat="server" CssClass="text-danger hidden" />
   </div>
   <div class="form-group">
      <label for="tbVille">Ville</label>
      <asp:TextBox ID="tbVille" runat="server" CssClass="form-control" MaxLength="50" />
      <asp:Label ID="errVille" runat="server" CssClass="text-danger hidden" />
   </div>
   <div class="form-group">  
      <label for="ddlProvince">Province</label>
      <asp:DropDownList Id="ddlProvince" CssClass="form-control" runat="server">
         <asp:ListItem Value="">Sélectionnez la province</asp:ListItem>
         <asp:ListItem Value="QC"> Québec </asp:ListItem>
         <asp:ListItem Value="ON"> Ontario </asp:ListItem>
         <asp:ListItem Value="NB"> Nouveau-Brunswick </asp:ListItem>
      </asp:DropDownList>
      <asp:Label ID="errProvince" runat="server" CssClass="text-danger hidden" />
   </div>
   <div class="form-group">
      <label for="tbCodePostal">Code Postal</label>
      <asp:TextBox ID="tbCodePostal" runat="server" CssClass="form-control" MaxLength="7" />
      <asp:Label ID="errCodePostal" runat="server" CssClass="text-danger hidden" />
   </div>
   <div class="form-group">
      <label>Pays</label>
      <select class="form-control" disabled>
         <option value="">Canada</option>
      </select>
   </div>
   <div class="form-group">
      <label for="tbTelephone1">Téléphone 1</label>
      <asp:TextBox ID="tbTelephone1" runat="server" CssClass="form-control" MaxLength="20" />
      <asp:Label ID="errTelephone1" runat="server" CssClass="text-danger hidden" />
   </div>
   <div class="form-group">
      <label for="tbTelephone2">Téléphone 2 (facultatif)</label>
      <asp:TextBox ID="tbTelephone2" runat="server" CssClass="form-control" MaxLength="20" />
      <asp:Label ID="errTelephone2" runat="server" CssClass="text-danger hidden" />
   </div>
   <div class="form-group">
      <label for="tbCourriel">Courriel</label>
      <asp:TextBox ID="tbCourriel" runat="server" CssClass="form-control" MaxLength="100" Enabled="False" />
   </div>
   <asp:Button ID="btnInscription" runat="server" CssClass="btn btn-lg Orange btn-block" Text="Modifier" OnClick="btnInscription_Click" />
</div>
<script>
   $(document).ready(function () {
      var exprNomOuPrenom = /^[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF]+(([-'\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF])*$/;
      var exprAdresse = /^(\d+-)?\d+([a-zA-Z]|\s\d\/\d)?\s[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*\s[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*$/;
      var exprCodePostal = /^[A-Z]\d[A-Z][\s-]?\d[A-Z]\d$/i;
      var exprTelephone = /^((\([0-9]{3}\)\s|[0-9]{3}[\s-])[0-9]{3}-[0-9]{4}|[0-9]{10})$/;
      $('#contentBody_tbNom').focusout(function () {
         if ($('#contentBody_tbNom').val() != '' && !exprNomOuPrenom.test($('#contentBody_tbNom').val())) {
            $('#contentBody_tbNom').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errNom').text('Le nom n\'est pas valide').removeClass('hidden');
         } else {
            $('#contentBody_tbNom').removeClass('border-danger').addClass('border-success');
            $('#contentBody_errNom').text('').addClass('hidden');
         }
      });
      $('#contentBody_tbPrenom').focusout(function () {
         if ($('#contentBody_tbPrenom').val() != '' && !exprNomOuPrenom.test($('#contentBody_tbPrenom').val())) {
            $('#contentBody_tbPrenom').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errPrenom').text('Le prénom n\'est pas valide').removeClass('hidden');
         } else {
            $('#contentBody_tbPrenom').removeClass('border-danger').addClass('border-success');
            $('#contentBody_errPrenom').text('').addClass('hidden');
         }
      });
      $('#contentBody_tbAdresse').focusout(function () {
         if ($('#contentBody_tbAdresse').val() != '' && !exprAdresse.test($('#contentBody_tbAdresse').val())) {
            $('#contentBody_tbAdresse').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errAdresse').text('L\'adresse n\'est pas dans un format valide. Référez-vous aux directives d\'adressage de Poste Canada à l\'adresse : https://www.canadapost.ca/tools/pg/manual/PGaddress-f.asp?ecid=murl10006450#contentBody_1437041').removeClass('hidden');
         } else {
            $('#contentBody_tbAdresse').removeClass('border-danger').addClass('border-success');
            $('#contentBody_errAdresse').text('').addClass('hidden');
         }
      });
      $('#contentBody_tbVille').focusout(function () {
         if ($('#contentBody_tbVille').val() != '' && !exprNomOuPrenom.test($('#contentBody_tbVille').val())) {
            $('#contentBody_tbVille').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errVille').text('La ville n\'est pas valide').removeClass('hidden');
         } else {
            $('#contentBody_tbVille').removeClass('border-danger').addClass('border-success');
            $('#contentBody_errVille').text('').addClass('hidden');
         }
      });
      $('#contentBody_tbCodePostal').focusout(function () {
         if ($('#contentBody_tbCodePostal').val() != '' && !exprCodePostal.test($('#contentBody_tbCodePostal').val())) {
            $('#contentBody_tbCodePostal').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errCodePostal').text('Le code postal n\'est pas dans un format valide. Les formats acceptés sont A9A9A9 ou A9A 9A9 ou A9A-9A9').removeClass('hidden');
         } else {
            $('#contentBody_tbCodePostal').removeClass('border-danger').addClass('border-success');
            $('#contentBody_errCodePostal').text('').addClass('hidden');
         }
      });
      $('#contentBody_tbTelephone1').focusout(function () {
         if ($('#contentBody_tbTelephone1').val() != '' && !exprTelephone.test($('#contentBody_tbTelephone1').val())) {
            $('#contentBody_tbTelephone1').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errTelephone1').text('Le téléphone 1 n\'est pas dans un format valide. Les formats acceptés sont 9999999999 ou (999) 999-9999 ou 999 999-9999 ou 999-999-9999').removeClass('hidden');
         } else {
            $('#contentBody_tbTelephone1').removeClass('border-danger').addClass('border-success');
            $('#contentBody_errTelephone1').text('').addClass('hidden');
         }
      });
      $('#contentBody_tbTelephone2').focusout(function () {
         if ($('#contentBody_tbTelephone2').val() != '' && !exprTelephone.test($('#contentBody_tbTelephone2').val())) {
            $('#contentBody_tbTelephone2').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errTelephone2').text('Le téléphone 2 n\'est pas dans un format valide. Les formats acceptés sont 9999999999 ou (999) 999-9999 ou 999 999-9999 ou 999-999-9999').removeClass('hidden');
         } else {
            $('#contentBody_tbTelephone2').removeClass('border-danger').addClass('border-success');
            $('#contentBody_errTelephone2').text('').addClass('hidden');
         }
      });
      $('#contentBody_btnInscription').click(function () {
         var binPageValide = true;
         if ($('#contentBody_tbNom').val() != '' && !exprNomOuPrenom.test($('#contentBody_tbNom').val())) {
            $('#contentBody_tbNom').removeClass('border-success').addClass('border-danger');           
            $('#contentBody_errNom').text('Le nom n\'est pas valide').removeClass('hidden');
            binPageValide = false;
         }
         if ($('#contentBody_tbPrenom').val() != '' && !exprNomOuPrenom.test($('#contentBody_tbPrenom').val())) {
            $('#contentBody_tbPrenom').removeClass('border-success').addClass('border-danger');          
            $('#contentBody_errPrenom').text('Le prénom n\'est pas valide').removeClass('hidden');
            binPageValide = false;
         }
         if ($('#contentBody_tbAdresse').val() != '' && !exprAdresse.test($('#contentBody_tbAdresse').val())) {
            $('#contentBody_tbAdresse').removeClass('border-success').addClass('border-danger');           
            $('#contentBody_errAdresse').text('L\'adresse n\'est pas dans un format valide. Référez-vous aux directives d\'adressage de Poste Canada à l\'adresse : https://www.canadapost.ca/tools/pg/manual/PGaddress-f.asp?ecid=murl10006450#contentBody_1437041').removeClass('hidden');
            binPageValide = false;
         }
         if ($('#contentBody_tbVille').val() != '' && !exprNomOuPrenom.test($('#contentBody_tbVille').val())) {
            $('#contentBody_tbVille').removeClass('border-success').addClass('border-danger');          
            $('#contentBody_errVille').text('La ville n\'est pas valide').removeClass('hidden');
            binPageValide = false;
         }         
         if ($('#contentBody_tbCodePostal').val() != '' && !exprCodePostal.test($('#contentBody_tbCodePostal').val())) {
            $('#contentBody_tbCodePostal').removeClass('border-success').addClass('border-danger');          
            $('#contentBody_errCodePostal').text('Le code postal n\'est pas dans un format valide. Les formats acceptés sont A9A9A9 ou A9A 9A9 ou A9A-9A9').removeClass('hidden');
            binPageValide = false;
         }
         if ($('#contentBody_tbTelephone1').val() != '' && !exprTelephone.test($('#contentBody_tbTelephone1').val())) {
            $('#contentBody_tbTelephone1').removeClass('border-success').addClass('border-danger');          
            $('#contentBody_errTelephone1').text('Le téléphone 1 n\'est pas dans un format valide. Les formats acceptés sont 9999999999 ou (999) 999-9999 ou 999 999-9999 ou 999-999-9999').removeClass('hidden');
            binPageValide = false;
         }
         if ($('#contentBody_tbTelephone2').val() != '' && !exprTelephone.test($('#contentBody_tbTelephone2').val())) {
            $('#contentBody_tbTelephone2').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errTelephone2').text('Le téléphone 2 n\'est pas dans un format valide. Les formats acceptés sont 9999999999 ou (999) 999-9999 ou 999 999-9999 ou 999-999-9999').removeClass('hidden');
            binPageValide = false;
         }
         return binPageValide;
      });
   });
</script>
</asp:Content>

