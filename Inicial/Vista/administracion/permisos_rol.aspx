<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/master/pagina_maestra.Master"
    AutoEventWireup="true" CodeBehind="permisos_rol.aspx.cs" Inherits="Inicial.Vista.administracion.permisos_rol" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="server">
    .:: Permisos Rol ::.
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenidoSistema" runat="server">
    <span class="ocultar" id="idMenuForm">0.3</span>
    <script src="../../Recursos/js/ajax/permisos_rol.js" type="text/javascript"></script>
    <input type="text" id="idEscondido" class="ocultar" />
    <div id="contenedorUsuario" class="tbBorde">
        <table class="centrar responsive">
            <tr>
                <td colspan="2" align="center">
                    <span class="tituloForm">ADMINISTRACI&Oacute;N DE PERMISOS DE ROLES</span>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    Rol
                    <select id="selRol" class="selectObligatorio" onchange="selectRol(this);">                        
                    </select>
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
                    <div id="imgGuardar" class="ocultar">
                        <span class="leyenda">Guardar</span><br />
                        <img src="../../Recursos/imagenes/administracion/guardar.png" title="Guardar" alt="Guardar"
                            class="imgAdmin" onclick="guardaPermisosRol()" /></div>
                </td>
            </tr>
        </table>
    </div>    
</asp:Content>
