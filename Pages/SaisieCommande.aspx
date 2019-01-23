<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="SaisieCommande.aspx.cs" Inherits="Pages_SaisieCommande" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
    <link rel="stylesheet" href="../static/style/panier.css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
    <asp:PlaceHolder id="phDynamique" runat="server" />


    <asp:Panel ID="divLivraison" CssClass="row" Visible="false" runat="server">
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
                <asp:TextBox ID="email" Text="" CssClass="form-control" runat="server"/>
            </div>

            <div class="row">
                <div class="col-sm-4 mb-3">
                    <label for="ville">Ville</label>
                    <asp:TextBox ID="ville" Text="" CssClass="form-control" runat="server"/>
                </div>
                <div class="col-sm-4 mb-3">
                    <label for="rue">Rue</label>
                    <asp:TextBox ID="rue" Text="" CssClass="form-control" runat="server"/>
                </div>
                <div class="col-sm-4 mb-3">
                    <label for="codepostal">Code postal</label>
                    <asp:TextBox ID="codepostal" Text="" CssClass="form-control" runat="server"/>
                </div>
            </div>

            <div class="row">
              <div class="col-sm-4 mb-3">
                    <label for="pays">Pays</label>
                    <asp:DropDownList id="pays" CssClass="form-control"
                        runat="server">
                          <asp:ListItem Selected="True" Value="CA"> Canada </asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-sm-4 mb-3">
                    <label for="province">Rue</label>
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

        <div class="col-md-12 order-md-1">
            <h4 class="mb-3">Méthode de livraison</h4>
            <div class="row">
                <div class="col-md-12 mb-3">
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
            </div>

            <hr class="mb-4">

            <div class="row">
                <div class="col-md-4 mb-3">
                    <span class="infos-livraison">Poids de la commande:</span>
                </div>
                <div class="col-md-4 mb-3">
                    <asp:Label ID="poidsTotalCommande" Text="" CssClass="infos-livraison" runat="server"></asp:Label>
                </div>
            </div>

            <hr class="mb-4">

            <div class="row">
                <div class="col-md-4 mb-3">
                    <span class="infos-livraison">Frais de transport:</span>
                </div>
                <div class="col-md-4 mb-3">
                    <asp:Label ID="fraisTransport" Text="" CssClass="infos-livraison" runat="server"></asp:Label>
                </div>
            </div>

            <hr class="mb-4">
        </div>

        
        <div class="col-md-1 order-md-1">
            <asp:Button ID="btnRetourPanier" CssClass="btn btn-warning" Text="Retour" runat="server" OnClick="retourPanier_click" />
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

    <asp:Panel ID="div_reponse_LESi" Visible="false" runat="server">
        <asp:Panel ID="LESi_reussi" CssClass="alert alert-success" Visible="false" runat="server">
          <strong>Réussi!</strong> Vous allez recevoir une confirmation par courriel sous peu.


        </asp:Panel>

        <asp:Panel ID="LESi_echoue" CssClass="alert alert-danger" Visible="false" runat="server">
          <strong>Échoué...</strong> Votre carte de crédit a été refusée.
        </asp:Panel>


    </asp:Panel>

    <asp:Panel ID="div_paiement" Visible="false" runat="server">
        <div class="row">
                <div class="col-md-12 mb-3">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-4 mb-3">
                                    <span class="infos-livraison">Sous total:</span>
                                </div>
                                <div class="col-md-4 mb-3">
                                    <asp:Label ID="sousTotal_paiement" Text="" CssClass="infos-livraison" runat="server"></asp:Label>
                                </div>
                            </div>
                            <hr class="mb-4">

                            <div class="row">
                                <div class="col-md-4 mb-3">
                                    <span class="infos-livraison">Livraison:</span>
                                </div>
                                <div class="col-md-4 mb-3">
                                    <asp:Label ID="fraisLivraison_paiement" Text="" CssClass="infos-livraison" runat="server"></asp:Label>
                                </div>
                            </div>
                            <hr class="mb-4">

                            <div class="row">
                                <div class="col-md-4 mb-3">
                                    <span class="infos-livraison">TPS:</span>
                                </div>
                                <div class="col-md-4 mb-3">
                                    <asp:Label ID="TPS_paiement" Text="" CssClass="infos-livraison" runat="server"></asp:Label>
                                </div>
                            </div>
                            <hr class="mb-4">

                            <div class="row">
                                <div class="col-md-4 mb-3">
                                    <span class="infos-livraison">TVQ:</span>
                                </div>
                                <div class="col-md-4 mb-3">
                                    <asp:Label ID="TVQ_paiement" Text="" CssClass="infos-livraison" runat="server"></asp:Label>
                                </div>
                            </div>
                            <hr class="mb-4">

                            <div class="row">
                                <div class="col-md-4 mb-3">
                                    <span class="infos-livraison">Total:</span>
                                </div>
                                <div class="col-md-4 mb-3">
                                    <asp:Label ID="total_paiement" Text="" CssClass="infos-livraison" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        <h4 class="mb-3">Méthode de paiment</h4>
            <div class="row">
                <div class="col-md-4">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-credit-card"></i></span>
                        <asp:TextBox ID="noCarte" CssClass="form-control" placeholder="000-000-000" runat="server"></asp:TextBox>
                      </div>
                </div>
            </div>

        <hr class="mb-4">

             <div class="row">
                <div class="col-md-1 order-md-1">
                    <asp:Button ID="btnRetourLivraison" CssClass="btn btn-warning" Text="Retour" runat="server" OnClick="retourLivraison_click" />
                </div>
                <div class="col-md-2 order-md-1">
                    <asp:LinkButton ID="btnLESi" 
                                runat="server" 
                                CssClass="btn btn-warning"    
                                OnClick="paiementLESi_click">
                        Payer&nbsp;&nbsp;<span aria-hidden="true" class="glyphicon glyphicon-credit-card"></span>
                    </asp:LinkButton>
                </div>
            </div>
    </asp:Panel>

    

    

</asp:Content>
