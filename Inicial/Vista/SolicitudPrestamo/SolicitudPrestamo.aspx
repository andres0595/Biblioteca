<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/master/pagina_maestra.Master" AutoEventWireup="true" CodeBehind="SolicitudPrestamo.aspx.cs" Inherits="Inicial.Vista.SolicitudPrestamo.SolicitudPrestamo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="server">
    .:: Solicitud Prestamo ::.
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenidoSistema" runat="server">
    <link href="../../Recursos/css/calendario/calendario.css" rel="stylesheet" type="text/css" />
    <link href="../../Recursos/css/calendario/jquery.ui.core.css" rel="stylesheet" type="text/css" />
    <link href="../../Recursos/css/calendario/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../Recursos/css/pestanas/demos.css" rel="stylesheet" type="text/css" />
    <link href="../../Recursos/css/pestanas/jquery.ui.tabs.css" rel="stylesheet" type="text/css" />
    <script src="../../Recursos/js/calendario/jquery-ui-1.8.7.custom.js" type="text/javascript"></script>
    <script src="../../Recursos/js/calendario/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Recursos/js/calendario/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="../../Recursos/js/calendario/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Recursos/js/pestanas/jquery.ui.tabs.js" type="text/javascript"></script>
    <%--fancybox CSS --%>
    <link rel="stylesheet" type="text/css" href="../../Recursos/js/fancybox/jquery.fancybox-1.3.4.css" media="screen" />
    <%--fancybox JS --%>
    <script type="text/javascript" src="../../Recursos/js/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../../Recursos/js/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    <script src="../../Recursos/js/ajax/SolicitudPrestamo.js" type="text/javascript"></script>
    <span class="ocultar" id="idMenuForm">2.A</span>
    <div id="contenedorUsuario" class="tbBorde">
        <table class="centrar responsive">
            <tr>
                <td colspan="2" align="center">
                    <span class="tituloForm">ADMINISTRACI&oacute;N DE PRESTAMOS</span>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <div style="float: left;" id="imgGuardar" class="linkIconoSuperior botonGuardar "
                        onclick="NuevaSolicitud();">
                        <img height="16px" width="16px" src="../../Recursos/imagenes/administracion/agregar_24x24.png">
                        <p>
                            Nuevo
                       
                        </p>
                    </div>
                </td>
                <td align="right">
                    <div style="float: right;" id="imgTodos" class="linkIconoSuperior botonTodos" onclick="verTodos();">
                        <p>
                            Ver Todos
                       
                        </p>
                        <img height="16px" width="16px" src="../../Recursos/imagenes/administracion/todos.png">
                    </div>
                    <div style="float: right;" id="imgBuscar" class="linkIconoSuperior botonBuscar" onclick="abrirFiltro();">
                        <p>
                            Filtrar
                       
                        </p>
                        <img height="16px" width="16px" src="../../Recursos/imagenes/administracion/buscar24x24.png">
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <div id="divListado" class="centrar listadoGeneral">
                    </div>
                </td>
            </tr>
        </table>
    </div>

    <div style="display: none">
        <div class="tbBorde fancyNormal" id="Divsolicitud">
            <%--style="width: 500px"--%>
            <table style="width: 480px" class="centrar">
                <tr>
                    <td align="center" colspan="5">
                        <span class="tituloForm">SOLICITAR PRESTAMO</span>
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <div>
                            <table>
                                <tr>
                                    <td>Fecha Solicitud
                                    </td>
                                    <td>
                                        <input type="text" id="txtfechasolicitud" class="campoTexto campoMayuscula" placeholder="DD/MM/AAAA" style="width: 120px" />
                                    </td>
                                    <td></td>
                                    <td></td>

                                    <td>Tipo de Libro
                                      </td>
                                    <td>
                                        <select id="selTipoLibro" class=" select">
                                            <option value="-1">-- SELECCIONE --</option>
                                        </select>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>

                <tr>
                    <td colspan="5">
                        <div>
                            <table>
                                <tr>
                                    <td>Código Libro
                                    </td>
                                    <td>
                                        <input type="text" id="txtcodigo" class=" campoMayuscula campoTextoCorto" />
                                        <img width="25px" src="../../Recursos/imagenes/administracion/buscar.png" title="Buscar"
                                            alt="Buscar" class="imgAdminPequenia" onclick="buscarLibro()" style="cursor: pointer;" />
                                        <img width="25px" src="../../Recursos/imagenes/administracion/validar.png" title="Buscar"
                                            alt="Buscar" class="imgAdminPequenia" onclick="ValidarLibro()" style="cursor: pointer;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Nombre Libro
                                      </td>
                                    <td>
                                        <input type="text" id="txtNombre" class="campoTextoMedio campoMayuscula"
                                            onkeypress="return vdireccion(event);" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <hr />
                    </td>
                </tr>

                <tr>
                    <td colspan="5" align="center">
                        <table class="centrar" border="0">
                            <tr>
                                <td align="center">
                                    <span>Guardar</span><br />
                                    <img src="../../Recursos/imagenes/administracion/guardar.png" onclick="GuardarCitaMedica()"
                                        alt="Cancelar" class="imgAdmin" />
                                </td>
                                <td align="center">
                                    <span>Cancelar</span><br />
                                    <img src="../../Recursos/imagenes/administracion/cancelar.png" onclick="cerrarFancy()"
                                        alt="Cancelar" class="imgAdmin" />
                                </td>
                            </tr>
                            <tr>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <%--DIV PARA EL FILTRO--%>
    <div style="display: none;" style="width: 490px">
        <div id="DivFiltro" style="width: 500px">
            <input type="text" id="Text4" class="ocultar" />
            <table class="tbBorde" style="width: 490px">
                <tr>
                    <td align="center" class="centrar" colspan="2">
                        <span class="tituloForm">FILTRO SOLICITUD DE PRESTAMO</span>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <br />
                    </td>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <div>
                            <table>
                                <tr>
                                    <td>Fecha Solicitud
                                    </td>
                                    <td>
                                        <input type="text" id="txtfechasolicitudFiltro" class="campoTexto campoMayuscula" placeholder="DD/MM/AAAA" style="width: 120px" />
                                    </td>
                                    <td></td>
                                    <td></td>

                                    <td>Tipo de Libro
                                      </td>
                                    <td>
                                        <select id="selTipoLibroFiltro" class=" select">
                                            <option value="-1">-- SELECCIONE --</option>
                                        </select>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <table class="centrar">
                            <tr>
                                <td align="center">
                                    <span class="leyenda ">Filtrar</span><br />
                                    <img src="../../Recursos/imagenes/administracion/listar.png" title="Listar" alt="Listar"
                                        class="imgAdmin" onclick="listarSolicitudes(1);" />
                                </td>
                                <td align="center">
                                    <span>Cancelar</span><br />
                                    <img src="../../Recursos/imagenes/administracion/cancelar.png" onclick="cerrarFancy()"
                                        alt="Cancelar" class="imgAdmin" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>

      <%-- FANCY BUSCAR LIBROS --%>
    <div style="display: none;">
        <div class="tbBorde" id="Divlibros" style="width: 666px;">
            <table class="centrar responsive">
                <tr>
                    <td colspan="2" align="center">
                        <span class="tituloForm">LIBROS DISPONIBLES</span>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <div style="float: right;" id="Div2" class="linkIconoSuperior botonTodos" onclick="vertodosLibros();">
                            <p>
                                Ver Todos
                            </p>
                            <img height="16px" width="16px" src="../../Recursos/imagenes/administracion/todos.png" />
                        </div>
                        <div style="float: right;" id="Div3" class="linkIconoSuperior botonBuscar ocultar"
                            onclick="abrirFiltrolibros();">
                            <p>
                                Filtrar
                            </p>
                            <img height="16px" width="16px" src="../../Recursos/imagenes/administracion/buscar24x24.png" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <div id="divlistadolibros" class="centrar listadoGeneral">
                        </div>
                    </td>
                </tr>
                <tr align="center">
                    <td align="center">
                        <div align="center">
                            <span class="leyenda">Cancelar</span><br />
                            <img src="../../Recursos/imagenes/administracion/cancelar.png" alt="Cancelar" class="imgAdmin"
                                onclick="OpenFancy('#Divsolicitud');" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>

        <%--DIV PARA EL FILTRO LIBROS--%>
    <div style="display: none;" style="width: 490px">
        <div id="DivFiltrolibros" style="width: 500px">
            <input type="text"  class="ocultar" />
            <table class="tbBorde" style="width: 490px">
                <tr>
                    <td align="center" class="centrar" colspan="2">
                        <span class="tituloForm">FILTRO LIBROS</span>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <br />
                    </td>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <div>
                            <table>
                                <tr>
                                    <td>Código Libro
                                    </td>
                                    <td>
                                        <input type="text" id="txtcodfiltro" class="campoTexto campoMayuscula" placeholder="DD/MM/AAAA" style="width: 120px" />
                                    </td>
                                    <td></td>
                                    <td></td>

                                    <td>Nombre
                                      </td>
                                     <td>
                                        <input type="text" id="txtnombrefiltro" class="campoTexto campoMayuscula" placeholder="DD/MM/AAAA" style="width: 120px" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <table class="centrar">
                            <tr>
                                <td align="center">
                                    <span class="leyenda ">Filtrar</span><br />
                                    <img src="../../Recursos/imagenes/administracion/listar.png" title="Listar" alt="Listar"
                                        class="imgAdmin" onclick="listarSolicitudes(1);" />
                                </td>
                                <td align="center">
                                    <span>Cancelar</span><br />
                                    <img src="../../Recursos/imagenes/administracion/cancelar.png" onclick="cerrarFancy()"
                                        alt="Cancelar" class="imgAdmin" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
