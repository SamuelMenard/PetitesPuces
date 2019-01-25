using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_ConnexionVendeur : System.Web.UI.Page
{
    List<String> lstImages2 = new List<string>();
    String nomEntreprise = "Apple";
    String nomEntreprise2 = "Apple";
    Panel panelBody2;
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
        Panel colClient = LibrairieControlesDynamique.divDYN(rowRB, nomEntreprise + "_colClient", "col-sm-12");
        LibrairieControlesDynamique.lblDYN(colClient, nomEntreprise + "_nomClient", "Raphael Benoit - Nombre de visites : 5 ", "infos-payage Orange");    
        LibrairieControlesDynamique.hrDYN(panelBody);

        creerPage();

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

  

    private void creerPage()
    {

        Panel panelGroup = LibrairieControlesDynamique.divDYN(phDynamique, nomEntreprise + "_PanelGroup2", "panel-group");
        Panel panelBase = LibrairieControlesDynamique.divDYN(panelGroup, nomEntreprise + "_base2", "panel panel-default");
        // Nom de l'entreprise
        Panel panelHeader = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_header2", "panel-heading");

        Panel rowInactif = LibrairieControlesDynamique.divDYN(panelHeader, nomEntreprise + "_rowInactif2_", "row");
        Panel colInactif = LibrairieControlesDynamique.divDYN(rowInactif, nomEntreprise + "_colInactif2_", "col-sm-6");
        LibrairieControlesDynamique.lblDYN(colInactif, nomEntreprise + "_nom2", nomEntreprise, "nom-entreprise");
        panelBody2 = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_PanelBody2", "panel-body");


        lstImages2.Clear();

        lstImages2.Add("500,97;Apple Watch 4 x1;../static/images/orangeShop.png");
        lstImages2.Add("250,97;Airpods x1;../static/images/orangeShop.png");
     
        
        panelBody2.Controls.Clear();
        Panel panCategorie = LibrairieControlesDynamique.divDYN(panelBody2, nomEntreprise + "_pretLivraison2_", "row text-center");
        Panel colCatAfficher = LibrairieControlesDynamique.divDYN(panCategorie, nomEntreprise + "_colLabelPretLivraison2", "col-sm-12");
        LibrairieControlesDynamique.lblDYN(colCatAfficher, nomEntreprise + "_labelCategorie2", "Panier Courants ", "infos-payage OrangeTitle");

        LibrairieControlesDynamique.hrDYN(panelBody2, "OrangeBorder", 30);
        // Rajouter les produits dans le panier
        int idItem = 0;
        for (int i = 0; i < 2; i++)
        {
            idItem++;
            int quantiteSelectionne = 1;
            string[] strValeurs;
            Double prix = 0.0;
            String nomProduit = "";
            String urlImage = "";

            if (lstImages2.Count > 0)
            {
                strValeurs = lstImages2[i].Split(';');
                prix = Convert.ToDouble(strValeurs[0]);
                nomProduit = strValeurs[1];
                urlImage = strValeurs[2];
            }


            Panel rowItem = LibrairieControlesDynamique.divDYN(panelBody2, nomEntreprise + "_rowItem2_" + idItem, "row valign top15");

            // ajouter l'image
            Panel colImg = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colImg2_" + idItem, "col-sm-2 ");
            LibrairieControlesDynamique.imgDYN(colImg, nomEntreprise + "_img2_" + idItem, urlImage, "img-size center-block");
            LibrairieControlesDynamique.lblDYN(colImg, nomEntreprise + "_noproduit2_" + idItem, "1000000" + i, "caption center-block text-center");


            // Nom du produit
            Panel colNom = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colNom2_" + idItem, "col-sm-2 LiensProduits nomClient");
            LibrairieControlesDynamique.lbDYN(colNom, nomEntreprise + "_nom2_" + idItem, nomProduit, null);

            Panel colNomClient = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colClient_" + idItem, "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colNomClient, nomEntreprise + "_NomClient_" + idItem, "Client : <br>Nicolas Jouanique<br>Nombre de visites : 8", "nomClient");

            // Quantité restant
            Panel colQuantite = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colQuantite2_" + idItem, "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colQuantite, nomEntreprise + "_quantite2_" + idItem, "Qte : 2", "prix_item");

            // Categorie
            Panel colCat = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colCategorie_" + idItem, "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colCat, nomEntreprise + "_categorie2_" + idItem, "Articles électroniques", "cat_item");

            // Prix item
            Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPri2x_" + idItem, "col-sm-2 text-right");
            LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prix2_" + idItem, "$" + prix.ToString(), "prix_item");

           


        }
        LibrairieControlesDynamique.hrDYN(panelBody2, "OrangeBorder", 30);
    }


}