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
            case "livraison": afficherLivraison(); break;
            case "bon": afficherBon(); break;
        }

    }

    public void afficherPanier()
    {
        // masquer le formulaire de livraison
        divLivraison.Visible = false;

        // masquer la div de paiement
        div_paiement.Visible = false;

        String nomEntreprise = "Apple";
        Double sousTotal = 0;
        Double TPS = 0;
        Double TVQ = 0;
        Double apresTaxes = 0;
        Double pourcentageTVQ = 0.09975;
        Double pourcentageTPS = 0.05;
        Double pourcentageTaxes = pourcentageTVQ + pourcentageTPS + 1;

        // Créer le panier du vendeur X
        Panel panelBase = LibrairieControlesDynamique.divDYN(phDynamique, idEntreprise + "_base", "panel panel-default");

        // Nom de l'entreprise
        Panel panelHeader = LibrairieControlesDynamique.divDYN(panelBase, idEntreprise + "_header", "panel-heading");
        LibrairieControlesDynamique.lblDYN(panelHeader, idEntreprise + "_nom", nomEntreprise, "nom-entreprise");

        // Liste des items + le total
        Panel panelBody = LibrairieControlesDynamique.divDYN(panelBase, idEntreprise + "_body", "panel-body");


        // Rajouter les produits dans le panier
        // temporaire
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

        // Afficher apres taxes
        // calculer le montant apres taxes
        apresTaxes = sousTotal * pourcentageTaxes;
        apresTaxes = Math.Round(apresTaxes, 2);

        Panel rowApresTaxes = LibrairieControlesDynamique.divDYN(panelBody, idEntreprise + "_rowApresTaxes", "row");
        Panel colLabelApresTaxes = LibrairieControlesDynamique.divDYN(rowApresTaxes, idEntreprise + "_colLabelApresTaxes", "col-sm-2");
        LibrairieControlesDynamique.lblDYN(colLabelApresTaxes, idEntreprise + "_labelApresTaxes", "Après taxes: ", "infos-payage");

        Panel colMontantApresTaxes = LibrairieControlesDynamique.divDYN(rowApresTaxes, idEntreprise + "_colMontantApresTaxes", "col-sm-2");
        LibrairieControlesDynamique.lblDYN(colMontantApresTaxes, idEntreprise + "_montantApresTaxes", "$" + apresTaxes.ToString(), "infos-payage");

        LibrairieControlesDynamique.hrDYN(panelBody);

        // Frais livraison
        Panel rowLivraison = LibrairieControlesDynamique.divDYN(panelBody, idEntreprise + "_rowLivraison", "row");
        Panel colLabelLivraison = LibrairieControlesDynamique.divDYN(rowLivraison, idEntreprise + "_colLabelLivraison", "col-sm-4");
        LibrairieControlesDynamique.lblDYN(colLabelLivraison, idEntreprise + "_labelLivraison", "Des frais de livraison pourraient s'appliquer", "avertissement-livraison");
        LibrairieControlesDynamique.hrDYN(panelBody);

        // Bouton shipping
        Panel rowBtnShipping = LibrairieControlesDynamique.divDYN(panelBody, idEntreprise + "_rowBtnShipping", "row");
        Panel colLabelBtnShipping = LibrairieControlesDynamique.divDYN(rowBtnShipping, idEntreprise + "_colLabelBtnShipping", "col-sm-2");
        HtmlButton btnShipping = LibrairieControlesDynamique.htmlbtnDYN(colLabelBtnShipping, idEntreprise + "_BtnShipping", "btn btn-warning", "Informations de livraison", "glyphicon glyphicon-plane", shipping_click);

    }

    public void afficherLivraison()
    {
        // masquer la div de paiement
        div_paiement.Visible = false;

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

        // rendre visible la division
        divLivraison.Visible = true;

    }

    public void afficherPaiement()
    {
        // masquer la div de livraison
        divLivraison.Visible = false;

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
        Boolean reussi = true;

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
        String itemID = btn.ID.Replace("btnRetirer_", "");
        System.Diagnostics.Debug.WriteLine(itemID);
    }

    public void update_click(Object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        String itemID = btn.ID.Replace("update_", "");
        System.Diagnostics.Debug.WriteLine(itemID);
    }

    public void shipping_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/SaisieCommande.aspx?IDEntreprise=" + this.idEntreprise + "&Etape=livraison";
        Response.Redirect(url, true);
    }

    public void retourPanier_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Retour");
        String url = "~/Pages/SaisieCommande.aspx?IDEntreprise=" + this.idEntreprise;
        Response.Redirect(url, true);
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

}