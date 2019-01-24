using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_RendreInactif : System.Web.UI.Page
{
    private String typeUtilisateurCourant;
    private String recherche;

    protected void Page_Load(object sender, EventArgs e)
    {
        getTypeUtilisateur();
        getRecherche();

        afficherUtilisateurs();
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        typeUtilisateur.SelectedValue = this.typeUtilisateurCourant;
        tbSearch.Text = this.recherche;
    }

    public void afficherUtilisateurs()
    {
        List<String> lstClients = new List<string>();
        lstClients.Add("Louis Porte");
        lstClients.Add("Louis Parte");
        lstClients.Add("Louis Papa");
        lstClients.Add("Louis Blou");
        lstClients.Add("Louis Blusse");
        lstClients.Add("Louis Ménard");
        lstClients.Add("Louis Lapierre");
        lstClients.Add("Louis Gagner");
        lstClients.Add("John Porte");
        lstClients.Add("Jacques Porte");
        lstClients.Add("George Porte");

        List<String> lstVendeurs = new List<string>();
        lstVendeurs.Add("Antoine Laroche");
        lstVendeurs.Add("Antoine Parle");
        lstVendeurs.Add("Antoine Meloche");
        lstVendeurs.Add("Jeff Meloche");
        lstVendeurs.Add("Lolipop Meloche");

        String urlImg = "";

        List<String> lstUtilisateurs = new List<string>();
        if (this.typeUtilisateurCourant == "C") { lstUtilisateurs = lstClients; urlImg = "../static/images/client.png"; }
        else if (this.typeUtilisateurCourant == "V") { lstUtilisateurs = lstVendeurs; urlImg = "../static/images/vendeur.jpg"; }


        Panel row = LibrairieControlesDynamique.divDYN(phDynamique, "", "row");
        for (int i = 0; i < lstUtilisateurs.Count; i++)
        {
            String nomUtil = lstUtilisateurs[i];
            bool afficher = true;

            if (this.recherche != "")
            {
                afficher = nomUtil.Contains(this.recherche);
            }

            if (afficher)
            {
                Panel colUser = LibrairieControlesDynamique.divDYN(row, "", "col-md-2");
                Panel panelDefault = LibrairieControlesDynamique.divDYN(colUser, "", "panel panel-default");
                Panel panelBody = LibrairieControlesDynamique.divDYN(panelDefault, "", "panel-body");
                LibrairieControlesDynamique.imgDYN(panelBody, "", urlImg, "img-responsive");
                Panel panelFooter = LibrairieControlesDynamique.divDYN(panelDefault, "", "panel-footer");
                LibrairieControlesDynamique.lblDYN(panelFooter, "", nomUtil);
            }

        }
        

    }

    public void retourDashboard_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Retour");
        String url = "~/Pages/AcceuilGestionnaire.aspx?";
        Response.Redirect(url, true);
    }

    public void recherche_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Retour");
        String url = "~/Pages/RendreInactif.aspx?Recherche=" + tbSearch.Text + "&TypeUtilisateur=" + typeUtilisateur.SelectedValue;
        Response.Redirect(url, true);
    }

    public void type_changed(Object sender, EventArgs e)
    {
        String url = "~/Pages/RendreInactif.aspx?Recherche=" + tbSearch.Text + "&TypeUtilisateur=" + typeUtilisateur.SelectedValue;
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

    private void getRecherche()
    {
        if (Request.QueryString["Recherche"] == null)
        {
            this.recherche = "";
        }
        else
        {
            this.recherche = Request.QueryString["Recherche"];
        }
    }
}