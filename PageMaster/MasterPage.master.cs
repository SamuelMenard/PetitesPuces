﻿using System;
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
    }

    public void affichageVendeur()
    {
        lbDeconnexion.Visible = true;
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

    public void accueil_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/AccueilInternaute.aspx?";
        
        if (Session["TypeUtilisateur"] != null)
        {
            switch (Session["TypeUtilisateur"].ToString())
            {
                //case "C": url = "~/Pages/AccueilInternaute.aspx?"; break;
                //case "V": url = "~/Pages/AccueilInternaute.aspx?"; break;
                case "G": url = "~/Pages/AcceuilGestionnaire.aspx?"; break;
            }
        }
        Response.Redirect(url, true);
    }
}
