<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InscriptionClient.aspx.cs" Inherits="Pages_InscriptionClient" %>

<!doctype html>
<html lang="fr">
<head>
   <meta charset="utf-8">
   <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
   <title>S'inscrire comme client - Les Petites Puces</title>

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
   <div class="container-fluid">
      <div class="row">
         <div class="col-md-12">
            <img class="mb-4" src="/static/images/logo.png" alt="" width="150" height="150">
         </div>
      </div>
      <div class="row">
         <form class="form-group col-md-6">
           <h1 class="h3 mb-3 font-weight-normal">Veuillez entrer vos informations</h1>
           <input type="email" id="tbCourriel" class="form-control" style="margin-bottom: -1px;" placeholder="Courriel" required autofocus>
           <input type="email" id="tbConfimationCourriel" class="form-control" style="margin-bottom: 10px;" placeholder="Confimation courriel" required>
           <input type="password" id="tbMotDePasse" class="form-control" style="margin-bottom: -1px;" placeholder="Mot de passe" required>
           <input type="password" id="tbConfimationMotDePasse" class="form-control" style="margin-bottom: 10px;" placeholder="Confimation mot de passe" required>
           <button class="btn btn-lg btn-primary btn-block" type="submit">S'inscrire</button>  
         </form>
         <span class="border"></span>
         <div class="form-group col-md-6">
            <button class="btn btn-lg btn-primary btn-block">Inscription vendeur</button>
            <button class="btn btn-lg btn-primary btn-block">Mot de passe oublié?</button>
            <button class="btn btn-lg btn-primary btn-block">Modification mot de passe</button>
         </div>
      </div>
   </div>
</body>
</html>