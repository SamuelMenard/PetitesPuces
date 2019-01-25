<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Connexion.aspx.cs" Inherits="Pages_Connexion" %>

<!doctype html>
<html lang="fr">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>Ouvrir une session - Les Petites Puces</title>

    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.6.9/angular.min.js"></script> 
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
    <form class="container-fluid" runat="server">
        <div class="row">
            <div class="col-md-12">
                <img class="mb-4" src="/static/images/logo.png" alt="" width="150" height="150">
            </div>
        </div>
        <div class="row">
            <div class="form-signin col-md-6">
                <h1 class="h3 mb-3 font-weight-normal">Veuillez entrer vos informations</h1>
                <asp:TextBox ID="tbCourriel" runat="server" CssClass="form-control" placeholder="Nom d'utilisateur"></asp:TextBox>
                <asp:TextBox ID="tbMDP" runat="server" CssClass="form-control" placeholder="Mot de passe" TextMode="Password"></asp:TextBox>
                <div class="checkbox mb-3">
                    <label>
                        <input type="checkbox" value="cbSeSouvenir">
                        Se souvenir de moi
                    </label>
                </div>
                <asp:DropDownList ID="ddlTypeUtilisateur" CssClass="form-control"
                    runat="server">
                    <asp:ListItem Selected="True" Value="C"> Client </asp:ListItem>
                    <asp:ListItem Value="V"> Vendeur </asp:ListItem>
                    <asp:ListItem Value="G"> Gestionnaire </asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:Button ID="btnConnexion" Text="Ouvrir une session" runat="server" CssClass="btn btn-lg btn-primary btn-block" BackColor="Orange" BorderColor="Orange" OnClick="connexion_click" />
                <a style="color: orange;" href="#">Mot de passe oublié?</a>
            </div>
            <span style="border-left: 2px solid orange"></span>
            <div class="form-signin col-md-6">
                <asp:Button ID="btnInscriptionClient" Text="Inscription Client" runat="server" CssClass="btn btn-lg btn-primary btn-block" BackColor="Orange" BorderColor="Orange" OnClick="inscriptionClient_click" />
                <asp:Button ID="btnInscriptionVendeur" Text="Inscription Vendeur" runat="server" CssClass="btn btn-lg btn-primary btn-block" BackColor="Orange" BorderColor="Orange" OnClick="inscriptionVendeur_click" />
                <asp:Button ID="btnAccueil" Text="Accueil" runat="server" CssClass="btn btn-lg btn-primary btn-block" BackColor="Orange" BorderColor="Orange" OnClick="accueil_click" />
            </div>
        </div>
    </form>
</body>
</html>
