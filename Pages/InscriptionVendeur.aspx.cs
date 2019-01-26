using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_InscriptionVendeur : System.Web.UI.Page
{
    private BD6B8_424SEntities dbContext = new BD6B8_424SEntities();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnEnvoyerDemande_Click(object sender, EventArgs e)
    {
        if (dbContext.PPVendeurs.Where(v => v.NomAffaires == tbNomEntreprise.Text).Any())
        {
            lblMessage.Text = "Ce nom d'entreprise existe déjà";
            divMessage.CssClass = "alert alert-danger alert-margins";
        }
        else if (dbContext.PPVendeurs.Where(v => v.AdresseEmail == tbCourriel.Text).Any())
        {
            lblMessage.Text = "Il y a déjà un profil associé à ce courriel";
            divMessage.CssClass = "alert alert-danger alert-margins";
        }
        else
        {
            PPVendeurs vendeur = new PPVendeurs();
            vendeur.NoVendeur = dbContext.PPVendeurs.Max(v => v.NoVendeur) + 1;
            vendeur.NomAffaires = tbNomEntreprise.Text;
            vendeur.Nom = tbNom.Text;
            vendeur.Prenom = tbPrenom.Text;
            vendeur.Rue = tbAdresse.Text;
            vendeur.Ville = tbVille.Text;
            vendeur.Province = ddlProvince.SelectedValue;
            vendeur.CodePostal = tbCodePostal.Text.ToUpper().Replace(" ", "");
            vendeur.Pays = "Canada";
            vendeur.Tel1 = tbTelephone1.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            if (!string.IsNullOrEmpty(tbTelephone2.Text))
                vendeur.Tel2 = tbTelephone2.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            vendeur.AdresseEmail = tbCourriel.Text;
            vendeur.MotDePasse = tbMotPasse.Text;
            vendeur.PoidsMaxLivraison = int.Parse(tbPoidsMaxLivraison.Text);
            vendeur.LivraisonGratuite = decimal.Parse(tbLivraisonGratuite.Text.Replace(".", ","));
            vendeur.Taxes = !cbTaxes.Checked;
            vendeur.DateCreation = DateTime.Now;

            dbContext.PPVendeurs.Add(vendeur);

            bool binOK = true;

            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception)
            {
                binOK = false;
            }

            if (binOK)
            {
                lblMessage.Text = "Votre demande d'inscription a été envoyé. Nous vous enverrons un courriel lorsque votre demande sera traitée.";
                divMessage.CssClass = "alert alert-success alert-margins";
            }
            else
            {
                lblMessage.Text = "Votre demande d'inscription n'a pas pu être envoyé. Réessayez ultérieurement.";
                divMessage.CssClass = "alert alert-danger alert-margins";
            }
        }

        foreach (Control controle in Page.Form.Controls)
            if (controle is TextBox)
                ((TextBox)controle).Text = "";
            else if (controle is DropDownList)
                ((DropDownList)controle).ClearSelection();
            else if (controle is CheckBox)
                ((CheckBox)controle).Checked = false;
        divMessage.Visible = true;
    }
}