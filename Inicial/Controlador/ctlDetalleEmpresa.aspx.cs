using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Xml;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.html;
using iTextSharp.text.xml;

namespace Inicial.Controlador
{
    public partial class ctlDetalleEmpresa : System.Web.UI.Page
    {
        //string[] arreglo_actividades;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private String limpiarHTML(String xhtmlString)
        {
            if (!xhtmlString.ToLower().StartsWith("<p>"))
            {
                xhtmlString = "<p>" + xhtmlString + "</p>";
            }

            xhtmlString = xhtmlString.Replace("\r", string.Empty);
            xhtmlString = xhtmlString.Replace("\n", string.Empty);
            xhtmlString = xhtmlString.Replace("\t", string.Empty);

            xhtmlString = xhtmlString.Replace("<BR>", "<br />");
            xhtmlString = xhtmlString.Replace("<br>", "<br />");

            System.Text.RegularExpressions.Regex re = null;
            System.Text.RegularExpressions.Match match = null;

            re = new System.Text.RegularExpressions.Regex("<span.*?>");
            match = re.Match(xhtmlString);
            while (match.Success)
            {
                foreach (System.Text.RegularExpressions.Capture c in match.Captures)
                {
                    xhtmlString = xhtmlString.Replace(c.Value, string.Empty);
                }
                match = match.NextMatch();
            }

            re = new System.Text.RegularExpressions.Regex("<\\w+?");
            match = re.Match(xhtmlString);

            while (match.Success)
            {
                foreach (System.Text.RegularExpressions.Capture c in match.Captures)
                {
                    xhtmlString = xhtmlString.Replace(c.Value, c.Value.ToLower());
                }
                match = match.NextMatch();
            }

            re = new System.Text.RegularExpressions.Regex("</\\w+?>");
            match = re.Match(xhtmlString);
            while (match.Success)
            {
                foreach (System.Text.RegularExpressions.Capture c in match.Captures)
                {
                    xhtmlString = xhtmlString.Replace(c.Value, c.Value.ToLower());
                }
                match = match.NextMatch();
            }

            while (xhtmlString.Contains("> "))
            {
                xhtmlString = xhtmlString.Replace("> ", ">");
            }
            while (xhtmlString.Contains("  "))
            {
                xhtmlString = xhtmlString.Replace("  ", " ");
            }

            xhtmlString = xhtmlString.Replace(" & ", " &amp; ");

            int length = 0;
            while (length != xhtmlString.Length)
            {
                length = xhtmlString.Length;
                xhtmlString = System.Text.RegularExpressions.Regex.Replace(xhtmlString, "(<.+?\\s+\\w+=)([^\"']\\S*?)([\\s>])", "$1\"$2\"$3");
            }
            return xhtmlString;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            MemoryStream mem = new MemoryStream();
            StreamWriter twr = new StreamWriter(mem);
            HtmlTextWriter myWriter = new HtmlTextWriter(twr);
            base.Render(myWriter);
            myWriter.Flush();
            myWriter.Dispose();
            StreamReader strmRdr = new StreamReader(mem);
            strmRdr.BaseStream.Position = 0;
            string pageContent = strmRdr.ReadToEnd();
            strmRdr.Dispose();
            mem.Dispose();
            writer.Write(pageContent);
            CreatePDFDocument(pageContent);
        }

        public void CreatePDFDocument(string strHtml)
        {

            Inicial.Modelo.ConexionBD_Sql_Server cx = new Modelo.ConexionBD_Sql_Server();
            string empresa = Session["nit_empresa"].ToString();
            string tipo = Request.Form["tipo"];
            string filepath = "";
            string vacio = "--";
            String tabla1 = "";
            String tabla2 = "";
            String tabla3 = "";
            String tabla4 = "";
            String tabla5 = "";
            String tabla6 = "";
            String tabla7 = "";
            String tabla8 = "";

            string nombreDocumento = "empresa.pdf";
            string strFileName = HttpContext.Current.Server.MapPath("~/Reportes/" + nombreDocumento);

            filepath = Request.PhysicalApplicationPath + "\\Reportes\\" + nombreDocumento;
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }

            cx = new Modelo.ConexionBD_Sql_Server();
            IDataReader infoEmpresa = cx.Paginar("cargaInfoGeneralEmpresaDetalle", "nit", Session["nit_empresa"]);

            try
            {
                string nit = "";
                string nombre = "";
                string tipoPersona = "";
                string nombreContri = "";
                string razonSocial = "";
                string nomTipSociedad = "";
                string imagen = "";
                string priApellido = "";
                string segApellido = "";
                string priNombre = "";
                string segNombre = "";

                while (infoEmpresa.Read())
                {
                    nit = infoEmpresa.GetValue(0).ToString();
                    nombre = infoEmpresa.GetValue(1).ToString();
                    tipoPersona = infoEmpresa.GetValue(2).ToString();
                    nombreContri = infoEmpresa.GetValue(4).ToString();


                    nombre = (nombre.Equals("")) ? vacio : nombre;
                    nombreContri = (nombreContri.Equals("-- SELECCIONE --")) ? vacio : nombreContri;

                    switch (tipoPersona)
                    {
                        case "1":

                            razonSocial = infoEmpresa.GetValue(5).ToString();
                            nomTipSociedad = infoEmpresa.GetValue(7).ToString();
                            imagen = infoEmpresa.GetValue(8).ToString();

                            razonSocial = (razonSocial.Equals("")) ? vacio : razonSocial;
                            nomTipSociedad = (nomTipSociedad.Equals("-- SELECCIONE --")) ? vacio : nomTipSociedad;
                            imagen = (imagen.Equals("")) ? "lcweb.png" : imagen;

                            tabla2 += "<table border='1'><tr  bgcolor=\"#BFBFBF\"><td colspan='6' align='center'>INFORMACIÓN GENERAL DE LA EMPRESA</td></tr>";
                            tabla2 += "<tr bgcolor=\"#BFBFBF\"><td  align='center'>NOMBRE</td><td  align='center'>NIT</td><td  align='center'>TIPO PERSONA</td><td  align='center'>TIPO CONTRIBUYENTE</td><td  align='center'>RAZÓN SOCIAL</td><td  align='center'>TIPO DE SOCIEDAD</td></tr>";

                            tabla2 += "<tr>";
                            tabla2 += "<td>" + nombre + "</td>";
                            tabla2 += "<td>" + nit + "</td>";
                            tabla2 += "<td>JURIDICA</td>";
                            tabla2 += "<td>" + nombreContri + "</td>";
                            tabla2 += "<td>" + razonSocial + "</td>";
                            tabla2 += "<td>" + nomTipSociedad + "</td>";
                            tabla2 += "</tr>";
                            break;
                        case "2":

                            priApellido = infoEmpresa.GetValue(5).ToString();
                            segApellido = infoEmpresa.GetValue(6).ToString();
                            priNombre = infoEmpresa.GetValue(7).ToString();
                            segNombre = infoEmpresa.GetValue(8).ToString();
                            imagen = infoEmpresa.GetValue(9).ToString();

                            priApellido = (priApellido.Equals("")) ? vacio : priApellido;
                            segApellido = (segApellido.Equals("")) ? vacio : segApellido;
                            priNombre = (priNombre.Equals("")) ? vacio : priNombre;
                            segNombre = (segNombre.Equals("")) ? vacio : segNombre;
                            imagen = (imagen.Equals("")) ? "lcweb.png" : imagen;

                            tabla2 += "<table border='1'><tr  bgcolor=\"#BFBFBF\"><td colspan='5'>INFORMACIÓN GENERAL DE LA EMPRESA</td></tr>";
                            tabla2 += "<tr bgcolor=\"#BFBFBF\"><td  align='center'>NOMBRE</td><td  align='center'>NIT</td><td  align='center'>TIPO PERSONA</td><td  align='center'>TIPO CONTRIBUYENTE</td><td  align='center'>NOMBRE COMPLETO</td></tr>";

                            tabla2 += "<tr>";
                            tabla2 += "<td>" + nombre + "</td>";
                            tabla2 += "<td>" + nit + "</td>";
                            tabla2 += "<td>NATURAL</td>";
                            tabla2 += "<td>" + priNombre + " " + segNombre + " " + priApellido + " " + segApellido + "</td>";

                            break;
                    }

                }
                tabla2 += "</table>";

                string logo = HttpContext.Current.Server.MapPath("~/logos/" + imagen);

                tabla1 += "<table><tr><td><img src=\"" + logo + "\" /></td><td>INFORMACION DE LA EMPRESA</td></tr></table>";


            }
            catch (Exception)
            {
                throw;
            }
            cx.Desconectar();
            strHtml = strHtml.Replace("$tabla1$", tabla1);
            strHtml = strHtml.Replace("$tabla2$", tabla2);

            cx = new Modelo.ConexionBD_Sql_Server();
            IDataReader infoEconomica = cx.Paginar("cargaInfoEconomicaEmpresaDetalle", "nit", Session["nit_empresa"]);

            try
            {
                string division = "";
                string grupo = "";
                string clase = "";
                string actEconomica = "";
                string sedes = "";
                string valor = "";
                string activos = "";
                string empleados = "";
                string tamanio = "";

                while (infoEconomica.Read())
                {
                    division = infoEconomica.GetValue(0).ToString();
                    grupo = infoEconomica.GetValue(1).ToString();
                    clase = infoEconomica.GetValue(2).ToString();
                    actEconomica = infoEconomica.GetValue(3).ToString();
                    sedes = infoEconomica.GetValue(4).ToString();
                    valor = infoEconomica.GetValue(5).ToString();
                    activos = infoEconomica.GetValue(6).ToString();
                    empleados = infoEconomica.GetValue(7).ToString();
                    tamanio = infoEconomica.GetValue(8).ToString();


                    division = (division.Equals("-- SELECCIONE --")) ? vacio : division;
                    grupo = (grupo.Equals("-- SELECCIONE --")) ? vacio : grupo;
                    clase = (clase.Equals("-- SELECCIONE --")) ? vacio : clase;
                    actEconomica = (actEconomica.Equals("-- SELECCIONE --")) ? vacio : actEconomica;

                    sedes = (sedes.Equals("")) ? vacio : sedes;
                    valor = (valor.Equals("")) ? vacio : valor;
                    activos = (activos.Equals("")) ? vacio : activos;
                    empleados = (empleados.Equals("")) ? vacio : empleados;
                    tamanio = (tamanio.Equals("-- SELECCIONE --")) ? vacio : tamanio;


                    tabla3 += "<table border='1'><tr  bgcolor=\"#BFBFBF\"><td colspan='5' align='center'>INFORMACIÓN ECÓNOMICA</td></tr>";
                    tabla3 += "<tr><td bgcolor=\"#BFBFBF\"  align='center'>DIVISIÓN</td><td colspan='4'>" + division + "</td></tr>";
                    tabla3 += "<tr><td bgcolor=\"#BFBFBF\"  align='center'>GRUPO</td><td colspan='4'>" + grupo + "</td></tr>";
                    tabla3 += "<tr><td bgcolor=\"#BFBFBF\"  align='center'>CLASE</td><td colspan='4'>" + clase + "</td></tr>";
                    tabla3 += "<tr><td bgcolor=\"#BFBFBF\"  align='center'>ACTIVIDADES ECONÓMICAS</td><td colspan='4'>" + actEconomica + "</td></tr>";
                    tabla3 += "<tr bgcolor=\"#BFBFBF\"><td align='center'>CANTIDAD SEDES</td><td align='center'>VALOR</td><td align='center'>ACTIVOS (SMMLV)</td><td align='center'>NUM. EMPLEADOS</td><td align='center'>TAMA&Ntilde;O</td></tr>";
                    tabla3 += "<tr><td>" + sedes + "</td><td>" + valor + "</td><td>" + activos + "</td><td>" + empleados + "</td><td>" + tamanio + "</td></tr></table>";

                }

            }
            catch (Exception)
            {
                throw;
            }

            cx.Desconectar();
            strHtml = strHtml.Replace("$tabla3$", tabla3);

            cx = new Modelo.ConexionBD_Sql_Server();
            IDataReader infoAdministrativa = cx.Paginar("cargaInfoAdministrativaEmpresaDetalle", "nit", Session["nit_empresa"]);

            try
            {
                string representanteLegal = "";
                string repTipoDoc = "";
                string repNumDoc = "";
                string contador = "";
                string conTipoDoc = "";
                string conNumDoc = "";
                string conTP = "";
                string fiscal = "";
                string fisTipoDoc = "";
                string fisNumDoc = "";
                string fisTP = "";


                while (infoAdministrativa.Read())
                {
                    representanteLegal = infoAdministrativa.GetValue(0).ToString();
                    repTipoDoc = infoAdministrativa.GetValue(1).ToString();
                    repNumDoc = infoAdministrativa.GetValue(2).ToString();
                    contador = infoAdministrativa.GetValue(3).ToString();
                    conTipoDoc = infoAdministrativa.GetValue(4).ToString();
                    conNumDoc = infoAdministrativa.GetValue(5).ToString();
                    conTP = infoAdministrativa.GetValue(6).ToString();
                    fiscal = infoAdministrativa.GetValue(7).ToString();
                    fisTipoDoc = infoAdministrativa.GetValue(8).ToString();
                    fisNumDoc = infoAdministrativa.GetValue(9).ToString();
                    fisTP = infoAdministrativa.GetValue(10).ToString();

                    representanteLegal = (representanteLegal.Equals("")) ? vacio : representanteLegal;
                    repTipoDoc = (repTipoDoc.Equals("-- SELECCIONE --")) ? vacio : repTipoDoc;
                    repNumDoc = (repNumDoc.Equals("")) ? vacio : repNumDoc;
                    contador = (contador.Equals("")) ? vacio : contador;
                    conTipoDoc = (conTipoDoc.Equals("-- SELECCIONE --")) ? vacio : conTipoDoc;
                    conNumDoc = (conNumDoc.Equals("")) ? vacio : conNumDoc;
                    conTP = (conTP.Equals("")) ? vacio : conTP;
                    fiscal = (fiscal.Equals("")) ? vacio : fiscal;
                    fisTipoDoc = (fisTipoDoc.Equals("-- SELECCIONE --")) ? vacio : fisTipoDoc;
                    fisNumDoc = (fisNumDoc.Equals("")) ? vacio : fisNumDoc;
                    fisTP = (fisTP.Equals("")) ? vacio : fisTP;

                    tabla4 += "<table border='1'><tr  bgcolor=\"#BFBFBF\"><td colspan='4' align='center'>INFORMACIÓN ADMINISTRATIVA</td></tr>";
                    tabla4 += "<tr><td bgcolor=\"#BFBFBF\"  align='center'>NOMBRE REPRESENTANTE LEGAL</td><td bgcolor=\"#BFBFBF\"  align='center'>TIPO DOCUMENTO</td><td bgcolor=\"#BFBFBF\"   colspan='2' align='center'>NÚMERO DOCUMENTO</td></tr>";
                    tabla4 += "<tr><td>" + representanteLegal + "</td><td>" + repTipoDoc + "</td><td colspan='2'>" + repNumDoc + "</td></tr>";
                    tabla4 += "<tr><td bgcolor=\"#BFBFBF\"  align='center'>NOMBRE CONTADOR</td><td bgcolor=\"#BFBFBF\" align='center'>TIPO DOCUMENTO</td><td bgcolor=\"#BFBFBF\" align='center'>NÚMERO DOCUMENTO</td><td bgcolor=\"#BFBFBF\" align='center'>T.P</td></tr>";
                    tabla4 += "<tr><td>" + contador + "</td><td>" + conTipoDoc + "</td><td>" + conNumDoc + "</td><td>" + conTP + "</td></tr>";
                    tabla4 += "<tr><td bgcolor=\"#BFBFBF\"  align='center'>NOMBRE REVISOR FISCAL</td><td bgcolor=\"#BFBFBF\" align='center'>TIPO DOCUMENTO</td><td bgcolor=\"#BFBFBF\" align='center'>NÚMERO DOCUMENTO</td><td bgcolor=\"#BFBFBF\" align='center'>T.P</td></tr>";
                    tabla4 += "<tr><td>" + fiscal + "</td><td>" + fisTipoDoc + "</td><td>" + fisNumDoc + "</td><td>" + fisTP + "</td></tr></table>";
                }

            }
            catch (Exception)
            {
                throw;
            }

            cx.Desconectar();
            strHtml = strHtml.Replace("$tabla4$", tabla4);



            cx = new Modelo.ConexionBD_Sql_Server();
            IDataReader infoFacturacion = cx.Paginar("listaInfoFacturaDetalle", "nit", Session["nit_empresa"]);

            try
            {
                string id = "";
                string fechaIni = "";
                string fechaFin = "";
                string resolucion = "";
                string fechaVencRes = "";
                int ctl = 0;
                while (infoFacturacion.Read())
                {
                    tabla5 += "<table border='1'><tr  bgcolor=\"#BFBFBF\"><td colspan='4' align='center'>INFORMACI&Oacute;N FACTURACI&Oacute;N</td></tr>";
                    tabla5 += "<tr><td bgcolor=\"#BFBFBF\"  align='center'>INICIO FACTURACIÓN</td><td bgcolor=\"#BFBFBF\"  align='center'>FIN FACTURACIÓN</td><td bgcolor=\"#BFBFBF\" align='center'>RESOLUCIÓN</td><td bgcolor=\"#BFBFBF\" align='center'>FECHA VENCIMIENTO RESOLUCIÓN</td></tr>";
                    int count = infoFacturacion.FieldCount;

                    for (int i = 0; i < count; i += 5)
                    {
                        id = infoFacturacion.GetValue(i).ToString();
                        fechaIni = infoFacturacion.GetValue(i + 1).ToString();
                        fechaFin = infoFacturacion.GetValue(i + 2).ToString();
                        resolucion = infoFacturacion.GetValue(i + 3).ToString();
                        fechaVencRes = infoFacturacion.GetValue(i + 4).ToString();

                        fechaIni = (fechaIni.Equals("")) ? vacio : fechaIni;
                        fechaFin = (fechaFin.Equals("")) ? vacio : fechaFin;
                        resolucion = (resolucion.Equals("")) ? vacio : resolucion;
                        fechaVencRes = (fechaVencRes.Equals("")) ? vacio : fechaVencRes;

                        tabla5 += "<tr><td>" + fechaIni + "</td><td>" + fechaFin + "</td><td>" + resolucion + "</td><td>" + fechaVencRes + "</td></tr>";
                        ctl = 1;
                    }
                }

                if (ctl.Equals(0))
                {
                    tabla5 += "<table border='1'><tr  bgcolor=\"#BFBFBF\"><td colspan='4' align='center'>INFORMACI&Oacute;N FACTURACI&Oacute;N</td></tr>";
                    tabla5 += "<tr><td bgcolor=\"#BFBFBF\"  align='center'>INICIO FACTURACIÓN</td><td bgcolor=\"#BFBFBF\"  align='center'>FIN FACTURACIÓN</td><td bgcolor=\"#BFBFBF\" align='center'>RESOLUCIÓN</td><td bgcolor=\"#BFBFBF\" align='center'>FECHA VENCIMIENTO RESOLUCIÓN</td></tr>";
                    tabla5 += "<tr><td>--</td><td>--</td><td>--</td><td>--</td></tr>";
                }

                tabla5 += "</table>";

            }
            catch (Exception)
            {
                throw;
            }

            cx.Desconectar();
            strHtml = strHtml.Replace("$tabla5$", tabla5);




            cx = new Modelo.ConexionBD_Sql_Server();
            IDataReader infoSedePrincipal = cx.Paginar("cargaInfoSedePpalEmpresaDetalle", "nit", Session["nit_empresa"]);

            try
            {
                string sedePrincipal = "";
                string pais = "";
                string depto = "";
                string muni = "";
                string direccion = "";
                string telefono = "";
                string email = "";
                string ubicacion = "";
                string empleados = "";


                while (infoSedePrincipal.Read())
                {
                    sedePrincipal = infoSedePrincipal.GetValue(0).ToString();
                    pais = infoSedePrincipal.GetValue(1).ToString();
                    depto = infoSedePrincipal.GetValue(2).ToString();
                    muni = infoSedePrincipal.GetValue(3).ToString();
                    direccion = infoSedePrincipal.GetValue(4).ToString();
                    telefono = infoSedePrincipal.GetValue(5).ToString();
                    email = infoSedePrincipal.GetValue(6).ToString();
                    ubicacion = infoSedePrincipal.GetValue(7).ToString();
                    empleados = infoSedePrincipal.GetValue(8).ToString();

                    sedePrincipal = (sedePrincipal.Equals("")) ? vacio : sedePrincipal;
                    pais = (pais.Equals("-- SELECCIONE --")) ? vacio : pais;
                    depto = (depto.Equals("-- SELECCIONE --")) ? vacio : depto;
                    muni = (muni.Equals("-- SELECCIONE --")) ? vacio : muni;
                    direccion = (direccion.Equals("")) ? vacio : direccion;
                    telefono = (telefono.Equals("")) ? vacio : telefono;
                    email = (email.Equals("")) ? vacio : email;
                    ubicacion = (ubicacion.Equals("-- SELECCIONE --")) ? vacio : ubicacion;
                    empleados = (empleados.Equals("")) ? vacio : empleados;

                    tabla6 += "<table border='1'><tr  bgcolor=\"#BFBFBF\"><td colspan='7' align='center'>INFORMACIÓN  SEDE PRINCIPAL</td></tr>";
                    tabla6 += "<tr><td bgcolor=\"#BFBFBF\"  align='center'>NOMBRE SEDE</td><td bgcolor=\"#BFBFBF\" align='center'>PAIS /DEPTO /MUNICIPIO</td>";
                    tabla6 += "<td bgcolor=\"#BFBFBF\" align='center'>DIRECCIÓN</td><td bgcolor=\"#BFBFBF\" align='center'>TELÉFONO</td>";
                    tabla6 += "<td bgcolor=\"#BFBFBF\" align='center'>EMAIL</td><td bgcolor=\"#BFBFBF\" align='center'>UBICACIÓN</td><td bgcolor=\"#BFBFBF\" align='center'>N° EMPLEADOS</td></tr>";
                    tabla6 += "<tr><td>" + sedePrincipal + "</td><td>" + pais + "/ " + depto + "/ " + muni + "</td><td>" + direccion + "</td><td>" + telefono + "</td><td>" + email + "</td>";
                    tabla6 += "<td>" + ubicacion + "</td><td>" + empleados + "</td></tr></table>";
                }

            }
            catch (Exception)
            {
                throw;
            }


            cx.Desconectar();
            strHtml = strHtml.Replace("$tabla6$", tabla6);




            cx = new Modelo.ConexionBD_Sql_Server();
            IDataReader infoContacto = cx.Paginar("cargaInfoContactoEmpresaDetalle", "nit", Session["nit_empresa"]);

            try
            {
                string nombre = "";
                string cargo = "";
                string telefono = "";
                string email = "";


                while (infoContacto.Read())
                {
                    nombre = infoContacto.GetValue(0).ToString();
                    cargo = infoContacto.GetValue(1).ToString();
                    telefono = infoContacto.GetValue(2).ToString();
                    email = infoContacto.GetValue(3).ToString();

                    nombre = (nombre.Equals("")) ? vacio : nombre;
                    cargo = (cargo.Equals("-- SELECCIONE --")) ? vacio : cargo;
                    telefono = (telefono.Equals("")) ? vacio : telefono;
                    email = (email.Equals("")) ? vacio : email;

                    tabla7 += "<table border='1'><tr  bgcolor=\"#BFBFBF\"><td colspan='4' align='center'>INFORMACIÓN CONTACTO DE LA EMPRESA</td></tr>";
                    tabla7 += "<tr><td bgcolor=\"#BFBFBF\"  align='center'>NOMBRE</td><td bgcolor=\"#BFBFBF\" align='center'>CARGO</td>";
                    tabla7 += "<td bgcolor=\"#BFBFBF\" align='center'>TELÉFONO</td><td bgcolor=\"#BFBFBF\" align='center'>EMAIL</td></tr>";
                    tabla7 += "<tr><td>" + nombre + "</td><td>" + cargo + "</td><td>" + telefono + "</td><td>" + email + "</td></tr></table>";

                }

            }
            catch (Exception)
            {
                throw;
            }


            cx.Desconectar();
            strHtml = strHtml.Replace("$tabla7$", tabla7);





            cx = new Modelo.ConexionBD_Sql_Server();
            IDataReader infoAdminEmpresa = cx.Paginar("cargaInfoAdministradorEmpresaDetalle", "nit", Session["nit_empresa"]);

            try
            {
                string usuario = "";
                string nombre = "";
                string email = "";
                int ctl = 0;

                while (infoAdminEmpresa.Read())
                {
                    usuario = infoAdminEmpresa.GetValue(0).ToString();
                    nombre = infoAdminEmpresa.GetValue(1).ToString();
                    email = infoAdminEmpresa.GetValue(2).ToString();

                    usuario = (usuario.Equals("")) ? vacio : usuario;
                    nombre = (nombre.Equals("")) ? vacio : nombre;
                    email = (email.Equals("")) ? vacio : email;

                    tabla8 += "<table border='1'><tr  bgcolor=\"#BFBFBF\"><td colspan='3' align='center'>INFORMACIÓN ADMINISTRADOR DEL SISTEMA</td></tr>";
                    tabla8 += "<tr><td bgcolor=\"#BFBFBF\"  align='center'>USUARIO</td><td bgcolor=\"#BFBFBF\" align='center'>NOMBRE</td><td bgcolor=\"#BFBFBF\" align='center'>EMAIL</td></tr>";

                    tabla8 += "<tr><td>" + usuario + "</td><td>" + nombre + "</td><td>" + email + "</td></tr></table>";
                    ctl = 1;
                }

                if (ctl.Equals(0))
                {
                    tabla8 += "<table border='1'><tr  bgcolor=\"#BFBFBF\"><td colspan='3' align='center'>INFORMACIÓN ADMINISTRADOR DEL SISTEMA</td></tr>";
                    tabla8 += "<tr><td bgcolor=\"#BFBFBF\"  align='center'>USUARIO</td><td bgcolor=\"#BFBFBF\" align='center'>NOMBRE</td><td bgcolor=\"#BFBFBF\" align='center'>EMAIL</td></tr>";
                    tabla8 += "<tr><td>--</td><td>--</td><td>--</td></tr></table>";
                }

            }
            catch (Exception)
            {
                throw;
            }

            cx.Desconectar();
            strHtml = strHtml.Replace("$tabla8$", tabla8);


             


            iTextSharp.text.html.simpleparser.StyleSheet style = new iTextSharp.text.html.simpleparser.StyleSheet();
            style.LoadTagStyle("img", "width", "100px");
            Document document = new Document();

            PdfWriter pdf = PdfWriter.GetInstance(document, new FileStream(strFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite));
            using (pdf)
            {
                //PdfWriter pdf = PdfWriter.GetInstance(document, new FileStream(strFileName, FileMode.Create));

                //PdfWriter.GetInstance(document, new FileStream(strFileName, FileMode.Create));
                String html = limpiarHTML(strHtml);

                StringReader se = new StringReader(html);
                HTMLWorker obj = new HTMLWorker(document);

                itsEvents ev = new itsEvents();
                pdf.PageEvent = ev;

                document.Open();

                obj.SetStyleSheet(style);
                obj.Parse(se);
                document.Close();
            }


            Response.Write("{'msj':1}");


        }

        public class itsEvents : PdfPageEventHelper
        {
            protected PdfTemplate total;
            protected BaseFont helv;
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {// Se crea el template
                total = writer.DirectContent.CreateTemplate(100, 100);
                total.BoundingBox = new Rectangle(-20, -20, 100, 100);
                helv = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);
            }

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                PdfContentByte cb = writer.DirectContent;
                cb.SaveState();
                cb.BeginText();
                cb.SetFontAndSize(helv, 9);
                string sPiePagina = "";

                float textSize = 6;
                float textBase = 15; // Este lo pone la informacion en la parte inferior
                // float textBase = 600; // Este pone la informacion en la parte superior

                sPiePagina = "Página " + writer.PageNumber;
                cb.SetTextMatrix(document.Left, textBase - 5);
                cb.ShowText(sPiePagina);
                cb.EndText();
                cb.AddTemplate(total, document.Left + textSize, textBase);

                cb.RestoreState();
            }

        }



    }
}