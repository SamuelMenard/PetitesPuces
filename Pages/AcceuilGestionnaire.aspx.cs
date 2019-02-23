using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_AcceuilGestionnaire : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        verifierPermissions("G");

        Page.Title = "Accueil";
    }

    public void nouvellesDemandes_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Nouvelle demande");
        String url = "~/Pages/DemandesVendeur.aspx?";
        Response.Redirect(url, true);
    }

    public void inactiviteClients_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/InactiviteClients.aspx?";
        Response.Redirect(url, true);
    }

    public void inactiviteVendeurs_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/InactiviteVendeurs.aspx?";
        Response.Redirect(url, true);
    }

    public void rendreInactif_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/RendreInactif.aspx?";
        Response.Redirect(url, true);
    }

    public void stats_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/Statistiques.aspx?";
        Response.Redirect(url, true);
    }

    public void redevances_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/GestionRedevances.aspx?";
        Response.Redirect(url, true);
    }

    public void vis_redevances_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/VisualiserRedevances.aspx?";
        Response.Redirect(url, true);
    }

    public void email_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/BoiteMessagerie.aspx?";
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