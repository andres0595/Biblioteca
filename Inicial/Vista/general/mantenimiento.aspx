<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/master/pagina_mantenimiento.Master"
    AutoEventWireup="true" CodeBehind="mantenimiento.aspx.cs" Inherits="Inicial.Vista.general.mantenimiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="server">
    .:: Mantenimiento ::.
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenidoSistema" runat="server">
    <span class="ocultar" id="idMenuForm">-100</span>
    <div class="tbBorde">
        <table class="centrar responsive">
            <tr>
                <td colspan="6" align="center">
                    <span class="tituloForm">MANTENIMIENTO LC WEB CONSULTORES</span><br />
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <span class="tituloForm">CONSULTAS RAPIDAS</span><br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <span class="mantenimiento"><a href="javascript:void(0)" onclick="buscaTablas()">TABLAS</a></span>
                </td>
                <td>
                    <span class="mantenimiento"><a href="javascript:void(0)" onclick="buscaVistas()">VISTAS</a></span>
                </td>
                <td>
                    <span class="mantenimiento"><a href="javascript:void(0)" onclick="buscaRutinas()">RUTINAS</a></span>
                </td>
                <td>
                    <span class="mantenimiento"><a href="javascript:void(0)" onclick="buscaPaquetes()">PAQUETES</a></span>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="mantenimiento"><a href="javascript:void(0)" onclick="variableSistema()">
                        RESUMEN SISTEMA</a></span>
                </td>
                <td>
                    <span class="mantenimiento"><a href="javascript:void(0)" onclick="listaDiscosDuros()">
                        DISCOS DUROS</a></span>
                </td>
                <td>
                    <span class="mantenimiento"><a href="javascript:void(0)" onclick="sistemaOperativo()">
                        S. OPERATIVO</a></span>
                </td>
                <td>
                    <span class="mantenimiento"><a href="javascript:void(0)" onclick="fondoConsolaDeComandos()">
                        CMD SISTEMA</a></span>
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <br />
                    <br />
                    <span>CONSOLA DE CONSULTAS</span>
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <textarea rows="10" cols="1" id="taConsulta" class="descripcion"></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <table>
                        <tr>
                            <td align="center">
                                <div id="divEjecutaSQL">
                                    <img src="../../Recursos/imagenes/administracion/Ejecutar.png" class="imgAdmin" onclick="ejecutarConsulta()"
                                        alt="Cambiar Clave" title="Ejecutar Comando SQL" />
                                    <br />
                                    <font size="1">Ejecutar SQL</font>
                                </div>
                            </td>
                            <td align="center">
                                <div class="ocultar" id="divEjecutaCMD">
                                    <img src="../../Recursos/imagenes/administracion/cmd.png" class="imgAdmin" onclick="consolaDeComandos()"
                                        alt="Cambiar Clave" title="Ejecutar Comando CMD" />
                                    <br />
                                    <font size="1">Ejecutar CMD</font></div>
                            </td>
                            <td align="center">
                                <img src="../../Recursos/imagenes/administracion/limpiar.png" class="imgAdmin" onclick="limpiar()"
                                    alt="Limpiar" title="Limpiar" />
                                <br />
                                <font size="1">Limpiar</font>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <div id="resultadoConsulta">
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
