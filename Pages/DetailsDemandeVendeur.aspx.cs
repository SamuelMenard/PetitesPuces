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
    private String etape;
    private TextBox tbRedevance;

    protected void Page_Load(object sender, EventArgs e)
    {
        getEtape();
        getNoVendeur();
        if (etape == "")
        {
            afficherInfosClient();
        }
        else if (etape == "redevance")
        {
            afficherFormRedevance();
        }
        
    }

    public void afficherInfosClient()
    {
        LibrairieControlesDynamique.btnDYN(phDynamique, "", "btn btn-warning", "Retour", retourDashboard_click);
        LibrairieControlesDynamique.brDYN(phDynamique);
        LibrairieControlesDynamique.brDYN(phDynamique);

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
        LibrairieControlesDynamique.lblDYN(colLBLEmail, "", "Courriel:", "autres-infos");
        Panel colEmail = LibrairieControlesDynamique.divDYN(rowAutresInfos, "", "col-md-8");
        LibrairieControlesDynamique.lblDYN(colEmail, "", vendeur.AdresseEmail, "autres-infos");

        Panel colLBLTel = LibrairieControlesDynamique.divDYN(rowAutresInfos, "", "col-md-4");
        LibrairieControlesDynamique.lblDYN(colLBLTel, "", "Téléphone:", "autres-infos");
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
        LibrairieControlesDynamique.lblDYN(colLivGratuite, "", (vendeur.LivraisonGratuite != null) ? Decimal.Round((decimal)vendeur.LivraisonGratuite, 2).ToString() : "", "autres-infos");

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
        HtmlButton btnOui = LibrairieControlesDynamique.htmlbtnDYN(colBtnOui, "btnOui_" + vendeur.NoVendeur, "btn btn-success", "", "glyphicon glyphicon-ok", btnOui_click);

        LibrairieControlesDynamique.spaceDYN(rowBoutons);

        // btn non
        HtmlButton btnNon = LibrairieControlesDynamique.htmlbtnDYN(colBtnNon, "btnNon_" + vendeur.NoVendeur, "btn btn-danger", "", "glyphicon glyphicon-remove", btnNon_click);
        

    }

    public void afficherFormRedevance()
    {
        LibrairieControlesDynamique.btnDYN(phDynamique, "", "btn btn-warning", "Retour", retourDetails_click);
        LibrairieControlesDynamique.brDYN(phDynamique);
        LibrairieControlesDynamique.brDYN(phDynamique);

        PPVendeurs vendeur = LibrairieLINQ.getInfosVendeur(this.noVendeur);

        // créer le tableau avec les infos
        Panel panelBase = LibrairieControlesDynamique.divDYN(phDynamique, "", "panel panel-default");
        Panel panelBody = LibrairieControlesDynamique.divDYN(panelBase, "", "panel-body");

        Panel row = LibrairieControlesDynamique.divDYN(panelBody, "", "row");
        Panel colNomEntreprise = LibrairieControlesDynamique.divDYN(row, "", "col-md-12");
        LibrairieControlesDynamique.lblDYN(colNomEntreprise, "", vendeur.NomAffaires, "header-nom");

        Panel divHR = LibrairieControlesDynamique.divDYN(row, "", "col-md-12");
        LibrairieControlesDynamique.hrDYN(divHR);

        
        Panel colLblRedevances = LibrairieControlesDynamique.divDYN(row, "", "col-md-12");
        Panel rowPourcentage = LibrairieControlesDynamique.divDYN(colLblRedevances, "", "row");

        Panel colLBL = LibrairieControlesDynamique.divDYN(rowPourcentage, "", "col-md-3");
        LibrairieControlesDynamique.lblDYN(colLBL, "", "Redevances par commande", "autres-infos");
        LibrairieControlesDynamique.spaceDYN(colLblRedevances);

        Panel colTB = LibrairieControlesDynamique.divDYN(rowPourcentage, "", "col-md-2");
        Panel div = LibrairieControlesDynamique.divDYN(colTB, "", "input-group");
        LibrairieControlesDynamique.lblDYN(div, "", "%", "input-group-addon");
        TextBox tb = LibrairieControlesDynamique.numericUpDownDYN(div, "", "", "0", "100", "form-control");
        tb.Style.Add("width", "70px");
        tb.MaxLength = 3;

        this.tbRedevance = tb;


        Panel colMessageConfirmation = LibrairieControlesDynamique.divDYN(row, "", "col-md-12");
        LibrairieControlesDynamique.brDYN(colMessageConfirmation);
        LibrairieControlesDynamique.brDYN(colMessageConfirmation);
        LibrairieControlesDynamique.lblDYN(colMessageConfirmation, "", "En continuant vous acceptez que " + vendeur.Prenom + " " + vendeur.Nom + 
            " ait un accès complet comme vendeur sur le site Les Petites Puces.", "autres-infos");

        LibrairieControlesDynamique.brDYN(colMessageConfirmation);
        LibrairieControlesDynamique.brDYN(colMessageConfirmation);
        
        LibrairieControlesDynamique.btnDYN(LibrairieControlesDynamique.divDYN(row, "", "col-md-12"), "btnConf_" + vendeur.NoVendeur, "btn btn-warning", "Confirmer", confirmer_click);
    }

    public void retourDashboard_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Retour");
        String url = "~/Pages/DemandesVendeur.aspx?";
        Response.Redirect(url, true);
    }

    public void retourDetails_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Retour");
        String url = "~/Pages/DetailsDemandeVendeur.aspx?NoVendeur=" + this.noVendeur;
        Response.Redirect(url, true);
    }

    public void btnNon_click(Object sender, EventArgs e)
    {
        HtmlButton btn = (HtmlButton)sender;
        String id = btn.ID.Replace("btnNon_", "");
        LibrairieLINQ.accepterOuDeleteDemandeVendeur(long.Parse(id), false, "");
        String url = "~/Pages/DemandesVendeur.aspx?Notification=refuse";
        Response.Redirect(url, true);
    }

    public void btnOui_click(Object sender, EventArgs e)
    {
        HtmlButton btn = (HtmlButton)sender;
        String id = btn.ID.Replace("btnOui_", "");
        String url = "~/Pages/DetailsDemandeVendeur.aspx?NoVendeur=" + this.noVendeur + "&Etape=redevance";
        Response.Redirect(url, true);
    }

    public void confirmer_click(Object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        String id = btn.ID.Replace("btnConf_", "");
        Decimal n;
        if (Decimal.TryParse(this.tbRedevance.Text, out n) && n >= 0 && n <= 100)
        {
            LibrairieLINQ.accepterOuDeleteDemandeVendeur(long.Parse(id), true, this.tbRedevance.Text);
            String url = "~/Pages/DemandesVendeur.aspx?Notification=accepte";
            Response.Redirect(url, true);
        }
        else
        {
            this.tbRedevance.CssClass = "form-control erreur";
        }
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

    private void getEtape()
    {
        if (Request.QueryString["Etape"] == null)
        {
            this.etape = "";
        }
        else
        {
            this.etape = Request.QueryString["Etape"];
        }
    }
}