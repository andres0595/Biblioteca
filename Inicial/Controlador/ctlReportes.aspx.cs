using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inicial.Controlador
{
    public partial class ctlReportes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string p = Request.QueryString.Get("p");
            if (p == null)//Se valida si los datos llegan por POST o GET
            {
                if (Request.HttpMethod.Equals("POST"))
                {
                    p = Request.Form["p"];
                }
            }

            if ((Session["nom_usuario"]) == null)
            {
                Response.Redirect("../vista/general/inicio.aspx");
            }

            string retorno = "";
            Inicial.Modelo.ConexionBD_Sql_Server cx = new Modelo.ConexionBD_Sql_Server();
            //string p = Request.QueryString.Get("p");
            string tipoConexion = cx.tipoConexion();
            string emp = (string)Session["nit_empresa"];
            string responsable = Session["usu_sistema"].ToString();
            string imagen = Session["imagen_empresa"].ToString();
            string empresa = Session["nit_empresa"].ToString();
            string pathToFiles = "";
            string img = "";
            string pathToFotos = "";
            switch (p)
            {
                /* Unidad de Medida */
                case "generaReporteCotizaciones":
                    pathToFiles = Server.MapPath("../logos");
                    pathToFotos = Server.MapPath("../Fotos");
                    img = pathToFiles + "\\" + imagen;
                    retorno = cx.Listar("paCrmGeneraReporteCotizaciones",
                        "idCotizaciones", Request.Form["idCotizaciones"],
                        "imagen", img,
                        "rutaFotos",pathToFotos,
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "generaReporteOportunidades":
                    pathToFiles = Server.MapPath("../logos");
                    img = pathToFiles + "\\" + imagen;
                    retorno = cx.InsertarRetorna("paCrmGenerarReporteOportunidad",
                        "fechaInicio", Request.Form["fechaInicio"],
                        "fechaFin", Request.Form["fechaFin"],
                        "idCliente", Request.Form["idCliente"],
                        "tipo", Request.Form["tipo"],
                        "estado", Request.Form["estado"],
                        "razonPerdida", Request.Form["razonPerdida"],
                        "fuente", Request.Form["fuente"],
                        "camapana", Request.Form["camapana"],
                        "lineaVenta", Request.Form["lineaVenta"],
                        "etapa", Request.Form["etapa"],
                        "responsableOportunidad", Request.Form["responsableOportunidad"],
                        "imagen", img,
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "generaReporteSeguimientos":
                    pathToFiles = Server.MapPath("../logos");
                    img = pathToFiles + "\\" + imagen;
                    retorno = cx.Listar("paCrmGeneraReporteSeguimientos",
                        "fechaInicio", Request.Form["fechaInicio"],
                        "fechaFin", Request.Form["fechaFin"],
                        "responsableSesion", Request.Form["responsableSesion"],
                        "estado", Request.Form["estado"],
                        "imagen", img,
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "generaReporteActividades":
                    pathToFiles = Server.MapPath("../logos");
                    img = pathToFiles + "\\" + imagen;
                    retorno = cx.Listar(p,
                        "fechaInicio", Request.Form["fechaInicio"],
                        "fechaFin", Request.Form["fechaFin"],
                        "responsableSesion", Request.Form["responsableSesion"],
                        "estado", Request.Form["estado"],
                        "imagen", img,
                        "empresa", emp,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "generaReporteInformacionProspecto":
                    pathToFiles = Server.MapPath("../logos");
                    img = pathToFiles + "\\" + imagen;
                    retorno = cx.InsertarRetorna("paCrmGeneraReporteInformacionProspecto",
                        "fechaInicio", Request.Form["fechaInicio"],
                        "fechaFin", Request.Form["fechaFin"],
                        "tipoDocumento", Request.Form["tipoDocumento"],
                        "segmento", Request.Form["segmento"],
                        "sector", Request.Form["sector"],
                        "estado", Request.Form["estado"],
                        "responsableProspecto", Request.Form["responsableProspecto"],
                        "imagen", img,
                        "empresa", emp,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "generaReporteClientes":
                    pathToFiles = Server.MapPath("../logos");
                    img = pathToFiles + "\\" + imagen;
                    retorno = cx.InsertarRetorna("paCrmGeneraReporteClientes",
                        "fechaInicio", Request.Form["fechaInicio"],
                        "fechaFin", Request.Form["fechaFin"],
                        "tipoDocumento", Request.Form["tipoDocumento"],
                        "segmento", Request.Form["segmento"],
                        "sector", Request.Form["sector"],
                        "estado", Request.Form["estado"],
                        "responsableProspecto", Request.Form["responsableProspecto"],
                        "imagen", img,
                        "empresa", emp,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "generaReporteSeguimientoProspecto":
                    pathToFiles = Server.MapPath("../logos");
                    img = pathToFiles + "\\" + imagen;
                    retorno = cx.InsertarRetorna("paCrmGenerarReporteSeguimientoProspectos",
                        "fechaInicio", Request.Form["fechaInicio"],
                        "fechaFin", Request.Form["fechaFin"],
                        "tipoDocumento", Request.Form["tipoDocumento"],
                        "segmento", Request.Form["segmento"],
                        "sector", Request.Form["sector"],
                        "estado", Request.Form["estado"],
                        "responsableProspecto", Request.Form["responsableProspecto"],
                        "imagen", img,
                        "empresa", emp,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "generaReporteSeguimientoClientes":
                    pathToFiles = Server.MapPath("../logos");
                    img = pathToFiles + "\\" + imagen;
                    retorno = cx.InsertarRetorna("paCrmGenerarReporteSeguimientoClientes",
                        "fechaInicio", Request.Form["fechaInicio"],
                        "fechaFin", Request.Form["fechaFin"],
                        "tipoDocumento", Request.Form["tipoDocumento"],
                        "segmento", Request.Form["segmento"],
                        "sector", Request.Form["sector"],
                        "estado", Request.Form["estado"],
                        "responsableProspecto", Request.Form["responsableProspecto"],
                        "imagen", img,
                        "empresa", emp,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "generaReporteContactos":
                    pathToFiles = Server.MapPath("../logos");
                    img = pathToFiles + "\\" + imagen;
                    retorno = cx.InsertarRetorna("paCrmGenerarReporteContacto",
                        "tipo", Request.Form["tipo"],
                        "estado", Request.Form["estado"],
                        "responsableContacto", Request.Form["responsableContacto"],
                        "idCliente", Request.Form["idCliente"],
                        "imagen", img,
                        "empresa", emp,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "generaReporteProgramacionActividad":
                    pathToFiles = Server.MapPath("../logos");
                    img = pathToFiles + "\\" + imagen;
                    retorno = cx.InsertarRetorna("paCrmGenerarReporteInformeActividades",
                        "fechaInicio", Request.Form["fechaInicio"],
                        "fechaFin", Request.Form["fechaFin"],
                        "idCliente", Request.Form["idCliente"],
                        "tipoActividad", Request.Form["tipoActividad"],
                        "tipo", Request.Form["tipo"],
                        "estado", Request.Form["estado"],
                        "responsableOportunidad", Request.Form["responsableOportunidad"],
                        "imagen", img,
                        "empresa", emp,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "generaReporteCampana":
                    pathToFiles = Server.MapPath("../logos");
                    img = pathToFiles + "\\" + imagen;
                    retorno = cx.InsertarRetorna("paCrmGenerarReporteCampanas",
                        "fechaInicio", Request.Form["fechaInicio"],
                        "fechaFin", Request.Form["fechaFin"],
                        "idCampana", Request.Form["campana"],
                        "tipoCampana", Request.Form["tipo"],
                        "estado", Request.Form["estado"],
                        "imagen", img,
                        "empresa", emp,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "generaReporteCotizacion":
                    pathToFiles = Server.MapPath("../logos");
                    img = pathToFiles + "\\" + imagen;
                    retorno = cx.InsertarRetorna("paCrmGenerarReporteCotizacionNueva",
                        "fechaInicio", Request.Form["fechaInicio"],
                        "fechaFin", Request.Form["fechaFin"],
                        "idCliente", Request.Form["idCliente"],
                        "tipoCliente", Request.Form["tipoCliente"],
                        "estado", Request.Form["estado"],
                        "formaPago", Request.Form["formaPago"],
                        "periodo", Request.Form["periodo"],
                        "imagen", img,
                        "empresa", emp,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "generarReporte2":
                    retorno = cx.InsertarRetorna("paGCAreportePrueba",
                        "empresa", emp,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;
                    /*
                case "generaReporteVenta":
                    pathToFiles = Server.MapPath("../logos");
                    img = pathToFiles + "\\" + imagen;
                    retorno = cx.Listar("paPosGeneraReporteVenta",
                        "idVenta", Request.Form["idVenta"],
                        "imagen", img,
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;*/

            }
        }
    }
}