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
        verifierPermissions("V");
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
        nbMois = Session["ddlMoisInactif"] != null && Convert.ToInt32(Session["ddlMoisInactif"]) != 0 ? Convert.ToInt32(Session["ddlMoisInactif"]) : 1;
        if(Session["NoVendeur"] != null)
            noVendeur = Convert.ToInt32((Session["NoVendeur"]));
        leVendeur = dbContext.PPVendeurs.Where(c => c.NoVendeur == noVendeur).First();
        nomEntreprise = leVendeur.NomAffaires;
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
        System.Diagnostics.Debug.WriteLine(" LE NBMOIS = " + nbMois + " dateParMois = " + dateParMois + " dateParMoisMAx = " + dateParMoisMax);
        if (nbMois == 7)
        {
            dateParMoisMax = DateTime.MinValue;
        }
        List<PPArticlesEnPanier> lstPaniersEntreprise = new List<PPArticlesEnPanier>();
        List<PPArticlesEnPanier> lstArticles = dbContext.PPArticlesEnPanier.GroupBy(x => x.NoClient).Select(t => t.OrderBy(c => c.DateCreation).FirstOrDefault()).ToList();
        foreach(PPArticlesEnPanier lesArticles in lstArticles)
        {            
            if ((lesArticles.DateCreation <= dateParMois) && (lesArticles.DateCreation >= dateParMoisMax))
                lstPaniersEntreprise.AddRange(dbContext.PPArticlesEnPanier.Where(c => (c.NoVendeur == noVendeur) && (c.NoClient == lesArticles.NoClient)).OrderBy(C => C.DateCreation).ToList());
        }       
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
                    string nomClient = (leClient.Nom != null && leClient.Nom != "") ? "Client : " + leClient.Prenom + " " + leClient.Nom : "Client : " + leClient.AdresseEmail;
                    LibrairieControlesDynamique.lblDYN(colNomClient, nomEntreprise + "_NomClient2_" + idItem, nomClient, "nomClient prix_item");
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
        LinkButton lb = (LinkButton)sender;
        string strNoProduit = lb.ID.Replace(nomEntreprise + "_nom2_", "");
        String url = "~/Pages/InscriptionProduit.aspx?NoProduit=" + strNoProduit + "&Operation=Afficher";
        Response.Redirect(url, true);
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