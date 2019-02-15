<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="Statistiques.aspx.cs" Inherits="Pages_Statistiques" %>
<%@ Import Namespace="System.Web.Script.Serialization" %> 

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.3/Chart.bundle.min.js"></script>
    <link rel="stylesheet" href="../static/style/circle.css">
    <link rel="stylesheet" href="../static/style/circle.scss">

    <script>
        // execute on page load
        window.onload = function () {
            graphNouveauxVendeurs();
            graphTotalClients();
            graphNouveauxClients();
            graphNbConnexionsClients();
        };

        function graphNouveauxVendeurs() {
            var mois1 = <%= new JavaScriptSerializer().Serialize(mois1) %>;
            var mois3 = <%= new JavaScriptSerializer().Serialize(mois3) %>;
            var mois6 = <%= new JavaScriptSerializer().Serialize(mois6) %>;
            var mois12 = <%= new JavaScriptSerializer().Serialize(mois12) %>;
            var moisToujours = <%= new JavaScriptSerializer().Serialize(moisToujours) %>;

            new Chart(document.getElementById("canvasNouveauxVendeurs"), {
                type: 'line',
                data: {
                labels: ['1 mois', '3 mois', '6 mois', '12 mois', 'Toujours'],
                datasets: [{ 
                        data: [mois1, mois3, mois6, mois12, moisToujours],
                        label: "Nouveaux vendeurs",
                        borderColor: "#3e95cd",
                        fill: false
                    }
                ]
                },
                options: {
                title: {
                    display: true,
                    text: 'Nouveaux vendeurs au courant du temps'
                }
                }
            });
        }

        function graphTotalClients() {
            var cActifs = <%= new JavaScriptSerializer().Serialize(clientsActifs) %>;
            var cPotentiels = <%= new JavaScriptSerializer().Serialize(clientsPotentiels) %>;
            var cVisiteurs = <%= new JavaScriptSerializer().Serialize(clientsVisiteurs) %>;

            // Bar chart
            new Chart(document.getElementById("canvasTotalClients"), {
                type: 'bar',
                data: {
                  labels: ["Actif", "Potentiels", "Visiteurs"],
                  datasets: [
                    {
                      label: "Clients",
                          backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
                          data: [cActifs, cPotentiels, cVisiteurs]
                    }
                  ]
                },
                options: {
                  legend: { display: false },
                  title: {
                    display: true,
                    text: 'Quantité totale de clients'
                  }
                }
            });
        }

        function graphNouveauxClients() {
            var mois3 = <%= new JavaScriptSerializer().Serialize(mois3Client) %>;
            var mois6 = <%= new JavaScriptSerializer().Serialize(mois6Client) %>;
            var mois9 = <%= new JavaScriptSerializer().Serialize(mois9Client) %>;
            var mois12 = <%= new JavaScriptSerializer().Serialize(mois12Client) %>;

            new Chart(document.getElementById("canvasNbNouveauxClientsDepuis"), {
                type: 'line',
                data: {
                labels: ['3 mois', '6 mois', '9 mois', '12 mois'],
                datasets: [{ 
                        data: [mois3, mois6, mois9, mois12],
                        label: "Nouveaux clients",
                        borderColor: "#3e95cd",
                        fill: false
                    }
                ]
                },
                options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            suggestedMax:10,
                            stepSize: 1,
                            beginAtZero: true
                        }
                    }]
                },
                title: {
                    display: true,
                    text: 'Nouveaux clients au courant du temps'
                }
                }
            });
        }

        function graphNbConnexionsClients() {
            var tabClient = <%= new JavaScriptSerializer().Serialize(tabClients) %>;
            var tabNbConnexions = <%= new JavaScriptSerializer().Serialize(tabNbConnexions) %>;

            var tabCouleurs = [];
            for (var i = 0; i < tabClient.length; i++) {
                tabCouleurs.push(getRandomColor());
            }

            new Chart(document.getElementById("canvasNbConnexionsClients"), {
                type: 'horizontalBar',
                data: {
                  labels: tabClient,
                  datasets: [
                    {
                      label: "Nombre de connexions",
                      backgroundColor: tabCouleurs,
                      data: tabNbConnexions
                    }
                  ]
                },
                options: {
                  legend: { display: false },
                  title: {
                    display: true,
                    text: 'Nombre de connexions par client'
                  }
                }
            });
        }

        function getRandomColor() {
            var letters = '0123456789ABCDEF'.split('');
            var color = '#';
            for (var i = 0; i < 6; i++ ) {
                color += letters[Math.floor(Math.random() * 16)];
            }
            return color;
        }
        
        
    </script>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <!-- Contenu de la page -->

    <div class="container">
        <div class="jumbotron">
            <h1>Visualisation des statistiques</h1>
            <p>Vous pouvez visualiser divers statistiques en lien avec les activités sur le site les Petites Puces.</p>
        </div>

        <div class="row">
            <div class="col-sm-12 mb-3">
                    <asp:Button ID="btnRetourPanier" CssClass="btn btn-warning" Text="Retour" runat="server" OnClick="retourDashboard_click" />
                </div>
            <br />
            <br />
                <div class="col-md-4">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <h3 class="text-center">Nombre total de vendeurs</h3>
                            <div class="inner-content text-center">
                                <div class="c100 p100 big center orange">
                                    <span><%=totalVendeurs %></span>
                                    <div class="slice"><div class="bar"></div><div class="fill"></div></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            <div class="col-md-4">
                <div class="panel panel-default">
                        <div class="panel-body">
                            <canvas id="canvasNouveauxVendeurs" width="200" height="200"></canvas>
                        </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="panel panel-default">
                        <div class="panel-body">
                            <canvas id="canvasTotalClients" width="200" height="200"></canvas>
                        </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-4">
                <div class="panel panel-default">
                        <div class="panel-body">
                            <h3>Nombre de visites d'un client pour un vendeur</h3>
                            <div class="row">
                                <div class="col-md-4">
                                    <h4>Vendeurs : </h4>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlVendeurs" CssClass="form-control" OnSelectedIndexChanged="ddlVendeurs_onChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <h4>Clients : </h4>
                                </div>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlClients" CssClass="form-control" OnSelectedIndexChanged="ddlClients_onChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="inner-content text-center">
                                <div class="c100 p100 big center orange">
                                    <span><%=nbVisitesClientVendeur %></span>
                                    <div class="slice"><div class="bar"></div><div class="fill"></div></div>
                                </div>
                            </div>
                        </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="panel panel-default">
                        <div class="panel-body">
                            <canvas id="canvasNbNouveauxClientsDepuis" width="200" height="200"></canvas>
                        </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="panel panel-default">
                        <div class="panel-body">
                            <canvas id="canvasNbConnexionsClients" width="200" height="200"></canvas>
                        </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-12 mb-3 table-responsive">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <h3>Montant total des commandes d'un client par vendeur</h3>
                        <asp:Table ID="tabTotalCommandesClientsParVendeur" runat="server" CssClass="table table-bordered table-condensed">
                            <asp:TableHeaderRow>
                                <asp:TableHeaderCell>Numéro de client</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Nom complet du client</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Numéro de vendeur</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Nom complet du vendeur</asp:TableHeaderCell> 
                                <asp:TableHeaderCell>Montant total des commandes</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Date de la dernière commande</asp:TableHeaderCell>
                            </asp:TableHeaderRow>
                        </asp:Table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    
    
</asp:Content>
