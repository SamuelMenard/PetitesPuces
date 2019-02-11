using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_GererPanierInactifs : System.Web.UI.Page
{
    DropDownList ddlNbMois;
    int moisChoisis;

    private BD6B8_424SEntities dbContext = new BD6B8_424SEntities();
    long noVendeur;
    String nomEntreprise;
    PPVendeurs leVendeur;
    Panel panelBody2;
    Panel PanelCollapse;
    int nbMois;

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("PAGE LOAD LE DDL = " + Session["ddlMoisInactif"]);
        if (Session["ddlMoisInactif"] != null && !IsPostBack)
        {
            if(Convert.ToInt32(Session["ddlMoisInactif"]) == 7)
            {
                ddlMois.SelectedIndex = ddlMois.Items.IndexOf(ddlMois.Items.FindByText("7+"));
            }
            else{
                ddlMois.SelectedIndex = ddlMois.Items.IndexOf(ddlMois.Items.FindByText(Convert.ToString(Session["ddlMoisInactif"])));
            }           
          
        }
        nbMois = Convert.ToInt32(Session["ddlMoisInactif"]);
        noVendeur = Convert.ToInt32((Session["NoVendeur"]));
        leVendeur = dbContext.PPVendeurs.Where(c => c.NoVendeur == noVendeur).First();
        nomEntreprise = leVendeur.NomAffaires;
        creerPage();
    }
    
    private void creerPage()
    {
        System.Diagnostics.Debug.WriteLine("PAGE LOAD LE DDL = " + nbMois);

        Panel panelGroup = LibrairieControlesDynamique.divDYN(phDynamique, nomEntreprise + "_PanelGroup2", "panel-group container-fluid marginFluid");
        Panel panelBase = LibrairieControlesDynamique.divDYN(panelGroup, nomEntreprise + "_base2", "panel panel-default");
        // Nom de l'entreprise
        Panel panelHeader = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_header2", "panel-heading");

        Panel rowInactif = LibrairieControlesDynamique.divDYN(panelHeader, nomEntreprise + "_rowInactif2_", "row");
        Panel colInactif = LibrairieControlesDynamique.divDYN(rowInactif, nomEntreprise + "_colInactif2_", "col-sm-12");
        LibrairieControlesDynamique.lblDYN(colInactif, nomEntreprise + "_nom2", nomEntreprise + " - " + leVendeur.AdresseEmail, "nom-entreprise");
        panelBody2 = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_PanelBody2", "panel-body");
        DateTime dateParMois = DateTime.Now.AddMonths(-nbMois);
      
        DateTime dateParMoisMax = DateTime.Now.AddMonths(-(nbMois+1));
        if (nbMois == 7)
        {
            dateParMoisMax = DateTime.MinValue;
        }
        List<PPArticlesEnPanier> lstPaniersEntreprise = new List<PPArticlesEnPanier>();
        List<PPClients> lesClients = dbContext.PPArticlesEnPanier.Where(c => (c.NoVendeur == noVendeur) && (c.DateCreation <= dateParMois) && (c.DateCreation >= dateParMoisMax)).OrderBy(C => C.DateCreation).Select(c => c.PPClients).Distinct().ToList();
        foreach(PPClients leClient in lesClients)
        {            
            if(nbMois != 7)
            lstPaniersEntreprise.AddRange(dbContext.PPArticlesEnPanier.Where(c => (c.NoVendeur == noVendeur) && (c.NoClient == leClient.NoClient) && (c.DateCreation <= dateParMois) && (c.DateCreation >= dateParMoisMax)).OrderBy(C => C.DateCreation).ToList());
            else
                lstPaniersEntreprise.AddRange(dbContext.PPArticlesEnPanier.Where(c => (c.NoVendeur == noVendeur) && (c.NoClient == leClient.NoClient)).OrderBy(C => C.DateCreation).ToList());
        }
       // lstPaniersEntreprise = 
        panelBody2.Controls.Clear();
        Panel panCategorie = LibrairieControlesDynamique.divDYN(panelBody2, nomEntreprise + "_pretLivraison2_", "row text-center");
        Panel colCatAfficher = LibrairieControlesDynamique.divDYN(panCategorie, nomEntreprise + "_colLabelPretLivraison2", "col-sm-12");
        LibrairieControlesDynamique.lblDYN(colCatAfficher, nomEntreprise + "_labelCategorie2", "Panier Courants ", "infos-payage OrangeTitle");        
        LibrairieControlesDynamique.hrDYN(colCatAfficher, "OrangeBorderPanier", 5);


        // Rajouter les produits dans le panier

        if (lstPaniersEntreprise.Count > 0)
        {
            long ancienID = 0;
            for (int i = 0; i < lstPaniersEntreprise.Count; i++)
            {
                long idItem = lstPaniersEntreprise[i].NoPanier;
                long idProduit = lstPaniersEntreprise[i].NoProduit.Value;                
                PPProduits leProduit = dbContext.PPProduits.Where(c => c.NoProduit == idProduit).First();
                PPCategories laCategorie = dbContext.PPCategories.Where(c => c.NoCategorie == leProduit.NoCategorie).First();
                long idClient = lstPaniersEntreprise[i].NoClient.Value;
                int NbVisites = dbContext.PPVendeursClients.Where(c => (c.NoClient == idClient) && (c.NoVendeur == noVendeur)).Count();
                PPClients leClient = dbContext.PPClients.Where(c => c.NoClient == idClient).First();
                decimal prix = leProduit.PrixVente.Value;
                String nomProduit = leProduit.Nom.ToString();
                String urlImage = "../static/images/" + leProduit.Photo.ToString();


                if (ancienID != lstPaniersEntreprise[i].NoClient.Value)
                {
                    //Trigger panel pour accordeon
                    Panel panelTrigger = LibrairieControlesDynamique.divDYN(panelBody2, nomEntreprise + "_PanelTrigger" + i, "trigger");
                    Panel rowClient = LibrairieControlesDynamique.divDYN(panelTrigger, nomEntreprise + "_rowTrigger_" + idItem, "row top15 text-center border-left");
                    // Col Glyph
                    Panel colGlyph = LibrairieControlesDynamique.divDYN(rowClient, nomEntreprise + "_colGlyph_" + idItem, "col-sm-2");
                    LibrairieControlesDynamique.lblDYN(colGlyph, nomEntreprise + "_GlyphPanier_" + idItem, "", "glyphicon glyphicon-shopping-cart text-left");
                    //Nom Client
                    Panel colNomClient = LibrairieControlesDynamique.divDYN(rowClient, nomEntreprise + "_colClient2_" + idItem, "col-sm-2 text-left");
                    LibrairieControlesDynamique.lblDYN(colNomClient, nomEntreprise + "_NomClient2_" + idItem, "Client : " + leClient.Prenom + " " + leClient.Nom, "nomClient prix_item");
                    // Nb Visites du client
                    Panel colClientVisites = LibrairieControlesDynamique.divDYN(rowClient, nomEntreprise + "_colVisites_" + idItem, "col-sm-2 text-left");
                    LibrairieControlesDynamique.lblDYN(colClientVisites, nomEntreprise + "_VisiteClient_" + idItem, " Nombre de visites : " + NbVisites, "nomClient prix_item");

                    //Date
                    Panel colDate = LibrairieControlesDynamique.divDYN(rowClient, nomEntreprise + "_colDate_" + idItem, "col-sm-2 text-left");
                    LibrairieControlesDynamique.lblDYN(colDate, nomEntreprise + "_date_" + idItem,"Date de création : "+ Convert.ToDateTime(lstPaniersEntreprise[i].DateCreation).ToShortDateString(), "prix_item");

                    //SousTotal du panier
                    decimal sousTotalPanier = 0;
                    for (int j = 0; j < lstPaniersEntreprise.Count; j++)
                    {
                        if (lstPaniersEntreprise[j].NoClient == leClient.NoClient)
                        {
                            long idItem2 = lstPaniersEntreprise[j].NoProduit.Value;
                            decimal calculSousTotal = (decimal)dbContext.PPProduits.Where(c => c.NoProduit == idItem2).First().PrixVente;
                            sousTotalPanier += calculSousTotal * lstPaniersEntreprise[j].NbItems.Value;
                        }
                    }
                    Panel colSousTotal = LibrairieControlesDynamique.divDYN(rowClient, nomEntreprise + "_colSousTotalPanier_" + idItem, "col-sm-2 text-center");
                    LibrairieControlesDynamique.lblDYN(colSousTotal, nomEntreprise + "_SousTotalPanier_" + idItem, "$" + sousTotalPanier.ToString("N"), "nomClient prix_item");

                    if (nbMois == 7)
                    {
                        Panel colDel = LibrairieControlesDynamique.divDYN(rowClient, nomEntreprise + "_colDel_", "col-sm-2");
                        Button btnSupprimer = LibrairieControlesDynamique.btnDYN(colDel, "btnSupprimer_" + idClient, "btn btn-danger", "Supprimer", btnSupprimer_click);
                        btnSupprimer.OnClientClick = "if( !PanierDeleteConfirmation()) return false;";
                        //btnSupprimer.Attributes.Add("onClientClick", );
                    }
                   

                    //Panneau Accordeon
                    PanelCollapse = LibrairieControlesDynamique.divDYN(panelBody2, nomEntreprise + "_PanelCollapse" + i, "panel panelAccord");

                }

                Panel rowItem = LibrairieControlesDynamique.divDYN(PanelCollapse, nomEntreprise + "_rowItem2_" + idItem, "row valign top15");

                // ajouter l'image
                Panel colImg = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colImg2_" + idItem, "col-sm-2 ");
                LibrairieControlesDynamique.imgDYN(colImg, nomEntreprise + "_img2_" + idItem, urlImage, "img-size center-block");
                LibrairieControlesDynamique.lblDYN(colImg, nomEntreprise + "_noproduit2_" + idItem, idProduit.ToString(), "caption center-block text-center");


                // Nom du produit
                Panel colNom = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colNom2_" + idItem, "col-sm-3 LiensProduits nomClient");
                LibrairieControlesDynamique.lbDYN(colNom, nomEntreprise + "_nom2_" + idProduit, nomProduit, descriptionProduit);

                // Quantité restant
                Panel colQuantite = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colQuantite2_" + idItem, "col-sm-2 text-right");
                LibrairieControlesDynamique.lblDYN(colQuantite, nomEntreprise + "_quantite2_" + idItem, "Qte : " + lstPaniersEntreprise[i].NbItems, "prix_item");

                // Categorie
                Panel colCat = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colCategorie_" + idItem, "col-sm-3 text-right");
                LibrairieControlesDynamique.lblDYN(colCat, nomEntreprise + "_categorie2_" + idItem, laCategorie.Description.ToString(), "cat_item");

                // Prix item
                Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPri2x_" + idItem, "col-sm-2 text-center");
                LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prix2_" + idItem, "Prix Unitaire<br> $" + prix.ToString("N"), "prix_item");

               

                ancienID = idClient;
            }
            LibrairieControlesDynamique.hrDYN(panelBody2, "OrangeBorderPanier", 5);
        }
        else
        {
            Panel row = LibrairieControlesDynamique.divDYN(panelBody2, nomEntreprise + "_rowPanierVide", "row marginFluid text-center");
            Panel message = LibrairieControlesDynamique.divDYN(row, nomEntreprise + "_messagePanierVide", "message text-center top15");
            Panel messageContainer = LibrairieControlesDynamique.divDYN(message, nomEntreprise + "_divMessage", "alert alert-danger alert-margins");
            LibrairieControlesDynamique.lblDYN(messageContainer, nomEntreprise + "_leMessageLabel", "Vous avez aucun panier courant.");
            LibrairieControlesDynamique.hrDYN(panelBody2, "OrangeBorderPanier", 5);
        }

    }

    private void descriptionProduit(object sender, EventArgs e)
    {
        //throw new NotImplementedException();
    }

    private void btnSupprimer_click(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine(" ENTER SUPPRIMER ");
        Button btn = (Button)sender;
        long idClient = long.Parse(btn.ID.Replace("btnSupprimer_", ""));
        List<PPArticlesEnPanier> lesArticles = dbContext.PPArticlesEnPanier.Where(c => c.NoClient == idClient).ToList();
        using (var dbContextTransaction = dbContext.Database.BeginTransaction())
        {
            try
            {               
                if (lesArticles.Count > 0)
                {
                    dbContext.PPArticlesEnPanier.RemoveRange(lesArticles);
                    dbContext.SaveChanges();
                }               
                dbContextTransaction.Commit();
                String url = "~/Pages/GererPanierInactifs.aspx?";
                Response.Redirect(url, false);
            }
            catch (Exception ex)
            {
                dbContextTransaction.Rollback();
                System.Diagnostics.Debug.WriteLine(ex.StackTrace.ToString());
            }
        }
    }

    protected void nbMoisChange(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine(" LE DDL = " + Session["ddlMoisInactif"]);
        int result;
        if (int.TryParse(ddlMois.SelectedItem.Text, out result))
        {
            Session["ddlMoisInactif"] = result;           
        }
        else
        {
            Session["ddlMoisInactif"] = 7;
        }         
        string url = "~/Pages/GererPanierInactifs.aspx?";
        Response.Redirect(url);
    }    
}