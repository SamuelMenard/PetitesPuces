using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class Pages_AccueilInternaute : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        afficherCategories();
    }

    public void afficherCategories()
    {
        Dictionary<Nullable<long>, List<PPVendeurs>> lstCategories = LibrairieLINQ.getEntreprisesTriesParCategories();

        // ajouter le liens mes paniers
        LibrairieControlesDynamique.liDYN(ulSideBar, "#categories", "Nos catégories", "section-header");


        foreach (KeyValuePair<Nullable<long>, List<PPVendeurs>> entry in lstCategories)
        {
            String nomCategorie = LibrairieLINQ.getCategorie(entry.Key).Description;
            long? noCategorie = entry.Key;

            // ajouter lien navbar
            LibrairieControlesDynamique.liDYN(ulSideBar, "#" + "contentBody_categorie" + noCategorie, nomCategorie, "");

            // créer le panel pour la catégorie
            Panel panelDefault = LibrairieControlesDynamique.divDYN(categoriesDynamique, "categorie" + noCategorie, "panel panel-default");
            Panel panelHeading = LibrairieControlesDynamique.divDYN(panelDefault, "", "panel-heading");
            Panel panelBody = LibrairieControlesDynamique.divDYN(panelDefault, "", "panel-body");

            // mettre le nom de la catégorie dans le header
            LibrairieControlesDynamique.h4DYN(panelHeading, nomCategorie);

            // créer la row
            Panel row = null;

            int nbEntres = 0;
            foreach (PPVendeurs vendeur in entry.Value)
            {
                long? noVendeur = vendeur.NoVendeur;
                long? nbItems = LibrairieLINQ.getNbProduitsEntrepriseDansCategorie(entry.Key, vendeur.NoVendeur);

                if (nbEntres % 6 == 0)
                {
                    row = LibrairieControlesDynamique.divDYN(panelBody, "", "row");
                    row.Style.Add("margin-bottom", "20 px");
                }

                XDocument document = XDocument.Load(Server.MapPath("\\static\\xml\\" + vendeur.Configuration));
                XElement configuration = document.Element("configuration");

                String nomEntreprise = vendeur.NomAffaires;
                String urlImg = "~/static/images/" + configuration.Descendants("urlImage").Single().Value;

                // rajouter les colonnes (entreprises)
                Panel col = LibrairieControlesDynamique.divDYN(row, "", "col-md-2");
                col.Style.Add("text-align", "center");
                Image img = LibrairieControlesDynamique.imgDYN(col, "", urlImg, "");
                img.Style.Add("width", "100px");
                LibrairieControlesDynamique.brDYN(col);

                // nom entreprise + nb produits
                LinkButton lbNomEntreprise = LibrairieControlesDynamique.lbDYN(col, nomCategorie + ";" + noVendeur, nomEntreprise, nomEntreprise_click);
                LibrairieControlesDynamique.spaceDYN(col);
                LibrairieControlesDynamique.spaceDYN(col);
                LibrairieControlesDynamique.lblDYN(col, "", nbItems.ToString(), "badge");
                nbEntres++;
            }
        }

    }

    public void nomEntreprise_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/NouveauxProduits.aspx?";
        Response.Redirect(url, true);
    }
}