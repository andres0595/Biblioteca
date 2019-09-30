//arreglo para generar la clave del usuario Administrador
var caracter = new Array("0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z");
var Nit_Global = '';
var digiverificacion_Global = '';

$(document).ready(function () {
    muestraVentana('REGISTRE SU EMPRESA');
    habilitarJuridica();
     
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

    cargarTipoContribuyente();
    cargarTipoSociedad();
    espacioBlanco();
});

function espacioBlanco() {
    var nit = document.getElementById('txtNit').value
    nit = $.trim(nit);

}

////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////  VARIABLES GLOBALES /////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////
var tipoContGlobal = '-1', tipoSocGlobal = '-1';
var paisGlobal = '-1', deptoGlobal = '-1', municipioGlobal = '-1';
var cargoGlobal = '-1', ubicacionGlobal = '-1';
var divGlobal = '-1', gruGlobal = '-1', claGlobal = '-1', factGlobal = -1, pagGlobal = 1;
var tipoDocRep = '1', tipoDocCont = '1', tipoDocRevFiscal = '1';

function cambiaTipoCont(obj) {
    tipoContGlobal = obj.value;
}

function cambiaTipoSoc(obj) {
    tipoSocGlobal = obj.value;
}

function cambiaDivision(obj) {
    divGlobal = obj.value;
}

function cambiaGrupo(obj) {
    gruGlobal = obj.value;
}

function cambiaClase(obj) {
    claGlobal = obj.value;
}

function cambiaDocRep(obj) {
    tipoDocRep = obj.value;
}

function cambiaDocCont(obj) {
    tipoDocCont = obj.value;
}

function cambiaDocRevF(obj) {
    tipoDocRevFiscal = obj.value;
}

function cambiaUbicacion(obj) {
    ubicacionGlobalc = obj.value;
}

function cambiaCargo(obj) {
    cargoGlobal = obj.value;
}

function cambiaPais(obj) {
    paisGlobal = obj.value;
}

function cambiaDepto(obj) {
    deptoGlobal = obj.value;
}

function cambiaMunic(obj) {
    municipioGlobal = obj.value;
}


function muestraVentanaFancybox(idDIv) {
    document.getElementById('txtInicioFacturacion').value = "";
    document.getElementById('txtFinFacturacion').value = "";
    document.getElementById('txtResolucionFacturacion').value = "";
    document.getElementById('txtVencimientoResolucion').value = "";
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


////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////  FORMULARIO DE REGISTRO DE TERCEROS /////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////

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
                document.getElementById('selectTipoDocumentoRepresentanteLegal').value = tipoDocRep;
                document.getElementById('selectTipoDocumentoContador').value = tipoDocCont;
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
                document.getElementById('selPaisSedePrincipal').value = paisGlobal;
                break;
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
                document.getElementById('selDeptoSedePrincipal').value = deptoGlobal;
                break;
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
                document.getElementById('selMuniSedePrincipal').value = municipioGlobal;
                break;
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

    document.getElementById('selTipoSociedad').value = 1;
}

var objGlobal = ""

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
    tamanioEmpresa = numTam;
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

    digiverificacion_Global = digVeri;

    var tipoPer = tipoPersona
    var contribuyente = document.getElementById('selectTipoContribuyente').value
    var priApe = document.getElementById('txtPrimerApellidoPersonaNatural').value
    var segApe = document.getElementById('txtSegundoApellidoPersonaNatural').value
    var priNom = document.getElementById('txtPrimerNombrePersonaNatural').value
    var segNom = document.getElementById('txtSegundoNombrePersonaNatural').value
    var razonSocial = document.getElementById('txtRazonSocial').value
    var sociedad = document.getElementById('selTipoSociedad').value
    //    var plan_contable = document.getElementById('selPlanContable').value
    //    var plan_unico_cuentas = document.getElementById('selPlanUnicoCuentas').value
    var digi1 = document.getElementById('txtUltimoNit').value
    var digi2 = document.getElementById('txtUltimosdosNit').value
    var uno = nit.charAt(9);
    var dos = nit.charAt(8) + nit.charAt(9);


    var uno9 = nit.charAt(8);
    var dos9 = nit.charAt(7) + nit.charAt(8);

    var uno8 = nit.charAt(7);
    var dos8 = nit.charAt(6) + nit.charAt(7);
    espacioBlanco();
    nit = $.trim(nit);
    Nit_Global = nit;
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'guardarInfoEmp'));
    arrayParameters.push(newArg('nit', nit));
    arrayParameters.push(newArg('digito', digVeri));
    arrayParameters.push(newArg('nombre', nombre));
    arrayParameters.push(newArg('tipoPersona', tipoPer));
    arrayParameters.push(newArg('tipoContribuyente', contribuyente));
    arrayParameters.push(newArg('razonSocial', razonSocial));
    arrayParameters.push(newArg('tipoSociedad', sociedad));
    //   arrayParameters.push(newArg('plan_contable', plan_contable));
    //   arrayParameters.push(newArg('plan_unico_cuentas', plan_unico_cuentas));
    arrayParameters.push(newArg('priApellido', priApe));
    arrayParameters.push(newArg('segApellido', segApe));
    arrayParameters.push(newArg('priNombre', priNom));
    arrayParameters.push(newArg('segNombre', segNom));
    arrayParameters.push(newArg('digi1', digi1));
    arrayParameters.push(newArg('digi2', digi2));
    arrayParameters.push(newArg('reg', 1));
    var send = arrayParameters.join('&');



    //muestraVentanaProgreso();
    //$.post('../../Controlador/ctlEmpresa.aspx', send, guardaInfoGeneralEmpresa_processResponse);

    if (nit.length == 10 && digi1 == uno && digi2 == dos) {
        //var uno = nit.charAt(9);
        //var dos = nit.charAt(8);
        //    dos = nit.charAt(9); // letra = H

        //uno == digi1;
        //dos == digi2;
        muestraVentanaProgreso();
        $.post('../../Controlador/ctlEmpresa.aspx', send, guardaInfoGeneralEmpresa_processResponse);


    } else if (nit.length == 9 && digi1 == uno9 && digi2 == dos9) {
        //var uno9 = nit.charAt(8);
        //var dos9 = nit.charAt(7);
        //    dos9 = nit.charAt(8); // letra = H

        //uno9 == digi1;
        //dos9 == digi2;

        muestraVentanaProgreso();
        $.post('../../Controlador/ctlEmpresa.aspx', send, guardaInfoGeneralEmpresa_processResponse);

    } else if (nit.length == 8 && digi1 == uno8 && digi2 == dos8) {
        //var uno9 = nit.charAt(8);
        //var dos9 = nit.charAt(7);
        //    dos9 = nit.charAt(8); // letra = H

        //uno9 == digi1;
        //dos9 == digi2;

        muestraVentanaProgreso();
        $.post('../../Controlador/ctlEmpresa.aspx', send, guardaInfoGeneralEmpresa_processResponse);

    } else if (nit.length > 10) {
        //var uno9 = nit.charAt(8);
        //var dos9 = nit.charAt(7) && it.charAt(8); // letra = H

        //uno9 == digi1;
        //dos9 == digi2;

        muestraVentana('muchos números');
    } else {
        muestraVentana('No coinciden los dígitos con el NIT');
    }
}

function guardaInfoGeneralEmpresa_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -2:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajeErrorGuarda);
                break;
            case 1:
                muestraVentana(mensajeGuarda + '\n');
                setTimeout("ocultarPestanas('pest_economica')", 2200);
                break;
            case 2:
                muestraVentana(mensajeEdita);
                setTimeout("ocultarPestanas('pest_economica')", 2200);
                break;
            case 3:
                muestraVentana("EMPRESA YA EXISTE !!");
                break;
        }
        //listarCodigosCIIU('-1', 'selDivision');
    } catch (elError) {
    }
}

function guardaInfoEconomicaEmpresa() {

    var IdGlobal = '-1';
    var empresaNit = Nit_Global + '-' + digiverificacion_Global;

    var actividadPrincipal = document.getElementById('txtActividadPrincipal').value;
    var actividadSecundaria = document.getElementById('txtActividadSecundaria').value;
    var otraActividad1 = document.getElementById('txtOtraActividad1').value;
    var otraActividad2 = document.getElementById('txtOtraActividad2').value;


    var shd_actividadPrincipal = document.getElementById('txtCodActividad').value;//1
    var shd_actividadSecundaria = document.getElementById('txtCodActividadSecundaria').value;//2
    var shd_otraActividad1 = document.getElementById('txtCodOtraActividad1').value;//3
    var shd_otraActividad2 = document.getElementById('txtCodOtraActividad2').value;//4


    // var codigoHacienda = document.getElementById('txtCodigoHacienda').value;
    var numSedes = document.getElementById('txtNumSedes').value
    var valor = document.getElementById('txtValor').value
    if (valor == '') {
        valor = 0;
    }

    var activos = document.getElementById('txtActivos').value
    var numEmpleados = document.getElementById('txtNumEmpleados').value
    var tamanio = tamanioEmpresa;

    if (tamanio === undefined) {
        tamanio = 1;
    }

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'guardarInfoEcoEmp'));

    arrayParameters.push(newArg('empresaNit ', empresaNit));


    arrayParameters.push(newArg('actividadPrincipal', actividadPrincipal));
    arrayParameters.push(newArg('actividadSecundaria', actividadSecundaria));
    arrayParameters.push(newArg('otraActividad1', otraActividad1));
    arrayParameters.push(newArg('otraActividad2', otraActividad2));


    arrayParameters.push(newArg('shd_actividadPrincipal', shd_actividadPrincipal));
    arrayParameters.push(newArg('shd_actividadSecundaria', shd_actividadSecundaria));
    arrayParameters.push(newArg('shd_otraActividad1', shd_otraActividad1));
    arrayParameters.push(newArg('shd_otraActividad2', shd_otraActividad2));



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
                muestraVentana(mensajeErrorGuarda);
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

    var nit = Nit_Global + '-' + digiverificacion_Global;

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
    arrayParameters.push(newArg('p', 'guardarInfoAdmEmp'));
    arrayParameters.push(newArg('nit', nit));
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
    arrayParameters.push(newArg('reg', '1'));
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
            case -2:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajeErrorGuarda);
                break;
            case 1:
                muestraVentana(mensajeGuarda);
                setTimeout("ocultarPestanas('pest_financiera')", 2200);
                listarInformacionFactura(1);
                break;
            case 2:
                muestraVentana(mensajeEdita);
                setTimeout("ocultarPestanas('pest_financiera')", 2200);
                listarInformacionFactura(1);
                break;
        }

    //    cargaTipDocumentoTercero();
    } catch (elError) {
    }
}

function guardaInfoFinancieraEmpresa() {
    var fi = document.getElementById('txtInicioFacturacion').value
    var ff = document.getElementById('txtFinFacturacion').value
    var resol = document.getElementById('txtResolucionFacturacion').value
    var vence = document.getElementById('txtVencimientoResolucion').value

    var idGlobal = -1;

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'guardarInfoFinEmp'));

    arrayParameters.push(newArg('idGlobal', idGlobal));
    arrayParameters.push(newArg('nit', Nit_Global + '-' + digiverificacion_Global));

    arrayParameters.push(newArg('fechaInicioFact', fi));
    arrayParameters.push(newArg('fechaFinFact', ff));
    arrayParameters.push(newArg('resolucion', resol));
    arrayParameters.push(newArg('vencimientoRes', vence));

    arrayParameters.push(newArg('reg', '1'));
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
                muestraVentana(mensajeErrorGuarda);
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
    cargaUbicacion();
    setTimeout("ocultarPestanas('pest_sedePrincipal')", 2200);


    document.getElementById('txtInicioFacturacion').value = '';
    document.getElementById('txtFinFacturacion').value = '';
    document.getElementById('txtResolucionFacturacion').value = '';
    document.getElementById('txtVencimientoResolucion').value = '';


}



/*********************************************************************************************************************************
LISTA LA INFORMACIÓN EXISTENTE
******************************************************************************************************************************** */
function listarInformacionFactura(pag) {
    cerrarVentanaEmergente();
    pagGlobal = pag;
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listaFacturacion'));
    arrayParameters.push(newArg('pag', pagGlobal));
    arrayParameters.push(newArg('empresa', Nit_Global + '-' + digiverificacion_Global));
    arrayParameters.push(newArg('reg', '1'));
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

            tabla = "<table class='tbListado centrar' style='text-align: center;'><tr><td class='encabezado' colspan='4'>FACTURACI&Oacute;N</td></tr>";
            tabla += "<tr><td class='encabezado'>INICIO FACT.</td><td class='encabezado'>FIN FACT.</td><td class='encabezado'>RESOLUCI&Oacute;N</td><td class='encabezado'>VER DETALLE</td></tr>";

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

function verDetalle(fechaIni, fechaFin, resolucion, fechaVencRes) {
    //    var tabla = "<table><tr><td>Fecha Inicio</td><td>" + fechaIni + "</td></tr><tr><td>Fecha Fin</td><td>" + fechaFin + "</td></tr><tr><td>Resoluci&oacute;n</td><td>" + resolucion + "</td><tr><td>Fecha Vencimineto Resoluci&oacute;n</td><td>" + fechaVencRes + "</td></tr></tr></table>";
    //    $("#divDetalle").html(tabla);
    //    muestraVentanaFancybox("divFormularioDetalle");
    var tabla = "<table class='tbListado centrar' style='text-align: center;'><tr><td class='encabezado' colspan='6'>FACTURACI&Oacute;N</td></tr>";
    tabla += "<tr><td class='encabezado'>INICIO FACTURACI&Oacute;N</td><td class='encabezado'>FIN FACTURACI&Oacute;N</td><td class='encabezado'>RESOLUCI&Oacute;N</td><td class='encabezado'>FECHA VENCIMIENTO</td></tr>";

    tabla += "<tr><td class='cuerpoListado6'>" + fechaIni + "</td><td class='cuerpoListado6'>" + fechaFin + "</td><td class='cuerpoListado6'>" + resolucion + "</td><td class='cuerpoListado6'>" + fechaVencRes + "</td></tr></tr></table>";
    $("#divDetalle").html(tabla);
    muestraVentanaFancybox("divFormularioDetalle");

    //    tabla = "<table class='tbListado centrar' style='text-align: center;'>";
    //    tabla += "<tr><td class='encabezado'>Fecha Inicio</td>";
    //    tabla += '<td class="cuerpoListado10">' + ((fechaIni == "") ? vacio : fechaIni) + '</td>';
    //    tabla += "</tr>";

    //    tabla += "<tr><td class='encabezado'>Fecha Fin</td>";
    //    tabla += '<td class="cuerpoListado10">' + ((fechaFin == "") ? vacio : fechaFin) + '</td>';
    //    tabla += "</tr>";

    //    tabla += "<tr><td class='encabezado'>Resoluci&oacute;n</td>";
    //    tabla += '<td class="cuerpoListado10">' + ((resolucion == "") ? vacio : resolucion) + '</td>';
    //    tabla += "</tr>";

    //    tabla += "<tr><td class='encabezado'>Fecha Vencimiento Resoluci&oacute;n</td>";
    //    tabla += '<td class="cuerpoListado10">' + ((fechaVencRes == "") ? vacio : fechaVencRes) + '</td>';
    //    tabla += "</tr>";
    //    
    //    tabla += '</table>';

    //    document.getElementById('divDetalle').innerHTML = tabla;
    //    muestraVentanaFancybox("divFormularioDetalle");
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
    arrayParameters.push(newArg('p', 'guardarInfoSedPpalEmp'));
    arrayParameters.push(newArg('nit', Nit_Global + '-' + digiverificacion_Global));
    arrayParameters.push(newArg('nombreSede', nomSede));
    arrayParameters.push(newArg('municipio', muniSede));
    arrayParameters.push(newArg('direccion', dirSede));
    arrayParameters.push(newArg('telefono', telSede));
    arrayParameters.push(newArg('mail', mailSede));
    arrayParameters.push(newArg('ubicacion', ubiSede));
    arrayParameters.push(newArg('numEmpleados', numEmpleSede));
    arrayParameters.push(newArg('reg', '1'));
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
            case -2:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajeErrorGuarda);
                break;
            case 1:
                muestraVentana(mensajeGuarda);
                setTimeout("ocultarPestanas('pest_contacto')", 2200);
                break;
            case 2:
                muestraVentana('INFORMACIÓN ALMACENADA CORRECTAMENTE');


                setTimeout("ocultarPestanas('pest_contacto')", 2200);
                break;
        }
        cargaCargos();
    } catch (elError) {
    }
}

function guardaInfoContactoEmpresa() {
    var nomContact = document.getElementById('txtContacto').value
    var cargoContact = document.getElementById('selCargoContacto').value
    var telContact = document.getElementById('txtTelContacto').value
    var mailContact = document.getElementById('txtEmailContacto').value
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'guardarInfoContEmp'));
    arrayParameters.push(newArg('nit', Nit_Global + '-' + digiverificacion_Global));
    arrayParameters.push(newArg('nombreContacto', nomContact));
    arrayParameters.push(newArg('cargoContacto', cargoContact));
    arrayParameters.push(newArg('telefono', telContact));
    arrayParameters.push(newArg('email', mailContact));
    arrayParameters.push(newArg('reg', '1'));
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
            case -2:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajeErrorGuarda);
                break;
            case 1:
                muestraVentana(mensajeGuarda);
                setTimeout("ocultarPestanas('pest_Administrador')", 2200);
                break;
            case 2:
                muestraVentana(mensajeEdita);
                setTimeout("ocultarPestanas('pest_Administrador')", 2200);
                break;
        }
    } catch (elError) {
    }
}

function guardaInfoAdministradorEmpresa() {
    var user = document.getElementById('txtUsuarioEmpresa').value
    var name = document.getElementById('txtNombreAdminEmpresa').value
    var mail = document.getElementById('txtEmailAdminEmpresa').value
    if (mail != '' && user != '') {
        if (vemail(mail)) {
            var clave = generarClave();
            var arrayParameters = new Array();
            arrayParameters.push(newArg('p', 'guardarInfoAdmorEmp'));
            arrayParameters.push(newArg('nit', Nit_Global + '-' + digiverificacion_Global));
            arrayParameters.push(newArg('usuario', user));
            arrayParameters.push(newArg('nombre', name));
            arrayParameters.push(newArg('clave', clave));
            arrayParameters.push(newArg('email', mail));
            arrayParameters.push(newArg('reg', '0'));
            var send = arrayParameters.join('&');

            muestraVentanaProgreso();
            $.post('../../Controlador/ctlEmpresa.aspx', send, guardaInfoAdministradorEmpresa_processResponse);
        } else {
            muestraVentana('El correo electr&oacute;nico no tiene el formato correcto!!!');
        }
    } else {
        muestraVentana(mensajeObligatorio);
    }
}

function guardaInfoAdministradorEmpresa_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -2:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajeErrorGuarda);
                break;
            case 1:
                muestraVentana('Informaci&oacute;n del Administrador registrada exitosamente!!\n\n Se ha enviado un correo al Usuario');
                setTimeout("ocultarPestanas('pest_logo')", 2200);
                break;
            case 2:
                muestraVentana(mensajeEdita);
                setTimeout("ocultarPestanas('pest_logo')", 2200);
                break;
        }
    } catch (elError) {
    }
}

function guardaLogoEmpresa() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'guardarInfoLogEmp'));
    arrayParameters.push(newArg('nit', Nit_Global + '-' + digiverificacion_Global));
    arrayParameters.push(newArg('reg', '1'));
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
            case -2:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajeErrorGuarda);
                break;
            case 1:
                muestraVentana(mensajeGuarda);
                break;
            case 3:
                muestraVentana("SELECIONE UNA IMAGEN");
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

function ocultarPestanas(objeto) {
    document.getElementById(objeto).style.display = 'block';
    $("#" + objeto).trigger('click');
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

    if (actividad != "") {
        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', 'validaActividadEconomica'));
        arrayParameters.push(newArg('actividad', actividad));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlGeneral.aspx', send, validaActividadEconomica_processResponse);
    } else {
        muestraVentana("INGRESE UNA ACTIVIDAD")
    }
}

function validaActividadEconomica_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana('SIN CONEXI&Oacute;N A LA BASE DE DATOS.');
                break;
            case 0:
                muestraVentana('NO SE ENCONTR&Oacute; INFORMACI&Oacute;N.');
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
                    if (globalActividadEconomica == 'ACTIVIDAD') {
                        var actvidad2 = document.getElementById('txtActividadSecundaria').value;
                        var actvidad3 = document.getElementById('txtOtraActividad1').value;
                        var actvidad4 = document.getElementById('txtOtraActividad2').value;
                    }
                    if (globalActividadEconomica == 'SECUNDARIA') {
                        var actvidad1 = document.getElementById('txtActividadPrincipal').value;                       
                        var actvidad3 = document.getElementById('txtOtraActividad1').value;
                        var actvidad4 = document.getElementById('txtOtraActividad2').value;
                    }
                    if (globalActividadEconomica == 'OTRA ACTIVIDAD 1') {
                        var actvidad1 = document.getElementById('txtActividadPrincipal').value;
                        var actvidad2 = document.getElementById('txtActividadSecundaria').value;                       
                        var actvidad4 = document.getElementById('txtOtraActividad2').value;
                    }
                    if (globalActividadEconomica == 'OTRA ACTIVIDAD 2') {
                        var actvidad1 = document.getElementById('txtActividadPrincipal').value;
                        var actvidad2 = document.getElementById('txtActividadSecundaria').value;
                        var actvidad3 = document.getElementById('txtOtraActividad1').value;                        
                    }
                   

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
            divTerceros.innerHTML = 'NO HAY INFORMACIÓN EN LA BASE DE DATOS';
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

    // var pag = 1;
    if (actividadEconomica === undefined) { }
    else if (actividadEconomica != "") {
        globalActividadEconomica = actividadEconomica;
    }


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
    $.post('../../Controlador/ctlPaginador.aspx', send, buscarTerapeutaPaginadoo_processResponse);
}

function buscaActividadEconomica_processResponse(res) {
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
                var rows = info.rows * 2;
                var ctl = true;
                var tabla = '<table><tr><td class="encabezado">C&Oacute;DIGO</td><td class="encabezado">NOMBRE</td></tr>';
                for (var i = 0; i < rows; i += 2) {
                    if (ctl) {
                        tabla += '<tr><td class="cuerpoListado11"><a onclick="validaActividadEconomicaBusqueda(\'' + info.data[i] + '\')" class="listado"  href="javascript:void(0)" >' + info.data[i] + '</td><td class="cuerpoListado11">' + info.data[i + 1] + '</td></tr>';
                    }
                    else {
                        tabla += '<tr><td class="cuerpoListado12"><a onclick="validaActividadEconomicaBusqueda(\'' + info.data[i] + '\')" class="listado"  href="javascript:void(0)" >' + info.data[i] + '</td><td class="cuerpoListado12">' + info.data[i + 1] + '</td></tr>';
                    }
                    ctl = !ctl;
                }
                tabla += '</table>';
                document.getElementById('listadoActividades').innerHTML = tabla;
                abreFancyActividades();
                break;
        }
    } catch (elError) {
    }
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
/*******************************************************************************************
CARGA IMPUESTOS
*******************************************************************************************/
function cargarImpuestos() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargaListaImpuestos'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, cargarImpuestos_processResponse);
}

function cargarImpuestos_processResponse(res) {
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
                llenarSelect(res, document.getElementById('selImpuestos'));
                break;
        }
    } catch (elError) { }
}
/*******************************************************************************************
CARGA PLAN CONTABLE (PARAMETRIZADOS EN ESCENARIO MATRIZ)
*******************************************************************************************/
function cargarMatriz() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargaListaMatriz'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, cargarMatriz_processResponse);
}

function cargarMatriz_processResponse(res) {
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
                llenarSelect(res, document.getElementById('selPlanContable'));
                break;
        }
    } catch (elError) { }
}

function buscarTerapeutaPaginado(pag) {



    ///var id_Servicio = document.getElementById("nombreServicio").value;
    //var codigoCodigoFil = document.getElementById('numDocuFil').value;
    var nombreCodigoFil = document.getElementById('numDocuFil').value;
    var especialiadadFil = '';

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listaAcividadesPaginado'));
    // arrayParameters.push(newArg('id_Servicio', id_Servicio));
    // arrayParameters.push(newArg('codigoCodigoFil', codigoCodigoFil));
    arrayParameters.push(newArg('codigo', nombreCodigoFil));
    arrayParameters.push(newArg('nombre', especialiadadFil));

    arrayParameters.push(newArg('pag', pag));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlPaginador.aspx', send, buscarTerapeutaPaginadoo_processResponse)



}

function buscarTerapeutaPaginadoo_processResponse(res) {
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
                id = datosRows[i];
                codigo = datosRows[i + 1];
                nombre = datosRows[i + 2];
                especialidad = datosRows[i + 3];
                correo = datosRows[i + 4];



                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar2 = "cuerpoListado3";
                } else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar2 = "cuerpoListado5";
                }

                ctl = !ctl;
                tabla += '<tr>';
                tabla += '<td class="' + claseAplicar + '" align="center">' + unescape(id).toUpperCase() + '</td>';
                tabla += '<td class="' + claseAplicar + '" align="center">' + unescape(codigo).toUpperCase() + '</td>';
                if (especialidad == -1) {
                    especialidad = 'NINGUNA';
                }

                // tabla += '<td class="' + claseAplicar + '" align="center">' + unescape(especialidad).toUpperCase() + '</td>';

                tabla += '<td class="' + claseAplicar + '" align="center"><a onclick="validaActividadEconomicaBusqueda( \'' + info.data[i] + '\')" class="listado"  href="javascript:void(0)" ><img width="15px" src="../../Recursos/imagenes/administracion/validar.png" title="Buscar"alt="Buscar" class="imgAdminPequenia"" />SELECCIONAR</td>';


                //tabla += '<td class="' + claseAplicar + '"><div id="linkDetalle" class="linkIconoLateral botonDetalle" onclick="detalleCodigo(\'' + id + '\',\'' + codigo + '\',\'' + nombre + '\',\'' + descr + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/listar.png"><p>Detalle</p></div></td>'; //copia los datos de la fila seleccionada 
                //tabla += '<td class="' + claseAplicar2 + '"><div id="linkEdita" class="linkIconoLateral botonEditar" onclick="editar(\'' + id + '\',\'' + codigo + '\',\'' + nombre + '\',\'' + descr + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/editar_24x24.png"><p>Editar</p></div></td>'; //copia los datos de la fila seleccionada 
                //tabla += '<td class="' + claseAplicar2 + '"><div id="linkEdita" class="linkIconoLateral botonEditar" onclick="eliminar( \'' + id + '\',\'' + nombre.toUpperCase() + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/eliminar_24x24.png"><p>Eliminar</p></div></td>'; //copia los datos de la fila seleccionada 

            }
            tabla += '</table>';
            divTerceros.innerHTML = tabla;
            divTerceros.innerHTML += pieDePaginaListar(info, 'buscaActividadEconomica');

            //autoincrementador
            //document.getElementById("lblcodigo").innerHTML = ++codigo;
            var idMenuForm = document.getElementById('idMenuForm').innerHTML; // se adiciona esta linea para que se de  el permiso de visualizar el editar despues de cargar
            permisosParaMenu(idMenuForm); // se adiciona esta linea para que se el permiso de visualizar el editar despues de cargar
        } else {
            divTerceros.innerHTML = 'AUN NO EXISTE INFORMACIÓN';
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

function cargarInfoEmpresa() {

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargarInfoEmpresa'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, cargarInfoEmpresa_processResponse)

}

function cargarInfoEmpresa_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('listadoActividadesEconomicas');
        if (res != '0') {


            for (var i = 0; i < datosRows.length; i += l) {
                id = datosRows[i];
                codigo = datosRows[i + 1];
                nombre = datosRows[i + 2];
                especialidad = datosRows[i + 3];
                correo = datosRows[i + 4];


            }

        } else {
            muestraVentana(mensajecero);
        }
    } catch (elError) {
    }
}