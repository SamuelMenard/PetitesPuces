using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_BoiteMessagerie : System.Web.UI.Page
{
    private List<CheckBox> lstCbVendeurs = new List<CheckBox>();
    private List<CheckBox> lstCbClients = new List<CheckBox>();

    private String trieVendeur;
    private String trieClient;

    protected void Page_Load(object sender, EventArgs e)
    {
        getTrieVendeur();
        getTrieClient();

        afficherTableaux();
    }

    public void afficherTableaux()
    {
        // rajouter les boutons d'option

        Panel colComplet = LibrairieControlesDynamique.divDYN(phChoix, "", "col-md-12");
        Panel row = LibrairieControlesDynamique.divDYN(colComplet, "", "row");
        Panel col1 = LibrairieControlesDynamique.divDYN(row, "", "col-md-1");
        Panel col2 = LibrairieControlesDynamique.divDYN(row, "", "col-md-2");
        LibrairieControlesDynamique.btnDYN(col1, "", "btn btn-warning", "Retour", retourDashboard_click);
        LibrairieControlesDynamique.btnDYN(col2, "", "btn btn-warning", "Confirmer", confirmer_click);

        remplireTableVendeurs();
        remplireTableClients();

        divChoixDestinataires.Visible = true;
        divCourriel.Visible = false;
    }

    public void afficherCourriel()
    {
        // rajouter les boutons d'option
        Panel colComplet = LibrairieControlesDynamique.divDYN(phCourriel, "", "col-md-12");
        Panel row = LibrairieControlesDynamique.divDYN(colComplet, "", "row");
        Panel col1 = LibrairieControlesDynamique.divDYN(row, "", "col-md-1");
        LibrairieControlesDynamique.btnDYN(col1, "", "btn btn-warning", "Retour", retourChoix_click);

        // afficher les destinataires
        foreach(CheckBox cb in lstCbVendeurs)
        {
            if (cb.Checked)
            {
                String id = cb.ID.Replace("cbVendeur_", "");
                PPVendeurs vendeur = LibrairieLINQ.getInfosVendeur(long.Parse(id));
                //Panel col = LibrairieControlesDynamique.divDYN(divDestinataires, "", "col-md-2");
                LibrairieControlesDynamique.spaceDYN(divDestinataires);
                Label lbl = LibrairieControlesDynamique.lblDYN(divDestinataires, "", vendeur.Prenom + " " + vendeur.Nom, "badge");
                lbl.Style.Add("background-color", "orange !important");
                
            }
        }

        foreach (CheckBox cb in lstCbClients)
        {
            if (cb.Checked)
            {
                String id = cb.ID.Replace("cbClient_", "");
                PPClients client = LibrairieLINQ.getFicheInformationsClient(long.Parse(id));
                //Panel col = LibrairieControlesDynamique.divDYN(divDestinataires, "", "col-md-2");
                LibrairieControlesDynamique.spaceDYN(divDestinataires);
                Label lbl = LibrairieControlesDynamique.lblDYN(divDestinataires, "", client.Prenom + " " + client.Nom, "badge");
                lbl.Style.Add("background-color", "orange !important");

            }
        }


        divChoixDestinataires.Visible = false;
        divCourriel.Visible = true;
    }

    public void remplireTableVendeurs()
    {
        List<PPVendeurs> lstVendeurs = LibrairieLINQ.getListeVendeursTrie(this.trieVendeur);
        Table table = LibrairieControlesDynamique.tableDYN(divTableVendeurs, "", "table table-striped");
        TableRow trHEADER = LibrairieControlesDynamique.trDYN(table);
        LibrairieControlesDynamique.lblDYN(LibrairieControlesDynamique.thDYN(trHEADER, "", ""), "", "Nom complet");
        LibrairieControlesDynamique.lblDYN(LibrairieControlesDynamique.thDYN(trHEADER, "", ""), "", "Nom d'affaire");
        LibrairieControlesDynamique.lblDYN(LibrairieControlesDynamique.thDYN(trHEADER, "", ""), "", "Date de cration");
        LibrairieControlesDynamique.lblDYN(LibrairieControlesDynamique.thDYN(trHEADER, "", ""), "", "Ventes");
        LibrairieControlesDynamique.lblDYN(LibrairieControlesDynamique.thDYN(trHEADER, "", ""), "", "✔");

        foreach (PPVendeurs vendeur in lstVendeurs)
        {
            List<PPHistoriquePaiements> lstHisto = LibrairieLINQ.getHistoriquePaiementVendeurs(vendeur.NoVendeur);

            TableRow tr = LibrairieControlesDynamique.trDYN(table);
            LibrairieControlesDynamique.lblDYN(LibrairieControlesDynamique.tdDYN(tr, "", ""), "", vendeur.Prenom + " " + vendeur.Nom);
            LibrairieControlesDynamique.lblDYN(LibrairieControlesDynamique.tdDYN(tr, "", ""), "", vendeur.NomAffaires);
            LibrairieControlesDynamique.lblDYN(LibrairieControlesDynamique.tdDYN(tr, "", ""), "", vendeur.DateCreation.ToString());
            LibrairieControlesDynamique.lblDYN(LibrairieControlesDynamique.tdDYN(tr, "", ""), "", Decimal.Round((Decimal)lstHisto.Sum(histo => histo.MontantVenteAvantLivraison), 2).ToString() + "$");
            lstCbVendeurs.Add(LibrairieControlesDynamique.cb(LibrairieControlesDynamique.tdDYN(tr, "", ""), "cbVendeur_" + vendeur.NoVendeur, ""));
        }
    }

    public void remplireTableClients()
    {
        List<PPClients> lstClients = LibrairieLINQ.getListeClientsTrie(this.trieClient);
        Table table = LibrairieControlesDynamique.tableDYN(divTableClients, "", "table table-striped");
        TableRow trHEADER = LibrairieControlesDynamique.trDYN(table);
        LibrairieControlesDynamique.lblDYN(LibrairieControlesDynamique.thDYN(trHEADER, "", ""), "", "Nom complet");
        LibrairieControlesDynamique.lblDYN(LibrairieControlesDynamique.thDYN(trHEADER, "", ""), "", "Date de cration");
        LibrairieControlesDynamique.lblDYN(LibrairieControlesDynamique.thDYN(trHEADER, "", ""), "", "✔");

        foreach (PPClients client in lstClients)
        {
            TableRow tr = LibrairieControlesDynamique.trDYN(table);
            LibrairieControlesDynamique.lblDYN(LibrairieControlesDynamique.tdDYN(tr, "", ""), "", client.Prenom + " " + client.Nom);
            LibrairieControlesDynamique.lblDYN(LibrairieControlesDynamique.tdDYN(tr, "", ""), "", client.DateCreation.ToString());
            lstCbClients.Add(LibrairieControlesDynamique.cb(LibrairieControlesDynamique.tdDYN(tr, "", ""), "cbClient_" + client.NoClient, ""));
        }
    }

    public void retourDashboard_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Retour");
        String url = "~/Pages/AcceuilGestionnaire.aspx?";
        Response.Redirect(url, true);
    }

    public void retourChoix_click(Object sender, EventArgs e)
    {
        afficherTableaux();
    }

    public void confirmer_click(Object sender, EventArgs e)
    {
        if (lstCbClients.Where(cb => cb.Checked).Any() || lstCbVendeurs.Where(cb => cb.Checked).Any())
        {
            divMessageErreur.Visible = false;
            afficherCourriel();
        }
        else
        {
            divMessageErreur.Visible = true;
        }
        
    }

    public void dateCroissant_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/BoiteMessagerie.aspx?TrieVendeur=dateCroissant" + "&TrieClient=" + this.trieClient;
        Response.Redirect(url, true);
    }

    public void dateDecroissant_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/BoiteMessagerie.aspx?TrieVendeur=dateDecroissant" + "&TrieClient=" + this.trieClient;
        Response.Redirect(url, true);
    }

    public void ventesCroissant_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/BoiteMessagerie.aspx?TrieVendeur=ventesCroissant" + "&TrieClient=" + this.trieClient;
        Response.Redirect(url, true);
    }

    public void ventesDecroissant_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/BoiteMessagerie.aspx?TrieVendeur=ventesDecroissant" + "&TrieClient=" + this.trieClient;
        Response.Redirect(url, true);
    }

    public void dateCroissantClients_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/BoiteMessagerie.aspx?TrieVendeur=" + this.trieVendeur + "&TrieClient=dateCroissant";
        Response.Redirect(url, true);
    }

    public void dateDecroissantClients_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/BoiteMessagerie.aspx?TrieVendeur=" + this.trieVendeur + "&TrieClient=dateDecroissant";
        Response.Redirect(url, true);
    }

    public void selectionToutVendeurs_click(Object sender, EventArgs e)
    {
        Button btnTout = (Button)sender;
        if (btnTout.Text == "Sélectionner tout")
        {
            btnTout.Text = "Déselectionner tout";
            foreach(CheckBox cb in lstCbVendeurs)
            {
                cb.Checked = true;
            }
        }
        else
        {
            btnTout.Text = "Sélectionner tout";
            foreach (CheckBox cb in lstCbVendeurs)
            {
                cb.Checked = false;
            }
        }
    }

    public void selectionToutClients_click(Object sender, EventArgs e)
    {
        Button btnTout = (Button)sender;
        if (btnTout.Text == "Sélectionner tout")
        {
            btnTout.Text = "Déselectionner tout";
            foreach(CheckBox cb in lstCbClients)
            {
                cb.Checked = true;
            }
        }
        else
        {
            btnTout.Text = "Sélectionner tout";
            foreach (CheckBox cb in lstCbClients)
            {
                cb.Checked = false;
            }
        }
    }

    private void getTrieVendeur()
    {
        if (Request.QueryString["TrieVendeur"] == null)
        {
            this.trieVendeur = "dateDecroissant";
        }
        else
        {
            this.trieVendeur = Request.QueryString["TrieVendeur"];
        }
    }

    private void getTrieClient()
    {
        if (Request.QueryString["TrieClient"] == null)
        {
            this.trieClient = "dateDecroissant";
        }
        else
        {
            this.trieClient = Request.QueryString["TrieClient"];
        }
    }
}