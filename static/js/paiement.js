/* envoyer le paiement */

function lesiForm() {
    var form = document.createElement("form");
    form.setAttribute('method', "post");
    form.setAttribute('action', "https://personnel.lmbrousseau.info/lesi-2019/lesi-effectue-paiement.php");
    
    
    var url = window.location.href;
    var NoVendeur = findGetParameter("IDEntreprise");
    var NomVendeur = document.getElementById('contentBody_lblNomEntreprise').innerHTML.replace(/&nbsp;/g, " ");

    var NoCarte = document.getElementById('contentBody_noCarte').value;
    var DateExpirationCarteCredit = document.getElementById('contentBody_tbDate').value;
    var NoSecuriteCarteCredit = document.getElementById('contentBody_tbCVV').value;

    var MontantPaiement = document.getElementById('contentBody_lblTotalPaiement').innerHTML.replace("$", "");
    MontantPaiement = MontantPaiement.replace(/,/g, ".");

    var NomPageRetour = url.replace("paiement", "confirmation");
    alert(NomPageRetour);

    // valider les champs
    var toutValide = true;
    var reNoCarte = /^\d{16}$/;
    var reNoSecurite = /^\d{3,4}$/;
    var reDateExpiration = /^\d{2}-\d{4}$/;

    if (!reNoCarte.test(NoCarte)) {
        document.getElementById('contentBody_noCarte').classList.add('erreur');
        toutValide = false;
    }
    else {
        document.getElementById('contentBody_noCarte').classList.remove('erreur');
    }

    if (!reNoSecurite.test(NoSecuriteCarteCredit)) {
        document.getElementById('contentBody_tbCVV').classList.add('erreur');
        toutValide = false;
    }
    else {
        document.getElementById('contentBody_tbCVV').classList.remove('erreur');
    }

    var dateValide = true;
    if (reDateExpiration.test(DateExpirationCarteCredit)) {
        var tabDate = DateExpirationCarteCredit.split("-");
        if (tabDate[0] >= 0 && tabDate[0] <= 12) {
            var today = new Date();
            var dateMax = new Date(2023, 12, 31);
            var dateCarte = new Date(tabDate[1], tabDate[0], 01);

            if (dateCarte > today && dateCarte < dateMax) {

            }
            else {
                toutValide = false;
                dateValide = false;
            }
        }
        else {
            toutValide = false;
            dateValide = false;
        }
    }
    else {
        toutValide = false;
        dateValide = false;
    }

    if (dateValide) {
        document.getElementById('contentBody_tbDate').classList.remove('erreur');
    }
    else {
        document.getElementById('contentBody_tbDate').classList.add('erreur');
    }

    if (toutValide) {
        // no vendeur
        var inputNoVendeur = document.createElement("input");
        inputNoVendeur.setAttribute('type', "text");
        inputNoVendeur.setAttribute('name', "NoVendeur");
        inputNoVendeur.value = NoVendeur;
        form.appendChild(inputNoVendeur);

        // nom vendeur
        var inputNomVendeur = document.createElement("input");
        inputNomVendeur.setAttribute('type', "text");
        inputNomVendeur.setAttribute('name', "NomVendeur");
        inputNomVendeur.value = NomVendeur;
        form.appendChild(inputNomVendeur);

        // No carte de crédit
        var inputNoCarteCredit = document.createElement("input");
        inputNoCarteCredit.setAttribute('type', "text");
        inputNoCarteCredit.setAttribute('name', "NoCarteCredit");
        inputNoCarteCredit.value = NoCarte;
        form.appendChild(inputNoCarteCredit);

        // Date expiration
        var inputDateExpiration = document.createElement("input");
        inputDateExpiration.setAttribute('type', "text");
        inputDateExpiration.setAttribute('name', "DateExpirationCarteCredit");
        inputDateExpiration.value = DateExpirationCarteCredit;
        form.appendChild(inputDateExpiration);

        // CVV
        var inputCVV = document.createElement("input");
        inputCVV.setAttribute('type', "text");
        inputCVV.setAttribute('name', "NoSecuriteCarteCredit");
        inputCVV.value = NoSecuriteCarteCredit;
        form.appendChild(inputCVV);

        // Montant paiement
        var inputMontant = document.createElement("input");
        inputMontant.setAttribute('type', "text");
        inputMontant.setAttribute('name', "MontantPaiement");
        inputMontant.value = MontantPaiement;
        form.appendChild(inputMontant);

        // Nom page retour
        var inputNomPageRetour = document.createElement("input");
        inputNomPageRetour.setAttribute('type', "text");
        inputNomPageRetour.setAttribute('name', "NomPageRetour");
        inputNomPageRetour.value = NomPageRetour;
        form.appendChild(inputNomPageRetour);

        document.body.appendChild(form);
        form.submit();
    }

    return false;
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



    