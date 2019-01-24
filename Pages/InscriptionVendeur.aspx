<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InscriptionVendeur.aspx.cs" Inherits="Pages_InscriptionVendeur" %>

<!doctype html>
<html lang="fr">
<head>
   <meta charset="utf-8">
   <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
   <title>S'inscrire comme vendeur - Les Petites Puces</title>

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
        max-width: 400px;
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
            <div class="mb-3">
               <input type="text" class="form-control" id="tbNomEntreprise" placeholder="Nom de l'entreprise" required>
            </div>
            <div class="row">
               <div class="col-md-6 mb-3">
                  <input type="text" class="form-control" id="tbPrenom" placeholder="Prénom" value="" required autofocus>
               </div>
               <div class="col-md-6 mb-3">
                  <input type="text" class="form-control" id="tbNom" placeholder="Nom" value="" required>
               </div>
            </div>
            <div class="mb-3">
               <input type="text" class="form-control" id="tbAdresse" placeholder="Adresse" required>
            </div>
            <div class="mb-3">
               <input type="text" class="form-control" id="tbVille" placeholder="Ville">
            </div>
            <div class="mb-3">  
               <asp:DropDownList Id="ddlProvince" CssClass="form-control" runat="server">
                  <asp:ListItem Value="">Sélectionnez la province</asp:ListItem>
                  <asp:ListItem Value="QC"> Québec </asp:ListItem>
                  <asp:ListItem Value="ON"> Ontario </asp:ListItem>
                  <asp:ListItem Value="NB"> Nouveau-Brunswick </asp:ListItem>
               </asp:DropDownList>
            </div>
            <div class="mb-3">
               <input type="text" class="form-control" id="tbCodePostal" placeholder="Code Postal" required>
            </div>
            <div class="mb-3">
               <select class="form-control" disabled>
                  <option value="">Canada</option>
               </select>
            </div>
            <div class="mb-3">
               <input type="tel" id="tbTelephone1" class="form-control" style="margin-bottom: -1px;" placeholder="Téléphone 1" required>
               <input type="tel" id="tbTelephone2" class="form-control" style="margin-bottom: 10px;" placeholder="Téléphone 2 (facultatif)" required>
            </div>
            <div class="mb-3">
               <input type="email" id="tbCourriel" class="form-control" style="margin-bottom: -1px;" placeholder="Courriel" required>
               <input type="email" id="tbConfimationCourriel" class="form-control" style="margin-bottom: 10px;" placeholder="Confimation courriel" required>
            </div>                   
            <div class="mb-3">
               <input type="password" id="tbMotDePasse" class="form-control" style="margin-bottom: -1px;" placeholder="Mot de passe" required>
               <input type="password" id="tbConfimationMotDePasse" class="form-control" style="margin-bottom: 10px;" placeholder="Confimation mot de passe" required>
            </div>
            <div class="input-group mb-3">
               <input type="number" class="form-control" id="tbPoidsMaxLivraison" placeholder="Poids de livraison maximum" required>
               <div class="input-group-append">
                  <span class="input-group-text">lbs</span>
               </div>
            </div>
            <div class="input-group mb-3">
               <input type="number" step="0.01" class="form-control" id="tbLivraisonGratuite" placeholder="Montant pour avoir la livraison gratuite" required>
               <div class="input-group-append">
                  <span class="input-group-text">$</span>
               </div>
            </div>
            <div class="checkbox mb-3">
               <label>
                  Exemption taxes <input type="checkbox" value="cbTaxes">
               </label>
            </div>
            <button class="btn btn-lg btn-primary btn-block" style="background-color: orange;border-color: orange" type="submit">S'inscrire</button>  
         </div>
         <span style="border-left: 2px solid orange"></span>
         <div class="form-group col-md-6">
            <button class="btn btn-lg btn-primary btn-block" style="background-color: orange;border-color: orange">Inscription client</button>
            <button class="btn btn-lg btn-primary btn-block" style="background-color: orange;border-color: orange">Mot de passe oublié?</button>
            <button class="btn btn-lg btn-primary btn-block" style="background-color: orange;border-color: orange">Modification mot de passe</button>
         </div>
      </div>
   </form>
</body>
</html>
