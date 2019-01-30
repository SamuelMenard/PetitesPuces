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
    string nomVendeur = "";
    long noVendeur;
    String nomEntreprise = "";
    Panel panelBody;

    protected void Page_Load(object sender, EventArgs e)
    {
        nomVendeur = Session["Courriel"].ToString();
        nomEntreprise = Session["NomAffaire"].ToString();
        noVendeur = Convert.ToInt32((Session["NoVendeur"]));
        creerPage();       
    }

    private void creerPage()
    {

        // Créer le panier du vendeur X
        Panel panelBase = LibrairieControlesDynamique.divDYN(phDynamique, nomEntreprise + "_base", "panel panel-default container");

        Panel panSearchFilter = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_searchFilter_", "clearfix topBotPad center");
        LibrairieControlesDynamique.tbDYN(panSearchFilter, nomEntreprise + "_searchText", "col-sm-4 left15");
        LibrairieControlesDynamique.divDYN(LibrairieControlesDynamique.lbDYN(panSearchFilter, nomEntreprise + "_btnSearch", "Search", null), nomEntreprise + "_searchLink", "glyphicon glyphicon-search");
        DropDownList dropdownlist = LibrairieControlesDynamique.ddlDYN(panSearchFilter, nomEntreprise + "_dropdowncategorie_", "left15");
        dropdownlist.Items.Insert(0, new ListItem("Catégorie", ""));

        LibrairieControlesDynamique.litDYN(panSearchFilter, "filterNumero", "<button type='button' class='btn btn-default left15'><span class=\"glyphicon glyphicon-sort\"></span> Numéro de produit</button>");
        LibrairieControlesDynamique.litDYN(panSearchFilter, "filterNumero", "<button type='button' class='btn btn-default left15'><span class=\"glyphicon glyphicon-sort\"></span> Date de parution</button>");

        LibrairieControlesDynamique.lblDYN(panSearchFilter, nomEntreprise + "_nbParPage", "Nombres de produits par page : ", "left15");
        DropDownList ddlNbPages = LibrairieControlesDynamique.ddlDYN(panSearchFilter, "ddlNbParPage", "left15");
        string[] strArrayDDL = new string[] { "5", "10", "15", "20", "25","50","Tous"};
        for( int i = 0; i < strArrayDDL.Length; i++)
        {
            ddlNbPages.Items.Insert(i, new ListItem(strArrayDDL[i], ""));
        }
        ddlNbPages.SelectedIndex = 2;

        // Nom de l'entreprise
        Panel panelHeader = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_header", "panel-heading");
        LibrairieControlesDynamique.lblDYN(panelHeader, nomEntreprise + "_nom", nomEntreprise, "nom-entreprise");

        // Liste des items + le total
        Panel panelBody = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_body", "panel-body");

        Panel panCategorie = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_categorie_", "row");
        Panel colCatAfficher = LibrairieControlesDynamique.divDYN(panCategorie, nomEntreprise + "_colLabelCategorie", "col-sm-4");
        LibrairieControlesDynamique.lblDYN(colCatAfficher, nomEntreprise + "_labelCategorie", "Articles électroniques ", "infos-payage");

        LibrairieControlesDynamique.hrDYN(panelBody);

        List<PPProduits> lesProduits = dbContext.PPProduits.Where(c => c.NoVendeur == noVendeur).ToList();
        // Rajouter les produits dans le panier
        if (lesProduits.Count > 0)
        {

            for (int i = 0; i < lesProduits.Count; i++)
            {

                long idItem = Convert.ToInt64(lesProduits[i].NoProduit);
                String nomProduit = lesProduits[i].Nom.ToString();
                Double prix = Convert.ToDouble(lesProduits[i].PrixVente);
                String urlImage = "../static/images/" + lesProduits[i].Photo;
                int noCat = lesProduits[i].NoCategorie.Value;
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
                Panel colQuantite = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colQuantite_" + idItem, "col-sm-1");
                LibrairieControlesDynamique.lblDYN(colQuantite, nomEntreprise + "_quantite_" + idItem, "Qte : " + lesProduits[i].NombreItems, "prix_item");

                // Categorie
                Panel colCat = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colCategorie_" + idItem, "col-sm-2");
                LibrairieControlesDynamique.lblDYN(colCat, nomEntreprise + "_categorie_" + idItem, categorie, "cat_item");

                // Prix item
                Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPrix_" + idItem, "col-sm-2 text-right");
                LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prixVente_" + idItem, "Prix de vente : $" + prix.ToString() + "<br>", "prix_item");
                LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prixDemande_" + idItem, "Prix demandé : $" + prix.ToString(), "prix_item");

                // CheckBox (Supprimer plusieurs)
                Panel colCB = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colCB_" + idItem, "col-sm-1 text-right");
                CheckBox cbDelete = LibrairieControlesDynamique.cb(colCB, nomEntreprise + "_cbSupprimer_" + idItem, "");

                // Btn Delete
                Panel colDel = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colDel_" + idItem, "col-sm-1 text-right");
                HtmlButton btnSupprimer = LibrairieControlesDynamique.htmlbtnDYN(colDel, "btnSupprimer_" + idItem, "btn btn-danger left15", "", "glyphicon glyphicon-remove", null);
                btnSupprimer.Attributes.Add("onclick", "Confirm();");
                btnSupprimer.Style.Add("height", "105px");


            }
        }
        LibrairieControlesDynamique.hrDYN(panelBody, "OrangeBorder", 30);
    }

    protected void link_ProduitDetail(object sender, EventArgs e)
    {
        LinkButton linkProduit = (LinkButton)sender;       
        Response.Redirect("~/Pages/AffichageProduitDetaille.aspx?productId=" + 10000001);      
    }
}