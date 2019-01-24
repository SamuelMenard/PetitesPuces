using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_GererPanierInactifs : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        List<String> lstImages = new List<string>();
        lstImages.Add("3302.97;Apple Watch 4 x1<br> iPhone XS Max x1<br> MacBook Air 13\", 256GB SSD - Rose Gold x1<br>;../static/images/orangeShop.png");
        lstImages.Add("1069.97;Airpods x1<br> Écouteurs beats x1<br> Homepod - Blanc x1;../static/images/orangeShop.png");       

        String nomEntreprise = "Apple";

       
        Double sousTotal = 0;
        Double TPS = 0;
        Double TVQ = 0;
        Double apresTaxes = 0;
        Double pourcentageTVQ = 0.09975;
        Double pourcentageTPS = 0.05;
        Double pourcentageTaxes = pourcentageTVQ + pourcentageTPS + 1;

        // Créer le panier du vendeur X

        Panel panelGroup = LibrairieControlesDynamique.divDYN(phDynamique, nomEntreprise + "_PanelGroup", "panel-group");
        Panel panelBase = LibrairieControlesDynamique.divDYN(panelGroup, nomEntreprise + "_base", "panel panel-default");





  

        // Nom de l'entreprise
        Panel panelHeader = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_header", "panel-heading");

        Panel rowInactif = LibrairieControlesDynamique.divDYN(panelHeader, nomEntreprise + "_rowInactif_", "row");       
        Panel colInactif = LibrairieControlesDynamique.divDYN(rowInactif, nomEntreprise + "_colInactif_", "col-sm-6");
        LibrairieControlesDynamique.lblDYN(colInactif, nomEntreprise + "_nom", nomEntreprise, "nom-entreprise");
        Panel colDDLInactif = LibrairieControlesDynamique.divDYN(rowInactif, nomEntreprise + "_colDDLInactif_", "col-sm-6");
        LibrairieControlesDynamique.lblDYN(colDDLInactif, nomEntreprise + "_nbParPage", "Nombre de mois inactifs : ", "left15");
        DropDownList ddlNbPages = LibrairieControlesDynamique.ddlDYN(colDDLInactif, "ddlNbParPage", "left15");
        ddlNbPages.SelectedIndexChanged += valeursPanier;
        ddlNbPages.Items.Insert(0, new ListItem("1", ""));
        ddlNbPages.Items.Insert(1, new ListItem("2", ""));
        ddlNbPages.Items.Insert(2, new ListItem("3", ""));
        ddlNbPages.Items.Insert(3, new ListItem("6", ""));
        ddlNbPages.Items.Insert(4, new ListItem("6+", ""));

        // Liste des items + le total

        //Label lblCollapse = LibrairieControlesDynamique.lblDYN(panelHeader, nomEntreprise + "_lblPanelTitle", "panel-title","panel-title");
        // LibrairieControlesDynamique.aDYN(panelHeader, "Collapsible panel", "#contentBody_"+nomEntreprise + "_PanelCollapse", true);
        //Panel panelCollaspe = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_PanelCollapse", "panel-collapse collapse");
        Panel panelBody = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_PanelBody", "panel-body");
        LibrairieControlesDynamique.hrDYN(panelBody, "OrangeBorder", 30);


        // Rajouter les produits dans le panier
        int idItem = 0;
        for (int i = 0; i < 2; i++)
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
            LibrairieControlesDynamique.lbDYN(colNom, nomEntreprise + "_nom_" + idItem, nomProduit, null);            

            // Quantité restant
            Panel colQuantite = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colQuantite_" + idItem, "col-sm-2");
           // LibrairieControlesDynamique.lblDYN(colQuantite, nomEntreprise + "_quantite_" + idItem, "Qte : 2", "border-quantite");

            // Categorie
            Panel colCat = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colCategorie_" + idItem, "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colCat, nomEntreprise + "_categorie_" + idItem, "Articles électroniques", "cat_item");

            // Prix item
            Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPrix_" + idItem, "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prix_" + idItem, "$" + prix.ToString(), "prix_item");          
           
        }
        LibrairieControlesDynamique.hrDYN(panelBody, "OrangeBorder",30);    
       


    }

    private void valeursPanier(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}