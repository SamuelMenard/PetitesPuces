﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_GestionPanierCommande : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;

        // Afficher message accueil avec nom

        afficherMessageAccueil();
        afficherPaniers();
    }

    public void afficherMessageAccueil()
    {
        String nomPrenom = "";
        if (Session["Prenom"] != null && Session["Nom"] != null)
        {
            nomPrenom += Session["Prenom"].ToString() + " " + Session["Nom"].ToString();
        }
        lblBienvenue.Text = "Bienvenue " + nomPrenom + "!";
    }

    public void afficherPaniers()
    {
        Dictionary<Nullable<long>, List<PPArticlesEnPanier>> lstPaniers = LibrairieLINQ.getPaniersClient(long.Parse(Session["NoClient"].ToString()));

        // ajouter le liens mes paniers
        LibrairieControlesDynamique.liDYN(ulSideBar, "#panier", "Mes paniers", "section-header");

        foreach (KeyValuePair<Nullable<long>, List<PPArticlesEnPanier>> entry in lstPaniers)
        {
            bool ruptureStock = false;
            bool quantiteModif = false;

            // do something with entry.Value or entry.Key
            long? idEntreprise = entry.Key;
            String nomEntreprise = entry.Value[0].PPVendeurs.NomAffaires;
            String nomVendeur = entry.Value[0].PPVendeurs.Prenom + " " + entry.Value[0].PPVendeurs.Nom;
            decimal? sousTotal = 0;

            // ajouter lien navbar
            LibrairieControlesDynamique.liDYN(ulSideBar, "#" + "contentBody_panier" + idEntreprise, entry.Value[0].PPVendeurs.NomAffaires, "");

            // Créer le panier du vendeur X
            Panel panelBase = LibrairieControlesDynamique.divDYN(paniersDynamique, "panier" + idEntreprise, "panel panel-default");

            // Nom de l'entreprise
            Panel panelHeader = LibrairieControlesDynamique.divDYN(panelBase, idEntreprise + "_header", "panel-heading");
            LibrairieControlesDynamique.lbDYN(panelHeader, "vendeur_" + idEntreprise, nomEntreprise + " (" + nomVendeur + ")", "nom-entreprise", nomEntreprisePanier_click);

            // Liste des items + le total
            Panel panelBody = LibrairieControlesDynamique.divDYN(panelBase, idEntreprise + "_body", "panel-body");


            // Rajouter les produits dans le panier

            foreach (PPArticlesEnPanier article in entry.Value)
            {
                long? idItem = article.NoProduit;
                short? quantiteSelectionne = article.NbItems;
                decimal? prixUnitaire = article.PPProduits.PrixDemande;

                decimal? prixAvecQuantites = article.PPProduits.PrixDemande * article.NbItems;
                decimal? montantRabais = article.PPProduits.PrixDemande - article.PPProduits.PrixVente;

                if (article.PPProduits.DateVente < DateTime.Now) { montantRabais = 0; }

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
                LibrairieControlesDynamique.lblDYN(colNom, idEntreprise + "_nom_" + idItem, nomProduit, "nom-item deux-lignes");
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
            Decimal? pctTPS = LibrairieLINQ.getPourcentageTaxes("TPS", idEntreprise) /100;
            Decimal? TPS = sousTotal * pctTPS;

            Decimal? pctTVQ = LibrairieLINQ.getPourcentageTaxes("TVQ", idEntreprise) / 100;
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

            // Bouton commander
            Panel rowBtnCommander = LibrairieControlesDynamique.divDYN(panelBody, idEntreprise + "_rowBtnCommander", "row");
            Panel colLabelBtnCommander = LibrairieControlesDynamique.divDYN(rowBtnCommander, idEntreprise + "_colLabelBtnCommander", "col-sm-12 text-right");

            if (!ruptureStock)
            {
                LibrairieControlesDynamique.btnDYN(colLabelBtnCommander, idEntreprise + "_btnCommander", "btn btn-warning", "Commander", commander_click);
            }
        }

    }

    public void retirer_click(Object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        String panierID = btn.ID.Replace("btnRetirer_", "");

        LibrairieLINQ.retirerArticlePanier(long.Parse(panierID));

        String url = "~/Pages/GestionPanierCommande.aspx?";
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
        String url = "~/Pages/GestionPanierCommande.aspx?#contentBody_quantite_" + itemID;
        Response.Redirect(url, true);
    }

    public void commander_click(Object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        String entrepriseID = btn.ID.Replace("_btnCommander", "");
        System.Diagnostics.Debug.WriteLine(entrepriseID);

        String url = "~/Pages/SaisieCommande.aspx?IDEntreprise=" + entrepriseID;
        Response.Redirect(url, true);
    }
    

    public void nomEntreprisePanier_click(Object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        String idVendeur = btn.ID.Replace("vendeur_", "");

        Session["NoVendeurCatalogue"] = idVendeur;

        String url = "~/Pages/ConsultationCatalogueProduitVendeur.aspx?";
        Response.Redirect(url, true);
    }
}