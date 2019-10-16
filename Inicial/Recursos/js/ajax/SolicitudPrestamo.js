$(document).ready(function () /*dindica que nuesstra función listarDatos(n) se ejecuta al iniciar.*/ {

    listarSolicitudes(1);

    $("#txtfechasolicitud").datepicker({
        changeMonth: true,
        changeYear: true
    });
});

/////////////// VARIABLES GLOBALES /////////////////

var idGlobal = "-1"; // id para crear o modificar alarmas

function fechaSistema() {
    var f = new Date();
    var meses = new Array("01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12");
    var fechaActual;
    var dia = "";
    dia = dia + f.getDate();
    var tam = dia.length;
    fechaActual = ((tam == 1) ? "0" + f.getDate() : f.getDate()) + "/" + meses[f.getMonth()] + "/" + f.getFullYear(); //descomentar si el dia de la fecha sale con un solo digito
    return fechaActual;
}


function limpiar() {
    document.getElementById("txtfechasolicitud").value = '';
    document.getElementById("selTipoLibro").value = '-1';
    document.getElementById('txtNombre').value = '';
}

function verTodos() {
    limpiar();
    listarSolicitudes(1);
}

///**** Cierra los fancy
function cerrarFancy() {
    $.fancybox.close();
    idGlobal = "-1";
    limpiar();
}
function abrirFiltro() {
    limpiar();
    OpenFancy('#filtro');
}

function NuevaSolicitud() {

    document.getElementById("txtfechasolicitud").value = fechaSistema();
    idGlobal = "-1";
    OpenFancy('#Divsolicitud');
}


function listarSolicitudes(pag) {
    pagGlobal = pag;

    var fechaSolicitud = document.getElementById('txtfechasolicitudFiltro').value;
    var tipolibro = document.getElementById('selTipoLibroFiltro').value;

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'paBLI_solicitudes_listar'));
    arrayParameters.push(newArg('fechaSolicitud', fechaSolicitud));
    arrayParameters.push(newArg('tipolibro', tipolibro))
    arrayParameters.push(newArg('pag', pag));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlPaginador.aspx', send, listarSolicitudes_processResponse);
    muestraVentanaProgreso("cargando ...");
}

function listarSolicitudes_processResponse(res) {

    try {
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('divListado');
        if (res != '0') {
            ocultaVentanaProgreso();
            var datosRows = info.data;
            var l = info.cols;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";

            var tabla = "<table class='tbListado centrar' style='text-align: center;'><tr><td class='encabezado' colspan='6'>SOLICITUDES EXISTENTES</td></tr>";
            tabla += "<tr><td class='encabezado' >FECHA SOLICITUD</td><td class='encabezado' >USUARIO</td><td class='encabezado'>TIPO LIBRO</td><td class='encabezado'>DETALLE</td><td class='encabezado'>EDITAR</td><td class='encabezado'>ELIMINAR</td></tr>";

            for (var i = 0; i < datosRows.length; i += l) {
                var id = datosRows[i]
                var fechasolicitud = datosRows[i + 1];
                var usuario = datosRows[i + 2];
                var tipoLibro = datosRows[i + 3];

                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar2 = "cuerpoListado3";
                } else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar2 = "cuerpoListado5";
                }

                ctl = !ctl;
                tabla += '<tr><td class="' + claseAplicar + '" align="center">' + unescape(fechasolicitud).toUpperCase() + '</td>';
                tabla += '<td class="' + claseAplicar2 + '" align="center">' + unescape(usuario).toUpperCase() + '</td>';
                tabla += '<td class="' + claseAplicar2 + '" align="center">' + unescape(tipoLibro).toUpperCase() + '</td>';
                tabla += '<td class="' + claseAplicar2 + '"><div id="linkDetalle" class="linkIconoLateral botonDetalle" onclick="detalle( \'' + id+ '\'  )"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/listar.png"><p>Detalle</p></div></td>';
                tabla += '<td class="' + claseAplicar2 + '"><div id="linkEdita" class="linkIconoLateral botonEditar" onclick="editarCita( \'' + id + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/editar_24x24.png"><p>Editar</p></div></td>';
                tabla += '<td class="' + claseAplicar2 + '"><div id="linkElimina" class="linkIconoLateral botonEliminar" onclick="eliminarCita(\'' + id + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/eliminar_24x24.png"><p>Eliminar</p></div></td></tr>';

            }
            tabla += '</table>'
            divTerceros.innerHTML = tabla;
            divTerceros.innerHTML += pieDePaginaListar(info, 'listarSolicitudes');
            var idMenuForm = document.getElementById('idMenuForm').innerHTML;
            permisosParaMenu(idMenuForm);
        } else {
            ocultaVentanaProgreso();
            divTerceros.innerHTML = ("AÚN NO SE HAN REGISTRADO SOLICITUDES");
        }
    } catch (elError) {
    }
    $.fancybox.close();
}


function buscarLibro() {
    listarLibros(1);
    OpenFancy('#Divlibros');
}

function listarLibros(pag) {
    pagGlobal = pag;

    var codigolibro = document.getElementById('txtcodfiltro').value;
    var nombrelibro = document.getElementById('txtnombrefiltro').value;

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', ' paBLI_libros_listar'));
    arrayParameters.push(newArg('codigolibro', codigolibro));
    arrayParameters.push(newArg('nombrelibro', nombrelibro))
    arrayParameters.push(newArg('pag', pag));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlPaginador.aspx', send, listarLibros_processResponse);
    muestraVentanaProgreso("cargando ...");
}

function listarLibros_processResponse(res) {

    try {
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('divlistadolibros');
        if (res != '0') {
            ocultaVentanaProgreso();
            var datosRows = info.data;
            var l = info.cols;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";

            var tabla = "<table class='tbListado centrar' style='text-align: center;'><tr><td class='encabezado' colspan='6'>SOLICITUDES EXISTENTES</td></tr>";
            tabla += "<tr><td class='encabezado' >CÓDIGO LIBRO</td><td class='encabezado' >NOMBRE LIBRO</td><td class='encabezado'>ASIGNAR</td></tr>";

            for (var i = 0; i < datosRows.length; i += l) {
                var codigo = datosRows[i]
                var nombre = datosRows[i + 1];

                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar2 = "cuerpoListado3";
                } else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar2 = "cuerpoListado5";
                }

                ctl = !ctl;
                tabla += '<tr><td class="' + claseAplicar + '" align="center">' + unescape(codigo).toUpperCase() + '</td>';
                tabla += '<td class="' + claseAplicar2 + '" align="center">' + unescape(nombre).toUpperCase() + '</td>';
                tabla += '<td class="' + claseAplicar2 + '"><div id="linkDetalle" class="linkIconoLateral botonAsignar" onclick="Asignarlibros(\'' + codigo + '\',\'' + nombre + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/listar.png"><p>Detalle</p></div></td>';
               

            }
            tabla += '</table>'
            divTerceros.innerHTML = tabla;
            divTerceros.innerHTML += pieDePaginaListar(info, 'listarLibros');
            var idMenuForm = document.getElementById('idMenuForm').innerHTML;
            permisosParaMenu(idMenuForm);
        } else {
            ocultaVentanaProgreso();
            divTerceros.innerHTML = mensajecero;
        }
    } catch (elError) {
    }
    $.fancybox.close();
}

function Asignarlibros(codigo,nombre) {
    document.getElementById("txtcodigo").value = codigo;
    document.getElementById("txtNombre").value = nombre;
    OpenFancy('#Divsolicitud');
}