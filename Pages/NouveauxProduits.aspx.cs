using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_NouveauxProduits : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        afficherNouveauxProduits();
    }

    public void afficherNouveauxProduits()
    {
        List<int> lstEntreprises = new List<int>();
        lstEntreprises.Add(1);
        lstEntreprises.Add(2);

        // Créer le panel
        Panel panelBase = LibrairieControlesDynamique.divDYN(phDynamique, "", "panel panel-default");

        // Titre
        Panel panelHeader = LibrairieControlesDynamique.divDYN(panelBase, "", "panel-heading");
        LibrairieControlesDynamique.lblDYN(panelHeader, "", "Nouveautés", "nom-entreprise");

        // Liste des items + le total
        Panel panelBody = LibrairieControlesDynamique.divDYN(panelBase, "", "panel-body");


        // Rajouter les produits
        // temporaire
        int idItem = 0;

        for (int i = 0; i < 3; i++)
        {
            idItem++;
            Double prix = 1300.99;
            int quantiteStock = 2;

            String nomProduit = "MacBook Air 13\", 256GB SSD - Rose Gold";
            String urlImage = "../static/images/macbookair13.jpg";
            String urlNouveau = "../static/images/nouveau.png";

            Panel rowItem = LibrairieControlesDynamique.divDYN(panelBody, "" + idItem, "row");

            // ajouter l'image
            Panel colImg = LibrairieControlesDynamique.divDYN(rowItem, "", "col-sm-2");
            LibrairieControlesDynamique.imgDYN(colImg, "", urlImage, "img-size");

            // Nom du produit
            Panel colNom = LibrairieControlesDynamique.divDYN(rowItem, "", "col-sm-4");
            LibrairieControlesDynamique.lblDYN(colNom, "", nomProduit, "nom-item");

            // Quantité sélectionné
            Panel colQuantite = LibrairieControlesDynamique.divDYN(rowItem, "", "col-sm-4");
            LibrairieControlesDynamique.lblDYN(colQuantite, "", "Qte: " + quantiteStock.ToString(), "nom-item");

            // Prix item
            Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, "", "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colPrix, "", "$" + prix.ToString(), "prix_item");
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