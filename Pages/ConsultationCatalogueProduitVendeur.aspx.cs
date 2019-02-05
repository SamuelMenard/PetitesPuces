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
    private int intNbPage;
    private int indexPages;
    bool booTriDate = false;
    bool booTriNumero = false;
    bool booTriDateStart = false;
    bool booTriNumeroStart = false;
    bool booNbPage = false;
    bool boolCat = false;
    int noPage = 1;

    //protected void Page_

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine(" DDLPAGe = " + Session["ddlNbPage"]);
        if (!booNbPage) {   
            booTriNumeroStart = Convert.ToBoolean(Session["booTri"]);
             booTriDateStart = Convert.ToBoolean(Session["booTriDate"]);
         }
       
        if (Request.QueryString["NoPage"] != null)
        {
            noPage = Convert.ToInt32(Request.QueryString["NoPage"]);
            
        }
        if(Session["ddlNbPage"] != null)
        {
            //ddlNbParPage.SelectedIndex = ddlNbParPage.Items.IndexOf(ddlNbParPage.Items.FindByText(Convert.ToString(Session["ddlNbPage"])));
        } 
        
        noVendeur = Session["NoVendeurCatalogue"] != null ? Convert.ToInt32(Session["NoVendeurCatalogue"]) : 10;
        
        int result;      
        if (intNbPage == 0 && int.TryParse(ddlNbParPage.SelectedItem.Text, out result))
            intNbPage = result;
        else
            intNbPage = int.MaxValue;

        nomEntreprise = dbContext.PPVendeurs.Where(c => c.NoVendeur == noVendeur).First().NomAffaires;    
        
        if (!IsPostBack)
        {
            System.Diagnostics.Debug.WriteLine(" TEST ");
            //Session["ddlNbPage"] = null;
            /* Session["NoPage"] = null;
             Session["booTri"] = null;
             Session["booTriDateStart"] = null;
             Session["booTriDate"] = null;
             Session["booTriNumeroStart"] = null;
             */
            if (Request.QueryString["NoPage"] != null)
            {
                noPage = Convert.ToInt32(Request.QueryString["NoPage"]);

                ddlNbParPage.SelectedIndex = ddlNbParPage.Items.IndexOf(ddlNbParPage.Items.FindByText(Convert.ToString(Session["ddlNbPage"])));
                //creerPage();

            }
            else
            {
                List<int> lstCategories = dbContext.PPProduits.Where(c => c.NoVendeur == 20).GroupBy(c => c.NoCategorie).Select(c => c.Key.Value).ToList();
                ddlCategorie.Items.Insert(0, new ListItem("Tous les catégories", "1"));
                for (int i = 0; i < lstCategories.Count; i++)
                {
                    int nbCat = lstCategories[i];

                    string strDescCat = dbContext.PPCategories.Where(c => c.NoCategorie == nbCat).First().Description;
                    ddlCategorie.Items.Insert(i + 1, new ListItem(strDescCat, nbCat.ToString()));
                }
                creerPage();
            }
        }
    }

    private void creerPage()
    {       

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
           
            int laCategorieDDL = Convert.ToInt32(ddlCategorie.SelectedValue);            
            Dictionary<string, List<PPProduits>> getProduitsSearch;
            if (Convert.ToBoolean(Session["booTriNumeroStart"]))
            {
                getProduitsSearch = getProduitsParNumero(_tbsearchText.Text.Trim());
            }else if (Convert.ToBoolean(Session["booTriDateStart"]))
            {
                getProduitsSearch = getProduitsParDate(_tbsearchText.Text.Trim());
            }else if (boolCat && laCategorieDDL != 1)
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
                if(descCategorie != "")
                {
                    Panel panCategorie = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_categorie_" + descCategorie, "flex-container");
                    Panel colCatAfficher = LibrairieControlesDynamique.divDYN(panCategorie, nomEntreprise + "_colLabelCategorie" + descCategorie, "");
                    LibrairieControlesDynamique.lblDYN(colCatAfficher, nomEntreprise + "_labelCategorie" + descCategorie, descCategorie, "infos-payage");
                }              
           
                foreach (PPProduits produitCat in keys.Value)
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
                    //LibrairieControlesDynamique.lblDYN(colQuantite, nomEntreprise + "_quantite_" + idItem, "Qte : " + produitCat.NombreItems, "prix_item");

                    TextBox tbQuantite = LibrairieControlesDynamique.numericUpDownDYN(colQuantite, "quantite_" + idItem, (produitCat.NombreItems < 1) ? "0" :"1"
                    , (produitCat.NombreItems < 1) ? "0" : produitCat.NombreItems.ToString(), "form-control border-quantite");

                    if (produitCat.NombreItems < 1)
                    {                        
                        tbQuantite.Enabled = false;
                        LibrairieControlesDynamique.lblDYN(colQuantite, "", "Rupture de stock", "rupture-stock");
                    }                

                    // Categorie
                    Panel colCat = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colCategorie_" + idItem, "col-sm-3");
                    LibrairieControlesDynamique.lblDYN(colCat, nomEntreprise + "_categorie_" + idItem, categorie, "cat_item");

                    // Prix item
                    Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPrix_" + idItem, "col-sm-2 text-right");
                    LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prixDemande_" + idItem, "Prix demandé : $" + prix.ToString(), "prix_item");

                }

            }        
            
        }
     
    }





    // aller chercher les paniers
    public Dictionary<string, List<PPProduits>> getProduitsVendeurParCategorie(string strSearch)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();        
        var tableProduits = dbContext.PPProduits;
        // retourner la liste des paniers
        Dictionary<string, List<PPProduits>> lstProduits = new Dictionary<string, List<PPProduits>>();
        
        int countProduits = countProduits = dbContext.PPProduits.Where(c => (c.NoVendeur == noVendeur) && (c.Nom.Contains(strSearch))).Count();             
        creerBtnPages(countProduits);
        var produits = from produit in tableProduits.Where(c => (c.NoVendeur == noVendeur) && (c.Nom.Contains(strSearch))).OrderBy(c => c.NoCategorie).ThenBy(c => c.Description).Skip(intNbPage * (noPage - 1)).Take(intNbPage)
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
        decimal decPages = Convert.ToDecimal(nbProduits) / Convert.ToDecimal(intNbPage);               
        int maximumIndex = (decPages >= (noPage+2)) ? (noPage + 2) : (decPages >= (noPage + 1)) ? (noPage + 1) : noPage;
        int nbAfficher = maximumIndex == (noPage + 1) ? 4 : maximumIndex == (noPage) ? 5 : 3;
        int indexPages = ((noPage - nbAfficher) < 1) ? 0 : (noPage - nbAfficher);
        //noPage = noPage > decPages ? Convert.ToInt32(decPages) : noPage;
        for ( int i = indexPages; i < maximumIndex; i++)
        {            
            if ((i+1) == noPage)
            {
                LibrairieControlesDynamique.liDYN(ulPages, "ConsultationCatalogueProduitVendeur.aspx?&NoPage=" + (i + 1).ToString(), (i + 1).ToString(), "btnPageSelected btn right5");
            }
            else
            {
                LibrairieControlesDynamique.liDYN(ulPages, "ConsultationCatalogueProduitVendeur.aspx?&NoPage=" + (i + 1).ToString(), (i + 1).ToString(), "btnPageOrange btn right5");   
            }
                    
        }    
    }   

    public Dictionary<string, List<PPProduits>> getProduitsParNumero(string strSearch)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableProduits = dbContext.PPProduits;
        // retourner la liste des paniers
        Dictionary<string, List<PPProduits>> lstProduits = new Dictionary<string, List<PPProduits>>();

        int countProduits = countProduits = dbContext.PPProduits.Where(c => (c.NoVendeur == noVendeur) && (c.Nom.Contains(strSearch))).Count();
        creerBtnPages(countProduits);
        var produits = from produit in tableProduits.Where(c => (c.NoVendeur == noVendeur) && (c.Nom.Contains(strSearch))).OrderBy(c => c.NoProduit).Skip(intNbPage * (noPage - 1)).Take(intNbPage)
                       select produit;

        if (booTriNumeroStart)
        {
            produits = from produit in tableProduits.Where(c => (c.NoVendeur == noVendeur) && (c.Nom.Contains(strSearch))).OrderByDescending(c => c.NoProduit).Skip(intNbPage * (noPage - 1)).Take(intNbPage)
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

        int countProduits = countProduits = dbContext.PPProduits.Where(c => (c.NoVendeur == noVendeur) && (c.Nom.Contains(strSearch))).Count();
        creerBtnPages(countProduits);
        var produits = from produit in tableProduits.Where(c => (c.NoVendeur == noVendeur) && (c.Nom.Contains(strSearch))).OrderBy(c => c.DateCreation).Skip(intNbPage * (noPage - 1)).Take(intNbPage)
                       select produit;

        if (booTriDateStart)
        {
            produits = from produit in tableProduits.Where(c => (c.NoVendeur == noVendeur) && (c.Nom.Contains(strSearch))).OrderByDescending(c => c.DateCreation).Skip(intNbPage * (noPage - 1)).Take(intNbPage)
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
        int countProduits = countProduits = dbContext.PPProduits.Where(c => (c.NoVendeur == noVendeur) && (c.Nom.Contains(strSearch) && (c.NoCategorie == laCategorieDDL))).Count();
        creerBtnPages(countProduits);
        var produits = from produit in tableProduits.Where(c => (c.NoVendeur == noVendeur) && (c.Nom.Contains(strSearch) && (c.NoCategorie == laCategorieDDL) )).OrderBy(c => c.NoCategorie).ThenBy(c => c.Description).Skip(intNbPage * (noPage - 1)).Take(intNbPage)
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
        System.Diagnostics.Debug.WriteLine(" EVENT DDLPAGE = " + Session["ddlNbPage"]);
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
        booNbPage = true;
        booTriDateStart = !Convert.ToBoolean(Session["booTriDate"]);
        booTriNumeroStart = !Convert.ToBoolean(Session["booTri"]);
        int laCategorieDDL = Convert.ToInt32(ddlCategorie.SelectedValue);
        if (laCategorieDDL != 1)
        {
            boolCat = true;
        }
        creerPage();
        /* String url = "~/Pages/ConsultationCatalogueProduitVendeur.aspx?&NoVendeur="+noVendeur+"&NbPage=" + ddlNbParPage.SelectedItem.Text+ "&indexPages="+ddlNbParPage.SelectedIndex;
         Response.Redirect(url, true);  */
    }
    protected void categorieIndexChange(object sender, EventArgs e)
     {
        Session["ddlCategorie"] = ddlCategorie.SelectedValue;
        ddlCategorie.SelectedIndex = ddlCategorie.Items.IndexOf(ddlCategorie.Items.FindByValue(Convert.ToString(Session["ddlCategorie"])));
        Session["booTriDateStart"] = false;
        Session["booTriNumeroStart"] = false;        
        _tbsearchText.Text = "";
        boolCat = true;
        creerPage();
    }
    protected void lbSearch(object sender, EventArgs e)
    {
        //booNbPage = true;        
        booTriDateStart = !Convert.ToBoolean(Session["booTriDate"]);
        booTriNumeroStart = !Convert.ToBoolean(Session["booTri"]);
        int laCategorieDDL = Convert.ToInt32(ddlCategorie.SelectedValue);
        if (laCategorieDDL != 1)
        {
            boolCat = true;
        }
        creerPage();        
    }
    protected void triParDate(object sender, EventArgs e)
    {
        
        if (Session["booTriDateStart"] != null && !Convert.ToBoolean(Session["booTriDateStart"]))
        {
            _tbsearchText.Text = "";
        }
        Session["booTriDateStart"] = true;
        Session["booTriNumeroStart"] = false;        
        booTriDate = Convert.ToBoolean(Session["booTriDateStart"]);
        Session["booTriDate"] = !Convert.ToBoolean(Session["booTriDate"]);       
        ddlCategorie.SelectedIndex = 0;
        creerPage();        
    }
    protected void triParNoProduit(object sender, EventArgs e)
    {
        if (Session["booTriNumeroStart"] != null && !Convert.ToBoolean(Session["booTriNumeroStart"]))
        {
            _tbsearchText.Text = "";
        }
        Session["booTriNumeroStart"] = true;
        Session["booTriDateStart"] = false;
        booTriNumero = Convert.ToBoolean(Session["booTriNumeroStart"]);
        Session["booTri"] = !Convert.ToBoolean(Session["booTri"]);       
        ddlCategorie.SelectedIndex = 0;
        creerPage();      
    }
}