$(document).ready(function () /*dindica que nuesstra función listarDatos(n) se ejecuta al iniciar.*/ {
    cargaArea();
    listarCargos(1);


});

/////////////// VARIABLES GLOBALES /////////////////

var idGlobal = "-1"; // id para crear o modificar alarmas
var nombreFiltro = "";
var areaFiltro = "";

///////////////////////////////////////////////////

function limpiar() {
    document.getElementById("txtNombreCargo").value = '';
    document.getElementById("taDescripcion").value = '';
    document.getElementById("selArea").value = '-1';
    document.getElementById("txtNombreFiltroCargo").value = '';
    document.getElementById("selArea2").value = '-1';

}

function cargaArea() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargaListaArea'));
    arrayParameters.push(newArg('id_dpto_emp', ''));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, cargaArea_processResponse);
}

function cargaArea_processResponse(res) {
    var info = eval('(' + res + ')');
    var msj = info.msj;
    switch (msj) {
        case -1:
            muestraVentana(mensajemenosuno);
            break;
        case 0:
            limpiarSelect(document.getElementById('selArea'));
            break;
        case 1:
            llenarSelect(unescape(res), document.getElementById('selArea'));
            llenarSelect(unescape(res), document.getElementById('selArea2'));
            break;
    }
}

///**** Cierra los fancy
function cerrarFancy() {
    $.fancybox.close();
    idGlobal = "-1";
    limpiar();
}

function nuevoCargo() {

    limpiar();

    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionOut': 'fade',
        'transitionInt': 'fade',
        'scrolling': 'no',
        'href': '#divNuevoCargo'
    });
    // setTimeout("abrirNuevoCargo()", 1400);

}
function abrirNuevoCargo() {
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionOut': 'fade',
        'transitionInt': 'fade',
        'scrolling': 'no',
        'href': '#divNuevoCargo'
    });

}

//** Gaurda las area

function guardarCargo() {

    var id = idGlobal;
    var area = document.getElementById('selArea').value;
    var nombre = document.getElementById('txtNombreCargo').value;
    var descripcion = document.getElementById('taDescripcion').value;

    if (nombre != "" && area != "-1") {
        /// DECLARAMOS EL ARREGLO QUE VAMOS A ENVIAR 
        var arrayParameters = new Array();

        arrayParameters.push(newArg('p', 'guardarCargos'));
        arrayParameters.push(newArg('id', id));
        arrayParameters.push(newArg('nombre', nombre));
        arrayParameters.push(newArg('asignacion', 'True'));
        arrayParameters.push(newArg('descripcion', descripcion));
        arrayParameters.push(newArg('area', area));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlCargos.aspx', send, ventanaRespuesta_processResponse);
    }

    else {
        muestraVentana(mensajeobligatorios);
    }


}

//** Borra el cargo seleccionada

function borrarCargo() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'borrarCargos'));
    arrayParameters.push(newArg('id', idGlobal));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlCargos.aspx', send, ventanaRespuesta_processResponse);
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
                listarCargos(1);
                idGlobal = "-1";
                break;
            case 2:
                muestraVentana('Información Actualizada correctamente');
                $.fancybox.close();
                listarCargos(1);
                idGlobal = "-1";
                break;
            case 3:
                muestraVentana('Información Eliminada correctamente');
                $.fancybox.close();
                listarCargos(1);
                idGlobal = "-1";
                break;
            case 4:
                muestraVentana('NO SE PUEDE BORRAR EL &Aacute;REA, HAY CARGOS');
                $.fancybox.close();
                listarCargos(1);
                idGlobal = "-1";
                break;

        }
    } catch (elError) {
    }
}

//** crea la tabla del detalle
/*
function detalle(id, fecha, nombre, descripcion) {

var tabla = "";
var vacio = "--";

tabla += "<table class='tbListado centrar' style='text-align: center;'><tr><td  class='encabezado' colspan ='2'><b>  </b></td></tr>";
tabla += "<tr>";
tabla += "<td class='encabezado'>NOMBRE</td><td class='encabezado'>FECHA</td></tr>";
// tabla += '<tr><td class="cuerpoListado10">' + ((id == "") ? vacio : id) + '</td>';
tabla += '<td class="cuerpoListado10">' + ((nombre == "") ? vacio : unescape(nombre).toUpperCase()) + '</td>';
tabla += '<td class="cuerpoListado10">' + ((fecha == "") ? vacio : fecha) + '</td>';

tabla += "<table class='tbListado centrar' style='text-align: center;'><tr><td  class='encabezado' colspan ='2'><b>  </b>DESCRIPCIÓN</td></tr>";



tabla += '</tr>';

tabla += '<td class="cuerpoListado10">' + ((descripcion == "") ? vacio : unescape(descripcion).toUpperCase()) + '</td>';

tabla += '</tr>';
tabla += "</table>";
document.getElementById("divTablaDetalle").innerHTML = tabla;

$.fancybox({
'showCloseButton': true,
'hideOnOverlayClick': true,
'transitionIn': 'fade',
'transitionOut': 'fade',
'transitionOut': 'fade',
'href': '#divDetalleAlarma',
'onClosed': function () {
}
});

}
*/
///*** permite editar las alarmas

function editarCargo(id, nombre, descripcion, area) {

    idGlobal = id;
    document.getElementById("txtNombreCargo").value = unescape(nombre);
    document.getElementById("taDescripcion").value = unescape(descripcion);
    document.getElementById("selArea").value = area;

    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionOut': 'fade',
        'transitionInt': 'fade',
        'scrolling': 'no',
        'href': '#divNuevoCargo'
    });

}

///** Elimina las areas


function eliminarCargo(id) {
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

    document.getElementById("txtNombreFiltroCargo").value = '';
    document.getElementById("selArea2").value = '-1';
    listarCargos(1);
}

//** verifica que los campos del filtro tengan datos

function verificarFiltro() {
    listarCargos(1);

    limpiar();
}

/* MUESTRA EL DETALLE DEL CARGO*/


function detalle(nombre, descripcion, nombreArea) {

    var tabla = "";
    var vacio = "--";

    tabla += "<table class='tbListado centrar' style='text-align: center;'>";
    tabla += "<tr>";
    tabla += "<td class='encabezado'>NOMBRE &Aacute;REA</td><td class='encabezado'>NOMBRE DEL CARGO</td></tr>";
    tabla += '<td class="cuerpoListado10">' + ((nombreArea == "") ? vacio : unescape(nombreArea)) + '</td><td class="cuerpoListado10">' + ((nombre == "") ? vacio : unescape(nombre)) + '</td>';


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
        'href': '#divDetalleCargo'
    });

}



///////////////////////// LISTAR DATOS /////////////////////////////////


/* Me enlista (crea el paginador) los datos guardados por el fancy - se ejecuta al inicar por la sentencia de bien arriba*/

function listarCargos(pag) {

    pagGlobal = pag;

    //***** Filtro ***///
    var nombreFiltro = document.getElementById("txtNombreFiltroCargo").value;
    var area = document.getElementById("selArea2").value;
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listaTodosCargos'));
    arrayParameters.push(newArg('nombreFiltro', nombreFiltro));
    arrayParameters.push(newArg('area', area));
    arrayParameters.push(newArg('pag', pag));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlPaginador.aspx', send, listarCargos_processResponse);
}

/*crea la tabla donde se muestran los datos*/
function listarCargos_processResponse(res) {

    try {
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('divListadoCargos');
        if (res != '0') {
            var datosRows = info.data;
            var l = info.cols;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";

            var id = "";
            var nombre = "";
            var descripcion = "";
            var area = "";

            var tabla = "<table class='tbListado centrar' style='text-align: center;'><tr><td class='encabezado' colspan='5'>CARGOS EXISTENTES</td></tr>";
            tabla += "<tr><td class='encabezado' colspan='2'>NOMBRE</td><td class='encabezado'>DETALLE</td><td class='encabezado'>EDITAR</td><td class='encabezado'>ELIMINAR</td></tr>";

            for (var i = 0; i < datosRows.length; i += l) {
                id = datosRows[i]
                nombre = datosRows[i + 1];
                descripcion = datosRows[i + 2];
                area = datosRows[i + 3];
                nombreArea = datosRows[i + 4];

                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar2 = "cuerpoListado3";
                } else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar2 = "cuerpoListado5";
                }

                ctl = !ctl;
                tabla += '<tr><td colspan="2" class="' + claseAplicar + '" align="center">' + unescape(nombre).toUpperCase() + '</td>';
                tabla += '<td class="' + claseAplicar2 + '"><div id="imgDetalle" class="linkIconoLateral botonDetalle" onclick="detalle( \'' + nombre.toUpperCase() + '\',\'' + descripcion.toUpperCase() + '\',\'' + nombreArea.toUpperCase() + '\' )"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/listar.png"><p>Detalle</p></div></td>'; //copia los datos de la fila seleccionada 
                tabla += '<td class="' + claseAplicar2 + '"><div id="imgEditar" class="linkIconoLateral botonEditar" onclick="editarCargo( \'' + id + '\',\'' + nombre.toUpperCase() + '\',\'' + descripcion.toUpperCase() + '\',\'' + area + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/editar_24x24.png"><p>Editar</p></div></td>'; //copia los datos de la fila seleccionada 
                tabla += '<td class="' + claseAplicar2 + '"><div id="imgEliminar" class="linkIconoLateral botonEliminar" onclick="eliminarCargo(\'' + id + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/eliminar_24x24.png"><p>Eliminar</p></div></td></tr>';
            }
            tabla += '</table>'
            divTerceros.innerHTML = tabla;
            divTerceros.innerHTML += pieDePaginaListar(info, 'listarCargos');
            var idMenuForm = document.getElementById('idMenuForm').innerHTML; // se adiciona esta linea para que se de  el permiso de visualizar el editar despues de cargar
            permisosParaMenu(idMenuForm); // se adiciona esta linea para que se el permiso de visualizar el editar despues de cargar
        } else {
            divTerceros.innerHTML = mensajecero;
        }
    } catch (elError) {
    }
    $.fancybox.close();
}




