<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/master/pagina_maestra.Master"
    AutoEventWireup="true" CodeBehind="Departamentos.aspx.cs" Inherits="Inicial.Vista.administracion.Departamentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="server">
    .:: Departamentos ::.
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenidoSistema" runat="server">
    <link href="../../Recursos/js/autocomplete/base/jquery.ui.all.css" rel="stylesheet"
        type="text/css" />
    <script src="../../Recursos/js/autocomplete/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Recursos/js/autocomplete/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Recursos/js/autocomplete/jquery.ui.position.js" type="text/javascript"></script>
    <script src="../../Recursos/js/autocomplete/jquery.ui.autocomplete.js" type="text/javascript"></script>
    <link href="../../Recursos/css/calendario/calendario.css" rel="stylesheet" type="text/css" />
    <link href="../../Recursos/css/calendario/jquery.ui.core.css" rel="stylesheet" type="text/css" />
    <link href="../../Recursos/css/calendario/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../Recursos/css/calendario/calendario.css" rel="stylesheet" type="text/css" />
    <link href="../../Recursos/css/calendario/jquery.ui.datepicker.css" rel="stylesheet"
        type="text/css" />
    <link href="../../Recursos/css/pestanas/demos.css" rel="stylesheet" type="text/css" />
    <link href="../../Recursos/css/pestanas/jquery.ui.tabs.css" rel="stylesheet" type="text/css" />
    <script src="../../Recursos/js/calendario/jquery-ui-1.8.7.custom.js" type="text/javascript"></script>
    <script src="../../Recursos/js/calendario/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Recursos/js/calendario/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="../../Recursos/js/calendario/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Recursos/js/pestanas/jquery.ui.tabs.js" type="text/javascript"></script>
    <%--fancybox CSS --%>
    <link rel="stylesheet" type="text/css" href="../../Recursos/js/fancybox/jquery.fancybox-1.3.4.css"
        media="screen" />
    <%--fancybox JS --%>
    <script type="text/javascript" src="../../Recursos/js/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../../Recursos/js/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    <script src="../../Recursos/js/ajax/digitoVerificacion.js" type="text/javascript"></script>
    <script src="../../Recursos/js/ajax/departamentos.js" type="text/javascript"></script>
    <span class="ocultar" id="idMenuForm">2.3</span>
    <div id="Div1" class="tbBorde">
        <table class="centrar responsive">
            <tr>
                <td colspan="2" align="center">
                    <span class="tituloForm">DEPARTAMENTOS DE LA EMPRESA</span>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <div style="float: left;" id="imgGuardar" class="linkIconoSuperior botonGuardar"
                        onclick="nuevoDepartamento();">
                        <img height="16px" width="16px" src="../../Recursos/imagenes/administracion/agregar_24x24.png">
                        <p>
                            Nuevo</p>
                    </div>
                </td>
                <td align="right">
                    <div style="float: right;" id="imgTodos" class="linkIconoSuperior botonTodos" onclick="verTodos();">
                        <p>
                            Ver Todos</p>
                        <img height="16px" width="16px" src="../../Recursos/imagenes/administracion/todos.png">
                    </div>
                    <div style="float: right;" id="imgBuscar" class="linkIconoSuperior botonBuscar" onclick="abrirFiltro();">
                        <p>
                            Filtrar</p>
                        <img height="16px" width="16px" src="../../Recursos/imagenes/administracion/buscar24x24.png">
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <div id="divListadoDepartamentos" class="centrar listadoGeneral">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <!--Formulario para Nuevo Usuario-->
    <div style="display: none;">
        <div id="divNuevoDepartamento" style="overflow: hidden;" class="fancyNormal tbBorde">
            <table class="centrar">
                <tr>
                    <td colspan="2" align="center">
                        <span class="tituloForm">CREACI&Oacute;N DE DEPARTAMENTOS</span>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <table class="centrar">
                                <tr>
                                    <td>
                                        Nombre del
                                        <br />
                                        Departamento:
                                        <input type="text" id="txtNombreDepartamento" class="campoTextoLargo campoMayuscula obligatorio"
                                            maxlength="30" onkeypress="return vdireccion(event)" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Descripci&oacute;n:
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="10">
                                        <textarea class="descripcionMediana campoMayuscula descripcion" id="taDescripcion"
                                            onkeypress="return vdireccion(event)" rows="2" cols="9"></textarea>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <table class="centrar">
                            <tr>
                                <td>
                                    <div>
                                        <span class="leyenda">Guardar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/guardar.png" title="Guardar" alt="Guardar"
                                            class="imgAdmin" onclick="guardarDepartamento()" /></div>
                                </td>
                                <td>
                                    <div>
                                        <span class="leyenda">Cancelar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/cancelar.png" title="Cancelar" alt="Cancelar"
                                            class="imgAdmin" onclick="cerrarFancy()" /></div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <%--    crea el fancy de confirmacion para eliminar  --%>
    <div style="display: none">
        <div id="divConfirmaEliminar" class="fancyEliminar">
            <table class="centrar">
                <tr>
                    <td class="encabezado">
                        <span>Mensaje</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>&#191;CONFIRMA QUE DESEA ELIMINAR ESTA INFORMACI&Oacute;N &#63;</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table>
                            <tr>
                                <td>
                                    <span class="leyenda">Aceptar</span><br />
                                    <img src="../../Recursos/imagenes/administracion/aceptar.png" alt="Aceptar" class="imgAdmin"
                                        onclick="borrarDepartamento()" />
                                </td>
                                <td>
                                    <span class="leyenda">Cancelar</span><br />
                                    <img src="../../Recursos/imagenes/administracion/cancelar.png" alt="Cancelar" class="imgAdmin"
                                        onclick="cerrarFancy()" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <%--DIV PARA EL FILTRO--%>
    <div style="display: none;">
        <div id="filtro">
            <input type="text" id="Text4" class="ocultar" />
            <table cellpadding="5" class="tbBorde listadoGeneralMedio">
                <tr>
                    <td align="center" class="centrar" colspan="2">
                        <span class="tituloForm">FILTRO DE DEPARTAMENTOS</span>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        Nombre Departamento:
                    </td>
                    <td>
                        <input type="text" class="campoTexto campoMayuscula" id="txtNombreFiltro" />
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
                                        class="imgAdmin" onclick="verificarFiltro();$.fancybox.close();" />
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
    <%--    crea el fancy Detalles  --%>
    <div style="display: none">
        <div id="divDetalleDepartamento" style="min-width: 385px;" class="centrar">
            <table>
                <tr>
                    <td colspan="10" align="center">
                        <span>DETALLES </span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <div id="divTablaDetalle" class="centrar listadoGeneral">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <div id="divTablaCargos" class="centrar listadoGeneral">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table>
                            <tr>
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
