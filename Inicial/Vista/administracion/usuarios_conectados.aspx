<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/master/pagina_maestra.Master"
    AutoEventWireup="true" CodeBehind="usuarios_conectados.aspx.cs" Inherits="Inicial.Vista.administracion.usuarios_conectados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="server">
    .:: Usuarios Conectados ::. 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenidoSistema" runat="server">
    <%--fancybox CSS --%>
    <link rel="stylesheet" type="text/css" href="../../Recursos/js/fancybox/jquery.fancybox-1.3.4.css"
        media="screen" />
    <%--fancybox JS --%>
    <script type="text/javascript" src="../../Recursos/js/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../../Recursos/js/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    <script src="../../Recursos/js/ajax/usuarios_conectados.js" type="text/javascript"></script>
    <span class="ocultar" id="idMenuForm">0.8</span>
    <div class="tbBorde">
        <input type="text" class="ocultar" id="idEscondido" />
        <table class="centrar responsive">
            <tr>
                <td colspan="6" align="center">
                    <span class="tituloForm">USUARIOS CONECTADOS</span><br />
                    <br />
                    Listado de los usuarios conectados en este momento.
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <div id="divListadoAuditoria" class="listadoGeneral">
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
