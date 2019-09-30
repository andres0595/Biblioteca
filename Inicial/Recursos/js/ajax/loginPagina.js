///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////// VARIABLES GLOBALES ////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

var caracter = new Array("0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z");

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////// FUNCIONES AUTOMATICAS /////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

$(document).ready(function () {
    $('#usuario').focus();
});

function recuperaClave() {
    $.fancybox({
        'transitionIn': 'none',
        'transitionOut': 'none',
        'modal': true,
        'href': '#divOlvideClave'
    });
}


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////LOGIN USUARIO///////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

/*********************************************************
METODO PARA INGRESAR AL SISTEMA
***********************************************************/

function ingresar() {
    var usu = document.getElementById('usuario').value;
    var cla = md5(document.getElementById('clave').value);

    if ((usu != '') && (cla != '')) {
        muestraVentanaProgreso('Identificando...');
        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', 'loguear'));
        arrayParameters.push(newArg('usuario', usu));
        arrayParameters.push(newArg('clave', cla));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlLogin.aspx', send, Ingresar_processResponse);
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
                    arrayParameters.push(newArg('p', 'sesion'));
                    arrayParameters.push(newArg('usu', info.data[0]));
                    arrayParameters.push(newArg('nom', info.data[1]));
                    arrayParameters.push(newArg('mail', info.data[2]));
                    arrayParameters.push(newArg('nit', info.data[3]));
                    arrayParameters.push(newArg('key', info.data[5]));

                    if (info.data[3] == 'NIT') {
                        estado = '3'; //Es Director
                    }

                    var send = arrayParameters.join('&');
                    $.post('../../Controlador/ctlLogin.aspx', send, crearSesion_processResponse);
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
                openInNewTab("http://aplicaciones.wdvisual.co/crm/vista/cambio_clave.aspx.aspx");
                document.getElementById("usuario").value = "";
                document.getElementById("clave").value = "";
                break;
            case '1':
                openInNewTab("http://aplicaciones.wdvisual.co/crm/vista/general/inicio.aspx");
                document.getElementById("usuario").value = "";
                document.getElementById("clave").value = "";
                break;
            case '3':
                openInNewTab("http://aplicaciones.wdvisual.co/crm/vista/general/elegirEmpresa.aspx");
                document.getElementById("usuario").value = "";
                document.getElementById("clave").value = "";
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
            break;

        case 'Explorer':
            if (version < 9) {
                muestraVentana('Debe actualizar su navegador a IE 9.');
                ctl = false;
            }
            break

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


/**********************************************************
METODO QUE PERMITE GENERAR UNA CLAVE AUTOMATICA
***********************************************************/
function generarClave() {
    var i = 0;
    var clave = '';
    while (i < 10) {
        clave += caracter[(Math.round(Math.random() * 35))];
        i++;
    }
    return clave;
}

function loginFormulario() {
    document.getElementById("loginFormulario").style.display = "block";
}


function recuperarClave() {
    var email = $('#email').val();
    if (email != '') {
        if (vemail(email)) {
            var cla = generarClave();
            var claE = md5(cla);
            muestraVentanaProgreso();
            var arrayParameters = new Array();
            arrayParameters.push(newArg('p', 'solicitaClave'));
            arrayParameters.push(newArg('email', email));
            arrayParameters.push(newArg('clave', cla));
            arrayParameters.push(newArg('claveE', claE));
            var send = arrayParameters.join('&');
            $.post('../../Controlador/ctlLogin.aspx', send, recuperarClave_processResponse);

        } else {
            muestraVentana('El correo Electr&oacute;nico no tiene el formato valido');
        }
    } else {
        muestraVentana(mensajeobligatorios);
    }
}

function recuperarClave_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval("(" + res + ")");
        var msj = info.data;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                muestraVentana("Se ha enviado un mensaje a su correo electr&oacute;nico con la clave.");
                $.fancybox.close()
                break;
        }
    } catch (elError) {
    }
}


function cancelaRecuperaClave() {
    $.fancybox.close();
    $('#email').val('');
}