/////////////////////////////////////////////////////////////
//////////////// VARIABLES GLOBALES /////////////////////////
/////////////////////////////////////////////////////////////
var datos = new Array();
var datosa = new Array();
var banderaGlobal = 0;

var seguiGlobal = 0;
var seguiGlobalTotal = 0;
var contadorMasterGlobal = '-1';
$(document).ready(function () {

    permisosParaMenu("I.1");
    //contarPacientesNuevosSP();
    //contarPacientesNuevosMasivosSP();
    //contarPacientesActivosSP();
    //contarPacientesCierreSP();

    //conteoIndicadoresIniciales();
    //PacientesMasivoXsemana_listar();
    //PacientesIndividualXsemana_listar();
    //SeguimientoPeriodicoXsemana_listar();
    //PacientesSinCobertura();
    //PacientesFallecidos();
    //listarPacientesXTrimestre();
    //TramitesGestionadosXtrimestre_listar();
    //PacientesActivosSinTramXedad_listar();
    //PacientesActivosConTramXedad_listar();
    //listarPacientesCerradosXTrimestre();

   // PacientesActivosXedad_listar();
    // PacientesCerradosXedad_listar();

    PermisosMenuInicio_validar();
    document.getElementById("lblverseguiper").innerHTML = "Ver Resumen";
    document.getElementById("divseguiperdetallado").style.display="none";
    mostrarotroresultado();
    verseguiper();
  //  mostrarotroresultado();
});

function PermisosMenuInicio_validar() {

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'PermisosMenuInicio_validar'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, PermisosMenuInicio_validar_processResponse);
    muestraVentanaProgreso('Cargando ...');
}

function PermisosMenuInicio_validar_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        //var msj = info.msj;
        switch (info) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                //  muestraVentana('NO EXISTEN TIPOS DE DOCUMENTOS');
                break;
            case 1:
                contactoInicialFinal();
                listasInicialFinal();
                conteoIndicadoresIniciales();
                PacientesMasivoXsemana_listar();
                PacientesIndividualXsemana_listar();
                SeguimientoPeriodicoXsemana_listar();
                SeguimientoPeriodicoXsemanaNo_listar();
                PacientesSinCobertura();
                PacientesFallecidos();
                listarPacientesXTrimestre();
                TramitesGestionadosXtrimestre_listar();
                PacientesActivosSinTramXedad_listar();
                PacientesActivosConTramXedad_listar();
                listarPacientesCerradosXTrimestre();
                listarPacientesXEdad();
               
                document.getElementById("imgIndicador").style.display = 'block';
                break;
            case 2:
                ocultaVentanaProgreso();
                document.getElementById("imgIndicador").style.display = 'none';
                break;
        }
    } catch (elError) { }
}

function conteoIndicadoresIniciales() {

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'conteoIndicadoresIniciales'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, conteoIndicadoresIniciales_processResponse);
    muestraVentanaProgreso('Cargando ...');
}

function conteoIndicadoresIniciales_processResponse(res) {
    try {
        controlProcces();
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
                document.getElementById("lblpacientesMasivos").innerHTML = info.data[0];
                document.getElementById("lblpacientesIndividuales").innerHTML = info.data[1];
                document.getElementById("lblpacientesSegPerProgramados").innerHTML = info.data[2];
                document.getElementById("lblpacientesSegPerRealizados").innerHTML = info.data[3];
                document.getElementById("lblpacientesCargados").innerHTML = info.data[4];
                document.getElementById("lblpacientesConTramites").innerHTML = info.data[5];
                document.getElementById("lblpacientesSinTramites").innerHTML = info.data[6];
                document.getElementById("lblpacientesActivos").innerHTML = info.data[7];
                document.getElementById("lblpacientesSinCobertura").innerHTML = info.data[8];
                document.getElementById("lblpacientesFallecidos").innerHTML = info.data[9];
                document.getElementById("lblpacientesCierreInicial").innerHTML = info.data[10];
                document.getElementById("lblpacientesActivosInicial").innerHTML = info.data[11];
                document.getElementById("lblpacientesNoSegPerRealizados").innerHTML = info.data[12];

                
               // document.getElementById("lblpacientesSegPerRealizadosTotal").innerHTML = parseInt(info.data[12]) + parseInt(info.data[3]);
               document.getElementById("lblpacientesSegPerRealizadosTotal").innerHTML = info.data[3];
               seguiGlobal =info.data[3];
               seguiGlobalTotal = parseInt(info.data[12]) + parseInt(info.data[3]);

                
                                              
                break;
        }
    } catch (elError) { }
}

function PacientesMasivoXsemana_listar() {


    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'PacientesMasivoXsemana_listar'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, PacientesMasivoXsemana_listar_processResponse);
    muestraVentanaProgreso('Cargando ...');
}

function PacientesMasivoXsemana_listar_processResponse(res) {
    try {
        controlProcces();
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('divlistarPacMasivos');
        if (res != '0') {
            var datosRows = info.data;
            var l = info.cols;
            var f = datosRows.length - l;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";
            var r = 0;
            var contador = 1;
            var tipo = 5;
            var f = new Date();
            var mes = f.getMonth() + 1;
            var anio = f.getFullYear();
         
            var tabla = "<table class='tbListado centrar' style='text-align: center;'>";
            tabla += "<tr><td class='encabezado'>SEMANA</td><td class='encabezado'>CANT.</td>";

            for (var i = 0; i < datosRows.length; i += l) {

                var semana = datosRows[i];
                var cantidad = datosRows[i + 1];

                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar2 = "cuerpoListado3";
                } else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar2 = "cuerpoListado5";
                }
                ctl = !ctl;

                tabla += '<tr onclick="iralcargue(\'' + contador + '\',\'' + tipo + '\',\'' + mes + '\',\'' + anio + '\')" style="cursor:pointer;"><td class="' + claseAplicar2 + '" align="center"><b>' + semana + '</b></td>';
                tabla += '<td class="' + claseAplicar2 + '" align="center">' + cantidad + ' </td></tr>';
                contador++;
            }

            tabla += '</table>'
            divTerceros.innerHTML = tabla;
        } else {
            divTerceros.innerHTML = mensajecero;
        }
    } catch (elError) {
    }
}

function PacientesIndividualXsemana_listar() {

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'PacientesIndividualXsemana_listar'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, PacientesIndividualXsemana_listar_processResponse);
    muestraVentanaProgreso('Cargando ...');
}

function PacientesIndividualXsemana_listar_processResponse(res) {
    try {
        controlProcces();
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('divlistarPacIndividial');
        if (res != '0') {
            var datosRows = info.data;
            var l = info.cols;
            var f = datosRows.length - l;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";
            var r = 0;
            var contador = 1;
            var tipo = 4;
            var f = new Date();
            var mes = f.getMonth() + 1;
            var anio = f.getFullYear();
            var tabla = "<table class='tbListado centrar' style='text-align: center;'>";
            tabla += "<tr><td class='encabezado'>SEMANA</td><td class='encabezado'>CANT.</td>";

            for (var i = 0; i < datosRows.length; i += l) {

                var semana = datosRows[i];
                var cantidad = datosRows[i + 1];

                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar2 = "cuerpoListado3";
                } else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar2 = "cuerpoListado5";
                }
                ctl = !ctl;

                tabla += '<tr onclick="iralcargue(\'' + contador + '\',\'' + tipo + '\',\'' + mes + '\',\'' + anio + '\')" style="cursor:pointer;"><td class="' + claseAplicar2 + '" align="center" style="width=60px;"><b>' + semana + '</b></td>';
                tabla += '<td class="' + claseAplicar2 + '" align="center">' + cantidad + ' </td></tr>';
                contador++;
            }

            tabla += '</table>'
            divTerceros.innerHTML = tabla;
        } else {
            divTerceros.innerHTML = mensajecero;
        }
    } catch (elError) {
    }
}


function SeguimientoPeriodicoXsemana_listar() {

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'SeguimientoPeriodicoXsemana_listar'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, SeguimientoPeriodicoXsemana_listar_processResponse);
    muestraVentanaProgreso('Cargando ...');
}

function SeguimientoPeriodicoXsemana_listar_processResponse(res) {
    try {
        controlProcces();
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('divlistarSegPerRealizados');
        var divTerceros2 = document.getElementById('divlistarSegPerRealizados2');
        if (res != '0') {
            var datosRows = info.data;
            var l = info.cols;
            var f = datosRows.length - l;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";
            var r = 0;
            var contador=1;
            var tabla = "<table class='tbListado centrar' style='text-align: center;'>";
            tabla += "<tr><td class='encabezado'>SEMANA</td><td class='encabezado'>CANT.</td>";

            for (var i = 0; i < datosRows.length; i += l) {

                var semana = datosRows[i];
                var cantidad = datosRows[i + 1];

                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar2 = "cuerpoListado3";
                } else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar2 = "cuerpoListado5";
                }
                ctl = !ctl;

                tabla += '<tr onclick="iralseguimiento(\'' + contador + '\')" style="cursor:pointer;"><td class="' + claseAplicar2 + '" align="center" style="width=60px;"><b>' + semana + '</b></td>';
                tabla += '<td class="' + claseAplicar2 + '" align="center">' + cantidad + ' </td></tr>';
                contador++;
            }

            tabla += '</table>'
            divTerceros.innerHTML = tabla;
            divTerceros2.innerHTML = tabla;
        } else {
            divTerceros.innerHTML = mensajecero;
        }
    } catch (elError) {
    }
}




function listarPacientesXTrimestre() {

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'PacientesXTrimestre_listar'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, listarPacientesXTrimestre_processResponse);
    muestraVentanaProgreso('Cargando ...');
}

function listarPacientesXTrimestre_processResponse(res) {
    try {
        controlProcces();
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('divGraficaCargados');
        if (res != '0') {
            var datosRows = info.data;
            var l = info.cols;
            var f = datosRows.length - l;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";
            var a = 0;



            for (var i = 0; i < datosRows.length; i += l) {

                var mes = datosRows[i];
                var mes_nombre = datosRows[i + 1];
                var cantidad_pacientes = datosRows[i + 2];

                datosa.push(mes_nombre);
                datosa.push(cantidad_pacientes);
                datos.push([datosa[a], parseInt(datosa[a + 1])]);
                a = a + 2;

            }

            drawChartPacientes();
        } else {
            divTerceros.innerHTML = mensajecero;
        }
    } catch (elError) {
    }
}


function drawChartPacientes() {
    // Create the data table.
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Mes');
    data.addColumn('number', 'Cantidad');
    data.addRows(datos);

    // Set chart options
    var options = {
        'title': 'Pacientes cargados último trimestre', 
        'vAxis': { title: 'Cantidad de Pacientes', titleTextStyle: { color: 'black', bold: true } },
        legend: { position: 'bottom' },
        'width': 227,
        'height': 237,        
        'hAxis': { direction: -1, slantedText: true, slantedTextAngle: 45 },
        seriesType: 'bars',
        bar: { groupWidth: "35%" }
    };

    var chart;
    chart = new google.visualization.ColumnChart(document.getElementById('divGraficaCargados'));
    chart.draw(data, options);
    datosa.length = 0;
    datos.length = 0;
}

function PacientesActivosConTramXedad_listar() {

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'PacientesActivosConTramXedad_listar'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, PacientesActivosConTramXedad_listar_processResponse);
    muestraVentanaProgreso('Cargando ...');
}

function PacientesActivosConTramXedad_listar_processResponse(res) {
    try {
     //   controlProcces();
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('divlistarActivosConTramites');
        if (res != '0') {
            var datosRows = info.data;
            var l = info.cols;
            var f = datosRows.length - l;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";
            var r = 0;

            var tabla = "<table class='tbListado centrar' style='text-align: center;'>";
            tabla += "<tr><td class='encabezado'>TIPO</td><td class='encabezado'>CANT.</td>";

            for (var i = 0; i < datosRows.length; i += l) {

                var tipo = datosRows[i];
                var cantidad = datosRows[i + 1];

                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar2 = "cuerpoListado3";
                } else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar2 = "cuerpoListado5";
                }
                ctl = !ctl;

                tabla += '<tr><td class="' + claseAplicar2 + '" align="center" style="width=60px;"><b>' + tipo + '</b></td>';
                tabla += '<td class="' + claseAplicar2 + '" align="center">' + cantidad + ' </td></tr>';

            }

            tabla += '</table>'
            divTerceros.innerHTML = tabla;
        } else {
            divTerceros.innerHTML = mensajecero;
        }
    } catch (elError) {
    }
}

function PacientesActivosSinTramXedad_listar() {

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'PacientesActivosSinTramXedad_listar'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, PacientesActivosSinTramXedad_listar_processResponse);
    muestraVentanaProgreso('Cargando ...');
}

function PacientesActivosSinTramXedad_listar_processResponse(res) {
    try {
      //  controlProcces();
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('divlistarActivosSinTramites');
        if (res != '0') {
            var datosRows = info.data;
            var l = info.cols;
            var f = datosRows.length - l;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";
            var r = 0;

            var tabla = "<table class='tbListado centrar' style='text-align: center;'>";
            tabla += "<tr><td class='encabezado'>TIPO</td><td class='encabezado'>CANT.</td>";

            for (var i = 0; i < datosRows.length; i += l) {

                var tipo = datosRows[i];
                var cantidad = datosRows[i + 1];

                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar2 = "cuerpoListado3";
                } else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar2 = "cuerpoListado5";
                }
                ctl = !ctl;

                tabla += '<tr><td class="' + claseAplicar2 + '" align="center" style="width=60px;"><b>' + tipo + '</b></td>';
                tabla += '<td class="' + claseAplicar2 + '" align="center">' + cantidad + ' </td></tr>';

            }

            tabla += '</table>'
            divTerceros.innerHTML = tabla;
        } else {
            divTerceros.innerHTML = mensajecero;
        }
    } catch (elError) {
    }
}

function TramitesGestionadosXtrimestre_listar() {


    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'TramitesGestionadosXtrimestre_listar'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, TramitesGestionadosXtrimestre_listar_processResponse);
    muestraVentanaProgreso('Cargando ...');
}

function TramitesGestionadosXtrimestre_listar_processResponse(res) {
    try {
    //    controlProcces();
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('divGraficaGestionados');
        if (res != '0') {
            var datosRows = info.data;
            var l = info.cols;
            var f = datosRows.length - l;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";
            var r = 0;
            datosa.length = 0;
            datos.length = 0;
           

            for (var i = 0; i < datosRows.length; i += l) {

                var tipo = datosRows[i];
                var cantidad_con_tram = datosRows[i + 1];

                datosa.push(tipo);
                datosa.push(cantidad_con_tram);
                datos.push([datosa[r], parseInt(datosa[r + 1])]);
                r = r + 2;

            }

            drawChartTramites();
        } else {
            divTerceros.innerHTML = mensajecero;
        }
    } catch (elError) {
    }
}

function drawChartTramites() {

    var data = new google.visualization.DataTable();
    data.addColumn('string', 'TIPO');
    data.addColumn('number', 'Cantidad de Pacientes');
    data.addRows(datos);

    var options = {
        title: 'Cantidad de Pacientes Activos por Edad',
         hAxis: { title: 'TIPO', titleTextStyle: { color: 'black', bold: true } },
        pieHole: 0.52,
      'width': 227,
        'height': 237,     
        legend: { position: 'none' },
    };

    var chart;
    chart = new google.visualization.PieChart(document.getElementById('divGraficaGestionados'));
    chart.draw(data, options);
    datosa.length = 0;
    datos.length = 0;
}
//function contarPacientesNuevosSP() {


//    // var id = iden;
//    var arrayParameters = new Array();
//    arrayParameters.push(newArg('p', 'contarPacientesNuevosCompletosSP'));

//    var send = arrayParameters.join('&');
//    $.post('../../Controlador/ctlGeneral.aspx', send, contarPacientesNuevosSP_processResponse);
//}

//function contarPacientesNuevosSP_processResponse(res) {
//    try {
//        var info = eval('(' + res + ')');
//        var msj = info.msj;
//        switch (msj) {
//            case -1:
//                muestraVentana('SIN CONEXIÓN A LA BASE DE DATOS.');
//                break;
//            case 0:
//                //  muestraVentana('NO EXISTEN TIPOS DE DOCUMENTOS');
//                break;
//            case 1:
//                document.getElementById("lblpacientesIngresadosInicial").innerHTML = info.data[0];
//                //  document.getElementById("lblpacientesCreadosCP").innerHTML = info.data[0];

//                break;
//        }
//    } catch (elError) { }
//}


//function contarPacientesNuevosMasivosSP() {


//    // var id = iden;
//    var arrayParameters = new Array();
//    arrayParameters.push(newArg('p', 'contarPacientesNuevosMasivosSP'));

//    var send = arrayParameters.join('&');
//    $.post('../../Controlador/ctlGeneral.aspx', send, contarPacientesNuevosMasivosSP_processResponse);
//}

//function contarPacientesNuevosMasivosSP_processResponse(res) {
//    try {
//        var info = eval('(' + res + ')');
//        var msj = info.msj;
//        switch (msj) {
//            case -1:
//                muestraVentana('SIN CONEXIÓN A LA BASE DE DATOS.');
//                break;
//            case 0:
//                //  muestraVentana('NO EXISTEN TIPOS DE DOCUMENTOS');
//                break;
//            case 1:
//                document.getElementById("lblpacientesIngresadosMasivosInicial").innerHTML = info.data[0];
//                //  document.getElementById("lblpacientesCreadosCP").innerHTML = info.data[0];

//                break;
//        }
//    } catch (elError) { }
//}


//function contarPacientesActivosSP() {


//    // var id = iden;
//    var arrayParameters = new Array();
//    arrayParameters.push(newArg('p', 'contarPacientesActivosSP'));

//    var send = arrayParameters.join('&');
//    $.post('../../Controlador/ctlGeneral.aspx', send, contarPacientesActivosSP_processResponse);
//}

//function contarPacientesActivosSP_processResponse(res) {
//    try {
//        var info = eval('(' + res + ')');
//        var msj = info.msj;
//        switch (msj) {
//            case -1:
//                muestraVentana('SIN CONEXIÓN A LA BASE DE DATOS.');
//                break;
//            case 0:
//                //  muestraVentana('NO EXISTEN TIPOS DE DOCUMENTOS');
//                break;
//            case 1:
//                var total = info.data[0];
//                var resultado = '';
//                for (var j, i = total.length - 1, j = 0; i >= 0; i--, j++) {

//                    resultado = total.charAt(i) + ((j > 0) && (j % 3 == 0) ? "." : "") + resultado;
//                }
//                document.getElementById("lblpacientesActivosInicial").innerHTML = resultado;
//                //  document.getElementById("lblpacientesCreadosCP").innerHTML = info.data[0];




//                break;
//        }
//    } catch (elError) { }
//}


//function contarPacientesCierreSP() {


//    // var id = iden;
//    var arrayParameters = new Array();
//    arrayParameters.push(newArg('p', 'contarPacientesCierreSP'));

//    var send = arrayParameters.join('&');
//    $.post('../../Controlador/ctlGeneral.aspx', send, contarPacientesCierreSP_processResponse);
//}

//function contarPacientesCierreSP_processResponse(res) {
//    try {
//        var info = eval('(' + res + ')');
//        var msj = info.msj;
//        switch (msj) {
//            case -1:
//                muestraVentana('SIN CONEXIÓN A LA BASE DE DATOS.');
//                break;
//            case 0:
//                //  muestraVentana('NO EXISTEN TIPOS DE DOCUMENTOS');
//                break;
//            case 1:
//                document.getElementById("lblpacientesCierreInicial").innerHTML = info.data[0];
//                //  document.getElementById("lblpacientesCreadosCP").innerHTML = info.data[0];

//                break;
//        }
//    } catch (elError) { }
//}

function direccionarcargue() {
    location.href = "../cargue/PacientesCargados.aspx";
}

function direccionarActivos() {
   // location.href = "../estados/Activos.aspx";
}

function direccionarBuscarPaciente() {
 //   location.href = "../estados/buscarPaciente.aspx";
}

function direccionarCerradosSinCobertura() {
    location.href = "../estados/sinCobertura.aspx";
}

function direccionarCerradosFallecidos() {
    location.href = "../estados/fallecidos.aspx";
}

function direccionarCerrados() {
  //  location.href = "../estados/fallecidos.aspx";
}

function PacientesSinCobertura() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'PacientesSinCobertura_indicador_inicio'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, PacientesSinCobertura_processResponse);
    muestraVentanaProgreso('Cargando ...');
}

function PacientesSinCobertura_processResponse(res) {
    try {
        controlProcces();
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('divlistarSinCobertura');
        if (res != '0') {
            var datosRows = info.data;
            var l = info.cols;
            var f = datosRows.length - l;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";
            var r = 0;

            var tabla = "<table class='tbListado centrar' style='text-align: center;'>";
            tabla += "<tr><td class='encabezado'>TIPO</td><td class='encabezado'>CANT.</td>";

            for (var i = 0; i < datosRows.length; i += l) {

                var tipo = datosRows[i];
                var cantidad = datosRows[i + 1];

                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar2 = "cuerpoListado3";
                } else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar2 = "cuerpoListado5";
                }
                ctl = !ctl;

                tabla += '<tr><td class="' + claseAplicar2 + '" align="center"><b>' + tipo + '</b></td>';
                tabla += '<td class="' + claseAplicar2 + '" align="center">' + cantidad + ' </td></tr>';

            }

            tabla += '</table>'
            divTerceros.innerHTML = tabla;
        } else {
            divTerceros.innerHTML = mensajecero;
        }
    } catch (elError) {
    }
}


function PacientesFallecidos() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'PacientesFallecidos_indicador_inicio'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, PacientesFallecidos_processResponse);
    muestraVentanaProgreso('Cargando ...');
}

function PacientesFallecidos_processResponse(res) {
    try {
        controlProcces();
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('divlistarFallecidos');
        if (res != '0') {
            var datosRows = info.data;
            var l = info.cols;
            var f = datosRows.length - l;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";
            var r = 0;

            var tabla = "<table class='tbListado centrar' style='text-align: center;'>";
            tabla += "<tr><td class='encabezado'>TIPO</td><td class='encabezado'>CANT.</td>";

            for (var i = 0; i < datosRows.length; i += l) {

                var tipo = datosRows[i];
                var cantidad = datosRows[i + 1];

                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar2 = "cuerpoListado3";
                } else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar2 = "cuerpoListado5";
                }
                ctl = !ctl;

                tabla += '<tr><td class="' + claseAplicar2 + '" align="center"><b>' + tipo + '</b></td>';
                tabla += '<td class="' + claseAplicar2 + '" align="center">' + cantidad + ' </td></tr>';

            }

            tabla += '</table>'
            divTerceros.innerHTML = tabla;
        } else {
            divTerceros.innerHTML = mensajecero;
        }
    } catch (elError) {
    }
}



function listarPacientesCerradosXTrimestre() {

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'PacientesCerradosXTrimestre_listar'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, listarPacientesCerradosXTrimestre_processResponse);
    muestraVentanaProgreso('Cargando ...');
}

function listarPacientesCerradosXTrimestre_processResponse(res) {
    try {
        controlProcces();
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('divGraficaCerrados');
        if (res != '0') {
            var datosRows = info.data;
            var l = info.cols;
            var f = datosRows.length - l;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";
            var r = 0;
            datosa.length = 0;
            datos.length = 0;

            for (var i = 0; i < datosRows.length; i += l) {

                var tipo = datosRows[i];
                var cantidad_con_tram = datosRows[i + 1];

                datosa.push(tipo);
                datosa.push(cantidad_con_tram);
                datos.push([datosa[r], parseInt(datosa[r + 1])]);
                r = r + 2;
                

            }

            drawChartPacientesCerrados();
        } else {
            divTerceros.innerHTML = mensajecero;
        }
    } catch (elError) {
    }
}
 
function drawChartPacientesCerrados() {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'TIPO');
    data.addColumn('number', 'Cantidad de Pacientes');
    data.addRows(datos);

    var options = {
        title: 'Cantidad de Pacientes Cerrados por Edad',
        hAxis: { title: 'TIPO', titleTextStyle: { color: 'black', bold: true } },
        pieHole: 0.52,
        'width': 227,
        'height': 237,     
        legend: { position: 'none' },
    };

    var chart;
    chart = new google.visualization.PieChart(document.getElementById('divGraficaCerrados'));
    chart.draw(data, options);
    datosa.length = 0;
    datos.length = 0;
}


function controlProcces(){

    banderaGlobal++;
    if ((banderaGlobal == 11)) {       
        ocultaVentanaProgreso();
    }
}

//function PacientesActivosXedad_listar() {


//    var arrayParameters = new Array();
//    arrayParameters.push(newArg('p', 'PacientesActivosXedad_listar'));
//    var send = arrayParameters.join('&');
//    $.post('../../Controlador/ctlGeneral.aspx', send, PacientesActivosXedad_listar_processResponse);
//    muestraVentanaProgreso('Cargando ...');
//}

//function PacientesActivosXedad_listar_processResponse(res) {
//    try {
//        controlProcces();
//        var info = eval('(' + res + ')');
//        var divTerceros = document.getElementById('divGraficaGestionados');
//        if (res != '0') {
//            var datosRows = info.data;
//            var l = info.cols;
//            var f = datosRows.length - l;
//            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";
//            var r = 0;
//            datosa.length = 0;
//            datos.length = 0;


//            for (var i = 0; i < datosRows.length; i += l) {

//                var tipo = datosRows[i];
//                var cantidad_con_tram = datosRows[i + 1];
//                var cantidad_sin_tram = datosRows[i + 2];

//                datosa.push(tipo);
//                datosa.push(cantidad_con_tram);
//                datosa.push(cantidad_sin_tram);
//                datos.push([datosa[r], parseInt(datosa[r + 1]), parseInt(datosa[r + 2])]);
//                r = r + 3;

//            }

//            drawChart();
//        } else {
//            divTerceros.innerHTML = mensajecero;
//        }
//    } catch (elError) {
//    }
//}

//function drawChart() {

//    // Create the data table.
//    var data = new google.visualization.DataTable();
//    data.addColumn('string', 'TIPO');
//    data.addColumn('number', 'CON TRÁMITES');
//    data.addColumn('number', 'SIN TRÁMITES');
//    data.addRows(datos);

//    // Set chart options
//    var options = {
//        title: 'Cantidad de pacientes activos por edad',
//        hAxis: { title: 'TIPO', titleTextStyle: { color: 'black', bold: true } },
//        'vAxis': { title: 'Cantidad de Pacientes', titleTextStyle: { color: 'black', bold: true } },
//        'width': 250,
//        'height': 260,
//        seriesType: 'bars',
//        'hAxis': { direction: -1, slantedText: true, slantedTextAngle: 40 },
//        bar: { groupWidth: "30%" },
//        legend: { position: 'top', textStyle: { fontSize: 7 } },
//    };

//    var chart;

//    chart = new google.visualization.ColumnChart(document.getElementById('divGraficaGestionados'));
//    chart.draw(data, options);

//    datosa.length = 0;
//    datos.length = 0;

//}


//function PacientesCerradosXedad_listar() {

//    var arrayParameters = new Array();
//    arrayParameters.push(newArg('p', 'PacientesCerradosXedad_listar'));
//    var send = arrayParameters.join('&');
//    $.post('../../Controlador/ctlGeneral.aspx', send, PacientesCerradosXedad_listar_processResponse);
//    muestraVentanaProgreso('Cargando ...');
//}

//function PacientesCerradosXedad_listar_processResponse(res) {
//    try {
//        controlProcces();
//        var info = eval('(' + res + ')');
//        var divTerceros = document.getElementById('divGraficaCerrados');
//        if (res != '0') {
//            var datosRows = info.data;
//            var l = info.cols;
//            var f = datosRows.length - l;
//            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";
//            var r = 0;
//            datosa.length = 0;
//            datos.length = 0;

//            for (var i = 0; i < datosRows.length; i += l) {


//                var tipo = datosRows[i];
//                var cantidad_con_tram = datosRows[i + 1];
//                var cantidad_sin_tram = datosRows[i + 2];

//                datosa.push(tipo);
//                datosa.push(cantidad_con_tram);
//                datosa.push(cantidad_sin_tram);
//                datos.push([datosa[r], parseInt(datosa[r + 1]), parseInt(datosa[r + 2])]);
//                r = r + 3;

//            }

//            drawChartCerrados();
//        } else {
//            divTerceros.innerHTML = mensajecero;
//        }
//    } catch (elError) {
//    }
//}

//function drawChartCerrados() {
//    // Create the data table.
//    var data = new google.visualization.DataTable();
//    data.addColumn('string', 'TIPO');
//    data.addColumn('number', 'CON TRÁMITES');
//    data.addColumn('number', 'SIN TRÁMITES');
//    data.addRows(datos);

//    // Set chart options
//    var options = {
//        title: 'Cantidad de pacientes cerrados por edad',
//        hAxis: { title: 'TIPO', titleTextStyle: { color: 'black', bold: true } },
//        'vAxis': { title: 'Cantidad de Pacientes', titleTextStyle: { color: 'black', bold: true } },
//        'width': 250,
//        'height': 260,
//        seriesType: 'bars',
//        'hAxis': { direction: -1, slantedText: true, slantedTextAngle: 40 },
//        bar: { groupWidth: "30%" },
//        legend: { position: 'top', textStyle: { fontSize: 7 } },
//    };

//    var chart;
//    chart = new google.visualization.ColumnChart(document.getElementById('divGraficaCerrados'));
//    chart.draw(data, options);
//    datosa.length = 0;
//    datos.length = 0;
//}



function SeguimientoPeriodicoXsemanaNo_listar() {

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'SeguimientoPeriodicoXsemanaNo_listar'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, SeguimientoPeriodicoXsemanaNo_listar_processResponse);
    muestraVentanaProgreso('Cargando ...');
}

function SeguimientoPeriodicoXsemanaNo_listar_processResponse(res) {
    try {
        controlProcces();
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('divlistarSegPerNoRealizados');
        if (res != '0') {
            var datosRows = info.data;
            var l = info.cols;
            var f = datosRows.length - l;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";
            var r = 0;
            var contador = 1;
            var tabla = "<table class='tbListado centrar' style='text-align: center;'>";
            tabla += "<tr><td class='encabezado'>SEMANA</td><td class='encabezado'>CANT.</td>";

            for (var i = 0; i < datosRows.length; i += l) {

                var semana = datosRows[i];
                var cantidad = datosRows[i + 1];

                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar2 = "cuerpoListado3";
                } else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar2 = "cuerpoListado5";
                }
                ctl = !ctl;

                tabla += '<tr ><td class="' + claseAplicar2 + '" align="center" style="width=60px;"><b>' + semana + '</b></td>';
                tabla += '<td class="' + claseAplicar2 + '" align="center">' + cantidad + ' </td></tr>';
                contador ++;
            }

            tabla += '</table>'
            divTerceros.innerHTML = tabla;
        } else {
            divTerceros.innerHTML = mensajecero;
        }
    } catch (elError) {
    }
}




function verseguiper(){

var id = document.getElementById("lblverseguiper").innerHTML;

//document.getElementById("lblpacientesSegPerRealizadosTotal").innerHTML = document.getElementById("lblpacientesSegPerRealizados").innerHTML;



if (id == "Ver Efectivos<br>No Efectivos"){
//conteoIndicadoresIniciales();

 //ocultaVentanaProgreso();

document.getElementById("divlistarSegPerRealizados2").style.display = "none";
document.getElementById("lblseguiperrealizados").innerHTML = "Ejecutados";
 document.getElementById("divlistarSegPerRealizados2").innerHTML = document.getElementById("divlistarSegPerRealizados").innerHTML;
 document.getElementById("lblpacientesSegPerRealizadosTotal").innerHTML = seguiGlobalTotal;
document.getElementById("divseguiperdetallado").style.display="block";
document.getElementById("divseguiperdetallado2").style.display="block";

document.getElementById("lblverseguiper").innerHTML = 'Ver Resumen';

}else{

//mostrarotroresultado();
document.getElementById("divlistarSegPerRealizados2").innerHTML = document.getElementById("divlistarSegPerRealizados").innerHTML;
document.getElementById("lblpacientesSegPerRealizadosTotal").innerHTML = document.getElementById("lblpacientesSegPerRealizados").innerHTML;
document.getElementById("divlistarSegPerRealizados2").style.display = "block";
document.getElementById("lblpacientesSegPerRealizadosTotal").innerHTML = seguiGlobal;
document.getElementById("lblseguiperrealizados").innerHTML = "Realizados";
document.getElementById("divseguiperdetallado").style.display="none";
document.getElementById("divseguiperdetallado2").style.display="none";

document.getElementById("lblverseguiper").innerHTML = "Ver Efectivos<br>No Efectivos";
}


}


function mostrarotroresultado(){

document.getElementById("lblpacientesSegPerRealizadosTotal").innerHTML = seguiGlobal;
}


//////NUEVO///////



function listarPacientesXEdad() {

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'PacientesCerradosXTrimestre_listar'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, listarPacientesXEdad_processResponse);
    muestraVentanaProgreso('Cargando ...');
}

function listarPacientesXEdad_processResponse(res) {
    try {
        controlProcces();
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('divGraficaPorEdad');
        if (res != '0') {
            var datosRows = info.data;
            var l = info.cols;
            var f = datosRows.length - l;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";
            var r = 0;
            datosa.length = 0;
            datos.length = 0;

            for (var i = 0; i < datosRows.length; i += l) {

                var tipo = datosRows[i];
                var cantidad_con_tram = datosRows[i + 1];

                datosa.push(tipo);
                datosa.push(cantidad_con_tram);
                datos.push([datosa[r], parseInt(datosa[r + 1])]);
                r = r + 2;
                

            }

            drawChartPacientesPorEdad();
        } else {
            divTerceros.innerHTML = mensajecero;
        }
    } catch (elError) {
    }
}
 
function drawChartPacientesPorEdad() {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'TIPO');
    data.addColumn('number', 'Cantidad de Pacientes');
    data.addRows(datos);

    var options = {
        title: 'Cantidad de Pacientes Pendientes por Ingreso por Edad',
        hAxis: { title: 'TIPO', titleTextStyle: { color: 'black', bold: true } },
        pieHole: 0.52,
       'width': 227,
        'height': 237,            
        legend: { position: 'none' },
    };

    var chart;
    chart = new google.visualization.PieChart(document.getElementById('divGraficaPorEdad'));
    chart.draw(data, options);
    datosa.length = 0;
    datos.length = 0;
}


///////////////////NUEVO INICIO//////////////////
///////////////////NUEVO INICIO//////////////////
///////////////////NUEVO INICIO//////////////////
///////////////////NUEVO INICIO//////////////////
///////////////////NUEVO INICIO//////////////////
///////////////////NUEVO INICIO//////////////////


function contactoInicialFinal(){

 var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'contactoInicialFinal'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, contactoInicialFinal_processResponse);
    muestraVentanaProgreso('Cargando ...');
}


function contactoInicialFinal_processResponse(res) {
    try {
        controlProcces();
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
			
			if (info.data[0] ==''){
                document.getElementById("lblcontactoinicial").innerHTML = '0%';
				
				}
				
				else {
				
				 document.getElementById("lblcontactoinicial").innerHTML = info.data[0];
				}
				
				
				if (info.data[1] == ''){
                document.getElementById("lblcontactofinal").innerHTML = '0%';
               }
			   else{
			   
			    document.getElementById("lblcontactofinal").innerHTML = info.data[1];
			   }
                break;
        }
    } catch (elError) { }
}




function listasInicialFinal(){

 var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listasInicialFinal'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, listasInicialFinal_processResponse);
    muestraVentanaProgreso('Cargando ...');
}


function listasInicialFinal_processResponse(res) {
    try {
        controlProcces();
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
                document.getElementById("lbl1rai").innerHTML = info.data[0];
                document.getElementById("lbl2dai").innerHTML = info.data[1];
                document.getElementById("lbl3rai").innerHTML = info.data[2];
                document.getElementById("lblNCi").innerHTML = info.data[3];

                document.getElementById("lbl1raf").innerHTML = info.data[4];
                document.getElementById("lbl2daf").innerHTML = info.data[5];
                document.getElementById("lbl3raf").innerHTML = info.data[6];
                document.getElementById("lblncf").innerHTML = info.data[7];
               
                break;
        }
    } catch (elError) { }
}