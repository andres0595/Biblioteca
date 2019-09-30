<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/master/pagina_maestra.Master"
    AutoEventWireup="true" CodeBehind="rol.aspx.cs" Inherits="Inicial.Vista.administracion.rol" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="server">
    .:: Rol ::.
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenidoSistema" runat="server">
    <%--fancybox CSS --%>
    <link rel="stylesheet" type="text/css" href="../../Recursos/js/fancybox/jquery.fancybox-1.3.4.css"
        media="screen" />
    <%--fancybox JS --%>
    <script type="text/javascript" src="../../Recursos/js/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../../Recursos/js/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    <script src="../../Recursos/js/ajax/rol.js" type="text/javascript"></script>
    <script src="../../Recursos/js/ajax/permisos_rol.js" type="text/javascript"></script>
    <span class="ocultar" id="idMenuForm">0.1</span>
    <input type="text" id="idEscondido" class="ocultar" />
    <div id="contenedorUsuario" class="tbBorde">
        <table class="centrar responsive">
            <tr>
                <td colspan="2" align="center">
                    <span class="tituloForm">ADMINISTRACI&Oacute;N DE LOS ROLES</span>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <div style="float: left;" id="imgGuardar" class="linkIconoSuperior" onclick="nuevoRol();">
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
                    <div style="float: right;" id="imgBuscar" class="linkIconoSuperior" onclick="abrirFiltro();">
                        <p>
                            Filtrar</p>
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
    <!--Formulario para Nuevo Rol-->
    <div style="display: none;">
        <div id="divNuevoRol" style="overflow: hidden;" class="fancyNormal tbBorde">
            <table class="centrar">
                <tr>
                    <td colspan="2" align="center">
                        <span class="tituloForm">ADMINISTRACI&Oacute;N DE LOS ROLES</span>
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
                                        Nombre del Rol
                                    </td>
                                    <td>
                                        <input type="text" id="txtRol" onkeypress="return letras(event);" class="campoTextoLargo campoMayuscula obligatorio"
                                            maxlength="50" />&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Nivel Acceso
                                    </td>
                                    <td>
                                        <select id="selNivelAcceso" class="selectObligatorio">                                      
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        Descripci&oacute;n
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <textarea rows="1" cols="6" id="taDescripcion" class="descripcion centrar campoMayuscula"
                                            onkeypress="return vdireccion(event)"></textarea>
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
                                            class="imgAdmin" onclick="guardaRol()" /></div>
                                </td>
                                <td>
                                    <div>
                                        <span class="leyenda">Cancelar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/cancelar.png" title="Cancelar" alt="Cancelar"
                                            class="imgAdmin" onclick="cancelaFormRol()" /></div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!--Formulario para ver los Permisos de un Rol-->
    <div style="display: none">
        <div id="permisosRol" class="fancyNormal tbBorde">
            <table class="centrar">
                <tr>
                    <td colspan="2" align="center">
                        <span class="tituloForm">ADMINISTRACI&Oacute;N DE PERMISOS DE ROLES</span>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div id="menusDisponibles" class="listadoGeneral" style="text-align: left;">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <table>
                            <tr>
                                <td>
                                    <div>
                                        <span class="leyenda">Guardar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/guardar.png" title="Guardar" alt="Guardar"
                                            class="imgAdmin" onclick="guardaPermisosRol2()" /></div>
                                </td>
                                <td>
                                    <div>
                                        <span class="leyenda">Cancelar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/cancelar.png" title="Cancelar" alt="Cancelar"
                                            class="imgAdmin" onclick="cancelarFormPermisos()" /></div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div style="display: none">
        <div id="accesosRol" class="fancyNormal tbBorde">
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
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <table>
                            <tr>
                                <td>
                                    <div>
                                        <span class="leyenda">Cancelar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/cancelar.png" title="Cancelar" alt="Cancelar"
                                            class="imgAdmin" onclick="cancelarFormPermisos()" /></div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div style="display: none">
        <div id="formVariables" class="fancyNormal tbBorde">
            <table class="centrar">
                <tr>
                    <td colspan="2" align="center">
                        <span class="tituloForm" id="tituloVaribales">DEPARTAMENTOS Y CIUDADES</span>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div id="divListaOpcionesVars" class="listadoGeneral" style="text-align: left;">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <table>
                            <tr>
                                <td>
                                    <div>
                                        <span class="leyenda">Guardar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/guardar.png" title="Guardar" alt="Guardar"
                                            class="imgAdmin" onclick="guardaVariablesAcceso()" /></div>
                                </td>
                                <td>
                                    <div>
                                        <span class="leyenda">Cancelar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/cancelar.png" title="Cancelar" alt="Cancelar"
                                            class="imgAdmin" onclick="cancelarFormVariables()" /></div>
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
                        <span class="tituloForm">FILTRAR ROLES</span>
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
                                        Nombre del Rol
                                    </td>
                                    <td>
                                        <input type="text" id="txtRolFiltro" onkeypress="return letras(event);" class="campoTextoLargo campoMayuscula" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Nivel Acceso
                                    </td>
                                    <td>
                                        <select id="selNivelAccesoFiltro" class="select">
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
                                            class="imgAdmin" onclick="listaRoles(1)" /></div>
                                </td>
                                <td>
                                    <div>
                                        <span class="leyenda">Cancelar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/cancelar.png" title="Cancelar" alt="Cancelar"
                                            class="imgAdmin" onclick="cancelaFormRol()" /></div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <div style="display: none">
        <div id="divDetalle" style="width: 800px">
            <table class="centrar responsive" style="width: 750px">
                <tr>
                    <td colspan="2" align="center">
                        <span class="tituloForm">DETALLE ROL</span>
                        <br />
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
                        <input type="checkbox" id="chkdetalleGeneral" onclick="verDetalle(this.id, this.checked);" />
                       <b>Información general</b> 
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <div id="divDetalleRol" class="centrar listadoGeneral tbBorde" style="display: none" onclick="verDet">
                            <div id="divTablaDetalleRol">
                            </div>                            
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="checkbox" id="chkdetallePermisos" onclick="verDetalle(this.id, this.checked);" />
                        <b>Permisos</b> 
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <div id="divTablaDetallePermisos" class="centrar listadoGeneral tbBorde" style="display: none">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="checkbox" id="chkdetalleUsuarios" onclick="verDetalle(this.id, this.checked);" />
                        <b>Usuarios</b>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <div id="divTablaDetalleUsuarios" class="centrar listadoGeneral tbBorde" style="display: none">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="checkbox" id="chkdetalleRegionales" onclick="verDetalle(this.id, this.checked);" />
                        <b>Acceso</b>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <div id="divTablaDetalleRegionales" class="centrar listadoGeneral tbBorde" style="display: none">
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
