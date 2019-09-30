using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inicial.Vista.administracion
{
    public partial class DetalleEmpresa_PDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            string empresa = Session["nit_empresa"].ToString();

            if (!IsPostBack)
            {

                cargarReporte(empresa);

            }
       }
        protected void cargarReporte(string empresa)
        {

            var source = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet_info_administrador", new Inicial.ModeloReportes.logica.ReporteDetalle_info_administrador().GetFiltro(empresa));
            var sourcedatos = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet_info_administrativo", new Inicial.ModeloReportes.logica.ReporteDetalle_info_administrativo().GetFiltro(empresa));
            var sourcedatos1 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet_info_contacto", new Inicial.ModeloReportes.logica.ReporteDetalle_info_contacto().GetFiltro(empresa));
            var sourcedatos2 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet_info_economica", new Inicial.ModeloReportes.logica.ReporteDetalle_info_economica().GetFiltro(empresa));
            var sourcedatos3 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet_info_factura", new Inicial.ModeloReportes.logica.ReporteDetalle_info_factura().GetFiltro(empresa));
            var sourcedatos4 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet_info_general", new Inicial.ModeloReportes.logica.ReporteDetalle_info_general().GetFiltro(empresa));
            var sourcedatos5 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet_info_sede_ppal", new Inicial.ModeloReportes.logica.ReporteDetalle_info_sede_ppal().GetFiltro(empresa));
            ReportViewer1.LocalReport.ReportPath = "ModeloReportes\\vista\\reporte_DetalleEmpresa.rdlc";
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(source);
            ReportViewer1.LocalReport.DataSources.Add(sourcedatos);
            ReportViewer1.LocalReport.DataSources.Add(sourcedatos1);
            ReportViewer1.LocalReport.DataSources.Add(sourcedatos2);
            ReportViewer1.LocalReport.DataSources.Add(sourcedatos3);
            ReportViewer1.LocalReport.DataSources.Add(sourcedatos4);
            ReportViewer1.LocalReport.DataSources.Add(sourcedatos5);            
            ReportViewer1.LocalReport.EnableExternalImages = true;
            ReportViewer1.LocalReport.Refresh();

        }
    }
}