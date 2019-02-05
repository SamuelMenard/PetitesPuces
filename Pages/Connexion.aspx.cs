using System;
using System.Collections.Generic;

public partial class Pages_Connexion : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void btnConnexion_click(Object sender, EventArgs e)
    {
        String typeUtilisateur = ddlTypeUtilisateur.SelectedValue.ToString();
        String courriel = tbCourriel.Text;
        String MDP = tbMDP.Text;

        bool verdictConnexion = false;
        String url = "";

        int codeErreur = LibrairieLINQ.connexionOK(courriel, MDP, typeUtilisateur);

        if (typeUtilisateur == "C" && codeErreur == 400)
        {
            verdictConnexion = true;
            List<String> lstInfos = LibrairieLINQ.infosBaseClient(courriel);
            Session["TypeUtilisateur"] = "C";
            Session["NoClient"] = lstInfos[0];
            Session["Nom"] = lstInfos[1];
            Session["Prenom"] = lstInfos[2];
            Session["Courriel"] = lstInfos[3];
            url = "~/Pages/AccueilClient.aspx?";

        }
        else if (typeUtilisateur == "V" && codeErreur == 400)
        {
            verdictConnexion = true;
            List<String> lstInfos = LibrairieLINQ.infosBaseVendeur(courriel);
            Session["TypeUtilisateur"] = "V";
            Session["NoVendeur"] = lstInfos[0];
            Session["NomAffaire"] = lstInfos[1];
            Session["Nom"] = lstInfos[2];
            Session["Prenom"] = lstInfos[3];
            Session["Courriel"] = lstInfos[4];
            url = "~/Pages/ConnexionVendeur.aspx?";

        }
        else if (typeUtilisateur == "G" && codeErreur == 400)
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

            alert_erreur.Visible = true;
            if (codeErreur == 401) { lblMessageErreur.Text = "Courriel ou mot de passe incorrect"; }
            else if (codeErreur == 402) { lblMessageErreur.Text = "Le compte a été désactivé"; }
        }
        
    }
}