﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InscriptionClient.aspx.cs" Inherits="Pages_InscriptionClient" %>

<!doctype html>
<html lang="fr">
<head>
   <meta charset="utf-8">
   <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
   <title>Inscription client - Les Petites Puces</title>

   <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.2.1/css/bootstrap.min.css" integrity="sha384-GJzZqFGwb1QTTN6wy59ffF1BuGJpLSa9DkKMp0DgiMDm4iYMj70gZWKYbI706tWS" crossorigin="anonymous">
   <link rel="shortcut icon" type="image/x-icon" href="/static/images/logo.png" />
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

      .message {
         width: 100%;
         padding: 15px;
         margin: auto;
      }
     
      @media (max-width: 700px) {
         .message {
            max-width: 350px;
         }
      }

      @media (min-width: 701px) {
         .message {
            max-width: 700px;
         }
      }

      .barre-verticale-orange {
        border-left: 1px solid orange;
      }

      @media (max-width: 700px) {
         .barre-verticale-orange {
            display: none;
         }
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
         <div class="message mx-auto">
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
      <div class="row">
         <div class="form-group col-md-6">           
            <h1 class="h3 mb-3 font-weight-normal">Veuillez entrer vos informations</h1>
            <div class="mb-3">
               <asp:TextBox ID="tbCourriel" runat="server" CssClass="form-control" placeholder="Courriel" MaxLength="100" />
               <asp:Label ID="errCourriel" runat="server" CssClass="text-danger d-none" />
               <asp:TextBox ID="tbConfirmationCourriel" runat="server" CssClass="form-control" placeholder="Confimation courriel" MaxLength="100" />
               <asp:Label ID="errConfirmationCourriel" runat="server" CssClass="text-danger d-none" />
            </div>
            <div class="mb-3">
               <asp:TextBox ID="tbMotPasse" runat="server" TextMode="Password" CssClass="form-control" placeholder="Mot de passe" MaxLength="50" />
               <asp:Label ID="errMotPasse" runat="server" CssClass="text-danger d-none" />
               <asp:TextBox ID="tbConfirmationMotPasse" runat="server" TextMode="Password" CssClass="form-control" placeholder="Confimation mot de passe" MaxLength="50" />
               <asp:Label ID="errConfirmationMotPasse" runat="server" CssClass="text-danger d-none" />
            </div>
            <asp:Button ID="btnInscription" runat="server"  CssClass="btn btn-lg btn-primary btn-block" style="margin-top: 10px;" BackColor="Orange" BorderColor="Orange" Text="S'inscrire" OnClick="btnInscription_Click" />
         </div>
         <span class="barre-verticale-orange"></span>
         <div class="form-group col-md-6">
            <asp:Button ID="btnInscriptionVendeur" runat="server" CssClass="btn btn-lg btn-primary btn-block" BackColor="Orange" BorderColor="Orange" Text="Inscription vendeur" PostBackUrl="~/Pages/InscriptionVendeur.aspx" />
            <asp:Button ID="btnOuvrirSession" runat="server" CssClass="btn btn-lg btn-primary btn-block" BackColor="Orange" BorderColor="Orange" Text="Ouvrir une session"  PostBackUrl="~/Pages/Connexion.aspx" />
            <asp:Button ID="btnAcceuil" runat="server" CssClass="btn btn-lg btn-primary btn-block" BackColor="Orange" BorderColor="Orange" Text="Acceuil"  PostBackUrl="~/Pages/AccueilInternaute.aspx" />
         </div>
      </div>
      <script>
         $(document).ready(function () {
            var exprCourriel = /^[a-zA-Z0-9]+([-._][a-zA-Z0-9]+)*@[a-zA-Z0-9]+([-._][a-zA-Z0-9]+)*\.[a-z]+$/;
            var exprMotPasse = /(?=^[a-zA-Z0-9]*[a-z])(?=^[a-zA-Z0-9]*[A-Z])(?=^[a-zA-Z0-9]*[0-9])(?=^[a-zA-Z0-9]{8,}$)/;
            $('#tbCourriel').focusout(function () {
               if ($('#tbCourriel').val() == '') {
                  $('#tbCourriel').removeClass('border-success').addClass('border-danger');
                  $('#errCourriel').text('Le courriel ne peut pas être vide').removeClass('d-none');
               } else if (!exprCourriel.test($('#tbCourriel').val())) {
                  $('#tbCourriel').removeClass('border-success').addClass('border-danger');
                  $('#errCourriel').text('Le courriel n\'est pas valide').removeClass('d-none');
               } else {
                  $('#tbCourriel').removeClass('border-danger').addClass('border-success');
                  $('#errCourriel').text('').addClass('d-none');
                  if ($('#tbConfirmationCourriel').val() != '' && exprCourriel.test($('#tbConfirmationCourriel').val())) {
                     if ($('#tbConfirmationCourriel').val() != $('#tbCourriel').val()) {
                        $('#tbConfirmationCourriel').removeClass('border-success').addClass('border-danger');
                        $('#errConfirmationCourriel').text('La confirmation du courriel ne correspond pas au courriel').removeClass('d-none');
                     } else {
                        $('#tbConfirmationCourriel').removeClass('border-danger').addClass('border-success');
                        $('#errConfirmationCourriel').text('').addClass('d-none');
                     }
                  }
               }
            });
            $('#tbConfirmationCourriel').focusout(function () {
               if ($('#tbConfirmationCourriel').val() == '') {
                  $('#tbConfirmationCourriel').removeClass('border-success').addClass('border-danger');
                  $('#errConfirmationCourriel').text('La confirmation du courriel ne peut pas être vide').removeClass('d-none');
               } else if (!exprCourriel.test($('#tbConfirmationCourriel').val())) {
                  $('#tbConfirmationCourriel').removeClass('border-success').addClass('border-danger');
                  $('#errConfirmationCourriel').text('La confirmation du courriel n\'est pas valide').removeClass('d-none');
               } else if ($('#tbConfirmationCourriel').val() != $('#tbCourriel').val()) {
                  $('#tbConfirmationCourriel').removeClass('border-success').addClass('border-danger');
                  $('#errConfirmationCourriel').text('La confirmation du courriel ne correspond pas au courriel').removeClass('d-none');
               } else {
                  $('#tbConfirmationCourriel').removeClass('border-danger').addClass('border-success');
                  $('#errConfirmationCourriel').text('').addClass('d-none');
               }
            });
            $('#tbMotPasse').focusout(function () {
               if ($('#tbMotPasse').val() == '') {
                  $('#tbMotPasse').removeClass('border-success').addClass('border-danger');
                  $('#errMotPasse').text('Le mot de passe ne peut pas être vide').removeClass('d-none');
               } else if (!exprMotPasse.test($('#tbMotPasse').val())) {
                  $('#tbMotPasse').removeClass('border-success').addClass('border-danger');
                  $('#errMotPasse').text('Le mot de passe doit contenir au moins 8 charactères dont une lettre minuscule, une lettre majuscule et un chiffre').removeClass('d-none');
               } else {
                  $('#tbMotPasse').removeClass('border-danger').addClass('border-success');
                  $('#errMotPasse').text('').addClass('d-none');
                  if ($('#tbConfirmationMotPasse').val() != '') {
                     if ($('#tbConfirmationMotPasse').val() != $('#tbMotPasse').val()) {
                        $('#tbConfirmationMotPasse').removeClass('border-success').addClass('border-danger');
                        $('#errConfirmationMotPasse').text('La confirmation du mot de passe ne correspond pas au mot de passe').removeClass('d-none');
                     } else {
                        $('#tbConfirmationMotPasse').removeClass('border-danger').addClass('border-success');
                        $('#errConfirmationMotPasse').text('').addClass('d-none');
                     }
                  }
               }
            });
            $('#tbConfirmationMotPasse').focusout(function () {
               if ($('#tbConfirmationMotPasse').val() == '') {
                  $('#tbConfirmationMotPasse').removeClass('border-success').addClass('border-danger');
                  $('#errConfirmationMotPasse').text('La confirmation du mot de passe ne peut pas être vide').removeClass('d-none');
               } else if ($('#tbConfirmationMotPasse').val() != $('#tbMotPasse').val()) {
                  $('#tbConfirmationMotPasse').removeClass('border-success').addClass('border-danger');
                  $('#errConfirmationMotPasse').text('La confirmation du mot de passe ne correspond pas au mot de passe').removeClass('d-none');
               } else {
                  $('#tbConfirmationMotPasse').removeClass('border-danger').addClass('border-success');
                  $('#errConfirmationMotPasse').text('').addClass('d-none');
               }
            });
            $('#btnInscription').click(function () {
               var binPageValide = true;
               if ($('#tbCourriel').val() == '' || !exprCourriel.test($('#tbCourriel').val())) {
                  $('#tbCourriel').removeClass('border-success').addClass('border-danger');
                  if ($('#tbCourriel').val() == '')
                     $('#errCourriel').text('Le courriel ne peut pas être vide').removeClass('d-none');
                  else
                     $('#errCourriel').text('Le courriel n\'est pas valide').removeClass('d-none');
                  binPageValide = false;
               }
               if ($('#tbConfirmationCourriel').val() == '' || !exprCourriel.test($('#tbConfirmationCourriel').val()) || $('#tbConfirmationCourriel').val() != $('#tbCourriel').val()) {
                  $('#tbConfirmationCourriel').removeClass('border-success').addClass('border-danger');
                  if ($('#tbConfirmationCourriel').val() == '')
                     $('#errConfirmationCourriel').text('La confirmation du courriel ne peut pas être vide').removeClass('d-none');
                  else if (!exprCourriel.test($('#tbConfirmationCourriel').val()))
                     $('#errConfirmationCourriel').text('La confirmation du courriel n\'est pas valide').removeClass('d-none');
                  else
                     $('#errConfirmationCourriel').text('La confirmation du courriel ne correspond pas au courriel').removeClass('d-none');                                   
                  binPageValide = false;
               }
               if ($('#tbMotPasse').val() == '' || !exprMotPasse.test($('#tbMotPasse').val())) {
                  $('#tbMotPasse').removeClass('border-success').addClass('border-danger');
                  if ($('#tbMotPasse').val() == '')
                     $('#errMotPasse').text('Le mot de passe ne peut pas être vide').removeClass('d-none');
                  else
                     $('#errMotPasse').text('Le mot de passe doit contenir au moins 8 charactères dont une lettre minuscule, une lettre majuscule et un chiffre').removeClass('d-none');
                  binPageValide = false;
               }
               if ($('#tbConfirmationMotPasse').val() == '' || $('#tbConfirmationMotPasse').val() != $('#tbMotPasse').val()) {
                  $('#tbConfirmationMotPasse').removeClass('border-success').addClass('border-danger');
                  if ($('#tbConfirmationMotPasse').val() == '')
                     $('#errConfirmationMotPasse').text('La confirmation du mot de passe ne peut pas être vide').removeClass('d-none');
                  else
                     $('#errConfirmationMotPasse').text('La confirmation du mot de passe ne correspond pas au mot de passe').removeClass('d-none');
                  binPageValide = false;
               }
               return binPageValide;
            });
         });
      </script>
   </form>
</body>
</html>