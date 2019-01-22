using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Description résumée de librairieControlesDynamique
/// </summary>
public static class LibrairieControlesDynamique
{
    static public Panel divDYN(Control Conteneur, String strID)
    {
        Panel panel = new Panel();
        panel.ID = strID;
        Conteneur.Controls.Add(panel);
        return panel;
    }

    static public Panel divDYN(Control Conteneur, String strID, String strStyle)
    {
        Panel panel = divDYN(Conteneur, strID);
        panel.CssClass = strStyle;
        return panel;
    }
    static public ImageButton btnImgDYN(Control Conteneur, String strID, String strImg, String strClass)
    {
        ImageButton img = new ImageButton
        {
            ID = strID,
            CssClass = strClass,
            ImageUrl = strImg
        };
        Conteneur.Controls.Add(img);
        return img;
    }

    static public Button btnDYN(Control conteneur, String strID, String strClass, String strValue)
    {
        Button btn = new Button
        {
            ID = strID,
            CssClass = strClass,
            Text = strValue
        };
        conteneur.Controls.Add(btn);
        return btn;
    }




    static public Image imgDYN(Control Conteneur, String strID, String strImg, String strClass)
    {
        Image img = new Image
        {
            ID = strID,
            CssClass = strClass,
            ImageUrl = strImg,
        };
        Conteneur.Controls.Add(img);
        return img;
    }
    static public Label lblDYN(Control Conteneur, String strID, String strValeur)
    {
        Label lbl = new Label()
        {
            ID = strID,
            Text = strValeur
        };
        Conteneur.Controls.Add(lbl);
        return lbl;
    }

    static public Label lblDYN(Control Conteneur, String strID, String strValeur, String strClass)
    {
        Label lbl = new Label()
        {
            ID = strID,
            Text = strValeur,
            CssClass = strClass
        };
        Conteneur.Controls.Add(lbl);
        return lbl;
    }

    static public TextBox tbDYN(Control Conteneur, String strID, String strClass)
    {
        TextBox tb = new TextBox()
        {
            ID = strID,
            CssClass = strClass
        };
        Conteneur.Controls.Add(tb);
        return tb;
    }
    static public TextBox tbDYN(Control Conteneur, String strID, String strValeur, String strClass)
    {
        TextBox tb = new TextBox()
        {
            ID = strID,
            CssClass = strClass,
            Text = strValeur
        };
        Conteneur.Controls.Add(tb);
        return tb;
    }
    static public void brDYN(Control Conteneur)
    {
        Literal br = new Literal();
        br.Text = "<br />";
        Conteneur.Controls.Add(br);
    }
    static public void hrDYN(Control Conteneur)
    {
        Literal hr = new Literal();
        hr.Text = "<hr>";
        Conteneur.Controls.Add(hr);
    }
    static public void brDYN(Control Conteneur, Int16 intNb)
    {

        Literal br = new Literal();
        br.Text = "";
        for (Int16 i = 1; i <= intNb; i++)
        {
            br.Text += "<br />";
        }
        Conteneur.Controls.Add(br);
    }
    static public Table tableDYN(Control Conteneur, String strID, String strStyle)
    {
        Table t = new Table()
        {
            ID = strID,
            CssClass = strStyle
        };
        Conteneur.Controls.Add(t);
        return t;
    }
    static public TableCell tdDYN(TableRow Conteneur, String strID, String strStyle)
    {
        TableCell t = new TableCell()
        {
            ID = strID,
            CssClass = strStyle
        };
        Conteneur.Controls.Add(t);
        return t;
    }
    static public TableRow trDYN(Table Conteneur)
    {
        TableRow t = new TableRow();
        Conteneur.Controls.Add(t);
        return t;
    }

}