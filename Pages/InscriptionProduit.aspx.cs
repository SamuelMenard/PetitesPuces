using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_InscriptionProduit : System.Web.UI.Page
{
    private BD6B8_424SEntities dbContext = new BD6B8_424SEntities();

    protected void chargerListeDeroulante()
    {
        var categories = dbContext.PPCategories;

        foreach (var categorie in categories)
            ddlCategorie.Items.Add(new ListItem(categorie.Description, categorie.NoCategorie.ToString()));
    }

    protected void initialiserDate()
    {
        tbDateVente.Text = DateTime.Now.AddDays(1).ToShortDateString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            chargerListeDeroulante();
            initialiserDate();
        }
        else
        {
            if (Session["fImage"] != null)
            {
                fImage = (FileUpload)Session["fImage"];
            }
        }
    }

    protected void btnInscription_Click(object sender, EventArgs e)
    {
        Regex exprTexte = new Regex("^[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9]+(([-' ][a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])|[a-zA-Z\u00C0-\u00D6\u00D9-\u00F6\u00F9-\u00FF0-9])*$");
        var exprMontant = new Regex("^\\d+\\.\\d{2}$");
        var exprNbItems = new Regex("^\\d+$");
        DateTime dateAujourdhui = DateTime.Now.Date;
        DateTime dateExpiration = Convert.ToDateTime(tbDateVente.Text + " 00:00:00");
        var exprPoids = new Regex("^\\d+(\\.\\d)?$");
        if (ddlCategorie.SelectedValue == "" ||
            tbNom.Text == "" ||
            !exprTexte.IsMatch(tbNom.Text) ||
            tbPrixDemande.Text == "" ||
            !exprMontant.IsMatch(tbPrixDemande.Text) ||
            double.Parse(tbPrixDemande.Text.Replace(".", ",")) > 214748.36 ||
            tbDescription.Text == "" ||
            !exprTexte.IsMatch(tbDescription.Text) ||
            !fImage.HasFile ||
            (fImage.PostedFile.ContentType != "image/jpeg" && fImage.PostedFile.ContentType != "image/png") ||
            fImage.PostedFile.ContentLength >= 31457280 ||
            tbNbItems.Text == "" ||
            !exprNbItems.IsMatch(tbNbItems.Text) ||
            int.Parse(tbNbItems.Text) > 32767 ||
            tbPrixVente.Text == "" ||
            !exprMontant.IsMatch(tbPrixVente.Text) ||
            double.Parse(tbPrixVente.Text.Replace(".", ",")) > 214748.36 ||
            (dateExpiration.Year <= dateAujourdhui.Year && dateExpiration.Month <= dateAujourdhui.Month && dateExpiration.Day <= dateAujourdhui.Day) ||
            tbPoids.Text == "" ||
            !exprPoids.IsMatch(tbPoids.Text) ||
            double.Parse(tbPoids.Text.Replace(".", ",")) > 66)
        {
            if (ddlCategorie.SelectedValue == "")
            {
                ddlCategorie.CssClass = "form-control border-danger";
                errCategorie.Text = "Vous devez sélectionner une catégorie";
                errCategorie.CssClass = "text-danger";
            }
            else
            {
                ddlCategorie.CssClass = "form-control border-success";
                errCategorie.Text = "";
                errCategorie.CssClass = "text-danger hidden";
            }
            if (tbNom.Text == "" || !exprTexte.IsMatch(tbNom.Text))
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
            if (tbPrixDemande.Text == "" || !exprMontant.IsMatch(tbPrixDemande.Text) || double.Parse(tbPrixDemande.Text.Replace(".", ",")) > 214748.36)
            {
                tbPrixDemande.CssClass = "form-control border-danger";
                if (tbPrixDemande.Text == "")
                    errPrixDemande.Text = "Le prix demandé ne peut pas être vide";
                else if (!exprMontant.IsMatch(tbPrixDemande.Text))
                    errPrixDemande.Text = "Le prix demandé doit être un nombre décimal avec deux chiffres après la virgule";
                else
                    errPrixDemande.Text = "Le prix demandé doit être inférieur à 214 748,37 $";
                errPrixDemande.CssClass = "text-danger";
            }
            else
            {
                tbPrixDemande.CssClass = "form-control border-success";
                errPrixDemande.Text = "";
                errPrixDemande.CssClass = "text-danger hidden";
            }
            if (tbDescription.Text == "" || !exprTexte.IsMatch(tbDescription.Text))
            {
                tbDescription.CssClass = "form-control border-danger";
                if (tbDescription.Text == "")
                    errDescription.Text = "La description ne peut pas être vide";
                else
                    errDescription.Text = "La description n'est pas dans un format valide";
                errDescription.CssClass = "text-danger";
            }
            else
            {
                tbDescription.CssClass = "form-control border-success";
                errDescription.Text = "";
                errDescription.CssClass = "text-danger hidden";
            }
            if (!fImage.HasFile || (fImage.PostedFile.ContentType != "image/jpeg" && fImage.PostedFile.ContentType != "image/png") || fImage.PostedFile.ContentLength >= 31457280)
            {
                fImage.CssClass += " border-danger";
                if (!fImage.HasFile)
                    errImage.Text = "Vous devez sélectionner une image";
                else if (fImage.PostedFile.ContentType != "image/jpeg" && fImage.PostedFile.ContentType != "image/png")
                    errImage.Text = "L'image sélectionnée doit être au format jpeg ou png";
                else
                    errImage.Text = "L'image sélectionnée doit être inférieure à 30 mo";
                errImage.CssClass = "text-danger";
            }
            else
            {
                Session["fImage"] = fImage;
                fImage.CssClass = "form-control border-success";
                errImage.Text = "";
                errImage.CssClass = "text-danger hidden";
            }
            if (tbNbItems.Text == "" || !exprNbItems.IsMatch(tbNbItems.Text) || int.Parse(tbNbItems.Text) > 32767)
            {
                tbNbItems.CssClass = "form-control border-danger";
                if (tbNbItems.Text == "")
                    errNbItems.Text = "La quantité ne peut pas être vide";
                else if (!exprNbItems.IsMatch(tbNbItems.Text))
                    errNbItems.Text = "La quantité doit être un entier";
                else
                    errNbItems.Text = "La quantité ne peut pas dépasser 32 767 items";
                errNbItems.CssClass = "text-danger";
            }
            else
            {
                tbNbItems.CssClass = "form-control border-success";
                errNbItems.Text = "";
                errNbItems.CssClass = "text-danger hidden";
            }
            if (tbPrixVente.Text == "" || !exprMontant.IsMatch(tbPrixVente.Text) || double.Parse(tbPrixVente.Text.Replace(".", ",")) > 214748.36)
            {
                tbPrixVente.CssClass = "form-control border-danger";
                if (tbPrixVente.Text == "")
                    errPrixVente.Text = "Le prix de vente ne peut pas être vide')";
                else if (!exprMontant.IsMatch(tbPrixVente.Text))
                    errPrixVente.Text = "Le prix de vente doit être un nombre décimal avec deux chiffres après la virgule";
                else
                    errPrixVente.Text = "Le prix de vente doit être inférieur à 214 748,37 $";
                errPrixVente.CssClass = "text-danger";
            }
            else
            {
                tbPrixVente.CssClass = "form-control border-success";
                errPrixVente.Text = "";
                errPrixVente.CssClass = "text-danger hidden";
            }
            if (dateExpiration.Year <= dateAujourdhui.Year && dateExpiration.Month <= dateAujourdhui.Month && dateExpiration.Day <= dateAujourdhui.Day)
            {
                tbDateVente.CssClass = "form-control border-danger";
                errDateVente.Text = "La date d'expiration du prix de vente doit être supérieure à la date d'aujourd'hui";
                errDateVente.CssClass = "text-danger";
            }
            else
            {
                tbDateVente.CssClass = "form-control border-success";
                errDateVente.Text = "";
                errDateVente.CssClass = "text-danger hidden";
            }
            if (tbPoids.Text == "" || !exprPoids.IsMatch(tbPoids.Text) || double.Parse(tbPoids.Text.Replace(".", ",")) > 66)
            {
                tbPoids.CssClass = "form-control border-danger";
                if (tbPoids.Text == "")
                    errPoids.Text = "Le poids ne peut pas être vide";
                else if (!exprPoids.IsMatch(tbPoids.Text))
                    errPoids.Text = "Le poids doit être un entier ou un nombre décimal avec un chiffre après la virgule";
                else
                    errPoids.Text = "Le poids de l'article ne peut pas dépasser le poids de livraison maximum permis de 66 lbs";
                errPoids.CssClass = "text-danger";
            }
            else
            {
                tbPoids.CssClass = "form-control border-success";
                errPoids.Text = "";
                errPoids.CssClass = "text-danger hidden";
            }
        }
        else
        {
            /*try
            {
                fImage.SaveAs(Server.MapPath("~/static/images/") + fImage.FileName);
            }
            catch (Exception ex)
            {
                fImage.CssClass += " border-danger";
                errImage.Text = "L'image sélectionnée n'a pas pu être téléversée. L'erreur suivante est survenue : " + ex.Message;
                errImage.CssClass = "text-danger";
            }*/
        }
    }
}