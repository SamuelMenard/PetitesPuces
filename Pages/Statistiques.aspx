<%@ Page Language="C#" MasterPageFile="../PageMaster/MasterPage.master" AutoEventWireup="true" CodeFile="Statistiques.aspx.cs" Inherits="Pages_Statistiques" %>
<%@ Import Namespace="System.Web.Script.Serialization" %> 

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <!-- Pour ajouter des imports dans le head -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.3/Chart.bundle.min.js"></script>

    <script>
        // execute on page load
        window.onload = function () {
            graphTotalVendeurs();
            graphNouveauxVendeurs();
            graphTotalClients();
        };

        function graphTotalVendeurs() {
            var tVendeurs = <%= new JavaScriptSerializer().Serialize(totalVendeurs) %>;
            // Bar chart
            new Chart(document.getElementById("canvasTotalVendeurs"), {
                type: 'bar',
                data: {
                  labels: ["Vendeurs"],
                  datasets: [
                    {
                      label: "Vendeurs",
                      backgroundColor: ["#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850"],
                      data: [tVendeurs]
                    }
                  ]
                },
                options: {
                  legend: { display: false },
                  title: {
                    display: true,
                    text: 'Quantité totale de vendeurs'
                  }
                }
            });
        }

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
                <canvas id="canvasTotalVendeurs" width="200" height="200"></canvas>
            </div>
            <div class="col-md-4">
                <canvas id="canvasNouveauxVendeurs" width="200" height="200"></canvas>
            </div>
            <div class="col-md-4">
                <canvas id="canvasTotalClients" width="200" height="200"></canvas>
            </div>
        </div>
    </div>
    
    
    
</asp:Content>
