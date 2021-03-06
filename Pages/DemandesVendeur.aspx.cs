﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_DemandesVendeur : System.Web.UI.Page
{

    private String notification;

    protected void Page_Load(object sender, EventArgs e)
    {
        verifierPermissions("G");

        Page.Title = "Demandes de vendeurs";

        Page.MaintainScrollPositionOnPostBack = true;
        getNotification();

        if (notification == "accepte")
        {
            divAccepte.Visible = true;
        }
        else if (notification == "refuse")
        {
            divRefuse.Visible = true;
        }
        afficherDemandes();
    }
    

    public void afficherDemandes()
    {
        List<PPVendeurs> lstVendeurs = LibrairieLINQ.getNouvellesDemandesVendeur();

        Panel row = LibrairieControlesDynamique.divDYN(phDynamique, "rowDemandeurs", "row");
        foreach(PPVendeurs vendeur in lstVendeurs)
        {
            long idVendeur = vendeur.NoVendeur;
            String nomVendeur = vendeur.Prenom + " " + vendeur.Nom;
            String nomEntreprise = vendeur.NomAffaires;
            String date = vendeur.DateCreation.ToString();
            String urlImg = "../static/images/user-management.png";

            

            // infos
            Panel colInfos = LibrairieControlesDynamique.divDYN(row, "colInfos_" + idVendeur, "col-md-4");
            Panel demandeBase = LibrairieControlesDynamique.divDYN(colInfos, "base_" + idVendeur, "panel panel-default");
            demandeBase.Style.Add("height", "200px");
            Panel demandeBody = LibrairieControlesDynamique.divDYN(demandeBase, "body_" + idVendeur, "panel-body");
            Panel demandeFooter = LibrairieControlesDynamique.divDYN(demandeBase, "footer_" + idVendeur, "panel-footer");

            Panel media = LibrairieControlesDynamique.divDYN(demandeBody, "media_" + idVendeur, "media");
            Panel mediaLeft = LibrairieControlesDynamique.divDYN(media, "mediaLeft_" + idVendeur, "media-left");
            Image img = LibrairieControlesDynamique.imgDYN(mediaLeft, "img_" + idVendeur, urlImg, "media-object");
            img.Style.Add("width", "75px");
            Panel mediaBody = LibrairieControlesDynamique.divDYN(media, "mediaBody_" + idVendeur, "media-body");
            LibrairieControlesDynamique.h4DYN(mediaBody, "h4_"+ idVendeur, nomVendeur);
            LibrairieControlesDynamique.pDYN(mediaBody, nomEntreprise);
            LibrairieControlesDynamique.pDYN(mediaBody, date);

            // boutons plus de détails
            HtmlButton btnPlusDetails = LibrairieControlesDynamique.htmlbtnDYN(demandeFooter, "btnDetails_" + idVendeur, "btn btn-info", "Plus de détails", "glyphicon glyphicon-info-sign", plusDetails_click);

            // btn oui
            //HtmlButton btnOui = LibrairieControlesDynamique.htmlbtnDYN(demandeFooter, "btnOui_" + idVendeur, "btn btn-success", "", "glyphicon glyphicon-ok", btnOui_click);

            // LibrairieControlesDynamique.spaceDYN(demandeFooter);

            // btn non
            // HtmlButton btnNon = LibrairieControlesDynamique.htmlbtnDYN(demandeFooter, "btnNon_" + idVendeur, "btn btn-danger", "", "glyphicon glyphicon-remove", btnNon_click);
        }

        if (lstVendeurs.Count() < 1)
        {
            divMessage.Visible = true;
        }
        
        

    }

    public void plusDetails_click(Object sender, EventArgs e)
    {
        HtmlButton btn = (HtmlButton)sender;
        String id = btn.ID.Replace("btnDetails_", "");
        String url = "~/Pages/DetailsDemandeVendeur.aspx?NoVendeur=" + id;
        Response.Redirect(url, true);
    }

    public void retourDashboard_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Retour");
        String url = "~/Pages/AcceuilGestionnaire.aspx?";
        Response.Redirect(url, true);
    }

    private void getNotification()
    {
        if (Request.QueryString["Notification"] == null)
        {
            this.notification = "";
        }
        else
        {
            this.notification = Request.QueryString["Notification"];
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


