using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;



public partial class Pages_SuppressionProduit : System.Web.UI.Page
{
    private BD6B8_424SEntities dbContext = new BD6B8_424SEntities();
    string nomVendeur = "";
    long noVendeur;   
    String nomEntreprise = "";
    Panel panelBody;



    protected void Page_Load(object sender, EventArgs e)
    {       
        if (Request.QueryString["ResultatModif"] != null)
        {
            string resultat = Request.QueryString["ResultatModif"];
            if (resultat == "OK")
            {
                lblMessage.Text = "Le produit a été modifié.";
                divMessage.CssClass = "alert alert-success alert-margins";
                divMessage.Visible = true;
            }
            else if (resultat == "PasOK")
            {
                lblMessage.Text = "Le produit n'a pas pu être modifié. Réessayez ultérieurement.";
                divMessage.CssClass = "alert alert-danger alert-margins";
                divMessage.Visible = true;
            }
        }

        nomVendeur = Session["Courriel"].ToString();
        nomEntreprise = Session["NomAffaire"].ToString();
        noVendeur = Convert.ToInt32((Session["NoVendeur"]));
        creerPage();       
    }

    private void creerPage()
    {
        dbContext.SaveChanges();
        Panel panelGroup = LibrairieControlesDynamique.divDYN(phDynamique, nomEntreprise + "_PanelGroup", "panel-group container-fluid marginFluid");
        Panel panelBase = LibrairieControlesDynamique.divDYN(panelGroup, nomEntreprise + "_base", "panel panel-default");
        // Nom de l'entreprise
        Panel panelHeader = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_header", "panel-heading");

        Panel rowInactif = LibrairieControlesDynamique.divDYN(panelHeader, nomEntreprise + "_rowInactif_", "row");
        Panel colInactif = LibrairieControlesDynamique.divDYN(rowInactif, nomEntreprise + "_colInactif_", "col-sm-12");
        LibrairieControlesDynamique.lblDYN(colInactif, nomEntreprise + "_nom", nomEntreprise + " - "+ nomVendeur, "nom-entreprise");
         panelBody = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_PanelBody", "panel-body");
       
        panelBody.Controls.Clear();
        // Btn Delete
        Panel rowDel = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowDel_", "row text-right");
        Panel colDel = LibrairieControlesDynamique.divDYN(rowDel, nomEntreprise + "_colDel_" , "col-sm-12");
        HtmlButton btnSupprimer = LibrairieControlesDynamique.htmlbtnDYN(colDel, "btnSupprimer_", "btn btn-danger", "Supprimer le(s) produit(s)", "glyphicon glyphicon-remove", btnSupprimer_click);
        btnSupprimer.Attributes.Add("onclick", "Confirm();");
       // btnSupprimer.Style.Add("width", "105px");
        LibrairieControlesDynamique.hrDYN(panelBody, "OrangeBorder", 30);

        // Chercher les produits de la compagnie        
        List<PPProduits> lesProduits = dbContext.PPProduits.Where(c => c.NoVendeur == noVendeur).ToList();        
      
        // Rajouter les produits dans le panier
        if (lesProduits.Count > 0)
        {
            
            for (int i = 0; i < lesProduits.Count; i++)
            {
                
                long idItem = Convert.ToInt64(lesProduits[i].NoProduit);
                String nomProduit = lesProduits[i].Nom.ToString();
                Double prix = Convert.ToDouble(lesProduits[i].PrixVente);
                String urlImage = "../static/images/"+ lesProduits[i].Photo;
                int noCat = lesProduits[i].NoCategorie.Value;                
                PPCategories pCategorie = dbContext.PPCategories.Where(c => c.NoCategorie.Equals(noCat)).First();
                String categorie = pCategorie.Description;
                String disponibilite = lesProduits[i].Disponibilité == true ? "Disponible" : "Indisponible";

                Panel rowItem = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowItem_" + idItem, "row valign top15");

                // ajouter l'image
                Panel colImg = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colImg_" + idItem, "col-sm-1 ");
                LibrairieControlesDynamique.imgDYN(colImg, nomEntreprise + "_img_" + idItem, urlImage, "img-size center-block");
                LibrairieControlesDynamique.lblDYN(colImg, nomEntreprise + "_noproduit_" + idItem, idItem.ToString(), "caption center-block text-center");

                // Nom du produit
                Panel colNom = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colNom_" + idItem, "col-sm-3 LiensProduits nomClient");
                LibrairieControlesDynamique.lbDYN(colNom, nomEntreprise + "_nom_" + idItem, nomProduit, modifierProduit);

                // Quantité restant
                Panel colQuantite = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colQuantite_" + idItem, "col-sm-1");
                LibrairieControlesDynamique.lblDYN(colQuantite, nomEntreprise + "_quantite_" + idItem, "Qte : "+lesProduits[i].NombreItems, "prix_item");

                // Categorie
                Panel colCat = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colCategorie_" + idItem, "col-sm-2 breakWord");
                LibrairieControlesDynamique.lblDYN(colCat, nomEntreprise + "_categorie_" + idItem, categorie, "cat_item");

                // Dispo
                Panel colDispo = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colDispo_" + idItem, "col-sm-1");
                LibrairieControlesDynamique.lblDYN(colDispo, nomEntreprise + "_Disponibilite_" + idItem, disponibilite, "cat_item");

                // Prix item
                Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPrix_" + idItem, "col-sm-2 text-right");
                LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prixVente_" + idItem, "Prix de vente : $" + prix.ToString()+"<br>", "prix_item");
                LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prixDemande_" + idItem, "Prix demandé : $" + prix.ToString(), "prix_item");

                // CheckBox (Supprimer plusieurs)
                Panel colCB = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colCB_" + idItem, "col-sm-2 text-center");
                CheckBox cbDelete = LibrairieControlesDynamique.cb(colCB, nomEntreprise + "_cbSupprimer_" + idItem, "");                  

              


            }
        }
        LibrairieControlesDynamique.hrDYN(panelBody, "OrangeBorder", 30);
    }

    private void modifierProduit(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        long idProduitMod = Convert.ToInt64(lb.ID.Replace(nomEntreprise + "_nom_", ""));
        String url = "~/Pages/InscriptionProduit.aspx?NoProduit="+ idProduitMod;
        Response.Redirect(url, true);
    }

    private void btnSupprimer_click(object sender, EventArgs e)
    {
        List<PPProduits> lesProduitsDelete = new List<PPProduits>();
        List<PPProduits> lesProduitsNonDispo = new List<PPProduits>();
        List<PPArticlesEnPanier> lesProduitsEnPaniers = new List<PPArticlesEnPanier>();
        List<PPDetailsCommandes> lesDetailsCommandes = new List<PPDetailsCommandes>();
    
        foreach (Control control in panelBody.Controls)
         {                  
            if (control is Panel)
            {               
                foreach (Control cRows in control.Controls)
                {
                    if (cRows is Panel)
                    {
                        foreach (Control cColDel in cRows.Controls)
                        {
                            if (cColDel is CheckBox && ((CheckBox)cColDel).Checked)
                            {
                                long noProduit = Convert.ToInt64(cColDel.ID.Replace(nomEntreprise + "_cbSupprimer_", ""));                                                                        
                                    
                                lesDetailsCommandes = dbContext.PPDetailsCommandes.Where(c => c.NoProduit == noProduit).ToList();
                               if(dbContext.PPArticlesEnPanier.Where(c => c.NoProduit == noProduit).Count() > 0)
                                {
                                    lesProduitsNonDispo.Add(dbContext.PPProduits.Where(c => c.NoProduit == noProduit).First());
                                }
                                else
                                {

                                    lesProduitsDelete.Add(dbContext.PPProduits.Where(c => c.NoProduit == noProduit).First());
                                }
                                lesProduitsEnPaniers.AddRange(dbContext.PPArticlesEnPanier.Where(c => c.NoProduit == noProduit).ToList());      
                            }                              
                        }
                    }                  
                }
            }           
         }
       
       
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {
            using (var dbContextTransaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    if (lesProduitsNonDispo.Count > 0)
                    {
                        foreach (PPProduits produit in lesProduitsNonDispo)
                        {
                            produit.NombreItems = 0;
                            produit.Disponibilité = false;
                            dbContext.SaveChanges();
                        }                       
                      
                    }
                    if (lesProduitsEnPaniers.Count > 0)
                    {
                        dbContext.PPArticlesEnPanier.RemoveRange(lesProduitsEnPaniers);
                        dbContext.SaveChanges();                     
                    }
                    if (lesProduitsDelete.Count > 0)
                    {
                        dbContext.PPProduits.RemoveRange(lesProduitsDelete);
                        dbContext.SaveChanges();                        
                    }
                    dbContextTransaction.Commit();
                    String url = "~/Pages/SuppressionProduit.aspx?";
                    Response.Redirect(url, false);
                }
                catch (Exception ex)
                {                    
                    dbContextTransaction.Rollback();
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace.ToString());
                }
            }
        }      
     
  
    }
   
}