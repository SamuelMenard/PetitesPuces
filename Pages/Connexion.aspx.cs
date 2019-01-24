using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Connexion : System.Web.UI.Page
{

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
        Session.RemoveAll();
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
        String typeUtilisateur = ddlTypeUtilisateur.SelectedValue.ToString();
        String courriel = tbCourriel.Text;
        String MDP = tbMDP.Text;

        bool verdictConnexion = false;
        String url = "";

        if (typeUtilisateur == "C" && LibrairieLINQ.connexionOK(courriel, MDP, typeUtilisateur))
        {
            verdictConnexion = true;
            Session["TypeUtilisateur"] = "C";
            url = "~/Pages/AccueilClient.aspx?";

        }
        else if (typeUtilisateur == "V" && LibrairieLINQ.connexionOK(courriel, MDP, typeUtilisateur))
        {
            verdictConnexion = true;
            List<String> lstInfos = LibrairieLINQ.infosBaseVendeur(courriel);
            Session["TypeUtilisateur"] = "V";
            Session["NoVendeur"] = lstInfos[0];
            Session["NomAffaire"] = lstInfos[1];
            Session["Nom"] = lstInfos[2];
            Session["Prenom"] = lstInfos[3];
            url = "~/Pages/ConnexionVendeur.aspx?";

        }
        else if (typeUtilisateur == "G" && LibrairieLINQ.connexionOK(courriel, MDP, typeUtilisateur))
        {
            verdictConnexion = true;
            Session["TypeUtilisateur"] = "G";
            url = "~/Pages/AcceuilGestionnaire.aspx?";
        }

        if (verdictConnexion)
        {
            Response.Redirect(url, true);
        }
        else
        {
            tbCourriel.CssClass = "form-control erreur";
            tbMDP.CssClass = "form-control erreur";
        }
        
    }
}