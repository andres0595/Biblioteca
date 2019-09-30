$(document).ready(function () /*dindica que nuesstra función listarDatos(n) se ejecuta al iniciar.*/
{
    listarGrupos(1);

});

/////////////// VARIABLES GLOBALES /////////////////

var idGlobal = "-1"; // id para crear o modificar 

///////////////////////////////////////////////////

/* LIMPIA LOS CAMPO */
function limpiar() {
    document.getElementById("txtNombreGrupo").value = '';
    document.getElementById("taDescripcion").value = '';
    document.getElementById("txtNombreFiltroGrupo").value = '';

}

///**** Cierra los fancy
function cerrarFancy() {
    $.fancybox.close();
    idGlobal = "-1";
    limpiar();
}

function nuevoGrupo() {
    document.getElementById("txtNombreGrupo").value="";
    document.getElementById("taDescripcion").value="";
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionOut': 'fade',
        'transitionInt': 'fade',
        'scrolling': 'no',
        'href': '#divNuevoGrupo'
    });

}

//** Gaurda las area

function guardarGrupo() {

    var id = idGlobal;
    var nombre = document.getElementById('txtNombreGrupo').value;
    var descripcion = document.getElementById('taDescripcion').value;

    if (nombre != "") {
        /// DECLARAMOS EL ARREGLO QUE VAMOS A ENVIAR 
        var arrayParameters = new Array();

        arrayParameters.push(newArg('p', 'guardarGrupo'));
        arrayParameters.push(newArg('id', id));
        arrayParameters.push(newArg('nombre', nombre));
        arrayParameters.push(newArg('descripcion', descripcion));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlGrupo.aspx', send, ventanaRespuesta_processResponse);
    }

    else {
        muestraVentana(mensajeobligatorios);
    }


}

///** Elimina - abre fancy de confirmacion


function eliminarGrupo(id) {
    idGlobal = id;
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionOut': 'fade',
        'transitionInt': 'fade',
        'scrolling': 'no',
        'href': '#divConfirmaEliminar'
    });
}


//** Borra la area seleccionada

function borrarGrupo() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'borrarGrupo'));
    arrayParameters.push(newArg('id', idGlobal));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGrupo.aspx', send, ventanaRespuesta_processResponse);
}

//** muestra las ventanas de respuestas

function ventanaRespuesta_processResponse(res) {
    try {
        var info = eval('(' + res + ')');

        switch (info) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 1:
                muestraVentana('Información almacenada correctamente');
                $.fancybox.close();
                listarGrupos(1);
                idGlobal = "-1";
                break;
            case 2:
                muestraVentana('Información Actualizada correctamente');
                $.fancybox.close();
                listarGrupos(1);
                idGlobal = "-1";
                break;
            case 3:
                muestraVentana('Información Eliminada correctamente');
                $.fancybox.close();
                listarGrupos(1);
                idGlobal = "-1";
                break;
        }
    } catch (elError) {
    }
}


//*** muesta un filtro por nombre

function abrirFiltro() {


    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'href': '#filtro'
    });
}

//**** muestra todo los datos

function verTodos() {

    document.getElementById("txtNombreFiltroGrupo").value = '';
    listarGrupos(1);
}

//** verifica que los campos del filtro tengan datos

function verificarFiltro() {
    listarGrupos(1);
    limpiar();
}


/* abre el fancy y le carga datos para editarlos*/
function editarGrupo(id, nombre, descripcion) {

    idGlobal = id;
    document.getElementById("txtNombreGrupo").value = unescape(nombre);
    document.getElementById("taDescripcion").value = unescape(descripcion);


    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionOut': 'fade',
        'transitionInt': 'fade',
        'scrolling': 'no',
        'href': '#divNuevoGrupo'       
    });

}

function detalle(nombre, descripcion) {

    var tabla = "";
    var vacio = "--";

    tabla += "<table class='tbListado centrar' style='text-align: center;'>";
    tabla += "<tr>";
    tabla += "<td class='encabezado'>NOMBRE DEL GRUPO</td></tr>";

    tabla += '<td class="cuerpoListado10">' + ((nombre == "") ? vacio : unescape(nombre)) + '</td>';


    tabla += "<table class='tbListado centrar' style='text-align: center;'><tr><td  class='encabezado' colspan ='2'><b>  </b>DESCRIPCIÓN</td></tr>";
    tabla += '</tr>';

    tabla += '<td class="cuerpoListado10">' + ((descripcion == "") ? vacio : unescape(descripcion)) + '</td>';

    tabla += '</tr>';
    tabla += "</table>";
    document.getElementById("divTablaDetalle").innerHTML = tabla;

    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'href': '#divDetalleGrupo'
    });

}


///////////////////////// LISTAR DATOS /////////////////////////////////


/* Me enlista (crea el paginador) los datos guardados por el fancy - se ejecuta al inicar por la sentencia de bien arriba*/

function listarGrupos(pag) {

    pagGlobal = pag;

    //***** Filtro ***///
    var nombre = document.getElementById("txtNombreFiltroGrupo").value;
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listarGrupos'));
    arrayParameters.push(newArg('nombre', nombre));
    arrayParameters.push(newArg('pag', pag));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlPaginador.aspx', send, listarGrupos_processResponse);
}

/*crea la tabla donde se muestran los datos*/
function listarGrupos_processResponse(res) {

    try {
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('divListadoGrupos');
        if (res != '0') {
            var datosRows = info.data;
            var l = info.cols;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";

            var id = "";
            var nombre = "";
            var descripcion = "";
            var tabla = "<table class='tbListado centrar' style='text-align: center;'><tr><td class='encabezado' colspan='5'>GRUPOS EXISTENTES</td></tr>";
            tabla += "<tr><td class='encabezado' colspan='2'>NOMBRE</td><td class='encabezado'>DETALLE</td><td class='encabezado'>EDITAR</td><td class='encabezado'>ELIMINAR</td></tr>";

            for (var i = 0; i < datosRows.length; i += l) {
                id = datosRows[i]
                nombre = datosRows[i + 1];
                descripcion = datosRows[i + 2];


                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar2 = "cuerpoListado3";
                } else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar2 = "cuerpoListado5";
                }

                ctl = !ctl;
                tabla += '<tr><td colspan="2" class="' + claseAplicar + '" align="center">' + unescape(nombre).toUpperCase() + '</td>';
                tabla += '<td class="' + claseAplicar2 + '"><div id="imgDetalle" class="linkIconoLateral botonDetalle" onclick="detalle(\'' + nombre.toUpperCase() + '\',\'' + descripcion.toUpperCase() + '\' )"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/listar.png"><p>Detalle</p></div></td>'; //copia los datos de la fila seleccionada 
                tabla += '<td class="' + claseAplicar2 + '"><div id="imgEditar" class="linkIconoLateral botonEditar" onclick="editarGrupo( \'' + id + '\',\'' + nombre.toUpperCase() + '\',\'' + descripcion.toUpperCase() + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/editar_24x24.png"><p>Editar</p></div></td>'; //copia los datos de la fila seleccionada 
                tabla += '<td class="' + claseAplicar2 + '"><div id="imgEliminar" class="linkIconoLateral botonEliminar" onclick="eliminarGrupo(\'' + id + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/eliminar_24x24.png"><p>Eliminar</p></div></td></tr>';
            }
            tabla += '</table>'
            divTerceros.innerHTML = tabla;
            divTerceros.innerHTML += pieDePaginaListar(info, 'listarGrupos');
            var idMenuForm = document.getElementById('idMenuForm').innerHTML; // se adiciona esta linea para que se de  el permiso de visualizar el editar despues de cargar
            permisosParaMenu(idMenuForm); // se adiciona esta linea para que se el permiso de visualizar el editar despues de cargar
        } else {
            divTerceros.innerHTML = mensajecero;
        }
    } catch (elError) {
    }
    $.fancybox.close();
}

function control(e, id) {
    if (e.ctrlKey == false && e.keyCode == 17) {

        muestraVentana('COMANDO RESTRINGIDO');
        document.getElementById(id).value = '';

    } else {
        //
    }



}

  