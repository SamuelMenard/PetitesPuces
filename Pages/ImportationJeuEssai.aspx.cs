using System;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class Pages_ImportationJeuEssai : System.Web.UI.Page
{
    private BD6B8_424SEntities dbContext = new BD6B8_424SEntities();

    protected void ajouterRangee(string strNomTable, int intNbEnregistements)
    {
        TableRow row = LibrairieControlesDynamique.trDYN(tabEtatTables);
        TableCell cell = LibrairieControlesDynamique.tdDYN(row, "", "");
        cell.Text = strNomTable;
        cell = LibrairieControlesDynamique.tdDYN(row, "", "");
        cell.Text = intNbEnregistements + (intNbEnregistements > 1 ? " enregistrements" : " enregistrement");
    }

    protected void remplirTableau(bool binImportation)
    {
        if (binImportation)
        {
            ajouterRangee("PPVendeurs", dbContext.PPVendeurs.Count());
            ajouterRangee("PPClients", dbContext.PPClients.Count());
            ajouterRangee("PPCategories", dbContext.PPCategories.Count());
            ajouterRangee("PPProduits", dbContext.PPProduits.Count());
            ajouterRangee("PPArticlesEnPanier", dbContext.PPArticlesEnPanier.Count());
            ajouterRangee("PPTypesPoids", dbContext.PPTypesPoids.Count());
            ajouterRangee("PPTypesLivraison", dbContext.PPTypesLivraison.Count());
            ajouterRangee("PPPoidsLivraisons", dbContext.PPPoidsLivraisons.Count());
            ajouterRangee("PPCommandes", dbContext.PPCommandes.Count());
            ajouterRangee("PPDetailsCommandes", dbContext.PPDetailsCommandes.Count());
            ajouterRangee("PPVendeursClients", dbContext.PPVendeursClients.Count());
            ajouterRangee("PPHistoriquePaiements", dbContext.PPHistoriquePaiements.Count());
            ajouterRangee("PPTaxeProvinciale", dbContext.PPTaxeProvinciale.Count());
            ajouterRangee("PPTaxeFederale", dbContext.PPTaxeFederale.Count());
            ajouterRangee("PPGestionnaires", dbContext.PPGestionnaires.Count());
        }
        else
        {
            ajouterRangee("PPGestionnaires", dbContext.PPGestionnaires.Count());
            ajouterRangee("PPTaxeFederale", dbContext.PPTaxeFederale.Count());
            ajouterRangee("PPTaxeProvinciale", dbContext.PPTaxeProvinciale.Count());
            ajouterRangee("PPHistoriquePaiements", dbContext.PPHistoriquePaiements.Count());
            ajouterRangee("PPVendeursClients", dbContext.PPVendeursClients.Count());
            ajouterRangee("PPDetailsCommandes", dbContext.PPDetailsCommandes.Count());
            ajouterRangee("PPCommandes", dbContext.PPCommandes.Count());
            ajouterRangee("PPPoidsLivraisons", dbContext.PPPoidsLivraisons.Count());
            ajouterRangee("PPTypesLivraison", dbContext.PPTypesLivraison.Count());
            ajouterRangee("PPTypesPoids", dbContext.PPTypesPoids.Count());
            ajouterRangee("PPArticlesEnPanier", dbContext.PPArticlesEnPanier.Count());
            ajouterRangee("PPProduits", dbContext.PPProduits.Count());
            ajouterRangee("PPCategories", dbContext.PPCategories.Count());
            ajouterRangee("PPClients", dbContext.PPClients.Count());
            ajouterRangee("PPVendeurs", dbContext.PPVendeurs.Count());
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           remplirTableau(false);

            if (!dbContext.PPArticlesEnPanier.Any() && !dbContext.PPCategories.Any() &&
                !dbContext.PPClients.Any() && !dbContext.PPCommandes.Any() &&
                !dbContext.PPDetailsCommandes.Any() && !dbContext.PPGestionnaires.Any() &&
                !dbContext.PPHistoriquePaiements.Any() && !dbContext.PPPoidsLivraisons.Any() &&
                !dbContext.PPProduits.Any() && !dbContext.PPTaxeFederale.Any() &&
                !dbContext.PPTaxeProvinciale.Any() && !dbContext.PPTypesLivraison.Any() &&
                !dbContext.PPTypesPoids.Any() && !dbContext.PPVendeurs.Any() &&
                !dbContext.PPVendeursClients.Any())
            {
                btnImporterDonnees.Visible = true;
            }
            else
            {
                btnViderBD.Visible = true;
            }
        }
    }

    protected void btnViderBD_Click(object sender, EventArgs e)
    {
        dbContext.PPGestionnaires.RemoveRange(dbContext.PPGestionnaires);
        dbContext.PPTaxeFederale.RemoveRange(dbContext.PPTaxeFederale);
        dbContext.PPTaxeProvinciale.RemoveRange(dbContext.PPTaxeProvinciale);         
        dbContext.PPHistoriquePaiements.RemoveRange(dbContext.PPHistoriquePaiements);
        dbContext.PPVendeursClients.RemoveRange(dbContext.PPVendeursClients);
        dbContext.PPDetailsCommandes.RemoveRange(dbContext.PPDetailsCommandes);
        dbContext.PPCommandes.RemoveRange(dbContext.PPCommandes);
        dbContext.PPPoidsLivraisons.RemoveRange(dbContext.PPPoidsLivraisons);
        dbContext.PPTypesLivraison.RemoveRange(dbContext.PPTypesLivraison);
        dbContext.PPTypesPoids.RemoveRange(dbContext.PPTypesPoids);
        dbContext.PPArticlesEnPanier.RemoveRange(dbContext.PPArticlesEnPanier);
        dbContext.PPProduits.RemoveRange(dbContext.PPProduits);
        dbContext.PPCategories.RemoveRange(dbContext.PPCategories);
        dbContext.PPClients.RemoveRange(dbContext.PPClients);
        dbContext.PPVendeurs.RemoveRange(dbContext.PPVendeurs);

        dbContext.SaveChanges();

        lblResultatImportation.Visible = false;

        remplirTableau(false);

        if (!dbContext.PPArticlesEnPanier.Any() && !dbContext.PPCategories.Any() &&
        !dbContext.PPClients.Any() && !dbContext.PPCommandes.Any() &&
        !dbContext.PPDetailsCommandes.Any() && !dbContext.PPGestionnaires.Any() &&
        !dbContext.PPHistoriquePaiements.Any() && !dbContext.PPPoidsLivraisons.Any() &&
        !dbContext.PPProduits.Any() && !dbContext.PPTaxeFederale.Any() &&
        !dbContext.PPTaxeProvinciale.Any() && !dbContext.PPTypesLivraison.Any() &&
        !dbContext.PPTypesPoids.Any() && !dbContext.PPVendeurs.Any() &&
        !dbContext.PPVendeursClients.Any())
        {
            btnViderBD.Visible = false;
            btnImporterDonnees.Visible = true;
        }
        else
        {
            btnImporterDonnees.Visible = false;
            btnViderBD.Visible = true;
        }
    }

    protected void btnImporterDonnees_Click(object sender, EventArgs e)
    {
        using (var transaction = dbContext.Database.BeginTransaction())
        {
            try
            {
                XDocument document = XDocument.Load(Server.MapPath("\\static\\xml\\PPVendeurs.xml"));
                XElement racine = document.Element("dataroot");
                foreach (var element in racine.Elements("PPVendeurs"))
                {
                    PPVendeurs vendeur = new PPVendeurs();
                    vendeur.NoVendeur = long.Parse(element.Descendants("NoVendeur").Single().Value);
                    vendeur.NomAffaires = element.Descendants("NomAffaires").Single().Value;
                    vendeur.Nom = element.Descendants("Nom").Single().Value;
                    vendeur.Prenom = element.Descendants("Prenom").Single().Value;
                    vendeur.Rue = element.Descendants("Rue").Single().Value;
                    vendeur.Ville = element.Descendants("Ville").Single().Value;
                    vendeur.Province = element.Descendants("Province").Single().Value;
                    vendeur.CodePostal = element.Descendants("CodePostal").Single().Value;
                    vendeur.Pays = element.Descendants("Pays").Single().Value;
                    vendeur.Tel1 = element.Descendants("Tel1").Single().Value;
                    vendeur.Tel2 = element.Descendants("Tel2").Single().Value == "NULL" ? null : element.Descendants("Tel2").Single().Value;
                    vendeur.AdresseEmail = element.Descendants("AdresseEmail").Single().Value;
                    vendeur.MotDePasse = element.Descendants("MotDePasse").Single().Value;
                    vendeur.PoidsMaxLivraison = int.Parse(element.Descendants("PoidsMaxLivraison").Single().Value);
                    vendeur.LivraisonGratuite = decimal.Parse(element.Descendants("LivraisonGratuite").Single().Value.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
                    vendeur.Taxes = element.Descendants("Taxes").Single().Value == "1" ? true : false;
                    if (element.Descendants("Pourcentage").Single().Value == "NULL")
                        vendeur.Pourcentage = null;
                    else
                        vendeur.Pourcentage = decimal.Parse(element.Descendants("Pourcentage").Single().Value.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
                    vendeur.Configuration = element.Descendants("Configuration").Single().Value;
                    if (element.Descendants("DateCreation").Single().Value == "NULL")
                        vendeur.DateCreation = null;
                    else
                        vendeur.DateCreation = DateTime.Parse(element.Descendants("DateCreation").Single().Value);
                    if (element.Descendants("DateMAJ").Single().Value == "NULL")
                        vendeur.DateMAJ = null;
                    else
                        vendeur.DateMAJ = DateTime.Parse(element.Descendants("DateMAJ").Single().Value);

                    if (element.Descendants("Statut").Single().Value == "NULL")
                        vendeur.Statut = null;
                    else
                        vendeur.Statut = short.Parse(element.Descendants("Statut").Single().Value);

                    dbContext.PPVendeurs.Add(vendeur);

                    XDocument configuration = new XDocument();
                    XElement urlImage = new XElement("urlImage");
                    urlImage.Value = "image_magasin.jpg";
                    XElement couleurFond = new XElement("couleurFond");
                    couleurFond.Value = "#ffffff";
                    XElement couleurTexte = new XElement("couleurTexte");
                    couleurTexte.Value = "#000000";
                    configuration.Add(new XElement("configuration", urlImage, couleurFond, couleurTexte));
                    configuration.Save(Server.MapPath("\\static\\xml\\" + vendeur.Configuration));
                }

                document = XDocument.Load(Server.MapPath("\\static\\xml\\PPClients.xml"));
                racine = document.Element("dataroot");
                foreach (var element in racine.Elements("PPClients"))
                {
                    PPClients client = new PPClients();
                    client.NoClient = long.Parse(element.Descendants("NoClient").Single().Value);
                    client.AdresseEmail = element.Descendants("AdresseEmail").Single().Value;
                    client.MotDePasse = element.Descendants("MotDePasse").Single().Value;
                    client.Nom = element.Descendants("Nom").Single().Value == "NULL" ? null : element.Descendants("Nom").Single().Value;
                    client.Prenom = element.Descendants("Prenom").Single().Value == "NULL" ? null : element.Descendants("Prenom").Single().Value;
                    client.Rue = element.Descendants("Rue").Single().Value == "NULL" ? null : element.Descendants("Rue").Single().Value;
                    client.Ville = element.Descendants("Ville").Single().Value == "NULL" ? null : element.Descendants("Ville").Single().Value;
                    client.Province = element.Descendants("Province").Single().Value == "NULL" ? null : element.Descendants("Province").Single().Value;
                    client.CodePostal = element.Descendants("CodePostal").Single().Value == "NULL" ? null : element.Descendants("CodePostal").Single().Value;
                    client.Pays = element.Descendants("Pays").Single().Value == "NULL" ? null : element.Descendants("Pays").Single().Value;
                    client.Tel1 = element.Descendants("Tel1").Single().Value == "NULL" ? null : element.Descendants("Tel1").Single().Value;
                    client.Tel2 = element.Descendants("Tel2").Single().Value == "NULL" ? null : element.Descendants("Tel2").Single().Value;
                    client.DateCreation = DateTime.Parse(element.Descendants("DateCreation").Single().Value);
                    if (element.Descendants("DateMAJ").Single().Value == "NULL")
                        client.DateMAJ = null;
                    else
                        client.DateMAJ = DateTime.Parse(element.Descendants("DateMAJ").Single().Value);
                    client.NbConnexions = short.Parse(element.Descendants("NbConnexions").Single().Value);
                    if (element.Descendants("DateDerniereConnexion").Single().Value == "NULL")
                        client.DateDerniereConnexion = null;
                    else
                        client.DateDerniereConnexion = DateTime.Parse(element.Descendants("DateDerniereConnexion").Single().Value);
                    client.Statut = short.Parse(element.Descendants("Statut").Single().Value);

                    dbContext.PPClients.Add(client);
                }

                document = XDocument.Load(Server.MapPath("\\static\\xml\\PPCategories.xml"));
                racine = document.Element("dataroot");
                foreach (var element in racine.Elements("PPCategories"))
                {
                    PPCategories categorie = new PPCategories();
                    categorie.NoCategorie = int.Parse(element.Descendants("NoCategorie").Single().Value);
                    categorie.Description = element.Descendants("Description").Single().Value;
                    categorie.Details = element.Descendants("Details").Single().Value;

                    dbContext.PPCategories.Add(categorie);
                }

                document = XDocument.Load(Server.MapPath("\\static\\xml\\PPProduits.xml"));
                racine = document.Element("dataroot");
                foreach (var element in racine.Elements("PPProduits"))
                {
                    PPProduits produit = new PPProduits();
                    produit.NoProduit = long.Parse(element.Descendants("NoProduit").Single().Value);
                    produit.NoVendeur = long.Parse(element.Descendants("NoVendeur").Single().Value);
                    produit.NoCategorie = int.Parse(element.Descendants("NoCategorie").Single().Value);
                    produit.Nom = element.Descendants("Nom").Single().Value;
                    produit.Description = element.Descendants("Description").Single().Value;
                    produit.Photo = element.Descendants("Photo").Single().Value;
                    produit.PrixDemande = decimal.Parse(element.Descendants("PrixDemande").Single().Value.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
                    produit.NombreItems = short.Parse(element.Descendants("NombreItems").Single().Value);
                    produit.Disponibilité = element.Descendants("Disponibilit_").Single().Value == "1" ? true : false;
                    produit.DateVente = DateTime.Parse(element.Descendants("DateVente").Single().Value);
                    produit.PrixVente = decimal.Parse(element.Descendants("PrixVente").Single().Value.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
                    produit.Poids = decimal.Parse(element.Descendants("Poids").Single().Value.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
                    produit.DateCreation = DateTime.Parse(element.Descendants("DateCreation").Single().Value);
                    if (element.Descendants("DateMAJ").Single().Value == "NULL")
                        produit.DateMAJ = null;
                    else
                        produit.DateMAJ = DateTime.Parse(element.Descendants("DateMAJ").Single().Value);

                    dbContext.PPProduits.Add(produit);
                }

                document = XDocument.Load(Server.MapPath("\\static\\xml\\PPArticlesEnPanier.xml"));
                racine = document.Element("dataroot");
                foreach (var element in racine.Elements("PPArticlesEnPanier"))
                {
                    PPArticlesEnPanier articleEnPanier = new PPArticlesEnPanier();
                    articleEnPanier.NoPanier = long.Parse(element.Descendants("NoPanier").Single().Value);
                    articleEnPanier.NoClient = long.Parse(element.Descendants("NoClient").Single().Value);
                    articleEnPanier.NoVendeur = long.Parse(element.Descendants("NoVendeur").Single().Value);
                    articleEnPanier.NoProduit = long.Parse(element.Descendants("NoProduit").Single().Value);
                    articleEnPanier.DateCreation = DateTime.Parse(element.Descendants("DateCreation").Single().Value);
                    articleEnPanier.NbItems = short.Parse(element.Descendants("NbItems").Single().Value);

                    dbContext.PPArticlesEnPanier.Add(articleEnPanier);
                }

                document = XDocument.Load(Server.MapPath("\\static\\xml\\PPTypesPoids.xml"));
                racine = document.Element("dataroot");
                foreach (var element in racine.Elements("PPTypesPoids"))
                {
                    PPTypesPoids typePoids = new PPTypesPoids();
                    typePoids.CodePoids = short.Parse(element.Descendants("CodePoids").Single().Value);
                    typePoids.PoidsMin = decimal.Parse(element.Descendants("PoidsMin").Single().Value.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
                    typePoids.PoidsMax = decimal.Parse(element.Descendants("PoidsMax").Single().Value.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));

                    dbContext.PPTypesPoids.Add(typePoids);
                }

                document = XDocument.Load(Server.MapPath("\\static\\xml\\PPTypesLivraison.xml"));
                racine = document.Element("dataroot");
                foreach (var element in racine.Elements("PPTypesLivraison"))
                {
                    PPTypesLivraison typeLivraison = new PPTypesLivraison();
                    typeLivraison.CodeLivraison = short.Parse(element.Descendants("CodeLivraison").Single().Value);
                    typeLivraison.Description = element.Descendants("Description").Single().Value;

                    dbContext.PPTypesLivraison.Add(typeLivraison);
                }

                document = XDocument.Load(Server.MapPath("\\static\\xml\\PPPoidsLivraisons.xml"));
                racine = document.Element("dataroot");
                foreach (var element in racine.Elements("PPPoidsLivraisons"))
                {
                    PPPoidsLivraisons poidsLivraison = new PPPoidsLivraisons();
                    poidsLivraison.CodeLivraison = short.Parse(element.Descendants("CodeLivraison").Single().Value);
                    poidsLivraison.CodePoids = short.Parse(element.Descendants("CodePoids").Single().Value);
                    poidsLivraison.Tarif = decimal.Parse(element.Descendants("Tarif").Single().Value.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));

                    dbContext.PPPoidsLivraisons.Add(poidsLivraison);
                }

                document = XDocument.Load(Server.MapPath("\\static\\xml\\PPCommandes.xml"));
                racine = document.Element("dataroot");
                foreach (var element in racine.Elements("PPCommandes"))
                {
                    PPCommandes commande = new PPCommandes();
                    commande.NoCommande = long.Parse(element.Descendants("NoCommande").Single().Value);
                    commande.NoClient = long.Parse(element.Descendants("NoClient").Single().Value);
                    commande.NoVendeur = long.Parse(element.Descendants("NoVendeur").Single().Value);
                    commande.DateCommande = DateTime.Parse(element.Descendants("DateCommande").Single().Value);
                    commande.CoutLivraison = decimal.Parse(element.Descendants("CoutLivraison").Single().Value.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
                    commande.TypeLivraison = short.Parse(element.Descendants("TypeLivraison").Single().Value);
                    commande.MontantTotAvantTaxes = decimal.Parse(element.Descendants("MontantTotAvantTaxes").Single().Value.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
                    commande.TPS = decimal.Parse(element.Descendants("TPS").Single().Value.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
                    commande.TVQ = decimal.Parse(element.Descendants("TVQ").Single().Value.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
                    commande.PoidsTotal = decimal.Parse(element.Descendants("PoidsTotal").Single().Value.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
                    commande.Statut = element.Descendants("Statut").Single().Value;
                    commande.NoAutorisation = element.Descendants("NoAutorisation").Single().Value;

                    dbContext.PPCommandes.Add(commande);
                }

                document = XDocument.Load(Server.MapPath("\\static\\xml\\PPDetailsCommandes.xml"));
                racine = document.Element("dataroot");
                foreach (var element in racine.Elements("PPDetailsCommandes"))
                {
                    PPDetailsCommandes detailCommande = new PPDetailsCommandes();
                    detailCommande.NoDetailCommandes = long.Parse(element.Descendants("NoDetailCommandes").Single().Value);
                    detailCommande.NoCommande = long.Parse(element.Descendants("NoCommande").Single().Value);
                    detailCommande.NoProduit = long.Parse(element.Descendants("NoProduit").Single().Value);
                    detailCommande.PrixVente = decimal.Parse(element.Descendants("PrixVente").Single().Value.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
                    detailCommande.Quantité = short.Parse(element.Descendants("Quantit_").Single().Value);

                    dbContext.PPDetailsCommandes.Add(detailCommande);
                }

                document = XDocument.Load(Server.MapPath("\\static\\xml\\PPVendeursClients.xml"));
                racine = document.Element("dataroot");
                foreach (var element in racine.Elements("PPVendeursClients"))
                {
                    PPVendeursClients vendeurClient = new PPVendeursClients();
                    vendeurClient.NoVendeur = long.Parse(element.Descendants("NoVendeur").Single().Value);
                    vendeurClient.NoClient = long.Parse(element.Descendants("NoClient").Single().Value);
                    vendeurClient.DateVisite = DateTime.Parse(element.Descendants("DateVisite").Single().Value);

                    dbContext.PPVendeursClients.Add(vendeurClient);
                }

                document = XDocument.Load(Server.MapPath("\\static\\xml\\PPHistoriquePaiements.xml"));
                racine = document.Element("dataroot");
                foreach (var element in racine.Elements("PPHistoriquePaiements"))
                {
                    PPHistoriquePaiements historiquePaiement = new PPHistoriquePaiements();
                    historiquePaiement.NoHistorique = long.Parse(element.Descendants("NoHistorique").Single().Value);
                    historiquePaiement.MontantVenteAvantLivraison = decimal.Parse(element.Descendants("MontantVenteAvantLivraison").Single().Value.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
                    historiquePaiement.NoVendeur = long.Parse(element.Descendants("NoVendeur").Single().Value);
                    historiquePaiement.NoClient = long.Parse(element.Descendants("NoClient").Single().Value);
                    historiquePaiement.NoCommande = long.Parse(element.Descendants("NoCommande").Single().Value);
                    historiquePaiement.DateVente = DateTime.Parse(element.Descendants("DateVente").Single().Value);
                    historiquePaiement.NoAutorisation = element.Descendants("NoCommande").Single().Value;
                    historiquePaiement.FraisLesi = decimal.Parse(element.Descendants("FraisLesi").Single().Value.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
                    historiquePaiement.Redevance = decimal.Parse(element.Descendants("Redevance").Single().Value.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
                    historiquePaiement.FraisLivraison = decimal.Parse(element.Descendants("FraisLivraison").Single().Value.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
                    historiquePaiement.FraisTPS = decimal.Parse(element.Descendants("FraisTPS").Single().Value.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
                    historiquePaiement.FraisTVQ = decimal.Parse(element.Descendants("FraisTVQ").Single().Value.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));

                    dbContext.PPHistoriquePaiements.Add(historiquePaiement);
                }

                document = XDocument.Load(Server.MapPath("\\static\\xml\\PPTaxeProvinciale.xml"));
                racine = document.Element("dataroot");
                foreach (var element in racine.Elements("PPTaxeProvinciale"))
                {
                    PPTaxeProvinciale taxeProvinciale = new PPTaxeProvinciale();
                    taxeProvinciale.NoTVQ = byte.Parse(element.Descendants("NoTVQ").Single().Value);
                    taxeProvinciale.DateEffectiveTVQ = DateTime.Parse(element.Descendants("DateEffectiveTVQ").Single().Value);
                    taxeProvinciale.TauxTVQ = decimal.Parse(element.Descendants("TauxTVQ").Single().Value.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));

                    dbContext.PPTaxeProvinciale.Add(taxeProvinciale);
                }

                document = XDocument.Load(Server.MapPath("\\static\\xml\\PPTaxeFederale.xml"));
                racine = document.Element("dataroot");
                foreach (var element in racine.Elements("PPTaxeFederale"))
                {
                    PPTaxeFederale taxeFederale = new PPTaxeFederale();
                    taxeFederale.NoTPS = byte.Parse(element.Descendants("NoTPS").Single().Value);
                    taxeFederale.DateEffectiveTPS = DateTime.Parse(element.Descendants("DateEffectiveTPS").Single().Value);
                    taxeFederale.TauxTPS = decimal.Parse(element.Descendants("TauxTPS").Single().Value.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));

                    dbContext.PPTaxeFederale.Add(taxeFederale);
                }

                document = XDocument.Load(Server.MapPath("\\static\\xml\\PPGestionnaires.xml"));
                racine = document.Element("dataroot");
                foreach (var element in racine.Elements("PPGestionnaires"))
                {
                    PPGestionnaires gestionnaire = new PPGestionnaires();
                    gestionnaire.ID = long.Parse(element.Descendants("ID").Single().Value);
                    gestionnaire.courriel = element.Descendants("courriel").Single().Value;
                    gestionnaire.motDePasse = element.Descendants("motDePasse").Single().Value;

                    dbContext.PPGestionnaires.Add(gestionnaire);
                }

                dbContext.SaveChanges();
                transaction.Commit();

                lblResultatImportation.Text = "Les données du jeu d'essai ont été importées.";
            }
            catch (DbUpdateException ex)
            {
                transaction.Rollback();

                if (ex.InnerException is UpdateException && ex.InnerException.InnerException is SqlException)
                    lblResultatImportation.Text = "Une erreur s'est produite lors de l'importation du jeu d'essai. Les données n'ont pas été importées. Voici l'erreur : " + ex.InnerException.InnerException.Message;
                else
                    lblResultatImportation.Text = "Une erreur s'est produite lors de l'importation du jeu d'essai. Les données n'ont pas été importées. Voici l'erreur : " + ex.Message;
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                lblResultatImportation.Text = "Une erreur s'est produite lors de l'importation du jeu d'essai. Les données n'ont pas été importées. Voici l'erreur : " + ex.Message;
            }
        }

        lblResultatImportation.Visible = true;

        remplirTableau(true);

        if (!dbContext.PPArticlesEnPanier.Any() && !dbContext.PPCategories.Any() &&
            !dbContext.PPClients.Any() && !dbContext.PPCommandes.Any() &&
            !dbContext.PPDetailsCommandes.Any() && !dbContext.PPGestionnaires.Any() &&
            !dbContext.PPHistoriquePaiements.Any() && !dbContext.PPPoidsLivraisons.Any() &&
            !dbContext.PPProduits.Any() && !dbContext.PPTaxeFederale.Any() &&
            !dbContext.PPTaxeProvinciale.Any() && !dbContext.PPTypesLivraison.Any() &&
            !dbContext.PPTypesPoids.Any() && !dbContext.PPVendeurs.Any() &&
            !dbContext.PPVendeursClients.Any())
        {
            btnViderBD.Visible = false;
            btnImporterDonnees.Visible = true;
        }
        else
        {
            btnImporterDonnees.Visible = false;
            btnViderBD.Visible = true;
        }
    }
}