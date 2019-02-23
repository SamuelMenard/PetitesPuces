using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VisualiserRedevances : System.Web.UI.Page
{
    private String trieVendeur;

    protected void Page_Load(object sender, EventArgs e)
    {
        verifierPermissions("G");

        Page.Title = "Encaisser les redevances";

        getTrieVendeur();
        remplirTableau();
    }

    public void remplirTableau()
    {

        Panel panelDefault = LibrairieControlesDynamique.divDYN(phTab, "", "panel panel-default");
        Panel panelBody = LibrairieControlesDynamique.divDYN(panelDefault, "", "panel-body");
        Panel row2 = LibrairieControlesDynamique.divDYN(panelBody, "", "row");
        Panel col2 = LibrairieControlesDynamique.divDYN(row2, "", "col-md-12");

        List<PPVendeurs> lstVendeurs = LibrairieLINQ.getListeVendeursTrie(this.trieVendeur);
        Table table = LibrairieControlesDynamique.tableDYN(col2, "", "table table-striped");
        TableRow trHEADER = LibrairieControlesDynamique.trDYN(table);
        LibrairieControlesDynamique.lblDYN(LibrairieControlesDynamique.thDYN(trHEADER, "", ""), "", "Courriel");
        LibrairieControlesDynamique.lblDYN(LibrairieControlesDynamique.thDYN(trHEADER, "", ""), "", "Nom d'affaire");
        LibrairieControlesDynamique.lblDYN(LibrairieControlesDynamique.thDYN(trHEADER, "", ""), "", "Date de cration");
        LibrairieControlesDynamique.lblDYN(LibrairieControlesDynamique.thDYN(trHEADER, "", ""), "", "Taux");
        LibrairieControlesDynamique.lblDYN(LibrairieControlesDynamique.thDYN(trHEADER, "", ""), "", "Redevances");
        LibrairieControlesDynamique.lblDYN(LibrairieControlesDynamique.thDYN(trHEADER, "", ""), "", "Encaisser");

        foreach (PPVendeurs vendeur in lstVendeurs)
        {
            List<PPHistoriquePaiements> lstHisto = LibrairieLINQ.getHistoriquePaiementVendeurs(vendeur.NoVendeur);

            TableRow tr = LibrairieControlesDynamique.trDYN(table);
            LibrairieControlesDynamique.lblDYN(LibrairieControlesDynamique.tdDYN(tr, "", ""), "", vendeur.AdresseEmail);
            LibrairieControlesDynamique.lblDYN(LibrairieControlesDynamique.tdDYN(tr, "", ""), "", vendeur.NomAffaires);
            LibrairieControlesDynamique.lblDYN(LibrairieControlesDynamique.tdDYN(tr, "", ""), "", vendeur.DateCreation.Value.ToString("yyyy'-'MM'-'dd HH':'mm"));
            LibrairieControlesDynamique.lblDYN(LibrairieControlesDynamique.tdDYN(tr, "", ""), "", (vendeur.Pourcentage*100).ToString() + "%");
            LibrairieControlesDynamique.lblDYN(LibrairieControlesDynamique.tdDYN(tr, "", ""), "", Decimal.Round((Decimal)vendeur.PPCommandes.Where(c => c.Statut.Equals("1")).Sum(c => c.MontantTotAvantTaxes * vendeur.Pourcentage), 2).ToString("C", CultureInfo.CurrentCulture));
            LibrairieControlesDynamique.btnDYN(LibrairieControlesDynamique.tdDYN(tr, "", ""), "btnEncaisser_" + vendeur.NoVendeur, "btn btn-success", "Encaisser", encaisser_click);
        }
    }

    private void getTrieVendeur()
    {
        if (Request.QueryString["TrieVendeur"] == null)
        {
            this.trieVendeur = "dateDecroissant";
        }
        else
        {
            this.trieVendeur = Request.QueryString["TrieVendeur"];
        }
    }

    public void retourDashboard_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Retour");
        String url = "~/Pages/AcceuilGestionnaire.aspx?";
        Response.Redirect(url, true);
    }

    public void dateCroissant_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/VisualiserRedevances.aspx?TrieVendeur=dateCroissant";
        Response.Redirect(url, true);
    }

    public void dateDecroissant_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/VisualiserRedevances.aspx?TrieVendeur=dateDecroissant";
        Response.Redirect(url, true);
    }

    public void redevancesCroissant_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/VisualiserRedevances.aspx?TrieVendeur=redevancesCroissant";
        Response.Redirect(url, true);
    }

    public void redevancesDecroissant_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/VisualiserRedevances.aspx?TrieVendeur=redevancesDecroissant";
        Response.Redirect(url, true);
    }

    public void encaisser_click(Object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        String id = btn.ID.Replace("btnEncaisser_", "");
        long lgId = long.Parse(id);

        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        PPVendeurs v = dataContext.PPVendeurs.Where(ven => ven.NoVendeur.Equals(lgId)).First();
        foreach(PPCommandes commande in v.PPCommandes)
        {
            commande.Statut = "2";
        }
        dataContext.SaveChanges();

        String url = "~/Pages/VisualiserRedevances.aspx?TrieVendeur=" + this.trieVendeur;
        Response.Redirect(url, true);
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