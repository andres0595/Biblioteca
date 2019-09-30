///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////CERRAR SESION///////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


/*****************************************************************
ESTA FUNCION PERMITE CERRAR LA SESION
******************************************************************/

var master = '';
function cerrarSesion(masterPage) {
    //alert(masterPage);
    master = masterPage;
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'termina'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlLogin.aspx', send, cerrarSesion_processResponse);
}

function cerrarSesion_processResponse(res) {
    try {
        switch (master) {
            case '1':
                location.href = "../../vista/general/inicio.aspx";
                break;
            case '2':
                location.href = "../../../vista/general/inicio.aspx";
                break;
            case '3':
                location.href = "../../../vista/general/acceso_formulario.aspx";
                break;
        }
    } catch (elError) {
    }
}