///////////////////////////////////////////////////////////////////////////////////
/////////////////////////// VARIABLES GLOBALES ////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////

var idInterval;
///////////////////////////////////////////////////////////////////////////////////
///////////////////////////////// FUNCIONES AUTOMATICAS ///////////////////////////
///////////////////////////////////////////////////////////////////////////////////

$(document).ready(function () {
    idInterval = setInterval("verificarSesion()", 10000);
});


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////LOGIN USUARIO ///////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////

/*********************************************************
METODO PARA INGRESAR AL SISTEMA
***********************************************************/
function verificarSesion() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'verificaSesion'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlLogin.aspx', send, Ingresar_processResponse);
}

function Ingresar_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case 0:
                muestraVentana("Se ha cerrado la sesi&oacute;n de este usuario inesperadamente.");
                //muestraVentana("Se ha iniciado sesi&oacute;n con este usuario en otro sitio.");
                setTimeout("location.href='../general/inicio.aspx'", 3500);
                break;
            case 1:
                clearInterval(idInterval);
                idInterval = setInterval("verificarSesion()", 10000);
                break;
        }
    } catch (elError) {
        //alert(elError);
    }
}
