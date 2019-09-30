<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/master/pagina_maestra.Master" AutoEventWireup="true" CodeBehind="DetalleEmpresa_PDF.aspx.cs" Inherits="Inicial.Vista.administracion.DetalleEmpresa_PDF" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenidoSistema" runat="server">
      <form id="form1" runat="server" style="height: 800px" class="centrar" >
             <label id="lblMsj" style="display:none;">Ocultar</label>
        <script type="text/javascript" >
            $(document).ready(function () /*dindica que nuesstra función listarDatos(n) se ejecuta al iniciar.*/ {
                muestraDiv();
            });

        </script>
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
           <div class="centrar">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                InteractiveDeviceInfos="(Colección)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                Width="1200px" Height="753px" BorderStyle="Solid" BorderColor="#1269A2" WaitMessageFont-Bold="True"
                CssClass="listadoGeneral700">
                <LocalReport ReportPath="ModeloReportes\Vista\reporte_DetalleEmpresa.rdlc" >
                </LocalReport>
            </rsweb:ReportViewer>
        </div>
    </form>
</asp:Content>
