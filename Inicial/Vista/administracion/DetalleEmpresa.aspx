<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/master/pagina_maestra.Master" AutoEventWireup="true" CodeBehind="DetalleEmpresa.aspx.cs" Inherits="Inicial.Vista.administracion.DetalleEmpresa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="server">
    .::Detalle Empresa::.
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenidoSistema" runat="server">
<script src="../../Recursos/js/ajax/detalleEmpresa.js" type="text/javascript"></script>
    <span class="ocultar" id="idMenuForm">2.8</span>
    <div class="tbBorde">
        <table class="centrar">
            <tr>
                <td align="center">
                    <span class="tituloForm">DETALLE EMPRESA</span>
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <div id="logoEmpresa">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <div style="float: left;">
                        <table>
                            <tr>
                                <td>
                                    <input type="checkbox" id="ckHabilitar"  onclick="mostrarTodosCK(this)"/>
                                </td>
                                <td>
                                    <div class="linkIconoSuperior" onclick="mostrarTodos()">
                                        <p>
                                            SELECCIONAR TODOS
                                        </p>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <div id="listadoInformacionGeneral" class="centrar listadoGeneralDetalle">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="checkbox" id="ckEconomica" onclick="mostrarListado(this, 1)" />
                    <span class="subtitulosDetalle">INFORMACI&Oacute;N EC&Oacute;NOMICA</span>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <div id="listadoInfoEconomica" class="ocultar listadoGeneralDetalle">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="checkbox" id="ckAdministrativa" onclick="mostrarListado(this, 2)" />
                    <span class="subtitulosDetalle">INFORMACI&Oacute;N ADMINISTRATIVA</span>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <div id="listadoInfoAdministrativa" class="ocultar listadoGeneralDetalle">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="checkbox" id="ckFacturacion" onclick="mostrarListado(this, 3)" />
                    <span class="subtitulosDetalle">INFORMACI&Oacute;N FACTURACI&Oacute;N</span>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <div id="listadoInfoFacturacion" class="ocultar listadoGeneralDetalle">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="checkbox" id="ckSedePrincipal" onclick="mostrarListado(this, 4)" />
                    <span class="subtitulosDetalle">INFORMACI&Oacute;N SEDE PRINCIPAL</span>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <div id="listadoInfoSedePrincipal" class="ocultar listadoGeneralDetalle">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="checkbox" id="ckContacto" onclick="mostrarListado(this, 5)" />
                    <span class="subtitulosDetalle">INFORMACI&Oacute;N CONTACTO</span>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <div id="listadoInfoContacto" class="ocultar listadoGeneralDetalle">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="checkbox" id="ckAdministrador" onclick="mostrarListado(this, 6)" />
                    <span class="subtitulosDetalle">INFORMACI&Oacute;N ADMINISTRADOR SISTEMA</span>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <div id="listadoInfoAdministrador" class="ocultar listadoGeneralDetalle">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <hr />
                </td>
            </tr>
            <tr>
                <td  align="center">
                    <table class="centrar">
                        <tr>
                            <td align="center">
                                <div>
                                    <span class="leyenda">Ver PDF</span><br />
                                    <img src="../../Recursos/imagenes/administracion/pdf.png" title="Ver PDF" alt="Ver PDF"
                                        class="imgAdmin" onclick="verPDF()" /></div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
