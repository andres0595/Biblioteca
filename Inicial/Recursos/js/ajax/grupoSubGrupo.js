$(document).ready(function () {
    listarGrupos(1);
    getConsecutivo();
    getConsecutivoSubGrupo();
});


////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////  VARIABLES GLOBALES /////////////////////////////////////////////
var pagGlobal = 1;
var pagGlobalSub = 1;
var idGlobal = "-1";
var numConsecutivoGlobal = 1;
var idGlobalSubgrupo = '-1';
var numConsecutivoGlobalSubGrupo = 1;

///////////////////////////////////////////////////////////////////////////////////////////////////////////
//// FUNCIONES INICIALES
///////////////////////////////////////////////////////////////////////////////////////////////////////////

function nuevo() {

    getConsecutivo();
    getConsecutivoSubGrupo();

    idGlobal = "-1";
    listarSubGrupos(1);

    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'scrolling': 'no',
        'href': '#divNuevo'
    });
}

function verTodos() {
    document.getElementById("txtCodigoFiltro").value = '';
    document.getElementById("txtNombreFiltro").value = '';
    listarGrupos(1);
}

function abrirFiltro() {
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'href': '#consulta'
    });
}

function limpiarFiltro() {
    document.getElementById("txtCodigoFiltro").value = '';
    document.getElementById("txtNombreFiltro").value = '';
}

function cancelarFiltro() {
    $.fancybox.close();
    limpiarFiltro();
}

function cancelaForm() {
    $.fancybox.close();
    limpiar();
    getConsecutivo();
}
////////////////////////////////////////////////////////////////////////////////////////////////////
function getConsecutivo() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'getConsecutivo'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGrupos.aspx', send, getConsecutivo_processResponse);
}
function getConsecutivo_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        var dataRows = info.data;
        if ((info.data != '0')) {
            numConsecutivoGlobal = parseInt(dataRows[0]) + 1;
            document.getElementById('txtCodigo').value = numConsecutivoGlobal;
        }
        else {
            numConsecutivoGlobal = 1;
            document.getElementById('txtCodigo').value = numConsecutivoGlobal;
        }

    } catch (elError) { }
}


/////////////////////////////////////////////////////////////////////////////////////////////////////
/// LIMPIAR
////////////////////////////////////////////////////////////////////////////////////////////////////

function limpiar() {
    //document.getElementById("txtCodigo").value = '';
    document.getElementById("txtNombre").value = '';
    document.getElementById("taDescripcion").value = '';
}

////////////////////////////////////////////////////////////////////////////////////////////////////////
///GUARDA GRUPOS
////////////////////////////////////////////////////////////////////////////////////////////////////////

function guardaGrupos() {
    var codigo = document.getElementById('txtCodigo').value;
    var nombre = document.getElementById('txtNombre').value;
    var descripcion = document.getElementById('taDescripcion').value;

    if (codigo != '' && nombre != '') {
        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', 'guardaGrupoSubGrupo'));
        arrayParameters.push(newArg('id', idGlobal));
        arrayParameters.push(newArg('codigo', codigo));
        arrayParameters.push(newArg('nombre', nombre));
        arrayParameters.push(newArg('descripcion', descripcion));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlGrupos.aspx', send, guardaGrupos_processResponse);
    }
    else
        muestraVentana(mensajeobligatorios)
}

function guardaGrupos_processResponse(res) {
    try {
        var info = eval('(' + res + ')');

        switch (info) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 1:
                muestraVentana(mensajeGuarda);
                listarGrupos(pagGlobal);
                limpiar();
                $.fancybox.close();
                break;
            case 2:
                muestraVentana(mensajeEdita);
                listarGrupos(pagGlobal);
                $.fancybox.close();
                break;
            case 3:
                muestraVentana(mensajeInfoExiste);
                break;
        }
    } catch (elError) {
    }
}

////////////////////////////////////////////////////////////////////////////////////////////////////////
//LISTAR GRUPO
////////////////////////////////////////////////////////////////////////////////////////////////////////
function listarGrupos(pag) {

    pagGlobal = pag;

    var codigo = document.getElementById("txtCodigoFiltro").value;
    var nombre = document.getElementById("txtNombreFiltro").value;

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listaGrupoSubGrupo'));
    arrayParameters.push(newArg('codigo', codigo));
    arrayParameters.push(newArg('nombre', nombre));
    arrayParameters.push(newArg('pag', pag));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlPaginador.aspx', send, listarGrupos_processResponse);
}

function listarGrupos_processResponse(res) {

    try {
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('listadoBodegas');
        if (res != '0') {
            var datosRows = info.data;
            var l = info.cols;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";

            var id = "";
            var codigo = "";
            var nombre = "";
            var descripcion = "";

            var tabla = "<table class='tbListado centrar' style='text-align: center;'><tr><td class='encabezado' colspan='9'>GRUPOS EXISTENTES</td></tr>";
            tabla += "<tr><td style='width:23%;' class='encabezado'>CÓDIGO</td><td style='width:42%;' class='encabezado'>NOMBRE DE GRUPO</td><td class='encabezado'>SUBGRUPOS</td><td class='encabezado'>DETALLE</td><td class='encabezado'>EDITAR</td><td class='encabezado'>ELIMINAR</td></tr>";

            for (var i = 0; i < datosRows.length; i += l) {
                id = datosRows[i]
                codigo = datosRows[i + 1];
                nombre = datosRows[i + 2];
                descripcion = datosRows[i + 3];


                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar2 = "cuerpoListado3";
                } else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar2 = "cuerpoListado5";
                }

                ctl = !ctl;
                tabla += '<tr><td class="' + claseAplicar2 + '" align="center">' + codigo + '</td><td class="' + claseAplicar2 + '" align="center">' + nombre + '</td>';
                tabla += '<td class="' + claseAplicar2 + '"><div id="imgAsignar" class="linkIconoLateral" onclick="AsignarSubgrupo(\'' + id + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/validar.png"><p>Asignar</p></div></td>';
                tabla += '<td class="' + claseAplicar2 + '"><div class="linkIconoLateral" onclick="verDetalleTercero(\'' + id + '\',\'' + codigo + '\',\'' + nombre + '\',\'' + descripcion + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/listar.png"><p>Detalle</p></div></td>';
                tabla += '<td class="' + claseAplicar2 + '"><div id="imgEditar" class="linkIconoLateral botonEditar" onclick="Editar(\'' + id + '\',\'' + codigo + '\',\'' + nombre + '\',\'' + descripcion + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/editar_24x24.png"><p>Editar</p></div></td>';
                tabla += '<td class="' + claseAplicar2 + '"><div id="imgEliminar" class="linkIconoLateral botonEliminar" onclick="confirmaElimina(\'' + id + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/eliminar_24x24.png"><p>Eliminar</p></div></td></tr>';
            }
            tabla += '</table>'
            divTerceros.innerHTML = tabla;
            divTerceros.innerHTML += pieDePaginaListar(info, 'listarGrupos');
            var idMenuForm = document.getElementById('idMenuForm').innerHTML; // se adiciona esta linea para que se el permiso de visualizar el editar despues de cargar
            permisosParaMenu(idMenuForm); // se adiciona esta linea para que se el permiso de visualizar el editar despues de cargar
        } else {
            divTerceros.innerHTML = mensajecero;
        }
    } catch (elError) {
    }
    $.fancybox.close();
}

////////////////////////////////////////////////////////////////////////////////////////////////////
// EDITAR
//////////////////////////////////////////////////////////////////////////////////////////////////////

function Editar(id, codigo, nombre, descripcion) {

    idGlobal = id;

    document.getElementById("txtCodigo").value = codigo;
    document.getElementById("txtNombre").value = nombre;
    document.getElementById('taDescripcion').value = descripcion;

    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'scrolling': 'no',
        'href': '#divNuevo'
    });
}

////////////////////////////////////////////////////////////////////////////////////////////////////
// DETALLE
//////////////////////////////////////////////////////////////////////////////////////////////////////

function verDetalleTercero(id, codigo, nombre, descripcion) {

    idGlobal = id;

    var tabla = "";
    var vacio = "--";

    tabla += "<table class='tbListado centrar' style='text-align: center;'><tr><td  class='encabezado' colspan ='2'><b>GRUPO</b></td></tr>";
    tabla += "<tr>";
    tabla += "<td class='encabezado'>CÓDIGO</td><td class='encabezado'>NOMBRE</td></tr>";
    tabla += '<tr><td class="cuerpoListado10">' + ((codigo == "") ? vacio : codigo) + '</td>';
    tabla += '<td class="cuerpoListado10">' + ((nombre == "") ? vacio : nombre) + '</td>';
    tabla += '</tr>';

    tabla += "<tr><td colspan='2' class='encabezado'>DESCRIPCION</td></tr>";
    tabla += '<tr>';
    tabla += '<td colspan="2" class="cuerpoListado10">' + ((descripcion == "") ? vacio : descripcion) + '</td>';
    tabla += '</tr>';

    tabla += "</table>";
    document.getElementById('listadoDetalle').innerHTML = tabla;

    listarSubGruposDetalle();

}


function listarSubGruposDetalle() {

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listarSubGruposDetalle'));
    arrayParameters.push(newArg('idGrupo', idGlobal));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGrupos.aspx', send, listarSubGruposDetalle_processResponse);
}

function listarSubGruposDetalle_processResponse(res) {

    try {
        var info = eval('(' + res + ')');
        var divSubGrupo = document.getElementById('listadoDetalle2');
        divSubGrupo.innerHTML = '';
        if (info.msj != '0') {
            var datosRows = info.data;
            var l = info.cols;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";

            var id = "";
            var idGrupo = "";
            var codigo = "";
            var nombre = "";
            var descripcion = "";

            var tabla = "<table class='tbListado centrar' style='text-align: center;'><tr><td class='encabezado' colspan='9'>SUBGRUPOS</td></tr>";
            tabla += "<tr><td style='width:23%;' class='encabezado'>CÓDIGO</td><td style='width:43%;' class='encabezado'>NOMBRE</td><td class='encabezado'>DETALLE</td></tr>";

            for (var i = 0; i < datosRows.length; i += l) {
                id = datosRows[i];
                idGrupo = datosRows[i + 1];
                codigo = datosRows[i + 2];
                nombre = datosRows[i + 3];
                descripcion = datosRows[i + 4];


                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar2 = "cuerpoListado3";
                } else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar2 = "cuerpoListado5";
                }

                ctl = !ctl;
                tabla += '<tr><td class="' + claseAplicar2 + '" align="center">' + codigo + '</td><td class="' + claseAplicar2 + '" align="center">' + nombre + '</td>';
                tabla += '<td class="' + claseAplicar2 + '"><div class="linkIconoLateral" onclick="verDetalleSubGrupos(\'' + 2 + '\',\'' + id + '\',\'' + codigo + '\',\'' + nombre + '\',\'' + descripcion + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/listar.png"><p>Detalle</p></div></td>';
            }
            tabla += '</table>'
            divSubGrupo.innerHTML = tabla;
        } else {
            divSubGrupo.innerHTML = mensajecero;
        }
    } catch (elError) {
    }
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'href': '#divDetalle'
    });

}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////  ELIMINA TERCERO   /////////////////////////////////////////////////

function confirmaElimina(id) {

    idGlobal = id;

    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'href': '#divEliminar'
    });

}

// Si le da clic en cancelar la desactivación 
function cancelaEliminacionTercero() {
    $.fancybox.close();
}

function elimina() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'eliminaGrupos'));
    arrayParameters.push(newArg('idGrupo', idGlobal));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGrupos.aspx', send, eliminaBodega_processResponse);
}

function eliminaBodega_processResponse(res) {
    try {
        var info = eval('(' + res + ')');

        switch (info) {
            case -2:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                listarGrupos(pagGlobal);
                $.fancybox.close();
                break;
            case 1:
                muestraVentana(mensajeElimina);
                listarGrupos(pagGlobal);
                limpiar();
                $.fancybox.close();
                break;
            case 2:
                muestraVentana(mensajeErrorEliminaRelacion);
                limpiar();
                $.fancybox.close();
                break;
        }
    } catch (elError) {
    }
}

//////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////SUBGRUPO
////////////////////////////////////////////////////////////////////////////////////////////////////
//ASIGNAR SUBGRUPO
function AsignarSubgrupo(id) {
    idGlobal = id;
    listarSubGrupos(1);
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'href': '#divGrupos'
    });
}

function verTodosSubGrupos() {
    nuevoFancySubGrupo();
    document.getElementById("txtCodigoFiltroSubGru").value = '';
    document.getElementById("txtNombreFiltroSubGru").value = '';
    listarSubGrupos(1);
}

function abrirFiltroSubGrupos() {
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'href': '#divFiltroSubgrupos'
    });
}

function limpiarFiltroSubGrupos() {
    document.getElementById("txtCodigoFiltroSubGru").value = '';
    document.getElementById("txtNombreFiltroSubGru").value = '';
}

function cancelarFiltroSubGrupos() {
    //$.fancybox.close();
    nuevoFancySubGrupo();
    limpiarFiltro();
}


function getConsecutivoSubGrupo() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'getConsecutivoSubGrupo'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGrupos.aspx', send, getConsecutivoSubGrupo_processResponse);
}
function getConsecutivoSubGrupo_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        var dataRows = info.data;
        if ((info.data != '0')) {
            numConsecutivoGlobalSubGrupo = parseInt(dataRows[0]) + 1;
            document.getElementById('txtCodigoSubGrupo').value = numConsecutivoGlobalSubGrupo;
        }
        else {
            numConsecutivoGlobal = 1;
            document.getElementById('txtCodigoSubGrupo').value = numConsecutivoGlobalSubGrupo;
        }

    } catch (elError) { }
}

////////////////////////////////////////////////////////////////////////////////////////////////////

function nuevoSubgrupo() {

    getConsecutivoSubGrupo();

    idGlobalSubgrupo = "-1";
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'scrolling': 'no',
        'href': '#nuevoSubgrupo'
    });
}

function limpiarSubGrupos() {
    document.getElementById("txtNombreSubgrupo").value = '';
    document.getElementById("taDescripcionSubGrupo").value = '';
    document.getElementById("txtCodigoFiltroSubGru").value = '';
    document.getElementById("txtNombreFiltroSubGru").value = '';

}

function cancelaFormSubgrupo() {
    nuevoFancySubGrupo();
    limpiarSubGrupos();
    getConsecutivoSubGrupo();
}

function guardaSubGrupos() {

    var codigo = document.getElementById('txtCodigoSubGrupo').value;
    var nombre = document.getElementById('txtNombreSubgrupo').value;
    var descripcion = document.getElementById('taDescripcionSubGrupo').value;

    if (codigo != '' && nombre != '') {
        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', 'guardaSubGrupos'));
        arrayParameters.push(newArg('idGrupo', idGlobal));
        arrayParameters.push(newArg('id', idGlobalSubgrupo));
        arrayParameters.push(newArg('codigo', codigo));
        arrayParameters.push(newArg('nombre', nombre));
        arrayParameters.push(newArg('descripcion', descripcion));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlGrupos.aspx', send, guardaSubGrupos_processResponse);
    }
    else
        muestraVentana(mensajeobligatorios)
}

function guardaSubGrupos_processResponse(res) {
    try {
        var info = eval('(' + res + ')');

        switch (info) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 1:
                muestraVentana(mensajeGuarda);
                getConsecutivoSubGrupo();
                nuevoFancySubGrupo();
                listarSubGrupos(pagGlobalSub);
                limpiarSubGrupos();
                break;
            case 2:
                nuevoFancySubGrupo();
                muestraVentana(mensajeEdita);
                getConsecutivoSubGrupo();
                listarSubGrupos(pagGlobalSub);
                limpiarSubGrupos();
                break;
            case 3:
                muestraVentana(mensajeInfoExiste);
                break;
        }
    } catch (elError) {
    }
}


function nuevoFancySubGrupo() {
    limpiarSubGrupos();
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'scrolling': 'no',
        'href': '#divGrupos'
    });
}


////////////////////////////////////////////////////////////////////////////////////////////////////////
//LISTAR SUBGRUPO
////////////////////////////////////////////////////////////////////////////////////////////////////////
function listarSubGrupos(pag) {

    pagGlobal = pag;

    var codigo = document.getElementById("txtCodigoFiltroSubGru").value;
    var nombre = document.getElementById("txtNombreFiltroSubGru").value;

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listarSubGrupos'));
    arrayParameters.push(newArg('id', idGlobal));
    arrayParameters.push(newArg('codigo', codigo));
    arrayParameters.push(newArg('nombre', nombre));
    arrayParameters.push(newArg('pag', pag));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlPaginador.aspx', send, listarSubGrupos_processResponse);
}

function listarSubGrupos_processResponse(res) {

    try {
        var info = eval('(' + res + ')');
        var divSubGrupo = document.getElementById('listadoSubgrupo');
        if (res != '0') {
            var datosRows = info.data;
            var l = info.cols;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";

            var id = "";
            var idGrupo = "";
            var codigo = "";
            var nombre = "";
            var descripcion = "";

            var tabla = "<table class='tbListado centrar' style='text-align: center;'><tr><td class='encabezado' colspan='9'>SUBGRUPOS EXISTENTES</td></tr>";
            tabla += "<tr><td style='width:23%;' class='encabezado'>CÓDIGO</td><td style='width:43%;' class='encabezado'>NOMBRE DEl SUBGRUPO</td><td class='encabezado'>DETALLE</td><td class='encabezado'>EDITAR</td><td class='encabezado'>ELIMINAR</td></tr>";

            for (var i = 0; i < datosRows.length; i += l) {
                id = datosRows[i];
                idGrupo = datosRows[i + 1];
                codigo = datosRows[i + 2];
                nombre = datosRows[i + 3];
                descripcion = datosRows[i + 4];


                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar2 = "cuerpoListado3";
                } else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar2 = "cuerpoListado5";
                }

                ctl = !ctl;
                tabla += '<tr><td class="' + claseAplicar2 + '" align="center">' + codigo + '</td><td class="' + claseAplicar2 + '" align="center">' + nombre + '</td>';
                tabla += '<td class="' + claseAplicar2 + '"><div class="linkIconoLateral" onclick="verDetalleSubGrupos(\'' + 1 + '\',\'' + id + '\',\'' + codigo + '\',\'' + nombre + '\',\'' + descripcion + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/listar.png"><p>Detalle</p></div></td>';
                tabla += '<td class="' + claseAplicar2 + '"><div id="imgEditar" class="linkIconoLateral botonEditar" onclick="EditarSubGrupos(\'' + id + '\',\'' + codigo + '\',\'' + nombre + '\',\'' + descripcion + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/editar_24x24.png"><p>Editar</p></div></td>';
                tabla += '<td class="' + claseAplicar2 + '"><div id="imgEliminar" class="linkIconoLateral botonEliminar" onclick="confirmaEliminaSubGrupo(\'' + id + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/eliminar_24x24.png"><p>Eliminar</p></div></td></tr>';
            }
            tabla += '</table>'
            divSubGrupo.innerHTML = tabla;
            divSubGrupo.innerHTML += pieDePaginaListar(info, 'listarSubGrupos');
            var idMenuForm = document.getElementById('idMenuForm').innerHTML; // se adiciona esta linea para que se el permiso de visualizar el editar despues de cargar
            permisosParaMenu(idMenuForm); // se adiciona esta linea para que se el permiso de visualizar el editar despues de cargar
        } else {
            divSubGrupo.innerHTML = mensajecero;
        }
    } catch (elError) {
    }
    //$.fancybox.close();

    //    $.fancybox({
    //        'showCloseButton': true,
    //        'hideOnOverlayClick': true,
    //        'transitionIn': 'fade',
    //        'transitionOut': 'fade',
    //        'transitionOut': 'fade',
    //        'href': '#divGrupos',
    //        'onClosed': function () {
    //        }
    //    });
}

////////////////////////////////////////////////////////////////////////////////////////////////////
// EDITAR
//////////////////////////////////////////////////////////////////////////////////////////////////////

function EditarSubGrupos(id, codigo, nombre, descripcion) {

    idGlobalSubgrupo = id;

    document.getElementById("txtCodigoSubGrupo").value = codigo;
    document.getElementById("txtNombreSubgrupo").value = nombre;
    document.getElementById('taDescripcionSubGrupo').value = descripcion;

    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'scrolling': 'no',
        'href': '#nuevoSubgrupo'
    });
}

////////////////////////////////////////////////////////////////////////////////////////////////////
// DETALLE
//////////////////////////////////////////////////////////////////////////////////////////////////////

function verDetalleSubGrupos(flag, id, codigo, nombre, descripcion) {

    var tabla = "";
    var vacio = "--";

    tabla += "<table class='tbListado centrar' style='text-align: center;'><tr><td  class='encabezado' colspan ='6'><b>SUBGRUPO</b></td></tr>";
    tabla += "<tr>";
    tabla += "<td class='encabezado'>CÓDIGO</td><td class='encabezado'>NOMBRE</td></tr>";
    tabla += '<tr><td class="cuerpoListado10">' + ((codigo == "") ? vacio : codigo) + '</td>';
    tabla += '<td class="cuerpoListado10">' + ((nombre == "") ? vacio : nombre) + '</td>';
    tabla += '</tr>';

    tabla += "<tr><td colspan='2' class='encabezado'>DESCRIPCION</td></tr>";
    tabla += '<tr>';
    tabla += '<td colspan="2" class="cuerpoListado10">' + ((descripcion == "") ? vacio : descripcion) + '</td>';
    tabla += '</tr>';

    if (flag == 1) {

        tabla += '<tr>';
        tabla += '<td colspan="10">';
        tabla += '<br /><hr />';
        tabla += '</td>';
        tabla += '</tr>';
        tabla += '<tr>';
        tabla += '<td colspan="10" align="center">';
        tabla += '<table>';
        tabla += '<tr>';
        tabla += '<td colspan="10" align="center">';
        tabla += '<span>Cancelar</span><br />';
        tabla += '<img src="../../Recursos/imagenes/administracion/cancelar.png" onclick="nuevoFancySubGrupo();"';
        tabla += 'alt="Cancelar" class="imgAdmin" />';
        tabla += '</td>';
        tabla += '</tr>';
        tabla += '</table>';
        tabla += '</td>';
        tabla += '</tr>';
    }
    else {

        tabla += '<tr>';
        tabla += '<td colspan="10">';
        tabla += '<br /><hr />';
        tabla += '</td>';
        tabla += '</tr>';
        tabla += '<tr>';
        tabla += '<td colspan="10" align="center">';
        tabla += '<table>';
        tabla += '<tr>';
        tabla += '<td colspan="10" align="center">';
        tabla += '<span>Cancelar</span><br />';
        tabla += '<img src="../../Recursos/imagenes/administracion/cancelar.png" onclick="$.fancybox.close();"';
        tabla += 'alt="Cancelar" class="imgAdmin" />';
        tabla += '</td>';
        tabla += '</tr>';
        tabla += '</table>';
        tabla += '</td>';
        tabla += '</tr>';
    }

    tabla += "</table>";
    document.getElementById('divDetalleSubGrupos').innerHTML = tabla;

    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'href': '#divDetalleSubGrupo'
    });
}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////  ELIMINA TERCERO   /////////////////////////////////////////////////

function confirmaEliminaSubGrupo(id) {

    idGlobalSubgrupo = id;

    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'href': '#divEliminarSubGrupo'
    });
}

function eliminaSubGrupo() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'eliminaSubGrupo'));
    arrayParameters.push(newArg('id', idGlobalSubgrupo));
    arrayParameters.push(newArg('idGrupo', idGlobal));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGrupos.aspx', send, eliminaSubGrupo_processResponse);
}

function eliminaSubGrupo_processResponse(res) {
    try {
        var info = eval('(' + res + ')');

        switch (info) {
            case -2:
                muestraVentana(mensajcemenosuno);
                break;
            case 0:
                nuevoFancySubGrupo();
                muestraVentana(mensajecero);
                listarSubGrupos(pagGlobal);
                break;
            case 1:
                nuevoFancySubGrupo();
                muestraVentana(mensajeElimina);
                listarSubGrupos(pagGlobal);
                break;
            case 2:
                muestraVentana(mensajeErrorEliminaRelacion);
                break;
        }
    } catch (elError) {
    }
}