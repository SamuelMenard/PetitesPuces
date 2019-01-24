<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Connexion.aspx.cs" Inherits="Pages_Connexion" %>

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
   <div class="container">
      <div class="row">
         <div class="col-md-12">
            <img class="mb-4" src="/static/images/logo.png" alt="" width="150" height="150">
         </div>
      </div>
       <form  runat="server">
              <div class="row">
                     <div class="col-md-5">
                        <h1 class="h3 mb-3 font-weight-normal">Veuillez entrer vos informations</h1>
                         <asp:TextBox ID="tbCourriel" runat="server" CssClass="form-control" placeholder="Nom d'utilisateur"></asp:TextBox>
                         <asp:TextBox ID="tbMDP" runat="server" CssClass="form-control" placeholder="Mot de passe" TextMode="Password"></asp:TextBox>
                        <div class="checkbox mb-3">
                            <label>
                                <input type="checkbox" value="cbSeSouvenir"> Se souvenir de moi
                            </label>
                        </div>
                         <asp:Button ID="btnConnexion" Text="Ouvrir une session" runat="server" CssClass="btn btn-lg btn-primary btn-block" OnClick="connexion_click" />
                        <a href="#">Mot de passe oublié?</a>
                      </div>
                         <div class="col-md-2">
                             <span class="border"></span>
                         </div>
                        <div class="col-md-5">
                            <asp:Button ID="btnInscriptionClient" Text="Inscription Client" runat="server" CssClass="btn btn-lg btn-primary btn-block" OnClick="inscriptionClient_click" />
                            <asp:Button ID="btnInscriptionVendeur" Text="Inscription Vendeur" runat="server" CssClass="btn btn-lg btn-primary btn-block" OnClick="inscriptionVendeur_click" />
                        </div>
              </div>
        </form>
   </div>
</body>
</html>