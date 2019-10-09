$(document).ready(function () /*dindica que nuesstra función listarDatos(n) se ejecuta al iniciar.*/ {

    listarCitaMedica(1);
    cargarEspecialidades();
});

/////////////// VARIABLES GLOBALES /////////////////

var idGlobal = "-1"; // id para crear o modificar alarmas

function limpiar() {
    document.getElementById("txtNombre").value = '';
    document.getElementById("txtFiltTramite").value = '';
    document.getElementById('txtcodigo').value = '';
    document.getElementById('txtcodigo').disabled = false;
    document.getElementById('selEspecialidad').value = '-1';
    document.getElementById('selFiltEspecialidad').value = '-1';
    document.getElementById('txtFiltCodOSI').value = '';
    document.getElementById('txtFiltTramite').value = '';
}

function verTodos() {
    limpiar();
    listarCitaMedica(1);
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
    idGlobal = "-1";
    OpenFancy('#Divsolicitud');
}


function GuardarCitaMedica() {

    var id = idGlobal;
    var codigo = document.getElementById('txtcodigo').value;
    var especialidad = document.getElementById('selEspecialidad').value;
    var nombre = document.getElementById('txtNombre').value;

    if (nombre != "" && codigo != "" && especialidad != "-1") {

        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', 'GuardarCitaMedica'));
        arrayParameters.push(newArg('id', id));
        arrayParameters.push(newArg('codigo', codigo));
        arrayParameters.push(newArg('especialidad', especialidad));
        arrayParameters.push(newArg('nombre', nombre));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlGeneral.aspx', send, GuardarCitaMedica_processResponse);
        muestraVentanaProgreso("cargando ...");
    }

    else {
        muestraVentana(mensajeobligatorios);
    }
}
function GuardarCitaMedica_processResponse(res) {
    ocultaVentanaProgreso();
    try {
        var info = eval('(' + res + ')');

        switch (info) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 1:
                listarCitaMedica(1);
                muestraVentana('CÓDIGO OSI ALMACENADO CORRECTAMENTE');
                idGlobal = "-1";
                $.fancybox.close();
                break;
            case 2:
                listarCitaMedica(1);
                muestraVentana('CÓDIGO OSI ACTUALIZADO CORRECTAMENTE');
                idGlobal = "-1";
                $.fancybox.close();
                break;
            case 3:
                listarCitaMedica(1);
                muestraVentana('CÓDIGO OSI ELIMINADO CORRECTAMENTE');
                idGlobal = "-1";
                $.fancybox.close();
                break;
            case 4:
                muestraVentana('NO SE PUEDE ELIMINAR EL CÓDIGO OSI<br />ESTA RELACIONADO EN ALGÚN TRÁMITE');
                $.fancybox.close();
                break;
            case 5:
                muestraVentana('YA EXISTE UN CÓDIGO OSI CON ESTE NOMBRE');
                idGlobal = "-1";
                limpiar();
                break;
            case 6:
                muestraVentana('YA EXISTE UN CÓDIGO OSI CON ESTE CÓDIGO');
                idGlobal = "-1";
                limpiar();
                break;
        }
    } catch (elError) {
    }
}

function listarCitaMedica(pag) {
    pagGlobal = pag;

    var especialidad = document.getElementById('selFiltEspecialidad').value;
    var codigo = document.getElementById('txtFiltCodOSI').value;
    var nombre = document.getElementById('txtFiltTramite').value;

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listarCitaMedica'));
    arrayParameters.push(newArg('especialidad', especialidad));
    arrayParameters.push(newArg('codigo', codigo));
    arrayParameters.push(newArg('nombre', nombre));
    arrayParameters.push(newArg('pag', pag));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlPaginador.aspx', send, listarCitaMedica_processResponse);
    muestraVentanaProgreso("cargando ...");
}

function listarCitaMedica_processResponse(res) {

    try {
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('divlistadocitas');
        if (res != '0') {
            ocultaVentanaProgreso();
            var datosRows = info.data;
            var l = info.cols;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";



            var tabla = "<table class='tbListado centrar' style='text-align: center;'><tr><td class='encabezado' colspan='6'>CÓDIGOS OSI EXISTENTES</td></tr>";
            tabla += "<tr><td class='encabezado' >TIPO DE SERVICIO</td><td class='encabezado' >C&Oacute;DIGO OSI</td><td class='encabezado'>NOMBRE</td><td class='encabezado'>DETALLE</td><td class='encabezado'>EDITAR</td><td class='encabezado'>ELIMINAR</td></tr>";

            for (var i = 0; i < datosRows.length; i += l) {
                var id = datosRows[i]
                var codigo = datosRows[i + 1];
                var nombre = datosRows[i + 2];
                var espec = datosRows[i + 3];
                var espec_nombre = datosRows[i + 4];

                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar2 = "cuerpoListado3";
                } else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar2 = "cuerpoListado5";
                }

                ctl = !ctl;
                tabla += '<tr><td class="' + claseAplicar + '" align="center">' + unescape(espec_nombre).toUpperCase() + '</td><td class="' + claseAplicar2 + '" align="center">' + unescape(codigo).toUpperCase() + '</td><td class="' + claseAplicar + '" align="center">' + nombre.substring(0, 30) + '...</td>';
                tabla += '<td class="' + claseAplicar2 + '"><div id="linkDetalle" class="linkIconoLateral botonDetalle" onclick="detalle( \'' + codigo.toUpperCase() + '\',\'' + nombre.toUpperCase() + '\' ,\'' + espec.toUpperCase() + '\',\'' + espec_nombre.toUpperCase() + '\'  )"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/listar.png"><p>Detalle</p></div></td>'; //copia los datos de la fila seleccionada
                tabla += '<td class="' + claseAplicar2 + '"><div id="linkEdita" class="linkIconoLateral botonEditar" onclick="editarCita( \'' + id + '\',\'' + codigo + '\',\'' + nombre.toUpperCase() + '\',\'' + espec + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/editar_24x24.png"><p>Editar</p></div></td>';
                tabla += '<td class="' + claseAplicar2 + '"><div id="linkElimina" class="linkIconoLateral botonEliminar" onclick="eliminarCita(\'' + codigo + '\',\'' + nombre + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/eliminar_24x24.png"><p>Eliminar</p></div></td></tr>';

            }
            tabla += '</table>'
            divTerceros.innerHTML = tabla;
            divTerceros.innerHTML += pieDePaginaListar(info, 'listarCitaMedica');
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

function editarCita(id, codigo, nombre, espec) {

    idGlobal = id;
    document.getElementById("txtcodigo").value = unescape(codigo);
    document.getElementById("txtcodigo").disabled = true;
    document.getElementById("txtNombre").value = unescape(nombre);
    document.getElementById('selEspecialidad').value = unescape(espec);
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'enableEscapeButton': false,
        'href': '#divNuevo'
    });
}

function eliminarCita(codigo, nombre) {
    idGlobal = codigo;
    document.getElementById('lblcitamedica').innerHTML = nombre;
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'enableEscapeButton': false,
        'href': '#EliminarConfirma'
    });
}
function eliminarCitaMedica() {

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'eliminarCitaMedica'));
    arrayParameters.push(newArg('id', idGlobal));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, GuardarCitaMedica_processResponse);
    muestraVentanaProgreso("cargando ...");
}

function cargarEspecialidades() {

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargarEspecialidades'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, cargarEspecialidades_processResponse);
}

function cargarEspecialidades_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                llenarSelect(res, document.getElementById('selEspecialidad'));
                llenarSelect(res, document.getElementById('selFiltEspecialidad'));
                break;
        }
    } catch (elError) { }
}

function detalle(codigo, nombre, espec, espec_nombre) {

    var tabla = "";
    var vacio = "--";

    tabla += "<table class='tbListado centrar' style='text-align: center;' colspan='2'>";
    tabla += "<tr>";
    tabla += "<td class='encabezado'>TIPO DE SERVICIO</td><td class='encabezado'>C&Oacute;DIGO OSI</td></tr>";

    tabla += '<td class="cuerpoListado10">' + ((espec_nombre == "") ? vacio : unescape(espec_nombre).toUpperCase()) + '</td>';
    tabla += '<td class="cuerpoListado10">' + ((codigo == "") ? vacio : unescape(codigo).toUpperCase()) + '</td></tr>';

    tabla += "<tr><td class='encabezado' colspan ='2'>NOMBRE TR&Aacute;MITE</td></tr>";
    tabla += '<td class="cuerpoListado10" colspan="2">' + ((nombre == "") ? vacio : unescape(nombre).toUpperCase()) + '</td></tr>';

    tabla += '</tr>';
    tabla += "</table>";
    document.getElementById("DivListadoDetalleTramite").innerHTML = tabla;


    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'href': '#divDetalleTramite'
    });


}