using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_ConsultationCatalogueProduitVendeur : System.Web.UI.Page
{

    private BD6B8_424SEntities dbContext = new BD6B8_424SEntities();   
    long noVendeur;
    String nomEntreprise = "";
    Panel panelBody;   
    int selectedIndex = 2;
    int numPage = 1;
    int nbPage = 15;
    private int intNbPage;
    private int indexPages;
    bool booTriDate = true;


    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        ddlNbParPage.SelectedIndex = this.indexPages;
        Session["NbPage"] = intNbPage;
        if (Session["trier"] != null)
            creerPage(Convert.ToInt32(Session["trier"]));
        else
            creerPage(0);
    }
    private void getVariables()
    {
       
        if (Request.QueryString["NbPage"] == null && Request.QueryString["indexPages"] == null)
        {
            this.intNbPage = 15;
            this.indexPages = 2;
           
        }
        else
        {
            
            this.intNbPage = Convert.ToInt32(Request.QueryString["NbPage"]);
            this.indexPages = Convert.ToInt32(Request.QueryString["indexPages"]);
        }

        if (Request.QueryString["Categorie"] != null)       
        {
            ddlCategorie.SelectedItem.Text = Request.QueryString["Categorie"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //booTriDate = 0;
        ddlCategorie.Items.Clear();       
        if (Request.QueryString["NoVendeur"] != null)
        {
           // noVendeur = Convert.ToInt64(Request.QueryString["NoVendeur"]);           
        }
        nomEntreprise = Session["NomAffaire"].ToString();
        noVendeur = Convert.ToInt32((Session["NoVendeur"]));
        //creerPage();            
        getVariables();
        List<int> lstCategories = dbContext.PPProduits.Where(c => c.NoVendeur == noVendeur).GroupBy(c => c.NoCategorie).Select(c => c.Key.Value).ToList();
        for (int i = 0; i < lstCategories.Count; i++)
        {
            int nbCat = lstCategories[i];
            string strDescCat = dbContext.PPCategories.Where(c => c.NoCategorie == nbCat).First().Description;
            ddlCategorie.Items.Insert(i, new ListItem(strDescCat, strDescCat));
        }
    }

    private void creerPage(int lesproduitsTrier)
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
        phDynamique.Controls.Clear();
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
            var produits = from produit in tableProduits.Where(c => c.NoVendeur == noVendeur).OrderBy(c => c.NoCategorie).ThenBy(c => c.Description).Skip(0).Take(intNbPage)
                                      group produit by new { produit.NoCategorie }
                                         into listeProduits
                                      select listeProduits;
            

           /* switch (lesproduitsTrier)
            {
                case 1:
                     produits = from produit in tableProduits.Where(c => c.NoVendeur == noVendeur).OrderByDescending(c => c.DateCreation).ThenBy(c => c.NoCategorie).ThenBy(c => c.Description).Skip(0).Take(intNbPage)
                                   group produit by new { produit.NoCategorie }
                               into listeProduits
                                   select listeProduits;

                    break;
                case 2:
                     produits = from produit in tableProduits.Where(c => c.NoVendeur == noVendeur).OrderByDescending(c => c.DateCreation).ThenBy(c => c.NoCategorie).ThenBy(c => c.Description).Skip(0).Take(intNbPage)
                                   group produit by new { produit.NoCategorie }
                      into listeProduits
                                   select listeProduits;
                    break;
                default:

                    break;

            }*/
      
          



           // Dictionary<Nullable<int>, PPProduits> lstPaniers =  produits;
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

            
            
        }
       // LibrairieControlesDynamique.hrDYN(panelBody, "OrangeBorder", 30);
    }

    public void nbPageChange(object sender, EventArgs e)
    {
        String url = "~/Pages/ConsultationCatalogueProduitVendeur.aspx?&NbPage=" + ddlNbParPage.SelectedItem.Text+ "&indexPages="+ddlNbParPage.SelectedIndex;
        Response.Redirect(url, true);   
        
    }

    // aller chercher les paniers
    public Dictionary<string, List<PPProduits>> getProduitsCategorie(string noClient)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();        
        var tableProduits = dbContext.PPProduits;
        // retourner la liste des paniers
        Dictionary<string, List<PPProduits>> lstProduits = new Dictionary<string, List<PPProduits>>();

        var produits = from produit in tableProduits.Where(c => c.NoVendeur == noVendeur).OrderBy(c => c.NoCategorie).ThenBy(c => c.Description).Skip(0).Take(intNbPage)
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


    public void trierParDate(object sender, EventArgs e)
    {
        var tableProduits = dbContext.PPProduits;
        var tableVendeurs = dbContext.PPVendeurs;
        booTriDate = !booTriDate;
        if (booTriDate)
        {
            Session["trier"] = 1;
            creerPage(1);
        }
        else
        {

            Session["trier"] = 2;
            creerPage(2);
        }

    }

    /* public void categorieIndexChange(object sender, EventArgs e)
     {
         Response.Write("GET LES PAGES " + ddlCategorie.SelectedItem);
         String url = "~/Pages/ConsultationCatalogueProduitVendeur.aspx?&Categorie=" + ddlCategorie.SelectedValue;
         Session["ddlCat"] = ddlCategorie.SelectedValue;
         //Response.Redirect(url, true);

     }*/
    protected void creerBasDePage(object sender, EventArgs e)
    {

       
        // btnPremier
        panelBody.Controls.Add(creationBtnPremier());

        //btnPrec
        panelBody.Controls.Add(creationBtnPrec());



        int intNbPages = (numPage - 2) > 0 ? (numPage - 2) : 1;

        for (int i = intNbPages; i < (numPage + 3) && i <= nbPage; i++)
        {
            Button btnPage = new Button();
            btnPage.ID = "btnPage" + i;

            if (i == numPage || i > nbPage)
            {
                btnPage.Enabled = false;
                btnPage.CssClass = "btn btn-success page-item active";
            }
            else
            {
                btnPage.PostBackUrl = "~/Catalogue/" + i;
                btnPage.CssClass = "btn btn-success";
            }
            btnPage.Text = "Page " + i;
            panelBody.Controls.Add(btnPage);
        }
        //btnNext
        panelBody.Controls.Add(creationBtnNext());
        //btnLast
        panelBody.Controls.Add(creationBtnLast());
        panelBody.Controls.Add(new LiteralControl("<br />"));
        Button btnRetourPagePrec = new Button();
        btnRetourPagePrec.ID = "backButton";
        btnRetourPagePrec.CssClass = "btn btn-danger";
        btnRetourPagePrec.Text = "Retour";
        btnRetourPagePrec.OnClientClick = "JavaScript:window.history.back(1);return false;";
      /*  if (strTitreRechercher != "")
        {
            btnRetourPagePrec.Visible = true;
        }
        else
        {
            btnRetourPagePrec.Visible = false;
        }*/
        panelBody.Controls.Add(btnRetourPagePrec);
    }

    private Button creationBtnLast()
    {
        //btnLast
        Button btnLast = new Button();
        btnLast.ID = "btnLast";

        if (nbPage == numPage)
        {
            btnLast.Enabled = false;
            btnLast.CssClass = "btn btn-success page-item disabled";
        }
        else
        {
            btnLast.PostBackUrl = "~/Catalogue/" + nbPage;
            btnLast.CssClass = "btn btn-success";
        }

        btnLast.Text = ">|";


        return btnLast;
    }

    private Button creationBtnNext()
    {
        //btnNext
        Button btnNext = new Button();
        btnNext.ID = "btnNext";
        if (nbPage == numPage)
        {
            btnNext.Enabled = false;
            btnNext.CssClass = "btn btn-success page-item disabled";
        }
        else
        {
            btnNext.PostBackUrl = "~/Catalogue/" + (numPage + 1);
            btnNext.CssClass = "btn btn-success";
        }
        btnNext.Text = ">";
        return btnNext;
    }

    private Button creationBtnPrec()
    {
        Button btnPrec = new Button();
        btnPrec.ID = "btnPrec";
        if (numPage == 1)
        {
            btnPrec.Enabled = false;
            btnPrec.CssClass = "btn btn-success page-item disabled";
        }
        else
        {
            btnPrec.PostBackUrl = "~/Catalogue/" + (numPage - 1);
            btnPrec.CssClass = "btn btn-success";
        }
        btnPrec.Text = "<";
        return btnPrec;
    }

    private Button creationBtnPremier()
    {
        Button btnPremier = new Button();
        btnPremier.ID = "btnPremier";
        if (numPage == 1)
        {
            btnPremier.Enabled = false;
            btnPremier.CssClass = "btn btn-success page-item disabled";
        }
        else
        {

            btnPremier.PostBackUrl = "~/Catalogue/1";
            btnPremier.CssClass = "btn btn-success";
        }
        btnPremier.Text = "|<";
        return btnPremier;
    }

    protected void link_ProduitDetail(object sender, EventArgs e)
    {
        LinkButton linkProduit = (LinkButton)sender;       
        Response.Redirect("~/Pages/AffichageProduitDetaille.aspx?productId=" + 10000001);      
    }
}