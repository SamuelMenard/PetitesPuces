<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="BoiteMessagerie.aspx.cs" Inherits="Pages_BoiteMessagerie" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
    <div class="container">
            <div class="jumbotron">
                <h1>Boîte de messagerie</h1> 
                <p>Sélectionnez des destinataires en cochant leur nom</p> 
          </div>

        

        <div id="divChoixDestinataires" class="row" runat="server">
            <div id="divMessageErreur" visible="false" class="alert alert-danger" runat="server">
              <strong>Attention !</strong> Vous devez sélectionner au moins un destinataire.
            </div>

            <div id="divEnvoieSucces" visible="false" class="alert alert-success" runat="server">
              <strong>Excellent !</strong> Le courriel a été envoyé avec succès.
            </div>

            
            <asp:PlaceHolder id="phChoix" runat="server" />


            <br />
            <br />
            <br />

            <div class="col-md-7">
                <div class="panel panel-default">
                    <div id="divTableVendeurs" class="panel-body" runat="server">
                        <h2>Vendeurs</h2>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Button ID="btnSelectionnerToutVendeurs" CssClass="btn btn-warning" Text="Sélectionner tout" runat="server" OnClick="selectionToutVendeurs_click" />
                            </div>
                            <div class="col-md-2">
                                <asp:LinkButton ID="btnTrierParDateVendeursCroissant" CssClass="btn btn-warning" OnClick="dateCroissant_click" runat="server">
                                    Date <span class="glyphicon glyphicon-sort-by-attributes"></span>
                                </asp:LinkButton>
                            </div>
                            <div class="col-md-2">
                                <asp:LinkButton ID="btnTrierParDateVendeursDecroissant" CssClass="btn btn-warning" OnClick="dateDecroissant_click" runat="server">
                                    Date <span class="glyphicon glyphicon-sort-by-attributes-alt"></span>
                                </asp:LinkButton>
                            </div>
                            <div class="col-md-2">
                                <asp:LinkButton ID="btnTrierParVentesVendeurCroissant" CssClass="btn btn-warning" OnClick="ventesCroissant_click" runat="server">
                                    Ventes <span class="glyphicon glyphicon-sort-by-attributes"></span>
                                </asp:LinkButton>
                            </div>
                            <div class="col-md-2">
                                <asp:LinkButton ID="btnTrierParVentesVendeurDecroissant" CssClass="btn btn-warning" OnClick="ventesDecroissant_click" runat="server">
                                    Ventes <span class="glyphicon glyphicon-sort-by-attributes-alt"></span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
            <div class="col-md-5">
                <div class="panel panel-default">
                    <div id="divTableClients" class="panel-body" runat="server">
                        <h2>Clients</h2>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Button ID="btnSelectionnerToutClients" CssClass="btn btn-warning" Text="Sélectionner tout" runat="server" OnClick="selectionToutClients_click" />
                            </div>
                            <div class="col-md-3">
                                <asp:LinkButton ID="btnDateCroissantClients" CssClass="btn btn-warning" OnClick="dateCroissantClients_click" runat="server">
                                    Date <span class="glyphicon glyphicon-sort-by-attributes"></span>
                                </asp:LinkButton>
                            </div>
                            <div class="col-md-3">
                                <asp:LinkButton ID="btnDateDecroissantClients" CssClass="btn btn-warning" OnClick="dateDecroissantClients_click" runat="server">
                                    Date <span class="glyphicon glyphicon-sort-by-attributes-alt"></span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>

        <asp:Panel ID="divCourriel" Visible="false" runat="server">

            <div id="divMessageErreurCourriel" visible="false" class="alert alert-danger" runat="server">
              <strong>Attention !</strong> L'objet et le message ne peuvent pas êtres vide.
            </div>

            <asp:PlaceHolder id="phCourriel" runat="server" />

        <br />
        <br />
        <br />

            <label>Destinataire(s):</label>
            <div class="panel panel-default">
                <div class="panel-body">
                    <asp:Panel ID="divDestinataires" CssClass="row" runat="server">
                    </asp:Panel>
                </div>
            </div>

            <div class="form-group">
              <label for="tbObject">Objet:</label>
              <asp:TextBox ID="tbObjet" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
              <label for="tbMessage">Message:</label>
              <textarea class="form-control" rows="5" id="tbMessage" style="resize: none;" runat="server"></textarea>
            </div>

            <asp:Button ID="btnEnvoyer" CssClass="btn btn-warning btn-block" Text="Envoyer" OnClick="envoyer_click" runat="server" />
        </asp:Panel>
        

        

            

    </div>
</asp:Content>
