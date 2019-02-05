using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Base : System.Web.UI.Page
{

    private BD6B8_424SEntities dbContext = new BD6B8_424SEntities();
    long noVendeur;
    String nomEntreprise = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        
        if (Request.QueryString["NoVendeur"] != null)
        {
            noVendeur = Convert.ToInt64(Request.QueryString["NoVendeur"]);
        }
        else
        {
            noVendeur = 20;
        }
        nomEntreprise = dbContext.PPVendeurs.Where(c => c.NoVendeur == noVendeur).First().NomAffaires;
        
        creerPage();

        if (!IsPostBack)
        {
            List<int> lstCategories = dbContext.PPProduits.Where(c => c.NoVendeur == 20).GroupBy(c => c.NoCategorie).Select(c => c.Key.Value).ToList();
            for (int i = 0; i < lstCategories.Count; i++)
            {
                int nbCat = lstCategories[i];

                string strDescCat = dbContext.PPCategories.Where(c => c.NoCategorie == nbCat).First().Description;
                ddlCategorie.Items.Insert(i, new ListItem(strDescCat, nbCat.ToString()));
            }
        }



    }

    private void creerPage()
    {

        // Créer le panier du vendeur X
        /* Panel panelBase = LibrairieControlesDynamique.divDYN(phDynamique, nomEntreprise + "_base", "panel panel-default container");

         Panel panSearchFilter = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_searchFilter_", "clearfix topBotPad center");
         Panel colFullRow = LibrairieControlesDynamique.divDYN(panSearchFilter, nomEntreprise + "_colFullRow_", "col-sm-12");
         LibrairieControlesDynamique.tbDYN(colFullRow, nomEntreprise + "_searchText", "left15");
         HtmlButton btnSearchProd = LibrairieControlesDynamique.htmlbtnDYN(colFullRow, "btnSearch", "btn btn-default left15", "", "glyphicon glyphicon-search", null);        
         DropDownList dropdownlist = LibrairieControlesDynamique.ddlDYN(colFullRow, nomEntreprise + "_dropdowncategorie_", "left15");

         //dropdownlist.AutoPostBack = true;



         HtmlButton btnSortProduit = LibrairieControlesDynamique.htmlbtnDYN(colFullRow, "btnTrierNoProduit_", "btn btn-default left15","Numéro de produit", "glyphicon glyphicon-sort",null);
         HtmlButton btnSortDate = LibrairieControlesDynamique.htmlbtnDYN(colFullRow, "btnTrierDate_", "btn btn-default left15", "Date de parution", "glyphicon glyphicon-sort", null);      

         LibrairieControlesDynamique.lblDYN(colFullRow, nomEntreprise + "_nbParPage", "Nombres de produits par page : ", "left15");
         ddlNbPages = LibrairieControlesDynamique.ddlDYN(colFullRow, "ddlNbParPage", "left15");      
         ddlNbPages.AutoPostBack = true;
         ddlNbPages.EnableViewState = true;
         ddlNbPages.SelectedIndexChanged += nbPageChange;       */

        // Nom de l'entreprise
        /* phDynamique.Controls.Clear();
         Panel panelHeader = LibrairieControlesDynamique.divDYN(phDynamique, nomEntreprise + "_header", "panel-heading");
         LibrairieControlesDynamique.lblDYN(panelHeader, nomEntreprise + "_nom", nomEntreprise, "nom-entreprise");

         // Liste des items + le total
         Panel panelBody = LibrairieControlesDynamique.divDYN(phDynamique, nomEntreprise + "_body", "panel-body");      
         int nbProduitsVendeur = dbContext.PPProduits.Where(c => c.NoVendeur == noVendeur).Count();     


         // Rajouter les produits dans le panier
         if (nbProduitsVendeur > 0)
         {           
             var tableProduits = dbContext.PPProduits;
             var tableVendeurs = dbContext.PPVendeurs;
             var produits = from produit in tableProduits.Where(c => (c.NoVendeur == noVendeur) && (c.Description.Contains(""))).OrderBy(c => c.NoCategorie).ThenBy(c => c.Description).Skip(0).Take(intNbPage)
                                       group produit by new { produit.NoCategorie }
                                          into listeProduits
                                       select listeProduits;            



             foreach (var keys in produits)
             {
                 string descCategorie = dbContext.PPCategories.Where(c => c.NoCategorie == keys.Key.NoCategorie).First().Description;
                 Panel panCategorie = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_categorie_"+ descCategorie, "flex-container");
                 Panel colCatAfficher = LibrairieControlesDynamique.divDYN(panCategorie, nomEntreprise + "_colLabelCategorie" + descCategorie, "");
                 LibrairieControlesDynamique.lblDYN(colCatAfficher, nomEntreprise + "_labelCategorie" + descCategorie, descCategorie, "infos-payage");

                 foreach (PPProduits produitCat in keys)
                 {
                     long idItem = Convert.ToInt64(produitCat.NoProduit);
                     String nomProduit = produitCat.Nom.ToString();
                     Double prix = Convert.ToDouble(produitCat.PrixVente);
                     String urlImage = "../static/images/" + produitCat.Photo;
                     int noCat = produitCat.NoCategorie.Value;
                     PPCategories pCategorie = dbContext.PPCategories.Where(c => c.NoCategorie.Equals(noCat)).First();
                     String categorie = pCategorie.Description;

                     Panel rowItem = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowItem_" + idItem, "row valign top15");

                     // ajouter l'image
                     Panel colImg = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colImg_" + idItem, "col-sm-2 ");
                     LibrairieControlesDynamique.imgDYN(colImg, nomEntreprise + "_img_" + idItem, urlImage, "img-size center-block");
                     LibrairieControlesDynamique.lblDYN(colImg, nomEntreprise + "_noproduit_" + idItem, idItem.ToString(), "caption center-block text-center");

                     // Nom du produit
                     Panel colNom = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colNom_" + idItem, "col-sm-3 LiensProduits nomClient");
                     LibrairieControlesDynamique.lbDYN(colNom, nomEntreprise + "_nom_" + idItem, nomProduit, null);

                     // Quantité restant
                     Panel colQuantite = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colQuantite_" + idItem, "col-sm-2");
                     LibrairieControlesDynamique.lblDYN(colQuantite, nomEntreprise + "_quantite_" + idItem, "Qte : " + produitCat.NombreItems, "prix_item");

                     // Categorie
                     Panel colCat = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colCategorie_" + idItem, "col-sm-3");
                     LibrairieControlesDynamique.lblDYN(colCat, nomEntreprise + "_categorie_" + idItem, categorie, "cat_item");

                     // Prix item
                     Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPrix_" + idItem, "col-sm-2 text-right");
                     LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prixDemande_" + idItem, "Prix demandé : $" + prix.ToString(), "prix_item");

                 }

             }



         }*/

    }

    protected void categorieIndexChange(object sender, EventArgs e)
    {
        Session["dd1"] = ddlCategorie.SelectedValue;
        ddlCategorie.SelectedIndex = ddlCategorie.Items.IndexOf(ddlCategorie.Items.FindByValue(Convert.ToString(Session["dd1"])));
        System.Diagnostics.Debug.WriteLine("JFOURRE TA MERE JSUIS IN" + ddlCategorie.SelectedValue + " SIZE DU DDL " + ddlCategorie.Items.Count + " VALEUR DU SESSION " + Session["dd1"]);      

    }
}