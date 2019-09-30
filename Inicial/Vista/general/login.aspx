<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/master/pagina_maestra.Master"
    AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Inicial.Vista.general.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="server" >
    .:: Login ::.
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenidoSistema" runat="server" autocomplete="off">
    <script src="../../Recursos/js/ajax/login.js" type="text/javascript"></script>
    <%--fancybox CSS --%>
    <link rel="stylesheet" type="text/css" href="../../Recursos/js/fancybox/jquery.fancybox-1.3.4.css"
        media="screen" />
    <%--AUTO COMPLETAR--%>
    <link href="../../Recursos/js/autocompletar/content/styles.css" rel="stylesheet"
        type="text/css" />
    <script src="../../Recursos/js/autocompletar/scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../Recursos/js/autocompletar/scripts/jquery.mockjax.js" type="text/javascript"></script>
    <script src="../../Recursos/js/autocompletar/src/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../Recursos/js/autocompletar/content/styles.css" rel="stylesheet"
        type="text/css" />
    <script src="../../Recursos/js/autocompletar/scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../Recursos/js/autocompletar/scripts/jquery.mockjax.js" type="text/javascript"></script>
    <script src="../../Recursos/js/autocompletar/src/jquery.autocomplete.js" type="text/javascript"></script>
    <%--AUTOCOMPLETAR--%>
    <link href="../../Recursos/js/autocompletar/content/styles.css" rel="stylesheet"
        type="text/css" />
    <script src="../../Recursos/js/autocompletar/scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../Recursos/js/autocompletar/scripts/jquery.mockjax.js" type="text/javascript"></script>
    <script src="../../Recursos/js/autocompletar/src/jquery.autocomplete.js" type="text/javascript"></script>
    <%--CALENDARIO--%>
    <link href="../../Recursos/css/calendario/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../../Recursos/css/calendario/jquery.ui.datepicker.css" rel="stylesheet"
        type="text/css" />
    <link href="../../Recursos/css/calendario/jquery.ui.core.css" rel="stylesheet" type="text/css" />
    <link href="../../Recursos/css/calendario/calendario.css" rel="stylesheet" type="text/css" />
    <script src="../../Recursos/js/calendario/jquery.ui.datepicker.autocomplete.js" type="text/javascript"></script>
    <script src="../../Recursos/js/calendario/jquery.ui.widget.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Recursos/js/accounting.js"></script>
    <%-- Jquery --%>
    <%--    <script type="text/javascript" src="../../Recursos/js/jquery.js"></script>--%>
    <%--fancybox JS --%>
    <script type="text/javascript" src="../../Recursos/js/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../../Recursos/js/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    <link href="../../Recursos/css/pestanas/jquery.ui.tabs.css" rel="stylesheet" type="text/css" />
    <script src="../../Recursos/js/pestanas/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="../../Recursos/js/navegadores.js" type="text/javascript"></script>
    <script src="../../Recursos/js/md5.js" type="text/javascript"></script>
    <div id="cuerpoLogin" style="margin-top: 100px;">
        <table class="centrarLogin" cellspacing="0" cellpadding="0" border="0" align="left">
            <tr>               
                <td>
                    <div>
                        <table class="centrar">
                            <tr>
                                <td colspan="8" align="center">                                    
                                    <h1 class="centrar">INGRESO AL SISTEMA</h1>                                    
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    Usuario:
                                    <br />
                                </td>                                
                            </tr>
                            <tr>
                                <td align="center">
                                    <input type="text" style="width: 168px;" class="campoTexto campoMayuscula" placeholder="USUARIO"
                                        id="usuario" onkeypress="return isEnter(event)" autocomplete="off"/>
                                    <br />
                                    <br />
                                </td>                                
                            </tr>
                             <tr>                           
                                <td align="center">
                                    Contraseña:
                                    <br />
                                </td>
                            </tr>
                            <tr>                                
                                <td align="center">
                                    <input type="password" style="width: 168px;" class="campoTexto" id="clave" placeholder="&#8226;&#8226;&#8226;&#8226;&#8226;&#8226;&#8226;&#8226;&#8226;&#8226;&#8226;&#8226;&#8226;"
                                        onkeypress="return isEnter(event)"  />
                                    <br />
                                    <br />
                                </td>
                            </tr>                            
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div style="display: none">
        <div id="divOlvideClave" class="fancyEliminar tbBorde">
            <table class="centrar">
                <tr>
                    <td align="center">
                        <span class="tituloForm">Recupera tu contrase&ntilde;a</span>
                    </td>
                </tr>
                <tr>
                    <td colspan="10">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="10">
                        <table border="0" class="centrar">
                            <tr>
                                <td colspan="2">
                                    <b>Correo electr&oacute;nico</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img src="../../Recursos/imagenes/administracion/mensaje.png" />
                                </td>
                                <td>
                                    <input type="text" id="email" class="campoMinuscula campoTextoMedio" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="10">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <table>
                            <tr>
                                <td align="right">
                                    <div id="imgAceptar">
                                        <span class="leyenda">Enviar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/aceptar.png" title="Guardar" alt="Guardar"
                                            class="imgAdmin" onclick="recuperarClave()" />
                                    </div>
                                </td>
                                <td align="left">
                                    <div id="imgCancelar">
                                        <span class="leyenda">Cancelar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/cancelar.png" title="Guardar" alt="Guardar"
                                            class="imgAdmin" onclick="cancelaRecuperaClave()" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div style="display: none">
        <div id="divOlvideUsuario" class="fancyEliminar tbBorde">
            <table class="centrar">
                <tr>
                    <td align="center">
                        <span class="tituloForm">Recupera tu usuario</span>
                    </td>
                </tr>
                <tr>
                    <td colspan="10">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="10">
                        <table border="0" class="centrar">
                            <tr>
                                <td colspan="2">
                                    <b>Correo electr&oacute;nico</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img src="../../Recursos/imagenes/administracion/mensaje.png" />
                                </td>
                                <td>
                                    <input type="text" id="txtCorreoRecuperarUsuario" class="campoMinuscula campoTextoMedio" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="10">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <table>
                            <tr>
                                <td align="right">
                                    <div id="Div2">
                                        <span class="leyenda">Enviar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/aceptar.png" title="Guardar" alt="Guardar"
                                            class="imgAdmin" onclick="recuperarUsuario()" />
                                    </div>
                                </td>
                                <td align="left">
                                    <div id="Div3">
                                        <span class="leyenda">Cancelar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/cancelar.png" title="Guardar" alt="Guardar"
                                            class="imgAdmin" onclick="cancelaRecuperaClave()" />
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
