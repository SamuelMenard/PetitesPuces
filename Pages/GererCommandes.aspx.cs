using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_GererCommandes : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        List<String> lstImages = new List<string>();
        lstImages.Add("500.99;Apple Watch 4;../static/images/appleWatch.jpg");
        lstImages.Add("1500.99;iPhone XS Max;../static/images/iPhone.jpg");
        lstImages.Add("1300.99;MacBook Air 13\", 256GB SSD - Rose Gold;../static/images/macbookair13.jpg");

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
        LibrairieControlesDynamique.lblDYN(panelHeader, nomEntreprise + "_nom", nomEntreprise, "nom-entreprise");

        // Liste des items + le total

        //Label lblCollapse = LibrairieControlesDynamique.lblDYN(panelHeader, nomEntreprise + "_lblPanelTitle", "panel-title","panel-title");
       // LibrairieControlesDynamique.aDYN(panelHeader, "Collapsible panel", "#contentBody_"+nomEntreprise + "_PanelCollapse", true);
        //Panel panelCollaspe = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_PanelCollapse", "panel-collapse collapse");
        Panel panelBody = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_PanelBody", "panel-body");

        Panel panCategorie = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_pretLivraison_", "row text-center");
        Panel colCatAfficher = LibrairieControlesDynamique.divDYN(panCategorie, nomEntreprise + "_colLabelPretLivraison", "col-sm-12");
        LibrairieControlesDynamique.lblDYN(colCatAfficher, nomEntreprise + "_labelCategorie", "Prêt pour livraison ", "infos-payage OrangeTitle");

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
            // LibrairieControlesDynamique.lblDYN(colNom, nomEntreprise + "_nom_" + idItem, nomProduit, "nom-item");

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
           // Panel rowBtnRetirer = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowBtnRetirer_" + idItem, "row");
           // Panel colBtnRetirer = LibrairieControlesDynamique.divDYN(rowBtnRetirer, nomEntreprise + "_colBtnRetirer_" + idItem, "col-sm-2 top5");
           // LibrairieControlesDynamique.btnDYN(colBtnRetirer, nomEntreprise + "_btnRetirer_" + idItem, "btn btn-default center-block", "RETIRER");
            LibrairieControlesDynamique.hrDYN(panelBody);
        }

        // Afficher le sous total
        Panel rowSousTotal = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowSousTotal", "row text-center center-block");
        Panel colLabelSousTotal = LibrairieControlesDynamique.divDYN(rowSousTotal, nomEntreprise + "_colLabelSousTotal", "col-sm-6");
        LibrairieControlesDynamique.lblDYN(colLabelSousTotal, nomEntreprise + "_labelSousTotal", "Sous total: ", "infos-payage");
        //Panel colMontantSousTotal = LibrairieControlesDynamique.divDYN(rowSousTotal, nomEntreprise + "_colMontantSousTotal", "col-sm-3");
        LibrairieControlesDynamique.lblDYN(colLabelSousTotal, nomEntreprise + "_montantSousTotal", "$" + sousTotal.ToString(), "infos-payage");
        Panel colPoste = LibrairieControlesDynamique.divDYN(rowSousTotal, nomEntreprise + "_colLabelPoste", "col-sm-6");
        LibrairieControlesDynamique.lblDYN(colPoste, nomEntreprise + "livraison_", "Méthode de livraison : Poste prioritaire ", "infos-payage");

        LibrairieControlesDynamique.hrDYN(panelBody);



        // Afficher la TPS
        // calculer le prix tps
        TPS = sousTotal * pourcentageTPS;
        TPS = Math.Round(TPS, 2);

        Panel rowTPS = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowTPS", "row text-center center-block");
        Panel colLabelTPS = LibrairieControlesDynamique.divDYN(rowTPS, nomEntreprise + "_colLabelTPS", "col-sm-6");
        LibrairieControlesDynamique.lblDYN(colLabelTPS, nomEntreprise + "_labelTPS", "TPS: ", "infos-payage");
        //Panel colMontantTPS = LibrairieControlesDynamique.divDYN(rowTPS, nomEntreprise + "_colMontantTPS", "col-sm-2");
        LibrairieControlesDynamique.lblDYN(colLabelTPS, nomEntreprise + "_montantTPS", "$" + TPS.ToString(), "infos-payage");
        Panel colPoids = LibrairieControlesDynamique.divDYN(rowTPS, nomEntreprise + "_colLabelPoid", "col-sm-6");
        LibrairieControlesDynamique.lblDYN(colPoids, nomEntreprise + "_Poids_", "Poids de la commande : 4 lbs", "infos-payage");

        LibrairieControlesDynamique.hrDYN(panelBody);

        // Afficher la TVQ
        // calculer le prix tvq
        TVQ = sousTotal * pourcentageTVQ;
        TVQ = Math.Round(TVQ, 2);

        Panel rowTVQ = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowTVQ", "row text-center center-block");
        Panel colLabelTVQ = LibrairieControlesDynamique.divDYN(rowTVQ, nomEntreprise + "_colLabelTVQ", "col-sm-6");
        LibrairieControlesDynamique.lblDYN(colLabelTVQ, nomEntreprise + "_labelTVQ", "TVQ: ", "infos-payage");

        //Panel colMontantTVQ = LibrairieControlesDynamique.divDYN(rowTVQ, nomEntreprise + "_colMontantTVQ", "col-sm-2");
        LibrairieControlesDynamique.lblDYN(colLabelTVQ, nomEntreprise + "_montantTVQ", "$" + TVQ.ToString(), "infos-payage");
        Panel colTransport = LibrairieControlesDynamique.divDYN(rowTVQ, nomEntreprise + "_colLabelTransport", "col-sm-6");
        LibrairieControlesDynamique.lblDYN(colTransport, nomEntreprise + "_fraisTransport_", "Frais de transport : $5 ", "infos-payage");

        LibrairieControlesDynamique.hrDYN(panelBody);

        // Afficher apres taxes
        // calculer le montant apres taxes
        apresTaxes = sousTotal * pourcentageTaxes;
        apresTaxes = Math.Round(apresTaxes, 2);

        Panel rowApresTaxes = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowApresTaxes", "row text-center");
        Panel colLabelApresTaxes = LibrairieControlesDynamique.divDYN(rowApresTaxes, nomEntreprise + "_colLabelApresTaxes", "col-sm-6");
        LibrairieControlesDynamique.lblDYN(colLabelApresTaxes, nomEntreprise + "_labelApresTaxes", "Après taxes: ", "infos-payage");

       // Panel colMontantApresTaxes = LibrairieControlesDynamique.divDYN(rowApresTaxes, nomEntreprise + "_colMontantApresTaxes", "col-sm-2");
        LibrairieControlesDynamique.lblDYN(colLabelApresTaxes, nomEntreprise + "_montantApresTaxes", "$" + apresTaxes.ToString(), "infos-payage");
        Panel colPaiement = LibrairieControlesDynamique.divDYN(rowApresTaxes, nomEntreprise + "_colLabelPaiement", "col-sm-6");
        LibrairieControlesDynamique.lblDYN(colPaiement, nomEntreprise + "_fraisTransport_", "Méthode de paiement : VISA ", "infos-payage");

        LibrairieControlesDynamique.hrDYN(panelBody);
        Panel rowRB = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowRBStatutCommande", "row text-center");
        Panel colClient = LibrairieControlesDynamique.divDYN(rowRB, nomEntreprise + "_colClient", "col-sm-6");
        LibrairieControlesDynamique.lblDYN(colClient, nomEntreprise + "_nomClient", "Raphael Benoit - Nombre de visites : 5 ", "infos-payage Orange");
        Panel colLivrer = LibrairieControlesDynamique.divDYN(rowRB, nomEntreprise + "_colLivrer", "col-sm-6");
        LibrairieControlesDynamique.htmlbtnDYN(colLivrer, "btnLivre", "btn btn-default Orange", "Livrer", "glyphicon glyphicon-send", btnLivre);
        LibrairieControlesDynamique.hrDYN(panelBody, "OrangeBorder",30);

        Panel panCategorie2 = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_Livre_", "row text-center");
        Panel colCatAfficher2 = LibrairieControlesDynamique.divDYN(panCategorie2, nomEntreprise + "_colLabelLivre", "col-sm-12");
        LibrairieControlesDynamique.lblDYN(colCatAfficher2, nomEntreprise + "_labelLivre", "Commandes livrés ", "infos-payage OrangeTitle");

        LibrairieControlesDynamique.hrDYN(panelBody);
      

        Double sousTotal2 = 0;
        Double TPS2 = 0;
        Double TVQ2= 0;
        Double apresTaxes2 = 0;
        Double pourcentageTVQ2 = 0.09975;
        Double pourcentageTPS2 = 0.05;
        Double pourcentageTaxes2 = pourcentageTVQ2 + pourcentageTPS2 + 1;

        List<String> lstImages2 = new List<string>();
        lstImages2.Add("219.99;Airpods;../static/images/airpods.jpg");
        lstImages2.Add("399.99;Écouteurs Beats;../static/images/beats.jpg");
        lstImages2.Add("449.99;Homepod - Blanc ;../static/images/homepod.jpg");
        // Rajouter les produits dans le panier
        int idItem2 = 3;
        for (int i = 3; i < 6; i++)
        {
            idItem2++;
            int quantiteSelectionne = 1;
            string[] strValeurs = lstImages2[i - 3].Split(';');

            Double prix = Convert.ToDouble(strValeurs[0]);

            // sum au sous total
            sousTotal2 += prix;

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
            //LibrairieControlesDynamique.lblDYN(colNom, nomEntreprise + "_nom_" + idItem2, nomProduit, "nom-item");

            // Quantité restant
            Panel colQuantite = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colQuantite_" + idItem2, "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colQuantite, nomEntreprise + "_quantite_" + idItem2, "Qte : 2", "border-quantite");

            // Categorie
            Panel colCat = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colCategorie_" + idItem2, "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colCat, nomEntreprise + "_categorie_" + idItem2, "Variables", "cat_item");

            // Prix item
            Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPrix_" + idItem2, "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prix_" + idItem2, "$" + prix.ToString(), "prix_item");

            //Panel rowItem2 = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowItem_" + idItem, "row");
            LibrairieControlesDynamique.hrDYN(panelBody);

        }
        // Afficher le sous total
        Panel rowSousTotal2 = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowSousTotal", "row text-center");
        Panel colLabelSousTotal2 = LibrairieControlesDynamique.divDYN(rowSousTotal2, nomEntreprise + "_colLabelSousTotal", "col-sm-6");
        LibrairieControlesDynamique.lblDYN(colLabelSousTotal2, nomEntreprise + "_labelSousTotal", "Sous total: ", "infos-payage");
       

        //Panel colMontantSousTotal2 = LibrairieControlesDynamique.divDYN(rowSousTotal2, nomEntreprise + "_colMontantSousTotal", "col-sm-2");
        LibrairieControlesDynamique.lblDYN(colLabelSousTotal2, nomEntreprise + "_montantSousTotal", "$" + sousTotal2.ToString(), "infos-payage");
        Panel colPoste2 = LibrairieControlesDynamique.divDYN(rowSousTotal2, nomEntreprise + "_colLabelPoste", "col-sm-6");
        LibrairieControlesDynamique.lblDYN(colPoste2, nomEntreprise + "livraison_", "Méthode de livraison : Poste prioritaire ", "infos-payage");

        LibrairieControlesDynamique.hrDYN(panelBody);

        // Afficher la TPS
        // calculer le prix tps
        TPS2 = sousTotal2 * pourcentageTPS2;
        TPS2 = Math.Round(TPS2, 2);

        Panel rowTPS2 = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowTPS", "row text-center");
        Panel colLabelTPS2 = LibrairieControlesDynamique.divDYN(rowTPS2, nomEntreprise + "_colLabelTPS", "col-sm-6");
        LibrairieControlesDynamique.lblDYN(colLabelTPS2, nomEntreprise + "_labelTPS", "TPS: ", "infos-payage");

        //Panel colMontantTPS2 = LibrairieControlesDynamique.divDYN(rowTPS2, nomEntreprise + "_colMontantTPS", "col-sm-2");
        LibrairieControlesDynamique.lblDYN(colLabelTPS2, nomEntreprise + "_montantTPS", "$" + TPS2.ToString(), "infos-payage");
        Panel colPoids2 = LibrairieControlesDynamique.divDYN(rowTPS2, nomEntreprise + "_colLabelPoid", "col-sm-6");
        LibrairieControlesDynamique.lblDYN(colPoids2, nomEntreprise + "_Poids_", "Poids de la commande : 4 lbs", "infos-payage");

        LibrairieControlesDynamique.hrDYN(panelBody);

        // Afficher la TVQ
        // calculer le prix tvq
        TVQ2 = sousTotal2 * pourcentageTVQ2;
        TVQ2 = Math.Round(TVQ2, 2);

        Panel rowTVQ2 = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowTVQ", "row text-center");
        Panel colLabelTVQ2 = LibrairieControlesDynamique.divDYN(rowTVQ2, nomEntreprise + "_colLabelTVQ", "col-sm-6");
        LibrairieControlesDynamique.lblDYN(colLabelTVQ2, nomEntreprise + "_labelTVQ", "TVQ: ", "infos-payage");

        //Panel colMontantTVQ2 = LibrairieControlesDynamique.divDYN(rowTVQ2, nomEntreprise + "_colMontantTVQ", "col-sm-2");
        LibrairieControlesDynamique.lblDYN(colLabelTVQ2, nomEntreprise + "_montantTVQ", "$" + TVQ2.ToString(), "infos-payage");
       
        Panel colTransport2 = LibrairieControlesDynamique.divDYN(rowTVQ2, nomEntreprise + "_colLabelTransport", "col-sm-6");
        LibrairieControlesDynamique.lblDYN(colTransport2, nomEntreprise + "_fraisTransport_", "Frais de transport : $5 ", "infos-payage");
        LibrairieControlesDynamique.hrDYN(panelBody);

        // Afficher apres taxes
        // calculer le montant apres taxes
        apresTaxes2 = sousTotal2 * pourcentageTaxes2;
        apresTaxes2 = Math.Round(apresTaxes2, 2);

        Panel rowApresTaxes2 = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowApresTaxes", "row text-center");
        Panel colLabelApresTaxes2 = LibrairieControlesDynamique.divDYN(rowApresTaxes2, nomEntreprise + "_colLabelApresTaxes", "col-sm-6");
        LibrairieControlesDynamique.lblDYN(colLabelApresTaxes2, nomEntreprise + "_labelApresTaxes", "Après taxes: ", "infos-payage");

        //Panel colMontantApresTaxes2 = LibrairieControlesDynamique.divDYN(rowApresTaxes2, nomEntreprise + "_colMontantApresTaxes", "col-sm-2");
        LibrairieControlesDynamique.lblDYN(colLabelApresTaxes2, nomEntreprise + "_montantApresTaxes", "$" + apresTaxes2.ToString(), "infos-payage");       
        Panel colPaiement2 = LibrairieControlesDynamique.divDYN(rowApresTaxes2, nomEntreprise + "_colLabelPaiement", "col-sm-6");
        LibrairieControlesDynamique.lblDYN(colPaiement2, nomEntreprise + "_fraisTransport_", "Méthode de paiement : VISA ", "infos-payage");


        LibrairieControlesDynamique.hrDYN(panelBody);

        Panel rowRB2 = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowRBStatutCommande", "row text-center");
        Panel colClient2 = LibrairieControlesDynamique.divDYN(rowRB2, nomEntreprise + "_colClient", "col-sm-12");
        LibrairieControlesDynamique.lblDYN(colClient2, nomEntreprise + "_nomClient", "Samuel Ménard - Nombre de visites :13 ", "infos-payage Orange");

        //LibrairieControlesDynamique.lbDYN(rowRB, "_Livre", "Livré", btnLivre);
        //LibrairieControlesDynamique.litDYN(rowRB, "btnLivre", "<button type='button' class='btn btn-default left15'><span class=\"glyphicon glyphicon-sort\"></span> Date de parution</button>");


    }



    private void btnLivre(object sender, EventArgs e)
    {
        
    }

    protected void link_ProduitDetail(object sender, EventArgs e)
    {
        LinkButton linkProduit = (LinkButton)sender;
        //Response.Write(linkProduit.ID);
        Response.Redirect("~/Pages/AffichageProduitDetaille.aspx?productId=" + 10000001);
        //linkProduit.PostBackUrl = "~/Pages/AffichageProduitDetaille.aspx?productId=" + 1;
    }
}