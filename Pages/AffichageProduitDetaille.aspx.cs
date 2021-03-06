﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_AffichageProduitDetaille : System.Web.UI.Page
{
    private BD6B8_424SEntities dbContext = new BD6B8_424SEntities();
    string prevPage = String.Empty;
    private int noProduit = 0;
    String nomEntreprise;
    long noVendeur;
    long noClient;
    Panel colQuantite;
    Panel colAjout;
    TextBox tbQuantite;
    private void getNbMois()
    {

        if (Request.QueryString["NoProduit"] == null)
        {
            string url = "~/Pages/Connexion.aspx?";
            Response.Redirect(url);
        }
        else
        {
            this.noProduit = Convert.ToInt32(Request.QueryString["NoProduit"]);
        }


    }

    protected void Page_Load(object sender, EventArgs e)
    {

        verifierPermissions("C");

        getNbMois();
        long parseNoClient;
        if (Session["NoClient"] != null && long.TryParse(Session["NoClient"].ToString(), out parseNoClient))
        {
            if (dbContext.PPProduits.Where(c => c.NoProduit.Equals(noProduit)).Any())
                noVendeur = dbContext.PPProduits.Where(c => c.NoProduit.Equals(noProduit)).First().NoVendeur.Value;
            noClient = parseNoClient;
        }
        else
        {
            string url = "~/Pages/Connexion.aspx?";
            Response.Redirect(url);
        }


        if (!IsPostBack)
        {
            if (Request.UrlReferrer != null)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString();
            }
        }
        PPProduits produitAfficher = new PPProduits();
        if (dbContext.PPProduits.Where(c => c.NoProduit == noProduit).Any())
             produitAfficher = dbContext.PPProduits.Where(c => c.NoProduit == noProduit).First();

        Page.Title = produitAfficher.Nom;

        nomEntreprise = dbContext.PPVendeurs.Where(c => c.NoVendeur == produitAfficher.NoVendeur).First().NomAffaires;


        // Créer le panier du vendeur X
        Panel panelBase = LibrairieControlesDynamique.divDYN(phDynamique, nomEntreprise + "_base", "panel panel-default");

        // Nom de l'entreprise
        Panel panelHeader = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_header", "panel-heading");
        Panel rowTitreMessage = LibrairieControlesDynamique.divDYN(panelHeader, nomEntreprise + "_rowTitreMessage", "row");
        Panel colLeTitre = LibrairieControlesDynamique.divDYN(rowTitreMessage, nomEntreprise + "_colTitreUnique_", "col-sm-11");
        LibrairieControlesDynamique.lblDYN(colLeTitre, nomEntreprise + "_nom", nomEntreprise, "nom-entreprise");
        Panel colMessage = LibrairieControlesDynamique.divDYN(rowTitreMessage, nomEntreprise + "_colEnvoieMessage_", "col-sm-1");
        LibrairieControlesDynamique.htmlbtnDYN(colMessage, nomEntreprise + "leIconeMessage", "", "", "glyphicon glyphicon-envelope", btnEnvoyer);

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

        decimal? montantRabais = produitAfficher.PrixDemande - produitAfficher.PrixVente;
        if (produitAfficher.DateVente < DateTime.Now) { montantRabais = 0; }


        String urlImage = "../static/images/" + produitAfficher.Photo;

        Panel rowItem = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowItem_" + idItem, "row");

        // ajouter l'image
        Panel colImg = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colImg_" + idItem, "col-sm-1");
        LibrairieControlesDynamique.imgDYN(colImg, nomEntreprise + "_img_" + idItem, urlImage, "img-size center-block");
        LibrairieControlesDynamique.lblDYN(colImg, nomEntreprise + "_noproduit_" + idItem, idItem.ToString(), "caption center-block text-center");


        // Nom du produit
        Panel colNom = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colNom_" + idItem, "col-sm-3 breakWord");
        //LibrairieControlesDynamique.lbDYN(colNom, nomEntreprise + "_nom_" + idItem, nomProduit,null);
        LibrairieControlesDynamique.lblDYN(colNom, nomEntreprise + "_nom_" + idItem, produitAfficher.Nom, "cat_item");

        // Quantité restant
        Panel colQuantite = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colQuantiteProduit_" + idItem, "col-sm-1");
        LibrairieControlesDynamique.lblDYN(colQuantite, nomEntreprise + "_quantiteProduit_" + idItem, "Qte : " + produitAfficher.NombreItems, "border-quantite cat_item");

        Panel colPoids = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPoids_" + idItem, "col-sm-1");
        LibrairieControlesDynamique.lblDYN(colPoids, nomEntreprise + "_Poids_" + idItem, produitAfficher.Poids + " lbs", "cat_item");

        // Categorie
        string categorie = dbContext.PPCategories.Where(c => c.NoCategorie == produitAfficher.NoCategorie).First().Description;
        Panel colCat = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colCategorie_" + idItem, "col-sm-2 breakWord");
        LibrairieControlesDynamique.lblDYN(colCat, nomEntreprise + "_categorie_" + idItem, categorie, "cat_item");

        // Prix item
        Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPrix_" + idItem, "col-sm-1 text-right");
        LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prixDemande_" + idItem, prix.ToString("C", CultureInfo.CurrentCulture), "prix_item");
        LibrairieControlesDynamique.brDYN(colPrix);
        LibrairieControlesDynamique.lblDYN(colPrix, "lblRabais" + idItem, (montantRabais > 0) ? "Rabais de " + Decimal.Round((Decimal)montantRabais, 2).ToString("C", CultureInfo.CurrentCulture) : "", "rabais");



        // Quantité restant
        colQuantite = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colQuantite_" + idItem, "col-sm-1 text-center");

        tbQuantite = LibrairieControlesDynamique.numericUpDownDYN(colQuantite, "quantite_" + idItem, (produitAfficher.NombreItems < 1) ? "0" : "1"
        , (produitAfficher.NombreItems < 1) ? "0" : produitAfficher.NombreItems.ToString(), "numUD form-control border-quantite");
        LibrairieControlesDynamique.lblDYN(colQuantite, nomEntreprise + "_nbQuantiteMax_" + idItem, "<br>Quantité : " + produitAfficher.NombreItems, "");



        colAjout = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colAjout_" + idItem, "col-sm-2 text-center");
        Button btnAjout = LibrairieControlesDynamique.btnDYN(colAjout, "btnAjouter_" + idItem, "btn valignMessage btnPageOrange", "Ajouter au panier", btnAjouter_click);
        btnAjout.UseSubmitBehavior = false;
        LibrairieControlesDynamique.lblDYN(colAjout, nomEntreprise + "_espaceGlyph_" + idItem, "<br>", "");
        long leVendeurProduit = produitAfficher.NoVendeur.Value;
        short nbProduitEnPanier = 0;
        if (dbContext.PPArticlesEnPanier.Where(c => (c.NoClient.Value.Equals(noClient)) && (c.NoVendeur.Value.Equals(leVendeurProduit)) && (c.NoProduit.Value.Equals(idItem))).Any())
            nbProduitEnPanier = dbContext.PPArticlesEnPanier.Where(c => (c.NoClient.Value.Equals(noClient)) && (c.NoVendeur.Value.Equals(leVendeurProduit)) && (c.NoProduit.Value.Equals(idItem))).First().NbItems.Value;
        LibrairieControlesDynamique.lblDYN(colAjout, nomEntreprise + "_nbEnPanier_" + idItem, "(" + nbProduitEnPanier + ")", "glyphicon glyphicon-shopping-cart");

        if (produitAfficher.NombreItems < 1)
        {
            tbQuantite.Enabled = false;
            LibrairieControlesDynamique.lblDYN(colQuantite, "lblRupture" + idItem, "<br>Rupture de stock", "rupture-stock");
            btnAjout.Enabled = false;
            btnAjout.CssClass = "btn valignMessage btnPageOrange disabled";
        }
        else
        {
            tbQuantite.Enabled = true;
            btnAjout.Enabled = true;
            btnAjout.CssClass = "btn valignMessage btnPageOrange";
        }




        Panel rowDescription = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowDescription_" + idItem, "row top15");
        LibrairieControlesDynamique.lblDYN(rowDescription, nomEntreprise + "_descTitle_" + idItem, "Description", "cat_item col-sm-2 center-block text-center");
        LibrairieControlesDynamique.lblDYN(rowDescription, nomEntreprise + "_description_" + idItem, produitAfficher.Description, "description_item cat_item col-sm-10 center-block breakWord ");
        LibrairieControlesDynamique.hrDYN(panelBody);
        Panel panelRetour = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_retour_", "row center-block text-center Retour");
        LibrairieControlesDynamique.lbDYN(panelRetour, "back_", "Retour", btnRetour);


    }

    private void btnEnvoyer(object sender, EventArgs e)
    {
        PPClients leClient = new PPClients();
        if (dbContext.PPClients.Where(c => c.NoClient == noClient).Any()) leClient = dbContext.PPClients.Where(c => c.NoClient == noClient).First();
        PPVendeurs leVendeur = new PPVendeurs();
        if (dbContext.PPVendeurs.Where(c => c.NoVendeur == noVendeur).Any()) leVendeur = dbContext.PPVendeurs.Where(c => c.NoVendeur == noVendeur).First();
        PPProduits produitAfficher = new PPProduits();
        if (dbContext.PPProduits.Where(c => c.NoProduit == noProduit).Any())
            produitAfficher = dbContext.PPProduits.Where(c => c.NoProduit == noProduit).First();
        MailMessage message = new MailMessage(leVendeur.AdresseEmail, leClient.AdresseEmail);
        message.Subject = noProduit + " : " + produitAfficher.Nom;
        message.Body = string.Format("Bonjour,\n\n" +
                                     "J'ai quelques questions à propos du produit {0} \n\n" +
                                     "Bonne journée\n"+
                                     "{1}, {2}\n",  
                                     noProduit,
                                     leClient.Nom,
                                     leClient.Prenom);

        tbExpediteur.Text = message.From.ToString();
        tbDestinataire.Text = message.To.ToString();
        tbSujet.Text = message.Subject;
        tbCorps.Text = message.Body;
        divCourriel.Visible = !divCourriel.Visible;
    }

    private void btnAjouter_click(object sender, EventArgs e)
    {
        Button btnAjout = (Button)sender;
        long noProduit = long.Parse(btnAjout.ID.Replace("btnAjouter_", ""));        
        short nbItems = short.Parse(tbQuantite.Text.Trim());
        PPArticlesEnPanier nouvelArticle = new PPArticlesEnPanier();        
        List<PPArticlesEnPanier> articleExistants = dbContext.PPArticlesEnPanier.Where(c => (c.NoClient == noClient) && (c.NoVendeur == noVendeur) && (c.NoProduit == noProduit)).ToList();

        if (dbContext.PPProduits.Where(c => c.NoProduit == noProduit).Any())
        {
            if (nbItems > dbContext.PPProduits.Where(c => c.NoProduit == noProduit).First().NombreItems.Value)
            {
                nbItems = dbContext.PPProduits.Where(c => c.NoProduit == noProduit).First().NombreItems.Value;                
                tbQuantite.Text = nbItems.ToString();
            }
        }

        DateTime dateNow = DateTime.Now.Date;
        List<PPVendeursClients> nouvelVisite = dbContext.PPVendeursClients.Where(c => (c.NoClient == noClient) && (c.NoVendeur == noVendeur) && (c.DateVisite >= dateNow)).ToList();
        PPVendeursClients modVisite = new PPVendeursClients();
        if (!nouvelVisite.Any())
        {
            modVisite.NoClient = noClient;
            modVisite.NoVendeur = noVendeur;
            modVisite.DateVisite = DateTime.Now;
            dbContext.PPVendeursClients.Add(modVisite);
        }


        if (articleExistants.Count > 0)
        {
            PPArticlesEnPanier articleExistant = articleExistants.First();
            articleExistant.NoClient = noClient;
            articleExistant.NoVendeur = noVendeur;
            articleExistant.NoProduit = noProduit;
            articleExistant.DateCreation = DateTime.Now;
            articleExistant.NbItems = nbItems;
        }
        else
        {
            nouvelArticle.NoPanier = dbContext.PPArticlesEnPanier.Max(c => c.NoPanier) + 1;
            nouvelArticle.NoClient = noClient;
            nouvelArticle.NoVendeur = noVendeur;
            nouvelArticle.NoProduit = noProduit;
            nouvelArticle.DateCreation = DateTime.Now;
            nouvelArticle.NbItems = nbItems;
            dbContext.PPArticlesEnPanier.Add(nouvelArticle);
        }



        try
        {
            dbContext.SaveChanges();

            Label lblEnPanier = (Label)colAjout.FindControl(nomEntreprise + "_nbEnPanier_" + noProduit);
            short nbProduitEnPanier = 0;
            if (dbContext.PPArticlesEnPanier.Where(c => (c.NoClient.Value.Equals(noClient)) && (c.NoVendeur.Value.Equals(noVendeur)) && (c.NoProduit.Value.Equals(noProduit))).Any())
                nbProduitEnPanier = dbContext.PPArticlesEnPanier.Where(c => (c.NoClient.Value.Equals(noClient)) && (c.NoVendeur.Value.Equals(noVendeur)) && (c.NoProduit.Value.Equals(noProduit))).First().NbItems.Value;
            lblEnPanier.Text = "(" + nbProduitEnPanier + ")";
            
            Panel row = LibrairieControlesDynamique.divDYN(messageAction, nomEntreprise + "_rowPanierVide", "row marginFluid text-center");
            Panel message = LibrairieControlesDynamique.divDYN(row, nomEntreprise + "_messagePanierVide", "message text-center top15");
            Panel messageContainer = LibrairieControlesDynamique.divDYN(message, nomEntreprise + "_divMessage", "alert alert-success alert-margins");
            string strMessage = "Le produit a été ajouté au panier";
            if (nbItems >1)
                strMessage = "Les produits ont été ajoutés au panier";
            LibrairieControlesDynamique.lblDYN(messageContainer, nomEntreprise + "_leMessageLabel", strMessage);
        }
        catch (Exception ex) { }
    }

    protected void btnRetour(object sender, EventArgs e)
    {
        object refUrl = ViewState["RefUrl"];
        if (refUrl != null)
            Response.Redirect((string)refUrl);
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
}