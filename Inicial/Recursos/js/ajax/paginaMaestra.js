var globalEliminar = -1;
var DocGlobal = '-1';
var idEspeGlobal = '-1';

var mensajeGuarda = 'INFORMACI&Oacute;N ALMACENADA CORRECTAMENTE.';
var mensajeErrorGuarda = 'NO SE HA ALMACENADO LA INFORMACI&Oacute;N.';
var mensajeEdita = 'INFORMACI&Oacute;N ACTUALIZADA CORRECTAMENTE.';
var mensajeErrorEdita = 'NO SE HA ACTUALIZADO LA INFORMACI&Oacute;N.';
var mensajeElimina = 'INFORMACI&Oacute;N ELIMINADA CORRECTAMENTE.';
var mensajeErrorElimina = 'NO SE HA ELIMINADO LA INFORMACI&Oacute;N.';
var mensajeErrorEliminaRelacion = 'NO SE ELIMIN&Oacute;, TIENE RELACIONADA INFORMACI&Oacute;N.';
var mensajeErrorConexion = 'SIN CONEXI&Oacute;N A LA BASE DE DATOS.';
var mensajeObligatorio = 'DEBE INGRESAR LOS CAMPOS OBLIGATORIOS.';
var mensajeConfirmaEliminacion = '&#191; CONFIRMA LA ELIMINACI&Oacute;N DE ESTA INFORMACI&Oacute;N &#63;';
var mensajeErrorEliminacion = 'ESTE REGISTRO NO SE PUEDE ELIMINAR YA QUE SE ENCUENTRA ASOCIADO. <br /> DESEA CAMBIAR SU ESTADO A INACTIVO?.';
var mensajeSinInformacion = 'NO SE ENCONTR&Oacute; INFORMACI&Oacute;N.';
var mensajeSinInformacionListado = 'NO SE ENCONTR&Oacute; INFORMACI&Oacute;N EN LA BASE DE DATOS.';
var mensajeDatosEliminar = 'DEBE SELECCIONAR INFORMACI&Oacute;N A ELIMINAR.'
var mensajeDatosEditar = 'DEBE SELECCIONAR INFORMACI&Oacute;N A EDITAR.'
var mensajeInfoExiste = 'LA INFORMACI&Oacute;N YA EXISTE EN LA BASE DE DATOS.';

var mensajemenosuno = 'INFORMACI&Oacute;N TEMPOR&Aacute;LMENTE NO DISPONIBLE';
var mensajecero = 'A&Uacute;N NO EXISTE INFORMACI&Oacute;N ';
var mensajeobligatorios = 'INGRESE LOS CAMPOS OBLIGATORIOS';



///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////   FUNCIONES QUE SE INICIAN AUTOMATICAMENTE  /////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


if (window.history) {
    function noBack() { window.history.forward() }
    noBack();
    window.onload = noBack;
    window.onpageshow = function (evt) { if (evt.persisted) noBack(); }
    window.onunload = function () { void (0); }
}

var ubicacion = '';
$().ready(function () {
    cargaIDMenus();
    verRutaSistema();
});


function newArg(key, value) {
    return key + "=" + encodeURIComponent(value);
}

function verRutaSistema() {
    var ruta = "";
    var idMenu = $("#idMenuForm").html();
    if (parseFloat(idMenu) >= 0) {
        var ele = document.getElementById("li" + idMenu);
        ruta = $(ele).find("a").first().html();
        ruta = calculaRuta(ele) + ruta;
    } else {
        switch (idMenu) {
            case '-1':
                ruta = "Inicio";
                break;

            case '-100':
                ruta = "Mantenimiento";
                break;

            case '-3':
                ruta = "Mi Cuenta > Cambiar Clave";
                break;
        }
    }
    $("#path").html(ruta);
}


function calculaRuta(obj) {
    var res = "";
    try {
        var tipo = obj.tagName.toLowerCase();
        var padre = $(obj).parent().parent();
        if ($(padre).prop("tagName") == "LI") {
            res = calculaRuta(document.getElementById($(padre).attr("id")));
            res += $(padre).find("a").first().html() + "<b> > </b>"
        }
    } catch (error) {
    }
    return res;
}

function trim(str) {
    return ltrim(rtrim(str));
}

function ltrim(str) {
    return str.replace(/^\s+|\s+$/g, "");
}

function rtrim(str) {
    return str.replace(/^\s+|\s+$/g, "");
}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////   VARIABLES GLOBALES  /////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

var arrayMenus = new Array(); /* Menus que estan en la base de datos */
var htmlMenu = ''; /* se almacena el menu para se rpintado con los datos que llegan de la base de datos*/
var menu_li = '';  /* para dar el tratamiento a cada uno de los items del menu */
var menusPermitidos = new Array();  /*para guardar los menus que le corresponden a la persona que ha ingresado */
var menusFinales = new Array();
var permisosMenuDB = new Array();
var permisosMenuRol = new Array();


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////  MOSTRAR LOS MENUS PERMITIDOS  ///////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function pintaMenu(am) {
    var cont = am.length;
    var mp = menusPermitidos;
    var numMenus = mp.length;
    var id = '';
    for (var j = 0; j < cont; j++) {
        id = am[j];
        for (var i = 0; i < numMenus; i++) {
            if (id == mp[i]) {
                id = "li" + id;
                try {
                    switch (id.length) {
                        /*Raiz de inicio de consultor*/
                        case 4:
                            document.getElementById(id).style.display = 'block';
                            break;

                        /*Nivel 1*/
                        case 5:
                            document.getElementById(id).style.display = 'block';
                            try {
                                document.getElementById(id.substring(0, 3)).style.display = 'block';
                            } catch (e) {
                            }
                            break;
                        case 6:
                            document.getElementById(id).style.display = 'block';
                            try {
                                document.getElementById(id.substring(0, 4)).style.display = 'block';
                            } catch (e) {
                            }
                            break;

                        /*Nivel 2*/
                        case 7:
                            document.getElementById(id).style.display = 'block';

                            try {
                                document.getElementById(id.substring(0, 3)).style.display = 'block';
                            } catch (e) {
                            }

                            try {
                                document.getElementById(id.substring(0, 5)).style.display = 'block';
                            } catch (e) {
                            }
                            break;

                        /*Nivel 2 con numeros iguales o superiores a 10 por ejemplo 1.1.12*/
                        case 8:
                            document.getElementById(id).style.display = 'block';
                            try {
                                document.getElementById(id.substring(0, 3)).style.display = 'block';
                            } catch (e) {
                            }
                            try {
                                document.getElementById(id.substring(0, 5)).style.display = 'block';
                            } catch (e) {
                            }
                            try {
                                document.getElementById(id.substring(0, 4)).style.display = 'block';
                            } catch (e) {
                            }
                            try {
                                document.getElementById(id.substring(0, 6)).style.display = 'block';
                            } catch (e) {
                            }
                            break;

                        /*Nivel 4*/
                        case 10:
                            document.getElementById(id).style.display = 'block';
                            try {
                                document.getElementById(id.substring(0, 3)).style.display = 'block';
                            } catch (e) {
                            }
                            try {
                                document.getElementById(id.substring(0, 5)).style.display = 'block';
                            } catch (e) {
                            }
                            try {
                                document.getElementById(id.substring(0, 4)).style.display = 'block';
                            } catch (e) {
                            }
                            try {
                                document.getElementById(id.substring(0, 6)).style.display = 'block';
                            } catch (e) {
                            }
                            try {
                                document.getElementById(id.substring(0, 8)).style.display = 'block';
                            } catch (e) {
                            }
                            try {
                                document.getElementById(id.substring(0, 7)).style.display = 'block';
                            } catch (e) {
                            }
                            break;

                        /*Nivel 4 con numeros iguales o superiores a 10 por ejemplo 51.1.1.12*/
                        case 11:
                            document.getElementById(id).style.display = 'block';
                            try {
                                document.getElementById(id.substring(0, 3)).style.display = 'block';
                            } catch (e) {
                            }
                            try {
                                document.getElementById(id.substring(0, 5)).style.display = 'block';
                            } catch (e) {
                            }
                            try {
                                document.getElementById(id.substring(0, 4)).style.display = 'block';
                            } catch (e) {
                            }
                            try {
                                document.getElementById(id.substring(0, 6)).style.display = 'block';
                            } catch (e) {
                            }
                            try {
                                document.getElementById(id.substring(0, 7)).style.display = 'block';
                            } catch (e) {
                            }
                            try {
                                document.getElementById(id.substring(0, 8)).style.display = 'block';
                            } catch (e) {
                            }
                            break;
                    }
                } catch (error) {
                }
                i = numMenus;
            }
        }
    }
}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////  FUNCIONES JQUERY DEL HOME  //////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

/*SE CARGAN LOS MENUS DINAMICAMENTE SEGUN LOS PERMISOS QUE SE LE DIERON AL USUARIO EN SU CREACION*/
function pintaPermisos() {
    var aux = document.getElementById('permisos').value;
    //alert(aux)
    var idMenuForm = document.getElementById('idMenuForm').innerHTML;
    var mPermisos = aux.split(';');
    var contMenus = mPermisos.length;
    var itemMenu = '';
    var tmp = 0;
    var permisos = new Array();
    for (var i = 0; i < contMenus; i++) {
        permisos = mPermisos[i].split(',');
        itemMenu = permisos[0];
        menusPermitidos.push(itemMenu);
        if (itemMenu == idMenuForm) {
            permisosMenuRol = permisos;
            permisosParaMenu(idMenuForm);
        }
    }
}

/*Arreglo con todos los menus que se encuentran disponibles en la pagina maestra (los id de los li que llevan a un formulario)*/
//var arrayMenus = new Array('li0.1', 'li0.2')


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////CARGA ID de MENUS DISPONIBLES///////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
function cargaIDMenus() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargaIDmenus'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlLogin.aspx', send, cargaIDMenus_processResponse);
}

function cargaIDMenus_processResponse(res) {
    try {
        var info = eval("(" + res + ")");
        var msj = info.msj;
        switch (msj) {
            case -2:
                muestraVentana(mensajemenosuno);
                break;

            case 0:
                muestraVentana(mensajecero);
                break;

            case 1:
                for (var i = 0; i < info.data.length; i++) {
                    arrayMenus.push(info.data[i]);
                }
                pintaPermisos();
                pintaMenu(arrayMenus);

                /*Se muestran las opciones generales, las que se dan sin permisos*/
                document.getElementById('liInicio').style.display = 'block';
                document.getElementById('limc').style.display = 'block';
                document.getElementById('licc').style.display = 'block';
                document.getElementById('licf').style.display = 'block';
                break;
        }
        //verRutaSistema();
    } catch (elError) {
    }
}


/***********************************************************
METODO QUE PERMITE MOSTRAR MENU SEGUN LOS PERMISOS ASIGNADOS
***********************************************************/
function permisosParaMenu(id) {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargaPermisosMenu'));
    arrayParameters.push(newArg('men_id', id));
    var send = arrayParameters.join('&');
    //alert(send)
    $.post('../../Controlador/ctlLogin.aspx', send, permisosParaMenu_processResponse);
}

function permisosParaMenu_processResponse(res) {
    try {
        var info = eval("(" + res + ")");
        //alert(info.data)
        var msj = info.msj;
        switch (msj) {
            case -2:
                muestraVentana(mensajemenosuno);
                break;

            case 0:
                muestraVentana(mensajecero);
                break;

            case 1:
                permisosMenuDB = info.data[0].split(','); //Permisos de la base de datos
                var permisosTemp = permisosMenuRol; //Permisos para el usuario

                for (var j = 1; j <= permisosTemp.length; j++) {
                    for (var i = 0; i < permisosMenuDB.length; i++) {
                        if (permisosTemp[j] == permisosMenuDB[i]) {
                            permisosMenuDB.splice(i, 1);
                        }
                    }
                }

                //alert(permisosMenuDB.length);
                for (var j = 0; j < permisosMenuDB.length; j++) {
                    try {
                        switch (permisosMenuDB[j]) {

                            case 'Guarda':
                                try {
                                    document.getElementById('imgGuardar').innerHTML = '';
                                } catch (e) {
                                }
                                $('.botonGuardar').html('');
                                break;

                            case 'Auditoria':
                                try {
                                    document.getElementById('botonAuditoria').innerHTML = '';
                                } catch (e) {
                                }
                                $('.botonAuditoria').html('');
                                break;

                            case 'Detalle':
                                try {
                                    document.getElementById('imgDetalle').innerHTML = '';
                                } catch (e) {
                                }
                                $('.botonDetalle').html('');
                                break;

                            case 'Busca':
                                try {
                                    document.getElementById('imgBuscar').innerHTML = '';
                                    document.getElementById('imgTodos').innerHTML = '';
                                } catch (e) {
                                }
                                $('.botonBuscar').html('');
                                $('.botonTodos').html('');
                                break;

                            case 'Lista':
                                try {
                                    document.getElementById('imgListar').innerHTML = '';
                                } catch (e) {
                                }
                                $('.botonListar').html('');
                                break;

                            case 'Edita':
                                try {
                                    document.getElementById('imgEditar').innerHTML = '';
                                } catch (e) {
                                }
                                $('.botonEditar').html('');
                                break;

                            case 'Elimina':
                                try {
                                    document.getElementById('imgEliminar').innerHTML = '';
                                } catch (e) {
                                }
                                $('.botonEliminar').html('');
                                break;

                            case 'Variables Acceso':
                                try {
                                    document.getElementById('imgVariables').innerHTML = '';
                                } catch (e) {
                                }
                                $('.botonAcceso').html('');
                                break;

                            case 'Permisos':
                                try {
                                    document.getElementById('imgPermisos').innerHTML = '';
                                } catch (e) {
                                }
                                $('.botonPermisos').html('');
                                break;

                            case 'Asignar':
                                try {
                                    document.getElementById('imgAsignar').innerHTML = '';
                                } catch (e) {
                                }
                                $('.botonAsignar').html('');
                                break;

                            case 'Activar':
                                try {
                                    document.getElementById('imgActivar').innerHTML = '';
                                } catch (e) {
                                }
                                $('.botonActivar').html('');
                                break;

                            case 'Crear':
                                try {
                                    document.getElementById('imgCrear').innerHTML = '';
                                } catch (e) {
                                }
                                $('.botonCrear').html('');
                                break;

                            case 'Asociar':
                                try {
                                    document.getElementById('imgAsociar').innerHTML = '';
                                } catch (e) {
                                }
                                $('.botonAsociar').html('');
                                break;

                            case 'Etapa':
                                try {
                                    document.getElementById('imgEtapa').innerHTML = '';
                                } catch (e) {
                                }
                                $('.botonEtapa').html('');
                                break;

                            case 'Estado':
                                try {
                                    document.getElementById('imgEstado').innerHTML = '';
                                } catch (e) {
                                }
                                $('.botonEstado').html('');
                                break;

                            case 'Ejecutar':
                                try {
                                    document.getElementById('imgEjecutar').innerHTML = '';
                                } catch (e) {
                                }
                                $('.botonEjecutar').html('');
                                break;

                            case 'Reabrir':
                                try {
                                    document.getElementById('imgReabrir').innerHTML = '';
                                } catch (e) {
                                }
                                $('.botonReabrir').html('');
                                break;

                            case 'Cerrar':
                                try {
                                    document.getElementById('imgCerrar').innerHTML = '';
                                } catch (e) {
                                }
                                $('.botonCerrar').html('');
                                break;

                            case 'Diagnostico':
                                try {
                                    document.getElementById('imgVerDiagnostico').innerHTML = '';
                                } catch (e) {
                                }
                                $('.botonVerDiagnostico').html('');
                                break;
                            case 'Indicadores':
                                try {
                                    document.getElementById('imgIndicador').style.display = 'none';
                                    document.getElementById('divimgVitaly').style.display = 'block';
                                } catch (e) {
                                }
                                $('.botonIndicador').html('');
                                document.getElementById('imgIndicador').style.display = 'none';
                                document.getElementById('divimgVitaly').style.display = 'block';
                                break;

                            case 'Marcacion':
                                try {
                                    document.getElementById('chkMarcacion').innerHTML = '';
                                } catch (e) {
                                }
                                $('.botonVerMarcacion').html('');
                                document.getElementById('chkMarcacion').style.display = 'none';
                                break;

                            case 'FiltroSino':
                                try {
                                    document.getElementById('selFil').innerHTML = '';
                                } catch (e) {
                                }
                                $('.botonVerFiltro').html('');
                                document.getElementById('selFil').style.display = 'none';
                                break;

                            case 'BuscaAñoMes':
                                try {
                                    document.getElementById('selFilAnio').innerHTML = '';
                                    document.getElementById('selFilMes').innerHTML = '';
                                } catch (e) {
                                }
                                $('.botonVerCamposFil').html('');
                                document.getElementById('selFilAnio').style.display = 'none';
                                document.getElementById('selFilMes').style.display = 'none';
                                break;


                            case 'Bukeala':
                                try {
                                    document.getElementById('imgBukeala').innerHTML = '';
                                } catch (e) {
                                }
                                $('.botonBukeala').html('');
                                break;

                            case 'Correo':
                                try {
                                    document.getElementById('imgCorreo').innerHTML = '';
                                } catch (e) {
                                }
                                $('.botonCorreo').html('');
                                break;

                            case 'Llamar':
                                try {
                                    document.getElementById('imgLlamar').innerHTML = '';
                                } catch (e) {
                                }
                                $('.botonLlamar').html('');
                                break;

                            case 'CerrarPaciente':
                                try {
                                    document.getElementById('imgCerrarPaciente').innerHTML = '';
                                } catch (e) {
                                }
                                $('.botonCerrarPaciente').html('');
                                break;

                            case 'Tramites':
                                try {
                                    document.getElementById('imgTramites').innerHTML = '';
                                } catch (e) {
                                }
                                $('.BotonTramites').html('');
                                break;

                            //                            case 'Estados':
                            //                                try {
                            //                                    document.getElementById('imgEstados').innerHTML = '';
                            //                                } catch (e) {
                            //                                }
                            //                                $('.botonEstados').html('');
                            //                                break;



                        }
                    } catch (mierror) {
                    }
                }

                for (var j = 0; j < permisosTemp.length; j++) {
                    try {
                        switch (permisosTemp[j]) {

                            case 'Guarda':
                                try {
                                    document.getElementById('imgGuardar').style.display = 'block';
                                } catch (e) {
                                }
                                $('.botonGuardar').css('display', 'inline-block');
                                break;

                            case 'Detalle':
                                try {
                                    document.getElementById('imgDetalle').style.display = 'block';
                                } catch (e) {
                                }
                                $('.botonDetalle').css('display', 'inline-block');
                                break;


                            case 'Busca':
                                try {
                                    document.getElementById('imgBuscar').style.display = 'block';
                                    document.getElementById('imgTodos').style.display = 'block';
                                } catch (e) {
                                }
                                $('.botonBuscar').css('display', 'inline-block');
                                $('.botonTodos').css('display', 'inline-block');
                                break;

                            case 'Lista':
                                try {
                                    document.getElementById('imgListar').style.display = 'block';
                                } catch (e) {
                                }
                                $('.botonListar').css('display', 'inline-block');
                                break;

                            case 'Edita':
                                try {
                                    document.getElementById('imgEditar').style.display = 'block';
                                } catch (e) {
                                }
                                $('.botonEditar').css('display', 'inline-block');
                                break;

                            case 'Elimina':
                                try {
                                    document.getElementById('imgEliminar').style.display = 'block';
                                } catch (e) {
                                }
                                $('.botonEliminar').css('display', 'inline-block');
                                break;

                            case 'Variables Acceso':
                                try {
                                    document.getElementById('imgVariables').style.display = 'block';
                                } catch (e) {
                                }
                                $('.botonAcceso').css('display', 'inline-block');
                                break;

                            case 'Permisos':
                                try {
                                    document.getElementById('imgPermisos').style.display = 'block';
                                } catch (e) {
                                }
                                $('.botonPermisos').css('display', 'inline-block');
                                break;

                            case 'Diagnostico':
                                try {
                                    document.getElementById('imgVerDiagnostico').style.display = 'block';
                                } catch (e) {
                                }
                                $('.botonVerDiagnostico').css('display');
                                break;

                            case 'Auditoria':
                                try {
                                    document.getElementById('botonAuditoria').style.display = 'block';
                                } catch (e) {
                                }
                                $('.botonAuditoria').css('display');
                                break;

                            //case 'Marcacion':
                            //    try {
                            //        document.getElementById('chkMarcacion').style.display = 'block';
                            //    } catch (e) {
                            //    }
                            //    $('.botonVerMarcacion').css('display');
                            //    break; 

                            case 'CerrarPaciente':
                                try {
                                    document.getElementById('botonCerrarPaciente').style.display = 'block';
                                } catch (e) {
                                }
                                $('.botonCerrarPaciente').css('display');
                                break;

                            case 'Estados':
                                try {
                                    document.getElementById('imgEstados').style.display = 'block';
                                } catch (e) {
                                }
                                $('.botonEstados').css('display');
                                break;
                        }
                    } catch (mierror) {
                    }
                }
                break;
        }
    } catch (elError) {
    }
}

/////////////////////////////////////// menu flotante /////////////////////////////////////////////////

// numero de pixels de separacion con la parte superior de la ventana  
var theTop = 200;

var menu, scrollIt, ventanaFlotante, ventanaFlotanteMadre, ventanaFlotantePadre;

// posicion actual  
var old = theTop;
// a true, la posici�n del menu se mantiene a "theTop" pixels, aunque se desplace la pagina verticalmente  
// a false, el menu es fijo  
var scrollIt = true;

// INICIALIZACION  
function init() {
    // obtiene referencia al objeto con el menu
    if (document.getElementById('ventanaFlotante') != null) {
        ventanaFlotante = new getObj('ventanaFlotante');
    }
    // obtiene referencia al objeto con el menu
    if (document.getElementById('divFlotanteConfirmaMadre') != null) {
        ventanaFlotanteMadre = new getObj('divFlotanteConfirmaMadre');
    }
    // obtiene referencia al objeto con el menu
    if (document.getElementById('divFlotanteConfirmaPadre') != null) {
        ventanaFlotantePadre = new getObj('divFlotanteConfirmaPadre');
    }
    // obtiene referencia al objeto con el menu
    menu = new getObj('ventana');
    // inicia el proceso que mantiene la posicion a "theTop" pixels
    movemenu();
}

//MOVIMIENTO  
function movemenu() {
    if (window.innerHeight) {
        pos = window.pageYOffset;
    } else if (document.documentElement && document.documentElement.scrollTop) {
        pos = document.documentElement.scrollTop;
    } else if (document.body) {
        pos = document.body.scrollTop;
    }

    if (pos < theTop) {
        pos = theTop;
    } else {
        pos += 200;
    }
    if (pos == old) {
        if (document.getElementById('ventanaFlotante') != null) {
            ventanaFlotante.style.top = pos + 'px';
        }
        if (document.getElementById('divFlotanteConfirmaMadre') != null) {
            ventanaFlotanteMadre.style.top = pos + 'px';
        }
        if (document.getElementById('divFlotanteConfirmaPadre') != null) {
            ventanaFlotantePadre.style.top = pos + 'px';
        }
        menu.style.top = pos + 'px';
    }

    old = pos;

    moveID = setTimeout('movemenu()', 10);
}

//OBTENCION DE REFERENCIA AL OBJETO  
function getObj(name) {
    // si soporta DOM Lelvel 2
    if (document.getElementById) {
        this.obj = document.getElementById(name);
        this.style = document.getElementById(name).style;
        // si soporta el DOM del IE4.x
    } else if (document.all) {
        this.obj = document.all[name];
        this.style = document.all[name].style;
        // si soporta el DOM del N4.x
    } else if (document.layers) {
        this.obj = document.layers[name];
        this.style = document.layers[name];
    }
}

$(document).ready(function () {
    if (document.getElementById('ventana') != null) {
        init();
    }
    if (document.getElementById('ventana') != null) {
        if (document.captureEvents) {      //N4 requiere invocar la funcion captureEvents  
            document.captureEvents(Event.LOAD);
        }
    }
    if (document.getElementById('divFlotanteConfirmaMadre') != null) {
        init();
    }
    if (document.getElementById('divFlotanteConfirmaMadre') != null) {
        if (document.captureEvents) {      //N4 requiere invocar la funcion captureEvents  
            document.captureEvents(Event.LOAD);
        }
    }
    if (document.getElementById('divFlotanteConfirmaPadre') != null) {
        init();
    }
    if (document.getElementById('divFlotanteConfirmaPadre') != null) {
        if (document.captureEvents) {      //N4 requiere invocar la funcion captureEvents  
            document.captureEvents(Event.LOAD);
        }
    }
})

/////////////////////////////////////// menu flotante /////////////////////////////////////////////////
function muestraVentana(mensaje) {
    try {
        document.getElementById('ventana').style.display = 'block';
        document.getElementById('msjVentana').innerHTML = '<br />' + mensaje;
        setTimeout("document.getElementById('ventana').style.display = 'none'", 1500);
    } catch (elError) {
    }
}


function muestraVentanaConfirmacion(mensaje, aceptar, cancelar) {
    try {
        document.getElementById('ventanaConfirmacion').style.display = 'block';
        document.getElementById('msjConfirmacion').innerHTML = '<br />' + mensaje;
        var opciones = '<table><tr><td class="centrar"><span class="leyenda">Aceptar</span><br /><img src="../../Recursos/imagenes/administracion/aceptar.png" title="Aceptar" alt="Aceptar" class="imgAdmin" onclick="' + aceptar + '" /></td>';
        opciones += '<td class="centrar"><span class="leyenda">Cancelar</span><br /><img src="../../Recursos/imagenes/administracion/cancelar.png" title="Cancelar" alt="Cancelar" class="imgAdmin" onclick="' + cancelar + '" /></td></tr></table>';
        document.getElementById('opciones').innerHTML = opciones
    } catch (elError) {
    }
}

function muestraVentanaProgreso(mensaje) {
    try {
        document.getElementById('ventanaProg').style.display = 'block';
        if (mensaje != null) {
            document.getElementById('msjVentanaProg').innerHTML = '<br />' + mensaje;
        } else {
            document.getElementById('msjVentanaProg').innerHTML = '<br /> Cargando...';
        }
    } catch (elError) {
    }
}

function ocultaVentanaProgreso() {
    try {
        document.getElementById('ventanaProg').style.display = 'none';
    } catch (elError) {
    }
}

/*
* Llena un select de forma generica para todos los navegadores
* llamado:  llenarSelect(this.responseText, document.getElementById('idSelect'))
*/

function llenarSelect(json, combo) {
    llenarSelect(json, combo, '');
}

/*
* Llena un select de forma generica para todos los navegadores con el valor definido en determinada posicion
* llamado:  llenarSelect(this.responseText, document.getElementById('idSelect'),id)
*/

function llenarSelect(json, combo, idSel) {
    try {
        limpiarSelect(combo);
        var info = eval('(' + json + ')');
        var opt = null;
        for (var i = 0; i < info.data.length; i += 2) {
            opt = new Option(unescape(info.data[i + 1]), info.data[i])
            if (info.data[i] == idSel) {
                opt.selected = true;
            }
            combo.options[combo.length] = opt;
            combo.name = info.data[i + 1];
        }
    } catch (elError) {
    }
}

/*
* limpia un select de forma generica para todos los navegadores  
* llamado:  limpiarSelect(document.getElementById('idSelect'))
*/
///////////////////////////////////////////////////////////////////////
function llenarSelectConSeleccion(json, combo, idSel) {
    try {
        limpiarSelectConSeleccion(combo);
        var info = eval('(' + json + ')');
        var opt = null;
        for (var i = 0; i < info.data.length; i += 2) {
            opt = new Option(info.data[i + 1], info.data[i])
            if (info.data[i] == idSel) {
                opt.selected = true;
            }
            combo.options[combo.length] = opt;
        }
    } catch (elError) {
    }
}
function limpiarSelectConSeleccion(combo) {
    try {
        while (combo.length > 0) {
            combo.remove(combo.length - 1);
        }
    } catch (elError) {
    }
    combo.options[0] = new Option('-- SELECCIONE --', '-1');
}
/////////////////////////////////////////////////////////////////////

function limpiarSelect(combo) {
    try {
        while (combo.length > 0) {
            combo.remove(combo.length - 1);
        }
    } catch (elError) {
    }
    //combo.options[0] = new Option('-- SELECCIONE --', '0');
}

function limpiarSelectOpcion(combo) {
    try {
        while (combo.length > 0) {
            combo.remove(combo.length - 1);
        }
    } catch (elError) {
    }
    combo.options[0] = new Option('-- SELECCIONE --', '-1');
}

function llenarSelectMultiple(json, combo) {
    try {
        limpiarSelectMultiple(combo);
        var info = eval('(' + json + ')');
        for (var i = 0; i < info.data.length; i += 2) {
            combo.options[combo.length] = new Option(info.data[i + 1], info.data[i]);
        }
    } catch (elError) {
    }
}

function limpiarSelectMultiple(combo) {
    try {
        while (combo.length > 0) {
            combo.remove(combo.length - 1);
        }
    } catch (elError) {
    }
}

/*Para manipular la ubicación*/
function mostrarUbicacion() {
    var url = window.location.href;
    var dirs = url.toString().split('/');
    var pagina = dirs[dirs.length - 1];
    var nombre = pagina.toString().split('.')[0].toLowerCase();
    nombre = nombre.substring(0, 1).toUpperCase() + nombre.substring(1, nombre.length);
    //document.getElementById("ubicacion").innerHTML = "Usted est&aacute; en: <u>" + nombre + "</u>";    
}


/* 
* Pie de pagina con los botones "siguiente", "anterior", "primero" y "último" para el listar v2.0 
* llamado:  pieDePaginaListar(data, 'nombreMetodoListar')
*/
function pieDePaginaListar(datos, inNomMetodo) {
    var PagAct = datos.PagAct;
    var PagSig = datos.PagSig;
    var PagAnt = datos.PagAnt;
    var PagUlt = datos.PagUlt;
    var Total = datos.Total;
    var tmpPieRetorna = "";

    tmpPieRetorna = "<table class='centrar'><tr>";
    if (PagAct > 1)
        tmpPieRetorna += "<td><span onclick='" + inNomMetodo + "(1)' ><img class='imgPaginacionHabilitada' border='0' src='../../Recursos/imagenes/listado/inicio.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img class='imgPaginacionDeshabilitada' border='0' src='../../Recursos/imagenes/listado/inicio_gris.png' /></td>";

    if (PagAct != 1)
        tmpPieRetorna += "<td><span onclick=\"" + inNomMetodo + "('" + PagAnt + "')\" ><img class='imgPaginacionHabilitada' border=0 src='../../Recursos/imagenes/listado/anterior.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/anterior_gris.png' class='imgPaginacionDeshabilitada' /></td>";

    tmpPieRetorna += "<td><span class='paginasBorde'>" + PagAct + "</span></td><td><span class='paginas'> / </span></td><td><span class='paginasBorde'>" + PagUlt + "</span></td>";

    if (PagAct < PagUlt)
        tmpPieRetorna += "<td><span onclick=\"" + inNomMetodo + "('" + PagSig + "')\" ><img class='imgPaginacionHabilitada' class='imgPaginacion' border=0 src='../../Recursos/imagenes/listado/siguiente.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/siguiente_gris.png' class='imgPaginacionDeshabilitada' /></td>";

    if (PagAct != PagUlt)
        tmpPieRetorna += "<td><span onclick='" + inNomMetodo + "(" + PagUlt + ")' ><img  class='imgPaginacionHabilitada' class='imgPaginacion' border=0  src='../../Recursos/imagenes/listado/ultimo.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/ultimo_gris.png' class='imgPaginacionDeshabilitada' /></td>";
    tmpPieRetorna += "</tr>";
    tmpPieRetorna += "<tr align='center' class='centrar'><td colspan='7' align='center' class='centrar'><span class='contador'> En total </span><span class='contadorBorde'>" + Total + "</span><span class='contador'> registros.</span></td></tr>";
    tmpPieRetorna += "</tr></table>";
    return tmpPieRetorna;
}

function pieDePaginaListar_Suma_Columna(datos, total, inNomMetodo) {
    var PagAct = datos.PagAct;
    var PagSig = datos.PagSig;
    var PagAnt = datos.PagAnt;
    var PagUlt = datos.PagUlt;
    var Total = datos.Total;
    var tmpPieRetorna = "";

    //  var Cantidad_Datos = datos.data[40];

    tmpPieRetorna = "<table class='centrar'><tr>";
    if (PagAct > 1)
        tmpPieRetorna += "<td><span onclick='" + inNomMetodo + "(1)' ><img class='imgPaginacionHabilitada' border='0' src='../../Recursos/imagenes/listado/inicio.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img class='imgPaginacionDeshabilitada' border='0' src='../../Recursos/imagenes/listado/inicio_gris.png' /></td>";

    if (PagAct != 1)
        tmpPieRetorna += "<td><span onclick=\"" + inNomMetodo + "('" + PagAnt + "')\" ><img class='imgPaginacionHabilitada' border=0 src='../../Recursos/imagenes/listado/anterior.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/anterior_gris.png' class='imgPaginacionDeshabilitada' /></td>";

    tmpPieRetorna += "<td><span class='paginasBorde'>" + PagAct + "</span></td><td><span class='paginas'> / </span></td><td><span class='paginasBorde'>" + PagUlt + "</span></td>";

    if (PagAct < PagUlt)
        tmpPieRetorna += "<td><span onclick=\"" + inNomMetodo + "('" + PagSig + "')\" ><img class='imgPaginacionHabilitada' class='imgPaginacion' border=0 src='../../Recursos/imagenes/listado/siguiente.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/siguiente_gris.png' class='imgPaginacionDeshabilitada' /></td>";

    if (PagAct != PagUlt)
        tmpPieRetorna += "<td><span onclick='" + inNomMetodo + "(" + PagUlt + ")' ><img  class='imgPaginacionHabilitada' class='imgPaginacion' border=0  src='../../Recursos/imagenes/listado/ultimo.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/ultimo_gris.png' class='imgPaginacionDeshabilitada' /></td>";
    tmpPieRetorna += "</tr>";
    tmpPieRetorna += "<tr align='center' class='centrar'><td colspan='7' align='center' class='centrar'><span class='contador'> En total: </span><span class='contadorBorde'>" + Total + "</span><span class='contador'> registros</span><span class='contador'> y </span><span class='contadorBorde'>" + total + "</span><span class='contador'> Trámites.</span></td>";
    //    tmpPieRetorna += "<td colspan='7' align='center' class='centrar'><span class='contador'> En total </span><span class='contadorBorde'>" + Cantidad_Datos + "</span><span class='contador'> Trámites.</span></td></tr>";
    tmpPieRetorna += "</table>";


    return tmpPieRetorna;
}

function pieDePaginaListar_Autorizaciones(datos, total, inNomMetodo) {
    var PagAct = datos.PagAct;
    var PagSig = datos.PagSig;
    var PagAnt = datos.PagAnt;
    var PagUlt = datos.PagUlt;
    var Total = datos.Total;
    var tmpPieRetorna = "";

    //  var Cantidad_Datos = datos.data[40];

    tmpPieRetorna = "<table class='centrar'><tr>";
    if (PagAct > 1)
        tmpPieRetorna += "<td><span onclick='" + inNomMetodo + "(1)' ><img class='imgPaginacionHabilitada' border='0' src='../../Recursos/imagenes/listado/inicio.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img class='imgPaginacionDeshabilitada' border='0' src='../../Recursos/imagenes/listado/inicio_gris.png' /></td>";

    if (PagAct != 1)
        tmpPieRetorna += "<td><span onclick=\"" + inNomMetodo + "('" + PagAnt + "')\" ><img class='imgPaginacionHabilitada' border=0 src='../../Recursos/imagenes/listado/anterior.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/anterior_gris.png' class='imgPaginacionDeshabilitada' /></td>";

    tmpPieRetorna += "<td><span class='paginasBorde'>" + PagAct + "</span></td><td><span class='paginas'> / </span></td><td><span class='paginasBorde'>" + PagUlt + "</span></td>";

    if (PagAct < PagUlt)
        tmpPieRetorna += "<td><span onclick=\"" + inNomMetodo + "('" + PagSig + "')\" ><img class='imgPaginacionHabilitada' class='imgPaginacion' border=0 src='../../Recursos/imagenes/listado/siguiente.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/siguiente_gris.png' class='imgPaginacionDeshabilitada' /></td>";

    if (PagAct != PagUlt)
        tmpPieRetorna += "<td><span onclick='" + inNomMetodo + "(" + PagUlt + ")' ><img  class='imgPaginacionHabilitada' class='imgPaginacion' border=0  src='../../Recursos/imagenes/listado/ultimo.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/ultimo_gris.png' class='imgPaginacionDeshabilitada' /></td>";
    tmpPieRetorna += "</tr>";
    tmpPieRetorna += "<tr align='center' class='centrar'><td colspan='7' align='center' class='centrar'><span class='contador'> En total: </span><span class='contadorBorde'>" + Total + "</span><span class='contador'> registros</span><span class='contador'> y </span><span class='contadorBorde'>" + total + "</span><span class='contador'> Trámites por Autorizar.</span></td>";
    //    tmpPieRetorna += "<td colspan='7' align='center' class='centrar'><span class='contador'> En total </span><span class='contadorBorde'>" + Cantidad_Datos + "</span><span class='contador'> Trámites.</span></td></tr>";
    tmpPieRetorna += "</table>";


    return tmpPieRetorna;
}



function pieDePaginaListar_Uno_X_Uno(datos, inNomMetodo) {
    var PagAct = datos.PagAct;
    var PagSig = datos.PagSig;
    var PagAnt = datos.PagAnt;
    var PagUlt = datos.PagUlt;
    var Total = datos.Total / 10;
    var tmpPieRetorna = "";

    tmpPieRetorna = "<table class='centrar'><tr>";
    if (PagAct > 1)
        tmpPieRetorna += "<td><span onclick='" + inNomMetodo + "(1)' ><img class='imgPaginacionHabilitada' border='0' src='../../Recursos/imagenes/listado/inicio.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img class='imgPaginacionDeshabilitada' border='0' src='../../Recursos/imagenes/listado/inicio_gris.png' /></td>";

    if (PagAct != 1)
        tmpPieRetorna += "<td><span onclick=\"" + inNomMetodo + "('" + PagAnt + "')\" ><img class='imgPaginacionHabilitada' border=0 src='../../Recursos/imagenes/listado/anterior.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/anterior_gris.png' class='imgPaginacionDeshabilitada' /></td>";

    tmpPieRetorna += "<td><span class='paginasBorde'>" + PagAct + "</span></td><td><span class='paginas'> / </span></td><td><span class='paginasBorde'>" + PagUlt + "</span></td>";

    if (PagAct < PagUlt)
        tmpPieRetorna += "<td><span onclick=\"" + inNomMetodo + "('" + PagSig + "')\" ><img class='imgPaginacionHabilitada' class='imgPaginacion' border=0 src='../../Recursos/imagenes/listado/siguiente.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/siguiente_gris.png' class='imgPaginacionDeshabilitada' /></td>";

    if (PagAct != PagUlt)
        tmpPieRetorna += "<td><span onclick='" + inNomMetodo + "(" + PagUlt + ")' ><img  class='imgPaginacionHabilitada' class='imgPaginacion' border=0  src='../../Recursos/imagenes/listado/ultimo.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/ultimo_gris.png' class='imgPaginacionDeshabilitada' /></td>";
    tmpPieRetorna += "</tr>";
    tmpPieRetorna += "<tr align='center' class='centrar'><td colspan='7' align='center' class='centrar'><span class='contador'> En total </span><span class='contadorBorde'>" + Total + "</span><span class='contador'> registros.</span></td></tr>";
    tmpPieRetorna += "</tr></table>";
    return tmpPieRetorna;
}

function pieDePaginaListar_BuscarPaciente(datos, inNomMetodo) {
    var PagAct = datos.PagAct;
    var PagSig = datos.PagSig;
    var PagAnt = datos.PagAnt;
    var PagUlt = datos.PagUlt;
    var Total = datos.Total;
    var tmpPieRetorna = "";

    tmpPieRetorna = "<table class='centrar'><tr>";
    if (PagAct > 1)
        tmpPieRetorna += "<td><span onclick='" + inNomMetodo + "(1)' ><img class='imgPaginacionHabilitada' border='0' src='../../Recursos/imagenes/listado/inicio.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img class='imgPaginacionDeshabilitada' border='0' src='../../Recursos/imagenes/listado/inicio_gris.png' /></td>";

    if (PagAct != 1)
        tmpPieRetorna += "<td><span onclick=\"" + inNomMetodo + "('" + PagAnt + "')\" ><img class='imgPaginacionHabilitada' border=0 src='../../Recursos/imagenes/listado/anterior.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/anterior_gris.png' class='imgPaginacionDeshabilitada' /></td>";

    tmpPieRetorna += "<td><span class='paginasBorde'>" + PagAct + "</span></td><td><span class='paginas'> / </span></td><td><span class='paginasBorde'>" + PagUlt + "</span></td>";

    if (PagAct < PagUlt)
        tmpPieRetorna += "<td><span onclick=\"" + inNomMetodo + "('" + PagSig + "')\" ><img class='imgPaginacionHabilitada' class='imgPaginacion' border=0 src='../../Recursos/imagenes/listado/siguiente.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/siguiente_gris.png' class='imgPaginacionDeshabilitada' /></td>";

    if (PagAct != PagUlt)
        tmpPieRetorna += "<td><span onclick='" + inNomMetodo + "(" + PagUlt + ")' ><img  class='imgPaginacionHabilitada' class='imgPaginacion' border=0  src='../../Recursos/imagenes/listado/ultimo.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/ultimo_gris.png' class='imgPaginacionDeshabilitada' /></td>";
    tmpPieRetorna += "</tr>";
    //tmpPieRetorna += "<tr align='center' class='centrar'><td align='center' class='centrar tooltip'><span class='contador'> En total </span><span class='contadorBorde'>" + Total + "</span><td 'class='encabezado'>CANTIDAD PACIENTES NUEVOS</td><td class='cuerpoListado10'>" + nuevos+ "</td><span class='contador'> registros.</span></td></tr>";
    //
    //    tmpPieRetorna += "<tr align='center' class='centrar' onmouseover='mostrarDiv();' onmouseout='ocultarDiv();'><td align='center' class='centrar' colspan='7'><span class='contador'> En total </span><span class='contadorBorde' >" + Total + "</span><span class='contador'> registros.</span><span id='divPrueba'></span></td></tr>";

    //    tmpPieRetorna += "<tr align='center' class='centrar' onmouseover='holamundo();' onmouseout='holamundo2();'><td align='center' class='centrar' colspan='7'><span class='contador'> En total </span><span class='contadorBorde' >" + Total + "</span><span class='contador'> registros.</span><span id='divPrueba'></span></td></tr>";

    tmpPieRetorna += "<tr align='center' class='centrar'><td align='center' class='centrar' colspan='7'><span class='contador'> En total </span><span class='contadorBorde' >" + Total + "</span><span class='contador'> registros.</span><span id='divPrueba' style='position:fixed;'></span></td></tr>";

    //tmpPieRetorna += "<tr align='center' class='centrar' onmouseover='mostrarDiv();' onmouseout='ocultarDiv();'><td align='center' class='centrar' colspan='7'><span class='contador'> En total </span><span class='contadorBorde' >" + Total + "</span><span class='contador'> registros.</span><span id='divPrueba' style='position:fixed;'></span></td></tr>";

    tmpPieRetorna += "</tr></table>";
    return tmpPieRetorna;
}

function pieDePaginaListar(datos, inNomMetodo) {
    var PagAct = datos.PagAct;
    var PagSig = datos.PagSig;
    var PagAnt = datos.PagAnt;
    var PagUlt = datos.PagUlt;
    var Total = datos.Total;
    var tmpPieRetorna = "";

    tmpPieRetorna = "<table class='centrar'><tr>";
    if (PagAct > 1)
        tmpPieRetorna += "<td><span onclick='" + inNomMetodo + "(1)' ><img class='imgPaginacionHabilitada' border='0' src='../../Recursos/imagenes/listado/inicio.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img class='imgPaginacionDeshabilitada' border='0' src='../../Recursos/imagenes/listado/inicio_gris.png' /></td>";

    if (PagAct != 1)
        tmpPieRetorna += "<td><span onclick=\"" + inNomMetodo + "('" + PagAnt + "')\" ><img class='imgPaginacionHabilitada' border=0 src='../../Recursos/imagenes/listado/anterior.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/anterior_gris.png' class='imgPaginacionDeshabilitada' /></td>";

    tmpPieRetorna += "<td><span class='paginasBorde'>" + PagAct + "</span></td><td><span class='paginas'> / </span></td><td><span class='paginasBorde'>" + PagUlt + "</span></td>";

    if (PagAct < PagUlt)
        tmpPieRetorna += "<td><span onclick=\"" + inNomMetodo + "('" + PagSig + "')\" ><img class='imgPaginacionHabilitada' class='imgPaginacion' border=0 src='../../Recursos/imagenes/listado/siguiente.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/siguiente_gris.png' class='imgPaginacionDeshabilitada' /></td>";

    if (PagAct != PagUlt)
        tmpPieRetorna += "<td><span onclick='" + inNomMetodo + "(" + PagUlt + ")' ><img  class='imgPaginacionHabilitada' class='imgPaginacion' border=0  src='../../Recursos/imagenes/listado/ultimo.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/ultimo_gris.png' class='imgPaginacionDeshabilitada' /></td>";
    tmpPieRetorna += "</tr>";
    tmpPieRetorna += "<tr align='center' class='centrar'><td colspan='7' align='center' class='centrar'><span class='contador'> En total </span><span class='contadorBorde'>" + Total + "</span><span class='contador'> registros.</span></td></tr>";
    tmpPieRetorna += "</tr></table>";
    return tmpPieRetorna;
}

function pieDePaginaListar_Suma_Columna(datos, total, inNomMetodo) {
    var PagAct = datos.PagAct;
    var PagSig = datos.PagSig;
    var PagAnt = datos.PagAnt;
    var PagUlt = datos.PagUlt;
    var Total = datos.Total;
    var tmpPieRetorna = "";

    //  var Cantidad_Datos = datos.data[40];

    tmpPieRetorna = "<table class='centrar'><tr>";
    if (PagAct > 1)
        tmpPieRetorna += "<td><span onclick='" + inNomMetodo + "(1)' ><img class='imgPaginacionHabilitada' border='0' src='../../Recursos/imagenes/listado/inicio.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img class='imgPaginacionDeshabilitada' border='0' src='../../Recursos/imagenes/listado/inicio_gris.png' /></td>";

    if (PagAct != 1)
        tmpPieRetorna += "<td><span onclick=\"" + inNomMetodo + "('" + PagAnt + "')\" ><img class='imgPaginacionHabilitada' border=0 src='../../Recursos/imagenes/listado/anterior.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/anterior_gris.png' class='imgPaginacionDeshabilitada' /></td>";

    tmpPieRetorna += "<td><span class='paginasBorde'>" + PagAct + "</span></td><td><span class='paginas'> / </span></td><td><span class='paginasBorde'>" + PagUlt + "</span></td>";

    if (PagAct < PagUlt)
        tmpPieRetorna += "<td><span onclick=\"" + inNomMetodo + "('" + PagSig + "')\" ><img class='imgPaginacionHabilitada' class='imgPaginacion' border=0 src='../../Recursos/imagenes/listado/siguiente.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/siguiente_gris.png' class='imgPaginacionDeshabilitada' /></td>";

    if (PagAct != PagUlt)
        tmpPieRetorna += "<td><span onclick='" + inNomMetodo + "(" + PagUlt + ")' ><img  class='imgPaginacionHabilitada' class='imgPaginacion' border=0  src='../../Recursos/imagenes/listado/ultimo.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/ultimo_gris.png' class='imgPaginacionDeshabilitada' /></td>";
    tmpPieRetorna += "</tr>";
    tmpPieRetorna += "<tr align='center' class='centrar'><td colspan='7' align='center' class='centrar'><span class='contador'> En total: </span><span class='contadorBorde'>" + Total + "</span><span class='contador'> registros</span><span class='contador'> y </span><span class='contadorBorde'>" + total + "</span><span class='contador'> Trámites.</span></td>";
    //    tmpPieRetorna += "<td colspan='7' align='center' class='centrar'><span class='contador'> En total </span><span class='contadorBorde'>" + Cantidad_Datos + "</span><span class='contador'> Trámites.</span></td></tr>";
    tmpPieRetorna += "</table>";


    return tmpPieRetorna;
}

function pieDePaginaListar(datos, inNomMetodo) {
    var PagAct = datos.PagAct;
    var PagSig = datos.PagSig;
    var PagAnt = datos.PagAnt;
    var PagUlt = datos.PagUlt;
    var Total = datos.Total;
    var tmpPieRetorna = "";

    tmpPieRetorna = "<table class='centrar'><tr>";
    if (PagAct > 1)
        tmpPieRetorna += "<td><span onclick='" + inNomMetodo + "(1)' ><img class='imgPaginacionHabilitada' border='0' src='../../Recursos/imagenes/listado/inicio.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img class='imgPaginacionDeshabilitada' border='0' src='../../Recursos/imagenes/listado/inicio_gris.png' /></td>";

    if (PagAct != 1)
        tmpPieRetorna += "<td><span onclick=\"" + inNomMetodo + "('" + PagAnt + "')\" ><img class='imgPaginacionHabilitada' border=0 src='../../Recursos/imagenes/listado/anterior.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/anterior_gris.png' class='imgPaginacionDeshabilitada' /></td>";

    tmpPieRetorna += "<td><span class='paginasBorde'>" + PagAct + "</span></td><td><span class='paginas'> / </span></td><td><span class='paginasBorde'>" + PagUlt + "</span></td>";

    if (PagAct < PagUlt)
        tmpPieRetorna += "<td><span onclick=\"" + inNomMetodo + "('" + PagSig + "')\" ><img class='imgPaginacionHabilitada' class='imgPaginacion' border=0 src='../../Recursos/imagenes/listado/siguiente.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/siguiente_gris.png' class='imgPaginacionDeshabilitada' /></td>";

    if (PagAct != PagUlt)
        tmpPieRetorna += "<td><span onclick='" + inNomMetodo + "(" + PagUlt + ")' ><img  class='imgPaginacionHabilitada' class='imgPaginacion' border=0  src='../../Recursos/imagenes/listado/ultimo.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/ultimo_gris.png' class='imgPaginacionDeshabilitada' /></td>";
    tmpPieRetorna += "</tr>";
    tmpPieRetorna += "<tr align='center' class='centrar'><td colspan='7' align='center' class='centrar'><span class='contador'> En total </span><span class='contadorBorde'>" + Total + "</span><span class='contador'> registros.</span></td></tr>";
    tmpPieRetorna += "</tr></table>";
    return tmpPieRetorna;
}

function pieDePaginaListar_Suma_Columna(datos, total, inNomMetodo) {
    var PagAct = datos.PagAct;
    var PagSig = datos.PagSig;
    var PagAnt = datos.PagAnt;
    var PagUlt = datos.PagUlt;
    var Total = datos.Total;
    var tmpPieRetorna = "";

    //  var Cantidad_Datos = datos.data[40];

    tmpPieRetorna = "<table class='centrar'><tr>";
    if (PagAct > 1)
        tmpPieRetorna += "<td><span onclick='" + inNomMetodo + "(1)' ><img class='imgPaginacionHabilitada' border='0' src='../../Recursos/imagenes/listado/inicio.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img class='imgPaginacionDeshabilitada' border='0' src='../../Recursos/imagenes/listado/inicio_gris.png' /></td>";

    if (PagAct != 1)
        tmpPieRetorna += "<td><span onclick=\"" + inNomMetodo + "('" + PagAnt + "')\" ><img class='imgPaginacionHabilitada' border=0 src='../../Recursos/imagenes/listado/anterior.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/anterior_gris.png' class='imgPaginacionDeshabilitada' /></td>";

    tmpPieRetorna += "<td><span class='paginasBorde'>" + PagAct + "</span></td><td><span class='paginas'> / </span></td><td><span class='paginasBorde'>" + PagUlt + "</span></td>";

    if (PagAct < PagUlt)
        tmpPieRetorna += "<td><span onclick=\"" + inNomMetodo + "('" + PagSig + "')\" ><img class='imgPaginacionHabilitada' class='imgPaginacion' border=0 src='../../Recursos/imagenes/listado/siguiente.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/siguiente_gris.png' class='imgPaginacionDeshabilitada' /></td>";

    if (PagAct != PagUlt)
        tmpPieRetorna += "<td><span onclick='" + inNomMetodo + "(" + PagUlt + ")' ><img  class='imgPaginacionHabilitada' class='imgPaginacion' border=0  src='../../Recursos/imagenes/listado/ultimo.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/ultimo_gris.png' class='imgPaginacionDeshabilitada' /></td>";
    tmpPieRetorna += "</tr>";
    tmpPieRetorna += "<tr align='center' class='centrar'><td colspan='7' align='center' class='centrar'><span class='contador'> En total: </span><span class='contadorBorde'>" + Total + "</span><span class='contador'> registros</span><span class='contador'> y </span><span class='contadorBorde'>" + total + "</span><span class='contador'> Trámites.</span></td>";
    //    tmpPieRetorna += "<td colspan='7' align='center' class='centrar'><span class='contador'> En total </span><span class='contadorBorde'>" + Cantidad_Datos + "</span><span class='contador'> Trámites.</span></td></tr>";
    tmpPieRetorna += "</table>";


    return tmpPieRetorna;
}

function pieDePaginaListar_tabulacion(datos, inNomMetodo) {
    var PagAct = datos.PagAct;
    var PagSig = datos.PagSig;
    var PagAnt = datos.PagAnt;
    var PagUlt = datos.PagUlt;
    var Total = datos.Total;
    var tmpPieRetorna = "";

    tmpPieRetorna = "<table class='centrar'><tr>";
    if (PagAct > 1)

        tmpPieRetorna += "<td><div><input type='image' class='imgPaginacionHabilitada' border='0' src='../../Recursos/imagenes/listado/inicio.png'  onclick='" + inNomMetodo + "(1)'/></div></td>";

    //tmpPieRetorna += "<td><span onclick='" + inNomMetodo + "(1)' ><img class='imgPaginacionHabilitada' border='0' src='../../Recursos/imagenes/listado/inicio.png' /></span></td>";
    else
        tmpPieRetorna += "<td><div><input type='image' class='imgPaginacionDeshabilitada' border='0' src='../../Recursos/imagenes/listado/inicio_gris.png'/></div></td>";

    //tmpPieRetorna += "<td><img class='imgPaginacionDeshabilitada' border='0' src='../../Recursos/imagenes/listado/inicio_gris.png' /></td>";

    if (PagAct != 1)

        tmpPieRetorna += "<td><div><input type='image' class='imgPaginacionHabilitada' border='0' src='../../Recursos/imagenes/listado/anterior.png'  onclick=\"" + inNomMetodo + "('" + PagAnt + "')\"></div></td>";

    //tmpPieRetorna += "<td><span onclick=\"" + inNomMetodo + "('" + PagAnt + "')\" ><img class='imgPaginacionHabilitada' border=0 src='../../Recursos/imagenes/listado/anterior.png' /></span></td>";
    else

        tmpPieRetorna += "<td><div><input type='image' class='imgPaginacionDeshabilitada' border='0' src='../../Recursos/imagenes/listado/anterior_gris.png'/></div></td>";

    //tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/anterior_gris.png' class='imgPaginacionDeshabilitada' /></td>"; 

    //tmpPieRetorna += "<td><div><input type='image' class='paginasBorde' " + PagAct + "/><input type='image' class='paginasBorde' " + PagUlt + "/></div></td>";

    tmpPieRetorna += "<td><span class='paginasBorde'>" + PagAct + "</span></td><td><span class='paginas'> / </span></td><td><span class='paginasBorde'>" + PagUlt + "</span></td>";

    if (PagAct < PagUlt)
        tmpPieRetorna += "<td><div><input type='image' class='imgPaginacionHabilitada imgPaginacion' border='0' src='../../Recursos/imagenes/listado/siguiente.png'  onclick=\"" + inNomMetodo + "('" + PagSig + "')\"></div></td>";

    //tmpPieRetorna += "<td><span onclick=\"" + inNomMetodo + "('" + PagSig + "')\" ><img class='imgPaginacionHabilitada' class='imgPaginacion' border=0 src='../../Recursos/imagenes/listado/siguiente.png' /></span></td>";
    else
        tmpPieRetorna += "<td><div><input type='image' class='imgPaginacionDeshabilitada' border='0' src='../../Recursos/imagenes/listado/siguiente_gris.png'/></div></td>";

    //tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/siguiente_gris.png' class='imgPaginacionDeshabilitada' /></td>";

    if (PagAct != PagUlt)
        tmpPieRetorna += "<td><div><input type='image' class='imgPaginacionHabilitada imgPaginacion' border='0' src='../../Recursos/imagenes/listado/ultimo.png'  onclick=\"" + inNomMetodo + "('" + PagUlt + "')\"></div></td>";

    //tmpPieRetorna += "<td><span onclick='" + inNomMetodo + "(" + PagUlt + ")' ><img  class='imgPaginacionHabilitada' class='imgPaginacion' border=0  src='../../Recursos/imagenes/listado/ultimo.png' /></span></td>";
    else
        tmpPieRetorna += "<td><div><input type='image' class='imgPaginacionDeshabilitada' border='0' src='../../Recursos/imagenes/listado/ultimo_gris.png'/></div></td>";

    // tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/ultimo_gris.png' class='imgPaginacionDeshabilitada' /></td>";

    tmpPieRetorna += "</tr>";
    tmpPieRetorna += "<tr align='center' class='centrar'><td colspan='7' align='center' class='centrar'><span class='contador'> En total </span><span class='contadorBorde'>" + Total + "</span><span class='contador'> registros.</span></td></tr>";
    tmpPieRetorna += "</tr></table>";
    return tmpPieRetorna;
}





function mostrarDiv() {
    listarEstados();
    // document.getElementById("divPrueba").innerHTML = tabla;
    document.getElementById("divPrueba").style.display = "block";
}

function ocultarDiv() {
    document.getElementById("divPrueba").style.display = "none";

}

function listarEstados() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listarEstados'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, listarEstados_processResponse);
}

function listarEstados_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('divPrueba');
        if (res != "{'msj':0}") {
            var datosRows = info.data;
            var l = info.cols;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";
            var f = info.rows;
            var vacio = "--";
            var tabla = "<table class='listadoGeneral centrar' style='text-align: center; position:fixed; width:232px;'><tr><td class='encabezado' colspan='2'>CANTIDAD DE PACIENTES POR ESTADO</td></tr>";

            var id = info.data[0];
            var sin_estado = info.data[1];
            var nuevo = info.data[3];
            var acepta = info.data[5];
            var noCont_2_3 = info.data[7];
            var noCont_Sanitas = info.data[9];
            var actualizado = info.data[11];
            var noacepta = info.data[13];
            var dudosos = info.data[15];
            //var datos_errados = info.data[17];
            var fallecidos = info.data[17];
            var prepagada = info.data[19];
            var sin_cobertura = info.data[21];
            var total = info.data[23];



            //if (ctl) {
            //    claseAplicar = "cuerpoListado9";
            //    claseAplicar2 = "cuerpoListado3";
            //} else {
            //    claseAplicar = "cuerpoListado10";
            //    claseAplicar2 = "cuerpoListado5";
            //}

            //ctl = !ctl;
            //  tabla += '<tr>';


            tabla += "<tr><td class='encabezado' >SIN ESTADO</td><td class='cuerpoListado3' style='width:87px;'>" + ((sin_estado == "") ? vacio : unescape(sin_estado)) + "</td></tr>";
            tabla += "<tr><td class='encabezado' >NUEVO</td><td class='cuerpoListado3' style='width:87px;'>" + ((nuevo == "") ? vacio : unescape(nuevo)) + "</td></tr>";
            tabla += "<tr><td class='encabezado' >ACEPTA</td><td class='cuerpoListado3' style='width:87px;'>" + ((acepta == "") ? vacio : unescape(acepta)) + "</td></tr>";
            tabla += "<tr><td class='encabezado' >NO CONTACTABLE 2DA Y 3RA</td><td class='cuerpoListado3' style='width:87px;'>" + ((noCont_2_3 == "") ? vacio : unescape(noCont_2_3)) + "</td></tr>";
            tabla += "<tr><td class='encabezado' >NO CONTACTABLE SANITAS</td><td class='cuerpoListado3' style='width:87px;'>" + ((noCont_Sanitas == "") ? vacio : unescape(noCont_Sanitas)) + "</td></tr>";
            tabla += "<tr><td class='encabezado' >ACTUALIZADOS</td><td class='cuerpoListado3' style='width:87px;'>" + ((actualizado == "") ? vacio : unescape(actualizado)) + "</td></tr>";
            tabla += "<tr><td class='encabezado' >NO ACEPTA</td><td class='cuerpoListado3' style='width:87px;'>" + ((noacepta == "") ? vacio : unescape(noacepta)) + "</td></tr>";
            tabla += "<tr><td class='encabezado' >DUDOSOS</td><td class='cuerpoListado3' style='width:87px;'>" + ((dudosos == "") ? vacio : unescape(dudosos)) + "</td></tr>";
            //tabla += "<tr><td class='encabezado' >DATOS ERRADOS</td><td class='cuerpoListado3' style='width:87px;' >" + ((datos_errados == "") ? vacio : unescape(datos_errados)) + "</td></tr>";
            tabla += "<tr><td class='encabezado' >FALLECIDOS</td><td class='cuerpoListado3' style='width:87px;' >" + ((fallecidos == "") ? vacio : unescape(fallecidos)) + "</td></tr>";
            tabla += "<tr><td class='encabezado' >PREPAGADA</td><td class='cuerpoListado3' style='width:87px;' >" + ((prepagada == "") ? vacio : unescape(prepagada)) + "</td></tr>";
            tabla += "<tr><td class='encabezado' >SIN COBERTURA</td><td class='cuerpoListado3' style='width:87px;' >" + ((sin_cobertura == "") ? vacio : unescape(sin_cobertura)) + "</td></tr>";
            tabla += "<tr><td class='encabezado' >TOTAL</td><td class='cuerpoListado3' style='width:87px;' >" + ((total == "") ? vacio : unescape(total)) + "</td></tr>";

            // tabla += '</tr>';

            tabla += '</table>';
            divTerceros.innerHTML = tabla;
            var idMenuForm = document.getElementById('idMenuForm').innerHTML; // se adiciona esta linea para que se de  el permiso de visualizar el editar despues de cargar
            permisosParaMenu(idMenuForm); // se adiciona esta linea para que se el permiso de visualizar el editar despues de cargar
        } else {
            divTerceros.innerHTML = mensajecero;
        }
    } catch (elError) {
    }
    //  $.fancybox.close();
}

function ContadoresOrdenes() {

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'ContadorOrdenes'));
    arrayParameters.push(newArg('id', DocGlobal));
    arrayParameters.push(newArg('id_espec', idEspeGlobal));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, ContadoresOrdenes_processResponse);
    muestraVentanaProgreso('Cargando ...');
}

function ContadoresOrdenes_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana('SIN CONEXIÓN A LA BASE DE DATOS.');
                break;
            case 0:
                //  muestraVentana('NO EXISTEN TIPOS DE DOCUMENTOS');
                break;
            case 1:
                document.getElementById("cont1").innerHTML = info.data[0];
                document.getElementById("cont2").innerHTML = info.data[1];
                document.getElementById("cont3").innerHTML = info.data[2];
                document.getElementById("cont4").innerHTML = info.data[3];
                break;
        }
    } catch (elError) { }
}

function ContadorResultado() {

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'ContadorResultado'));
    arrayParameters.push(newArg('id', DocGlobal));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, ContadorResultado_processResponse);
    muestraVentanaProgreso('Cargando ...');
}

function ContadorResultado_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana('SIN CONEXIÓN A LA BASE DE DATOS.');
                break;
            case 0:
                //  muestraVentana('NO EXISTEN TIPOS DE DOCUMENTOS');
                break;
            case 1:
                document.getElementById("cont5").innerHTML = info.data[0];
                document.getElementById("cont6").innerHTML = info.data[1];
                document.getElementById("cont7").innerHTML = info.data[2];
                break;
        }
    } catch (elError) { }
}

function pieDePaginaListar_OrdenesMedicas(datos, inNomMetodo) {
    ContadoresOrdenes();
    var PagAct = datos.PagAct;
    var PagSig = datos.PagSig;
    var PagAnt = datos.PagAnt;
    var PagUlt = datos.PagUlt;
    var Total = datos.Total;
    var tmpPieRetorna = "";

    tmpPieRetorna = "<table class='centrar'><tr class='centrar'><td colspan='10'><div><table class='centrar'><tr>";
    if (PagAct > 1)
        tmpPieRetorna += "<td  align='center' class='centrar'><span onclick='" + inNomMetodo + "(1)' ><img class='imgPaginacionHabilitada' border='0' src='../../Recursos/imagenes/listado/inicio.png' /></span></td>";

    else
        tmpPieRetorna += "<td align='center' class='centrar'><img class='imgPaginacionDeshabilitada' border='0' src='../../Recursos/imagenes/listado/inicio_gris.png' /></td>";

    if (PagAct != 1)
        tmpPieRetorna += "<td align='center' class='centrar'><span onclick=\"" + inNomMetodo + "('" + PagAnt + "')\" ><img class='imgPaginacionHabilitada' border=0 src='../../Recursos/imagenes/listado/anterior.png' /></span></td>";
    else
        tmpPieRetorna += "<td align='center' class='centrar'><img border=0 src='../../Recursos/imagenes/listado/anterior_gris.png' class='imgPaginacionDeshabilitada' /></td>";

    tmpPieRetorna += "<td  align='center' class='centrar'><span class='paginasBorde'>" + PagAct + "</span></td><td><span class='paginas'> / </span></td><td><span class='paginasBorde'>" + PagUlt + "</span></td>";

    if (PagAct < PagUlt)
        tmpPieRetorna += "<td  align='center' class='centrar'><span onclick=\"" + inNomMetodo + "('" + PagSig + "')\" ><img class='imgPaginacionHabilitada' class='imgPaginacion' border=0 src='../../Recursos/imagenes/listado/siguiente.png' /></span></td>";
    else
        tmpPieRetorna += "<td  align='center' class='centrar'><img border=0 src='../../Recursos/imagenes/listado/siguiente_gris.png' class='imgPaginacionDeshabilitada' /></td>";

    if (PagAct != PagUlt)
        tmpPieRetorna += "<td  align='center' class='centrar'><span onclick='" + inNomMetodo + "(" + PagUlt + ")' ><img  class='imgPaginacionHabilitada' class='imgPaginacion' border=0  src='../../Recursos/imagenes/listado/ultimo.png' /></span></td>";
    else
        tmpPieRetorna += "<td  align='center' class='centrar'><img border=0 src='../../Recursos/imagenes/listado/ultimo_gris.png' class='imgPaginacionDeshabilitada' /></td>";
    tmpPieRetorna += "</tr>";
    //tmpPieRetorna += "<tr align='center' class='centrar'><td align='center' class='centrar tooltip'><span class='contador'> En total </span><span class='contadorBorde'>" + Total + "</span><td 'class='encabezado'>CANTIDAD PACIENTES NUEVOS</td><td class='cuerpoListado10'>" + nuevos+ "</td><span class='contador'> registros.</span></td></tr>";

    //    tmpPieRetorna += "<tr align='center' class='centrar' onmouseover='mostrarDiv();' onmouseout='ocultarDiv();'><td align='center' class='centrar' colspan='7'><span class='contador'> En total </span><span class='contadorBorde' >" + Total + "</span><span class='contador'> registros.</span><span id='divPrueba'></span></td></tr>";

    //    tmpPieRetorna += "<tr align='center' class='centrar' onmouseover='holamundo();' onmouseout='holamundo2();'><td align='center' class='centrar' colspan='7'><span class='contador'> En total </span><span class='contadorBorde' >" + Total + "</span><span class='contador'> registros.</span><span id='divPrueba'></span></td></tr>";

    tmpPieRetorna += "<tr align='center' class='centrar'><td align='center' class='centrar' colspan='10'><span class='contador'> En total </span><span class='contadorBorde' >" + Total + "</span><span class='contador'> registros.</span></td><br/></tr>";

    tmpPieRetorna += "</tr></table></div></td></tr>"
    //tmpPieRetorna += "<tr><td colspan='7' style='font-weight: bold;'>Cantidad de órdenes<br/>en Gestión<br/><label id='cont1' style='font-size: 18px;'>0</label></td><td></td><td colspan='7' style='font-weight: bold;'>Cantidad de órdenes<br/>Gestionadas<br/><label id='cont2' style='font-size: 18px;'>0</label></td><td></td><td colspan='7' style='font-weight: bold;'>Cantidad de órdenes<br/>Rechazadas<br/><label id='cont3' style='font-size: 18px;'>0</label><td></td><td style='font-weight: bold;'>Cantidad de órdenes<br/>en Análisis<br/><label id='cont4' style='font-size: 18px;'>0</label></td></td></tr>";
    tmpPieRetorna += "<tr><td colspan='10'><div><table class='centrar'><tr><td style='font-weight: bold;'>Cantidad de órdenes<br/>en Gestión<br/><label id='cont1' style='font-size: 18px;'>0</label></td><td>&nbsp;</td><td style='font-weight: bold;'>Cantidad de órdenes<br/>Gestionadas<br/><label id='cont2' style='font-size: 18px;'>0</label></td><td>&nbsp;</td><td style='font-weight: bold;'>Cantidad de órdenes<br/>Rechazadas<br/><label id='cont3' style='font-size: 18px;'>0</label><td>&nbsp;</td><td style='font-weight: bold;'>Cantidad de órdenes<br/>en Análisis<br/><label id='cont4' style='font-size: 18px;'>0</label></td></td></tr></table></div></td></tr>"
    tmpPieRetorna += "</tr></table>";
    return tmpPieRetorna;
}

function pieDePaginaListar_Resultados(datos, inNomMetodo) {
    ContadorResultado();
    var PagAct = datos.PagAct;
    var PagSig = datos.PagSig;
    var PagAnt = datos.PagAnt;
    var PagUlt = datos.PagUlt;
    var Total = datos.Total;
    var tmpPieRetorna = "";

    tmpPieRetorna = "<table class='centrar'><tr>";
    if (PagAct > 1)
        tmpPieRetorna += "<td><span onclick='" + inNomMetodo + "(1)' ><img class='imgPaginacionHabilitada' border='0' src='../../Recursos/imagenes/listado/inicio.png' /></span></td>";

    else
        tmpPieRetorna += "<td><img class='imgPaginacionDeshabilitada' border='0' src='../../Recursos/imagenes/listado/inicio_gris.png' /></td>";

    if (PagAct != 1)
        tmpPieRetorna += "<td><span onclick=\"" + inNomMetodo + "('" + PagAnt + "')\" ><img class='imgPaginacionHabilitada' border=0 src='../../Recursos/imagenes/listado/anterior.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/anterior_gris.png' class='imgPaginacionDeshabilitada' /></td>";

    tmpPieRetorna += "<td><span class='paginasBorde'>" + PagAct + "</span></td><td><span class='paginas'> / </span></td><td><span class='paginasBorde'>" + PagUlt + "</span></td>";

    if (PagAct < PagUlt)
        tmpPieRetorna += "<td><span onclick=\"" + inNomMetodo + "('" + PagSig + "')\" ><img class='imgPaginacionHabilitada' class='imgPaginacion' border=0 src='../../Recursos/imagenes/listado/siguiente.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/siguiente_gris.png' class='imgPaginacionDeshabilitada' /></td>";

    if (PagAct != PagUlt)
        tmpPieRetorna += "<td><span onclick='" + inNomMetodo + "(" + PagUlt + ")' ><img  class='imgPaginacionHabilitada' class='imgPaginacion' border=0  src='../../Recursos/imagenes/listado/ultimo.png' /></span></td>";
    else
        tmpPieRetorna += "<td><img border=0 src='../../Recursos/imagenes/listado/ultimo_gris.png' class='imgPaginacionDeshabilitada' /></td>";
    tmpPieRetorna += "</tr>";
    //tmpPieRetorna += "<tr align='center' class='centrar'><td align='center' class='centrar tooltip'><span class='contador'> En total </span><span class='contadorBorde'>" + Total + "</span><td 'class='encabezado'>CANTIDAD PACIENTES NUEVOS</td><td class='cuerpoListado10'>" + nuevos+ "</td><span class='contador'> registros.</span></td></tr>";

    //    tmpPieRetorna += "<tr align='center' class='centrar' onmouseover='mostrarDiv();' onmouseout='ocultarDiv();'><td align='center' class='centrar' colspan='7'><span class='contador'> En total </span><span class='contadorBorde' >" + Total + "</span><span class='contador'> registros.</span><span id='divPrueba'></span></td></tr>";

    //    tmpPieRetorna += "<tr align='center' class='centrar' onmouseover='holamundo();' onmouseout='holamundo2();'><td align='center' class='centrar' colspan='7'><span class='contador'> En total </span><span class='contadorBorde' >" + Total + "</span><span class='contador'> registros.</span><span id='divPrueba'></span></td></tr>";

    tmpPieRetorna += "<tr align='center' class='centrar'><td align='center' class='centrar'  colspan='7'><span class='contador'> En total </span><span class='contadorBorde' >" + Total + "</span><span class='contador'> registros.</span></td><br/></tr>";
    // tmpPieRetorna += "<tr><td style='font-weight: bold;'>Laboratorio<br/><label id='cont5' style='font-size: 18px;'>0</label></td><td></td><td style='font-weight: bold;'>Procedimientos Quirurgicos<br/><label id='cont6' style='font-size: 18px;'>0</label></td><td></td><td style='font-weight: bold;'>Exámen Diagnóstico<br/><label id='cont7' style='font-size: 18px;'>0</label><td></td></td></tr>";
    tmpPieRetorna += "<tr><td colspan='7'><div><table class='centrar'><tr><td style='font-weight: bold;'>Laboratorio<br/><label id='cont5' style='font-size: 18px;'>0</label></td><td>&nbsp;&nbsp;</td><td></td><td style='font-weight: bold;'>Procedimientos<br/>Quirurgicos<br/><label id='cont6' style='font-size: 18px;'>0</label></td><td>&nbsp;&nbsp;</td><td style='font-weight: bold;'>Exámen<br/>Diagnóstico<br/><label id='cont7' style='font-size: 18px;'>0</label></td></tr></table></div></td></tr>"
    tmpPieRetorna += "</tr></table>";
    return tmpPieRetorna;
}


function ContadorLlamadas() {

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'conteoPacientes'));
    arrayParameters.push(newArg('fechaInicio', globalFechaInicio));
    arrayParameters.push(newArg('fechaFin', globalFechaFin));
    arrayParameters.push(newArg('bandera', banderaGlobal));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, ContadorLlamadas_processResponse);
    muestraVentanaProgreso('Cargando ...');
}

function ContadorLlamadas_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana('SIN CONEXIÓN A LA BASE DE DATOS.');
                break;
            case 0:
                //  muestraVentana('NO EXISTEN TIPOS DE DOCUMENTOS');
                break;
            case 1:
                document.getElementById("cont1").innerHTML = info.data[2];
                document.getElementById("cont2").innerHTML = info.data[6];
                document.getElementById("cont3").innerHTML = info.data[10];
                break;
        }
    } catch (elError) { }
}

function pieDePaginaListar_TramitesPrioridad(datos, bandera, inNomMetodo) {
    banderaGlobal = bandera;
    ContadorLlamadas();
    var PagAct = datos.PagAct;
    var PagSig = datos.PagSig;
    var PagAnt = datos.PagAnt;
    var PagUlt = datos.PagUlt;
    var Total = datos.Total;
    var tmpPieRetorna = "";

    tmpPieRetorna = "<table class='centrar'><tr class='centrar'><td colspan='10'><div><table class='centrar'><tr>";
    if (PagAct > 1)
        tmpPieRetorna += "<td  align='center' class='centrar'><span onclick='" + inNomMetodo + "(1)' ><img class='imgPaginacionHabilitada' border='0' src='../../Recursos/imagenes/listado/inicio.png' /></span></td>";

    else
        tmpPieRetorna += "<td align='center' class='centrar'><img class='imgPaginacionDeshabilitada' border='0' src='../../Recursos/imagenes/listado/inicio_gris.png' /></td>";

    if (PagAct != 1)
        tmpPieRetorna += "<td align='center' class='centrar'><span onclick=\"" + inNomMetodo + "('" + PagAnt + "')\" ><img class='imgPaginacionHabilitada' border=0 src='../../Recursos/imagenes/listado/anterior.png' /></span></td>";
    else
        tmpPieRetorna += "<td align='center' class='centrar'><img border=0 src='../../Recursos/imagenes/listado/anterior_gris.png' class='imgPaginacionDeshabilitada' /></td>";

    tmpPieRetorna += "<td  align='center' class='centrar'><span class='paginasBorde'>" + PagAct + "</span></td><td><span class='paginas'> / </span></td><td><span class='paginasBorde'>" + PagUlt + "</span></td>";

    if (PagAct < PagUlt)
        tmpPieRetorna += "<td  align='center' class='centrar'><span onclick=\"" + inNomMetodo + "('" + PagSig + "')\" ><img class='imgPaginacionHabilitada' class='imgPaginacion' border=0 src='../../Recursos/imagenes/listado/siguiente.png' /></span></td>";
    else
        tmpPieRetorna += "<td  align='center' class='centrar'><img border=0 src='../../Recursos/imagenes/listado/siguiente_gris.png' class='imgPaginacionDeshabilitada' /></td>";

    if (PagAct != PagUlt)
        tmpPieRetorna += "<td  align='center' class='centrar'><span onclick='" + inNomMetodo + "(" + PagUlt + ")' ><img  class='imgPaginacionHabilitada' class='imgPaginacion' border=0  src='../../Recursos/imagenes/listado/ultimo.png' /></span></td>";
    else
        tmpPieRetorna += "<td  align='center' class='centrar'><img border=0 src='../../Recursos/imagenes/listado/ultimo_gris.png' class='imgPaginacionDeshabilitada' /></td>";
    tmpPieRetorna += "</tr>";
    //tmpPieRetorna += "<tr align='center' class='centrar'><td align='center' class='centrar tooltip'><span class='contador'> En total </span><span class='contadorBorde'>" + Total + "</span><td 'class='encabezado'>CANTIDAD PACIENTES NUEVOS</td><td class='cuerpoListado10'>" + nuevos+ "</td><span class='contador'> registros.</span></td></tr>";

    //    tmpPieRetorna += "<tr align='center' class='centrar' onmouseover='mostrarDiv();' onmouseout='ocultarDiv();'><td align='center' class='centrar' colspan='7'><span class='contador'> En total </span><span class='contadorBorde' >" + Total + "</span><span class='contador'> registros.</span><span id='divPrueba'></span></td></tr>";

    //    tmpPieRetorna += "<tr align='center' class='centrar' onmouseover='holamundo();' onmouseout='holamundo2();'><td align='center' class='centrar' colspan='7'><span class='contador'> En total </span><span class='contadorBorde' >" + Total + "</span><span class='contador'> registros.</span><span id='divPrueba'></span></td></tr>";

    tmpPieRetorna += "<tr align='center' class='centrar'><td align='center' class='centrar' colspan='10'><span class='contador'> En total </span><span class='contadorBorde' >" + Total + "</span><span class='contador'> registros.</span></td><br/></tr>";

    tmpPieRetorna += "</tr></table></div></td></tr>"
    //tmpPieRetorna += "<tr><td colspan='7' style='font-weight: bold;'>Cantidad de órdenes<br/>en Gestión<br/><label id='cont1' style='font-size: 18px;'>0</label></td><td></td><td colspan='7' style='font-weight: bold;'>Cantidad de órdenes<br/>Gestionadas<br/><label id='cont2' style='font-size: 18px;'>0</label></td><td></td><td colspan='7' style='font-weight: bold;'>Cantidad de órdenes<br/>Rechazadas<br/><label id='cont3' style='font-size: 18px;'>0</label><td></td><td style='font-weight: bold;'>Cantidad de órdenes<br/>en Análisis<br/><label id='cont4' style='font-size: 18px;'>0</label></td></td></tr>";
    tmpPieRetorna += "<tr><td colspan='10'><div><table class='centrar'><tr><td style='font-weight: bold;'>Cantidad de llamadas<br/>con prioridad Alta<br/><label id='cont1' style='font-size: 18px;'>0</label></td><td>&nbsp;</td><td style='font-weight: bold;'>Cantidad de llamadas<br/>con prioridad Media<br/><label id='cont2' style='font-size: 18px;'>0</label></td><td>&nbsp;</td><td style='font-weight: bold;'>Cantidad de llamadas<br/>con prioridad Baja<br/><label id='cont3' style='font-size: 18px;'>0</label><td>&nbsp;</td></td></tr></table></div></td>"
    tmpPieRetorna += "</tr></table>";
    return tmpPieRetorna;
}
















function confirmaEliminar(id) {
    globalEliminar = id;
    $.fancybox({
        'showCloseButton': true,
        'transitionIn': 'fade',
        'scrolling': 'no',
        'href': '#confirmaEliminar'
    });
}

/* Cierra las ventanas emergentes hechas con Fancybox */
function cerrarVentanaEmergente() {
    $.fancybox.close();
}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//onkeypress = "return validarCaracteres(event,this.id)"

function validarCaracteres(e, id) {
    k = (document.all) ? e.keyCode : e.which;
    if (k == 13) {
        document.getElementById(id).value = document.getElementById(id).value + "\n"
    }
}

/////////////////////////////////////////////////////////////////////////////////
function llenarSelectResponsable(json, combo, idSel) {
    try {
        limpiarSelect(combo);
        var info = eval('(' + json + ')');
        var opt = null;
        for (var i = 0; i < info.data.length; i += 3) {
            opt = new Option(info.data[i + 1], info.data[i])
            if (info.data[i] == idSel) {
                opt.selected = true;
            }
            combo.options[combo.length] = opt;
        }
    } catch (elError) {
    }
}


///////////////////////////////////////////////////////////////////////
function llenarSelectConSeleccionResponsable(json, combo, idSel) {
    try {
        limpiarSelectConSeleccionResponsable(combo);
        var info = eval('(' + json + ')');
        var opt = null;
        for (var i = 0; i < info.data.length; i += 3) {
            opt = new Option(info.data[i + 1], info.data[i])
            if (info.data[i] == idSel) {
                opt.selected = true;
            }
            combo.options[combo.length] = opt;
        }
    } catch (elError) {
    }
}
function limpiarSelectConSeleccionResponsable(combo) {
    try {
        while (combo.length > 0) {
            combo.remove(combo.length - 1);
        }
    } catch (elError) {
    }
    combo.options[0] = new Option('-- SELECCIONE --', '-1');
}
/////////////////////////////////////////////////////////////////////
function llenarSelectConSeleccion(json, combo, idSel) {
    try {
        limpiarSelectConSeleccion(combo);
        var info = eval('(' + json + ')');
        var opt = null;
        for (var i = 0; i < info.data.length; i += 2) {
            opt = new Option(info.data[i + 1], info.data[i])
            if (info.data[i] == idSel) {
                opt.selected = true;
            }
            combo.options[combo.length] = opt;
        }
    } catch (elError) {
    }
}

function limpiarSelectConSeleccion(combo) {
    try {
        while (combo.length > 0) {
            combo.remove(combo.length - 1);
        }
    } catch (elError) {
    }
    combo.options[0] = new Option('-- SELECCIONE --', '-1');
}
/////////////////////////////////////////////////////////////////////

function formatoNumero(numero, decimales, separador_decimal, separador_miles) { // v2007-08-06
    numero = parseFloat(numero);
    if (isNaN(numero)) {
        return "";
    }

    if (decimales !== undefined) {
        // Redondeamos
        numero = numero.toFixed(decimales);
    }

    // Convertimos el punto en separador_decimal
    numero = numero.toString().replace(".", separador_decimal !== undefined ? separador_decimal : ",");

    if (separador_miles) {
        // Añadimos los separadores de miles
        var miles = new RegExp("(-?[0-9]+)([0-9]{3})");
        while (miles.test(numero)) {
            numero = numero.replace(miles, "$1" + separador_miles + "$2");
        }
    }

    return numero;
}

function muestraDiv() {
    var msj = document.getElementById("lblMsj").innerHTML;
    if (msj == 'Ocultar') {
        document.getElementById("divMenu").style.display = "none";
        document.getElementById("lblMsj").innerHTML = 'Mostrar';
        //document.getElementById("secundario").className = "secundarioOculto"
        document.getElementById("imagenIzq").style.display = "none";
        document.getElementById("imagenDer").style.display = "block";
    }
    else if (msj == 'Mostrar') {
        document.getElementById("divMenu").style.display = "block";
        document.getElementById("lblMsj").innerHTML = 'Ocultar';
        //document.getElementById("secundario").className = "secundarioActivo"
        document.getElementById("imagenIzq").style.display = "block";
        document.getElementById("imagenDer").style.display = "none";
    }
}

var idFechaGlobal;

function ValidaFecha(fecha, id) {

    idFechaGlobal = id;
    if (fecha != '') {
        var arrayparameters = new Array();
        arrayparameters.push(newArg('p', 'Fecha_validar'));
        arrayparameters.push(newArg('fecha', fecha));
        var send = arrayparameters.join('&');
        $.post('../../Controlador/ctlGeneral.aspx', send, ValidaFecha_processResponse);
    }
}

function ValidaFecha_processResponse(res) {
    try {
        var info = eval('(' + res + ')');

        switch (info) {
            case 0:
                muestraVentana("FORMATO DE FECHA INCORRECTO");
                document.getElementById(idFechaGlobal).value = "";
                break;

        }
    } catch (elError) {
    }
}

function OpenFancy(div) {
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'enableEscapeButton': false,
        'href': div
    });
}