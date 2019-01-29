using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_ConnexionVendeur : System.Web.UI.Page
{
    private BD6B8_424SEntities dbContext = new BD6B8_424SEntities();
    long noVendeur;
   
    String nomEntreprise;
    Panel panelBody2;
    Panel PanelCollapse;
    PPVendeurs leVendeur;
    protected void Page_Load(object sender, EventArgs e)
    {
        noVendeur = Convert.ToInt32((Session["NoVendeur"]));
        leVendeur = dbContext.PPVendeurs.Where(c => c.NoVendeur == noVendeur).First();
        nomEntreprise = leVendeur.NomAffaires;       
        creerSectionPretLivraison();
        creerPage();
    }

    private void creerSectionPretLivraison()
    {

        
        
    
        // Créer le panier du vendeur X
        Panel panelGroup = LibrairieControlesDynamique.divDYN(phDynamique, nomEntreprise + "_PanelGroup", "panel-group container");
        Panel panelBase = LibrairieControlesDynamique.divDYN(panelGroup, nomEntreprise + "_base", "panel panel-default");
        

        // Nom de l'entreprise
        Panel panelHeader = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_header", "panel-heading");
        LibrairieControlesDynamique.lblDYN(panelHeader, nomEntreprise + "_nom", nomEntreprise + " - " + leVendeur.AdresseEmail, "nom-entreprise");

        // Liste des items + le total

        //Label lblCollapse = LibrairieControlesDynamique.lblDYN(panelHeader, nomEntreprise + "_lblPanelTitle", "panel-title","panel-title");
        // LibrairieControlesDynamique.aDYN(panelHeader, "Collapsible panel", "#contentBody_"+nomEntreprise + "_PanelCollapse", true);
        //Panel panelCollaspe = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_PanelCollapse", "panel-collapse collapse");
        Panel panelBody = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_PanelBody", "panel-body");

        Panel panCategorie = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_pretLivraison_", "row text-center");
        Panel colCatAfficher = LibrairieControlesDynamique.divDYN(panCategorie, nomEntreprise + "_colLabelPretLivraison", "col-sm-12");
        LibrairieControlesDynamique.lblDYN(colCatAfficher, nomEntreprise + "_labelCategorie", "Prêt pour livraison ", "infos-payage OrangeTitle");

        //LibrairieControlesDynamique.hrDYN(panelBody);
        LibrairieControlesDynamique.hrDYN(panelBody, "OrangeBorderPanier", 5);
        List<PPCommandes> lstCommandes = dbContext.PPCommandes.Where(c => c.Statut.Equals("0")).ToList();
        // Rajouter les produits dans le panier
        
        for (int i = 0; i < lstCommandes.Count; i++)
        {
            long idItem = lstCommandes[i].NoCommande;

            decimal prix = lstCommandes[i].MontantTotAvantTaxes.Value;
            long idClient = lstCommandes[i].NoClient.Value;
            PPClients leClient = dbContext.PPClients.Where(c => c.NoClient == idClient).First();

            Panel rowItem = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowItem_" + idItem, "row valign");

            // ajouter l'image
            Panel colImg = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colImg_" + idItem, "col-sm-2");
            LibrairieControlesDynamique.imgDYN(colImg, nomEntreprise + "_img_" + idItem, "../static/images/imageCommande.png", "img-size center-block");
            LibrairieControlesDynamique.lblDYN(colImg, nomEntreprise + "_noproduit_" + idItem, idItem.ToString(), "caption center-block text-center");


            // Date de la commande
            Panel colDate = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colDate_" + idItem, "col-sm-2 text-center ");
            LibrairieControlesDynamique.lblDYN(colDate, nomEntreprise + "_date_" + idItem, "Date de la commande<br>"+lstCommandes[i].DateCommande.Value.ToShortDateString(), "prix_item");

            //Button Facture Commande
            Panel colFacture = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colFacture_" + idItem, "col-sm-2 text-center");
            LibrairieControlesDynamique.htmlbtnDYN(colFacture, "btnFacture", "btn btn-default Orange", "Facture", "glyphicon glyphicon-list-alt", btnLivre);

            // Pooids total commande
            Panel colPoids = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPoids_" + idItem, "col-sm-1 text-center");
            LibrairieControlesDynamique.lblDYN(colPoids, nomEntreprise + "_Poids_" + idItem, "Poids <br>"+ lstCommandes[i].PoidsTotal+ " lbs", "border-quantite prix_item");

            // Nom du client
            Panel colNomClient = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colClient_" + idItem, "col-sm-3 text-center");
            LibrairieControlesDynamique.lblDYN(colNomClient, nomEntreprise + "_NomClient_" + idItem, "Client<br>" + leClient.Prenom + " " + leClient.Nom, "nomClient prix_item");

            // Total avant taxes
            Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPrix_" + idItem, "col-sm-2 text-center");
            LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prix_" + idItem, "Total avant taxes<br>$" + prix.ToString("N"), "prix_item");
            
         //   LibrairieControlesDynamique.hrDYN(panelBody);
        }

        LibrairieControlesDynamique.hrDYN(panelBody, "OrangeBorderPanier", 5);

        /* // Afficher le sous total
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


         LibrairieControlesDynamique.lblDYN(colLabelApresTaxes, nomEntreprise + "_montantApresTaxes", "$" + apresTaxes.ToString(), "infos-payage");
         Panel colPaiement = LibrairieControlesDynamique.divDYN(rowApresTaxes, nomEntreprise + "_colLabelPaiement", "col-sm-6");
         LibrairieControlesDynamique.lblDYN(colPaiement, nomEntreprise + "_fraisTransport_", "Méthode de paiement : VISA ", "infos-payage");

         LibrairieControlesDynamique.hrDYN(panelBody);
         Panel rowRB = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowRBStatutCommande", "row text-center");
         Panel colClient = LibrairieControlesDynamique.divDYN(rowRB, nomEntreprise + "_colClient", "col-sm-12");
         LibrairieControlesDynamique.lblDYN(colClient, nomEntreprise + "_nomClient", "Raphael Benoit - Nombre de visites : 5 ", "infos-payage Orange");
         LibrairieControlesDynamique.hrDYN(panelBody);*/
    }

    private void btnLivre(object sender, EventArgs e)
    {
        
    }

    protected void link_ProduitDetail(object sender, EventArgs e)
    {
        LinkButton linkProduit = (LinkButton)sender;        
        Response.Redirect("~/Pages/AffichageProduitDetaille.aspx?productId=" + 10000001);
    }

  

    private void creerPage()
    {    
   
       
        Panel panelGroup = LibrairieControlesDynamique.divDYN(phDynamique, nomEntreprise + "_PanelGroup2", "panel-group container");
        Panel panelBase = LibrairieControlesDynamique.divDYN(panelGroup, nomEntreprise + "_base2", "panel panel-default");
        // Nom de l'entreprise
        Panel panelHeader = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_header2", "panel-heading");

        Panel rowInactif = LibrairieControlesDynamique.divDYN(panelHeader, nomEntreprise + "_rowInactif2_", "row");
        Panel colInactif = LibrairieControlesDynamique.divDYN(rowInactif, nomEntreprise + "_colInactif2_", "col-sm-12");
        LibrairieControlesDynamique.lblDYN(colInactif, nomEntreprise + "_nom2", nomEntreprise + " - " + leVendeur.AdresseEmail, "nom-entreprise");
        panelBody2 = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_PanelBody2", "panel-body");
        
        List<PPArticlesEnPanier> lstPaniersEntreprise = dbContext.PPArticlesEnPanier.Where(c => c.NoVendeur == noVendeur).ToList();      
        panelBody2.Controls.Clear();
        Panel panCategorie = LibrairieControlesDynamique.divDYN(panelBody2, nomEntreprise + "_pretLivraison2_", "row text-center");
        Panel colCatAfficher = LibrairieControlesDynamique.divDYN(panCategorie, nomEntreprise + "_colLabelPretLivraison2", "col-sm-12");
        LibrairieControlesDynamique.lblDYN(colCatAfficher, nomEntreprise + "_labelCategorie2", "Panier Courants ", "infos-payage OrangeTitle");
        LibrairieControlesDynamique.hrDYN(colCatAfficher, "OrangeBorderPanier", 5);

       
        // Rajouter les produits dans le panier
        long ancienID = 0;
        for (int i = 0; i < lstPaniersEntreprise.Count; i++)
        {
            long idItem = lstPaniersEntreprise[i].NoPanier;
            long idProduit = lstPaniersEntreprise[i].NoProduit.Value;
            PPProduits leProduit = dbContext.PPProduits.Where(c => c.NoProduit == idProduit).First();           
            PPCategories laCategorie = dbContext.PPCategories.Where(c => c.NoCategorie == leProduit.NoCategorie).First();
            long idClient = lstPaniersEntreprise[i].NoClient.Value;
            int NbVisites = dbContext.PPVendeursClients.Where(c => (c.NoClient == idClient) && (c.NoVendeur == noVendeur)).Count();            
            PPClients leClient = dbContext.PPClients.Where(c => c.NoClient == idClient).First();
            decimal prix = leProduit.PrixVente.Value;
            String nomProduit = leProduit.Nom.ToString();
            String urlImage = "../static/images/"+ leProduit.Photo.ToString();      
              

            if (ancienID != lstPaniersEntreprise[i].NoClient.Value)
            {                
                //Trigger panel pour accordeon
                Panel panelTrigger = LibrairieControlesDynamique.divDYN(panelBody2, nomEntreprise + "_PanelTrigger" + i, "trigger");
                Panel rowClient = LibrairieControlesDynamique.divDYN(panelTrigger, nomEntreprise + "_rowTrigger_" + idItem, "row top15 text-center border-left");
                // Col Glyph
                Panel colGlyph = LibrairieControlesDynamique.divDYN(rowClient, nomEntreprise + "_colGlyph_" + idItem, "col-sm-2");
                LibrairieControlesDynamique.lblDYN(colGlyph, nomEntreprise + "_GlyphPanier_" + idItem, "", "glyphicon glyphicon-shopping-cart text-left");
                //Nom Client
                Panel colNomClient = LibrairieControlesDynamique.divDYN(rowClient, nomEntreprise + "_colClient2_" + idItem, "col-sm-4 text-left");                
                LibrairieControlesDynamique.lblDYN(colNomClient, nomEntreprise + "_NomClient2_" + idItem, "Client : " + leClient.Prenom + " " + leClient.Nom, "nomClient prix_item");
                // Nb Visites du client
                Panel colClientVisites = LibrairieControlesDynamique.divDYN(rowClient, nomEntreprise + "_colVisites_" + idItem, "col-sm-4 text-left");
                LibrairieControlesDynamique.lblDYN(colClientVisites, nomEntreprise + "_VisiteClient_" + idItem, " Nombre de visites : " + NbVisites, "nomClient prix_item");
                //SousTotal du panier
                decimal sousTotalPanier = 0;
                for (int j = 0; j < lstPaniersEntreprise.Count; j++)
                {
                    if(lstPaniersEntreprise[j].NoClient == leClient.NoClient)
                    {
                        long idItem2 = lstPaniersEntreprise[j].NoProduit.Value;
                        decimal calculSousTotal = (decimal)dbContext.PPProduits.Where(c => c.NoProduit == idItem2).First().PrixVente;
                        sousTotalPanier += calculSousTotal * lstPaniersEntreprise[j].NbItems.Value;
                    }                 
                }
                Panel colSousTotal = LibrairieControlesDynamique.divDYN(rowClient, nomEntreprise + "_colSousTotalPanier_" + idItem, "col-sm-2 text-center");
                LibrairieControlesDynamique.lblDYN(colSousTotal, nomEntreprise + "_SousTotalPanier_" + idItem, "$"+ sousTotalPanier.ToString("N"), "nomClient prix_item");

                //Panneau Accordeon
                PanelCollapse = LibrairieControlesDynamique.divDYN(panelBody2, nomEntreprise + "_PanelCollapse" + i, "panel panelAccord");                
               
            }            

            Panel rowItem = LibrairieControlesDynamique.divDYN(PanelCollapse, nomEntreprise + "_rowItem2_" + idItem, "row valign top15");

            // ajouter l'image
            Panel colImg = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colImg2_" + idItem, "col-sm-2 ");
            LibrairieControlesDynamique.imgDYN(colImg, nomEntreprise + "_img2_" + idItem, urlImage, "img-size center-block");
            LibrairieControlesDynamique.lblDYN(colImg, nomEntreprise + "_noproduit2_" + idItem, "1000000" + i, "caption center-block text-center");


            // Nom du produit
            Panel colNom = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colNom2_" + idItem, "col-sm-3 LiensProduits nomClient");
            LibrairieControlesDynamique.lbDYN(colNom, nomEntreprise + "_nom2_" + idItem, nomProduit, null);          

            // Quantité restant
            Panel colQuantite = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colQuantite2_" + idItem, "col-sm-2 text-right");
            LibrairieControlesDynamique.lblDYN(colQuantite, nomEntreprise + "_quantite2_" + idItem, "Qte : "+ lstPaniersEntreprise[i].NbItems, "prix_item");

            // Categorie
            Panel colCat = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colCategorie_" + idItem, "col-sm-3 text-right");
            LibrairieControlesDynamique.lblDYN(colCat, nomEntreprise + "_categorie2_" + idItem, laCategorie.Description.ToString(), "cat_item");

            // Prix item
            Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPri2x_" + idItem, "col-sm-2 text-center");
            LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prix2_" + idItem, "Prix Unitaire<br> $" + prix.ToString("N"), "prix_item");           
            
            ancienID = idClient;
        }
        LibrairieControlesDynamique.hrDYN(panelBody2, "OrangeBorderPanier", 5);

    }


}