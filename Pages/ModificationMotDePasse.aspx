<%@ Page Language="C#" MasterPageFile="~/PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="ModificationMotDePasse.aspx.cs" Inherits="Pages_ModificationMotDePasse" %>

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
   <asp:Panel ID="divModification" runat="server">
      <h1 class="h3 mb-3 font-weight-normal">Veuillez entrer votre mot de passe actuel ainsi votre nouveau mot de passe deux fois</h1>
      <div class="form-group">
         <asp:TextBox ID="tbMotPasseActuel" runat="server" TextMode="Password" CssClass="form-control" placeholder="Mot de passe actuel" MaxLength="50" />
         <asp:HiddenField id="hidMotPasseActuel" runat="server" />
         <asp:Label ID="errMotPasseActuel" runat="server" CssClass="text-danger hidden" />
      </div>
      <div class="form-group">
         <asp:TextBox ID="tbNouveauMotPasse" runat="server" TextMode="Password" CssClass="form-control" style="margin-top: 10px;" placeholder="Nouveau mot de passe" MaxLength="50" />
         <asp:HiddenField id="hidNouveauMotPasse" runat="server" />
         <asp:Label ID="errNouveauMotPasse" runat="server" CssClass="text-danger hidden" />
         <asp:TextBox ID="tbConfirmationNouveauMotPasse" runat="server" TextMode="Password" CssClass="form-control" placeholder="Confimation du nouveau mot de passe" MaxLength="50" />
         <asp:HiddenField id="hidConfirmationNouveauMotPasse" runat="server" />
         <asp:Label ID="errConfirmationNouveauMotPasse" runat="server" CssClass="text-danger hidden" />
      </div>
      <asp:Button ID="btnModifierMotPasse" runat="server" CssClass="btn btn-lg Orange btn-block" Text="Modifier le mot de passe" OnClick="btnModifierMotPasse_Click" />
   </asp:Panel>
</div>
<script type="text/javascript">
   $(document).ready(function () {
      if ($('#contentBody_hidMotPasseActuel').val() != '')
         $('#contentBody_tbMotPasseActuel').val($('#contentBody_hidMotPasseActuel').val());
      if ($('#contentBody_hidNouveauMotPasse').val() != '')
         $('#contentBody_tbNouveauMotPasse').val($('#contentBody_hidNouveauMotPasse').val());
      if ($('#contentBody_hidConfirmationNouveauMotPasse').val() != '')
         $('#contentBody_tbConfirmationNouveauMotPasse').val($('#contentBody_hidConfirmationNouveauMotPasse').val());
      var exprMotPasse = /(?=^[a-zA-Z0-9]*[a-z])(?=^[a-zA-Z0-9]*[A-Z])(?=^[a-zA-Z0-9]*[0-9])(?=^[a-zA-Z0-9]{8,}$)/;
      $('#contentBody_tbMotPasseActuel').focusout(function () {
         if ($('#contentBody_tbMotPasseActuel').val() == '') {
            $('#contentBody_tbMotPasseActuel').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errMotPasseActuel').text('Vous n\'avez pas tapé votre mot de passe').removeClass('hidden');
         } else {
            $('#contentBody_tbMotPasseActuel').removeClass('border-danger').addClass('border-success');
            $('#contentBody_errMotPasseActuel').text('').addClass('hidden');
         }
      });
      $('#contentBody_tbNouveauMotPasse').focusout(function () {
         if ($('#contentBody_tbNouveauMotPasse').val() == '') {
            $('#contentBody_tbNouveauMotPasse').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errNouveauMotPasse').text('Le nouveau mot de passe ne peut pas être vide').removeClass('hidden');
         } else if (!exprMotPasse.test($('#contentBody_tbNouveauMotPasse').val())) {
            $('#contentBody_tbNouveauMotPasse').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errNouveauMotPasse').text('Le nouveau mot de passe doit contenir au moins 8 charactères dont une lettre minuscule, une lettre majuscule et un chiffre').removeClass('hidden');
         } else {
            $('#contentBody_tbNouveauMotPasse').removeClass('border-danger').addClass('border-success');
            $('#contentBody_errNouveauMotPasse').text('').addClass('hidden');
            if ($('#contentBody_tbConfirmationNouveauMotPasse').val() != '') {
               if ($('#contentBody_tbConfirmationNouveauMotPasse').val() != $('#contentBody_tbNouveauMotPasse').val()) {
                  $('#contentBody_tbConfirmationNouveauMotPasse').removeClass('border-success').addClass('border-danger');
                  $('#contentBody_errConfirmationNouveauMotPasse').text('La confirmation du nouveau mot de passe ne correspond pas au mot de passe').removeClass('hidden');
               } else {
                  $('#contentBody_tbConfirmationNouveauMotPasse').removeClass('border-danger').addClass('border-success');
                  $('#contentBody_errConfirmationNouveauMotPasse').text('').addClass('hidden');
               }
            }
         }
      });
      $('#contentBody_tbConfirmationNouveauMotPasse').focusout(function () {
         if ($('#contentBody_tbConfirmationNouveauMotPasse').val() == '') {
            $('#contentBody_tbConfirmationNouveauMotPasse').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errConfirmationNouveauMotPasse').text('La confirmation du nouveau mot de passe ne peut pas être vide').removeClass('hidden');
         } else if ($('#contentBody_tbConfirmationNouveauMotPasse').val() != $('#contentBody_tbNouveauMotPasse').val()) {
            $('#contentBody_tbConfirmationNouveauMotPasse').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errConfirmationNouveauMotPasse').text('La confirmation du nouveau mot de passe ne correspond pas au mot de passe').removeClass('hidden');
         } else {
            $('#contentBody_tbConfirmationNouveauMotPasse').removeClass('border-danger').addClass('border-success');
            $('#contentBody_errConfirmationNouveauMotPasse').text('').addClass('hidden');
         }
      });
      $('#contentBody_btnModifierMotPasse').click(function () {
         var binPageValide = true;
         if ($('#contentBody_tbMotPasseActuel').val() == '') {
            $('#contentBody_tbMotPasseActuel').removeClass('border-success').addClass('border-danger');
            $('#contentBody_errMotPasseActuel').text('Vous n\'avez pas tapé votre mot de passe').removeClass('hidden');
            binPageValide = false;
         }
         if ($('#contentBody_tbNouveauMotPasse').val() == '' || !exprMotPasse.test($('#contentBody_tbNouveauMotPasse').val())) {
            $('#contentBody_tbNouveauMotPasse').removeClass('border-success').addClass('border-danger');
            if ($('#contentBody_tbNouveauMotPasse').val() == '')
               $('#contentBody_errNouveauMotPasse').text('Le nouveau mot de passe ne peut pas être vide').removeClass('hidden');
            else
               $('#contentBody_errNouveauMotPasse').text('Le nouveau mot de passe doit contenir au moins 8 charactères dont une lettre minuscule, une lettre majuscule et un chiffre').removeClass('hidden');
            binPageValide = false;
         }
         if ($('#contentBody_tbConfirmationNouveauMotPasse').val() == '' || $('#contentBody_tbConfirmationNouveauMotPasse').val() != $('#contentBody_tbNouveauMotPasse').val()) {
            $('#contentBody_tbConfirmationNouveauMotPasse').removeClass('border-success').addClass('border-danger');
            if ($('#contentBody_tbConfirmationNouveauMotPasse').val() == '')
               $('#contentBody_errConfirmationNouveauMotPasse').text('La confirmation du nouveau mot de passe ne peut pas être vide').removeClass('hidden');
            else
               $('#contentBody_errConfirmationNouveauMotPasse').text('La confirmation du nouveau mot de passe ne correspond pas au mot de passe').removeClass('hidden');
            binPageValide = false;
         }
         return binPageValide;
      });
   });
</script>
</asp:Content>

