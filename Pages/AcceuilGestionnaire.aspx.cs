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

    public void inactivite_click(Object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Inactivite");
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