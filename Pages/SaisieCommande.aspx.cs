﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IronPdf;

public partial class Pages_SaisieCommande : System.Web.UI.Page
{
    private static BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
    private long idEntreprise;
    private long numCommande;
    private String etape;
    private short typeLivraison;
    private int erreur;

    protected void Page_Load(object sender, EventArgs e)
    {
        verifierPermissions("C");

        Page.Title = "Passer commande";

        Page.MaintainScrollPositionOnPostBack = true;

        // Aller chercher les valeurs en GET
        String url = "";

        bool redIdEntreprise = getIdEntreprise();
        getEtapeCommande();
        bool redTypeLiv = getTypeLiv();
        getErreur();

        if (redIdEntreprise)
        {
            url = "~/Pages/GestionPanierCommande.aspx?";
        }
        else if (redTypeLiv)
        {
            url = "~/Pages/SaisieCommande.aspx?IDEntreprise=" + this.idEntreprise + "&Etape=livraison";
            Session["EtapePaiement"] = "livraison";
        }

        if (url != "") { Response.Redirect(url, true); }

        switch (etape)
        {
            case "panier": afficherPanier(); break;
            case "perso": afficherInfosPerso(); break;
            case "livraison": afficherLivraison(); break;
            case "paiement": afficherPaiement(); break;
            case "confirmation": afficherReponseLESi(); break;
            case "bon": afficherBon(); getNumCommande(); break;
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

        if (lstArticlesEnPanier.Count != 0)
        {
            bool ruptureStock = false;
            bool quantiteModif = false;

            // do something with entry.Value or entry.Key
            long? idEntreprise = lstArticlesEnPanier[0].NoVendeur;
            String nomEntreprise = lstArticlesEnPanier[0].PPVendeurs.NomAffaires;
            String nomVendeur = lstArticlesEnPanier[0].PPVendeurs.Prenom + " " + lstArticlesEnPanier[0].PPVendeurs.Nom;
            decimal? sousTotal = 0;
            decimal poidsTotal = 0;

            // Créer le panier du vendeur X
            Panel panelBase = LibrairieControlesDynamique.divDYN(paniersDynamique, "", "panel panel-default");

            // Nom de l'entreprise
            Panel panelHeader = LibrairieControlesDynamique.divDYN(panelBase, "", "panel-heading");
            LibrairieControlesDynamique.lbDYN(panelHeader, "vendeur_" + idEntreprise, nomEntreprise + " (" + nomVendeur + ")", "nom-entreprise", nomEntreprisePanier_click);

            // Liste des items + le total
            Panel panelBody = LibrairieControlesDynamique.divDYN(panelBase, "", "panel-body");


            // Rajouter les produits dans le panier

            foreach (PPArticlesEnPanier article in lstArticlesEnPanier)
            {
                long? idItem = article.NoProduit;
                short? quantiteSelectionne = article.NbItems;
                decimal? prixUnitaire = article.PPProduits.PrixDemande;

                decimal? prixAvecQuantites = article.PPProduits.PrixDemande * article.NbItems;
                decimal? prixAvecQuantitesAvecRabais = article.PPProduits.PrixVente * article.NbItems;
                decimal? montantRabais = (article.PPProduits.PrixDemande - article.PPProduits.PrixVente) * quantiteSelectionne;

                if (article.PPProduits.PrixVente == null || article.PPProduits.DateVente < DateTime.Now) { montantRabais = 0; prixAvecQuantitesAvecRabais = prixAvecQuantites; }

                decimal? poids = article.PPProduits.Poids;
                poidsTotal += (Decimal)(poids * quantiteSelectionne);

                // sum au sous total
                sousTotal += prixAvecQuantitesAvecRabais;

                String nomProduit = article.PPProduits.Nom;
                String urlImage = "../static/images/" + article.PPProduits.Photo;

                Panel rowItem = LibrairieControlesDynamique.divDYN(panelBody, "", "row");

                // ajouter l'image
                Panel colImg = LibrairieControlesDynamique.divDYN(rowItem, "", "col-sm-2");
                LibrairieControlesDynamique.imgDYN(colImg, "", urlImage, "img-size");

                // Nom du produit
                Panel colNom = LibrairieControlesDynamique.divDYN(rowItem, "", "col-sm-4");
                LibrairieControlesDynamique.lblDYN(colNom, "", nomProduit, "nom-item deux-lignes");
                LibrairieControlesDynamique.brDYN(colNom);
                LibrairieControlesDynamique.lblDYN(colNom, "", "Poids: " + poids + " lbs", "prix_unitaire");

                // Quantité sélectionné
                Panel colQuantite = LibrairieControlesDynamique.divDYN(rowItem, "", "col-sm-4");

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
                Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, "", "col-sm-2");

                if (montantRabais > 0)
                {
                    LibrairieControlesDynamique.lblDYN(colPrix, "", Decimal.Round((Decimal)prixAvecQuantites, 2).ToString("C", CultureInfo.CurrentCulture), "prix-item-avant-rabais");
                    LibrairieControlesDynamique.brDYN(colPrix);
                }

                LibrairieControlesDynamique.lblDYN(colPrix, "", Decimal.Round((Decimal)prixAvecQuantitesAvecRabais, 2).ToString("C", CultureInfo.CurrentCulture), "prix_item");
                LibrairieControlesDynamique.brDYN(colPrix);
                LibrairieControlesDynamique.lblDYN(colPrix, "", (montantRabais > 0) ? "Rabais de " + Decimal.Round((Decimal)montantRabais, 2).ToString("C", CultureInfo.CurrentCulture) : "", "rabais");


                // Bouton retirer
                Panel rowBtnRetirer = LibrairieControlesDynamique.divDYN(panelBody, "", "row");
                Panel colBtnRetirer = LibrairieControlesDynamique.divDYN(rowBtnRetirer, "", "col-sm-2");
                LibrairieControlesDynamique.btnDYN(colBtnRetirer, "btnRetirer_" + article.NoPanier, "btn btn-default", "RETIRER", retirer_click);
                LibrairieControlesDynamique.hrDYN(panelBody);
            }

            // Afficher le sous total
            Panel rowSousTotal = LibrairieControlesDynamique.divDYN(panelBody, "", "row");
            Panel colLabelSousTotal = LibrairieControlesDynamique.divDYN(rowSousTotal, "", "col-sm-10 text-right");
            LibrairieControlesDynamique.lblDYN(colLabelSousTotal, "", "Sous total: ", "infos-payage");

            Panel colMontantSousTotal = LibrairieControlesDynamique.divDYN(rowSousTotal, "", "col-sm-2 text-right");
            LibrairieControlesDynamique.lblDYN(colMontantSousTotal, "", Decimal.Round((Decimal)sousTotal, 2).ToString("C", CultureInfo.CurrentCulture), "infos-payage");

            LibrairieControlesDynamique.hrDYN(panelBody);

            // Afficher la TPS
            Decimal? pctTPS = LibrairieLINQ.getPourcentageTaxes("TPS", this.idEntreprise) / 100;
            Decimal? TPS = sousTotal * pctTPS;

            // Afficher la TVQ
            Decimal? pctTVQ = LibrairieLINQ.getPourcentageTaxes("TVQ", this.idEntreprise) / 100;
            Decimal? TVQ = sousTotal * pctTVQ;

            if (pctTPS != 0)
            {
                Panel rowTPS = LibrairieControlesDynamique.divDYN(panelBody, "", "row");
                Panel colLabelTPS = LibrairieControlesDynamique.divDYN(rowTPS, "", "col-sm-10 text-right");
                LibrairieControlesDynamique.lblDYN(colLabelTPS, "", "TPS: ", "infos-payage");

                Panel colMontantTPS = LibrairieControlesDynamique.divDYN(rowTPS, "", "col-sm-2 text-right");
                LibrairieControlesDynamique.lblDYN(colMontantTPS, "", Decimal.Round((Decimal)TPS, 2).ToString("C", CultureInfo.CurrentCulture), "infos-payage");

                LibrairieControlesDynamique.hrDYN(panelBody);
            }
            

            // Afficher la TVQ (si nécessaire)
            if (pctTVQ != 0)
            {

                Panel rowTVQ = LibrairieControlesDynamique.divDYN(panelBody, "", "row");
                Panel colLabelTVQ = LibrairieControlesDynamique.divDYN(rowTVQ, "", "col-sm-10 text-right");
                LibrairieControlesDynamique.lblDYN(colLabelTVQ, "", "TVQ: ", "infos-payage");

                Panel colMontantTVQ = LibrairieControlesDynamique.divDYN(rowTVQ, "", "col-sm-2 text-right");
                LibrairieControlesDynamique.lblDYN(colMontantTVQ, "", Decimal.Round((Decimal)TVQ, 2).ToString("C", CultureInfo.CurrentCulture), "infos-payage");

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

            // vérifier limite de poids
            bool limiteDePoidsDepasse = LibrairieLINQ.depassePoidsMax(this.idEntreprise, poidsTotal);
            if (limiteDePoidsDepasse)
            {
                Panel rowPoidsDepasse = LibrairieControlesDynamique.divDYN(panelBody, "", "row");
                Panel colPoidsDepasse = LibrairieControlesDynamique.divDYN(rowPoidsDepasse, "", "col-sm-12 text-right");
                LibrairieControlesDynamique.lblDYN(colPoidsDepasse, "", "Le poids de la commande dépasse la limite établie par le vendeur (" + LibrairieLINQ.poidsMaximumLivraisonCompganie(this.idEntreprise) + " lbs)", "rupture-stock-message");

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
            Panel rowBtnShipping = LibrairieControlesDynamique.divDYN(panelBody, "", "row");
            Panel colLabelBtnShipping = LibrairieControlesDynamique.divDYN(rowBtnShipping, "", "col-sm-2");

            System.Diagnostics.Debug.WriteLine("Rupture stock: " + ruptureStock);
            System.Diagnostics.Debug.WriteLine("Limite depasse: " + ruptureStock);
            if (!ruptureStock && !limiteDePoidsDepasse)
            {
                HtmlButton btnShipping = LibrairieControlesDynamique.htmlbtnDYN(colLabelBtnShipping, "", "btn btn-warning", "Informations de livraison", "glyphicon glyphicon-user", infosPerso_click);
            }
        }
        else
        {
            String url = "~/Pages/AccueilClient.aspx?";
            Response.Redirect(url, true);
        }

    }

    public bool afficherResumePanier()
    {
        bool valide = true;
        List<PPArticlesEnPanier> lstArticlesEnPanier = LibrairieLINQ.getProduitsVendeurSpecifiqueClient(long.Parse(Session["NoClient"].ToString()), this.idEntreprise);

        if (lstArticlesEnPanier.Count != 0)
        {
            bool ruptureStock = false;
            bool quantiteModif = false;

            // do something with entry.Value or entry.Key
            long? idEntreprise = lstArticlesEnPanier[0].NoVendeur;
            String nomEntreprise = lstArticlesEnPanier[0].PPVendeurs.NomAffaires;
            String nomVendeur = lstArticlesEnPanier[0].PPVendeurs.Prenom + " " + lstArticlesEnPanier[0].PPVendeurs.Nom;
            decimal? sousTotal = 0;
            decimal poidsTotal = 0;

            // Créer le panier du vendeur X
            Panel panelBase = LibrairieControlesDynamique.divDYN(paniersDynamique, "", "panel panel-default");

            // Nom de l'entreprise
            Panel panelHeader = LibrairieControlesDynamique.divDYN(panelBase, "", "panel-heading");
            Label lblVendeur = LibrairieControlesDynamique.lblDYN(panelHeader, "lblNomEntreprise", nomEntreprise, "nom-entreprise");

            // Liste des items + le total
            Panel panelBody = LibrairieControlesDynamique.divDYN(panelBase, "", "panel-body");


            // Rajouter les produits dans le panier

            foreach (PPArticlesEnPanier article in lstArticlesEnPanier)
            {
                long? idItem = article.NoProduit;
                short? quantiteSelectionne = article.NbItems;

                decimal? prixAvecQuantites = article.PPProduits.PrixDemande * article.NbItems;
                decimal? prixAvecQuantitesAvecRabais = article.PPProduits.PrixVente * article.NbItems;
                decimal? montantRabais = article.PPProduits.PrixDemande - article.PPProduits.PrixVente;

                if (article.PPProduits.PrixVente == null || article.PPProduits.DateVente < DateTime.Now) { montantRabais = 0; prixAvecQuantitesAvecRabais = prixAvecQuantites; }

                decimal? poids = article.PPProduits.Poids;
                poidsTotal += (Decimal)(poids * quantiteSelectionne);

                // sum au sous total
                sousTotal += prixAvecQuantites;

                String nomProduit = article.PPProduits.Nom;
                String urlImage = "../static/images/" + article.PPProduits.Photo;

                Panel rowItem = LibrairieControlesDynamique.divDYN(panelBody, "", "row");

                // ajouter l'image
                Panel colImg = LibrairieControlesDynamique.divDYN(rowItem, "", "col-sm-2");
                LibrairieControlesDynamique.imgDYN(colImg, "", urlImage, "img-size");

                // Nom du produit
                Panel colNom = LibrairieControlesDynamique.divDYN(rowItem, "", "col-sm-4");
                LibrairieControlesDynamique.lblDYN(colNom, "", nomProduit, "nom-item deux-lignes");
                LibrairieControlesDynamique.brDYN(colNom);
                LibrairieControlesDynamique.lblDYN(colNom, "", "Poids: " + poids + " lbs", "prix_unitaire");

                // Quantité sélectionné
                Panel colQuantite = LibrairieControlesDynamique.divDYN(rowItem, "", "col-sm-4");

                TextBox tbQuantite = LibrairieControlesDynamique.numericUpDownDYN(colQuantite, "",
                    quantiteSelectionne.ToString(), (article.PPProduits.NombreItems < 1) ? "1" : article.PPProduits.NombreItems.ToString(), "form-control border-quantite");
                tbQuantite.ReadOnly = true;

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
                }




                // Prix item
                Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, "", "col-sm-2");

                if (montantRabais > 0)
                {
                    LibrairieControlesDynamique.lblDYN(colPrix, "", Decimal.Round((Decimal)prixAvecQuantites, 2).ToString("C", CultureInfo.CurrentCulture), "prix-item-avant-rabais");
                    LibrairieControlesDynamique.brDYN(colPrix);
                }
                
                LibrairieControlesDynamique.lblDYN(colPrix, "", Decimal.Round((Decimal)prixAvecQuantitesAvecRabais, 2).ToString("C", CultureInfo.CurrentCulture), "prix_item");
                LibrairieControlesDynamique.brDYN(colPrix);
                LibrairieControlesDynamique.lblDYN(colPrix, "", (montantRabais > 0) ? "Rabais de " + Decimal.Round((Decimal)montantRabais, 2).ToString("C", CultureInfo.CurrentCulture) : "", "rabais");

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

            // vérifier limite de poids
            bool limiteDePoidsDepasse = LibrairieLINQ.depassePoidsMax(this.idEntreprise, poidsTotal);
            if (limiteDePoidsDepasse)
            {
                Panel rowPoidsDepasse = LibrairieControlesDynamique.divDYN(panelBody, "", "row");
                Panel colPoidsDepasse = LibrairieControlesDynamique.divDYN(rowPoidsDepasse, "", "col-sm-12 text-right");
                LibrairieControlesDynamique.lblDYN(colPoidsDepasse, "", "Le poids de la commande dépasse la limite établie par le vendeur (" + LibrairieLINQ.poidsMaximumLivraisonCompganie(this.idEntreprise) + " lbs)", "rupture-stock-message");

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

            if (ruptureStock || limiteDePoidsDepasse) { valide = false; }
        }
        else
        {
            String url = "~/Pages/AccueilClient.aspx?";
            Response.Redirect(url, true);
        }
        return valide;

    }

    public void afficherInfosPerso()
    {
        // ajuster la bar de progression
        progressBar.Style.Add("width", "50%");

        // remplir les infos
        if (!Page.IsPostBack)
        {
            remplirTbInfosClient();
        }

        // rendre visible la division
        divInfosPerso.Visible = true;

    }

    public void afficherLivraison()
    {

        // afficher resumé panier
        btnPaiementSecurise.Visible = afficherResumePanier();

        // ajuster la bar de progression
        progressBar.Style.Add("width", "75%");

        // poids total
        Decimal poidsTotal = LibrairieLINQ.getPoidsTotalPanierClient(long.Parse(Session["NoClient"].ToString()), this.idEntreprise);

        // calculer le prix pour livraison
        Decimal prixLivraisonStandard = Decimal.Round(getPrixLivraisonSelonPoids(poidsTotal, this.idEntreprise), 2);

        // mettre à jour les label de prix et poids
        poidsTotalCommande.Text = poidsTotal.ToString() + " lbs";

        if (prixLivraisonStandard == 0)
        {
            fraisTransport.Text = "GRATUIT";
        }
        else
        {
            fraisTransport.Text = prixLivraisonStandard.ToString("C", CultureInfo.CurrentCulture);
        }

        // get sous total avec rabais
        Decimal sousTotal = Decimal.Round(LibrairieLINQ.getSousTotalPanierClient(long.Parse(Session["NoClient"].ToString()), this.idEntreprise), 2);

        // calculer TPS et TVQ
        Decimal TPSPourcentage = (Decimal)LibrairieLINQ.getPourcentageTaxes("TPS", this.idEntreprise) / 100;
        Decimal TVQPourcentage = (Decimal)LibrairieLINQ.getPourcentageTaxes("TVQ", this.idEntreprise) / 100;
        Decimal TPS = Math.Round((sousTotal + prixLivraisonStandard) * TPSPourcentage, 2);
        Decimal TVQ = Math.Round((sousTotal + prixLivraisonStandard) * TVQPourcentage, 2);

        // afficher les autres infos
        lblSousTotalLiv.Text = sousTotal.ToString("C", CultureInfo.CurrentCulture);
        if (TPSPourcentage != 0) { lblTPSLiv.Text = TPS.ToString("C", CultureInfo.CurrentCulture); }
        else { divTPSLiv.Visible = false; }

        if (TVQPourcentage != 0) { lblTVQLiv.Text = TVQ.ToString("C", CultureInfo.CurrentCulture); }
        else { divTVQLiv.Visible = false; }

        lblTotalLiv.Text = (sousTotal + TPS + TVQ + prixLivraisonStandard).ToString("C", CultureInfo.CurrentCulture);

        // rendre visible la div livraison
        divLiv.Visible = true;
    }

    public void afficherPaiement()
    {
        // afficher si erreur != -1
        if (this.erreur != -1)
        {
            String message = "";

            if (this.erreur == 0) { message = "Transaction annulée par l'utilisateur"; }
            else if (this.erreur == 1) { message = "Transaction refusée : Date d'expiration dépassée"; }
            else if (this.erreur == 2) { message = "Transaction refusée : Limite de crédit atteinte"; }
            else if (this.erreur == 3) { message = "Transaction refusée : Carte refusée (tout autre motif)"; }
            else if (this.erreur == 9999) { message = "ERREUR DE VALIDATION"; }

            LESi_echoue.Visible = true;
            corpsMessageErreur.Text = message;
        }

        // ajuster la bar de progression
        progressBar.Style.Add("width", "100%");

        // poids total
        Decimal poidsTotal = LibrairieLINQ.getPoidsTotalPanierClient(long.Parse(Session["NoClient"].ToString()), this.idEntreprise);

        // calculer le prix pour livraison
        Decimal prixLivraisonStandard = Decimal.Round(getPrixLivraisonSelonPoids(poidsTotal, this.idEntreprise), 2);

        // mettre à jour les label de prix et poids
        lblPoidsPaiement.Text = poidsTotal.ToString() + " lbs";

        if (prixLivraisonStandard == 0)
        {
            lblFraisTransportPaiement.Text = "GRATUIT";
        }
        else
        {
            lblFraisTransportPaiement.Text = prixLivraisonStandard.ToString("C", CultureInfo.CurrentCulture);
        }

        // get sous total avec rabais
        Decimal sousTotal = Decimal.Round(LibrairieLINQ.getSousTotalPanierClient(long.Parse(Session["NoClient"].ToString()), this.idEntreprise), 2);

        // calculer TPS et TVQ
        Decimal TPSPourcentage = (Decimal)LibrairieLINQ.getPourcentageTaxes("TPS", this.idEntreprise) / 100;
        Decimal TVQPourcentage = (Decimal)LibrairieLINQ.getPourcentageTaxes("TVQ", this.idEntreprise) / 100;
        Decimal TPS = Math.Round((sousTotal + prixLivraisonStandard) * TPSPourcentage, 2);
        Decimal TVQ = Math.Round((sousTotal + prixLivraisonStandard) * TVQPourcentage, 2);

        // afficher les autres infos
        lblSousTotalPaiement.Text = sousTotal.ToString("C", CultureInfo.CurrentCulture);

        if (TPSPourcentage != 0) { lblTPSPaiement.Text = TPS.ToString("C", CultureInfo.CurrentCulture); }
        else { divTPSPaiement.Visible = false; }

        if (TVQPourcentage != 0) { lblTVQPaiement.Text = TVQ.ToString("C", CultureInfo.CurrentCulture); }
        else { divTVQPaiement.Visible = false; }

        lblTotalPaiement.Text = (sousTotal + TPS + TVQ + prixLivraisonStandard).ToString("C", CultureInfo.CurrentCulture);

        // afficher resume paiement
        //btnLESi.Visible = afficherResumePanier();
        afficherResumePanier();

        // afficher la div
        div_paiement.Visible = true;
    }

    public void afficherReponseLESi()
    {
        // vérifier que les quantités sont guud
        bool depassePoids = LibrairieLINQ.depassePoidsMax(this.idEntreprise, LibrairieLINQ.getPoidsTotalPanierClient(long.Parse(Session["NoClient"].ToString()), this.idEntreprise));
        bool rupture = LibrairieLINQ.ruptureDeStockPanierClient(long.Parse(Session["NoClient"].ToString()), this.idEntreprise);

        if (!depassePoids && !rupture)
        {
            // get le no autorisation
            int noAutorisation = 0;

            int n;
            if (Request.Form["NoAutorisation"] == null || !int.TryParse(Request.Form["NoAutorisation"], out n))
            {
                noAutorisation = 3;
            }
            else { noAutorisation = n; }

            // get date autorisation
            string date = "";

            if (Request.Form["DateAutorisation"] == null)
            {
                date = DateTime.Now.ToString();
            }
            else { date = Request.Form["DateAutorisation"]; }

            // get le no autorisation
            Decimal frais = 0;

            Decimal nd;
            if (Request.Form["FraisMarchand"] == null || !Decimal.TryParse(Request.Form["FraisMarchand"], out nd))
            {
                frais = 3;
            }
            else { frais = nd; }


            // rendre visible la bonne div
            if (noAutorisation >= 1000 && noAutorisation <= 5000 && noAutorisation != 1234)
            {
                // rajouter les infos de la commande dans la base de données
                long noCommande = creerCommande(date, frais);

                String url = "~/Pages/SaisieCommande.aspx?IDEntreprise=" + this.idEntreprise + "&Etape=bon&Commande=" + noCommande;
                Session["EtapePaiement"] = "bon";
                Response.Redirect(url, true);
            }
            else
            {
                String url = "~/Pages/SaisieCommande.aspx?IDEntreprise=" + this.idEntreprise + "&Etape=paiement" + "&TypeLivraison=" + this.typeLivraison + "&Erreur=" + noAutorisation;
                Session["EtapePaiement"] = "paiement";
                Response.Redirect(url, true);
            }
        }
        else
        {
            String url = "~/Pages/SaisieCommande.aspx?IDEntreprise=" + this.idEntreprise + "&Etape=paiement" + "&TypeLivraison=" + this.typeLivraison;
            Session["EtapePaiement"] = "paiement";
            Response.Redirect(url, true);
        }
    }

    public long creerCommande(String date, Decimal fraisLESI)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableCommandes = dataContext.PPCommandes;
        var tableDetailsCommande = dataContext.PPDetailsCommandes;
        var tableHistoriqueCommande = dataContext.PPHistoriquePaiements;
        var tableArticlesPanier = dataContext.PPArticlesEnPanier;
        var tableDetailsCommandes = dataContext.PPDetailsCommandes;

        var dernierHistorique = from h in tableHistoriqueCommande orderby h.NoHistorique descending select h;
        var dernierDetailCommande = from dc in tableDetailsCommande orderby dc.NoDetailCommandes descending select dc;
        var derniereCommande = from c in tableCommandes orderby c.NoCommande descending select c;
        var articlesPanier = LibrairieLINQ.getProduitsVendeurSpecifiqueClient(long.Parse(Session["NoClient"].ToString()), this.idEntreprise);

        // poids total
        Decimal poidsTotal = LibrairieLINQ.getPoidsTotalPanierClient(long.Parse(Session["NoClient"].ToString()), this.idEntreprise);

        // calculer le prix pour livraison
        Decimal prixLivraison = Decimal.Round(getPrixLivraisonSelonPoids(poidsTotal, this.idEntreprise), 2);

        // get sous total avec rabais
        Decimal sousTotal = Decimal.Round(LibrairieLINQ.getSousTotalPanierClient(long.Parse(Session["NoClient"].ToString()), this.idEntreprise), 2);

        // calculer TPS et TVQ
        Decimal TPSPourcentage = (Decimal)LibrairieLINQ.getPourcentageTaxes("TPS", this.idEntreprise) / 100;
        Decimal TVQPourcentage = (Decimal)LibrairieLINQ.getPourcentageTaxes("TVQ", this.idEntreprise) / 100;
        Decimal TPS = Math.Round((sousTotal + prixLivraison) * TPSPourcentage, 2);
        Decimal TVQ = Math.Round((sousTotal + prixLivraison) * TVQPourcentage, 2);


        // code type livraison
        short noTypeLivraison = this.typeLivraison;
        


        // calculer un id de commande unique
        long biggestCommandeID = 1;

        if (derniereCommande.Count() > 0)
        {
            biggestCommandeID = derniereCommande.First().NoCommande + 1;
        }
        this.numCommande = biggestCommandeID;

        // calculer un no d'autorisation unique
        long biggestNoAutorisation = 1000;
        if (derniereCommande.Count() > 0)
        {
            biggestNoAutorisation = long.Parse(derniereCommande.First().NoAutorisation) + 1;
        }

        // calculer un id de détail de commande unique
        long biggestNoDetailCommande = 1;
        if (dernierDetailCommande.Count() > 0)
        {
            biggestNoDetailCommande = dernierDetailCommande.First().NoDetailCommandes + 1;
        }

        // calculer un id d'historique unique
        long biggestHistoriqueID = 1;
        if (dernierHistorique.Count() > 0)
        {
            biggestHistoriqueID = dernierHistorique.First().NoHistorique + 1;
        }

        // calculer redevance vendeur
        Decimal? pourcentageRedevance = LibrairieLINQ.getRedevanceVendeur(this.idEntreprise);
        Decimal redevanceArgent = (Decimal)((pourcentageRedevance != null) ? sousTotal * (pourcentageRedevance/100) : 0);

        using (var dbTransaction = dataContext.Database.BeginTransaction())
        {
            try
            {
                // Faire la liste des produits
                foreach (var article in articlesPanier)
                {
                    PPDetailsCommandes detailCommande = new PPDetailsCommandes
                    {
                        NoDetailCommandes = biggestNoDetailCommande,
                        NoCommande = biggestCommandeID,
                        NoProduit = article.NoProduit,
                        PrixVente = article.PPProduits.PrixVente,
                        Quantité = article.NbItems
                    };
                    dataContext.PPDetailsCommandes.Add(detailCommande);
                    biggestNoDetailCommande++;
                }


                // PPCommandes
                PPCommandes commande = new PPCommandes
                {
                    NoCommande = biggestCommandeID,
                    NoClient = long.Parse(Session["NoClient"].ToString()),
                    NoVendeur = this.idEntreprise,
                    DateCommande = DateTime.Now,
                    CoutLivraison = prixLivraison,
                    TypeLivraison = noTypeLivraison,
                    MontantTotAvantTaxes = sousTotal,
                    TPS = TPS,
                    TVQ = TVQ,
                    PoidsTotal = poidsTotal,
                    Statut = "0",
                    NoAutorisation = biggestNoAutorisation.ToString()
                };

                dataContext.PPCommandes.Add(commande);

                // creer la table PPHistorique
                //PPHistoriquePaiements historique 
                PPHistoriquePaiements historique = new PPHistoriquePaiements
                {
                    NoHistorique = biggestHistoriqueID,
                    MontantVenteAvantLivraison = sousTotal,
                    NoVendeur = this.idEntreprise,
                    NoClient = long.Parse(Session["NoClient"].ToString()),
                    NoCommande = biggestCommandeID,
                    DateVente = Convert.ToDateTime(date),
                    NoAutorisation = biggestNoAutorisation.ToString(),
                    FraisLesi = fraisLESI,
                    Redevance = redevanceArgent,
                    FraisLivraison = prixLivraison,
                    FraisTPS = TPS,
                    FraisTVQ = TVQ
                };

                dataContext.PPHistoriquePaiements.Add(historique);

                // delete les paniers du client
                long noClient = long.Parse(Session["NoClient"].ToString());
                var tousLesArticlesClient = from ap in tableArticlesPanier
                                            where ap.NoClient == noClient && ap.NoVendeur == idEntreprise
                                            select ap;

                foreach (var article in tousLesArticlesClient)
                {
                    dataContext.PPArticlesEnPanier.Remove(article);
                }

                dataContext.SaveChanges();
                dbTransaction.Commit();

                // creer PDF
                String fluxHTML = "";
                String urlPDF = "~/static/pdf/" + biggestCommandeID + ".pdf";

                // créer le flux de données HTML
                PPClients clientPDF = LibrairieLINQ.getFicheInformationsClient(noClient);
                PPVendeurs vendeurPDF = LibrairieLINQ.getInfosVendeur(this.idEntreprise);
                PPCommandes commandePDF = LibrairieLINQ.getInfosCommande(biggestCommandeID);



                fluxHTML += genererTableFlux(clientPDF, vendeurPDF, commandePDF);

                creerCommandePDF(fluxHTML, urlPDF);
                return biggestCommandeID;
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
            }
        }
        return -1;
    }

    public void creerCommandePDF(String fluxHTML, String urlPDF)
    {
        IronPdf.HtmlToPdf Renderer = new IronPdf.HtmlToPdf();
        Renderer.PrintOptions.CustomCssUrl = new Uri(HttpContext.Current.Server.MapPath("~/static/style/pdfCommande.css"));
        Renderer.PrintOptions.DPI = 300;
        // There are many advanced  PDF Settings

        // Render an HTML document or snippet as a string  
        Renderer.RenderHtmlAsPdf(fluxHTML).SaveAs(HttpContext.Current.Server.MapPath(urlPDF));
    }

    public String genererTableFlux(PPClients client, PPVendeurs vendeur, PPCommandes commande)
    {
        String flux = "";

        // infos vendeur
        flux += "<div class=\"div-bot-margin\">";

        flux += "<p>";
        flux += "<h2>" + vendeur.NomAffaires + "</h2>";
        flux += vendeur.Prenom + " " + vendeur.Nom;
        flux += "<br/>";
        flux += "Tel: " + vendeur.Tel1;
        flux += "<br/>";
        flux += vendeur.AdresseEmail;
        flux += "<br/>";
        flux += vendeur.Rue + ", " + vendeur.Ville + ", " + vendeur.Province + ", " + vendeur.Pays;
        flux += "</p>";

        flux += "</div>";

        // infos client
        flux += "<div class=\"div-bot-margin round-border div-padding\">";

        flux += "<p>";
        flux += "<h4>No client: " + client.NoClient + "</h4>";
        flux += client.Prenom + " " + client.Nom;
        flux += "<br/>";
        flux += vendeur.Rue + ", " + vendeur.Ville + ", " + vendeur.Province + ", " + vendeur.Pays;
        flux += "<br/>";
        flux += "Tel: " + client.Tel1;
        flux += "<br/>";
        flux += client.AdresseEmail;
        flux += "</p>";

        flux += "</div>";

        // no commande avec date
        flux += "<br/>";
        flux += "<div class=\"div-bot-margin div-right-margin\">";

        flux += "<p>";
        flux += "<h3>No commande: " + commande.NoCommande + "</h3>";
        flux += "No authorisation: " + commande.NoAutorisation;
        flux += "<br/>";
        flux += "Poids total: " + commande.PoidsTotal + " lbs";
        flux += "<br/>";
        flux += "Type livraison: " + commande.PPTypesLivraison.Description;
        flux += "<br/>";
        flux += commande.DateCommande;
        flux += "</p>";

        flux += "</div>";

        // détails commande
        String[] titreColonnes = { "Nom du produit", "No produit", "Prix de vente (unitaire)", "Quantité", "Total" };
        flux += "<div class=\"div-bot-margin\">";
        flux += "<table class=\"table-width-full\">";

        // headers
        flux += "<tr>";
        foreach(string str in titreColonnes)
        {
            flux += "<th>";
            flux += str;
            flux += "</th>";
        }
        flux += "</tr>";

        // rows avec produits
        
        foreach(PPDetailsCommandes detail in commande.PPDetailsCommandes)
        {
            flux += "<tr>";

            flux += "<td>";
            flux += detail.PPProduits.Nom;
            flux += "</td>";

            flux += "<td>";
            flux += detail.PPProduits.NoProduit;
            flux += "</td>";

            flux += "<td>";
            flux += (detail.PPProduits.PrixVente != null ? Decimal.Round((decimal)detail.PPProduits.PrixVente, 2) : Decimal.Round((decimal)detail.PPProduits.PrixDemande, 2)).ToString("C", CultureInfo.CurrentCulture);
            flux += "</td>";

            flux += "<td>";
            flux += detail.Quantité;
            flux += "</td>";

            flux += "<td>";
            flux += (detail.PPProduits.PrixVente != null ? Decimal.Round((decimal)(detail.PPProduits.PrixVente * detail.Quantité), 2) : Decimal.Round((decimal)(detail.PPProduits.PrixDemande * detail.Quantité), 2)).ToString("C", CultureInfo.CurrentCulture);
            flux += "</td>";

            flux += "</tr>";
        }

        flux += "</table>";
        flux += "</div>";

        // afficher le résumé de la commande avec total, tops, tvq, frais transport
        flux += "<div>";

        flux += "<table class=\"table-float\">";

        flux += "<tr>";
        flux += "<th>";
        flux += "Sous total";
        flux += "</th>";
        flux += "<td>";
        flux += Decimal.Round((decimal)commande.MontantTotAvantTaxes, 2).ToString("C", CultureInfo.CurrentCulture);
        flux += "</td>";
        flux += "</tr>";

        flux += "<tr>";
        flux += "<th>";
        flux += "Frais livraison";
        flux += "</th>";
        flux += "<td>";
        flux += Decimal.Round((decimal)commande.CoutLivraison, 2).ToString("C", CultureInfo.CurrentCulture);
        flux += "</td>";
        flux += "</tr>";

        flux += "<tr>";
        flux += "<th>";
        flux += "TPS";
        flux += "</th>";
        flux += "<td>";
        flux += Decimal.Round((decimal)commande.TPS, 2).ToString("C", CultureInfo.CurrentCulture);
        flux += "</td>";
        flux += "</tr>";

        flux += "<tr>";
        flux += "<th>";
        flux += "TVQ";
        flux += "</th>";
        flux += "<td>";
        flux += Decimal.Round((decimal)commande.TVQ, 2).ToString("C", CultureInfo.CurrentCulture);
        flux += "</td>";
        flux += "</tr>";

        flux += "<tr>";
        flux += "<th>";
        flux += "Total";
        flux += "</th>";
        flux += "<td>";
        flux += Decimal.Round((decimal)(commande.MontantTotAvantTaxes + commande.CoutLivraison + commande.TPS + commande.TVQ), 2).ToString("C", CultureInfo.CurrentCulture);
        flux += "</td>";
        flux += "</tr>";

        flux += "</table>";

        flux += "</div>";
        


        return flux;
    }

    public void afficherBon()
    {
        divProgressBar.Visible = false;
        LESi_reussi.Visible = true;
    }

    public void remplirTbInfosClient()
    {
        // aller chercher les informations du client et les afficher dans les textbox
        PPClients ficheClient = LibrairieLINQ.getFicheInformationsClient(long.Parse(Session["NoClient"].ToString()));
        prenom.Text = (ficheClient.Prenom != null) ? ficheClient.Prenom.Trim() : "";
        nomfamille.Text = (ficheClient.Nom != null) ? ficheClient.Nom.Trim() : "";
        email.Text = (ficheClient.AdresseEmail != null) ? ficheClient.AdresseEmail.Trim() : "";
        ville.Text = (ficheClient.Ville != null) ? ficheClient.Ville.Trim() : "";
        codepostal.Text = (ficheClient.CodePostal != null) ? ficheClient.CodePostal.Substring(0, 3) + " " + ficheClient.CodePostal.Substring(3, 3) : "";
        adresse.Text = (ficheClient.Rue != null) ? ficheClient.Rue : "";

        if (ficheClient.Province != null)
        {
            province.SelectedValue = ficheClient.Province.Trim();
        }

        if (ficheClient.Tel1 != null)
        {
            String num = "(" + ficheClient.Tel1.Substring(0, 3) + ") " + ficheClient.Tel1.Substring(3, 3) + "-" + ficheClient.Tel1.Substring(6);
            tel.Text = num;
        }
        else
        {
            tel.Text = "";
        }

        if (ficheClient.Tel2 != null)
        {
            String num = "(" + ficheClient.Tel2.Substring(0, 3) + ") " + ficheClient.Tel2.Substring(3, 3) + "-" + ficheClient.Tel2.Substring(6);
            cell.Text = num;
        }
        else
        {
            cell.Text = "";
        }
    }

    public Decimal getPrixLivraisonSelonPoids(Decimal poids, long noVendeur)
    {
        // code poids
        int codePoids = LibrairieLINQ.getCodePoids(poids);

        // code type livraison
        int noTypeLivraison = 1;
        if (rbRegulier.Checked == true)
        {
            noTypeLivraison = 401;
        }
        else if (rbPrioritaire.Checked == true)
        {
            noTypeLivraison = 402;
        }
        else if (rbCompagnie.Checked == true)
        {
            noTypeLivraison = 403;
        }

        if ((this.etape == "paiement" || this.etape == "confirmation") && this.typeLivraison != -1)
        {
            noTypeLivraison = this.typeLivraison;
        }

        Decimal sousTotal = LibrairieLINQ.getSousTotalPanierClient(long.Parse(Session["NoClient"].ToString()), this.idEntreprise);
        Decimal prixLivraison = LibrairieLINQ.getPrixLivraison(codePoids, noTypeLivraison, noVendeur);
        Decimal prixLivraisonGratuite = LibrairieLINQ.prixPourLivraisonGratuite(this.idEntreprise);

        if (noTypeLivraison == 401 && sousTotal > prixLivraisonGratuite)
        {
            prixLivraison = 0;
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
        // vérifier que le poids de la commande ne dépasse pas le maximum de la compagnie et qu'il n'y a pas d'articles en rupture de stock
        bool depassePoids = LibrairieLINQ.depassePoidsMax(this.idEntreprise, LibrairieLINQ.getPoidsTotalPanierClient(long.Parse(Session["NoClient"].ToString()), this.idEntreprise));
        bool rupture = LibrairieLINQ.ruptureDeStockPanierClient(long.Parse(Session["NoClient"].ToString()), this.idEntreprise);

        System.Diagnostics.Debug.WriteLine("Depasse poids: " + depassePoids);
        System.Diagnostics.Debug.WriteLine("Rupture: " + rupture);

        if (!depassePoids && !rupture)
        {
            String url = "~/Pages/SaisieCommande.aspx?IDEntreprise=" + this.idEntreprise + "&Etape=perso";
            Session["EtapePaiement"] = "perso";
            Response.Redirect(url, true);
        }
    }

    public void livraison_click(Object sender, EventArgs e)
    {
        bool valide = true;
        if (!rfvPrenom.IsValid || !rePrenom.IsValid)
        {
            prenom.CssClass = "form-control erreur";
            valide = false;
        }
        else
        {
            prenom.CssClass = "form-control";
        }

        if (!rfvNom.IsValid || !reNom.IsValid)
        {
            nomfamille.CssClass = "form-control erreur";
            valide = false;
        }
        else
        {
            nomfamille.CssClass = "form-control";
        }

        if (!rfvEmail.IsValid)
        {
            email.CssClass = "form-control erreur";
            valide = false;
        }
        else
        {
            email.CssClass = "form-control";
        }

        if (!rfvVille.IsValid || !reVille.IsValid)
        {
            ville.CssClass = "form-control erreur";
            valide = false;
        }
        else
        {
            ville.CssClass = "form-control";
        }

        if (!rfvCodePostal.IsValid || !reCodePostal.IsValid)
        {
            codepostal.CssClass = "form-control erreur";
            valide = false;
        }
        else
        {
            codepostal.CssClass = "form-control";
        }

        if (!rfvAdresse.IsValid || !reAdresse.IsValid)
        {
            adresse.CssClass = "form-control erreur";
            valide = false;
        }
        else
        {
            adresse.CssClass = "form-control";
        }

        if (!rfvTel.IsValid || !reTel.IsValid)
        {
            tel.CssClass = "form-control erreur";
            valide = false;
        }
        else
        {
            tel.CssClass = "form-control";
        }

        if (!reCell.IsValid)
        {
            cell.CssClass = "form-control erreur";
            valide = false;
        }
        else
        {
            cell.CssClass = "form-control";
        }


        // si valide = continuer
        if (valide){
            BD6B8_424SEntities dataContext = new BD6B8_424SEntities();

            using (var dbTransaction = dataContext.Database.BeginTransaction())
            {
                try
                {
                    long noClient = long.Parse(Session["NoClient"].ToString());

                    var tableClients = dataContext.PPClients;
                    PPClients client = (from c in tableClients
                                        where c.NoClient == noClient
                                        select c).First();

                    client.Prenom = prenom.Text;
                    client.Nom = nomfamille.Text;
                    client.Ville = ville.Text;
                    client.CodePostal = codepostal.Text.ToUpper().Replace(" ", "").Replace("-", "");
                    client.Rue = adresse.Text;

                    String num = tel.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    client.Tel1 = num;

                    client.Tel2 = (cell.Text != "") ? cell.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") : null;
                    client.Province = province.SelectedValue;
                    dataContext.SaveChanges();
                    dbTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                }
                
            }
            String url = "~/Pages/SaisieCommande.aspx?IDEntreprise=" + this.idEntreprise + "&Etape=livraison";
            Session["EtapePaiement"] = "livraison";
            Response.Redirect(url, true);
        }

    }

    public void retourPanier_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Retour");
        String url = "~/Pages/SaisieCommande.aspx?IDEntreprise=" + this.idEntreprise;
        Session["EtapePaiement"] = null;
        Response.Redirect(url, true);
    }

    public void retourInfosPerso_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/SaisieCommande.aspx?IDEntreprise=" + this.idEntreprise + "&Etape=perso";
        Session["EtapePaiement"] = "perso";
        Response.Redirect(url, true);
    }

    public void retourLivraison_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/SaisieCommande.aspx?IDEntreprise=" + this.idEntreprise + "&Etape=livraison";
        Session["EtapePaiement"] = "livraison";
        Response.Redirect(url, true);
    }

    public void paiementLESi_click(Object sender, EventArgs e)
    {
        // vérifier que le poids de la commande ne dépasse pas le maximum de la compagnie et qu'il n'y a pas d'articles en rupture de stock
        bool depassePoids = LibrairieLINQ.depassePoidsMax(this.idEntreprise, LibrairieLINQ.getPoidsTotalPanierClient(long.Parse(Session["NoClient"].ToString()), this.idEntreprise));
        bool rupture = LibrairieLINQ.ruptureDeStockPanierClient(long.Parse(Session["NoClient"].ToString()), this.idEntreprise);

        if (!depassePoids && !rupture)
        {
            afficherReponseLESi();
        }
        else
        {
            afficherPaiement();
        }
    }

    public void paiementSecurise_click(Object sender, EventArgs e)
    {
        // vérifier que le poids de la commande ne dépasse pas le maximum de la compagnie et qu'il n'y a pas d'articles en rupture de stock
        bool depassePoids = LibrairieLINQ.depassePoidsMax(this.idEntreprise, LibrairieLINQ.getPoidsTotalPanierClient(long.Parse(Session["NoClient"].ToString()), this.idEntreprise));
        bool rupture = LibrairieLINQ.ruptureDeStockPanierClient(long.Parse(Session["NoClient"].ToString()), this.idEntreprise);

        if (!depassePoids && !rupture)
        {
            int typeLiv = 401;
            if (rbRegulier.Checked) { typeLiv = 401; }
            else if (rbPrioritaire.Checked) { typeLiv = 402; }
            else if (rbCompagnie.Checked) { typeLiv = 403; }
            //afficherPaiement();
            String url = "~/Pages/SaisieCommande.aspx?IDEntreprise=" + this.idEntreprise + "&Etape=paiement" + "&TypeLivraison=" + typeLiv;
            Session["EtapePaiement"] = "paiement";
            Response.Redirect(url, true);
        }
        else
        {
            //afficherLivraison();
        }
    }

    public void visualiserBon_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Visualiser le bon de: " + this.numCommande);
        String url = "../static/pdf/" + this.numCommande + ".pdf";
        Response.Write("<script>window.open ('" + url + "','_blank');</script>");
    }

    public void imprimerBon_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Visualiser le bon de: " + this.numCommande);
        String url = "../static/pdf/" + this.numCommande + ".pdf";
        Response.Write("<script>window.open ('" + url + "','_blank');</script>");
    }

    public void livraison_changed(Object sender, EventArgs e)
    {
        // calculer le prix de la livraison
        Decimal poidsTotal = LibrairieLINQ.getPoidsTotalPanierClient(long.Parse(Session["NoClient"].ToString()), this.idEntreprise);

        Decimal prixLivraison = Decimal.Round(getPrixLivraisonSelonPoids(poidsTotal, this.idEntreprise), 2);

        if (prixLivraison == 0)
        {
            fraisTransport.Text = "GRATUIT";
        }
        else
        {
            fraisTransport.Text = prixLivraison.ToString("C", CultureInfo.CurrentCulture);
        }
    }

    private bool getIdEntreprise()
    {
        bool rediriger = false;

        int n;
        if (Request.QueryString["IDEntreprise"] == null || !int.TryParse(Request.QueryString["IDEntreprise"], out n))
        {
            rediriger = true;
        }
        else
        {
            BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
            var tableVendeurs = dataContext.PPVendeurs;

            if ((from v in tableVendeurs where v.NoVendeur == n select v).Count() == 0)
            {
                rediriger = true;
            }
            else
            {
                this.idEntreprise = n;
            }
        }
        return rediriger;
    }

    private void getEtapeCommande()
    {
        if (Session["EtapePaiement"] == null)
        {
            this.etape = "panier";
            Session["EtapePaiement"] = "panier";
        }
        else
        {
            if (Request.QueryString["Etape"] == "confirmation" && Session["EtapePaiement"].ToString() == "paiement")
            {
                this.etape = "confirmation";
                Session["EtapePaiement"] = "confirmation";
            }
            else
            {
                this.etape = Session["EtapePaiement"].ToString();
            }
        }
    }

    private void getNumCommande()
    {
        long n;
        if (Request.QueryString["Commande"] == null || !long.TryParse(Request.QueryString["Commande"], out n))
        {
            this.numCommande = 0;
        }
        else
        {
            this.numCommande = n;
        }
    }

    private bool getTypeLiv()
    {
        bool rediriger = false;

        short n;
        if (Request.QueryString["TypeLivraison"] == null || !short.TryParse(Request.QueryString["TypeLivraison"], out n))
        {
            if (this.etape == "paiement") { rediriger = true; }
            else
            {
                this.typeLivraison = -1;
            }
        }
        else
        {
            if (!(n >= 401 && n <= 403))
            {
                rediriger = true;
            }
            this.typeLivraison = n;
        }

        return rediriger;
    }

    private void getErreur()
    {
        int n;
        if (Request.QueryString["Erreur"] == null || !int.TryParse(Request.QueryString["Erreur"], out n))
        {
            this.erreur = -1;
        }
        else
        {
            this.erreur = n;
        }
    }


    public void nomEntreprisePanier_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("ALLER VISITER LE CATALOGUE");
        LinkButton btn = (LinkButton)sender;
        String idVendeur = btn.ID.Replace("vendeur_", "");

        Session["NoVendeurCatalogue"] = idVendeur;

        String url = "~/Pages/ConsultationCatalogueProduitVendeur.aspx?";
        Response.Redirect(url, true);
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


}