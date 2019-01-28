using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_InscriptionProduit : System.Web.UI.Page
{
    private BD6B8_424SEntities dbContext = new BD6B8_424SEntities();

    protected void chargerListeDeroulante()
    {
        var categories = dbContext.PPCategories;

        foreach (var categorie in categories)
            ddlCategorie.Items.Add(new ListItem(categorie.Description, categorie.NoCategorie.ToString()));
    }

    protected void initialiserDate()
    {
        tbDateVente.Text = DateTime.Now.AddDays(1).ToShortDateString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        chargerListeDeroulante();
        initialiserDate();
    }

    protected void btnInscription_Click(object sender, EventArgs e)
    {
        string strFichier = fImage.FileName;
    }
}