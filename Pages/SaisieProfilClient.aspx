<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="SaisieProfilClient.aspx.cs" Inherits="Pages_SaisieProfilClient" %>

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
      <asp:TextBox ID="tbCourriel" runat="server" CssClass="form-control" placeholder="Courriel" MaxLength="100" Enabled="false" />     
   </div>
   
   <asp:Button ID="btnInscription" runat="server" CssClass="btn btn-lg Orange btn-block" Text="Modifier" OnClick="btnInscription_Click" />
</div>
<script>
   $(document).ready(function () {
            var exprNomOuPrenom = /^[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF]+(([-'\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF])*$/;
            var exprAdresse = /^(\d+-)?\d+([a-zA-Z]|\s\d\/\d)?\s[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*\s[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*$/;
            var exprCodePostal = /^[A-Z]\d[A-Z]\s?\d[A-Z]\d$/i;
            var exprTelephone = /^((\([0-9]{3}\)\s|[0-9]{3}[\s-])[0-9]{3}-[0-9]{4}|[0-9]{10})$/;
            var exprCourriel = /^[a-zA-Z0-9]+([-._][a-zA-Z0-9]+)*@[a-zA-Z0-9]+([-._][a-zA-Z0-9]+)*\.[a-z]+$/;
            var exprMotPasse = /(?=^[a-zA-Z0-9]*[a-z])(?=^[a-zA-Z0-9]*[A-Z])(?=^[a-zA-Z0-9]*[0-9])(?=^[a-zA-Z0-9]{8,}$)/;
            $("#tbNom").focusout(function () {
               if ($("#tbNom").val() == '') {
                  $("#tbNom").removeClass("border-success").addClass("border-danger");
                  $("#errNom").text('Le nom ne peut pas être vide').removeClass('d-none');
               } else if (!exprNomOuPrenom.test($("#tbNom").val())) {
                  $("#tbNom").removeClass("border-success").addClass("border-danger");
                  $("#errNom").text('Le nom n\'est pas dans un format valide').removeClass('d-none');
               } else {
                  $("#tbNom").removeClass("border-danger").addClass("border-success");
                  $("#errNom").text('').addClass('d-none');
               }
            });
            $("#tbPrenom").focusout(function () {
               if ($("#tbPrenom").val() == '') {
                  $("#tbPrenom").removeClass("border-success").addClass("border-danger");
                  $("#errPrenom").text('Le prénom ne peut pas être vide').removeClass('d-none');
               } else if (!exprNomOuPrenom.test($("#tbPrenom").val())) {
                  $("#tbPrenom").removeClass("border-success").addClass("border-danger");
                  $("#errPrenom").text('Le prénom n\'est pas dans un format valide').removeClass('d-none');
               } else {
                  $("#tbPrenom").removeClass("border-danger").addClass("border-success");
                  $("#errPrenom").text('').addClass('d-none');
               }
            });
            $("#tbAdresse").focusout(function () {
               if ($("#tbAdresse").val() == '') {
                  $("#tbAdresse").removeClass("border-success").addClass("border-danger");
                  $("#errAdresse").text('L\'adresse ne peut pas être vide').removeClass('d-none');
               } else if (!exprAdresse.test($("#tbAdresse").val())) {
                  $("#tbAdresse").removeClass("border-success").addClass("border-danger");
                  $("#errAdresse").text('L\'adresse n\'est pas dans un format valide').removeClass('d-none');
               } else {
                  $("#tbAdresse").removeClass("border-danger").addClass("border-success");
                  $("#errAdresse").text('').addClass('d-none');
               }
            });
            $("#tbVille").focusout(function () {
               if ($("#tbVille").val() == '') {
                  $("#tbVille").removeClass("border-success").addClass("border-danger");
                  $("#errVille").text('La ville ne peut pas être vide').removeClass('d-none');
               } else if (!exprNomOuPrenom.test($("#tbVille").val())) {
                  $("#tbVille").removeClass("border-success").addClass("border-danger");
                  $("#errVille").text('La ville n\'est pas dans un format valide').removeClass('d-none');
               } else {
                  $("#tbVille").removeClass("border-danger").addClass("border-success");
                  $("#errVille").text('').addClass('d-none');
               }
            });
            $("#ddlProvince").focusout(function () {
               if ($("#ddlProvince").val() == '') {
                  $("#ddlProvince").removeClass("border-success").addClass("border-danger");
                  $("#errProvince").text('Vous devez sélectionner une province').removeClass('d-none');
               } else {
                  $("#ddlProvince").removeClass("border-danger").addClass("border-success");
                  $("#errProvince").text('').addClass('d-none');
               }
            });
            $("#tbCodePostal").focusout(function () {
               if ($("#tbCodePostal").val() == '') {
                  $("#tbCodePostal").removeClass("border-success").addClass("border-danger");
                  $("#errCodePostal").text('Le code postal ne peut pas être vide').removeClass('d-none');
               } else if (!exprCodePostal.test($("#tbCodePostal").val())) {
                  $("#tbCodePostal").removeClass("border-success").addClass("border-danger");
                  $("#errCodePostal").text('Le code postal n\'est pas dans un format valide').removeClass('d-none');
               } else {
                  $("#tbCodePostal").removeClass("border-danger").addClass("border-success");
                  $("#errCodePostal").text('').addClass('d-none');
               }
            });
            $("#tbTelephone1").focusout(function () {
               if ($("#tbTelephone1").val() == '') {
                  $("#tbTelephone1").removeClass("border-success").addClass("border-danger");
                  $("#errTelephone1").text('Le téléphone 1 ne peut pas être vide').removeClass('d-none');
               } else if (!exprTelephone.test($("#tbTelephone1").val())) {
                  $("#tbTelephone1").removeClass("border-success").addClass("border-danger");
                  $("#errTelephone1").text('Le téléphone 1 n\'est pas dans un format valide').removeClass('d-none');
               } else {
                  $("#tbTelephone1").removeClass("border-danger").addClass("border-success");
                  $("#errTelephone1").text('').addClass('d-none');
               }
            });
            $("#tbTelephone2").focusout(function () {
               if ($("#tbTelephone2").val() != '' && !exprTelephone.test($("#tbTelephone2").val())) {
                  $("#tbTelephone2").removeClass("border-success").addClass("border-danger");
                  $("#errTelephone2").text('Le téléphone 2 n\'est pas dans un format valide').removeClass('d-none');
               } else {
                  $("#tbTelephone2").removeClass("border-danger").addClass("border-success");
                  $("#errTelephone2").text('').addClass('d-none');
               }
            });
     
     $("#btnInscription").click(function () {
        var binPageValide = true;
        if ($("#tbNom").val() == '' || !exprNomOuPrenom.test($("#tbNom").val())) {
            $("#tbNom").removeClass("border-success").addClass("border-danger");
            if ($("#tbNom").val() == '')
                $("#errNom").text('Le nom ne peut pas être vide').removeClass('d-none');
            else
                $("#errNom").text('Le nom n\'est pas dans un format valide').removeClass('d-none');
            binPageValide = false;
        }
        if ($("#tbPrenom").val() == '' || !exprNomOuPrenom.test($("#tbPrenom").val())) {
            $("#tbPrenom").removeClass("border-success").addClass("border-danger");
            if ($("#tbPrenom").val() == '')
                $("#errPrenom").text('Le prénom ne peut pas être vide').removeClass('d-none');
            else
                $("#errPrenom").text('Le prénom n\'est pas dans un format valide').removeClass('d-none');
            binPageValide = false;
        }
        if ($("#tbAdresse").val() == '' || !exprAdresse.test($("#tbAdresse").val())) {
            $("#tbAdresse").removeClass("border-success").addClass("border-danger");
            if ($("#tbAdresse").val() == '')
                $("#errAdresse").text('L\'adresse ne peut pas être vide').removeClass('d-none');
            else
                $("#errAdresse").text('L\'adresse n\'est pas dans un format valide').removeClass('d-none');
            binPageValide = false;
        }
        if ($("#tbVille").val() == '' || !exprNomOuPrenom.test($("#tbVille").val())) {
            $("#tbVille").removeClass("border-success").addClass("border-danger");
            if ($("#tbVille").val() == '')
                $("#errVille").text('La ville ne peut pas être vide').removeClass('d-none');
            else
                $("#errVille").text('La ville n\'est pas dans un format valide').removeClass('d-none');
            binPageValide = false;
        }
        if ($("#ddlProvince").val() == '') {
            $("#ddlProvince").removeClass("border-success").addClass("border-danger");
            $("#errProvince").text('Vous devez sélectionner une province').removeClass('d-none');
            binPageValide = false;
        }
        if ($("#tbCodePostal").val() == '' || !exprCodePostal.test($("#tbCodePostal").val())) {
            $("#tbCodePostal").removeClass("border-success").addClass("border-danger");
            if ($("#tbCodePostal").val() == '')
                $("#errCodePostal").text('Le code postal ne peut pas être vide').removeClass('d-none');
            else
                $("#errCodePostal").text('Le code postal n\'est pas dans un format valide').removeClass('d-none');
            binPageValide = false;
        }
        if ($("#tbTelephone1").val() == '' || !exprTelephone.test($("#tbTelephone1").val())) {
            $("#tbTelephone1").removeClass("border-success").addClass("border-danger");
            if ($("#tbTelephone1").val() == '')
                $("#errTelephone1").text('Le téléphone 1 ne peut pas être vide').removeClass('d-none');
            else
                $("#errTelephone1").text('Le téléphone 1 n\'est pas dans un format valide').removeClass('d-none');
            binPageValide = false;
        }
        if ($("#tbTelephone2").val() != '' && !exprTelephone.test($("#tbTelephone2").val())) {
            $("#tbTelephone2").removeClass("border-success").addClass("border-danger");
            $("#errTelephone2").text('Le téléphone 2 n\'est pas dans un format valide').removeClass('d-none');
            binPageValide = false;
        }
         return binPageValide;
      });
   });
</script>
</asp:Content>

