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
    private List<CheckBox> lstCheckBox = new List<CheckBox>();

    protected void Page_Load(object sender, EventArgs e)
    {
        verifierPermissions("G");

        Page.Title = "Vendeurs inactifs";

        getNbMois();
        afficherVendeursInactifs();
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        mois.SelectedValue = this.nbMois.ToString();
    }

    public void afficherVendeursInactifs()
    {
        List<PPVendeurs> lstVendeursInactifs = LibrairieLINQ.getVendeursInactifsDepuis(this.nbMois);

        Panel row = LibrairieControlesDynamique.divDYN(phDynamique, "rowDemandeurs", "row");
        foreach (PPVendeurs Vendeur in lstVendeursInactifs)
        {
            long idVendeur = Vendeur.NoVendeur;
            String nomVendeur = Vendeur.Prenom + " " + Vendeur.Nom;
            String urlImg = "../static/images/vendeur.jpg";

            // infos
            Panel colInfos = LibrairieControlesDynamique.divDYN(row, "colInfos_" + idVendeur, "col-md-4");
            Panel demandeBase = LibrairieControlesDynamique.divDYN(colInfos, "base_" + idVendeur, "panel panel-default");
            demandeBase.Style.Add("height", "170px");
            Panel demandeBody = LibrairieControlesDynamique.divDYN(demandeBase, "body_" + idVendeur, "panel-body");
            Panel demandeFooter = LibrairieControlesDynamique.divDYN(demandeBase, "footer_" + idVendeur, "panel-footer");

            Panel media = LibrairieControlesDynamique.divDYN(demandeBody, "media_" + idVendeur, "media");
            Panel mediaLeft = LibrairieControlesDynamique.divDYN(media, "mediaLeft_" + idVendeur, "media-left");
            Image img = LibrairieControlesDynamique.imgDYN(mediaLeft, "img_" + idVendeur, urlImg, "media-object");
            img.Style.Add("width", "75px");
            Panel mediaBody = LibrairieControlesDynamique.divDYN(media, "mediaBody_" + idVendeur, "media-body");
            LibrairieControlesDynamique.h4DYN(mediaBody, "h4_" + idVendeur, nomVendeur);
            LibrairieControlesDynamique.pDYN(mediaBody, Vendeur.AdresseEmail);


            // btn non
            Panel divBotRow = LibrairieControlesDynamique.divDYN(demandeFooter, "", "row");
            Panel divColBotCheck = LibrairieControlesDynamique.divDYN(divBotRow, "", "col-md-2");
            CheckBox cb = LibrairieControlesDynamique.cb(divColBotCheck, "checkbox_" + idVendeur, "", "checkmark");
            lstCheckBox.Add(cb);

            Panel divBotColBtn = LibrairieControlesDynamique.divDYN(divBotRow, "", "col-md-2");
            HtmlButton btnNon = LibrairieControlesDynamique.htmlbtnDYN(divBotColBtn, "btnNon_" + idVendeur, "btn btn-danger", "", "glyphicon glyphicon-remove", btnNon_click);
        }

        if (lstVendeursInactifs.Count() < 1)
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
        LibrairieLINQ.desactiverCompteVendeur(long.Parse(id));
        String url = "~/Pages/InactiviteVendeurs.aspx?NbMois=" + mois.SelectedValue;
        Response.Redirect(url, true);
    }

    public void selectiontout_click(Object sender, EventArgs e)
    {
        Button btn = (Button)sender;

        if (btn.Text == "Sélectionner tout")
        {
            btn.Text = "Désélectionner tout";
            foreach (CheckBox c in lstCheckBox)
            {
                c.Checked = true;
            }
        }
        else
        {
            btn.Text = "Sélectionner tout";
            foreach (CheckBox c in lstCheckBox)
            {
                c.Checked = false;
            }
        }

    }

    public void supprimerselections_click(Object sender, EventArgs e)
    {
        foreach (CheckBox c in lstCheckBox)
        {
            if (c.Checked)
            {
                String idVendeur = c.ID.Replace("checkbox_", "");
                LibrairieLINQ.desactiverCompteVendeur(long.Parse(idVendeur));
            }
        }

        String url = "~/Pages/InactiviteVendeurs.aspx?NbMois=" + mois.SelectedValue;
        Response.Redirect(url, true);
    }

    public void ddlChanged(Object sender, EventArgs e)
    {
        String url = "~/Pages/InactiviteVendeurs.aspx?NbMois=" + mois.SelectedValue;
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