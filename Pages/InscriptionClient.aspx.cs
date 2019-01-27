using System;
using System.Linq;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_InscriptionClient : System.Web.UI.Page
{
    private BD6B8_424SEntities dbContext = new BD6B8_424SEntities();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnInscription_Click(object sender, EventArgs e)
    {
        if (dbContext.PPClients.Where(c => c.AdresseEmail == tbCourriel.Text).Any())
        { 
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

                if (LibrairieCourriel.envoyerCourriel(message))
                {
                    lblMessage.Text = "Votre profil à été créé. Un rappel de vos informations de connexion vous a été envoyé par courriel.";
                    divMessage.CssClass = "alert alert-success alert-margins";
                }
                else
                {
                    dbContext.PPClients.Remove(client);
                    dbContext.SaveChanges();

                    lblMessage.Text = "Votre profil n'a pas pu être créé. Assurez-vous que vous avez saisi correctement votre courriel et que celui-ci existe vraiment.";
                    divMessage.CssClass = "alert alert-danger alert-margins";
                }
            }
            else
            {
                lblMessage.Text = "Votre profil n'a pas pu être créé. Réessayez ultérieurement.";
                divMessage.CssClass = "alert alert-danger alert-margins";
            }
        }

        foreach (Control controle in Page.Form.Controls)
            if (controle is TextBox)
                ((TextBox)controle).Text = "";
        divMessage.Visible = true;
    }
}