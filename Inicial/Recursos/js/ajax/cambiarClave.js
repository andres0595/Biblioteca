///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////  CAMBIA CLAVE   /////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

$(document).ready(function () {
    ocultaVentanaProgreso();
})


/*****************************************************************
ESTA FUNCION PERMITE CAMBIAR LA CLAVE
******************************************************************/

function cambiaClaveUsuario() {
    var actual = document.getElementById('claveActual').value;
    var clave1 = document.getElementById('nuevaClave1').value;
    var clave2 = document.getElementById('nuevaClave2').value;

    if ((clave1 != '') && (clave2 != '')) {
        if (clave1 == clave2) {
            var arrayParameters = new Array();
            arrayParameters.push(newArg('p', 'cambio'));
            arrayParameters.push(newArg('c', clave1));
            arrayParameters.push(newArg('ca', actual));
            var send = arrayParameters.join('&');
            muestraVentanaProgreso("Enviando...");
            $.post('../../Controlador/ctlCambiarClave.aspx', send, cambiaClaveUsuario_processResponse);
        } else {
            muestraVentana('Las claves no coinciden.');
        }
    } else {
        muestraVentana(mensajeObligatorio);
    }
}

function cambiaClaveUsuario_processResponse(res) {
    try {
        var info = eval("(" + res + ")");
        var msj = info.msj;
        ocultaVentanaProgreso();
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                muestraVentana(mensajeEdita);
                setTimeout("redireccionar()", 2000);
                break;
        }
    } catch (elError) {
    }
}

/*****************************************************************
ESTA FUNCION REDIRECCIONAR AL INICIO DEL SISTEMA
******************************************************************/
function redireccionar() {
    location.href = "../../vista/general/inicio.aspx";
}