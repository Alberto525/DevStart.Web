
var idlocalhost = document.getElementById("idlocalhost");
var Servidor = idlocalhost.baseURI;

window.onload = function () {
    ListarPerfil();
}




function ListarPerfil() {
    var url = Servidor + "/ListadoPerfil";
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
                TraerDataPerfil(Respuesta);
            }
            else {
                WebNotifyAsBlock("error", "NO SE PUDO TRAER LA INFORMACIÓN", "ERROR!!");
            }
        }
    }
    xhr.send();
}


var TBPerfil = document.getElementById("TBPerfil");

function TraerDataPerfil(Data) {
    var Contador = 1;
    if (Data != null) {
        var Contenido = "";
        Contenido += "<div class='col-lg-12'>";
        Contenido += "<table class='table table-bordered'>";
        Contenido += "<thead>";
        Contenido += "<tr>";
        Contenido += "<th>#</th>";
        Contenido += "<th>DESCRIPCIÓN</th>";
        Contenido += "<th>ESTADO</th>";
        Contenido += "<th>ACCIONES</th>";
        Contenido += "</tr>";
        Contenido += "</thead>";
        Contenido += "<tbody>";
        for (i = 0; i < Data.length; i++) {
            Contenido += "<tr>";
            Contenido += "<td>" + Contador++ + "</td>";
            Contenido += "<td>" + Data[i].Descripcion + "</td>";
            if (Data[i].Estado == true) {
                Contenido += "<td>";
                Contenido += "<a class='material-icons';>check_circle</a>";
                Contenido += "</td>";
            } else {
                Contenido += "<td>";
                Contenido += "<a class='material-icons';>check_circle_outline</a>";
                Contenido += "</td>";
            }
            Contenido += "<td>";
            Contenido += "<a class='material-icons'; href=" + Servidor + '/Edit/' + Data[i].ID_Perfil + "> edit </a>";
            Contenido += "<a class='material-icons'; href=" + Servidor + '/Detail/' + Data[i].ID_Perfil + "> content_paste </a>";
            Contenido += "<a class='material-icons'; href=" + Servidor + '/Delete/' + Data[i].ID_Perfil + "> delete </a>";
            Contenido += "</td>";
        }
        Contenido += "</tr>";
        Contenido += "</tbody>";
        Contenido += "</table>";
        Contenido += "</div>";
        TBPerfil.innerHTML = Contenido;
    }
}
