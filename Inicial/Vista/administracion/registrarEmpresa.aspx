<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/master/pagina_maestra.Master" AutoEventWireup="true" CodeBehind="registrarEmpresa.aspx.cs" Inherits="Inicial.Vista.administracion.registrarEmpresa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="server">
    .::Empresa::.
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenidoSistema" runat="server">
    <link href="../../Recursos/css/calendario/calendario.css" rel="stylesheet" type="text/css" />
    <link href="../../Recursos/css/calendario/jquery.ui.core.css" rel="stylesheet" type="text/css" />
    <link href="../../Recursos/css/calendario/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
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
    <script src="../../Recursos/js/ajax/registra_empresas.js" type="text/javascript"></script>
    <script src="../../Recursos/js/ajax/digitoVerificacion.js" type="text/javascript"></script>
    <span class="ocultar" id="idMenuForm">2.1</span>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#tabs").tabs();
        })
    </script>

     <div class="tbBorde">
        <div class="demo">
            <div id="tabs">
                <ul>
                    <li><a id="pest_general" href="#infoGeneral">General</a></li>
                    <li><a id="pest_economica" href="#infoEconomica">Econ&oacute;mica</a></li>
                    <li><a id="pest_administrativa" href="#infoAdministrativa" onclick="cargaTipDocumentoTercero();">Administrativa</a></li>
                    <li><a id="pest_financiera" href="#infoFinanciera" onclick="listarInformacionFactura(1);">Inf. Facturaci&oacute;n</a></li>
                    <li><a id="pest_sedePrincipal" href="#infoSedePrincipal" onclick="cargaPais(); cargaUbicacion();">Sede Principal</a></li>
                    <li><a id="pest_contacto" href="#infoContacto" onclick="cargaCargos();">Contacto</a></li>
                    <li><a id="pest_Administrador" href="#infoAdministrador">Administrador del Sistema</a></li>
                    <li><a id="pest_logo" href="#infoLogo">Logo</a></li>
                </ul>
                <div id="infoGeneral">
                    <input type="text" id="idEscondidoInfoGeneral" class="ocultar" />
                    <table class="centrar responsive">
                        <tr>
                            <td colspan="4" align="center">
                                <span class="tituloForm">INFORMACI&Oacute;N GENERAL DE LA EMPRESA</span>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>Nombre
                            </td>
                            <td colspan="4">
                                <input type="text" class="campoMayuscula campoTextoLargo" id="txtNombreEmpresa" />
                            </td>
                        </tr>
                        <tr>
                            <td>NIT
                            </td>
                            <td>
                                <input type="text" id="txtNit" onkeypress="return numeros(event);" class="campoTexto obligatorio"
                                    onkeyup="calculaDigitoVerificacion(this)" maxlength="10" />
                               
                            </td>
                            <td>D&iacute;gito Verificaci&oacute;n
                            </td>
                            <td colspan="1">
                                <label id="numeroVerificacion" class="campoMinuscula msj">
                                    --
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Último Dígito <br />NIT
                            </td>
                            <td colspan="1">
                                <input type="text" id="txtUltimoNit" onkeypress="return numeros(event);" class="campoTexto obligatorio"
                                    <%--onkeyup="calculaDigitoVerificacion(this)"--%> maxlength="1" style="width:30px;"/>                               
                            </td>
                            <td>
                                Últimos Dos <br />Dígitos NIT
                            </td>
                            <td colspan="1">
                                <input type="text" id="txtUltimosdosNit" onkeypress="return numeros(event);" class="campoTexto obligatorio"
                                    <%--onkeyup="calculaDigitoVerificacion(this)"--%> maxlength="2" style="width:30px;"/>                               
                            </td>
                        </tr>
                        <tr>
                            <td>Tipo Persona
                            </td>
                            <td colspan="1"> 
                                <input type="radio" name="tipoPersona" checked="checked" value="1" id="jur" onclick="habilitarJuridica()" />Jur&iacute;dica
                                <input type="radio" name="tipoPersona" value="2" id="nat" onclick="habilitarNatural()" />Natural
                            </td>
                            <td>Tipo Contribuyente
                            </td>
                            <td>
                                <select id="selectTipoContribuyente" class="select" onchange="cambiaTipoCont(this);">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <div id="divPersonaNatural" runat="server" style="display: none;" clientidmode="Static">
                                    <table>
                                        <tr>
                                            <td>Primer Apellido
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <input type="text" class="campoTexto campoMayuscula" onkeypress="return letras(event); "
                                                    id="txtPrimerApellidoPersonaNatural" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>Segundo Apellido
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <input type="text" class="campoTexto campoMayuscula" onkeypress="return letras(event); "
                                                    id="txtSegundoApellidoPersonaNatural" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Primer Nombre
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <input type="text" class="campoTexto campoMayuscula" onkeypress="return letras(event); "
                                                    id="txtPrimerNombrePersonaNatural" />
                                            </td>
                                            <td>Otros Nombres
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <input type="text" class="campoTexto campoMayuscula" onkeypress="return letras(event); "
                                                    id="txtSegundoNombrePersonaNatural" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="divPersonaJuridica" runat="server" style="display: none;" clientidmode="Static">
                                    <table>
                                        <tr>
                                            <td>Razon Social  
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;                                        
                                            </td>
                                            <td colspan="1">
                                                <input type="text" class="campoMayuscula campoTexto" id="txtRazonSocial" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
                                            </td>
                                            <td>Tipo de sociedad
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td colspan="1">
                                                <select id="selTipoSociedad" class="select" onchange="cambiaTipoSoc(this);">
                                                </select>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center">
                                <table class="centrar">
                                    <tr>
                                        <td align="center">
                                            <div id="imgGuardar">
                                                <span class="leyenda">Siguiente</span><br />
                                                <img src="../../Recursos/imagenes/administracion/guardarysiguiente.png" title="Guardar"
                                                    alt="Guardar" class="imgAdmin" onclick="guardaInfoGeneralEmpresa()" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="infoEconomica">
                    <input type="text" id="Text4" class="ocultar" />
                    <table class="centrar">
                        <tr>
                            <td colspan="4" align="center">
                                <span class="tituloForm">INFORMACI&Oacute;N ECON&Oacute;MICA</span>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span>ACTIVIDAD ECON&Oacute;MICA</span>
                            </td>
                        </tr>
                        <tr>
                            <td>Actividad Principal
                            </td>
                            <td colspan="5">
                                <div>
                                    <table>
                                        <tr>
                                            <td>
                                                <input type="text" id="txtActividadPrincipal" class="campoTextoCorto" onkeypress="return numeros(event);" />
                                            </td>
                                            <td id="CodAuxActividadPrincipal" style="display:none;">
                                                SHD
                                            </td>
                                            <td>
                                                <input type="text" id="txtCodActividad" style="width:24px; display:none;"  class="campoTextoCorto"/>
                                            </td>
                                            <td align="right">
                                                <img width="25px" src="../../Recursos/imagenes/administracion/validar.png" title="Validar"
                                                    alt="Validar" class="imgAdminPequenia" onclick="validaActividadEconomica('ACTIVIDAD')" />
                                                <img width="25px" src="../../Recursos/imagenes/administracion/buscar.png" title="Buscar"
                                                    alt="Buscar" class="imgAdminPequenia" onclick="buscaActividadEconomica(1, 'ACTIVIDAD')" />
                                            </td>
                                            <td>
                                                <label id="lblActividadPrincipal" class="campoMayuscula msj">Nombre de la Actividad Selecciona </label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                       <tr>
                            <td align="center" colspan="5">
                                <div id="divListadoCodHacienda1" style="display:none;" class="centrar listadoGeneral">
                                </div>
                            </td>
                       </tr>
                        <tr>
                            <td>Actividad Secundaria
                            </td>
                            <td colspan="5">
                                <div>
                                    <table>
                                        <tr>
                                            <td>
                                                <input type="text" id="txtActividadSecundaria" class="campoTextoCorto" onkeypress="return numeros(event);" />
                                            </td>
                                            <td id="CodAuxActividadSecundaria" style="display:none;">
                                                SHD
                                            </td>
                                            <td>
                                               <input type="text" id="txtCodActividadSecundaria" style="width:24px; display:none;"  class="campoTextoCorto"/>
                                            </td>
                                            <td align="right">
                                                <img width="25px" src="../../Recursos/imagenes/administracion/validar.png" title="Validar"
                                                    alt="Validar" class="imgAdminPequenia" onclick="validaActividadEconomica('SECUNDARIA')" />
                                                <img width="25px" src="../../Recursos/imagenes/administracion/buscar.png" title="Buscar"
                                                    alt="Buscar" class="imgAdminPequenia" onclick="buscaActividadEconomica(1, 'SECUNDARIA')" />
                                            </td>
                                            <td>
                                                <label id="lblActividadSecundaria" class="campoMayuscula msj">Nombre de la Actividad Selecciona </label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="5">
                                <div id="divListadoCodHacienda2" style="display:none;" class="centrar listadoGeneral">
                                </div>
                            </td>
                       </tr>
                        <tr>
                            <td>Otra Actividad 1
                            </td>
                            <td colspan="5">
                                <div>
                                    <table>
                                        <tr>
                                            <td>
                                                <input type="text" id="txtOtraActividad1" class="campoTextoCorto" onkeypress="return numeros(event);" />
                                            </td>
                                            <td id="CodAuxOtraActividad1" style="display:none;">
                                                SHD
                                            </td>
                                            <td>
                                                <input type="text" id="txtCodOtraActividad1" style="width:24px; display:none;" class="campoTextoCorto"/>
                                            </td>
                                            <td align="right">
                                                <img width="25px" src="../../Recursos/imagenes/administracion/validar.png" title="Validar"
                                                    alt="Validar" class="imgAdminPequenia" onclick="validaActividadEconomica('OTRA ACTIVIDAD 1')" />
                                                <img width="25px" src="../../Recursos/imagenes/administracion/buscar.png" title="Buscar"
                                                    alt="Buscar" class="imgAdminPequenia" onclick="buscaActividadEconomica(1, 'OTRA ACTIVIDAD 1')" />
                                            </td>
                                            <td>
                                                <label id="lblOtraActividad1" class="campoMayuscula msj">Nombre de la Actividad Selecciona </label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="5">
                                <div id="divListadoCodHacienda3" style="display:none;" class="centrar listadoGeneral">
                                </div>
                            </td>
                       </tr>
                        <tr>
                            <td>Otra Actividad 2
                            </td>
                            <td colspan="3">
                                <div>
                                    <table>
                                        <tr>
                                            <td>
                                                <input type="text" id="txtOtraActividad2" class="campoTextoCorto" onkeypress="return numeros(event);" />
                                            </td>
                                            <td id="CodAuxOtraActividad2" style="display:none;">
                                                SHD
                                            </td>
                                            <td>
                                                <input type="text" id="txtCodOtraActividad2" style="width:24px; display:none;"  class="campoTextoCorto"/>
                                            </td>
                                            <td align="right">
                                                <img width="25px" src="../../Recursos/imagenes/administracion/validar.png" title="Validar"
                                                    alt="Validar" class="imgAdminPequenia" onclick="validaActividadEconomica('OTRA ACTIVIDAD 2')" />
                                                <img width="25px" src="../../Recursos/imagenes/administracion/buscar.png" title="Buscar"
                                                    alt="Buscar" class="imgAdminPequenia" onclick="buscaActividadEconomica(1, 'OTRA ACTIVIDAD 2')" />
                                            </td>
                                            <td>
                                                <label id="lblOtraActividad2" class="campoMayuscula msj">Nombre de la Actividad Selecciona </label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="5">
                                <div id="divListadoCodHacienda4" style="display:none;" class="centrar listadoGeneral">
                                </div>
                            </td>
                       </tr>
<%--                        <tr>
                            <td>Codigo Secretaria Hacienda
                            </td>
                            <td>
                                <input type="text" id="txtCodigoHacienda" class="campoTexto" />
                            </td>
                        </tr>--%>
                        <tr>
                            <td colspan="6">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td>Cantidad Sedes
                            </td>
                            <td colspan="5">
                                <input type="text" id="txtNumSedes" class="campoTextoCorto" onkeypress="return numeros(event);" />
                            </td>
                        </tr>
                        <tr>
                            <td>Valor
                            </td>
                            <td>
                                <input type="text" id="txtValor" class="campoTexto" onkeypress="return numeros(event);"
                                    onkeyup="calculaTamanoEmpresa()" />
                            </td>
                            <td>Activos (SMMLV)
                            </td>
                            <td>
                                <input type="text" id="txtActivos" class="campoTexto" onkeypress="return numeros(event);"
                                    onkeyup="calculaTamanoEmpresa()" />
                            </td>
                        </tr>
                        <tr>
                            <td>N&uacute;m. Empleados
                            </td>
                            <td>
                                <input type="text" id="txtNumEmpleados" class="campoTextoCorto" onkeypress="return numeros(event);"
                                    onkeyup="calculaTamanoEmpresa()" />
                            </td>
                            <td>Tamaño
                            </td>
                            <td colspan="5">
                                <span id="tamano"></span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center">
                                <table class="centrar">
                                    <tr>
                                        <td align="center">
                                            <div id="Div1">
                                                <span class="leyenda">Siguiente</span><br />
                                                <img src="../../Recursos/imagenes/administracion/guardarysiguiente.png" title="Guardar"
                                                    alt="Guardar" class="imgAdmin" onclick="guardaInfoEconomicaEmpresa()" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="infoAdministrativa">
                    <input type="text" id="Text1" class="ocultar" />
                    <table class="centrar">
                        <tr>
                            <td colspan="4" align="center">
                                <span class="tituloForm">INFORMACI&Oacute;N ADMINISTRATIVA</span>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <span>DATOS DEL REPRESENTANTE LEGAL</span>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>Representante Legal
                            </td>
                            <td colspan="3" align="left">
                                <input type="text" id="txtRepresentanteLegal" class=" campoTextoLargo campoMayuscula"
                                    onkeypress="return letras(event);" />
                            </td>
                        </tr>
                        <tr>
                            <td>Tipo Documento
                            </td>
                            <td align="left">
                                <select id="selectTipoDocumentoRepresentanteLegal" class="select" onchange="cambiaDocRep(this)">
                                    <option value="1">--SELECCIONE--</option>
                                </select>
                            </td>
                            <td>N&uacute;mero Documento
                            </td>
                            <td>
                                <input type="text" id="txtNumeroDocumentoRepresentante" class="campoTexto" onkeypress="return numeros(event); " maxlength="10"/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <hr />
                                <span>DATOS DEL CONTADOR</span>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>Contador
                            </td>
                            <td>
                                <input type="text" id="txtContador" class="campoTextoLargo campoMayuscula" onkeypress="return letras(event);" />
                            </td>
                            <td>T.P
                            </td>
                            <td>
                                <input type="text" id="txtTPContador" class="campoTexto campoMayuscula" onkeypress="return numerosLetrasGuionPunto(event); " />
                            </td>
                        </tr>
                        <tr>
                            <td>Tipo Documento
                            </td>
                            <td align="left">
                                <select id="selectTipoDocumentoContador" class="select" onchange="cambiaDocCont(this)">
                                    <option value="1">--SELECCIONE--</option>
                                </select>
                            </td>
                            <td>N&uacute;mero Documento
                            </td>
                            <td>
                                <input type="text" id="txtDocumentoIdContador" class="campoTexto" onkeypress="return numeros(event); " maxlength="10"/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <hr />
                                <span>DATOS DEL REVISOR FISCAL</span>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>Revisor Fiscal
                            </td>
                            <td>
                                <input type="text" id="txtRevisorFiscal" class="campoTextoLargo campoMayuscula" onkeypress="return letras(event);" />
                            </td>
                            <td>T.P
                            </td>
                            <td>
                                <input type="text" id="txtTPRevisorFiscal" class="campoTexto campoMayuscula" onkeypress="return numerosLetrasGuionPunto(event); " />
                            </td>
                        </tr>
                        <tr>
                            <td>Tipo Documento
                            </td>
                            <td align="left">
                                <select id="selectTipoDocumentoRevisorFiscal" class="select" onchange="cambiaDocRevF(this)">
                                    <option value="1">--SELECCIONE--</option>
                                </select>
                            </td>
                            <td>N&uacute;mero Documento
                            </td>
                            <td>
                                <input type="text" id="txtDocumentoIdRevisor" class="campoTexto" onkeypress="return numeros(event); " maxlength="10"/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center">
                                <table class="centrar">
                                    <tr>
                                        <td align="center">
                                            <div id="Div8">
                                                <span class="leyenda">Siguiente</span><br />
                                                <img src="../../Recursos/imagenes/administracion/guardarysiguiente.png" title="Guardar"
                                                    alt="Guardar" class="imgAdmin" onclick="guardaInfoAdministrativaEmpresa()" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="infoFinanciera">
                    <input type="text" id="Text5" class="ocultar" />
                    <table class="centrar">
                        <tr>
                            <td colspan="4" align="center">
                                <span class="tituloForm">INFORMACI&Oacute;N FACTURACI&Oacute;N</span>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="linkSmall" class="linkIconoSuperior botonGuardar ocultar" onclick="muestraVentanaFancybox('divFacturacion');">
                                    <img height="16px" width="16px" src="../../Recursos/imagenes/administracion/agregar_24x24.png"
                                        alt="" /><p>
                                            Nuevo
                                    </p>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center">
                                <div id="listadoInformacion" class="centrar listadoGeneral">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center">
                                <table class="centrar">
                                    <tr>
                                        <td align="center">
                                            <div id="Div26">
                                                <span class="leyenda">Siguiente</span><br />
                                                <img src="../../Recursos/imagenes/administracion/guardarysiguiente.png" title="Guardar"
                                                    alt="Guardar" class="imgAdmin" onclick="continuarFacturacion()" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="infoSedePrincipal">
                    <input type="text" id="Text6" class="ocultar" />
                    <table class="centrar">
                        <tr>
                            <td colspan="6" align="center">
                                <span class="tituloForm">INFORMACI&Oacute;N DE LA SEDE PRINCIPAL</span>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>Nombre
                            </td>
                            <td colspan="5">
                                <input type="text" class="campoMayuscula campoTextoLargo" id="txtNombreSedePrincipal" />
                            </td>
                        </tr>
                        <tr>
                            <td>Pa&iacute;s
                            </td>
                            <td>
                                <select id="selPaisSedePrincipal" class="select" onchange="cargaDepartamento(this); cambiaPais(this);">
                                    <option value="-1">-- SELECCIONE --</option>
                                </select>
                            </td>
                            <td>Departamento
                            </td>
                            <td>
                                <select id="selDeptoSedePrincipal" class="select" onchange="cargaMunicipio(this); cambiaDepto(this);">
                                    <option value="-1">-- SELECCIONE --</option>
                                </select>
                            </td>
                            <td>Municipio
                            </td>
                            <td>
                                <select id="selMuniSedePrincipal" class="select" onchange="cambiaMunic(this);">
                                    <option value="-1">-- SELECCIONE --</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td>Direcci&oacute;n
                            </td>
                            <td>
                                <input type="text" id="txtDirSedePrincipal" onkeypress="return vdireccion(event);"
                                    class="campoTexto campoMayuscula" />
                            </td>
                            <td>Tel&eacute;fono
                            </td>
                            <td>
                                <input type="text" id="txtTelSedePrincipal" class="campoTexto" onkeypress="return numeros(event);" />
                            </td>
                            <td>Email
                            </td>
                            <td>
                                <input type="text" id="txtMailSedePrincipal" class="campoTexto campoMinuscula" />
                            </td>
                        </tr>
                        <tr>
                            <td>Ubicaci&oacute;n
                            </td>
                            <td>
                                <select id="selUbicacionSedePrincipal" class="select" onchange="cambiaUbicacion(this);">
                                </select>
                            </td>
                            <td>No. Empleados
                            </td>
                            <td>
                                <input type="text" id="txtNumEmpleSedePrincipal" class="campoTexto" onkeypress="return numeros(event);" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center">
                                <table class="centrar">
                                    <tr>
                                        <td align="center">
                                            <div id="Div30">
                                                <span class="leyenda">Siguiente</span><br />
                                                <img src="../../Recursos/imagenes/administracion/guardarysiguiente.png" title="Guardar"
                                                    alt="Guardar" class="imgAdmin" onclick="guardaInfoSedePrincipalEmpresa()" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="infoContacto">
                    <input type="text" id="Text3" class="ocultar" />
                    <table class="centrar">
                        <tr>
                            <td colspan="4" align="center">
                                <span class="tituloForm">INFORMACI&Oacute;N DEL CONTACTO DE LA EMPRESA</span>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>Nombre Contacto
                            </td>
                            <td colspan="3">
                                <input type="text" id="txtContacto" class="campoTextoLargo campoMayuscula" />
                            </td>
                        </tr>
                        <tr>
                            <td>Cargo del Contacto
                            </td>
                            <td colspan="3">
                                <select id="selCargoContacto" class="select" onchange="cambiaCargo(this);">
                                <option value="-1">-- SELECCIONE --</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td>Tel&eacute;fono
                            </td>
                            <td>
                                <input type="text" id="txtTelContacto" class="campoTexto" onkeypress="return numeros(event)" />
                            </td>
                            <td>Email
                            </td>
                            <td>
                                <input type="text" id="txtEmailContacto" class="campoTexto campoMinuscula" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center">
                                <table class="centrar">
                                    <tr>
                                        <td align="center">
                                            <div id="Div16">
                                                <span class="leyenda">Siguiente</span><br />
                                                <img src="../../Recursos/imagenes/administracion/guardarysiguiente.png" title="Guardar"
                                                    alt="Guardar" class="imgAdmin" onclick="guardaInfoContactoEmpresa()" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="infoAdministrador">
                    <input type="text" id="Text22" class="ocultar" />
                    <table class="centrar">
                        <tr>
                            <td colspan="4" align="center">
                                <span class="tituloForm">INFORMACI&Oacute;N DEL ADMINISTRADOR DEL SISTEMA PARA LA EMPRESA</span>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4">
                                <div id="div20">
                                    <table>
                                        <tr>
                                            <td>Usuario
                                            </td>
                                            <td>
                                                <input type="text" id="txtUsuarioEmpresa" class="campoTexto campoMayuscula obligatorio" />
                                            </td>
                                            <td>Nombre
                                            </td>
                                            <td>
                                                <input type="text" id="txtNombreAdminEmpresa" class="campoTexto campoMayuscula obligatorio" />
                                            </td>
                                            <td>E-mail
                                            </td>
                                            <td>
                                                <input type="text" id="txtEmailAdminEmpresa" class="campoTexto campoMinuscula obligatorio" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center">
                                <table class="centrar">
                                    <tr>
                                        <td align="center">
                                            <div id="Div22">
                                                <span class="leyenda">Siguiente</span><br />
                                                <img src="../../Recursos/imagenes/administracion/guardarysiguiente.png" title="Guardar"
                                                    alt="Guardar" class="imgAdmin" onclick="guardaInfoAdministradorEmpresa()" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="infoLogo">
                    <input type="text" id="Text2" class="ocultar" />
                    <table class="centrar">
                        <tr>
                            <td colspan="6" align="center">
                                <span class="tituloForm">LOGOTIPO DE LA EMPRESA</span>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center">
                                <iframe id="Iframe1" src="../../Controlador/ctlCargaLogoEmpresa.aspx?reg=1" class="iframeArchivoImagen"
                                    runat="server"></iframe>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center">
                                <table class="centrar">
                                    <tr>
                                        <td align="center">
                                            <div id="Div6">
                                                <span class="leyenda">Finalizar</span><br />
                                                <img src="../../Recursos/imagenes/administracion/guardar.png" title="Guardar" alt="Guardar"
                                                    class="imgAdmin" onclick="guardaLogoEmpresa()" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div style="display: none;">
        <div id="divFacturacion" class="tbBorde fancyNormal">
            <table class="centrar">
                <tr>
                    <td align="center" colspan="4">
                        <span class="tituloForm">ADMINISTRACI&Oacute;N DE FACTURACI&Oacute;N</span>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>Inicio Facturaci&oacute;n
                    </td>
                    <td>
                        <input type="text" class="campoTexto campoMayuscula" id="txtInicioFacturacion" />
                    </td>
                    <td>Fin Facturaci&oacute;n
                    </td>
                    <td>
                        <input type="text" class="campoTexto campoMayuscula" id="txtFinFacturacion" />
                    </td>
                </tr>
                <tr>
                    <td>Resolucion
                    </td>
                    <td>
                        <input type="text" class="campoTexto campoMayuscula" id="txtResolucionFacturacion" />
                    </td>
                    <td>Fecha Vencimiento<br />
                        Resolucion
                    </td>
                    <td>
                        <input type="text" class="campoTexto campoMayuscula" id="txtVencimientoResolucion" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table class="centrar" border="0">
                            <tr>
                                <td align="center">
                                    <div id="Div2">
                                        <span class="leyenda">Guardar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/guardar.png" title="Guardar" alt="Guardar"
                                            class="imgAdmin" onclick="guardaInfoFinancieraEmpresa()" />
                                    </div>
                                </td>
                                <td align="center">
                                    <span class="leyenda">Cancelar</span><br />
                                    <img src="../../Recursos/imagenes/administracion/cancelar.png" onclick="cerrarVentanaEmergente()"
                                        alt="Cancelar" class="imgAdmin" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!-- Formulario para ver el detalle del registro -->
    <div style="display: none;">
        <div id="divFormularioDetalle" class="listadoGeneral fancyNormal">
            <table class="centrar">
                <tr>
                    <td colspan="2" align="center">
                        <span class="tituloForm">DETALLE DE FACTURACI&Oacute;N</span>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                        <div id="divDetalle" class="centrar">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <span class="leyenda">Cancelar</span><br />
                        <img src="../../Recursos/imagenes/administracion/cancelar.png" onclick="cerrarVentanaEmergente()"
                            alt="Cancelar" class="imgAdmin" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="divListadoEmpresas">
    </div>
    <div id="listadoEmpresas" class="centrar">
    </div>
    <%--VENTANA PARA LISTAR LAS ACTIVIDADES ECONOMICAS--%>
    <div style="display: none;">
        <div id="divListadoActividades">
            <table border="0" class="tbListado centrar">
<%--                <tr>
                    <td colspan="2" align="right">
                        <span class="ventanaCerrar puntero">Cerrar</span>
                    </td>
                </tr>--%>
                <tr>
                    <td>
                        <div id="listadoActividades">
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>

     <!--Ventana con informacion del Profesional-->
    <div style="display: none;">
        <div id="divListadoActividadesEconomicas" class="centrar">
            <table border="0" class="fancyNormal tbBorde">
                <tr>
                    <td colspan="4" align="center">
                        <span class="tituloForm">BUSCAR ACTIVIDADES</span>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <table>
                            <tr>
                                <td>
                                    Código
                                </td>
                                <td>
                                    <input type="text" id="numDocuFil" class="campoTextoMedio campoMayuscula"  />
                                </td>
                                <td>

                                    <img src="../../Recursos/imagenes/administracion/listar24X24.png" title="Listar"
                                        alt="Listar" style="cursor: pointer;" onclick="buscaActividadEconomica(1, '')" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Nombre
                                </td>
                                <td>
                                    <input type="text" id="nomProFil" class="campoTextoMedio campoMayuscula"  />
                                </td>
                                <td>
                                    <img src="../../Recursos/imagenes/administracion/listar24X24.png" title="Listar"
                                        alt="Listar" style="cursor: pointer;" onclick="buscaActividadEconomica(1,'')" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="centrar" align="center" colspan="4">
                        <div id="listadoActividadesEconomicas" class=" listadoGeneral">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <table class="centrar">
                            <tr>
                                <td>
                                    <div>
                                        <span class="leyenda">Cancelar</span><br />
                                        <img src="../../Recursos/imagenes/administracion/cancelar.png" title="Cancelar" alt="Cancelar"
                                            class="imgAdmin" onclick="cerrarVentanaEmergente()" /></div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>






</asp:Content>

