using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_SaisieCommande : System.Web.UI.Page
{
    private static BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
    private long idEntreprise;
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
                decimal? prixAvecQuantitesAvecRabais = article.PPProduits.PrixVente * article.NbItems;
                decimal? montantRabais = article.PPProduits.PrixDemande - article.PPProduits.PrixVente;

                decimal? poids = article.PPProduits.Poids;
                poidsTotal += (Decimal)(poids * quantiteSelectionne);

                // sum au sous total
                sousTotal += prixAvecQuantitesAvecRabais;

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

                if (montantRabais > 0)
                {
                    LibrairieControlesDynamique.lblDYN(colPrix, "", "$" + Decimal.Round((Decimal)prixAvecQuantites, 2).ToString(), "prix-item-avant-rabais");
                    LibrairieControlesDynamique.brDYN(colPrix);
                }

                LibrairieControlesDynamique.lblDYN(colPrix, "", "$" + Decimal.Round((Decimal)prixAvecQuantitesAvecRabais, 2).ToString(), "prix_item");
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
            Decimal? pctTPS = LibrairieLINQ.getPourcentageTaxes("TPS", this.idEntreprise) / 100;
            Decimal? TPS = sousTotal * pctTPS;

            // Afficher la TVQ
            Decimal? pctTVQ = LibrairieLINQ.getPourcentageTaxes("TVQ", this.idEntreprise) / 100;
            Decimal? TVQ = sousTotal * pctTVQ;

            if (pctTPS != 0)
            {
                Panel rowTPS = LibrairieControlesDynamique.divDYN(panelBody, idEntreprise + "_rowTPS", "row");
                Panel colLabelTPS = LibrairieControlesDynamique.divDYN(rowTPS, idEntreprise + "_colLabelTPS", "col-sm-10 text-right");
                LibrairieControlesDynamique.lblDYN(colLabelTPS, idEntreprise + "_labelTPS", "TPS: ", "infos-payage");

                Panel colMontantTPS = LibrairieControlesDynamique.divDYN(rowTPS, idEntreprise + "_colMontantTPS", "col-sm-2 text-right");
                LibrairieControlesDynamique.lblDYN(colMontantTPS, idEntreprise + "_montantTPS", "$" + Decimal.Round((Decimal)TPS, 2).ToString(), "infos-payage");

                LibrairieControlesDynamique.hrDYN(panelBody);
            }
            

            // Afficher la TVQ (si nécessaire)
            if (pctTVQ != 0)
            {

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
            Panel rowBtnShipping = LibrairieControlesDynamique.divDYN(panelBody, idEntreprise + "_rowBtnShipping", "row");
            Panel colLabelBtnShipping = LibrairieControlesDynamique.divDYN(rowBtnShipping, idEntreprise + "_colLabelBtnShipping", "col-sm-2");

            System.Diagnostics.Debug.WriteLine("Rupture stock: " + ruptureStock);
            System.Diagnostics.Debug.WriteLine("Limite depasse: " + ruptureStock);
            if (!ruptureStock && !limiteDePoidsDepasse)
            {
                HtmlButton btnShipping = LibrairieControlesDynamique.htmlbtnDYN(colLabelBtnShipping, idEntreprise + "_BtnShipping", "btn btn-warning", "Informations de livraison", "glyphicon glyphicon-user", infosPerso_click);
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

                decimal? prixAvecQuantites = article.PPProduits.PrixDemande * article.NbItems;
                decimal? prixAvecQuantitesAvecRabais = article.PPProduits.PrixVente * article.NbItems;
                decimal? montantRabais = article.PPProduits.PrixDemande - article.PPProduits.PrixVente;

                decimal? poids = article.PPProduits.Poids;
                poidsTotal += (Decimal)(poids * quantiteSelectionne);

                // sum au sous total
                sousTotal += prixAvecQuantites;

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
                Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, idEntreprise + "_colPrix_" + idItem, "col-sm-2");

                if (montantRabais > 0)
                {
                    LibrairieControlesDynamique.lblDYN(colPrix, "", "$" + Decimal.Round((Decimal)prixAvecQuantites, 2).ToString(), "prix-item-avant-rabais");
                    LibrairieControlesDynamique.brDYN(colPrix);
                }
                
                LibrairieControlesDynamique.lblDYN(colPrix, "", "$" + Decimal.Round((Decimal)prixAvecQuantitesAvecRabais, 2).ToString(), "prix_item");
                LibrairieControlesDynamique.brDYN(colPrix);
                LibrairieControlesDynamique.lblDYN(colPrix, "", (montantRabais > 0) ? "Rabais de $" + Decimal.Round((Decimal)montantRabais, 2).ToString() : "", "rabais");

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

        // masquer div livraison
        divLiv.Visible = false;

        // remplir les infos
        if (!Page.IsPostBack)
        {
            remplirTbInfosClient();
        }

        // masquer la div de paiement
        div_paiement.Visible = false;

        // rendre visible la division
        divInfosPerso.Visible = true;

    }

    public void afficherLivraison()
    {
        // afficher resumé panier
        btnPaiementSecurise.Visible = afficherResumePanier();

        // ajuster la bar de progression
        progressBar.Style.Add("width", "75%");

        // masquer div paiement
        div_paiement.Visible = false;

        // masquer div infos perso
        divInfosPerso.Visible = false;

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
            fraisTransport.Text = "$" + prixLivraisonStandard.ToString();
        }

        // get sous total avec rabais
        Decimal sousTotal = Decimal.Round(LibrairieLINQ.getSousTotalPanierClient(long.Parse(Session["NoClient"].ToString()), this.idEntreprise), 2);

        // calculer TPS et TVQ
        Decimal TPSPourcentage = (Decimal)LibrairieLINQ.getPourcentageTaxes("TPS", this.idEntreprise) / 100;
        Decimal TVQPourcentage = (Decimal)LibrairieLINQ.getPourcentageTaxes("TVQ", this.idEntreprise) / 100;
        Decimal TPS = Math.Round((sousTotal + prixLivraisonStandard) * TPSPourcentage, 2);
        Decimal TVQ = Math.Round((sousTotal + prixLivraisonStandard) * TVQPourcentage, 2);

        // afficher les autres infos
        lblSousTotalLiv.Text = "$" + sousTotal.ToString();
        if (TPSPourcentage != 0) { lblTPSLiv.Text = "$" + TPS.ToString(); }
        else { divTPSLiv.Visible = false; }

        if (TVQPourcentage != 0) { lblTVQLiv.Text = "$" + TVQ.ToString(); }
        else { divTVQLiv.Visible = false; }

        lblTotalLiv.Text = "$"+ (sousTotal + TPS + TVQ + prixLivraisonStandard).ToString();

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
            lblFraisTransportPaiement.Text = "$" + prixLivraisonStandard.ToString();
        }

        // get sous total avec rabais
        Decimal sousTotal = Decimal.Round(LibrairieLINQ.getSousTotalPanierClient(long.Parse(Session["NoClient"].ToString()), this.idEntreprise), 2);

        // calculer TPS et TVQ
        Decimal TPSPourcentage = (Decimal)LibrairieLINQ.getPourcentageTaxes("TPS", this.idEntreprise) / 100;
        Decimal TVQPourcentage = (Decimal)LibrairieLINQ.getPourcentageTaxes("TVQ", this.idEntreprise) / 100;
        Decimal TPS = Math.Round((sousTotal + prixLivraisonStandard) * TPSPourcentage, 2);
        Decimal TVQ = Math.Round((sousTotal + prixLivraisonStandard) * TVQPourcentage, 2);

        // afficher les autres infos
        lblSousTotalPaiement.Text = "$" + sousTotal.ToString();

        if (TPSPourcentage != 0) { lblTPSPaiement.Text = "$" + TPS.ToString(); }
        else { divTPSPaiement.Visible = false; }

        if (TVQPourcentage != 0) { lblTVQPaiement.Text = "$" + TVQ.ToString(); }
        else { divTVQPaiement.Visible = false; }

        lblTotalPaiement.Text = "$" + (sousTotal + TPS + TVQ + prixLivraisonStandard).ToString();

        // afficher resume paiement
        btnLESi.Visible = afficherResumePanier();

        // afficher la div
        div_paiement.Visible = true;
    }

    public void afficherReponseLESi()
    {
        Boolean reussi = true;

        // rendre visible la bonne div
        if (reussi)
        {
            // rajouter les infos de la commande dans la base de données
            creerCommande();

            String url = "~/Pages/SaisieCommande.aspx?IDEntreprise=" + this.idEntreprise + "&Etape=bon";
            Response.Redirect(url, true);
        }
        else
        {
            LESi_echoue.Visible = true;
            afficherPaiement();
        }
    }

    public void creerCommande()
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
        short noTypeLivraison = 1;

        if (rbRegulier.Checked == true)
        {
            noTypeLivraison = 1;
        }
        else if (rbPrioritaire.Checked == true)
        {
            noTypeLivraison = 2;
        }
        else if (rbCompagnie.Checked == true)
        {
            noTypeLivraison = 3;
        }


        // calculer un id de commande unique
        long biggestCommandeID = 1;

        if (derniereCommande.Count() > 0)
        {
            biggestCommandeID = derniereCommande.First().NoCommande + 1;
        }

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
                    DateVente = DateTime.Now,
                    NoAutorisation = biggestNoAutorisation.ToString(),
                    FraisLesi = 10,
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

                foreach(var article in tousLesArticlesClient)
                {
                    dataContext.PPArticlesEnPanier.Remove(article);
                }

                dataContext.SaveChanges();
                dbTransaction.Commit();
            }
            catch(Exception ex)
            {
                dbTransaction.Rollback();
            }
            
        }
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
        codepostal.Text = (ficheClient.CodePostal != null) ? ficheClient.CodePostal.Trim() : "";
        String[] tabCiviqueRue = ((ficheClient.Rue != null) ? ficheClient.Rue : "").Split(' ');

        if (tabCiviqueRue.Length == 2)
        {
            noCivique.Text = tabCiviqueRue[0].Trim();
            rue.Text = tabCiviqueRue[1].Trim();
        }

        if (ficheClient.Province != null)
        {
            province.SelectedValue = ficheClient.Province.Trim();
        }

        tel.Text = (ficheClient.Tel1 != null) ? ficheClient.Tel1.Trim() : "";
        cell.Text = (ficheClient.Tel2 != null) ? ficheClient.Tel2.Trim() : "";
    }

    public Decimal getPrixLivraisonSelonPoids(Decimal poids, long noVendeur)
    {
        // code poids
        int codePoids = LibrairieLINQ.getCodePoids(poids);

        // code type livraison
        int noTypeLivraison = 1;

        if (rbRegulier.Checked == true)
        {
            noTypeLivraison = 1;
        }
        else if (rbPrioritaire.Checked == true)
        {
            noTypeLivraison = 2;
        }
        else if (rbCompagnie.Checked == true)
        {
            noTypeLivraison = 3;
        }

        Decimal sousTotal = LibrairieLINQ.getSousTotalPanierClient(long.Parse(Session["NoClient"].ToString()), this.idEntreprise);
        Decimal prixLivraison = LibrairieLINQ.getPrixLivraison(codePoids, noTypeLivraison, noVendeur);
        Decimal prixLivraisonGratuite = LibrairieLINQ.prixPourLivraisonGratuite(this.idEntreprise);

        if (noTypeLivraison == 1 && sousTotal > prixLivraisonGratuite)
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
        bool rupture = LibrairieLINQ.ruptureDeStockPanierClient(long.Parse(Session["NoClient"].ToString()));

        if (!depassePoids && !rupture)
        {
            String url = "~/Pages/SaisieCommande.aspx?IDEntreprise=" + this.idEntreprise + "&Etape=livraison";
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

        if (!rfvNoCivique.IsValid || !reNoCivique.IsValid)
        {
            noCivique.CssClass = "form-control erreur";
            valide = false;
        }
        else
        {
            noCivique.CssClass = "form-control";
        }

        if (!rfvRue.IsValid || !reRue.IsValid)
        {
            rue.CssClass = "form-control erreur";
            valide = false;
        }
        else
        {
            rue.CssClass = "form-control";
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
            afficherLivraison();
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
                    client.CodePostal = codepostal.Text;
                    client.Rue = noCivique.Text + " " + rue.Text;
                    client.Tel1 = tel.Text;
                    client.Tel2 = (cell.Text != "") ? cell.Text : null;
                    client.Province = province.SelectedValue;
                    dataContext.SaveChanges();
                    dbTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                }
                
            }
            
            
        }

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
        // vérifier que le poids de la commande ne dépasse pas le maximum de la compagnie et qu'il n'y a pas d'articles en rupture de stock
        bool depassePoids = LibrairieLINQ.depassePoidsMax(this.idEntreprise, LibrairieLINQ.getPoidsTotalPanierClient(long.Parse(Session["NoClient"].ToString()), this.idEntreprise));
        bool rupture = LibrairieLINQ.ruptureDeStockPanierClient(long.Parse(Session["NoClient"].ToString()));

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
        bool rupture = LibrairieLINQ.ruptureDeStockPanierClient(long.Parse(Session["NoClient"].ToString()));

        if (!depassePoids && !rupture)
        {
            afficherPaiement();
        }
        else
        {
            afficherLivraison();
        }
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
        // calculer le prix de la livraison
        Decimal poidsTotal = LibrairieLINQ.getPoidsTotalPanierClient(long.Parse(Session["NoClient"].ToString()), this.idEntreprise);

        Decimal prixLivraison = getPrixLivraisonSelonPoids(poidsTotal, this.idEntreprise);

        if (prixLivraison == 0)
        {
            fraisTransport.Text = "GRATUIT";
        }
        else
        {
            fraisTransport.Text = "$" + prixLivraison.ToString();
        }

        afficherLivraison();
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