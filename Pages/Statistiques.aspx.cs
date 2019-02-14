using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Statistiques : System.Web.UI.Page
{
    public int mois1;
    public int mois3;
    public int mois6;
    public int mois12;
    public int moisToujours;

    public int Mois1 { get { return this.mois1; } set { this.mois1 = value; } }
    public int Mois3 { get { return this.mois3; } set { this.mois3 = value; } }
    public int Mois6 { get { return this.mois6; } set { this.mois6 = value; } }
    public int Mois12 { get { return this.mois12; } set { this.mois12 = value; } }
    public int MoisToujours { get { return this.moisToujours; } set { this.moisToujours = value; } }

    public int totalVendeurs;
    public int TotalVendeurs { get { return this.totalVendeurs; } set { this.totalVendeurs = value; } }

    public int clientsActifs;
    public int clientsPotentiels;
    public int clientsVisiteurs;

    public int ClientsActifs { get { return this.clientsActifs; } set { this.clientsActifs = value; } }
    public int ClientsPotentiels { get { return this.clientsPotentiels; } set { this.clientsPotentiels = value; } }
    public int ClientsVisiteurs { get { return this.clientsVisiteurs; } set { this.clientsVisiteurs = value; } }

    public string nbTotalVendeurs_value { get { return nbTotalVendeurs_value; } }

    BD6B8_424SEntities dataContext = new BD6B8_424SEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        var tableVendeur = dataContext.PPVendeurs;
        var tableClients = dataContext.PPClients;

        // 1) Tous les vendeurs sur le site
        totalVendeurs = (from vendeur in tableVendeur where vendeur.Statut == 1 select vendeur).Count();

        // 2) Nouveaux vendeurs depuis 1,3,6,12, toujours
        DateTime d1 = DateTime.Now.AddMonths(-1);
        mois1 = (from v1 in tableVendeur where v1.DateCreation <= d1 && v1.Statut == null select v1).Count();

        DateTime d3 = DateTime.Now.AddMonths(-3);
        mois3 = (from v1 in tableVendeur where v1.DateCreation <= d3 && v1.Statut == null select v1).Count();

        DateTime d6 = DateTime.Now.AddMonths(-6);
        mois6 = (from v1 in tableVendeur where v1.DateCreation <= d6 && v1.Statut == null select v1).Count();

        DateTime d12 = DateTime.Now.AddMonths(-12);
        mois12 = (from v1 in tableVendeur where v1.DateCreation <= d12 && v1.Statut == null select v1).Count();
        
        moisToujours = (from v1 in tableVendeur where v1.Statut == null select v1).Count();

        // 3) Nombre total de clients
        clientsActifs = (from client in tableClients where client.PPCommandes.Count > 0 select client).Count();
        clientsPotentiels = (from client in tableClients where client.PPCommandes.Count == 0 && client.PPArticlesEnPanier.Count() > 0 select client).Count();
        clientsVisiteurs = (from client in tableClients where client.PPCommandes.Count == 0 && client.PPArticlesEnPanier.Count() == 0 select client).Count();

        
        


    }
}