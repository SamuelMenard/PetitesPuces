using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_AcceuilGestionnaire : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void nouvellesDemandes_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Nouvelle demande");
        String url = "~/Pages/DemandesVendeur.aspx?";
        Response.Redirect(url, true);
    }

    public void inactiviteClients_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/InactiviteClients.aspx?";
        Response.Redirect(url, true);
    }

    public void inactiviteVendeurs_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/InactiviteVendeurs.aspx?";
        Response.Redirect(url, true);
    }

    public void rendreInactif_click(Object sender, EventArgs e)
    {
        String url = "~/Pages/RendreInactif.aspx?";
        Response.Redirect(url, true);
    }

    public void stats_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Stats");
    }

    public void redevances_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Redevances");
    }
}