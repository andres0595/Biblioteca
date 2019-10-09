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
                    <td align="center" colspan="6">
                        <span class="tituloForm">SOLICITAR PRESTAMO</span>
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                      <td>
                        Fecha Solicitud
                    </td>
                    <td>
                        <input type="text" id="txtfechasolicitud" class="campoTexto campoMayuscula obligatorio" placeHolder="DD/MM/AAAA"/>
                    </td>
                    <td>
                        Tipo de Libro
                    </td>
                    <td>
                        <select id="selTipoLibro" class=" select obligatorio">
                            <option value="-1">-- SELECCIONE --</option>
                        </select>
                    </td>
                </tr>              
                <tr>
                    <td>
                        Nombre:
                    </td>
                    <td>
                        <input type="text" id="txtNombre" class="campoTextoLargo campoMayuscula obligatorio"
                            onkeypress="return vdireccion(event);" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center">
                        <hr />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center">
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
</asp:Content>
