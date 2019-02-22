using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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
        verifierPermissions("V");
        if(Session["NoVendeur"] != null)
            noVendeur = Convert.ToInt32((Session["NoVendeur"]));
        leVendeur = dbContext.PPVendeurs.Where(c => c.NoVendeur == noVendeur).First();
        nomEntreprise = leVendeur.NomAffaires;       
        creerSectionPretLivraison();
        creerPage();
    }

    public void verifierPermissions(String typeUtilisateur)
    {
        String url = "";

        if (Session["TypeUtilisateur"] == null)
        {
            url = "~/Pages/AccueilInternaute.aspx?";
            Response.Redirect(url, true);
        }
        else if (Session["TypeUtilisateur"].ToString() != typeUtilisateur)
        {
            String type = Session["TypeUtilisateur"].ToString();
            if (type == "C")
            {
                url = "~/Pages/AccueilClient.aspx?";
            }
            else if (type == "V")
            {
                url = "~/Pages/ConnexionVendeur.aspx?";
            }
            else if (type == "G")
            {
                url = "~/Pages/AcceuilGestionnaire.aspx?";
            }
            else
            {
                url = "~/Pages/AccueilInternaute.aspx?";
            }

            Response.Redirect(url, true);
        }
    }

    private void creerSectionPretLivraison()
    {

        
        
    
        // Créer le panier du vendeur X
        Panel panelGroup = LibrairieControlesDynamique.divDYN(phDynamique, nomEntreprise + "_PanelGroup", "panel-group container-fluid marginFluid");
        Panel panelBase = LibrairieControlesDynamique.divDYN(panelGroup, nomEntreprise + "_base", "panel panel-default");
        

        // Nom de l'entreprise
        Panel panelHeader = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_header", "panel-heading");
        LibrairieControlesDynamique.lblDYN(panelHeader, nomEntreprise + "_nom", nomEntreprise + " - " + leVendeur.AdresseEmail, "nom-entreprise");

        // Liste des items + le total        
        Panel panelBody = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_PanelBody", "panel-body");

        Panel panCategorie = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_pretLivraison_", "row text-center");
        Panel colCatAfficher = LibrairieControlesDynamique.divDYN(panCategorie, nomEntreprise + "_colLabelPretLivraison", "col-sm-12");
        LibrairieControlesDynamique.lblDYN(colCatAfficher, nomEntreprise + "_labelCategorie", "Prêt pour livraison ", "infos-payage OrangeTitle");
        LibrairieControlesDynamique.liDYN(ulSideBar, "#contentBody_" + nomEntreprise + "_labelCategorie", "Prêt pour livraison", "");

        LibrairieControlesDynamique.hrDYN(panelBody, "OrangeBorderPanier", 5);
        List<PPCommandes> lstCommandes = dbContext.PPCommandes.Where(c => c.Statut.Equals("0")).ToList();

        if (lstCommandes.Count > 0)
        {
            // Rajouter les produits dans le panier

            for (int i = 0; i < lstCommandes.Count; i++)
            {
                long idItem = lstCommandes[i].NoCommande;

                decimal prix = lstCommandes[i].MontantTotAvantTaxes.Value;
                long idClient = lstCommandes[i].NoClient.Value;
                PPClients leClient = dbContext.PPClients.Where(c => c.NoClient == idClient).First();
                int NbVisites = dbContext.PPVendeursClients.Where(c => (c.NoClient == idClient) && (c.NoVendeur == noVendeur)).Count();

                Panel rowItem = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowItem_" + idItem, "row valign");

                // ajouter l'image
                Panel colImg = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colImg_" + idItem, "col-sm-2");
                LibrairieControlesDynamique.imgDYN(colImg, nomEntreprise + "_img_" + idItem, "../static/images/imageCommande.png", "img-size center-block");
                LibrairieControlesDynamique.lblDYN(colImg, nomEntreprise + "_noproduit_" + idItem, idItem.ToString(), "caption center-block text-center");


                // Date de la commande
                Panel colDate = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colDate_" + idItem, "col-sm-2 text-center ");
                LibrairieControlesDynamique.lblDYN(colDate, nomEntreprise + "_date_" + idItem, "Date de la commande<br>" + lstCommandes[i].DateCommande.Value.ToShortDateString(), "prix_item");

                //Button Facture Commande
                Panel colFacture = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colFacture_" + idItem, "col-sm-2 text-center");
                if (File.Exists(Server.MapPath("~/static/pdf/" + idItem + ".pdf")))
                {
                    LibrairieControlesDynamique.htmlbtnDYN(colFacture, "btnFacture" + idItem, "btn btn-default Orange", "Facture", "glyphicon glyphicon-list-alt", btnLivre);
                }
                else
                {
                    HtmlButton btn = LibrairieControlesDynamique.htmlbtnDYN(colFacture, "btnFacture" + idItem, "btn btn-default Orange disabled", "Facture", "glyphicon glyphicon-list-alt", btnLivre);
                    btn.Attributes.Add("disabled", "disabled");
                }
                

                // Pooids total commande
                Panel colPoids = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPoids_" + idItem, "col-sm-1 text-center");
                LibrairieControlesDynamique.lblDYN(colPoids, nomEntreprise + "_Poids_" + idItem, "Poids <br>" + lstCommandes[i].PoidsTotal + " lbs", "border-quantite prix_item");

                // Nom du client
                Panel colNomClient = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colClient_" + idItem, "col-sm-2 text-center");
                LibrairieControlesDynamique.lblDYN(colNomClient, nomEntreprise + "_NomClient_" + idItem, "Client<br>" + leClient.Prenom + " " + leClient.Nom, "nomClient prix_item");

                // Visites
                Panel colVisites = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colVisite_" + idItem, "col-sm-1 text-center");
                LibrairieControlesDynamique.lblDYN(colVisites, nomEntreprise + "_Visites_" + idItem, "Visite(s)<br>" + NbVisites, "prix_item");

                // Total avant taxes
                Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPrix_" + idItem, "col-sm-2 text-center");
                LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prix_" + idItem, "Total avant taxes<br>" + prix.ToString("C", CultureInfo.CurrentCulture), "prix_item");

                //   LibrairieControlesDynamique.hrDYN(panelBody);
            }

            LibrairieControlesDynamique.hrDYN(panelBody, "OrangeBorderPanier", 5);
        }
        else
        {
            Panel row = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowPretLivraisonVide", "row marginFluid text-center ");
            Panel message = LibrairieControlesDynamique.divDYN(row, nomEntreprise + "_messagePretLivraisonVide", "message text-center top15");
            Panel messageContainer = LibrairieControlesDynamique.divDYN(message, nomEntreprise + "_divMessageLivraison", "alert alert-danger alert-margins valignMessage");
            LibrairieControlesDynamique.lblDYN(messageContainer, nomEntreprise + "_leMessageLabelLivraison", "Vous avez aucune commande prête pour la livraison.");
            LibrairieControlesDynamique.hrDYN(panelBody, "OrangeBorderPanier", 5);
        }
        
    }

    private void btnLivre(object sender, EventArgs e)
    {
        HtmlButton btn = (HtmlButton)sender;
        string fileName = btn.ID.Replace("btnFacture","");       
        if (File.Exists(Server.MapPath("~/static/pdf/" + fileName + ".pdf")))
        {
            string url = "../static/pdf/" + fileName + ".pdf";
            System.Diagnostics.Debug.WriteLine(" VOICI LE NOM DU FICHIER " + "../static/pdf/" + fileName + ".pdf");
            Response.Write("<script>window.open ('" + url + "','_blank');</script>");           
        }           
    }

    protected void link_ProduitDetail(object sender, EventArgs e)
    {
        LinkButton linkProduit = (LinkButton)sender;        
        Response.Redirect("~/Pages/AffichageProduitDetaille.aspx?productId=" + 10000001);
    }

  

    private void creerPage()
    {    
   
       
        Panel panelGroup = LibrairieControlesDynamique.divDYN(phDynamique, nomEntreprise + "_PanelGroup2", "panel-group container-fluid marginFluid");
        Panel panelBase = LibrairieControlesDynamique.divDYN(panelGroup, nomEntreprise + "_base2", "panel panel-default");
        // Nom de l'entreprise
        Panel panelHeader = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_header2", "panel-heading");

        Panel rowInactif = LibrairieControlesDynamique.divDYN(panelHeader, nomEntreprise + "_rowInactif2_", "row");
        Panel colInactif = LibrairieControlesDynamique.divDYN(rowInactif, nomEntreprise + "_colInactif2_", "col-sm-12");
        LibrairieControlesDynamique.lblDYN(colInactif, nomEntreprise + "_nom2", nomEntreprise + " - " + leVendeur.AdresseEmail, "nom-entreprise");
        panelBody2 = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_PanelBody2", "panel-body");

        List<PPArticlesEnPanier> lstPaniersEntreprise = new List<PPArticlesEnPanier>();
        List<PPArticlesEnPanier> lstArticles = dbContext.PPArticlesEnPanier.GroupBy(x => x.NoClient).Select(t => t.OrderBy(c => c.DateCreation).FirstOrDefault()).ToList();
        foreach (PPArticlesEnPanier lesArticles in lstArticles)
        {
             lstPaniersEntreprise.AddRange(dbContext.PPArticlesEnPanier.Where(c => (c.NoVendeur == noVendeur) && (c.NoClient == lesArticles.NoClient)).OrderBy(C => C.DateCreation).ToList());
        }

            
        panelBody2.Controls.Clear();
        Panel panCategorie = LibrairieControlesDynamique.divDYN(panelBody2, nomEntreprise + "_pretLivraison2_", "row text-center");
        Panel colCatAfficher = LibrairieControlesDynamique.divDYN(panCategorie, nomEntreprise + "_colLabelPretLivraison2", "col-sm-12");
        LibrairieControlesDynamique.lblDYN(colCatAfficher, nomEntreprise + "_labelCategorie2", "Panier Courants ", "infos-payage OrangeTitle");
        LibrairieControlesDynamique.liDYN(ulSideBar, "#contentBody_"+nomEntreprise + "_labelCategorie2", "Panier Courants", "");
        LibrairieControlesDynamique.hrDYN(colCatAfficher, "OrangeBorderPanier", 5);


        // Rajouter les produits dans le panier

        if (lstPaniersEntreprise.Count > 0)
        {
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
                decimal prix = leProduit.PrixDemande.Value;
                String nomProduit = leProduit.Nom.ToString();
                String urlImage = "../static/images/" + leProduit.Photo.ToString();


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
                    string nomClient = (leClient.Nom != null && leClient.Nom != "") ? "Client : " + leClient.Prenom + " " + leClient.Nom : "Client : " + leClient.AdresseEmail ;
                    LibrairieControlesDynamique.lblDYN(colNomClient, nomEntreprise + "_NomClient2_" + idItem, nomClient, "nomClient prix_item");
                    // Nb Visites du client
                    Panel colClientVisites = LibrairieControlesDynamique.divDYN(rowClient, nomEntreprise + "_colVisites_" + idItem, "col-sm-4 text-left");
                    LibrairieControlesDynamique.lblDYN(colClientVisites, nomEntreprise + "_VisiteClient_" + idItem, " Nombre de visites : " + NbVisites, "nomClient prix_item");
                    //SousTotal du panier
                    decimal sousTotalPanier = 0;
                    for (int j = 0; j < lstPaniersEntreprise.Count; j++)
                    {
                        if (lstPaniersEntreprise[j].NoClient == leClient.NoClient)
                        {
                            long idItem2 = lstPaniersEntreprise[j].NoProduit.Value;
                            decimal calculSousTotal = (decimal)dbContext.PPProduits.Where(c => c.NoProduit == idItem2).First().PrixDemande;
                            sousTotalPanier += calculSousTotal * lstPaniersEntreprise[j].NbItems.Value;
                        }
                    }
                    Panel colSousTotal = LibrairieControlesDynamique.divDYN(rowClient, nomEntreprise + "_colSousTotalPanier_" + idItem, "col-sm-2 text-center");
                    LibrairieControlesDynamique.lblDYN(colSousTotal, nomEntreprise + "_SousTotalPanier_" + idItem, sousTotalPanier.ToString("C", CultureInfo.CurrentCulture), "nomClient prix_item");

                    //Panneau Accordeon
                    PanelCollapse = LibrairieControlesDynamique.divDYN(panelBody2, nomEntreprise + "_PanelCollapse" + i, "panel panelAccord");

                }

                Panel rowItem = LibrairieControlesDynamique.divDYN(PanelCollapse, nomEntreprise + "_rowItem2_" + idItem, "row valign top15");

                // ajouter l'image
                Panel colImg = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colImg2_" + idItem, "col-sm-2 ");
                LibrairieControlesDynamique.imgDYN(colImg, nomEntreprise + "_img2_" + idItem, urlImage, "img-size center-block");
                LibrairieControlesDynamique.lblDYN(colImg, nomEntreprise + "_noproduit2_" + idItem, idProduit.ToString(), "caption center-block text-center");


                // Nom du produit
                Panel colNom = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colNom2_" + idItem, "col-sm-3 LiensProduits nomClient");
                LibrairieControlesDynamique.lbDYN(colNom, nomEntreprise + "_nom2_" + idProduit, nomProduit, descriptionProduit);

                // Quantité restant
                Panel colQuantite = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colQuantite2_" + idItem, "col-sm-2 text-right");
                LibrairieControlesDynamique.lblDYN(colQuantite, nomEntreprise + "_quantite2_" + idItem, "Qte : " + lstPaniersEntreprise[i].NbItems, "prix_item");

                // Categorie
                Panel colCat = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colCategorie_" + idItem, "col-sm-3 text-right");
                LibrairieControlesDynamique.lblDYN(colCat, nomEntreprise + "_categorie2_" + idItem, laCategorie.Description.ToString(), "cat_item");

                // Prix item
                Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPri2x_" + idItem, "col-sm-2 text-center");
                LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prix2_" + idItem, "Prix Unitaire<br>" + prix.ToString("C", CultureInfo.CurrentCulture), "prix_item");

                ancienID = idClient;
            }
            LibrairieControlesDynamique.hrDYN(panelBody2, "OrangeBorderPanier", 5);
        }
        else
        {           
            Panel row = LibrairieControlesDynamique.divDYN(panelBody2, nomEntreprise + "_rowPanierVide", "row marginFluid text-center");
            Panel message = LibrairieControlesDynamique.divDYN(row, nomEntreprise + "_messagePanierVide", "message text-center top15");
            Panel messageContainer = LibrairieControlesDynamique.divDYN(message, nomEntreprise + "_divMessage", "alert alert-danger alert-margins");
            LibrairieControlesDynamique.lblDYN(messageContainer, nomEntreprise + "_leMessageLabel", "Vous avez aucun panier courant.");
            LibrairieControlesDynamique.hrDYN(panelBody2, "OrangeBorderPanier", 5);
        }

    }

    private void descriptionProduit(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        string strNoProduit = lb.ID.Replace(nomEntreprise + "_nom2_", "");
        String url = "~/Pages/InscriptionProduit.aspx?NoProduit=" + strNoProduit+"&Operation=Afficher";
        Response.Redirect(url, true);     
    }
}