$(document).ready(function () {
    // contarPacientesNuevosSP();
   // muestraVentana('CARGANDO...');
    //    cargaResponsable();
    //    setTimeout("listarObjetivos()", 400);
    //    setTimeout("listarInformacion()", 600);
    //    setTimeout("listarOportunidades()", 800);
    //    setTimeout("listarActividadesAbiertas(" + 1 + ")", 1000);
    //setTimeout("listarActividadesNoEjecutadas(" + 1 + ")", 1000);
    //actualizarPactientes();////////pacientes activos e inactivos///04

    //IndicadorCumplimientoGestion();//////////////paSP_IndicadorCumplimientoGestion_generar
    //IndicadorLlamadaBienvenida();/////////////////paSP_IndicadorLlamadaBienvenida_generar
    //IndicadorContactabilidadPacientes();////////////paSP_IndicadorContactabilidadPaciente_generar
    //IndicadorNoAceptacion();//////////////paSP_IndicadorNoAceptacion_generar
    //IndicadorDevolucionInoportunidad();//////////paSP_IndicadorDevolucionInoportunidad_generar
});


var arrayAvance = new Array();
var arrayObjetivo = new Array();
var tipo = "";
var globalIdClientes = -1;
var globalIdEtapaProximo = -1;
var pagGlobal = 1;
var pagGlobalArchivos = 1;
var nuevaEtapa;



//function IndicadorCumplimientoGestion() {
//    var arrayParameters = new Array();
//    arrayParameters.push(newArg('p', 'IndicadorCumplimientoGestionCargar'));
//    var send = arrayParameters.join('&');
//    $.post('../../Controlador/ctlGeneral.aspx', send, '');
//}

//function IndicadorLlamadaBienvenida() {
//    var arrayParameters = new Array();
//    arrayParameters.push(newArg('p', 'IndicadorLlamadaBienvenidaCargar'));
//    var send = arrayParameters.join('&');
//    $.post('../../Controlador/ctlGeneral.aspx', send, '');
//}

//function IndicadorContactabilidadPacientes() {
//    var arrayParameters = new Array();
//    arrayParameters.push(newArg('p', 'IndicadorContactabilidadPacienteCargar'));
//    var send = arrayParameters.join('&');
//    $.post('../../Controlador/ctlGeneral.aspx', send, '');
//}

//function IndicadorNoAceptacion() {
//    var arrayParameters = new Array();
//    arrayParameters.push(newArg('p', 'IndicadorNoAceptacionCargar'));
//    var send = arrayParameters.join('&');
//    $.post('../../Controlador/ctlGeneral.aspx', send, '');
//}

//function IndicadorDevolucionInoportunidad() {
//    var arrayParameters = new Array();
//    arrayParameters.push(newArg('p', 'IndicadorDevolucionInoportunidadCargar'));
//    var send = arrayParameters.join('&');
//    $.post('../../Controlador/ctlGeneral.aspx', send, '');
//}

//function actualizarPactientes() {
//    var arrayParameters = new Array();
//    arrayParameters.push(newArg('p', 'actualizarPactientes'));
//    var send = arrayParameters.join('&');
//    $.post('../../Controlador/ctlGeneral.aspx', send, '');
//}

//function actualizarIniInfoPaciente() {
//    var arrayParameters = new Array();
//    arrayParameters.push(newArg('p', 'actualizarIniInfoPaciente'));
//    var send = arrayParameters.join('&');
//    $.post('../../Controlador/ctlGeneral.aspx', send, '');
//}

//function actualizarIniInfoPaciPrograma() {
//    var arrayParameters = new Array();
//    arrayParameters.push(newArg('p', 'actualizarIniInfoPaciPrograma'));
//    var send = arrayParameters.join('&');
//    $.post('../../Controlador/ctlGeneral.aspx', send, '');
//}

//function actualizarIniInfoSeguiPer() {
//    var arrayParameters = new Array();
//    arrayParameters.push(newArg('p', 'actualizarIniInfoSeguiPer'));
//    var send = arrayParameters.join('&');
//    $.post('../../Controlador/ctlGeneral.aspx', send, '');
//}

//function actualizarIniInfoContactabilidad() {
//    var arrayParameters = new Array();
//    arrayParameters.push(newArg('p', 'actualizarIniInfoContactabilidad'));
//    var send = arrayParameters.join('&');
//    $.post('../../Controlador/ctlGeneral.aspx', send, '');
//}

//function InicioInfoPaciNoContac_contar() {
//    var arrayParameters = new Array();
//    arrayParameters.push(newArg('p', 'paSP_InicioInfoPaciNoContac_contar'));
//    var send = arrayParameters.join('&');
//    $.post('../../Controlador/ctlGeneral.aspx', send, '');
//}
/////nuevo contador

//function actualizarIniInfoContactabilidad2018() {
//    var arrayParameters = new Array();
//    arrayParameters.push(newArg('p', 'paSP_InicioInfoContactabilidad2018_contar'));
//    var send = arrayParameters.join('&');
//    $.post('../../Controlador/ctlGeneral.aspx', send, '');
//}



function cargaResponsable() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargaResponsable'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, cargaResponsable_processResponse);
}

function cargaResponsable_processResponse(res) {
    var info = eval('(' + res + ')');
    var msj = info.msj;
    switch (msj) {
        case -1:
            muestraVentana('SIN CONEXIÓN A LA BASE DE DATOS.');
            break;
        case 0:
            limpiarSelectOpcion(document.getElementById('selResponsable'));
            break;
        case 1:
            if (info.data[2] == 1 || info.data[2] == 4)
                document.getElementById('selResponsable').disabled = false
            llenarSelectResponsable(res, document.getElementById('selResponsable'));
            break;
    }
}

function cambiarResponsable() {
    muestraVentana('CARGANDO...');
    arrayAvance = new Array();
    arrayObjetivo = new Array();
    tipo = "";
    document.getElementById('7').checked = true;
    listarOportunidades();
    listarActividadesAbiertas(1);
    setTimeout("listarInformacion()", 200);
    setTimeout("listarObjetivos()", 500);
}

function fechaSistema() {
    var f = new Date();
    var meses = new Array("01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12");
    var fechaActual;
    var dia = "";
    dia = dia + f.getDate();
    var tam = dia.length;
    //fechaActual = f.getDate() + "/" + meses[f.getMonth()] + "/" + f.getFullYear();
    fechaActual = ((tam == 1) ? "0" + f.getDate() : f.getDate()) + "/" + meses[f.getMonth()] + "/" + f.getFullYear(); //descomentar si el dia de la fecha sale con un solo digito
    return fechaActual;
}

function fechaSistemaTomorrow() {
    var f = new Date();
    var meses = new Array("01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12");
    var fechaActual;
    var dia = "";
    var fechaTomorrow = new Date(f.getTime() + 24 * 60 * 60 * 1000);
    dia = dia + fechaTomorrow.getDate(); // se agrega esto para calcular la fecha de mañana.
    var tam = dia.length;
    //fechaActual = f.getDate() + "/" + meses[f.getMonth()] + "/" + f.getFullYear();
    fechaActual = ((tam == 1) ? "0" + fechaTomorrow.getDate() : fechaTomorrow.getDate()) + "/" + meses[fechaTomorrow.getMonth()] + "/" + fechaTomorrow.getFullYear(); //descomentar si el dia de la fecha sale con un solo digito
    return fechaActual;
}

function listarInformacion() {
    var responsable = document.getElementById('selResponsable').value;
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listarInformacion'));
    arrayParameters.push(newArg('responsableSesion', responsable));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlPresentacion.aspx', send, listarInformacion_processResponse);
}

function listarInformacion_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        if (msj != '0') {
            document.getElementById('listadoInformacion').style.display = 'block';
            var datosRows = info.data;
            var cols = info.cols;
            var vacio = '--';
            var tabla = '';
            var nombre = "", identificacion = "", cargo = "", telefono = "", correo = "", imagen = "";

            for (var i = 0; i < datosRows.length; i += cols) {
                nombre = datosRows[i];
                identificacion = datosRows[i + 1];
                cargo = datosRows[i + 2];
                telefono = datosRows[i + 3];
                correo = datosRows[i + 4];
                imagen = datosRows[i + 5];

                tabla = '<table class="tbListado centrar" style="text-align: center;">';
                tabla += "<tr><td rowspan='5' align='center'><img height='150px' width='120px' src='" + '../../Fotos/' + imagen + "'></td>";

                tabla += '<td class="nombreFila" style="width:25%"> &nbsp;NOMBRE</td><td class="cuerpoListado10">' + ((nombre == "") ? vacio : unescape(nombre)) + '</td></tr>';
                tabla += '<tr><td class="nombreFila"> &nbsp;DOCUMENTO</td><td class="cuerpoListado10">' + ((identificacion == "") ? vacio : unescape(identificacion)) + '</td></tr>';
                tabla += '<tr><td class="nombreFila"> &nbsp;CARGO</td><td class="cuerpoListado10">' + ((cargo == "") ? vacio : unescape(cargo)) + '</td></tr>';
                tabla += '<tr><td class="nombreFila"> &nbsp;TELEFONO</td><td class="cuerpoListado10">' + ((telefono == "") ? vacio : unescape(telefono)) + '</td></tr>';
                tabla += '<tr><td class="nombreFila"> &nbsp;E-MAIL</td><td class="cuerpoListado10">' + ((correo == "") ? vacio : unescape(correo)) + '</td></tr>';

                tabla += '</table>';
            }
            document.getElementById('listadoInformacion').innerHTML = tabla;

        }
        else {
            document.getElementById('listadoInformacion').style.display = 'none';
            document.getElementById('listadoInformacion').innerHTML = 'NO SE ENCONTR&Oacute; INFORMACI&Oacute;N EN LA BASE DE DATOS.';
        }
    } catch (elError) {
        //alert(elError)
    }
}

function listarOportunidades() {
    var responsable = document.getElementById('selResponsable').value;

    var tiempo = "22";

    //    if (document.getElementById('7').checked) {
    //        tiempo = 7;
    //    }
    //    else if (document.getElementById('15').checked) {
    //        tiempo = 15;
    //    }
    //    else if (document.getElementById('30').checked) {
    //        tiempo = 30;
    //    }

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listarOportunidades'));
    arrayParameters.push(newArg('responsableSesion', responsable));
    arrayParameters.push(newArg('tiempo', tiempo));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlPresentacion.aspx', send, listarOportunidades_processResponse);
}

function listarOportunidades_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        if (msj != '0') {
            document.getElementById('listadoOportunidades').style.display = 'block';
            var datosRows = info.data;
            var cols = info.cols;
            var vacio = '--';
            var tabla = '';
            var colorTexto = "";
            var tipo = "", cantidad = "", monto = "";

            tabla = '<table class="tbListado centrar" style="text-align: center;">';
            tabla += '<tr><td class="encabezado" colspan="3">RESUMEN OPORTUNIDADES</td></tr>';
            tabla += '<tr><td class="encabezado">TIPO</td><td class="encabezado">CANTIDAD</td><td class="encabezado">MONTO</td></tr>';

            for (var i = 0; i < datosRows.length; i += cols) {
                tipo = datosRows[i];
                cantidad = datosRows[i + 1];
                monto = datosRows[i + 2];

                if (tipo == 'GANADAS') {
                    colorTexto = "textoVerde";
                }
                else if (tipo == 'PERDIDAS') {
                    colorTexto = "textoRojo";
                }
                else if (tipo == 'ABIERTAS') {
                    colorTexto = "textoAzul";
                }

                tabla += '<tr><td class="cuerpoListado10 ' + colorTexto + '">' + ((tipo == "") ? vacio : tipo) + '</td>';
                tabla += '<td class="cuerpoListado10 ' + colorTexto + '">' + ((cantidad == "") ? vacio : cantidad) + '</td>';
                tabla += '<td class="cuerpoListado10 ' + colorTexto + '">' + ((monto == "") ? vacio : '$' + formato_numero(monto, 0, ".", ".")) + '</td></tr>';
            }
            tabla += '</table>';
            document.getElementById('listadoOportunidades').innerHTML = tabla;

        }
        else {
            document.getElementById('listadoOportunidades').style.display = 'none';
            document.getElementById('listadoOportunidades').innerHTML = 'NO SE ENCONTR&Oacute; INFORMACI&Oacute;N EN LA BASE DE DATOS.';
        }
    } catch (elError) {
        //alert(elError)
    }
}

function listarObjetivos() {
    var responsable = document.getElementById('selResponsable').value;
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listarObjetivos'));
    arrayParameters.push(newArg('responsableSesion', responsable));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlPresentacion.aspx', send, listarObjetivos_processResponse);
}

function listarObjetivos_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        if (msj != '0') {
            document.getElementById('listadoObjetivos').style.display = 'block';
            document.getElementById('listadoGrafica').style.display = 'block';
            document.getElementById('listadoGrafica').innerHTML = '';
            var datosRows = info.data;
            var cols = info.cols;
            var vacio = '--';
            var tabla = '';
            var metrico = "", fechaLimite = "", objetivo = "", avance = "", porcentaje = "";
            var tipo = "";
            var arrayObjetivo = new Array();
            var arrayAvance = new Array();

            tabla = '<table class="tbListado centrar" style="text-align: center;">';
            tabla += '<tr><td class="encabezado">METRICO</td><td class="encabezado">FECHA LIMITE</td><td class="encabezado">OBJETIVO</td><td class="encabezado">AVANCE</td><td class="encabezado">%</td></tr>';

            for (var i = 0; i < datosRows.length; i += cols) {
                metrico = datosRows[i];
                fechaLimite = datosRows[i + 1];
                objetivo = datosRows[i + 2];
                avance = datosRows[i + 3];
                porcentaje = datosRows[i + 4];

                var valorObjetivo = parseInt(objetivo) - parseInt(avance);

                if (valorObjetivo < 0) {
                    valorObjetivo = 0;
                }

                tipo = tipo + metrico.replace(" ", "\n") + '|';
                arrayObjetivo.push(valorObjetivo.toString());
                arrayAvance.push(avance);

                tabla += '<tr><td class="cuerpoListado10">' + ((metrico == "") ? vacio : metrico) + '</td>';
                tabla += '<td class="cuerpoListado10">' + ((fechaLimite == "") ? vacio : fechaLimite) + '</td>';
                tabla += '<td class="cuerpoListado10">' + ((objetivo == "") ? vacio : objetivo) + '</td>';
                tabla += '<td class="cuerpoListado10">' + ((avance == "") ? vacio : avance) + '</td>';
                tabla += '<td class="cuerpoListado10">' + ((porcentaje == "") ? vacio : parseFloat(porcentaje).toFixed(2)) + '%</td></tr>';

            }
            tabla += '</table>';
            document.getElementById('listadoObjetivos').innerHTML = tabla;

            var data = ({
                series1data: arrayAvance,
                series2data: arrayObjetivo
            });
            var graph = new JpGraph(
                'stacked vertical bar',
                'listadoGrafica',
                data,
                {
                    //width: 400,
                    //height: 500,
                    //backgroundColor: '#f9f5da',
                    series1: "title: Avance; color:#008bd0; scale:left",
                    series2: "title: Objetivo; color:#e94933; scale:left",
                    xLabels: tipo,
                    xfontColor: "#444",
                    xopacity: 1.0,
                    xfontSize: 12,
                    xfontWeight: "normal",
                    xfontStyle: "normal",
                    xfontFamily: "Helvetica, Arial, Verdana, sans-serif",
                    xtextAnchor: "end",
                    xrotation: 300,
                    barWidth: 35,
                    barGap: 20,
                    displayValues: false,
                    //title: "Sales by Month",
                    titleFontSize: "24px",
                    titleFontWeight: "normal",
                    titleFontStyle: "normal",
                    titleFontFamily: "Helvetica, Arial, Verdana, sans-serif",
                    titleFontColor: "#000",
                    //xTitle: "Months",
                    xTitleFontSize: 16,
                    xTitleFontWeight: "normal",
                    xTitleFontStyle: "normal",
                    xTitleFontFamily: "Helvetica, Arial, Verdana, sans-serif",
                    xTitleFontColor: "#444",
                    //yTitle: "Gross Value",
                    yTitleFontSize: 16,
                    yTitleFontWeight: "normal",
                    yTitleFontStyle: "normal",
                    yTitleFontFamily: "Helvetica, Arial, Verdana, sans-serif",
                    yTitleFontColor: "#444",
                    threeD: true,
                    depth: 15,
                    animationTime: 750,
                    gridBackgroundColor: "#dbd39a",
                    gridBanding: true,
                    gridOpacity: 1.0,
                    gridBackgroundImage: "",
                    gridLineWidth: 0.3,
                    gridLineStyle: ". ",
                    gridAxisWidth: 2,
                    gridAxisStyle: "- ",
                    gridNRows: 6,
                    gridColSpacing: 40,
                    gridRowSpacing: 50,
                    gridxPos: 75,
                    gridyPos: 400,
                    xLabelPre: "",
                    xLabelPost: "",
                    yLabelColor: "#777",
                    //yLabelPre: "$",
                    yLabelPost: "",
                    x2LabelPre: "",
                    x2LabelPost: "",
                    y2LabelPre: "",
                    y2LabelPost: "",
                    ndecplaces: 0,
                    ndecplaces2: 3,
                    labelFontSize: 11,
                    labelFontWeight: "lighter",
                    labelFontStyle: "normal",
                    labelFontFamily: "Helvetica, Arial, Verdana, sans-serif",
                    labelFontColor: "#f62",
                    xrotation: 315,
                    legendXpos: 300,
                    legendYpos: 20,
                    legendPadding: 6,
                    legendRoundRadius: 6,
                    legendOpacity: 0.7,
                    legendBackground: true,
                    legendBackgoundColor: '#ffffff',
                    legendBorderColor: '#888',
                    legendBorderWidth: 1,
                    legendStyle: 'vertical',
                    //legendTitle: 'Products',
                    legendTitleFontSize: 14,
                    legendTitleFontWeight: 'normal',
                    legendTitleFontStyle: 'normal',
                    legendTitleFontFamily: 'Helvetica, Arial, Verdana, sans-serif',
                    legendTitleFontColor: '#000',
                    legendFontSize: 12,
                    legendFontWeight: 'normal',
                    legendFontStyle: 'normal',
                    legendFontFamily: 'Helvetica, Arial, Verdana, sans-serif'
                    //legendFontColor: '#444'
                });
        }
        else {
            document.getElementById('listadoObjetivos').style.display = 'none';
            document.getElementById('listadoGrafica').style.display = 'none';
            document.getElementById('listadoObjetivos').innerHTML = 'NO SE ENCONTR&Oacute; INFORMACI&Oacute;N EN LA BASE DE DATOS.';
        }
    } catch (elError) {
        //alert(elError)
    }
}


function listarActividadesAbiertas(pag) {

    pagGlobal = pag;

    var responsable = document.getElementById('selResponsable').value;
    var fechaHoy = fechaSistema();                  //  se usan estas fechas para realizar el filtro del las activiade abierta para que muestre unicamente
    var fechaTomorrow = fechaSistemaTomorrow();     //  las actividades del día de hoy y de mañana  

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listarActividadesAbiertasPresentacion'));
    arrayParameters.push(newArg('responsableSesion', responsable));
    arrayParameters.push(newArg('fechaHoy', fechaHoy));
    arrayParameters.push(newArg('fechaTomorrow', fechaTomorrow));
    arrayParameters.push(newArg('pag', pag));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlPaginador.aspx', send, listarActividadesAbiertas_processResponse);
}

function listarActividadesAbiertas_processResponse(res) {

    try {
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('listadoPendientes');
        if (res != '0') {
            ocultaVentanaProgreso();
            //document.getElementById('listadoPendientes').style.display = 'block';
            document.getElementById('listadoPendientes').style.display = 'block';

            var datosRows = info.data;
            var cols = info.cols;
            var ctl = true, claseAplicar = "", claseAplicar1 = "";
            var colorTexto = "";
            var id = "", tipoActividad = "", nombreTipoActividad = "", fecha = "", horaInicio = "", horaFin = "", idCliente = "", nombreCliente = "", idOportunidad = "", nombreOportunidad = "";
            var idContacto = "", nombreContacto = "", objetivo = "", lugar = "", responsableSesion = "", nombreResponsable = "", observaciones = "", mostrarHoraFin = "";
            var id1 = "", nombre = "", tipoDocumento = "", nombreTipoDocumento = "", codigoTercero = "", pais = "", ciudad = "", direccion = "", telefono = "", pagina = "", sector = "", nombreSector = "";
            var responsableSesion = "", nombreResponsable = "", otrosDatos = "", estado = "";

            tabla = "<table class='tbListado centrar' style='text-align:center;'><tr><td class='encabezado' colspan='4'>ACTIVIDADES PENDIENTES EXISTENTES</td></tr>";
            tabla += "<tr><td style='width:7%' align='center' class='encabezado'></td><td style='width:25%' align='center' class='encabezado'>ACTIVIDAD</td><td class='encabezado'>CLIENTE</td><td class='encabezado' style='width:25%'>FECHA | HORAS</td></tr>";
            //tabla += "<td class='encabezado' style='width:9%'>DETALLE</td><td class='encabezado' style='width:10%'>EJECUTAR</td><td class='encabezado' style='width:8%'>EDITAR</td><td class='encabezado' style='width:9%'>ELIMINAR</td></tr>";

            for (var i = 0; i < datosRows.length; i += cols) {
                id = datosRows[i];
                tipoActividad = datosRows[i + 1];
                nombreTipoActividad = datosRows[i + 2];
                fecha = datosRows[i + 3];
                horaInicio = datosRows[i + 4];
                horaFin = datosRows[i + 5];
                idCliente = datosRows[i + 6];
                nombreCliente = datosRows[i + 7];
                idOportunidad = datosRows[i + 8];
                nombreOportunidad = datosRows[i + 9];
                idContacto = datosRows[i + 10];
                nombreContacto = datosRows[i + 11];
                objetivo = datosRows[i + 12];
                lugar = datosRows[i + 13];
                responsableSesion = datosRows[i + 14];
                nombreResponsable = datosRows[i + 15];
                observaciones = datosRows[i + 16];
                id1 = datosRows[i + 17];
                nombre = datosRows[i + 18];
                tipoDocumento = datosRows[i + 19];
                nombreTipoDocumento = datosRows[i + 20];
                codigoTercero = datosRows[i + 21];
                pais = datosRows[i + 22];
                ciudad = datosRows[i + 23];
                direccion = unescape(datosRows[i + 24]).toUpperCase();
                telefono = datosRows[i + 25];
                pagina = datosRows[i + 26];
                sector = datosRows[i + 27];
                nombreSector = datosRows[i + 28];
                responsableSesion1 = datosRows[i + 29];
                nombreResponsable1 = datosRows[i + 30];
                otrosDatos = datosRows[i + 31];
                estado = datosRows[i + 32];
                diferenciaDias = datosRows[i + 33];
                idEtapa = datosRows[i + 33];

                if (diferenciaDias <= 0) {
                    colorTexto = "textoRojo";
                }
                else if (diferenciaDias > 0 && diferenciaDias < 5) {
                    colorTexto = "textoAmarillo";
                }
                else if (diferenciaDias >= 5) {
                    colorTexto = "textoVerde";
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

                var icono = nombreTipoActividad.split("-");

                tabla += '<td align="center" class="' + claseAplicar + '"><img height="16px" width="16px" src=' + icono[0] + '></td><td class="' + claseAplicar + ' ' + colorTexto + '"> <div class="' + colorTexto + ' alineacion" onclick="ejecutar(\'' + id + '\', \'' + icono[1] + '\', \'' + fecha + '\', \'' + horaInicio + '\', \'' + horaFin + '\', \'' + objetivo + '\', \'' + idCliente + '\', \'' + nombre + '\', \'' + nombreContacto + '\', \'' + nombreOportunidad + '\', \'' + nombreResponsable + '\', \'' + 1 + '\', \'' + idOportunidad + '\', \'' + idEtapa + '\');" ><p>' + icono[1] + '</p></div></td>';
                tabla += '<td align="center" class="' + claseAplicar + '"><div class="' + colorTexto + ' alineacion" onclick="detalle(\'' + id1 + '\', \'' + nombre + '\', \'' + nombreTipoDocumento + '\', \'' + codigoTercero + '\', \'' + pais + '\', \'' + ciudad + '\', \'' + direccion + '\', \'' + telefono + '\', \'' + pagina + '\', \'' + nombreSector + '\', \'' + nombreResponsable + '\', \'' + otrosDatos + '\');" ><p>' + nombreCliente + " " + nombreContacto + '</p></div></td>';
                tabla += '<td align="center" class="' + claseAplicar + ' ' + colorTexto + '">' + fecha + " | " + DeMilitar_AmPm(horaInicio) + " - " + DeMilitar_AmPm(horaFin) + '</td>';
                tabla += '</td></tr>';
            }
            tabla += '</table>'

            divTerceros.innerHTML = tabla;
            divTerceros.innerHTML += pieDePaginaListar(info, 'listarActividadesAbiertas');
            var idMenuForm = document.getElementById('idMenuForm').innerHTML; // se adiciona esta linea para que se de  el permiso de visualizar el editar despues de cargar
            // permisosParaMenu(idMenuForm); // se adiciona esta linea para que se el permiso de visualizar el editar despues de cargar
        } else {
            document.getElementById('listadoPendientes').style.display = 'none';
            divTerceros.innerHTML = 'NO HAY INFORMACIÓN EN LA BASE DE DATOS';
        }
    } catch (elError) {
    }
}


//function detallePrueba(id,nombre,nombreTipoDocumento,codigoTercero,pais,ciudad,direccion,telefono,pagina,nombreSector,nombreResponsable,otrosDatos) {
//    muestraVentana('ventana detalle de prueba');
////    globalIdClientes = id;

////    document.getElementById('ckHabilitar').checked = false;
////    document.getElementById('ckContactos').checked = false;
////    document.getElementById('listadoContactos').style.display = 'none';
////    document.getElementById('ckArchivos').checked = false;
////    document.getElementById('listadoArchivos').style.display = 'none';

////    var vacio = '--';

////    tabla = "<table class='tbListado centrar' style='text-align: center;'>";
////    tabla += "<tr><td class='encabezado' colspan='5'>INFORMACIÓN GENERAL</td></tr>";
////    tabla += "<tr><td class='encabezado'>NOMBRE</td><td class='encabezado'>TIPO DOCUMENTO</td><td class='encabezado'>CÓDIGO TERCERO</td><td class='encabezado'>PAIS</td><td class='encabezado'>CIUDAD</td></tr>";

////    tabla += '<tr>';
////    tabla += '<td class="cuerpoListado10">' + ((nombre == "") ? vacio : nombre) + '</td>';
////    tabla += '<td class="cuerpoListado10">' + ((nombreTipoDocumento == "") ? vacio : nombreTipoDocumento) + '</td>';
////    tabla += '<td class="cuerpoListado10">' + ((codigoTercero == "") ? vacio : codigoTercero) + '</td>';
////    tabla += '<td class="cuerpoListado10">' + ((pais == "") ? vacio : pais) + '</td>';
////    tabla += '<td class="cuerpoListado10">' + ((ciudad == "") ? vacio : ciudad) + '</td>';

////    tabla += '</tr>';

////    tabla += "<tr><td class='encabezado'>DIRECCIÓN</td><td class='encabezado'>TELÉFONO</td><td class='encabezado'>PAGINA</td><td class='encabezado'>SECTOR</td><td class='encabezado'>RESPONSABLE</td></tr>";

////    tabla += '<tr>';
////    tabla += '<td class="cuerpoListado10">' + ((direccion == "") ? vacio : direccion) + '</td>';
////    tabla += '<td class="cuerpoListado10">' + ((telefono == "") ? vacio : telefono) + '</td>';
////    tabla += '<td class="cuerpoListado10">' + ((pagina == "") ? vacio : pagina) + '</td>';
////    tabla += '<td class="cuerpoListado10">' + ((nombreSector == "") ? vacio : nombreSector) + '</td>';
////    tabla += '<td class="cuerpoListado10">' + ((nombreResponsable == "") ? vacio : nombreResponsable) + '</td>';
////    tabla += '</tr>';

////    tabla += "<tr><td class='encabezado' colspan='5'>OTROS DATOS</td></tr>";
////    tabla += '<tr>';
////    tabla += '<td class="cuerpoListado10" colspan="5">' + ((otrosDatos == "") ? vacio : otrosDatos) + '</td>';
////    tabla += '</tr>';

////    tabla += '</table>';

////    document.getElementById('listadoDetalle').innerHTML = tabla;

////    listarDetalleContactos();
////    listarDetalleArchivos();

////    $.fancybox({
////        'showCloseButton': true,
////        'hideOnOverlayClick': true,
////        'transitionIn': 'fade',
////        'transitionOut': 'fade',
////        'transitionOut': 'fade',
////        'enableEscapeButton': true,
////        'href': '#divDetalle',
////        'onClosed': function () {
////            // limpiar();
////        }
////    });
//}

//////////////////////////////////////////////////////////////////////
//
//
//
////////////////////////////////////////////////////////////////////////

//function listarActividadesAbiertas() {

//    var responsable = document.getElementById('selResponsable').value;

//    var fechaHoy = fechaSistema();                  //  se usan estas fechas para realizar el filtro del las activiade abierta para que muestre unicamente
//    var fechaTomorrow = fechaSistemaTomorrow();     //  las actividades del día de hoy y de mañana  

//    var arrayParameters = new Array();
//    arrayParameters.push(newArg('p', 'listarActividadesAbiertas'));
//    arrayParameters.push(newArg('responsableSesion', responsable));
//    arrayParameters.push(newArg('fechaHoy', fechaHoy));
//    arrayParameters.push(newArg('fechaTomorrow', fechaTomorrow));
//    var send = arrayParameters.join('&');
//    $.post('../../Controlador/ctlPresentacion.aspx', send, listarActividadesAbiertas_processResponse);
//}

//function listarActividadesAbiertas_processResponse(res) {
//    var info = eval('(' + res + ')');
//    var msj = info.msj;
//    switch (msj) {
//        case -1:
//            muestraVentana('SIN CONEXIÓN A LA BASE DE DATOS.');
//            break;
//        case 0:
//            document.getElementById('listadoPendientes').style.display = 'none';
//            document.getElementById('listadoPendientes').innerHTML = '';
//            break;
//        case 1:
//            ocultaVentanaProgreso();
//            document.getElementById('listadoPendientes').style.display = 'block';
//            var datosRows = info.data;
//            var cols = info.cols;
//            var ctl = true, claseAplicar = "", claseAplicar1 = "";
//            var id = "", tipoActividad = "", nombreTipoActividad = "", fecha = "", horaInicio = "", horaFin = "", idCliente = "", nombreCliente = "", idOportunidad = "", nombreOportunidad = "";
//            var idContacto = "", nombreContacto = "", objetivo = "", lugar = "", responsableSesion = "", nombreResponsable = "", observaciones = "", mostrarHoraFin = "";
//            var id1 = "", nombre = "", tipoDocumento = "", nombreTipoDocumento = "", codigoTercero = "", pais = "", ciudad = "", direccion = "", telefono = "", pagina = "", sector = "", nombreSector = "";
//            var responsableSesion = "", nombreResponsable = "", otrosDatos = "", estado = "";

//            tabla = "<table class='tbListado centrar' style='text-align:center;'><tr><td class='encabezado' colspan='8'>ACTIVIDADES ABIERTAS EXISTENTES</td></tr>";
//            tabla += "<tr><td style='width:10%' align='center' class='encabezado'></td><td style='width:28%' align='center' class='encabezado'>ACTIVIDAD</td><td class='encabezado'>CLIENTE</td><td class='encabezado' style='width:11%'>FECHA</td></tr>";
//            //tabla += "<td class='encabezado' style='width:9%'>DETALLE</td><td class='encabezado' style='width:10%'>EJECUTAR</td><td class='encabezado' style='width:8%'>EDITAR</td><td class='encabezado' style='width:9%'>ELIMINAR</td></tr>";

//            for (var i = 0; i < datosRows.length; i += cols) {
//                id = datosRows[i];
//                tipoActividad = datosRows[i + 1];
//                nombreTipoActividad = datosRows[i + 2];
//                fecha = datosRows[i + 3];
//                horaInicio = datosRows[i + 4];
//                horaFin = datosRows[i + 5];
//                idCliente = datosRows[i + 6];
//                nombreCliente = datosRows[i + 7];
//                idOportunidad = datosRows[i + 8];
//                nombreOportunidad = datosRows[i + 9];
//                idContacto = datosRows[i + 10];
//                nombreContacto = datosRows[i + 11];
//                objetivo = datosRows[i + 12];
//                lugar = datosRows[i + 13];
//                responsableSesion = datosRows[i + 14];
//                nombreResponsable = datosRows[i + 15];
//                observaciones = datosRows[i + 16];
//                id1 = datosRows[i + 17];
//                nombre = datosRows[i + 18];
//                tipoDocumento = datosRows[i + 19];
//                nombreTipoDocumento = datosRows[i + 20];
//                codigoTercero = datosRows[i + 21];
//                pais = datosRows[i + 22];
//                ciudad = datosRows[i + 23];
//                direccion = datosRows[i + 24];
//                telefono = datosRows[i + 25];
//                pagina = datosRows[i + 26];
//                sector = datosRows[i + 27];
//                nombreSector = datosRows[i + 28];
//                responsableSesion = datosRows[i + 29];
//                nombreResponsable = datosRows[i + 30];
//                otrosDatos = datosRows[i + 31];
//                estado = datosRows[i + 32];

//                if (ctl) {
//                    claseAplicar = "cuerpoListado7";
//                    claseAplicar1 = "cuerpoListado1";
//                }
//                else {
//                    claseAplicar = "cuerpoListado8";
//                    claseAplicar1 = "cuerpoListado2";
//                }
//                ctl = !ctl;

//                //                if (horaFin == "")
//                //                    mostrarHoraFin = "";
//                //                else
//                //                    mostrarHoraFin = " - " + horaFin;

//                //                var icono = nombreTipoActividad.split("-");
//                var icono = nombreTipoActividad.split("-");
//                var partirHoraInicio = horaInicio.split(":");
//                horasInicio = partirHoraInicio[0];
//                minutosInicio = partirHoraInicio[1];
//                var partirHoraFin = horaFin.split(":");
//                horasFin = partirHoraFin[0];
//                minutosFin = partirHoraFin[1];

//                if (horasFin == "-H-" || minutosFin == "-M-") {
//                    mostrarHoraFin = "";
//                    detalleHoraFin = "";
//                }
//                else {
//                    mostrarHoraFin = " - " + horaFin;
//                    detalleHoraFin = horaFin;
//                }

//                tabla += '<td align="center" class="' + claseAplicar + '"><img height="16px" width="16px" src=' + icono[0] + '></td><td class="' + claseAplicar + '"> <div id="imgEditar" class="linkIconoLateral botonEditar " onclick="ejecutar(\'' + id + '\', \'' + icono[1] + '\', \'' + fecha + '\', \'' + horasInicio + '\', \'' + minutosInicio + '\', \'' + horasFin + '\', \'' + minutosFin + '\', \'' + mostrarHoraFin + '\', \'' + objetivo + '\', \'' + idCliente + '\', \'' + nombreContacto + '\', \'' + nombreOportunidad + '\', \'' + nombreResponsable + '\');" ><p>' + icono[1] + '</p></div></td>';
//                tabla += '<td align="center" class="' + claseAplicar + '"><div id="imgEditar" class="linkIconoLateral botonEditar  onclick="detalle(\'' + id1 + '\', \'' + nombre + '\', \'' + nombreTipoDocumento + '\', \'' + codigoTercero + '\', \'' + pais + '\', \'' + ciudad + '\', \'' + direccion + '\', \'' + telefono + '\', \'' + pagina + '\', \'' + nombreSector + '\', \'' + nombreResponsable + '\', \'' + otrosDatos + '\');" ><p>' + nombreCliente + " " + nombreContacto + '</p></div></td>';
//                tabla += '<td align="center" class="' + claseAplicar + '">' + fecha + " " + horaInicio + mostrarHoraFin + '</td>';
//                tabla += '</td></tr>';
//            }
//            tabla += '</table>';
//            document.getElementById('listadoPendientes').innerHTML = tabla;
//            // divLista.innerHTML = tabla;


//            break;
//    }
//}

//////////////////////////////////////////////////////////////////////
//
//
//
////////////////////////////////////////////////////////////////////////


//function ejecutar(id, nombreTipoActividad, fecha, horaInicio, horaFin, mostrarHoraFin, objetivo, nombreCliente, nombreContacto, nombreOportunidad, nombreResponsable) {
//    globalIdActividadesAbiertas = id;

//    var vacio = '--';
//    var tabla = '';

//    tabla = "<table class='tbListado centrar' style='text-align: center;'>";
//    //tabla += "<tr><td class='encabezado' colspan='4'>INFORMACIÓN GENERAL</td></tr>";
//    tabla += "<tr><td class='encabezado'>TIPO DE ACTIVIDAD</td>";

//    tabla += '<td class="cuerpoListado10">' + ((nombreTipoActividad == "") ? vacio : nombreTipoActividad) + '</td>';
//    tabla += '</tr>';

//    tabla += "<tr><td class='encabezado'>FECHA / HORA</td>";

//    tabla += '<td class="cuerpoListado10">' + ((fecha + " " + horaInicio + mostrarHoraFin == "") ? vacio : fecha + " " + horaInicio + mostrarHoraFin) + '</td>';
//    tabla += '</tr>';

//    tabla += "<tr><td class='encabezado'>OBJETIVO</td>";

//    tabla += '<td class="cuerpoListado10">' + ((objetivo == "") ? vacio : objetivo) + '</td>';
//    tabla += '</tr>';

//    tabla += "<tr><td class='encabezado'>CLIENTE</td>";

//    tabla += '<td class="cuerpoListado10">' + ((nombreCliente == "") ? vacio : nombreCliente) + '</td>';
//    tabla += '</tr>';

//    tabla += "<tr><td class='encabezado'>CONTACTO</td>";

//    tabla += '<td class="cuerpoListado10">' + ((nombreContacto == "") ? vacio : nombreContacto) + '</td>';
//    tabla += '</tr>';

//    tabla += "<tr><td class='encabezado'>OPORTUNIDAD</td>";

//    tabla += '<td class="cuerpoListado10">' + ((nombreOportunidad == "") ? vacio : nombreOportunidad) + '</td>';
//    tabla += '</tr>';

//    tabla += "<tr><td class='encabezado'>RESPONSABLE</td>";

//    tabla += '<td class="cuerpoListado10">' + ((nombreResponsable == "") ? vacio : nombreResponsable) + '</td>';
//    tabla += '</tr>';

//    tabla += '</table>';

//    document.getElementById('listadoDetalleEjecutar').innerHTML = tabla;

//    document.getElementById('txtFechaEjecucion').value = fecha;
//    document.getElementById('txtHoraInicioEjecucion').value = horaInicio;
//    document.getElementById('txtHoraFinEjecucion').value = horaFin;

//    $.fancybox({
//        'showCloseButton': true,
//        'hideOnOverlayClick': true,
//        'transitionIn': 'fade',
//        'transitionOut': 'fade',
//        'transitionOut': 'fade',
//        'enableEscapeButton': true,
//        'href': '#divEjecutar',
//        'onClosed': function () {
//            //  limpiar();
//        }
//    });
//}


function ejecutar(id, nombreTipoActividad, fecha, horasInicio, horasFin, objetivo, idCliente, nombreCliente, nombreContacto, nombreOportunidad, nombreResponsable, validar, idOportunidad, idEtapa) {
    //function ejecutar(id, nombreTipoActividad, fecha, horaInicio, horaFin, mostrarHoraFin, objetivo, nombreCliente, nombreContacto, nombreOportunidad, nombreResponsable) {
    globalEtapaActual = idEtapa;
    globalIdActividadesAbiertas = id;
    listaArchivosActividad(1);

    document.getElementById('lblTipoActividad').innerHTML = nombreTipoActividad;
    document.getElementById('lblFechaHora').innerHTML = fecha + "   " + DeMilitar_AmPm(horasInicio) + " - " + DeMilitar_AmPm(horasFin);
    document.getElementById('lblObjetivo').innerHTML = objetivo;
    document.getElementById('lblCliente').innerHTML = nombreCliente;
    document.getElementById('lblPersona').innerHTML = nombreContacto;
    document.getElementById('lblOportunidad').innerHTML = nombreOportunidad;
    document.getElementById('lblResponsable').innerHTML = nombreResponsable;

    if (parseInt(horasInicio.substring(0, 2)) > 12) {
        document.getElementById('AmPmHoraInicioEjecucion').value = 'PM';
        var hora = (parseInt(horasInicio.substring(0, 2)) - 12);
        if (hora < 10) {
            horasInicio = '0' + (parseInt(horasInicio.substring(0, 2)) - 12) + horasInicio.substring(2, 5);
        }
        else {
            horasInicio = (parseInt(horasInicio.substring(0, 2)) - 12) + horasInicio.substring(2, 5);
        }
    }
    else if (parseInt(horasInicio.substring(0, 2)) == 12) {
        document.getElementById('AmPmHoraInicioEjecucion').value = 'PM';
    }
    else {
        if (horasInicio.substring(0, 2) == '00') {
            document.getElementById('AmPmHoraInicioEjecucion').value = 'AM';
            horasInicio = '12' + horasInicio.substring(2, 5);
        }
        else {
            document.getElementById('AmPmHoraInicioEjecucion').value = 'AM';
        }
    }

    document.getElementById('horaInicioEjecucion').value = horasInicio;

    if (parseInt(horasFin.substring(0, 2)) > 12) {
        document.getElementById('AmPmHoraFinEjecucion').value = 'PM';
        var hora = (parseInt(horasFin.substring(0, 2)) - 12);
        if (hora < 10) {
            horasFin = '0' + (parseInt(horasFin.substring(0, 2)) - 12) + horasFin.substring(2, 5);
        }
        else {
            horasFin = (parseInt(horasFin.substring(0, 2)) - 12) + horasFin.substring(2, 5);
        }
    }
    else if (parseInt(horasFin.substring(0, 2)) == 12) {
        document.getElementById('AmPmHoraFinEjecucion').value = 'PM';
    }
    else {
        if (horasFin.substring(0, 2) == '00') {
            document.getElementById('AmPmHoraFinEjecucion').value = 'AM';
            horasFin = '12' + horasFin.substring(2, 5);
        }
        else {
            document.getElementById('AmPmHoraFinEjecucion').value = 'AM';
        }
    }

    document.getElementById('horaFinEjecucion').value = horasFin;

    document.getElementById('txtFechaEjecucion').value = fecha;
    muestraVentanaProgreso();
    consultarEtapa(idOportunidad);
}

function consultarEtapa(idOportunidad) {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'consultarEtapa'));
    arrayParameters.push(newArg('actividadesAbiertas', '1'));
    arrayParameters.push(newArg('idOportunidad', idOportunidad));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, consultarEtapa_processResponse);
}

function consultarEtapa_processResponse(res) {
    var info = eval('(' + res + ')');
    var msj = info.msj;
    switch (msj) {
        case -1:
            ocultaVentanaProgreso();
            muestraVentana('SIN CONEXIÓN A LA BASE DE DATOS.');
            break;
        case 0:
            //llenarSelect(unescape(res), document.getElementById('selEtapa'));
            ocultaVentanaProgreso();

            $.fancybox({
                'showCloseButton': true,
                'hideOnOverlayClick': true,
                'transitionIn': 'fade',
                'transitionOut': 'fade',
                'transitionOut': 'fade',
                'enableEscapeButton': true,
                'href': '#divEjecutar',
                'onClosed': function () {
                    limpiar();
                }
            });
            var tabla = '';
            tabla += '<iframe id="iframeArchivo" style="width:390px; height:100px" src="../../Controlador/ctlArchivosActividad.aspx?id=' + globalIdActividadesAbiertas + '" class="iframeArchivo" runat="server"></iframe>';
            document.getElementById('iframeArchivo').innerHTML = tabla;
            break;
        case 1:

            // llenarSelect(unescape(res), document.getElementById('selEtapa'));
            globalIdEtapaProximo = info.data[2];
            ocultaVentanaProgreso();
            document.getElementById('txtActual').value = info.data[1];
            document.getElementById('txtSiguiente').value = info.data[3];

            if (info.data[3] === undefined) {
                document.getElementById('txtSiguiente').value = '';
                document.getElementById('cambiarEtapa').style.display = 'none';
                document.getElementById("etapaNO").checked = true;
            }
            else {
                document.getElementById('cambiarEtapa').style.display = 'block';
            }

            document.getElementById('txtActual').disabled = true;
            document.getElementById('txtSiguiente').disabled = true;
            $.fancybox({
                'showCloseButton': true,
                'hideOnOverlayClick': true,
                'transitionIn': 'fade',
                'transitionOut': 'fade',
                'transitionOut': 'fade',
                'enableEscapeButton': true,
                'href': '#divEjecutar',
                'onClosed': function () {
                    limpiar();
                }
            });
            var tabla = '';
            tabla += '<iframe id="iframeArchivo" style="width:390px; height:100px" src="../../Controlador/ctlArchivosActividad.aspx?id=' + globalIdActividadesAbiertas + '" class="iframeArchivo" runat="server"></iframe>';
            document.getElementById('iframeArchivo').innerHTML = tabla;
            break;
    }
}

function CambiarEtapa(res) {
    if (res == "SI") {
        document.getElementById("etapaSI").checked = true;
        document.getElementById("etapaNO").checked = false;
        nuevaEtapa = globalIdEtapaProximo;

    }
    else {
        document.getElementById("etapaSI").checked = false;
        document.getElementById("etapaNO").checked = true;
        nuevaEtapa = globalEtapaActual;
    }
}


//function guardaEjecutarSeguimiento() {
//    var siActividad = document.getElementById("rbSiActividad").checked;
//    var noActividad = document.getElementById("rbNoActividad").checked;
//    var siObjetivo = document.getElementById("rbSiObjetivo").checked;
//    var noObjetivo = document.getElementById("rbNoObjetivo").checked;

//    var fecha = document.getElementById('txtFechaEjecucion').value;
//    var horasInicio = document.getElementById('selHorasInicioEjecucion').value;
//    var minutosInicio = document.getElementById('selMinutosInicioEjecucion').value;
//    var horasFin = document.getElementById('selHorasFinEjecucion').value;
//    var minutosFin = document.getElementById('selMinutosFinEjecucion').value;
//    var observaciones = document.getElementById('txtObservacionesEjecutar').value;

//    if (siActividad == true) {
//        if (fecha != "" && horasInicio != "-H-" && minutosInicio != "-M-" && horasFin != "-H-" && minutosFin != "-M-") {
//            var arrayParameters = new Array();
//            arrayParameters.push(newArg('p', 'guardaEjecutarSeguimiento'));
//            arrayParameters.push(newArg('idActividadesAbiertas', globalIdActividadesAbiertas));
//            arrayParameters.push(newArg('siActividad', siActividad));
//            arrayParameters.push(newArg('noActividad', noActividad));
//            arrayParameters.push(newArg('siObjetivo', siObjetivo));
//            arrayParameters.push(newArg('noObjetivo', noObjetivo));
//            arrayParameters.push(newArg('fecha', fecha));
//            arrayParameters.push(newArg('horasInicio', horasInicio));
//            arrayParameters.push(newArg('minutosInicio', minutosInicio));
//            arrayParameters.push(newArg('horasFin', horasFin));
//            arrayParameters.push(newArg('minutosFin', minutosFin));
//            arrayParameters.push(newArg('observaciones', escape(observaciones)));
//            var send = arrayParameters.join('&');
//            $.post('../../Controlador/ctlActividadesAbiertas.aspx', send, guardaEjecutarSeguimiento_processResponse);
//        } else
//            muestraVentana('DEBE INGRESAR LOS CAMPOS OBLIGATORIOS.');
//    } else {
//        fecha = document.getElementById('txtFechaEjecucion').value = "";
//        horasInicio = document.getElementById('selHorasInicioEjecucion').value = "-H-";
//        minutosInicio = document.getElementById('selMinutosInicioEjecucion').value = "-M-";
//        horasFin = document.getElementById('selHorasFinEjecucion').value = "-H-";
//        minutosFin = document.getElementById('selMinutosFinEjecucion').value = "-M-";
//        var arrayParameters = new Array();
//        arrayParameters.push(newArg('p', 'guardaEjecutarSeguimiento'));
//        arrayParameters.push(newArg('idActividadesAbiertas', globalIdActividadesAbiertas));
//        arrayParameters.push(newArg('siActividad', siActividad));
//        arrayParameters.push(newArg('noActividad', noActividad));
//        arrayParameters.push(newArg('siObjetivo', siObjetivo));
//        arrayParameters.push(newArg('noObjetivo', noObjetivo));
//        arrayParameters.push(newArg('fecha', fecha));
//        arrayParameters.push(newArg('horasInicio', horasInicio));
//        arrayParameters.push(newArg('minutosInicio', minutosInicio));
//        arrayParameters.push(newArg('horasFin', horasFin));
//        arrayParameters.push(newArg('minutosFin', minutosFin));
//        arrayParameters.push(newArg('observaciones', observaciones));
//        var send = arrayParameters.join('&');
//        $.post('../../Controlador/ctlActividadesAbiertas.aspx', send, guardaEjecutarSeguimiento_processResponse);
//    }
//}

//function guardaEjecutarSeguimiento_processResponse(res) {
//    try {
//        var info = eval('(' + res + ')');
//        switch (info) {
//            case -2:
//                muestraVentana('SIN CONEXI&Oacute;N A LA BASE DE DATOS.');
//                break;
//            case 0:
//                muestraVentana('NO SE ENCONTR&Oacute; INFORMACI&Oacute;N.');
//                break;
//            case 1:
//                muestraVentana('INFORMACI&Oacute;N ALMACENADA CORRECTAMENTE.');
//                listarActividadesAbiertas(1);
//                limpiar();
//                cerrarFancy();
//                break;
//        }
//    } catch (elError) {
//    }
//}

function guardaEjecutarSeguimiento() {
    var siActividad = document.getElementById("rbSiActividad").checked;
    var noActividad = document.getElementById("rbNoActividad").checked;
    var siObjetivo = document.getElementById("rbSiObjetivo").checked;
    var noObjetivo = document.getElementById("rbNoObjetivo").checked;

    var cambioEtapa = document.getElementById("etapaSI").checked;

    var fecha = document.getElementById('txtFechaEjecucion').value;

    var observaciones = document.getElementById('txtObservacionesEjecutar').value;

    var horasInicio = document.getElementById('horaInicioEjecucion').value;

    var AmPmInicio = document.getElementById('AmPmHoraInicioEjecucion').value;
    if (AmPmInicio == 'PM') {
        if (parseInt(horasInicio.substring(0, 2)) != 12) {
            horasInicio = (parseInt(horasInicio.substring(0, 2)) + 12) + horasInicio.substring(2, 5);
        }
    }
    else {
        if (parseInt(horasInicio.substring(0, 2)) == 12) {
            horasInicio = '00' + horasInicio.substring(2, 5);
        }
    }

    var horasFin = document.getElementById('horaFinEjecucion').value;

    var AmPmFin = document.getElementById('AmPmHoraFinEjecucion').value;
    if (AmPmFin == 'PM') {
        if (parseInt(horasFin.substring(0, 2)) != 12) {
            horasFin = (parseInt(horasFin.substring(0, 2)) + 12) + horasFin.substring(2, 5);
        }
    }
    else {
        if (parseInt(horasFin.substring(0, 2)) == 12) {
            horasFin = '00' + horasFin.substring(2, 5);
        }
    }

    if (globalEtapaActual == nuevaEtapa) {
        if (siActividad == true) {
            if (fecha != "" && horasInicio != "" && horasFin != "") {
                var arrayParameters = new Array();
                arrayParameters.push(newArg('p', 'guardaEjecutarSeguimiento'));
                arrayParameters.push(newArg('idActividadesAbiertas', globalIdActividadesAbiertas));
                arrayParameters.push(newArg('siActividad', siActividad));
                arrayParameters.push(newArg('noActividad', noActividad));
                arrayParameters.push(newArg('siObjetivo', siObjetivo));
                arrayParameters.push(newArg('noObjetivo', noObjetivo));
                arrayParameters.push(newArg('fecha', fecha));
                arrayParameters.push(newArg('horasInicio', horasInicio));
                //arrayParameters.push(newArg('minutosInicio', minutosInicio));
                arrayParameters.push(newArg('horasFin', horasFin));
                //arrayParameters.push(newArg('minutosFin', minutosFin));
                arrayParameters.push(newArg('observaciones', escape(observaciones)));
                var send = arrayParameters.join('&');
                $.post('../../Controlador/ctlActividadesAbiertas.aspx', send, guardaEjecutarSeguimiento_processResponse);
            } else
                muestraVentana('DEBE INGRESAR LOS CAMPOS OBLIGATORIOS.');
        } else {
            fecha = document.getElementById('txtFechaEjecucion').value = "";
            horasInicio = document.getElementById('horaInicioEjecucion').value = "";
            //minutosInicio = document.getElementById('selMinutosInicioEjecucion').value = "-M-";
            horasFin = document.getElementById('horaFinEjecucion').value = "";
            //minutosFin = document.getElementById('selMinutosFinEjecucion').value = "-M-";
            var arrayParameters = new Array();
            arrayParameters.push(newArg('p', 'guardaEjecutarSeguimiento'));
            arrayParameters.push(newArg('idActividadesAbiertas', globalIdActividadesAbiertas));
            arrayParameters.push(newArg('siActividad', siActividad));
            arrayParameters.push(newArg('noActividad', noActividad));
            arrayParameters.push(newArg('siObjetivo', siObjetivo));
            arrayParameters.push(newArg('noObjetivo', noObjetivo));
            arrayParameters.push(newArg('fecha', fecha));
            arrayParameters.push(newArg('horasInicio', horasInicio));
            //arrayParameters.push(newArg('minutosInicio', minutosInicio));
            arrayParameters.push(newArg('horasFin', horasFin));
            //arrayParameters.push(newArg('minutosFin', minutosFin));
            arrayParameters.push(newArg('observaciones', observaciones));
            var send = arrayParameters.join('&');
            $.post('../../Controlador/ctlActividadesAbiertas.aspx', send, guardaEjecutarSeguimiento_processResponse);
        }
    }
    else {

        if (siActividad == true) {
            if (fecha != "" && horasInicio != "" && horasFin != "") {
                var arrayParameters = new Array();
                arrayParameters.push(newArg('p', 'guardaEjecutarSeguimiento'));
                arrayParameters.push(newArg('idActividadesAbiertas', globalIdActividadesAbiertas));
                arrayParameters.push(newArg('siActividad', siActividad));
                arrayParameters.push(newArg('noActividad', noActividad));
                arrayParameters.push(newArg('siObjetivo', siObjetivo));
                arrayParameters.push(newArg('noObjetivo', noObjetivo));
                arrayParameters.push(newArg('fecha', fecha));
                arrayParameters.push(newArg('horasInicio', horasInicio));
                //arrayParameters.push(newArg('minutosInicio', minutosInicio));
                arrayParameters.push(newArg('horasFin', horasFin));
                //arrayParameters.push(newArg('minutosFin', minutosFin));
                arrayParameters.push(newArg('observaciones', escape(observaciones)));
                var send = arrayParameters.join('&');
                $.post('../../Controlador/ctlActividadesAbiertas.aspx', send, guardaEjecutarSeguimiento_processResponse);

                /* cambio de etapa*/

            } else
                muestraVentana('DEBE INGRESAR LOS CAMPOS OBLIGATORIOS.');
        } else {
            fecha = document.getElementById('txtFechaEjecucion').value = "";
            horasInicio = document.getElementById('horaInicioEjecucion').value = "";
            //minutosInicio = document.getElementById('selMinutosInicioEjecucion').value = "-M-";
            horasFin = document.getElementById('horaFinEjecucion').value = "";
            //minutosFin = document.getElementById('selMinutosFinEjecucion').value = "-M-";
            var arrayParameters = new Array();
            arrayParameters.push(newArg('p', 'guardaEjecutarSeguimiento'));
            arrayParameters.push(newArg('idActividadesAbiertas', globalIdActividadesAbiertas));
            arrayParameters.push(newArg('siActividad', siActividad));
            arrayParameters.push(newArg('noActividad', noActividad));
            arrayParameters.push(newArg('siObjetivo', siObjetivo));
            arrayParameters.push(newArg('noObjetivo', noObjetivo));
            arrayParameters.push(newArg('fecha', fecha));
            arrayParameters.push(newArg('horasInicio', horasInicio));
            //arrayParameters.push(newArg('minutosInicio', minutosInicio));
            arrayParameters.push(newArg('horasFin', horasFin));
            //arrayParameters.push(newArg('minutosFin', minutosFin));
            arrayParameters.push(newArg('observaciones', observaciones));
            var send = arrayParameters.join('&');
            $.post('../../Controlador/ctlActividadesAbiertas.aspx', send, guardaEjecutarSeguimiento_processResponse);

            /*cambio de etapa*/
        }

    }
    if (cambioEtapa) {
        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', 'cambiaEtapaOportunidad'));
        arrayParameters.push(newArg('idOportunidad', actividadGlobal));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlAbiertas.aspx', send, '');
    }
}

function guardaEjecutarSeguimiento_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        switch (info) {
            case -2:
                muestraVentana('SIN CONEXI&Oacute;N A LA BASE DE DATOS.');
                break;
            case 0:
                muestraVentana('NO SE ENCONTR&Oacute; INFORMACI&Oacute;N.');
                break;
            case 1:
                muestraVentana('INFORMACI&Oacute;N ALMACENADA CORRECTAMENTE.');
                //cerrarFancy();

                //document.getElementById('txtCliente').value = clienteGlobal + ' - ' + empresaGlobal;
                //setTimeout("cargaOportunidades(" + globalCliente + ")", 500);
                //setTimeout("cargaPersona(" + globalCliente + ")", 800);
                //setTimeout("document.getElementById('selOportunidades').value = " + actividadGlobal, 800);
                //setTimeout("document.getElementById('selPersona').value = " + contactoGlobal, 1000);

                //$.fancybox({
                //    'showCloseButton': true,
                //    'hideOnOverlayClick': true,
                //    'transitionIn': 'fade',
                //    'transitionOut': 'fade',
                //    'transitionOut': 'fade',
                //    'enableEscapeButton': true,
                //    'href': '#divNuevaActividad',
                //    'onClosed': function () {
                //        limpiar();
                //    }
                //});

                listarActividadesAbiertas(pagGlobal);
                cerrarFancy();
                break;
        }
    } catch (elError) {
    }
}


function cerrarFancy() {
    $.fancybox.close();
}

function listaArchivosActividad(pag) {
    pagGlobalArchivos = pag;
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listarArchivosActividadesAbiertas'));
    arrayParameters.push(newArg('pag', pag));
    arrayParameters.push(newArg('idActividad', globalIdActividadesAbiertas));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlPaginador.aspx', send, listaArchivosActividad_processResponse);

}

function listaArchivosActividad_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var divLista = document.getElementById('listadoGeneralArchivos');
        var tabla = '';
        if (res != '0') {
            var datosRows = info.data;
            var cols = info.cols;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";
            var id = "", nombre = "";
            tabla = "<table class='tbListado centrar' style='text-align: center;'><tr><td class='encabezado' colspan='3'>HISTORIAL DE ARCHIVOS</td></tr>";
            tabla += "<tr><td class='encabezado'>NOMBRE ARCHIVO</td>";
            tabla += "<td class='encabezado' style='width:20%' >DESCARGAR</td><td class='encabezado' style='width:20%' >ELIMINAR</td></tr>";

            for (var i = 0; i < datosRows.length; i += cols) {
                id = datosRows[i];
                nombre = datosRows[i + 1];

                if (ctl) {
                    claseAplicar = "cuerpoListado7";
                    claseAplicar1 = "cuerpoListado1";
                }
                else {
                    claseAplicar = "cuerpoListado8";
                    claseAplicar1 = "cuerpoListado2";
                }
                ctl = !ctl;

                tabla += '<td class="' + claseAplicar + '">' + nombre + '</td>';

                tabla += '<td class="' + claseAplicar + '"><div id="imgEditar" class="linkIconoLateral botonEditar " onclick="descargarArchivos(\'' + id + '\',\'' + nombre + '\' );" ><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/descargar.png"><p>Descargar</p></div></td>';

                tabla += '<td class="' + claseAplicar1 + '"><div id="imgEliminar" class="linkIconoLateral botonEliminar ocultar" onclick="confirmaEliminarArchivos(\'' + id + '\');"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/eliminar_24x24.png"> <p>Eliminar</p></div>';

                tabla += '</td></tr>';
            }
            divLista.innerHTML = tabla;
            divLista.innerHTML += pieDePaginaListar(info, 'listaArchivosActividad'); /*llama de nuevo al paginador con la siguiente pagina*/
            var idMenuForm = document.getElementById('idMenuForm').innerHTML; /* se adiciona esta linea para que se el permiso de visualizar el editar despues de cargar*/
            permisosParaMenu(idMenuForm); /* se adiciona esta linea para que se el permiso de visualizar el editar despues de cargar*/
        } else {
            //$.fancybox.close();
            divLista.innerHTML = 'NO SE ENCONTRÁ INFORMACIÓN EN LA BASE DE DATOS.';
        }
    } catch (elError) {
        //alert(elError)
    }
}

/*****************************************************/
function detalle(id, nombre, nombreTipoDocumento, codigoTercero, pais, ciudad, direccion, telefono, pagina, nombreSector, nombreResponsable, otrosDatos) {
    globalIdClientes = id;

    document.getElementById('ckHabilitar').checked = false;
    document.getElementById('ckContactos').checked = false;
    document.getElementById('listadoContactos').style.display = 'none';
    document.getElementById('ckArchivos').checked = false;
    document.getElementById('listadoArchivos').style.display = 'none';

    var vacio = '--';

    tabla = "<table class='tbListado centrar' style='text-align: center;'>";
    tabla += "<tr><td class='encabezado' colspan='5'>INFORMACIÓN GENERAL</td></tr>";
    tabla += "<tr><td class='encabezado'>NOMBRE</td><td class='encabezado'>TIPO DOCUMENTO</td><td class='encabezado'>CÓDIGO TERCERO</td><td class='encabezado'>PAIS</td><td class='encabezado'>CIUDAD</td></tr>";

    tabla += '<tr>';
    tabla += '<td class="cuerpoListado10">' + ((nombre == "") ? vacio : nombre) + '</td>';
    tabla += '<td class="cuerpoListado10">' + ((nombreTipoDocumento == "") ? vacio : nombreTipoDocumento) + '</td>';
    tabla += '<td class="cuerpoListado10">' + ((codigoTercero == "") ? vacio : codigoTercero) + '</td>';
    tabla += '<td class="cuerpoListado10">' + ((pais == "") ? vacio : pais) + '</td>';
    tabla += '<td class="cuerpoListado10">' + ((ciudad == "") ? vacio : ciudad) + '</td>';

    tabla += '</tr>';

    tabla += "<tr><td class='encabezado'>DIRECCIÓN</td><td class='encabezado'>TELÉFONO</td><td class='encabezado'>PAGINA</td><td class='encabezado'>SECTOR</td><td class='encabezado'>RESPONSABLE</td></tr>";

    tabla += '<tr>';
    tabla += '<td class="cuerpoListado10">' + ((direccion == "") ? vacio : unescape(direccion)) + '</td>';
    tabla += '<td class="cuerpoListado10">' + ((telefono == "") ? vacio : telefono) + '</td>';
    tabla += '<td class="cuerpoListado10">' + ((pagina == "") ? vacio : pagina) + '</td>';
    tabla += '<td class="cuerpoListado10">' + ((nombreSector == "") ? vacio : nombreSector) + '</td>';
    tabla += '<td class="cuerpoListado10">' + ((nombreResponsable == "") ? vacio : nombreResponsable) + '</td>';
    tabla += '</tr>';

    tabla += "<tr><td class='encabezado' colspan='5'>OTROS DATOS</td></tr>";
    tabla += '<tr>';
    tabla += '<td class="cuerpoListado10" colspan="5">' + ((otrosDatos == "") ? vacio : unescape(otrosDatos)) + '</td>';
    tabla += '</tr>';

    tabla += '</table>';

    document.getElementById('listadoDetalle').innerHTML = tabla;

    listarDetalleContactos();
    listarDetalleArchivos();

    $.fancybox({
        'showCloseButton': true,
        'hideOnOverlayClick': true,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'enableEscapeButton': true,
        'href': '#divDetalle',
        'onClosed': function () {
            // limpiar();
        }
    });
}

function mostrarListado(valor, pestana) {
    switch (pestana) {
        case 1:
            if (valor.checked) {
                document.getElementById('listadoContactos').style.display = 'block';
            } else {
                document.getElementById('listadoContactos').style.display = 'none';
            }
            break;
        case 2:
            if (valor.checked) {
                document.getElementById('listadoArchivos').style.display = 'block';
            } else {
                document.getElementById('listadoArchivos').style.display = 'none';
            }
            break;
        case 3:
            if (valor.checked) {
                document.getElementById('listadoContactos').style.display = 'block';
                document.getElementById('listadoArchivos').style.display = 'block';

                document.getElementById('ckContactos').checked = true;
                document.getElementById('ckArchivos').checked = true;
            } else {
                document.getElementById('listadoContactos').style.display = 'none';
                document.getElementById('listadoArchivos').style.display = 'none';

                document.getElementById('ckContactos').checked = false;
                document.getElementById('ckArchivos').checked = false;
            }
            break;
    }
}

function listarDetalleContactos() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listarDetallePersonas'));
    arrayParameters.push(newArg('idClientes', globalIdClientes));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlClientes.aspx', send, listarDetalleContactos_processResponse);
}

function listarDetalleContactos_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        if (msj != '0') {
            var datosRows = info.data;
            var cols = info.cols;
            var tabla = '';
            //var vacio = '--';
            var id = "", nombre = "", dependencia = "", cargo = "", telefono = "", celularCompleto = "", indicativoCelular = "", codigoCelular = "", celular = "", pais = "", ciudad = "", direccion = "";
            var email = "", email2 = "", responsableSesion = "", otrosDatos = "", estado = "", responsable = "", empresa = "";
            var nombreResponsableSesion = "";

            tabla = "<table class='tbListado centrar' style='text-align: center;'>";
            tabla += "<tr><td class='encabezado' colspan='6'>CONTACTOS EXISTENTES</td></tr>";
            for (var i = 0; i < datosRows.length; i += cols) {
                id = datosRows[i];
                nombre = datosRows[i + 1];
                dependencia = datosRows[i + 2];
                cargo = datosRows[i + 3]
                telefono = datosRows[i + 4];
                celularCompleto = datosRows[i + 5];
                pais = datosRows[i + 6];
                ciudad = datosRows[i + 7];
                direccion = datosRows[i + 8];
                email = datosRows[i + 9];
                email2 = datosRows[i + 10];
                responsableSesion = datosRows[i + 11];
                otrosDatos = datosRows[i + 12];
                estado = datosRows[i + 13];
                responsable = datosRows[i + 14];
                empresa = datosRows[i + 15];
                nombreResponsableSesion = datosRows[i + 16];

                var vacio = '--';

                tabla += "<tr><td class='encabezado' colspan='2'>NOMBRE</td><td class='encabezado'>DEPENDENCIA</td><td class='encabezado'>CARGO</td><td class='encabezado'>TELÉFONO</td><td class='encabezado'>CELULAR</td></tr>";

                tabla += '<tr>';
                tabla += '<td class="cuerpoListado10" colspan="2">' + ((nombre == "") ? vacio : unescape(nombre)) + '</td>';
                tabla += '<td class="cuerpoListado10">' + ((dependencia == "") ? vacio : dependencia) + '</td>';
                tabla += '<td class="cuerpoListado10">' + ((cargo == "") ? vacio : cargo) + '</td>';
                tabla += '<td class="cuerpoListado10">' + ((telefono == "") ? vacio : telefono) + '</td>';
                tabla += '<td class="cuerpoListado10">' + ((celularCompleto == "") ? vacio : celularCompleto) + '</td>';
                tabla += '</tr>';

                tabla += "<tr><td class='encabezado'>PAIS</td><td class='encabezado'>CIUDAD</td><td class='encabezado'>DIRECCIÓN</td><td class='encabezado'>EMAIL</td><td class='encabezado'>EMAIL SECUNDARIO</td><td class='encabezado'>RESPONSABLE</td></tr>";

                tabla += '<tr>';
                tabla += '<td class="cuerpoListado10">' + ((pais == "") ? vacio : pais) + '</td>';
                tabla += '<td class="cuerpoListado10">' + ((ciudad == "") ? vacio : ciudad) + '</td>';
                tabla += '<td class="cuerpoListado10">' + ((direccion == "") ? vacio : unescape(direccion)) + '</td>';
                tabla += '<td class="cuerpoListado10">' + ((email == "") ? vacio : email) + '</td>';
                tabla += '<td class="cuerpoListado10">' + ((email2 == "") ? vacio : email2) + '</td>';
                tabla += '<td class="cuerpoListado10">' + ((nombreResponsableSesion == "") ? vacio : nombreResponsableSesion) + '</td>';
                tabla += '</tr>';

                tabla += "<tr><td class='encabezado' colspan='6'>OTROS DATOS</td></tr>";
                tabla += '<tr>';
                tabla += '<td class="cuerpoListado10" colspan="6">' + ((otrosDatos == "") ? vacio : unescape(otrosDatos)) + '</td>';
                tabla += '</tr>';
                tabla += '<tr><td> <br /> </td></tr>';
            }
            tabla += '</table>';
            document.getElementById('listadoContactos').innerHTML = tabla;
        }
        else {
            document.getElementById('listadoContactos').innerHTML = 'NO SE ENCONTR&Oacute; INFORMACI&Oacute;N EN LA BASE DE DATOS.';
        }
    } catch (elError) {
        //alert(elError)
    }
}

function listarDetalleArchivos() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listarDetalleArchivos'));
    arrayParameters.push(newArg('idClientes', globalIdClientes));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlClientes.aspx', send, listarDetalleArchivos_processResponse);
}

function listarDetalleArchivos_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        if (msj != '0') {
            var datosRows = info.data;
            var tabla = '';
            var vacio = '--';
            var tipo = "", nombreTemporal = "", fecha = "", responsable = "";

            tabla = "<table class='tbListado centrar' style='text-align: center;'>";
            tabla += "<tr><td class='encabezado' colspan='5'>ARCHIVOS EXISTENTES</td></tr>";
            for (var i = 0; i < datosRows.length; i += 4) {
                tipo = datosRows[i];
                nombreTemporal = datosRows[i + 1];
                fecha = datosRows[i + 2];
                responsable = datosRows[i + 3];

                var nombre = nombreTemporal.split('()');

                if (tipo == 1)
                    tipo = "RUT";
                else if (tipo == 2)
                    tipo = "CÁMARA DE COMERCIO";
                else
                    tipo = "OTRO";

                var vacio = '--';

                tabla += "<tr><td class='encabezado'>TIPO</td><td class='encabezado'>NOMBRE</td><td class='encabezado'>SUBIDO POR</td><td class='encabezado'>FECHA</td><td class='encabezado'>DESCARGAR</td></tr>";

                tabla += '<tr>';
                tabla += '<td class="cuerpoListado10">' + ((tipo == "") ? vacio : tipo) + '</td>';
                tabla += '<td class="cuerpoListado10">' + ((nombre[1] == "") ? vacio : nombre[1]) + '</td>';
                tabla += '<td class="cuerpoListado10">' + ((responsable == "") ? vacio : responsable) + '</td>';
                tabla += '<td class="cuerpoListado10">' + ((fecha == "") ? vacio : fecha) + '</td>';
                tabla += '<td class="cuerpoListado10"><div class="linkIconoLateral" onclick="descargarArchivos(\'' + nombreTemporal + '\');"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/listar.png"><p>Descargar</p></div></td>';
                tabla += '</tr>';
                tabla += '<tr><td> <br /> </td></tr>';
            }
            tabla += '</table>';
            document.getElementById('listadoArchivos').innerHTML = tabla;
        }
        else {
            document.getElementById('listadoArchivos').innerHTML = 'NO SE ENCONTR&Oacute; INFORMACI&Oacute;N EN LA BASE DE DATOS.';
        }
    } catch (elError) {
        //alert(elError)
    }
}

function limpiar() {
    globalIdActividadesAbiertas = -1;

    document.getElementById("rbSiActividad").checked = true;
    document.getElementById("rbNoActividad").checked = false;
    document.getElementById("rbSiObjetivo").checked = true;
    document.getElementById("rbNoObjetivo").checked = false;

    document.getElementById('txtFechaEjecucion').value = "";
    //document.getElementById('selHorasInicioEjecucion').value = "-H-";
    //document.getElementById('selMinutosInicioEjecucion').value = "-M-";
    //document.getElementById('selHorasFinEjecucion').value = "-H-";
    //document.getElementById('selMinutosFinEjecucion').value = "-M-";
    document.getElementById('txtObservacionesEjecutar').value = "";

    document.getElementById('listadoDetalle').innerHTML = "";
}

///////////////////////////////////****************************************/////////////////////////////////////////////

function fechaSistemaAyer() {
    var f = new Date();
    var meses = new Array("01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12");
    var fechaActual;
    var dia = "";
    var fechaAyer = new Date(f.getTime() - 24 * 60 * 60 * 1000);
    dia = dia + fechaAyer.getDate(); // se agrega esto para calcular la fecha de mañana.
    var tam = dia.length;
    //fechaActual = f.getDate() + "/" + meses[f.getMonth()] + "/" + f.getFullYear();
    fechaActual = ((tam == 1) ? "0" + fechaAyer.getDate() : fechaAyer.getDate()) + "/" + meses[fechaAyer.getMonth()] + "/" + fechaAyer.getFullYear(); //descomentar si el dia de la fecha sale con un solo digito
    return fechaActual;
}


//function listarActividadesNoEjecutadas(pag) {


//    var responsable = document.getElementById('selResponsable').value;
//    var fechaHoy = fechaSistemaAyer();                  //  se usan estas fechas para realizar el filtro del las activiade abierta para que muestre unicamente
//    var dato = new Date();
//    var horaActual = dato.getHours();
//    horaActual = horaActual + '' + dato.getMinutes();  // se usa esta hora actual para filtrar de aqui para abajo las no ejecutadas.

//    var arrayParameters = new Array();
//    arrayParameters.push(newArg('p', 'listarActividadesNoEjecutadasPresentacion'));
//    arrayParameters.push(newArg('responsableSesion', responsable));
//    arrayParameters.push(newArg('fechaHoy', fechaHoy));
//    arrayParameters.push(newArg('horaActual', horaActual));
//    arrayParameters.push(newArg('pag', pag));
//    var send = arrayParameters.join('&');
//    $.post('../../Controlador/ctlPaginador.aspx', send, listarActividadesNoEjecutadas_processResponse);
//}

//function listarActividadesNoEjecutadas_processResponse(res) {

//    try {
//        var info = eval('(' + res + ')');
//        var divTerceros = document.getElementById('listadoNoEjecutadas');
//        if (res != '0') {
//            ocultaVentanaProgreso();
//            document.getElementById('listadoNoEjecutadas').style.display = 'block';
//            var datosRows = info.data;
//            var cols = info.cols;
//            var ctl = true, claseAplicar = "", claseAplicar1 = "";
//            var id = "", tipoActividad = "", nombreTipoActividad = "", fecha = "", horaInicio = "", horaFin = "", idCliente = "", nombreCliente = "", idOportunidad = "", nombreOportunidad = "";
//            var idContacto = "", nombreContacto = "", objetivo = "", lugar = "", responsableSesion = "", nombreResponsable = "", observaciones = "", mostrarHoraFin = "";
//            var id1 = "", nombre = "", tipoDocumento = "", nombreTipoDocumento = "", codigoTercero = "", pais = "", ciudad = "", direccion = "", telefono = "", pagina = "", sector = "", nombreSector = "";
//            var responsableSesion = "", nombreResponsable = "", otrosDatos = "", estado = "";

//            tabla = "<table class='tbListado centrar' style='text-align:center;'><tr><td class='encabezado' colspan='4'>ACTIVIDADES VENCIDAS EXISTENTES</td></tr>";
//            tabla += "<tr><td style='width:10%' align='center' class='encabezado'></td><td style='width:28%' align='center' class='encabezado'>ACTIVIDAD</td><td class='encabezado'>CLIENTE</td><td class='encabezado' style='width:11%'>FECHA</td></tr>";
//            //tabla += "<td class='encabezado' style='width:9%'>DETALLE</td><td class='encabezado' style='width:10%'>EJECUTAR</td><td class='encabezado' style='width:8%'>EDITAR</td><td class='encabezado' style='width:9%'>ELIMINAR</td></tr>";

//            for (var i = 0; i < datosRows.length; i += cols) {
//                id = datosRows[i];
//                tipoActividad = datosRows[i + 1];
//                nombreTipoActividad = datosRows[i + 2];
//                fecha = datosRows[i + 3];
//                horaInicio = datosRows[i + 4];
//                horaFin = datosRows[i + 5];
//                idCliente = datosRows[i + 6];
//                nombreCliente = datosRows[i + 7];
//                idOportunidad = datosRows[i + 8];
//                nombreOportunidad = datosRows[i + 9];
//                idContacto = datosRows[i + 10];
//                nombreContacto = datosRows[i + 11];
//                objetivo = datosRows[i + 12];
//                lugar = datosRows[i + 13];
//                responsableSesion = datosRows[i + 14];
//                nombreResponsable = datosRows[i + 15];
//                observaciones = datosRows[i + 16];
//                id1 = datosRows[i + 17];
//                nombre = datosRows[i + 18];
//                tipoDocumento = datosRows[i + 19];
//                nombreTipoDocumento = datosRows[i + 20];
//                codigoTercero = datosRows[i + 21];
//                pais = datosRows[i + 22];
//                ciudad = datosRows[i + 23];
//                direccion = datosRows[i + 24];
//                telefono = datosRows[i + 25];
//                pagina = datosRows[i + 26];
//                sector = datosRows[i + 27];
//                nombreSector = datosRows[i + 28];
//                responsableSesion = datosRows[i + 29];
//                nombreResponsable = datosRows[i + 30];
//                otrosDatos = datosRows[i + 31];
//                estado = datosRows[i + 32];

//                if (ctl) {
//                    claseAplicar = "cuerpoListado7";
//                    claseAplicar1 = "cuerpoListado1";
//                }
//                else {
//                    claseAplicar = "cuerpoListado8";
//                    claseAplicar1 = "cuerpoListado2";
//                }
//                ctl = !ctl;

//                //                if (horaFin == "")
//                //                    mostrarHoraFin = "";
//                //                else
//                //                    mostrarHoraFin = " - " + horaFin;

//                //                var icono = nombreTipoActividad.split("-");
//                var icono = nombreTipoActividad.split("-");
//                var partirHoraInicio = horaInicio.split(":");
//                horasInicio = partirHoraInicio[0];
//                minutosInicio = partirHoraInicio[1];
//                var partirHoraFin = horaFin.split(":");
//                horasFin = partirHoraFin[0];
//                minutosFin = partirHoraFin[1];

//                if (horasFin == "-H-" || minutosFin == "-M-") {
//                    mostrarHoraFin = "";
//                    detalleHoraFin = "";
//                }
//                else {
//                    mostrarHoraFin = " - " + horaFin;
//                    detalleHoraFin = horaFin;
//                }

//                tabla += '<td align="center" class="' + claseAplicar + '"><img height="16px" width="16px" src=' + icono[0] + '></td><td class="' + claseAplicar + '"> <div id="imgEditar" class="linkIconoLateral botonEditar " onclick="ejecutar(\'' + id + '\', \'' + icono[1] + '\', \'' + fecha + '\', \'' + horasInicio + '\', \'' + minutosInicio + '\', \'' + horasFin + '\', \'' + minutosFin + '\', \'' + mostrarHoraFin + '\', \'' + objetivo + '\', \'' + idCliente + '\', \'' + nombre + '\', \'' + nombreContacto + '\', \'' + nombreOportunidad + '\', \'' + nombreResponsable + '\', \'' + 2 + '\');" ><p>' + icono[1] + '</p></div></td>';
//                tabla += '<td align="center" class="' + claseAplicar + '"><div class="linkIconoLateralInicio botonEditar" onclick="detalle(\'' + id1 + '\', \'' + nombre + '\', \'' + nombreTipoDocumento + '\', \'' + codigoTercero + '\', \'' + pais + '\', \'' + ciudad + '\', \'' + direccion + '\', \'' + telefono + '\', \'' + pagina + '\', \'' + nombreSector + '\', \'' + nombreResponsable + '\', \'' + otrosDatos + '\');" ><p>' + nombreCliente + " " + nombreContacto + '</p></div></td>';
//                tabla += '<td align="center" class="' + claseAplicar + '">' + fecha + " " + horaInicio + mostrarHoraFin + '</td>';
//                tabla += '</td></tr>';
//            }
//            tabla += '</table>'

//            divTerceros.innerHTML = tabla;
//            divTerceros.innerHTML += pieDePaginaListar(info, 'listarActividadesNoEjecutadas');
//            var idMenuForm = document.getElementById('idMenuForm').innerHTML; // se adiciona esta linea para que se de  el permiso de visualizar el editar despues de cargar
//            //permisosParaMenu(idMenuForm); // se adiciona esta linea para que se el permiso de visualizar el editar despues de cargar
//        } else {
//            divTerceros.innerHTML = 'NO HAY INFORMACIÓN EN LA BASE DE DATOS';
//        }
//    } catch (elError) {
//    }

//}

function DeMilitar_AmPm(horaMilitar) {
    if (parseInt(horaMilitar.substring(0, 2)) > 12) {
        var hora = (parseInt(horaMilitar.substring(0, 2)) - 12);
        if (hora < 10) {
            horaMilitar = '0' + (parseInt(horaMilitar.substring(0, 2)) - 12) + horaMilitar.substring(2, 5) + 'PM';
        }
        else {
            horaMilitar = (parseInt(horaMilitar.substring(0, 2)) - 12) + horaMilitar.substring(2, 5) + 'PM';
        }
    }
    else if (parseInt(horaMilitar.substring(0, 2)) == 12) {
        horaMilitar = horaMilitar + 'PM';
    }
    else {
        if (horaMilitar.substring(0, 2) == '00') {
            horaMilitar = '12' + horaMilitar.substring(2, 5) + 'AM';
        }
        else {
            horaMilitar = horaMilitar + 'AM';
        }
    }
    if (horaMilitar == 'AM' || horaMilitar == 'PM') { horaMilitar = ''; }
    return horaMilitar;
}



function formato_numero(numero, decimales, separador_decimal, separador_miles) { // v2007-08-06
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
