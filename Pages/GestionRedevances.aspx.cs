using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_GestionRedevances : System.Web.UI.Page
{
    private Dictionary<long, TextBox> lstRedevances = new Dictionary<long, TextBox>();
    private PPVendeurs vendeur;
    private String etape;
    private Decimal taux;
    private String emailErreur;

    private TextBox objet;
    private HtmlTextArea message;

    protected void Page_Load(object sender, EventArgs e)
    {
        getEtape();
        

        if (etape == "All") {
            getEmailErreur();
            afficherVendeursModifiables();
            if (emailErreur == "OK")
            {
                divSuccessCourriel.Visible = true;
                lblSuccessCourriel.Text = "Le courriel a été envoyé avec succès.";
            }
        }
        else if (etape == "Courriel") {
            getVendeur();
            getTaux();
            if (this.vendeur == null || this.taux == -1)
            {
                String url = "~/Pages/GestionRedevances.aspx?";
                Response.Redirect(url, true);
            }

            afficherFormCourriel();
        }
        
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
            Panel colUser = LibrairieControlesDynamique.divDYN(row, "col_" + idVendeur, "col-md-3");
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
            lstRedevances.Add(idVendeur, tb);
        }

        if (lstVendeurs.Count() == 0)
        {
            divMessage.Visible = true;
        }

    }

    public void afficherFormCourriel()
    {
        divToolBar.Visible = false;

        LibrairieControlesDynamique.btnDYN(phDynamique, "", "btn btn-warning", "Retour", retourRedevance_click);
        LibrairieControlesDynamique.brDYN(phDynamique);
        LibrairieControlesDynamique.brDYN(phDynamique);

        LibrairieControlesDynamique.lblDYN(phDynamique, "", "Destinataire :").Style.Add("font-size", "20px");
        Panel panelDefault = LibrairieControlesDynamique.divDYN(phDynamique, "", "panel panel-default");
        Panel panelBody = LibrairieControlesDynamique.divDYN(panelDefault, "", "panel-body");

        LibrairieControlesDynamique.spaceDYN(panelBody);
        Label lbl = LibrairieControlesDynamique.lblDYN(panelBody, "", vendeur.Prenom + " " + vendeur.Nom, "badge");
        lbl.Style.Add("background-color", "orange !important");

        // objet
        Panel divObjet = LibrairieControlesDynamique.divDYN(phDynamique, "", "form-group");
        LibrairieControlesDynamique.lblDYN(divObjet, "", "Objet :");
        this.objet = LibrairieControlesDynamique.tbDYN(divObjet, "", "Modification taux de redevance", "form-control");

        // message
        Panel divMessage = LibrairieControlesDynamique.divDYN(phDynamique, "", "form-group");
        LibrairieControlesDynamique.lblDYN(divObjet, "", "Message :");
        this.message = LibrairieControlesDynamique.textAreaDYN(divObjet, "", 10, "form-control", "Votre taux de redevance vient d'être modifié. Le taux a été réévalué à " + this.taux + "%.");

        // bouton envoyer
        LibrairieControlesDynamique.btnDYN(phDynamique, "", "btn btn-warning", "Envoyer", envoyerCourrielAccepte_click);

    }

    public void retourDashboard_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Retour");
        String url = "~/Pages/AcceuilGestionnaire.aspx?";
        Response.Redirect(url, true);
    }

    public void envoyerCourrielAccepte_click(Object sender, EventArgs e)
    {
        if (objet.Text != "" && message.Value != "")
        {
            LibrairieLINQ.modifierRedevanceVendeur(this.vendeur.NoVendeur, taux);
            LibrairieCourriel.envoyerCourriel("ppuces@gmail.com", vendeur.AdresseEmail, this.objet.Text, this.message.Value);
            String url = "~/Pages/GestionRedevances.aspx?Email=OK";
            Response.Redirect(url, true);
        }
        else
        {
            divErreurCourriel.Visible = true;
            lblErreurCourriel.Text = "Le courriel doit contenir un objet ainsi qu'un corps.";
        }


    }

    public void btnConfirmer_click(Object sender, EventArgs e)
    {
        HtmlButton btn = (HtmlButton)sender;
        String id = btn.ID.Replace("btnConfirmer_", "");
        TextBox tb = new TextBox();

        Decimal n;
        if (lstRedevances.TryGetValue(long.Parse(id), out tb) && Decimal.TryParse(tb.Text, out n) && n >= 0 && n <= 100)
        {
            // faire requête qui change le pourcentage de redevance
            tb.CssClass = "form-control";
            String url = "~/Pages/GestionRedevances.aspx?Etape=Courriel&NoVendeur=" + id + "&Taux=" + tb.Text;
            Response.Redirect(url, true);
        }
        else
        {
            tb.CssClass = "form-control erreur";
        }
    }

    public void retourRedevance_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Retour");
        String url = "~/Pages/GestionRedevances.aspx?";
        Response.Redirect(url, true);
    }

    private void getVendeur()
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableVendeurs = dataContext.PPVendeurs;

        long n = 0;
        if (Request.QueryString["NoVendeur"] == null || !long.TryParse(Request.QueryString["NoVendeur"], out n))
        {
            this.vendeur = null;
        }
        else
        {
            var v = from vend in tableVendeurs where vend.NoVendeur == n select vend;
            if (v.Count() > 0 && v.First().PPCommandes.Count() < 1 && v.First().Statut == 1)
            {
                this.vendeur = v.First();
            }
            else
            {
                this.vendeur = null;
            }
        }
    }

    private void getEtape()
    {
        if (Request.QueryString["Etape"] == null || Request.QueryString["Etape"] != "Courriel")
        {
            this.etape = "All";
        }
        else
        {
            this.etape = Request.QueryString["Etape"];
        }
    }

    private void getTaux()
    {
        Decimal t = -1;
        if (Request.QueryString["Taux"] == null || !Decimal.TryParse(Request.QueryString["Taux"], out t))
        {
           this.taux = -1;
        }
        else
        {
            if (t < 0 || t > 100) { this.taux = -1; }
            else
            {
                this.taux = int.Parse(Request.QueryString["Taux"]);
            }
        }
    }

    private void getEmailErreur()
    {
        if (Request.QueryString["Email"] == null)
        {
            this.emailErreur = "";
        }
        else
        {
            this.emailErreur = Request.QueryString["Email"];
        }
    }
}