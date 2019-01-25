using System;
using System.Linq;

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
            tbCourriel.Text = "";
            tbConfirmationCourriel.Text = "";
            lblMessage.Text = "Il y a déjà un profil associé à ce courriel";
            divMessage.CssClass = "alert alert-danger alert-margins";
            divMessage.Visible = true;
        }
        else
        {
            PPClients client = new PPClients();
            client.NoClient = dbContext.PPClients.Max(c => c.NoClient) + 1;
            client.AdresseEmail = tbCourriel.Text;
            client.MotDePasse = tbMotPasse.Text;
            client.DateCreation = DateTime.Now;

            dbContext.PPClients.Add(client);
            dbContext.SaveChanges();

            tbCourriel.Text = "";
            tbConfirmationCourriel.Text = "";
            lblMessage.Text = "Le profil à été créé";
            divMessage.CssClass = "alert alert-success alert-margins";
            divMessage.Visible = true;
        }
    }
}