using System;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_InscriptionClientVendeur : System.Web.UI.Page
{
    private BD6B8_424SEntities dbContext = new BD6B8_424SEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        string url = null;
        if (Session["TypeUtilisateur"] == null)
            url = "~/Pages/AccueilInternaute.aspx?";
        else if (Session["TypeUtilisateur"].ToString() != "V")
            if (Session["TypeUtilisateur"].ToString() == "C")
                url = "~/Pages/AccueilClient.aspx?";
            else
                url = "~/Pages/AcceuilGestionnaire.aspx?";
        if (url != null)
            Response.Redirect(url, true);

        if (!IsPostBack)
        {
            long noVendeur = Convert.ToInt64(Session["NoVendeur"]);
            PPVendeurs vendeur = dbContext.PPVendeurs.Where(v => v.NoVendeur == noVendeur).Single();
            tbNom.Text = vendeur.Nom;
            tbPrenom.Text = vendeur.Prenom;
            tbAdresse.Text = vendeur.Rue;
            tbVille.Text = vendeur.Ville;
            ddlProvince.SelectedValue = vendeur.Province;
            tbCodePostal.Text = vendeur.CodePostal.Substring(0, 3) + " " + vendeur.CodePostal.Substring(3, 3);
            tbTelephone1.Text = "(" + vendeur.Tel1.Substring(0, 3) + ") " + vendeur.Tel1.Substring(3, 3) + "-" + vendeur.Tel1.Substring(6);
            if (vendeur.Tel2 != null)
                tbTelephone2.Text = "(" + vendeur.Tel2.Substring(0, 3) + ") " + vendeur.Tel2.Substring(3, 3) + "-" + vendeur.Tel2.Substring(6);
        }
    }

    protected bool validerPage()
    {
        Regex exprNomOuPrenom = new Regex("^[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF]+(([-'\\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF])*$");
        Regex exprAdresse = new Regex("^(\\d+-)?\\d+([a-zA-Z]|\\s\\d/\\d)?\\s[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*\\s[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*$");
        Regex exprCodePostal = new Regex("^[A-Z]\\d[A-Z]\\s?\\d[A-Z]\\d$", RegexOptions.IgnoreCase);
        Regex exprTelephone = new Regex("^((\\([0-9]{3}\\)\\s|[0-9]{3}[\\s-])[0-9]{3}-[0-9]{4}|[0-9]{10})$");
        Regex exprCourriel = new Regex("^[a-zA-Z0-9]+([-._][a-zA-Z0-9]+)*@[a-zA-Z0-9]+([-._][a-zA-Z0-9]+)*\\.[a-z]+$");
        Regex exprMotPasse = new Regex("(?=^[a-zA-Z0-9]*[a-z])(?=^[a-zA-Z0-9]*[A-Z])(?=^[a-zA-Z0-9]*[0-9])(?=^[a-zA-Z0-9]{8,}$)");
        return tbNom.Text != "" && exprNomOuPrenom.IsMatch(tbNom.Text) &&
               tbPrenom.Text != "" && exprNomOuPrenom.IsMatch(tbPrenom.Text) &&
               tbAdresse.Text != "" && exprAdresse.IsMatch(tbAdresse.Text) &&
               tbVille.Text != "" && exprNomOuPrenom.IsMatch(tbVille.Text) &&
               ddlProvince.SelectedValue != "" &&
               tbCodePostal.Text != "" && exprCodePostal.IsMatch(tbCodePostal.Text) &&
               tbTelephone1.Text != "" && exprTelephone.IsMatch(tbTelephone1.Text) &&
               (tbTelephone2.Text == "" || exprTelephone.IsMatch(tbTelephone2.Text)) &&
               tbCourriel.Text != "" && exprCourriel.IsMatch(tbCourriel.Text) &&
               tbConfirmationCourriel.Text != "" && exprCourriel.IsMatch(tbConfirmationCourriel.Text) && tbConfirmationCourriel.Text == tbCourriel.Text &&
               tbMotPasse.Text != "" && exprMotPasse.IsMatch(tbMotPasse.Text) &&
               tbConfirmationMotPasse.Text != "" && tbConfirmationMotPasse.Text == tbMotPasse.Text;
    }

    protected void afficherErreurs()
    {
        Regex exprNomOuPrenom = new Regex("^[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF]+(([-'\\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF])*$");
        Regex exprAdresse = new Regex("^(\\d+-)?\\d+([a-zA-Z]|\\s\\d/\\d)?\\s[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*\\s[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*$");
        Regex exprCodePostal = new Regex("^[A-Z]\\d[A-Z]\\s?\\d[A-Z]\\d$", RegexOptions.IgnoreCase);
        Regex exprTelephone = new Regex("^((\\([0-9]{3}\\)\\s|[0-9]{3}[\\s-])[0-9]{3}-[0-9]{4}|[0-9]{10})$");
        Regex exprCourriel = new Regex("^[a-zA-Z0-9]+([-._][a-zA-Z0-9]+)*@[a-zA-Z0-9]+([-._][a-zA-Z0-9]+)*\\.[a-z]+$");
        Regex exprMotPasse = new Regex("(?=^[a-zA-Z0-9]*[a-z])(?=^[a-zA-Z0-9]*[A-Z])(?=^[a-zA-Z0-9]*[0-9])(?=^[a-zA-Z0-9]{8,}$)");
        if (tbNom.Text == "" || !exprNomOuPrenom.IsMatch(tbNom.Text))
        {
            tbNom.CssClass = "form-control border-danger";
            if (tbNom.Text == "")
                errNom.Text = "Le nom ne peut pas être vide";
            else
                errNom.Text = "Le nom n'est pas dans un format valide";
            errNom.CssClass = "text-danger";
        }
        else
        {
            tbNom.CssClass = "form-control border-success";
            errNom.Text = "";
            errNom.CssClass = "text-danger hidden";
        }
        if (tbPrenom.Text == "" || !exprNomOuPrenom.IsMatch(tbPrenom.Text))
        {
            tbPrenom.CssClass = "form-control border-danger";
            if (tbPrenom.Text == "")
                errPrenom.Text = "Le prénom ne peut pas être vide";
            else
                errPrenom.Text = "Le prénom n'est pas dans un format valide";
            errPrenom.CssClass = "text-danger";
        }
        else
        {
            tbPrenom.CssClass = "form-control border-success";
            errPrenom.Text = "";
            errPrenom.CssClass = "text-danger hidden";
        }
        if (tbAdresse.Text == "" || !exprAdresse.IsMatch(tbAdresse.Text))
        {
            tbAdresse.CssClass = "form-control border-danger";
            if (tbAdresse.Text == "")
                errAdresse.Text = "L'adresse ne peut pas être vide";
            else
                errAdresse.Text = "L'adresse n'est pas dans un format valide";
            errAdresse.CssClass = "text-danger";
        }
        else
        {
            tbAdresse.CssClass = "form-control border-success";
            errAdresse.Text = "";
            errAdresse.CssClass = "text-danger hidden";
        }
        if (tbVille.Text == "" || !exprNomOuPrenom.IsMatch(tbVille.Text))
        {
            tbVille.CssClass = "form-control border-danger";
            if (tbVille.Text == "")
                errVille.Text = "La ville ne peut pas être vide";
            else
                errVille.Text = "La ville n'est pas dans un format valide";
            errVille.CssClass = "text-danger";
        }
        else
        {
            tbVille.CssClass = "form-control border-success";
            errVille.Text = "";
            errVille.CssClass = "text-danger hidden";
        }
        if (ddlProvince.SelectedValue == "")
        {
            ddlProvince.CssClass = "form-control border-danger";
            errProvince.Text = "Vous devez sélectionner une province";
            errProvince.CssClass = "text-danger";
        }
        else
        {
            ddlProvince.CssClass = "form-control border-success";
            errProvince.Text = "";
            errProvince.CssClass = "text-danger hidden";
        }
        if (tbCodePostal.Text == "" || !exprCodePostal.IsMatch(tbCodePostal.Text))
        {
            tbCodePostal.CssClass = "form-control border-danger";
            if (tbCodePostal.Text == "")
                errCodePostal.Text = "Le code postal ne peut pas être vide";
            else
                errCodePostal.Text = "Le code postal n'est pas dans un format valide";
            errCodePostal.CssClass = "text-danger";
        }
        else
        {
            tbCodePostal.CssClass = "form-control border-success";
            errCodePostal.Text = "";
            errCodePostal.CssClass = "text-danger hidden";
        }
        if (tbTelephone1.Text == "" || !exprTelephone.IsMatch(tbTelephone1.Text))
        {
            tbTelephone1.CssClass = "form-control border-danger";
            if (tbTelephone1.Text == "")
                errTelephone1.Text = "Le téléphone 1 ne peut pas être vide";
            else
                errTelephone1.Text = "Le téléphone 1 n'est pas dans un format valide";
            errTelephone1.CssClass = "text-danger";
        }
        else
        {
            tbTelephone1.CssClass = "form-control border-success";
            errTelephone1.Text = "";
            errTelephone1.CssClass = "text-danger hidden";
        }
        if (tbTelephone2.Text != "" && !exprTelephone.IsMatch(tbTelephone2.Text))
        {
            tbTelephone2.CssClass = "form-control border-danger";
            errTelephone2.Text = "Le téléphone 2 n'est pas dans un format valide";
            errTelephone2.CssClass = "text-danger";
        }
        else
        {
            tbTelephone2.CssClass = "form-control border-success";
            errTelephone2.Text = "";
            errTelephone2.CssClass = "text-danger hidden";
        }
        if (tbCourriel.Text == "" || !exprCourriel.IsMatch(tbCourriel.Text))
        {
            tbCourriel.CssClass = "form-control border-danger";
            if (tbCourriel.Text == "")
                errCourriel.Text = "Le courriel ne peut pas être vide";
            else
                errCourriel.Text = "Le courriel n'est pas dans un format valide";
            errCourriel.CssClass = "text-danger";
        }
        else
        {
            tbCourriel.CssClass = "form-control border-success";
            errCourriel.Text = "";
            errCourriel.CssClass = "text-danger hidden";
        }
        if (tbConfirmationCourriel.Text == "" || !exprCourriel.IsMatch(tbConfirmationCourriel.Text) || tbConfirmationCourriel.Text != tbCourriel.Text)
        {
            tbConfirmationCourriel.CssClass = "form-control border-danger";
            if (tbConfirmationCourriel.Text == "")
                errConfirmationCourriel.Text = "La confirmation du courriel ne peut pas être vide";
            else if (!exprCourriel.IsMatch(tbConfirmationCourriel.Text))
                errConfirmationCourriel.Text = "La confirmation du courriel n'est pas dans un format valide";
            else
                errConfirmationCourriel.Text = "La confirmation du courriel ne correspond pas au courriel";
            errConfirmationCourriel.CssClass = "text-danger";
        }
        else
        {
            tbConfirmationCourriel.CssClass = "form-control border-success";
            errConfirmationCourriel.Text = "";
            errConfirmationCourriel.CssClass = "text-danger hidden";
        }
        if (tbMotPasse.Text == "" || !exprMotPasse.IsMatch(tbMotPasse.Text))
        {
            tbMotPasse.CssClass = "form-control border-danger";
            if (tbMotPasse.Text == "")
                errMotPasse.Text = "Le mot de passe ne peut pas être vide";
            else
                errMotPasse.Text = "Le mot de passe doit contenir au moins 8 charactères dont une lettre minuscule, une lettre majuscule et un chiffre";
            errMotPasse.CssClass = "text-danger";
        }
        else
        {
            tbMotPasse.CssClass = "form-control border-success";
            errMotPasse.Text = "";
            errMotPasse.CssClass = "text-danger hidden";
        }
        if (tbConfirmationMotPasse.Text == "" || tbConfirmationMotPasse.Text != tbMotPasse.Text)
        {
            tbConfirmationMotPasse.CssClass = "form-control border-danger";
            if (tbConfirmationMotPasse.Text == "")
                errConfirmationMotPasse.Text = "La confirmation du mot de passe ne peut pas être vide";
            else
                errConfirmationMotPasse.Text = "La confirmation du mot de passe ne correspond pas au mot de passe";
            errConfirmationMotPasse.CssClass = "text-danger";
        }
        else
        {
            tbConfirmationMotPasse.CssClass = "form-control border-success";
            errConfirmationMotPasse.Text = "";
            errConfirmationMotPasse.CssClass = "text-danger hidden";
        }
    }

    protected void btnInscription_Click(object sender, EventArgs e)
    {
        if (validerPage())
        {
            if (dbContext.PPClients.Where(c => c.AdresseEmail == tbCourriel.Text).Any() ||
                dbContext.PPVendeurs.Where(v => v.AdresseEmail == tbCourriel.Text).Any() ||
                dbContext.PPGestionnaires.Where(g => g.courriel == tbCourriel.Text).Any())
            {
                tbExpediteur.Text = "";
                tbDestinataire.Text = "";
                tbSujet.Text = "";
                tbCorps.Text = "";
                divCourriel.Visible = false;

                lblMessage.Text = "Il y a déjà un profil associé à ce courriel";
                divMessage.CssClass = "alert alert-danger alert-margins";
            }
            else
            {
                PPClients client = new PPClients();
                client.NoClient = dbContext.PPClients.Max(c => c.NoClient) + 1;
                client.Nom = tbNom.Text;
                client.Prenom = tbPrenom.Text;
                client.Rue = tbAdresse.Text;
                client.Ville = tbVille.Text;
                client.Province = ddlProvince.SelectedValue;
                client.CodePostal = tbCodePostal.Text.ToUpper().Replace(" ", "");
                client.Pays = "Canada";
                client.Tel1 = tbTelephone1.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                if (!string.IsNullOrEmpty(tbTelephone2.Text))
                    client.Tel2 = tbTelephone2.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                client.AdresseEmail = tbCourriel.Text;
                client.MotDePasse = tbMotPasse.Text;
                client.DateCreation = DateTime.Now;
                client.NbConnexions = 0;
                client.Statut = 1;

                dbContext.PPClients.Add(client);

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
                    MailMessage message = new MailMessage("ppuces@gmail.com", client.AdresseEmail);
                    message.Subject = "Création profil Les Petites Puces";
                    message.Body = string.Format("Bonjour,\n\n" +
                                                 "Suite à votre demande, votre profil Les Petites Puces a été créé. Votre numéro de client est {0}. Voici vos informations de connexion :\n" +
                                                 "Identifiant : {1}\n" +
                                                 "Mot de passe : {2}\n\n" +
                                                 "Merci de faire affaire avec nous,\n" +
                                                 "Les Petites Puces",
                                                 client.NoClient,
                                                 client.AdresseEmail,
                                                 client.MotDePasse);

                    tbExpediteur.Text = message.From.ToString();
                    tbDestinataire.Text = message.To.ToString();
                    tbSujet.Text = message.Subject;
                    tbCorps.Text = message.Body;
                    divCourriel.Visible = true;

                    lblMessage.Text = "Votre profil à été créé. Vos informations de connexion vous ont été envoyées par courriel.";
                    divMessage.CssClass = "alert alert-success alert-margins";
                }
            }

            foreach (Control controle in Page.Form.Controls)
                if (controle.HasControls())
                    foreach (Control controleEnfant in controle.Controls)
                        if (controleEnfant is TextBox)
                            ((TextBox)controleEnfant).Text = "";
                        else
                    if (controle is TextBox)
                            ((TextBox)controle).Text = "";
            divMessage.Visible = true;
            divInscription.Visible = false;
        }
        else
            afficherErreurs();
    }
}