<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/master/pagina_maestra.Master"
    AutoEventWireup="true" CodeBehind="usuario.aspx.cs" Inherits="Inicial.Vista.administracion.usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="server">
    .:: Usuarios ::.
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenidoSistema" runat="server">
    <%-- Librerias para el select multiple --%>

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
    <!-- Ubicacion del JS para la vista actual -->
    <script src="../../Recursos/js/ajax/usuario.js" type="text/javascript"></script>
    <link href="../../Recursos/js/docsupport/prism.css" rel="stylesheet" />
    <link href="../../Recursos/js/docsupport/chosen.css" rel="stylesheet" />
    <style type="text/css" media="all">
        /* fix rtl for demo */
        .chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
    </style>
    <script src="../../Recursos/js/docsupport/chosen.jquery.js" type="text/javascript"></script>
    <script src="../../Recursos/js/docsupport/prism.js" type="text/javascript"></script>


    <span class="ocultar" id="idMenuForm">0.6</span>
    <div id="Div1" class="tbBorde">
        <table class="centrar responsive">
            <tr>
                <td colspan="2" align="center">
                    <span class="tituloForm">ADMINISTRACI&Oacute;N DE LOS USUARIOS</span>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <div style="float: left;" id="imgGuardar" class="linkIconoSuperior botonGuardar"
                        onclick="nuevoUsuario();">
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
                    <div id="divListadoUsuarios" class="centrar listadoGeneral">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div style="display: none;">
        <div id="contenedorUsuario" class="fancyNormal tbBorde">
            <table class="centrar">
                <tr>
                    <td colspan="6" align="center">
                        <span>MEN&Uacute;S PERMITIDOS</span>
                        <br />
                        <br />
                        <div class="listadoGeneral">
                            <div id="menusDisponibles" style="text-align: left;">
                            </div>
                        </div>
                        <br />
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="6">
                        <table class="centrar">
                            <tr>
                                <td>
                                    <div>
                                        <span class="leyenda">Guardar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/guardar.png" title="Guardar" alt="Guardar"
                                            class="imgAdmin" onclick="editaPermisoUsuario()" />
                                    </div>
                                </td>
                                <td>
                                    <div>
                                        <span class="leyenda">Cancelar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/cancelar.png" title="Cancelar" alt="Cancelar"
                                            class="imgAdmin" onclick="cancelaFormRol()" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="listadoUsuario" class="centrar">
    </div>
    <!--Formulario para Nuevo Usuario-->
    <div style="display: none;">
        <div id="divNuevoUsuario" style="overflow: hidden;" class="fancyNormal tbBorde">
            <table class="centrar">
                <tr>
                    <td colspan="4" align="center">
                        <span class="tituloForm">ADMINISTRACI&Oacute;N DE LOS USUARIOS</span>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <iframe id="iframeFoto" name="iframeFoto" src="../../Controlador/ctlCargaFoto.aspx"
                            class="iframeHojaVida" runat="server" scrolling="no" width="270px" height="220px">
                        </iframe>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table>
                            <tr>
                                <td style="width: 100px">
                                    Nombre del usuario
                                </td>
                                <td colspan="3">
                                    <input type="text" id="txtNombreUsuario" class="campoTextoLargo campoMayuscula obligatorio"
                                        onkeypress="return letras(event)" />
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td>
                                    Usuario para login
                                </td>
                                <td colspan="3">
                                    <input type="text" id="txtUsuario" onkeypress="return letras(event);" class="campoTextoLargo campoMayuscula obligatorio" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Correo electr&oacute;nico
                                </td>
                                <td colspan="3">
                                    <input type="text" id="txtCorreo" class="campoTextoLargo campoMinuscula obligatorio" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Documento
                                </td>
                                <td>
                                    <input type="text" id="txtDocumento" onkeypress="return numeros(event);" class="campoTexto obligatorio"
                                        maxlength="12" />
                                </td>
                                <td>
                                    Tel&eacute;fono
                                </td>
                                <td>
                                    <input type="text" id="txtTelefono" onkeypress="return numeros(event);" class="campoTexto"
                                        maxlength="12" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Direcci&oacute;n
                                </td>
                                <td colspan="3">
                                    <input type="text" id="txtDireccion" class="campoTextoLargo campoMayuscula" onkeypress="return vdireccion(event)"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Departamento
                                </td>
                                <td style="width: 10px">
                                    <select id="SelDepto" class="select obligatorio" onchange="cargaArea();">
                                        <option value="-1">-- SELECCIONE --</option>
                                    </select>
                                </td>
                                <td>
                                    &Aacute;rea
                                </td>
                                <td style="width: 10px">
                                    <select id="selArea" class="select obligatorio" onchange="cargaCargo();">
                                        <option value="-1">-- SELECCIONE --</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Cargo
                                </td>
                                <td>
                                    <select id="selCargo" class="select obligatorio">
                                        <option value="-1">-- SELECCIONE --</option>
                                    </select>
                                </td>
                                <%--                                <td>
                                    Grupo
                                </td>
                                <td>
                                    <select id="selGrupo" class="select">
                                        <option value="-1">-- SELECCIONE --</option>
                                    </select>
                                </td>--%>
                                <tr>
                                    <td>
                                        Grupo
                                    </td>
                                    <td colspan="10">
                                       <%-- <select id="selGrupo" data-placeholder="Seleccione Grupos..." size="400px" class="chosen-select"
                                            multiple style="width: 350px;">
                                            <option value=""></option>
                                        </select>--%>
                                       <select id="selGrupo" data-placeholder="Seleccione Grupos..." size="505px"
                                             class="chosen-select" multiple style="width: 505px;" >
                                            <option value=""></option>
                                        </select>
                                    </td>
                                </tr>
                            </tr>
                            <tr>
                                <td>
                                    Activar usuario
                                </td>
                                <td>
                                    <input type="checkbox" checked="checked" id="chkEstadoUsuario" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center">
                        <hr />
                        <span>ROLES PERMITIDOS</span>
                        <br />
                        <br />
                        <div id="rolesDisponibles" class="listadoGeneral">
                        </div>
                        <br />
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <table class="centrar">
                            <tr>
                                <td colspan="2">
                                    <div>
                                        <span class="leyenda">Guardar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/guardar.png" title="Guardar" alt="Guardar"
                                            class="imgAdmin" onclick="guardaUsuario()" />
                                    </div>
                                </td>
                                <td colspan="2">
                                    <div>
                                        <span class="leyenda">Cancelar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/cancelar.png" title="Cancelar" alt="Cancelar"
                                            class="imgAdmin" onclick="cancelaFormRol()" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!--Formulario para Filtrar-->
    <div style="display: none;">
        <div id="divFiltro" style="overflow: hidden;" class="fancyNormal tbBorde">
            <table class="centrar">
                <tr>
                    <td colspan="2" align="center">
                        <span class="tituloForm">FILTRAR USUARIOS</span>
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
                                        Nombre del Usuario
                                    </td>
                                    <td>
                                        <input type="text" id="txtNombreUsuarioFiltro" class="campoTextoLargo campoMayuscula" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Usuario para Login
                                    </td>
                                    <td>
                                        <input type="text" id="txtUsuarioFiltro" class="campoTextoLargo campoMayuscula" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Correo Electr&oacute;nico
                                    </td>
                                    <td>
                                        <input type="text" id="txtCorreoFiltro" class="campoTextoLargo campoMinuscula" />
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
                                            class="imgAdmin" onclick="listarUsuario(1);$.fancybox.close()" />
                                    </div>
                                </td>
                                <td>
                                    <div>
                                        <span class="leyenda">Cancelar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/cancelar.png" title="Cancelar" alt="Cancelar"
                                            class="imgAdmin" onclick="cancelaFormRol()" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <%--DIV PARA LAS VARIABLES DE SESION--%>
    <div style="display: none;">
        <div id="divAcceso" style="overflow: hidden;" class="fancyNormal tbBorde">
            <table class="centrar">
                <tr>
                    <td colspan="2" align="center">
                        <span class="tituloForm">VARIABLES DE ACCESO A LA INFORMACI&Oacute;N</span>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div id="divListadoVariables" class="listadoGeneral" style="text-align: left;">
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
                                        <span class="leyenda">Cancelar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/cancelar.png" title="Cancelar" alt="Cancelar"
                                            class="imgAdmin" onclick="cancelaFormRol()" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <%--DIV PARA LAS VARIABLES DE SESION--%>
    <div style="display: none;">
        <div id="divEmpresasVariablesAcceso" style="overflow: hidden;" class="fancyNormal tbBorde">
            <table class="centrar">
                <tr>
                    <td colspan="2" align="center">
                        <span class="tituloForm">ELEGIR EMPRESA</span>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div id="listadoEmpresaVariablesAcceso" class="listadoGeneral" style="text-align: left;">
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
                                        <span class="leyenda">Cancelar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/cancelar.png" title="Cancelar" alt="Cancelar"
                                            class="imgAdmin" onclick="cancelaFormRol()" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <%--DIV PARA LAS APLICACIONES--%>
    <div style="display: none;">
        <div id="divAplicacionesEmpresa" style="overflow: hidden;" class="fancyNormal tbBorde">
            <table class="centrar">
                <tr>
                    <td colspan="2" align="center">
                        <span class="tituloForm">ELEGIR APLICACIONES</span>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div id="listadoAplicacionesEmpresa" class="listadoGeneral" style="text-align: left;">
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
                                        <span class="leyenda">Cancelar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/cancelar.png" title="Cancelar" alt="Cancelar"
                                            class="imgAdmin" onclick="cancelaFormRol()" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!--Formulario para el detalle-->
    <div style="display: none;">
        <div id="divDetalle" style="overflow: hidden;" class="fancyNormal tbBorde">
            <table class="centrar">
                <tr>
                    <td colspan="2" align="center">
                        <span class="tituloForm">DETALLE</span>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="subtitulosDetalle">Informacion General</span>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <div id="listadoUsuariosDetalle" class="centrar listadoGeneral">
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
                                        <span class="leyenda">Cancelar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/cancelar.png" title="Cancelar" alt="Cancelar"
                                            class="imgAdmin" onclick="cancelaFormRol()" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
