using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Pages_GererCommandes : System.Web.UI.Page
{
    private BD6B8_424SEntities dbContext = new BD6B8_424SEntities();
    long noVendeur;

    String nomEntreprise;
    Panel panelBody2;
    Panel PanelCollapse;
    PPVendeurs leVendeur;

    protected void Page_Load(object sender, EventArgs e)
    {
        verifierPermissions("V");

        Page.Title = "Gestion des commandes";

        Page.MaintainScrollPositionOnPostBack = true;
        if (Session["NoVendeur"] != null)
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
        phDynamique.Controls.Clear();
        ulSideBar.Controls.Clear();
        // Créer le panier du vendeur X
        Panel panelGroup = LibrairieControlesDynamique.divDYN(phDynamique, nomEntreprise + "_PanelGroup", "panel-group container-fluid marginFluidSmall");
        Panel panelBase = LibrairieControlesDynamique.divDYN(panelGroup, nomEntreprise + "_base", "panel panel-default");


        // Nom de l'entreprise
        Panel panelHeader = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_header", "panel-heading");
        LibrairieControlesDynamique.lblDYN(panelHeader, nomEntreprise + "_nom", nomEntreprise + " - " + leVendeur.AdresseEmail, "nom-entreprise");

        // Liste des items + le total

        //Label lblCollapse = LibrairieControlesDynamique.lblDYN(panelHeader, nomEntreprise + "_lblPanelTitle", "panel-title","panel-title");
        // LibrairieControlesDynamique.aDYN(panelHeader, "Collapsible panel", "#contentBody_"+nomEntreprise + "_PanelCollapse", true);
        //Panel panelCollaspe = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_PanelCollapse", "panel-collapse collapse");
        Panel panelBody = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_PanelBody", "panel-body");

        Panel panCategorie = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_pretLivraison_", "row text-center");
        Panel colCatAfficher = LibrairieControlesDynamique.divDYN(panCategorie, nomEntreprise + "_colLabelPretLivraison", "col-sm-12");
        LibrairieControlesDynamique.lblDYN(colCatAfficher, nomEntreprise + "_labelCategorie", "Prêt pour livraison ", "infos-payage OrangeTitle");
        LibrairieControlesDynamique.liDYN(ulSideBar, "#contentBody_" + nomEntreprise + "_labelCategorie", "Prêt pour livraison", "");



        //LibrairieControlesDynamique.hrDYN(panelBody);
        LibrairieControlesDynamique.hrDYN(panelBody, "OrangeBorderPanier", 5);
        List<PPCommandes> lstCommandes = dbContext.PPCommandes.Where(c => c.Statut.Equals("0") && c.NoVendeur == noVendeur).OrderByDescending(c => c.DateCommande).ToList();
        // Rajouter les produits dans le panier        
        if (lstCommandes.Count > 0)
        {

            for (int i = 0; i < lstCommandes.Count; i++)
            {
                long idItem = lstCommandes[i].NoCommande;

                decimal prix = lstCommandes[i].MontantTotAvantTaxes.Value;
                long idClient = lstCommandes[i].NoClient.Value;
                PPClients leClient = dbContext.PPClients.Where(c => c.NoClient == idClient).First();
                int NbVisites = dbContext.PPVendeursClients.Where(c => (c.NoClient == idClient) && (c.NoVendeur == noVendeur)).Count();

                Panel rowItem = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowItem_" + idItem, "row valign");

                // ajouter l'image
                Panel colImg = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colImg_" + idItem, "col-sm-1");
                LibrairieControlesDynamique.imgDYN(colImg, nomEntreprise + "_img_" + idItem, "../static/images/imageCommande.png", "img-size center-block");
                LibrairieControlesDynamique.lblDYN(colImg, nomEntreprise + "_noproduit_" + idItem, idItem.ToString(), "caption center-block text-center");


                // Date de la commande
                Panel colDate = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colDate_" + idItem, "col-sm-2 text-center ");
                LibrairieControlesDynamique.lblDYN(colDate, nomEntreprise + "_date_" + idItem, "Date de la commande<br>" + lstCommandes[i].DateCommande.Value.ToShortDateString(), "prix_item");

                //Button Facture Commande
                Panel colFacture = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colFacture_" + idItem, "col-sm-2 text-center");
                if (File.Exists(Server.MapPath("~/static/pdf/" + idItem + ".pdf")))
                {
                    LibrairieControlesDynamique.htmlbtnDYN(colFacture, "btnFactures" + idItem, "btn btn-default Orange", "Facture", "glyphicon glyphicon-list-alt", btnFacture);
                }
                else
                {
                    HtmlButton btn = LibrairieControlesDynamique.htmlbtnDYN(colFacture, "btnFactureDisabled" + idItem, "btn btn-default Orange disabled", "Facture", "glyphicon glyphicon-list-alt", btnFacture);
                    btn.Attributes.Add("disabled", "disabled");
                }



                // Pooids total commande
                Panel colPoids = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPoids_" + idItem, "col-sm-1 text-center");
                LibrairieControlesDynamique.lblDYN(colPoids, nomEntreprise + "_Poids_" + idItem, "Poids <br>" + lstCommandes[i].PoidsTotal + " lbs", "border-quantite prix_item");

                // Nom du client
                Panel colNomClient = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colClient_" + idItem, "col-sm-2 text-center");
                LibrairieControlesDynamique.lblDYN(colNomClient, nomEntreprise + "_NomClient_" + idItem, "Client<br>" + leClient.Prenom + " " + leClient.Nom, "nomClient prix_item");

                // Visites
                Panel colVisites = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colVisite_" + idItem, "col-sm-1 text-center");
                LibrairieControlesDynamique.lblDYN(colVisites, nomEntreprise + "_Visites_" + idItem, "Visite(s)<br>" + NbVisites, "prix_item");

                // Total avant taxes
                Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPrix_" + idItem, "col-sm-2 text-center");
                LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prix_" + idItem, "Total<br>(sans taxes)<br>" + prix.ToString("C", CultureInfo.CurrentCulture), "prix_item");


                Panel colLivrer = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colLivrer" + idItem, "col-sm-1 text-center");
                Button btnLivrer = LibrairieControlesDynamique.btnDYN(colLivrer, "btnLivre" + idItem, "btn btn-default OrangeButton", "Livrer", btnLivre);
                btnLivrer.OnClientClick = "if( !livraisonConfirm()) return false;";

                //   LibrairieControlesDynamique.hrDYN(panelBody);
            }
        }
        else
        {
            Panel row = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowPretLivraisonVide", "row marginFluid text-center ");
            Panel message = LibrairieControlesDynamique.divDYN(row, nomEntreprise + "_messagePretLivraisonVide", "message text-center top15");
            Panel messageContainer = LibrairieControlesDynamique.divDYN(message, nomEntreprise + "_divMessageLivraison", "alert alert-danger alert-margins valignMessage");
            LibrairieControlesDynamique.lblDYN(messageContainer, nomEntreprise + "_leMessageLabelLivraison", "Vous avez aucune commande prête pour la livraison.");
        }

        LibrairieControlesDynamique.hrDYN(panelBody, "OrangeBorderPanier", 5);

        Panel panCategorie2 = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_Livre_", "row text-center");
        Panel colCatAfficher2 = LibrairieControlesDynamique.divDYN(panCategorie2, nomEntreprise + "_colLabelLivre", "col-sm-12");
        LibrairieControlesDynamique.lblDYN(colCatAfficher2, nomEntreprise + "_labelLivre", "Commandes livrés ", "infos-payage OrangeTitle");
        HtmlGenericControl li = LibrairieControlesDynamique.liDYN(ulSideBar, "#contentBody_" + nomEntreprise + "_labelLivre", "Commandes livrés", "");
        LibrairieControlesDynamique.hrDYN(panelBody, "OrangeBorderPanier", 5);


        List<PPCommandes> lstCommandesLivre = dbContext.PPCommandes.Where(c => !c.Statut.Equals("0") && c.NoVendeur == noVendeur).OrderByDescending(c => c.DateCommande).ToList();
        // Rajouter les produits dans le panier        
        if (lstCommandesLivre.Count > 0)
        {
            for (int i = 0; i < lstCommandesLivre.Count; i++)
            {
                long idItem = lstCommandesLivre[i].NoCommande;

                decimal prix = lstCommandesLivre[i].MontantTotAvantTaxes.Value;
                long idClient = lstCommandesLivre[i].NoClient.Value;
                PPClients leClient = dbContext.PPClients.Where(c => c.NoClient == idClient).First();
                int NbVisites = dbContext.PPVendeursClients.Where(c => (c.NoClient == idClient) && (c.NoVendeur == noVendeur)).Count();

                Panel rowItem = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowItem2_" + idItem, "row valign");

                // ajouter l'image
                Panel colImg = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colImg2_" + idItem, "col-sm-1");
                LibrairieControlesDynamique.imgDYN(colImg, nomEntreprise + "_img2_" + idItem, "../static/images/imageCommande.png", "img-size center-block");
                LibrairieControlesDynamique.lblDYN(colImg, nomEntreprise + "_noproduit2_" + idItem, idItem.ToString(), "caption center-block text-center");


                // Date de la commande
                Panel colDate = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colDate2_" + idItem, "col-sm-2 text-center ");
                LibrairieControlesDynamique.lblDYN(colDate, nomEntreprise + "_date2_" + idItem, "Date de la commande<br>" + lstCommandesLivre[i].DateCommande.Value.ToShortDateString(), "prix_item");

                //Button Facture Commande
                Panel colFacture = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colFacture2_" + idItem, "col-sm-2 text-center");
                if (File.Exists(Server.MapPath("~/static/pdf/" + idItem + ".pdf")))
                {
                    LibrairieControlesDynamique.htmlbtnDYN(colFacture, "btnFacture2" + idItem, "btn btn-default Orange", "Facture", "glyphicon glyphicon-list-alt", btnFactures);
                }
                else
                {
                    HtmlButton btn = LibrairieControlesDynamique.htmlbtnDYN(colFacture, "btnFacture2" + idItem, "btn btn-default Orange disabled", "Facture", "glyphicon glyphicon-list-alt", btnFactures);
                    btn.Attributes.Add("disabled", "disabled");
                }



                // Pooids total commande
                Panel colPoids = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPoids2_" + idItem, "col-sm-1 text-center");
                LibrairieControlesDynamique.lblDYN(colPoids, nomEntreprise + "_Poids2_" + idItem, "Poids <br>" + lstCommandesLivre[i].PoidsTotal + " lbs", "border-quantite prix_item");

                // Nom du client
                Panel colNomClient = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colClient2_" + idItem, "col-sm-2 text-center");
                LibrairieControlesDynamique.lblDYN(colNomClient, nomEntreprise + "_NomClient2_" + idItem, "Client<br>" + leClient.Prenom + " " + leClient.Nom, "nomClient prix_item");

                // Visites
                Panel colVisites = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colVisite_" + idItem, "col-sm-1 text-center");
                LibrairieControlesDynamique.lblDYN(colVisites, nomEntreprise + "_Visites_" + idItem, "Visite(s)<br>" + NbVisites, "prix_item");

                List<PPDetailsCommandes> lstDetailsCommandes = dbContext.PPDetailsCommandes.Where(c => c.NoCommande == idItem).ToList();
                int nbItems = 0;
                for (int j = 0; j < lstDetailsCommandes.Count; j++)
                {
                    nbItems++;
                }

                // Nombre items
                Panel colNbItem = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colNbItem_" + idItem, "col-sm-1 text-center");
                LibrairieControlesDynamique.lblDYN(colNbItem, nomEntreprise + "_nbItems_" + idItem, "Nombres d'items<br>" + nbItems, "nomClient prix_item");

                // Total avant taxes
                Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPrix2_" + idItem, "col-sm-2 text-center");
                LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prix2_" + idItem, "Total (sans taxes)<br>" + prix.ToString("C", CultureInfo.CurrentCulture), "prix_item");


            }//   LibrairieControlesDynamique.hrDYN(panelBody);

        }

        else
        {
            Panel row = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowCommLivrer", "row marginFluid text-center ");
            Panel message = LibrairieControlesDynamique.divDYN(row, nomEntreprise + "_messageCommLivrer", "message text-center top15");
            Panel messageContainer = LibrairieControlesDynamique.divDYN(message, nomEntreprise + "_divCommLivrer", "alert alert-danger alert-margins valignMessage");
            LibrairieControlesDynamique.lblDYN(messageContainer, nomEntreprise + "_leMessageLabelCommLivrer", "Vous avez aucune commande prête pour la livraison.");
        }


        LibrairieControlesDynamique.hrDYN(panelBody, "OrangeBorderPanier", 5);

    }

    private void btnFactures(object sender, EventArgs e)
    {
        HtmlButton btn = (HtmlButton)sender;
        string fileName = btn.ID.Replace("btnFacture2", "");
        if (File.Exists(Server.MapPath("~/static/pdf/" + fileName + ".pdf")))
        {
            string url = "../static/pdf/" + fileName + ".pdf";
            Response.Write("<script>window.open ('" + url + "','_blank');</script>");
        }
    }

    private void btnLivre(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        long idCommande = long.Parse(btn.ID.Replace("btnLivre", "").ToString());
        PPCommandes commandeLivre = dbContext.PPCommandes.Where(c => c.NoCommande == idCommande).First();
        commandeLivre.Statut = "1";
        try
        {
            dbContext.SaveChanges();
            creerPage();
        }
        catch (Exception ex)
        {

        }

    }

    private void btnFacture(object sender, EventArgs e)
    {
        HtmlButton btn = (HtmlButton)sender;
        string fileName = btn.ID.Replace("btnFactures", "");
        if (File.Exists(Server.MapPath("~/static/pdf/" + fileName + ".pdf")))
        {
            string url = "../static/pdf/" + fileName + ".pdf";
            Response.Write("<script>window.open ('" + url + "','_blank');</script>");
        }
    }
}