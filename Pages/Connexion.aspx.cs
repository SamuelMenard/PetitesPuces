using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

public partial class Pages_Connexion : System.Web.UI.Page
{
    private BD6B8_424SEntities dbContext = new BD6B8_424SEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            tbCourrielMotDePasseOublie.Text = "";
            errCourrielMotDePasseOublie.Text = "";
            errCourrielMotDePasseOublie.CssClass = "text-danger d-none";
        }
    }

    public void btnConnexion_click(Object sender, EventArgs e)
    {
        String typeUtilisateur = ddlTypeUtilisateur.SelectedValue.ToString();
        String courriel = tbCourriel.Text;
        String MDP = tbMDP.Text;

        bool verdictConnexion = false;
        String url = "";

        int codeErreur = LibrairieLINQ.connexionOK(courriel, MDP, typeUtilisateur);

        if (typeUtilisateur == "C" && codeErreur == 400)
        {
            verdictConnexion = true;
            List<String> lstInfos = LibrairieLINQ.infosBaseClient(courriel);
            Session["TypeUtilisateur"] = "C";
            Session["NoClient"] = lstInfos[0];
            Session["Nom"] = lstInfos[1];
            Session["Prenom"] = lstInfos[2];
            Session["Courriel"] = lstInfos[3];
            url = "~/Pages/AccueilClient.aspx?";

        }
        else if (typeUtilisateur == "V" && codeErreur == 400)
        {
            verdictConnexion = true;
            List<String> lstInfos = LibrairieLINQ.infosBaseVendeur(courriel);
            Session["TypeUtilisateur"] = "V";
            Session["NoVendeur"] = lstInfos[0];
            Session["NomAffaire"] = lstInfos[1];
            Session["Nom"] = lstInfos[2];
            Session["Prenom"] = lstInfos[3];
            Session["Courriel"] = lstInfos[4];
            url = "~/Pages/ConnexionVendeur.aspx?";

        }
        else if (typeUtilisateur == "G" && codeErreur == 400)
        {
            verdictConnexion = true;
            Session["TypeUtilisateur"] = "G";
            url = "~/Pages/AcceuilGestionnaire.aspx?";
        }

        if (verdictConnexion)
        {
            Response.Redirect(url, true);
        }
        else
        {
            tbCourriel.CssClass = "form-control erreur";
            tbMDP.CssClass = "form-control erreur";

            alert_erreur.Visible = true;
            if (codeErreur == 401) { lblMessageErreur.Text = "Courriel ou mot de passe incorrect"; }
            else if (codeErreur == 402) { lblMessageErreur.Text = "Le compte a été désactivé"; }
        }
        
    }

    protected void btnEnvoyerMotDePasse_Click(object sender, EventArgs e)
    {
        if (dbContext.PPClients.Where(c => c.AdresseEmail == tbCourrielMotDePasseOublie.Text).Any() ||
            dbContext.PPVendeurs.Where(v => v.AdresseEmail == tbCourrielMotDePasseOublie.Text).Any())
        {
            MailMessage message = null;
                
            if (dbContext.PPClients.Where(c => c.AdresseEmail == tbCourrielMotDePasseOublie.Text).Any())
            {
                PPClients client = dbContext.PPClients.Where(c => c.AdresseEmail == tbCourrielMotDePasseOublie.Text).Single();

                message = new MailMessage("ppuces@gmail.com", client.AdresseEmail);
                message.Subject = "Mot de passe oublié Les Petites Puces";
                message.Body = string.Format("Bonjour,\n\n" +
                                             "Suite à votre demande, nous vous envoyons un rappel de vos informations de connexion.\n" +
                                             "Identifiant : {1}\n" +
                                             "Mot de passe : {2}\n\n" +
                                             "Merci de faire affaire avec nous,\n" +
                                             "Les Petites Puces",
                                             client.AdresseEmail,
                                             client.MotDePasse);
            }
            else
            {
                PPVendeurs vendeur = dbContext.PPVendeurs.Where(v => v.AdresseEmail == tbCourrielMotDePasseOublie.Text).Single();

                message = new MailMessage("ppuces@gmail.com", vendeur.AdresseEmail);
                message.Subject = "Mot de passe oublié Les Petites Puces";
                message.Body = string.Format("Bonjour,\n\n" +
                                             "Suite à votre demande, nous vous envoyons vos informations de connexion.\n" +
                                             "Identifiant : {1}\n" +
                                             "Mot de passe : {2}\n\n" +
                                             "Merci de faire affaire avec nous,\n" +
                                             "Les Petites Puces",
                                             vendeur.AdresseEmail,
                                             vendeur.MotDePasse);
            }

            if (LibrairieCourriel.envoyerCourriel(message))
            {
                lblMessage.Text = "Nous vous avons envoyé vos informations de connexion par courriel.";
                divMessage.CssClass = "alert alert-success alert-margins";
            }
            else
            {
                lblMessage.Text = "Nous n'avons pas pu vous envoyer vos informations de connexion par courriel.";
                divMessage.CssClass = "alert alert-danger alert-margins";
            }
        }
        else
        {
            errCourrielMotDePasseOublie.Text = "Le courriel que vous avez entré ne correspond pas à un profil client ou un profil vendeur";
            errCourrielMotDePasseOublie.CssClass = "text-danger";
        }
    }
}