using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_SaisieCommande : System.Web.UI.Page
{
    private int idEntreprise;
    private String etape;

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;

        // Aller chercher les valeurs en GET
        getIdEntreprise();
        getEtapeCommande();

        switch (etape)
        {
            case "panier": afficherPanier(); break;
            case "livraison": afficherInfosPerso(); break;
            case "bon": afficherBon(); break;
        }

    }

    public void afficherPanier()
    {
        // ajuster la bar de progression
        progressBar.Style.Add("width", "25%");

        List<PPArticlesEnPanier> lstArticlesEnPanier = LibrairieLINQ.getProduitsVendeurSpecifiqueClient(long.Parse(Session["NoClient"].ToString()), this.idEntreprise);
        // masquer le formulaire de livraison
        divInfosPerso.Visible = false;

        // masquer la div de paiement
        div_paiement.Visible = false;

        bool ruptureStock = false;
        bool quantiteModif = false;

        // do something with entry.Value or entry.Key
        long? idEntreprise = lstArticlesEnPanier[0].NoVendeur;
        String nomEntreprise = lstArticlesEnPanier[0].PPVendeurs.NomAffaires;
        String nomVendeur = lstArticlesEnPanier[0].PPVendeurs.Prenom + " " + lstArticlesEnPanier[0].PPVendeurs.Nom;
        decimal? sousTotal = 0;

        // Créer le panier du vendeur X
        Panel panelBase = LibrairieControlesDynamique.divDYN(paniersDynamique, "panier" + idEntreprise, "panel panel-default");

        // Nom de l'entreprise
        Panel panelHeader = LibrairieControlesDynamique.divDYN(panelBase, idEntreprise + "_header", "panel-heading");
        LibrairieControlesDynamique.lbDYN(panelHeader, "vendeur_" + idEntreprise, nomEntreprise + " (" + nomVendeur + ")", "nom-entreprise", nomEntreprisePanier_click);

        // Liste des items + le total
        Panel panelBody = LibrairieControlesDynamique.divDYN(panelBase, idEntreprise + "_body", "panel-body");


        // Rajouter les produits dans le panier

        foreach (PPArticlesEnPanier article in lstArticlesEnPanier)
        {
            long? idItem = article.NoProduit;
            short? quantiteSelectionne = article.NbItems;
            decimal? prixUnitaire = article.PPProduits.PrixDemande;

            decimal? prixAvecQuantites = article.PPProduits.PrixDemande * article.NbItems;
            decimal? montantRabais = article.PPProduits.PrixDemande - article.PPProduits.PrixVente;

            decimal? poids = article.PPProduits.Poids;

            // sum au sous total
            sousTotal += (prixUnitaire * article.NbItems);

            String nomProduit = article.PPProduits.Nom;
            String urlImage = "../static/images/" + article.PPProduits.Photo;

            Panel rowItem = LibrairieControlesDynamique.divDYN(panelBody, idEntreprise + "_rowItem_" + idItem, "row");

            // ajouter l'image
            Panel colImg = LibrairieControlesDynamique.divDYN(rowItem, idEntreprise + "_colImg_" + idItem, "col-sm-2");
            LibrairieControlesDynamique.imgDYN(colImg, idEntreprise + "_img_" + idItem, urlImage, "img-size");

            // Nom du produit
            Panel colNom = LibrairieControlesDynamique.divDYN(rowItem, idEntreprise + "_colNom_" + idItem, "col-sm-4");
            LibrairieControlesDynamique.lblDYN(colNom, idEntreprise + "_nom_" + idItem, nomProduit, "nom-item");
            LibrairieControlesDynamique.brDYN(colNom);
            LibrairieControlesDynamique.lblDYN(colNom, "", "Poids: " + poids + " lbs", "prix_unitaire");

            // Quantité sélectionné
            Panel colQuantite = LibrairieControlesDynamique.divDYN(rowItem, idEntreprise + "_colQuantite_" + idItem, "col-sm-4");

            TextBox tbQuantite = LibrairieControlesDynamique.numericUpDownDYN(colQuantite, "quantite_" + idItem,
                quantiteSelectionne.ToString(), (article.PPProduits.NombreItems < 1) ? "1" : article.PPProduits.NombreItems.ToString(), "form-control border-quantite");

            // vérifier les quantites
            if (article.PPProduits.NombreItems < 1)
            {
                ruptureStock = true;
                tbQuantite.Enabled = false;
                LibrairieControlesDynamique.lblDYN(colQuantite, "", "Rupture de stock", "rupture-stock");
            }
            else
            {
                if (article.PPProduits.NombreItems < article.NbItems)
                {
                    LibrairieLINQ.modifierQuantitePanier(article.NoPanier, article.PPProduits.NombreItems.ToString());
                    tbQuantite.Text = article.PPProduits.NombreItems.ToString();
                    quantiteModif = true;
                }
                LibrairieControlesDynamique.lbDYN(colQuantite, "update_" + article.NoPanier + ";" + idItem, "Mettre à jour", update_click);

            }




            // Prix item
            Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, idEntreprise + "_colPrix_" + idItem, "col-sm-2");

            LibrairieControlesDynamique.lblDYN(colPrix, "", "$" + Decimal.Round((Decimal)prixAvecQuantites, 2).ToString(), "prix_item");
            LibrairieControlesDynamique.brDYN(colPrix);
            LibrairieControlesDynamique.lblDYN(colPrix, "", "Prix unitaire: $" + Decimal.Round((Decimal)prixUnitaire, 2).ToString(), "prix_unitaire");
            LibrairieControlesDynamique.brDYN(colPrix);
            LibrairieControlesDynamique.lblDYN(colPrix, "", (montantRabais > 0) ? "Rabais de $" + Decimal.Round((Decimal)montantRabais, 2).ToString() : "", "rabais");


            // Bouton retirer
            Panel rowBtnRetirer = LibrairieControlesDynamique.divDYN(panelBody, idEntreprise + "_rowBtnRetirer_" + idItem, "row");
            Panel colBtnRetirer = LibrairieControlesDynamique.divDYN(rowBtnRetirer, idEntreprise + "_colBtnRetirer_" + idItem, "col-sm-2");
            LibrairieControlesDynamique.btnDYN(colBtnRetirer, "btnRetirer_" + article.NoPanier, "btn btn-default", "RETIRER", retirer_click);
            LibrairieControlesDynamique.hrDYN(panelBody);
        }

        // Afficher le sous total
        Panel rowSousTotal = LibrairieControlesDynamique.divDYN(panelBody, idEntreprise + "_rowSousTotal", "row");
        Panel colLabelSousTotal = LibrairieControlesDynamique.divDYN(rowSousTotal, idEntreprise + "_colLabelSousTotal", "col-sm-10 text-right");
        LibrairieControlesDynamique.lblDYN(colLabelSousTotal, idEntreprise + "_labelSousTotal", "Sous total: ", "infos-payage");

        Panel colMontantSousTotal = LibrairieControlesDynamique.divDYN(rowSousTotal, idEntreprise + "_colMontantSousTotal", "col-sm-2 text-right");
        LibrairieControlesDynamique.lblDYN(colMontantSousTotal, idEntreprise + "_montantSousTotal", "$" + Decimal.Round((Decimal)sousTotal, 2).ToString(), "infos-payage");

        LibrairieControlesDynamique.hrDYN(panelBody);

        // Afficher la TPS
        Decimal? pctTPS = LibrairieLINQ.getPourcentageTaxes("TPS") / 100;
        Decimal? TPS = sousTotal * pctTPS;

        Panel rowTPS = LibrairieControlesDynamique.divDYN(panelBody, idEntreprise + "_rowTPS", "row");
        Panel colLabelTPS = LibrairieControlesDynamique.divDYN(rowTPS, idEntreprise + "_colLabelTPS", "col-sm-10 text-right");
        LibrairieControlesDynamique.lblDYN(colLabelTPS, idEntreprise + "_labelTPS", "TPS: ", "infos-payage");

        Panel colMontantTPS = LibrairieControlesDynamique.divDYN(rowTPS, idEntreprise + "_colMontantTPS", "col-sm-2 text-right");
        LibrairieControlesDynamique.lblDYN(colMontantTPS, idEntreprise + "_montantTPS", "$" + Decimal.Round((Decimal)TPS, 2).ToString(), "infos-payage");

        LibrairieControlesDynamique.hrDYN(panelBody);

        // Afficher la TVQ (si nécessaire)
        if (lstArticlesEnPanier[0].PPVendeurs.Province == "QC")
        {
            Decimal? pctTVQ = LibrairieLINQ.getPourcentageTaxes("TVQ") / 100;
            Decimal? TVQ = sousTotal * pctTVQ;

            Panel rowTVQ = LibrairieControlesDynamique.divDYN(panelBody, idEntreprise + "_rowTVQ", "row");
            Panel colLabelTVQ = LibrairieControlesDynamique.divDYN(rowTVQ, idEntreprise + "_colLabelTVQ", "col-sm-10 text-right");
            LibrairieControlesDynamique.lblDYN(colLabelTVQ, idEntreprise + "_labelTVQ", "TVQ: ", "infos-payage");

            Panel colMontantTVQ = LibrairieControlesDynamique.divDYN(rowTVQ, idEntreprise + "_colMontantTVQ", "col-sm-2 text-right");
            LibrairieControlesDynamique.lblDYN(colMontantTVQ, idEntreprise + "_montantTVQ", "$" + Decimal.Round((Decimal)TVQ, 2).ToString(), "infos-payage");

            LibrairieControlesDynamique.hrDYN(panelBody);
        }

        // afficher message quantite modif (au besoins)
        if (quantiteModif)
        {
            Panel rowModif = LibrairieControlesDynamique.divDYN(panelBody, "", "row");
            Panel colModif = LibrairieControlesDynamique.divDYN(rowModif, "", "col-sm-12 text-right");
            LibrairieControlesDynamique.lblDYN(colModif, "", "Certaines quantités ont été modifiées dû à la quantité en stock", "modif-stock-message");

            LibrairieControlesDynamique.hrDYN(panelBody);
        }

        // afficher message rupture (au besoins)
        if (ruptureStock)
        {
            Panel rowRupture = LibrairieControlesDynamique.divDYN(panelBody, "", "row");
            Panel colRupture = LibrairieControlesDynamique.divDYN(rowRupture, "", "col-sm-12 text-right");
            LibrairieControlesDynamique.lblDYN(colRupture, "", "Veuillez retirer les articles en rupture de stock avant de pouvoir commander", "rupture-stock-message");

            LibrairieControlesDynamique.hrDYN(panelBody);
        }

        Panel rowFraisTransport = LibrairieControlesDynamique.divDYN(panelBody, "", "row");
        Panel colFraisTransport = LibrairieControlesDynamique.divDYN(rowFraisTransport, "", "col-sm-12 text-right");
        LibrairieControlesDynamique.lblDYN(colFraisTransport, "", "Des frais de transport pourraient s'appliquer", "avertissement-livraison");

        LibrairieControlesDynamique.hrDYN(panelBody);

        // Bouton shipping
        Panel rowBtnShipping = LibrairieControlesDynamique.divDYN(panelBody, idEntreprise + "_rowBtnShipping", "row");
        Panel colLabelBtnShipping = LibrairieControlesDynamique.divDYN(rowBtnShipping, idEntreprise + "_colLabelBtnShipping", "col-sm-2");
        HtmlButton btnShipping = LibrairieControlesDynamique.htmlbtnDYN(colLabelBtnShipping, idEntreprise + "_BtnShipping", "btn btn-warning", "Informations de livraison", "glyphicon glyphicon-user", infosPerso_click);

    }

    public void afficherInfosPerso()
    {
        // ajuster la bar de progression
        progressBar.Style.Add("width", "50%");

        // masquer div livraison
        divLiv.Visible = false;

        // masquer la div de paiement
        div_paiement.Visible = false;

        // rendre visible la division
        divInfosPerso.Visible = true;

    }

    public void afficherLivraison()
    {
        // ajuster la bar de progression
        progressBar.Style.Add("width", "75%");

        // masquer div paiement
        div_paiement.Visible = false;

        // masquer div infos perso
        divInfosPerso.Visible = false;

        // calculer le poids total
        Double poidsTotal = 4;
        Double prixLivraisonStandard = getPrixLivraisonSelonPoids(poidsTotal, 1);

        // mettre à jour les label de prix et poids
        poidsTotalCommande.Text = poidsTotal.ToString() + " lbs";

        if (prixLivraisonStandard == 0)
        {
            fraisTransport.Text = "GRATUIT";
        }
        else
        {
            fraisTransport.Text = "$" + prixLivraisonStandard.ToString();
        }

        // rendre visible la div livraison
        divLiv.Visible = true;
    }

    public void afficherPaiement()
    {
        // ajuster la bar de progression
        progressBar.Style.Add("width", "100%");

        // masquer la div d'infos perso
        divInfosPerso.Visible = false;

        // masquer la div de livraison
        divLiv.Visible = false;

        // afficher le sous total
        Double sousTotal = 3902.97;

        // calculer le prix de la livraison
        Double poidsTotal = 4;
        int type = 0;

        if (rbRegulier.Checked == true) { type = 1; }
        else if (rbPrioritaire.Checked == true) { type = 2; }
        else if (rbCompagnie.Checked == true) { type = 3; }

        Double prixLivraison = getPrixLivraisonSelonPoids(poidsTotal, type);

        // calculer TPS et TVQ
        Double TPSPourcentage = 0.05;
        Double TVQPourcentage = 0.0975;
        Double TPS = Math.Round((sousTotal + prixLivraison) * TPSPourcentage, 2);
        Double TVQ = Math.Round((sousTotal + prixLivraison) * TVQPourcentage, 2);

        // Calculer le coût total
        Double coutTotal = sousTotal + prixLivraison + TPS + TVQ;

        // afficher les données
        sousTotal_paiement.Text = "$" + sousTotal.ToString();
        fraisLivraison_paiement.Text = (prixLivraison == 0)?"GRATUIT":"$" + prixLivraison.ToString();
        TPS_paiement.Text = "$" + TPS.ToString();
        TVQ_paiement.Text = "$" + TVQ.ToString();
        total_paiement.Text = "$" + coutTotal.ToString();

        // afficher la div
        div_paiement.Visible = true;
    }

    public void afficherReponseLESi()
    {
        Boolean reussi = false;

        // rendre visible la bonne div
        if (reussi)
        {
            String url = "~/Pages/SaisieCommande.aspx?IDEntreprise=" + this.idEntreprise + "&Etape=bon";
            Response.Redirect(url, true);
        }
        else
        {
            LESi_echoue.Visible = true;
            afficherPaiement();
        }
    }

    public void afficherBon()
    {
        LESi_reussi.Visible = true;
    }

    public Double getPrixLivraisonSelonPoids(Double poidsCommande, int typeLivraison)
    {
        // type livraison
        // 1 = standard
        // 2 = prioritaire
        // 3 = compagnie
        Double prixLivraison = 0;

        // calculer le prix pour livraison régulière
        if (poidsCommande < 5)
        {
            switch (typeLivraison)
            {
                case 1: prixLivraison = 0; break;
                case 2: prixLivraison = 5; break;
                case 3: prixLivraison = 10; break;
            }
        }
        else if (poidsCommande >= 5 && poidsCommande < 20)
        {
            switch (typeLivraison)
            {
                case 1: prixLivraison = 5; break;
                case 2: prixLivraison = 10; break;
                case 3: prixLivraison = 15; break;
            }
        }
        else if (poidsCommande >= 20 && poidsCommande < 100)
        {
            switch (typeLivraison)
            {
                case 1: prixLivraison = 10; break;
                case 2: prixLivraison = 15; break;
                case 3: prixLivraison = 20; break;
            }
        }
        else if (poidsCommande >= 100)
        {
            switch (typeLivraison)
            {
                case 1: prixLivraison = 20; break;
                case 2: prixLivraison = 25; break;
                case 3: prixLivraison = 30; break;
            }
        }

        return prixLivraison;
    }

    public void retirer_click(Object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        String panierID = btn.ID.Replace("btnRetirer_", "");

        LibrairieLINQ.retirerArticlePanier(long.Parse(panierID));

        String url = "~/Pages/SaisieCommande.aspx?IDEntreprise=" + this.idEntreprise;
        Response.Redirect(url, true);
    }

    public void update_click(Object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        String[] tabIDs = btn.ID.Replace("update_", "").Split(';');
        String panierID = tabIDs[0];
        String itemID = tabIDs[1];

        TextBox tb = (TextBox)paniersDynamique.FindControl("quantite_" + itemID);

        LibrairieLINQ.modifierQuantitePanier(long.Parse(panierID), tb.Text);
        String url = "~/Pages/SaisieCommande.aspx?IDEntreprise=" + this.idEntreprise + "#contentBody_quantite_" + itemID;
        Response.Redirect(url, true);
    }

    public void infosPerso_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/SaisieCommande.aspx?IDEntreprise=" + this.idEntreprise + "&Etape=livraison";
        Response.Redirect(url, true);
    }

    public void livraison_click(Object sender, EventArgs e)
    {
        afficherLivraison();
    }

    public void retourPanier_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Retour");
        String url = "~/Pages/SaisieCommande.aspx?IDEntreprise=" + this.idEntreprise;
        Response.Redirect(url, true);
    }

    public void retourInfosPerso_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Retour");
        afficherInfosPerso();
    }

    public void retourLivraison_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Retour");
        LESi_echoue.Visible = false;
        afficherLivraison();
    }

    public void paiementLESi_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Paiement LESi");
        afficherReponseLESi();
    }

    public void paiementSecurise_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Paiement");
        afficherPaiement();
    }

    public void visualiserBon_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Visualiser le bon");
    }

    public void imprimerBon_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Imprimer le bon");
    }

    public void livraison_changed(Object sender, EventArgs e)
    {
        // calculer le nouveau coût de livraison
        Double poidsCommande = 4;
        Double fraisLivraison = 0;

        if (rbRegulier.Checked == true)
        {
            fraisLivraison = getPrixLivraisonSelonPoids(poidsCommande, 1);
        }
        else if (rbPrioritaire.Checked == true)
        {
            fraisLivraison = getPrixLivraisonSelonPoids(poidsCommande, 2);
        }
        else if (rbCompagnie.Checked == true)
        {
            fraisLivraison = getPrixLivraisonSelonPoids(poidsCommande, 3);
        }

        if (fraisLivraison == 0)
        {
            fraisTransport.Text = "GRATUIT";
        }
        else
        {
            fraisTransport.Text = "$" + fraisLivraison.ToString();
        }

        // masquer les autres div
        divInfosPerso.Visible = false;
        div_paiement.Visible = false;

        // afficher la div livraison
        divLiv.Visible = true;

        // modifier panier
        progressBar.Style.Add("width", "75%");
    }

    private void getIdEntreprise()
    {
        int n;
        if (Request.QueryString["IDEntreprise"] == null || !int.TryParse(Request.QueryString["IDEntreprise"], out n))
        {
            String url = "~/Pages/GestionPanierCommande.aspx?";
            Response.Redirect(url, true);
        }
        else
        {
            this.idEntreprise = n;
        }
    }

    private void getEtapeCommande()
    {
        if (Request.QueryString["Etape"] == null)
        {
            this.etape = "panier";
        }
        else
        {
            this.etape = Request.QueryString["Etape"];
        }
    }

    public void nomEntreprisePanier_click(Object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        String idVendeur = btn.ID.Replace("vendeur_", "");
        System.Diagnostics.Debug.WriteLine(idVendeur);
    }

}