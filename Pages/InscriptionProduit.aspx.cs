using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Transactions;
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
            
            if (Request.QueryString["NoProduit"] != null)
            {
                long noProduit = long.Parse(Request.QueryString["NoProduit"]);
                PPProduits produit = dbContext.PPProduits.Where(p => p.NoProduit == noProduit).Single();
                ddlCategorie.SelectedValue = produit.NoCategorie.ToString();
                tbNom.Text = produit.Nom;
                tbPrixDemande.Text = produit.PrixDemande.ToString()
                                                        .Remove(produit.PrixDemande.ToString().IndexOf(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator) + 3)
                                                        .Replace(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, ".");
                tbDescription.Text = produit.Description;
                tbNbItems.Text = produit.NombreItems.ToString();
                tbPrixVente.Text = produit.PrixVente.ToString()
                                                    .Remove(produit.PrixVente.ToString().IndexOf(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator) + 3)
                                                    .Replace(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, ".");
                tbDateVente.Text = produit.DateVente.Value.ToShortDateString();
                tbPoids.Text = produit.Poids.ToString().Replace(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, ".");
                if (!(bool)produit.Disponibilité)
                {
                    btnOui.CssClass = "btn Orange notActive";
                    btnNon.CssClass = "btn Orange active";
                }
                rbDisponibilite.Value = (bool)produit.Disponibilité ? "O" : "N";
                btnValider.Visible = false;
                imgTeleverse.ImageUrl = "~/static/images/" + produit.Photo;
                divImage.Visible = true;
                btnModifier.Visible = true;
            }

            ClientScript.RegisterStartupScript(GetType(), "toggleRbDisponibilite",
                "<script>" +
                "   $('#radioBtn a').on('click', function () {" +
                "      var sel = $(this).data('title');" +
                "      var tog = $(this).data('toggle');" +
                "      $('#contentBody_' + tog).prop('value', sel);" +
                "      $('a[data-toggle=\"' + tog + '\"]').not('[data-title=\"' + sel + '\"]').removeClass('active').addClass('notActive');" +
                "      $('a[data-toggle=\"' + tog + '\"][data-title=\"' + sel + '\"]').removeClass('notActive').addClass('active');" +
                "   });" +
                "</script>");
        }
    }

    protected bool validerPage()
    {
        Regex exprTexte = new Regex("^[\\w\\s!\"$%?&()\\-;:«»°,'.]+$");
        Regex exprMontant = new Regex("^\\d+\\.\\d{2}$");
        Regex exprNbItems = new Regex("^\\d+$");
        DateTime dateAujourdhui = DateTime.Now.Date;
        DateTime dateExpiration = Convert.ToDateTime(tbDateVente.Text + " 00:00:00");
        Regex exprPoids = new Regex("^\\d+(\\.\\d)?$");
        bool binValide = true;
        if (ddlCategorie.SelectedValue == "" ||
            tbNom.Text == "" || !exprTexte.IsMatch(tbNom.Text) ||
            tbPrixDemande.Text == "" || !exprMontant.IsMatch(tbPrixDemande.Text) || double.Parse(tbPrixDemande.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)) > 214748.36 ||
            tbDescription.Text == "" || !exprTexte.IsMatch(tbDescription.Text) ||
            tbNbItems.Text == "" || !exprNbItems.IsMatch(tbNbItems.Text) || int.Parse(tbNbItems.Text) > 32767 ||
            tbPrixVente.Text == "" || !exprMontant.IsMatch(tbPrixVente.Text) || double.Parse(tbPrixVente.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)) > 214748.36 ||
            (dateExpiration.Year <= dateAujourdhui.Year && dateExpiration.Month <= dateAujourdhui.Month && dateExpiration.Day <= dateAujourdhui.Day) ||
            tbPoids.Text == "" || !exprPoids.IsMatch(tbPoids.Text) || double.Parse(tbPoids.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)) > 66)
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
            if (tbPrixDemande.Text == "" || !exprMontant.IsMatch(tbPrixDemande.Text) || double.Parse(tbPrixDemande.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)) > 214748.36)
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
            if (tbPrixVente.Text == "" || !exprMontant.IsMatch(tbPrixVente.Text) || double.Parse(tbPrixVente.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)) > 214748.36)
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
            if (tbPoids.Text == "" || !exprPoids.IsMatch(tbPoids.Text) || double.Parse(tbPoids.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)) > 66)
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
            binValide = false;
        }
        return binValide;
    }

    protected void btnValider_Click(object sender, EventArgs e)
    {
        if (validerPage())
        {
            ddlCategorie.Enabled = false;
            tbNom.Enabled = false;
            tbPrixDemande.Enabled = false;
            tbDescription.Enabled = false;
            tbNbItems.Enabled = false;
            tbPrixVente.Enabled = false;
            tbDateVente.Enabled = false;
            tbPoids.Enabled = false;
            if (rbDisponibilite.Value != "O")
            {
                btnOui.CssClass = "btn Orange notActive";
                btnNon.CssClass = "btn Orange active";
            }
            ClientScript.RegisterStartupScript(GetType(), "toggleRbDisponibilite", "");

            btnValider.Visible = false;
            divImage.Visible = true;

            if (Request.QueryString["NoProduit"] == null)
            {
                btnInscription.Visible = true;
                btnInscription.Enabled = false;
            }   
            else
                btnModifier.Visible = true;
        }
    }

    protected void btnInscription_Click(object sender, EventArgs e)
    {
        long noVendeur = Convert.ToInt64(Session["NoVendeur"]);
        long nbProduit = 0;
        foreach (PPProduits produit in dbContext.PPProduits.Where(p => p.NoVendeur == noVendeur))
            if (long.Parse(produit.NoProduit.ToString().Substring(2)) > nbProduit)
                nbProduit = long.Parse(produit.NoProduit.ToString().Substring(2));

        PPProduits nouveauProduit = new PPProduits();
        nouveauProduit.NoProduit = long.Parse(string.Format("{0}{1:D5}", noVendeur, nbProduit + 1));
        nouveauProduit.NoVendeur = noVendeur;
        nouveauProduit.NoCategorie = int.Parse(ddlCategorie.SelectedValue);
        nouveauProduit.Nom = tbNom.Text;
        nouveauProduit.Description = tbDescription.Text;
        nouveauProduit.Photo = imgTeleverse.ImageUrl.Substring(imgTeleverse.ImageUrl.LastIndexOf("/")+1);
        nouveauProduit.PrixDemande = decimal.Parse(tbPrixDemande.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
        nouveauProduit.NombreItems = short.Parse(tbNbItems.Text);
        nouveauProduit.Disponibilité = rbDisponibilite.Value == "O" ? true : false;
        nouveauProduit.DateVente = Convert.ToDateTime(tbDateVente.Text + " 00:00:00");
        nouveauProduit.PrixVente = decimal.Parse(tbPrixVente.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
        nouveauProduit.Poids = decimal.Parse(tbPoids.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
        nouveauProduit.DateCreation = DateTime.Now;

        dbContext.PPProduits.Add(nouveauProduit);

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
            lblMessage.Text = "Le produit a été créé.";
            divMessage.CssClass = "alert alert-success alert-margins";
        }
        else
        {
            lblMessage.Text = "Le produit n'a pas pu être créé. Réessayez ultérieurement.";
            divMessage.CssClass = "alert alert-danger alert-margins";
        }

        foreach (Control controle in Page.Form.Controls)
        {
            if (controle.HasControls())
            {
                foreach (Control controleEnfant in controle.Controls)
                {
                    if (controleEnfant is TextBox)
                    {
                        ((TextBox)controleEnfant).Text = "";
                        ((TextBox)controleEnfant).Enabled = true;
                    }

                    else if (controleEnfant is DropDownList)
                    {
                        ((DropDownList)controleEnfant).ClearSelection();
                        ((DropDownList)controleEnfant).Enabled = true;
                    }
                }
            }
            else
            {
                if (controle is TextBox)
                {
                    ((TextBox)controle).Text = "";
                    ((TextBox)controle).Enabled = true;
                }
                else if (controle is DropDownList)
                {
                    ((DropDownList)controle).ClearSelection();
                    ((DropDownList)controle).Enabled = true;
                }
            }
        }
        initialiserDate();
        btnOui.CssClass = "btn Orange active";
        btnNon.CssClass = "btn Orange notActive";
        rbDisponibilite.Value = "O";
        ClientScript.RegisterStartupScript(GetType(), "toggleRbDisponibilite",
            "<script>" +
            "   $('#radioBtn a').on('click', function () {" +
            "      var sel = $(this).data('title');" +
            "      var tog = $(this).data('toggle');" +
            "      $('#contentBody_' + tog).prop('value', sel);" +
            "      $('a[data-toggle=\"' + tog + '\"]').not('[data-title=\"' + sel + '\"]').removeClass('active').addClass('notActive');" +
            "      $('a[data-toggle=\"' + tog + '\"][data-title=\"' + sel + '\"]').removeClass('notActive').addClass('active');" +
            "   });" +
            "</script>");

        divMessage.Visible = true;
        btnValider.Visible = true;
        divImage.Visible = false;
        btnInscription.Visible = false;
    }

    protected void btnModifier_Click(object sender, EventArgs e)
    {
        long noProduit = long.Parse(Request.QueryString["NoProduit"]);

        PPProduits produit = dbContext.PPProduits.Where(p => p.NoProduit == noProduit).Single();
        produit.NoCategorie = int.Parse(ddlCategorie.SelectedValue);
        produit.Nom = tbNom.Text;
        produit.Description = tbDescription.Text;
        produit.PrixDemande = decimal.Parse(tbPrixDemande.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
        produit.NombreItems = short.Parse(tbNbItems.Text);
        produit.Disponibilité = rbDisponibilite.Value == "O" ? true : false;
        produit.DateVente = Convert.ToDateTime(tbDateVente.Text + " 00:00:00");
        produit.PrixVente = decimal.Parse(tbPrixVente.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
        produit.Poids = decimal.Parse(tbPoids.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
        produit.DateMAJ = DateTime.Now;

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
            Response.Redirect("~/Pages/SuppressionProduit.aspx?ResultatModif=OK");
        else
            Response.Redirect("~/Pages/SuppressionProduit.aspx?ResultatModif=PasOk");
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

            if (Request.QueryString["NoProduit"] == null)
            {
                long noVendeur = Convert.ToInt64(Session["NoVendeur"]);
                long nbProduit = 0;
                foreach (PPProduits produit in dbContext.PPProduits.Where(p => p.NoVendeur == noVendeur))
                    if (long.Parse(produit.NoProduit.ToString().Substring(2)) > nbProduit)
                        nbProduit = long.Parse(produit.NoProduit.ToString().Substring(2));
                long noProduit = long.Parse(string.Format("{0}{1:D5}", noVendeur, nbProduit + 1));

                try
                {
                    fImage.SaveAs(Server.MapPath("~/static/images/") + noProduit + fImage.FileName.Substring(fImage.FileName.LastIndexOf(".")));
                }
                catch (Exception ex)
                {
                    errImage.Text = "L'image sélectionnée n'a pas pu être téléversée. L'erreur suivante est survenue : " + ex.Message;
                    errImage.CssClass = "text-danger";
                    binOK = false;
                }

                if (binOK)
                {
                    imgTeleverse.ImageUrl = "~/static/images/" + noProduit + fImage.FileName.Substring(fImage.FileName.LastIndexOf("."));
                    btnInscription.Enabled = true;
                }
            }
            else
            {
                long noProduit = long.Parse(Request.QueryString["NoProduit"]);
                PPProduits produit = dbContext.PPProduits.Where(p => p.NoProduit == noProduit).Single();

                try
                {
                    File.Move(Server.MapPath("~/static/images/") + produit.Photo,
                              Server.MapPath("~/static/images/") + produit.NoProduit + "_old" + produit.Photo.Substring(produit.Photo.IndexOf(".")));
                }
                catch
                {
                    errImage.Text = "L'image sélectionnée n'a pas pu être téléversée.";
                    errImage.CssClass = "text-danger";
                    binOK = false;
                }

                if (binOK)
                {
                    try
                    {
                        fImage.SaveAs(Server.MapPath("~/static/images/") + produit.NoProduit + fImage.FileName.Substring(fImage.FileName.LastIndexOf(".")));
                    }
                    catch (Exception ex)
                    {
                        errImage.Text = "L'image sélectionnée n'a pas pu être téléversée. L'erreur suivante s'est produite : " + ex.Message;
                        errImage.CssClass = "text-danger";
                        binOK = false;
                    }
                }

                if (binOK)
                {
                    try
                    {
                        File.Delete(Server.MapPath("~/static/images/") + produit.NoProduit + "_old" + produit.Photo.Substring(produit.Photo.IndexOf(".")));
                    }
                    catch {}
                }
                else
                {
                    try
                    {
                        File.Move(Server.MapPath("~/static/images/") + produit.NoProduit + "_old" + produit.Photo.Substring(produit.Photo.IndexOf(".")),
                                  Server.MapPath("~/static/images/") + produit.Photo);
                    }
                    catch
                    {
                        produit.Photo = produit.NoProduit + "_old" + produit.Photo.Substring(produit.Photo.IndexOf("."));

                        imgTeleverse.ImageUrl = "~/static/images/" + produit.NoProduit + "_old" + produit.Photo.Substring(produit.Photo.IndexOf("."));

                        try
                        {
                            dbContext.SaveChanges();
                        }
                        catch {}
                    }
                }

                if (binOK)
                    imgTeleverse.ImageUrl = "~/static/images/" + produit.NoProduit + fImage.FileName.Substring(fImage.FileName.LastIndexOf("."));
            }
        }
    }
}