using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

public partial class Pages_ModificationProfilVendeur : System.Web.UI.Page
{
    private BD6B8_424SEntities dbContext = new BD6B8_424SEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            long noVendeur = Convert.ToInt64(Session["NoVendeur"]);
            PPVendeurs vendeur = dbContext.PPVendeurs.Where(v => v.NoVendeur == noVendeur).Single();
            tbNomEntreprise.Text = vendeur.NomAffaires;
            tbNom.Text = vendeur.Nom;
            tbPrenom.Text = vendeur.Prenom;
            tbAdresse.Text = vendeur.Rue;
            tbVille.Text = vendeur.Ville;
            ddlProvince.SelectedValue = vendeur.Province;
            tbCodePostal.Text = vendeur.CodePostal.Substring(0, 3) + " " + vendeur.CodePostal.Substring(3, 3);
            tbTelephone1.Text = "(" + vendeur.Tel1.Substring(0, 3) + ") " + vendeur.Tel1.Substring(3, 3) + "-" + vendeur.Tel1.Substring(6);
            if (vendeur.Tel2 != null)
                tbTelephone2.Text = "(" + vendeur.Tel2.Substring(0, 3) + ") " + vendeur.Tel2.Substring(3, 3) + "-" + vendeur.Tel2.Substring(6);
            tbCourriel.Text = vendeur.AdresseEmail;
            tbPoidsMaxLivraison.Text = vendeur.PoidsMaxLivraison.ToString();
            tbLivraisonGratuite.Text = vendeur.LivraisonGratuite.ToString()
                                                                .Remove(vendeur.LivraisonGratuite.ToString().IndexOf(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator) + 3)
                                                                .Replace(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, ".");
            cbTaxes.Checked = (bool)!vendeur.Taxes;

            XDocument document = XDocument.Load(Server.MapPath("\\static\\xml\\" + vendeur.Configuration));
            XElement configuration = document.Element("configuration");
            if ((imgTeleverse.ImageUrl = "~/static/images/" + configuration.Descendants("urlImage").Single().Value) == "~/static/images/image_magasin.jpg")
                btnSelectionnerImage.Visible = true;
            else
                btnChangerImage.Visible = true;
            cpCouleurFond.Value = configuration.Descendants("couleurFond").Single().Value;
            cpCouleurTexte.Value = configuration.Descendants("couleurTexte").Single().Value;
        }
        else
            divMessage.Visible = false;
    }

    protected bool validerPage()
    {
        Regex exprNomEntreprise = new Regex("^[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*$");
        Regex exprNomOuPrenom = new Regex("^[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF]+(([-'\\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF])*$");
        Regex exprAdresse = new Regex("^(\\d+-)?\\d+([a-zA-Z]|\\s\\d/\\d)?\\s[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*\\s[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*$");
        Regex exprCodePostal = new Regex("^[A-Z]\\d[A-Z]\\s?\\d[A-Z]\\d$", RegexOptions.IgnoreCase);
        Regex exprTelephone = new Regex("^((\\([0-9]{3}\\)\\s|[0-9]{3}[\\s-])[0-9]{3}-[0-9]{4}|[0-9]{10})$");
        Regex exprPoids = new Regex("^\\d+$");
        Regex exprMontant = new Regex("^\\d+\\.\\d{2}$");
        return tbNomEntreprise.Text != "" && exprNomEntreprise.IsMatch(tbNomEntreprise.Text) &&
               tbNom.Text != "" && exprNomOuPrenom.IsMatch(tbNom.Text) &&
               tbPrenom.Text != "" && exprNomOuPrenom.IsMatch(tbPrenom.Text) &&
               tbAdresse.Text != "" && exprAdresse.IsMatch(tbAdresse.Text) &&
               tbVille.Text != "" && exprNomOuPrenom.IsMatch(tbVille.Text) &&
               ddlProvince.SelectedValue != "" &&
               tbCodePostal.Text != "" && exprCodePostal.IsMatch(tbCodePostal.Text) &&
               tbTelephone1.Text != "" && exprTelephone.IsMatch(tbTelephone1.Text) &&
               (tbTelephone2.Text == "" || exprTelephone.IsMatch(tbTelephone2.Text)) &&
               tbPoidsMaxLivraison.Text != "" && exprPoids.IsMatch(tbPoidsMaxLivraison.Text) &&
               tbLivraisonGratuite.Text != "" && exprMontant.IsMatch(tbLivraisonGratuite.Text) && double.Parse(tbLivraisonGratuite.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)) <= 214748.36;
    }

    protected void afficherErreurs()
    {
        Regex exprNomEntreprise = new Regex("^[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*$");
        Regex exprNomOuPrenom = new Regex("^[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF]+(([-'\\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF])*$");
        Regex exprAdresse = new Regex("^(\\d+-)?\\d+([a-zA-Z]|\\s\\d/\\d)?\\s[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*\\s[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-'\\s][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*$");
        Regex exprCodePostal = new Regex("^[A-Z]\\d[A-Z]\\s?\\d[A-Z]\\d$", RegexOptions.IgnoreCase);
        Regex exprTelephone = new Regex("^((\\([0-9]{3}\\)\\s|[0-9]{3}[\\s-])[0-9]{3}-[0-9]{4}|[0-9]{10})$");
        Regex exprPoids = new Regex("^\\d+$");
        Regex exprMontant = new Regex("^\\d+\\.\\d{2}$");
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
        if (tbPoidsMaxLivraison.Text == "" || !exprPoids.IsMatch(tbPoidsMaxLivraison.Text))
        {
            tbPoidsMaxLivraison.CssClass = "form-control border-danger";
            if (tbPoidsMaxLivraison.Text == "")
                errPoidsMaxLivraison.Text = "Le poids de livraison maximum ne peut pas être vide";
            else
                errPoidsMaxLivraison.Text = "Le poids de livraison maximum doit être un entier";
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
                errLivraisonGratuite.Text = "Le montant pour avoir la livraison gratuite doit être un nombre décimal avec deux chiffres après la virgule";
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

    protected void btnTeleverserImage_Click(object sender, EventArgs e)
    {
        if ((fImage.PostedFile.ContentType != "image/jpeg" && fImage.PostedFile.ContentType != "image/png") || fImage.PostedFile.ContentLength >= 31457280)
        {
            if (fImage.PostedFile.ContentType != "image/jpeg" && fImage.PostedFile.ContentType != "image/png")
                errImage.Text = "L'image sélectionnée doit être au format jpeg ou png";
            else
                errImage.Text = "L'image sélectionnée doit être inférieure à 30 mo";
            errImage.CssClass = "text-danger";
        }
        else
        {
            errImage.Text = "";
            errImage.CssClass = "text-danger hidden";

            bool binOK = true;

            long noVendeur = Convert.ToInt64(Session["NoVendeur"]);
            PPVendeurs vendeur = dbContext.PPVendeurs.Where(v => v.NoVendeur == noVendeur).Single();
            XDocument document = XDocument.Load(Server.MapPath("\\static\\xml\\" + vendeur.Configuration));
            XElement configuration = document.Element("configuration");
            string urlImage = configuration.Descendants("urlImage").Single().Value;

            if (urlImage != "image_magasin.jpg")
            {
                try
                {
                    File.Move(Server.MapPath("~/static/images/") + urlImage,
                              Server.MapPath("~/static/images/") + vendeur.NoVendeur + "_old" + urlImage.Substring(urlImage.IndexOf(".")));
                }
                catch
                {
                    errImage.Text = "L'image sélectionnée n'a pas pu être téléversée.";
                    errImage.CssClass = "text-danger";
                    binOK = false;
                }
            }

            if (binOK)
            {
                try
                {
                    fImage.SaveAs(Server.MapPath("~/static/images/") + vendeur.NoVendeur + fImage.FileName.Substring(fImage.FileName.LastIndexOf(".")));
                }
                catch (Exception ex)
                {
                    errImage.Text = "L'image sélectionnée n'a pas pu être téléversée. L'erreur suivante s'est produite : " + ex.Message;
                    errImage.CssClass = "text-danger";
                    binOK = false;
                }
            }

            if (urlImage != "image_magasin.jpg")
            {
                if (binOK)
                {
                    try
                    {
                        File.Delete(Server.MapPath("~/static/images/") + vendeur.NoVendeur + "_old" + urlImage.Substring(urlImage.IndexOf(".")));
                    }
                    catch { }
                }
                else
                {
                    try
                    {
                        File.Move(Server.MapPath("~/static/images/") + vendeur.NoVendeur + "_old" + urlImage.Substring(urlImage.IndexOf(".")),
                                  Server.MapPath("~/static/images/") + urlImage);
                    }
                    catch
                    {


                        try
                        {
                            configuration.Descendants("urlImage").Single().Value = vendeur.NoVendeur + "_old" + urlImage.Substring(urlImage.IndexOf("."));
                            document.Save(Server.MapPath("\\static\\xml\\" + vendeur.Configuration));

                            imgTeleverse.ImageUrl = "~/static/images/" + vendeur.NoVendeur + "_old" + urlImage.Substring(urlImage.IndexOf("."));
                        }
                        catch { }
                    }
                }
            }

            if (binOK)
            {
                try
                {
                    configuration.Descendants("urlImage").Single().Value = vendeur.NoVendeur + fImage.FileName.Substring(fImage.FileName.LastIndexOf("."));
                    document.Save(Server.MapPath("\\static\\xml\\" + vendeur.Configuration));

                    imgTeleverse.ImageUrl = "~/static/images/" + vendeur.NoVendeur + fImage.FileName.Substring(fImage.FileName.LastIndexOf("."));

                    if (urlImage == "image_magasin.jpg")
                    {
                        btnSelectionnerImage.Visible = false;
                        btnChangerImage.Visible = true;
                    }
                }
                catch
                {
                    errImage.Text = "L'image sélectionnée n'a pas pu être téléversée.";
                    errImage.CssClass = "text-danger";
                }
            }
        }
    }

    protected void btnRemiseAZero_Click(object sender, EventArgs e)
    {
        imgTeleverse.ImageUrl = "~/static/images/image_magasin.jpg";
        cpCouleurFond.Value = "#ffffff";
        cpCouleurTexte.Value = "#000000";
        btnSelectionnerImage.Visible = true;
        btnChangerImage.Visible = false;
        btnRemiseAZero.Enabled = false;
    }

    protected void btnModifierProfil_Click(object sender, EventArgs e)
    {
        if (validerPage())
        {
            long noVendeur = Convert.ToInt64(Session["NoVendeur"]);

            if (dbContext.PPVendeurs.Where(v => v.NoVendeur != noVendeur && v.NomAffaires == tbNomEntreprise.Text).Any())
            {
                lblMessage.Text = "Ce nom d'entreprise existe déjà";
                divMessage.CssClass = "alert alert-danger alert-margins";
            }
            else
            {
                PPVendeurs vendeur = dbContext.PPVendeurs.Where(v => v.NoVendeur == noVendeur).Single();
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
                vendeur.PoidsMaxLivraison = int.Parse(tbPoidsMaxLivraison.Text);
                vendeur.LivraisonGratuite = decimal.Parse(tbLivraisonGratuite.Text.Replace(".", ","));
                vendeur.Taxes = !cbTaxes.Checked;
                vendeur.DateMAJ = DateTime.Now;

                bool binOK = true;

                try
                {
                    XDocument document = XDocument.Load(Server.MapPath("\\static\\xml\\" + vendeur.Configuration));
                    XElement configuration = document.Element("configuration");

                    if (imgTeleverse.ImageUrl == "~/static/images/image_magasin.jpg")
                    {
                        try
                        {
                            if (configuration.Descendants("urlImage").Single().Value != "image_magasin.jpg")
                                File.Delete(Server.MapPath("~/static/images/") + configuration.Descendants("urlImage").Single().Value);
                        }
                        catch { }
                    }

                    configuration.Descendants("urlImage").Single().Value = imgTeleverse.ImageUrl.Substring(imgTeleverse.ImageUrl.LastIndexOf("/") + 1);
                    configuration.Descendants("couleurFond").Single().Value = cpCouleurFond.Value;
                    configuration.Descendants("couleurTexte").Single().Value = cpCouleurTexte.Value;
                    document.Save(Server.MapPath("\\static\\xml\\" + vendeur.Configuration));

                    dbContext.SaveChanges();
                }
                catch (Exception)
                {
                    binOK = false;
                }

                if (binOK)
                {
                    lblMessage.Text = "Votre profil a été modifié.";
                    divMessage.CssClass = "alert alert-success alert-margins";
                }
                else
                {
                    lblMessage.Text = "Votre profil n'a pas pu être modifié. Réessayez ultérieurement.";
                    divMessage.CssClass = "alert alert-danger alert-margins";
                }
            }
            errImage.Text = "";
            errImage.CssClass = "text-danger hidden";
            divMessage.Visible = true;
        }
        else
            afficherErreurs();
    }
}