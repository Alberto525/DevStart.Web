

var URL = new URLSearchParams(location.search);
var ID_Perfil = URL.get('ID');
var IDDesc;


var Servidor = "https://localhost:44375/";

window.onload = function () {
    RQST.Decrypts(ID_Perfil);
    Mostrar();
}




var txtDescripcion = document.getElementById("txtDescripcion");
var chkActive = document.getElementById("chkActive");

function Mostrar() {

    var url = Servidor + "T_Perfil/MostrarDataPerfil?ID_Perfil=" + ID_Perfil;
    var xhr = new XMLHttpRequest();
    xhr.open("post", url, true);
    xhr.onloadstart = function () {
        //Aqui se bloquea el boton y se bloquea los input mientras se ejecuta la consulta
    }
    xhr.onreadystatechange = function () {

        if (xhr.readyState == 4 && xhr.status == 200) {
            //recibir la información del servidor y operar segun el caso
            Respuesta = JSON.parse(xhr.response);
            if (Respuesta != null) {
                JSON.stringify(Respuesta);
                document.getElementById("idDescripcion").classList.add("active");
                chkActive.checked = Respuesta.Estado;
                txtDescripcion.value = Respuesta.Descripcion;

            }
            else {
                WebNotifyAsBlock("error", "NO SE PUDO TRAER LA INFORMACIÓN", "ERROR!!");
            }
        }
    }
    xhr.send();

}





function Editar() {
    
      
    if (txtDescripcion.value !== "" && txtDescripcion.value.length > 3) {
        var url = Servidor + "T_Perfil/Editar";
        var Form = new FormData();
        Form.append("Descripcion", txtDescripcion.value);
        Form.append("Estado", chkActive.checked);
        Form.append("ID_Perfil", VD);

        var xhr = new XMLHttpRequest();
        xhr.open("post", url, true);
        xhr.onloadstart = function () {
        }
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {

                let Respuesta = xhr.responseText.split("_");
                if (Respuesta[0] == "Correcto") {
                    WebNotifyAsBlock("success", "OK", "Correcto!!");
                    //window.parent.location.href = Servidor + "Home/APPStart";
                    window.location = Servidor + "Perfil/T_Perfil";

                } else {

                }
            }
        }
        xhr.send(Form);
    } else {
        WebNotifyAsBlock("warning", "Falta rellenar algunos datos", "Información");
    }
    }
