using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class Pages_InscriptionVendeurClient : System.Web.UI.Page
{
    private BD6B8_424SEntities dbContext = new BD6B8_424SEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        string url = null;
        if (Session["TypeUtilisateur"] == null)
            url = "~/Pages/AccueilInternaute.aspx?";
        else if (Session["TypeUtilisateur"].ToString() != "C")
            if (Session["TypeUtilisateur"].ToString() == "V")
                url = "~/Pages/ConnexionVendeur.aspx?";
            else
                url = "~/Pages/AcceuilGestionnaire.aspx?";
        if (url != null)
            Response.Redirect(url, true);

        Page.Title = "Devenir vendeur";

        if (!IsPostBack)
        {
            long noClient = Convert.ToInt64(Session["NoClient"]);
            PPClients client = dbContext.PPClients.Where(c => c.NoClient == noClient).Single();
            tbNom.Text = client.Nom;
            tbPrenom.Text = client.Prenom;
            tbAdresse.Text = client.Rue;
            tbVille.Text = client.Ville;
            ddlProvince.SelectedValue = client.Province;
            if (client.CodePostal != null)
                tbCodePostal.Text = client.CodePostal.Substring(0, 3) + " " + client.CodePostal.Substring(3, 3);
            if (client.Tel1 != null)
                tbTelephone1.Text = "(" + client.Tel1.Substring(0, 3) + ") " + client.Tel1.Substring(3, 3) + "-" + client.Tel1.Substring(6);
            if (client.Tel2 != null)
                tbTelephone2.Text = "(" + client.Tel2.Substring(0, 3) + ") " + client.Tel2.Substring(3, 3) + "-" + client.Tel2.Substring(6);
        }
    }

    protected bool validerPage()
    {
        Regex exprNomEntreprise = new Regex("^[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*$");
        Regex exprNomOuPrenom = new Regex("^[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF]+(([-'\\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF])*$");
        Regex exprAdresse = new Regex("^(\\d+-)?\\d+([a-zA-Z]|\\s\\d/\\d)?\\s[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*\\s[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*$");
        Regex exprCodePostal = new Regex("^[A-Z]\\d[A-Z][\\s-]?\\d[A-Z]\\d$", RegexOptions.IgnoreCase);
        Regex exprTelephone = new Regex("^((\\([0-9]{3}\\)\\s|[0-9]{3}[\\s-])[0-9]{3}-[0-9]{4}|[0-9]{10})$");
        Regex exprCourriel = new Regex("^[a-zA-Z0-9]+([-._][a-zA-Z0-9]+)*@[a-zA-Z0-9]+([-._][a-zA-Z0-9]+)*\\.[a-z]+$");
        Regex exprMotPasse = new Regex("(?=^[a-zA-Z0-9]*[a-z])(?=^[a-zA-Z0-9]*[A-Z])(?=^[a-zA-Z0-9]*[0-9])(?=^[a-zA-Z0-9]{8,}$)");
        Regex exprPoids = new Regex("^\\d+$");
        Regex exprMontant = new Regex("^\\d+(\\.\\d{2})?$");
        return tbNomEntreprise.Text != "" && exprNomEntreprise.IsMatch(tbNomEntreprise.Text) &&
               tbNom.Text != "" && exprNomOuPrenom.IsMatch(tbNom.Text) &&
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
               tbConfirmationMotPasse.Text != "" && tbConfirmationMotPasse.Text == tbMotPasse.Text &&
               tbPoidsMaxLivraison.Text != "" && exprPoids.IsMatch(tbPoidsMaxLivraison.Text) && int.Parse(tbPoidsMaxLivraison.Text) <= 2147483647 &&
               tbLivraisonGratuite.Text != "" && exprMontant.IsMatch(tbLivraisonGratuite.Text) && double.Parse(tbLivraisonGratuite.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)) <= 214748.36;
    }

    protected void afficherErreurs()
    {
        Regex exprNomEntreprise = new Regex("^[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*$");
        Regex exprNomOuPrenom = new Regex("^[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF]+(([-'\\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF])*$");
        Regex exprAdresse = new Regex("^(\\d+-)?\\d+([a-zA-Z]|\\s\\d/\\d)?\\s[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*\\s[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*$");
        Regex exprCodePostal = new Regex("^[A-Z]\\d[A-Z][\\s-]?\\d[A-Z]\\d$", RegexOptions.IgnoreCase);
        Regex exprTelephone = new Regex("^((\\([0-9]{3}\\)\\s|[0-9]{3}[\\s-])[0-9]{3}-[0-9]{4}|[0-9]{10})$");
        Regex exprCourriel = new Regex("^[a-zA-Z0-9]+([-._][a-zA-Z0-9]+)*@[a-zA-Z0-9]+([-._][a-zA-Z0-9]+)*\\.[a-z]+$");
        Regex exprMotPasse = new Regex("(?=^[a-zA-Z0-9]*[a-z])(?=^[a-zA-Z0-9]*[A-Z])(?=^[a-zA-Z0-9]*[0-9])(?=^[a-zA-Z0-9]{8,}$)");
        Regex exprPoids = new Regex("^\\d+$");
        Regex exprMontant = new Regex("^\\d+(\\.\\d{2})?$");
        if (tbNomEntreprise.Text == "" || !exprNomEntreprise.IsMatch(tbNomEntreprise.Text))
        {
            tbNomEntreprise.CssClass = "form-control border-danger";
            if (tbNomEntreprise.Text == "")
                errNomEntreprise.Text = "Le nom de l'entreprise ne peut pas être vide";
            else
                errNomEntreprise.Text = "Le nom de l'entreprise n'est pas dans un format valide";
            errNomEntreprise.CssClass = "text-danger";
        }
        else
        {
            tbNomEntreprise.CssClass = "form-control border-success";
            errNomEntreprise.Text = "";
            errNomEntreprise.CssClass = "text-danger hidden";
        }
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
                errAdresse.Text = "L'adresse n'est pas dans un format valide. Référez-vous aux directives d'adressage de Poste Canada à l'adresse : https://www.canadapost.ca/tools/pg/manual/PGaddress-f.asp?ecid=murl10006450#1437041";
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
        if (tbPoidsMaxLivraison.Text == "" || !exprPoids.IsMatch(tbPoidsMaxLivraison.Text) || int.Parse(tbPoidsMaxLivraison.Text) > 2147483647)
        {
            tbPoidsMaxLivraison.CssClass = "form-control border-danger";
            if (tbPoidsMaxLivraison.Text == "")
                errPoidsMaxLivraison.Text = "Le poids de livraison maximum ne peut pas être vide";
            else if (!exprPoids.IsMatch(tbPoidsMaxLivraison.Text))
                errPoidsMaxLivraison.Text = "Le poids de livraison maximum doit être un entier positif";
            else
                errPoidsMaxLivraison.Text = "Le poids de livraison maximum doit être inférieur à 2 147 483 647 lbs";
            errPoidsMaxLivraison.CssClass = "text-danger";
        }
        else
        {
            tbPoidsMaxLivraison.CssClass = "form-control border-success";
            errPoidsMaxLivraison.Text = "";
            errPoidsMaxLivraison.CssClass = "text-danger hidden";
        }
        if (tbLivraisonGratuite.Text == "" || !exprMontant.IsMatch(tbLivraisonGratuite.Text) || double.Parse(tbLivraisonGratuite.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)) > 214748.36)
        {
            tbLivraisonGratuite.CssClass = "form-control border-danger";
            if (tbLivraisonGratuite.Text == "")
                errLivraisonGratuite.Text = "Le montant pour avoir la livraison gratuite ne peut pas être vide";
            else if (!exprMontant.IsMatch(tbLivraisonGratuite.Text))
                errLivraisonGratuite.Text = "Le montant pour avoir la livraison gratuite doit être un nombre positif";
            else
                errLivraisonGratuite.Text = "Le montant pour avoir la livraison gratuite doit être inférieur à 214 748,37 $";
            errLivraisonGratuite.CssClass = "text-danger";
        }
        else
        {
            tbLivraisonGratuite.CssClass = "form-control border-success";
            errLivraisonGratuite.Text = "";
            errLivraisonGratuite.CssClass = "text-danger hidden";
        }
    }

    protected void btnEnvoyerDemande_Click(object sender, EventArgs e)
    {
        if (validerPage())
        {
            if (dbContext.PPVendeurs.Where(v => v.NomAffaires == tbNomEntreprise.Text).Any())
            {
                lblMessage.Text = "Ce nom d'entreprise existe déjà";
                divMessage.CssClass = "alert alert-danger alert-margins";
            }
            else if (dbContext.PPVendeurs.Where(v => v.AdresseEmail == tbCourriel.Text).Any() ||
                     dbContext.PPClients.Where(c => c.AdresseEmail == tbCourriel.Text).Any() ||
                     dbContext.PPGestionnaires.Where(g => g.courriel == tbCourriel.Text).Any())
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
                vendeur.CodePostal = tbCodePostal.Text.ToUpper().Replace(" ", "").Replace("-", "");
                vendeur.Pays = "Canada";
                vendeur.Tel1 = tbTelephone1.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                if (!string.IsNullOrEmpty(tbTelephone2.Text))
                    vendeur.Tel2 = tbTelephone2.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                vendeur.AdresseEmail = tbCourriel.Text;
                vendeur.MotDePasse = tbMotPasse.Text;
                vendeur.PoidsMaxLivraison = int.Parse(tbPoidsMaxLivraison.Text);
                vendeur.LivraisonGratuite = decimal.Parse(tbLivraisonGratuite.Text.Replace(".", ","));
                vendeur.Taxes = !cbTaxes.Checked;
                vendeur.Configuration = vendeur.NoVendeur + ".xml";
                vendeur.DateCreation = DateTime.Now;

                dbContext.PPVendeurs.Add(vendeur);

                bool binOK = true;

                try
                {
                    XDocument document = new XDocument();
                    XElement urlImage = new XElement("urlImage");
                    urlImage.Value = "image_magasin.jpg";
                    XElement couleurFond = new XElement("couleurFond");
                    couleurFond.Value = "#ffffff";
                    XElement couleurTexte = new XElement("couleurTexte");
                    couleurTexte.Value = "#000000";
                    document.Add(new XElement("configuration", urlImage, couleurFond, couleurTexte));
                    document.Save(Server.MapPath("\\static\\xml\\12.xml"));

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
                if (controle.HasControls())
                    foreach (Control controleEnfant in controle.Controls)
                        if (controleEnfant is TextBox)
                            ((TextBox)controleEnfant).Text = "";
                        else if (controleEnfant is DropDownList)
                            ((DropDownList)controleEnfant).ClearSelection();
                        else if (controleEnfant is CheckBox)
                            ((CheckBox)controleEnfant).Checked = false;
                        else
                    if (controle is TextBox)
                            ((TextBox)controle).Text = "";
                        else if (controle is DropDownList)
                            ((DropDownList)controle).ClearSelection();
                        else if (controle is CheckBox)
                            ((CheckBox)controle).Checked = false;
            divMessage.Visible = true;
            divInscription.Visible = false;
        }
        else
            afficherErreurs();
    }
}