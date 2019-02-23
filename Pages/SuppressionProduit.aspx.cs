using System;
using System.Collections.Generic;
using System.Globalization;
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
        verifierPermissions("V");

        Page.Title = "Gestion des produits";

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
        if (Request.QueryString["ResultatSuppr"] != null)
        {
            string resultatSupprimer = Request.QueryString["ResultatSuppr"];
            if (resultatSupprimer == "OK")
            {
                lblMessage.Text = "Le produit a été supprimé.";
                divMessage.CssClass = "alert alert-success alert-margins";
                divMessage.Visible = true;
            }
            else if (resultatSupprimer == "PasOK")
            {
                lblMessage.Text = "Le produit n'a pas pu être supprimé. Réessayez ultérieurement.";
                divMessage.CssClass = "alert alert-danger alert-margins";
                divMessage.Visible = true;
            }
        }
        PPVendeurs leVendeur;
        if (Session["NoVendeur"] != null)
        {
            noVendeur = Convert.ToInt32((Session["NoVendeur"]));
            leVendeur = dbContext.PPVendeurs.Where(c => c.NoVendeur.Equals(noVendeur)).First();
            nomVendeur = leVendeur.AdresseEmail;
            nomEntreprise = leVendeur.NomAffaires;
        }
           
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
       
       // btnSupprimer.Style.Add("width", "105px");
        LibrairieControlesDynamique.hrDYN(panelBody, "OrangeBorder", 30);

        // Chercher les produits de la compagnie        
        List<PPProduits> lesProduits = dbContext.PPProduits.Where(c => (c.NoVendeur == noVendeur) && (c.Disponibilité != null) ).ToList();        
      
        // Rajouter les produits dans le panier
        if (lesProduits.Count > 0)
        {
            
            for (int i = 0; i < lesProduits.Count; i++)
            {
                
                long idItem = Convert.ToInt64(lesProduits[i].NoProduit);
                String nomProduit = lesProduits[i].Nom.ToString();
                Double prix = Convert.ToDouble(lesProduits[i].PrixDemande);
                Double prixVente = Convert.ToDouble(lesProduits[i].PrixVente);
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
                Panel colNom = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colNom_" + idItem, "col-sm-2 LiensProduits nomClient breakWord");
                LibrairieControlesDynamique.lbDYN(colNom, nomEntreprise + "_nom_" + idItem, nomProduit, modifierProduit);

                // Modification
                Panel colMod = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colMod_" + idItem, "col-sm-1 text-center");
                LibrairieControlesDynamique.btnDYN(colMod, "btnModifier_" + idItem, "btn btnPageOrange", "Modifier", btnModifier_Click);

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
                if (lesProduits[i].PrixVente != null)
                {
                    if (lesProduits[i].DateVente > DateTime.Now)
                        LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prixVente_" + idItem, "Prix de vente : " + prixVente.ToString("C", CultureInfo.CurrentCulture) + "<br>", "prix_item");
                    else
                        LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prixVente_" + idItem, "Prix de vente : " + prixVente.ToString("C", CultureInfo.CurrentCulture) + "<br>", "prix_item produitNonVente");
                }
                else
                    LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prixVente_" + idItem, "Prix de vente : N/A" + "<br>", "prix_item");

                LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prixDemande_" + idItem, "Prix demandé : " + prix.ToString("C", CultureInfo.CurrentCulture), "prix_item");

               // Panel rowDel = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowDel_", "row text-right");
                Panel colDel = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colDel_"+idItem, "col-sm-2 text-center");         
                HtmlButton btnSupprimmer = LibrairieControlesDynamique.htmlbtnDYN(colDel, "btnSupprimer_" + idItem, "btn btn-danger", "Supprimer", "glyphicon glyphicon-remove", btnSupprimer_click);
                //btnSupprimer.Attributes.Add("onclick", "Confirm();");
                // CheckBox (Supprimer plusieurs)
                //Panel colCB = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colCB_" + idItem, "col-sm-2 text-center");
                //CheckBox cbDelete = LibrairieControlesDynamique.cb(colCB, nomEntreprise + "_cbSupprimer_" + idItem, "");                  




            }
        }
        else
        {
            Panel row = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowPanierVide", "row marginFluid text-center");
            Panel message = LibrairieControlesDynamique.divDYN(row, nomEntreprise + "_messagePanierVide", "message text-center top15");
            Panel messageContainer = LibrairieControlesDynamique.divDYN(message, nomEntreprise + "_divMessage", "alert alert-danger alert-margins");
            LibrairieControlesDynamique.lblDYN(messageContainer, nomEntreprise + "_leMessageLabel", "Vous avez aucun produit dans votre catalogue");           
        }
        LibrairieControlesDynamique.hrDYN(panelBody, "OrangeBorder", 30);
    }

    private void btnModifier_Click(object sender, EventArgs e)
    {
        // throw new NotImplementedException();
        Button lb = (Button)sender;
        long idProduitMod = Convert.ToInt64(lb.ID.Replace("btnModifier_", ""));
        String url = "~/Pages/InscriptionProduit.aspx?NoProduit=" + idProduitMod + "&Operation=Modifier";
        Response.Redirect(url, true);
    }

    private void modifierProduit(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        long idProduitMod = Convert.ToInt64(lb.ID.Replace(nomEntreprise + "_nom_", ""));
        String url = "~/Pages/InscriptionProduit.aspx?NoProduit=" + idProduitMod + "&Operation=Afficher";
        Response.Redirect(url, true);
    }

    private void btnSupprimer_click(object sender, EventArgs e)
    {

        HtmlButton lb = (HtmlButton)sender;
        long idProduitMod = Convert.ToInt64(lb.ID.Replace("btnSupprimer_", ""));
        String url = "~/Pages/InscriptionProduit.aspx?NoProduit="+ idProduitMod+"&Operation=Supprimer";
        Response.Redirect(url, true);
        /*List<PPProduits> lesProduitsDelete = new List<PPProduits>();
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
                                    
                               // lesDetailsCommandes = dbContext.PPDetailsCommandes.Where(c => c.NoProduit == noProduit).ToList();
                               if(dbContext.PPDetailsCommandes.Where(c => c.NoProduit == noProduit).Count() > 0)
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
     */
  
    }
   
}