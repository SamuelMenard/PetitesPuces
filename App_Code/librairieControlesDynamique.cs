using System;
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
    static public DropDownList ddlDYN(Control Conteneur, String strID, String strStyle)
    {
        DropDownList dropDownList = new DropDownList();
        dropDownList.ID = strID;
        dropDownList.CssClass = strStyle;            
        Conteneur.Controls.Add(dropDownList);
        return dropDownList;
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

    static public LinkButton lbDYN(Control Conteneur, String strID, String strValeur, String style, EventHandler onClick)
    {
        LinkButton lb = new LinkButton()
        {
            ID = strID,
            Text = strValeur
        };
        lb.CssClass = style;
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

    static public TextBox numericUpDownDYN(Control Conteneur, String strID, String strValeur, String max, String strClass)
    {
        TextBox tb = new TextBox()
        {
            ID = strID,
            CssClass = strClass,
            Text = strValeur,
            MaxLength = 2
        };
        tb.Attributes["type"] = "number";
        tb.Attributes["min"] = "1";
        tb.Attributes["max"] = max;
        tb.Attributes["step"] = "1";
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
    static public void h4DYN(Control Conteneur, String strValeur)
    {
        Literal h4 = new Literal();
        h4.Text = "<h4>" + strValeur + "</h4>";
        Conteneur.Controls.Add(h4);
    }
    static public void h4DYN(Control Conteneur, String id, String strValeur)
    {
        Literal h4 = new Literal();
        h4.Text = "<h4 id=\"" + id + "\" >" + strValeur + "</h4>";
        Conteneur.Controls.Add(h4);
    }
    static public void pDYN(Control Conteneur, String strValeur)
    {
        Literal p = new Literal();
        p.Text = "<p>" + strValeur + "</p>";
        Conteneur.Controls.Add(p);
    }
    static public void spaceDYN(Control Conteneur)
    {
        Literal space = new Literal();
        space.Text = "&nbsp;";
        Conteneur.Controls.Add(space);
    }

    static public void hrDYN(Control Conteneur, String strStyle, int size)
    {
        Literal hr = new Literal();        
        hr.Text = "<hr size="+size+" class="+strStyle+">";        
        Conteneur.Controls.Add(hr);
    }
    static public void aDYN(Control Conteneur, String strValeur, String URL)
    {
        Literal a = new Literal();
        a.Text = "<a href=\"" + URL + "\">" + strValeur + "</a>";
        Conteneur.Controls.Add(a);
    }

    static public void aDYN(Control Conteneur, String strValeur, String URL,Boolean booToggle)
    {
        Literal a = new Literal();
        a.Text = "<a data-toggle = \"collapse\" href=\"" + URL + "\">" + strValeur + "</a>";       
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

    static public void sectionDYN(Control Conteneur, String id)
    {

        Literal section = new Literal();
        section.Text = "";
        section.Text += "<section id=\"" + id + "\"></section>" ;
        Conteneur.Controls.Add(section);
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

    static public Literal litDYN(Control conteneur, String strID, String strValeur)
    {
        Literal ltrDyn = new Literal();
        ltrDyn.Text = strValeur;
        conteneur.Controls.Add(ltrDyn);
        return ltrDyn;
    }

    static public CheckBox cb(Control conteneur, String strID, String strValeur)
    {
        CheckBox cb = new CheckBox();
        cb.ID = strID;
        cb.Text = strValeur;
        conteneur.Controls.Add(cb);
        return cb;
    }

    static public CheckBox cb(Control conteneur, String strID, String strValeur, String strCss)
    {
        CheckBox cb = new CheckBox();
        cb.ID = strID;
        cb.Text = strValeur;
        cb.CssClass = strCss;
        conteneur.Controls.Add(cb);
        return cb;
    }

    static public HtmlGenericControl liDYN(Control control, String href, String text, String style)
    {
        HtmlGenericControl li = new HtmlGenericControl("li");
        control.Controls.Add(li);

        HtmlGenericControl anchor = new HtmlGenericControl("a");
        anchor.Attributes.Add("class", style);
        anchor.Attributes.Add("href", href);
        anchor.InnerText = text;

        li.Controls.Add(anchor);
        return li;
    }

}