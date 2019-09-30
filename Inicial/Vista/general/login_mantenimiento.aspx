<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/master/pagina_maestra.Master"
    AutoEventWireup="true" CodeBehind="login_mantenimiento.aspx.cs" Inherits="Inicial.Vista.general.login_mantenimiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="server">
    .:: MANTENIMIENTO ::.
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenidoSistema" runat="server">
    <script src="../../Recursos/js/md5.js" type="text/javascript"></script>
    <link href="../../Recursos/css/login.css" rel="stylesheet" type="text/css" />
    <script src="../../Recursos/js/ajax/login_mantenimiento.js" type="text/javascript"></script>
    <span class="ocultar" id="idMenuForm">0</span>
    <div class="divLogin">
        <br />
        <table class="tbLogin responsive">
            <tr>
                <td>
                    Usuario:
                </td>
                <td>
                    <input type="password" class="campoTexto campoMayuscula" id="usuario" onkeypress="return isEnter(event)" />
                </td>
                <td rowspan="2" align="center">
                    <img src="../../Recursos/imagenes/login/ok.png" alt="" class="imgIngresar" onclick="ingresar()" />
                </td>
            </tr>
            <tr>
                <td>
                    Clave:
                </td>
                <td>
                    <input type="password" class="campoTexto" id="clave" onkeypress="return isEnter(event)" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
