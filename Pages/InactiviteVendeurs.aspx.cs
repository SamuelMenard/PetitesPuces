using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_InactiviteVendeurs : System.Web.UI.Page
{
    private int nbMois;

    protected void Page_Load(object sender, EventArgs e)
    {
        getNbMois();
        afficherVendeursInactifs();
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        mois.SelectedValue = this.nbMois.ToString();
    }

    public void afficherVendeursInactifs()
    {
        List<PPClients> lstClientsInactifs = LibrairieLINQ.getClientsInactifsDepuis(this.nbMois);

        Panel row = LibrairieControlesDynamique.divDYN(phDynamique, "rowDemandeurs", "row");
        foreach (PPClients client in lstClientsInactifs)
        {
            long idClient = client.NoClient;
            String nomclient = client.Prenom + " " + client.Nom;
            String urlImg = "../static/images/client.png";

            // infos
            Panel colInfos = LibrairieControlesDynamique.divDYN(row, "colInfos_" + idClient, "col-md-4");
            Panel demandeBase = LibrairieControlesDynamique.divDYN(colInfos, "base_" + idClient, "panel panel-default");
            demandeBase.Style.Add("height", "150px");
            Panel demandeBody = LibrairieControlesDynamique.divDYN(demandeBase, "body_" + idClient, "panel-body");

            Panel media = LibrairieControlesDynamique.divDYN(demandeBody, "media_" + idClient, "media");
            Panel mediaLeft = LibrairieControlesDynamique.divDYN(media, "mediaLeft_" + idClient, "media-left");
            Image img = LibrairieControlesDynamique.imgDYN(mediaLeft, "img_" + idClient, urlImg, "media-object");
            img.Style.Add("width", "75px");
            Panel mediaBody = LibrairieControlesDynamique.divDYN(media, "mediaBody_" + idClient, "media-body");
            LibrairieControlesDynamique.h4DYN(mediaBody, "h4_" + idClient, nomclient);
            LibrairieControlesDynamique.pDYN(mediaBody, client.AdresseEmail);


            // btn non
            HtmlButton btnNon = LibrairieControlesDynamique.htmlbtnDYN(demandeBody, "btnNon_" + idClient, "btn btn-danger", "", "glyphicon glyphicon-remove", btnNon_click);
        }

        if (lstClientsInactifs.Count() < 1)
        {
            divMessage.Visible = true;
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
        String id = btn.ID.Replace("btnNon_", "");
        /*LibrairieLINQ.desactiverCompteClient(long.Parse(id));
        String url = "~/Pages/InactiviteClients.aspx?";
        Response.Redirect(url, true);*/
    }

    public void ddlChanged(Object sender, EventArgs e)
    {
        String url = "~/Pages/InactiviteVendeurs.aspx?NbAnnees=" + mois.SelectedValue;
        Response.Redirect(url, true);
    }

    private void getNbMois()
    {
        int n;
        if (Request.QueryString["NbAnnees"] == null || !int.TryParse(Request.QueryString["NbAnnees"], out n))
        {
            this.nbMois = 1;
        }
        else
        {
            this.nbMois = n;
        }
    }
}