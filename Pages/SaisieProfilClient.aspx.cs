using System;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_SaisieProfilClient : System.Web.UI.Page
{
    private BD6B8_424SEntities dbContext = new BD6B8_424SEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            verifierPermissions("C");
            long noClient = 0;
            if(Session["NoClient"] != null)
                 noClient = Convert.ToInt64(Session["NoClient"]);

            PPClients client = dbContext.PPClients.Where(v => v.NoClient == noClient).Single();
            tbNom.Text = client.Nom;
            tbPrenom.Text = client.Prenom;
            tbAdresse.Text = client.Rue;
            tbVille.Text = client.Ville;
            ddlProvince.SelectedValue = client.Province;    
            tbCodePostal.Text = client.CodePostal != null ? client.CodePostal.Substring(0, 3) + " " + client.CodePostal.Substring(3, 3) : "";
            tbTelephone1.Text = client.Tel1 != null ? "(" + client.Tel1.Substring(0, 3) + ") " + client.Tel1.Substring(3, 3) + "-" + client.Tel1.Substring(6) : "";
            if (client.Tel2 != null)
                tbTelephone2.Text = "(" + client.Tel2.Substring(0, 3) + ") " + client.Tel2.Substring(3, 3) + "-" + client.Tel2.Substring(6);
            tbCourriel.Text = client.AdresseEmail;
        }
    }

    public void verifierPermissions(String typeUtilisateur)
    {
        String url = "";

        if (Session["TypeUtilisateur"] == null)
        {
            url = "~/Pages/AccueilInternaute.aspx?";
            Response.Redirect(url, true);
        }
        else if (Session["TypeUtilisateur"].ToString() != typeUtilisateur)
        {
            String type = Session["TypeUtilisateur"].ToString();
            if (type == "C")
            {
                url = "~/Pages/AccueilClient.aspx?";
            }
            else if (type == "V")
            {
                url = "~/Pages/ConnexionVendeur.aspx?";
            }
            else if (type == "G")
            {
                url = "~/Pages/AcceuilGestionnaire.aspx?";
            }
            else
            {
                url = "~/Pages/AccueilInternaute.aspx?";
            }

            Response.Redirect(url, true);
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
               tbCourriel.Text != "" && exprCourriel.IsMatch(tbCourriel.Text) ;
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
            errNom.CssClass = "text-danger d-none";
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
            errPrenom.CssClass = "text-danger d-none";
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
            errAdresse.CssClass = "text-danger d-none";
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
            errVille.CssClass = "text-danger d-none";
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
            errProvince.CssClass = "text-danger d-none";
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
            errCodePostal.CssClass = "text-danger d-none";
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
            errTelephone1.CssClass = "text-danger d-none";
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
            errTelephone2.CssClass = "text-danger d-none";
        }       
    }

    protected void btnInscription_Click(object sender, EventArgs e)
    {
        if (validerPage())
        {

            long noClient = Convert.ToInt64(Session["NoClient"]);
            PPClients client = dbContext.PPClients.Where(v => v.NoClient == noClient).Single();
            client.Nom = tbNom.Text.Trim();
            client.Prenom = tbPrenom.Text.Trim();
            client.Rue = tbAdresse.Text.Trim();
            client.Ville = tbVille.Text.Trim();
            client.Province = ddlProvince.SelectedValue;
            client.CodePostal = tbCodePostal.Text.ToUpper().Replace(" ", "").Trim();
            client.Pays = "Canada";
            client.Tel1 = tbTelephone1.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            if (!string.IsNullOrEmpty(tbTelephone2.Text))
                client.Tel2 = tbTelephone2.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            client.AdresseEmail = client.AdresseEmail;
            client.MotDePasse = client.MotDePasse;
            client.DateCreation = client.DateCreation;
            client.DateMAJ = DateTime.Now;
            client.NbConnexions = client.NbConnexions;
            client.Statut = client.Statut;

            bool binOK = true;

            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception)
            {
                binOK = false;
            }
        }
        else
            afficherErreurs();


    }
}