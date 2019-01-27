using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Description résumée de LibrairieLINQ
/// </summary>
public static class LibrairieLINQ
{
    private static BD6B8_424SEntities dataContext = new BD6B8_424SEntities();

    public static object JSON { get; private set; }

    // Pour la connexion
    public static bool connexionOK(String courriel, String mdp, String typeConnexion)
    {
        /*
         * Type connexion
         * C = client
         * V = vendeur
         * G = gestionnaire
         * */
        var clients = dataContext.PPClients;
        var vendeurs = dataContext.PPVendeurs;
        bool valide = false;
        switch (typeConnexion)
        {
            case "C": valide = clients.Where(client => client.AdresseEmail == courriel && client.MotDePasse == mdp).Any(); break;
            case "V": valide = vendeurs.Where(vendeur => vendeur.AdresseEmail == courriel && vendeur.MotDePasse == mdp
                            && vendeur.NoVendeur >= 10 && vendeur.NoVendeur <= 99).Any();
                break;
            case "G":
                valide = vendeurs.Where(gestionnaire => gestionnaire.AdresseEmail == courriel 
                            && gestionnaire.MotDePasse == mdp && gestionnaire.NoVendeur >= 100 && gestionnaire.NoVendeur <= 200).Any();
                break;
        }
        return valide;
    }

    // Avoir les infos de bases afin de les storer en variable de session
    public static List<String> infosBaseVendeur(String courriel)
    {
        List<String> lstInfos = new List<string>();
        var vendeurs = dataContext.PPVendeurs;
        var infos = from vendeur in vendeurs
                    where vendeur.AdresseEmail == courriel
                    select vendeur;
        foreach(var info in infos)
        {
            string[] tab = { info.NoVendeur.ToString(), info.NomAffaires, info.Nom, info.Prenom, info.AdresseEmail };
            lstInfos.AddRange(tab);
        }
        return lstInfos;
    }

    // Avoir les infos de bases afin de les storer en variable de session
    public static List<String> infosBaseClient(String courriel)
    {
        List<String> lstInfos = new List<string>();
        var clients = dataContext.PPClients;
        var infos = from client in clients
                    where client.AdresseEmail == courriel
                    select client;
        foreach (var info in infos)
        {
            string[] tab = { info.NoClient.ToString(), info.Nom, info.Prenom, info.AdresseEmail };
            lstInfos.AddRange(tab);
        }
        return lstInfos;
    }

    // aller chercher les paniers
    public static Dictionary<Nullable<long>, List<PPArticlesEnPanier>> getPaniersClient(long noClient)
    {
        var articlesPanier = dataContext.PPArticlesEnPanier;

        // retourner la liste des paniers
        Dictionary<Nullable<long>, List<PPArticlesEnPanier>> lstPaniers = new Dictionary<Nullable<long>, List<PPArticlesEnPanier>>();

        var articlesPanierClient = from articlePanier in articlesPanier
                                   where articlePanier.NoClient == noClient
                                   select articlePanier;

        foreach(var articlePanier in articlesPanierClient)
        {
            List<PPArticlesEnPanier> lstTempo;
            if (lstPaniers.TryGetValue(articlePanier.NoVendeur, out lstTempo))
            {
                lstTempo.Add(articlePanier);
            }
            else
            {
                List<PPArticlesEnPanier> lst = new List<PPArticlesEnPanier>();
                    lst.Add(articlePanier);
                lstPaniers.Add(articlePanier.NoVendeur, lst);
            }
        }


        return lstPaniers;
    }

    // aller chercher les compagnies de chaques catégories
    public static Dictionary<Nullable<long>, List<PPVendeurs>> getEntreprisesTriesParCategories()
    {
        var tableProduits = dataContext.PPProduits;
        var tableVendeurs = dataContext.PPVendeurs;
        Dictionary<Nullable<long>, List<PPVendeurs>> lstCategories = new Dictionary<long?, List<PPVendeurs>>();

        var produits = from produit in tableProduits
                       group produit by new { produit.NoCategorie, produit.NoVendeur }
                       into listeProduits select listeProduits;


        foreach(var keys in produits)
        {
            List<PPVendeurs> lstTempo;
            if (lstCategories.TryGetValue(keys.Key.NoCategorie, out lstTempo))
            {
                PPVendeurs vendeur = (from v in tableVendeurs
                              where v.NoVendeur == keys.Key.NoVendeur
                              select v).First();
                lstTempo.Add(vendeur);
            }
            else
            {
                List<PPVendeurs> lst = new List<PPVendeurs>();
                PPVendeurs vendeur = (from v in tableVendeurs
                                      where v.NoVendeur == keys.Key.NoVendeur
                                      select v).First();
                lst.Add(vendeur);
                lstCategories.Add(keys.Key.NoCategorie, lst);
            }
        }


        /*foreach(var produit in produits)
        {
            List<PPProduits> lstTempo;
            if (lstCategories.TryGetValue(produit.NoVendeur, out lstTempo))
            {
                lstTempo.Add(produit);
            }
            else
            {
                List<PPProduits> lst = new List<PPProduits>();
                lst.Add(produit);
                lstCategories.Add(produit.NoVendeur, lst);
            }
        }*/

        return lstCategories;
    }

    // get le nb de produits d'un certain vendeur pour une certaine catégorie
    public static int getNbProduitsEntrepriseDansCategorie(long? noCategorie, long? noVendeur)
    {
        var tableProduits = dataContext.PPProduits;
        return (from produit in tableProduits
                where produit.NoCategorie == noCategorie && produit.NoVendeur == noVendeur
                select produit).Count();
               
    }

    // get une catégorie spécifique
    public static PPCategories getCategorie(long? noCategorie)
    {
        var tableCategorie = dataContext.PPCategories;
        return (from c in tableCategorie
                           where c.NoCategorie == noCategorie
                           select c).First();
    }

    // retirer un item du panier
    public static void retirerArticlePanier(long? noPanier)
    {
        var tablePaniers = dataContext.PPArticlesEnPanier;
        var panier = (from p in tablePaniers where p.NoPanier == noPanier select p).First();
        dataContext.PPArticlesEnPanier.Remove(panier);
        dataContext.SaveChanges();
    }

    // modifier quantite dans panier
    public static int modifierQuantitePanier(long? noPanier, String strNbItems)
    {
        // codes de retour
        // 400 -> réussi sans erreur
        // 401 -> pas une entrée valide
        // 402 -> quantité en stock insuffisante
        int code = 400;

        var tablePaniers = dataContext.PPArticlesEnPanier;
        var panier = (from p in tablePaniers where p.NoPanier == noPanier select p).First();

        short nbItems = 0;
        bool valide = short.TryParse(strNbItems, out nbItems);

        if (!valide || nbItems < 1) { code = 401; }
        else if (panier.PPProduits.NombreItems < nbItems) { code = 402; }
        else
        {
            panier.NbItems = nbItems;
            dataContext.SaveChanges();
        }

        System.Diagnostics.Debug.WriteLine("Code: " + code);

        return code;
    }

    // get le pourcentage de taxes
    public static Decimal? getPourcentageTaxes(String typeTaxe)
    {
        var tableTPS = dataContext.PPTaxeFederale;
        var tableTVQ = dataContext.PPTaxeProvinciale;
        Decimal? taxe = 0;
        switch (typeTaxe)
        {
            case "TPS":
                taxe = (from tps in tableTPS orderby tps.DateEffectiveTPS descending select tps).First().TauxTPS;
                break;
            case "TVQ":
                taxe = (from tvq in tableTVQ orderby tvq.DateEffectiveTVQ descending select tvq).First().TauxTVQ;
                break;
        }
        return taxe;
    }
}