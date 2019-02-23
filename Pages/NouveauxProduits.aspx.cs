using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_NouveauxProduits : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Nouveaux produits";

        afficherNouveauxProduits();
    }

    public void afficherNouveauxProduits()
    {
        List<PPProduits> lstProduits = LibrairieLINQ.getNouveauxProduits();

        // Créer le panel
        Panel panelBase = LibrairieControlesDynamique.divDYN(phDynamique, "", "panel panel-default");

        // Titre
        Panel panelHeader = LibrairieControlesDynamique.divDYN(panelBase, "", "panel-heading");
        LibrairieControlesDynamique.lblDYN(panelHeader, "", "Nouveautés", "nom-entreprise");

        // Liste des items + le total
        Panel panelBody = LibrairieControlesDynamique.divDYN(panelBase, "", "panel-body");


        // Rajouter les produits

        foreach(PPProduits produit in lstProduits)
        {
            Decimal prix = Decimal.Round((Decimal)produit.PrixDemande, 2);
            short quantiteStock = (short)produit.NombreItems;

            String nomProduit = produit.Nom;
            String urlImage = "../static/images/" + produit.Photo;
            String urlNouveau = "../static/images/nouveau.png";

            Panel rowItem = LibrairieControlesDynamique.divDYN(panelBody, "" + produit.NoProduit, "row");

            // ajouter l'image
            Panel colImg = LibrairieControlesDynamique.divDYN(rowItem, "", "col-sm-2");
            LibrairieControlesDynamique.imgDYN(colImg, "", urlImage, "img-size");

            // Nom du produit
            Panel colNom = LibrairieControlesDynamique.divDYN(rowItem, "", "col-sm-4");
            LibrairieControlesDynamique.lblDYN(colNom, "", nomProduit, "nom-item deux-lignes");

            // Quantité sélectionné
            Panel colQuantite = LibrairieControlesDynamique.divDYN(rowItem, "", "col-sm-4");
            LibrairieControlesDynamique.lblDYN(colQuantite, "", "Qte: " + quantiteStock.ToString(), "nom-item");

            // Prix item
            Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, "", "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colPrix, "", prix.ToString("C", CultureInfo.CurrentCulture), "prix_item");
            LibrairieControlesDynamique.spaceDYN(colPrix);
            LibrairieControlesDynamique.spaceDYN(colPrix);
            LibrairieControlesDynamique.imgDYN(colPrix, "", urlNouveau, "img-size-new");

            LibrairieControlesDynamique.hrDYN(panelBody);
            
        }
    }

    public void retour_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Retour");
        String url = "~/Pages/AccueilInternaute.aspx?";
        Response.Redirect(url, true);
    }
}