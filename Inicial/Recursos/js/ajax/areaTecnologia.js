///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////  VARIABLES GLOBALES   ///////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
var idGlobal = "-1"; // id para crear o modificar alarmas
var id_cargoGlobal = "-1"
var docuGLOBAL = "-1";
var nomGLOBAL = "-1";
var idAGLOBAL = '-1';

var idRGLOBAL = '-1';
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////  FUNCIONES JQUERY   ///////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

$(document).ready(function () /*dindica que nuesstra función listarDatos(n) se ejecuta al iniciar.*/{

    listarPQRArea(1);

});

function verTodos() {
    document.getElementById("txtFilDocumento").value = "";
    document.getElementById("txtFilRadicado").value = "";


    //listarCarguePQR(1);
    listarPQRArea(1);
}


function abrirFiltro() {
    document.getElementById("txtFilDocumento").value = "";
    document.getElementById("txtFilRadicado").value = "";

    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'enableEscapeButton': false,
        'href': '#divFiltroCarguePQR'

    });

}

function listarPQRArea(pag) {

    var documento = document.getElementById("txtFilDocumento").value;

    var radicado = document.getElementById("txtFilRadicado").value;


    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'paSP_area_pqr_listar'));
    // arrayParameters.push(newArg('documento', documento));
    arrayParameters.push(newArg('radicado', radicado));
    arrayParameters.push(newArg('documento', documento));
    arrayParameters.push(newArg('bandera', '31')); // ÁREA TECNOLOGÍA
    arrayParameters.push(newArg('pag', pag));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlPaginador.aspx', send, listarPQRArea_processResponse)
    muestraVentanaProgreso("cargando ...");
}

function listarPQRArea_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('divlistadoPQRArea');
        if (res != '0') {
            ocultaVentanaProgreso();
            var datosRows = info.data;
            var l = info.cols;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";

            var identidad = "";
            var nombre = "";
            var fecha = "";
            var cargados = "";
            var rechazados = "";
            var duplicados = "";
            var total = "";
            var tabla = "<table class='tbListado centrar' style='text-align: center;'><tr><td class='encabezado' colspan='8'>PQR'S EXISTENTES</td></tr>";
            tabla += "<tr><td class='encabezado'>No RADICADO</td><td class='encabezado'>DOCUMENTO</td><td class='encabezado'>NOMBRE</td><td class='encabezado'>FECHA</td><td class='encabezado'>DETALLE</td><td class='encabezado'>GESTIÓN</td><td class='encabezado'>ARCHIVO</td>";

            for (var i = 0; i < datosRows.length; i += l) {
                identidad = datosRows[i];
                documento = datosRows[i + 1];
                especialidad = datosRows[i + 2];
                fecha = datosRows[i + 3];
                nombre = datosRows[i + 4];
                archivo = datosRows[i + 5];




                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar2 = "cuerpoListado3";
                } else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar2 = "cuerpoListado5";
                }

                ctl = !ctl;
                tabla += '<tr><td class="' + claseAplicar2 + '" align="center">' + documento + '</td>';
                tabla += '<td class="' + claseAplicar + '" align="center">' + unescape(especialidad).toUpperCase() + '</td>';
                tabla += '<td class="' + claseAplicar + '" align="center">' + unescape(fecha).toUpperCase() + '</td>';
                //tabla += '<td class="' + claseAplicar + '" align="center">' + unescape(correo).toUpperCase().substring(0,18) + '@...</td>';
                tabla += '<td class="' + claseAplicar + '" align="center">' + unescape(nombre).toUpperCase() + '</td>';

                tabla += '<td class="' + claseAplicar2 + '"><div id="linkElimina" class="linkIconoLateral" onclick="analisisAlterno(\'' + identidad + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/listar.png"><p>Detalle</p></div></td>';

                tabla += '<td class="' + claseAplicar2 + '"><div id="linkElimina" class="linkIconoLateral" onclick="analisisAlterno(\'' + identidad + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/aceptar.png"><p>Gestionar</p></div></td>';
                //tabla += '<td class="' + claseAplicar2 + '"><div id="linkElimina" class="linkIconoLateral" onclick="analisisAlterno(\'' + identidad + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/listar.png"><p>Detalle</p></div></td>';

                if (archivo != '') {
                    tabla += '<td class="' + claseAplicar2 + '"><div id="linkElimina" class="linkIconoLateral" onclick="verArchivoTramite(\'' + archivo + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/cargar.png"><p>Visualizar</p></div></td>';
                } else {
                    tabla += '<td class="' + claseAplicar2 + '"><div id="linkElimina" class="linkIconoLateral" ><b>Llamada</b></div></td>';

                }

                if (datosRows[i + 7] == 2) {
                    tabla += '<td class="' + claseAplicar2 + '"><div id="linkElimina" class="linkIconoLateral" >--</div></td></tr>';
                }

                else {

                    if (datosRows[i + 8] == 1) {
                        tabla += '<td class="' + claseAplicar2 + '"><div id="linkElimina" class="linkIconoLateral" onclick="transferirTramite(\'' + documento + '\', \'' + nombre + '\', \'' + identidad + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/todos.png"><p>Aprobar</p></div></td></tr>';
                    }
                    else if (datosRows[i + 8] == 2) {

                        tabla += '<td class="' + claseAplicar2 + '"><div id="linkElimina" class="linkIconoLateral" >--</div></td></tr>';
                    }
                }
            }
            tabla += '</table>'
            divTerceros.innerHTML = tabla;
            divTerceros.innerHTML += pieDePaginaListar(info, 'listarCargueAlterno');
            var idMenuForm = document.getElementById('idMenuForm').innerHTML; // se adiciona esta linea para que se de  el permiso de visualizar el editar despues de cargar
            permisosParaMenu(idMenuForm); // se adiciona esta linea para que se el permiso de visualizar el editar despues de cargar
        } else {
            ocultaVentanaProgreso();
            divTerceros.innerHTML = mensajecero;
        }
    } catch (elError) {
    }
    $.fancybox.close();
}
