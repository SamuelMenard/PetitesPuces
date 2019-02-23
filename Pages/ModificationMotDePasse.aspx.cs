using System;
using System.Linq;
using System.Text.RegularExpressions;

public partial class Pages_ModificationMotDePasse : System.Web.UI.Page
{
    private BD6B8_424SEntities dbContext = new BD6B8_424SEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        string url = null;
        if (Session["TypeUtilisateur"] == null)
            url = "~/Pages/AccueilInternaute.aspx?";
        else if (Session["TypeUtilisateur"].ToString() == "G")
            url = "~/Pages/AcceuilGestionnaire.aspx?";
        if (url != null)
            Response.Redirect(url, true);

        Page.Title = "Changer mon mot de passe";
    }

    protected void btnModifierMotPasse_Click(object sender, EventArgs e)
    {
        Regex exprMotPasse = new Regex("(?=^[a-zA-Z0-9]*[a-z])(?=^[a-zA-Z0-9]*[A-Z])(?=^[a-zA-Z0-9]*[0-9])(?=^[a-zA-Z0-9]{8,}$)");
        string courriel = Session["Courriel"].ToString();
        if (tbMotPasseActuel.Text == "" || !(dbContext.PPClients.Where(c => c.AdresseEmail == courriel && c.MotDePasse == tbMotPasseActuel.Text).Any() ||
                                             dbContext.PPVendeurs.Where(v => v.AdresseEmail == courriel && v.MotDePasse == tbMotPasseActuel.Text).Any()) ||
            tbNouveauMotPasse.Text == "" || !exprMotPasse.IsMatch(tbNouveauMotPasse.Text) ||
            tbConfirmationNouveauMotPasse.Text == "" || tbConfirmationNouveauMotPasse.Text != tbNouveauMotPasse.Text)
        {
            if (tbMotPasseActuel.Text == "" ||
            !(dbContext.PPClients.Where(c => c.AdresseEmail == courriel && c.MotDePasse == tbMotPasseActuel.Text).Any() ||
              dbContext.PPVendeurs.Where(v => v.AdresseEmail == courriel && v.MotDePasse == tbMotPasseActuel.Text).Any()))
            {
                tbMotPasseActuel.CssClass = "form-control border-danger";
                if (tbMotPasseActuel.Text == "")
                    errMotPasseActuel.Text = "Vous n'avez pas tapé votre mot de passe";
                else
                    errMotPasseActuel.Text = "Vous n'avez pas tapé le bon mot de passe";
                errMotPasseActuel.CssClass = "text-danger";
            }
            else
            {
                tbMotPasseActuel.CssClass = "form-control border-success";
                hidMotPasseActuel.Value = tbMotPasseActuel.Text;
                errMotPasseActuel.Text = "";
                errMotPasseActuel.CssClass = "text-danger hidden";
            }
            if (tbNouveauMotPasse.Text == "" || !exprMotPasse.IsMatch(tbNouveauMotPasse.Text))
            {
                tbNouveauMotPasse.CssClass = "form-control border-danger";
                if (tbNouveauMotPasse.Text == "")
                    errNouveauMotPasse.Text = "Le mot de passe ne peut pas être vide";
                else
                    errNouveauMotPasse.Text = "Le mot de passe doit contenir au moins 8 charactères dont une lettre minuscule, une lettre majuscule et un chiffre";
                errNouveauMotPasse.CssClass = "text-danger";
            }
            else
            {
                tbNouveauMotPasse.CssClass = "form-control border-success";
                hidNouveauMotPasse.Value = tbNouveauMotPasse.Text;
                errNouveauMotPasse.Text = "";
                errNouveauMotPasse.CssClass = "text-danger hidden";
            }
            if (tbConfirmationNouveauMotPasse.Text == "" || tbConfirmationNouveauMotPasse.Text != tbNouveauMotPasse.Text)
            {
                tbConfirmationNouveauMotPasse.CssClass = "form-control border-danger";
                if (tbConfirmationNouveauMotPasse.Text == "")
                    errConfirmationNouveauMotPasse.Text = "La confirmation du mot de passe ne peut pas être vide";
                else
                    errConfirmationNouveauMotPasse.Text = "La confirmation du mot de passe ne correspond pas au mot de passe";
                errConfirmationNouveauMotPasse.CssClass = "text-danger";
            }
            else
            {
                tbConfirmationNouveauMotPasse.CssClass = "form-control border-success";
                hidConfirmationNouveauMotPasse.Value = tbConfirmationNouveauMotPasse.Text;
                errConfirmationNouveauMotPasse.Text = "";
                errConfirmationNouveauMotPasse.CssClass = "text-danger hidden";
            }
        }
        else
        {

            if (dbContext.PPClients.Where(c => c.AdresseEmail == courriel).Any())
                dbContext.PPClients.Where(c => c.AdresseEmail == courriel).Single().MotDePasse = tbNouveauMotPasse.Text;
            else
                dbContext.PPVendeurs.Where(v => v.AdresseEmail == courriel).Single().MotDePasse = tbNouveauMotPasse.Text;

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
                lblMessage.Text = "Votre mot de passe à été modifié. Il sera en vigeur dès la prochaine connexion.";
                divMessage.CssClass = "alert alert-success alert-margins";
            }
            else
            {
                lblMessage.Text = "Votre mot de passe n'a pas pu être modifié. Réessayez ultérieurement.";
                divMessage.CssClass = "alert alert-danger alert-margins";
            }

            tbMotPasseActuel.CssClass = "form-control";
            hidMotPasseActuel.Value = "";
            errMotPasseActuel.Text = "";
            errMotPasseActuel.CssClass = "text-danger hidden";
            tbNouveauMotPasse.CssClass = "form-control";
            hidNouveauMotPasse.Value = "";
            errNouveauMotPasse.Text = "";
            errNouveauMotPasse.CssClass = "text-danger hidden";
            tbConfirmationNouveauMotPasse.CssClass = "form-control";
            hidConfirmationNouveauMotPasse.Value = "";
            errConfirmationNouveauMotPasse.Text = "";
            errConfirmationNouveauMotPasse.CssClass = "text-danger hidden";
            divModification.Visible = false;
            divMessage.Visible = true;
        }
    }
}