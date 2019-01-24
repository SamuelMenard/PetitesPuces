using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_ConsultationCatalogueProduitVendeur : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        List<String> lstImages = new List<string>();
        lstImages.Add("500,99;Apple Watch 4;../static/images/appleWatch.jpg");
        lstImages.Add("1500,99;iPhone XS Max;../static/images/iPhone.jpg");
        lstImages.Add("1300,99;MacBook Air 13\", 256GB SSD - Rose Gold;../static/images/macbookair13.jpg");

        String nomEntreprise = "Apple";

        Double sousTotal = 0;
        Double TPS = 0;
        Double TVQ = 0;

        // Créer le panier du vendeur X
        Panel panelBase = LibrairieControlesDynamique.divDYN(phDynamique, nomEntreprise + "_base", "panel panel-default");

        Panel panSearchFilter = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_searchFilter_", "clearfix topBotPad center");
        LibrairieControlesDynamique.tbDYN(panSearchFilter, nomEntreprise + "_searchText", "col-sm-4 left15");       
        LibrairieControlesDynamique.divDYN(LibrairieControlesDynamique.lbDYN(panSearchFilter, nomEntreprise + "_btnSearch", "Search", null), nomEntreprise + "_searchLink", "glyphicon glyphicon-search");
        DropDownList dropdownlist = LibrairieControlesDynamique.ddlDYN(panSearchFilter, nomEntreprise + "_dropdowncategorie_", "left15");
        dropdownlist.Items.Insert(0, new ListItem("Catégorie", ""));

        LibrairieControlesDynamique.litDYN(panSearchFilter, "filterNumero", "<button type='button' class='btn btn-default left15'><span class=\"glyphicon glyphicon-sort\"></span> Numéro de produit</button>");
        LibrairieControlesDynamique.litDYN(panSearchFilter, "filterNumero", "<button type='button' class='btn btn-default left15'><span class=\"glyphicon glyphicon-sort\"></span> Date de parution</button>");

        LibrairieControlesDynamique.lblDYN(panSearchFilter, nomEntreprise + "_nbParPage", "Nombres de produits par page : ", "left15");
        DropDownList ddlNbPages = LibrairieControlesDynamique.ddlDYN(panSearchFilter,"ddlNbParPage", "left15");
        ddlNbPages.Items.Insert(0, new ListItem("5", ""));
        ddlNbPages.Items.Insert(1, new ListItem("10", ""));
        ddlNbPages.Items.Insert(2, new ListItem("15", ""));      


        // Nom de l'entreprise
        Panel panelHeader = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_header", "panel-heading");
        LibrairieControlesDynamique.lblDYN(panelHeader, nomEntreprise + "_nom", nomEntreprise, "nom-entreprise");

        // Liste des items + le total
        Panel panelBody = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_body", "panel-body");

        Panel panCategorie = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_categorie_", "row");
        Panel colCatAfficher = LibrairieControlesDynamique.divDYN(panCategorie, nomEntreprise + "_colLabelCategorie", "col-sm-4");
        LibrairieControlesDynamique.lblDYN(colCatAfficher, nomEntreprise + "_labelCategorie", "Articles électroniques ", "infos-payage");

        LibrairieControlesDynamique.hrDYN(panelBody);


        // Rajouter les produits dans le panier
        int idItem = 0;
        for (int i = 0; i < 3; i++)
        {
            idItem++;
            int quantiteSelectionne = 1;
            string[] strValeurs = lstImages[i].Split(';');

            Double prix = Convert.ToDouble(strValeurs[0]);

            // sum au sous total
            sousTotal += prix;

            String nomProduit = strValeurs[1];
            String urlImage = strValeurs[2];

            Panel rowItem = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowItem_" + idItem, "row");

            // ajouter l'image
            Panel colImg = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colImg_" + idItem, "col-sm-2");
            LibrairieControlesDynamique.imgDYN(colImg, nomEntreprise + "_img_" + idItem, urlImage, "img-size center-block");
            LibrairieControlesDynamique.lblDYN(colImg, nomEntreprise + "_noproduit_" + idItem, "1000000" + i, "caption center-block text-center");


            // Nom du produit
            Panel colNom = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colNom_" + idItem, "col-sm-4 LiensProduits");
            LibrairieControlesDynamique.lbDYN(colNom, nomEntreprise + "_nom_" + idItem, nomProduit, link_ProduitDetail);
            

            // Quantité restant
            Panel colQuantite = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colQuantite_" + idItem, "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colQuantite, nomEntreprise + "_quantite_" + idItem, "Qte : 2", "border-quantite");

            // Categorie
            Panel colCat = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colCategorie_" + idItem, "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colCat, nomEntreprise + "_categorie_" + idItem, "Articles électroniques", "cat_item");

            // Prix item
            Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPrix_" + idItem, "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prix_" + idItem, "$" + prix.ToString(), "prix_item");
            LibrairieControlesDynamique.hrDYN(panelBody);
        }


        Panel panCategorie2 = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_categorie2_", "row");
        Panel colCatAfficher2 = LibrairieControlesDynamique.divDYN(panCategorie2, nomEntreprise + "_colLabelCategorie2", "col-sm-4");
        LibrairieControlesDynamique.lblDYN(colCatAfficher2, nomEntreprise + "_labelCategorie2", "Variables ", "infos-payage");

        LibrairieControlesDynamique.hrDYN(panelBody);

        List<String> lstImages2 = new List<string>();
        lstImages2.Add("219,99;Airpods;../static/images/airpods.jpg");
        lstImages2.Add("399,99;Écouteurs Beats;../static/images/beats.jpg");
        lstImages2.Add("449,99;Homepod - Blanc ;../static/images/homepod.jpg");
        // Rajouter les produits dans le panier
        int idItem2 = 3;
        for (int i = 3; i < 6; i++)
        {
            idItem2++;
            int quantiteSelectionne = 1;
            string[] strValeurs = lstImages2[i - 3].Split(';');

            Double prix = Convert.ToDouble(strValeurs[0]);

            // sum au sous total
            sousTotal += prix;

            String nomProduit = strValeurs[1];
            String urlImage = strValeurs[2];

            Panel rowItem = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowItem_" + idItem2, "row");

            // ajouter l'image
            Panel colImg = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colImg_" + idItem2, "col-sm-2");
            LibrairieControlesDynamique.imgDYN(colImg, nomEntreprise + "_img_" + idItem2, urlImage, "img-size center-block");
            LibrairieControlesDynamique.lblDYN(colImg, nomEntreprise + "_noproduit_" + idItem2, "1000000" + (i), "caption center-block text-center");

            // Nom du produit
            Panel colNom = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colNom_" + idItem2, "col-sm-4 LiensProduits");
            LibrairieControlesDynamique.lbDYN(colNom, nomEntreprise + "_nom_" + idItem2, nomProduit, null);
           

            // Quantité restant
            Panel colQuantite = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colQuantite_" + idItem2, "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colQuantite, nomEntreprise + "_quantite_" + idItem2, "Qte : 2", "border-quantite");

            // Categorie
            Panel colCat = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colCategorie_" + idItem2, "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colCat, nomEntreprise + "_categorie_" + idItem2, "Variables", "cat_item");

            // Prix item
            Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPrix_" + idItem2, "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prix_" + idItem2, "$" + prix.ToString(), "prix_item");

            
            LibrairieControlesDynamique.hrDYN(panelBody);

        }      
        
    }
    protected void link_ProduitDetail(object sender, EventArgs e)
    {
        LinkButton linkProduit = (LinkButton)sender;       
        Response.Redirect("~/Pages/AffichageProduitDetaille.aspx?productId=" + 10000001);      
    }
}