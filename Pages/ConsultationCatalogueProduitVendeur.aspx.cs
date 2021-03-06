﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class Pages_ConsultationCatalogueProduitVendeur : System.Web.UI.Page
{

    private BD6B8_424SEntities dbContext = new BD6B8_424SEntities();
    long noVendeur;
    String nomEntreprise = "";
    private int intNbPage;
    private int indexPages;
    bool booTriDate = false;
    bool booTriNumero = false;
    int noPage = 1;
    long noClient;
    Panel panelBody;
    Panel colQuantite;
    Panel colAjout;
    //protected void Page_

    protected void Page_Load(object sender, EventArgs e)
    {

        booTriDate = Convert.ToBoolean(Session["booTriDate"]);
        booTriNumero = Convert.ToBoolean(Session["booTri"]);
        verifierPermissions("C");

        if (Request.QueryString["NoPage"] != null)
        {
            noPage = Convert.ToInt32(Request.QueryString["NoPage"]);

        }
        long parseNoVendeur;
        long parseNoClient;
        if(Session["NoVendeurCatalogue"] != null && Session["NoClient"] != null && long.TryParse(Session["NoVendeurCatalogue"].ToString(),out parseNoVendeur) && long.TryParse(Session["NoClient"].ToString(), out parseNoClient))
        {
            noVendeur = parseNoVendeur;
            noClient = parseNoClient;
        }
        else
        {
            string url = "~/Pages/Connexion.aspx?";
            Response.Redirect(url);
        }
       
        int result;
        if (intNbPage == 0 && int.TryParse(ddlNbParPage.SelectedItem.Text, out result))
            intNbPage = result;
        else
            intNbPage = int.MaxValue;

        Page.Title = nomEntreprise = dbContext.PPVendeurs.Where(c => c.NoVendeur == noVendeur).First().NomAffaires;

        if (!IsPostBack)
        {

            if (Request.QueryString["search"] != null)
            {
                _tbsearchText.Text = Request.QueryString["search"];

            }

            if (Session["ddlNbPage"] != null)
            {
                ddlNbParPage.SelectedIndex = ddlNbParPage.Items.IndexOf(ddlNbParPage.Items.FindByText(Convert.ToString(Session["ddlNbPage"])));
                intNbPage = Convert.ToInt32(Session["ddlNbPage"]);
            }

            List<int> lstCategories = dbContext.PPProduits.Where(c => c.NoVendeur == noVendeur).GroupBy(c => c.NoCategorie).Select(c => c.Key.Value).ToList();
            ddlCategorie.Items.Insert(0, new ListItem("Tous les catégories", "1"));
            for (int i = 0; i < lstCategories.Count; i++)
            {
                int nbCat = lstCategories[i];

                string strDescCat = dbContext.PPCategories.Where(c => c.NoCategorie == nbCat).First().Description;
                ddlCategorie.Items.Insert(i + 1, new ListItem(strDescCat, nbCat.ToString()));
            }
            if (Session["ddlCategorie"] != null)
            {
                ddlCategorie.SelectedIndex = ddlCategorie.Items.IndexOf(ddlCategorie.Items.FindByValue(Convert.ToString(Session["ddlCategorie"])));
            }

            creerPage();

        }
        else
        {
            creerPage();
        }
    }

    private void creerPage()
    {
        // Nom de l'entreprise
        PPVendeurs vendeur;
        phDynamique.Controls.Clear();
        //_header.Controls.Clear();

       
        Panel colImage = LibrairieControlesDynamique.divDYN(_rowHeader_, nomEntreprise + "_colImage_", "col-sm-2 text-center");
        if(dbContext.PPVendeurs.Where(c => c.NoVendeur == noVendeur).Any())
        {
            vendeur = dbContext.PPVendeurs.Where(c => c.NoVendeur == noVendeur).First();          
            XDocument docXml = XDocument.Load(Server.MapPath("\\static\\xml\\" + vendeur.Configuration));
            XElement elements = docXml.Element("configuration");
            String urlImg = "~/static/images/" + elements.Descendants("urlImage").Single().Value;
            System.Web.UI.WebControls.Image img = LibrairieControlesDynamique.imgDYN(colImage, "", urlImg, "");
            img.Style.Add("width", "100px");
            String urlBackColor = elements.Descendants("couleurFond").Single().Value;
            String urlForeColor = elements.Descendants("couleurTexte").Single().Value;
            _base.BackColor = ColorTranslator.FromHtml(urlBackColor);
            _base.ForeColor = ColorTranslator.FromHtml(urlForeColor);
               
        }
            
       
        Panel colEntreprise = LibrairieControlesDynamique.divDYN(_rowHeader_, nomEntreprise + "_colEntreprise_", "col-sm-6");
        LibrairieControlesDynamique.lblDYN(colEntreprise, nomEntreprise + "_nom", nomEntreprise, "nom-entreprise");
        Panel colVendeurs = LibrairieControlesDynamique.divDYN(_rowHeader_, nomEntreprise + "_colDdlVendeur_", "col-sm-4 text-right prix_item");
        LibrairieControlesDynamique.lblDYN(colVendeurs, nomEntreprise + "_lblDDLVendeur", "Choisir un vendeur : ", "");
        DropDownList ddlVendeurs = LibrairieControlesDynamique.ddlDYN(colVendeurs, "lesVendeurs", "");
        ddlVendeurs.Items.Clear();
        ddlVendeurs.Items.Insert(0, new ListItem("Tous les vendeurs", ""));
        ddlVendeurs.AutoPostBack = true;
        ddlVendeurs.EnableViewState = true;
        List<PPVendeurs> lesVendeurs = dbContext.PPVendeurs.Where(c => (c.Statut.Value.Equals(1)) && (c.PPProduits.Count() > 0) ).ToList();      
        for (int i = 0; i < lesVendeurs.Count; i++)
        {
            long leNoVendeur = lesVendeurs[i].NoVendeur;
            string strNomEntreprise = lesVendeurs[i].NomAffaires;
            ddlVendeurs.Items.Insert(i+1, new ListItem(strNomEntreprise, leNoVendeur.ToString()));
            
        }
        ddlVendeurs.SelectedIndexChanged += ddlVendeurIndex;
        if (Convert.ToString(Session["NoVendeurCatalogue"]) != null && Convert.ToString(Session["NoVendeurCatalogue"]) != "")
            ddlVendeurs.SelectedIndex = ddlVendeurs.Items.IndexOf(ddlVendeurs.Items.FindByValue(Convert.ToString(Session["NoVendeurCatalogue"])));
        else if(ddlVendeurs.Items.Count > 0)
            ddlVendeurs.SelectedIndex = 0;
        // ddlVendeurs.SelectedItem.Value = noVendeur.ToString();

        // Liste des items + le total
        panelBody = LibrairieControlesDynamique.divDYN(phDynamique, nomEntreprise + "_body", "panel-body");
        int nbProduitsVendeur = dbContext.PPProduits.Where(c => c.NoVendeur == noVendeur).Count();


        // Rajouter les produits dans le panier
        if (nbProduitsVendeur > 0)
        {

            int laCategorieDDL = Convert.ToInt32(ddlCategorie.SelectedValue);
            Dictionary<string, List<PPProduits>> getProduitsSearch;
            if (laCategorieDDL != 1)
            {                
                getProduitsSearch = getProduitsUneCategorie(_tbsearchText.Text.Trim());
            }
            else if (Convert.ToBoolean(Session["booTriDateStart"]))
            {
                getProduitsSearch = getProduitsParDate(_tbsearchText.Text.Trim());
            }
            else if (Convert.ToBoolean(Session["booTriNumeroStart"]))
            {
                getProduitsSearch = getProduitsParNumero(_tbsearchText.Text.Trim());
            }
            else
            {
                getProduitsSearch = getProduitsVendeurParCategorie(_tbsearchText.Text.Trim());
            }

            foreach (var keys in getProduitsSearch)
            {
                string descCategorie = keys.Key;
                if (descCategorie != "")
                {
                    Panel panCategorie = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_categorie_" + descCategorie, "flex-container");
                    Panel colCatAfficher = LibrairieControlesDynamique.divDYN(panCategorie, nomEntreprise + "_colLabelCategorie" + descCategorie, "");
                    LibrairieControlesDynamique.lblDYN(colCatAfficher, nomEntreprise + "_labelCategorie" + descCategorie, descCategorie, "infos-payage");
                }

                foreach (PPProduits produitCat in keys.Value)
                {
                    long idItem = Convert.ToInt64(produitCat.NoProduit);
                    String nomProduit = produitCat.Nom.ToString();
                    Double prix;

                    if (produitCat.DateVente > DateTime.Now.Date)
                    {
                        prix = Convert.ToDouble(produitCat.PrixVente);
                    }
                    else
                    {
                        prix = Convert.ToDouble(produitCat.PrixDemande);
                    }
                    decimal? montantRabais = produitCat.PrixDemande - produitCat.PrixVente;
                    if (produitCat.DateVente < DateTime.Now) { montantRabais = 0; }
                    String urlImage = "../static/images/" + produitCat.Photo;
                    int noCat = produitCat.NoCategorie.Value;
                    PPCategories pCategorie = dbContext.PPCategories.Where(c => c.NoCategorie.Equals(noCat)).First();
                    String categorie = pCategorie.Description;

                    Panel rowItem = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowItem_" + idItem, "row valign top15");

                    // ajouter l'image
                    Panel colImg = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colImg_" + idItem, "col-sm-2");
                    LibrairieControlesDynamique.imgDYN(colImg, nomEntreprise + "_img_" + idItem, urlImage, "img-size center-block");
                    LibrairieControlesDynamique.lblDYN(colImg, nomEntreprise + "_noproduit_" + idItem, idItem.ToString(), "caption center-block text-center");

                    // Nom du produit
                    Panel colNom = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colNom_" + idItem, "col-sm-3 text-justify LiensProduits nomClient");
                    LinkButton leProduit = LibrairieControlesDynamique.lbDYN(colNom, nomEntreprise + "_nom_" + idItem, nomProduit, null);
                    leProduit.PostBackUrl = "~/Pages/AffichageProduitDetaille.aspx?&NoProduit=" + idItem;

                    // Categorie
                    Panel colCat = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colCategorie_" + idItem, "col-sm-2");
                    LibrairieControlesDynamique.lblDYN(colCat, nomEntreprise + "_categorie_" + idItem, categorie, "cat_item");

                    // Prix item
                    Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPrix_" + idItem, "col-sm-1 text-right");
                    LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prixDemande_" + idItem, prix.ToString("C", CultureInfo.CurrentCulture), "prix_item");
                    LibrairieControlesDynamique.brDYN(colPrix);
                    LibrairieControlesDynamique.lblDYN(colPrix, "lblRabais" + idItem, (montantRabais > 0) ? "Rabais de " + Decimal.Round((Decimal)montantRabais, 2).ToString("C", CultureInfo.CurrentCulture) : "", "rabais");

                    // Quantité restant
                    colQuantite = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colQuantite_" + idItem, "col-sm-2 text-center");                   

                    TextBox tbQuantite = LibrairieControlesDynamique.numericUpDownDYN(colQuantite, "quantite_" + idItem, (produitCat.NombreItems < 1) ? "0" : "1"
                    , (produitCat.NombreItems < 1) ? "0" : produitCat.NombreItems.ToString(), "numUD form-control border-quantite");
                    LibrairieControlesDynamique.lblDYN(colQuantite, nomEntreprise + "_nbQuantiteMax_" + idItem, "<br>Quantité : " + produitCat.NombreItems, "");

                    colAjout = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colAjout_" + idItem, "col-sm-2 text-center");
                    Button btnAjout = LibrairieControlesDynamique.btnDYN(colAjout, "btnAjouter_" + idItem, "btn valignMessage btnPageOrange", "Ajouter au panier", btnAjouter_click);
                    btnAjout.UseSubmitBehavior = false;
                    LibrairieControlesDynamique.lblDYN(colAjout, nomEntreprise + "_espaceGlyph_" + idItem, "<br>", "");
                    long leVendeurProduit = produitCat.NoVendeur.Value;
                    short nbProduitEnPanier = 0;
                    if (dbContext.PPArticlesEnPanier.Where(c => (c.NoClient.Value.Equals(noClient)) && (c.NoVendeur.Value.Equals(leVendeurProduit)) && (c.NoProduit.Value.Equals(idItem))).Any())
                        nbProduitEnPanier = dbContext.PPArticlesEnPanier.Where(c => (c.NoClient.Value.Equals(noClient)) && (c.NoVendeur.Value.Equals(leVendeurProduit)) && (c.NoProduit.Value.Equals(idItem))).First().NbItems.Value;
                    LibrairieControlesDynamique.lblDYN(colAjout, nomEntreprise + "_nbEnPanier_" + idItem, "(" + nbProduitEnPanier + ")", "glyphicon glyphicon-shopping-cart");

                    if (produitCat.NombreItems < 1)
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


                }

            }

        }

    }

    private void ddlVendeurIndex(object sender, EventArgs e)
    {
        DropDownList ddlVendeur = (DropDownList)sender;
        String url = "";
        if (ddlVendeur.SelectedValue != "")
        {
            Session["NoVendeurCatalogue"] = ddlVendeur.SelectedValue;
            url = "~/Pages/ConsultationCatalogueProduitVendeur.aspx?";           
        }
        else
        {
            Session["NoVendeurCatalogue"] = "";
            url = "~/Pages/searchClient.aspx?";           
        }
        Response.Redirect(url, true);
    }

    private void btnAjouter_click(object sender, EventArgs e)
    {
        Button btnAjout = (Button)sender;
        long noProduit = long.Parse(btnAjout.ID.Replace("btnAjouter_", ""));
        TextBox tb = (TextBox)colQuantite.FindControl("quantite_" + noProduit);
        short nbItems = short.Parse(tb.Text.Trim());
        PPArticlesEnPanier nouvelArticle = new PPArticlesEnPanier();
        System.Diagnostics.Debug.WriteLine(" NO CLIENT "+ noClient + " noVendeur " + noVendeur + " noProduit " + noProduit);
        List<PPArticlesEnPanier> articleExistants = dbContext.PPArticlesEnPanier.Where(c => (c.NoClient == noClient) && (c.NoVendeur == noVendeur) && (c.NoProduit == noProduit)).ToList();

        if (dbContext.PPProduits.Where(c => c.NoProduit == noProduit).Any())
        {
            if (nbItems > dbContext.PPProduits.Where(c => c.NoProduit == noProduit).First().NombreItems.Value)
            {
                nbItems = dbContext.PPProduits.Where(c => c.NoProduit == noProduit).First().NombreItems.Value;
                TextBox tbQuantite = (TextBox)colQuantite.FindControl("quantite_" + noProduit);
                tbQuantite.Text = nbItems.ToString();
            }
        }

        DateTime dateNow = DateTime.Now.Date;
        List<PPVendeursClients> nouvelVisite = dbContext.PPVendeursClients.Where(c => (c.NoClient == noClient) && (c.NoVendeur == noVendeur) && (c.DateVisite >= dateNow) ).ToList();
        PPVendeursClients modVisite = new PPVendeursClients();
        if (!nouvelVisite.Any())
        {
            modVisite.NoClient = noClient;
            modVisite.NoVendeur = noVendeur;
            modVisite.DateVisite = DateTime.Now;
            dbContext.PPVendeursClients.Add(modVisite);
        }
   

        if (articleExistants.Count > 0 )
        {
            PPArticlesEnPanier articleExistant = articleExistants.First();          
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
            if (nbItems > 1)
                strMessage = "Les produits ont été ajoutés au panier";
            LibrairieControlesDynamique.lblDYN(messageContainer, nomEntreprise + "_leMessageLabel", strMessage);          
        } catch (Exception ex) { }
    }

    // aller chercher les paniers
    public Dictionary<string, List<PPProduits>> getProduitsVendeurParCategorie(string strSearch)
    {

        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableProduits = dbContext.PPProduits;
        // retourner la liste des paniers
        Dictionary<string, List<PPProduits>> lstProduits = new Dictionary<string, List<PPProduits>>();

        int countProduits = countProduits = dbContext.PPProduits.Where(c => (c.NoVendeur == noVendeur) && (c.Nom.Contains(strSearch)) && (c.Disponibilité == true) ).Count();
        creerBtnPages(countProduits);
        var produits = from produit in tableProduits.Where(c => (c.NoVendeur == noVendeur) && (c.Nom.Contains(strSearch)) && (c.Disponibilité == true) ).OrderBy(c => c.NoCategorie).ThenBy(c => c.Description).Skip(intNbPage * (noPage - 1)).Take(intNbPage)
                       group produit by new { produit.NoCategorie }
                                         into listeProduits
                       select listeProduits;


        foreach (var articlesProduits in produits)
        {
            List<PPProduits> lstTempo;

            foreach (var lesProduits in articlesProduits)
            {
                string cat = dbContext.PPCategories.Where(c => c.NoCategorie == articlesProduits.Key.NoCategorie).First().Description;
                if (lstProduits.TryGetValue(cat, out lstTempo))
                {
                    lstTempo.Add(lesProduits);
                }
                else
                {
                    List<PPProduits> lst = new List<PPProduits>();
                    lst.Add(lesProduits);
                    lstProduits.Add(cat, lst);
                }
            }

        }
        return lstProduits;
    }

    private void creerBtnPages(int nbProduits)
    {

        if (nbProduits < 1)
        {
            Panel row = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowProduitsVide", "row marginFluid text-center ");
            Panel message = LibrairieControlesDynamique.divDYN(row, nomEntreprise + "_messageProduitsVide", "message text-center top15");
            Panel messageContainer = LibrairieControlesDynamique.divDYN(message, nomEntreprise + "_divMessageProduits", "alert alert-danger alert-margins valignMessage");
            LibrairieControlesDynamique.lblDYN(messageContainer, nomEntreprise + "_leMessageLabelProduits", "Votre recherche ne correspond à aucun article.");
        }

        ulPages.Controls.Clear();
        ulPagesTop.Controls.Clear();
        decimal decPages = Convert.ToDecimal(nbProduits) / Convert.ToDecimal(intNbPage);
        noPage = noPage > Math.Ceiling(decPages) ? 1 : noPage;
        int maximumIndex = (Math.Ceiling(decPages) >= (noPage + 2)) ? (noPage + 2) : (Math.Ceiling(decPages) >= (noPage + 1)) ? (noPage + 1) : noPage;
        int nbAfficher = maximumIndex == (noPage + 1) ? 4 : maximumIndex == (noPage) ? 5 : 3;
        int indexPages = ((noPage - nbAfficher) < 1) ? 0 : (noPage - nbAfficher);
        for (int i = indexPages; i < maximumIndex; i++)
        {
            if ((i + 1) == noPage)
            {
                LibrairieControlesDynamique.liDYN(ulPages, "ConsultationCatalogueProduitVendeur.aspx?&NoPage=" + (i + 1).ToString() + "&search=" + _tbsearchText.Text.Trim(), (i + 1).ToString(), "btnPageSelected btn right5");
                LibrairieControlesDynamique.liDYN(ulPagesTop, "ConsultationCatalogueProduitVendeur.aspx?&NoPage=" + (i + 1).ToString() + "&search=" + _tbsearchText.Text.Trim(), (i + 1).ToString(), "btnPageSelected btn right5");
            }
            else
            {
                LibrairieControlesDynamique.liDYN(ulPages, "ConsultationCatalogueProduitVendeur.aspx?&NoPage=" + (i + 1).ToString() + "&search=" + _tbsearchText.Text.Trim(), (i + 1).ToString(), "btnPageOrange btn right5");
                LibrairieControlesDynamique.liDYN(ulPagesTop, "ConsultationCatalogueProduitVendeur.aspx?&NoPage=" + (i + 1).ToString() + "&search=" + _tbsearchText.Text.Trim(), (i + 1).ToString(), "btnPageOrange btn right5");
            }

        }
    }

    public Dictionary<string, List<PPProduits>> getProduitsParNumero(string strSearch)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableProduits = dbContext.PPProduits;
        // retourner la liste des paniers
        Dictionary<string, List<PPProduits>> lstProduits = new Dictionary<string, List<PPProduits>>();

        int countProduits = countProduits = dbContext.PPProduits.Where(c => (c.NoVendeur == noVendeur) && (c.Nom.Contains(strSearch)) && (c.Disponibilité == true) ).Count();
        creerBtnPages(countProduits);
        var produits = from produit in tableProduits.Where(c => (c.NoVendeur == noVendeur) && (c.Nom.Contains(strSearch)) && (c.Disponibilité == true) ).OrderBy(c => c.NoProduit).Skip(intNbPage * (noPage - 1)).Take(intNbPage)
                       select produit;

        if (!booTriNumero)
        {
            produits = from produit in tableProduits.Where(c => (c.NoVendeur == noVendeur) && (c.Nom.Contains(strSearch)) && (c.Disponibilité == true) ).OrderByDescending(c => c.NoProduit).Skip(intNbPage * (noPage - 1)).Take(intNbPage)
                       select produit;
        }

        List<PPProduits> lstTempo;

        foreach (var lesProduits in produits)
        {
            if (lstProduits.TryGetValue("", out lstTempo))
            {
                lstTempo.Add(lesProduits);
            }
            else
            {
                List<PPProduits> lst = new List<PPProduits>();
                lst.Add(lesProduits);
                lstProduits.Add("", lst);
            }
        }

        return lstProduits;
    }
    public Dictionary<string, List<PPProduits>> getProduitsParDate(string strSearch)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableProduits = dbContext.PPProduits;
        // retourner la liste des paniers
        Dictionary<string, List<PPProduits>> lstProduits = new Dictionary<string, List<PPProduits>>();

        int countProduits = countProduits = dbContext.PPProduits.Where(c => (c.NoVendeur == noVendeur) && (c.Nom.Contains(strSearch)) && (c.Disponibilité == true) ).Count();
        creerBtnPages(countProduits);
        var produits = from produit in tableProduits.Where(c => (c.NoVendeur == noVendeur) && (c.Nom.Contains(strSearch)) && (c.Disponibilité == true) ).OrderBy(c => c.DateCreation).Skip(intNbPage * (noPage - 1)).Take(intNbPage)
                       select produit;

        if (!booTriDate)
        {
            produits = from produit in tableProduits.Where(c => (c.NoVendeur == noVendeur) && (c.Nom.Contains(strSearch)) && (c.Disponibilité == true) ).OrderByDescending(c => c.DateCreation).Skip(intNbPage * (noPage - 1)).Take(intNbPage)
                       select produit;
        }
        List<PPProduits> lstTempo;

        foreach (var lesProduits in produits)
        {
            if (lstProduits.TryGetValue("", out lstTempo))
            {
                lstTempo.Add(lesProduits);
            }
            else
            {
                List<PPProduits> lst = new List<PPProduits>();
                lst.Add(lesProduits);
                lstProduits.Add("", lst);
            }
        }

        return lstProduits;
    }

    public Dictionary<string, List<PPProduits>> getProduitsUneCategorie(string strSearch)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableProduits = dbContext.PPProduits;
        // retourner la liste des paniers
        Dictionary<string, List<PPProduits>> lstProduits = new Dictionary<string, List<PPProduits>>();
        int laCategorieDDL = Convert.ToInt32(ddlCategorie.SelectedValue);
        int countProduits = countProduits = dbContext.PPProduits.Where(c => (c.NoVendeur == noVendeur) && (c.Nom.Contains(strSearch)) && (c.NoCategorie == laCategorieDDL) && (c.Disponibilité == true) ).Count();
        creerBtnPages(countProduits);
        var produits = from produit in tableProduits.Where(c => (c.NoVendeur == noVendeur) && (c.Nom.Contains(strSearch)) && (c.NoCategorie == laCategorieDDL) && (c.Disponibilité == true) ).OrderBy(c => c.NoCategorie).ThenBy(c => c.Description).Skip(intNbPage * (noPage - 1)).Take(intNbPage)
                       group produit by new { produit.NoCategorie }
                                         into listeProduits
                       select listeProduits;

        if (Convert.ToBoolean(Session["booTriDateStart"]))
        {
            if (!booTriDate)
            {
                produits = from produit in tableProduits.Where(c => (c.NoVendeur == noVendeur) && (c.Nom.Contains(strSearch)) && (c.Disponibilité == true) && (c.NoCategorie == laCategorieDDL)).OrderByDescending(c => c.DateCreation).Skip(intNbPage * (noPage - 1)).Take(intNbPage)
                           group produit by new { produit.NoCategorie }
                                             into listeProduits
                           select listeProduits;
            }
            else if (booTriDate)
            {
                produits = from produit in tableProduits.Where(c => (c.NoVendeur == noVendeur) && (c.Nom.Contains(strSearch)) && (c.Disponibilité == true) && (c.NoCategorie == laCategorieDDL)).OrderBy(c => c.DateCreation).Skip(intNbPage * (noPage - 1)).Take(intNbPage)
                           group produit by new { produit.NoCategorie }
                                             into listeProduits
                           select listeProduits;
            }
        }
        else if (Convert.ToBoolean(Session["booTriNumeroStart"]))
        {
            if (!booTriNumero)
            {
                produits = from produit in tableProduits.Where(c => (c.NoVendeur == noVendeur) && (c.Nom.Contains(strSearch)) && (c.Disponibilité == true) && (c.NoCategorie == laCategorieDDL)).OrderByDescending(c => c.NoProduit).Skip(intNbPage * (noPage - 1)).Take(intNbPage)
                           group produit by new { produit.NoCategorie }
                                             into listeProduits
                           select listeProduits;
            }
            else if (booTriNumero)
            {
                produits = from produit in tableProduits.Where(c => (c.NoVendeur == noVendeur) && (c.Nom.Contains(strSearch)) && (c.Disponibilité == true) && (c.NoCategorie == laCategorieDDL)).OrderBy(c => c.NoProduit).Skip(intNbPage * (noPage - 1)).Take(intNbPage)
                           group produit by new { produit.NoCategorie }
                                             into listeProduits
                           select listeProduits;
            }
        }


        foreach (var articlesProduits in produits)
        {
            List<PPProduits> lstTempo;

            foreach (var lesProduits in articlesProduits)
            {
                string cat = dbContext.PPCategories.Where(c => c.NoCategorie == articlesProduits.Key.NoCategorie).First().Description;
                if (lstProduits.TryGetValue(cat, out lstTempo))
                {
                    lstTempo.Add(lesProduits);
                }
                else
                {
                    List<PPProduits> lst = new List<PPProduits>();
                    lst.Add(lesProduits);
                    lstProduits.Add(cat, lst);
                }
            }

        }
        return lstProduits;
    }

    protected void nbPageChange(object sender, EventArgs e)
    {
        int result;
        if (int.TryParse(ddlNbParPage.SelectedItem.Text, out result))
        {
            Session["ddlNbPage"] = result;
            ddlNbParPage.SelectedIndex = ddlNbParPage.Items.IndexOf(ddlNbParPage.Items.FindByText(Convert.ToString(Session["ddlNbPage"])));
        }
        else
        {
            Session["ddlNbPage"] = int.MaxValue;
            ddlNbParPage.SelectedIndex = ddlNbParPage.Items.IndexOf(ddlNbParPage.Items.FindByText("Tous"));
        }
        intNbPage = Convert.ToInt32(Session["ddlNbPage"]);
        int laCategorieDDL = Convert.ToInt32(ddlCategorie.SelectedValue);
        string url = "~/Pages/ConsultationCatalogueProduitVendeur.aspx?&NoPage=" + 1 + "&search=" + _tbsearchText.Text.Trim();
        Response.Redirect(url);
    }
    protected void categorieIndexChange(object sender, EventArgs e)
    {
        Session["ddlCategorie"] = ddlCategorie.SelectedValue;
        ddlCategorie.SelectedIndex = ddlCategorie.Items.IndexOf(ddlCategorie.Items.FindByValue(Convert.ToString(Session["ddlCategorie"])));
        Session["booTriDateStart"] = false;
        Session["booTriNumeroStart"] = false;
        _tbsearchText.Text = "";
        string url = "~/Pages/ConsultationCatalogueProduitVendeur.aspx?&NoPage=" + 1 + "&search=" + _tbsearchText.Text.Trim();
        Response.Redirect(url);
    }
    protected void lbSearch(object sender, EventArgs e)
    {
        int laCategorieDDL = Convert.ToInt32(ddlCategorie.SelectedValue);
        string url = "~/Pages/ConsultationCatalogueProduitVendeur.aspx?&NoPage=" + 1 + "&search=" + _tbsearchText.Text.Trim();
        Response.Redirect(url);
    }
    protected void triParDate(object sender, EventArgs e)
    {
        if (Session["booTriDateStart"] != null && !Convert.ToBoolean(Session["booTriDateStart"]))
        {
            _tbsearchText.Text = "";
        }
        if (Convert.ToBoolean(Session["booTriNumeroStart"]))
            noPage = 1;
        Session["booTriDateStart"] = true;
        Session["booTriNumeroStart"] = false;
        Session["booTriDate"] = !Convert.ToBoolean(Session["booTriDate"]);
        booTriDate = Convert.ToBoolean(Session["booTriDate"]);
        //ddlCategorie.SelectedIndex = 0;
        //Session["ddlCategorie"] = 1;
        string url = "~/Pages/ConsultationCatalogueProduitVendeur.aspx?&NoPage=" + noPage + "&search=" + _tbsearchText.Text.Trim();
        Response.Redirect(url);
    }
    protected void triParNoProduit(object sender, EventArgs e)
    {
        if (Session["booTriNumeroStart"] != null && !Convert.ToBoolean(Session["booTriNumeroStart"]))
        {
            _tbsearchText.Text = "";
        }
        if (Convert.ToBoolean(Session["booTriDateStart"]))
            noPage = 1;
        Session["booTriNumeroStart"] = true;
        Session["booTriDateStart"] = false;
        Session["booTri"] = !Convert.ToBoolean(Session["booTri"]);
        booTriNumero = Convert.ToBoolean(Session["booTri"]);
        //ddlCategorie.SelectedIndex = 0;
        //Session["ddlCategorie"] = 1;
        string url = "~/Pages/ConsultationCatalogueProduitVendeur.aspx?&NoPage=" + noPage + "&search=" + _tbsearchText.Text.Trim();
        Response.Redirect(url);
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