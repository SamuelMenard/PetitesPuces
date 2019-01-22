using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_GestionPanierCommande : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<String> lstEntreprises = new List<string>();
        lstEntreprises.Add("Apple");
        lstEntreprises.Add("Microsoft");


        for (int y = 0; y < lstEntreprises.Count; y++)
        {
            String nomEntreprise = lstEntreprises[y];
            Double sousTotal = 0;
            Double TPS = 0;
            Double TVQ = 0;

            // Créer le panier du vendeur X
            Panel panelBase = LibrairieControlesDynamique.divDYN(phDynamique, nomEntreprise + "_base", "panel panel-default");

            // Nom de l'entreprise
            Panel panelHeader = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_header", "panel-heading");
            LibrairieControlesDynamique.lblDYN(panelHeader, nomEntreprise + "_nom", nomEntreprise, "nom-entreprise");

            // Liste des items + le total
            Panel panelBody = LibrairieControlesDynamique.divDYN(panelBase, nomEntreprise + "_body", "panel-body");


            // Rajouter les produits dans le panier
            int idItem = 0;
            for (int i = 0; i < 3; i++)
            {
                idItem++;
                int quantiteSelectionne = 1;
                Double prix = 1300.99;

                // sum au sous total
                sousTotal += prix;

                String nomProduit = "MacBook Air 13\", 256GB SSD - Rose Gold";
                String urlImage = "../static/images/macbookair13.jpg";

                Panel rowItem = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowItem_" + idItem, "row");

                // ajouter l'image
                Panel colImg = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colImg_" + idItem, "col-sm-2");
                LibrairieControlesDynamique.imgDYN(colImg, nomEntreprise + "_img_" + idItem, urlImage, "img-size");

                // Nom du produit
                Panel colNom = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colNom_" + idItem, "col-sm-4");
                LibrairieControlesDynamique.lblDYN(colNom, nomEntreprise + "_nom_" + idItem, nomProduit, "nom-item");

                // Quantité sélectionné
                Panel colQuantite = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colQuantite_" + idItem, "col-sm-4");
                LibrairieControlesDynamique.tbDYN(colQuantite, nomEntreprise + "_quantite_" + idItem, quantiteSelectionne.ToString(), "form-control border-quantite");

                // Prix item
                Panel colPrix = LibrairieControlesDynamique.divDYN(rowItem, nomEntreprise + "_colPrix_" + idItem, "col-sm-2");
                LibrairieControlesDynamique.lblDYN(colPrix, nomEntreprise + "_prix_" + idItem, "$" + prix.ToString(), "prix_item");

                // Bouton retirer
                Panel rowBtnRetirer = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowBtnRetirer_" + idItem, "row");
                Panel colBtnRetirer = LibrairieControlesDynamique.divDYN(rowBtnRetirer, nomEntreprise + "_colBtnRetirer_" + idItem, "col-sm-2");
                LibrairieControlesDynamique.btnDYN(colBtnRetirer, nomEntreprise + "_btnRetirer_" + idItem, "btn btn-default", "RETIRER");
                LibrairieControlesDynamique.hrDYN(panelBody);
            }

            // Afficher le sous total
            Panel rowSousTotal = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowSousTotal", "row");
            Panel colLabelSousTotal = LibrairieControlesDynamique.divDYN(rowSousTotal, nomEntreprise + "_colLabelSousTotal", "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colLabelSousTotal, nomEntreprise + "_labelSousTotal", "Sous total: ", "infos-payage");

            Panel colMontantSousTotal = LibrairieControlesDynamique.divDYN(rowSousTotal, nomEntreprise + "_colMontantSousTotal", "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colMontantSousTotal, nomEntreprise + "_montantSousTotal", "$" + sousTotal.ToString(), "infos-payage");

            LibrairieControlesDynamique.hrDYN(panelBody);

            // Afficher la TPS
            Panel rowTPS = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowTPS", "row");
            Panel colLabelTPS = LibrairieControlesDynamique.divDYN(rowTPS, nomEntreprise + "_colLabelTPS", "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colLabelTPS, nomEntreprise + "_labelTPS", "TPS: ", "infos-payage");

            Panel colMontantTPS = LibrairieControlesDynamique.divDYN(rowTPS, nomEntreprise + "_colMontantTPS", "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colMontantTPS, nomEntreprise + "_montantTPS", "$" + TPS.ToString(), "infos-payage");

            LibrairieControlesDynamique.hrDYN(panelBody);

            // Afficher la TVQ
            Panel rowTVQ = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowTVQ", "row");
            Panel colLabelTVQ = LibrairieControlesDynamique.divDYN(rowTVQ, nomEntreprise + "_colLabelTVQ", "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colLabelTVQ, nomEntreprise + "_labelTVQ", "TVQ: ", "infos-payage");

            Panel colMontantTVQ = LibrairieControlesDynamique.divDYN(rowTVQ, nomEntreprise + "_colMontantTVQ", "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colMontantTVQ, nomEntreprise + "_montantTVQ", "$" + TVQ.ToString(), "infos-payage");

            LibrairieControlesDynamique.hrDYN(panelBody);

            // Frais livraison
            Panel rowLivraison = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowLivraison", "row");
            Panel colLabelLivraison = LibrairieControlesDynamique.divDYN(rowLivraison, nomEntreprise + "_colLabelLivraison", "col-sm-4");
            LibrairieControlesDynamique.lblDYN(colLabelLivraison, nomEntreprise + "_labelLivraison", "Des frais de livraison pourraient s'appliquer", "avertissement-livraison");
            LibrairieControlesDynamique.hrDYN(panelBody);

            // Bouton commander
            Panel rowBtnCommander = LibrairieControlesDynamique.divDYN(panelBody, nomEntreprise + "_rowBtnCommander", "row");
            Panel colLabelBtnCommander = LibrairieControlesDynamique.divDYN(rowBtnCommander, nomEntreprise + "_colLabelBtnCommander", "col-sm-4");
            LibrairieControlesDynamique.btnDYN(colLabelBtnCommander, nomEntreprise + "_btnCommander", "btn btn-warning", "Commander");
        }

    }
}