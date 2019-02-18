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
        lbInscriptionVendeur.Visible = true;
        lbChangerMotPasse.Visible = true;
        lbPanier.Visible = true;
        lbInfosPersos.Visible = true;
        lbRechercheDetaillee.Visible = true;
    }

    public void affichageVendeur()
    {
        lbDeconnexion.Visible = true;
        lbInscriptionClient.Visible = true;
        lbChangerMotPasse.Visible = true;
        lbGererCommandes.Visible = true;
        lbPaniersInactifs.Visible = true;
        lbAjoutProduit.Visible = true;
        lbSupprimerProduit.Visible = true;
        lblInfoPersoVendeur.Visible = true;
    }

    public void affichageGestionnaire()
    {
        lbDeconnexion.Visible = true;
    }

    public void connexion_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/Connexion.aspx?";
        Response.Redirect(url, true);
    }

    public void deconnexion_click(Object sender, EventArgs e)
    {
        Session.RemoveAll();
        String url = "~/Pages/Connexion.aspx?";
        Response.Redirect(url, true);
    }

    public void inscriptionClient_click(Object sender, EventArgs e)
    {
        String url = "";
        if (((Control)sender).ID == "lbInscriptionClient")
            url = "~/Pages/InscriptionClientVendeur.aspx?";
        else
            url = "~/Pages/InscriptionClient.aspx?";
        Response.Redirect(url, true);
    }

    public void inscriptionVendeur_click(Object sender, EventArgs e)
    {
        String url = "";
        if (((Control)sender).ID == "lbInscriptionVendeur")
            url = "~/Pages/InscriptionVendeurClient.aspx?";
        else
            url = "~/Pages/InscriptionVendeur.aspx?";
        Response.Redirect(url, true);
    }

    public void changerMotPasse_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/ModificationMotDePasse.aspx?";
        Response.Redirect(url, true);
    }

    public void panier_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/GestionPanierCommande.aspx?";
        Response.Redirect(url, true);
    }

    public void infosPersosVendeur_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/ModificationProfilVendeur.aspx?";
        Response.Redirect(url, true);
    }

    public void infosPersos_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/SaisieProfilClient.aspx?";
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

    public void rechercheDetaillee_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/searchClient.aspx?";
        Session["NoVendeurCatalogue"] = "";
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
