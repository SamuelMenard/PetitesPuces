﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_AccueilClient : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;
        afficherPaniers();
        LibrairieControlesDynamique.hrDYN(phDynamique);
        afficherCategories();
    }

    public void afficherPaniers()
    {
        List<int> lstEntreprises = new List<int>();
        lstEntreprises.Add(1);
        lstEntreprises.Add(2);

        Dictionary<int, String> mapEntreprises = new Dictionary<int, string>();
        mapEntreprises.Add(1, "Apple");
        mapEntreprises.Add(2, "Microsoft");

        // temporaire
        int idItem = 0;

        for (int y = 0; y < lstEntreprises.Count; y++)
        {
            int idEntreprise = lstEntreprises[y];
            String nomEntreprise = mapEntreprises[idEntreprise];
            String nomVendeur = "Raphaël Benoït";
            Double sousTotal = 0;

            // Créer le panier du vendeur X
            Panel panelBase = LibrairieControlesDynamique.divDYN(phDynamique, idEntreprise + "_base", "panel panel-default");

            // Nom de l'entreprise
            Panel panelHeader = LibrairieControlesDynamique.divDYN(panelBase, idEntreprise + "_header", "panel-heading");
            LibrairieControlesDynamique.lblDYN(panelHeader, idEntreprise + "_nom", nomEntreprise + " (" + nomVendeur + ")", "nom-entreprise");

            // Liste des items + le total
            Panel panelBody = LibrairieControlesDynamique.divDYN(panelBase, idEntreprise + "_body", "panel-body");


            // Rajouter les produits dans le panier

            for (int i = 0; i < 3; i++)
            {
                idItem++;
                int quantiteSelectionne = 1;
                Double prix = 1300.99;

                // sum au sous total
                sousTotal += prix;

                String nomProduit = "MacBook Air 13\", 256GB SSD - Rose Gold";
                String urlImage = "../static/images/macbookair13.jpg";

                Panel rowItem = LibrairieControlesDynamique.divDYN(panelBody, idEntreprise + "_rowItem_" + idItem, "row");

                // ajouter l'image
                Panel colImg = LibrairieControlesDynamique.divDYN(rowItem, idEntreprise + "_colImg_" + idItem, "col-sm-2");
                LibrairieControlesDynamique.imgDYN(colImg, idEntreprise + "_img_" + idItem, urlImage, "img-size");

                // Nom du produit
                Panel colNom = LibrairieControlesDynamique.divDYN(rowItem, idEntreprise + "_colNom_" + idItem, "col-sm-4");
                LibrairieControlesDynamique.lblDYN(colNom, idEntreprise + "_nom_" + idItem, nomProduit, "nom-item");

                // Quantité sélectionné
                Panel colQuantite = LibrairieControlesDynamique.divDYN(rowItem, idEntreprise + "_colQuantite_" + idItem, "col-sm-4");
                LibrairieControlesDynamique.tbDYN(colQuantite, idEntreprise + "quantite_" + idItem, quantiteSelectionne.ToString(), "form-control border-quantite");
                LibrairieControlesDynamique.lbDYN(colQuantite, "update_" + idItem, "Mettre à jour", update_click);

                // Prix item
                Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, idEntreprise + "_colPrix_" + idItem, "col-sm-2");
                LibrairieControlesDynamique.lblDYN(colPrix, idEntreprise + "_prix_" + idItem, "$" + prix.ToString(), "prix_item");

                // Bouton retirer
                Panel rowBtnRetirer = LibrairieControlesDynamique.divDYN(panelBody, idEntreprise + "_rowBtnRetirer_" + idItem, "row");
                Panel colBtnRetirer = LibrairieControlesDynamique.divDYN(rowBtnRetirer, idEntreprise + "_colBtnRetirer_" + idItem, "col-sm-2");
                LibrairieControlesDynamique.btnDYN(colBtnRetirer, "btnRetirer_" + idItem, "btn btn-default", "RETIRER", retirer_click);
                LibrairieControlesDynamique.hrDYN(panelBody);
            }

            // Afficher le sous total
            Panel rowSousTotal = LibrairieControlesDynamique.divDYN(panelBody, idEntreprise + "_rowSousTotal", "row");
            Panel colLabelSousTotal = LibrairieControlesDynamique.divDYN(rowSousTotal, idEntreprise + "_colLabelSousTotal", "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colLabelSousTotal, idEntreprise + "_labelSousTotal", "Sous total: ", "infos-payage");

            Panel colMontantSousTotal = LibrairieControlesDynamique.divDYN(rowSousTotal, idEntreprise + "_colMontantSousTotal", "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colMontantSousTotal, idEntreprise + "_montantSousTotal", "$" + sousTotal.ToString(), "infos-payage");

            LibrairieControlesDynamique.hrDYN(panelBody);

            // Bouton commander
            Panel rowBtnCommander = LibrairieControlesDynamique.divDYN(panelBody, idEntreprise + "_rowBtnCommander", "row");
            Panel colLabelBtnCommander = LibrairieControlesDynamique.divDYN(rowBtnCommander, idEntreprise + "_colLabelBtnCommander", "col-sm-4");
            LibrairieControlesDynamique.btnDYN(colLabelBtnCommander, idEntreprise + "_btnCommander", "btn btn-warning", "Commander", commander_click);
        }
    }

    public void afficherCategories()
    {
        List<String> lstCategories = new List<string>();
        lstCategories.Add("Électronique");
        lstCategories.Add("Maison");
        lstCategories.Add("Extérieur");

        int noVendeur = 0;

        for (int i = 0; i < lstCategories.Count; i++)
        {
            String nomCategorie = lstCategories[i];

            // créer le panel pour la catégorie
            Panel panelDefault = LibrairieControlesDynamique.divDYN(phDynamique, "", "panel panel-default");
            Panel panelHeading = LibrairieControlesDynamique.divDYN(panelDefault, "", "panel-heading");
            Panel panelBody = LibrairieControlesDynamique.divDYN(panelDefault, "", "panel-body");

            // mettre le nom de la catégorie dans le header
            LibrairieControlesDynamique.h4DYN(panelHeading, nomCategorie);

            // créer la row
            Panel row = null;

            List<String> lstEntreprises = new List<string>();
            lstEntreprises.Add("Apple");
            lstEntreprises.Add("Déco Découverte");
            lstEntreprises.Add("Rona");
            lstEntreprises.Add("Bureau En Gros");
            lstEntreprises.Add("Vidéotron");
            lstEntreprises.Add("Vidéotron");
            lstEntreprises.Add("Vidéotron");
            lstEntreprises.Add("Vidéotron");
            lstEntreprises.Add("Vidéotron");

            for (int y = 0; y < lstEntreprises.Count; y++)
            {
                noVendeur++;
                int nbItems = 7;

                if (y % 6 == 0)
                {
                    row = LibrairieControlesDynamique.divDYN(panelBody, "", "row");
                    row.Style.Add("margin-bottom", "20 px");
                }

                String nomEntreprise = lstEntreprises[y];
                String urlImg = "../static/images/videotron.png";

                // rajouter les colonnes (entreprises)
                Panel col = LibrairieControlesDynamique.divDYN(row, "", "col-md-2");
                col.Style.Add("text-align", "center");
                Image img = LibrairieControlesDynamique.imgDYN(col, "", urlImg, "");
                img.Style.Add("width", "100px");
                LibrairieControlesDynamique.brDYN(col);

                // nom entreprise + nb produits
                LinkButton lbNomEntreprise = LibrairieControlesDynamique.lbDYN(col, "lbNomEntreprise_" + noVendeur, nomEntreprise, nomEntreprise_click);
                LibrairieControlesDynamique.spaceDYN(col);
                LibrairieControlesDynamique.spaceDYN(col);
                LibrairieControlesDynamique.lblDYN(col, "lblQuantiteItems_" + noVendeur, nbItems.ToString(), "badge");

            }
        }

    }

    public void retirer_click(Object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        String itemID = btn.ID.Replace("btnRetirer_", "");
        System.Diagnostics.Debug.WriteLine(itemID);
    }

    public void update_click(Object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        String itemID = btn.ID.Replace("update_", "");
        System.Diagnostics.Debug.WriteLine(itemID);
    }

    public void commander_click(Object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        String entrepriseID = btn.ID.Replace("_btnCommander", "");
        System.Diagnostics.Debug.WriteLine(entrepriseID);

        String url = "~/Pages/SaisieCommande.aspx?IDEntreprise=" + entrepriseID;
        Response.Redirect(url, true);
    }

    public void nomEntreprise_click(Object sender, EventArgs e)
    {
        
    }
}