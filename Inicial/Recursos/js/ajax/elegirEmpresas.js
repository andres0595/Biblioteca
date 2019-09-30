$(document).ready(function () {
    listaEmpresa(1)
});

function listaEmpresa(pag) {

    document.getElementById('msjVentana').innerHTML = "";

    //var nit = document.getElementById('nit').value;
    //var nom = document.getElementById('razonSocial').value;
    muestraVentanaProgreso();
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'empresa'));
    arrayParameters.push(newArg('nit', ''));
    arrayParameters.push(newArg('nombre', ''));
    arrayParameters.push(newArg('pag', pag));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlPaginador.aspx', send, listaEmpresa_processResponse);
}

function listaEmpresa_processResponse(respuesta) {
    try {
        ocultaVentanaProgreso();
        var res = respuesta;
        var info = eval('(' + res + ')');
        var divEmpresas = document.getElementById('listadoEmpresas');
        if (res != '0') {
            var datosRows = info.data;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";
            var razon = ""
            var nit = ""
           // var municipio = ""
           // var direccion = ""
           // var telefono = ""
            var actividad = ""

            var tabla = "<table class='tbListado centrar' style='text-align: center;'><tr><td class='encabezado' colspan='7'>EMPRESAS</td></tr>";
            tabla += "<tr><td class='encabezado'>NIT</td><td class='encabezado'>RAZ&Oacute;N SOCIAL</td><td class='encabezado'>ACTIVIDAD</td><td class='encabezado'>SELECCIONAR</td></tr>";

            for (var i = 0; i < datosRows.length; i += 3) {
                nit = datosRows[i];
                razon = datosRows[i + 1];
              //  municipio = datosRows[i + 2];
               // direccion = datosRows[i + 3];
               // telefono = datosRows[i + 4];
                actividad = datosRows[i + 2];

                if (ctl) {
                    claseAplicar = "cuerpoListado7";
                    claseAplicar2 = "cuerpoListado1";
                } else {
                    claseAplicar = "cuerpoListado8";
                    claseAplicar2 = "cuerpoListado2";
                }

                ctl = !ctl;
                tabla += '<tr><td class="' + claseAplicar2 + '" align="center">' + nit + '</td><td class="' + claseAplicar + '">' + razon + '</td><td class="' + claseAplicar + '">' + actividad + '</td>';
                tabla += '<td class="' + claseAplicar2 + '"><input type="radio" name="group1" id="' + nit + '" value="' + nit + '" onclick="elegirEmpresa(this.id, this.checked,\'' + razon + '\');"></td></tr>';
            }
            divEmpresas.innerHTML = tabla;
            divEmpresas.innerHTML += pieDePaginaListar(info, 'listaEmpresa');
        } else {
            divEmpresas.innerHTML = mensajecero;
        }
    } catch (elError) {
        //alert(elError);
    }
}

var empresaGlobal = "";
function elegirEmpresa(id, bool, razon) {
    if (bool) {
        empresaGlobal = razon;
        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', 'crearSesionEmpresa'));
        arrayParameters.push(newArg('nit', id));
        arrayParameters.push(newArg('razon', razon));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlDirector.aspx', send, elegirEmpresa_processResponse);
    } else {
        empresaGlobal = "";
        var arrayParameters = new Array();
        arrayParameters.push(newArg('p', 'crearSesionEmpresa'));
        arrayParameters.push(newArg('nit', 'NIT'));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlDirector.aspx', send, elegirEmpresa_processResponse);
    }
}

function elegirEmpresa_processResponse(res) {
    try {
        switch (res) {
            case '1':
                location.href = "../general/inicio.aspx";
        }
    } catch (elError) {
    }
}