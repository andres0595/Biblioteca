///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////  VARIABLES GLOBALES   ///////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

var rolGlobal = ''; /* ROL QUE SE ESTA GUARDANDO, BUSCANDO, ELIMINANDO O MODIFICANDO */
var idMenus = new Array(); /* PARA GUARDAR LOS ID DE LOS MENUS QUE LLEGAN DE LA BASE DE DATOS */
var numEnvio = 0;
var paisGlobal = ''
var deptoGlobal = ''
var globlalIdRol = '';
var divRegGlobal = '';
var globalRolMenus = '';


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////  FUNCIONES JQUERY   ///////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
$(document).ready(function () {
    cargaNivelAcceso();
    listaRoles(1);
});


/*******************************************************************************************
CARGA EL DEPARTAMENTO
*******************************************************************************************/
function cargaDepartamento(codPais) {
    paisGlobal = codPais
    var arrayParameters = new Array()
    arrayParameters.push(newArg('p', 'cargaDeptoLista'))
    arrayParameters.push(newArg('pais', codPais))
    var send = arrayParameters.join('&')
    $.post('../../Controlador/ctlGeneral.aspx', send, cargaDepartamento_processResponse)
}

function cargaDepartamento_processResponse(res) {
    var info = eval("(" + res + ")")
    var msj = info.msj
    switch (msj) {
        case -1:
            muestraVentana(mensajemenosuno);
            break;
        case 0:

            break;
        case 1:
            var divListado = document.getElementById('divListaOpcionesVars');
            var ctl = true, claseAplicar = "", claseAplicar1 = "";
            var id = "", depto = "";
            //Inicio de la tabla
            var cadena = '<table width="100%">';
            //Cabecera
            cadena += '<tr><td class="encabezado">DEPARTAMENTOS</td><td class="encabezado">MUNICIPIOS</td></tr>';

            for (var i = 0; i < info.rows * 2; i += 2) {
                id = info.data[i];
                depto = info.data[i + 1];

                if (ctl) {
                    claseAplicar = "cuerpoListado3";
                    claseAplicar1 = "cuerpoListado7";
                }
                else {
                    claseAplicar = "cuerpoListado4";
                    claseAplicar1 = "cuerpoListado8";
                }
                ctl = !ctl;

                //Cuerpo
                cadena += '<tr><td class="' + claseAplicar1 + '"><input type="checkbox" id="dep_' + id + '" onclick="agregaDepto(\'' + id + '\')" />' + depto;
                cadena += '<div id="mun_dep_' + id + '" class="ocultar"><table class="centrar"><tr><td colspan="4" align="center"><b>MUNICIPIOS</b></td></tr>';
                cadena += '<tr><td align="center"><span>Sin Asignar</span></td>';
                cadena += '<td colspan="2" align="center">&nbsp;</td><td align="center"><span>Asignados</span></td></tr>';
                cadena += '<tr><td align="center"><select multiple="multiple" class="selectMultiple" id="selMunics' + id + '"></select></td>';
                cadena += '<td colspan="2" align="center"><input type="button" value=">>" onclick="agregaQuitaMunics(\'selMunics' + id + '\',\'selMunicsAgregados' + id + '\')" />';
                cadena += '<br /><input type="button" value="<<" onclick="agregaQuitaMunics(\'selMunicsAgregados' + id + '\',\'selMunics' + id + '\')" /></td>';
                cadena += '<td align="center"><select multiple="multiple" class="selectMultiple" id="selMunicsAgregados' + id + '"></select></td></tr></table>';
                cadena += '</div></td>';
                cadena += '<td class="' + claseAplicar + '">';
                cadena += '<div id="btnAg' + id + '" class="linkIconoLateral botonEditar" onclick="mostrarDivMunicipios(\'' + id + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/agregar24x24.png"><p>Agregar</p></div>';
                cadena += '<div id="btnOc' + id + '" class="linkIconoLateral botonEditar ocultar" onclick="ocultarDivMunicipios(\'' + id + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/eliminar24x24.png"><p>Ocultar</p></div>';
                cadena += '</td></tr>';
            }
            cadena += '</table>';
            divListado.innerHTML = cadena;
            break;
    }
}

/*********************************************************************************************
CAMBIA MUNICIPIOS DE UN SELECT A OTRO
*********************************************************************************************/
function agregaQuitaMunics(idOrigen, idDestino) {
    var munics = '';
    var aux = '';
    var selMunOrigen = new Array();
    selMunOrigen = retornaMunicipiosLista(idOrigen).split(',');
    var selMunDestino = document.getElementById(idDestino);
    for (var i = 0; i < (selMunOrigen.length - 1) ; i += 2) {
        selMunDestino.options[selMunDestino.length] = new Option(selMunOrigen[i + 1], selMunOrigen[i]);
    }
}

/*********************************************************************************************
RETORNA LOS MUNICIPIOS SELECCIONADOS
*********************************************************************************************/
function retornaMunicipiosLista(idSelect) {
    var munics = '';
    var aux = '';
    var selMun = document.getElementById(idSelect);
    var long = selMun.length;
    var arraySalen = new Array();
    try {
        for (var i = 0; i < long; i++) {
            aux = selMun.options[i];
            if (aux.selected) {
                munics += aux.value + ',' + aux.text + ',';
                arraySalen.push(aux.value);
            }
        }
        for (var i = 0; i < arraySalen.length; i++) {
            $("#" + idSelect).find("option[value='" + arraySalen[i] + "']").remove();
        }
    } catch (elError) {
    }
    return munics;
}

/*************************************************************************************************************************
AL CHEQUEAR O DESCHEQUEAR DEPARTAMENTOS LOS INCLUYE EN UN ARREGLO
**************************************************************************************************************************/
var deptosAgregados = new Array();
function agregaDepto(id) {
    if (document.getElementById('dep_' + id).checked) {
        deptosAgregados.push(id);
        //mostrarDivMunicipios(id);
    }
    else {
        deptosAgregados.splice(deptosAgregados.indexOf(id), 1);
        ocultarDivMunicipios(id);
        //Desagregar municipios
    }
}

function mostrarDivMunicipios(id) {
    document.getElementById('dep_' + id).checked = true;
    document.getElementById('mun_dep_' + id).style.display = 'block';
    $("#btnAg" + id).addClass("ocultar");
    $("#btnOc" + id).removeClass("ocultar");
    cargaMunicipio(id.toString());
}

function ocultarDivMunicipios(id) {
    document.getElementById('mun_dep_' + id).style.display = 'none';
    $("#btnOc" + id).addClass("ocultar");
    $("#btnAg" + id).removeClass("ocultar");
}

/*******************************************************************************************
CARGA EL MUNICIPIO
*******************************************************************************************/
function cargaMunicipio(codDepto) {
    deptoGlobal = codDepto;
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargaMpioLista'));
    arrayParameters.push(newArg('departamento', codDepto));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, cargaMunicipio_processResponse);
}

function cargaMunicipio_processResponse(res) {
    var info = eval("(" + res + ")");
    var msj = info.msj;
    switch (msj) {
        case -1:
            muestraVentana(mensajemenosuno);
            break;
        case 0:
            break;
        case 1:
            llenarSelectMultiple(res, document.getElementById('selMunics' + deptoGlobal));
            break;
    }
}

function guardaVariablesAcceso() {
    switch (tipoVarGlobal) {
        case 1:
            var arrayParameters = new Array();
            arrayParameters.push(newArg('p', 'restringeDeptosCiudades'));
            arrayParameters.push(newArg('departamentos', deptosAgregados.join(';')));
            var send = arrayParameters.join('&');
            $.post('../../Controlador/ctlVariables.aspx', send, cargaMunicipio_processResponse)
            break;
    }
}

/*******************************************************************************************
CARGA EL SELECT CON LOS NIVELES DE ACCESO DISPONIBLES
*******************************************************************************************/
function cargaNivelAcceso() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'nivelAcceso'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, cargaNivelAcceso_processResponse);
}

function cargaNivelAcceso_processResponse(res) {
    var info = eval("(" + res + ")");
    var msj = info.msj;
    switch (msj) {
        case -1:
            muestraVentana(mensajemenosuno);
            break;
        case 0:
            //muestraVentana(mensajeSinInformacion);
            limpiarSelectOpcion(document.getElementById('selNivelAcceso'));
            limpiarSelectOpcion(document.getElementById('selNivelAccesoFiltro'));
            break;
        case 1:
            llenarSelect(res, document.getElementById('selNivelAcceso'));
            llenarSelect(res, document.getElementById('selNivelAccesoFiltro'));
            break;
    }
}


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////  GUARDA ROLES     /////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/***********************************************************
METODO QUE PERMITE GUARDAR Y EDITAR ROLES
***********************************************************/
function guardaRol() {
    var id = rolGlobal;
    var rol = document.getElementById('txtRol').value;
    var nivel = document.getElementById('selNivelAcceso').value;
    var des = document.getElementById('taDescripcion').value;
    var opcion = 'guardar';

    if (id != '') {
        opcion = 'editar';
    }

    if ((rol != '') && nivel != '-1') {
        muestraVentanaProgreso();
        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', opcion));
        arrayParameters.push(newArg('des', des));
        arrayParameters.push(newArg('nivel', nivel));
        arrayParameters.push(newArg('rol', rol));
        arrayParameters.push(newArg('id', id));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlRol.aspx', send, guardaRol_processResponse);
    } else {
        muestraVentana(mensajeObligatorio);
    }
}


function guardaRol_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval("(" + res + ")");
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case -2:
                muestraVentana(mensajeErrorGuarda);
                break;
            case 0:
                muestraVentana(mensajecero);
                numEnvio = 0;
                break;
            case 1:
                $.fancybox.close();
                rolGlobal = '';
                muestraVentana(mensajeGuarda);
                limpiar2();
                listaRoles(1);
                break;
        }
    } catch (elError) {
    }
}

function abrirFiltro() {
    document.getElementById('txtRolFiltro').value = '';
    document.getElementById('selNivelAccesoFiltro').value = '-1';

    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'scrolling': 'no',
        'href': '#divFiltro'
    });
}


function verTodos() {
    limpiar2();
    listaRoles(1);
}


/* ********************************************************************************************************************************
LISTA LOS FORMATOS EXISTENTES
******************************************************************************************************************************** */
function listaRoles(pag) {
    var nombRol = document.getElementById('txtRolFiltro').value;
    var nivelRol = document.getElementById('selNivelAccesoFiltro').value;
    muestraVentanaProgreso();
    cancelaFormRol();

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listarGrupoRol'));
    arrayParameters.push(newArg('pag', pag));
    arrayParameters.push(newArg('rol', nombRol));
    arrayParameters.push(newArg('nivel', nivelRol));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlPaginador.aspx', send, listaRoles_processResponse);
}

function listaRoles_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + res + ')');
        var divListado = document.getElementById('listadoRoles');

        if (res != '0') {
            var dataRows = info.data;
            var l = dataRows.length;
            var cols = info.cols;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";
            var id = "";
            var rol = "";
            var desc = "";
            var nivel = "";
            var claseAplicar = "";
            //tabla = '<div style="float:left;" id="imgGuardar" class="linkIconoLateral botonGuardar ocultar" onclick="nuevoRol();"><img height="25px" width="25px" src="../../Recursos/imagenes/administracion/agregar_24x24.png"> <font size= "4">Nuevo</font></div><br /><br />';
            tabla = "<table class='tbListado centrar' style='text-align: center;'><tr><td class='encabezado' colspan='7'>ROLES EXISTENTES</td></tr>";
            tabla += "<tr><td class='encabezado'>ROL</td><td class='encabezado'>DESCRIPCI&Oacute;N</td><td class='encabezado'>DETALLE</td><td class='encabezado'>PERMISOS</td><td class='encabezado'>VARIABLES</td><td class='encabezado'>EDITAR</td><td class='encabezado'>ELIMINAR</td></tr>";

            for (var i = 0; i < l; i += cols) {
                id = dataRows[i];
                rol = dataRows[i + 1];
                desc = dataRows[i + 2];
                nivel = dataRows[i + 4];
                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar1 = "cuerpoListado3";
                }
                else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar1 = "cuerpoListado5";
                }
                ctl = !ctl;
                if (desc.length > 30) {
                    tabla += '<tr><td class="' + claseAplicar + '">' + rol + '</td><td class="' + claseAplicar + '">' + desc.substring(0, 30) + '...</td>';
                } else {
                    tabla += '<tr><td class="' + claseAplicar + '">' + rol + '</td><td class="' + claseAplicar + '">' + desc + '</td>';
                }
                tabla += '<td class="' + claseAplicar1 + '"><div id="linkDetalle" class="linkIconoLateral botonDetalle" onclick="DetalleRol(\'' + id + '\');"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/listar24x24.png"><p>Detalle</p></div></td>';
                tabla += '<td class="' + claseAplicar1 + '"><div id="imgPermisos" class="linkIconoLateral botonPermisos" onclick="permisosRol(' + id + ');"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/aceptar.png"><p>Permisos</p></div></td>';
                tabla += '<td class="' + claseAplicar1 + '"><div id="imgVariables" class="botonAcceso linkIconoLateral" onclick="ventanaVariables(' + id + ');"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/variables24x24.png"><p>Acceso</p></div></td>';
                tabla += '<td class="' + claseAplicar1 + '"><div id="imgEditar" class="linkIconoLateral botonEditar" onclick="editar(\'' + id + '\',\'' + rol + '\',\'' + desc + '\',\'' + nivel + '\');"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/editar_24x24.png"><p>Editar</p></div></td>';
                tabla += '<td class="' + claseAplicar1 + '"><div id="imgElimina" class="linkIconoLateral botonEliminar" onclick="confirmaEliminar(' + id + ');"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/eliminar_24x24.png"><p>Eliminar</p></div></td></tr>';
            }
            tabla += '</table>';
            divListado.innerHTML = tabla;
            divListado.innerHTML += pieDePaginaListar(info, 'listaRoles'); /*llama de nuevo el paginar con la nueva pag*/
            var idMenuForm = document.getElementById('idMenuForm').innerHTML; /* se adiciona esta linea para que se el permiso de visualizar el editar despues de cargar*/
            permisosParaMenu(idMenuForm); /* se adiciona esta linea para que se el permiso de visualizar el editar despues de cargar*/
        } else {
            divListado.innerHTML = mensajecero;
        }
    } catch (elError) {
    }
}

function ventanaVariables(id) {
    //muestraVariables();
    rolGlobal = id;
    listarRegionales();
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'scrolling': 'no',
        'href': '#accesosRol'
    });
}

function limpiarFormVariables() {
    rolGlobal = '';
    tipoVarGlobal = -1;
}


function listarRegionales() {

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listarRegionalesRol'));
    arrayParameters.push(newArg('rol', rolGlobal));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, listarRegionales_processResponse);

}

function listarRegionales_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('divListadoVariables');
        if (res != "{'msj':0}") {
            var datosRows = info.data;
            var l = info.cols;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";
            var contador = 0;
            var bandera = 0;
            var tabla = "<table class='tbListado centrar' style='text-align: center;'>";
            tabla += "<tr><td class='encabezado'>REGIONAL</td><td class='encabezado'>RESTRICCI&OacuteN</td></tr>";


            for (var i = 0; i < datosRows.length; i += l) {
                var reg_id = datosRows[i]                
                var reg_nombre = datosRows[i + 1];
                var check = datosRows[i + 2];


                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar2 = "cuerpoListado3";
                } else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar2 = "cuerpoListado5";
                }

                ctl = !ctl;
                
                tabla += '<tr>';
                tabla += '<td class="' + claseAplicar + '" align="center">' + unescape(reg_nombre).toUpperCase() + '</td>'
                if (check == 1) {
                    tabla += '<td class="' + claseAplicar2 + '"><div id="imgEditar" class="linkIconoLateral"><input type="checkbox" onclick="quitarCheckReg(\'' + reg_id + '\',this.checked)" checked id="chkRegional" /></div></td>';
                } else {
                    tabla += '<td class="' + claseAplicar2 + '"><div id="imgEditar" class="linkIconoLateral"><input type="checkbox" onclick="quitarCheckReg(\'' + reg_id + '\',this.checked)" id="chkRegional" /></div></td>';
                }
        }
        tabla += '</table>'
        divTerceros.innerHTML = tabla;
    } else {
            divTerceros.innerHTML = mensajecero;
}
} catch (elError) {
}
}

function quitarCheckReg(id, check) {

    if (check == true) {
        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', 'guardarRestriccionesRegionales'));
        arrayParameters.push(newArg('id', id));
        arrayParameters.push(newArg('id_rol', rolGlobal));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlGeneral.aspx', send, quitarCheckReg_processResponse);
    } else {
        eliminarRegistro(rolGlobal, id);
    }
}

function quitarCheckReg_processResponse(res) {
    var info = eval("(" + res + ")");
    //var msj = info.msj;
    switch (info) {
        case -1:
            muestraVentana(mensajemenosuno);
            break;
        case 0:
            muestraVentana(mensajeSinInformacion);           
        case 1:
            muestraVentana('Restricci&oacuten Almacenada Correctamente');
            break;
        case 2:
            muestraVentana('Registro Actualizado Correctamente');
            break;
    }
}

var tipoVarGlobal = -1;


function eliminarRegistro(rolGlobal, id) {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'eliminarRestriccionesRegionales'));
    arrayParameters.push(newArg('id', id));
    arrayParameters.push(newArg('id_rol', rolGlobal));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, quitarCheckReg_processResponse);

}

function editar(id, nombre, desc, nivel) {
    rolGlobal = id;
    $('#txtRol').val(nombre);
    $('#taDescripcion').val(desc);
    $('#selNivelAcceso').val(nivel);
    nuevoRolEditar();
}

function nuevoRol() {
    limpiar2();
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'href': '#divNuevoRol',
        'onClosed': function () {
            limpiar2();
        }
    });
}


function nuevoRolEditar() {
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'href': '#divNuevoRol',
        'onClosed': function () {
            limpiar2();
        }
    });
}


function limpiar2() {
    selTodos(false);
    rolGlobal = '';
    $('#txtRol').val('');
    $('#taDescripcion').val('');
    $('#selNivelAcceso').val('0');
    document.getElementById('txtRolFiltro').value = '';
    document.getElementById('selNivelAccesoFiltro').value = '-1';
}

function cancelaFormRol() {
    $.fancybox.close();
}

/* ********************************************************************************************************************************
ELIMINA LA INFORMACIÓN DEL FORMATO
******************************************************************************************************************************** */
function eliminar() {
    // var id = globalEliminar;
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'eliminar'));
    arrayParameters.push(newArg('id', globalEliminar));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlRol.aspx', send, eliminarprocessResponse);
}

function eliminarprocessResponse(res) {
    try {
        var info = eval("(" + res + ")");
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajeErrorElimina);
                break;
            case 1:
                $.fancybox.close();
                rolGlobal = '';
                muestraVentana(mensajeElimina);
                listaRoles(1);
                break;
        }
    } catch (elError) {
        //alert(elError);
    }
}

//function limpiar2() {
//    selTodos(false);
//}

/***********************************************************
METODO QUE PERMITE GUARDAR LOS PERMISOS DE LOS ROLES
***********************************************************/
function guardaPermisosRol2() {
    //var rol = document.getElementById('selRol').value;
    var rol = rolGlobal;
    if ((rol != '0')) {
        var m = idMenus.length;
        var sel = '';
        var cb = null;
        id = '';
        for (var i = 0; i < m; i++) {
            id = idMenus[i];
            cb = document.getElementById(id);
            if ((cb.checked)) {
                var ctl = false;
                var objForm = objForm = document.getElementById('form' + id);
                for (var j = 0; j < objForm.elements.length; j++) {
                    if (objForm.elements[j].type == "checkbox") {
                        if (objForm.elements[j].checked) {
                            ctl = true;
                            j = objForm.elements.length;
                        }
                    }
                }
                if (ctl) {

                    if (sel == '') {
                        sel = id;
                    }
                    else {
                        sel += ';' + id;
                    }
                    for (var k = 0; k < objForm.elements.length; k++) {
                        var e = objForm.elements[k];
                        if (e.type == "checkbox") {
                            if ((e.checked) && (e.id != 'todos' + id)) {
                                sel += ',' + e.id.substring(0, (e.id.length - id.length));
                            }
                        }
                    }
                }
            }
        }
        if (sel != '') {
            var arrayParameters = new Array();
            arrayParameters.push(newArg('p', 'guardar'));
            arrayParameters.push(newArg('rol', rol));
            arrayParameters.push(newArg('menus', sel));
            var send = arrayParameters.join('&');
            $.post('../../Controlador/ctlPermisosRol.aspx', send, guardaPermisosRol2_processResponse);
        } else {
            muestraVentana('Debe elegir al menos un men&uacute; con opciones.');
        }
    } else {
        muestraVentana(mensajeObligatorio);
    }
}


function guardaPermisosRol2_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval("(" + res + ")");
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case -2:
                muestraVentana(mensajeErrorGuarda);
                break;
            case 0:
                muestraVentana(mensajeInfoExiste);
                numEnvio = 0;
                break;
            case 1:
                muestraVentana(mensajeGuarda);
                cancelarFormPermisos();
                break;
        }
    } catch (elError) {
    }
}

function cancelarFormVariables() {
    ventanaVariables(rolGlobal);
}

function DetalleRol(idRol) {
    globlalIdRol = idRol;
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'rol_detalle'));
    arrayParameters.push(newArg('id', idRol));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, DetallePaciente_processResponse);
    muestraVentanaProgreso("Cargando ...");
}

function DetallePaciente_processResponse(res) {
    ocultaVentanaProgreso();
    try {
        var info = eval('(' + res + ')');

        if (res != "{'msj':0}") {
            var datosRows = info.data;

            var rol = datosRows[0];
            var niv_acceso = datosRows[1];
            var decripcion = datosRows[2];           

            document.getElementById("chkdetalleGeneral").checked = true;
            verDetalle("chkdetalleGeneral", true);
            document.getElementById("chkdetallePermisos").checked = false;
            verDetalle("chkdetallePermisos", false);
            document.getElementById("chkdetalleUsuarios").checked = false;
            verDetalle("chkdetalleUsuarios", false);
            document.getElementById("chkdetalleRegionales").checked = false;
            verDetalle("chkdetalleRegionales", false);
           

        
            var tabla = "";
            var vacio = "--";

            tabla += "<table class='tbListado centrar' class='encabezado' colspan ='2' style='text-align: center;'>";
            tabla += "<tr>";
            tabla += '<tr>';
            tabla += '<td colspan="2" class="encabezado">DETALLE ROL</td>';
            tabla += '</tr>';

            tabla += "<table class='tbListado centrar' class='encabezado' colspan ='2' style='text-align: center;'>";
            tabla += "<tr>";
            tabla += "<td class='encabezado'>ROL</td><td class='encabezado'>NIVEL ACCESO</td></tr>";
            tabla += '<td class="cuerpoListado10">' + ((rol == "") ? vacio : unescape(rol)) + '</td>';
            tabla += '<td class="cuerpoListado10">' + ((niv_acceso == "") ? vacio : unescape(niv_acceso)) + '</td>';

            tabla += "<tr>";
            tabla += "<td class='encabezado' colspan='2'>DESCRIPCION</td></tr>";
            tabla += '<td class="cuerpoListado10" colspan="2">' + ((decripcion == "") ? vacio : unescape(decripcion)) + '</td>';
         


           

            document.getElementById("divTablaDetalleRol").innerHTML = tabla;
            listarCategoriaRol();
            listarUsuariosRol();
            listarRegionalesRol();

            $.fancybox({
                'showCloseButton': false,
                'hideOnOverlayClick': false,
                'enableEscapeButton': false,
                'transitionIn': 'fade',
                'transitionOut': 'fade',
                'transitionOut': 'fade',
                'href': '#divDetalle'
            });

        } else {

        }
    } catch (elError) {
    }
}

//identidad, tipo_doc, nombreCompleto, fecha_nacimiento, genero, municipio, direccion, cod_postal, email, telefono1, telefono2, celular1, celular2, plan_sanitas, diagnostico, telefono_con, celular_con, sms_con, email_con, id_carga, id_ocupacion, ocupacion, id_dpto, depto, canal_reporte, prepagada, cono_diagnostico, con_tel, con_cel, con_sms, con_email

function verDetalle(id, value) {
    if (id == "chkdetalleGeneral") {
        if (value == true) {
            document.getElementById("divDetalleRol").style.display = 'block';
        } else {
            document.getElementById("divDetalleRol").style.display = 'none';
        }
    } else if (id == "chkdetallePermisos") {
        if (value == true) {
            document.getElementById("divTablaDetallePermisos").style.display = 'block';
        } else {
            document.getElementById("divTablaDetallePermisos").style.display = 'none';
        }
    } else if (id == "chkdetalleUsuarios") {
        if (value == true) {
            document.getElementById("divTablaDetalleUsuarios").style.display = 'block';
        } else {
            document.getElementById("divTablaDetalleUsuarios").style.display = 'none';
        }
    } else if (id == "chkdetalleRegionales") {
        if (value == true) {
            document.getElementById("divTablaDetalleRegionales").style.display = 'block';
        } else {
            document.getElementById("divTablaDetalleRegionales").style.display = 'none';
        }
    }
}

function listarCategoriaRol() {

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'CategoriaRol'));
    arrayParameters.push(newArg('id', globlalIdRol));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, listarCategoriaRol_processResponse);
    muestraVentanaProgreso("Cargando ...");
}

function listarCategoriaRol_processResponse(res) {
    ocultaVentanaProgreso();
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('divTablaDetallePermisos');
        if (res != "{'msj':0}") {
            var datosRows = info.data;
            var l = info.cols;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";
            //CategoriaArray.length = 0;
            var contador = 0;
          
            var tabla = "<table class='tbListado centrar' style='text-align: center;'><tr><td class='encabezado' colspan='2'>LISTA CATEGORIAS</td></tr>";
            tabla += "<tr><td class='encabezado' colspan='2'>CATEGOR&Iacute;A</td>";

            for (var i = 0; i < datosRows.length; i += l) {
                var id = datosRows[i];
                var categoria = datosRows[i + 1];

                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar2 = "cuerpoListado3";
                } else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar2 = "cuerpoListado5";
                }


                ctl = !ctl;
                tabla += '<tr>';
                // tabla += '<td class="' + claseAplicar2 + '" align="center">' + unescape(categoria).toUpperCase() + '</td>'

                tabla += '<td class="' + claseAplicar2 + '" "center" style="cursor: pointer; font-size:13px;" onclick="controllistarmenus(\'' + id + '\',\'' + globlalIdRol + '\')" colspan="2"><b>' + unescape(categoria).toUpperCase() + '</b></td></tr>';
                // tabla += '<td class="' + claseAplicar2 + '"><div id="imgBukeala" class="linkIconoLateral botonBukeala" onclick="controllistarciudades(\'' + id + '\')"><b>' + unescape(categoria).toUpperCase() + '</b></div></td>';
                tabla += '<tr></td><td class="cuerpoListado9" colspan="2"><div align="center" id="divAuxReg_' + id + '"></div></td></tr>';

            }
            tabla += '</table>'
            divTerceros.innerHTML = tabla;

        } else {
            divTerceros.innerHTML = mensajecero;
        }
    } catch (elError) {
    }
}


function controllistarmenus(categoria,rol) {

    divRegGlobal = categoria;
    globalRolMenus = rol;



    if (document.getElementById('divAuxReg_' + divRegGlobal + '').innerHTML == "") {
        listarMenusCategorias(categoria, rol);
    } else {
        document.getElementById('divAuxReg_' + divRegGlobal + '').innerHTML = "";
    }
}


function listarMenusCategorias(categoria, rol) {  

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listarMenusCategorias'));
    arrayParameters.push(newArg('categoria', categoria));
    arrayParameters.push(newArg('rol', rol));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, listarMenusCategorias_processResponse);
    muestraVentanaProgreso("Cargando ...");
}

function listarMenusCategorias_processResponse(res) {
    ocultaVentanaProgreso();
    try {
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('divAuxReg_' + divRegGlobal + '');
        if (res != "{'msj':0}") {
            var datosRows = info.data;
            var l = info.cols;
            var a = 0;
            var bandera = 0;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";
            //CiudadArray.length = 0;            

            var tabla = "<table class='tbListado centrar' style='text-align: center;'><tr><td class='encabezado' colspan='2'>LISTA MENUS</td></tr>";
            tabla += "<tr><td class='encabezado'>MEN&Uacute;</td><td class='encabezado'>PERMISOS</td>";

            //var bool = document.getElementById("chkSeleccionTodosCiudad"+ datosRows[2]).checked;

            for (var i = 0; i < datosRows.length; i += l) {
                var menu = datosRows[i];
                var permisos = datosRows[i + 1];

                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar2 = "cuerpoListado3";
                } else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar2 = "cuerpoListado5";
                }


                ctl = !ctl;
                tabla += '<tr>';
                tabla += '<td class="' + claseAplicar + '" align="center">' + unescape(menu).toUpperCase() + '</td>'
                tabla += '<td class="' + claseAplicar + '" align="center">' + unescape(permisos).toUpperCase() + '</td></tr>'

                //tabla += '<td class="' + claseAplicar2 + '" "center" style="cursor: pointer; font-size:13px;" onclick="controllistarmenus(\'' + id + '\',\'' + globlalIdRol + '\')" colspan="2"><b>' + unescape(categoria).toUpperCase() + '</b></td></tr>';
                // tabla += '<td class="' + claseAplicar2 + '"><div id="imgBukeala" class="linkIconoLateral botonBukeala" onclick="controllistarciudades(\'' + id + '\')"><b>' + unescape(categoria).toUpperCase() + '</b></div></td>';
                // tabla += '<tr></td><td class="cuerpoListado9" colspan="2"><div align="center" id="divAuxReg_' + id + '"></div></td></tr>';

            }
            tabla += '</table>'
            divTerceros.innerHTML = tabla;

        } else {
            divTerceros.innerHTML = mensajecero;
        }
    } catch (elError) { }
}

function listarUsuariosRol() {

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listarUsuariosRol'));
    arrayParameters.push(newArg('rol', globlalIdRol));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, listarUsuariosRols_processResponse);
    muestraVentanaProgreso("Cargando ...");
}

function listarUsuariosRols_processResponse(res) {
    ocultaVentanaProgreso();
    try {
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('divTablaDetalleUsuarios');
        if (res != "{'msj':0}") {
            var datosRows = info.data;
            var l = info.cols;
            var a = 0;
            var bandera = 0;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";
            //CiudadArray.length = 0;            

            var tabla = "<table class='tbListado centrar' style='text-align: center;'><tr><td class='encabezado' colspan='3'>LISTA USUARIOS</td></tr>";
            tabla += "<tr><td class='encabezado'>USUARIO</td><td class='encabezado'>NOMBRE USUARIO</td><td class='encabezado'>CATEGOR&Iacute;A</td>";

            //var bool = document.getElementById("chkSeleccionTodosCiudad"+ datosRows[2]).checked;

            for (var i = 0; i < datosRows.length; i += l) {
                var usuario = datosRows[i];
                var nombre = datosRows[i + 1];
                var categoria = datosRows[i + 2];

                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar2 = "cuerpoListado3";
                } else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar2 = "cuerpoListado5";
                }


                ctl = !ctl;
                tabla += '<tr>';
                tabla += '<td class="' + claseAplicar + '" align="center">' + unescape(usuario).toUpperCase() + '</td>';
                tabla += '<td class="' + claseAplicar + '" align="center">' + unescape(nombre).toUpperCase() + '</td>';
                tabla += '<td class="' + claseAplicar + '" align="center">' + unescape(categoria).toUpperCase() + '</td></tr>'

                //tabla += '<td class="' + claseAplicar2 + '" "center" style="cursor: pointer; font-size:13px;" onclick="controllistarmenus(\'' + id + '\',\'' + globlalIdRol + '\')" colspan="2"><b>' + unescape(categoria).toUpperCase() + '</b></td></tr>';
                // tabla += '<td class="' + claseAplicar2 + '"><div id="imgBukeala" class="linkIconoLateral botonBukeala" onclick="controllistarciudades(\'' + id + '\')"><b>' + unescape(categoria).toUpperCase() + '</b></div></td>';
                // tabla += '<tr></td><td class="cuerpoListado9" colspan="2"><div align="center" id="divAuxReg_' + id + '"></div></td></tr>';

            }
            tabla += '</table>'
            divTerceros.innerHTML = tabla;

        } else {
            divTerceros.innerHTML = mensajecero;
        }
    } catch (elError) { }
}



function listarRegionalesRol() {

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listarRegionalesRolDetalle'));
    arrayParameters.push(newArg('rol', globlalIdRol));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, listarRegionalesRol_processResponse);

}

function listarRegionalesRol_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('divTablaDetalleRegionales');
        if (res != "{'msj':0}") {
            var datosRows = info.data;
            var l = info.cols;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";
            var contador = 0;
            var bandera = 0;
            var tabla = "<table class='tbListado centrar' style='text-align: center;'>";
            tabla += "<tr><td class='encabezado'>REGIONAL</td></tr>";


            for (var i = 0; i < datosRows.length; i += l) {
                var reg_id = datosRows[i]                
                var reg_nombre = datosRows[i + 1];


                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar2 = "cuerpoListado3";
                } else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar2 = "cuerpoListado5";
                }

                ctl = !ctl;
                
                tabla += '<tr>';
                tabla += '<td class="' + claseAplicar + '" align="center">' + unescape(reg_nombre).toUpperCase() + '</td></tr>'
                //if (check == 1) {
                //    tabla += '<td class="' + claseAplicar2 + '"><div id="imgEditar" class="linkIconoLateral"><input type="checkbox" onclick="quitarCheckReg(\'' + reg_id + '\',this.checked)" checked id="chkRegional" /></div></td>';
                //} else {
                //    tabla += '<td class="' + claseAplicar2 + '"><div id="imgEditar" class="linkIconoLateral"><input type="checkbox" onclick="quitarCheckReg(\'' + reg_id + '\',this.checked)" id="chkRegional" /></div></td>';
                //}
            }
            tabla += '</table>'
            divTerceros.innerHTML = tabla;
        } else {
            divTerceros.innerHTML = mensajecero;
        }
    } catch (elError) {
    }
}