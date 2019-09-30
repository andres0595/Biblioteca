///////////////////////////////////////////////////////////////////////////////////
/////////////////////////// VARIABLES GLOBALES ////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////
var numUsuGlobal = 0;
var pagGlobal = 1;

///////////////////////////////////////////////////////////////////////////////////
///////////////////////////////// FUNCIONES AUTOMATICAS ///////////////////////////
///////////////////////////////////////////////////////////////////////////////////

$().ready(function () {
    listar(pagGlobal);
    setInterval("listar(" + pagGlobal + ");", 10000);
});


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////LOGIN USUARIO ///////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////


/*************************************************************************************************************************
LISTA LOS USUARIOS CONECTADOS EN EL SISTEMA
**************************************************************************************************************************/
function listar(pag) {
    //alert(pag)
    pagGlobal = pag;
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'conectados'));
    arrayParameters.push(newArg('pag', pag));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlPaginador.aspx', send, listar_processResponse);
    //   muestraVentanaProgreso("Cargando ...");
}

function listar_processResponse(res) {
    //alert(res)
    try {
        var info = eval('(' + res + ')');
        var divNomenclatura = document.getElementById('divListadoAuditoria');
        if (res != '0') {
            //  ocultaVentanaProgreso();
            var datosRows = info.data;
            var cols = info.cols, lon = datosRows.length;
            var ctl = true, claseAplicar = "";
            var id = "", responsable = "", accion = "", usuario = "", fecha = "";

            tabla = "<table class='tbListado centrar' style='text-align: center;'><tr><td class='encabezado' colspan='4'>CONEXI&Oacute;N USUARIOS</td></tr>";
            tabla += "<tr><td class='encabezado'>USUARIO</td><td class='encabezado'>NOMBRES Y APELLIDOS</td><td class='encabezado'>USUARIO CREADOR</td><td class='encabezado'>DESCONECTAR</td></tr>";

            for (var i = 0; i < lon; i += cols) {
                nom = datosRows[i];
                usuario = datosRows[i + 1];
                creador = datosRows[i + 2];
                clave = datosRows[i + 3];

                if (ctl) {
                    claseAplicar = "cuerpoListado7";
                }
                else {
                    claseAplicar = "cuerpoListado8";
                }

                ctl = !ctl;
                tabla += '<tr><td class="' + claseAplicar + '">' + nom + '</td><td class="' + claseAplicar + '">' + usuario + '</td><td class="' + claseAplicar + '">' + creador + '</td>';
                tabla += '<td class="' + claseAplicar + '"><div id="imgDesconectar" class="linkIconoLateral" onclick="desconectar(\'' + clave + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/desconectar_22x22.png"><p>Desconectar</p></div></td></tr>';
            }
            tabla += '</table>'
            divNomenclatura.innerHTML = tabla;
            divNomenclatura.innerHTML += pieDePaginaListar(info, 'listar'); /*llama de nuevo al paginador con la siguiente pagina*/
            var idMenuForm = document.getElementById('idMenuForm').innerHTML; // se adiciona esta linea para que se de  el permiso de visualizar el editar despues de cargar
            permisosParaMenu(idMenuForm); // se adiciona esta linea para que se el permiso de visualizar el editar despues de cargar
        }
        else {
            //ocultaVentanaProgreso();
            divTerceros.innerHTML = mensajecero;
        }
    } catch (elError) {
        //alert(elError)
    }
}


function desconectar(clave) {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'desconectar'));
    arrayParameters.push(newArg('clave', clave));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlLogin.aspx', send, desconectar_processResponse);
}

function desconectar_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana("ERROR DESCONECTANDO USUARIO.");
                break;
            case 1:
                muestraVentana("USUARIO DESCONECTADO.");
                listar(pagGlobal);
                break;
        }
    } catch (elError) {
    }
}