using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_RendreInactif : System.Web.UI.Page
{
    private String typeUtilisateurCourant;

    protected void Page_Load(object sender, EventArgs e)
    {
        verifierPermissions("G");

        Page.Title = "Désactiver un compte client ou vendeur";

        getTypeUtilisateur();

        afficherUtilisateurs();
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        typeUtilisateur.SelectedValue = this.typeUtilisateurCourant;
    }

    public void afficherUtilisateurs()
    {
        String urlImg = "";
        if (this.typeUtilisateurCourant == "C")
        {
            urlImg = "../static/images/client.png";
            List<PPClients> lstClients = LibrairieLINQ.getListeClients();
            Panel row = LibrairieControlesDynamique.divDYN(phDynamique, "row_utilisateurs", "row");
            foreach(PPClients client in lstClients)
            {
                long idClient = client.NoClient;
                String nomUtil = client.AdresseEmail;
                Panel colUser = LibrairieControlesDynamique.divDYN(row, "col_" + idClient, "col-md-2");
                Panel panelDefault = LibrairieControlesDynamique.divDYN(colUser, "", "panel panel-default");
                Panel panelHeading = LibrairieControlesDynamique.divDYN(panelDefault, "", "panel-heading");
                Panel panelBody = LibrairieControlesDynamique.divDYN(panelDefault, "", "panel-body");
                LibrairieControlesDynamique.imgDYN(panelBody, "", urlImg, "img-responsive");
                Panel panelFooter = LibrairieControlesDynamique.divDYN(panelDefault, "", "panel-footer");

                Panel rowNomBtn = LibrairieControlesDynamique.divDYN(panelFooter, "", "row");
                Panel colBtn = LibrairieControlesDynamique.divDYN(rowNomBtn, "", "col-md-4");
                Panel colNom = LibrairieControlesDynamique.divDYN(rowNomBtn, "", "col-md-8");

                HtmlButton btnNon = LibrairieControlesDynamique.htmlbtnDYN(panelHeading, "btnNonClient_" + idClient, "btn btn-danger", "", "glyphicon glyphicon-remove", btnNon_click);
                LibrairieControlesDynamique.lblDYN(panelFooter, "lbl_" + idClient, nomUtil);
            }
        }
        else if (this.typeUtilisateurCourant == "V")
        {
            urlImg = "../static/images/vendeur.jpg";
            List<PPVendeurs> lstVendeurs = LibrairieLINQ.getListeVendeurs();
            Panel row = LibrairieControlesDynamique.divDYN(phDynamique, "row_utilisateurs", "row");
            foreach (PPVendeurs vendeur in lstVendeurs)
            {
                long idVendeur = vendeur.NoVendeur;
                String nomUtil = vendeur.Prenom + " " + vendeur.Nom;
                Panel colUser = LibrairieControlesDynamique.divDYN(row, "col_" + idVendeur, "col-md-2");
                Panel panelDefault = LibrairieControlesDynamique.divDYN(colUser, "", "panel panel-default");
                Panel panelBody = LibrairieControlesDynamique.divDYN(panelDefault, "", "panel-body");
                LibrairieControlesDynamique.imgDYN(panelBody, "", urlImg, "img-responsive");
                Panel panelFooter = LibrairieControlesDynamique.divDYN(panelDefault, "", "panel-footer");

                Panel rowNomBtn = LibrairieControlesDynamique.divDYN(panelFooter, "", "row");
                Panel colBtn = LibrairieControlesDynamique.divDYN(rowNomBtn, "", "col-md-4");
                Panel colNom = LibrairieControlesDynamique.divDYN(rowNomBtn, "", "col-md-8");

                HtmlButton btnNon = LibrairieControlesDynamique.htmlbtnDYN(colBtn, "btnNonVendeur_" + idVendeur, "btn btn-danger", "", "glyphicon glyphicon-remove", btnNon_click);
                LibrairieControlesDynamique.lblDYN(colNom, "lbl_" + idVendeur, nomUtil);
            }
        }


        
        

    }

    public void retourDashboard_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Retour");
        String url = "~/Pages/AcceuilGestionnaire.aspx?";
        Response.Redirect(url, true);
    }

    public void btnNon_click(Object sender, EventArgs e)
    {
        HtmlButton btn = (HtmlButton)sender;
        String id = "";

        if (this.typeUtilisateurCourant == "C")
        {
            id = btn.ID.Replace("btnNonClient_", "");
            LibrairieLINQ.desactiverCompteClient(long.Parse(id));
        }
        else if (this.typeUtilisateurCourant == "V")
        {
            id = btn.ID.Replace("btnNonVendeur_", "");
            LibrairieLINQ.desactiverCompteVendeur(long.Parse(id));
        }

        String url = "~/Pages/RendreInactif.aspx?TypeUtilisateur=" + typeUtilisateur.SelectedValue;
        Response.Redirect(url, true);
    }

    public void type_changed(Object sender, EventArgs e)
    {
        String url = "~/Pages/RendreInactif.aspx?TypeUtilisateur=" + typeUtilisateur.SelectedValue;
        Response.Redirect(url, true);
    }

    private void getTypeUtilisateur()
    {
        if (Request.QueryString["TypeUtilisateur"] == null)
        {
            this.typeUtilisateurCourant = "C";
        }
        else
        {
            if (Request.QueryString["TypeUtilisateur"] != "C" && Request.QueryString["TypeUtilisateur"] != "V")
            {
                this.typeUtilisateurCourant = "C";
            }
            else
            {
                this.typeUtilisateurCourant = Request.QueryString["TypeUtilisateur"];
            }
            
        }
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