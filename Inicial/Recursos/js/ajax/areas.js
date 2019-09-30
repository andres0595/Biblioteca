$(document).ready(function () /*dindica que nuesstra función listarDatos(n) se ejecuta al iniciar.*/
{
    listarAreas(1);
    cargaDepartamentoEmpresa();
});

/////////////// VARIABLES GLOBALES /////////////////

var idGlobal = "-1"; // id para crear o modificar 

///////////////////////////////////////////////////

function limpiar() {
    document.getElementById("seldepartamento").value = '-1';
    document.getElementById("txtNombreArea").value = '';
    document.getElementById("taDescripcion").value = '';
    document.getElementById("txtNombreFiltro").value == '';
}

///**** Cierra los fancy
function cerrarFancy() {
    $.fancybox.close();
    idGlobal = "-1";
    limpiar();
}

function nuevaArea() {
    limpiar();
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionOut': 'fade',
        'transitionInt': 'fade',
        'scrolling': 'no',
        'href': '#divNuevaArea'
    });

}

//** Gaurda las area

function guardarArea() {

    var id = idGlobal;
    var nombre = document.getElementById('txtNombreArea').value;
    var departamento = document.getElementById('seldepartamento').value;
    var descripcion = document.getElementById('taDescripcion').value;

    if (nombre != "" && departamento != "-1") {
        /// DECLARAMOS EL ARREGLO QUE VAMOS A ENVIAR 
        var arrayParameters = new Array();

        arrayParameters.push(newArg('p', 'guardarAreas'));
        arrayParameters.push(newArg('id', id));
        arrayParameters.push(newArg('nombre', nombre));
        arrayParameters.push(newArg('departamento', departamento));
        arrayParameters.push(newArg('descripcion', descripcion));
        var send = arrayParameters.join('&');
        $.post('../../Controlador/ctlAreas.aspx', send, ventanaRespuesta_processResponse);
    }

    else {
        muestraVentana("DEBE INGRESAR LOS CAMPOS OBLIGATORIOS");
    }


}

//** Borra la area seleccionada

function borrarArea() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'borrarAreas'));
    arrayParameters.push(newArg('id', idGlobal));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlAreas.aspx', send, ventanaRespuesta_processResponse);
}

//** muestra las ventanas de respuestas

function ventanaRespuesta_processResponse(res) {
    try {
        var info = eval('(' + res + ')');

        switch (info) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 1:
                muestraVentana('Información almacenada correctamente');
                $.fancybox.close();
                listarAreas(1);
                idGlobal = "-1";
                break;
            case 2:
                muestraVentana('Información Actualizada correctamente');
                $.fancybox.close();
                listarAreas(1);
                idGlobal = "-1";
                break;
            case 3:
                muestraVentana('Información Eliminada correctamente');
                $.fancybox.close();
                listarAreas(1);
                idGlobal = "-1";
                break;
            case 4:
                muestraVentana('NO SE PUEDE BORRAR EL &Aacute;REA, HAY CARGOS');
                $.fancybox.close();
                listarAreas(1);
                idGlobal = "-1";
                break;

        }
    } catch (elError) {
    }
}

//** crea la tabla del detalle

function detalle(id, nombre, descripcion) {

    var tabla = "";
    var vacio = "--";

    tabla += "<table class='tbListado centrar' style='text-align: center;'><tr><td  class='encabezado' colspan ='2'><b> DETALLES </b></td></tr>";
    tabla += "<tr>";
    tabla += "<td class='encabezado' colspan ='2'>NOMBRE &Aacute;REA</td></tr>";
    // tabla += '<tr><td class="cuerpoListado10">' + ((id == "") ? vacio : id) + '</td>';
    tabla += '<td class="cuerpoListado10">' + ((nombre == "") ? vacio : unescape(nombre).toUpperCase()) + '</td>';


    tabla += "<table class='tbListado centrar' style='text-align: center;'><tr><td  class='encabezado' colspan ='2'><b>  </b>DESCRIPCIÓN</td></tr>";



    tabla += '</tr>';

    tabla += '<td class="cuerpoListado10">' + ((descripcion == "") ? vacio : unescape(descripcion).toUpperCase()) + '</td>';

    tabla += '</tr>';
    tabla += "</table>";
    document.getElementById("divTablaDetalle").innerHTML = tabla;

    listaCargosRelacionados(1, id);





}


/*****   LISTA CARGOS RELACIONADOS CON EL ÁREA  *******/
function listaCargosRelacionados(pag, idArea) {
    // consultarNumInscritos(idAula);
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listaTodosCargos'));
    arrayParameters.push(newArg('nombreFiltro', '')); // envio el nombreFiltro vacio para re utilizar el PA que tulizo al filtrar en  cargos.ASPX
    arrayParameters.push(newArg('area', idArea));
    arrayParameters.push(newArg('pag', pag));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlPaginador.aspx', send, listaCargosRelacionados_processResponse);
}

function listaCargosRelacionados_processResponse(res) {

    try {
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('divTablaCargos');
        if (res != '0') {
            var datosRows = info.data;
            var l = info.cols;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";

            var id = "";
            var nombre = "";
            var descripcion = "";
            var area = "";

            var tabla = "<table class='tbListado centrar' style='text-align: center;'><tr><td class='encabezado' colspan='2'>CARGOS ASOCIADOS A ESTA &Aacute;REA</td></tr>";
            tabla += "<tr><td class='encabezado' colspan='2'>NOMBRE DEL CARGO</td></tr>";
            //tabla += "<tr><td class='encabezado' colspan='2'>NOMBRE</td><td class='encabezado'>DETALLE</td><td class='encabezado'>EDITAR</td><td class='encabezado'>ELIMINAR</td></tr>";

            for (var i = 0; i < datosRows.length; i += l) {
                id = datosRows[i]
                nombre = datosRows[i + 1];
                descripcion = datosRows[i + 2];
                area = datosRows[i + 3];

                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar2 = "cuerpoListado3";
                } else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar2 = "cuerpoListado5";
                }

                ctl = !ctl;
                tabla += '<tr><td colspan="2" class="' + claseAplicar + '" align="center">' + unescape(nombre).toUpperCase() + '</td>';
                //tabla += '<td class="' + claseAplicar2 + '"><div id="linkDetalle" class="linkIconoLateral botonDetalle" onclick="detalle( \'' + nombre.toUpperCase() + '\',\'' + descripcion.toUpperCase() + '\' )"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/listar.png"><p>Detalle</p></div></td>'; //copia los datos de la fila seleccionada 
                //tabla += '<td class="' + claseAplicar2 + '"><div id="linkEdita" class="linkIconoLateral botonEditar" onclick="editarCargo( \'' + id + '\',\'' + unescape(nombre).toUpperCase() + '\',\'' + unescape(descripcion).toUpperCase() + '\',\'' + area + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/editar_24x24.png"><p>Editar</p></div></td>'; //copia los datos de la fila seleccionada 
                //tabla += '<td class="' + claseAplicar2 + '"><div id="linkElimina" class="linkIconoLateral botonEliminar" onclick="eliminarCargo(\'' + id + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/eliminar_24x24.png"><p>Eliminar</p></div></td></tr>';
            }
            tabla += '</table>'
            divTerceros.innerHTML = tabla;
            divTerceros.innerHTML += pieDePaginaListar(info, 'listarCargos');
            var idMenuForm = document.getElementById('idMenuForm').innerHTML; // se adiciona esta linea para que se de  el permiso de visualizar el editar despues de cargar
            permisosParaMenu(idMenuForm); // se adiciona esta linea para que se el permiso de visualizar el editar despues de cargar
        } else {
            divTerceros.innerHTML = 'NO HAY CARGOS ASOCIADOS';
        }


        /* LLAMAMOS EL DIV PARA ABRIR EL DETALLE*/
        $.fancybox({
            'showCloseButton': false,
            'hideOnOverlayClick': false,
            'enableEscapeButton': false,
            'transitionIn': 'fade',
            'transitionOut': 'fade',
            'transitionOut': 'fade',
            'href': '#divDetalleArea'
        });



    }
    catch (elError) {
    }
}



///*** permite editar 

function editarArea(id, nombre, descripcion, departamento) {

    idGlobal = id;
    document.getElementById("txtNombreArea").value = unescape(nombre);
    document.getElementById("taDescripcion").value = unescape(descripcion);
    document.getElementById("seldepartamento").value = unescape(departamento);


    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionOut': 'fade',
        'transitionInt': 'fade',
        'scrolling': 'no',
        'href': '#divNuevaArea',
        'onClosed': function () {
            limpiar();
        }
    });

}

///** Elimina las areas


function eliminarArea(id) {
    idGlobal = id;
    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionOut': 'fade',
        'transitionInt': 'fade',
        'scrolling': 'no',
        'href': '#divConfirmaEliminar',
        'onClosed': function ()
        { }

    });
}


//*** muesta un filtro por nombre

function abrirFiltro() {
    document.getElementById("txtNombreFiltro").value == '';

    $.fancybox({
        'showCloseButton': false,
        'hideOnOverlayClick': false,
        'enableEscapeButton': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'transitionOut': 'fade',
        'href': '#filtro',
        'onClosed': function () {
            limpiar();
        }
    });
}

//**** muestra todo los datos

function verTodos() {

    document.getElementById("txtNombreFiltro").value = '';
    listarAreas(1);
}

//** verifica que los campos del filtro tengan datos

function verificarFiltro() {
    listarAreas(1);
    limpiar();
}


///////////////////////// LISTAR DATOS /////////////////////////////////


/* Me enlista (crea el paginador) los datos guardados por el fancy - se ejecuta al inicar por la sentencia de bien arriba*/

function listarAreas(pag) {

    pagGlobal = pag;

    //***** Filtro ***///
    var nombre = document.getElementById("txtNombreFiltro").value;
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'listaAreas'));
    arrayParameters.push(newArg('nombre', nombre));
    arrayParameters.push(newArg('pag', pag));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlPaginador.aspx', send, listarAreas_processResponse);
}

/*crea la tabla donde se muestran los datos*/
function listarAreas_processResponse(res) {

    try {
        var info = eval('(' + res + ')');
        var divTerceros = document.getElementById('divListadoAreas');
        if (res != '0') {
            var datosRows = info.data;
            var l = info.cols;
            var ctl = true, claseAplicar = "", claseAplicar1 = "", claseAplicar2 = "";

            var id = "";
            var fecha = "";
            var nombre = "";
            var descripcion = "";
            var icono = "";
            var tabla = "<table class='tbListado centrar' style='text-align: center;'><tr><td class='encabezado' colspan='5'>&Aacute;REAS EXISTENTES</td></tr>";
            tabla += "<tr><td class='encabezado' colspan='2'>NOMBRE</td><td class='encabezado'>DETALLE</td><td class='encabezado'>EDITAR</td><td class='encabezado'>ELIMINAR</td></tr>";

            for (var i = 0; i < datosRows.length; i += l) {
                id = datosRows[i]
                nombre = datosRows[i + 1];
                descripcion = datosRows[i + 2];
                var departamento = datosRows[i + 3];


                if (ctl) {
                    claseAplicar = "cuerpoListado9";
                    claseAplicar2 = "cuerpoListado3";
                } else {
                    claseAplicar = "cuerpoListado10";
                    claseAplicar2 = "cuerpoListado5";
                }

                ctl = !ctl;
                tabla += '<tr><td colspan="2" class="' + claseAplicar + '" align="center">' + unescape(nombre).toUpperCase() + '</td>';
                tabla += '<td class="' + claseAplicar2 + '"><div id="imgDetalle" class="linkIconoLateral botonDetalle" onclick="detalle(\'' + id + '\',\'' + nombre.toUpperCase() + '\',\'' + descripcion.toUpperCase() + '\' )"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/listar.png"><p>Detalle</p></div></td>'; //copia los datos de la fila seleccionada 
                tabla += '<td class="' + claseAplicar2 + '"><div id="imgEditar" class="linkIconoLateral botonEditar" onclick="editarArea( \'' + id + '\',\'' + unescape(nombre).toUpperCase() + '\',\'' + unescape(descripcion).toUpperCase() + '\',\'' + departamento + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/editar_24x24.png"><p>Editar</p></div></td>'; //copia los datos de la fila seleccionada 
                tabla += '<td class="' + claseAplicar2 + '"><div id="imgEliminar" class="linkIconoLateral botonEliminar" onclick="eliminarArea(\'' + id + '\')"><img height="16px" width="16px" src="../../Recursos/imagenes/administracion/eliminar_24x24.png"><p>Eliminar</p></div></td></tr>';
            }
            tabla += '</table>'
            divTerceros.innerHTML = tabla;
            divTerceros.innerHTML += pieDePaginaListar(info, 'listarAreas');

            var idMenuForm = document.getElementById('idMenuForm').innerHTML; // se adiciona esta linea para que se de  el permiso de visualizar el editar despues de cargar
            permisosParaMenu(idMenuForm); // se adiciona esta linea para que se el permiso de visualizar el editar despues de cargar
        } else {
            divTerceros.innerHTML = 'NO HAY INFORMACIÓN EN LA BASE DE DATOS';
        }
    } catch (elError) {
    }
}


function cargaDepartamentoEmpresa() {
    var arrayParameters = new Array();
    arrayParameters.push(newArg('p', 'cargaDepartamentoEmpresa'));
    var send = arrayParameters.join('&');
    $.post('../../Controlador/ctlGeneral.aspx', send, cargaCategorias_processResponse);
}

function cargaCategorias_processResponse(res) {
    try {
        var info = eval('(' + res + ')');
        var msj = info.msj;
        switch (msj) {
            case -1:
                muestraVentana(mensajemenosuno);
                break;
            case 0:
                // muestraVentana(mensajeSinInformacion);
                break;
            case 1:
                //  llenarSelect(res, document.getElementById('selCatsucursales'));
                llenarSelect(res, document.getElementById('seldepartamento'));
                break;
        }
    } catch (elError) { }
}




