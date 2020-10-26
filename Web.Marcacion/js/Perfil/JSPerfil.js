
//var idlocalhost = document.getElementById("idlocalhost");
///var Servidor = idlocalhost.baseURI;

var Servidor = "https://localhost:44375/";

window.onload = function () {
    ListarPerfil();
}

function ListarPerfil() {
    var url = Servidor + "T_Perfil/ListadoPerfil";
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
            //Servidor + 'Perfil/T_Perfil/Edit?ID_Perfil=' + Data[i].ID_Perfil
            Contenido += "<a class='material-icons'; onclick='Editar(" + Data[i].ID_Perfil+")'> edit </a>";
           
            Contenido += "<a class='material-icons'; href=" + Servidor + 'Perfil/T_Perfil/Detail/' + Data[i].ID_Perfil + "> content_paste </a>";
            Contenido += "<a class='material-icons'; href=" + Servidor + 'Perfil/T_Perfil/Delete=GHGH/' + Data[i].ID_Perfil + "> delete </a>";
            Contenido += "</td>";
        }
        Contenido += "</tr>";
        Contenido += "</tbody>";
        Contenido += "</table>";
        Contenido += "</div>";
        TBPerfil.innerHTML = Contenido;
    }
}

function Editar(ID_Perfil) {
    
    RQST.Encrypt(ID_Perfil.toString(),"T_Perfil/Edit");
}



function Crear() {
    var txtDescripcion = document.getElementById("txtDescripcion");
    if (txtDescripcion.value !== "" && txtDescripcion.value.length > 3) {
        var url = Servidor + "T_Perfil/Crear";
        var Form = new FormData();
        Form.append("Descripcion", txtDescripcion.value);
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
