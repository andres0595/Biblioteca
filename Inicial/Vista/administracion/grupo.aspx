<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/master/pagina_maestra.Master"
    AutoEventWireup="true" CodeBehind="grupo.aspx.cs" Inherits="Inicial.Vista.administracion.grupo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="server">
    .:: Grupos ::.
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenidoSistema" runat="server">
    <script src="../../Recursos/js/autocomplete/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Recursos/js/autocomplete/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Recursos/js/autocomplete/jquery.ui.position.js" type="text/javascript"></script>
    <script src="../../Recursos/js/autocomplete/jquery.ui.autocomplete.js" type="text/javascript"></script>
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
    <script src="../../Recursos/js/ajax/grupo.js" type="text/javascript"></script>
    <span class="ocultar" id="idMenuForm">2.6</span>
    <div id="Div1" class="tbBorde">
        <table class="centrar responsive">
            <tr>
                <td colspan="2" align="center">
                    <span class="tituloForm">GRUPOS DE TRABAJO</span>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <div style="float: left;" id="imgGuardar" class="linkIconoSuperior botonGuardar"
                        onclick="nuevoGrupo();">
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
                    <div id="divListadoGrupos" class="centrar listadoGeneral">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <!--Formulario para Nuevo Grupo-->
    <div style="display: none;">
        <div id="divNuevoGrupo" style="overflow: hidden;" class="fancyNormal tbBorde">
            <table class="centrar">
                <tr>
                    <td colspan="2" align="center">
                        <span class="tituloForm">CREACI&Oacute;N DE GRUPOS</span>
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
                                        Nombre del grupo:
                                        <input type="text" id="txtNombreGrupo" class="campoTextoLargo campoMayuscula obligatorio"
                                            onkeypress="return vdireccion(event)" maxlength="30" onkeyup="control(event, id)"/>
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
                                            onkeypress="return vdireccion(event)" rows="2" cols="9" ></textarea>
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
                                            class="imgAdmin" onclick="guardarGrupo()" /></div>
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
                                        onclick="borrarGrupo()" />
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
                        <span class="tituloForm">FILTRO DE GRUPOS</span>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        Nombre Grupo:
                    </td>
                    <td>
                        <input type="text" class="campoTexto campoMayuscula" id="txtNombreFiltroGrupo" />
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
                                        class="imgAdmin" onclick="verificarFiltro()" />
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
        <div id="divDetalleGrupo" style="min-width:400px;" class="centrar">
            <table class="centrar">
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
