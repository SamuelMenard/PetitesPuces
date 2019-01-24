using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_AffichageProduitDetaille : System.Web.UI.Page
{
    string prevPage = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["RefUrl"] = Request.UrlReferrer.ToString();
        }


        List<String> lstImages = new List<string>();       
        lstImages.Add("500.99;Apple Watch 4;../static/images/appleWatch.jpg");
       

        String nomEntreprise = "Apple";
            Double sousTotal = 0;
            Double TPS = 0;
            Double TVQ = 0;

            // Créer le panier du vendeur X
            Panel panelBase = LibrairieControlesDynamique.divDYN(phDynamique, nomEntreprise + "_base", "panel panel-default");

            // Nom de l'entreprise
            Panel panelHeader = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_header", "panel-heading");
            LibrairieControlesDynamique.lblDYN(panelHeader, nomEntreprise + "_nom", nomEntreprise, "nom-entreprise");

            // Liste des items + le total
            Panel panelBody = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_body", "panel-body");
       
           // LibrairieControlesDynamique.hrDYN(panelBody);


        // Rajouter les produits dans le panier
        int idItem = 1;          
                
                int quantiteSelectionne = 1;
                string[] strValeurs = lstImages[0].Split(';');             
            
                Double prix = Convert.ToDouble(strValeurs[0]);

                // sum au sous total
                sousTotal += prix;

                String nomProduit = strValeurs[1];
                String urlImage = strValeurs[2];

                Panel rowItem = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowItem_" + idItem, "row");

                // ajouter l'image
                Panel colImg = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colImg_" + idItem, "col-sm-2");
                LibrairieControlesDynamique.imgDYN(colImg, nomEntreprise + "_img_" + idItem, urlImage, "img-size center-block");
                LibrairieControlesDynamique.lblDYN(colImg, nomEntreprise + "_noproduit_" + idItem, "1000000"+1, "caption center-block text-center");
                                 

                // Nom du produit
                Panel colNom = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colNom_" + idItem, "col-sm-4");
                //LibrairieControlesDynamique.lbDYN(colNom, nomEntreprise + "_nom_" + idItem, nomProduit,null);
                 LibrairieControlesDynamique.lblDYN(colNom, nomEntreprise + "_nom_" + idItem, nomProduit, "nom-item");

                // Quantité restant
                Panel colQuantite = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colQuantite_" + idItem, "col-sm-2");
                LibrairieControlesDynamique.lblDYN(colQuantite, nomEntreprise + "_quantite_" + idItem, "Qte : 2", "border-quantite");

                // Categorie
                Panel colCat = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colCategorie_" + idItem, "col-sm-2");
                LibrairieControlesDynamique.lblDYN(colCat, nomEntreprise + "_categorie_" + idItem, "Articles électroniques", "cat_item");

                // Prix item
                Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPrix_" + idItem, "col-sm-2");
                LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prix_" + idItem, "$" + prix.ToString(), "prix_item");

                //Panel rowItem2 = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowItem_" + idItem, "row");

            // Bouton retirer
            Panel rowBtnAjout = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowBtnRetirer_" + idItem, "row");
            Panel colBtnAjout = LibrairieControlesDynamique.divDYN(rowBtnAjout, nomEntreprise + "_colBtnRetirer_" + idItem, "col-sm-2 top5");
            LibrairieControlesDynamique.btnDYN(colBtnAjout, nomEntreprise + "_btnRetirer_" + idItem, "btn btn-default center-block", "AJOUTER");
            Panel rowDescription = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowDescription_" + idItem, "row top15");
            LibrairieControlesDynamique.lblDYN(rowDescription, nomEntreprise + "_descTitle_" + idItem, "Description", "cat_item col-sm-2 center-block text-center");
        LibrairieControlesDynamique.lblDYN(rowDescription, nomEntreprise + "_description_" + idItem, 
"Boîtier en aluminium argent GPS, GLONASS, Galileo et QZSS intégrés S4 avec processeur 2 cœurs 64 bits. Puce sans fil W3 d’Apple. Altimètre barométrique. Capacité de 16 Go1. Capteur de fréquence cardiaque optique. Capteur de fréquence cardiaque électrique. Accéléromètre amélioré(jusqu’à 32 g). Gyroscope amélioré Capteur de luminosité ambiante. Écran - pression Retina DELO LTPO(1 000 nits). Digital Crown avec rétroaction haptique Haut - parleur plus puissant Verre Ion - X renforcé Dos en cristal de saphir et céramique Wi - Fi(802.11b / g / n 2, 4 GHz) Bluetooth 5.0 Batterie rechargeable au lithium - ion intégrée Jusqu’à 18 heures d’autonomie2 Étanche jusqu’à 50 m3 watchOS 5", "description_item cat_item col-sm-10 center-block text-justify");
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