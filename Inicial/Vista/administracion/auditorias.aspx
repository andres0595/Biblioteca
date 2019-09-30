<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/master/pagina_maestra.Master" AutoEventWireup="true" CodeBehind="auditorias.aspx.cs" Inherits="Inicial.Vista.administracion.auditorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="server">
    .::Auditorias::.
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenidoSistema" runat="server">
    <%--fancybox CSS --%>
    <link rel="stylesheet" type="text/css" href="../../Recursos/js/fancybox/jquery.fancybox-1.3.4.css"
        media="screen" />
    <%--fancybox JS --%>
    <script type="text/javascript" src="../../Recursos/js/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../../Recursos/js/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    <script src="../../Recursos/js/ajax/auditoriaNueva.js" type="text/javascript"></script>

    <script src="../../Recursos/js/calendario/jquery-ui-1.8.7.custom.js" type="text/javascript"></script>
    <script src="../../Recursos/js/calendario/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Recursos/js/calendario/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="../../Recursos/js/calendario/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Recursos/js/pestanas/jquery.ui.tabs.js" type="text/javascript"></script>

    <link href="../../Recursos/css/calendario/calendario.css" rel="stylesheet" type="text/css" />
    <link href="../../Recursos/css/calendario/jquery.ui.core.css" rel="stylesheet" type="text/css" />
    <link href="../../Recursos/css/calendario/jquery.ui.theme.css" rel="stylesheet" type="text/css" />

    <span class="ocultar" id="idMenuForm">0.7</span>
    <input type="text" id="idEscondido" class="ocultar" />
    <div id="contenedorUsuario" class="tbBorde">
        <table class="centrar responsive">
            <tr>
                <td colspan="2" align="center">
                    <span class="tituloForm">AUDITORIA</span>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left"></td>
                <td align="right">
                    <div style="float: right;" id="imgTodos" class="linkIconoSuperior botonTodos" onclick="limpiarFiltro();listaAuditorias(1);">
                        <p>
                            Ver Todos
                        </p>
                        <img height="16px" width="16px" src="../../Recursos/imagenes/administracion/todos.png">
                    </div>
                    <div style="float: right;" id="imgBuscar" class="linkIconoSuperior botonBuscar ocultar"
                        onclick="abrirFiltro();">
                        <p>
                            Filtrar
                        </p>
                        <img height="16px" width="16px" src="../../Recursos/imagenes/administracion/buscar24x24.png">
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <div id="listadoRoles" class="centrar listadoGeneral">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <!--Formulario para Filtrar-->
    <div style="display: none;">
        <div id="divFiltro" style="overflow: hidden;" class="fancyNormal tbBorde">
            <table class="centrar">
                <tr>
                    <td colspan="2" align="center">
                        <span class="tituloForm">FILTRAR AUDITORIA</span>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div>
                            <table class="centrar" width="520px">
                                <tr>
                                    <td>Fecha Inicio
                                    </td>
                                    <td>
                                        <input type="text" id="txtFechaInicio" class="campoTexto campoMayuscula" placeholder="DD/MM/AAAA" />
                                    </td>
                                    <td>Fecha Fin
                                    </td>
                                    <td>
                                        <input type="text" id="txtFechaFin" class="campoTexto campoMayuscula" placeholder="DD/MM/AAAA" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Objeto
                                    </td>
                                    <td>
                                        <input type="text" id="txtObjeto" onkeypress="return letras(event);" class="campoTexto campoMayuscula" />
                                    </td>
                                    <td>Responsable
                                    </td>
                                    <td>
                                        <input type="text" id="txtResponable" onkeypress="return letras(event);" class="campoTexto campoMayuscula" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Acción:
                                    </td>
                                    <td>
                                        <select class="select" id="selAccion">
                                             <option value="-1">--SELECCIONE--</option>
                                        </select>
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
                                        <span class="leyenda">Listar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/listar.png" title="Listar" alt="Listar"
                                            class="imgAdmin" onclick="listaAuditorias(1);$.fancybox.close();" />
                                    </div>
                                </td>
                                <td>
                                    <div>
                                        <span class="leyenda">Cancelar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/cancelar.png" title="Cancelar" alt="Cancelar"
                                            class="imgAdmin" onclick="$.fancybox.close();" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <%--DIV PARA EL DETALLE--%>
    <div style="display: none">
        <div id="divDetalle">
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
                        <br />
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table>
                            <tr>
                                <td align="center">
                                    <span>Cancelar</span><br />
                                    <img src="../../Recursos/imagenes/administracion/cancelar.png" onclick="$.fancybox.close();"
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
