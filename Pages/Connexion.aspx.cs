using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;

public partial class Pages_Connexion : System.Web.UI.Page
{
    private BD6B8_424SEntities dbContext = new BD6B8_424SEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["idPetitesPuces"] != null)
            {
                tbCourriel.Text = Server.HtmlEncode(Request.Cookies["idPetitesPuces"].Value);
                cbSeSouvenir.Checked = true;
            }
        }
        else
        {
            if (!cbSeSouvenir.Checked && Request.Cookies["idPetitesPuces"] != null)
            {
                HttpCookie cookie = new HttpCookie("idPetitesPuces");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            divMessage.Visible = false;
            divCourriel.Visible = false;
        }
    }

    public void btnConnexion_click(Object sender, EventArgs e)
    {
        String typeUtilisateur = null;
        String courriel = tbCourriel.Text;
        String MDP = tbMDP.Text;

        if (dbContext.PPClients.Where(c => c.AdresseEmail == courriel).Any())
            typeUtilisateur = "C";
        else if (dbContext.PPVendeurs.Where(v => v.AdresseEmail == courriel).Any())
            typeUtilisateur = "V";
        else if (dbContext.PPGestionnaires.Where(g => g.courriel == courriel).Any())
            typeUtilisateur = "G";

        bool verdictConnexion = false;
        String url = "";

        int codeErreur = typeUtilisateur != null ? LibrairieLINQ.connexionOK(courriel, MDP, typeUtilisateur) : 401;

        if (typeUtilisateur != null)
        {
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

                PPClients client = dbContext.PPClients.Where(c => c.AdresseEmail == courriel).Single();
                if (client.DateDerniereConnexion != null)
                {
                    if (client.DateDerniereConnexion.Value.Date != DateTime.Now.Date)
                    {
                        client.NbConnexions++;
                        client.DateDerniereConnexion = DateTime.Now;
                    }
                }
                else
                {
                    client.NbConnexions++;
                    client.DateDerniereConnexion = DateTime.Now;
                }            

                try
                {
                    dbContext.SaveChanges();
                }
                catch { }
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
        }

        if (verdictConnexion)
        {
            if (cbSeSouvenir.Checked)
            {
                HttpCookie cookie = new HttpCookie("idPetitesPuces");
                cookie.Value = courriel;
                cookie.Expires = DateTime.MaxValue;
                Response.Cookies.Add(cookie);
            }

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
                message.IsBodyHtml = true;
                message.Body = string.Format("Bonjour,<br /><br />" +
                                             "Suite à votre demande, nous vous envoyons vos informations de connexion.<br /><br />" +
                                             "Identifiant : {0}<br />" +
                                             "Mot de passe : {1}<br /><br />" +
                                             "Vous pouvez suivre ce lien pour vous connecter à nouveau : <a href=\"http://424s.cgodin.qc.ca/Pages/Connexion.aspx\">http://424s.cgodin.qc.ca/Pages/Connexion.aspx</a>.<br /><br />" +
                                             "Merci de faire affaire avec nous,<br />" +
                                             "Les Petites Puces",
                                             client.AdresseEmail,
                                             client.MotDePasse);
            }
            else
            {
                PPVendeurs vendeur = dbContext.PPVendeurs.Where(v => v.AdresseEmail == tbCourrielMotDePasseOublie.Text).Single();

                message = new MailMessage("ppuces@gmail.com", vendeur.AdresseEmail);
                message.Subject = "Mot de passe oublié Les Petites Puces";
                message.IsBodyHtml = true;
                message.Body = string.Format("Bonjour,<br /><br />" +
                                             "Suite à votre demande, nous vous envoyons vos informations de connexion.<br /><br />" +
                                             "Identifiant : {0}<br />" +
                                             "Mot de passe : {1}<br /><br />" +
                                             "Vous pouvez suivre ce lien pour vous connecter à nouveau : <a href=\"http://424s.cgodin.qc.ca/Pages/Connexion.aspx\">http://424s.cgodin.qc.ca/Pages/Connexion.aspx</a>.<br /><br />" +
                                             "Merci de faire affaire avec nous,<br />" +
                                             "Les Petites Puces",
                                             vendeur.AdresseEmail,
                                             vendeur.MotDePasse);
            }

            /*if (LibrairieCourriel.envoyerCourriel(message))
            {
                lblMessage.Text = "Nous vous avons envoyé vos informations de connexion par courriel.";
                divMessage.CssClass = "alert alert-success alert-margins";
            }
            else
            {
                lblMessage.Text = "Nous n'avons pas pu vous envoyer vos informations de connexion par courriel.";
                divMessage.CssClass = "alert alert-danger alert-margins";
            }*/

            lblMessage.Text = "Votre profil à été créé. Vos informations de connexion vous ont été envoyées par courriel.";
            divMessage.CssClass = "alert alert-success alert-margins";
            divMessage.Visible = true;

            tbExpediteur.Text = message.From.ToString();
            tbDestinataire.Text = message.To.ToString();
            tbSujet.Text = message.Subject;
            divCorps.InnerHtml = message.Body;
            divCourriel.Visible = true;

            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "cacherModal", "$('#modalMotDePasseOublie').modal('hide');", true);
        }
        else
        {
            errCourrielMotDePasseOublie.Text = "Le courriel que vous avez entré ne correspond pas à un profil client ou un profil vendeur";
            errCourrielMotDePasseOublie.CssClass = "text-danger";
        }
    }
}