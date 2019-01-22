<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Connexion.aspx.cs" Inherits="Pages_Connexion" %>

<!doctype html>
<html lang="en">
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
    <form class="form-signin">
  <img class="mb-4" src="/static/images/logo.png" alt="" width="150" height="150">
  <h1 class="h3 mb-3 font-weight-normal">Veuillez entrer vos informations</h1>
  <label for="inputEmail" class="sr-only">Courriel</label>
  <input type="email" id="inputEmail" class="form-control" placeholder="Courriel" required autofocus>
  <label for="inputPassword" class="sr-only">Mot de passe</label>
  <input type="password" id="inputPassword" class="form-control" placeholder="Mot de passe" required>
  <div class="checkbox mb-3">
    <label>
      <input type="checkbox" value="remember-me"> Se souvenir de moi
    </label>
  </div>
  <button class="btn btn-lg btn-primary btn-block" type="submit">Ouvrir une session</button>
</form>
</body>
</html>
