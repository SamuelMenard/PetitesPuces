using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_AccueilInternaute : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        afficherCategories();
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

    public void nomEntreprise_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/NouveauxProduits.aspx?";
        Response.Redirect(url, true);
    }
}