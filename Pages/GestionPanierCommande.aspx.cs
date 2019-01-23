using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_GestionPanierCommande : System.Web.UI.Page
{
    static string prevPage = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;
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
            Double sousTotal = 0;
            Double TPS = 0;
            Double TVQ = 0;
            Double pourcentageTVQ = 0.09975;
            Double pourcentageTPS = 0.05;

            // Créer le panier du vendeur X
            Panel panelBase = LibrairieControlesDynamique.divDYN(phDynamique, idEntreprise + "_base", "panel panel-default");

            // Nom de l'entreprise
            Panel panelHeader = LibrairieControlesDynamique.divDYN(panelBase, idEntreprise + "_header", "panel-heading");
            LibrairieControlesDynamique.lblDYN(panelHeader, idEntreprise + "_nom", nomEntreprise, "nom-entreprise");

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

            // Afficher la TPS
            // calculer le prix tps
            TPS = sousTotal * pourcentageTPS;
            TPS = Math.Round(TPS, 2);

            Panel rowTPS = LibrairieControlesDynamique.divDYN(panelBody, idEntreprise + "_rowTPS", "row");
            Panel colLabelTPS = LibrairieControlesDynamique.divDYN(rowTPS, idEntreprise + "_colLabelTPS", "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colLabelTPS, idEntreprise + "_labelTPS", "TPS: ", "infos-payage");

            Panel colMontantTPS = LibrairieControlesDynamique.divDYN(rowTPS, idEntreprise + "_colMontantTPS", "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colMontantTPS, idEntreprise + "_montantTPS", "$" + TPS.ToString(), "infos-payage");

            LibrairieControlesDynamique.hrDYN(panelBody);

            // Afficher la TVQ
            // calculer le prix tvq
            TVQ = sousTotal * pourcentageTVQ;
            TVQ = Math.Round(TVQ, 2);

            Panel rowTVQ = LibrairieControlesDynamique.divDYN(panelBody, idEntreprise + "_rowTVQ", "row");
            Panel colLabelTVQ = LibrairieControlesDynamique.divDYN(rowTVQ, idEntreprise + "_colLabelTVQ", "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colLabelTVQ, idEntreprise + "_labelTVQ", "TVQ: ", "infos-payage");

            Panel colMontantTVQ = LibrairieControlesDynamique.divDYN(rowTVQ, idEntreprise + "_colMontantTVQ", "col-sm-2");
            LibrairieControlesDynamique.lblDYN(colMontantTVQ, idEntreprise + "_montantTVQ", "$" + TVQ.ToString(), "infos-payage");

            LibrairieControlesDynamique.hrDYN(panelBody);

            // Frais livraison
            Panel rowLivraison = LibrairieControlesDynamique.divDYN(panelBody, idEntreprise + "_rowLivraison", "row");
            Panel colLabelLivraison = LibrairieControlesDynamique.divDYN(rowLivraison, idEntreprise + "_colLabelLivraison", "col-sm-4");
            LibrairieControlesDynamique.lblDYN(colLabelLivraison, idEntreprise + "_labelLivraison", "Des frais de livraison pourraient s'appliquer", "avertissement-livraison");
            LibrairieControlesDynamique.hrDYN(panelBody);

            // Bouton commander
            Panel rowBtnCommander = LibrairieControlesDynamique.divDYN(panelBody, idEntreprise + "_rowBtnCommander", "row");
            Panel colLabelBtnCommander = LibrairieControlesDynamique.divDYN(rowBtnCommander, idEntreprise + "_colLabelBtnCommander", "col-sm-4");
            LibrairieControlesDynamique.btnDYN(colLabelBtnCommander, idEntreprise + "_btnCommander", "btn btn-warning", "Commander", commander_click);
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
}