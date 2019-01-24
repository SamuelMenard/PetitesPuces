﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Connexion.aspx.cs" Inherits="Pages_Connexion" %>

<!doctype html>
<html lang="fr">
<head>
   <meta charset="utf-8">
   <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
   <title>Ouvrir une session - Les Petites Puces</title>

   <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.2.1/css/bootstrap.min.css" integrity="sha384-GJzZqFGwb1QTTN6wy59ffF1BuGJpLSa9DkKMp0DgiMDm4iYMj70gZWKYbI706tWS" crossorigin="anonymous">

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
   </style>
   <link href="/static/style/signin.css" rel="stylesheet">
</head>
<body class="text-center">
   <div class="container-fluid">
      <div class="row">
         <div class="col-md-12">
            <img class="mb-4" src="/static/images/logo.png" alt="" width="150" height="150">
         </div>
      </div>
      <div class="row">
         <form class="form-signin col-md-6">
           <h1 class="h3 mb-3 font-weight-normal">Veuillez entrer vos informations</h1>
           <input type="email" id="tbCourriel" class="form-control" placeholder="Courriel" required autofocus>
           <input type="password" id="tbMotDePasse" class="form-control" placeholder="Mot de passe" required>
            <div class="checkbox mb-3">
               <label>
                  <input type="checkbox" value="cbSeSouvenir"> Se souvenir de moi
               </label>
            </div>
            <button class="btn btn-lg btn-primary btn-block" type="submit">Ouvrir une session</button>
            <a href="#">Mot de passe oublié?</a>
         </form>
         <span class="border"></span>
         <div class="form-signin col-md-6">
            <button class="btn btn-lg btn-primary btn-block">Inscription client</button>
            <button class="btn btn-lg btn-primary btn-block">Inscription vendeur</button>
            <button class="btn btn-lg btn-primary btn-block">Modification mot de passe</button>
         </div>
      </div>
   </div>
</body>
</html>