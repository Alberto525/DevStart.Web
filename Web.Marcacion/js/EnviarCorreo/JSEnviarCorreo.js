
var Servidor = "https://localhost:44375/";


function Enviar() {
    var txtDestino = document.getElementById("txtDestino");
    var txtAsunto = document.getElementById("txtAsunto");
    var txtMensaje = document.getElementById("txtMensaje");

    if (txtDestino.value !== "" && txtAsunto.value !== "" && txtMensaje.value !== "") {
        var url = Servidor + "CorreoEnvio/Enviar";
        var Form = new FormData();
        Form.append("Asunto", txtAsunto.value);
        Form.append("Mensaje", txtMensaje.value);
        Form.append("Destino", txtDestino.value);
        var xhr = new XMLHttpRequest();
        xhr.open("post", url, true);
        xhr.onloadstart = function () {
        }
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {

                let Respuesta = xhr.responseText.split("_");
                if (Respuesta[0] == "Correcto") {
                    WebNotifyAsBlock("success", "OK", "Correcto!!");

                    window.location = Servidor + "EmpleadoJornada/T_EmpleadoJornada";

                } else {
                    WebNotifyAsBlock("error", "ERROR!!", Respuesta[1]);
                }
            }
        }
        xhr.send(Form);
    } else {
        WebNotifyAsBlock("warning", "Falta rellenar algunos datos", "Información");
    }
}