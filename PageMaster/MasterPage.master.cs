using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class PageMaster_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["TypeUtilisateur"] != null)
        {
            switch (Session["TypeUtilisateur"].ToString())
            {
                case "C": affichageClient(); break;
                case "V": affichageVendeur(); break;
                case "G": affichageGestionnaire(); break;
            }
        }
        else
        {
            affichageAnonyme();
        }
        
    }

    public void affichageAnonyme()
    {
        lbConnexion.Visible = true;
        lbInscription.Visible = true;
    }

    public void affichageClient()
    {
        lbDeconnexion.Visible = true;
        lbPanier.Visible = true;
        lbInscriptionVendeur.Visible = true;
        lbInfosPersos.Visible = true;
    }

    public void affichageVendeur()
    {
        lbDeconnexion.Visible = true;
        lbGererCommandes.Visible = true;
        lbPaniersInactifs.Visible = true;
        lbAjoutProduit.Visible = true;
        lbSupprimerProduit.Visible = true;
    }

    public void affichageGestionnaire()
    {
        lbDeconnexion.Visible = true;
    }

    public void inscription_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/Connexion.aspx?";
        Response.Redirect(url, true);
    }

    public void connexion_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/Connexion.aspx?";
        Response.Redirect(url, true);
    }

    public void deconnexion_click(Object sender, EventArgs e)
    {
        Session.Remove("TypeUtilisateur");
        String url = "~/Pages/Connexion.aspx?";
        Response.Redirect(url, true);
    }

    public void panier_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/GestionPanierCommande.aspx?";
        Response.Redirect(url, true);
    }

    public void inscriptionVendeur_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/InscriptionVendeur.aspx?";
        Response.Redirect(url, true);
    }

    public void infosPersos_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/SaisieModificationProfil.aspx?";
        Response.Redirect(url, true);
    }

    public void gererCommandes_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/GererCommandes.aspx?";
        Response.Redirect(url, true);
    }

    public void paniersInactifs_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/GererPanierInactifs.aspx?";
        Response.Redirect(url, true);
    }

    public void ajoutProduit_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/InscriptionProduit.aspx?";
        Response.Redirect(url, true);
    }

    public void supprimerProduit_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/SuppressionProduit.aspx?";
        Response.Redirect(url, true);
    }

    public void accueil_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/AccueilInternaute.aspx?";
        
        if (Session["TypeUtilisateur"] != null)
        {
            switch (Session["TypeUtilisateur"].ToString())
            {
                case "C": url = "~/Pages/AccueilClient.aspx?"; break;
                case "V": url = "~/Pages/ConnexionVendeur.aspx?"; break;
                case "G": url = "~/Pages/AcceuilGestionnaire.aspx?"; break;
            }
        }
        Response.Redirect(url, true);
    }
}
