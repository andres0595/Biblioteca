///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////LOGIN USUARIO///////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


/*********************************************************
METODO PARA INGRESAR AL SISTEMA
***********************************************************/

function ingresar() {
    var usu = document.getElementById('usuario').value
    var cla = md5(document.getElementById('clave').value)

    if ((usu != '') && (cla != '')) {
        muestraVentanaProgreso('Identificando...');

        var arrayParameters = new Array()
        arrayParameters.push(newArg('p', 'logueaUsuarioMantenimiento'))
        arrayParameters.push(newArg('usuario', usu))
        arrayParameters.push(newArg('clave', cla))
        var send = arrayParameters.join('&')
        $.post('../../Controlador/ctlLoginMantenimiento.aspx', send, Ingresar_processResponse)
    } else
        muestraVentana(mensajeobligatorios);
}

function Ingresar_processResponse(res) {
    try {
        var info = eval("(" + res + ")");
        var msj = info.msj;
        ocultaVentanaProgreso();

        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break
            case 0:
                muestraVentana('Datos Invalidos.');

                document.getElementById('clave').value = ''
                document.getElementById('usuario').value = ''
                break
            case 1:
                location.href = "../general/mantenimiento.aspx"
                break
        }
    } catch (elError) {
    }
}