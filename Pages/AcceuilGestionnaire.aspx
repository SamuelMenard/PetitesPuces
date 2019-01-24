<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../PageMaster/MasterPage.master" CodeFile="AcceuilGestionnaire.aspx.cs" Inherits="Pages_AcceuilGestionnaire" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
    <link rel="stylesheet" href="../static/style/dashboardGestionnaire.css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->
    <h1>Tableau de bord du gestionnaire</h1>
    <br />
    <br />
    <br />

    <div class="row">

        <asp:LinkButton ID="btn_gererDemandes" 
                        runat="server"
            OnClick="nouvellesDemandes_click">

                <asp:Panel ID="gererDemandes" CssClass="col-md-3" runat="server">
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
            OnClick="inactiviteClients_click">
                <asp:Panel ID="gererInactiviteClients" CssClass="col-md-3" runat="server">
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
            OnClick="inactiviteVendeurs_click">
                <asp:Panel ID="gererInactiviteVendeurs" CssClass="col-md-3" runat="server">
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
            OnClick="rendreInactif_click">
                <asp:Panel ID="rendreInactif" CssClass="col-md-3" runat="server">
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

        <asp:LinkButton ID="btn_visualiserStats" 
                        runat="server"
            OnClick="stats_click">
                <asp:Panel ID="visualiserStats" CssClass="col-md-3" runat="server">
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
            OnClick="redevances_click">
                <asp:Panel ID="visualiserRedevances" CssClass="col-md-3" runat="server">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <img src="../static/images/payroll.png" alt="LOGO" class="img-responsive">
                        </div>
                        <div class="panel-footer">
                            <h4>Visualiser les redevances</h4>
                            <br />
                        </div>
                    </div>
                </asp:Panel>
        </asp:LinkButton>
        

    </div>
</asp:Content>
