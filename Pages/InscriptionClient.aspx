<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InscriptionClient.aspx.cs" Inherits="Pages_InscriptionClient" %>

<!doctype html>
<html lang="fr">
<head>
   <meta charset="utf-8">
   <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
   <title>S'inscrire comme client - Les Petites Puces</title>

   <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.2.1/css/bootstrap.min.css" integrity="sha384-GJzZqFGwb1QTTN6wy59ffF1BuGJpLSa9DkKMp0DgiMDm4iYMj70gZWKYbI706tWS" crossorigin="anonymous">
   <script src="https://code.jquery.com/jquery-3.3.1.min.js" integrity="sha256-FgpCb/KJQlLNfOu91ta32o/NMZxltwRo8QtmkMRdAu8=" crossorigin="anonymous"></script>

   <style>
      .bd-placeholder-img {
        font-size: 1.125rem;
        text-anchor: middle;
      }

      @media (min-width: 768px) {
        .bd-placeholder-img-lg {
          font-size: 3.5rem;
        }
      }

      body {
        display: -ms-flexbox;
        display: flex;
        -ms-flex-align: center;
        align-items: center;
        padding-top: 40px;
        padding-bottom: 40px;
        background-color: #f5f5f5;
      }

      .form-group {
        width: 100%;
        max-width: 350px;
        padding: 15px;
        margin: auto;
      }

      .form-group .form-control {
        position: relative;
        box-sizing: border-box;
        height: auto;
        padding: 10px;
        font-size: 16px;
      }
   </style>
</head>
<body class="text-center">
   <form class="container-fluid" runat="server">
      <div class="row">
         <div class="col-md-12">
            <img class="mb-4" src="/static/images/logo.png" alt="" width="150" height="150">
         </div>
      </div>
      <div class="row">
         <div class="form-group col-md-6">
           <h1 class="h3 mb-3 font-weight-normal">Veuillez entrer vos informations</h1>
           <asp:TextBox ID="tbCourriel" runat="server" CssClass="form-control" placeholder="Courriel" />
           <asp:Label ID="errCourriel" runat="server" CssClass="text-danger" style="display: none;" />
           <asp:TextBox ID="tbConfirmationCourriel" runat="server" CssClass="form-control" placeholder="Confimation courriel" />
           <asp:Label ID="errConfirmationCourriel" runat="server" CssClass="text-danger" style="display: none;" />
           <asp:TextBox ID="tbMotPasse" runat="server" TextMode="Password" CssClass="form-control" style="margin-top: 10px;" placeholder="Mot de passe" />
            <asp:Label ID="errMotPasse" runat="server" CssClass="text-danger" style="display: none;" />
           <asp:TextBox ID="tbConfirmationMotPasse" runat="server" TextMode="Password" CssClass="form-control" placeholder="Confimation mot de passe" />
            <asp:Label ID="errConfirmationMotPasse" runat="server" CssClass="text-danger" style="display: none;" />
           <asp:Button ID="btnInscription" runat="server"  CssClass="btn btn-lg btn-primary btn-block" style="margin-top: 10px;" BackColor="Orange" BorderColor="Orange" Text="S'inscrire" OnClick="btnInscription_Click" />
         </div>
         <span style="border-left: 2px solid orange"></span>
         <div class="form-group col-md-6">
            <asp:Button ID="btnInscriptionVendeur" runat="server" CssClass="btn btn-lg btn-primary btn-block" BackColor="Orange" BorderColor="Orange" Text="Inscription vendeur" OnClick="btnInscriptionVendeur_Click" />
            <asp:Button ID="btnMotDePasseOublie" runat="server" CssClass="btn btn-lg btn-primary btn-block" BackColor="Orange" BorderColor="Orange" Text="Mot de passe oublié?" OnClick="btnMotDePasseOublie_Click" />
            <asp:Button ID="btnAcceuil" runat="server" CssClass="btn btn-lg btn-primary btn-block" BackColor="Orange" BorderColor="Orange" Text="Acceuil" OnClick="btnAcceuil_Click" />
         </div>
      </div>
      <script>
         $(document).ready(function () {
            var exprCourriel = /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
            var exprMotPasse = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.{8,})/;
            $("#tbCourriel").focusout(function () {
               if ($("#tbCourriel").val() == '') {
                  $("#tbCourriel").removeClass("border-success").addClass("border-danger");
                  $("#errCourriel").text('Le courriel ne peut pas être vide').show();
               } else if (!exprCourriel.test($("#tbCourriel").val())) {
                  $("#tbCourriel").removeClass("border-success").addClass("border-danger");
                  $("#errCourriel").text('Le courriel n\'est pas dans un format valide').show();
               } else {
                  $("#tbCourriel").removeClass("border-danger").addClass("border-success");
                  $("#errCourriel").text('').hide();
                  if ($("#tbConfirmationCourriel").val() != '' && exprCourriel.test($("#tbConfirmationCourriel").val())) {
                     if ($("#tbConfirmationCourriel").val() != $("#tbCourriel").val()) {
                        $("#tbConfirmationCourriel").removeClass("border-success").addClass("border-danger");
                        $("#errConfirmationCourriel").text('La confirmation du courriel ne correspond pas au courriel').show();
                     } else {
                        $("#tbConfirmationCourriel").removeClass("border-danger").addClass("border-success");
                        $("#errConfirmationCourriel").text('').hide();
                     }
                  }
               }
            });
            $("#tbConfirmationCourriel").focusout(function () {
               if ($("#tbConfirmationCourriel").val() == '') {
                  $("#tbConfirmationCourriel").removeClass("border-success").addClass("border-danger");
                  $("#errConfirmationCourriel").text('La confirmation du courriel ne peut pas être vide').show();
               } else if (!exprCourriel.test($("#tbConfirmationCourriel").val())) {
                  $("#tbConfirmationCourriel").removeClass("border-success").addClass("border-danger");
                  $("#errConfirmationCourriel").text('La confirmation du courriel n\'est pas dans un format valide').show();
               } else if ($("#tbConfirmationCourriel").val() != $("#tbCourriel").val()) {
                  $("#tbConfirmationCourriel").removeClass("border-success").addClass("border-danger");
                  $("#errConfirmationCourriel").text('La confirmation du courriel ne correspond pas au courriel').show();
               } else {
                  $("#tbConfirmationCourriel").removeClass("border-danger").addClass("border-success");
                  $("#errConfirmationCourriel").text('').hide();
               }
            });
            $("#tbMotPasse").focusout(function () {
               if ($("#tbMotPasse").val() == '') {
                  $("#tbMotPasse").removeClass("border-success").addClass("border-danger");
                  $("#errMotPasse").text('Le mot de passe ne peut pas être vide').show();
               } else if (!exprMotPasse.test($("#tbMotPasse").val())) {
                  $("#tbMotPasse").removeClass("border-success").addClass("border-danger");
                  $("#errMotPasse").text('Le mot de passe doit contenir au moins 8 charactères dont une lettre minuscule, une lettre majuscule et un chiffre').show();
               } else {
                  $("#tbMotPasse").removeClass("border-danger").addClass("border-success");
                  $("#errMotPasse").text('').hide();
                  if ($("#tbConfirmationMotPasse").val() != '') {
                     if ($("#tbConfirmationMotPasse").val() != $("#tbMotPasse").val()) {
                        $("#tbConfirmationMotPasse").removeClass("border-success").addClass("border-danger");
                        $("#errConfirmationMotPasse").text('La confirmation du mot de passe ne correspond pas au mot de passe').show();
                     } else {
                        $("#tbConfirmationMotPasse").removeClass("border-danger").addClass("border-success");
                        $("#errConfirmationMotPasse").text('').hide();
                     }
                  }
               }
            });
            $("#tbConfirmationMotPasse").focusout(function () {
               if ($("#tbConfirmationMotPasse").val() == '') {
                  $("#tbConfirmationMotPasse").removeClass("border-success").addClass("border-danger");
                  $("#errConfirmationMotPasse").text('La confirmation du mot de passe ne peut pas être vide').show();
               } else if ($("#tbConfirmationMotPasse").val() != $("#tbMotPasse").val()) {
                  $("#tbConfirmationMotPasse").removeClass("border-success").addClass("border-danger");
                  $("#errConfirmationMotPasse").text('La confirmation du mot de passe ne correspond pas au mot de passe').show();
               } else {
                  $("#tbConfirmationMotPasse").removeClass("border-danger").addClass("border-success");
                  $("#errConfirmationMotPasse").text('').hide();
               }
            });
            $("#btnInscription").click(function () {
               var binPageValide = true;
               if ($("#tbCourriel").val() == '' || !exprCourriel.test($("#tbCourriel").val())) {
                  $("#tbCourriel").removeClass("border-success").addClass("border-danger");
                  if ($("#tbCourriel").val() == '')
                     $("#errCourriel").text('Le courriel ne peut pas être vide').show();
                  else
                     $("#errCourriel").text('Le courriel n\'est pas dans un format valide').show();
                  binPageValide = false;
               }
               if ($("#tbConfirmationCourriel").val() == '' || !exprCourriel.test($("#tbConfirmationCourriel").val()) || $("#tbConfirmationCourriel").val() != $("#tbCourriel").val()) {
                  $("#tbConfirmationCourriel").removeClass("border-success").addClass("border-danger");
                  if ($("#tbConfirmationCourriel").val() == '')
                     $("#errConfirmationCourriel").text('La confirmation du courriel ne peut pas être vide').show();
                  else if (!exprCourriel.test($("#tbConfirmationCourriel").val()))
                     $("#errConfirmationCourriel").text('La confirmation du courriel n\'est pas dans un format valide').show();
                  else
                     $("#errConfirmationCourriel").text('La confirmation du courriel ne correspond pas au courriel').show();                                   
                  binPageValide = false;
               }
               if ($("#tbMotPasse").val() == '' || !exprMotPasse.test($("#tbMotPasse").val())) {
                  $("#tbMotPasse").removeClass("border-success").addClass("border-danger");
                  if ($("#tbMotPasse").val() == '')
                     $("#errMotPasse").text('Le mot de passe ne peut pas être vide').show();
                  else
                     $("#errMotPasse").text('Le mot de passe doit contenir au moins 8 charactères dont une lettre minuscule, une lettre majuscule et un chiffre').show();
                  binPageValide = false;
               }
               if ($("#tbConfirmationMotPasse").val() == '' || $("#tbConfirmationMotPasse").val() != $("#tbMotPasse").val()) {
                  $("#tbConfirmationMotPasse").removeClass("border-success").addClass("border-danger");
                  if ($("#tbConfirmationMotPasse").val() == '')
                     $("#errConfirmationMotPasse").text('La confirmation du mot de passe ne peut pas être vide').show();
                  else
                     $("#errConfirmationMotPasse").text('La confirmation du mot de passe ne correspond pas au mot de passe').show();
                  binPageValide = false;
               }
               return binPageValide;
            });
         });
      </script>
   </form>
</body>
</html>