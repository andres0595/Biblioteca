//arreglo para generar la clave del usuario Administrador
var caracter = new Array("0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z");

$(document).ready(function () {
    habilitarJuridica();
    cargaUbicacion();
    cargaInfoGeneralEmpresa();
    cargaTipDocumentoTercero();
    calculaTamanoFinal(new Array());

    $(function () {
        $("#txtVencimientoResolucion").datepicker({
            changeMonth: true,
            changeYear: true,
            numberOfMonths: 1,
            yearRange: 'c-15:c+15'
        });
    });
    $(function () {
        $("#txtInicioFacturacion").datepicker({
            changeMonth: true,
            changeYear: true,
            numberOfMonths: 1,
            yearRange: 'c-15:c+15'
        });
    });
    $(function () {
        $("#txtFinFacturacion").datepicker({
            changeMonth: true,
            changeYear: true,
            numberOfMonths: 1,
            yearRange: 'c-15:c+15'
        });
    });
});

////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////  VARIABLES GLOBALES /////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////
var paisGlobal = '-1';
var deptoGlobal = '-1';
var municipioGlobal = '-1';
var idGlobal = -1, pagGlobal = 1;
var globalActividadEconomica = "";


//var pag = 1;


function muestraVentanaFancybox(idDIv) {
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'scrolling': 'no',
        'href': '#' + idDIv,
        'onClosed': function () {
            idGlobal = "";
            $("#divFormulario input[type=text]").val("");
            $("#divFormulario textarea").val("");
        }
    });
}

/*******************************************************************************************
VERIFICA LA CANTIDAD DE DIGITOS DEL CAMPO NIT
*******************************************************************************************/
function calculaDigitoVerificacion(nit) {
    var dig = '--';
    if (nit.value.length >= 6) {
        dig = digitoVerificacion(nit.value);
    }
    else {
        dig = '--';
    }
    document.getElementById('numeroVerificacion').innerHTML = dig;
}

/*******************************************************************************************
CARGA EL TIPO DE DOCUMENTO TERCERO
*******************************************************************************************/
function cargaTipDocumentoTercero() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargaTipoDocPersona'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, cargaTipDocumentoTercero_processResponse);
}

function cargaTipDocumentoTercero_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                var data = info.data
                llenarSelect(res, document.getElementById('selectTipoDocumentoRepresentanteLegal'));
                llenarSelect(res, document.getElementById('selectTipoDocumentoContador'));
                llenarSelect(res, document.getElementById('selectTipoDocumentoRevisorFiscal'));
                document.getElementById('selectTipoDocumentoRepresentanteLegal').value = tipoDocRepGlobal;
                document.getElementById('selectTipoDocumentoContador').value = tipoDocContGlobal;
                document.getElementById('selectTipoDocumentoRevisorFiscal').value = tipoDocRevFiscal;
                break;
        }
    } catch (elError) { }
}

var tipoPersona = ""
function habilitarJuridica() {
    document.getElementById("divPersonaJuridica").style.display = "block";
    document.getElementById("divPersonaNatural").style.display = "none";
    tipoPersona = "1"
}

function habilitarNatural() {
    document.getElementById("divPersonaNatural").style.display = "block";
    document.getElementById("divPersonaJuridica").style.display = "none";
    tipoPersona = "2"
}

function estaEnVector(id, array) {
    var ctl = false;
    var long = array.length;
    for (var i = 0; i < long; i++) {
        if (array[i] == id) {
            ctl = true;
            return ctl;
        }
    }
    return ctl;

}

///////PARA DETERMINAR LA POSICION DEL ELEMENTO////////////
function posicion(id, array) {
    var ctl = -1;
    var long = array.length;
    for (var i = 0; i < long; i++) {
        if (array[i] == id) {
            ctl = i;
            return ctl;
        }
    }
    return ctl;
}

/*******************************************************************************************
CARGA EL PAIS
*******************************************************************************************/
function cargaPais() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargaPais'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, cargaPais_processResponse);
}

function cargaPais_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                llenarSelect(res, document.getElementById('selPaisSedePrincipal'));
                break;
        }
        if (paisGlobal != '') {
            document.getElementById('selPaisSedePrincipal').value = paisGlobal;
            cargaDepartamentoValor(paisGlobal);
        }
    } catch (elError) { }
}


/*******************************************************************************************
CARGA EL DEPARTAMENTO
*******************************************************************************************/
function cargaDepartamento(codPais) {
    if (codPais.value != '-1') {
        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', 'cargaDepto'));
        arrayParameters.push(newArg('pais', codPais.value));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlGeneral.aspx', send, cargaDepartamento_processResponse);
    } else {
        limpiarSelectOpcion(document.getElementById('selDeptoSedePrincipal'));
        limpiarSelectOpcion(document.getElementById('selMuniSedePrincipal'));
    }
}

/*******************************************************************************************
CARGA EL DEPARTAMENTO POR VALOR PASADO POR PARAMETRO
*******************************************************************************************/
function cargaDepartamentoValor(codPais) {
    if (codPais != '-1') {
        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', 'cargaDepto'));
        arrayParameters.push(newArg('pais', codPais));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlGeneral.aspx', send, cargaDepartamento_processResponse);
    } else {
        limpiarSelectOpcion(document.getElementById('selDeptoSedePrincipal'));
        limpiarSelectOpcion(document.getElementById('selMuniSedePrincipal'));
    }
}

function cargaDepartamento_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                llenarSelect(res, document.getElementById('selDeptoSedePrincipal'));
                break;
        }
        if (deptoGlobal != '-1') {
            document.getElementById('selDeptoSedePrincipal').value = deptoGlobal;
            cargaMunicipioValor(deptoGlobal);
        }
    } catch (elError) { }
}


/*******************************************************************************************
CARGA EL MUNICIPIO
*******************************************************************************************/
function cargaMunicipio(codMun) {
    if (codMun.value != '-1') {
        if (codMun.value != deptoGlobal) {
            municipioGlobal = '-1';
        }

        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', 'cargaMpio'));
        arrayParameters.push(newArg('depto', codMun.value));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlGeneral.aspx', send, cargaMunicipio_processResponse);
    } else {
        limpiarSelectOpcion(document.getElementById('selMuniSedePrincipal'));
    }
}

/*******************************************************************************************
CARGA EL MUNICIPIO POR VALOR
*******************************************************************************************/
function cargaMunicipioValor(codMun) {
    if (codMun != '-1') {
        if (codMun != deptoGlobal) {
            municipioGlobal = '-1';
        }

        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', 'cargaMpio'));
        arrayParameters.push(newArg('depto', codMun));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlGeneral.aspx', send, cargaMunicipio_processResponse);
    } else {
        limpiarSelectOpcion(document.getElementById('selMuniSedePrincipal'));
    }
}

function cargaMunicipio_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                llenarSelect(res, document.getElementById('selMuniSedePrincipal'));
                break;
        }
        if (municipioGlobal != '-1') {
            document.getElementById('selMuniSedePrincipal').value = municipioGlobal;
        }
    } catch (elError) { }
}

/**********************************************************************************************
CARGA LA UBICACION PARA LA SEDE PRINCIPAL
**********************************************************************************************/
function cargaUbicacion() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargaTipoUbic'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, cargaUbicacion_processResponse);
}

function cargaUbicacion_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                llenarSelect(res, document.getElementById('selUbicacionSedePrincipal'));
                document.getElementById('selUbicacionSedePrincipal').value = ubicacionGlobal;
                break;
        }
    } catch (elError) { }
}

/**********************************************************************************************
CARGA LOS CARGOS PRESENTES EN EL SISTEMA
**********************************************************************************************/
function cargaCargos() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargaListaCargos'));
    arrayParameters.push(newArg('id_area', ''));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, cargaCargos_processResponse);
}

function cargaCargos_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                llenarSelect(res, document.getElementById('selCargoContacto'));
                document.getElementById('selCargoContacto').value = cargoGlobal;
                break;
        }
    } catch (elError) { }
}

/*******************************************************************************************
CARGA TIPO CONTRIBUYENTE
*******************************************************************************************/
function cargarTipoContribuyente() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargaTipCont'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, cargarTipoContribuyente_processResponse);
}

function cargarTipoContribuyente_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                llenarSelect(res, document.getElementById('selectTipoContribuyente'));
                document.getElementById('selectTipoContribuyente').value = tipoContGlobal;
                break;
        }
    } catch (elError) { }
}


/*******************************************************************************************
CARGA TIPOS SOCIEDADES
*******************************************************************************************/
function cargarTipoSociedad() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargaTipSoc'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, cargarTipoSociedad_processResponse);
}

function cargarTipoSociedad_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                llenarSelect(res, document.getElementById('selTipoSociedad'));
                document.getElementById('selTipoSociedad').value = tipoSocGlobal;
                break;
        }
    } catch (elError) { }
}


var objGlobal = ""
function listarCodigosCIIU(sel, obj) {
    var nivel = sel;
    objGlobal = obj
    if (nivel != null && nivel != '0' && nivel != '-1') {
        nivel = sel.value
        //actividadEconomica = nivel;
    }
    if (sel.value == '-1') {
        limpiarSelectOpcion(document.getElementById('selGrupo'));
        limpiarSelectOpcion(document.getElementById('selClase'));
        document.getElementById('divCodigosCIIU').innerHTML = '';
        gruGlobal = '-1';
        claGlobal = '-1';
        actividadEconomica = '-1';
        return;
    }

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargaListaCIIU'));
    arrayParameters.push(newArg('nivel', nivel));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, listarCodigosCIIU_processResponse);
}

function listarCodigosCIIU_processResponse(res) {
    var info = eval('(' + res + ')');
    var msj = info.msj
    switch (msj) {
        case -1:
            muestraVentana(mensajemenosuno);
            break;
        case 0:
            muestraVentana(mensajecero);
            break;
        case 1:
            if (objGlobal.substring(0, 3) == "sel") {
                llenarSelect(res, document.getElementById(objGlobal));
                if (objGlobal.substring(3) == "Grupo") {
                    if (gruGlobal != '') {
                        document.getElementById('selGrupo').value = gruGlobal;
                    }
                }
                if (objGlobal.substring(3) == "Clase") {
                    if (claGlobal != '') {
                        document.getElementById('selClase').value = claGlobal;
                    }
                }
                if (objGlobal.substring(3) == "Division") {
                    if (divGlobal != '') {
                        document.getElementById('selDivision').value = divGlobal;
                    }
                }
            }
            if (objGlobal.substring(0, 3) == "div") {
                var data = info.data
                var ciiu = "", table = "", ctl = false, claseAplicar = "", claseSel = "";

                var divCIIU = document.getElementById(objGlobal);
                table = "<table class='tbListado centrar'><tr><td class='encabezado' colspan='2'>ACTIVIDADES ECONOMICAS</td></tr>";
                table += "<tr><td class='encabezado'>Sel</td><td class='encabezado'>ACTIVIDAD</td></tr>";
                for (var i = 2; i < data.length; i += 2) {
                    id = data[i];
                    ciiu = data[i + 1];

                    if (ctl) {
                        claseAplicar = "cuerpoListado11";
                        claseSel = "cuerpoListado7"
                    } else {
                        claseAplicar = "cuerpoListado12";
                        claseSel = "cuerpoListado8"
                    }
                    ctl = !ctl;
                    table += '<tr><td class="' + claseSel + '" align="center"><input type="radio" name="group1" onchange="codCiiu();" id="rdb' + id + '" value="' + id + '"/></td><td class="' + claseSel + '" align="left" style="text-align: left;">' + id + ' - ' + ciiu + '</td>';
                    //table += '<td class="' + claseAplicar + '"><div id="linkEdita" class="linkIconoLateral" onclick="buscarMÉDICAmento(' + id + ');"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/editar_24x24.png"> <p>Editar</p></div>';                        
                    table += '</tr>';
                }
                table += '</table>'
                divCIIU.innerHTML = table;
                if (actividadEconomica != '') {
                    //document.getElementById('rdb' + actividadEconomica).checked = true;
                    $("#rdb" + actividadEconomica).attr("checked", "checked");
                }
            }
            break;
    }
}


var actividadEconomica = "";
function codCiiu() {
    var val = "";
    var sel = document.getElementsByName('group1').length;
    for (var x = 0; x < sel; x++) {
        if (document.getElementsByName('group1').item(x).checked) {
            val = document.getElementsByName('group1').item(x).value.toString()
        }
    }
    actividadEconomica = val;
}


/***********************************************************************
CALCULA EL TAMAÑO DE LA EMPRESA CON EL VALOR, NUMEMPLEADOS Y ACTIVOS
***********************************************************************/
function calculaTamanoEmpresa() {
    var activos = document.getElementById('txtActivos').value
    var numEmpleados = document.getElementById('txtNumEmpleados').value
    var valor = document.getElementById('txtValor').value
    var arrayTamano = new Array()
    if ((activos != '') && (numEmpleados != '') && (valor != '')) {
        arrayTamano.push(calculaConNumEmpleados(numEmpleados))
        arrayTamano.push(calculaConActivos(activos))
        arrayTamano.push(calculaConValor(valor))
        calculaTamanoFinal(arrayTamano)
    } else {
        if ((activos != '') && (numEmpleados != '')) {
            arrayTamano.push(calculaConNumEmpleados(numEmpleados))
            arrayTamano.push(calculaConActivos(activos))
            calculaTamanoFinal(arrayTamano)
        } else {
            if ((activos != '') && (valor != '')) {
                arrayTamano.push(calculaConValor(valor))
                arrayTamano.push(calculaConActivos(activos))
                calculaTamanoFinal(arrayTamano)
            } else {
                if ((numEmpleados != '') && (valor != '')) {
                    arrayTamano.push(calculaConValor(valor))
                    arrayTamano.push(calculaConNumEmpleados(numEmpleados))
                    calculaTamanoFinal(arrayTamano)
                } else {
                    if (numEmpleados != '') {
                        arrayTamano.push(calculaConNumEmpleados(numEmpleados))
                        calculaTamanoFinal(arrayTamano)
                    } else {
                        if (valor != '') {
                            arrayTamano.push(calculaConValor(valor))
                            calculaTamanoFinal(arrayTamano)
                        } else {
                            arrayTamano.push(calculaConActivos(activos))
                            calculaTamanoFinal(arrayTamano)
                        }
                    }
                }
            }
        }
    }
}


/***********************************************************************
CALCULA EL TAMAÑO DE LA EMPRESA CON EL VALOR, NUMEMPLEADOS Y ACTIVOS
***********************************************************************/
var tamanioEmpresa = 0
function calculaTamanoFinal(array) {
    var numTam = array.sortNum().reverse()[0]
    switch (numTam) {
        case 1:
            document.getElementById('tamano').innerHTML = 'MICRO'
            break;

        case 2:
            document.getElementById('tamano').innerHTML = 'PEQUEÑA'
            break;

        case 3:
            document.getElementById('tamano').innerHTML = 'MEDIANA'
            break;

        case 4:
            document.getElementById('tamano').innerHTML = 'GRANDE'
            break;

        default:
            document.getElementById('tamano').innerHTML = 'MICRO'
    }
    tamanioEmpresa = numTam
}


/***********************************************************************
CALCULA EL TAMAÑO DE LA EMPRESA CON LOS ACTIVOS
***********************************************************************/
function calculaConActivos(activos) {
    if (activos <= 500)
        tamActivosGlobal = 1
    else {
        if ((activos > 500) && (activos <= 5000))
            tamActivosGlobal = 2
        else {
            if ((activos > 5001) && (activos <= 30000))
                tamActivosGlobal = 3
            else
                tamActivosGlobal = 4
        }
    }
    return tamActivosGlobal
}


/***********************************************************************
CALCULA EL TAMAÑO DE LA EMPRESA CON EL VALOR
***********************************************************************/
function calculaConValor(valor) {
    if (valor < 1000000)
        return false
    else {
        if (valor <= 230750000)
            tamValorGlobal = 1
        else {
            if ((valor > 230750000) && (valor <= 2307500000))
                tamValorGlobal = 2
            else {
                if ((valor > 2307500000) && (valor <= 13845000000))
                    tamValorGlobal = 3
                else
                    tamValorGlobal = 4
            }
        }
    }
    return tamValorGlobal
}


/***********************************************************************
CALCULA EL TAMAÑO DE LA EMPRESA CON EL NUMEMPLEADOS
***********************************************************************/
function calculaConNumEmpleados(numEmpleados) {
    if (numEmpleados <= 10)
        tamNumEmpleadosGlobal = 1
    else {
        if ((numEmpleados > 10) && (numEmpleados <= 50))
            tamNumEmpleadosGlobal = 2
        else {
            if ((numEmpleados > 51) && (numEmpleados <= 200))
                tamNumEmpleadosGlobal = 3
            else
                tamNumEmpleadosGlobal = 4
        }
    }
    return tamNumEmpleadosGlobal
}
/* Se crea la funcion SortNum para aplicarlo en el calculo del tamaño de la empresa */
Array.prototype.sortNum = function () {
    return this.sort(function (a, b) { return a - b; });
}


////////////////////////////////FORMULARIO NUEVO CON PESTAÑAS///////////////////
function guardaInfoGeneralEmpresa() {
    var nombre = document.getElementById('txtNombreEmpresa').value
    var nit = document.getElementById('txtNit').value
    var digVeri = document.getElementById('numeroVerificacion').innerHTML
    var tipoPer = tipoPersona
    var contribuyente = document.getElementById('selectTipoContribuyente').value
    var priApe = document.getElementById('txtPrimerApellidoPersonaNatural').value
    var segApe = document.getElementById('txtSegundoApellidoPersonaNatural').value
    var priNom = document.getElementById('txtPrimerNombrePersonaNatural').value
    var segNom = document.getElementById('txtSegundoNombrePersonaNatural').value
    var razonSocial = document.getElementById('txtRazonSocial').value
    var sociedad = document.getElementById('selTipoSociedad').value

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'editaInfoEmp'));
    arrayParameters.push(newArg('nit', nit));
    arrayParameters.push(newArg('digito', digVeri));
    arrayParameters.push(newArg('nombre', nombre));
    arrayParameters.push(newArg('tipoPersona', tipoPer));
    arrayParameters.push(newArg('tipoContribuyente', contribuyente));
    arrayParameters.push(newArg('razonSocial', razonSocial));
    arrayParameters.push(newArg('tipoSociedad', sociedad));
    arrayParameters.push(newArg('priApellido', priApe));
    arrayParameters.push(newArg('segApellido', segApe));
    arrayParameters.push(newArg('priNombre', priNom));
    arrayParameters.push(newArg('segNombre', segNom));
    arrayParameters.push(newArg('digi1', ''));
    arrayParameters.push(newArg('digi2', ''));
    arrayParameters.push(newArg('reg', '0'));
    var send = arrayParameters.join('&');

    muestraVentanaProgreso();
    $.post('../../Controlador/ctlEmpresa.aspx', send, guardaInfoGeneralEmpresa_processResponse);
}

function guardaInfoGeneralEmpresa_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                muestraVentana(mensajeGuarda);
                cargaInfoEconomicaEmpresa();
                setTimeout("ocultarPestanas('pest_economica')", 2200);
                break;
            case 2:
                muestraVentana(mensajeEdita);
                cargaInfoEconomicaEmpresa();
                setTimeout("ocultarPestanas('pest_economica')", 2200);
                break;
        }
    } catch (elError) {
    }
}

function guardaInfoEconomicaEmpresa() {
    var actividadPrincipal = document.getElementById('txtActividadPrincipal').value;
    var shd_actividadPrincipal = document.getElementById('txtCodActividad').value;
    var actividadSecundaria = document.getElementById('txtActividadSecundaria').value;
    var shd_actividadSecundaria = document.getElementById('txtCodActividadSecundaria').value;
    var otraActividad1 = document.getElementById('txtOtraActividad1').value;
    var shd_otraActividad1 = document.getElementById('txtCodOtraActividad1').value;
    var otraActividad2 = document.getElementById('txtOtraActividad2').value;
    var shd_otraActividad2 = document.getElementById('txtCodOtraActividad2').value;
    // var codigoHacienda = document.getElementById('txtCodigoHacienda').value;
    var numSedes = document.getElementById('txtNumSedes').value
    var valor = document.getElementById('txtValor').value
    var activos = document.getElementById('txtActivos').value
    var numEmpleados = document.getElementById('txtNumEmpleados').value
    var tamanio = tamanioEmpresa;
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'editaInfoEcoEmp'));
    arrayParameters.push(newArg('actividadPrincipal', actividadPrincipal));
    arrayParameters.push(newArg('shd_actividadPrincipal', shd_actividadPrincipal));
    arrayParameters.push(newArg('actividadSecundaria', actividadSecundaria));
    arrayParameters.push(newArg('shd_actividadSecundaria', shd_actividadSecundaria));
    arrayParameters.push(newArg('otraActividad1', otraActividad1));
    arrayParameters.push(newArg('shd_otraActividad1', shd_otraActividad1));
    arrayParameters.push(newArg('otraActividad2', otraActividad2));
    arrayParameters.push(newArg('shd_otraActividad2', shd_otraActividad2));
    //   arrayParameters.push(newArg('codigoHacienda', codigoHacienda));
    arrayParameters.push(newArg('numSedes', numSedes));
    arrayParameters.push(newArg('valor', valor));
    arrayParameters.push(newArg('activos', activos));
    arrayParameters.push(newArg('numEmpleados', numEmpleados));
    arrayParameters.push(newArg('tamanio', tamanio));
    arrayParameters.push(newArg('reg', '1'));
    var send = arrayParameters.join('&');

    muestraVentanaProgreso();
    $.post('../../Controlador/ctlEmpresa.aspx', send, guardaInfoEconomicaEmpresa_processResponse);
}

function guardaInfoEconomicaEmpresa_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -2:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                muestraVentana(mensajeGuarda);
                setTimeout("ocultarPestanas('pest_administrativa')", 2200);
                break;
            case 2:
                muestraVentana(mensajeEdita);
                setTimeout("ocultarPestanas('pest_administrativa')", 2200);
                break;
        }
    } catch (elError) {
    }
}

function guardaInfoAdministrativaEmpresa() {
    var repLegal = document.getElementById('txtRepresentanteLegal').value
    var tipoDocRep = document.getElementById('selectTipoDocumentoRepresentanteLegal').value
    var numDocRep = document.getElementById('txtNumeroDocumentoRepresentante').value
    var conta = document.getElementById('txtContador').value
    var tpConta = document.getElementById('txtTPContador').value
    var tipoDocConta = document.getElementById('selectTipoDocumentoContador').value
    var numDocConta = document.getElementById('txtDocumentoIdContador').value
    var revisor = document.getElementById('txtRevisorFiscal').value
    var tpRevisor = document.getElementById('txtTPRevisorFiscal').value
    var tipoDocRevisor = document.getElementById('selectTipoDocumentoRevisorFiscal').value
    var numDocRevisor = document.getElementById('txtDocumentoIdRevisor').value
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'editaInfoAdmEmp'));
    arrayParameters.push(newArg('representante', repLegal));
    arrayParameters.push(newArg('tipoDocRepresentante', tipoDocRep));
    arrayParameters.push(newArg('numDocRepresentante', numDocRep));
    arrayParameters.push(newArg('contador', conta));
    arrayParameters.push(newArg('tarjetaProfContador', tpConta));
    arrayParameters.push(newArg('tipoDocContador', tipoDocConta));
    arrayParameters.push(newArg('numDocContador', numDocConta));
    arrayParameters.push(newArg('revisorFiscal', revisor));
    arrayParameters.push(newArg('tarjetaProfRevisor', tpRevisor));
    arrayParameters.push(newArg('tipoDocRevisor', tipoDocRevisor));
    arrayParameters.push(newArg('numDocRevisor', numDocRevisor));
    arrayParameters.push(newArg('reg', '0'));
    var send = arrayParameters.join('&');

    muestraVentanaProgreso();
    $.post('../../Controlador/ctlEmpresa.aspx', send, guardaInfoAdministrativaEmpresa_processResponse);
}

function guardaInfoAdministrativaEmpresa_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                muestraVentana(mensajeGuarda);
                cargaInfoFinancieraEmpresa();
                setTimeout("ocultarPestanas('pest_financiera')", 2200);
                break;
            case 2:
                muestraVentana(mensajeEdita);
                cargaInfoFinancieraEmpresa();
                setTimeout("ocultarPestanas('pest_financiera')", 2200);
                break;
        }
    } catch (elError) {
    }
}

function nuevoRegistroFacturacion() {
    limpiarFacturacion();
    muestraVentanaFancybox('divFacturacion');
}


function limpiarFacturacion() {
    factGlobal = -1;
    $("#txtInicioFacturacion").val("");
    $("#txtFinFacturacion").val("");
    $("#txtResolucionFacturacion").val("");
    $("#txtVencimientoResolucion").val("");
}

function guardaInfoFinancieraEmpresa() {
    var fi = document.getElementById('txtInicioFacturacion').value
    var ff = document.getElementById('txtFinFacturacion').value
    var resol = document.getElementById('txtResolucionFacturacion').value
    var vence = document.getElementById('txtVencimientoResolucion').value

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'editaInfoFinEmp'));
    arrayParameters.push(newArg('idGlobal', idGlobal));
    arrayParameters.push(newArg('fechaInicioFact', fi));
    arrayParameters.push(newArg('fechaFinFact', ff));
    arrayParameters.push(newArg('resolucion', resol));
    arrayParameters.push(newArg('vencimientoRes', vence));

    arrayParameters.push(newArg('reg', '0'));
    var send = arrayParameters.join('&');

    muestraVentanaProgreso();
    $.post('../../Controlador/ctlEmpresa.aspx', send, guardaInfoFinancieraEmpresa_processResponse);
}

function guardaInfoFinancieraEmpresa_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -2:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                muestraVentana(mensajeGuarda);
                listarInformacionFactura(pagGlobal);

                break;
            case 2:
                muestraVentana(mensajeEdita);
                listarInformacionFactura(pagGlobal);
                break;
        }
    } catch (elError) {
    }
}

function continuarFacturacion() {
    cargaPais();
    setTimeout("ocultarPestanas('pest_sedePrincipal')", 2200);
}



/*********************************************************************************************************************************
LISTA LA INFORMACIÓN EXISTENTE
******************************************************************************************************************************** */
function listarInformacionFactura(pag) {
    cerrarVentanaEmergente();
    pagGlobal = pag;

    var empresa = document.getElementById('empresa').value;

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listaFacturacion'));
    arrayParameters.push(newArg('pag', pagGlobal));
    arrayParameters.push(newArg('empresa', empresa));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlPaginador.aspx', send, listarInformacionFactura_processResponse);
}

function listarInformacionFactura_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var divListado = document.getElementById('listadoInformacion');

        if (res != '0') {
            var dataRows = info.data;
            var l = dataRows.length;
            var cols = info.cols;
            var ctl = true, claseAplicar = "", claseAplicar2 = "";
            var id = "";
            var fechaIni = "";
            var fechaFin = "";
            var resolucion = "";
            var fechaVencRes = "";

            tabla = "<table class='tbListado centrar' style='text-align: center;'><tr><td class='encabezado' colspan='6'>FACTURACI&Oacute;N</td></tr>";
            tabla += "<tr><td class='encabezado'>INICIO FACT.</td><td class='encabezado'>FIN FACT.</td><td class='encabezado'>RESOLUCI&Oacute;N</td><td class='encabezado'>VER DETALLE</td><td class='encabezado'>EDITAR</td><td class='encabezado'>ELIMINAR</td></tr>";

            for (var i = 0; i < l; i += cols) {
                id = dataRows[i];
                fechaIni = dataRows[i + 1];
                fechaFin = dataRows[i + 2];
                resolucion = dataRows[i + 3];
                fechaVencRes = dataRows[i + 4];

                if (ctl) {
                    claseAplicar = "cuerpoListado5";
                    claseAplicar2 = "cuerpoListado7";
                }
                else {
                    claseAplicar = "cuerpoListado2";
                    claseAplicar2 = "cuerpoListado8";
                }
                ctl = !ctl;

                tabla += '<tr><td class="' + claseAplicar + '">' + fechaIni + '</td><td class="' + claseAplicar2 + '">' + fechaFin + '</td><td class="' + claseAplicar2 + '">' + resolucion + '</td>';
                tabla += '<td class="' + claseAplicar + '"><div id="linkDetalle" class="linkIconoLateral botonDetalle" onclick="verDetalle(\'' + fechaIni + '\',\'' + fechaFin + '\',\'' + resolucion + '\',\'' + fechaVencRes + '\');"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/listar.png"><p>Detalle</p></div></td>';
                tabla += '<td class="' + claseAplicar + '"><div id="imgEditar" class="linkIconoLateral botonEditar ocultar" onclick="abrirEditar(\'' + id + '\',\'' + fechaIni + '\',\'' + fechaFin + '\',\'' + resolucion + '\',\'' + fechaVencRes + '\');"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/editar_24x24.png"><p>Editar</p></div></td>';
                tabla += '<td class="' + claseAplicar + '"><div id="imgEliminar" class="linkIconoLateral botonEliminar ocultar" onclick="confirmaEliminar(\'' + id + '\');"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/eliminar_24x24.png"><p>Eliminar</p></div></td></tr>';
            }
            tabla += '</table>';
            divListado.innerHTML = tabla;
            divListado.innerHTML += pieDePaginaListar(info, 'listar'); /*llama de nuevo el paginar con la nueva pag*/

            var idMenuForm = document.getElementById('idMenuForm').innerHTML; /* se adiciona esta linea para que se el permiso de visualizar el editar despues de cargar*/
            permisosParaMenu(idMenuForm); /* se adiciona esta linea para que se el permiso de visualizar el editar despues de cargar*/
        } else {
            divListado.innerHTML = mensajecero;
        }
    } catch (elError) {
        //alert(elError)
    }
}

function verDetalle(ini, fin, resolucion, fechaVencRes) {
    var tabla = "<table class='tbListado centrar' style='text-align: center;'><tr><td class='encabezado' colspan='6'>FACTURACI&Oacute;N</td></tr>";
    tabla += "<tr><td class='encabezado'>INICIO FACTURACI&Oacute;N</td><td class='encabezado'>FIN FACTURACI&Oacute;N</td><td class='encabezado'>RESOLUCI&Oacute;N</td><td class='encabezado'>FECHA VENCIMIENTO</td></tr>";

    tabla += "<tr><td class='cuerpoListado6'>" + ini + "</td><td class='cuerpoListado6'>" + fin + "</td><td class='cuerpoListado6'>" + resolucion + "</td><td class='cuerpoListado6'>" + fechaVencRes + "</td></tr></tr></table>";
    $("#divDetalle").html(tabla);
    muestraVentanaFancybox("divFormularioDetalle");
}

/*********************************************************************************************************************************
MUESTRA VENTANA PARA CONFIRMAR LA ELIMINACIÓN
******************************************************************************************************************************** */
function confirmaEliminar(id) {
    factGlobal = id;
    muestraVentanaFancybox("confirmaEliminar");
}

/*********************************************************************************************************************************
ELIMINA LA INFORMACIÓN DEL REGISTRO
******************************************************************************************************************************** */
function eliminarFacturacion() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'eliminarFact'));
    arrayParameters.push(newArg('id', factGlobal));
    arrayParameters.push(newArg('reg', '0'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlEmpresa.aspx', send, eliminarFacturacion_processResponse);
}

function eliminarFacturacion_processResponse(res) {
    //alert(res)
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                muestraVentana(mensajeElimina);
                listarInformacionFactura(pagGlobal);
                cerrarVentanaEmergente();
                break;
        }
    } catch (elError) { }
}

/*********************************************************************************************************************************
MUESTRA VENTANA PARA EDITAR LA INFORMACIÓN
******************************************************************************************************************************** */
function abrirEditar(id, fecIni, fecFin, Res, vencRes) {
    factGlobal = id;
    $("#txtInicioFacturacion").val(fecIni);
    $("#txtFinFacturacion").val(fecFin);
    $("#txtResolucionFacturacion").val(Res);
    $("#txtVencimientoResolucion").val(vencRes);
    muestraVentanaFancybox("divFacturacion");
}

function guardaInfoSedePrincipalEmpresa() {
    var nomSede = document.getElementById('txtNombreSedePrincipal').value
    var muniSede = document.getElementById('selMuniSedePrincipal').value
    var dirSede = document.getElementById('txtDirSedePrincipal').value
    var telSede = document.getElementById('txtTelSedePrincipal').value
    var mailSede = document.getElementById('txtMailSedePrincipal').value
    var ubiSede = document.getElementById('selUbicacionSedePrincipal').value
    var numEmpleSede = document.getElementById('txtNumEmpleSedePrincipal').value
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'editaInfoSedPpalEmp'));
    arrayParameters.push(newArg('nombreSede', nomSede));
    arrayParameters.push(newArg('municipio', muniSede));
    arrayParameters.push(newArg('direccion', dirSede));
    arrayParameters.push(newArg('telefono', telSede));
    arrayParameters.push(newArg('mail', mailSede));
    arrayParameters.push(newArg('ubicacion', ubiSede));
    arrayParameters.push(newArg('numEmpleados', numEmpleSede));
    arrayParameters.push(newArg('reg', '0'));
    var send = arrayParameters.join('&');

    muestraVentanaProgreso();
    $.post('../../Controlador/ctlEmpresa.aspx', send, guardaInfoSedePrincipalEmpresa_processResponse);
}

function guardaInfoSedePrincipalEmpresa_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                muestraVentana(mensajeGuarda);
                cargaInfoContactoEmpresa();
                setTimeout("ocultarPestanas('pest_contacto')", 2200);
                break;
            case 2:
                muestraVentana(mensajeEdita);
                cargaInfoContactoEmpresa();
                setTimeout("ocultarPestanas('pest_contacto')", 2200);
                break;
        }
    } catch (elError) {
    }
}

function guardaInfoContactoEmpresa() {
    var nomContact = document.getElementById('txtContacto').value
    var cargoContact = document.getElementById('selCargoContacto').value
    var telContact = document.getElementById('txtTelContacto').value
    var mailContact = document.getElementById('txtEmailContacto').value
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'editaInfoContEmp'));
    arrayParameters.push(newArg('nombreContacto', nomContact));
    arrayParameters.push(newArg('cargoContacto', cargoContact));
    arrayParameters.push(newArg('telefono', telContact));
    arrayParameters.push(newArg('email', mailContact));
    arrayParameters.push(newArg('reg', '0'));
    var send = arrayParameters.join('&');

    muestraVentanaProgreso();
    $.post('../../Controlador/ctlEmpresa.aspx', send, guardaInfoContactoEmpresa_processResponse);
}

function guardaInfoContactoEmpresa_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                muestraVentana(mensajeGuarda);
                cargaInfoAdministradorEmpresa();
                setTimeout("ocultarPestanas('pest_Administrador')", 2200);
                break;
            case 2:
                muestraVentana(mensajeEdita);
                cargaInfoAdministradorEmpresa();
                setTimeout("ocultarPestanas('pest_Administrador')", 2200);
                break;
        }
    } catch (elError) {
    }
}

function guardaInfoAdministradorEmpresa() {

    setTimeout("ocultarPestanas('pest_logo')", 200);
    //var user = document.getElementById('txtUsuarioEmpresa').value
    //var name = document.getElementById('txtNombreAdminEmpresa').value
    //var mail = document.getElementById('txtEmailAdminEmpresa').value
    //if (mail != '' && user != '') {
    //    if (vemail(mail)) {
    //        var clave = generarClave();
    //        var arrayParameters = new Array();
    //        arrayParameters.push(newArg('p', 'editaInfoAdmorEmp'));
    //        arrayParameters.push(newArg('id', idGlobal));
    //        arrayParameters.push(newArg('usuario', user));
    //        arrayParameters.push(newArg('nombre', name));
    //        arrayParameters.push(newArg('clave', clave));
    //        arrayParameters.push(newArg('email', mail));
    //        arrayParameters.push(newArg('reg', '0'));
    //        var send = arrayParameters.join('&');

    //        muestraVentanaProgreso();
    //        $.post('../../Controlador/ctlEmpresa.aspx', send, guardaInfoAdministradorEmpresa_processResponse);
    //    } else {
    //        muestraVentana('El correo electr&oacute;nico no tiene el formato correcto!!!');
    //    }
    //} else {
    //    muestraVentana(mensajeObligatorio);
    //}
}

function guardaInfoAdministradorEmpresa_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                muestraVentana('Informaci&oacute;n del Administrador registrada exitosamente!!\n\n Se ha enviado un correo al Usuario');
                cargaInfoLogoEmpresa();
                setTimeout("ocultarPestanas('pest_logo')", 2200);
                break;
            case 2:
                muestraVentana(mensajeEdita);
                cargaInfoLogoEmpresa();
                setTimeout("ocultarPestanas('pest_logo')", 2200);
                break;
        }
    } catch (elError) {
    }
}

function guardaLogoEmpresa() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'editaInfoLogEmp'));
    arrayParameters.push(newArg('reg', '0'));
    var send = arrayParameters.join('&');

    muestraVentanaProgreso();
    $.post('../../Controlador/ctlEmpresa.aspx', send, guardaLogoEmpresa_processResponse);
}

function guardaLogoEmpresa_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                muestraVentana(mensajeGuarda);
                break;
            case 3:
                muestraVentana(mensajeSinInformacion);
                break;
        }
    } catch (elError) {
    }
}


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////  GENERA CLAVE ALEATORIA   /////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function generarClave() {
    var i = 0;
    var clave = '';
    while (i < 10) {
        clave += caracter[(Math.round(Math.random() * 35))];
        i++;
    }
    return clave;
}



/////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*******************************************************************************************
VALIDA ACTIVIDAD ECONOMICA QUE NO CONTENGA AUXILIARES
Procedimiento: validaActividadEconomica
*******************************************************************************************/
function validaActividadEconomicaBusqueda(idActividad) {
    var actividad = String(idActividad);
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'validaActividadEconomica'));
    arrayParameters.push(newArg('actividad', actividad));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, validaActividadEconomica_processResponse);
}

/*******************************************************************************************
VALIDA ACTIVIDAD ECONOMICA QUE NO CONTENGA AUXILIARES
Procedimiento: validaActividadEconomica
*******************************************************************************************/
function validaActividadEconomica(actividadEconomica) {
    globalActividadEconomica = actividadEconomica;
    var actividad = '';
    if (actividadEconomica == 'ACTIVIDAD')
        actividad = document.getElementById('txtActividadPrincipal').value;
    if (actividadEconomica == 'SECUNDARIA')
        actividad = document.getElementById('txtActividadSecundaria').value;
    if (actividadEconomica == 'OTRA ACTIVIDAD 1')
        actividad = document.getElementById('txtOtraActividad1').value;
    if (actividadEconomica == 'OTRA ACTIVIDAD 2')
        actividad = document.getElementById('txtOtraActividad2').value;

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'validaActividadEconomica'));
    arrayParameters.push(newArg('actividad', actividad));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, validaActividadEconomica_processResponse);
}

function validaActividadEconomica_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                //verificarEstadoCuenta(document.getElementById("txtCuentaCompra").value);
                break;
            case 1:
                var codigo = info.data[0];

                if (codigo == '1020' || codigo == '1040' || codigo == '1420' || codigo == '1430' || codigo == '3520' || codigo == '3600' || codigo == '3900' || codigo == '4541' || codigo == '4620' || codigo == '4632' || codigo == '4645' || codigo == '4649' || codigo == '4661' || codigo == '4663' || codigo == '4711' || codigo == '4719' || codigo == '4724' || codigo == '4752' || codigo == '4761' || codigo == '4773' || codigo == '4781' || codigo == '4791' || codigo == '4792' || codigo == '4799' || codigo == '5811' || codigo == '6020' || codigo == '6499' || codigo == '6611' || codigo == '6910' || codigo == '6920' || codigo == '7010' || codigo == '7020' || codigo == '7110' || codigo == '7120' || codigo == '7220' || codigo == '7320' || codigo == '7410' || codigo == '7490' || codigo == '8523' || codigo == '8551') {

                    if (info.data[0] != -2) {
                        if (globalActividadEconomica == 'ACTIVIDAD') {
                            document.getElementById('txtActividadPrincipal').value = info.data[0];
                            var codigo = info.data[0];
                            document.getElementById('lblActividadPrincipal').innerHTML = info.data[1];
                            listaCodigosHacienda(codigo);
                        }
                        if (globalActividadEconomica == 'SECUNDARIA') {
                            document.getElementById('txtActividadSecundaria').value = info.data[0];
                            var codigo = info.data[0];
                            document.getElementById('lblActividadSecundaria').innerHTML = info.data[1];
                            listaCodigosHacienda(codigo);
                        }
                        if (globalActividadEconomica == 'OTRA ACTIVIDAD 1') {
                            document.getElementById('txtOtraActividad1').value = info.data[0];
                            var codigo = info.data[0];
                            document.getElementById('lblOtraActividad1').innerHTML = info.data[1];
                            listaCodigosHacienda(codigo);
                        }
                        if (globalActividadEconomica == 'OTRA ACTIVIDAD 2') {
                            document.getElementById('txtOtraActividad2').value = info.data[0];
                            var codigo = info.data[0];
                            document.getElementById('lblOtraActividad2').innerHTML = info.data[1];
                            listaCodigosHacienda(codigo);
                        }
                        document.getElementById('listadoActividades').innerHTML = '';
                        muestraVentana('CÓDIGO VALIDO!!!');
                        cerrarVentana();
                    }
                    else {
                        muestraVentana('ESTE CÓDIGO CONTIENE CÓDIGO AUXILIARES.');
                    }
                }
                else {
                    var actvidad1 = document.getElementById('txtActividadPrincipal').value;
                    var actvidad2 = document.getElementById('txtActividadSecundaria').value;
                    var actvidad3 = document.getElementById('txtOtraActividad1').value;
                    var actvidad4 = document.getElementById('txtOtraActividad2').value;

                    if (info.data[0] == actvidad1 || info.data[0] == actvidad2 || info.data[0] == actvidad3 || info.data[0] == actvidad4) {
                        muestraVentana('ESTE CÓDIGO YA SE HA REGISTRADO');
                    }
                    else {
                        if (info.data[0] != -2) {
                            if (globalActividadEconomica == 'ACTIVIDAD') {
                                document.getElementById('txtActividadPrincipal').value = info.data[0];
                                var codigo = info.data[0];
                                document.getElementById('lblActividadPrincipal').innerHTML = info.data[1];
                                listaCodigosHacienda(codigo);
                            }
                            if (globalActividadEconomica == 'SECUNDARIA') {
                                document.getElementById('txtActividadSecundaria').value = info.data[0];
                                var codigo = info.data[0];
                                document.getElementById('lblActividadSecundaria').innerHTML = info.data[1];
                                listaCodigosHacienda(codigo);
                            }
                            if (globalActividadEconomica == 'OTRA ACTIVIDAD 1') {
                                document.getElementById('txtOtraActividad1').value = info.data[0];
                                var codigo = info.data[0];
                                document.getElementById('lblOtraActividad1').innerHTML = info.data[1];
                                listaCodigosHacienda(codigo);
                            }
                            if (globalActividadEconomica == 'OTRA ACTIVIDAD 2') {
                                document.getElementById('txtOtraActividad2').value = info.data[0];
                                var codigo = info.data[0];
                                document.getElementById('lblOtraActividad2').innerHTML = info.data[1];
                                listaCodigosHacienda(codigo);
                            }
                            document.getElementById('listadoActividades').innerHTML = '';
                            muestraVentana('CÓDIGO VALIDO!!!');
                            cerrarVentana();
                        }
                        else {
                            muestraVentana('ESTE CÓDIGO CONTIENE CÓDIGO AUXILIARES.');
                        }
                    }
                }
        }
    } catch (elError) { }
}
/*******************************************************************************************
LISTA LOS CODIGOS DE HACIENDA DE ACUERDO AL CODIGO DE ACTIVIDAD
Procedimiento: 
*******************************************************************************************/
function listaCodigosHacienda(codigo) {
    if (codigo == '1020' || codigo == '1040' || codigo == '1420' || codigo == '1430' || codigo == '3520' || codigo == '3600' || codigo == '3900' || codigo == '4541' || codigo == '4620' || codigo == '4632' || codigo == '4645' || codigo == '4649' || codigo == '4661' || codigo == '4663' || codigo == '4711' || codigo == '4719' || codigo == '4724' || codigo == '4752' || codigo == '4761' || codigo == '4773' || codigo == '4781' || codigo == '4791' || codigo == '4792' || codigo == '4799' || codigo == '5811' || codigo == '6020' || codigo == '6499' || codigo == '6611' || codigo == '6910' || codigo == '6920' || codigo == '7010' || codigo == '7020' || codigo == '7110' || codigo == '7120' || codigo == '7220' || codigo == '7320' || codigo == '7410' || codigo == '7490' || codigo == '8523' || codigo == '8551') {
        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', 'listarCodigosHacienda'));
        arrayParameters.push(newArg('codigo', codigo));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlGeneral.aspx', send, listaCodigosHacienda_processResponse);
    }
    else {
    }
}

function listaCodigosHacienda_processResponse(res) {
    try {
        var info = eval('(' + res + ')');

        if (globalActividadEconomica == 'ACTIVIDAD') {
            document.getElementById('divListadoCodHacienda1').style.display = 'block';
            var divTerceros = document.getElementById('divListadoCodHacienda1');
        } else if (globalActividadEconomica == 'SECUNDARIA') {
            document.getElementById('divListadoCodHacienda2').style.display = 'block';
            var divTerceros = document.getElementById('divListadoCodHacienda2');
        } else if (globalActividadEconomica == 'OTRA ACTIVIDAD 1') {
            document.getElementById('divListadoCodHacienda3').style.display = 'block';
            var divTerceros = document.getElementById('divListadoCodHacienda3');
        } else {
            document.getElementById('divListadoCodHacienda4').style.display = 'block';
            var divTerceros = document.getElementById('divListadoCodHacienda4');
        }

        var tabla = '';
        if (res != '0') {
            var datosRows = info.data;
            var cols = info.cols;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";
            var codigo_CIIU = "", codigo_Hac = "", descripcion = "",

            tabla = "<table class='tbListado centrar' style='text-align: center;'><tr><td class='encabezado' colspan='2'>CLASIFICACI&Oacute;N SECRETAR&Iacute;A DE HACIENDA DISTRITAL</td></tr>";
            tabla += "<tr><td class='encabezado'>C&Oacute;DIGO</td><td class='encabezado'>DESCRIPCI&Oacute;N</td></tr>";

            for (var i = 0; i < datosRows.length; i += cols) {
                codigo_CIIU = datosRows[i];
                codigo_Hac = datosRows[i + 1];
                descripcion = datosRows[i + 2];

                if (ctl) {
                    claseAplicar = "cuerpoListado7";
                    claseAplicar1 = "cuerpoListado1";
                }
                else {
                    claseAplicar = "cuerpoListado8";
                    claseAplicar1 = "cuerpoListado2";
                }
                ctl = !ctl;
                tabla += '<tr><td class="' + claseAplicar1 + '"><a onclick="selecionCodigoHacienda(\'' + datosRows[i] + '\',\'' + datosRows[i + 1] + '\')" style="color:red;"  href="javascript:void(0)" >' + datosRows[i + 1] + '</td><td class="' + claseAplicar + '">' + descripcion + '</td></tr>';
            }


            tabla += '</table>'
            divTerceros.innerHTML = tabla;
        } else {
            divTerceros.innerHTML = mensajecero;
        }
    } catch (elError) {
    }

}
/*******************************************************************************************
FUNCION DE SELECCION DEL CODIGO DE HACIENDA
*******************************************************************************************/
function listaCodigosHacienda(codigo) {
    if (codigo == '1020' || codigo == '1040' || codigo == '1420' || codigo == '1430' || codigo == '3520' || codigo == '3600' || codigo == '3900' || codigo == '4541' || codigo == '4620' || codigo == '4632' || codigo == '4645' || codigo == '4649' || codigo == '4661' || codigo == '4663' || codigo == '4711' || codigo == '4719' || codigo == '4724' || codigo == '4752' || codigo == '4761' || codigo == '4773' || codigo == '4781' || codigo == '4791' || codigo == '4792' || codigo == '4799' || codigo == '5811' || codigo == '6020' || codigo == '6499' || codigo == '6611' || codigo == '6910' || codigo == '6920' || codigo == '7010' || codigo == '7020' || codigo == '7110' || codigo == '7120' || codigo == '7220' || codigo == '7320' || codigo == '7410' || codigo == '7490' || codigo == '8523' || codigo == '8551') {
        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', 'listarCodigosHacienda'));
        arrayParameters.push(newArg('codigo', codigo));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlGeneral.aspx', send, listaCodigosHacienda_processResponse);
    }
    else {
    }
}

function listaCodigosHacienda_processResponse(res) {
    try {
        var info = eval('(' + res + ')');

        if (globalActividadEconomica == 'ACTIVIDAD') {
            document.getElementById('divListadoCodHacienda1').style.display = 'block';
            var divTerceros = document.getElementById('divListadoCodHacienda1');
        } else if (globalActividadEconomica == 'SECUNDARIA') {
            document.getElementById('divListadoCodHacienda2').style.display = 'block';
            var divTerceros = document.getElementById('divListadoCodHacienda2');
        } else if (globalActividadEconomica == 'OTRA ACTIVIDAD 1') {
            document.getElementById('divListadoCodHacienda3').style.display = 'block';
            var divTerceros = document.getElementById('divListadoCodHacienda3');
        } else {
            document.getElementById('divListadoCodHacienda4').style.display = 'block';
            var divTerceros = document.getElementById('divListadoCodHacienda4');
        }

        var tabla = '';
        if (res != '0') {
            var datosRows = info.data;
            var cols = info.cols;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";
            var codigo_CIIU = "", codigo_Hac = "", descripcion = "",

            tabla = "<table class='tbListado centrar' style='text-align: center;'><tr><td class='encabezado' colspan='2'>CLASIFICACI&Oacute;N SECRETAR&Iacute;A DE HACIENDA DISTRITAL</td></tr>";
            tabla += "<tr><td class='encabezado'>C&Oacute;DIGO</td><td class='encabezado'>DESCRIPCI&Oacute;N</td></tr>";

            for (var i = 0; i < datosRows.length; i += cols) {
                codigo_CIIU = datosRows[i];
                codigo_Hac = datosRows[i + 1];
                descripcion = datosRows[i + 2];

                if (ctl) {
                    claseAplicar = "cuerpoListado7";
                    claseAplicar1 = "cuerpoListado1";
                }
                else {
                    claseAplicar = "cuerpoListado8";
                    claseAplicar1 = "cuerpoListado2";
                }
                ctl = !ctl;
                tabla += '<tr><td class="' + claseAplicar1 + '"><a onclick="selecionCodigoHacienda(\'' + datosRows[i] + '\',\'' + datosRows[i + 1] + '\')" style="color:red;"  href="javascript:void(0)" >' + datosRows[i + 1] + '</td><td class="' + claseAplicar + '">' + descripcion + '</td></tr>';
            }


            tabla += '</table>'
            divTerceros.innerHTML = tabla;
        } else {
            divTerceros.innerHTML = mensajecero;
        }
    } catch (elError) {
    }
    //$.fancybox.close();
    //  globalActividadEconomica = "";
}

/*******************************************************************************************
FUNCION DE SELECCION DEL CODIGO DE HACIENDA
*******************************************************************************************/
function selecionCodigoHacienda(codigoACT, codigoSHD) {

    var actvidad1 = document.getElementById('txtActividadPrincipal').value;
    var shd1 = document.getElementById('txtCodActividad').value;
    var actvidad2 = document.getElementById('txtActividadSecundaria').value;
    var shd2 = document.getElementById('txtCodActividadSecundaria').value;
    var actvidad3 = document.getElementById('txtOtraActividad1').value;
    var shd3 = document.getElementById('txtCodOtraActividad1').value;
    var actvidad4 = document.getElementById('txtOtraActividad2').value;
    var shd4 = document.getElementById('txtCodOtraActividad2').value;


    if ((codigoACT == actvidad1 && codigoSHD == shd1) || (codigoACT == actvidad2 && codigoSHD == shd2) || (codigoACT == actvidad3 && codigoSHD == shd3) || (codigoACT == actvidad4 && codigoSHD == shd4)) {
        muestraVentana('ESTE CÓDIGO YA SE HA REGISTRADO');
    }
    else {
        if (globalActividadEconomica == 'ACTIVIDAD') {
            document.getElementById('txtCodActividad').value = codigoSHD;
            document.getElementById('CodAuxActividadPrincipal').style.display = 'block';
            document.getElementById('txtCodActividad').style.display = 'block';
            document.getElementById('divListadoCodHacienda1').style.display = 'none';
        } else if (globalActividadEconomica == 'SECUNDARIA') {
            document.getElementById('txtCodActividadSecundaria').value = codigoSHD;
            document.getElementById('txtCodActividadSecundaria').style.display = 'block';
            document.getElementById('CodAuxActividadSecundaria').style.display = 'block';
            document.getElementById('divListadoCodHacienda2').style.display = 'none';
        } else if (globalActividadEconomica == 'OTRA ACTIVIDAD 1') {
            document.getElementById('txtCodOtraActividad1').value = codigoSHD;
            document.getElementById('txtCodOtraActividad1').style.display = 'block';
            document.getElementById('CodAuxOtraActividad1').style.display = 'block';
            document.getElementById('divListadoCodHacienda3').style.display = 'none';
        } else {
            document.getElementById('txtCodOtraActividad2').value = codigoSHD;
            document.getElementById('txtCodOtraActividad2').style.display = 'block';
            document.getElementById('CodAuxOtraActividad2').style.display = 'block';
            document.getElementById('divListadoCodHacienda4').style.display = 'none';
        }
        globalActividadEconomica = "";
    }
}
/*******************************************************************************************
BUSCAR ACTIVIDAD ECONOMICA
Procedimiento: buscaActividadEconomica
*******************************************************************************************/
function buscaActividadEconomica(pag, actividadEconomica) {
    if (actividadEconomica === undefined) { }
    else if (actividadEconomica != "") {
        globalActividadEconomica = actividadEconomica;
    }

    // var pag = 1;

    var actividad = '';
    if (actividadEconomica == 'ACTIVIDAD')
        actividad = document.getElementById('txtActividadPrincipal').value;
    if (actividadEconomica == 'SECUNDARIA')
        actividad = document.getElementById('txtActividadSecundaria').value;
    if (actividadEconomica == 'OTRA ACTIVIDAD 1')
        actividad = document.getElementById('txtOtraActividad1').value;
    if (actividadEconomica == 'OTRA ACTIVIDAD 2')
        actividad = document.getElementById('txtOtraActividad2').value;

    var codigo = actividad;
    if (codigo == '') {
        codigo = document.getElementById('numDocuFil').value;
    }
    var nombre = document.getElementById('nomProFil').value;

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listaAcividadesPaginado'));
    // arrayParameters.push(newArg('actividad', actividad));
    arrayParameters.push(newArg('codigo', codigo));
    arrayParameters.push(newArg('nombre', nombre));

    arrayParameters.push(newArg('pag', pag));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlPaginador.aspx', send, buscaActividadEconomica_processResponse);
}

function buscaActividadEconomica_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('listadoActividadesEconomicas');
        if (res != '0') {
            var datosRows = info.data;
            var l = info.cols;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";

            var id = "";
            var codigo = "";
            var nombre = "";
            var especialidad = "";


            var tabla = "<table class='tbListado centrar' style='text-align: center;'> <tr><td class='encabezado' colspan='5'>CÓDIGOS EXISTENTES</td></tr>";
            tabla += "<tr><td class='encabezado'>CÓDIGO</td><td class='encabezado'>NOMBRE</td><td class='encabezado'>SELECCIONAR</td>";

            for (var i = 0; i < datosRows.length; i += l) {
                codigo = datosRows[i];
                nombre = datosRows[i + 1];




                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar2 = "cuerpoListado3";
                } else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar2 = "cuerpoListado5";
                }

                ctl = !ctl;
                tabla += '<tr>';
                tabla += '<td class="' + claseAplicar + '" align="center">' + unescape(codigo).toUpperCase() + '</td>';
                tabla += '<td class="' + claseAplicar + '" align="center">' + unescape(nombre).toUpperCase() + '</td>';
                if (especialidad == -1) {
                    especialidad = 'NINGUNA';
                }
                // tabla += '<td class="' + claseAplicar + '" align="center">' + unescape(especialidad).toUpperCase() + '</td>';

                tabla += '<td class="' + claseAplicar + '" align="center"><a onclick="validaActividadEconomicaBusqueda( \'' + codigo + '\',\'' + nombre + '\')" class="listado"  href="javascript:void(0)" ><img width="15px" src="../../Recursos/imagenes/administracion/validar.png" title="Buscar"alt="Buscar" class="imgAdminPequenia"" />SELECCIONAR</td>';



            }
            tabla += '</table>';
            divTerceros.innerHTML = tabla;
            divTerceros.innerHTML += pieDePaginaListar(info, 'buscaActividadEconomica');

            //autoincrementador
            //document.getElementById("lblcodigo").innerHTML = ++codigo;
            var idMenuForm = document.getElementById('idMenuForm').innerHTML; // se adiciona esta linea para que se de  el permiso de visualizar el editar despues de cargar
            permisosParaMenu(idMenuForm); // se adiciona esta linea para que se el permiso de visualizar el editar despues de cargar
        } else {
            divTerceros.innerHTML = mensajecero;
        }
    } catch (elError) {
    }

    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'href': '#divListadoActividadesEconomicas'
    });


    document.getElementById('numDocuFil').value = '';
    document.getElementById('nomProFil').value = '';
    // document.getElementById('especialiadadFil').value = '';


}

function abreFancyActividades() {
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'href': '#divListadoActividades'
    });
}


function cerrarVentana() {
    $.fancybox.close();
}

/*INICIO METODOS PARA CARGAR INFORMACION DE LA EMPRESA*/
function cargaInfoGeneralEmpresa() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargarInfoEmp'));
    arrayParameters.push(newArg('reg', '0'));
    var send = arrayParameters.join('&');

    muestraVentanaProgreso();
    $.post('../../Controlador/ctlEmpresa.aspx', send, cargaInfoGeneralEmpresa_processResponse);
}

var tipoContGlobal = '-1', tipoSocGlobal = '-1';
function cargaInfoGeneralEmpresa_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -2:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                document.getElementById('txtNombreEmpresa').value = info.data[1]
                var digito = info.data[0].split('-')
                var dig = digito[1];
                document.getElementById('txtNit').value = digito[0]
                document.getElementById('numeroVerificacion').innerHTML = dig;
                tipoContGlobal = info.data[3];
                if (info.data[2] == 1) {
                    document.getElementById('jur').checked = true
                    habilitarJuridica();
                    document.getElementById('txtRazonSocial').value = info.data[4];
                    tipoSocGlobal = info.data[5];
                } else {
                    document.getElementById('nat').checked = true
                    habilitarNatural();
                    document.getElementById('txtPrimerApellidoPersonaNatural').value = info.data[4];
                    document.getElementById('txtSegundoApellidoPersonaNatural').value = info.data[5];
                    document.getElementById('txtPrimerNombrePersonaNatural').value = info.data[6];
                    document.getElementById('txtSegundoNombrePersonaNatural').value = info.data[7];
                }
                break;
        }
        cargarTipoContribuyente();
        cargarTipoSociedad();
        // cargaInfoEconomicaEmpresa();
        // cargaInfoAdministrativaEmpresa();
    } catch (elError) {
    }
}

function cargaInfoEconomicaEmpresa() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargarInfoEcoEmp'));
    arrayParameters.push(newArg('reg', '0'));
    var send = arrayParameters.join('&');

    muestraVentanaProgreso();
    $.post('../../Controlador/ctlEmpresa.aspx', send, cargaInfoEconomicaEmpresa_processResponse);
}

var divGlobal = '';
var gruGlobal = '';
var claGlobal = '';
function cargaInfoEconomicaEmpresa_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + res + ')');
        var msj = info.msj;

        switch (msj) {
            case -2:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                var a = info.data[3].toString();
                divGlobal = info.data[0].toString();
                gruGlobal = info.data[1].toString();
                claGlobal = info.data[2].toString();

                var b = info.data[16];
                var c = info.data[18];
                var d = info.data[20];
                var e = info.data[22];
                document.getElementById('txtActividadPrincipal').value = info.data[16];
                document.getElementById('txtActividadSecundaria').value = info.data[18];
                document.getElementById('txtOtraActividad1').value = info.data[20];
                document.getElementById('txtOtraActividad2').value = info.data[22];

                document.getElementById('lblActividadPrincipal').innerHTML = info.data[30];
                document.getElementById('lblActividadSecundaria').innerHTML = info.data[31];
                document.getElementById('lblOtraActividad1').innerHTML = info.data[32];
                document.getElementById('lblOtraActividad2').innerHTML = info.data[33];


                if (b == '1020' || b == '1040' || b == '1420' || b == '1430' || b == '3520' || b == '3600' || b == '3900' || b == '4541' || b == '4620' || b == '4632' || b == '4645' || b == '4649' || b == '4661' || b == '4663' || b == '4711' || b == '4719' || b == '4724' || b == '4752' || b == '4761' || b == '4773' || b == '4781' || b == '4791' || b == '4792' || b == '4799' || b == '5811' || b == '6020' || b == '6499' || b == '6611' || b == '6910' || b == '6920' || b == '7010' || b == '7020' || b == '7110' || b == '7120' || b == '7220' || b == '7320' || b == '7410' || b == '7490' || b == '8523' || b == '8551') {
                    document.getElementById('txtCodActividad').value = info.data[17];
                    document.getElementById('CodAuxActividadPrincipal').style.display = 'block';
                    document.getElementById('txtCodActividad').style.display = 'block';
                }
                else {
                    document.getElementById('CodAuxActividadPrincipal').style.display = 'none';
                    document.getElementById('txtCodActividad').style.display = 'none';
                }
                if (c == '1020' || c == '1040' || c == '1420' || c == '1430' || c == '3520' || c == '3600' || c == '3900' || c == '4541' || c == '4620' || c == '4632' || c == '4645' || c == '4649' || c == '4661' || c == '4663' || c == '4711' || c == '4719' || c == '4724' || c == '4752' || c == '4761' || c == '4773' || c == '4781' || c == '4791' || c == '4792' || c == '4799' || c == '5811' || c == '6020' || c == '6499' || c == '6611' || c == '6910' || c == '6920' || c == '7010' || c == '7020' || c == '7110' || c == '7120' || c == '7220' || c == '7320' || c == '7410' || c == '7490' || c == '8523' || b == '8551') {
                    document.getElementById('txtCodActividadSecundaria').value = info.data[19];
                    document.getElementById('CodAuxActividadSecundaria').style.display = 'block';
                    document.getElementById('txtCodActividadSecundaria').style.display = 'block';
                }
                else {
                    document.getElementById('CodAuxActividadSecundaria').style.display = 'none';
                    document.getElementById('txtCodActividadSecundaria').style.display = 'none';
                }
                if (d == '1020' || d == '1040' || d == '1420' || d == '1430' || d == '3520' || d == '3600' || d == '3900' || d == '4541' || d == '4620' || d == '4632' || d == '4645' || d == '4649' || d == '4661' || d == '4663' || d == '4711' || d == '4719' || d == '4724' || d == '4752' || d == '4761' || d == '4773' || d == '4781' || d == '4791' || d == '4792' || d == '4799' || d == '5811' || d == '6020' || d == '6499' || d == '6611' || d == '6910' || d == '6920' || d == '7010' || d == '7020' || d == '7110' || d == '7120' || d == '7220' || d == '7320' || d == '7410' || d == '7490' || d == '8523' || d == '8551') {
                    document.getElementById('txtCodOtraActividad1').value = info.data[21];
                    document.getElementById('CodAuxOtraActividad1').style.display = 'block';
                    document.getElementById('txtCodOtraActividad1').style.display = 'block';
                }
                else {
                    document.getElementById('CodAuxOtraActividad1').style.display = 'none';
                    document.getElementById('txtCodOtraActividad1').style.display = 'none';
                }
                if (e == '1020' || e == '1040' || e == '1420' || e == '1430' || e == '3520' || e == '3600' || e == '3900' || e == '4541' || e == '4620' || e == '4632' || e == '4645' || b == '4649' || e == '4661' || b == '4663' || e == '4711' || e == '4719' || e == '4724' || e == '4752' || e == '4761' || e == '4773' || e == '4781' || e == '4791' || e == '4792' || e == '4799' || e == '5811' || e == '6020' || e == '6499' || e == '6611' || e == '6910' || e == '6920' || e == '7010' || e == '7020' || e == '7110' || e == '7120' || e == '7220' || e == '7320' || e == '7410' || e == '7490' || e == '8523' || e == '8551') {
                    document.getElementById('txtCodOtraActividad2').value = info.data[23];
                    document.getElementById('CodAuxOtraActividad2').style.display = 'block';
                    document.getElementById('txtCodOtraActividad2').style.display = 'block';
                }
                else {
                    document.getElementById('CodAuxOtraActividad2').style.display = 'none';
                    document.getElementById('txtCodOtraActividad2').style.display = 'none';
                }

                document.getElementById('txtNumSedes').value = info.data[6];
                document.getElementById('txtValor').value = info.data[7].toString().replace(',', '.');
                document.getElementById('txtActivos').value = info.data[8];
                document.getElementById('txtNumEmpleados').value = info.data[9];
                switch (info.data[10]) {
                    case '1':
                        document.getElementById('tamano').innerHTML = 'MICRO'
                        break;

                    case '2':
                        document.getElementById('tamano').innerHTML = 'PEQUEÑA'
                        break;

                    case '3':
                        document.getElementById('tamano').innerHTML = 'MEDIANA'
                        break;

                    case '4':
                        document.getElementById('tamano').innerHTML = 'GRANDE'
                        break;

                    default:
                        document.getElementById('tamano').innerHTML = 'MICRO'
                        break;
                }
                actividadEconomica = a;

                //                listarCodigosCIIU('-1', 'selDivision');
                //                setTimeout("listarCodigosCIIU(document.getElementById('selDivision'), 'selGrupo')", 1000);
                //                setTimeout("listarCodigosCIIU(document.getElementById('selGrupo'), 'selClase')", 1500);
                //                setTimeout("listarCodigosCIIU(document.getElementById('selClase'), 'divCodigosCIIU')", 2000);

                calculaTamanoEmpresa();
        }
    } catch (elError) {
    }
}

function cargaInfoAdministrativaEmpresa() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargarInfoAdmEmp'));
    arrayParameters.push(newArg('reg', '0'));
    var send = arrayParameters.join('&');

    muestraVentanaProgreso();
    $.post('../../Controlador/ctlEmpresa.aspx', send, cargaInfoAdministrativaEmpresa_processResponse);
}

var tipoDocRepGlobal = '0', tipoDocContGlobal = '0', tipoDocRevFiscal = '0';


function cargaInfoAdministrativaEmpresa_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -2:
                muestraVentana(mensajemenosuno);
                break;
            case 0:

                document.getElementById('selectTipoDocumentoRepresentanteLegal').value = '-1';
                document.getElementById('selectTipoDocumentoContador').value = '-1';
                document.getElementById('selectTipoDocumentoRevisorFiscal').value = '-1';
                break;
            case 1:

                document.getElementById('txtRepresentanteLegal').value = info.data[0];
                document.getElementById('selectTipoDocumentoRepresentanteLegal').value = info.data[1];
                document.getElementById('txtNumeroDocumentoRepresentante').value = info.data[2];
                document.getElementById('txtContador').value = info.data[3];
                document.getElementById('txtTPContador').value = info.data[6];
                document.getElementById('selectTipoDocumentoContador').value = info.data[4];
                document.getElementById('txtDocumentoIdContador').value = info.data[5];
                document.getElementById('txtRevisorFiscal').value = info.data[7];
                document.getElementById('txtTPRevisorFiscal').value = info.data[10];
                document.getElementById('selectTipoDocumentoRevisorFiscal').value = info.data[8];
                document.getElementById('txtDocumentoIdRevisor').value = info.data[9];
                break;
        }

    } catch (elError) {
    }
}

function cargaInfoFinancieraEmpresa() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargarInfoFinEmp'));
    arrayParameters.push(newArg('reg', '0'));
    var send = arrayParameters.join('&');

    muestraVentanaProgreso();
    $.post('../../Controlador/ctlEmpresa.aspx', send, cargaInfoFinancieraEmpresa_processResponse);
}

function cargaInfoFinancieraEmpresa_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -2:
                muestraVentana(mensajeErrorConexion);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                document.getElementById('txtInicioFacturacion').value = info.data[0]
                document.getElementById('txtFinFacturacion').value = info.data[1]
                document.getElementById('txtResolucionFacturacion').value = info.data[2];
                document.getElementById('txtVencimientoResolucion').value = info.data[3];
                document.getElementById('txtInicioFacturacion2').value = info.data[4]
                document.getElementById('txtFinFacturacion2').value = info.data[5]
                document.getElementById('txtResolucionFacturacion2').value = info.data[6];
                document.getElementById('txtVencimientoResolucion2').value = info.data[7];
                break;
        }
    } catch (elError) {
    }
}

function cargaInfoSedePpalEmpresa() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargarInfoSedPpalEmp'));
    arrayParameters.push(newArg('reg', '0'));
    var send = arrayParameters.join('&');

    muestraVentanaProgreso();
    $.post('../../Controlador/ctlEmpresa.aspx', send, cargaInfoSedePpalEmpresa_processResponse);
}

var ubicacionGlobal = '1';
function cargaInfoSedePpalEmpresa_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -2:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                document.getElementById('txtNombreSedePrincipal').value = info.data[0];
                if (info.data[1] != "") {
                    paisGlobal = info.data[1];
                }
                if (info.data[2] != "") {
                    deptoGlobal = info.data[2];
                }
                if (info.data[3] != "") {
                    municipioGlobal = info.data[3];
                }


                document.getElementById('txtDirSedePrincipal').value = info.data[4];
                document.getElementById('txtTelSedePrincipal').value = info.data[5];
                document.getElementById('txtMailSedePrincipal').value = info.data[6];
                if (info.data[7] != "") {
                    document.getElementById('selUbicacionSedePrincipal').value = info.data[7];
                    ubicacionGlobal = info.data[7];
                }
                document.getElementById('txtNumEmpleSedePrincipal').value = info.data[8];
                break;
        }
        cargaPais();
        cargaUbicacion();
    } catch (elError) {
    }
}

function cargaInfoContactoEmpresa() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargarInfoContEmp'));
    arrayParameters.push(newArg('reg', '0'));
    var send = arrayParameters.join('&');

    muestraVentanaProgreso();
    $.post('../../Controlador/ctlEmpresa.aspx', send, cargaInfoContactoEmpresa_processResponse);
}

var cargoGlobal = '-1';
function cargaInfoContactoEmpresa_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -2:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                document.getElementById('txtContacto').value = info.data[0];
                if (info.data[1] != "") {
                    cargoGlobal = info.data[1];
                }
                document.getElementById('txtTelContacto').value = info.data[2];
                document.getElementById('txtEmailContacto').value = info.data[3];
                break;
        }
        cargaCargos();
    } catch (elError) {
    }
}

function cargaInfoAdministradorEmpresa() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargarInfoAdmorEmp'));
    arrayParameters.push(newArg('reg', '0'));
    var send = arrayParameters.join('&');

    muestraVentanaProgreso();
    $.post('../../Controlador/ctlEmpresa.aspx', send, cargaInfoAdministradorEmpresa_processResponse);
}

function cargaInfoAdministradorEmpresa_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -2:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                document.getElementById('txtUsuarioEmpresa').value = info.data[0];
                document.getElementById('txtNombreAdminEmpresa').value = info.data[1];
                document.getElementById('txtEmailAdminEmpresa').value = info.data[2];
                document.getElementById('txtUsuarioEmpresa').disabled = "disabled";
                document.getElementById('txtNombreAdminEmpresa').disabled = "disabled";
                document.getElementById('txtEmailAdminEmpresa').disabled = "disabled";
                break;
        }
    } catch (elError) {
    }
}
/*******************************************
CARGA EL LOGO DE LA EMPRESA
*******************************************/
function cargaInfoLogoEmpresa() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargarInfoLogEmp'));
    arrayParameters.push(newArg('reg', '0'));
    var send = arrayParameters.join('&');

    muestraVentanaProgreso();
    $.post('../../Controlador/ctlEmpresa.aspx', send, cargaInfoLogoEmpresa_processResponse);
}

function cargaInfoLogoEmpresa_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -2:
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                break;
        }
    } catch (elError) {
    }
}

function ocultarPestanas(objeto) {
    document.getElementById(objeto).style.display = 'block';
    $("#" + objeto).trigger('click');
}
