﻿<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="SaisieCommande.aspx.cs" Inherits="Pages_SaisieCommande" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
    <link rel="stylesheet" href="../static/style/panier.css">
    <script src="../static/js/paiement.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Hidden variables -->
    <asp:HiddenField ID="hNoVendeur" runat="server" />
    <asp:HiddenField ID="hNomVendeur" runat="server" />
    <asp:HiddenField ID="hNoCarteCredit" runat="server" />
    <asp:HiddenField ID="hDateExpirationCarteCredit" runat="server" />
    <asp:HiddenField ID="hNoSecuriteCarteCredit" runat="server" />
    <asp:HiddenField ID="hNoCommande" runat="server" />


    <!-- Contenu de la page -->
    <div class="container">
     <div id="panier">
        <div class="progress" id="divProgressBar" runat="server">
            <div id="progressBar" class="progress-bar progress-bar-warning progress-bar-striped active" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width:25%" runat="server">
              <asp:Label ID="lblEtapeLivraison" runat="server"></asp:Label>
            </div>
            <span class="badge" style="background-color:white !important; z-index:10 !important; position:relative; right:10px;">
                    <span class="glyphicon glyphicon-shopping-cart" style="color:orange !important"></span>
                </span>
          </div>
        <asp:PlaceHolder id="paniersDynamique" runat="server" />
    </div>           
    


    <asp:Panel ID="divInfosPerso" CssClass="row" Visible="false" runat="server">
        <div class="col-md-12 order-md-1">
          <h4 class="mb-3">Informations de livraison</h4>
            <div class="row">
              <div class="col-md-6 mb-3">
                <label for="prenom">Prénom</label>
                  <asp:TextBox ID="prenom" MaxLength="15" Text="" CssClass="form-control" runat="server"/>
              </div>
              <div class="col-md-6 mb-3">
                <label for="nomfamille">Nom de famille</label>
                  <asp:TextBox ID="nomfamille" MaxLength="15" Text="" CssClass="form-control" runat="server"/>
              </div>
            </div>

            <div class="mb-3">
              <label for="email">Courriel</label>
                <asp:TextBox ID="email" Text="" CssClass="form-control" Enabled="false" runat="server"/>
            </div>

            <div class="row">
                <div class="col-sm-4 mb-3">
                    <label for="adresse">Adresse</label>
                    <asp:TextBox ID="adresse" Text="" CssClass="form-control" runat="server"/>
                </div>
               <div class="col-sm-5 mb-3">
                    <label for="ville">Ville</label>
                    <asp:TextBox ID="ville" Text="" CssClass="form-control" runat="server"/>
                </div>
                <div class="col-sm-3 mb-3">
                    <label for="codepostal">Code postal</label>
                    <asp:TextBox ID="codepostal" Text="" CssClass="form-control" runat="server" ToolTip="Format: A9A9A9 ou A9A 9A9 ou A9A-9A9"/>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-3 mb-3">
                    <label for="tel">Téléphone</label>
                    <asp:TextBox ID="tel" Text="" CssClass="form-control" runat="server" ToolTip="Format: 9999999999 ou (999) 999-9999 ou 999 999-9999 ou 999-999-9999"/>
                </div>
                <div class="col-sm-3 mb-3">
                    <label for="cell">Cellulaire</label>
                    <asp:TextBox ID="cell" Text="" CssClass="form-control" runat="server" ToolTip="Format: 9999999999 ou (999) 999-9999 ou 999 999-9999 ou 999-999-9999"/>
                </div>
            </div>

            <!-- validation controls: Required field validators -->
            <asp:RequiredFieldValidator id="rfvPrenom"
                    ControlToValidate="prenom"
                    Display="None"
                ValidationGroup="grInfosPerso"
                EnableClientScript="False" 
                    runat="server"/> 
            <asp:RequiredFieldValidator id="rfvNom"
                    ControlToValidate="nomfamille"
                    Display="None"
                ValidationGroup="grInfosPerso"
                EnableClientScript="False" 
                    runat="server"/>
            <asp:RequiredFieldValidator id="rfvEmail"
                    ControlToValidate="email"
                    Display="None"
                ValidationGroup="grInfosPerso"
                EnableClientScript="False" 
                    runat="server"/>
            <asp:RequiredFieldValidator id="rfvVille"
                    ControlToValidate="ville"
                    Display="None"
                ValidationGroup="grInfosPerso"
                EnableClientScript="False" 
                    runat="server"/>
            <asp:RequiredFieldValidator id="rfvCodePostal"
                    ControlToValidate="codepostal"
                    Display="None"
                ValidationGroup="grInfosPerso"
                EnableClientScript="False" 
                    runat="server"/>
            <asp:RequiredFieldValidator id="rfvAdresse"
                    ControlToValidate="adresse"
                    Display="None"
                ValidationGroup="grInfosPerso"
                EnableClientScript="False" 
                    runat="server"/>
            <asp:RequiredFieldValidator id="rfvTel"
                    ControlToValidate="tel"
                    Display="None"
                ValidationGroup="grInfosPerso"
                EnableClientScript="False" 
                    runat="server"/>

            <!-- validation controls: Reg ex -->
            <asp:RegularExpressionValidator id="rePrenom" 
                     ControlToValidate="prenom"
                     ValidationExpression="^[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF]+(([-'\\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF])*$"
                     Display="None"
                     EnableClientScript="False" 
                ValidationGroup="grInfosPerso"
                     runat="server"/>
            <asp:RegularExpressionValidator id="reNom" 
                     ControlToValidate="nomfamille"
                     ValidationExpression="^[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF]+(([-'\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF])*$"
                     Display="None"
                     EnableClientScript="False" 
                ValidationGroup="grInfosPerso"
                     runat="server"/>
            <asp:RegularExpressionValidator id="reVille" 
                     ControlToValidate="ville"
                     ValidationExpression="^[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF]+(([-'\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF])*$"
                     Display="None"
                     EnableClientScript="False" 
                ValidationGroup="grInfosPerso"
                     runat="server"/>
            <asp:RegularExpressionValidator id="reCodePostal" 
                     ControlToValidate="codepostal"
                     ValidationExpression="^[A-Z]\d[A-Z][\s-]?\d[A-Z]\d$"
                     Display="None"
                     EnableClientScript="False" 
                ValidationGroup="grInfosPerso"
                     runat="server"/>
            <asp:RegularExpressionValidator id="reAdresse" 
                     ControlToValidate="adresse"
                     ValidationExpression="^(\d+-)?\d+([a-zA-Z]|\s\d/\d)?\s[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*\s[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*$"
                     Display="None"
                     EnableClientScript="False" 
                ValidationGroup="grInfosPerso"
                     runat="server"/>
            <asp:RegularExpressionValidator id="reTel" 
                     ControlToValidate="tel"
                     ValidationExpression="^((\([0-9]{3}\)\s|[0-9]{3}[\s-])[0-9]{3}-[0-9]{4}|[0-9]{10})$"
                     Display="None"
                     EnableClientScript="False" 
                ValidationGroup="grInfosPerso"
                     runat="server"/>
            <asp:RegularExpressionValidator id="reCell" 
                     ControlToValidate="cell"
                     ValidationExpression="^((\([0-9]{3}\)\s|[0-9]{3}[\s-])[0-9]{3}-[0-9]{4}|[0-9]{10})$"
                     Display="None"
                     EnableClientScript="False" 
                ValidationGroup="grInfosPerso"
                     runat="server"/>
            

            <div class="row">
              <div class="col-sm-4 mb-3">
                    <label for="pays">Pays</label>
                    <asp:DropDownList id="pays" CssClass="form-control"
                        runat="server">
                          <asp:ListItem Selected="True" Value="CA"> Canada </asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-sm-4 mb-3">
                    <label for="province">Province</label>
                    <asp:DropDownList id="province" CssClass="form-control"
                        runat="server">
                          <asp:ListItem Selected="True" Value="QC"> Québec </asp:ListItem>
                          <asp:ListItem Value="ON"> Ontario </asp:ListItem>
                          <asp:ListItem Value="NB"> Nouveau-Brunswick </asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <hr class="mb-4">
        </div>

        
        <div class="col-md-1 order-md-1">
            <asp:Button ID="btnRetourPanier" CssClass="btn btn-warning" Text="Retour" runat="server" OnClick="retourPanier_click" />
        </div>
        <div class="col-md-2 order-md-1">
            <asp:LinkButton ID="btnLivraison" 
                        runat="server" 
                        CssClass="btn btn-warning"    
                        OnClick="livraison_click"
                ValidationGroup="grInfosPerso">
                Livraison&nbsp;&nbsp;<span aria-hidden="true" class="glyphicon glyphicon-plane"></span>
            </asp:LinkButton>
        </div>
    </asp:Panel>

    <asp:Panel ID="divLiv" runat="server" CssClass="row" Visible="false">
        
            <div class="col-md-6 mb-3">
                <h4 class="mb-3">Méthode de livraison</h4>
                <div class="radio">
                    <label class="infos-livraison"><asp:RadioButton id="rbRegulier" Checked="true" 
                        runat="server" GroupName="typeLivraison" OnCheckedChanged="livraison_changed" AutoPostBack="true"></asp:RadioButton>Poste régulière</label>
                </div>
                <div class="radio">
                    <label class="infos-livraison"><asp:RadioButton id="rbPrioritaire" 
                        runat="server" GroupName="typeLivraison" OnCheckedChanged="livraison_changed" AutoPostBack="true"></asp:RadioButton>Poste prioritaire</label>
                </div>
                <div class="radio">
                    <label class="infos-livraison"><asp:RadioButton id="rbCompagnie" 
                        runat="server" GroupName="typeLivraison" OnCheckedChanged="livraison_changed" AutoPostBack="true"></asp:RadioButton>Compagnie de livraison</label>
                </div>
            </div>

            <div class="col-md-6 mb-3">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <span class="infos-livraison">Poids de la commande:</span>
                            </div>
                            <div class="col-md-6 mb-3">
                                <asp:Label ID="poidsTotalCommande" Text="" CssClass="infos-livraison" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <span class="infos-livraison">Sous total:</span>
                            </div>
                            <div class="col-md-6 mb-3">
                                <asp:Label ID="lblSousTotalLiv" Text="" CssClass="infos-livraison" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <span class="infos-livraison">Frais de transport:</span>
                            </div>
                            <div class="col-md-6 mb-3">
                                <asp:Label ID="fraisTransport" Text="" CssClass="infos-livraison" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="row" id="divTPSLiv" runat="server">
                            <div class="col-md-6 mb-3">
                                <span class="infos-livraison">TPS:</span>
                            </div>
                            <div class="col-md-6 mb-3">
                                <asp:Label ID="lblTPSLiv" Text="" CssClass="infos-livraison" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="row" id="divTVQLiv" runat="server">
                            <div class="col-md-6 mb-3">
                                <span class="infos-livraison">TVQ:</span>
                            </div>
                            <div class="col-md-6 mb-3">
                                <asp:Label ID="lblTVQLiv" Text="" CssClass="infos-livraison" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <span class="infos-livraison">Total:</span>
                            </div>
                            <div class="col-md-6 mb-3">
                                <asp:Label ID="lblTotalLiv" Text="" CssClass="infos-livraison" runat="server"></asp:Label>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

        <hr class="mb-4">

        <div class="col-md-1 order-md-1">
            <asp:Button ID="btnRetourInfosPerso" CssClass="btn btn-warning" Text="Retour" runat="server" OnClick="retourInfosPerso_click" />
        </div>
        <div class="col-md-2 order-md-1">
            <asp:LinkButton ID="btnPaiementSecurise" 
                        runat="server" 
                        CssClass="btn btn-warning"    
                        OnClick="paiementSecurise_click">
                Paiement Sécurisé&nbsp;&nbsp;<span aria-hidden="true" class="glyphicon glyphicon-credit-card"></span>
            </asp:LinkButton>
        </div>
    </asp:Panel>   
        
    <asp:Panel ID="LESi_reussi" Visible="false" runat="server">
        <div class="alert alert-success">
            <strong>Réussi!</strong> Vous allez recevoir un bon de commande par courriel sous peu.
        </div>

        <div class="text-center">
            <div class="conteneur-remerciement">
                <img src="../static/images/logo.png" alt="LOGO" height="200" width="200">
                <h1>Merci d'avoir magasiné chez nous !</h1>
                <h4>Pour toutes questions veuillez communiquer avec notre soutiens technique.</h4>
            </div>
        </div>

        <br />
        <br />
        <br />

        <div class="panel panel-default">
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-3">
                        <asp:Button ID="btnVisualiserBon" CssClass="btn btn-warning" Text="Visualiser le bon de commande" runat="server" OnClick="visualiserBon_click" />
                    </div>
                    <div class="col-md-2">
                        <asp:LinkButton ID="btnImprimerBon" 
                                    runat="server" 
                                    CssClass="btn btn-warning"    
                                    OnClick="imprimerBon_click">
                            <span aria-hidden="true" class="glyphicon glyphicon-print"></span>
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        
    </asp:Panel>

    <asp:Panel ID="LESi_echoue" CssClass="alert alert-danger" Visible="false" runat="server">
        <asp:Label ID="corpsMessageErreur" runat="server"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="div_paiement" Visible="false" runat="server" CssClass="row">

            <div class="col-md-6 mb-3">
                <div class="row">
                    <div class="col-md-8">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-credit-card"></i></span>
                            <asp:TextBox ID="noCarte" CssClass="form-control" placeholder="0000000000000000 (16 chiffres)" MaxLength="16" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group">
                            <span class="input-group-addon">CVV</span>
                            <asp:TextBox ID="tbCVV" CssClass="form-control" placeholder="000 à 9999" MaxLength="4" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="col-md-4">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            <asp:TextBox ID="tbDate" CssClass="form-control" placeholder="MM-AAAA" MaxLength="7" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-6 mb-3">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <span class="infos-livraison">Poids de la commande:</span>
                            </div>
                            <div class="col-md-6 mb-3">
                                <asp:Label ID="lblPoidsPaiement" Text="" CssClass="infos-livraison" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <span class="infos-livraison">Sous total:</span>
                            </div>
                            <div class="col-md-6 mb-3">
                                <asp:Label ID="lblSousTotalPaiement" Text="" CssClass="infos-livraison" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <span class="infos-livraison">Frais de transport:</span>
                            </div>
                            <div class="col-md-6 mb-3">
                                <asp:Label ID="lblFraisTransportPaiement" Text="" CssClass="infos-livraison" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="row" id="divTPSPaiement" runat="server">
                            <div class="col-md-6 mb-3">
                                <span class="infos-livraison">TPS:</span>
                            </div>
                            <div class="col-md-6 mb-3">
                                <asp:Label ID="lblTPSPaiement" Text="" CssClass="infos-livraison" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="row" id="divTVQPaiement" runat="server">
                            <div class="col-md-6 mb-3">
                                <span class="infos-livraison">TVQ:</span>
                            </div>
                            <div class="col-md-6 mb-3">
                                <asp:Label ID="lblTVQPaiement" Text="" CssClass="infos-livraison" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <span class="infos-livraison">Total:</span>
                            </div>
                            <div class="col-md-6 mb-3">
                                <asp:Label ID="lblTotalPaiement" Text="" CssClass="infos-livraison" runat="server"></asp:Label>
                            </div>
                        </div>

                    </div>
                </div>
            </div>


        <hr class="mb-4">

             <div class="row">
                <div class="col-md-1 order-md-1">
                    <asp:Button ID="btnRetourLivraison" CssClass="btn btn-warning" Text="Retour" runat="server" OnClick="retourLivraison_click" />
                </div>
                <div class="col-md-2 order-md-1">
                    <button id="btnLESi" class="btn btn-warning" onclick="return lesiForm();">
                        Payer&nbsp;&nbsp;<span aria-hidden="true" class="glyphicon glyphicon-credit-card"></span>
                    </button>
                </div>
            </div>
    </asp:Panel>

    </div>

</asp:Content>
