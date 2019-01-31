using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_GererPanierInactifs : System.Web.UI.Page
{
    DropDownList ddlNbMois;
    int moisChoisis;

    protected void Page_Load(object sender, EventArgs e)
    {
       // getNbMois();         
        creerPage();
    }


    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        //ddlNbMois.SelectedValue = this.moisChoisis.ToString();
    }

    private void creerPage()
    {
        List<String> lstImages = new List<string>();
        String nomEntreprise = "Apple";
        Panel panelBody;
        
        Panel panelGroup = LibrairieControlesDynamique.divDYN(phDynamique, nomEntreprise + "_PanelGroup", "panel-group");
        Panel panelBase = LibrairieControlesDynamique.divDYN(panelGroup, nomEntreprise + "_base", "panel panel-default");
        // Nom de l'entreprise
        Panel panelHeader = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_header", "panel-heading");

        Panel rowInactif = LibrairieControlesDynamique.divDYN(panelHeader, nomEntreprise + "_rowInactif_", "row");
        Panel colInactif = LibrairieControlesDynamique.divDYN(rowInactif, nomEntreprise + "_colInactif_", "col-sm-6");
        LibrairieControlesDynamique.lblDYN(colInactif, nomEntreprise + "_nom", nomEntreprise, "nom-entreprise");
        Panel colDDLInactif = LibrairieControlesDynamique.divDYN(rowInactif, nomEntreprise + "_colDDLInactif_", "col-sm-6");
        LibrairieControlesDynamique.lblDYN(colDDLInactif, nomEntreprise + "_nbParPage", "Nombre de mois inactifs : ", "left15");
        ddlNbMois = LibrairieControlesDynamique.ddlDYN(colDDLInactif, "ddlMoisInactifs", "left15");
        ddlNbMois.Items.Clear();
        //ddlNbMois.AutoPostBack = true;
        //ddlNbMois.ViewStateMode = ViewStateMode.Enabled;
        //ddlNbMois.SelectedIndexChanged += valeursPanier;
        //ddlNbMois.AppendDataBoundItems = true;
        ddlNbMois.Items.Insert(0, new ListItem(String.Empty, String.Empty));
        ddlNbMois.Items.Insert(1, new ListItem("1", "1"));
        ddlNbMois.Items.Insert(2, new ListItem("2", "2"));
        ddlNbMois.Items.Insert(3, new ListItem("3", "3"));
        ddlNbMois.Items.Insert(4, new ListItem("6", "6"));
        ddlNbMois.Items.Insert(5, new ListItem("6+", "7"));
        panelBody = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_PanelBody", "panel-body");

        lstImages.Clear();
        if (moisChoisis == 1)
        {
            lstImages.Add("500,97;Apple Watch 4 x1;../static/images/orangeShop.png");
            lstImages.Add("250,97;Airpods x1;../static/images/orangeShop.png");
        }
        else if (moisChoisis == 2)
        {
            lstImages.Add("1500,97;iPhone XS Max x1;../static/images/orangeShop.png");
            lstImages.Add("450,97;Écouteurs beats x1;../static/images/orangeShop.png");
        }
        else if (moisChoisis > 6)
        {
            lstImages.Add("2200,97;MacBook Air 13\", 256GB SSD - Rose Gold x1<br>;../static/images/orangeShop.png");
            lstImages.Add("350,97;Homepod - Blanc x1;../static/images/orangeShop.png");
        }

        panelBody.Controls.Clear();
        LibrairieControlesDynamique.hrDYN(panelBody, "OrangeBorder", 30);
        // Rajouter les produits dans le panier
        int idItem = 0;
        for (int i = 0; i < 2; i++)
        {
            idItem++;            
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
               

            Panel rowItem = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowItem_" + idItem, "row");

            // ajouter l'image
            Panel colImg = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colImg_" + idItem, "col-sm-2");
            LibrairieControlesDynamique.imgDYN(colImg, nomEntreprise + "_img_" + idItem, urlImage, "img-size center-block");
            LibrairieControlesDynamique.lblDYN(colImg, nomEntreprise + "_noproduit_" + idItem, "1000000" + i, "caption center-block text-center");


            // Nom du produit
            Panel colNom = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colNom_" + idItem, "col-sm-4 LiensProduits");
            LibrairieControlesDynamique.lbDYN(colNom, nomEntreprise + "_nom_" + idItem, nomProduit, null);

            // Quantité restant
            Panel colNomClient = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colClient_" + idItem, "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colNomClient, nomEntreprise + "_NomClient_" + idItem, "Client : <br>Nicolas Jouanique", "nomClient");

            // Categorie
            Panel colCat = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colCategorie_" + idItem, "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colCat, nomEntreprise + "_categorie_" + idItem, "Articles électroniques", "cat_item");

            // Prix item
            Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPrix_" + idItem, "col-sm-2 text-right");
            LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prix_" + idItem, "$" + prix.ToString(), "prix_item");
            if (moisChoisis > 6)
            {
                HtmlButton btnSupprimer = LibrairieControlesDynamique.htmlbtnDYN(colPrix, "btnSupprimer_" + idItem, "btn btn-danger left15", "", "glyphicon glyphicon-remove", btnSupprimer_click);
                btnSupprimer.Style.Add("height", "105px");
            }

        }
        LibrairieControlesDynamique.hrDYN(panelBody, "OrangeBorder", 30);
    }

    private void btnSupprimer_click(object sender, EventArgs e)
    {
        
    }

    private void valeursPanier(object sender, EventArgs e)
    {        
        String url = "~/Pages/GererPanierInactifs.aspx?NbMois=" + ddlNbMois.SelectedValue;
        Response.Redirect(url, true);    
    }

    private void getNbMois()
    {
        int n;        
        if (Request.QueryString["NbMois"] == null || !int.TryParse(Request.QueryString["NbMois"], out n))
        {
            this.moisChoisis = 1;
        }
        else
        {
            this.moisChoisis = n;
        }
    }
}