using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_SuppressionProduit : System.Web.UI.Page
{
    List<String> lstImages = new List<string>();
    String nomEntreprise = "Apple";
    Panel panelBody;    
    int moisChoisis;
    
    protected void Page_Load(object sender, EventArgs e)
    {        

        Panel panelGroup = LibrairieControlesDynamique.divDYN(phDynamique, nomEntreprise + "_PanelGroup", "panel-group");
        Panel panelBase = LibrairieControlesDynamique.divDYN(panelGroup, nomEntreprise + "_base", "panel panel-default");
        // Nom de l'entreprise
        Panel panelHeader = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_header", "panel-heading");

        Panel rowInactif = LibrairieControlesDynamique.divDYN(panelHeader, nomEntreprise + "_rowInactif_", "row");       
        Panel colInactif = LibrairieControlesDynamique.divDYN(rowInactif, nomEntreprise + "_colInactif_", "col-sm-6");
        LibrairieControlesDynamique.lblDYN(colInactif, nomEntreprise + "_nom", nomEntreprise, "nom-entreprise");        
        panelBody = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_PanelBody", "panel-body");

        lstImages.Clear();
       
        lstImages.Add("500,97;Apple Watch 4;../static/images/appleWatch.jpg");
        lstImages.Add("250,97;Airpods;../static/images/airpods.jpg");      
        lstImages.Add("1500,97;iPhone XS Max;../static/images/iPhone.jpg");
        lstImages.Add("450,97;Écouteurs beats;../static/images/beats.jpg");
        lstImages.Add("2200,97;MacBook Air 13\", 256GB SSD - Rose Gold<br>;../static/images/macbookair13.jpg");
        lstImages.Add("350,97;Homepod - Blanc;../static/images/homepod.jpg");        

        creerPage();
    }

    private void creerPage()
    {        
        panelBody.Controls.Clear();
        LibrairieControlesDynamique.hrDYN(panelBody, "OrangeBorder", 30);
        // Rajouter les produits dans le panier
        int idItem = 0;
        for (int i = 0; i < 6; i++)
        {
            idItem++;
            int quantiteSelectionne = 1;
            string[] strValeurs;
            Double prix = 0.0;
            String nomProduit = "";
            String urlImage = "";
          
            if(lstImages.Count > 0)
            {
                strValeurs = lstImages[i].Split(';');
                prix = Convert.ToDouble(strValeurs[0]);
                nomProduit = strValeurs[1];
                urlImage = strValeurs[2];
            }
               

            Panel rowItem = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowItem_" + idItem, "row valign top15");

            // ajouter l'image
            Panel colImg = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colImg_" + idItem, "col-sm-2 ");
            LibrairieControlesDynamique.imgDYN(colImg, nomEntreprise + "_img_" + idItem, urlImage, "img-size center-block");
            LibrairieControlesDynamique.lblDYN(colImg, nomEntreprise + "_noproduit_" + idItem, "1000000" + i, "caption center-block text-center");


            // Nom du produit
            Panel colNom = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colNom_" + idItem, "col-sm-4 LiensProduits nomClient");
            LibrairieControlesDynamique.lbDYN(colNom, nomEntreprise + "_nom_" + idItem, nomProduit, null);

            // Quantité restant
            Panel colQuantite = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colQuantite_" + idItem, "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colQuantite, nomEntreprise + "_quantite_" + idItem, "Qte : 2", "prix_item");
          
            // Categorie
            Panel colCat = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colCategorie_" + idItem, "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colCat, nomEntreprise + "_categorie_" + idItem, "Articles électroniques", "cat_item");

            // Prix item
            Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPrix_" + idItem, "col-sm-1 text-right");
            LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prix_" + idItem, "$" + prix.ToString(), "prix_item");

            Panel colDel = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colDel_" + idItem, "col-sm-1 text-right");
            HtmlButton btnSupprimer = LibrairieControlesDynamique.htmlbtnDYN(colDel, "btnSupprimer_" + idItem, "btn btn-danger left15", "", "glyphicon glyphicon-remove", btnSupprimer_click);
            btnSupprimer.Style.Add("height", "105px");
            

        }
        LibrairieControlesDynamique.hrDYN(panelBody, "OrangeBorder", 30);
    }

    private void btnSupprimer_click(object sender, EventArgs e)
    {
        //throw new NotImplementedException();
    }


}