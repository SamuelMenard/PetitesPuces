﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_searchClient : System.Web.UI.Page
{

    private BD6B8_424SEntities dbContext = new BD6B8_424SEntities();   
    String nomEntreprise = "";
    private int intNbPage;
    private int indexPages;
    bool booTriDate = false;
    bool booTriNumero = false;
    int noPage = 1;
    long noClient;
    Panel panelBody;
    Panel colQuantite;
    //protected void Page_

    protected void Page_Load(object sender, EventArgs e)
    {

        booTriDate = Convert.ToBoolean(Session["booTriDate"]);
        booTriNumero = Convert.ToBoolean(Session["booTri"]);

        if (Request.QueryString["NoPage"] != null)
        {
            noPage = Convert.ToInt32(Request.QueryString["NoPage"]);

        }

        
        //noClient = long.Parse(Session["NoClient"].ToString());
        noClient = 10001;
        int result;
        if (intNbPage == 0 && int.TryParse(ddlNbParPage.SelectedItem.Text, out result))
            intNbPage = result;
        else
            intNbPage = int.MaxValue;
        

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

            List<int> lstCategories = dbContext.PPProduits.GroupBy(c => c.NoCategorie).Select(c => c.Key.Value).ToList();
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
        phDynamique.Controls.Clear();
        Panel panelHeader = LibrairieControlesDynamique.divDYN(phDynamique, nomEntreprise + "_header", "panel-heading");
        LibrairieControlesDynamique.lblDYN(panelHeader, nomEntreprise + "_nom", nomEntreprise, "nom-entreprise");

        // Liste des items + le total
        panelBody = LibrairieControlesDynamique.divDYN(phDynamique, nomEntreprise + "_body", "panel-body");
        int nbProduitsVendeur = dbContext.PPProduits.Count();


        // Rajouter les produits dans le panier
        if (nbProduitsVendeur > 0)
        {

            int laCategorieDDL = Convert.ToInt32(ddlCategorie.SelectedValue);
            Dictionary<string, List<PPProduits>> getProduitsSearch;
            if (Convert.ToBoolean(Session["booTriNumeroStart"]))
            {
                getProduitsSearch = getProduitsParNumero(_tbsearchText.Text.Trim());
            }
            else if (Convert.ToBoolean(Session["booTriDateStart"]))
            {
                getProduitsSearch = getProduitsParDate(_tbsearchText.Text.Trim());
            }
            else if (laCategorieDDL != 1)
            {
                getProduitsSearch = getProduitsUneCategorie(_tbsearchText.Text.Trim());
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
                    LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prixDemande_" + idItem, prix.ToString("N") + " $ ", "prix_item");
                    LibrairieControlesDynamique.brDYN(colPrix);
                    LibrairieControlesDynamique.lblDYN(colPrix, "lblRabais" + idItem, (montantRabais > 0) ? "Rabais de $" + Decimal.Round((Decimal)montantRabais, 2).ToString() : "", "rabais");

                    // Quantité restant
                    colQuantite = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colQuantite_" + idItem, "col-sm-2 text-center");

                    TextBox tbQuantite = LibrairieControlesDynamique.numericUpDownDYN(colQuantite, "quantite_" + idItem, (produitCat.NombreItems < 1) ? "0" : "1"
                    , (produitCat.NombreItems < 1) ? "0" : produitCat.NombreItems.ToString(), "form-control border-quantite");

                    Panel colAjout = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colAjout_" + idItem, "col-sm-2 text-right");
                    Button btnAjout = LibrairieControlesDynamique.btnDYN(colAjout, "btnAjouter_" + idItem+"-"+produitCat.NoVendeur, "btn valignMessage btnPageOrange", "Ajouter au panier", btnAjouter_click);

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

    private void btnAjouter_click(object sender, EventArgs e)
    {
        Button btnAjout = (Button)sender;
        string[] str = btnAjout.ID.Replace("btnAjouter_", "").Split('-');
        long noProduit = long.Parse(str[0]);
        long noVendeur = long.Parse(str[1]);
        TextBox tb = (TextBox)colQuantite.FindControl("quantite_" + noProduit);
        short nbItems = short.Parse(tb.Text.Trim());
        PPArticlesEnPanier nouvelArticle = new PPArticlesEnPanier();
        nouvelArticle.NoPanier = dbContext.PPArticlesEnPanier.Max(c => c.NoPanier) + 1; 
        nouvelArticle.NoClient = noClient;
        nouvelArticle.NoVendeur = noVendeur;
        nouvelArticle.NoProduit = noProduit;
        nouvelArticle.DateCreation = DateTime.Now;
        nouvelArticle.NbItems = nbItems;
        dbContext.PPArticlesEnPanier.Add(nouvelArticle);

        try
        {
            dbContext.SaveChanges();
        } catch (Exception ex) { }
    }

    // aller chercher les paniers
    public Dictionary<string, List<PPProduits>> getProduitsVendeurParCategorie(string strSearch)
    {

        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableProduits = dbContext.PPProduits;
        // retourner la liste des paniers
        Dictionary<string, List<PPProduits>> lstProduits = new Dictionary<string, List<PPProduits>>();

        int countProduits = countProduits = dbContext.PPProduits.Where(c =>(c.Nom.Contains(strSearch)) && (c.Disponibilité == true) ).Count();
        creerBtnPages(countProduits);
        var produits = from produit in tableProduits.Where(c =>(c.Nom.Contains(strSearch)) && (c.Disponibilité == true) ).OrderBy(c => c.NoCategorie).ThenBy(c => c.Description).Skip(intNbPage * (noPage - 1)).Take(intNbPage)
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
        decimal decPages = Convert.ToDecimal(nbProduits) / Convert.ToDecimal(intNbPage);
        noPage = noPage > Math.Ceiling(decPages) ? 1 : noPage;
        int maximumIndex = (Math.Ceiling(decPages) >= (noPage + 2)) ? (noPage + 2) : (Math.Ceiling(decPages) >= (noPage + 1)) ? (noPage + 1) : noPage;
        int nbAfficher = maximumIndex == (noPage + 1) ? 4 : maximumIndex == (noPage) ? 5 : 3;
        int indexPages = ((noPage - nbAfficher) < 1) ? 0 : (noPage - nbAfficher);
        for (int i = indexPages; i < maximumIndex; i++)
        {
            if ((i + 1) == noPage)
            {
                LibrairieControlesDynamique.liDYN(ulPages, "searchClient.aspx?&NoPage=" + (i + 1).ToString() + "&search=" + _tbsearchText.Text.Trim(), (i + 1).ToString(), "btnPageSelected btn right5");
            }
            else
            {
                LibrairieControlesDynamique.liDYN(ulPages, "searchClient.aspx?&NoPage=" + (i + 1).ToString() + "&search=" + _tbsearchText.Text.Trim(), (i + 1).ToString(), "btnPageOrange btn right5");
            }

        }
    }

    public Dictionary<string, List<PPProduits>> getProduitsParNumero(string strSearch)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableProduits = dbContext.PPProduits;
        // retourner la liste des paniers
        Dictionary<string, List<PPProduits>> lstProduits = new Dictionary<string, List<PPProduits>>();

        int countProduits = countProduits = dbContext.PPProduits.Where(c =>  (c.Nom.Contains(strSearch)) && (c.Disponibilité == true) ).Count();
        creerBtnPages(countProduits);
        var produits = from produit in tableProduits.Where(c => (c.Nom.Contains(strSearch)) && (c.Disponibilité == true) ).OrderBy(c => c.NoProduit).Skip(intNbPage * (noPage - 1)).Take(intNbPage)
                       select produit;

        if (!booTriNumero)
        {
            produits = from produit in tableProduits.Where(c =>  (c.Nom.Contains(strSearch)) && (c.Disponibilité == true) ).OrderByDescending(c => c.NoProduit).Skip(intNbPage * (noPage - 1)).Take(intNbPage)
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

        int countProduits = countProduits = dbContext.PPProduits.Where(c => (c.Nom.Contains(strSearch)) && (c.Disponibilité == true) ).Count();
        creerBtnPages(countProduits);
        var produits = from produit in tableProduits.Where(c =>  (c.Nom.Contains(strSearch)) && (c.Disponibilité == true) ).OrderBy(c => c.DateCreation).Skip(intNbPage * (noPage - 1)).Take(intNbPage)
                       select produit;

        if (!booTriDate)
        {
            produits = from produit in tableProduits.Where(c => (c.Nom.Contains(strSearch)) && (c.Disponibilité == true) ).OrderByDescending(c => c.DateCreation).Skip(intNbPage * (noPage - 1)).Take(intNbPage)
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
        int countProduits = countProduits = dbContext.PPProduits.Where(c =>  (c.Nom.Contains(strSearch)) && (c.NoCategorie == laCategorieDDL) && (c.Disponibilité == true) ).Count();
        creerBtnPages(countProduits);
        var produits = from produit in tableProduits.Where(c => (c.Nom.Contains(strSearch)) && (c.NoCategorie == laCategorieDDL) && (c.Disponibilité == true) ).OrderBy(c => c.NoCategorie).ThenBy(c => c.Description).Skip(intNbPage * (noPage - 1)).Take(intNbPage)
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
        string url = "~/Pages/searchClient.aspx?&NoPage=" + 1 + "&search=" + _tbsearchText.Text.Trim();
        Response.Redirect(url);
    }
    protected void categorieIndexChange(object sender, EventArgs e)
    {
        Session["ddlCategorie"] = ddlCategorie.SelectedValue;
        ddlCategorie.SelectedIndex = ddlCategorie.Items.IndexOf(ddlCategorie.Items.FindByValue(Convert.ToString(Session["ddlCategorie"])));
        Session["booTriDateStart"] = false;
        Session["booTriNumeroStart"] = false;
        _tbsearchText.Text = "";
        string url = "~/Pages/searchClient.aspx?&NoPage=" + 1 + "&search=" + _tbsearchText.Text.Trim();
        Response.Redirect(url);
    }
    protected void lbSearch(object sender, EventArgs e)
    {
        int laCategorieDDL = Convert.ToInt32(ddlCategorie.SelectedValue);
        string url = "~/Pages/searchClient.aspx?&NoPage=" + 1 + "&search=" + _tbsearchText.Text.Trim();
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
        ddlCategorie.SelectedIndex = 0;
        Session["ddlCategorie"] = 1;
        string url = "~/Pages/searchClient.aspx?&NoPage=" + noPage + "&search=" + _tbsearchText.Text.Trim();
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
        ddlCategorie.SelectedIndex = 0;
        Session["ddlCategorie"] = 1;
        string url = "~/Pages/searchClient.aspx?&NoPage=" + noPage + "&search=" + _tbsearchText.Text.Trim();
        Response.Redirect(url);
    }
}