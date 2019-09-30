var rolGlobal = '';

//////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////// VARIABLES GLOBALES //////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////

var idMenus = new Array();

//////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////// FUNCIONES AUTOMATICAS ///////////////////////////
//////////////////////////////////////////////////////////////////////////////////////
$().ready(function () {
    cargaRoles();
    cargaCategoriasMenus();
});


/* ****************************************************************
CARGA LAS VISITAS
**************************************************************** */
function cargaRoles() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargaPermisos'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlPermisosRol.aspx', send, cargaRoles_processResponse);
}

function cargaRoles_processResponse(res) {
    try {
        var info = eval("(" + res + ")");
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                llenarSelect(res, document.getElementById('selRol'));
                break;
        }
    } catch (elError) {
    }
}


/*************************************************************************************************************************
CARGA LAS CATEGORIAS DE LOS MENÚS
**************************************************************************************************************************/
function cargaCategoriasMenus() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargaCategorias'));
    muestraVentanaProgreso();
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlPermisosRol.aspx', send, cargaCategoriasMenus_processResponse);
}

function cargaCategoriasMenus_processResponse(res) {
    var info = eval("(" + res + ")");
    var msj = info.msj
    ocultaVentanaProgreso();
    switch (msj) {
        case -1:
            muestraVentana(mensajemenosuno);
            break;
        case 0:
            document.getElementById('menusDisponibles').innerHTML = mensajecero;
            break;
        case 1:
            var divListado = document.getElementById('menusDisponibles');
            var ctl = true, claseAplicar = "", claseAplicar1 = "";
            var id = "", categoria = "";
            var cadena = '<table width="100%">';
            cadena += '<tr><td class="encabezado">CATEGOR&Iacute;A</td></tr>';

            for (var i = 0; i < info.rows * 2; i += 2) {
                id = info.data[i];
                categoria = info.data[i + 1];

                if (ctl) {
                    claseAplicar = "cuerpoListado7";
                }
                else {
                    claseAplicar = "cuerpoListado8";
                }
                ctl = !ctl;

                //Cuerpo
                cadena += '<tr><td class="' + claseAplicar + '"><input type="checkbox" id="cat_' + id + '" onclick="cargaMenusCategoriaRol(\'' + id + '\')">' + categoria;
                cadena += '<div id="men_cat_' + id + '" class="ocultar"></div></td></tr>';
            }
            cadena += '</table>';
            divListado.innerHTML = cadena;
            divListado.style.display = 'block';
            break;
    }
}


//////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////// FORMULARIO PERMISOS ROL /////////////////////////
//////////////////////////////////////////////////////////////////////////////////////
var categoriaCheck = '';
/***********************************************************
METODO QUE PERMITE CARGAR MENU
***********************************************************/
function cargaMenusCategoriaRol(categoria) {
    categoriaCheck = categoria;
    divMenusCategoria = document.getElementById('men_cat_' + categoriaCheck);
    if (document.getElementById('cat_' + categoria).checked) {
        if (divMenusCategoria.innerHTML.length == 0) {
            var arrayParameters = new Array();
            arrayParameters.push(newArg('p', 'cargaMenusCategoria'));
            arrayParameters.push(newArg('categoria', categoriaCheck));
            var send = arrayParameters.join('&');
            $.post('../../Controlador/ctlRol.aspx', send, cargaMenusCategoriaRol_processResponse);
        } else {
            divMenusCategoria.style.display = 'block';
        }
    } else {
        divMenusCategoria.style.display = 'none';
    }
}

function cargaMenusCategoriaRol_processResponse(res) {
    try {
        var info = eval("(" + res + ")");
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                var rows = info.rows * 5;
                var id = '';
                var ctl = true;
                var divPer = '';
                var per = new Array();
                var tabla = '<table border="0" class="tbListado centrar"><tr><td class="encabezado encabezadoFijo0">Sel.</td><td class="encabezado encabezadoFijo1">MEN&Uacute;</td><td class="encabezado">DESCRIPCI&Oacute;N</td><td class="encabezado encabezadoFijo3">PERMISOS</td></tr>';
                for (var i = 0; i < rows; i += 5) {
                    id = info.data[i];

                    divNombsPer = '<div id="nomb_' + id + '" class="puntero">';
                    divNombsPer += '<div class="linkIconoLateral botonEditar"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/agregar24x24.png"><p>Agregar</p></div>';
                    divNombsPer += '</div>';

                    divPer = '<div id="div' + id + '" name="divPermisos" class="ocultar"><form name="form' + id + '" id="form' + id + '"><table>';
                    per = info.data[i + 4].split(',');
                    divPer += '<tr><td class="encabezado" colspan="2"><input type="checkbox" onclick="selTodosPermisos(this.checked, \'' + id + '\')" id="todos' + id + '"> Todos</td></tr>';
                    for (var j = 0; j < per.length; j++)
                        divPer += '<tr><td><input type="checkbox" onclick="quitaPermiso(this.checked, \'' + per[j] + '' + id + '\',\'' + id + '\')" name="cbPermiso" id="' + per[j] + '' + id + '"></td><td>' + per[j] + '</td></tr>';

                    divPer += '</table></form></div>';
                    idMenus.push(id);
                    if (ctl) {
                        tabla += '<tr><td class="cuerpoListado2_1" align="center"><input type="checkbox" name="cbPermiso" id="' + id + '" onclick="muestraPermisos(this.checked, this.id)"  /></td><td class="cuerpoListado6_1">' + info.data[i + 1] + '</td><td class="cuerpoListado10_1">' + info.data[i + 2] + '</td><td class="cuerpoListado5_1" onclick="divConChecks(\'' + id + '\')">' + divNombsPer + divPer + '</td></tr>';
                    }
                    else {
                        tabla += '<tr><td class="cuerpoListado4_1" align="center"><input type="checkbox" name="cbPermiso" id="' + id + '" onclick="muestraPermisos(this.checked, this.id)"  /></td><td class="cuerpoListado8_1">' + info.data[i + 1] + '</td><td class="cuerpoListado12_1">' + info.data[i + 2] + '</td><td class="cuerpoListado7_1" onclick="divConChecks(\'' + id + '\')">' + divNombsPer + divPer + '</td></tr>';
                    }
                    ctl = !ctl;
                }
                tabla += '</table>';
                document.getElementById('men_cat_' + categoriaCheck).innerHTML += tabla;
                document.getElementById('men_cat_' + categoriaCheck).style.display = 'block';
                buscaRol2(rolGlobal);
                break;
        }
    } catch (elError) {
    }
}

/***********************************************************
METODO QUE PERMITE CARGAR MENU
***********************************************************/
function cargaMenusDisponiblesRol() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargaMenusDisponibles'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlRol.aspx', send, caragMenusDisponiblesRol_processResponse);
}

function caragMenusDisponiblesRol_processResponse(res) {
    try {
        var info = eval("(" + res + ")");
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                var rows = info.rows * 5;
                var id = '';
                var ctl = true;
                var divPer = '';
                var per = new Array();
                var tabla = '<table border="0" class="tbListado centrar"><tr><td class="encabezado">Todos<input type="checkbox" id="cbTodos" onclick="selTodos(this.checked)" /></td><td class="encabezado">MEN&Uacute;</td><td class="encabezado">DESCRIPCI&Oacute;N</td><td class="encabezado">PERMISOS</td></tr>';
                for (var i = 0; i < rows; i += 5) {
                    id = info.data[i];
                    divNombsPer = '<div id="nomb_' + id + '" class="ocultar puntero">';
                    divPer = '<div id="div' + id + '" name="divPermisos" class="ocultar"><form name="form' + id + '" id="form' + id + '"><table>';
                    per = info.data[i + 4].split(',');
                    divPer += '<tr><td class="encabezado" colspan="2"><input type="checkbox" checked="checked" onclick="selTodosPermisos(this.checked, \'' + id + '\')" id="todos' + id + '"> Todos</td></tr>';
                    for (var j = 0; j < per.length; j++) {
                        divPer += '<tr><td><input type="checkbox" name="cbPermiso" checked="checked" id="' + per[j] + '' + id + '"></td><td>' + per[j] + '</td></tr>';
                        if (j != (per.length - 1))
                            divNombsPer += per[j] + ', ';
                        else
                            divNombsPer += per[j];
                    }
                    divNombsPer += '</div>'
                    divPer += '</table></form></div>';
                    idMenus.push(id);
                    if (ctl) {
                        tabla += '<tr><td class="cuerpoListado2" align="center"><input type="checkbox" name="cbPermiso" id="' + id + '" onclick="muestraPermisos(this.checked, this.id)"  /></td><td class="cuerpoListado6">' + info.data[i + 1] + '</td><td class="cuerpoListado10">' + info.data[i + 2] + '</td><td class="cuerpoListado5" onclick="divConChecks(\'' + id + '\')">' + divNombsPer + divPer + '</td></tr>';
                    }
                    else {
                        tabla += '<tr><td class="cuerpoListado4" align="center"><input type="checkbox" name="cbPermiso" id="' + id + '" onclick="muestraPermisos(this.checked, this.id)"  /></td><td class="cuerpoListado8">' + info.data[i + 1] + '</td><td class="cuerpoListado12">' + info.data[i + 2] + '</td><td class="cuerpoListado7" onclick="divConChecks(\'' + id + '\')">' + divNombsPer + divPer + '</td></tr>';
                    }
                    ctl = !ctl;
                }
                tabla += '</table>';
                document.getElementById('menusDisponibles').innerHTML += tabla;
                break;
        }
    } catch (elError) {
    }
}

function divConChecks(idDiv) {
    document.getElementById('nomb_' + idDiv).style.display = 'none';
    document.getElementById('div' + idDiv).style.display = 'block';
    document.getElementById(idDiv).checked = true;
}

function selTodos(bool) {
    var id = '';
    if (bool) {
        $('div[name="divPermisos"]').css({ 'display': 'block' });
        $('input[name="cbPermiso"]').attr('checked', true);
    } else {
        $('div[name="divPermisos"]').css({ 'display': 'none' });
        $('input[name="cbPermiso"]').attr('checked', false);
    }
}


/***********************************************************
METODO QUE PERMITE SELECCIONAR Y DESELECCIONAR LOS PERMISOS
***********************************************************/
function selTodosPermisos(boolCB, id) {
    var objForm = document.getElementById('form' + id);
    if (boolCB) {
        for (var j = 0; j < objForm.elements.length; j++) {
            if (objForm.elements[j].type == "checkbox") {
                objForm.elements[j].checked = true;
            }
        }
    } else {
        var sel = '';
        for (var j = 0; j < objForm.elements.length; j++) {
            if (objForm.elements[j].type == "checkbox") {
                objForm.elements[j].checked = false;
                var e = objForm.elements[j];

                if (!(e.checked) && (e.id != 'todos' + id)) {
                    if (sel == '')
                        sel += id + ';' + e.id.substring(0, (e.id.length - id.length));
                    else
                        sel += ',' + e.id.substring(0, (e.id.length - id.length));
                }
            }
        }
        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', 'quitaPermisos'));
        arrayParameters.push(newArg('rol', rolGlobal));
        arrayParameters.push(newArg('menus', sel));
        arrayParameters.push(newArg('tipo', 0));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlRol.aspx', send, quitaPermisosRol_processResponse);
    }
}

/***********************************************************
QUITA UN PERMISO ESPECÍFICO
***********************************************************/
function quitaPermiso(boolCB, idObj, id) {
    if (!boolCB) {
        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', 'quitaPermisos'));
        arrayParameters.push(newArg('rol', rolGlobal));

        var sel = id + ';' + idObj.substring(0, (idObj.length - id.length)); ;

        arrayParameters.push(newArg('menus', sel));
        arrayParameters.push(newArg('tipo', 1));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlRol.aspx', send, quitaPermisosRol_processResponse);
    }
}

function quitaPermisosRol_processResponse(res) {
    try {
        var info = eval("(" + res + ")");
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
              //  muestraVentana('No se pudieron quitar los permisos sobre el menu del rol');
                break;
            case 1:
                break;
        }
    } catch (elError) {
    }
}

/***********************************************************
METODO QUE PERMITE MOSTRAR/OCULTA EL DIV CONTENEDOR DE LOS PERMISOS DE ADMINISTRACION CORRESPONDIENTE AL CHECKBOX SELECCIONADO
***********************************************************/
function muestraPermisos(boolCB, idDiv) {
    if (boolCB) {
        document.getElementById('div' + idDiv).style.display = 'block';
        document.getElementById('nomb_' + idDiv).style.display = 'none';
    }
    else {
        document.getElementById('div' + idDiv).style.display = 'none';
        document.getElementById('nomb_' + idDiv).style.display = 'block';
        selTodosPermisos(false, idDiv);
    }
    var objForm = objForm = document.getElementById('form' + idDiv);
    for (var j = 0; j < objForm.elements.length; j++) {
        if (objForm.elements[j].type == "checkbox") {
            objForm.elements[j].checked = false;
        }
    }
}


/***********************************************************
METODO QUE PERMITE BUSCAR ROL
***********************************************************/
function buscaRol(sel) {
    if ($('#selRol').val() != '-1') {
        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', 'buscar'));
        arrayParameters.push(newArg('rol', sel.value));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlRol.aspx', send, buscaRol_processResponse);
    } else {
        limpiar();
    }
}


function buscaRol_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + quitarEnter(res) + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                limpiar();                
                break;
            case 1:
                limpiar();
                var tam = info.data.length;
                var menusPermisos = null;
                var id = null;

                for (var i = 0; i < tam; i += 5) {
                    menusPermisos = info.data[i + 4].split(",");
                    id = info.data[i + 3];
                    tamPermisos = menusPermisos.length;
                    document.getElementById(id).checked = true;
                    document.getElementById('div' + id).style.display = 'block';
                    for (var j = 0; j < tamPermisos; j++) {
                        document.getElementById(menusPermisos[j] + id).checked = true;
                    }
                }
                break
        }
    } catch (elError) {
    }
}


function limpiar() {
    selTodos(false);
}


/***********************************************************
METODO QUE PERMITE GUARDAR LOS PERMISOS DE LOS ROLES
***********************************************************/
function guardaPermisosRol() {
    //var rol = document.getElementById('selRol').value;
    var rol = rolGlobal;
    if ((rol != '-1')) {
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
            //muestraVentanaProgreso();
            if (sel.length <= 1200) {
                var arrayParameters = new Array();
                arrayParameters.push(newArg('p', 'guardar'));
                arrayParameters.push(newArg('rol', rol));
                arrayParameters.push(newArg('menus', sel));
                arrayParameters.push(newArg('ctl', '0'));
                var send = arrayParameters.join('&');
                $.post('../../Controlador/ctlPermisosRol.aspx', send, guardaPermisosRol_processResponse);
            } else {
                var numPartes = sel.length % 1200
                if (numPartes > 0) {
                    numPartes = Math.ceil(sel.length / 1200);
                }
                else {
                    numPartes = sel.length / 1200;
                }
                enviarMenusPermisos();
                var selAux = '';
                function enviarMenusPermisos() {
                    var send = '';
                    var arrayParameters = new Array();
                    if (numEnvio == (numPartes - 1)) {
                        selAux = sel.substring((numEnvio * 1200), (sel.length));
                        arrayParameters.push(newArg('p', 'guardar'));
                        arrayParameters.push(newArg('rol', rol));
                        arrayParameters.push(newArg('menus', selAux));
                        arrayParameters.push(newArg('ctl', '2'));
                        send = arrayParameters.join('&');
                        $.post('../../Controlador/ctlPermisosRol.aspx', send, guardaPermisosRol_processResponse);
                    } else {
                        selAux = sel.substring((numEnvio * 1200), ((numEnvio + 1) * 1200));
                        arrayParameters.push(newArg('p', 'guardar'));
                        arrayParameters.push(newArg('menus', selAux));
                        arrayParameters.push(newArg('ctl', '1'));
                        send = arrayParameters.join('&');
                        $.post('../../Controlador/ctlPermisosRol.aspx', send, enviarMenusPermisos_processResponse);
                    }
                }

                function enviarMenusPermisos_processResponse(res) {
                    numEnvio++;
                    enviarMenusPermisos();
                }
            }
        } else {
            muestraVentana('Debe elegir al menos un men&uacute; con opciones.');
            return false;
        }
    } else {
        muestraVentana(mensajeObligatorio);
    }
}


function guardaPermisosRol_processResponse(res) {
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
                muestraVentana(mensajeEdita);
                cargaCategoriasMenus();
                break;
        }
    } catch (elError) {
    }
}

function cancelarFormPermisos() {
    $.fancybox.close();
}

function permisosRol(id) {
    rolGlobal = id;
    cargaCategoriasMenus();
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'scrolling': 'no',
        'href': '#permisosRol'
    });
}

function limpiarFormPermisos() {
    idMenus = new Array();
    rolGlobal = '';
    categoriaCheck = '';
    limpiar();
    cargaCategoriasMenus();
}

function selectRol(rol) {
    limpiarFormPermisos();
    rolGlobal = rol.value;
}

function buscaRol2(id) {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'buscaCategoria'));
    arrayParameters.push(newArg('rol', id));
    arrayParameters.push(newArg('categoria', categoriaCheck));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlRol.aspx', send, buscaRol2_processResponse);
}

function buscaRol2_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + quitarEnter(res) + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                //No encontró permisos para el rol en el menú específicos                        
                break;
            case 1:
                var tam = info.data.length;
                var menusPermisos = null;
                var id = null;
                var nombPermisos = '';

                for (var i = 0; i < tam; i += 5) {
                    menusPermisos = info.data[i + 4].split(",");
                    id = info.data[i + 3];
                    tamPermisos = menusPermisos.length;
                    document.getElementById(id).checked = true;
                    if (document.getElementById('div' + id).style.display == 'none')
                        document.getElementById('nomb_' + id).style.display = 'block';
                    //document.getElementById('div' + id).style.display = 'block';
                    nombPermisos = '';

                    for (var j = 0; j < tamPermisos; j++) {
                        document.getElementById(menusPermisos[j] + id).checked = true;
                        if (j != (tamPermisos - 1))
                            nombPermisos += menusPermisos[j] + ', ';
                        else
                            nombPermisos += menusPermisos[j];
                    }
                    document.getElementById('nomb_' + id).innerHTML = nombPermisos;
                }
                break;
        }
    } catch (elError) {
    }
}