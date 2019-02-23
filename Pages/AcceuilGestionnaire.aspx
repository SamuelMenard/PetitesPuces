<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../PageMaster/MasterPage.master" CodeFile="AcceuilGestionnaire.aspx.cs" Inherits="Pages_AcceuilGestionnaire" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
    <link rel="stylesheet" href="../static/style/dashboardGestionnaire.css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
    <div class="container">

        <div class="jumbotron">
                <h1>Tableau de bord du gestionnaire</h1> 
                <p>Vous pouvez retrouver ci-bas des liens vers les différents outils d'administration</p> 
        </div>

        <div class="row">
            <asp:LinkButton ID="btn_gererDemandes" 
                        runat="server"
                CssClass="col-md-3"
            OnClick="nouvellesDemandes_click">

                <asp:Panel ID="gererDemandes" CssClass="" runat="server">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <img src="../static/images/shake-hands.jpg" alt="LOGO" class="img-responsive">
                        </div>
                        <div class="panel-footer">
                            <h4>Gérer les nouvelles demandes de vendeur</h4>
                        </div>
                    </div>
                </asp:Panel>
            </asp:LinkButton>

            <asp:LinkButton ID="btn_gererInactiviteClients" 
                            runat="server"
                CssClass="col-md-3"
                OnClick="inactiviteClients_click">
                    <asp:Panel ID="gererInactiviteClients" CssClass="" runat="server">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <img src="../static/images/user-management.png" alt="LOGO" class="img-responsive">
                            </div>
                            <div class="panel-footer">
                                <h4>Gérer l'inactivité des clients</h4>
                                <br />
                            </div>
                        </div>
                    </asp:Panel>
            </asp:LinkButton>

            <asp:LinkButton ID="btn_gererInactiviteVendeurs" 
                            runat="server"
                CssClass="col-md-3"
                OnClick="inactiviteVendeurs_click">
                    <asp:Panel ID="gererInactiviteVendeurs" CssClass="" runat="server">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <img src="../static/images/user-management.png" alt="LOGO" class="img-responsive">
                            </div>
                            <div class="panel-footer">
                                <h4>Gérer l'inactivité des vendeurs</h4>
                            </div>
                        </div>
                    </asp:Panel>
            </asp:LinkButton>

            <asp:LinkButton ID="btn_RendreInactif" 
                            runat="server"
                CssClass="col-md-3"
                OnClick="rendreInactif_click">
                    <asp:Panel ID="rendreInactif" CssClass="" runat="server">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <img src="../static/images/user-management.png" alt="LOGO" class="img-responsive">
                            </div>
                            <div class="panel-footer">
                                <h4>Rendre inactif un client ou un vendeur</h4>
                            </div>
                        </div>
                    </asp:Panel>
            </asp:LinkButton>
        </div>

        <div class="row">
            <asp:LinkButton ID="btn_visualiserStats" 
                            runat="server"
                CssClass="col-md-3"
                OnClick="stats_click">
                    <asp:Panel ID="visualiserStats" CssClass="" runat="server">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <img src="../static/images/stats.png" alt="LOGO" class="img-responsive">
                            </div>
                            <div class="panel-footer">
                                <h4>Visualiser les statistiques</h4>
                                <br />
                            </div>
                        </div>
                    </asp:Panel>
            </asp:LinkButton>

            <asp:LinkButton ID="btn_visualiserRedevances" 
                            runat="server"
                CssClass="col-md-3"
                OnClick="redevances_click">
                    <asp:Panel ID="visualiserRedevances" CssClass="" runat="server">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <img src="../static/images/payroll.png" alt="LOGO" class="img-responsive">
                            </div>
                            <div class="panel-footer">
                                <h4>Gestion des redevances</h4>
                                <br />
                            </div>
                        </div>
                    </asp:Panel>
            </asp:LinkButton>

            <asp:LinkButton ID="btn_Email" 
                        runat="server"
                CssClass="col-md-3"
            OnClick="email_click">
                    <asp:Panel ID="Panel1" CssClass="" runat="server">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <img src="../static/images/email-logo.png" alt="LOGO" class="img-responsive">
                            </div>
                            <div class="panel-footer">
                                <h4>Boîte de messagerie</h4>
                                <br />
                            </div>
                        </div>
                    </asp:Panel>
            </asp:LinkButton>

            <asp:LinkButton ID="btn_VisRedevances" 
                        runat="server"
                CssClass="col-md-3"
            OnClick="vis_redevances_click">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <img src="../static/images/vis_redevances.png" alt="LOGO" class="img-responsive">
                            </div>
                            <div class="panel-footer">
                                <h4>Encaisser les redevances</h4>
                                <br />
                            </div>
                        </div>
            </asp:LinkButton>
        </div>

    </div>
    
</asp:Content>
