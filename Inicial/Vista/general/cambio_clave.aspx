<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/master/pagina_maestra.Master"
    AutoEventWireup="true" CodeBehind="cambio_clave.aspx.cs" Inherits="Inicial.Vista.general.cambio_clave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="server">
    .:: Cambiar Clave ::.
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenidoSistema" runat="server">
    <script src="../../Recursos/js/ajax/cambiarClave.js" type="text/javascript"></script>
    <span class="ocultar" id="idMenuForm">-3</span>
    <div class="tbBorde">
        <table class="centrar responsive">
            <tr>
                <td colspan="2" align="center">
                    <span class="tituloForm">CAMBIAR CLAVE</span><br />
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    Clave Actual
                </td>
                <td>
                    <input type="password" id="claveActual" class="campoTexto obligatorio" />
                </td>
            </tr>
            <tr>
                <td>
                    Nueva Clave
                </td>
                <td>
                    <input type="password" id="nuevaClave1" class="campoTexto obligatorio" />
                </td>
            </tr>
            <tr>
                <td>
                    Confirmar Clave
                </td>
                <td>
                    <input type="password" id="nuevaClave2" class="campoTexto obligatorio" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
            <tr>
                <td colspan="2" align="center">
                    <div>
                        <span class="leyenda">Cambiar Clave</span><br />
                        <img src="../../Recursos/imagenes/administracion/cambioClave.png" alt="Cambiar Clave"
                            title="Cambiar Clave" class="imgAdmin" onclick="cambiaClaveUsuario()" /></div>
                </td>
            </tr>
            </tr>
        </table>
        <br />
        <br />
    </div>
</asp:Content>
