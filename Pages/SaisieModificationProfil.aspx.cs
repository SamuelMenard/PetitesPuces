using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_SaisieModificationProfil : System.Web.UI.Page
{
    private BD6B8_424SEntities dbContext = new BD6B8_424SEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
       if(Session["NoClient"] != null)
        {
            creationFormulaire();
        }
    }

    protected void creationFormulaire()
    {
        long idClient = long.Parse(Session["NoClient"].ToString());
        PPClients leClient = dbContext.PPClients.Where(c => c.NoClient == idClient).First();
        txtNom.Text = leClient.Nom;
        txtPrenom.Text = leClient.Prenom;
        txtEmail.Text = leClient.AdresseEmail;
        txtRue.Text = leClient.Rue;
        txtPays.Text = leClient.Pays;
        txtCodePostal.Text = leClient.CodePostal;
        txtCellulaire.Text = leClient.Tel1;
        txtTelephone.Text = leClient.Tel2;
        txtVille.Text = leClient.Ville;
        province.SelectedValue = leClient.Province;
    }
}

