﻿using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
            
            if (Request.UrlReferrer != null)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString();
            }            
            chargerListeDeroulante();
            initialiserDate();

            Page.Title = "S'inscrire comme client";

            if (Request.QueryString["Operation"] != null)
            {
                string operation = Request.QueryString["Operation"].ToString();
                if (operation == "Afficher" || operation == "Modifier" || operation == "Supprimer")
                {
                    if (Request.QueryString["NoProduit"] != null)
                    {
                        if (Regex.IsMatch(Request.QueryString["NoProduit"].ToString(), "^\\d{7}$"))
                        {
                            long noProduit = long.Parse(Request.QueryString["NoProduit"]);
                            long noVendeur = Convert.ToInt64(Session["NoVendeur"]);
                            if (dbContext.PPProduits.Where(p => p.NoProduit == noProduit && p.NoVendeur == noVendeur).Any())
                            {
                                PPProduits produit = dbContext.PPProduits.Where(p => p.NoProduit == noProduit).Single();
                                tbNo.Text = produit.NoProduit.ToString();
                                ddlCategorie.SelectedValue = produit.NoCategorie.ToString();
                                tbNom.Text = produit.Nom;
                                tbPrixDemande.Text = produit.PrixDemande.ToString()
                                                                        .Remove(produit.PrixDemande.ToString().IndexOf(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator) + 3)
                                                                        .Replace(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, ".");
                                tbDescription.Text = produit.Description;
                                tbDateCreation.Text = produit.DateCreation.Value.ToShortDateString();
                                tbNbItems.Text = produit.NombreItems.ToString();
                                if (produit.PrixVente != null)
                                {
                                    tbPrixVente.Text = produit.PrixVente.ToString()
                                                                        .Remove(produit.PrixVente.ToString().IndexOf(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator) + 3)
                                                                        .Replace(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, ".");
                                    tbDateVente.Text = produit.DateVente.Value.ToShortDateString();
                                    divDateVente.CssClass = "form-group";
                                }
                                tbPoids.Text = produit.Poids.ToString().Replace(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, ".");
                                if (!(bool)produit.Disponibilité)
                                {
                                    btnOui.CssClass = "btn Orange notActive";
                                    btnNon.CssClass = "btn Orange active";
                                }
                                rbDisponibilite.Value = (bool)produit.Disponibilité ? "O" : "N";
                                imgTeleverse.ImageUrl = "~/static/images/" + produit.Photo;

                                if (operation == "Afficher" || operation == "Supprimer")
                                {
                                    
                                    ddlCategorie.Enabled = false;
                                    errCategorie.Visible = false;
                                    tbNom.Enabled = false;
                                    errNom.Visible = false;
                                    tbPrixDemande.Enabled = false;
                                    errPrixDemande.Visible = false;
                                    tbDescription.Enabled = false;
                                    errDescription.Visible = false;
                                    lblImage.Visible = true;
                                    fImage.Visible = false;
                                    errImage.Visible = false;
                                    btnSelectionnerImage.Visible = false;
                                    btnTeleverserImage.Visible = false;
                                    tbNbItems.Enabled = false;
                                    errNbItems.Visible = false;
                                    if (produit.PrixVente != null)
                                    {
                                        tbPrixVente.Enabled = false;
                                        errPrixVente.Visible = false;
                                        tbDateVente.Enabled = false;
                                        errDateVente.Visible = false;
                                    }
                                    else
                                    {
                                        divNbItems.CssClass = "form-group col-sm-12";
                                        divPrixVente.Visible = false;
                                        divDateVente.Visible = false;
                                    }
                                    tbPoids.Enabled = false;
                                    errPoids.Visible = false;
                                }

                                lblCategorie.Visible = true;
                                lblNom.Visible = true;
                                lblPrixDemande.Visible = true;
                                lblDescription.Visible = true;
                                divDateCreation.Visible = true;
                                lblNbItems.Visible = true;
                                lblPrixVente.Visible = true;
                                if (produit.PrixVente != null)
                                    lblDateVente.InnerText = "Date d’expiration du prix de vente";
                                lblPoids.Visible = true;
                                lblDisponibilite.Visible = true;
                                lblDisponibiliteAjout.Visible = false;

                                if (operation == "Afficher")
                                {
                                    Page.Title = produit.Nom;

                                    btnRetour.Visible = true;
                                }    
                                else if (operation == "Modifier")
                                {
                                    Page.Title = "Modification produit";

                                    btnChangerImage.Visible = true;
                                    btnModifier.Visible = true;
                                }
                                else if (operation == "Supprimer")
                                {
                                    Page.Title = "Suppression produit";

                                    if (dbContext.PPArticlesEnPanier.Where(a => a.NoProduit == noProduit).Count() > 0)
                                        btnSupprimer.OnClientClick = "if (!confirm('Ce produit a été déposé dans un panier. Voulez-vous vraiment le supprimer?')) { return false; }";
                                    else
                                        btnSupprimer.OnClientClick = "if (!confirm('Voulez-vous vraiment supprimer ce produit?')) { return false; }";
                                    btnSupprimer.Visible = true;
                                }
                            }
                            else
                            {
                                object refUrl = ViewState["RefUrl"];
                                if (refUrl != null)
                                    Response.Redirect((string)refUrl);
                                else
                                    Response.Redirect("~/Pages/SuppressionProduit.aspx?");
                            }
                                
                        }
                        else
                        {
                            object refUrl = ViewState["RefUrl"];
                            if (refUrl != null)
                                Response.Redirect((string)refUrl);
                            else
                                Response.Redirect("~/Pages/SuppressionProduit.aspx?");
                        }
                    }
                    else
                    {
                        object refUrl = ViewState["RefUrl"];
                        if (refUrl != null)
                            Response.Redirect((string)refUrl);
                        else
                            Response.Redirect("~/Pages/SuppressionProduit.aspx?");
                    }
                }
                else
                {
                    object refUrl = ViewState["RefUrl"];
                    if (refUrl != null)
                        Response.Redirect((string)refUrl);
                    else
                        Response.Redirect("~/Pages/SuppressionProduit.aspx?");
                }
            }
            else
            {
                Page.Title = "Ajout produit";

                long noVendeur = Convert.ToInt64(Session["NoVendeur"]);
                long nbProduit = 0;
                foreach (PPProduits produit in dbContext.PPProduits.Where(p => p.NoVendeur == noVendeur))
                    if (long.Parse(produit.NoProduit.ToString().Substring(2)) > nbProduit)
                        nbProduit = long.Parse(produit.NoProduit.ToString().Substring(2));
                tbNo.Text = string.Format("{0}{1:D5}", noVendeur, nbProduit + 1);
                btnSelectionnerImage.Visible = true;
                btnInscription.Visible = true;
            }  
        }
        else
            divMessage.Visible = false;

        if (Request.QueryString["Operation"] == null || (Request.QueryString["Operation"] != null && Request.QueryString["Operation"] == "Modifier"))
        {
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
        Regex exprMontant = new Regex("^\\d+(\\.\\d{2})?$");
        Regex exprNbItems = new Regex("^\\d+$");
        Regex exprPoids = new Regex("^\\d+(\\.\\d)?$");
        bool dateExpirationValide = false;
        if (tbDateVente.Text != "")
            if (Convert.ToDateTime(tbDateVente.Text).Date > DateTime.Now.Date)
                dateExpirationValide = true;
        return ddlCategorie.SelectedValue != "" &&
               tbNom.Text != "" && exprTexte.IsMatch(tbNom.Text) &&
               tbPrixDemande.Text != "" && exprMontant.IsMatch(tbPrixDemande.Text) && double.Parse(tbPrixDemande.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)) <= 214748.36 &&
               tbDescription.Text != "" && exprTexte.IsMatch(tbDescription.Text) &&
               imgTeleverse.ImageUrl != "~/static/images/image_placeholder.png" &&
               tbNbItems.Text != "" && exprNbItems.IsMatch(tbNbItems.Text) && int.Parse(tbNbItems.Text) <= 32767 &&
               (tbPrixVente.Text == "" || (tbPrixVente.Text != "" && exprMontant.IsMatch(tbPrixVente.Text) && double.Parse(tbPrixVente.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)) <= double.Parse(tbPrixDemande.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)) && double.Parse(tbPrixVente.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)) <= 214748.36 && dateExpirationValide)) &&
               tbPoids.Text != "" && exprPoids.IsMatch(tbPoids.Text) && double.Parse(tbPoids.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)) < 10000000;
    }

    protected void afficherErreurs()
    {
        Regex exprTexte = new Regex("^[\\w\\s!\"$%?&()\\-;:«»°,'.]+$");
        Regex exprMontant = new Regex("^\\d+(\\.\\d{2})?$");
        Regex exprNbItems = new Regex("^\\d+$");
        Regex exprPoids = new Regex("^\\d+(\\.\\d)?$");
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
                errNom.Text = "Le nom n'est pas valide";
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
                errPrixDemande.Text = "Le prix demandé doit être un nombre positif";
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
                errDescription.Text = "La description n'est pas valide";
            errDescription.CssClass = "text-danger";
        }
        else
        {
            tbDescription.CssClass = "form-control border-success";
            errDescription.Text = "";
            errDescription.CssClass = "text-danger hidden";
        }
        if (imgTeleverse.ImageUrl == "~/static/images/image_placeholder.png")
        {
            imgTeleverse.CssClass = "thumbnail img-responsive border-danger";
            if (errImage.Text == "")
                errImage.Text = "Vous devez sélectionner une image";
            errImage.CssClass = "text-danger";
        }
        else
        {
            imgTeleverse.CssClass = "thumbnail img-responsive border-success";
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
        if (tbPrixVente.Text != "")
        {
            if (!exprMontant.IsMatch(tbPrixVente.Text) || double.Parse(tbPrixVente.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)) > double.Parse(tbPrixDemande.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)) || double.Parse(tbPrixVente.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)) > 214748.36)
            {
                tbPrixVente.CssClass = "form-control border-danger";
                if (!exprMontant.IsMatch(tbPrixVente.Text))
                    errPrixVente.Text = "Le prix de vente doit être un nombre positif";
                else if (double.Parse(tbPrixVente.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)) > double.Parse(tbPrixDemande.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)))
                    errPrixVente.Text = "Le prix de vente ne peut pas être supérieur au prix demandé";
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
            if (tbDateVente.Text == "")
            {
                tbDateVente.CssClass = "form-control border-danger";
                errDateVente.Text = "Vous devez sélectionner une date d'expiration du prix de vente";
                errDateVente.CssClass = "text-danger";
            }
            else if (Convert.ToDateTime(tbDateVente.Text).Date <= DateTime.Now.Date)
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
        }
        else
        {
            tbPrixVente.CssClass = "form-control border-success";
            errPrixVente.Text = "";
            errPrixVente.CssClass = "text-danger hidden";
        }
        if (tbPoids.Text == "" || !exprPoids.IsMatch(tbPoids.Text) || double.Parse(tbPoids.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)) > 9999999.9)
        {
            tbPoids.CssClass = "form-control border-danger";
            if (tbPoids.Text == "")
                errPoids.Text = "Le poids ne peut pas être vide";
            else if (!exprPoids.IsMatch(tbPoids.Text))
                errPoids.Text = "Le poids doit être un entier ou un nombre décimal avec un chiffre après la virgule";
            else
                errPoids.Text = "Le poids doit être inférieur à 10 000 000 lbs";
            errPoids.CssClass = "text-danger";
        }
        else
        {
            tbPoids.CssClass = "form-control border-success";
            errPoids.Text = "";
            errPoids.CssClass = "text-danger hidden";
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
                    if (File.Exists(Server.MapPath("~/static/images/") + noProduit + ".jpg"))
                        File.Delete(Server.MapPath("~/static/images/") + noProduit + ".jpg");
                    else if (File.Exists(Server.MapPath("~/static/images/") + noProduit + ".JPG"))
                        File.Delete(Server.MapPath("~/static/images/") + noProduit + ".JPG");
                    else if (File.Exists(Server.MapPath("~/static/images/") + noProduit + ".png"))
                        File.Delete(Server.MapPath("~/static/images/") + noProduit + ".png");
                    else if (File.Exists(Server.MapPath("~/static/images/") + noProduit + ".PNG"))
                        File.Delete(Server.MapPath("~/static/images/") + noProduit + ".PNG");
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
                        fImage.SaveAs(Server.MapPath("~/static/images/") + noProduit + fImage.FileName.Substring(fImage.FileName.LastIndexOf(".")));
                    }
                    catch (Exception ex)
                    {
                        errImage.Text = "L'image sélectionnée n'a pas pu être téléversée. L'erreur suivante est survenue : " + ex.Message;
                        errImage.CssClass = "text-danger";
                        binOK = false;
                    }
                }

                if (binOK)
                {
                    imgTeleverse.ImageUrl = "~/static/images/" + noProduit + fImage.FileName.Substring(fImage.FileName.LastIndexOf("."));
                    btnSelectionnerImage.Visible = false;
                    btnChangerImage.Visible = true;
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
                    catch { }
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
                        catch { }
                    }
                }

                if (binOK)
                {
                    imgTeleverse.ImageUrl = "~/static/images/" + produit.NoProduit + fImage.FileName.Substring(fImage.FileName.LastIndexOf("."));
                }
            }
        }

        if (rbDisponibilite.Value != "O")
        {
            btnOui.CssClass = "btn Orange notActive";
            btnNon.CssClass = "btn Orange active";
        }
        else
        {
            btnOui.CssClass = "btn Orange active";
            btnNon.CssClass = "btn Orange notActive";
        }
    }

    protected void btnInscription_Click(object sender, EventArgs e)
    {
        if (validerPage())
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
            if (tbPrixVente.Text == "")
            {
                nouveauProduit.DateVente = null;
                nouveauProduit.PrixVente = null;
            }
            else
            {
                nouveauProduit.DateVente = Convert.ToDateTime(tbDateVente.Text + " 00:00:00");
                nouveauProduit.PrixVente = decimal.Parse(tbPrixVente.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
            }
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
                if (controle.HasControls())
                    foreach (Control controleEnfant in controle.Controls)
                        if (controleEnfant is TextBox)
                            ((TextBox)controleEnfant).Text = "";
                        else if (controleEnfant is DropDownList)
                            ((DropDownList)controleEnfant).ClearSelection();
                else
                    if (controle is TextBox)
                        ((TextBox)controle).Text = "";
                    else if (controle is DropDownList)
                        ((DropDownList)controle).ClearSelection();          
            tbNo.Text = (nouveauProduit.NoProduit + 1).ToString();
            imgTeleverse.ImageUrl = "~/static/images/image_placeholder.png";
            btnSelectionnerImage.Visible = true;
            btnChangerImage.Visible = false;
            tbNbItems.Text = "";
            initialiserDate();
            btnOui.CssClass = "btn Orange active";
            btnNon.CssClass = "btn Orange notActive";
            rbDisponibilite.Value = "O";
            divMessage.Visible = true;
        }
        else
        {
            afficherErreurs();

            if (rbDisponibilite.Value != "O")
            {
                btnOui.CssClass = "btn Orange notActive";
                btnNon.CssClass = "btn Orange active";
            }
            else
            {
                btnOui.CssClass = "btn Orange active";
                btnNon.CssClass = "btn Orange notActive";
            }
        }
    }

    protected void btnRetour_Click(object sender, EventArgs e)
    {
        
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
            else
                Response.Redirect("~/Pages/SuppressionProduit.aspx?");
        
    }

    protected void btnModifier_Click(object sender, EventArgs e)
    {
        if (validerPage())
        {
            long noProduit = long.Parse(Request.QueryString["NoProduit"]);

            PPProduits produit = dbContext.PPProduits.Where(p => p.NoProduit == noProduit).Single();
            produit.NoCategorie = int.Parse(ddlCategorie.SelectedValue);
            produit.Nom = tbNom.Text;
            produit.Description = tbDescription.Text;
            produit.PrixDemande = decimal.Parse(tbPrixDemande.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
            produit.NombreItems = short.Parse(tbNbItems.Text);
            produit.Disponibilité = rbDisponibilite.Value == "O" ? true : false;
            if (tbPrixVente.Text == "")
            {
                produit.DateVente = null;
                produit.PrixVente = null;
            }
            else
            {
                produit.DateVente = Convert.ToDateTime(tbDateVente.Text + " 00:00:00");
                produit.PrixVente = decimal.Parse(tbPrixVente.Text.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
            }
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
        else
        {
            afficherErreurs();

            if (rbDisponibilite.Value != "O")
            {
                btnOui.CssClass = "btn Orange notActive";
                btnNon.CssClass = "btn Orange active";
            }
            else
            {
                btnOui.CssClass = "btn Orange active";
                btnNon.CssClass = "btn Orange notActive";
            }
        }
    }

    protected void btnSupprimer_Click(object sender, EventArgs e)
    {
        long noProduit = long.Parse(Request.QueryString["NoProduit"]);
        PPProduits produit = dbContext.PPProduits.Where(p => p.NoProduit == noProduit).First();

        using (var dbContextTransaction = dbContext.Database.BeginTransaction())
        {
            try
            {
                if (dbContext.PPArticlesEnPanier.Where(a => a.NoProduit == noProduit).Any())
                    dbContext.PPArticlesEnPanier.RemoveRange(dbContext.PPArticlesEnPanier.Where(a => a.NoProduit == noProduit));
                if (dbContext.PPDetailsCommandes.Where(d => d.NoProduit == noProduit).Any())
                {             
                    produit.NombreItems = 0;                       
                    produit.Disponibilité = null;
                }
                else
                {
                    File.Delete(Server.MapPath("~/static/images/") + produit.Photo);
                    dbContext.PPProduits.Remove(produit);
                }
                dbContext.SaveChanges();
                dbContextTransaction.Commit();
                string url = "~/Pages/SuppressionProduit.aspx?ResultatSuppr=OK";
                Response.Redirect(url, false);
            }
            catch
            {
                dbContextTransaction.Rollback();
                string url = "~/Pages/SuppressionProduit.aspx?ResultatSuppr=PasOk";
                Response.Redirect(url, false);
            }
        }
    }
}