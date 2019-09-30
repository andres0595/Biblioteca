<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/master/pagina_maestra.Master"
    AutoEventWireup="true" CodeBehind="aud_usuario.aspx.cs" Inherits="Inicial.Vista.administracion.aud_usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="server">
    .:: Auditoria Usuario ::.
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenidoSistema" runat="server">
    <%--fancybox CSS --%>
    <link rel="stylesheet" type="text/css" href="../../Recursos/js/fancybox/jquery.fancybox-1.3.4.css"
        media="screen" />
    <%--fancybox JS --%>
    <script type="text/javascript" src="../../Recursos/js/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../../Recursos/js/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    <script src="../../Recursos/js/ajax/auditoria.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            listaAuditoriaUsuario(1);
        });
    </script>
    <span class="ocultar" id="idMenuForm">0.102</span>
    <div class="tbBorde">
        <input type="text" class="ocultar" id="idEscondido" />
        <table class="centrar responsive">
            <tr>
                <td colspan="6" align="center">
                    <span class="tituloForm">AUDITOR&Iacute;A USUARIOS</span><br />
                    <br />
                    Lista las últimas acciones realizadas con los datos de Usuarios
                </td>
            </tr>
            <tr>
                <td colspan="6" align="right">
                    <div style="float: right;" id="imgTodos" class="linkIconoSuperior botonTodos" onclick="verTodos(2);">
                        <p>
                            Ver Todos</p>
                        <img height="16px" width="16px" src="../../Recursos/imagenes/administracion/todos.png">
                    </div>
                    <div style="float: right;" id="imgBuscar" class="linkIconoSuperior botonBuscar ocultar"
                        onclick="abrirFiltro(2);">
                        <p>
                            Filtrar</p>
                        <img height="16px" width="16px" src="../../Recursos/imagenes/administracion/buscar24x24.png">
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <div id="divListadoAuditoria" class="listadoGeneral">
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    &nbsp;
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
                        <span class="tituloForm">FILTRAR AUDITOR&Iacute;A DE USUARIOS</span>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div>
                            <table class="centrar" width="520px">
                                <tr>
                                    <td>
                                        Responsable
                                    </td>
                                    <td>
                                        <input type="text" id="txtResponsable" onkeypress="return letras(event);" class="campoTextoLargo campoMayuscula" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Acci&oacute;n
                                    </td>
                                    <td>
                                        <input type="text" id="txtAccion" onkeypress="return letras(event);" class="campoTextoLargo campoMayuscula" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Usuario
                                    </td>
                                    <td>
                                        <input type="text" id="txtUsuario" onkeypress="return letras(event);" class="campoTextoLargo campoMayuscula" />
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
                                            class="imgAdmin" onclick="listaAuditoriaUsuario(1)" /></div>
                                </td>
                                <td>
                                    <div>
                                        <span class="leyenda">Cancelar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/cancelar.png" title="Cancelar" alt="Cancelar"
                                            class="imgAdmin" onclick="cancelaForm()" /></div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
