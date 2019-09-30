<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/master/pagina_maestra.Master"
    AutoEventWireup="true" CodeBehind="login_directo.aspx.cs" Inherits="Inicial.Vista.general.login_directo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="server">
    .:: Login Directo ::.
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenidoSistema" runat="server">
    <script src="../../Recursos/js/md5.js" type="text/javascript"></script>
    <script src="../../Recursos/js/navegadores.js" type="text/javascript"></script>
    <script src="../../Recursos/js/ajax/login_directo.js" type="text/javascript"></script>
    <div class="divLogin">
        <br />
        <table class="tbLogin responsive">
            <tr>
                <td>
                    Usuario:
                </td>
                <td>
                    <input type="text" class="campoTexto campoMayuscula" id="usuario" onkeypress="return isEnter(event)" />
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
    <table class="centrar" id="tablaNavegadores">
        <tr>
            <td align="center" colspan="2">
                <span>Para un funcionamiento optimo del sistema se recomienda utilizar uno de los siguientes
                    navegadores:</span><br />
                <table>
                    <tr>
                        <td align="center" class="anchoCelda">
                            <a href="http://windows.microsoft.com/es-ES/internet-explorer/downloads/ie" target="_blank">
                                <img src="../../Recursos/imagenes/generales/explorer.png" alt="Explorer" class="imgIngresar"
                                    title="IE 9" /></a>
                        </td>
                        <td align="center" class="anchoCelda">
                            <a href="http://www.google.com/chrome" target="_blank">
                                <img src="../../Recursos/imagenes/generales/Chrome.png" alt="Chrome" class="imgIngresar"
                                    title="Chrome 10 +" /></a>
                        </td>
                        <td align="center" class="anchoCelda">
                            <a href="http://www.mozilla.com/en-US/firefox/fx/" target="_blank">
                                <img src="../../Recursos/imagenes/generales/firefox.png" alt="firefox" class="imgIngresar"
                                    title="Firefox 4 +" /></a>
                        </td>
                        <td align="center" class="anchoCelda">
                            <a href="http://www.apple.com/es/safari/download/" target="_blank">
                                <img src="../../Recursos/imagenes/generales/safari.png" alt="" class="imgIngresar"
                                    title="Safari 5 +" /></a>
                        </td>
                        <!--
                        <td>
                            <a href="http://www.opera.com/download/" target="_blank">
                                <img src="../../Recursos/imagenes/generales/opera.png" alt="" class="imgIngresar"
                                    title="Opera 10 +" /></a>
                        </td>-->
                    </tr>
                    <tr>
                        <td align="center">
                            <span>ie 9</span>
                        </td>
                        <td align="center">
                            <span>Chrome 10+</span>
                        </td>
                        <td align="center">
                            <span>Firefox 4+</span>
                        </td>
                        <td align="center">
                            <span>Safari 5+</span>
                        </td>
                    </tr>
                </table>
                <hr />
            </td>
        </tr>
    </table>    
</asp:Content>
