using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Connexion : System.Web.UI.Page
{
    private BD6B8_424SEntities dataContext = new BD6B8_424SEntities();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void inscriptionClient_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/InscriptionClient.aspx?";
        Response.Redirect(url, true);
    }

    public void accueil_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/AccueilInternaute.aspx?";
        Response.Redirect(url, true);
    }

    public void inscriptionVendeur_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/InscriptionVendeur.aspx?";
        Response.Redirect(url, true);
    }

    public void connexion_click(Object sender, EventArgs e)
    {
        var clients = dataContext.PPClients;

        String courriel = tbCourriel.Text;
        String MDP = tbMDP.Text;

        if (clients.Where(client => client.AdresseEmail == courriel).Any())
        {
            Session["TypeUtilisateur"] = "C";
            String url = "~/Pages/AccueilClient.aspx?";
            Response.Redirect(url, true);

        }
        else if (courriel == "vendeur")
        {
            Session["TypeUtilisateur"] = "V";
            String url = "~/Pages/ConnexionVendeur.aspx?";
            Response.Redirect(url, true);

        }
        else if (courriel == "gestionnaire")
        {
            Session["TypeUtilisateur"] = "G";
            String url = "~/Pages/AcceuilGestionnaire.aspx?";
            Response.Redirect(url, true);
        }
        
    }
}