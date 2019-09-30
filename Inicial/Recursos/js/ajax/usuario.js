///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////  VARIABLES GLOBALES   ///////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
var usuario = '' /* USUARIO QUE SE ESTA GUARDANDO, BUSCANDO, ELIMINANDO O MODIFICANDO */
var idMenus = new Array() /* PARA GUARDAR LOS ID DE LOS MENUS QUE LLEGAN DE LA BASE DE DATOS */
var idRoles = new Array() /* PARA GUARDAR LOS ID DE LOS ROLES QUE LLEGAN DE LA BASE DE DATOS */
var rolesBuscados = new Array()
var caracter = new Array("0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z")
var editar = false;
var nombreGlobal;
var mailGlobal;
var estadoGlobal;
var globalUsuario = '';
var deptoGlobal = '-1';
var areaGlobal = '-1';
var cargoGlobal = '-1';
var grupoGlobal = '-1';
var rolGlobal = '-1';

var vectorAplicaciones = new Array();
var grupoArrayGlobal = new Array();
var grupoDetalleArrayGlobal = new Array();

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////  FUNCIONES JQUERY   ///////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
$(document).ready(function () {
    cargaMenus();
    cargaDeptoEmpresa();
    cargaGrupo();
    listarUsuario(1);
    cargaRoles();
    // cargaArea();
    // cargaCargo();

});
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////// FUNCION PARA EL SELECT MULTIPLE DE GRUPO   //////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
function seleccionMultiple() {
    var config = {
        '.chosen-select': {},
        '.chosen-select-deselect': { allow_single_deselect: true },
        '.chosen-select-no-single': { disable_search_threshold: 10 },
        '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
        '.chosen-select-width': { width: "95%" }
    }

    for (var selector in config) {
        $(selector).chosen(config[selector]);
    }

    //    var el = document.getElementById('selCiudades_chosen');
    //    if (el) {
    //        el.className += el.className ? ' obligatorio' : 'obligatorio';
    //    }
}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////CARGA MENUS DISPONIBLES/////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
function cargaMenus() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargarMenuUsu'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlUsuarios.aspx', send, cargaMenus_processResponse);
}

function cargaMenus_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -2:
                muestraVentana(mensajemenosuno);
                break;

            case 0:
                muestraVentana(mensajeSinInformacion);
                break;

            case 1:
                var rows = info.rows * 5;
                var id = '';
                var ctl = true;
                var divPer = '';
                var per = new Array();
                var tabla = '<table border="0" class="tbListado centrar"><tr><td class="encabezado">Todos<input type="checkbox" id="cbTodos" onclick="selTodos(this.checked)" /></td><td class="encabezado">MENU</td><td class="encabezado">DESCRIPCION</td><td class="encabezado">PERMISOS</td></tr>';
                for (var i = 0; i < rows; i += 5) {
                    id = info.data[i];
                    divPer = '<div id="div' + id + '" class="ocultar"><form name="form' + id + '" id="form' + id + '"><table>';
                    per = info.data[i + 4].split(',');
                    divPer += '<tr><td class="encabezado" colspan="2">Todos &nbsp;&nbsp; <input type="checkbox" checked="checked" onclick="selTodosPermisos(this.checked, \'' + id + '\')" id="todos' + id + '"></td></tr>';
                    for (var j = 0; j < per.length; j++) {
                        divPer += '<tr><td>' + per[j] + '</td><td><input type="checkbox" id="' + per[j] + '' + id + '"></td></tr>';
                    }
                    divPer += '</table></form></div>';
                    //idMenus.push(id)
                    if (ctl) {
                        tabla += '<tr><td class="cuerpoListado2" align="center"><input type="checkbox" id="' + id + '" onclick="muestraPermisos(this.checked, this.id)"  /></td><td class="cuerpoListado10">' + info.data[i + 1] + '</td><td class="cuerpoListado10">' + info.data[i + 2] + '</td><td class="cuerpoListado6">' + divPer + '</td></tr>';
                    } else {
                        tabla += '<tr><td class="cuerpoListado4" align="center"><input type="checkbox" id="' + id + '" onclick="muestraPermisos(this.checked, this.id)"  /></td><td class="cuerpoListado12">' + info.data[i + 1] + '</td><td class="cuerpoListado12">' + info.data[i + 2] + '</td><td class="cuerpoListado8">' + divPer + '</td></tr>';
                    }
                    ctl = !ctl;
                }
                tabla += '</table>';
                document.getElementById('menusDisponibles').innerHTML = tabla;
                break
        }
    } catch (elError) {
    }
}

/*
GUARDA EL USUARIO SE USA EDITAR PARA EL PROCEDIMIENTO ALMACENADO
GUARDA USUARIO
*/
function guardaUsuario() {
    if (editar != true) {
        var email = document.getElementById('txtCorreo').value
        if (vemail(email)) {
            var clave = generarClave();
            var nombreUsuario = document.getElementById('txtNombreUsuario').value;
            var documento = document.getElementById('txtDocumento').value;
            var telefono = document.getElementById('txtTelefono').value;
            var direccion = document.getElementById('txtDireccion').value;
            var departamento = document.getElementById('SelDepto').value;
            var area = document.getElementById('selArea').value;
            var cargo = document.getElementById('selCargo').value;
            //var grupo = document.getElementById('selGrupo').value;
            var grupo = $("#selGrupo").chosen().val();

            var estado = document.getElementById('chkEstadoUsuario').checked;

            if (estado == true) {
                estado = 2;
            } else {
                estado = 3;
            }

            var selRoles = '';
            if ($("input[name='rolAsignado']:checked").attr('id') != null) {
                selRoles = $("input[name='rolAsignado']:checked").attr('id').substring(3);
            }

            document.getElementById("txtUsuario").value = email;
            var usuario = document.getElementById('txtUsuario').value;

            if (grupo == null) {
                grupo = '';
            }

            if ((usuario != '') && (email != '') && (nombreUsuario != '') && (selRoles != '') && (documento != '') && (departamento != '-1') && (area != '-1') && (cargo != '-1')) {
                var arrayParameters = new Array();
                arrayParameters.push(newArg('p', 'guardarUsu'));
                arrayParameters.push(newArg('edita', 'false'));
                arrayParameters.push(newArg('usuario', usuario));
                arrayParameters.push(newArg('nombreUsuario', nombreUsuario));
                arrayParameters.push(newArg('documento', documento));
                arrayParameters.push(newArg('clave', clave));
                arrayParameters.push(newArg('email', email));
                arrayParameters.push(newArg('telefono', telefono));
                arrayParameters.push(newArg('direccion', direccion));
                arrayParameters.push(newArg('departamento', departamento));
                arrayParameters.push(newArg('area', area));
                arrayParameters.push(newArg('cargo', cargo));
                arrayParameters.push(newArg('grupo', grupo));
                arrayParameters.push(newArg('estado', estado));
                arrayParameters.push(newArg('rol', selRoles));
                var send = arrayParameters.join('&');

                muestraVentanaProgreso();

                $.post('../../Controlador/ctlUsuarios.aspx', send, guardaUsuario_processResponse);
            }
            else {
                muestraVentana(mensajeObligatorio);
            }
        } else {
            muestraVentana('El correo electr&oacute;nico no tiene el formato correcto');
        }
    } else {
        var email = mailGlobal;
        if (vemail(email)) {
            var estado = document.getElementById('chkEstadoUsuario').checked;

            if (estado == true) {
                estado = 2;
            } else {
                estado = 3;
            }

            var usuario = document.getElementById('txtUsuario').value;
            var nombreUsuario = document.getElementById('txtNombreUsuario').value;
            var documento = document.getElementById('txtDocumento').value;
            var telefono = document.getElementById('txtTelefono').value;
            var direccion = document.getElementById('txtDireccion').value;
            var departamento = document.getElementById('SelDepto').value;
            var area = document.getElementById('selArea').value;
            var cargo = document.getElementById('selCargo').value;
            //var grupo = document.getElementById('selGrupo').value;
            var grupo = $("#selGrupo").chosen().val();

            var selRoles = '';
            if ($("input[name='rolAsignado']:checked").attr('id') != null) {
                selRoles = $("input[name='rolAsignado']:checked").attr('id').substring(3);
            }

            if (grupo == null) {
                grupo = '';
            }
            if ((usuario != '') && (email != '') && (nombreUsuario != '') && (selRoles != '') && (documento != '') && (departamento != '-1') && (area != '-1') && (cargo != '-1')) {

                var arrayParameters = new Array();
                arrayParameters.push(newArg('p', 'guardarUsu'));
                arrayParameters.push(newArg('edita', 'true'));
                arrayParameters.push(newArg('usuario', usuario));
                arrayParameters.push(newArg('nombreUsuario', nombreUsuario));
                arrayParameters.push(newArg('documento', documento));
                arrayParameters.push(newArg('clave', clave));
                arrayParameters.push(newArg('email', email));
                arrayParameters.push(newArg('telefono', telefono));
                arrayParameters.push(newArg('direccion', direccion));
                //arrayParameters.push(newArg('departamento', departamento));
                //arrayParameters.push(newArg('area', area));
                arrayParameters.push(newArg('cargo', cargo));
                arrayParameters.push(newArg('grupo', grupo));
                arrayParameters.push(newArg('estado', estado));
                arrayParameters.push(newArg('rol', selRoles));
                var send = arrayParameters.join('&');

                muestraVentanaProgreso();

                $.post('../../Controlador/ctlUsuarios.aspx', send, guardaUsuario_processResponse);

            } else {
                muestraVentana(mensajeObligatorio);
            }
        } else {
            muestraVentana('El correo electr&oacute;nico no tiene el formato correcto');
        }
    }
}

function guardaUsuario_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + res + ')')
        var msj = info.msj
        switch (msj) {
            case 0:
                muestraVentana('YA EXISTE ESTE USUARIO PARA LOGIN');
                break
            case 1:
                $.fancybox.close();
                usuario = '';
                muestraVentana(mensajeGuarda);
                limpiar();
                listarUsuario(1);
                break
            case 2:
                $.fancybox.close();
                usuario = '';
                muestraVentana(mensajeGuarda);
                listarUsuario(1);
                limpiar();
                break
            case 2:
                $.fancybox.close();
                usuario = '';
                muestraVentana('INFORMACION EDITADA CORRECTAMENTE.');
                limpiar();
                listarUsuario(1);
                break
            case 3:
                muestraVentana('USUARIO PARA LOGIN YA EXISTENTE');
                break
            case 7:
                muestraVentana('YA EXISTE UN USUARIO CON ESE DOCUMENTO  ');
                break
            default:
                muestraVentana(mensajemenosuno);
                //limpiar();
                break
        }
    } catch (elError) {
    }
}



///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////  GENERA CLAVE ALEATORIA   /////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
function generarClave() {
    var i = 0
    var clave = ''
    while (i < 10) {
        clave += caracter[(Math.round(Math.random() * 35))]
        i++
    }
    return clave;
}


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////  GENERAR NUEVA CLAVE   ////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
function generaClave() {
    var usu = document.getElementById('usuarioLogin').value
    var email = document.getElementById('email').value
    if ((usu != '') && (email != '')) {
        var arrayParameters = new Array()
        var clave = generarClave()
        arrayParameters.push(newArg('p', 'genClav'))
        arrayParameters.push(newArg('c', clave))
        arrayParameters.push(newArg('u', usu))
        arrayParameters.push(newArg('e', email))
        var send = arrayParameters.join('&')
        $.post('../../Controlador/ctlUsuarios.aspx', send, generaClave_processResponse)
    } else {
        muestraVentana(mensajeObligatorio);
    }
}

function generaClave_processResponse(res) {
    try {
        var info = eval("(" + res + ")")
        var msj = info.msj
        switch (msj) {
            case -2:
                muestraVentana(mensajemenosuno);
                break
            case 0:
                muestraVentana('Error al generar la nueva clave!!!');
                break
            case 1:
                muestraVentana('Clave generada éxitosamente !!! <br />Se ha enviado un correo de confirmaci&oacute;n');
                setTimeout("limpiar()", 3000)
                break
        }
    } catch (elError) {
    }
}


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////  ELIMINA USUARIO   /////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
function eliminar() {
    var usu = globalEliminar;

    if (usu != '') {
        var arrayParameters = new Array()
        arrayParameters.push(newArg('p', 'eliminarUsu'))
        arrayParameters.push(newArg('u', usu))
        var send = arrayParameters.join('&')
        $.post('../../Controlador/ctlUsuarios.aspx', send, eliminaUsuario_processResponse)
    } else {
        muestraVentana(mensajeDatosEliminar);
    }
}

function eliminaUsuario_processResponse(res) {
    try {
        var info = eval("(" + res + ")")
        var msj = info.msj
        switch (msj) {
            case -2:
                muestraVentana(mensajemenosuno);
                break
            case 0:
                muestraVentana(mensajeSinInformacion);
                break
            case 1:
                $.fancybox.close();
                usuario = '';
                muestraVentana(mensajeElimina);
                setTimeout("limpiar()", 3000)
                listarUsuario(1);
                break
        }
    } catch (elError) {
    }
}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////  BUSCAR USUARIO   /////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
function buscaUsuario(usu) {
    usuario = usu
    if (usuario != '') {
        var arrayParameters = new Array()
        arrayParameters.push(newArg('p', 'buscarUsu'))
        arrayParameters.push(newArg('u', usu))
        var send = arrayParameters.join('&')
        $.post('../../Controlador/ctlUsuarios.aspx', send, buscaUsuario_processResponse)
    } else {
        muestraVentana(mensajeObligatorio);
    }
}

function buscaUsuario_processResponse(res) {
    try {
        var info = eval("(" + res + ")")
        var msj = info.msj
        switch (msj) {
            case -2:
                muestraVentana(mensajemenosuno);
                break
            case 0:
                muestraVentana(mensajeSinInformacion);
                break
            case 1:
                var r = info.data[4]
                if (r != '') {
                    var tam = info.data.length
                    var id = null
                    try {
                        for (var i = 0; i < tam; i += 5) {
                            id = info.data[i + 4]
                            document.getElementById('rol' + id).checked = true
                        }
                    } catch (erro) {
                        muestraVentana('Error al cargar los roles asociados a este usuario');
                    }
                }
                break
        }
    } catch (elError) {
    }
}

/*************************************************************************************************************************
CARGA LAS CATEGORIAS DE LOS MENÚS
**************************************************************************************************************************/
function cargaCategoriasMenus(usuario) {
    muestraVentanaProgreso('Cargando menus disponibles...');
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'href': '#contenedorUsuario'
    });
    globalUsuario = usuario;
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargarCatsMenu'));
    arrayParameters.push(newArg('usuario', usuario));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlUsuarios.aspx', send, cargaCategoriasMenus_processResponse);
}

function cargaCategoriasMenus_processResponse(res) {
    var info = eval("(" + res + ")");
    var msj = info.msj;
    ocultaVentanaProgreso();
    switch (msj) {
        case -1:
            muestraVentana(mensajemenosuno);
            break;
        case 0:
            document.getElementById('menusDisponibles').innerHTML = mensajeSinInformacion;
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

                cadena += '<tr><td class="' + claseAplicar + '"><input type="checkbox" id="cat_' + id + '" onclick="cargaMenusCategoriaUsuario(\'' + id + '\')">' + categoria;
                cadena += '<div id="men_cat_' + id + '" class="ocultar"></div></td></tr>';
            }
            cadena += '</table>';
            divListado.innerHTML = cadena;
            divListado.style.display = 'block';
            break;
    }
}

var categoriaCheck = '';
/***********************************************************
METODO QUE PERMITE CARGAR MENU
***********************************************************/
function cargaMenusCategoriaUsuario(categoria) {
    categoriaCheck = categoria;
    divMenusCategoria = document.getElementById('men_cat_' + categoriaCheck);
    if (document.getElementById('cat_' + categoria).checked) {
        if (divMenusCategoria.innerHTML.length == 0) {
            var arrayParameters = new Array();
            arrayParameters.push(newArg('p', 'cargaMenusCategoria'));
            arrayParameters.push(newArg('categoria', categoriaCheck));
            var send = arrayParameters.join('&');
            $.post('../../Controlador/ctlRol.aspx', send, cargaMenusCategoriaUsuario_processResponse);
        } else {
            divMenusCategoria.style.display = 'block';
        }
    } else {
        divMenusCategoria.style.display = 'none';
    }
}

function cargaMenusCategoriaUsuario_processResponse(res) {
    try {
        var info = eval("(" + res + ")");
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajeSinInformacion);
                break;
            case 1:
                var rows = info.rows * 5;
                var id = '';
                var ctl = true;
                var divPer = '';
                var per = new Array();
                var tabla = '<table border="0" class="tbListado centrar"><tr><td class="encabezado encabezadoFijo0">Sel.</td><td class="encabezado encabezadoFijo1">MEN&Uacute;</td><td class="encabezado encabezadoFijo4">DESCRIPCI&Oacute;N</td><td class="encabezado encabezadoFijo4">PERMISOS</td></tr>';
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
                    var rol = id.toString();
                    if (ctl) {
                        tabla += '<tr><td class="cuerpoListado2_1" align="center"><input type="checkbox" name="cbPermiso" id="' + id + '" onclick="muestraPermisos(this.checked, this.id)"  /></td><td class="cuerpoListado6_1">' + info.data[i + 1] + '</td><td class="cuerpoListado10_1">' + info.data[i + 2] + '</td><td class="cuerpoListado5_1" onclick="divConChecks(\'' + id + '\')">' + divNombsPer + divPer + '</td></tr>';
                    }
                    else {
                        tabla += '<tr><td class="cuerpoListado4_1" align="center"><input type="checkbox" name="cbPermiso" id="' + id + '" onclick="muestraPermisos(this.checked, this.id)"  /></td><td class="cuerpoListado8_1">' + info.data[i + 1] + '</td><td class="cuerpoListado12_1">' + info.data[i + 2] + '</td><td class="cuerpoListado7_1" onclick="divConChecks(\'' + id + '\')">' + divNombsPer + divPer + '</td></tr>';
                    }
                    ctl = !ctl;
                    //
                }
                //divConChecks(id); 
                tabla += '</table>';
                document.getElementById('men_cat_' + categoriaCheck).innerHTML += tabla;
                document.getElementById('men_cat_' + categoriaCheck).style.display = 'block';
                buscaRol2(globalUsuario);
                break;
        }
    } catch (elError) {
    }
}

function buscaRol2(usuario) {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'buscarRolCatUsu'));
    arrayParameters.push(newArg('usuario', usuario));
    arrayParameters.push(newArg('categoria', categoriaCheck));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlUsuarios.aspx', send, buscaRol2_processResponse);
}

function buscaRol2_processResponse(res) {
    try {
        var info = eval('(' + quitarEnter(res) + ')');
        var msj = info.msj;

        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
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
                    document.getElementById(id).disabled = true;
                    if (document.getElementById('div' + id).style.display == 'none');
                    document.getElementById('nomb_' + id).style.display = 'block';
                    nombPermisos = '';

                    for (var j = 0; j < tamPermisos; j++) {
                        document.getElementById(menusPermisos[j] + id).checked = true;
                        document.getElementById(menusPermisos[j] + id).disabled = true;
                        if (j != (tamPermisos - 1))
                            nombPermisos += menusPermisos[j] + ', ';
                        else
                            nombPermisos += menusPermisos[j];
                    }
                    document.getElementById('nomb_' + id).innerHTML = nombPermisos;

                    var arrayParameters = new Array();
                    arrayParameters.push(newArg('p', 'buscarMensUsu'));
                    arrayParameters.push(newArg('usuario', globalUsuario));
                    arrayParameters.push(newArg('categoria', categoriaCheck));
                    var send = arrayParameters.join('&');
                    $.post('../../Controlador/ctlUsuarios.aspx', send, buscaMenuUsuario_processResponse);
                }
                break;
        }
    } catch (elError) {
    }
}

function buscaMenuUsuario_processResponse(res) {
    try {
        var info = eval('(' + quitarEnter(res) + ')');
        var msj = info.msj;

        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                break;
            case 1:
                var tam = info.data.length;
                var menusPermisos = null;
                var id = null;
                var permisosAct = '';

                for (var i = 0; i < tam; i += 5) {
                    menusPermisos = info.data[i + 4].split(",");
                    id = info.data[i + 3];
                    tamPermisos = menusPermisos.length;
                    document.getElementById(id).checked = true;
                    nombPermisos = '';
                    permisosAct = document.getElementById('nomb_' + id).innerHTML;

                    for (var j = 0; j < tamPermisos; j++) {
                        document.getElementById(menusPermisos[j] + id).checked = true;
                        if (permisosAct.indexOf(menusPermisos[j].toString()) == -1) {
                            if (j != (tamPermisos - 1))
                                nombPermisos += menusPermisos[j] + ', ';
                            else
                                nombPermisos += menusPermisos[j];
                        }
                    }
                    var divNombres = document.getElementById('nomb_' + id);
                    if (divNombres.innerHTML.indexOf('div') != -1)
                        divNombres.innerHTML = nombPermisos;
                    else
                        if (nombPermisos != "") {
                            divNombres.innerHTML += ', ' + nombPermisos;
                        }
                }
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
            if (objForm.elements[j].type == "checkbox" && !objForm.elements[j].disabled) {
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
        arrayParameters.push(newArg('p', 'quitarPermsUsu'));
        arrayParameters.push(newArg('usuario', globalUsuario));
        arrayParameters.push(newArg('menus', sel));
        arrayParameters.push(newArg('tipo', 0));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlUsuarios.aspx', send, quitaPermisosUsuario_processResponse);
    }
}

/***********************************************************
QUITA UN PERMISO ESPECÍFICO
***********************************************************/
function quitaPermiso(boolCB, idObj, id) {
    if (!boolCB) {
        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', 'quitarPermsUsu'));
        arrayParameters.push(newArg('usuario', globalUsuario));

        var sel = id + ';' + idObj.substring(0, (idObj.length - id.length));

        arrayParameters.push(newArg('menus', sel));
        arrayParameters.push(newArg('tipo', 1));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlUsuarios.aspx', send, quitaPermisosUsuario_processResponse);
    }
}

function quitaPermisosUsuario_processResponse(res) {
    try {
        var info = eval("(" + res + ")");
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
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

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////CARGA ROLES DISPONIBLES/////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
function cargaRoles() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargarRol'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlUsuarios.aspx', send, cargaRoles_processResponse);
}

function cargaRoles_processResponse(res) {
    try {
        var info = eval("(" + res + ")");
        var msj = info.msj;
        switch (msj) {
            case -2:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                document.getElementById('rolesDisponibles').innerHTML = mensajeSinInformacion;
                break;
            case 1:
                var rows = info.rows * 3;
                var id = '';
                var ctl = true;
                var divPer = '';
                var per = new Array();
                var tabla = '<table border="0" class="tbListado centrar"><tr><td class="encabezado">Sel.</td><td class="encabezado">ROL</td><td class="encabezado">DESCRIPCI&Oacute;N</td></tr>';
                for (var i = 0; i < rows; i += 3) {
                    id = info.data[i];
                    idRoles.push(id);

                    if (ctl)
                        tabla += '<tr><td class="cuerpoListado1" align="center"><input type="radio" name="rolAsignado" value="' + id + '" id="rol' + id + '" onclick="validaRol(\'' + id + '\');"></td><td class="cuerpoListado9">' + info.data[i + 1] + '</td><td class="cuerpoListado9">' + info.data[i + 2] + '</td></tr>';
                    else
                        tabla += '<tr><td class="cuerpoListado2" align="center"><input type="radio" name="rolAsignado" value="' + id + '" id="rol' + id + '" onclick="validaRol(\'' + id + '\');"></td><td class="cuerpoListado10">' + info.data[i + 1] + '</td><td class="cuerpoListado10">' + info.data[i + 2] + '</td></tr>';
                    ctl = !ctl;
                }
                tabla += '</table>';
                document.getElementById('rolesDisponibles').innerHTML = tabla;
                break
        }
    } catch (elError) {
    }
}



function validaRol(id) {
    rolGlobal = id;
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'validaRol'));
    arrayParameters.push(newArg('id', id));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlUsuarios.aspx', send, validaRol_processResponse);
}

function validaRol_processResponse(res) {
    try {
        var info = eval('(' + quitarEnter(res) + ')');

        switch (info) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                break;
            case 2:
                muestraVentana("ESTE ROL NO TIENE PERMISOS ASOCIADOS");
                document.getElementById('rol' + rolGlobal).checked = false;
                break;
        }
    } catch (elError) {
    }
}


////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////// Evalua si la opcion de seleccionar todos se activo o no para hacer la respectiva selección //////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
function selTodosRoles(bool) {
    var id = '';
    if (bool) {
        for (var i = 0; i < idRoles.length; i++) {
            id = idRoles[i];
            document.getElementById('rol' + id).checked = true;
        }
    } else {
        for (var j = 0; j < idRoles.length; j++) {
            id = idRoles[j];
            document.getElementById('rol' + id).checked = false;
        }
    }
}


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////  LIMPIAR LISTADO  /////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
function limpiarListado() {
    document.getElementById('listadoUsuario').innerHTML = "";
}

function verTodos() {
    limpiarFiltro();
    listarUsuario(1);
}

function limpiarFiltro() {
    document.getElementById('txtNombreUsuarioFiltro').value = '';
    document.getElementById('txtUsuarioFiltro').value = '';
    document.getElementById('txtCorreoFiltro').value = '';
}

function abrirFiltro() {
    limpiarFiltro();
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'scrolling': 'no',
        'href': '#divFiltro'
    });
}

/* ********************************************************************************************************************************
LISTA LOS USUARIOS EXISTENTES
******************************************************************************************************************************** */
function listarUsuario(pag) {
    var arrayParameters = new Array();
    var nombre = document.getElementById('txtNombreUsuarioFiltro').value;
    var usuario = document.getElementById('txtUsuarioFiltro').value;
    var correo = document.getElementById('txtCorreoFiltro').value;
    cancelaFormRol();

    arrayParameters.push(newArg('p', 'listaGrupoUsuario'));
    arrayParameters.push(newArg('pag', pag));
    arrayParameters.push(newArg('nombre', nombre));
    arrayParameters.push(newArg('usuario', usuario));
    arrayParameters.push(newArg('correo', correo));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlPaginador.aspx', send, listarUsuario_processResponse);
}

function listarUsuario_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var divListado = document.getElementById('divListadoUsuarios');

        if (res != '0') {
            var dataRows = info.data;
            var l = dataRows.length;
            var cols = info.cols;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";
            var id = "";
            var rol = "";
            var desc = "";
            var claseAplicar = "";
            tabla = "<table class='tbListado centrar' style='text-align: center;'><tr><td class='encabezado' colspan='7'>USUARIOS EXISTENTES</td></tr>";
            tabla += "<tr><td class='encabezado'style='width:26%'>USUARIO PARA EL SISTEMA</td><td class='encabezado'style='width:26%'>NOMBRE DEL USUARIO</td>";
            tabla += "<td class='encabezado'>PERMISOS</td><td class='encabezado'>DETALLE</td><td class='encabezado'>EDITAR</td><td class='encabezado'>ELIMINAR</td></tr>";

            for (var i = 0; i < l; i += cols) {
                usu_usuario = dataRows[i];
                usu_nombre = dataRows[i + 1];
                usu_documento = dataRows[i + 2];
                usu_mail = dataRows[i + 4];
                usu_telefono = dataRows[i + 5];
                usu_direccion = dataRows[i + 6];
                usu_depto = dataRows[i + 7];
                usu_area = dataRows[i + 8];
                usu_cargo = dataRows[i + 9];
                usu_grupo = dataRows[i + 18];
                usu_estado = dataRows[i + 13];
                usu_foto = dataRows[i + 10];
                id_depto = dataRows[i + 17];
                id_area = dataRows[i + 16];
                id_cargo = dataRows[i + 15];
                id_grupo = dataRows[i + 14];

                rol = dataRows[i + 19];

                if (usu_estado != "2") {// 2 es estado activo
                    usu_estado = 0;
                } else {
                    usu_estado = 1;
                }
                if (ctl) {
                    claseAplicar = "cuerpoListado7";
                    claseAplicar1 = "cuerpoListado1";
                }
                else {
                    claseAplicar = "cuerpoListado8";
                    claseAplicar1 = "cuerpoListado2";
                }
                ctl = !ctl;

                tabla += '<tr><td class="' + claseAplicar + '">' + usu_usuario + '</td><td class="' + claseAplicar + '">' + usu_nombre + '</td>';

                tabla += '<td class="' + claseAplicar1 + '"><div id="imgPermisos" class="linkIconoLateral botonPermisos " onclick="cargaCategoriasMenus(\'' + usu_usuario + '\');"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/aceptar.png"><p>Permisos</p></div></td>';
                tabla += '<td class="' + claseAplicar1 + '"><div id="imgDetalle" class="linkIconoLateral botonDetalle" onclick="detalle(\'' + usu_usuario + '\',\'' + usu_nombre + '\',\'' + usu_mail + '\');"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/listar.png"><p>Detalle</p></div></td>';
                tabla += '<td class="' + claseAplicar1 + '"><div id="imgEditar" class="linkIconoLateral botonEditar" onclick="edita(\'' + usu_usuario + '\',\'' + usu_nombre + '\',\'' + usu_documento + '\',\'' + usu_mail + '\',\'' + usu_telefono + '\',\'' + usu_direccion + '\',\'' + id_depto + '\',\'' + id_area + '\',\'' + id_cargo + '\',\'' + id_grupo + '\',\'' + usu_estado + '\',\'' + usu_foto + '\',\'' + rol + '\');"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/editar_24x24.png"><p>Editar</p></div></td>';
                tabla += '<td class="' + claseAplicar1 + '"><div id="imgEliminar" class="linkIconoLateral botonEliminar" onclick="confirmaEliminar(\'' + usu_usuario + '\');"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/eliminar_24x24.png"> <p>Eliminar</p></div>';
                tabla += '</td></tr>';
            }
            divListado.innerHTML = tabla;
            divListado.innerHTML += pieDePaginaListar(info, 'listarUsuario'); /*llama de nuevo el paginar con la nueva pag*/
            var idMenuForm = document.getElementById('idMenuForm').innerHTML; /* se adiciona esta linea para que se el permiso de visualizar el editar despues de cargar*/
            permisosParaMenu(idMenuForm); /* se adiciona esta linea para que se el permiso de visualizar el editar despues de cargar*/
        } else {
            divListado.innerHTML = mensajecero;
        }
    } catch (elError) {
    }
}

var globalUsuario;
/************************************************************************************************************/
/************************************************************************************************************/
/************************************************************************************************************/
function ventanaVariables(usu_usuario) {
    globalUsuario = usu_usuario;
    listaVariables();
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'scrolling': 'no',
        'href': '#divAcceso',
        'onClosed': function () {
        }
    });
}

function listaVariables() {
    var divListado = document.getElementById('divListadoVariables');
    var ctl = true, claseAplicar = "", claseAplicar1 = "";

    //Inicio de la tabla
    var cadena = '<table width="100%">';
    //Cabecera
    cadena += '<tr><td class="encabezado">VARIABLES</td><td class="encabezado">RESTRICCI&Oacute;N</td></tr>';
    var claseAplicar = "cuerpoListado7";
    var claseAplicar2 = "cuerpoListado3";

    //Cuerpo
    cadena += '<tr>';
    cadena += '<td class="' + claseAplicar + '">EMPRESAS</td>';
    cadena += '<td class="' + claseAplicar2 + '" onclick="ventanaRestricciones(1)">';
    cadena += '<div class="linkIconoLateral botonEditar"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/variables24x24.png"><p>Restricci&oacute;n</p></div>';
    cadena += '</td>';
    cadena += '</tr>';
    //Fin

    //Cuerpo
    //cadena += '<tr>';
    //cadena += '<td class="' + claseAplicar + '">APLICACIONES</td>';
    //cadena += '<td class="' + claseAplicar2 + '" onclick="ventanaAplicaciones(1)">';
    //cadena += '<div class="linkIconoLateral botonEditar"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/variables24x24.png"><p>Restricci&oacute;n</p></div>';
    //cadena += '</td>';
    //cadena += '</tr>';
    //Fin

    cadena += '</table>';

    divListado.innerHTML = cadena;
}


function ventanaRestricciones(flag) {
    listarEmpresasSeleccion();
    /* LLAMAMOS EL DIV PARA ABRIR EL DETALLE*/
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'href': '#divEmpresasVariablesAcceso',
        'onClosed': function () {
        }
    });
}

function listarEmpresasSeleccion() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listarEmpresasSeleccion'));
    arrayParameters.push(newArg('usuario', globalUsuario));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlUsuarios.aspx', send, listarEmpresasSeleccion_processResponse);
}

function listarEmpresasSeleccion_processResponse(res) {
    var info = eval('(' + res + ')');
    var divTerceros = document.getElementById('listadoEmpresaVariablesAcceso');
    if (info.msj != '0') {
        var datosRows = info.data;
        var l = info.cols;
        var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";

        var nit = "";
        var nombre = "";
        var estado = "";

        var tabla = "<table class='tbListado centrar' style='text-align: center;'><tr><td class='encabezado' colspan='10'>SELECCIONE LAS EMPRESAS</td></tr>";
        tabla += "<tr><td class='encabezado'>NIT</td><td class='encabezado'>EMPRESA</td><td class='encabezado'>SELECCIONE</td></tr>";

        for (var i = 0; i < datosRows.length; i += l) {

            nit = datosRows[i]
            nombre = datosRows[i + 1];
            estado = datosRows[i + 2];

            if (ctl) {
                claseAplicar = "cuerpoListado9";
                claseAplicar2 = "cuerpoListado3";
            } else {
                claseAplicar = "cuerpoListado10";
                claseAplicar2 = "cuerpoListado5";
            }

            ctl = !ctl;
            tabla += '<tr>';
            tabla += '<td class="' + claseAplicar + '" align="center">' + nit + '</td>';
            tabla += '<td class="' + claseAplicar + '" align="center">' + nombre + '</td>';
            if (parseInt(estado) == 2) {
                tabla += '<td class="' + claseAplicar + '" align="center"><input id="chk' + i + '" checked type="checkbox" onclick="asignarEmpresa(\'' + globalUsuario + '\',\'' + nit + '\',this.id);"/></td>';
            }
            else {
                tabla += '<td class="' + claseAplicar + '" align="center"><input id="chk' + i + '" type="checkbox" onclick="asignarEmpresa(\'' + globalUsuario + '\',\'' + nit + '\',this.id);"/></td>';
            }
            tabla += '</tr>';
        }
        tabla += '</table>';
        divTerceros.innerHTML = tabla;
    } else {
        divTerceros.innerHTML = mensajecero;
    }

}

function asignarEmpresa(usuario, nit, idChk) {

    var estadoCheck = document.getElementById(idChk).checked;
    var estado = "";

    if (estadoCheck)
        estado = 1;
    else
        estado = 0;

    var arrayParameters = new Array();

    arrayParameters.push(newArg('p', 'asignarEmpresaUsuario'));
    arrayParameters.push(newArg('usuario', usuario));
    arrayParameters.push(newArg('nit', nit));
    arrayParameters.push(newArg('estado', estado));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlUsuarios.aspx', send, asignarEmpresa_processResponse);
}
function asignarEmpresa_processResponse(res) {
    try {
        var info = eval('(' + res + ')');

        switch (info) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 1:
                muestraVentana('Informaci&oacute;n almacenada correctamente');
                listarEmpresasSeleccion();
                break;
        }
    } catch (elError) {
    }
}


function ventanaAplicaciones(flag) {
    //listarAplicacionesEmpresas();
    /* LLAMAMOS EL DIV PARA ABRIR EL DETALLE*/
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'href': '#divAplicacionesEmpresa',
        'onClosed': function () {
        }
    });
}
/************************************************************************************************************/
/************************************************************************************************************/
/************************************************************************************************************/
/************************************************************************************************************/
/*
ABRE EL FANCY PARA CREAR NUEVO USUARIO
Y CREA EL IFRAME PARA LA FOTO
*/
function nuevoUsuario() {

    cargaDeptoEmpresa();
    cargaGrupo();
    globalUsuario = -1;
    deptoGlobal = '-1';
    areaGlobal = '-1';
    cargoGlobal = '-1';
    grupoGlobal = '-1';

    var arrayparameters = new Array();
    arrayparameters.push(newArg('p', 'cargaEditarFotoDefecto'));
    var send = arrayparameters.join('&');
    $.post('../../Controlador/ctlUsuarios.aspx', send, "");
    setTimeout("cambiarValor();", 400)

    document.getElementById('txtUsuario').disabled = false;
    editar = false;
    limpiar();
    document.getElementById('txtCorreo').disabled = false;
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'href': '#divNuevoUsuario',
        'onClosed': function () {
            limpiar();
        }
    });
}

function edita(usu_usuario, usu_nombre, usu_documento, usu_mail, usu_telefono, usu_direccion, usu_dpto_emp, usu_area, usu_cargo, usu_grupo, usu_estado, usu_foto, rol) {
    globalUsuario = usu_usuario;

    var arrayparameters = new Array();
    arrayparameters.push(newArg('p', 'cargaEditarFoto'));
    arrayparameters.push(newArg('usu_usuario', usu_usuario));
    var send = arrayparameters.join('&');
    $.post('../../Controlador/ctlUsuarios.aspx', send, '');

    usuario = usu_usuario;
    nombreGlobal = usu_nombre;
    mailGlobal = usu_mail;
    estadoGlobal = usu_estado;
    editar = true;
    deptoGlobal = usu_dpto_emp;
    areaGlobal = usu_area;
    cargoGlobal = usu_cargo;
    cargaDeptoEmpresa();

    grupoGlobal = usu_grupo;


    document.getElementById('txtUsuario').value = usuario;
    document.getElementById('txtUsuario').disabled = true;
    document.getElementById('txtNombreUsuario').value = nombreGlobal;
    document.getElementById('txtCorreo').value = mailGlobal;
    document.getElementById('txtCorreo').disabled = true;
    document.getElementById('txtDocumento').value = usu_documento;
    document.getElementById('txtTelefono').value = usu_telefono;
    document.getElementById('txtDireccion').value = usu_direccion;

    //buscaUsuario(usuario);

    setTimeout("cambiarValor()", 3300); // SE LE ASIGAN ESTE VALOR DE ESPERA PARA QUE PUEDA CARGAR LA IMÁGEN
    // setTimeout("document.getElementById('SelDepto').value = " + id_dpto_emp, 50);


    // setTimeout("document.getElementById('selArea').value = " + usu_area, 50); // SE ASIGNA ESTOS VALORES PARA QUE PUEDAR CARGAR LOS DATOS EN LA LISTA
    // setTimeout("document.getElementById('selCargo').value=" + usu_cargo, 50); // SE ASIGNA ESTOS VALORES PARA QUE PUEDAR CARGAR LOS DATOS EN LA LISTA QUE DEPENDEN DEL ÁREA
    //  setTimeout("document.getElementById('selGrupo').value=" + usu_grupo, 50);
    //// CHECKED NUESTRO CAMPO DE ACTIVAR USUARIO SEGUN EL VALOR ( 1 = CHECKED  //  0 = NO CHECKED)
    if (estadoGlobal != "0") {
        document.getElementById('chkEstadoUsuario').checked = true;
    } else {
        document.getElementById('chkEstadoUsuario').checked = false;
    }

    ////SE CREA UN MÉTODO PARA ABRIR EL FANCY Y SE LE DA UN TIEMPO PARA PODER CARGAR LOS VALORES QUE SE LE ENVÍAN.
    setTimeout("fancyEditar()", 100);


    grupoArrayGlobal.length = 0;
    grupoArrayGlobal = grupoGlobal.toString().split(',');

    for (var i = 0; i < grupoArrayGlobal.length; i++) {

        $("#selGrupo option[value=" + grupoArrayGlobal[i] + "]").attr("selected", true);
    }
    $('.chosen-select').trigger('chosen:updated');


    document.getElementById('rol' + rol).checked = true;

}

function fancyEditar() {

    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'href': '#divNuevoUsuario',
        'onClosed': function () {
            limpiar();
        }
    });
}


function cancelaFormRol() {
    $.fancybox.close();
    limpiar();
}

function limpiarCheckBox() {
    for (var pos = 0; pos < idMenus.length; pos++) {
        document.getElementById(idMenus.elements[pos]).checked = false;
    }
}


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////  LIMPIAR FORMULARIO   /////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
function limpiar() {
    document.getElementById('txtNombreUsuario').value = '';
    document.getElementById('txtUsuario').value = '';
    document.getElementById('txtCorreo').value = '';
    document.getElementById('chkEstadoUsuario').checked = false;
    document.getElementById('txtDocumento').value = '';
    document.getElementById('txtTelefono').value = '';
    document.getElementById('txtDireccion').value = '';
    document.getElementById('selArea').value = '-1';
    document.getElementById('selCargo').value = '-1';
    document.getElementById('selGrupo').value = '';
    editar = "false";

    limpiarListado()
    var id = null

    for (var i = 0; i < idMenus.length; i++) {
        id = idMenus[i]
        if (document.getElementById(id) != null) {
            document.getElementById(id).checked = false
            document.getElementById(id).disabled = false

            var objForm = document.getElementById('form' + id);
            for (var j = 0; j < objForm.elements.length; j++) {
                if (objForm.elements[j].type == "checkbox") {
                    objForm.elements[j].checked = false
                    objForm.elements[j].disabled = false
                }
            }
            document.getElementById('div' + id).style.display = 'none'
        }
    }

    for (var i = 0; i < idRoles.length; i++) {
        id = idRoles[i]
        document.getElementById('rol' + id).checked = false
    }
}


function editaPermisoUsuario() {
    id = '';
    var m = idMenus.length;
    var sel = '';
    var cb = null;
    for (var i = 0; i < m; i++) {
        id = idMenus[i];
        cb = document.getElementById(id);

        if ((cb.checked)) {
            var ctl = false;
            var objForm = objForm = document.getElementById('form' + id);
            for (var j = 0; j < objForm.elements.length; j++) {
                if (objForm.elements[j].type == "checkbox")
                    if ((objForm.elements[j].checked) && (!objForm.elements[j].disabled)) {
                        ctl = true;
                        j = objForm.elements.length;
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
                        if ((e.checked) && (!e.disabled) && (e.id != 'todos' + id)) {
                            sel += ',' + e.id.substring(0, (e.id.length - id.length));
                        }
                    }
                }
            }
        }
    }
    if ((sel != '')) {
        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', 'editarPermisoUsu'));
        arrayParameters.push(newArg('u', globalUsuario));
        arrayParameters.push(newArg('m', sel));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlUsuarios.aspx', send, editaPermisoUsuario_processResponse);
    } else {
        muestraVentana('Debe elegir al menos un men&uacute;!!!');
        return false;
    }
}

function editaPermisoUsuario_processResponse(res) {
    try {
        var info = eval("(" + res + ")")
        var msj = info.msj
        switch (msj) {
            case 0:
                muestraVentana(mensajecero);
                setTimeout("limpiar()", 2000);
                break;

            case 1:
                $.fancybox.close();
                usuario = '';
                limpiar();
                globalUsuario = '';
                muestraVentana(mensajeEdita);
                break;
        }
    } catch (elError) {
    }
}
/////-------------------------------------------------------------------///

function cambiarValor() {
    parent.iframeFoto.location.href = "../../Controlador/ctlCargaFotoUsuario.aspx";
}


////*********** CARGA DEPARTAMENTO *********///////
function cargaDeptoEmpresa() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargaDepartamentoEmpresa'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, cargaDeptoEmpresa_processResponse);
}

function cargaDeptoEmpresa_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana('SIN CONEXI&Oacute;N A LA BASE DE DATOS.');
                break;
            case 0:
                limpiarSelect(document.getElementById('SelDepto'));
                break;
            case 1:
                llenarSelect(unescape(res), document.getElementById('SelDepto'));
                break;
        }
        if (deptoGlobal != '') {
            document.getElementById('SelDepto').value = deptoGlobal;
            cargaAreaValor(deptoGlobal);
        }

    } catch (elError) { }
}


////*********** CARGA ÁREA *********///////
function cargaArea() {
    var id_depto_emp = document.getElementById('SelDepto').value;
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargaListaArea'));
    arrayParameters.push(newArg('id_dpto_emp', id_depto_emp));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, cargaArea_processResponse);
}

// CARGA EL AREA SEGUN EL PARAMETRO DE DEPTO
function cargaAreaValor(id_depto_emp) {
    if (id_depto_emp != '-1') {
        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', 'cargaListaArea'));
        arrayParameters.push(newArg('id_dpto_emp', id_depto_emp));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlGeneral.aspx', send, cargaArea_processResponse);
    } else {
        limpiarSelectOpcion(document.getElementById('selArea'));
        limpiarSelectOpcion(document.getElementById('selArea'));
    }
}

function cargaArea_processResponse(res) {
    try {
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
                break;
        }
        if (areaGlobal != '-1') {
            document.getElementById('selArea').value = areaGlobal;
            cargaCargo(areaGlobal);

        }
    } catch (elError) { }
}



///**********CARGA CARGO*****************///
function cargaCargo() {
    var id_area = document.getElementById('selArea').value;
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargaListaCargos'));
    arrayParameters.push(newArg('id_area', id_area));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, cargaCargo_processResponse);
}

function cargacargoValor(id_area) {
    if (id_area != '-1') {
        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', 'cargaListaCargos'));
        arrayParameters.push(newArg('id_area', id_area));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlGeneral.aspx', send, cargaArea_processResponse);
    } else {
        limpiarSelectOpcion(document.getElementById('selArea'));
        limpiarSelectOpcion(document.getElementById('selArea'));
    }
}


function cargaCargo_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                limpiarSelect(document.getElementById('selCargo'));
                break;
            case 1:
                llenarSelect(unescape(res), document.getElementById('selCargo'));
                break;
        }
        if (cargoGlobal != '-1') {
            document.getElementById('selCargo').value = cargoGlobal;
            cargaGrupo(cargoGlobal)
        }

    } catch (elError) { }
}

///************CARGA GRUPOS***********************///


function cargaGrupo() {

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargaGrupo'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, cargaGrupo_processResponse);
}

function cargaGrupo_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                //limpiarSelect(document.getElementById('selGrupo'));
                break;
            case 1:

                //llenarSelect(unescape(res), document.getElementById('selGrupo'));
                //$("#selGrupo").find("option[value='-1']").remove();
                //seleccionMultiple();
                //llenarSelect(unescape(res), document.getElementById('selGrupo'));

                //if (msj != 0) {
                llenarSelect(unescape(res), document.getElementById('selGrupo'));
                $("#selGrupo").find("option[value='-1']").remove();
                seleccionMultiple();
                // }
                break;
        }
        if (grupoGlobal != '-1') {
            document.getElementById('selGrupo').value = grupoGlobal;

        }
    } catch (elErrror) { }
}
/////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////

function detalle(usu_usuario, usu_nombre, usu_mail) {

    //var arrayParameters = new Array();
    //arrayParameters.push(newArg('p', 'detalleAppsUsuario'));
    //arrayParameters.push(newArg('usuario', usu_usuario));
    //var send = arrayParameters.join('&');
    //$.post('../../ctlIdent/ctlUsuarios.aspx', send, detalleAppsUsuario_processResponse);

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'detalleUsuario'));
    arrayParameters.push(newArg('usuario', usu_usuario));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlUsuarios.aspx', send, detalle_processResponse);
}

function detalle_processResponse(res) {
    try {
        var info = eval("(" + res + ")");
        var msj = info.msj;
        switch (msj) {
            case -2:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                document.getElementById('listadoUsuariosDetalle').innerHTML = mensajeSinInformacion;
                break;
            case 1:

                var usuario = info.data[0];
                var nombre = info.data[1];
                var documento = info.data[2];
                var clave = info.data[3];
                var email = info.data[4];
                var telefono = info.data[5];
                var direccion = info.data[6];
                var depto = info.data[7];
                var area = info.data[8];
                var cargo = info.data[9];
                var grupo = info.data[19];
                var estado = info.data[13];
                var foto = info.data[10];
                var rol = info.data[18];

                grupoDetalleArrayGlobal.length = 0;
                grupoDetalleArrayGlobal = grupo.toString().split(',');

                var tabla = "";
                var vacio = "--";

                tabla = '<table border="0" class="tbListado centrar">';


                tabla += '<tr>';
                tabla += '<td colspan="2" class="encabezado">FOTO USUARIO</td>';
                tabla += '</tr>';
                tabla += '<tr>';
                tabla += '<td colspan="2" class="cuerpoListado1"><img colspan="2" id="img1" style="width:125px;margin-top:5px;margin-bottom:5px;" src="../../Fotos/' + ((foto == "" || foto == "NULL") ? "no_disponible.jpg" : unescape(foto)) + ' "alt="" /></td>';
                tabla += '</tr>';

                tabla += '<tr>';
                tabla += '<td colspan="2" class="encabezado">USUARIO LOGIN</td>';
                tabla += '</tr>';
                tabla += '<tr>';
                tabla += '<td colspan="2" class="cuerpoListado1">' + ((usuario == "") ? vacio : unescape(usuario)) + '</td>';
                tabla += '</tr>';

                tabla += '<tr>';
                tabla += '<td class="encabezado">NOMBRE DEL USUARIO</td><td class="encabezado">CORREO ELECTR&Oacute;NICO</td>';
                tabla += '</tr>';
                tabla += '<tr>';
                tabla += '<td class="cuerpoListado1">' + ((nombre == "") ? vacio : unescape(nombre)) + '</td>';
                tabla += '<td class="cuerpoListado1">' + ((email == "") ? vacio : unescape(email)) + '</td>';
                tabla += '</tr>';

                tabla += '<tr>';
                tabla += '<td class="encabezado">DOCUMENTO</td><td class="encabezado">TEL&Eacute;FONO</td>';
                tabla += '</tr>';
                tabla += '<tr>';
                tabla += '<td class="cuerpoListado1">' + ((documento == "") ? vacio : unescape(documento)) + '</td>';
                tabla += '<td class="cuerpoListado1">' + ((telefono == "") ? vacio : unescape(telefono)) + '</td>';
                tabla += '</tr>';

                tabla += '<tr>';
                tabla += '<td class="encabezado" colspan="2">DIRECCI&Oacute;N</td>';
                tabla += '</tr>';
                tabla += '<tr>';
                tabla += '<td class="cuerpoListado1" colspan="2">' + ((direccion == "") ? vacio : unescape(direccion)) + '</td>';
                tabla += '</tr>';

                tabla += '<tr>';
                tabla += '<td class="encabezado" >DEPARTAMENTO</td><td class="encabezado">&Aacute;REA</td>';
                tabla += '</tr>';
                tabla += '<tr>';
                tabla += '<td class="cuerpoListado1">' + ((depto == "") ? vacio : unescape(depto)) + '</td>';
                tabla += '<td class="cuerpoListado1">' + ((area == "") ? vacio : unescape(area)) + '</td>';
                tabla += '</tr>';

                tabla += '<tr>';
                tabla += '<td class="encabezado">CARGO</td><td class="encabezado">ROL</td>';
                tabla += '</tr>';
                tabla += '<tr>';
                tabla += '<td class="cuerpoListado1">' + ((cargo == "") ? vacio : unescape(cargo)) + '</td>';
                tabla += '<td class="cuerpoListado1">' + ((rol == "") ? vacio : unescape(rol)) + '</td>';
                tabla += '</tr>';

                tabla += '<tr>';
                tabla += '<td class="encabezado" colspan="2">GRUPOS</td>';
                tabla += '</tr>';
                for (var i = 0; i < grupoDetalleArrayGlobal.length; i++) {
                    tabla += '<tr>';
                    tabla += '<td class="cuerpoListado1" colspan="2">' + ((grupoDetalleArrayGlobal[i] == "") ? vacio : unescape(grupoDetalleArrayGlobal[i])) + '</td>';
                    tabla += '</tr>';
                }

                tabla += '</table>';
                document.getElementById('listadoUsuariosDetalle').innerHTML = tabla;
                break
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

    } catch (elError) {
    }
}


function detalleAppsUsuario_processResponse(res) {
    try {
        var info = eval("(" + res + ")");
        var msj = info.msj;
        switch (msj) {
            case -2:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                document.getElementById('listadoUsuariosAplicacioness').innerHTML = mensajeSinInformacion;
                break;
            case 1:

                var cols = info.cols;
                var tamanio = info.data.length;

                var tabla = "";
                var vacio = "--";

                var id = "", nombre = "";

                tabla = '<table border="0" class="tbListado centrar">';
                tabla += '<tr>';
                tabla += '<td colspan="2" class="encabezado">APLICACIONES ASIGNADA</td>';
                tabla += '</tr>';

                for (var i = 0; i < tamanio; i += cols) {

                    id = info.data[i];
                    nombre = info.data[i + 1];

                    tabla += '<tr>';
                    tabla += '<td colspan="2" class="cuerpoListado1">' + ((nombre == "") ? vacio : unescape(nombre)) + '</td>';
                    tabla += '</tr>';

                }

                tabla += '</table>';
                document.getElementById('listadoUsuariosAplicacioness').innerHTML = tabla;
                break
        }

    } catch (elError) {
    }
}



