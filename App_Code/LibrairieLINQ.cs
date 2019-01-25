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

    public static List<String> infosBaseVendeur(String courriel)
    {
        List<String> lstInfos = new List<string>();
        var vendeurs = dataContext.PPVendeurs;
        var infos = from vendeur in vendeurs
                    where vendeur.AdresseEmail == courriel
                    select vendeur;
        foreach(var info in infos)
        {
            string[] tab = { info.NoVendeur.ToString(), info.NomAffaires, info.Nom, info.Prenom };
            lstInfos.AddRange(tab);
        }
        return lstInfos;
    }
}