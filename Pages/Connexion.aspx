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

        .barre-verticale-orange {
            border-left: 1px solid orange;
        }

        @media (max-width: 700px) {
            .barre-verticale-orange {
               display: none;
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
                <asp:Panel ID="alert_erreur" class="alert alert-danger" runat="server" Visible="false">
                  <strong>Le nom d'utilisateur ou le mot de passe est incorrect</strong>
                </asp:Panel>
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
                <asp:Button ID="btnConnexion" Text="Ouvrir une session" runat="server" CssClass="btn btn-lg btn-primary btn-block" BackColor="Orange" BorderColor="Orange" OnClick="btnConnexion_click" />
                <a style="color: orange;" href="#">Mot de passe oublié?</a>
            </div>
            <span class="barre-verticale-orange"></span>
            <div class="form-signin col-md-6">
                <asp:Button ID="btnInscriptionClient" runat="server" CssClass="btn btn-lg btn-primary btn-block" BackColor="Orange" BorderColor="Orange" Text="Inscription client" PostBackUrl="~/Pages/InscriptionClient.aspx" />
                <asp:Button ID="btnInscriptionVendeur" runat="server" CssClass="btn btn-lg btn-primary btn-block" BackColor="Orange" BorderColor="Orange" Text="Inscription vendeur" PostBackUrl="~/Pages/InscriptionVendeur.aspx" />
                <asp:Button ID="btnAcceuil" runat="server" CssClass="btn btn-lg btn-primary btn-block" BackColor="Orange" BorderColor="Orange" Text="Acceuil"  PostBackUrl="~/Pages/AccueilInternaute.aspx" />
            </div>
        </div>
    </form>
</body>
</html>
