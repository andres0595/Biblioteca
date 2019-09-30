using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;

namespace Inicial.Controlador
{
    public partial class ctlEmpresa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["nom_usuario"]) == null)
            {
                Response.Redirect("../vista/general/inicio.aspx");
            }

            string p = Request.Form["p"];
            string retorno = "";
            string responsable = Session["usu_sistema"].ToString();
            string empresa = Session["nit_empresa"].ToString();
            string clave = "";
            if (Request.Form["reg"].ToString() != "1")
                Session["empresa_creada"] = Session["nit_empresa"];
            else if (Request.Form["reg"].ToString() == "1" && (Session["empresa_creada"] == null || Session["empresa_creada"].ToString().Equals("")))
                Session["empresa_creada"] = "NIT";

            Modelo.ConexionBD_Sql_Server cx = new Modelo.ConexionBD_Sql_Server();

            switch (p)
            {
                /*INICIO CASOS GUARDA EMPRESA*/
                case "guardarInfoEmp":
                    retorno = cx.EjecutaRetorna("paINI_InfoGeneralEmpresa_guardar",
                                            "nit", Request.Form["nit"],
                                            "digito", Request.Form["digito"],
                                            "nombre", Request.Form["nombre"],
                                            "tipoPersona", Request.Form["tipoPersona"],
                                            "tipoContribuyente", Request.Form["tipoContribuyente"],
                                            "razonSocial", Request.Form["razonSocial"],
                                            "tipoSociedad", Request.Form["tipoSociedad"],
                                            "priApellido", Request.Form["priApellido"],
                                            "segApellido", Request.Form["segApellido"],
                                            "priNombre", Request.Form["priNombre"],
                                            "segNombre", Request.Form["segNombre"],
                                            "digi1", Request.Form["digi1"],
                                            "digi2", Request.Form["digi2"],
                                            "responsable", responsable);
                    Session["empresa_creada"] = retorno.Split('*')[1];
                    Response.Write(retorno.Split('*')[0]);
                    break;

                case "guardarInfoEcoEmp":
                    retorno = cx.InsertarRetorna("paINI_InfoEconomicaEmpresa_guardar",



                        "empresaNit ", Request.Form["empresaNit "],

                        "actividadPrincipal", Request.Form["actividadPrincipal"],
                        "actividadSecundaria", Request.Form["actividadSecundaria"],
                        "otraActividad1", Request.Form["otraActividad1"],
                        "otraActividad2", Request.Form["otraActividad2"],

                        "shd_actividadPrincipal", Request.Form["shd_actividadPrincipal"],
                        "shd_actividadSecundaria", Request.Form["shd_actividadSecundaria"],
                        "shd_otraActividad1", Request.Form["shd_otraActividad1"],
                        "shd_otraActividad2", Request.Form["shd_otraActividad2"],

                                                                        //  "codigoHacienda", Request.Form["codigoHacienda"],
                        "numSedes", Request.Form["numSedes"],
                        "valor", Request.Form["valor"],
                        "activos", Request.Form["activos"],
                        "numEmpleados", Request.Form["numEmpleados"],
                        "tamanio", Request.Form["tamanio"],
                        "responsable", responsable);
                    Response.Write("{'msj':" + retorno + "}");
                    break;

                case "guardarInfoAdmEmp":
                    retorno = cx.InsertarRetorna("paINI_InfoAdministrativaEmpresa_guardar",
                                            "nit", Request.Form["nit"],
                                            "representante", Request.Form["representante"],
                                            "tipoDocRepresentante", Request.Form["tipoDocRepresentante"],
                                            "numDocRepresentante", Request.Form["numDocRepresentante"],
                                            "contador", Request.Form["contador"],
                                            "tipoDocContador", Request.Form["tipoDocContador"],
                                            "numDocContador", Request.Form["numDocContador"],
                                            "tarjetaProfContador", Request.Form["tarjetaProfContador"],
                                            "revisorFiscal", Request.Form["revisorFiscal"],
                                            "tipoDocRevisor", Request.Form["tipoDocRevisor"],
                                            "numDocRevisor", Request.Form["numDocRevisor"],
                                            "tarjetaProfRevisor", Request.Form["tarjetaProfRevisor"],
                                            "responsable", responsable);
                    Response.Write("{'msj':" + retorno + "}");
                    break;

                case "guardarInfoFinEmp":
                    retorno = cx.InsertarRetorna("paINI_InfoFinancieraEmpresa_guardar",
                                            "idGlobal", Request.Form["idGlobal"],
                                            "nit", Request.Form["nit"],
                                            "fechaInicioFact", Request.Form["fechaInicioFact"],
                                            "fechaFinFact", Request.Form["fechaFinFact"],
                                            "resolucion", Request.Form["resolucion"],
                                            "vencimientoRes", Request.Form["vencimientoRes"],
                                            "responsable", responsable);
                    Response.Write("{'msj':" + retorno + "}");
                    break;

                case "eliminarFact":
                    retorno = cx.Ejecutar("eliminaInfoFinanciera",
                                            "id", Request.Form["id"],
                                            "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "eliminarUsuario":
                    retorno = cx.InsertarRetorna("paINI_Usuario_borrar",
                                            "id", Request.Form["id"],
                                            "empresa", empresa,
                                            "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardarInfoSedPpalEmp":
                    retorno = cx.InsertarRetorna("paINI_InfoSedePpalEmpresa_guardar",
                                            "nit", Request.Form["nit"],
                                            "nombreSede", Request.Form["nombreSede"],
                                            "municipio", Request.Form["municipio"],
                                            "direccion", Request.Form["direccion"],
                                            "telefono", Request.Form["telefono"],
                                            "email", Request.Form["mail"],
                                            "ubicacion", Request.Form["ubicacion"],
                                            "numEmpleados", Request.Form["numEmpleados"],
                                            "responsable", responsable);
                    Response.Write("{'msj':" + retorno + "}");
                    break;

                case "guardarInfoContEmp":
                    retorno = cx.InsertarRetorna("paINI_InfoContactoEmpresa_guardar",
                                            "nit", Request.Form["nit"],
                                            "nombreContacto", Request.Form["nombreContacto"],
                                            "cargoContacto", Request.Form["cargoContacto"],
                                            "telefono", Request.Form["telefono"],
                                            "email", Request.Form["email"],
                                            "responsable", responsable);
                    Response.Write("{'msj':" + retorno + "}");
                    break;

                case "guardarInfoAdmorEmp":
                    clave = Request.Form["clave"];
                    retorno = cx.InsertarRetorna("paINI_InfoAdministradorEmpresa_guardar",
                                            "nit", Request.Form["nit"],
                                            "usuario", Request.Form["usuario"],
                                            "nombre", Request.Form["nombre"],
                                            "clave", cifrarMd5(clave),
                                            "email", Request.Form["email"],
                                            "responsable", responsable);
                    if (retorno.Contains("1"))
                        enviarConfirmacion(Request.Form["usuario"].ToUpper(), clave, Request.Form["email"].ToLower());
                    Response.Write("{'msj':" + retorno + "}");
                    break;

                case "guardarInfoLogEmp":
                    if (Session["logo_empresa"] != null && !Session["logo_empresa"].ToString().Equals(""))
                    {
                        retorno = cx.InsertarRetorna("paINI_InfoLogoEmpresa_guardar",
                                                "nit", Request.Form["nit"],
                                                "logo", Session["logo_empresa"],
                                                "responsable", responsable);
                        Response.Write("{'msj':" + retorno + "}");
                    }
                    else
                    {
                        Response.Write("{'msj':3}");
                    }
                    break;
                /*FIN CASOS GUARDA EMPRESA*/
                /*caso edita*/
                case "editaInfoEmp":
                    retorno = cx.EjecutaRetorna("paINI_InfoGeneralEmpresa_guardar",
                                            "nit", Request.Form["nit"],
                                            "digito", Request.Form["digito"],
                                            "nombre", Request.Form["nombre"],
                                            "tipoPersona", Request.Form["tipoPersona"],
                                            "tipoContribuyente", Request.Form["tipoContribuyente"],
                                            "razonSocial", Request.Form["razonSocial"],
                                            "tipoSociedad", Request.Form["tipoSociedad"],
                                            "priApellido", Request.Form["priApellido"],
                                            "segApellido", Request.Form["segApellido"],
                                            "priNombre", Request.Form["priNombre"],
                                            "segNombre", Request.Form["segNombre"],
                                            "digi1", Request.Form["digi1"],
                                            "digi2", Request.Form["digi2"],
                                            "responsable", responsable);
                    Session["empresa_creada"] = retorno.Split('*')[1];
                    Response.Write(retorno.Split('*')[0]);
                    break;

                case "editaInfoEcoEmp":
                    retorno = cx.InsertarRetorna("paINI_InfoEconomicaEmpresa_guardar",
                        "empresaNit ", empresa,
                        "actividadPrincipal", Request.Form["actividadPrincipal"],
                        "actividadSecundaria", Request.Form["actividadSecundaria"],
                        "otraActividad1", Request.Form["otraActividad1"],
                        "otraActividad2", Request.Form["otraActividad2"],
                        "shd_actividadPrincipal", Request.Form["shd_actividadPrincipal"],
                        "shd_actividadSecundaria", Request.Form["shd_actividadSecundaria"],
                        "shd_otraActividad1", Request.Form["shd_otraActividad1"],
                        "shd_otraActividad2", Request.Form["shd_otraActividad2"],

                                                                        //  "codigoHacienda", Request.Form["codigoHacienda"],
                        "numSedes", Request.Form["numSedes"],
                        "valor", Request.Form["valor"],
                        "activos", Request.Form["activos"],
                        "numEmpleados", Request.Form["numEmpleados"],
                        "tamanio", Request.Form["tamanio"],
                        "responsable", responsable);
                    Response.Write("{'msj':" + retorno + "}");
                    break;

                case "editaInfoAdmEmp":
                    retorno = cx.InsertarRetorna("paINI_InfoAdministrativaEmpresa_guardar", "nit", empresa,
                                            "representante", Request.Form["representante"],
                                            "tipoDocRepresentante", Request.Form["tipoDocRepresentante"],
                                            "numDocRepresentante", Request.Form["numDocRepresentante"],
                                            "contador", Request.Form["contador"],
                                            "tipoDocContador", Request.Form["tipoDocContador"],
                                            "numDocContador", Request.Form["numDocContador"],
                                            "tarjetaProfContador", Request.Form["tarjetaProfContador"],
                                            "revisorFiscal", Request.Form["revisorFiscal"],
                                            "tipoDocRevisor", Request.Form["tipoDocRevisor"],
                                            "numDocRevisor", Request.Form["numDocRevisor"],
                                            "tarjetaProfRevisor", Request.Form["tarjetaProfRevisor"],
                                            "responsable", responsable);
                    Response.Write("{'msj':" + retorno + "}");
                    break;

                case "editaInfoFinEmp":
                    retorno = cx.InsertarRetorna("paINI_InfoFinancieraEmpresa_guardar",
                                            "nit", empresa,
                                            "idGlobal", Request.Form["idGlobal"],
                                            "fechaInicioFact", Request.Form["fechaInicioFact"],
                                            "fechaFinFact", Request.Form["fechaFinFact"],
                                            "resolucion", Request.Form["resolucion"],
                                            "vencimientoRes", Request.Form["vencimientoRes"],
                                            "responsable", responsable);
                    Response.Write("{'msj':" + retorno + "}");
                    break;


                case "editaInfoSedPpalEmp":
                    retorno = cx.InsertarRetorna("paINI_InfoSedePpalEmpresa_guardar",
                                             "nit", empresa,
                                            "nombreSede", Request.Form["nombreSede"],
                                            "municipio", Request.Form["municipio"],
                                            "direccion", Request.Form["direccion"],
                                            "telefono", Request.Form["telefono"],
                                            "email", Request.Form["mail"],
                                            "ubicacion", Request.Form["ubicacion"],
                                            "numEmpleados", Request.Form["numEmpleados"],
                                            "responsable", responsable);
                    Response.Write("{'msj':" + retorno + "}");
                    break;

                case "editaInfoContEmp":
                    retorno = cx.InsertarRetorna("paINI_InfoContactoEmpresa_guardar",
                                            "nit", empresa,
                                            "nombreContacto", Request.Form["nombreContacto"],
                                            "cargoContacto", Request.Form["cargoContacto"],
                                            "telefono", Request.Form["telefono"],
                                            "email", Request.Form["email"],
                                            "responsable", responsable);
                    Response.Write("{'msj':" + retorno + "}");
                    break;

                case "editaInfoAdmorEmp":
                    clave = Request.Form["clave"];
                    retorno = cx.InsertarRetorna("paINI_InfoAdministradorEmpresa_editar",
                                            "nit", empresa,
                                            "id", Request.Form["id"],
                                            "usuario", Request.Form["usuario"],
                                            "nombre", Request.Form["nombre"],
                                            "clave", cifrarMd5(clave),
                                            "email", Request.Form["email"],
                                            "responsable", responsable);
                    if (retorno.Contains("1"))
                        enviarConfirmacion(Request.Form["usuario"].ToUpper(), clave, Request.Form["email"].ToLower());
                    Response.Write("{'msj':" + retorno + "}");
                    break;

                case "editaInfoLogEmp":
                    if (Session["logo_empresa"] != null && !Session["logo_empresa"].ToString().Equals(""))
                    {
                        retorno = cx.InsertarRetorna("paINI_InfoLogoEmpresa_guardar", "nit", empresa,
                                                "logo", Session["logo_empresa"],
                                                "responsable", responsable);
                        Response.Write("{'msj':" + retorno + "}");
                    }
                    else
                    {
                        Response.Write("{'msj':3}");
                    }
                    break;

                /*INICIO CASOS CARGA INFORMACION EMPRESA*/
                case "cargarInfoEmp":
                    if (Session["nit_empresa"] != null)
                    {
                        retorno = cx.Buscar("paINI_InfoGeneralEmpresa_carga", "nit", Session["nit_empresa"]);
                        Response.Write(retorno);
                    }
                    else
                    {
                        Response.Write("{'msj':2}");
                    }
                    break;

                case "cargarInfoEcoEmp":
                    if (Session["nit_empresa"] != null)
                    {
                        retorno = cx.Buscar("paINI_InfoEconomicaEmpresa_carga", "nit", Session["nit_empresa"]);
                        Response.Write(retorno);
                    }
                    else
                    {
                        Response.Write("{'msj':2}");
                    }
                    break;

                case "cargarInfoAdmEmp":
                    if (Session["nit_empresa"] != null)
                    {
                        retorno = cx.Buscar("paINI_InfoAdministrativaEmpresa_carga", "nit", Session["nit_empresa"]);
                        Response.Write(retorno);
                    }
                    else
                    {
                        Response.Write("{'msj':2}");
                    }
                    break;

                case "cargarInfoFinEmp":
                    if (Session["nit_empresa"] != null)
                    {
                        retorno = cx.Buscar("paINI_InfoFinancieraEmpresa_carga", "nit", Session["nit_empresa"]);
                        Response.Write(retorno);
                    }
                    else
                    {
                        Response.Write("{'msj':2}");
                    }
                    break;

                case "cargarInfoSedPpalEmp":
                    if (Session["nit_empresa"] != null)
                    {
                        retorno = cx.Buscar("paINI_InfoSedePpalEmpresa_carga", "nit", Session["nit_empresa"]);
                        Response.Write(retorno);
                    }
                    else
                    {
                        Response.Write("{'msj':2}");
                    }
                    break;

                case "cargarInfoContEmp":
                    if (Session["nit_empresa"] != null)
                    {
                        retorno = cx.Buscar("paINI_InfoContactoEmpresa_carga", "nit", Session["nit_empresa"]);
                        Response.Write(retorno);
                    }
                    else
                    {
                        Response.Write("{'msj':2}");
                    }
                    break;

                case "cargarInfoAdmorEmp":
                    if (Session["nit_empresa"] != null)
                    {
                        retorno = cx.Buscar("paINI_InfoAdministradorEmpresa_carga", "nit", Session["nit_empresa"]);
                        Response.Write(retorno);
                    }
                    else
                    {
                        Response.Write("{'msj':2}");
                    }
                    break;

                case "cargarInfoLogEmp":
                    retorno = cx.Buscar("paINI_InfoLogoEmpresa_carga", "nit", Session["nit_empresa"]);
                    Response.Write(retorno);
                    break;
                /*FIN CASOS CARGA INFO EMPRESA*/

                /*INICIO CASOS EMPRESA PARA MOSTRAR EN DETALLE*/

                case "cargarInfoEmpDetalle":
                    if (Session["nit_empresa"] != null)
                    {
                        retorno = cx.Buscar("paINI_InfoGeneralEmpresa_cargaDetalle", "nit", Session["nit_empresa"]);
                        Response.Write(retorno);
                    }
                    else
                    {
                        Response.Write("{'msj':2}");
                    }
                    break;


                case "cargarInfoEcoEmpDetalle":
                    if (Session["nit_empresa"] != null)
                    {
                        retorno = cx.Buscar("paINI_InfoEconomicaEmpresa_cargaDetalle", "nit", Session["nit_empresa"]);
                        Response.Write(retorno);
                    }
                    else
                    {
                        Response.Write("{'msj':2}");
                    }
                    break;

                case "cargarInfoAdmEmpDetalle":
                    if (Session["nit_empresa"] != null)
                    {
                        retorno = cx.Buscar("paINI_InfoAdministrativaEmpresa_cargaDetalle", "nit", Session["nit_empresa"]);
                        Response.Write(retorno);
                    }
                    else
                    {
                        Response.Write("{'msj':2}");
                    }
                    break;

                case "cargarInfoFacturacion":
                    if (Session["nit_empresa"] != null)
                    {
                        retorno = cx.Buscar("paINI_InfoFactura_cargaDetalle", "nit", Session["nit_empresa"]);
                        Response.Write(retorno);
                    }
                    else
                    {
                        Response.Write("{'msj':2}");
                    }
                    break;

                case "cargarInfoSedPpalEmpDetalle":
                    if (Session["nit_empresa"] != null)
                    {
                        retorno = cx.Buscar("paINI_InfoSedePpalEmpresa_cargaDetalle", "nit", Session["nit_empresa"]);
                        Response.Write(retorno);
                    }
                    else
                    {
                        Response.Write("{'msj':2}");
                    }
                    break;

                case "cargarInfoContEmpDetalle":
                    if (Session["nit_empresa"] != null)
                    {
                        retorno = cx.Buscar("paINI_InfoContactoEmpresa_cargaDetalle", "nit", Session["nit_empresa"]);
                        Response.Write(retorno);
                    }
                    else
                    {
                        Response.Write("{'msj':2}");
                    }
                    break;

                case "cargarInfoAdmorEmpDetalle":
                    if (Session["nit_empresa"] != null)
                    {
                        retorno = cx.Buscar("paINI_InfoAdministradorEmpresa_cargaDetalle", "nit", Session["nit_empresa"]);
                        Response.Write(retorno);
                    }
                    else
                    {
                        Response.Write("{'msj':2}");
                    }
                    break;             
            }
        }

        /// <summary>
        /// Envía un correo de confirmación al usuario ingresado.
        /// </summary>
        /// <param name="usuario">El nombre de usuario ingresado.</param>
        /// <param name="clave">La clave del usuario</param>
        /// <param name="email">El email para la confirmación</param>
        protected void enviarConfirmacion(string usuario, string clave, string email)
        {
            string ip = Request.ServerVariables["REMOTE_ADDR"];
            string pc = Request.ServerVariables["REMOTE_HOST"];
            string server = Request.ServerVariables["SERVER_NAME"];

            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();

            correo.From = new System.Net.Mail.MailAddress("lcwebc2011@gmail.com");
            correo.To.Add(email);
            correo.Subject = "Registro en ERP";
            correo.Body = "Se ha registrado con éxito en ERP de LC Web.\n\n Para acceder al sistema copie y pegue el siguiente link como URL:\n productos.lcweb.co/erp\n\nUsuario: " + usuario + "\nContraseña: " + clave + "\n\n" + "\n\nFecha y hora UTC: " +
                DateTime.Now.ToUniversalTime().ToString("dd/MM/yyyy HH:mm:ss") + "\n\nSe accedió desde la siguiente ubicación:\n" + ip + "\n" + pc + "\n" + server;
            correo.IsBodyHtml = false;
            correo.Priority = System.Net.Mail.MailPriority.Normal;
            //
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

            smtp.Credentials = new System.Net.NetworkCredential("lcwebc2011", "LCWEBC2011*");
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(correo);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }


        /// <summary>
        /// Cifra la clave en MD5 Hash
        /// </summary>
        /// <param name="clave">La clave original</param>
        /// <returns>La clave cifrada en MD5</returns>
        private string cifrarMd5(string clave)
        {

            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();

            byte[] data = System.Text.Encoding.ASCII.GetBytes(clave);
            data = provider.ComputeHash(data);

            string md5 = string.Empty;

            for (int i = 0; i < data.Length; i++)
            {
                md5 += data[i].ToString("x2").ToLower();
            }

            return md5;
        }
    }
}