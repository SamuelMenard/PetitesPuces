using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Statistiques : System.Web.UI.Page
{
    private long noVendeurSelectionne;
    private long noClientSelectionne;

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

    public String nbVisitesClientVendeur;
    public String NbVisitesClientVendeur { get { return this.nbVisitesClientVendeur; } set { this.nbVisitesClientVendeur = value; } }

    public string nbTotalVendeurs_value { get { return nbTotalVendeurs_value; } }

    

    protected void Page_Load(object sender, EventArgs e)
    {
        getNoVendeur();
        getNoClient();

        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableVendeur = dataContext.PPVendeurs;
        var tableClients = dataContext.PPClients;
        var tableVendeursClients = dataContext.PPVendeursClients;

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

        // 4) Nombre de visites d'un client pour un vendeur
        // populer liste des vendeurs
        List<PPVendeurs> lstVendeurs = (from vendeur in tableVendeur where vendeur.Statut == 1 select vendeur).ToList();
        foreach (PPVendeurs vendeur in lstVendeurs)
        {
            ddlVendeurs.Items.Add(new ListItem(vendeur.NomAffaires, vendeur.NoVendeur.ToString()));
        }
        
        // populer liste des clients
        List<PPClients> lstClients = (from client in tableClients where client.Statut == 1 select client).ToList();

        foreach (PPClients client in lstClients)
        {
            ddlClients.Items.Add(new ListItem(client.AdresseEmail, client.NoClient.ToString()));
        }

        // mettre le nb de visites
        if (lstClients.Count == 0)
        {
            nbVisitesClientVendeur = "N/A";
        }
        else
        {
            PPVendeurs vendeur = (this.noVendeurSelectionne != -1) ? (from v in tableVendeur where v.NoVendeur == this.noVendeurSelectionne select v).First() : lstVendeurs.First();
            PPClients client = (this.noClientSelectionne != -1) ? (from c in tableClients where c.NoClient == this.noClientSelectionne select c).First():lstClients.First();
            nbVisitesClientVendeur = (from v in client.PPVendeursClients where v.NoVendeur == vendeur.NoVendeur select v).Count().ToString();
        }

    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        ddlVendeurs.SelectedValue = this.noVendeurSelectionne.ToString();
        ddlClients.SelectedValue = this.noClientSelectionne.ToString();
    }

    public void ddlVendeurs_onChanged(Object sender, EventArgs e)
    {
        String url = "~/Pages/Statistiques.aspx?NoVendeur=" + ddlVendeurs.SelectedValue + (this.noClientSelectionne != -1?"&NoClient=" + this.noClientSelectionne:"");
        Response.Redirect(url, true);
    }

    public void ddlClients_onChanged(Object sender, EventArgs e)
    {
        String url = "~/Pages/Statistiques.aspx?NoClient=" + ddlClients.SelectedValue + (this.noVendeurSelectionne != -1 ? "&NoVendeur=" + this.noVendeurSelectionne : "");
        Response.Redirect(url, true);
    }

    private void getNoVendeur()
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableVendeurs = dataContext.PPVendeurs;

        long n;
        if (Request.QueryString["NoVendeur"] == null || !long.TryParse(Request.QueryString["NoVendeur"], out n))
        {
            this.noVendeurSelectionne = -1;
        }
        else
        {
            if ((from vendeur in tableVendeurs where vendeur.NoVendeur == n select vendeur).Count() > 0)
            {
                this.noVendeurSelectionne = n;
            }
            else
            {
                this.noVendeurSelectionne = -1;
            }
            
        }
    }

    private void getNoClient()
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableClients = dataContext.PPClients;

        long n;
        if (Request.QueryString["NoClient"] == null || !long.TryParse(Request.QueryString["NoClient"], out n))
        {
            this.noClientSelectionne = -1;
        }
        else
        {
            if ((from client in tableClients where client.NoClient == n select client).Count() > 0)
            {
                this.noClientSelectionne = n;
            }
            else
            {
                this.noClientSelectionne = -1;
            }

        }
    }
}