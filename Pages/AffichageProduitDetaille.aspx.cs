using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_AffichageProduitDetaille : System.Web.UI.Page
{
    private BD6B8_424SEntities dbContext = new BD6B8_424SEntities();
    string prevPage = String.Empty;
    private int noProduit = 0;

    private void getNbMois()
    {

        if (Request.QueryString["NoProduit"] == null)
        {
           
        }
        else
        {
            this.noProduit = Convert.ToInt32(Request.QueryString["NoProduit"]);           
        }
       
      
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        getNbMois();
        if (!IsPostBack)
        {
            if (Request.UrlReferrer != null)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString();
            }
        }

       PPProduits produitAfficher = dbContext.PPProduits.Where( c => c.NoProduit == noProduit).First();
        List<String> lstImages = new List<string>();       
        lstImages.Add("500,99;Apple Watch 4;../static/images/appleWatch.jpg");
        

         String nomEntreprise = dbContext.PPVendeurs.Where( c => c.NoVendeur == produitAfficher.NoVendeur).First().NomAffaires;
          

            // Créer le panier du vendeur X
            Panel panelBase = LibrairieControlesDynamique.divDYN(phDynamique, nomEntreprise + "_base", "panel panel-default");

            // Nom de l'entreprise
            Panel panelHeader = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_header", "panel-heading");
            LibrairieControlesDynamique.lblDYN(panelHeader, nomEntreprise + "_nom", nomEntreprise, "nom-entreprise");

            // Liste des items + le total
            Panel panelBody = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_body", "panel-body");
       
           // LibrairieControlesDynamique.hrDYN(panelBody);


        // Rajouter les produits dans le panier
        
                 long idItem = produitAfficher.NoProduit;


        Double prix;

        if (produitAfficher.DateVente > DateTime.Now.Date)
        {
            prix = Convert.ToDouble(produitAfficher.PrixVente);
        }
        else
        {
            prix = Convert.ToDouble(produitAfficher.PrixDemande);
        }



                String urlImage = "../static/images/"+ produitAfficher.Photo;

                Panel rowItem = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowItem_" + idItem, "row");

                // ajouter l'image
                Panel colImg = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colImg_" + idItem, "col-sm-2");
                LibrairieControlesDynamique.imgDYN(colImg, nomEntreprise + "_img_" + idItem, urlImage, "img-size center-block");
                LibrairieControlesDynamique.lblDYN(colImg, nomEntreprise + "_noproduit_" + idItem, "1000000"+1, "caption center-block text-center");
                                 

                // Nom du produit
                Panel colNom = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colNom_" + idItem, "col-sm-4");
                //LibrairieControlesDynamique.lbDYN(colNom, nomEntreprise + "_nom_" + idItem, nomProduit,null);
                 LibrairieControlesDynamique.lblDYN(colNom, nomEntreprise + "_nom_" + idItem, produitAfficher.Nom, "nom-item");

                // Quantité restant
                Panel colQuantite = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colQuantite_" + idItem, "col-sm-2");
                LibrairieControlesDynamique.lblDYN(colQuantite, nomEntreprise + "_quantite_" + idItem, "Qte : 2", "border-quantite");

        // Categorie
        string categorie = dbContext.PPCategories.Where(c => c.NoCategorie == produitAfficher.NoCategorie).First().Description;
                Panel colCat = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colCategorie_" + idItem, "col-sm-2");
                LibrairieControlesDynamique.lblDYN(colCat, nomEntreprise + "_categorie_" + idItem, categorie, "cat_item");

                // Prix item
                Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPrix_" + idItem, "col-sm-2");
                LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prix_" + idItem,  prix.ToString("N")+ " $", "prix_item");
               

            // Bouton retirer
            Panel rowBtnAjout = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowBtnRetirer_" + idItem, "row");
            Panel colBtnAjout = LibrairieControlesDynamique.divDYN(rowBtnAjout, nomEntreprise + "_colBtnRetirer_" + idItem, "col-sm-2 top5");
            LibrairieControlesDynamique.btnDYN(colBtnAjout, nomEntreprise + "_btnRetirer_" + idItem, "btn btn-default center-block", "AJOUTER");
            Panel rowDescription = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowDescription_" + idItem, "row top15");
            LibrairieControlesDynamique.lblDYN(rowDescription, nomEntreprise + "_descTitle_" + idItem, "Description", "cat_item col-sm-2 center-block text-center");
        LibrairieControlesDynamique.lblDYN(rowDescription, nomEntreprise + "_description_" + idItem,"", "description_item cat_item col-sm-10 center-block text-justify");
        LibrairieControlesDynamique.hrDYN(panelBody);
            Panel panelRetour = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_retour_", "row center-block text-center Retour");
            LibrairieControlesDynamique.lbDYN(panelRetour, "back_", "Retour", btnRetour);     
          
        
    }
    protected void btnRetour(object sender, EventArgs e)
    {
        object refUrl = ViewState["RefUrl"];
        if (refUrl != null)
            Response.Redirect((string)refUrl);
    }
}