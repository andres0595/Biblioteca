//VARIABLES GLOBALES AUTOCOMPLETAR
var vectorIdProveedor = new Array();
var vectorProveedor = new Array();
var vectorIdContacto = new Array();
var vectorContacto = new Array();
var stockArray = new Array();
var globalContacto = "";
var globalCliente = "";

var vectorPaises = Array();
var vectorIdPaises = Array();
var vectorCiudad = Array();
var vectorIdCiudad = Array();

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////// VARIABLES GLOBALES ////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

var caracter = new Array("0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z");
var banderaUsu = "0"; // saber si ya acepto terminos 
var usuarioGlobal = "-1";

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////// FUNCIONES AUTOMATICAS /////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

$(document).ready(function () {
    $('#usuario').focus();



    $(function () {
        $("#fechaNacimiento").datepicker({
            changeMonth: true,
            changeYear: true,
            //            numberOfMonths: 1,
            //            maxDate: "+2Y",
            yearRange: '1940:2030'
        });
    });

    //cargarGenero();
    //cargarRH();
    //cargarInfo(1);
    //cargarInfo(2);
    //cargarInfo(3);
    //cargarInfo(4);
    //cargarInfo(5);
});

function recuperaClave() {

    document.getElementById('email').value = '';
    $.fancybox({
        'transitionIn': 'none',
        'transitionOut': 'none',
        'modal': true,
        'href': '#divOlvideClave'
    });
}

function cerrarFancy() {
    $.fancybox.close();
}


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////LOGIN USUARIO///////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

/*********************************************************
METODO PARA INGRESAR AL SISTEMA
***********************************************************/

function ingresar() {
    var usu = document.getElementById('usuario').value;
    var cla = md5(document.getElementById('clave').value);

    if ((usu != '') && (cla != '')) {
        muestraVentanaProgreso('Identificando...');
        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', 'loguear'));
        // arrayParameters.push(newArg('p', 'loguear_Regional'));
        arrayParameters.push(newArg('usuario', usu));
        arrayParameters.push(newArg('clave', cla));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlLogin.aspx', send, Ingresar_processResponse);
    } else {
        muestraVentana('debe ingresar datos');
    }
}

var estado = "";
var regionales = "";
function Ingresar_processResponse(res) {
    try {
        var info = eval("(" + res + ")");
        var msj = info.msj;
        ocultaVentanaProgreso();

        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana('Datos Invalidos.');
                document.getElementById('clave').value = '';
                break;
            case 1:
                estado = info.data[4];
                regionales = info.data[14];
                if (estado != 3 && estado != 4) {// ESTADOS DE INACTIVO O ELIMINADO
                    var arrayParameters = new Array();
                    arrayParameters.push(newArg('p', 'sesion'));
                    arrayParameters.push(newArg('usu', info.data[0]));
                    arrayParameters.push(newArg('nom', info.data[1]));
                    arrayParameters.push(newArg('mail', info.data[2]));
                    arrayParameters.push(newArg('nit', info.data[3]));
                    arrayParameters.push(newArg('key', info.data[5]));

                    arrayParameters.push(newArg('documento', info.data[6]));
                    arrayParameters.push(newArg('telefono', info.data[7]));
                    arrayParameters.push(newArg('area', info.data[8]));
                    arrayParameters.push(newArg('cargo', info.data[9]));
                    arrayParameters.push(newArg('foto', info.data[11]));
                    arrayParameters.push(newArg('rol', info.data[10]));
                    arrayParameters.push(newArg('nombreEmpresa', info.data[12]));
                    arrayParameters.push(newArg('departamento', info.data[13]));

                    if (info.data[3] == 'NIT') {
                        estado = '-10'; //Es Director
                    }

                    var send = arrayParameters.join('&');
                    $.post('../../Controlador/ctlLogin.aspx', send, crearSesion_processResponse);


                } else {
                    muestraVentana("Su cuenta se encuentra deshabilitada! <br> Comuniquese con el administrador");
                }
                break;
        }
    } catch (elError) {
    }
}

/*************************************************************
METODO QUE REDIRECCIONA DESPUES DE HABERSE CREADO LA SESION
**************************************************************/
//function crearSesion_processResponse(res) {
//    try {
//        switch (estado) {
//            case '-2':
//                capturarIps();
//                location.href = "../general/cambio_clave.aspx";
//                break;
//            case '2':
//                capturarIps();
//                if (regionales == "7") {
//                    location.href = "../general/inicio.aspx";
//                }else if (regionales == "0") {
//                    location.href = "../general/inicio.aspx";
//                } else  {
//                   location.href = "../general/inicioV2.aspx";
//               }
//              
//                
//                break;
//            case '-10':
//                capturarIps();
//                location.href = "../general/elegirEmpresa.aspx";
//                break;
//        }
//    } catch (elError) {
//    }
//}



function crearSesion_processResponse(res) {
    try {
        switch (estado) {
            case '-2':
                capturarIps();
                location.href = "../general/cambio_clave.aspx";
                break;
            case '2':
                capturarIps();
                location.href = "../general/inicio.aspx";
                break;
            case '-10':
                capturarIps();
                location.href = "../general/elegirEmpresa.aspx";
                break;
        }
    } catch (elError) {
    }
}


function capturarIps() {

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'capturarIps'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send);

}


/**********************************************************
METODO AUTOMATICO QUE DETECTA NAVEGADOR
***********************************************************/
BrowserDetect.init();
$(document).ready(function () { muestraNavegadores(BrowserDetect.browser, BrowserDetect.version); });


/**********************************************************
METODO QUE MUESTRA MENSAJES SEGUN LA VERSION DEL NAVEGADOR
***********************************************************/

function muestraNavegadores(navegador, version) {
    var ctl = true;
    switch (navegador) {
        case 'Chrome':
            if (version < 10) {
                muestraVentana('Debe actualizar su navegador a Chrome 10+.');
                ctl = false;
            }
            break;

        case 'Explorer':
            if (version < 9) {
                muestraVentana('Debe actualizar su navegador a IE 9.');
                ctl = false;
            }
            break

        case 'Apple':
            if (version < 5) {
                muestraVentana('Debe actualizar su navegador a Safari 5+.');
                ctl = false;
            }
            break;

        case 'Firefox':
            if (version < 4) {
                muestraVentana('Debe actualizar su navegador a Mozilla Firefox 4+.');
                ctl = false;
            }
            break;
    }
    if (ctl) {
        //document.getElementById('tablaNavegadores').style.display = 'none';
    }
}


/**********************************************************
METODO QUE PERMITE GENERAR UNA CLAVE AUTOMATICA
***********************************************************/
function generarClave() {
    var i = 0;
    var clave = '';
    while (i < 10) {
        clave += caracter[(Math.round(Math.random() * 35))];
        i++;
    }
    return clave;
}

function loginFormulario() {
    document.getElementById("loginFormulario").style.display = "block";
}


function recuperarClave() {
    var email = $('#email').val();
    if (email != '') {
        if (vemail(email)) {
            var cla = generarClave();
            var claE = md5(cla);
            muestraVentanaProgreso("Recuperando Contrase&ntilde;a..");
            var arrayParameters = new Array();
            arrayParameters.push(newArg('p', 'solicitaClave'));
            arrayParameters.push(newArg('email', email));
            arrayParameters.push(newArg('clave', cla));
            arrayParameters.push(newArg('claveE', claE));
            var send = arrayParameters.join('&');
            $.post('../../Controlador/ctlLogin.aspx', send, recuperarClave_processResponse);

        } else {
            muestraVentana('El correo Electr贸nico no tiene el formato valido');
        }
    } else {
        muestraVentana(mensajeobligatorios);
    }
}

function recuperarClave_processResponse(res) {
    try {
        ocultaVentanaProgreso();
        var info = eval("(" + res + ")");
        var msj = info.data;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:
                muestraVentana("Se ha enviado un mensaje a tu correo electr&oacute;nico con la clave.");
                $.fancybox.close()
                break;

            case 3:
                muestraVentana("Su cuenta se encuentra deshabilitada! <br> Comuniquese con el administrador");
                $.fancybox.close()
                break;
        }
    } catch (elError) {
    }
}


function cancelaRecuperaClave() {
    $.fancybox.close();
    $('#email').val('');
}



////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////CODIGO PARA CRAR UN NUEVO USUARIO//////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////

function ventanaRespuesta_processResponse(res) {
    ocultaVentanaProgreso();
    try {

        var info = eval('(' + res + ')');

        switch (info) {
            case -1:
                muestraVentana(mensajemenosuno);
                usuarioGlobal = "-1";

                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:

                muestraVentana('Informaci贸n almacenada correctamente');
                setTimeout("ocultarPestanas('Pest_equipoLectura')", 50);

                break;
            case 2:
                muestraVentana('Informaci贸n Actualizada correctamente');
                // $.fancybox.close();

                //listarGeneral(1);
                setTimeout("ocultarPestanas('Pest_equipoLectura')", 50);

                break;

            case 4:
                muestraVentana('NO SE PUEDE BORRAR EL 谩REA, HAY CARGOS');
                $.fancybox.close();
                usuarioGlobal = "-1";
                break;
            case 5:
                muestraVentana('YA EXISTE UN USUARIO CON ESE DOCUMENTO');
                break;

            case 6:
                //document.getElementById("txtDocumentoUsu").value = documentoGlobal;                    
                muestraVentana('Informaci贸n almacenada correctamente');
                usuarioGlobal = "-1";
                cerrarFancy();
                break;
            case 7:
                muestraVentana('YA EXISTE UN USUARIO CON ESE CORREO REGISTRADO');
                usuarioGlobal = '-1';
                break;

        }
    } catch (elError) {
    }
}

function Guardar() {
    //limpiar();
    var id = usuarioGlobal;
    var tipoDocumento = document.getElementById("tipoDocumento").value;
    var documento = document.getElementById("documento").value;
    var nombres = document.getElementById("nombres").value;
    var apellidos = document.getElementById("apellidos").value;
    var fechaNacimiento = document.getElementById("fechaNacimiento").value;
    var cualgen = document.getElementById("txtCualGen").value;
    var genero = document.getElementById("genero").value;
    var rh = document.getElementById("rh").value;
    var educacion = document.getElementById("educacion").value;
    var poblacion = document.getElementById("poblacion").value;
    var ocupacion = document.getElementById("ocupacion").value;
    var entidad = document.getElementById("entidad").value;
    var sisben = document.getElementById("sisben").value;
    var categoria = document.getElementById("categoria").value;
    var email = document.getElementById("emailUsu").value;
    var informacion = document.getElementById("informacion").value;
    var permiso = document.getElementById("permiso").value;

    var cla = generarClave();
    var claE = md5(cla);



    if (documento != "" && nombres != "" && apellidos != "" && email != "") {
        documentoGlobal = document.getElementById("documento").value;
        if (banderaUsu == 1) {
            var arrayparameters = new Array();
            arrayparameters.push(newArg('p', 'guardarUsuario'));
            arrayparameters.push(newArg('id', id));
            arrayparameters.push(newArg('tipoDocumento', tipoDocumento));
            arrayparameters.push(newArg('documento', documento));
            arrayparameters.push(newArg('nombres', nombres));
            arrayparameters.push(newArg('apellidos', apellidos));
            arrayparameters.push(newArg('fechaNacimiento', fechaNacimiento));
            arrayparameters.push(newArg('cualgen', cualgen));
            arrayparameters.push(newArg('genero', genero));
            arrayparameters.push(newArg('rh', rh));
            arrayparameters.push(newArg('educacion', educacion));
            arrayparameters.push(newArg('poblacion', poblacion));
            arrayparameters.push(newArg('ocupacion', ocupacion));
            arrayparameters.push(newArg('entidad', entidad));
            arrayparameters.push(newArg('sisben', sisben));
            arrayparameters.push(newArg('categoria', categoria));
            arrayparameters.push(newArg('email', email));
            arrayparameters.push(newArg('informacion', informacion));
            arrayparameters.push(newArg('permiso', permiso));
            arrayparameters.push(newArg('clave', cla));
            arrayparameters.push(newArg('claveE', claE));
            var send = arrayparameters.join('&');
            $.post('../../Controlador/ctlLogin.aspx', send, ventanaRespuesta_processResponse);
            usuarioGlobal = documento;
            muestraVentanaProgreso("CREANDO USUARIO");
        } else {
            muestraVentana('DEBE ACEPTAR LAS CONDICIONES');
        }

    }
    else {
        muestraVentana(mensajeobligatorios);
    }

}

function autonumerico() {
    var solicitar = new Array();

    solicitar.push(newArg('p', 'ObtenerCodigoConsMotivoDev'));
    var send = solicitar.join('&');
    $.post('../../Controlador/ctlLogin.aspx', send, autonumerico_processResponse);
}

function autonumerico_processResponse(res) {
    var info = eval('(' + res + ')');
    var solicitado = parseInt(((info.data[0] == "") ? 0 : info.data[0])) + parseInt(1);
    //var NConsecutivo = solicitado[4];
    document.getElementById("txtConsecutivoMotivo").value = unescape(solicitado);
}

function ValidaEdad(fecha) {

    //var camb_fecha = document.getElementById("fechaNaciPaciente").value;
    //edad(camb_fecha);

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'validadEdad'));

    arrayParameters.push(newArg('fecha', fecha));

    //document.getElementById("edadPaciente").value = edad;
    //document.getElementById("selTipoEdadPaciente").value = 'A袿S';

    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlLogin.aspx', send, validaEdad_processResponse);
}

function validaEdad_processResponse(res) {
    try {
        var info = eval('(' + res + ')');

        if (res != '0') {
            var datosRows = info.data;
            var l = info.cols;


            var edad = "";
            var tipoedad = "";



            for (var i = 0; i < datosRows.length; i += l) {
                edad = datosRows[i];
                document.getElementById('edad').value = edad;

                tipoedad = datosRows[i + 1];

                if (tipoedad == 2) {
                    document.getElementById('edad').value = edad + ' D铆AS';

                }
                if (tipoedad == 1) {
                    document.getElementById('edad').value = edad + ' A帽OS';
                }
                if (tipoedad == 3) {
                    document.getElementById('edad').value = edad + ' MESES';
                }
            }

        } else {

        }
    } catch (elError) {
    }

}




function cargarTipoDocumento() {

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargarTipoDocumento'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlLogin.aspx', send, cargarTipo_Documento);
}

function cargarTipo_Documento(res) {
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
                llenarSelect(res, document.getElementById('tipoDocumento'));
                //llenarSelect(res, document.getElementById('selDepartamentoContacto'));
                break;
        }
    } catch (elError) { }

}

function cargarGenero() {/*
    var Arreglo = new Array();
    Arreglo.push(newArg('p', 'cargarGenero'));
    var send = (Arreglo.join('&'));
    $post('../../Controlador/ctlLogin.aspx', send, cargar_Genero);*/

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargarGenero'));
    // arrayParameters.push(newArg('a', a));s
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlLogin.aspx', send, cargar_Genero);

}

function cargar_Genero(res) {
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
                llenarSelect(res, document.getElementById('genero'));
                //llenarSelect(res, document.getElementById('selDepartamentoContacto'));
                break;
        }
    } catch (elError) { }

}

function cargarRH() {/*
    var Arreglo = new Array();
    Arreglo.push(newArg('p', 'cargarRH'));
    var send = (Arreglo.join('&'));
    $post('../../Controlador/ctlLogin.aspx', send, cargar_RH);*/

    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargarRH'));
    // arrayParameters.push(newArg('a', a));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlLogin.aspx', send, cargar_RH);

}

function cargar_RH(res) {
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
                llenarSelect(res, document.getElementById('rh'));
                //llenarSelect(res, document.getElementById('selDepartamentoContacto'));
                break;
        }
    } catch (elError) { }

}

function cargarInfo(a) {
    // SelecGlobal = a;
    // var Arreglo = new Array();
    //   Arreglo.push(newArg('p', 'cargarInfo'));
    // var send = (Arreglo.join('&'));
    // Arreglo.push(newArg('a', a));
    //  $post('../../Controlador/ctlLogin.aspx', send, cargar_Info);


    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargarInfo'));
    arrayParameters.push(newArg('a', a));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlLogin.aspx', send, cargar_Info);

}

function cargar_Info(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        var prueba = info.data[0];
        SelecGlobal = info.data[0];
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                muestraVentana(mensajecero);
                break;
            case 1:

                switch (prueba) {
                    case '-1':
                        llenarSelect(res, document.getElementById('educacion'));
                        break;
                    case '-2':
                        llenarSelect(res, document.getElementById('poblacion'));
                        break;
                    case '-3':
                        llenarSelect(res, document.getElementById('ocupacion'));
                        break;

                    case '-4':
                        llenarSelect(res, document.getElementById('sisben'));
                        break;

                    case '-5':
                        llenarSelect(res, document.getElementById('estrato'));
                        break;

                }
                /*
                if (SelecGlobal == '-2') {
                llenarSelect(res, document.getElementById('sisben'));
                }

                if (SelecGlobal == '-3') {
                llenarSelect(res, document.getElementById('estrato'));
                }*/


                //llenarSelect(res, document.getElementById('selDepartamentoContacto'));
                break;
        }
    } catch (elError) { }
}


function limpiarUsuario() {


    document.getElementById("tipoDocumento").value = '1';
    document.getElementById("documento").value = "";
    document.getElementById("nombres").value = "";
    document.getElementById("apellidos").value = "";
    document.getElementById("fechaNacimiento").value = "";
    document.getElementById("edad").value = "";
    document.getElementById("genero").value = "-1";
    document.getElementById("rh").value = "-1";
    document.getElementById("educacion").value = "-1";
    document.getElementById("poblacion").value = "-2";
    document.getElementById("ocupacion").value = "-3";
    document.getElementById("entidad").value = "";
    document.getElementById("sisben").value = "-4";
    document.getElementById("categoria").value = "";
    document.getElementById("emailUsu").value = "";
    document.getElementById("informacion").checked = false;
    document.getElementById("permiso").checked = false;
    document.getElementById("estrato").value = "-5";
    document.getElementById("fechaNacimiento").value = "";
    document.getElementById("pais").value = "-1";
    document.getElementById("departamento").value = "-1";
    document.getElementById("municipio").value = "-1";
    document.getElementById("direccion").value = "";
    document.getElementById("barrio").value = "";
    document.getElementById("comuna").value = "-1";
    document.getElementById("telefono").value = "";
    document.getElementById("celular").value = "";

}


function cargarPais() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargaPais'));
    // arrayParameters.push(newArg('a', a));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlLogin.aspx', send, cargar_Pais);
}

function cargar_Pais(res) {
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
                llenarSelect(res, document.getElementById('pais'));
                //llenarSelect(res, document.getElementById('selDepartamentoContacto'));
                break;
        }

        if (paisGlobal != '-1') {
            document.getElementById('pais').value = paisGlobal;
            cargaDepartamentoValor(paisGlobal);
        }


    } catch (elError) { }

}

function cargaDepartamentoValor(codDep) {
    if (codDep != '-1') {
        if (codDep != deptoGlobal) {

        }
        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', 'cargaDepto'));
        arrayParameters.push(newArg('pais', codDep));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlLogin.aspx', send, cargaDepartamento_processResponse);
    } else {
        limpiarSelectOpcion(document.getElementById('departamento'));
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
                llenarSelect(res, document.getElementById('departamento'));
                //llenarSelect(res, document.getElementById('selMunicipioContacto'));
                break;
        }

        if (deptoGlobal != '-1') {
            document.getElementById('departamento').value = deptoGlobal;
            cargaMunicipioValor(deptoGlobal);
        }
    } catch (elError) { }
}

function cargaMunicipioValor(codMun) {
    if (codMun != '-1') {
        if (codMun != deptoGlobal) {
            municipioGlobal = '-1';
        }
        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', 'cargaMpio'));
        arrayParameters.push(newArg('depto', codMun));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlLogin.aspx', send, cargaMunicipio_processResponse);
    } else {
        limpiarSelectOpcion(document.getElementById('municipio'));
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
                llenarSelect(res, document.getElementById('municipio'));
                //llenarSelect(res, document.getElementById('selMunicipioContacto'));
                break;
        }

        if (municipioGlobal != '-1') {
            document.getElementById('municipio').value = municipioGlobal;
        }
    } catch (elError) { }
}

function cargarDepartamento(pais) {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargaDepto'));
    arrayParameters.push(newArg('pais', pais));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlLogin.aspx', send, cargar_Departamento);
}

function cargar_Departamento(res) {
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
                llenarSelect(res, document.getElementById('departamento'));
                //llenarSelect(res, document.getElementById('selDepartamentoContacto'));
                break;
        }
    } catch (elError) { }

}

function cargarMunicipio(depto) {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargaMpio'));
    arrayParameters.push(newArg('depto', depto));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlLogin.aspx', send, cargar_Municipio);
}

function cargar_Municipio(res) {
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
                llenarSelect(res, document.getElementById('municipio'));
                //llenarSelect(res, document.getElementById('selDepartamentoContacto'));
                break;
        }
    } catch (elError) { }
}


function cargarLocalidad(local) {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargaLocal'));
    arrayParameters.push(newArg('local', local));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlLogin.aspx', send, cargar_Localidad);
}

function cargar_Localidad(res) {
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
                llenarSelect(res, document.getElementById('comuna'));
                //llenarSelect(res, document.getElementById('selDepartamentoContacto'));
                break;
        }
    } catch (elError) { }
}

/*
function cargarBarrio(barrio) {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargarBarrio'));
    arrayParameters.push(newArg('barrio', barrio));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlLogin.aspx', send, cargar_Barrio);
}

function cargar_Barrio(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana('INFORMACI贸N TEMPORALMENTE NO DISPONIBLE');
                break;
            case 0:
                // muestraVentana('NO HAY PAISES REGISTRADOS');
                break;
            case 1:
                llenarSelect(res, document.getElementById('barrio'));
                //llenarSelect(res, document.getElementById('selDepartamentoContacto'));
                break;
        }
    } catch (elError) { }
}*/

function seleccion(a, chk) {
    switch (a) {
        case 'informacion':
            document.getElementById('informacion').value = 1;
            break;
        case 'permiso':
            document.getElementById('permiso').value = 1;

            if (chk == true) {
                banderaUsu = 1;
            } else {
                banderaUsu = 0;
            }
            break;
    }


}

function GuardarResidencia() {

    var pais = document.getElementById("pais").value;
    var departamento = document.getElementById("departamento").value;
    var municipio = document.getElementById("municipio").value;
    var direccion = document.getElementById("direccion").value;
    var barrio = document.getElementById("barrio").value;
    var estrato = document.getElementById("estrato").value;
    var telefono = document.getElementById("telefono").value;
    var celular = document.getElementById("celular").value;



    var arrayparameters = new Array();
    arrayparameters.push(newArg('p', 'guardarResidencia'));
    arrayparameters.push(newArg('id', usuarioGlobal));
    arrayparameters.push(newArg('pais', pais));
    arrayparameters.push(newArg('departamento', departamento));
    arrayparameters.push(newArg('municipio', municipio));
    arrayparameters.push(newArg('direccion', direccion));
    arrayparameters.push(newArg('barrio', barrio));
    arrayparameters.push(newArg('estrato', estrato));
    arrayparameters.push(newArg('telefono', telefono));
    arrayparameters.push(newArg('celular', celular));
    var send = arrayparameters.join('&');

    $.post('../../Controlador/ctlLogin.aspx', send, ventanaRespuesta_processResponse);

}


function validaSisben(value) {
    if (value == -1 || value == 3) {
        document.getElementById("categoriaSisben").style.display = 'none'
        // document.getElementById("categoriaSisben1").style.display = 'none'
    } else {
        document.getElementById("categoriaSisben").style.display = 'block'
        // document.getElementById("categoriaSisben1").style.display = 'block'
    }
}


function validaGenero(value) {
    if (value == 3) {
        document.getElementById("cualGenero").style.display = 'block'
        // document.getElementById("categoriaSisben1").style.display = 'none'
    } else {
        document.getElementById("cualGenero").style.display = 'none'
        // document.getElementById("categoriaSisben1").style.display = 'block'
    }
}

function validaDocumento(e) {
    if (e.keyCode == 13 || e.keyCode == 9) {
        var documento = document.getElementById("documento").value;
        cargarInfoGeneral(documento);

    }
}

function nuevoUsuario() {

    setTimeout("ocultarPestanas('Pest_puntoVenta')", 50);
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionOut': 'fade',
        'transitionInt': 'fade',
        'scrolling': 'no',
        'href': '#divNuevoUsuario'
    });

    limpiarUsuario();
}

function ocultarPestanas(objeto) {
    document.getElementById(objeto).style.display = 'block';
    $("#" + objeto).trigger('click');
}


function cargaBarrio(mun) {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargarBarrios'));
    arrayParameters.push(newArg('mun', mun));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlLogin.aspx', send, cargaBarrio_processResponse);
}

function cargaBarrio_processResponse(res) {
    var info = eval('(' + res + ')');
    var msj = info.msj;
    var aux = 0;
    var dataRows = info.data;
    switch (msj) {
        case -1:
            muestraVentana(mensajemenosuno);
            break;
        case 0:
            muestraVentana(mensajecero);
            break;
        case 1:
            for (var i = 0; i < dataRows.length; i += 2) {//asigna a los vectores los valores de la consulta
                vectorIdProveedor[aux] = info.data[i];
                vectorProveedor[aux] = info.data[i + 1];
                aux += 1;

            }
            break;
    }
    setTimeout("funcionAutocompletarBarrio()", 500);
}

function funcionAutocompletarBarrio() {
    $(function () {
        'use strict';
        var countriesArray = $.map(
        vectorProveedor, function (value, key) {
            return {
                value: value,
                data: key
            };
        });
        $('#barrio').autocomplete({
            lookup: countriesArray,
            minChars: 0,
            onSelect: function (suggestion) {
                vectorIdContacto.length = 0;
                vectorContacto.length = 0;
                globalCliente = vectorIdProveedor[suggestion.data];
                // document.getElementById('productos').name = globalCliente;
                //  alert(globalCliente);
            }
        });

    });
}

