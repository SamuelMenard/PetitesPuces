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
        </div>
    </div>
    
    
    
</asp:Content>
