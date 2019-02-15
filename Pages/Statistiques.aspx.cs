using System;
using System.Collections.Generic;
using System.Globalization;
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
    
    public int mois3Client;
    public int mois6Client;
    public int mois9Client;
    public int mois12Client;

    public int Mois3Client { get { return this.mois3Client; } set { this.mois3Client = value; } }
    public int Mois6Client { get { return this.mois6Client; } set { this.mois6Client = value; } }
    public int Mois9Client { get { return this.mois9Client; } set { this.mois9Client = value; } }
    public int Mois12Client { get { return this.mois12Client; } set { this.mois12Client = value; } }

    public List<String> tabClients = new List<string>();
    public List<short> tabNbConnexions = new List<short>();
    public List<String> TabClients { get { return this.tabClients; } set { this.tabClients = value; } }
    public List<short> TabNbConnexions { get { return this.tabNbConnexions; } set { this.tabNbConnexions = value; } }


    public string nbTotalVendeurs_value { get { return nbTotalVendeurs_value; } }

    

    protected void Page_Load(object sender, EventArgs e)
    {
        verifierPermissions("G");

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
        mois1 = (from v1 in tableVendeur where v1.DateCreation <= d1 select v1).Count();

        DateTime d3 = DateTime.Now.AddMonths(-3);
        mois3 = (from v1 in tableVendeur where v1.DateCreation <= d3 select v1).Count();

        DateTime d6 = DateTime.Now.AddMonths(-6);
        mois6 = (from v1 in tableVendeur where v1.DateCreation <= d6 select v1).Count();

        DateTime d12 = DateTime.Now.AddMonths(-12);
        mois12 = (from v1 in tableVendeur where v1.DateCreation <= d12 select v1).Count();
        
        moisToujours = (from v1 in tableVendeur select v1).Count();

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

        // 5) Nombre de nouveaux clients depuis 3, 6, 9, 12 mois
        DateTime d3C = DateTime.Now.AddMonths(-3);
        mois3Client = (from c in tableClients where c.DateCreation <= d3C select c).Count();

        DateTime d6C = DateTime.Now.AddMonths(-6);
        mois6Client = (from c in tableClients where c.DateCreation <= d6C select c).Count();

        DateTime d9C = DateTime.Now.AddMonths(-9);
        mois9Client = (from c in tableClients where c.DateCreation <= d9C select c).Count();

        DateTime d12C = DateTime.Now.AddMonths(-12);
        mois12Client = (from c in tableClients where c.DateCreation <= d12C select c).Count();

        // 6) Nombre connexions clients
        foreach(PPClients client in tableClients)
        {
            tabClients.Add(client.AdresseEmail);
            tabNbConnexions.Add((short)client.NbConnexions);
        }

        // 8) Total des commandes d'un client par vendeur
        var commandesParClient = from commande in dataContext.PPCommandes
                                 orderby commande.NoClient
                                 group commande by commande.PPClients;

        foreach (var commandesClient in commandesParClient)
        {
            PPClients client = commandesClient.Key;
            var commandesParVendeur = from commande in commandesClient
                                      orderby commande.NoVendeur
                                      group commande by commande.PPVendeurs;

            bool premierVendeur = true;
            foreach (var commandesVendeur in commandesParVendeur)
            {
                PPVendeurs vendeur = commandesVendeur.Key;
                decimal montantTotalCommandes = 0;
                DateTime dateDeniereCommande = DateTime.MinValue;
                foreach (PPCommandes commande in commandesVendeur)
                {
                    montantTotalCommandes += (decimal)(commande.MontantTotAvantTaxes + commande.CoutLivraison + commande.TPS + commande.TVQ);
                    if (commande.DateCommande > dateDeniereCommande)
                        dateDeniereCommande = (DateTime)commande.DateCommande;
                }

                TableRow row = new TableRow();
                TableCell cell;
                if (premierVendeur)
                {
                    cell = new TableCell();
                    cell.RowSpan = commandesParVendeur.Count();
                    cell.Text = client.NoClient.ToString();
                    row.Cells.Add(cell);
                    cell = new TableCell();
                    cell.RowSpan = commandesParVendeur.Count();
                    cell.Text = client.Prenom + " " + client.Nom;
                    row.Cells.Add(cell);
                }
                cell = new TableCell();
                cell.Text = vendeur.NoVendeur.ToString();
                row.Cells.Add(cell);
                cell = new TableCell();
                cell.Text = vendeur.Prenom + " " + vendeur.Nom;
                row.Cells.Add(cell);
                cell = new TableCell();
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.Text = montantTotalCommandes.ToString("C", CultureInfo.CurrentCulture);
                row.Cells.Add(cell);
                cell = new TableCell();
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.Text = dateDeniereCommande.ToString("yyyy'-'MM'-'dd HH':'mm");
                row.Cells.Add(cell);
                tabTotalCommandesClientsParVendeur.Rows.Add(row);

                premierVendeur = false;
            }
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

    public void retourDashboard_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Retour");
        String url = "~/Pages/AcceuilGestionnaire.aspx?";
        Response.Redirect(url, true);
    }

    public void verifierPermissions(String typeUtilisateur)
    {
        String url = "";

        if (Session["TypeUtilisateur"] == null)
        {
            url = "~/Pages/AccueilInternaute.aspx?";
            Response.Redirect(url, true);
        }
        else if (Session["TypeUtilisateur"].ToString() != typeUtilisateur)
        {
            String type = Session["TypeUtilisateur"].ToString();
            if (type == "C")
            {
                url = "~/Pages/AccueilClient.aspx?";
            }
            else if (type == "V")
            {
                url = "~/Pages/ConnexionVendeur.aspx?";
            }
            else if (type == "G")
            {
                url = "~/Pages/AcceuilGestionnaire.aspx?";
            }
            else
            {
                url = "~/Pages/AccueilInternaute.aspx?";
            }

            Response.Redirect(url, true);
        }
    }
}