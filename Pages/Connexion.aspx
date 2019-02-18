<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Connexion.aspx.cs" Inherits="Pages_Connexion" %>

<!doctype html>
<html lang="fr">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>Ouvrir une session - Les Petites Puces</title>

    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.6.9/angular.min.js"></script> 
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.2.1/js/bootstrap.min.js"></script>
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

        .corps {
           display:block;
           width:100%;
           height:auto;
           padding:.375rem .75rem;
           font-size:1rem;
           font-weight:400;
           line-height:1.5;
           color:#495057;
           background-color:#e9ecef;
           opacity:1;
           background-clip:padding-box;
           border:1px solid #ced4da;
           border-radius:.25rem;
           transition:border-color .15s ease-in-out,box-shadow .15s ease-in-out;
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
        <asp:ScriptManager runat="server" />
        <asp:UpdatePanel runat="server">
        <ContentTemplate>
        <div class="row">
            <div class="message mx-auto">
                <asp:Panel ID="divMessage" runat="server">
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
                 <div id="divCorps" runat="server" class="corps" ></div>
              </asp:Panel>
              <br />
           </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
        <div class="row">
            <div class="form-signin col-md-6">

                <asp:Panel ID="alert_erreur" class="alert alert-danger" runat="server" Visible="false">
                  <asp:Label ID="lblMessageErreur" Text="" runat="server"></asp:Label>
                </asp:Panel>

                <h1 class="h3 mb-3 font-weight-normal">Veuillez entrer vos informations</h1>
                <asp:TextBox ID="tbCourriel" runat="server" CssClass="form-control" placeholder="Nom d'utilisateur"></asp:TextBox>
                <asp:TextBox ID="tbMDP" runat="server" CssClass="form-control" placeholder="Mot de passe" TextMode="Password"></asp:TextBox>
                <div class="checkbox mb-3">
                    <label>
                        <asp:CheckBox ID="cbSeSouvenir" runat="server" />
                        Se souvenir de moi
                    </label>
                </div>
                <asp:Button ID="btnConnexion" Text="Ouvrir une session" runat="server" CssClass="btn btn-lg btn-primary btn-block" BackColor="Orange" BorderColor="Orange" OnClick="btnConnexion_click" />
                <button type="button" class="btn btn-link" style="color: orange;" data-toggle="modal" data-target="#modalMotDePasseOublie">Mot de passe oublié?</button>
                <div class="modal" id="modalMotDePasseOublie">
                    <div class="modal-dialog">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title">Mot de passe oublié</h5>
                                    </div>
                                    <div class="modal-body">
                                        <asp:TextBox ID="tbCourrielMotDePasseOublie" runat="server" CssClass="form-control" placeholder="Identifiant/Courriel" MaxLength="100" />
                                        <asp:Label ID="errCourrielMotDePasseOublie" runat="server" CssClass="text-danger d-none" />
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-lg btn-secondary" data-dismiss="modal">Fermer</button>
                                        <asp:Button ID="btnEnvoyerMotDePasse" Text="Envoyer le mot de passe" runat="server" CssClass="btn btn-lg btn-primary" BackColor="Orange" BorderColor="Orange" OnClick="btnEnvoyerMotDePasse_Click" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <span class="barre-verticale-orange"></span>
            <div class="form-signin col-md-6">
                <asp:Button ID="btnInscriptionClient" runat="server" CssClass="btn btn-lg btn-primary btn-block" BackColor="Orange" BorderColor="Orange" Text="Inscription client" PostBackUrl="~/Pages/InscriptionClient.aspx" />
                <asp:Button ID="btnInscriptionVendeur" runat="server" CssClass="btn btn-lg btn-primary btn-block" BackColor="Orange" BorderColor="Orange" Text="Inscription vendeur" PostBackUrl="~/Pages/InscriptionVendeur.aspx" />
                <asp:Button ID="btnAcceuil" runat="server" CssClass="btn btn-lg btn-primary btn-block" BackColor="Orange" BorderColor="Orange" Text="Acceuil"  PostBackUrl="~/Pages/AccueilInternaute.aspx" />
            </div>
        </div>
        <script>
            $('#modalMotDePasseOublie').on('hidden.bs.modal', function () {
                $('#tbCourrielMotDePasseOublie').val('');
                $('#errCourrielMotDePasseOublie').val('').addClass('d-none');
            });
        </script>
    </form>
</body>
</html>
