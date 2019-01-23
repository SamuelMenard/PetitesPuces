using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_InactiviteVendeurs : System.Web.UI.Page
{
    private int nbAnnees;

    protected void Page_Load(object sender, EventArgs e)
    {
        getNbMois();
        afficherVendeursInactifs(nbAnnees);
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        annee.SelectedValue = this.nbAnnees.ToString();
    }

    public void afficherVendeursInactifs(int nbAnnees)
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

        List<String> lstClients2 = new List<string>();
        lstClients2.Add("Raphaël Benoït");
        lstClients2.Add("Raphaël Benoït");
        lstClients2.Add("Samuel Ménard");

        List<String> lstClients = new List<string>();
        if (nbAnnees == 1) { lstClients = lstClients1; }
        else if (nbAnnees == 2) { lstClients = lstClients2; }

        int noVendeur = 0;

        for (int i = 0; i < lstClients.Count; i++)
        {
            noVendeur++;
            String nomVendeur = lstClients[i];
            String urlImg = "../static/images/vendeur.jpg";

            Panel row = LibrairieControlesDynamique.divDYN(phDynamique, "row_" + noVendeur, "row");

            // infos
            Panel colInfos = LibrairieControlesDynamique.divDYN(row, "colInfos_" + noVendeur, "col-md-10");
            Panel demandeBase = LibrairieControlesDynamique.divDYN(colInfos, "base_" + noVendeur, "panel panel-default");
            Panel demandeBody = LibrairieControlesDynamique.divDYN(demandeBase, "body_" + noVendeur, "panel-body");

            Panel media = LibrairieControlesDynamique.divDYN(demandeBody, "media_" + noVendeur, "media");
            Panel mediaLeft = LibrairieControlesDynamique.divDYN(media, "mediaLeft_" + noVendeur, "media-left");
            Image img = LibrairieControlesDynamique.imgDYN(mediaLeft, "img_" + noVendeur, urlImg, "media-object");
            img.Style.Add("width", "75px");
            Panel mediaBody = LibrairieControlesDynamique.divDYN(media, "mediaBody_" + noVendeur, "media-body");
            LibrairieControlesDynamique.h4DYN(mediaBody, nomVendeur);
            LibrairieControlesDynamique.pDYN(mediaBody, "No. " + noVendeur);

            Panel colSupprimer = LibrairieControlesDynamique.divDYN(row, "colSupprimer" + noVendeur, "col-sm-2");
            // btn non
            HtmlButton btnSupprimer = LibrairieControlesDynamique.htmlbtnDYN(colSupprimer, "btnSupprimer_" + noVendeur, "btn btn-danger", "", "glyphicon glyphicon-remove", btnSupprimer_click);
            btnSupprimer.Style.Add("height", "105px");
        }

        // si il n'y a pas de clients dans la liste
        if (lstClients.Count == 0)
        {
            Panel alert = LibrairieControlesDynamique.divDYN(phDynamique, "", "alert alert-warning");
            LibrairieControlesDynamique.lblDYN(alert, "", "Il n'y a pas de clients inactif depuis plus de " + nbAnnees + " ans");
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
        String url = "~/Pages/InactiviteVendeurs.aspx?NbAnnees=" + annee.SelectedValue;
        Response.Redirect(url, true);
    }

    private void getNbMois()
    {
        int n;
        if (Request.QueryString["NbAnnees"] == null || !int.TryParse(Request.QueryString["NbAnnees"], out n))
        {
            this.nbAnnees = 1;
        }
        else
        {
            this.nbAnnees = n;
        }
    }
}