﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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

    static public Button btnDYN(Control conteneur, String strID, String strClass, String strValue, EventHandler onClick)
    {
        Button btn = new Button
        {
            ID = strID,
            CssClass = strClass,
            Text = strValue
        };
        btn.Click += onClick;
        conteneur.Controls.Add(btn);
        return btn;
    }

    static public HtmlButton htmlbtnDYN(Control conteneur, String strID, String strClass, String strValue, String strGlyphClass, EventHandler onClick)
    {
        HtmlButton hb = new HtmlButton();
        hb.ID = strID;
        hb.ServerClick += onClick;
        hb.Attributes.Add("class", strClass);
        hb.InnerHtml = strValue + "  " + "<span class=\"" + strGlyphClass + "\"></span>";

        conteneur.Controls.Add(hb);
        return hb;
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

    static public LinkButton lbDYN(Control Conteneur, String strID, String strValeur, EventHandler onClick)
    {
        LinkButton lb = new LinkButton()
        {
            ID = strID,
            Text = strValeur
        };
        lb.Click += onClick;
        Conteneur.Controls.Add(lb);
        return lb;
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
    static public void aDYN(Control Conteneur, String strValeur, String URL)
    {
        Literal a = new Literal();
        a.Text = "<a href=\"" + URL + "\">" + strValeur + "</a>";
        Conteneur.Controls.Add(a);
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