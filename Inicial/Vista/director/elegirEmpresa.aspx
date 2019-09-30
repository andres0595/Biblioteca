<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/master/pagina_maestra.Master"
    AutoEventWireup="true" CodeBehind="elegirEmpresa.aspx.cs" Inherits="Inicial.Vista.director.elegirEmpresa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="server">
    .:: Elegir Empresa ::.  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenidoSistema" runat="server">
    <head>
        <link href="../../Recursos/css/calendario/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
        <link href="../../Recursos/css/calendario/jquery.ui.datepicker.css" rel="stylesheet"
            type="text/css" />
        <link href="../../Recursos/css/calendario/jquery.ui.core.css" rel="stylesheet" type="text/css" />
        <link href="../../Recursos/css/calendario/calendario.css" rel="stylesheet" type="text/css" />
        <link href="../../Recursos/css/pestanas/jquery.ui.tabs.css" rel="stylesheet" type="text/css" />
        <link href="../../Recursos/css/pestanas/demos.css" rel="stylesheet" type="text/css" />
        <script src="../../Recursos/js/calendario/jquery-ui-1.8.7.custom.js" type="text/javascript"></script>
        <script src="../../Recursos/js/calendario/jquery.ui.core.js" type="text/javascript"></script>
        <script src="../../Recursos/js/calendario/jquery.ui.datepicker.js" type="text/javascript"></script>
        <script src="../../Recursos/js/calendario/jquery.ui.widget.js" type="text/javascript"></script>
        <script src="../../Recursos/js/ajax/digitoVerificacion.js" type="text/javascript"></script>
        <script src="../../Recursos/js/ajax/elegirEmpresas.js" type="text/javascript"></script>
    </head>
    <span class="ocultar" id="idMenuForm">2.2</span>
    <input type="text" id="idEscondido" class="ocultar" />
    <div id="contenedorEmpresa" class="tbBorde centrar" style="margin: auto;">
        <table class="centrar">
            <tr>
                <td colspan="6" align="center">
                    <span class="tituloForm">ELEGIR EMPRESA</span>
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <div id="listadoEmpresas" class="centrar listadoGeneral">
        </div>
    </div>    
</asp:Content>
