using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_DemandesVendeur : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;
        afficherDemandes();
    }

    public void afficherDemandes()
    {
        int idVendeur = 0;


        for (int i = 0; i < 5; i++)
        {
            idVendeur++;
            String nomVendeur = "Raphaël Benoît";
            String nomEntreprise = "Skelton Brand";
            String date = DateTime.Now.ToShortDateString();
            String urlImg = "../static/images/user-management.png";

            Panel row = LibrairieControlesDynamique.divDYN(phDynamique, "row_" + idVendeur, "row");

            // infos
            Panel colInfos = LibrairieControlesDynamique.divDYN(row, "colInfos_" + idVendeur, "col-md-8");
            Panel demandeBase = LibrairieControlesDynamique.divDYN(colInfos, "base_" + idVendeur, "panel panel-default");
            Panel demandeBody = LibrairieControlesDynamique.divDYN(demandeBase, "body_" + idVendeur, "panel-body");

            Panel media = LibrairieControlesDynamique.divDYN(demandeBody, "media_" + idVendeur, "media");
            Panel mediaLeft = LibrairieControlesDynamique.divDYN(media, "mediaLeft_" + idVendeur, "media-left");
            Image img = LibrairieControlesDynamique.imgDYN(mediaLeft, "img_" + idVendeur, urlImg, "media-object");
            img.Style.Add("width", "75px");
            Panel mediaBody = LibrairieControlesDynamique.divDYN(media, "mediaBody_" + idVendeur, "media-body");
            LibrairieControlesDynamique.h4DYN(mediaBody, nomVendeur);
            LibrairieControlesDynamique.pDYN(mediaBody, nomEntreprise);
            LibrairieControlesDynamique.pDYN(mediaBody, date);

            // btn oui
            Panel colOuiNon = LibrairieControlesDynamique.divDYN(row, "colOuiNon_" + idVendeur, "col-sm-2");
            HtmlButton btnOui = LibrairieControlesDynamique.htmlbtnDYN(colOuiNon, "btnOui_" + idVendeur, "btn btn-success", "", "glyphicon glyphicon-ok", btnOui_click);
            btnOui.Style.Add("height", "130px");

            LibrairieControlesDynamique.spaceDYN(colOuiNon);

            // btn non
            HtmlButton btnNon = LibrairieControlesDynamique.htmlbtnDYN(colOuiNon, "btnNon_" + idVendeur, "btn btn-danger", "", "glyphicon glyphicon-remove", btnNon_click);
            btnNon.Style.Add("height", "130px");
        }
        
        

    }

    public void btnNon_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Refuser demande");
    }

    public void btnOui_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Accepter demande");
    }

    public void retourDashboard_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Retour");
        String url = "~/Pages/AcceuilGestionnaire.aspx?";
        Response.Redirect(url, true);
    }
}

/*
 <div class="media">
  <div class="media-left">
    <img src="img_avatar1.png" class="media-object" style="width:60px">
  </div>
  <div class="media-body">
    <h4 class="media-heading">John Doe</h4>
    <p>Lorem ipsum...</p>
  </div>
</div>
 * */
