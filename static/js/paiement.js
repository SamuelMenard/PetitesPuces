/* envoyer le paiement */

function lesiForm() {
    var form = document.createElement("form");
    form.setAttribute('method', "post");
    form.setAttribute('action', "https://personnel.lmbrousseau.info/lesi-2019/lesi-effectue-paiement.php");

    var NoVendeur = document.createElement("input"); //input element, text
    NoVendeur.setAttribute('type', "text");
    NoVendeur.setAttribute('name', "NoVendeur");
    form.appendChild(NoVendeur);
    
    var url = window.location.href;
    var NoVendeur = findGetParameter("IDEntreprise");
    var NomVendeur = document.getElementById('contentBody_lblNomEntreprise').innerHTML.replace(/&nbsp;/g, " ");

    var NoCarte = document.getElementById('contentBody_noCarte').value;
    var DateExpirationCarteCredit = document.getElementById('contentBody_tbDate').value;
    var NoSecuriteCarteCredit = document.getElementById('contentBody_tbCVV').value;

    var MontantPaiement = document.getElementById('contentBody_lblTotalPaiement').innerHTML.replace("$", "");
    MontantPaiement = MontantPaiement.replace(/,/g, ".");

    var NomPageRetour = "http://424s.cgodin.qc.ca/Pages/SaisieCommande.aspx?";


}

// source: https://stackoverflow.com/questions/5448545/how-to-retrieve-get-parameters-from-javascript
function findGetParameter(parameterName) {
    var result = null,
        tmp = [];
    var items = location.search.substr(1).split("&");
    for (var index = 0; index < items.length; index++) {
        tmp = items[index].split("=");
        if (tmp[0] === parameterName) result = decodeURIComponent(tmp[1]);
    }
    return result;
}



    