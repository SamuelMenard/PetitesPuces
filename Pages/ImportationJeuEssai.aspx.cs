using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_ImportationJeuEssai : System.Web.UI.Page
{
    private BD6B8_424SEntities dbContext = new BD6B8_424SEntities();

    protected void ajouteRangeeTabEtatTables(string strNomTable, bool binVide)
    {
        TableRow row = LibrairieControlesDynamique.trDYN(tabEtatTables);
        TableCell cell = LibrairieControlesDynamique.tdDYN(row, "", "");
        cell.Text = strNomTable;
        cell = LibrairieControlesDynamique.tdDYN(row, "", "");
        cell.Text = binVide ? "Vide" : "Contient des données";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!dbContext.PPArticlesEnPanier.Any() && !dbContext.PPCategories.Any() &&
            !dbContext.PPClients.Any() && !dbContext.PPCommandes.Any() &&
            !dbContext.PPDetailsCommandes.Any() && !dbContext.PPGestionnaires.Any() &&
            !dbContext.PPHistoriquePaiements.Any() && !dbContext.PPPoidsLivraisons.Any() &&
            !dbContext.PPProduits.Any() && !dbContext.PPTaxeFederale.Any() &&
            !dbContext.PPTaxeProvinciale.Any() && !dbContext.PPTypesLivraison.Any() &&
            !dbContext.PPTypesPoids.Any() && !dbContext.PPVendeurs.Any() &&
            !dbContext.PPVendeursClients.Any())
        {
            panImporterDonnees.Visible = true;
        }
        else
        {
            
            ajouteRangeeTabEtatTables("PPDetailsCommandes", !dbContext.PPDetailsCommandes.Any());
            ajouteRangeeTabEtatTables("PPCommandes", !dbContext.PPCommandes.Any());
            ajouteRangeeTabEtatTables("PPPoidsLivraisons", !dbContext.PPPoidsLivraisons.Any());
            ajouteRangeeTabEtatTables("PPTypesLivraison", !dbContext.PPTypesLivraison.Any());
            ajouteRangeeTabEtatTables("PPTypesPoids", !dbContext.PPTypesPoids.Any());
            ajouteRangeeTabEtatTables("PPTaxeFederale", !dbContext.PPTaxeFederale.Any());
            ajouteRangeeTabEtatTables("PPTaxeProvinciale", !dbContext.PPTaxeProvinciale.Any());
            ajouteRangeeTabEtatTables("PPHistoriquePaiements", !dbContext.PPHistoriquePaiements.Any());
            ajouteRangeeTabEtatTables("PPArticlesEnPanier", !dbContext.PPArticlesEnPanier.Any());
            ajouteRangeeTabEtatTables("PPProduits", !dbContext.PPProduits.Any());
            ajouteRangeeTabEtatTables("PPCategories", !dbContext.PPCategories.Any());
            ajouteRangeeTabEtatTables("PPVendeursClients", !dbContext.PPVendeursClients.Any());
            ajouteRangeeTabEtatTables("PPVendeurs", !dbContext.PPVendeurs.Any());
            ajouteRangeeTabEtatTables("PPClients", !dbContext.PPClients.Any());
            ajouteRangeeTabEtatTables("PPGestionnaires", !dbContext.PPGestionnaires.Any());
            panViderBD.Visible = true;
        }
    }

    protected void btnViderBD_Click(object sender, EventArgs e)
    {

    }

    protected void btnImporterDonnees_Click(object sender, EventArgs e)
    {

    }
}