using System;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_InscriptionClient : System.Web.UI.Page
{
    private BD6B8_424SEntities dbContext = new BD6B8_424SEntities();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected bool validerPage()
    {
        Regex exprCourriel = new Regex("^[a-zA-Z0-9]+([-._][a-zA-Z0-9]+)*@[a-zA-Z0-9]+([-._][a-zA-Z0-9]+)*\\.[a-z]+$");
        Regex exprMotPasse = new Regex("(?=^[a-zA-Z0-9]*[a-z])(?=^[a-zA-Z0-9]*[A-Z])(?=^[a-zA-Z0-9]*[0-9])(?=^[a-zA-Z0-9]{8,}$)");
        return tbCourriel.Text != "" && exprCourriel.IsMatch(tbCourriel.Text) &&
               tbConfirmationCourriel.Text != "" && exprCourriel.IsMatch(tbConfirmationCourriel.Text) && tbConfirmationCourriel.Text == tbCourriel.Text &&
               tbMotPasse.Text != "" && exprMotPasse.IsMatch(tbMotPasse.Text) &&
               tbConfirmationMotPasse.Text != "" && tbConfirmationMotPasse.Text == tbMotPasse.Text;
    }

    protected void afficherErreurs()
    {
        Regex exprCourriel = new Regex("^[a-zA-Z0-9]+([-._][a-zA-Z0-9]+)*@[a-zA-Z0-9]+([-._][a-zA-Z0-9]+)*\\.[a-z]+$");
        Regex exprMotPasse = new Regex("(?=^[a-zA-Z0-9]*[a-z])(?=^[a-zA-Z0-9]*[A-Z])(?=^[a-zA-Z0-9]*[0-9])(?=^[a-zA-Z0-9]{8,}$)");
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
            errCourriel.CssClass = "text-danger d-none";
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
            errConfirmationCourriel.CssClass = "text-danger d-none";
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
            errMotPasse.CssClass = "text-danger d-none";
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
            errConfirmationMotPasse.CssClass = "text-danger d-none";
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

                    /*if (LibrairieCourriel.envoyerCourriel(message))
                    {
                        lblMessage.Text = "Votre profil à été créé. Vos informations de connexion vous ont été envoyées par courriel.";
                        divMessage.CssClass = "alert alert-success alert-margins";
                    }
                    else
                    {
                        dbContext.PPClients.Remove(client);
                        dbContext.SaveChanges();

                        lblMessage.Text = "Votre profil n'a pas pu être créé. Assurez-vous que vous avez saisi correctement votre courriel et que celui-ci existe vraiment.";
                        divMessage.CssClass = "alert alert-danger alert-margins";
                    }*/

                    tbExpediteur.Text = message.From.ToString();
                    tbDestinataire.Text = message.To.ToString();
                    tbSujet.Text = message.Subject;
                    tbCorps.Text = message.Body;
                    divCourriel.Visible = true;

                    lblMessage.Text = "Votre profil à été créé. Vos informations de connexion vous ont été envoyées par courriel.";
                    divMessage.CssClass = "alert alert-success alert-margins";
                }
                /*else
                {
                    lblMessage.Text = "Votre profil n'a pas pu être créé. Réessayez ultérieurement.";
                    divMessage.CssClass = "alert alert-danger alert-margins";
                }*/
            }

            foreach (Control controle in Page.Form.Controls)
                if (controle is TextBox)
                    ((TextBox)controle).Text = "";
            divMessage.Visible = true;
        }
        else
            afficherErreurs();
    }
}