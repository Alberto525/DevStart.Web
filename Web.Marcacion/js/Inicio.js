

class Request {

    Encrypt(value, destino, type = 'e') {
        var xhr = new XMLHttpRequest();
        var frm = new FormData();
        frm.append("valor", value);
        let url = "/Home/";
        url += (type === 'e') ? "Encripta" : "Desencripta";
        xhr.open("POST", location.origin + url, true);
        xhr.onloadstart = function () {

        };
        xhr.onreadystatechange = function () {

            if (xhr.readyState === 4 && xhr.status === 200) {
                if (xhr.responseURL === location.origin + url) {

                    location.href = destino + "?ID=" + encodeURIComponent(xhr.responseText);

                }
                else {
                    location.href = xhr.responseURL;
                }
            }
        };
        xhr.send(frm);
    }


    Decrypts(Valor) {
        var xhr = new XMLHttpRequest();
        var frm = new FormData();
        frm.append("valor", Valor);
        let url = "/Home/Desencripta";
        xhr.open("POST", url, true);
        xhr.onloadstart = function () {

        };
        xhr.onreadystatechange = function () {

            if (xhr.readyState === 4 && xhr.status === 200) {
                VD = xhr.responseText;
            } else {
            }
        };

        xhr.send(frm);
    }



}
var VD;

var RQST = new Request();
