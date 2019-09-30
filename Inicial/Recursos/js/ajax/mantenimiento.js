///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////  BUSCA VISTAS     /////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


/***********************************************************
METODO QUE PERMITE BUSCAR VISTAS
***********************************************************/

function buscaVistas() {
    limpiar();
    muestraVentanaProgreso();
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'buscaVistas'));
    var send = arrayParameters.join('&');
    //alert(send)
    $.post('../../Controlador/ctlMantenimiento.aspx', send, buscaVistas_processResponse);
}

function buscaVistas_processResponse(res) {
    try {
        ocultaVentanaProgreso();

        var info = eval('(' + quitarEnter(res) + ')');
        var msj = info.msj;

        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                var long = info.data.length;
                var clase = '';
                var clase2 = '';
                var calse3 = '';
                var ctlClase = true;
                var maxLength = '';
                var html = '<table><tr><td class="encabezado" colspan="2">RESULTADO DE LA CONSULTA</td></tr><tr><td class="encabezado">NOMBRE VISTA</td><td class="encabezado">CUERPO VISTA</td></tr>';
                for (var i = 0; i < long; i += 5) {
                    if (ctlClase) {
                        clase = 'cuerpoListado3';
                        clase2 = 'cuerpoListado1';
                        clase3 = 'cuerpoListado7';
                    } else {
                        clase = 'cuerpoListado4';
                        clase2 = 'cuerpoListado2';
                        clase3 = 'cuerpoListado8';
                    }
                    html += '<tr><td class="' + clase + '">' + info.data[i] + '</td><td class="' + clase2 + '"><span class="listadoMantenimiento" onclick="verVista(\'' + info.data[i + 1] + '\')"><a>VER</a></span></td>';
                    ctlClase = !ctlClase;
                }
                document.getElementById('resultadoConsulta').innerHTML = html;
                break;
        }
    } catch (elError) {
    }
}


function verVista(texto) {
    document.getElementById('taConsulta').value = ponerEnter(texto);
}



///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////  BUSCA TABLAS     /////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


/***********************************************************
METODO QUE PERMITE BUSCAR LAS TABLAS
***********************************************************/

function buscaTablas() {
    limpiar();
    muestraVentanaProgreso();
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'buscaTablas'));
    var send = arrayParameters.join('&');
    //alert(send)
    $.post('../../Controlador/ctlMantenimiento.aspx', send, buscaTablas_processResponse);
}

function buscaTablas_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + quitarEnter(res) + ')');
        var msj = info.msj;
        //alert(msj)
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                var long = info.data.length;
                var table = '';
                var ctl = false;
                var clase = '';
                var ctlClase = true;
                var tableTemp = '';
                var maxLength = '';
                var html = '<table><tr><td class="encabezado" colspan="2">RESULTADO DE LA CONSULTA</td></tr><tr><td class="encabezado">NOMBRE TABLA</td><td class="encabezado">CAMPO (TIPO)(NUM CARACTERES)</td></tr>';
                for (var i = 0; i < long; i++) {
                    if (ctlClase) {
                        clase = 'cuerpoListado7';
                    }
                    else {
                        clase = 'cuerpoListado8';
                    }

                    tableTemp = info.data[i];

                    if (tableTemp != table) {
                        if (table != '') {
                            html += '<br /><br /></td>';
                        }
                        table = tableTemp;
                        html += '<tr><td class="' + clase + '"><span class="listadoMantenimiento" onclick="ejecutarConsultaTabla(\'' + table + '\')"><a>' + table + '</a></span></td><td class="' + clase + '">';
                        ctlClase = !ctlClase;
                    } else {
                        switch (info.data[i + 3]) {
                            case 'NULL':
                                maxLength = '';
                                break;

                            case '-1':
                                maxLength = '(MAX)';
                                break;

                            case '':
                                maxLength = '';
                                break;

                            default:
                                maxLength = '(' + info.data[i + 3] + ')';
                                break;
                        }
                        html += '<br />' + info.data[i + 1] + ' (' + info.data[i + 2] + ')' + ' ' + maxLength;
                    }
                    i += 3;
                    //ctl = true;
                }

                document.getElementById('resultadoConsulta').innerHTML = html;
                break;
        }
    } catch (elError) {
    }
}


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////  BUSCA PAQUETES   /////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function buscaPaquetes() {
    limpiar();
    muestraVentanaProgreso();
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'buscaPaquetes'));
    var send = arrayParameters.join('&');
    //alert(send)
    $.post('../../Controlador/ctlMantenimiento.aspx', send, buscaPaquetes_processResponse);
}

function buscaPaquetes_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + quitarEnter(res) + ')');
        var msj = info.msj;
        //alert(msj)
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                var long = info.data.length;
                var paquete = '';
                var ctl = false;
                var clase = '';
                var ctlClase = true;
                var html = '<table><tr><td class="encabezado">RESULTADO DE LA CONSULTA</td></tr><tr><td class="encabezado">NOMBRE DEL PAQUETE</td></tr>';
                for (var i = 0; i < long; i++) {
                    if (ctlClase)
                        clase = 'cuerpoListado3';
                    else
                        clase = 'cuerpoListado6';
                    ctlClase = !ctlClase;
                    paquete = info.data[i];
                    html += '<tr><td class="' + clase + '"><span class="listadoMantenimiento" onclick="ejecutarConsultaPaquete(\'' + paquete + '\')"><a>' + paquete + '</a></span></td>';
                    ctl = true;
                }
                document.getElementById('resultadoConsulta').innerHTML = html;
                break;
        }
    } catch (elError) {
    }
}


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////  BUSCA RUTINAS    /////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

/***********************************************************
METODO QUE PERMITE BUSCAR RUTINAS
***********************************************************/

function buscaRutinas() {
    limpiar();
    muestraVentanaProgreso(); ;
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'buscaRutinas'));
    var send = arrayParameters.join('&');
    //alert(send);
    $.post('../../Controlador/ctlMantenimiento.aspx', send, buscaRutinas_processResponse);
}

function buscaRutinas_processResponse(res) {
    try {
        ocultaVentanaProgreso(); ;
        var info = eval('(' + quitarEnter(res) + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                var long = info.data.length;
                var clase = '';
                var clase2 = '';
                var ctlClase = true;
                var maxLength = '';
                var html = '<table><tr><td class="encabezado" colspan="5">RESULTADO DE LA CONSULTA</td></tr><tr><td class="encabezado">NOMBRE RUTINA</td><td class="encabezado">TIPO</td><td class="encabezado">CUERPO RUTINA</td><td class="encabezado">F. CREACI&Oacute;N</td><td class="encabezado">F. MODIF.</td></tr>';
                for (var i = 0; i < long; i += 5) {
                    if (ctlClase) {
                        clase = 'cuerpoListado3';
                        clase2 = 'cuerpoListado1';
                    } else {
                        clase = 'cuerpoListado6';
                        clase2 = 'cuerpoListado4';
                    }
                    html += '<tr><td class="' + clase + '">' + info.data[i] + '</td><td class="' + clase + '">' + info.data[i + 1] + '</td><td class="' + clase2 + '"><span onclick="verRutina(' + i + ')" class="listadoMantenimiento"><a>VER</a></span><div class="ocultar" id="rutina' + i + '">' + info.data[i + 2] + '</div></td><td class="' + clase + '">' + info.data[i + 3] + '</td><td class="' + clase + '">' + info.data[i + 4] + '</td>';
                    ctlClase = !ctlClase;
                }
                document.getElementById('resultadoConsulta').innerHTML = html;
                break;
        }
    } catch (elError) {
    }
}

function verRutina(id) {
    document.getElementById('taConsulta').value = ponerEnter(document.getElementById('rutina' + id).innerHTML);
}


/***********************************************************
METODO QUE PERMITE EJECUTAR CONSULTAS DE UNA TABLA
***********************************************************/

function ejecutarConsultaTabla(tabla) {
    muestraVentanaProgreso();
    document.getElementById('taConsulta').value = 'SELECT * FROM ' + tabla;
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'ejecutaTabla'));
    arrayParameters.push(newArg('tabla', tabla));
    var send = arrayParameters.join('&');
    //alert(send);
    $.post('../../Controlador/ctlMantenimiento.aspx', send, ejecutarConsulta_processResponse);
}

/***********************************************************
METODO QUE PERMITE EJECUTAR CONSULTAS 
***********************************************************/
function ejecutarConsulta() {
    muestraVentanaProgreso();
    var rut = document.getElementById('taConsulta').value;
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'ejecutaRutina'));
    arrayParameters.push(newArg('rutina', rut));
    var send = arrayParameters.join('&');
    //alert(send);
    $.post('../../Controlador/ctlMantenimiento.aspx', send, ejecutarConsulta_processResponse);
}

function ejecutarConsulta_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + quitarEnter(res) + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case -2:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                muestraVentana('Rutina Ejecutada Con &Eacute;xito.');
                break;
            case 11:
                var long = info.data.length;
                var campos = info.fields;
                var clase = '';
                var ctlClase = true;
                var html = '<table><tr><td class="encabezado" colspan="' + campos + '">RESULTADO DE LA CONSULTA</td></tr>';
                for (var i = 0; i < long; i += campos) {
                    if (ctlClase) {
                        clase = 'cuerpoListado3';
                    }
                    else {
                        clase = 'cuerpoListado6';
                    }
                    html += '<tr>';
                    for (var j = 0; j < campos; j++) {
                        html += '<td class="' + clase + '">' + info.data[i + j] + '</td>';
                    }
                    html += '</tr>';
                    ctlClase = !ctlClase;
                }
                html += '</table>';
                document.getElementById('resultadoConsulta').innerHTML = html;
                break;
            case 00:
                muestraVentana(mensajecero);
                break;
        }
    } catch (elError) {
    }
}


var paquete_global = '';
/***************************************************
PARA EJECUTAR LA CONSULTA DE UN RETORNO DE UN PAQUETE
**************************************************/
function ejecutarConsultaPaquete(paquete) {
    paquete_global = paquete;
    muestraVentanaProgreso();
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'ejecutaPaquete'));
    arrayParameters.push(newArg('paquete', paquete));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlMantenimiento.aspx', send, ejecutarConsultaPaquete_processResponse);
}

function ejecutarConsultaPaquete_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + quitarEnter(red) + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case -2:
                muestraVentana(mensajemenosuno);
                break;
            case 11:
                var long = info.data.length;
                var campos = info.fields;
                var clase = '';
                var ctlClase = true;
                var html = '<table width="100%"><tr><td class="encabezado">RESULTADO DE LA CONSULTA</td></tr>';
                html += '<tr><td class="encabezado">PROCEDIMIENTOS ALMACENADOS ' + paquete_global + '</td></tr>';
                for (var i = 1; i < long - 1; i += campos) {
                    if (ctlClase)
                        clase = 'cuerpoListado3';
                    else
                        clase = 'cuerpoListado6';
                    html += '<tr>'
                    for (var j = 0; j < campos; j++) {
                        if (info.data[i + j].toString().indexOf('REF') == -1)
                            html += '<td class="' + clase + '">' + info.data[i + j] + '</td>';
                    }
                    html += '</tr>';
                    ctlClase = !ctlClase;
                }
                html += '</table>';
                document.getElementById('resultadoConsulta').innerHTML = html;
                ejecutarConsultaCuerpoPaquete(paquete_global);
                break;
            case 00:
                muestraVentana(mensajecero);
                break;
        }
    } catch (elError) {
    }
}

/***************************************************
DEVUELVE EL CÓDIGO FUENTE DEL CUERPO DE UN PAQUETE
**************************************************/
function ejecutarConsultaCuerpoPaquete(paquete) {
    muestraVentanaProgreso();
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cuerpoPaquete'));
    arrayParameters.push(newArg('paquete', paquete));
    var send = arrayParameters.join('&');
    //alert(send)
    $.post('../../Controlador/ctlMantenimiento.aspx', send, ejecutarConsultaCuerpoPaquete_processResponse);
}

function ejecutarConsultaCuerpoPaquete_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + ponerEnter(res) + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case -2:
                muestraVentana(mensajemenosuno);
                break;
            case 11:
                var long = info.data.length;
                var html = '';
                for (var i = 0; i < long; i++) {
                    html += info.data[i] + '\n';
                }
                document.getElementById('taConsulta').value = html;
                break;
            case 00:
                muestraVentana(mensajecero);
                break;
        }
    } catch (elError) {
    }
}


/***********************************************************
METODO QUE PERMITE LIMPIAR
***********************************************************/
function limpiar() {
    document.getElementById('taConsulta').style.backgroundColor = "#FFF";
    document.getElementById('divEjecutaCMD').style.display = 'none';
    document.getElementById('divEjecutaSQL').style.display = 'block';
    document.getElementById('taConsulta').style.color = "#000";
    document.getElementById('taConsulta').value = '';
    document.getElementById('resultadoConsulta').innerHTML = '';
    //document.getElementById('msjVentana').innerHTML = '';
    paquete_global = '';
}


/***********************************************************
METODO QUE PERMITE CERRAR LA SESION DE MANTENIMIENTO
***********************************************************/
function cerrarSesionMantenimiento(masterPage) {
    master = masterPage;
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cerrarSesionMantenimiento'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlLoginMantenimiento.aspx', send, cerrarSesionMantenimiento_processResponse);
}

function cerrarSesionMantenimiento_processResponse(res) {
    try { location.href = "../../vista/general/inicio.aspx"; }
    catch (error) {
    }
}

/***********************************************************
METODO QUE PERMITE MOSTRAR LAS VARIABLES DEL SISTEMA
***********************************************************/
function variableSistema() {
    limpiar();
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'variableSistema'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlMantenimiento.aspx', send, variableSistema_processResponse);
}

function variableSistema_processResponse(res) {
    try {
        document.getElementById('resultadoConsulta').innerHTML = "<br/><br/><b>Variables del Sistema</b><br><br>" + "<table class='tbBorde centrar'>" + res + "</table>";
    } catch (error) {
    }
}

/***********************************************************
METODO QUE PERMITE MOSTRAR LOS DISCOS DUROS
***********************************************************/

function listaDiscosDuros() {
    limpiar();
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listaDiscosDuros'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlMantenimiento.aspx', send, listaDiscosDuros_processResponse);
}

function listaDiscosDuros_processResponse(res) {
    try {
        document.getElementById('resultadoConsulta').innerHTML = "<br/><br/><b>Listado de Discos Duros</b><br><br>" + "<table class='tbBorde centrar'>" + res + "</table>";
    }
    catch (error) {
    }
}

/***********************************************************
METODO QUE CAMBIA LA APARIENCIA DE COMANDOS
***********************************************************/

function sistemaOperativo() {
    limpiar();
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'sistemaOperativo'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlMantenimiento.aspx', send, sistemaOperativo_processResponse);
}

function sistemaOperativo_processResponse(res) {
    try {
        document.getElementById('resultadoConsulta').innerHTML = "<br/><br/><b>Sistema Operativo</b><br><br>" + "<table class='tbBorde centrar'>" + res + "</table>";
    } catch (error) {
    }
}

function fondoConsolaDeComandos() {
    limpiar();
    document.getElementById('taConsulta').style.backgroundColor = "#000";
    document.getElementById('taConsulta').style.color = "#FFF";
    document.getElementById('divEjecutaCMD').style.display = 'block';
    document.getElementById('divEjecutaSQL').style.display = 'none';
}

/***********************************************************
METODO QUE CAMBIA LA APARIENCIA DE COMANDOS
***********************************************************/

function consolaDeComandos() {
    var arrayParameters = new Array();
    var comando = document.getElementById('taConsulta').value;
    arrayParameters.push(newArg('p', 'consolaComandos'));
    arrayParameters.push(newArg('com', comando));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlMantenimiento.aspx', send, consolaDeComandos_processResponse);
}

function consolaDeComandos_processResponse(res) {
    try {
        document.getElementById('resultadoConsulta').innerHTML = "<br/><br/><b>Terminal del sistema</b><br><br>" + "<table class='tbBorde centrar'>" + res + "</table>";
    } catch (error) {
    }
}