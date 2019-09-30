<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/master/pagina_director.Master"
    AutoEventWireup="true" CodeBehind="elegirEmpresa.aspx.cs" Inherits="Inicial.Vista.general.elegirEmpresa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenidoSistema" runat="server">
    <head>
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
        <script src="../../Recursos/js/ajax/elegirEmpresas.js" type="text/javascript"></script>
    </head>
    <span class="ocultar" id="idMenuForm">7.2</span>
    <input type="text" id="idEscondido" class="ocultar" />
    <div id="contenedorEmpresa" class="centrar" style="margin: auto;">
        <table class="centrar responsive">
            <tr>
                <td colspan="6" align="center">
                    <span class="tituloForm">ELEGIR EMPRESA</span>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <div style="float: left;" id="imgGuardar" class="linkIconoSuperior botonGuardar ocultar"
                        onclick="nuevaArea();">
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
                <td colspan="10">
                    <div id="listadoEmpresas" class="centrar listadoGeneral">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <%--DIV PARA EL FILTRO--%>
    <div style="display: none;">
        <div id="filtro" class="fancyEliminar tbBorde">
            <table cellpadding="2" class="centrar">
                <tr>
                    <td align="center" class="centrar" colspan="2">
                        <span class="tituloForm">FILTRO DE EMPRESAS</span>
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
                        Nit:
                    </td>
                    <td>
                        <input type="text" class="campoTexto campoMayuscula" id="txtNitFiltro" onkeypress="return numerosguion(event);"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        Nombre:
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
                                        class="imgAdmin" onclick="listaEmpresa(1);$.fancybox.close()" />
                                </td>
                                <td align="center">
                                    <span>Cancelar</span><br />
                                    <img src="../../Recursos/imagenes/administracion/cancelar.png" onclick="$.fancybox.close()"
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
