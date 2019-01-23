using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_InactiviteClients : System.Web.UI.Page
{

    private int nbMois;

    protected void Page_Load(object sender, EventArgs e)
    {
        getNbMois();
        afficherClientsInactifs(nbMois);
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        mois.SelectedValue = this.nbMois.ToString();
    }

    public void afficherClientsInactifs(int nbMois)
    {
        List<String> lstClients1 = new List<string>();
        lstClients1.Add("Raphaël Benoït");
        lstClients1.Add("Samuel Ménard");
        lstClients1.Add("Olivier Brodeur");
        lstClients1.Add("Francis Perreault");
        lstClients1.Add("Raphaël Benoït");
        lstClients1.Add("Raphaël Benoït");
        lstClients1.Add("Samuel Ménard");
        lstClients1.Add("Raphaël Benoït");

        List<String> lstClients3 = new List<string>();
        lstClients3.Add("Raphaël Benoït");
        lstClients3.Add("Raphaël Benoït");
        lstClients3.Add("Samuel Ménard");

        List<String> lstClients24 = new List<string>();
        lstClients24.Add("Raphaël Benoït");

        List<String> lstClients = new List<string>();
        if (nbMois == 1) { lstClients = lstClients1; }
        else if (nbMois >= 3 && nbMois < 24) { lstClients = lstClients3; }
        else if (nbMois == 24) { lstClients = lstClients24; }

        int noClient = 0;

        for (int i = 0; i < lstClients.Count; i++)
        {
            noClient++;
            String nomClient = lstClients[i];
            String urlImg = "../static/images/client.png";

            Panel row = LibrairieControlesDynamique.divDYN(phDynamique, "row_" + noClient, "row");

            // infos
            Panel colInfos = LibrairieControlesDynamique.divDYN(row, "colInfos_" + noClient, "col-md-10");
            Panel demandeBase = LibrairieControlesDynamique.divDYN(colInfos, "base_" + noClient, "panel panel-default");
            Panel demandeBody = LibrairieControlesDynamique.divDYN(demandeBase, "body_" + noClient, "panel-body");

            Panel media = LibrairieControlesDynamique.divDYN(demandeBody, "media_" + noClient, "media");
            Panel mediaLeft = LibrairieControlesDynamique.divDYN(media, "mediaLeft_" + noClient, "media-left");
            Image img = LibrairieControlesDynamique.imgDYN(mediaLeft, "img_" + noClient, urlImg, "media-object");
            img.Style.Add("width", "75px");
            Panel mediaBody = LibrairieControlesDynamique.divDYN(media, "mediaBody_" + noClient, "media-body");
            LibrairieControlesDynamique.h4DYN(mediaBody, nomClient);
            LibrairieControlesDynamique.pDYN(mediaBody, "No. " + noClient);
            
            Panel colSupprimer = LibrairieControlesDynamique.divDYN(row, "colSupprimer" + noClient, "col-sm-2");
            // btn non
            HtmlButton btnSupprimer = LibrairieControlesDynamique.htmlbtnDYN(colSupprimer, "btnSupprimer_" + noClient, "btn btn-danger", "", "glyphicon glyphicon-remove", btnSupprimer_click);
            btnSupprimer.Style.Add("height", "105px");
        }

        // si il n'y a pas de clients dans la liste
        if (lstClients.Count == 0)
        {
            Panel alert = LibrairieControlesDynamique.divDYN(phDynamique, "", "alert alert-warning");
            LibrairieControlesDynamique.lblDYN(alert, "", "Il n'y a pas de clients inactif depuis plus de " + nbMois + " mois");
        }
    }

    public void retourDashboard_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Retour");
        String url = "~/Pages/AcceuilGestionnaire.aspx?";
        Response.Redirect(url, true);
    }

    public void btnSupprimer_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Supprimer");
    }

    public void ddlChanged(Object sender, EventArgs e)
    {
        String url = "~/Pages/InactiviteClients.aspx?NbMois=" + mois.SelectedValue;
        Response.Redirect(url, true);
    }

    private void getNbMois()
    {
        int n;
        if (Request.QueryString["NbMois"] == null || !int.TryParse(Request.QueryString["NbMois"], out n))
        {
            this.nbMois = 1;
        }
        else
        {
            this.nbMois = n;
        }
    }
}