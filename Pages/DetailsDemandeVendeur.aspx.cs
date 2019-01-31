using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_DetailsDemandeVendeur : System.Web.UI.Page
{
    private long noVendeur;

    protected void Page_Load(object sender, EventArgs e)
    {
        getNoVendeur();
        afficherInfosClient();
    }

    public void afficherInfosClient()
    {
        PPVendeurs vendeur = LibrairieLINQ.getInfosVendeur(this.noVendeur);

        // créer le tableau avec les infos
        Panel panelBase = LibrairieControlesDynamique.divDYN(phDynamique, "", "panel panel-default");
        Panel panelBody = LibrairieControlesDynamique.divDYN(panelBase, "", "panel-body");
        Panel panelFooter = LibrairieControlesDynamique.divDYN(panelBase, "", "panel-footer");

        Panel rowVendeur = LibrairieControlesDynamique.divDYN(panelBody, "", "row");

        // section image
        Panel colPhoto = LibrairieControlesDynamique.divDYN(rowVendeur, "", "col-md-3");
        LibrairieControlesDynamique.imgDYN(colPhoto, "", "../static/images/user-management.png", "img-rounded").Style.Add("width", "180px");
        

        // section informations
        Panel colInfos = LibrairieControlesDynamique.divDYN(rowVendeur, "", "col-md-9");
        Panel rowInfos = LibrairieControlesDynamique.divDYN(colInfos, "", "row");

        Panel colHeader = LibrairieControlesDynamique.divDYN(rowInfos, "", "col-md-12 align-left");
        Label lblHeaderNom = LibrairieControlesDynamique.lblDYN(colHeader, "", vendeur.NomAffaires, "header-nom");
        LibrairieControlesDynamique.hrDYN(LibrairieControlesDynamique.divDYN(rowInfos, "", "col-md-12"));

        Panel colAutreInfos = LibrairieControlesDynamique.divDYN(rowInfos, "", "col-md-11");
        Panel rowAutresInfos = LibrairieControlesDynamique.divDYN(colAutreInfos, "", "row align-left");

        Panel colLBLNom = LibrairieControlesDynamique.divDYN(rowAutresInfos, "", "col-md-4");
        LibrairieControlesDynamique.lblDYN(colLBLNom, "", "Nom:", "autres-infos");
        Panel colNom = LibrairieControlesDynamique.divDYN(rowAutresInfos, "", "col-md-8");
        LibrairieControlesDynamique.lblDYN(colNom, "", vendeur.Prenom + " " + vendeur.Nom, "autres-infos");

        Panel colLBLEmail = LibrairieControlesDynamique.divDYN(rowAutresInfos, "", "col-md-4");
        LibrairieControlesDynamique.lblDYN(colLBLEmail, "", "Courriel::", "autres-infos");
        Panel colEmail = LibrairieControlesDynamique.divDYN(rowAutresInfos, "", "col-md-8");
        LibrairieControlesDynamique.lblDYN(colEmail, "", vendeur.AdresseEmail, "autres-infos");

        Panel colLBLTel = LibrairieControlesDynamique.divDYN(rowAutresInfos, "", "col-md-4");
        LibrairieControlesDynamique.lblDYN(colLBLTel, "", "Courriel::", "autres-infos");
        Panel colTel = LibrairieControlesDynamique.divDYN(rowAutresInfos, "", "col-md-8");
        LibrairieControlesDynamique.lblDYN(colTel, "", vendeur.Tel1, "autres-infos");

        Panel colLBLAdresse = LibrairieControlesDynamique.divDYN(rowAutresInfos, "", "col-md-4");
        LibrairieControlesDynamique.lblDYN(colLBLAdresse, "", "Adresse:", "autres-infos");
        Panel colAdresse = LibrairieControlesDynamique.divDYN(rowAutresInfos, "", "col-md-8");
        LibrairieControlesDynamique.lblDYN(colAdresse, "", vendeur.Rue + ", " + vendeur.Ville + ", " + vendeur.Province + ", " + vendeur.Pays, "autres-infos");

        LibrairieControlesDynamique.brDYN(rowAutresInfos);
        LibrairieControlesDynamique.brDYN(rowAutresInfos);

        Panel colLBLNoVendeur = LibrairieControlesDynamique.divDYN(rowAutresInfos, "", "col-md-4");
        LibrairieControlesDynamique.lblDYN(colLBLNoVendeur, "", "No vendeur:", "autres-infos");
        Panel colNoVendeur = LibrairieControlesDynamique.divDYN(rowAutresInfos, "", "col-md-8");
        LibrairieControlesDynamique.lblDYN(colNoVendeur, "", vendeur.NoVendeur.ToString(), "autres-infos");

        Panel colLBLPoids = LibrairieControlesDynamique.divDYN(rowAutresInfos, "", "col-md-4");
        LibrairieControlesDynamique.lblDYN(colLBLPoids, "", "Poids maximal:", "autres-infos");
        Panel colPoids = LibrairieControlesDynamique.divDYN(rowAutresInfos, "", "col-md-8");
        LibrairieControlesDynamique.lblDYN(colPoids, "", vendeur.PoidsMaxLivraison.ToString() + " lbs", "autres-infos");

        Panel colLBLLivGratuite = LibrairieControlesDynamique.divDYN(rowAutresInfos, "", "col-md-4");
        LibrairieControlesDynamique.lblDYN(colLBLLivGratuite, "", "Livraison gratuite:", "autres-infos");
        Panel colLivGratuite = LibrairieControlesDynamique.divDYN(rowAutresInfos, "", "col-md-8");
        LibrairieControlesDynamique.lblDYN(colLivGratuite, "", vendeur.LivraisonGratuite.ToString(), "autres-infos");

        Panel colLBLTaxes = LibrairieControlesDynamique.divDYN(rowAutresInfos, "", "col-md-4");
        LibrairieControlesDynamique.lblDYN(colLBLTaxes, "", "Taxes:", "autres-infos");
        Panel colTaxes = LibrairieControlesDynamique.divDYN(rowAutresInfos, "", "col-md-8");
        LibrairieControlesDynamique.lblDYN(colTaxes, "", (vendeur.Taxes == true) ? "Oui" : "Non", "autres-infos");

        Panel colLBLDateCreation = LibrairieControlesDynamique.divDYN(rowAutresInfos, "", "col-md-4");
        LibrairieControlesDynamique.lblDYN(colLBLDateCreation, "", "Date création:", "autres-infos");
        Panel colDateCreation = LibrairieControlesDynamique.divDYN(rowAutresInfos, "", "col-md-8");
        LibrairieControlesDynamique.lblDYN(colDateCreation, "", vendeur.DateCreation.ToString(), "autres-infos");

        Panel rowBoutons = LibrairieControlesDynamique.divDYN(panelFooter, "", "row");
        Panel colBtnOui = LibrairieControlesDynamique.divDYN(rowBoutons, "", "col-md-1");
        Panel colBtnNon = LibrairieControlesDynamique.divDYN(rowBoutons, "", "col-md-1");

        // btn oui
        HtmlButton btnOui = LibrairieControlesDynamique.htmlbtnDYN(colBtnOui, "", "btn btn-success", "", "glyphicon glyphicon-ok", btnOui_click);

        LibrairieControlesDynamique.spaceDYN(rowBoutons);

        // btn non
        HtmlButton btnNon = LibrairieControlesDynamique.htmlbtnDYN(colBtnNon, "", "btn btn-danger", "", "glyphicon glyphicon-remove", btnNon_click);






    }

    public void retourDashboard_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Retour");
        String url = "~/Pages/DemandesVendeur.aspx?";
        Response.Redirect(url, true);
    }

    public void btnNon_click(Object sender, EventArgs e)
    {
        HtmlButton btn = (HtmlButton)sender;
        String id = btn.ID.Replace("btnNon_", "");
        LibrairieLINQ.accepterOuDeleteDemandeVendeur(long.Parse(id), false);
        String url = "~/Pages/DemandesVendeur.aspx?";
        Response.Redirect(url, true);
    }

    public void btnOui_click(Object sender, EventArgs e)
    {
        HtmlButton btn = (HtmlButton)sender;
        String id = btn.ID.Replace("btnOui_", "");
        LibrairieLINQ.accepterOuDeleteDemandeVendeur(long.Parse(id), true);
        String url = "~/Pages/DemandesVendeur.aspx?";
        Response.Redirect(url, true);
    }

    private void getNoVendeur()
    {
        long n;
        if (Request.QueryString["NoVendeur"] == null || !long.TryParse(Request.QueryString["NoVendeur"], out n))
        {
            String url = "~/Pages/DemandesVendeur.aspx?";
            Response.Redirect(url, true);
        }
        else
        {
            this.noVendeur = n;
        }
    }
}