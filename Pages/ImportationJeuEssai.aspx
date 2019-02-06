<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImportationJeuEssai.aspx.cs" Inherits="Pages_ImportationJeuEssai" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
   <form runat="server">
       <asp:Table ID="tabEtatTables" runat="server" GridLines="Both">
           <asp:TableHeaderRow runat="server">
               <asp:TableHeaderCell runat="server" Text="Tables"></asp:TableHeaderCell>
               <asp:TableHeaderCell runat="server" Text="État"></asp:TableHeaderCell>
           </asp:TableHeaderRow>
       </asp:Table>
       <br />
       <asp:Button ID="btnViderBD" runat="server" Text="Vider la base de données" Visible="false" OnClick="btnViderBD_Click" />
       <asp:Button ID="btnImporterDonnees" runat="server" Text="Importer les données du jeu d'essai dans la base de données" Visible="false" OnClick="btnImporterDonnees_Click" />
       <br />
       <br />
       <asp:Label ID="lblResultatImportation" runat="server" Visible="false" ForeColor="Red" />
   </form>
</body>
</html>
