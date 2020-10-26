

var Servidor = "https://localhost:44375/";

function needcredentials() {

    var materialLoginFormEmail = document.getElementById("materialLoginFormEmail");
    var materialLoginFormPassword = document.getElementById("materialLoginFormPassword");
    if (materialLoginFormEmail.value != "" && materialLoginFormPassword.value != "") {

        var URL = Servidor + "Home/Ingresar";
        var Formulario = new FormData();
        Formulario.append("us", materialLoginFormEmail.value);
        Formulario.append("pw", materialLoginFormPassword.value);

        var xhr = new XMLHttpRequest();
        xhr.open("post", URL, true);
        xhr.onloadstart = function () {
            WebShowLoading();
        }
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {

                let Respuesta = xhr.responseText.split("_");
                if (Respuesta[0] == "Correcto") {
                    WebNotifyAsBlock("success", "OK", "Correcto!!");
                    window.location = Servidor + "Perfil/T_Perfil";
                } else if (Respuesta[0] == "Incorrecto"){
                    
                    WebNotifyAsBlock("error", "Credenciales Incorrectas", "ERROR!!");
                    setTimeout(function Regresar() {
                        window.location = Servidor;
                    } , 900);
                   
                }
            }
        }
        xhr.send(Formulario);
    } else {
        WebNotifyAsBlock("warning", "Falta rellenar algunos datos", "Información");
    }
}
