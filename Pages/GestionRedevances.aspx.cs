using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_GestionRedevances : System.Web.UI.Page
{

    private TextBox tbRedevance;
    protected void Page_Load(object sender, EventArgs e)
    {
        afficherVendeursModifiables();
    }

    public void afficherVendeursModifiables()
    {
        String urlImg = "../static/images/vendeur.jpg";
        List<PPVendeurs> lstVendeurs = LibrairieLINQ.getVendeursAvecRedevanceModifiable();
        Panel row = LibrairieControlesDynamique.divDYN(phDynamique, "row_utilisateurs", "row");
        foreach (PPVendeurs vendeur in lstVendeurs)
        {
            long idVendeur = vendeur.NoVendeur;
            String nomUtil = vendeur.Prenom + " " + vendeur.Nom;
            Panel colUser = LibrairieControlesDynamique.divDYN(row, "col_" + idVendeur, "col-md-2");
            Panel panelDefault = LibrairieControlesDynamique.divDYN(colUser, "", "panel panel-default");
            panelDefault.Style.Add("width", "200px");

            Panel panelHeader = LibrairieControlesDynamique.divDYN(panelDefault, "", "panel-heading");
            LibrairieControlesDynamique.h4DYN(panelHeader, "h4_" + idVendeur, nomUtil);

            Panel panelBody = LibrairieControlesDynamique.divDYN(panelDefault, "", "panel-body");
            LibrairieControlesDynamique.imgDYN(panelBody, "", urlImg, "img-responsive");

            Panel panelFooter = LibrairieControlesDynamique.divDYN(panelDefault, "", "panel-footer");
            Panel rowBtnTb = LibrairieControlesDynamique.divDYN(panelFooter, "", "row");
            Panel colBtn = LibrairieControlesDynamique.divDYN(rowBtnTb, "", "col-md-4");
            Panel colTB = LibrairieControlesDynamique.divDYN(rowBtnTb, "", "col-md-8");

            HtmlButton btnNon = LibrairieControlesDynamique.htmlbtnDYN(colBtn, "btnConfirmer_" + idVendeur, "btn btn-success", "", "glyphicon glyphicon-ok", btnConfirmer_click);
            Panel div = LibrairieControlesDynamique.divDYN(colTB, "", "input-group");
            LibrairieControlesDynamique.lblDYN(div, "", "%", "input-group-addon");
            TextBox tb = LibrairieControlesDynamique.numericUpDownDYN(div, "", Decimal.Round((Decimal)vendeur.Pourcentage, 0).ToString(), "0", "100", "form-control");
            tb.MaxLength = 3;
            this.tbRedevance = tb;
        }

        if (lstVendeurs.Count() == 0)
        {
            divMessage.Visible = true;
        }

    }

    public void retourDashboard_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Retour");
        String url = "~/Pages/AcceuilGestionnaire.aspx?";
        Response.Redirect(url, true);
    }

    public void btnConfirmer_click(Object sender, EventArgs e)
    {
        HtmlButton btn = (HtmlButton)sender;
        String id = btn.ID.Replace("btnConfirmer_", "");
        Decimal n;
        if (Decimal.TryParse(this.tbRedevance.Text, out n) && n >= 0 && n <= 100)
        {
            // faire requête qui change le pourcentage de redevance
            this.tbRedevance.CssClass = "form-control";
            LibrairieLINQ.modifierRedevanceVendeur(long.Parse(id), Decimal.Parse(this.tbRedevance.Text));
        }
        else
        {
            this.tbRedevance.CssClass = "form-control erreur";
        }
        

    }
}