using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Description résumée de LibrairieLINQ
/// </summary>
public static class LibrairieLINQ
{

    public static object JSON { get; private set; }

    // Pour la connexion
    public static int connexionOK(String courriel, String mdp, String typeConnexion)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        /*
         * Type connexion
         * C = client
         * V = vendeur
         * G = gestionnaire
         * 
         * Codes
         * 400 -> connexion OK
         * 401 -> courriel ou mdp incorrect
         * 402 -> Compte désactivé
         * */
        int codeErreur = 401;

        var clients = dataContext.PPClients;
        var vendeurs = dataContext.PPVendeurs;
        var gestionnaires = dataContext.PPGestionnaires;
        
        switch (typeConnexion)
        {
            case "C":
                var client = from c in clients where c.AdresseEmail == courriel && c.MotDePasse == mdp select c;

                if (client.Count() == 0) { codeErreur = 401; }
                else if (client.First().Statut != 1) { codeErreur = 402; }
                else { codeErreur = 400; }
                break;
            case "V":
                var vendeur = from v in vendeurs where v.AdresseEmail == courriel && v.MotDePasse == mdp select v;
                if (vendeur.Count() == 0) { codeErreur = 401; }
                else if (vendeur.First().Statut != 1) { codeErreur = 402; }
                else { codeErreur = 400; }
                break;
            case "G":
                bool valide = gestionnaires.Where(gestionnaire => gestionnaire.courriel == courriel
                            && gestionnaire.motDePasse == mdp).Any();
                if (valide) { codeErreur = 400; }
                break;
        }
        return codeErreur;
    }

    // Avoir les infos de bases afin de les storer en variable de session
    public static List<String> infosBaseVendeur(String courriel)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        List<String> lstInfos = new List<string>();
        var vendeurs = dataContext.PPVendeurs;
        var infos = from vendeur in vendeurs
                    where vendeur.AdresseEmail == courriel
                    select vendeur;
        foreach (var info in infos)
        {
            string[] tab = { info.NoVendeur.ToString(), info.NomAffaires, info.Nom, info.Prenom, info.AdresseEmail };
            lstInfos.AddRange(tab);
        }
        return lstInfos;
    }

    // Avoir les infos de bases afin de les storer en variable de session
    public static List<String> infosBaseClient(String courriel)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
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
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var articlesPanier = dataContext.PPArticlesEnPanier;

        // retourner la liste des paniers
        Dictionary<Nullable<long>, List<PPArticlesEnPanier>> lstPaniers = new Dictionary<Nullable<long>, List<PPArticlesEnPanier>>();

        var articlesPanierClient = from articlePanier in articlesPanier
                                   where articlePanier.NoClient == noClient
                                   select articlePanier;

        foreach (var articlePanier in articlesPanierClient)
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
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableProduits = dataContext.PPProduits;
        var tableVendeurs = dataContext.PPVendeurs;
        Dictionary<Nullable<long>, List<PPVendeurs>> lstCategories = new Dictionary<long?, List<PPVendeurs>>();

        var produits = from produit in tableProduits
                       group produit by new { produit.NoCategorie, produit.NoVendeur }
                       into listeProduits
                       select listeProduits;


        foreach (var keys in produits)
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
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableProduits = dataContext.PPProduits;
        return (from produit in tableProduits
                where produit.NoCategorie == noCategorie && produit.NoVendeur == noVendeur
                select produit).Count();

    }

    // get une catégorie spécifique
    public static PPCategories getCategorie(long? noCategorie)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableCategorie = dataContext.PPCategories;
        return (from c in tableCategorie
                where c.NoCategorie == noCategorie
                select c).First();
    }

    // retirer un item du panier
    public static void retirerArticlePanier(long? noPanier)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tablePaniers = dataContext.PPArticlesEnPanier;
        var panier = (from p in tablePaniers where p.NoPanier == noPanier select p).First();
        dataContext.PPArticlesEnPanier.Remove(panier);
        dataContext.SaveChanges();
    }

    // modifier quantite dans panier
    public static int modifierQuantitePanier(long? noPanier, String strNbItems)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
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
    public static Decimal? getPourcentageTaxes(String typeTaxe, long? noVendeur)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableTPS = dataContext.PPTaxeFederale;
        var tableTVQ = dataContext.PPTaxeProvinciale;
        var tableVendeur = dataContext.PPVendeurs;

        PPVendeurs vendeur = (from v in tableVendeur
                             where v.NoVendeur == noVendeur
                             select v).First();

        bool appliqueTaxes = (bool)vendeur.Taxes;
        bool appliqueTVQ = (vendeur.Province == "QC") ? true : false;

        Decimal ? taxe = 0;
        if (appliqueTaxes)
        {
            switch (typeTaxe)
            {
                case "TPS":
                    taxe = (from tps in tableTPS orderby tps.DateEffectiveTPS descending select tps).First().TauxTPS;
                    break;
                case "TVQ":
                    taxe = (appliqueTVQ) ? (from tvq in tableTVQ orderby tvq.DateEffectiveTVQ descending select tvq).First().TauxTVQ : 0;
                    break;
            }
        }
        
        return taxe;
    }

    // get les produits d'une compagnie spécifique pour un vendeur spécifique
    public static List<PPArticlesEnPanier> getProduitsVendeurSpecifiqueClient(long? noClient, long? noVendeur)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableArticlesPanier = dataContext.PPArticlesEnPanier;
        var articlesPanier = from ap in tableArticlesPanier
                             where ap.NoClient == noClient && ap.NoVendeur == noVendeur
                             select ap;
        return articlesPanier.ToList();
    }

    // get informations client
    public static PPClients getFicheInformationsClient(long? noClient)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableClients = dataContext.PPClients;
        var client = (from c in tableClients
                      where c.NoClient == noClient
                      select c).First();
        return (PPClients)client;
    }

    // get code poids selon le poids du panier
    public static int getCodePoids(decimal poids)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        int code = 0;
        var tablePoids = dataContext.PPTypesPoids;
        code = (from p in tablePoids
               where p.PoidsMin <= poids && p.PoidsMax >= poids
               select p).First().CodePoids;
        return code;
    }

    // calculer le prix de livraison
    public static decimal getPrixLivraison(int codePoids, int codeLivraison, long noVendeur)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tablePoidsLivraison = dataContext.PPPoidsLivraisons;
        var tableVendeur = dataContext.PPVendeurs;

        Decimal? prixTheorique = (from pl in tablePoidsLivraison
                                 where pl.CodePoids == codePoids && pl.CodeLivraison == codeLivraison
                                 select pl).First().Tarif;

        if (prixTheorique == null) { prixTheorique = 0; }

        return (decimal)prixTheorique;
    }

    // get poids total panier
    public static Decimal getPoidsTotalPanierClient(long noClient, long noVendeur)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tablePaniers = dataContext.PPArticlesEnPanier;
        var tousArticles = from p in tablePaniers
                           where p.NoClient == noClient && p.NoVendeur == noVendeur
                           select p;

        return (Decimal)tousArticles.Sum(article => article.PPProduits.Poids * article.NbItems);
    }

    // get sous total avec les rabais!
    public static Decimal getSousTotalPanierClient(long noClient, long noVendeur)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tablePaniers = dataContext.PPArticlesEnPanier;
        var tousArticles = from p in tablePaniers
                           where p.NoClient == noClient && p.NoVendeur == noVendeur
                           select p;

        return (Decimal)tousArticles.Sum(article => ((article.PPProduits.DateVente > DateTime.Now) ? article.PPProduits.PrixVente : article.PPProduits.PrixDemande) * article.NbItems);
    }

    // vérifier si le poids de la commande dépasse le max de la compagnie
    public static bool depassePoidsMax(long noVendeur, Decimal poidsTotal)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableVendeur = dataContext.PPVendeurs;
        Decimal? maxPoidsCompagnie = (from v in tableVendeur
                                     where v.NoVendeur == noVendeur
                                     select v).First().PoidsMaxLivraison;

        bool valide = false;
        if (maxPoidsCompagnie != null)
        {
            if (poidsTotal > maxPoidsCompagnie)
            {
                valide = true;
            }
        }
        return valide;
    }

    // savoir si la TVQ est appliqué
    public static bool TVQApplique(long noVendeur)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableVendeur = dataContext.PPVendeurs;
        String province = (from v in tableVendeur
                       where v.NoVendeur == noVendeur
                       select v).First().Province;
        return (province == "QC") ? true : false;
    }

    // voir si il y a un item qui est out of stock dans le panier du client
    public static bool ruptureDeStockPanierClient(long? noClient, long? noVendeur)
    {
        bool rupture = false;
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableArticlesPanier = dataContext.PPArticlesEnPanier;
        var tousLesPaniers = from ap in tableArticlesPanier
                             where ap.NoClient == noClient && ap.NoVendeur == noVendeur
                             select ap;

        foreach(var panier in tousLesPaniers)
        {
            if (panier.PPProduits.NombreItems < 1)
            {
                rupture = true;
            }
        }
        return rupture;
    }

    // get poids maximum de livraison compagnie
    public static Decimal poidsMaximumLivraisonCompganie(long? noVendeur)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableVendeur = dataContext.PPVendeurs;
        Decimal? maxPoidsCompagnie = (from v in tableVendeur
                                      where v.NoVendeur == noVendeur
                                      select v).First().PoidsMaxLivraison;

        return (Decimal)maxPoidsCompagnie;
    }

    // get redevance vendeur
    public static Decimal? getRedevanceVendeur(long? noVendeur)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableVendeur = dataContext.PPVendeurs;
        return (from v in tableVendeur where v.NoVendeur == noVendeur select v).First().Pourcentage;
    }

    // get prix pour livraison gratuite
    public static Decimal prixPourLivraisonGratuite(long noVendeur)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableVendeur = dataContext.PPVendeurs;
        return (Decimal)(from v in tableVendeur where v.NoVendeur == noVendeur select v).First().LivraisonGratuite;
    }

    // get infos vendeur
    public static PPVendeurs getInfosVendeur(long? noVendeur)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableVendeur = dataContext.PPVendeurs;
        return (from v in tableVendeur where v.NoVendeur == noVendeur select v).First();
    }

    // get infos commande
    public static PPCommandes getInfosCommande(long? noCommande)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableCommande = dataContext.PPCommandes;
        return (from c in tableCommande where c.NoCommande == noCommande select c).First();
    }

    // get nouvelles demandes de vendeur
    public static List<PPVendeurs> getNouvellesDemandesVendeur()
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableVendeurs = dataContext.PPVendeurs;
        List<PPVendeurs> lst = (from v in tableVendeurs where v.Statut == null select v).ToList();
        
        return lst;
    }

    // accepter ou delete la demande d'un vendeur
    public static void accepterOuDeleteDemandeVendeur(long noVendeur, bool accepte, String redevance)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableVendeurs = dataContext.PPVendeurs;
        PPVendeurs vendeur = (from v in tableVendeurs where v.NoVendeur == noVendeur select v).First();

        if (accepte) { vendeur.Statut = 1; vendeur.Pourcentage = Decimal.Parse(redevance); }
        else { dataContext.PPVendeurs.Remove(vendeur); }
        dataContext.SaveChanges();

    }

    // get les clients inactifs depuis la période sélectionnée
    public static List<PPClients> getClientsInactifsDepuis(int nbMois)
    {

        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableClient = dataContext.PPClients;
        
        List<PPClients> lstClientsInactifs = new List<PPClients>();
        foreach(PPClients client in tableClient)
        {
            bool inactifPanier = false;
            bool inactifCommande = false;
            DateTime date = DateTime.Today;
            date = date.AddMonths(nbMois * -1);

            // si déjà désactivé ignorer
            if (client.Statut == 1)
            {
                // vérifier les panier
                var panierPlusRecent = from ap in client.PPArticlesEnPanier orderby ap.DateCreation descending select ap;
                if (panierPlusRecent.Count() > 0)
                {
                    if (panierPlusRecent.First().DateCreation < date)
                    {
                        inactifPanier = true;
                    }
                }
                else
                {
                    inactifPanier = true;
                }

                // vérifier les commandes
                var commandesPlusRecentes = from c in client.PPCommandes orderby c.DateCommande descending select c;
                if (commandesPlusRecentes.Count() > 0)
                {
                    if (commandesPlusRecentes.First().DateCommande < date)
                    {
                        inactifCommande = true;
                    }
                }
                else
                {
                    inactifCommande = true;
                }

                if (inactifCommande && inactifPanier) { lstClientsInactifs.Add(client); }
            }

        }
        return lstClientsInactifs;

    }

    // get vendeurs inactifs depuis
    public static List<PPVendeurs> getVendeursInactifsDepuis(int nbMois)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableVendeurs = dataContext.PPVendeurs;

        List<PPVendeurs> lstVendeursInactifs = new List<PPVendeurs>();
        foreach(PPVendeurs vendeur in tableVendeurs)
        {
            bool inactifProduits = false;
            bool inactifCommande = false;
            DateTime date = DateTime.Today;
            date = date.AddMonths(nbMois * -1);

            if (vendeur.Statut == 1)
            {
                // produits
                var produitPlusRecent = from produit in vendeur.PPProduits orderby produit.DateCreation descending select produit;
                if (produitPlusRecent.Count() > 0)
                {
                    if (produitPlusRecent.First().DateCreation < date)
                    {
                        inactifProduits = true;
                    }
                }
                else
                {
                    inactifProduits = true;
                }

                // commandes
                var commandePlusRecente = from commande in vendeur.PPCommandes orderby commande.DateCommande select commande;
                if (commandePlusRecente.Count() > 0)
                {
                    if (commandePlusRecente.First().DateCommande < date)
                    {
                        inactifCommande = true;
                    }
                }
                else
                {
                    inactifCommande = true;
                }
            }

            if (inactifProduits && inactifCommande) { lstVendeursInactifs.Add(vendeur); }
            
        }
        return lstVendeursInactifs;
    }

    // désactiver compte client
    public static void desactiverCompteClient(long noClient)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableClient = dataContext.PPClients;
        PPClients client = (from c in tableClient where c.NoClient == noClient select c).First();

        // delete le compte s'il n'a jamais visité de vendeur
        if (client.PPVendeursClients.Count() == 0)
        {
            dataContext.PPClients.Remove(client);
        }
        else if (client.PPCommandes.Count() == 0)
        {
            foreach(PPArticlesEnPanier ap in client.PPArticlesEnPanier.ToList())
            {
                dataContext.PPArticlesEnPanier.Remove(ap);
            }
            dataContext.PPClients.Remove(client);
        }
        else
        {
            foreach (PPArticlesEnPanier ap in client.PPArticlesEnPanier.ToList())
            {
                dataContext.PPArticlesEnPanier.Remove(ap);
            }
            client.Statut = 0;
        }

        
        dataContext.SaveChanges();

    }

    // désactiver compte client
    public static void desactiverCompteVendeur(long noVendeur)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableVendeur = dataContext.PPVendeurs;
        PPVendeurs vendeur = (from v in tableVendeur where v.NoVendeur == noVendeur select v).First();

        foreach(PPProduits produit in vendeur.PPProduits.ToList())
        {
            if (produit.PPDetailsCommandes.Count() == 0)
            {
                dataContext.PPProduits.Remove(produit);
            }
            else
            {
                produit.Disponibilité = false;
            }
        }

        vendeur.Statut = 0;
        dataContext.SaveChanges();

    }

    // get tout les clients
    public static List<PPClients> getListeClients()
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableClient = dataContext.PPClients;
        return (from client in tableClient where client.Statut == 1 select client).ToList();
    }

    // get tout les vendeurs
    public static List<PPVendeurs> getListeVendeurs()
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableVendeur = dataContext.PPVendeurs;
        return (from vendeur in tableVendeur where vendeur.Statut == 1 select vendeur).ToList();
    }

    // get vendeurs avec taux redevance modifiable
    public static List<PPVendeurs> getVendeursAvecRedevanceModifiable()
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableVendeur = dataContext.PPVendeurs;
        return (from vendeur in tableVendeur where vendeur.PPCommandes.Count() == 0 && vendeur.Statut == 1 select vendeur).ToList();
    }

    // modifier redevance vendeur
    public static void modifierRedevanceVendeur(long noVendeur, Decimal redevance)
    {
        BD6B8_424SEntities dataContext = new BD6B8_424SEntities();
        var tableVendeur = dataContext.PPVendeurs;
        PPVendeurs vendeur = (from v in tableVendeur
                             where v.NoVendeur == noVendeur
                             select v).First();
        vendeur.Pourcentage = redevance;
        dataContext.SaveChanges();
    }



}