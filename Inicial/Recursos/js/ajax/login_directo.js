var caracter = new Array("0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z");

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////LOGIN USUARIO///////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


/*********************************************************
METODO PARA INGRESAR AL SISTEMA
***********************************************************/

function ingresar() {
    var usu = document.getElementById('usuario').value;
    var cla = document.getElementById('clave').value;

    if ((usu != '') && (cla != '')) {
        muestraVentanaProgreso('Identificando...');
        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', 'logueaUsuario'));
        arrayParameters.push(newArg('usuario', usu));
        arrayParameters.push(newArg('clave', cla));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlLoginDirecto.aspx', send, Ingresar_processResponse);
    } else {
        muestraVentana(mensajeobligatorios);
    }
}

var estado = "";
function Ingresar_processResponse(res) {
    try {
        var info = eval("(" + res + ")");
        var msj = info.msj;
        ocultaVentanaProgreso();
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana('Datos Invalidos.');
                document.getElementById('clave').value = '';
                break;
            case 1:
                estado = info.data[4];
                if (estado != 2) {
                    var arrayParameters = new Array();
                    arrayParameters.push(newArg('p', 'crearSesion'));
                    arrayParameters.push(newArg('usu', info.data[0]));
                    arrayParameters.push(newArg('nom', info.data[1]));
                    arrayParameters.push(newArg('mail', info.data[2]));
                    arrayParameters.push(newArg('nit', info.data[3]));
                    var send = arrayParameters.join('&');
                    $.post('../../Controlador/ctlLoginDirecto.aspx', send, crearSesion_processResponse);
                } else {
                    muestraVentana("Su cuenta se encuentra deshabilitada! Comuniquese con el administrador");
                }
                break;
        }
    } catch (elError) {
    }
}

/*************************************************************
METODO QUE REDIRECCIONA DESPUES DE HABERSE CREADO LA SESION
**************************************************************/
function crearSesion_processResponse(res) {
    try {
        switch (estado) {
            case '0':
                capturarIps();
                location.href = "../general/cambio_clave.aspx";
                break;
            case '1':
                capturarIps();
                location.href = "../general/inicio.aspx";
                break;
        }
    } catch (elError) {
    }
}

function capturarIps() {

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'capturarIps'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, capturarIps_processResponse);

}

function capturarIps_processResponse(res) {
    try {
        var info = eval('(' + res + ')');

        switch (info) {
            case 2:
                location.href = "../../vista/general/login.aspx";
                break;
            case 3:
                location.href = "../../vista/general/inicio.aspx";
                break;
        }
    } catch (elError) {
    }
}


/**********************************************************
METODO AUTOMATICO QUE DETECTA NAVEGADOR
***********************************************************/
BrowserDetect.init();
$(document).ready(function () { muestraNavegadores(BrowserDetect.browser, BrowserDetect.version); });


/**********************************************************
METODO QUE MUESTRA MENSAJES SEGUN LA VERSION DEL NAVEGADOR
***********************************************************/

function muestraNavegadores(navegador, version) {
    var ctl = true;
    switch (navegador) {
        case 'Chrome':
            if (version < 10) {
                muestraVentana('Debe actualizar su navegador a Chrome 10+.');
                ctl = false;
            }
            break

        case 'Explorer':
            if (version < 9) {
                muestraVentana('Debe actualizar su navegador a IE 9.');
                ctl = false;
            }
            break;

        case 'Apple':
            if (version < 5) {
                muestraVentana('Debe actualizar su navegador a Safari 5+.');
                ctl = false;
            }
            break;

        case 'Firefox':
            if (version < 4) {
                muestraVentana('Debe actualizar su navegador a Mozilla Firefox 4+.');
                ctl = false;
            }
            break;
    }
    if (ctl) {
        document.getElementById('tablaNavegadores').style.display = 'none';
    }
}

function loginFormulario() {
    document.getElementById("loginFormulario").style.display = "block";
}