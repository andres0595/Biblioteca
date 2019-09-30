using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO.Ports;
using System.IO;
using System.Web.SessionState;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.Timers;
using System.Diagnostics;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Collections;
using System.Management;

namespace Inicial.Controlador
{
    public partial class ctlGeneral : System.Web.UI.Page
    {
        public static string gDate = "-2";
        public static string gDate2;
        public static string cGlobal = "-1";
        public static string tGlobal = "-1";
        public static string mGlobal = "-1";
        public static string aGlobal = "-1";

        //  public static string p;
        protected void Page_Load(object sender, EventArgs e)
        {



            if ((Session["nom_usuario"]) == null)
            {
                Response.Redirect("../vista/general/inicio.aspx");
            }
            string p = Request.Form["p"]; ;

            string retorno = "";
            string retorno1 = "";
            string retorno2 = "";
            string retorno3 = "";
            string retorno4 = "";
            string retorno5 = "";
            string responsable = Session["usu_sistema"].ToString();
            string empresa = Session["nit_empresa"].ToString();
            string fecha = DateTime.Now.ToString();
            string cadena = "";
            string usuario = Session["nom_usuario"].ToString();
            string localIP = Request.ServerVariables["REMOTE_ADDR"];

            Session["Anio_sesion"] = "";



            Modelo.ConexionBD_Sql_Server cx = new Modelo.ConexionBD_Sql_Server();



            //  Modelo.ConexionBD_Sql_Server cx = new Modelo.ConexionBD_Sql_Server();


            //if (gDate != null)
            //{
            //    if (gDate.Length >= 10)
            //    {

            //        p = "leertajeta";
            //    }
            //}






            switch (p)
            {
                case "nivelAcceso":
                    retorno = cx.Listar("cargaNivelAcceso",
                    "responsable", Session["usu_sistema"]);
                    Response.Write(retorno);
                    break;

                case "iralseguimiento":

                    Session["contador_seg_per"] = Request.Form["contador"].ToString();

                    //                    Request.Form["contador"] = Session["contador_seg_per"].ToString() ;
                    gDate = Request.Form["contador"];
                    //   cadena = Session["contador_seg_per"].ToString();

                    break;


                case "iralcargue":

                    cGlobal = Request.Form["contador"]; //número de semana
                    tGlobal = Request.Form["tipo"]; // si es individual o masivo
                    mGlobal = Request.Form["mes"]; //número del mes
                    aGlobal = Request.Form["anio"]; // año


                    break;


                case "verTodosCargue":

                    cGlobal = "-1";
                    tGlobal = "-1";
                    mGlobal = "-1";
                    aGlobal = "-1";

                    break;

                case "cargaPais":
                    retorno = cx.Listar("paINI_Pais_cargar");
                    Response.Write(retorno);
                    break;

                case "cargaDepto":
                    retorno = cx.Listar("paINI_Departamento_cargar");
                    //"pais", Request.Form["pais"]
                    Response.Write(retorno);
                    break;

                case "cargaMpio":
                    retorno = cx.Listar("paINI_Municipio_cargar", "depto", Request.Form["depto"]);
                    Response.Write(retorno);
                    break;

                case "cargaDeptoLista":
                    retorno = cx.Listar("cargaDepartamentoLista", "pais", Request.Form["pais"]);
                    Response.Write(retorno);
                    break;

                case "cargaMpioLista":
                    retorno = cx.Listar("cargaMunicipioLista", "depto", Request.Form["departamento"]);
                    Response.Write(retorno);
                    break;

                case "cargaTipoDocPersona":
                    retorno = cx.Listar("paINI_TipoDocumento_cargar");
                    Response.Write(retorno);
                    break;


                case "cargaTipoDocuPersona":
                    retorno = cx.Listar("paINI_TipoDocTercero_cargar");
                    Response.Write(retorno);
                    break;

                case "cargaTipoUbic":
                    retorno = cx.Listar("cargaUbicacion");
                    Response.Write(retorno);
                    break;

                case "cargaListaCargos":
                    retorno = cx.Listar("paINI_Cargos_cargar",
                        "id_area", Request.Form["id_area"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaTipCont":
                    retorno = cx.Listar("cargarTipoContribuyente");
                    Response.Write(retorno);
                    break;

                case "cargaTipPers":
                    retorno = cx.Listar("cargarTipoPersona");
                    Response.Write(retorno);
                    break;

                case "cargaTipSoc":
                    retorno = cx.Listar("cargaTipoSociedad");
                    Response.Write(retorno);
                    break;

                case "cargaListaCIIU":
                    retorno = cx.Listar("cargaCodigosCIIU", "nivel", Request.Form["nivel"]);
                    Response.Write(retorno);
                    break;

                case "cargaPaisAutocompletar":
                    retorno = cx.Listar("cargaPaisAutocompletar");
                    Response.Write(retorno);
                    break;

                case "cargaCiudadAutocompletar":
                    retorno = cx.Listar("cargaCiudadAutocompletar", "codAlfaPais", Request.Form["codAlfaPais"]);
                    Response.Write(retorno);
                    break;

                case "cargaClienteAutocompletar":
                    retorno = cx.Listar("cargaClienteAutocompletar", "empresa", empresa, "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaDepartamentoEmpresa":
                    retorno = cx.Listar("paINI_DepartamentoEmpresa_carga",
                        "empresa", empresa);
                    Response.Write(retorno);
                    break;


                case "cargaCargo":
                    retorno = cx.Listar("cargaCargo",
                        "empresa", empresa);
                    Response.Write(retorno);
                    break;

                case "cargaGrupo":
                    retorno = cx.Listar("paINI_Grupos_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaRol":
                    retorno = cx.Listar("cargaRol",
                        "empresa", empresa);
                    Response.Write(retorno);
                    break;

                case "cargaListaArea":
                    retorno = cx.Listar("paINI_AreasEmpresa_cargar",
                        "id_dpto_emp", Request.Form["id_dpto_emp"],
                        "empresa", empresa);
                    Response.Write(retorno);
                    break;

                /*case "buscaActividadEconomica":
                    retorno = cx.Listar("buscaActividadEconomica",
                        "actividad", Request.Form["actividad"]);
                    Response.Write(retorno);
                    break;*/

                case "validaActividadEconomica":
                    retorno = cx.Listar("paINI_ActividadesEconomicas_validar",
                        "actividad", Request.Form["actividad"]);
                    Response.Write(retorno);
                    break;

                case "cargarInfoEmpresa":
                    retorno = cx.Listar("paINI_InfoEmpresa_carga",
                         "empresa", empresa);
                    Response.Write(retorno);
                    break;


                case "listarCodigosHacienda":
                    retorno = cx.Listar("paINI_CodigosHacienda_listar",
                        "codigo", Request.Form["codigo"]);
                    Response.Write(retorno);
                    break;

                case "infoGeneralPDF":
                    retorno = cx.InsertarRetorna("paINI_ReporteInfoGeneralEmpresa_cargaDetalle",
                                            "nit", empresa);
                    Response.Write(retorno);
                    break;

                case "infoAdministradorPDF":
                    retorno = cx.InsertarRetorna("paINI_ReporteInfoAdministradorEmpresa_cargaDetalle",
                                            "nit", empresa);
                    Response.Write(retorno);
                    break;

                case "infoAdministrativoPDF":
                    retorno = cx.InsertarRetorna("paINI_ReporteInfoAdministrativaEmpresa_cargaDetalle",
                                            "nit", empresa);
                    Response.Write(retorno);
                    break;

                case "infoContactoPDF":
                    retorno = cx.InsertarRetorna("paINI_ReporteInfoContactoEmpresa_cargaDetalle",
                                            "nit", empresa);
                    Response.Write(retorno);
                    break;

                case "infoEconomicaPDF":
                    retorno = cx.InsertarRetorna("paINI_ReporteInfoEconomicaEmpresa_cargaDetalle",
                                            "nit", empresa);
                    Response.Write(retorno);
                    break;

                case "infoSedePpalPDF":
                    retorno = cx.InsertarRetorna("paINI_ReporteInfoSedePpalEmpresa_cargaDetalle",
                                            "nit", empresa);
                    Response.Write(retorno);
                    break;

                case "infoFacturaPDF":
                    retorno = cx.InsertarRetorna("paINI_ReporteInfoFactura_cargaDetalle",
                                            "nit", empresa);
                    Response.Write(retorno);
                    break;

                case "cargaAccion":
                    retorno = cx.Listar("paINI_Acciones_Cargar");
                    Response.Write(retorno);
                    break;

                case "guardarespecialidadesSP":
                    retorno = cx.InsertarRetorna("paSP_especialidades_guardar",
                        "id", Request.Form["id"],
                        "nombre", Request.Form["nombre"],
                        "descripcion", Request.Form["descripcion"],
                        "limitedias", Request.Form["limitedias"],
                        "alertadias", Request.Form["alertadias"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardarRestriccionesRegionales":
                    retorno = cx.InsertarRetorna("paSP_RestriccionesRegional_guardar",
                        "id", Request.Form["id"],
                        "id_rol", Request.Form["id_rol"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "eliminarRestriccionesRegionales":
                    retorno = cx.InsertarRetorna("paSP_RestriccionesRegional_Eliminar",
                        "id", Request.Form["id"],
                        "id_rol", Request.Form["id_rol"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "borrarespecialidadesSP":
                    retorno = cx.InsertarRetorna("paSP_especialidades_borrar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardarPrestadoresSP":
                    retorno = cx.Listar("paSP_PrestadoresSP_guardar",
                         "id", Request.Form["id"],
                         "documentoPres", Request.Form["documentoPres"],
                         "digitoVer", Request.Form["digitoVer"],
                         "nivelCompl", Request.Form["nivelCompl"],
                         "nivelCont", Request.Form["nivelCont"],
                         "tipo_doc", Request.Form["tipo_doc"],
                         "contribuyente", Request.Form["contribuyente"],
                        "regimen", Request.Form["regimen"],
                        "red", Request.Form["red"],
                        "razonSocial", Request.Form["razonSocial"],
                        "nombre_comercial", Request.Form["nombre_comercial"],
                        "primer_nom", Request.Form["primer_nom"],
                        "segundo_nom", Request.Form["segundo_nom"],
                        "primer_apell", Request.Form["primer_apell"],
                        "segundo_apell", Request.Form["segundo_apell"],
                        "descripcion", Request.Form["descripcion"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardarPrestadores":
                    retorno = cx.Listar("paSP_Prestadores_guardar",
                       "id", Request.Form["id"],
                        "documentoPres", Request.Form["documentoPres"],
                        "digitoVer", Request.Form["digitoVer"],
                        "nivelCompl", Request.Form["nivelCompl"],
                         "nivelCont", Request.Form["nivelCont"],
                       "ciudad", Request.Form["ciudad"],
                       "tipo_doc", Request.Form["tipo_doc"],
                        "contribuyente", Request.Form["contribuyente"],
                       "regimen", Request.Form["regimen"],
                       "red", Request.Form["red"],
                       "razonSocial", Request.Form["razonSocial"],
                       "nombre_comercial", Request.Form["nombre_comercial"],
                       "primer_nom", Request.Form["primer_nom"],
                       "segundo_nom", Request.Form["segundo_nom"],
                       "primer_apell", Request.Form["primer_apell"],
                       "segundo_apell", Request.Form["segundo_apell"],
                       "direccion", Request.Form["direccion"],
                       "descripcion", Request.Form["descripcion"],
                       "empresa", empresa,
                       "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "borrarPrestadoresSP":
                    retorno = cx.Listar("paSP_PrestadoresSP_borrar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "borrarPrestadores":
                    retorno = cx.Listar("paSP_Prestadores_borrar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardaPacienteInfoGene":
                    retorno = cx.Listar("paSP_Paciente_guardar",
                       "id", Request.Form["id"],
                        "tipodoc", Request.Form["tipodoc"],
                        "numidenti", Request.Form["numidenti"],
                        "primernombre", Request.Form["primernombre"],
                        "segundonombre", Request.Form["segundonombre"],
                        "primerapellido", Request.Form["primerapellido"],
                        "segundoapellido", Request.Form["segundoapellido"],
                        "fechana", Request.Form["fechana"],
                        "genero", Request.Form["genero"],
                        "ocupacion", Request.Form["ocupacion"],
                        "observaciong", Request.Form["observaciong"],
                        "ciudadresi", Request.Form["ciudadresi"],
                        "direccion", Request.Form["direccion"],
                        "codpostal", Request.Form["codpostal"],
                        "email", Request.Form["email"],
                        "telefono1", Request.Form["telefono1"],
                        "telefono2", Request.Form["telefono2"],
                        "celular1", Request.Form["celular1"],
                        "celular2", Request.Form["celular2"],
                        "plansanitas", Request.Form["plansanitas"],
                        "canalrepor", Request.Form["canalrepor"],
                        "checkmedicinapre", Request.Form["checkmedicinapre"],
                        "checkconocediag", Request.Form["checkconocediag"],
                        "checktel", Request.Form["checktel"],
                        "checkcel", Request.Form["checkcel"],
                        "checksms", Request.Form["checksms"],
                        "checkemail", Request.Form["checkemail"],
                        "checkmarcacion", Request.Form["checkmarcacion"],
                        "modulo", Request.Form["modulo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "guardaPacienteRecepcion":
                    retorno = cx.InsertarRetorna("paSP_PacienteRecepcion_guardar",
                        "id", Request.Form["id"],
                        "tipoLlamada", Request.Form["tipoLlamada"],
                        "numidenti", Request.Form["numidenti"],
                        "tipodoc", Request.Form["tipodoc"],
                        "nombreCompleto", Request.Form["nombreCompleto"],
                        "telefono", Request.Form["telefono"],
                        "celular", Request.Form["celular"],
                        "tipoDocAcu", Request.Form["tipoDocAcu"],
                        "numidenti_acu", Request.Form["numidenti_acu"],
                        "nombreAcudiente", Request.Form["nombreAcudiente"],
                        "parentesco", Request.Form["parentesco"],
                        "telefonoAcu", Request.Form["telefonoAcu"],
                        "celularAcu", Request.Form["celularAcu"],
                        "area", Request.Form["area"],
                        "motivo", Request.Form["motivo"],
                        "Prioridad", Request.Form["Prioridad"],
                        "observaciones", Request.Form["observaciones"],
                        "dialogo", Request.Form["dialogo"],
                        "email", Request.Form["email"],
                        "chkInfoEmail", Request.Form["chkInfoEmail"],
                        "empresaContacto", Request.Form["empresaContacto"],
                        "chkDevLlam", Request.Form["chkDevLlam"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "guardaPacienteInfoGeneRechazados":
                    retorno = cx.Listar("paSP_PacienteRechazado_guardar",
                       "id", Request.Form["id"],
                        "tipodoc", Request.Form["tipodoc"],
                        "numidenti", Request.Form["numidenti"],
                        "primernombre", Request.Form["primernombre"],
                        "segundonombre", Request.Form["segundonombre"],
                        "primerapellido", Request.Form["primerapellido"],
                        "segundoapellido", Request.Form["segundoapellido"],
                        "fechana", Request.Form["fechana"],
                        "genero", Request.Form["genero"],
                        "ocupacion", Request.Form["ocupacion"],
                        "observaciong", Request.Form["observaciong"],
                        "ciudadresi", Request.Form["ciudadresi"],
                        "direccion", Request.Form["direccion"],
                        "codpostal", Request.Form["codpostal"],
                        "email", Request.Form["email"],
                        "telefono1", Request.Form["telefono1"],
                        "telefono2", Request.Form["telefono2"],
                        "celular1", Request.Form["celular1"],
                        "celular2", Request.Form["celular2"],
                        "plansanitas", Request.Form["plansanitas"],
                        "canalrepor", Request.Form["canalrepor"],
                        "checkmedicinapre", Request.Form["checkmedicinapre"],
                        "checkconocediag", Request.Form["checkconocediag"],
                        "checktel", Request.Form["checktel"],
                        "checkcel", Request.Form["checkcel"],
                        "checksms", Request.Form["checksms"],
                        "checkemail", Request.Form["checkemail"],
                        "checkmarcacion", Request.Form["checkmarcacion"],
                        "modulo", Request.Form["modulo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardaCargaPacientes":
                    retorno = cx.Listar("paSP_PacientesCargue_guardar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardaCargaPacientes2017":
                    retorno = cx.Listar("paSP_PacientesCargue2017_guardar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardaCargaPacientes2018":
                    retorno = cx.Listar("paSP_PacientesCargue2018_guardar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "borrarCargaPacientesTMP":
                    retorno = cx.InsertarRetorna("paSP_Carga_Alterna_borrar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaTipoDoc":
                    retorno = cx.Listar("paINI_TipoDocumento_cargar"
                       );
                    Response.Write(retorno);
                    break;

                case "cargaPlanSanitas":
                    retorno = cx.Listar("paINI_Plan_cargar"
                       );
                    Response.Write(retorno);
                    break;

                case "cargaGenero":
                    retorno = cx.Listar("paINI_Genero_cargar"
                       );
                    Response.Write(retorno);
                    break;

                case "cargaPacienteParen":
                    retorno = cx.Listar("paINI_Parentesco_cargar"
                       );
                    Response.Write(retorno);
                    break;

                case "cargaPrioridadRecepcion":
                    retorno = cx.Listar("paSP_PrioridadRecepcion_cargar"
                       );
                    Response.Write(retorno);
                    break;

                case "cargaPrioridadRecepcionReporte":
                    retorno = cx.Listar("paSP_PrioridadRecepcionReporte_cargar"
                       );
                    Response.Write(retorno);
                    break;

                case "guardarParentesco":
                    retorno = cx.InsertarRetorna("paSP_Parentesco_guardar",
                        "id", Request.Form["id"],
                        "txtDocumentoAcudientePacI", Request.Form["txtDocumentoAcudientePacI"],
                        "txtNombreAcudientePacI", Request.Form["txtNombreAcudientePacI"],
                        "selParentescoPacI", Request.Form["selParentescoPacI"],
                        //"selDepartaAcudientePacI", Request.Form["selDepartaAcudientePacI"],
                        "selCiudadAcudientePacI", Request.Form["selCiudadAcudientePacI"],
                        "txtTelAcudientePacI", Request.Form["txtTelAcudientePacI"],
                        "txtCelAcudientePacI", Request.Form["txtCelAcudientePacI"],
                        //"txtTelAcudientePacI2", Request.Form["txtTelAcudientePacI2"],
                        //"txtCelAcudientePacI2", Request.Form["txtCelAcudientePacI2"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaTipoDiagnostico":
                    retorno = cx.Listar("paINI_TipoDiagnostico_cargar"
                       );
                    Response.Write(retorno);
                    break;

                case "cargaConoceDiagnostico":
                    retorno = cx.Listar("paSP_ConoceDiagnostico_cargar"
                       );
                    Response.Write(retorno);
                    break;

                case "listarParentesco":
                    retorno = cx.Listar("paSP_Parentesco_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardarCie10":
                    retorno = cx.InsertarRetorna("paSP_Cie10_guardar",

                        "id", Request.Form["id"],

                        "txtcodigocie10PacI", Request.Form["txtcodigocie10PacI"],
                        "selTipoDiag", Request.Form["selTipoDiag"],
                        //  "selCasoEsp", Request.Form["selCasoEsp"],
                        "selCiudadReportePacI", Request.Form["selCiudadReportePacI"],

                        "empresa", empresa,
                        "responsable", responsable
                        );
                    Response.Write(retorno);
                    break;


                case "guardarAnalisisDiagnostico":
                    retorno = cx.InsertarRetorna("paSP_AnalisisDiagnostico_guardar",
                        "id", Request.Form["id"],
                        "conoce_diagnostico", Request.Form["conoce_diagnostico"],
                        "empresa", empresa,
                        "responsable", responsable
                        );
                    Response.Write(retorno);
                    break;



                case "cargaCasoEspecial":
                    retorno = cx.Listar("paINI_CasoEspecial_cargar"
                       );
                    Response.Write(retorno);
                    break;

                case "cargaCodigoPostal":
                    retorno = cx.Listar("paINI_CodigoPostal_cargar"
                       );
                    Response.Write(retorno);
                    break;

                case "listarcie10":
                    retorno = cx.Listar("paSP_Cie10Paciente_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarcie10_Codigo":
                    retorno = cx.Listar("paSP_Cie10CodPaciente_listar",
                        "id", Request.Form["id"],
                        "codigo", Request.Form["codigo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "validadEdad":
                    retorno = cx.Listar("paSP_Edad_Validar",
                        "fecha", Request.Form["fecha"],
                        "empresa", empresa);
                    Response.Write(retorno);
                    break;

                case "guardarObservacion":
                    retorno = cx.InsertarRetorna("paSP_ObservacionPaciente_guardar",

                        "id", Request.Form["id"],

                        "idPaciente", Request.Form["idPaciente"],
                        "estado", Request.Form["estado"],
                        "txtobservacionTabsSPBP", Request.Form["txtobservacionTabsSPBP"],
                        "motivo", Request.Form["motivo"],
                        "empresa", empresa,
                        "responsable", responsable
                        );
                    Response.Write(retorno);
                    break;

                case "listarObservacion":
                    retorno = cx.Listar("paSP_Observacion_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarCheck":
                    retorno = cx.Listar("paSP_listaCheckDiagnostico_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarTramitesDetalle":
                    retorno = cx.Listar("paSP_TramitesDetalle_Listar",
                        "documento", Request.Form["documento"],
                        "idarchivo", Request.Form["idarchivo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaMotivo":
                    retorno = cx.Listar("paSP_ObservacionMotivo__Cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaMotivoPaciente":
                    retorno = cx.Listar("paSP_ObservacionMotivoPaciente__Cargar",
                        "menu", Request.Form["menu"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaTipoRed":
                    retorno = cx.Listar("paSP_PrestadorRed_Cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaOpcionPrestador":
                    retorno = cx.Listar("paSP_PrestadorOpcion_Cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "borrarCIE10":
                    retorno = cx.InsertarRetorna("paSP_Cie10_borrar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable
                        );
                    Response.Write(retorno);
                    break;

                case "borrarParentesco":
                    retorno = cx.InsertarRetorna("paSP_Parentesco_borrar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable
                        );
                    Response.Write(retorno);
                    break;
                case "borrarObservacion":
                    retorno = cx.InsertarRetorna("paSP_Observacion_borrar",

                        "id", Request.Form["id"],//Cedula del Paciente
                        //"Observacion", Request.Form["Observacion"],//Cedula del Paciente

                        "empresa", empresa,
                        "responsable", responsable
                        );
                    Response.Write(retorno);
                    break;


                case "borrarObservacionBUS":
                    retorno = cx.InsertarRetorna("paSP_ObservacionBUS_borrar",

                        "id", Request.Form["id"],//Cedula del Paciente
                        //"Observacion", Request.Form["Observacion"],//Cedula del Paciente
                        "motivo", Request.Form["motivo"],
                        "empresa", empresa,
                        "responsable", responsable
                        );
                    Response.Write(retorno);
                    break;

                case "editarPacientePacLL":
                    retorno = cx.Listar("paSP_Paciente_editar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "rol_detalle":
                    retorno = cx.Listar("paSP_rol_detalle",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarMenusCategorias":
                    retorno = cx.Listar("paSP_MenusCategoria_listar",
                        "categoria", Request.Form["categoria"],
                         "rol", Request.Form["rol"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarUsuariosRol":
                    retorno = cx.Listar("paSP_UsuariosRol_listar",
                        "rol", Request.Form["rol"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaEstadoPaciente":
                    retorno = cx.Listar("paSP_Estado_Cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "CategoriaRol":
                    retorno = cx.Listar("paSP_CategoriaRol_Listar",
                         "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaEstadoDialogo":
                    retorno = cx.Listar("paSP_Estado_Dialogo_Cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaEstadoObservacion":
                    retorno = cx.Listar("paINI_Estado_Observacion_Cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaEstadoPacientecargue":
                    retorno = cx.Listar("paSP_EstadoPacienteCargue_Cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaNivel":
                    retorno = cx.Listar("paSP_PrestadorNivel_Cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardadiasNoHabiles":
                    retorno = cx.InsertarRetorna("paSP_DiasNoHabiles_guardar",
                        "id", Request.Form["id"],
                        "nombre", Request.Form["nombre"],
                        "descripcion", Request.Form["descripcion"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "eliminarDianohabil":
                    retorno = cx.InsertarRetorna("paSP_DiasNoHabiles_eliminar",
                        "id", Request.Form["id"]);
                    Response.Write(retorno);
                    break;

                case "listarInfoPaci":
                    retorno = cx.Listar("paSP_PacienteInfo_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargueDocumentacionConsolidado":
                    retorno = cx.Listar("paSP_HistoriaClinicaDocumento_listar",
                        "documento", Request.Form["documento"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargueDocumentacionDetallado":
                    retorno = cx.Listar("paSP_HistoriaClinicaDocumentoGene_listar",
                        "documento", Request.Form["documento"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarCitaMedica":
                    retorno = cx.InsertarRetorna("paSP_CitaMedica_guardar",
                        "id", Request.Form["id"],
                         "codigo", Request.Form["codigo"],
                         "especialidad", Request.Form["especialidad"],
                        "nombre", Request.Form["nombre"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "eliminarCitaMedica":
                    retorno = cx.InsertarRetorna("paSP_CitaMedica_eliminar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "listarmensaje":
                    retorno = cx.Listar("paSP_Dialogomensaje1_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "cargaTipoCargue":
                    retorno = cx.Listar("paSP_TipoCargue_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarmensajeEditar":
                    retorno = cx.Listar("paSP_Dialogomensaje_Editar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarDialogo":
                    retorno = cx.InsertarRetorna("paSP_Dialogo_guardar",
                        "id", Request.Form["id"],
                        "dialogo", Request.Form["dialogo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarDialogoNuevo":
                    retorno = cx.InsertarRetorna("paSP_DialogoNuevo_guardar",
                        "id", Request.Form["id"],
                        "dialogo", Request.Form["dialogo"],
                        "descripcion", Request.Form["descripcion"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarObservacionNuevo":
                    retorno = cx.InsertarRetorna("paINI_ObservacionNuevo_guardar",
                        "id", Request.Form["id"],
                        "Observacion", Request.Form["Observacion"],
                        "descripcion", Request.Form["descripcion"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "llamarPaciente":
                    retorno = cx.Listar("paSP_Paciente_llamar",
                        "id", Request.Form["id"],
                        "tipollamada", Request.Form["tipollamada"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "enviarCorreoPaciente":
                    retorno = cx.Listar("paSP_EnviarRechazadosTraDicPaciente_Email",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarTramitesInformarPac":
                    retorno = cx.Listar("paSP_listarTramitesInformarPac_Listar",
                        "id", Request.Form["id"],
                        "estado", Request.Form["estado"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "DetallePaciente":
                    retorno = cx.Listar("paSP_Paciente_detalle2",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "llamarPrestador":
                    retorno = cx.Listar("paSP_Prestador_llamar",
                        "id", Request.Form["id"],
                        "tipollamada", Request.Form["tipollamada"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "llamarPacienteDevolucion":
                    retorno = cx.Listar("paSP_PacienteRecepcion_llamar",
                        "id", Request.Form["id"],
                        "tipollamada", Request.Form["tipollamada"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaRespuestaPaciente":
                    retorno = cx.Listar("paSP_RespuestaPaciente_cargar");
                    Response.Write(retorno);
                    break;

                case "cargarRespuestas":
                    retorno = cx.Listar("paSP_Respuestas_cargar",
                     "categoria", Request.Form["categoria"],
                     "empresa", empresa,
                     "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargarRespuestaRegistrarRespuesa":
                    retorno = cx.Listar("paSP_RespuestaRegistrarRespuesa_cargar",
                     "empresa", empresa,
                     "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "CargaMotivos":
                    retorno = cx.Listar("paSP_Respuesta_cargar",
                     "categoria", Request.Form["categoria"],
                     "empresa", empresa,
                     "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "CargarMotivoNovigentePres":
                    retorno = cx.Listar("paSP_MotivoNovigentePres_cargar",
                     "menu", Request.Form["menu"],
                     "empresa", empresa,
                     "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaRespuestaPaciente_generico":
                    retorno = cx.Listar("paSP_RespuestaPaciente_generico_cargar",
                        "caso", Request.Form["caso"]);
                    Response.Write(retorno);
                    break;

                case "guardaRespuestaPacLL":
                    retorno = cx.InsertarRetorna("paSP_PacienteDudosoRespuesta_Guardar",
                        "id", Request.Form["id"],
                        "decision", Request.Form["decision"],
                        "observacion", Request.Form["observacion"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardaPacienteEstado":
                    retorno = cx.InsertarRetorna("paSP_PacienteEstado_guardar",
                        "id", Request.Form["id"],
                        "estado", Request.Form["estado"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "nocontactable123_contar":
                    retorno = cx.Listar("paSP_nocontactable123_contar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cancelarLlamada":
                    retorno = cx.InsertarRetorna("paSP_llamada_cancelar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "TramitesInformarCorreo":
                    retorno = cx.InsertarRetorna("paSP_TramitesInformar_Guardar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "TramitesInformarCorreoEliminar":
                    retorno = cx.InsertarRetorna("paSP_TramitesInformar_Eliminar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "TramitesRechazadosInformarCorreo":
                    retorno = cx.InsertarRetorna("paSP_TramitesRechazadosInformar_Guardar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "TramitesRecahzadosInformarCorreoEliminar":
                    retorno = cx.InsertarRetorna("paSP_TramitesRechazadosInformar_Eliminar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "cargaPacienteEstado":
                    retorno = cx.Listar("paSP_PacienteEstados_cargar"
                       );
                    Response.Write(retorno);
                    break;

                case "cargaEstadoPacEditar":
                    retorno = cx.Listar("paSP_PacienteEstadosEditar_cargar"
                       );
                    Response.Write(retorno);
                    break;

                case "cargaEstadoSinCoberturaEditar":
                    retorno = cx.Listar("paSP_EstadoSinCobertura_cargar"
                       );
                    Response.Write(retorno);
                    break;

                case "contarRandompacientesLlamadaNUEVO":
                    retorno = cx.Listar("paSP_PacienteNuevo_random",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "contarRandompacientesLlamadaNUEVOINI_FIN":
                    retorno = cx.Listar("paSP_PacienteNuevoINI_FIN_random",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarRandompacientesActualizados":
                    retorno = cx.Listar("paSP_PacienteActualizado_random",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaDialogosLlamada":
                    retorno = cx.Listar("paSP_DialogosLlamada_cargar",
                        "opcion", Request.Form["opcion"],
                        "docPaciente", Request.Form["docPaciente"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaDialogosRecepcionLlamada":
                    retorno = cx.Listar("paSP_DialogosRecepcionLlamada_cargar",
                        "opcion", Request.Form["opcion"],
                        "docPaciente", Request.Form["docPaciente"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "conteoPacientes":
                    retorno = cx.Listar("paSP_PrioridadPacientes_contar",
                        "fechaInicio", Request.Form["fechaInicio"],
                        "fechaFin", Request.Form["fechaFin"],
                        "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaMotivoPacienteCUC":
                    retorno = cx.Listar("paCUC_ObservacionMotivo_Cargar",
                        "menu", Request.Form["menu"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "detalleTramitesInfoPacienteProgramacionOptimizado":
                    retorno = cx.Listar("paSP_PacienteProgramar_detalle",
                        "idtramite", Request.Form["idtramite"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "GuardarLlamada":
                    retorno = cx.InsertarRetorna("paSP_PacienteLlamar_guardar",
                         "id", Request.Form["id"],
                        "respuesta", Request.Form["respuesta"],
                        "observacion", Request.Form["observacion"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarDudosos":
                    retorno = cx.Listar("paSP_pacientesDudosos_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;
                case "contarRandompacientesLlamadaDUDOSOS":
                    retorno = cx.Listar("paSP_PacienteDudoso_random",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "contarRandompacientesLlamadaDUDOSOS_INI_FIN":
                    retorno = cx.Listar("paSP_PacienteDudosoINI_FIN_random",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;
                case "contarRandompacientesLlamadaNC":
                    retorno = cx.Listar("paSP_PacienteNC_random",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarPacientes":
                    retorno = cx.Listar("paSP_PacientesPendientes_contar",
                        "estado", Request.Form["estado"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarPacienteDudosoEditar":
                    retorno = cx.Listar("paSP_PacienteDudoso_Editar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardarCantidadTramites":
                    retorno = cx.InsertarRetorna("paSP_PacienteCantidadTramites_guardar",
                        "documento", Request.Form["documento"],
                        "cantidad", Request.Form["cantidad"],
                        "tramitesi", Request.Form["tramitesi"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaCanalReporte":
                    retorno = cx.Listar("paSP_CanalReporte_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaLlamdaEstado":
                    retorno = cx.Listar("paSP_LlamadaEstados_cargar"
                       );
                    Response.Write(retorno);
                    break;

                case "cargaTipoReporte":
                    retorno = cx.Listar("paSP_TipoReporte_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteUsuarioVsEstado":
                    retorno = cx.Listar("paSP_UsuarioVsEstado_Reporte",
                        "txtFechaInicio", Request.Form["txtFechaInicio"],
                        "txtFechaFin", Request.Form["txtFechaFin"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargarEspecialidades":
                    retorno = cx.Listar("paSP_Especialidades_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarTramitesRealizados":
                    retorno = cx.Listar("paSP_TramitesRealizados_Contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "contarRandomSPVyA":
                    retorno = cx.Listar("paSP_Paciente_random",
                        "estado", Request.Form["estado"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarRandomSPVyANoCUC":
                    retorno = cx.Listar("paSP_PacienteNoCUC_random",
                        "estado", Request.Form["estado"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarValidarAprobarPendientes":
                    retorno = cx.Listar("paSP_Tramites_contar",
                        "estado", Request.Form["estado"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarValidarAprobarRealizados":
                    retorno = cx.Listar("paSP_ValidarAprobarRealizados_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarSPVyA":
                    retorno = cx.Listar("paSP_PacienteInfoTramites_listar",
                        "id", Request.Form["id"],
                        "estado", Request.Form["estado"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "PacientesConTramites":
                    retorno = cx.Listar("paSP_PacienteConTramites_listar",
                        "documentoFil", Request.Form["documentoFil"],
                        "nombreFil", Request.Form["nombreFil"],
                        "estado", Request.Form["estado"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarHijosSPVyA":
                    retorno = cx.Listar("paSP_TramitesHijos_listar",
                        "id", Request.Form["id"],
                        "estado", Request.Form["estado"],
                        "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarHijosAutorizacionCUC":
                    retorno = cx.Listar("paSP_HijosAutorizacionCUC_listar",
                        "id", Request.Form["id"],
                        "estado", Request.Form["estado"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarHijosSPVyACUC":
                    retorno = cx.Listar("paSP_TramitesHijosCUC_listar",
                        "id", Request.Form["id"],
                        "estado", Request.Form["estado"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarHijosSPVyANOCUC":
                    retorno = cx.Listar("paSP_TramitesHijosNOCUC_listar",
                        "id", Request.Form["id"],
                        "estado", Request.Form["estado"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarHijosSPVyARepro":
                    retorno = cx.Listar("paSP_TramitesHijosRepro_listar",
                        "id", Request.Form["id"],
                        "estado", Request.Form["estado"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarAutoTramitesHijos":
                    retorno = cx.Listar("paSP_AutorizacionesTramHijos_listar",
                        "id", Request.Form["id"],
                        "estado", Request.Form["estado"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "registrarFULLSPVyA":
                    retorno = cx.InsertarRetorna("paSP_ValidarAprovar_registrar",
                        "id", Request.Form["id"],
                        "requiere", Request.Form["requiere"],
                        "aprobacion", Request.Form["aprobacion"],
                        "numautorizacion", Request.Form["numautorizacion"],
                        "prestador", Request.Form["prestador"],
                        "tipo", Request.Form["tipo"],
                        "modulo", Request.Form["modulo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardarFinalAutorizacion":
                    retorno = cx.InsertarRetorna("paSP_autorizacionRegistro_guardar",
                        "id", Request.Form["id"],
                        "anulacion", Request.Form["anulacion"],
                        "motivo_Anulacion", Request.Form["motivo_Anulacion"],
                         "modulo", Request.Form["modulo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "RegistrarCambioAutoSP":
                    retorno = cx.InsertarRetorna("paSP_RegistrarCambioAutoSP_registrar",
                        "id", Request.Form["id"],
                        "requiere", Request.Form["requiere"],
                        "aprobacion", Request.Form["aprobacion"],
                        "numautorizacion", Request.Form["numautorizacion"],
                        "prestador", Request.Form["prestador"],
                        "tipo", Request.Form["tipo"],
                        "modulo", Request.Form["modulo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "AutorizacionCUC":
                    retorno = cx.InsertarRetorna("paCUC_AutorizacionCUC_registrar",
                        "id", Request.Form["id"],
                        "requiere", Request.Form["requiere"],
                        "aprobacion", Request.Form["aprobacion"],
                        "numautorizacion", Request.Form["numautorizacion"],
                        "prestador", Request.Form["prestador"],
                        "tipo", Request.Form["tipo"],
                        "modulo", Request.Form["modulo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarcie10CUC":
                    retorno = cx.Listar("paCUC_Cie10Paciente_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ValidarAprobar_guardar":
                    retorno = cx.InsertarRetorna("paSP_ValidarAprobar_guardar",
                        "id", Request.Form["id"],
                        "requiere", Request.Form["requiere"],
                        "aprobacion", Request.Form["aprobacion"],
                        "numautorizacion", Request.Form["numautorizacion"],
                        "prestador", Request.Form["prestador"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listallamyvaliSPVyA":
                    retorno = cx.Listar("paSP_PacienteTramites_listar",
                        "documento", Request.Form["documento"],
                        "nombre", Request.Form["nombre"],
                        "estado", Request.Form["estado"],
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listaObservacionSPVyA":
                    retorno = cx.Listar("paSP_TramitesObservaciones_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardaObservacionMasterSPVyA":
                    retorno = cx.InsertarRetorna("paSP_TramitesObservaciones_guardar",
                        "id", Request.Form["id"],
                        "observacion", Request.Form["observacion"],
                        "estado", Request.Form["estado"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "eliminarobservacionMasterSPVyA":
                    retorno = cx.InsertarRetorna("paSP_TramitesObservaciones_borrar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "vertodosVyA":
                    retorno = cx.Listar("paSPvertodosVyA",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "documento", Request.Form["documento"],
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "contarTramitesPendientesSPT":
                    retorno = cx.Listar("paSP_TramitesPendientes_Contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;
                case "contarTramitesPendientes2SPT":
                    retorno = cx.Listar("paSP_TramitesPendientes2LL_Contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarRandompacientesTramite":
                    retorno = cx.Listar("paSP_TramiteRandompacientes_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarRandom2pacientesTramite":
                    retorno = cx.Listar("paSP_TramitesPendientes2LL_Random",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardaRequerimientoTemp":
                    retorno = cx.InsertarRetorna("paSP_TramitesTemp_Guarda",
                        "id", Request.Form["id"],
                        "fechar", Request.Form["fechar"],
                        "fechal", Request.Form["fechal"],
                        "requerimiento", Request.Form["requerimiento"],
                        "especialidad", Request.Form["especialidad"],
                          "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarRequerimientosTEMP":
                    retorno = cx.Listar("paSP_TramitesTemp_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "borrarTramitesTemp":
                    retorno = cx.InsertarRetorna("paSP_TramitesTemp_borrar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardaTramitePaquete":
                    retorno = cx.InsertarRetorna("paSP_TramitePaquete_Guardar",
                        "id", Request.Form["id"],
                        "idLlamada", Request.Form["idLlamada"],
                          "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarTramiteCierre":
                    retorno = cx.InsertarRetorna("paSP_TramiteCierre_Guardar",
                        "id", Request.Form["id"],
                         "idLlamada", Request.Form["idLlamada"],
                        "decision", Request.Form["decision"],
                        "observacion", Request.Form["observacion"],
                          "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarReprogamarcionLlam":
                    retorno = cx.InsertarRetorna("paSP_TramiteReprogramarLlam_Guarda",
                        "id", Request.Form["id"],
                        "idLlamada", Request.Form["idLlamada"],
                        "fecha", Request.Form["fecha"],
                        "observacion", Request.Form["observacion"],
                          "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardaObservacionTramiteTemp":
                    retorno = cx.InsertarRetorna("paSP_TramiteTempObservacion_Guarda",
                        "id", Request.Form["id"],
                        "observacion", Request.Form["observacion"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listaObservacion":
                    retorno = cx.Listar("paSP_TramiteTempObservacion_lista",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "CargaMotivoCierre":
                    retorno = cx.Listar("paSP_MotivoCierre_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "EliminarObserTramite":
                    retorno = cx.InsertarRetorna("paSP_TramiteObser_borrar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarLlamadasReprogramadas":
                    retorno = cx.Listar("paSP_TramiteLlamProgamadas_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "contarInformarPendientes":
                    retorno = cx.Listar("paSP_Informar_Tramites_Pendientes_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarInformarRealizados":
                    retorno = cx.Listar("paSP_Informar_Tramites_Realizados_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarRandomSPINFO":
                    retorno = cx.Listar("paSP_Informar_Tramites_Pendientes_random",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarRandomSPINFO_NOCUC":
                    retorno = cx.Listar("paSP_Informar_Tramites_Pendientes_NOCUC_random",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarRandomSPINFO_CUC":
                    retorno = cx.Listar("paSP_Informar_Tramites_Pendientes_CUC_random",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "Informar_Tramites_Pendientes_listar":
                    retorno = cx.Listar("paSP_Informar_Tramites_Pendientes_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardaObservacionMasterSPINFO":
                    retorno = cx.InsertarRetorna("paSP_guardaObservacionMasterSPINFO",
                        "id", Request.Form["id"],
                        "observacion", Request.Form["observacion"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listaObservacionSPINFO":
                    retorno = cx.Listar("paSP_listaObservacionSPINFO",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "eliminarobservacionMasterSPINFO":
                    retorno = cx.InsertarRetorna("paSP_eliminarobservacionMasterSPINFO",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ActualizarFechaHora":
                    retorno = cx.InsertarRetorna("paSP_ActualizarFechaHoraSPINFO_actualizar",
                        "id", Request.Form["id"],
                        "fecha", Request.Form["fecha"],
                        "hora", Request.Form["hora"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardarInfoPacienteSPINFO":
                    retorno = cx.InsertarRetorna("paSP_Informar_Tramites_Pendientes_guardar",
                        "id", Request.Form["id"],
                        "respuesta", Request.Form["respuesta"],
                        "idLlamada", Request.Form["idLlamada"],
                        "contesta_1 ", Request.Form["contesta_1 "],
                        "contesta_2 ", Request.Form["contesta_2 "],
                        "contesta_3 ", Request.Form["contesta_3 "],
                        "contesta_4 ", Request.Form["contesta_4 "],
                        "motivo_1 ", Request.Form["motivo_1 "],
                        "motivo_2 ", Request.Form["motivo_2 "],
                        "motivo_3 ", Request.Form["motivo_3 "],
                        "motivo_4 ", Request.Form["motivo_4 "],
                        "registro_1 ", Request.Form["registro_1 "],
                        "registro_2 ", Request.Form["registro_2 "],
                        "registro_3 ", Request.Form["registro_3 "],
                        "registro_4 ", Request.Form["registro_4 "],
                        "vigente_1 ", Request.Form["vigente_1 "],
                        "vigente_2 ", Request.Form["vigente_2 "],
                        "vigente_3 ", Request.Form["vigente_3 "],
                        "vigente_4 ", Request.Form["vigente_4 "],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "contarTramitesRequiereDocumentacion":
                    retorno = cx.Listar("paSP_TramitesRequiereDoc_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "RandomTramitesPendienteDocumentacion":
                    retorno = cx.Listar("paSP_TramitesPendienteDoc_random",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarTramitesPendienteDocumentacion":
                    retorno = cx.Listar("paSP_TramitesPendienteDoc_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarTramitesPendienteDocumentacion":
                    retorno = cx.Listar("paSP_TramitesPendientesDoc_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarTramitesPendienteDocumentacion2":
                    retorno = cx.Listar("paSP_TramitesPendientesDoc2_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "registrarTramitePendienteDocumentacion":
                    retorno = cx.InsertarRetorna("paSP_TramitesPendienteDoc_guardar",
                        "id", Request.Form["id"],
                        "respuesta", Request.Form["respuesta"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "RandomTramitesRequiereDocumentacion":
                    retorno = cx.Listar("paSP_TramitesRequiereDoc_random",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarTramitesRequiereDocumentacion":
                    retorno = cx.Listar("paSP_TramitesRequiereDoc_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarTramitesRequiereDocumentacion2":
                    retorno = cx.Listar("paSP_TramitesRequiereDoc2_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardaDocumentoTramite":
                    retorno = cx.InsertarRetorna("paSP_TramitesDocumentos_asociar",
                        "id", Request.Form["id"],
                        "hijos", Request.Form["hijos"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "buscarPacientesTramites":
                    retorno = cx.Listar("paSP_PacienteTramites_listar",
                        "documento", Request.Form["documento"],
                        "nombre", Request.Form["nombre"],
                        "estado", Request.Form["estado"],
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listaObservacionTramite":
                    retorno = cx.Listar("paSP_TramitesObservaciones_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardarObsTramite":
                    retorno = cx.InsertarRetorna("paSP_TramitesObservaciones_guardar",
                        "id", Request.Form["id"],
                        "observacion", Request.Form["observacion"],
                        "estado", Request.Form["estado"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardarObsArchivo":
                    retorno = cx.InsertarRetorna("paSP_ArchivoObservaciones_guardar",
                        "id", Request.Form["id"],
                        "observacion", Request.Form["observacion"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "EliminarObserArchivo":
                    retorno = cx.InsertarRetorna("paSP_ArchivoObservacion_borrar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "eliminarObsTramites":
                    retorno = cx.InsertarRetorna("paSP_TramitesObservaciones_borrar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarProgramarSolicitud":
                    retorno = cx.Listar("paSP_ProgramarSolicitud_Contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarProgramarSoliRandom":
                    retorno = cx.Listar("paSP_ProgramarRandom_Contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarProgramarSolicRealizados":
                    retorno = cx.Listar("paSP_ProgramarSolicRealizados_Contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarRandomSPSEGUI":
                    retorno = cx.Listar("paSP_Paciente_random",
                        "estado", Request.Form["estado"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarRandomSPSEGUI_NOCUC":
                    retorno = cx.Listar("paSP_Paciente_NOCUC_random",
                        "estado", Request.Form["estado"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "TramiteProgramacion":
                    retorno = cx.Listar("paSP_ProgramarSolicTramites_listar",
                         "idtramite", Request.Form["idtramite"],
                          "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarSeguimientoPendientes":
                    retorno = cx.Listar("paSP_Tramites_contar",
                        "estado", Request.Form["estado"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarPrograrSolicitudTram":
                    retorno = cx.InsertarRetorna("paSP_ProgramarSolicitudTramite_Guardar",
                        "id", Request.Form["id"],
                        "numautorizacion", Request.Form["numautorizacion"],
                        "prestador", Request.Form["prestador"],
                        "fechaprogramacion", Request.Form["fechaprogramacion"],
                        "hora", Request.Form["hora"],
                        "medico", Request.Form["medico"],
                        "observaciones", Request.Form["observaciones"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarSeguimientoRealizados":
                    retorno = cx.Listar("paSP_SeguimientoSolicitudesRealizadas_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "validarPrestadores":
                    retorno = cx.InsertarRetorna("paSP_validar_Prestadores",
                        "prestador", Request.Form["prestador"],
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarSPSEGUI":
                    retorno = cx.Listar("paSP_PacienteInfoTramites_listar",
                        "id", Request.Form["id"],
                        "estado", Request.Form["estado"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarHijosSPSEGUI":
                    retorno = cx.Listar("paSP_TramitesHijos_listar",
                        "id", Request.Form["id"],
                        "estado", Request.Form["estado"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listallamyvaliSPSEGUI":
                    retorno = cx.Listar("paSP_PacienteTramites_listar",
                        "documento", Request.Form["documento"],
                        "nombre", Request.Form["nombre"],
                        "estado", Request.Form["estado"],
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listalbuscaSPSEGUI":
                    retorno = cx.Listar("paSP_PacienteSeguiPeriodico_listar",
                        "documento", Request.Form["documento"],
                        "nombre", Request.Form["nombre"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "SeguimientoRespuesta":
                    retorno = cx.InsertarRetorna("paSP_SegumientoSolicitudRes_guardar",
                        "id", Request.Form["id"],
                        "bandera", Request.Form["bandera"],
                        "motivo", Request.Form["motivo"],
                        "reqauto", Request.Form["reqauto"],
                        "observacion", Request.Form["observacion"],
                        "contesta_1", Request.Form["contesta_1"],
                        "contesta_2", Request.Form["contesta_2"],
                        "contesta_3", Request.Form["contesta_3"],
                        "contesta_4", Request.Form["contesta_4"],
                        "motivo_1", Request.Form["motivo_1"],
                        "motivo_2", Request.Form["motivo_2"],
                        "motivo_3", Request.Form["motivo_3"],
                        "motivo_4", Request.Form["motivo_4"],
                        "registro_1", Request.Form["registro_1"],
                        "registro_2", Request.Form["registro_2"],
                        "registro_3", Request.Form["registro_3"],
                        "registro_4", Request.Form["registro_4"],
                        "vigente_1", Request.Form["vigente_1"],
                        "vigente_2", Request.Form["vigente_2"],
                        "vigente_3", Request.Form["vigente_3"],
                        "vigente_4", Request.Form["vigente_4"],
                        "idllamada", Request.Form["idllamada"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardarllamadaSeguimiento":
                    retorno = cx.InsertarRetorna("paSP_SegumientoLlamada_guardar",
                        "contesta_1", Request.Form["contesta_1"],
                        "contesta_2", Request.Form["contesta_2"],
                        "contesta_3", Request.Form["contesta_3"],
                        "contesta_4", Request.Form["contesta_4"],
                        "motivo_1", Request.Form["motivo_1"],
                        "motivo_2", Request.Form["motivo_2"],
                        "motivo_3", Request.Form["motivo_3"],
                        "motivo_4", Request.Form["motivo_4"],
                        "registro_1", Request.Form["registro_1"],
                        "registro_2", Request.Form["registro_2"],
                        "registro_3", Request.Form["registro_3"],
                        "registro_4", Request.Form["registro_4"],
                        "vigente_1", Request.Form["vigente_1"],
                        "vigente_2", Request.Form["vigente_2"],
                        "vigente_3", Request.Form["vigente_3"],
                        "vigente_4", Request.Form["vigente_4"],
                        "idllamada", Request.Form["idllamada"],
                        "respuesta", Request.Form["respuesta"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "SolicitudesPendienteslistar":
                    retorno = cx.Listar("paSP_ProgramarSolicitudPendientes_listar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "documento", Request.Form["documento"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listallamyvaliSPINFO":
                    retorno = cx.Listar("paSP_InformarPaciente_Buscar",
                        "codigo", Request.Form["codigo"],
                        "nombre", Request.Form["nombre"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;



                case "contarRecordarPendientes":
                    retorno = cx.Listar("paSP_Recordar_Tramites_Pendientes_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarrecordarRealizados":
                    retorno = cx.Listar("paSP_Recordar_Tramites_Realizados_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarRandomSPRECOR":
                    retorno = cx.Listar("paSP_Recordar_Tramites_Pendientes_random",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarRandomSPRECOR_NOCUC":
                    retorno = cx.Listar("paSP_Recordar_Tramites_Pendientes_NOCUC_random",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "Recordar_Tramites_Pendientes_listar":
                    retorno = cx.Listar("paSP_Recordar_Tramites_Pendientes_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardaObservacionMasterSPRECOR":
                    retorno = cx.InsertarRetorna("paSP_guardaObservacionMasterSPRECOR",
                        "id", Request.Form["id"],
                        "observacion", Request.Form["observacion"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "abrirUltimoArchivo":
                    retorno = cx.Listar("paSP_abrirUltimoArchivo_listar",
                        "idTramiteGlobal", Request.Form["idTramiteGlobal"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listaObservacionSPRECOR":
                    retorno = cx.Listar("paSP_listaObservacionSPRECOR",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "eliminarobservacionMasterSPRECOR":
                    retorno = cx.InsertarRetorna("paSP_eliminarobservacionMasterSPRECOR",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardarRECORPacienteSPRECOR":
                    retorno = cx.InsertarRetorna("paSP_Recordar_Tramites_Pendientes_guardar",
                        "id", Request.Form["id"],
                        "respuesta", Request.Form["respuesta"],
                        "idLlamada", Request.Form["idLlamada"],
                        "contesta_1 ", Request.Form["contesta_1 "],
                        "contesta_2 ", Request.Form["contesta_2 "],
                        "contesta_3 ", Request.Form["contesta_3 "],
                        "contesta_4 ", Request.Form["contesta_4 "],
                        "motivo_1 ", Request.Form["motivo_1 "],
                        "motivo_2 ", Request.Form["motivo_2 "],
                        "motivo_3 ", Request.Form["motivo_3 "],
                        "motivo_4 ", Request.Form["motivo_4 "],
                        "registro_1 ", Request.Form["registro_1 "],
                        "registro_2 ", Request.Form["registro_2 "],
                        "registro_3 ", Request.Form["registro_3 "],
                        "registro_4 ", Request.Form["registro_4 "],
                        "vigente_1 ", Request.Form["vigente_1 "],
                        "vigente_2 ", Request.Form["vigente_2 "],
                        "vigente_3 ", Request.Form["vigente_3 "],
                        "vigente_4 ", Request.Form["vigente_4 "],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "listallamyvaliSPRECOR":
                    retorno = cx.Listar("paSP_RecordarPaciente_Buscar",
                        "codigo", Request.Form["codigo"],
                        "nombre", Request.Form["nombre"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarRecorPaci":
                    retorno = cx.Listar("paSP_PacienteInfo_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarRandomSeguimientoPer":
                    //Thread.Sleep(3333);
                    retorno = cx.Listar("paSP_SeguimientoPeriodicoRandom_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarObservacionSeguimientoPer":
                    retorno = cx.InsertarRetorna("paSP_SeguimientoPerObs_Guardar",
                        "id", Request.Form["id"],
                        "contacto", Request.Form["contacto"],
                        "observacion", Request.Form["observacion"],
                        "contesta_1 ", Request.Form["contesta_1 "],
                        "contesta_2 ", Request.Form["contesta_2 "],
                        "contesta_3 ", Request.Form["contesta_3 "],
                        "contesta_4 ", Request.Form["contesta_4 "],
                        "motivo_1 ", Request.Form["motivo_1 "],
                        "motivo_2 ", Request.Form["motivo_2 "],
                        "motivo_3 ", Request.Form["motivo_3 "],
                        "motivo_4 ", Request.Form["motivo_4 "],
                        "registro_1 ", Request.Form["registro_1 "],
                        "registro_2 ", Request.Form["registro_2 "],
                        "registro_3 ", Request.Form["registro_3 "],
                        "registro_4 ", Request.Form["registro_4 "],
                        "vigente_1 ", Request.Form["vigente_1 "],
                        "vigente_2 ", Request.Form["vigente_2 "],
                        "vigente_3 ", Request.Form["vigente_3 "],
                        "vigente_4 ", Request.Form["vigente_4 "],
                        "id_llamada", Request.Form["id_llamada"],
                        "nivelPaciente", Request.Form["nivelPaciente"],
                        "operador_cel1", Request.Form["operador_cel1"],
                        "operador_cel2", Request.Form["operador_cel2"],
                        "estado", Request.Form["estado"],
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarObservacionSeguimientoPer2":
                    retorno = cx.InsertarRetorna("paSP_SeguimientoPerObs_Guardar2",
                        "id", Request.Form["id"],
                        "contacto", Request.Form["contacto"],
                        "observacion", Request.Form["observacion"],
                        "contesta_1 ", Request.Form["contesta_1 "],
                        "contesta_2 ", Request.Form["contesta_2 "],
                        "contesta_3 ", Request.Form["contesta_3 "],
                        "contesta_4 ", Request.Form["contesta_4 "],
                        "motivo_1 ", Request.Form["motivo_1 "],
                        "motivo_2 ", Request.Form["motivo_2 "],
                        "motivo_3 ", Request.Form["motivo_3 "],
                        "motivo_4 ", Request.Form["motivo_4 "],
                        "registro_1 ", Request.Form["registro_1 "],
                        "registro_2 ", Request.Form["registro_2 "],
                        "registro_3 ", Request.Form["registro_3 "],
                        "registro_4 ", Request.Form["registro_4 "],
                        "vigente_1 ", Request.Form["vigente_1 "],
                        "vigente_2 ", Request.Form["vigente_2 "],
                        "vigente_3 ", Request.Form["vigente_3 "],
                        "vigente_4 ", Request.Form["vigente_4 "],
                        "id_llamada", Request.Form["id_llamada"],
                        "nivelPaciente", Request.Form["nivelPaciente"],
                        "operador_cel1", Request.Form["operador_cel1"],
                        "operador_cel2", Request.Form["operador_cel2"],
                        "estado", Request.Form["estado"],
                        "tramite_medico", Request.Form["tramite_medico"],
                        "fecha_tramite", Request.Form["fecha_tramite"],
                        "cantidad", Request.Form["cantidad"],
                        "tramitesi", Request.Form["tramitesi"],
                          "contesta_1_Acu", Request.Form["contesta_1_Acu"],
                        "motivo_1_Acu", Request.Form["motivo_1_Acu"],
                        "registro_1_Acu", Request.Form["registro_1_Acu"],
                        "vigente_1_Acu", Request.Form["vigente_1_Acu"],
                        "contesta_2_Acu", Request.Form["contesta_2_Acu"],
                        "motivo_2_Acu", Request.Form["motivo_2_Acu"],
                        "registro_2_Acu", Request.Form["registro_2_Acu"],
                        "vigente_2_Acu", Request.Form["vigente_2_Acu"],
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarSeguimientoPrioritario":
                    retorno = cx.InsertarRetorna("paSP_SeguimientoPioritario_Guardar",
                        "id", Request.Form["id"],
                        "contacto", Request.Form["contacto"],
                        "observacion", Request.Form["observacion"],
                        "contesta_1 ", Request.Form["contesta_1 "],
                        "contesta_2 ", Request.Form["contesta_2 "],
                        "contesta_3 ", Request.Form["contesta_3 "],
                        "contesta_4 ", Request.Form["contesta_4 "],
                        "motivo_1 ", Request.Form["motivo_1 "],
                        "motivo_2 ", Request.Form["motivo_2 "],
                        "motivo_3 ", Request.Form["motivo_3 "],
                        "motivo_4 ", Request.Form["motivo_4 "],
                        "registro_1 ", Request.Form["registro_1 "],
                        "registro_2 ", Request.Form["registro_2 "],
                        "registro_3 ", Request.Form["registro_3 "],
                        "registro_4 ", Request.Form["registro_4 "],
                        "vigente_1 ", Request.Form["vigente_1 "],
                        "vigente_2 ", Request.Form["vigente_2 "],
                        "vigente_3 ", Request.Form["vigente_3 "],
                        "vigente_4 ", Request.Form["vigente_4 "],
                        "id_llamada", Request.Form["id_llamada"],
                        "nivelPaciente", Request.Form["nivelPaciente"],
                        "operador_cel1", Request.Form["operador_cel1"],
                        "operador_cel2", Request.Form["operador_cel2"],
                        "estado", Request.Form["estado"],
                        "tramite_medico", Request.Form["tramite_medico"],
                        "fecha_tramite", Request.Form["fecha_tramite"],
                        "cantidad", Request.Form["cantidad"],
                        "tramitesi", Request.Form["tramitesi"],
                        //
                        "tipollam", Request.Form["tipollam"],
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "GuardarObservacionDevolucionPer":
                    retorno = cx.InsertarRetorna("paSP_DevolucionPerObs_Guardar",
                        "id", Request.Form["id"],
                        "DocPaciente", Request.Form["DocPaciente"],
                        "respuesta", Request.Form["respuesta"],
                        "observacion", Request.Form["observacion"],
                        "contesta_1 ", Request.Form["contesta_1 "],
                        "contesta_2 ", Request.Form["contesta_2 "],
                        "contesta_3 ", Request.Form["contesta_3 "],
                        "contesta_4 ", Request.Form["contesta_4 "],
                        "motivo_1 ", Request.Form["motivo_1 "],
                        "motivo_2 ", Request.Form["motivo_2 "],
                        "motivo_3 ", Request.Form["motivo_3 "],
                        "motivo_4 ", Request.Form["motivo_4 "],
                        "registro_1 ", Request.Form["registro_1 "],
                        "registro_2 ", Request.Form["registro_2 "],
                        "registro_3 ", Request.Form["registro_3 "],
                        "registro_4 ", Request.Form["registro_4 "],
                        "vigente_1 ", Request.Form["vigente_1 "],
                        "vigente_2 ", Request.Form["vigente_2 "],
                        "vigente_3 ", Request.Form["vigente_3 "],
                        "vigente_4 ", Request.Form["vigente_4 "],
                        "operador_cel1", Request.Form["operador_cel1"],
                        "operador_cel2", Request.Form["operador_cel2"],
                        "dialogo", Request.Form["dialogo"],
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "CargarContacto":
                    retorno = cx.Listar("paSP_Contacto_cargar");
                    Response.Write(retorno);
                    break;

                case "cargarRecordado":
                    retorno = cx.Listar("paSP_Recordar_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaSelect":
                    retorno = cx.Listar("paSP_SelectInformar_Cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaSelect_SeguimientoSolicitudes":
                    retorno = cx.Listar("paSP_SelectSeguimientoSolicitudes_Cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaTramitesEstado":
                    retorno = cx.Listar("paSP_TramitesEstado_Cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaEstadosDevolver":
                    retorno = cx.Listar("paSP_TramitesEstadoDevolver_Cargar",
                        "idTramite", Request.Form["idTramite"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "confirmaeliminarTramite":
                    retorno = cx.InsertarRetorna("paSP_Tramite_eliminar",
                        "id", Request.Form["id"],
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "confirmadevolverTramite":
                    retorno = cx.InsertarRetorna("paSP_Tramite_devolver",
                        "id", Request.Form["id"],
                        "estado", Request.Form["estado"],
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardarCierrePacRT":
                    retorno = cx.InsertarRetorna("paSP_TramiteCierre_Guardar",
                        "id", Request.Form["id"],
                        "idLlamada", Request.Form["idLlamada"],
                        "decision", Request.Form["decision"],
                        "observacion", Request.Form["observacion"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardaeditarTramite":
                    retorno = cx.InsertarRetorna("paSP_Tramites_editar",
                        "id", Request.Form["id"],
                        "estado", Request.Form["estado"],
                        "nombre", Request.Form["nombre"],
                        "especialidad", Request.Form["especialidad"],
                        "numautori", Request.Form["numautori"],
                        "prestador", Request.Form["prestador"],
                        "fechapro", Request.Form["fechapro"],
                        "horapro", Request.Form["horapro"],
                        "medicopro", Request.Form["medicopro"],
                        "fechaOr", Request.Form["fechaOr"],
                        "vigencia", Request.Form["vigencia"],
                        "fechaVen", Request.Form["fechaVen"],
                        "nomMed", Request.Form["nomMed"],
                         "espeMed", Request.Form["espeMed"],
                         "entidad", Request.Form["entidad"],
                         "fechaProg", Request.Form["fechaProg"],
                        "tiempo_Est_Prog", Request.Form["tiempo_Est_Prog"],
                        "cantidadCiclos", Request.Form["cantidadCiclos"],
                        "relacionMed", Request.Form["relacionMed"],
                        "Diagnostico", Request.Form["Diagnostico"],
                        "codServicio", Request.Form["codServicio"],
                        "prescripcion", Request.Form["prescripcion"],
                        "periodicidad", Request.Form["periodicidad"],
                        "concentracion", Request.Form["concentracion"],
                        "NumEntregas", Request.Form["NumEntregas"],
                        "cantidad", Request.Form["cantidad"],
                        "MedNoPos", Request.Form["MedNoPos"],
                        "HistoriasCli", Request.Form["HistoriasCli"],
                        "SoporteNoPos", Request.Form["SoporteNoPos"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarArchivo":
                    retorno = cx.InsertarRetorna("paSP_Archivo_Guardar",
                        "id", Request.Form["id"],
                        "cantidad", Request.Form["cantidad"],
                           "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargarinfocontacto":
                    retorno = cx.Listar("paSP_Info_Contacto_cargar",
                        "id", Request.Form["id"],
                           "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "cargarinfocontactoRecepcion":
                    retorno = cx.Listar("paSP_Info_ContactoRecepcion_cargar",
                        "id", Request.Form["id"],
                           "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargarMotivoNoVigente":
                    retorno = cx.Listar("paSP_Motivo_No_Vigente_cargar",
                        "menu", Request.Form["menu"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargarOperadorMovil":
                    retorno = cx.Listar("paSP_OperadorMovil_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargarNivelPaciente":
                    retorno = cx.Listar("paSP_NivelPaciente_cargar");
                    Response.Write(retorno);
                    break;

                case "cargarRegional":
                    retorno = cx.Listar("paSP_Regional_cargar");
                    Response.Write(retorno);
                    break;

                case "cargaMunicipioRegional":
                    retorno = cx.Listar("paSP_MunicipioReg_cargar",
                        "regional", Request.Form["regional"]);
                    Response.Write(retorno);
                    break;

                case "ReportePacientesCargados":
                    retorno = cx.Listar("paSP_Reporte_PacCargados_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReportePacientesCargadosNuevo":
                    retorno = cx.Listar("paSP_Reporte_PacCargados_generar_Nuevo",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteSeguimiento"://////SI REQUIERE AUTORIZACION//////
                    retorno = cx.Listar("paSP_Reporte_Tramites_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                         "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteSeguimientoRegional": //////SI REQUIERE AUTORIZACION//////
                    retorno = cx.Listar("paSP_Reporte_TramitesRegional_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                         "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteSolicitudVSProgramacion": //////NO REQUIERE AUTORIZACION//////
                    retorno = cx.Listar("paSP_Reporte_Solicitud_Vs_Prog_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                         "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteSolicitudVSProgramacionRegional": //////NO REQUIERE AUTORIZACION//////
                    retorno = cx.Listar("paSP_Reporte_Solicitud_Vs_ProgRegional_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                         "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteVyAVsProgramacion":
                    retorno = cx.Listar("paSP_Reporte_VyA_Vs_Prog_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                         "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteVyAVsProgramacionRegional":
                    retorno = cx.Listar("paSP_Reporte_VyA_Vs_Prog_Regional_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                         "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteDiagnosticoPacientesMunicipio":
                    retorno = cx.Listar("paSP_Reporte_CIE10_Municipio_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "region", Request.Form["region"],
                        "depto", Request.Form["depto"],
                        "municipio", Request.Form["municipio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteProgramacionVsCita":
                    retorno = cx.Listar("paSP_Reporte_Prog_Vs_Cita_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                         "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteProgramacionVsCita2":
                    retorno = cx.Listar("paSP_Reporte_Programacion_Vs_Cita_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                         "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteProgramacionVsCitaRegional":
                    retorno = cx.Listar("paSP_Reporte_Prog_Vs_Cita_Regional_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                         "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteProgramacionVsCitaRegional2":
                    retorno = cx.Listar("paSP_Reporte_Programacion_Vs_Cita_Regional_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                         "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteSeguimientoPer":
                    retorno = cx.Listar("paSP_Reporte_SeguiPeriodico_Regional_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                         "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteSeguimientoPeriodico":
                    retorno = cx.Listar("paSP_Reporte_SeguimientoPeriodico_Regional_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                         "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteRecepcionLlamadas":
                    retorno = cx.Listar("paSP_Reporte_LlamadasRecepcion_generar_Nuevo",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteRecepcionLlamadasPrioridad":
                    retorno = cx.Listar("paSP_Reporte_LlamadasRecepcionPrioridad_generar_Nuevo",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                         "prioridad", Request.Form["prioridad"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteRecepcionLlamadasMes":
                    retorno = cx.Listar("paSP_Reporte_LlamadasRecepcionMes_generar_Nuevo",
                        "mesini", Request.Form["mesini"],
                        "mesfin", Request.Form["mesfin"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteRecepcionLlamadasHora":
                    retorno = cx.Listar("paSP_Reporte_LlamadasRecepcionHora_generar_Nuevo",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "horaini", Request.Form["horaini"],
                        "horafin", Request.Form["horafin"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteRecepcionLlamadasVSDevolucionLlamadas":
                    retorno = cx.Listar("paSP_Reporte_LlamadasRecepcionVSDevolucionLlamada_generar_Nuevo",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReportePacientesEstados":
                    retorno = cx.Listar("paSP_Reporte_PacCargadosRegional_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReportePacientesEstadosNuevo":
                    retorno = cx.Listar("paSP_Reporte_PacCargadosRegional_generar_Nuevo",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReportePacientesEstadosRegion":
                    retorno = cx.Listar("paSP_Reporte_PacCargadosMunicipio_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReportePacientesEstadosRegionNuevo":
                    retorno = cx.Listar("paSP_Reporte_PacCargadosMunicipio_generar_Nuevo",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardarMotivoOtros":
                    retorno = cx.InsertarRetorna("paSP_Motivo_Otros_Guardar",
                        "motivo", Request.Form["motivo"],
                           "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "ReportePrestadoresTramites":
                    retorno = cx.Listar("paSP_Reporte_TramitesPresta_Region_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                         "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReportePrestadoresTramites2":
                    retorno = cx.Listar("paSP_Reporte_TramitesPrestadores_Region_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                         "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReportePacientesEdadGenero":
                    retorno = cx.Listar("paSP_Reporte_Pacientes_edad_genero_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReportePrestadoresTramitesRegional":
                    retorno = cx.Listar("paSP_Reporte_TramitesPresta_Ciudad_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReportePrestadoresTramitesRegional2":
                    retorno = cx.Listar("paSP_Reporte_TramitesPrestadores_Ciudad_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteSeguimientoSolicRegional":
                    retorno = cx.Listar("paSP_Reporte_SeguiSolicitud_Regional_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                         "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "ReporteSeguimientoSolicRegional2":
                    retorno = cx.Listar("paSP_Reporte_SeguimientoSolicitud_Regional_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                         "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "ReporteReprogramacionRegional":
                    retorno = cx.Listar("paSP_Reporte_TramitesReprog_Regional_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                         "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteReprogramacionRegional2":
                    retorno = cx.Listar("paSP_Reporte_TramitesReprogramados_Regional_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                         "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteLlamadasBienvenida":
                    retorno = cx.Listar("paSP_Reporte_Pacientes_BienvMunicipio_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteLlamadasBienvenidaNuevo":
                    retorno = cx.Listar("paSP_Reporte_Pacientes_BienvMunicipio_generar_nuevo",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteLlamadasBienvenidaRegional":
                    retorno = cx.Listar("paSP_Reporte_Pacientes_BienvRegional_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteLlamadasBienvenidaRegional_Nuevo":
                    retorno = cx.Listar("paSP_Reporte_Pacientes_BienvRegional_generar_Nuevo",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReportePacientesGestionados":
                    retorno = cx.Listar("paSP_Reporte_PacientesTramites_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReportePacientesGestionadosNuevo":
                    retorno = cx.Listar("paSP_Reporte_PacientesTramites_generar_Nuevo",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReportePacientesGestionadosMunicipio":
                    retorno = cx.Listar("paSP_Reporte_PacientesTramitesRegional_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReportePacientesGestionadosMunicipio2":
                    retorno = cx.Listar("paSP_Reportes_PacientesTramitesRegionales_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "Reportetramites":
                    retorno = cx.Listar("paSP_Reporte_Tramites_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReportetramitesEspecialidad":
                    retorno = cx.Listar("paSP_Reporte_TramitesEspecialidad_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReportetramitesEspecialidad2":
                    retorno = cx.Listar("paSP_Reportes_TramitesEspecialidades_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "cargarMotivoRechazo":
                    retorno = cx.Listar("paSP_MotivoRechazo_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardaMotivoRechazo":
                    retorno = cx.InsertarRetorna("paSP_MotivoRechazo_guardar",
                        "motivo", Request.Form["motivo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardaAnalisisRechazoTemp":
                    retorno = cx.InsertarRetorna("paSP_AnalisisRechazo_temp_guardar",
                        "idtramite", Request.Form["idtramite"],
                        "rechazo", Request.Form["rechazo"],
                        "especialidad", Request.Form["especialidad"],
                        "motivo", Request.Form["motivo"],
                        "descripcion", Request.Form["descripcion"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "listarAnalisisRechazoTEMP":
                    retorno = cx.Listar("paSP_AnalisisRechazo_temp_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "borrarAnalisisRechazoTemp":
                    retorno = cx.InsertarRetorna("paSP_AnalisisRechazo_temp_eliminar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardaAnalisisRechazo":
                    retorno = cx.InsertarRetorna("paSP_AnalisisRechazo_guardar",
                        "doc", Request.Form["doc"],
                        "id_archivo", Request.Form["id_archivo"],
                        "cantidad", Request.Form["cantidad"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarLlamadainicio":
                    retorno = cx.InsertarRetorna("paSP_PacienteLlamar_guardar_pruebaV2",
                         "id", Request.Form["id"],
                        "respuesta", Request.Form["respuesta"],
                        "observacion", Request.Form["observacion"],
                        "contesta_1 ", Request.Form["contesta_1 "],
                        "contesta_2 ", Request.Form["contesta_2 "],
                        "contesta_3 ", Request.Form["contesta_3 "],
                        "contesta_4 ", Request.Form["contesta_4 "],
                        "motivo_1 ", Request.Form["motivo_1 "],
                        "motivo_2 ", Request.Form["motivo_2 "],
                        "motivo_3 ", Request.Form["motivo_3 "],
                        "motivo_4 ", Request.Form["motivo_4 "],
                        "registro_1 ", Request.Form["registro_1 "],
                        "registro_2 ", Request.Form["registro_2 "],
                        "registro_3 ", Request.Form["registro_3 "],
                        "registro_4 ", Request.Form["registro_4 "],
                        "vigente_1 ", Request.Form["vigente_1 "],
                        "vigente_2 ", Request.Form["vigente_2 "],
                        "vigente_3 ", Request.Form["vigente_3 "],
                        "vigente_4 ", Request.Form["vigente_4 "],
                        "nivelPaciente", Request.Form["nivelPaciente"],
                        "operador_cel1", Request.Form["operador_cel1"],
                        "operador_cel2", Request.Form["operador_cel2"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "GuardarLlamadainicio2":
                    retorno = cx.InsertarRetorna("paSP_PacienteLlamar_guardar2",
                         "id", Request.Form["id"],
                        "respuesta", Request.Form["respuesta"],
                        "observacion", Request.Form["observacion"],
                        "contesta_1 ", Request.Form["contesta_1 "],
                        "contesta_2 ", Request.Form["contesta_2 "],
                        "contesta_3 ", Request.Form["contesta_3 "],
                        "contesta_4 ", Request.Form["contesta_4 "],
                        "motivo_1 ", Request.Form["motivo_1 "],
                        "motivo_2 ", Request.Form["motivo_2 "],
                        "motivo_3 ", Request.Form["motivo_3 "],
                        "motivo_4 ", Request.Form["motivo_4 "],
                        "registro_1 ", Request.Form["registro_1 "],
                        "registro_2 ", Request.Form["registro_2 "],
                        "registro_3 ", Request.Form["registro_3 "],
                        "registro_4 ", Request.Form["registro_4 "],
                        "vigente_1 ", Request.Form["vigente_1 "],
                        "vigente_2 ", Request.Form["vigente_2 "],
                        "vigente_3 ", Request.Form["vigente_3 "],
                        "vigente_4 ", Request.Form["vigente_4 "],
                        "nivelPaciente", Request.Form["nivelPaciente"],
                        "operador_cel1", Request.Form["operador_cel1"],
                        "operador_cel2", Request.Form["operador_cel2"],
                        "cantidad", Request.Form["cantidad"],
                        "tramitesi", Request.Form["tramitesi"],
                        "contesta_1_Acu", Request.Form["contesta_1_Acu"],
                        "motivo_1_Acu", Request.Form["motivo_1_Acu"],
                        "registro_1_Acu", Request.Form["registro_1_Acu"],
                        "vigente_1_Acu", Request.Form["vigente_1_Acu"],
                        "contesta_2_Acu", Request.Form["contesta_2_Acu"],
                        "motivo_2_Acu", Request.Form["motivo_2_Acu"],
                        "registro_2_Acu", Request.Form["registro_2_Acu"],
                        "vigente_2_Acu", Request.Form["vigente_2_Acu"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarLlamadaRecordarEnvio":
                    retorno = cx.InsertarRetorna("paSP_PacienteLlamar_recordarEnvio_guardar",
                         "id", Request.Form["id"],
                        "respuesta", Request.Form["respuesta"],
                        "observacion", Request.Form["observacion"],
                        "contesta_1 ", Request.Form["contesta_1 "],
                        "contesta_2 ", Request.Form["contesta_2 "],
                        "contesta_3 ", Request.Form["contesta_3 "],
                        "contesta_4 ", Request.Form["contesta_4 "],
                        "motivo_1 ", Request.Form["motivo_1 "],
                        "motivo_2 ", Request.Form["motivo_2 "],
                        "motivo_3 ", Request.Form["motivo_3 "],
                        "motivo_4 ", Request.Form["motivo_4 "],
                        "registro_1 ", Request.Form["registro_1 "],
                        "registro_2 ", Request.Form["registro_2 "],
                        "registro_3 ", Request.Form["registro_3 "],
                        "registro_4 ", Request.Form["registro_4 "],
                        "vigente_1 ", Request.Form["vigente_1 "],
                        "vigente_2 ", Request.Form["vigente_2 "],
                        "vigente_3 ", Request.Form["vigente_3 "],
                        "vigente_4 ", Request.Form["vigente_4 "],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarInfoRechazoDetalle":
                    retorno = cx.Listar("paSP_Rechazo_listar",
                        "id", Request.Form["id"],
                        "idarchivo", Request.Form["idarchivo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "GuardaTramiteTemp":
                    retorno = cx.InsertarRetorna("paSP_AnalisisGestionar_temp_guardar",
                        "docPaciente", Request.Form["docPaciente"],
                        "especialidad", Request.Form["especialidad"],
                        "fechaSistema", Request.Form["fechaSistema"],
                        "fechaOrden", Request.Form["fechaOrden"],
                        "vigencia", Request.Form["vigencia"],
                        "fechaVencimiento", Request.Form["fechaVencimiento"],
                        "codigoCie", Request.Form["codigoCie"],
                        "medicoOrdena", Request.Form["medicoOrdena"],
                        "especialidadMedOrdena", Request.Form["especialidadMedOrdena"],
                        "institucion", Request.Form["institucion"],
                        "codOSI", Request.Form["codOSI"],
                        "nombreTramite", Request.Form["nombreTramite"],
                        "prestador", Request.Form["prestador"],
                        "requiereAUTO", Request.Form["requiereAUTO"],
                        "aprobacion", Request.Form["aprobacion"],
                        "tipo", Request.Form["tipo"],
                        "numAUTO", Request.Form["numAUTO"],

                        "tipocita", Request.Form["tipocita"],
                        "especialidadCita", Request.Form["especialidadCita"],
                        "tiempoProg", Request.Form["tiempoProg"],
                        "tipoPac", Request.Form["tipoPac"],
                        "prioridad", Request.Form["prioridad"],
                        "NomMedicamento", Request.Form["NomMedicamento"],
                        "UnidadBase", Request.Form["UnidadBase"],
                        "dosis", Request.Form["dosis"],
                        "presentacion", Request.Form["presentacion"],
                        "periodicidad", Request.Form["periodicidad"],
                        "cantidadFrecuencia", Request.Form["cantidadFrecuencia"],
                        "cantidad", Request.Form["cantidad"],
                        "tiempoTram", Request.Form["tiempoTram"],
                        "cantidadtiempoTram", Request.Form["cantidadtiempoTram"],
                        "cantidadMed", Request.Form["cantidadMed"],
                        "cantidadEntre", Request.Form["cantidadEntre"],
                        "codServicio", Request.Form["codServicio"],
                        "fechaProg", Request.Form["fechaProg"],
                        "ciclos", Request.Form["ciclos"],

                        "medicoAgendar", Request.Form["medicoAgendar"],
                        "especialidadMedAgendar", Request.Form["especialidadMedAgendar"],
                         "diagnosticoPac", Request.Form["diagnosticoPac"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "CargarDiagnosticoPaciente":
                    retorno = cx.Listar("paSP_DiagnoticoPaciente_cargar",
                         "documento", Request.Form["documento"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "borrarAnalisisTramitesTemp":
                    retorno = cx.InsertarRetorna("paSP_AnalisisGestionar_temp_eliminar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "borrarAnalisisTramitesTemp_auto":
                    retorno = cx.InsertarRetorna("paSP_AnalisisGestionar_temp_auto_eliminar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarAnalisisGestionarTEMP":
                    retorno = cx.Listar("paSP_AnalisisGestionar_temp_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarDocumento":
                    retorno = cx.Listar("paSP_Documento_Paciente_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardaAnalisisGestion":
                    retorno = cx.InsertarRetorna("paSP_AnalisisGestion_guardar",
                        "doc", Request.Form["doc"],
                        "id_archivo", Request.Form["id_archivo"],
                        "UAP", Request.Form["UAP"],
                        "tipoArch", Request.Form["tipoArch"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardaAnalisisGestionV2":
                    retorno = cx.InsertarRetorna("paSP_AnalisisGestion_guardarV2",
                        "doc", Request.Form["doc"],
                        "id_archivo", Request.Form["id_archivo"],
                        "UAP", Request.Form["UAP"],
                        "tipoArch", Request.Form["tipoArch"],
                        "tipo", Request.Form["tipo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;




                case "GuardaAnalisisGestionPrimeraVez":
                    retorno = cx.InsertarRetorna("paSP_AnalisisGestionPrimeraVez_guardar",
                        "doc", Request.Form["doc"],
                        "id_archivo", Request.Form["id_archivo"],
                        "UAP", Request.Form["UAP"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "listarRegionales":
                    retorno = cx.Listar("paSP_Regionales_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarRegionalesRol":
                    retorno = cx.Listar("paSP_RegionalesRol_listar",
                        "rol", Request.Form["rol"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "listarRegionalesRolDetalle":
                    retorno = cx.Listar("paSP_RegionalesRol_Detalle",
                        "rol", Request.Form["rol"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarCodigoCie10":
                    retorno = cx.Listar("paSP_CodigosCie10_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_cie10_x_anios_listar":
                    retorno = cx.Listar("paSP_cie10_x_anios_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_tipos_servicio_x_anios_listar":
                    retorno = cx.Listar("paSP_tipos_servicio_x_anios_listar",
                        "id", Request.Form["id"],
                        "anio", Request.Form["anio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_cie10_x_anios2_listar":
                    retorno = cx.Listar("paSP_cie10_x_anios2_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_ciudad_x_anios2_listar":
                    retorno = cx.Listar("paSP_ciudad_x_anios2_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_tipo_servicio_x_anios2_listar":
                    retorno = cx.Listar("paSP_tipo_servicio_x_anios2_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_graf_correos_x_mes_listar":
                    retorno = cx.Listar("paSP_graf_correos_x_mes_listar",
                        "mes", Request.Form["mes"],
                        "anio", Request.Form["anio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_graf_correos_x_regional_listar":
                    retorno = cx.Listar("paSP_graf_correos_x_regional_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_cie10_x_anios_regional_listar":
                    retorno = cx.Listar("paSP_cie10_x_anios_regional_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_cie10_x_anios_regional3_listar":
                    retorno = cx.Listar("paSP_cie10_x_anios_regional3_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_ciudad_x_anios_regional_listar":
                    retorno = cx.Listar("paSP_ciudad_x_anios_regional_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_tipo_servicio_x_anios_regional_listar":
                    retorno = cx.Listar("paSP_tipo_servicio_x_anios_regional_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;
                case "ReportePacientesXmes":
                    retorno = cx.Listar("paSP_UsuariosCreadosXmes_listar",
                        "tipo", Request.Form["tipo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "PacientesTipoContar":
                    retorno = cx.Listar("paSP_PacientesTipo_contar",
                        "tipo", Request.Form["tipo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarCierrePaciente":
                    retorno = cx.InsertarRetorna("paSP_AnalisisGestion_cierrePaciente",
                        "docPaciente", Request.Form["docPaciente"],
                        "decision", Request.Form["decision"],
                        "observacion", Request.Form["observacion"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarCierrePacienteRechazados":
                    retorno = cx.InsertarRetorna("paSP_historialRechazados_cierrePaciente",
                        "docPaciente", Request.Form["docPaciente"],
                        "primer_nombre", Request.Form["primer_nombre"],
                        "segundo_nombre", Request.Form["segundo_nombre"],
                        "primer_apellido", Request.Form["primer_apellido"],
                        "segundo_apelido", Request.Form["segundo_apelido"],
                        "decision", Request.Form["decision"],
                        "observacion", Request.Form["observacion"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "detallePaciente":
                    retorno = cx.InsertarRetorna("paSP_Paciente_detalle_listar",
                        "docPaciente", Request.Form["docPaciente"],
                        "decision", Request.Form["decision"],
                        "observacion", Request.Form["observacion"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "ReporteDiagnosticoPacientesRegional":
                    retorno = cx.Listar("paSP_Reporte_Clinico_General",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "region", Request.Form["region"],
                        "ciudad", Request.Form["ciudad"],
                        "categoriaCie", Request.Form["categoriaCie"],
                        "codigoCie", Request.Form["codigoCie"],
                        "diagnos", Request.Form["diagnos"],
                        "tipo", Request.Form["tipo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "actualizarPactientes":
                    retorno = cx.InsertarRetorna("paSP_PacientesEstados_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteDiagnostico":
                    retorno = cx.Listar("paSP_ReporteDiagnostico_generar",
                        "tipo", Request.Form["tipo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteDiagnosticoTotales":
                    retorno = cx.Listar("paSP_ReporteDiagnosticoTotales_generar",
                        "tipo", Request.Form["tipo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "tramites_validar":
                    retorno = cx.Listar("paSP_Tramites_parametrizados_validar",
                        "codigo", Request.Form["codigo"],
                        //   "nombre", Request.Form["nombre"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "Osi_Sanitas_validar":
                    retorno = cx.Listar("paSP_Osi_Sanitas_validar",
                        "codigo", Request.Form["codigo"],
                        //   "nombre", Request.Form["nombre"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "validarMedico":
                    retorno = cx.Listar("paSP_Medico_validar",
                        "nombre", Request.Form["nombre"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "validarPrestador":
                    retorno = cx.Listar("paSP_Prestador_validar",
                        "nombre", Request.Form["nombre"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteEpidemiologico":
                    retorno = cx.Listar("paSP_ReporteEpidemiologico_generar",
                        "categoria", Request.Form["categoria"],
                        "codigo", Request.Form["codigo"],
                        "tipo", Request.Form["tipo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "especialidad_fechaLimite_cargar":
                    retorno = cx.Listar("paSP_Especialidad_FechaLimite_cargar",
                        "especialidad", Request.Form["especialidad"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "CargarReporteIndicador":
                    retorno = cx.Listar("paSP_ReporteIndicadores_Cargar");
                    Response.Write(retorno);
                    break;

                case "CargarAnio":
                    retorno = cx.Listar("paSP_Anio_Cargar");
                    Response.Write(retorno);
                    break;

                case "AnioPacientes_Cargar":
                    retorno = cx.Listar("paSP_AnioPacientes_Cargar");
                    Response.Write(retorno);
                    break;

                case "IndicadorCumplimientoGestionCargar":
                    retorno = cx.Listar("paSP_IndicadorCumplimientoGestion_generar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "IndicadorLlamadaBienvenidaCargar":
                    retorno = cx.Listar("paSP_IndicadorLlamadaBienvenida_generar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "IndicadorContactabilidadPacienteCargar":
                    retorno = cx.Listar("paSP_IndicadorContactabilidadPaciente_generar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "IndicadorNoAceptacionCargar":
                    retorno = cx.Listar("paSP_IndicadorNoAceptacion_generar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "IndicadorDevolucionInoportunidadCargar":
                    retorno = cx.Listar("paSP_IndicadorDevolucionInoportunidad_generar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;
                case "listarReporteCumplimiento":
                    retorno = cx.Listar("paSP_ReporteCumplimiento_listar",
                        "anio", Request.Form["anio"],
                        //"reporte", Request.Form["reporte"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarReporteEfectividadBi":
                    retorno = cx.Listar("paSP_ReporteEfectividad_listar",
                        "anio", Request.Form["anio"],
                        //"reporte", Request.Form["reporte"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarReporteNoaceptacion":
                    retorno = cx.Listar("paSP_ReporteNoaceptacion_listar",
                        "anio", Request.Form["anio"],
                        //"reporte", Request.Form["reporte"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "listarReporteDevolucionInop":
                    retorno = cx.Listar("paSP_ReporteDevolucionInop_listar",
                        "anio", Request.Form["anio"],
                        //"reporte", Request.Form["reporte"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarReporteContactacUsu":
                    retorno = cx.Listar("paSP_ReporteContactacUsu_listar",
                        "anio", Request.Form["anio"],
                        //"reporte", Request.Form["reporte"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarReporteContactacIps":
                    retorno = cx.Listar("paSP_ReporteContactacIps_listar",
                        "anio", Request.Form["anio"],
                        //"reporte", Request.Form["reporte"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "eliminarArchivosTemps":
                    retorno = cx.Listar("paSP_ArchivosTramitesPaciente_Guarda",
                        "nombreArchivo", Request.Form["nombreArchivo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "programarSolicitudes_guardar":
                    retorno = cx.InsertarRetorna("paSP_programarSolicitudes_guardar2",
                        "id", Request.Form["id"],
                        "numautorizacion", Request.Form["numautorizacion"],
                        "prestador", Request.Form["prestador"],
                        "fechacita", Request.Form["fechacita"],
                        "horacita", Request.Form["horacita"],
                        "medicocita", Request.Form["medicocita"],
                        "tipo", Request.Form["tipo"],

                        "presentacion", Request.Form["presentacion"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "programarSolicitudes_Sanitas_guardar":
                    retorno = cx.InsertarRetorna("paSP_programarSolicitudes_Sanitas_guardar",
                        "id", Request.Form["id"],
                        "numautorizacion", Request.Form["numautorizacion"],
                        "prestador", Request.Form["prestador"],
                        "fechacita", Request.Form["fechacita"],
                        "horacita", Request.Form["horacita"],
                        "medicocita", Request.Form["medicocita"],
                        "tipo", Request.Form["tipo"],
                        "presentacion", Request.Form["presentacion"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "SeguimientoPeriodico_cargar":
                    retorno = cx.Listar("paSP_SeguimientoPeriodico_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardarmotivootrospac":
                    retorno = cx.InsertarRetorna("paSP_Motivo_Otros_Pac_Guardar",
                        "motivo", Request.Form["motivo"],
                           "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "borrarArchivosTemCorreo":
                    retorno = cx.InsertarRetorna("paSP_CorreoArchivosAuto_borrar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargarCategoriasDireccionamiento":
                    retorno = cx.Listar("paSP_categorias_direccionamiento_cargar",
                           "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaTipoProgramacion":
                    retorno = cx.Listar("paSP_TipoProgramacion_cargar");
                    Response.Write(retorno);
                    break;

                case "cargaTipoVyA":
                    retorno = cx.Listar("paSP_Tipo_Autorizacion_cargar",
                           "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardarmotivoOtrosVyA":
                    retorno = cx.InsertarRetorna("paSP_Motivo_Otros_VyA_Guardar",
                        "motivo", Request.Form["motivo"],
                           "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaEstadoRecordacion":
                    retorno = cx.Listar("paSP_estadoRecordacion_cargar",
                           "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarTramitesPredecesor":
                    retorno = cx.Listar("paSP_AnalisisGestionar_Predecesores_listar",
                        "idtramite", Request.Form["idtramite"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "predecesorGuardar":
                    retorno = cx.InsertarRetorna("paSP_Predecesor_guardar",
                        "tramite", Request.Form["tramite"],
                        "tramitePadre", Request.Form["tramitePadre"],
                        "estado", Request.Form["estado"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarPredecesoras":
                    retorno = cx.Listar("paSP_Predecesoras_listar",
                        "idtramite", Request.Form["idtramite"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "detalleTramitesInfoDetallada":
                    retorno = cx.Listar("paSP_Tramites_InfoDetallada_detalle",
                        "idtramite", Request.Form["idtramite"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "detalleTramitesInfoDetalladaCUC2":////////CASO PARA SP2017
                    retorno = cx.Listar("paCUC_Tramites_InfoDetallada3_detalle",
                        "idtramite", Request.Form["idtramite"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "detalleTramitesInfoDetallada2":
                    retorno = cx.Listar("paSP_Tramites_InfoDetallada2_detalle",
                        "idtramite", Request.Form["idtramite"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "detalleTramitesInfoDetallada3":
                    retorno = cx.Listar("paSP_Tramites_InfoDetallada3_detalle",
                        "idtramite", Request.Form["idtramite"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "listarInfoDetalleOsi":
                    retorno = cx.Listar("paSP_Tramites_InfoOsi_detalle",
                        "idtramite", Request.Form["idtramite"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "reprogramacionGuardarInformar":
                    retorno = cx.InsertarRetorna("paSP_reprogramacionInformarPac_guardar",
                        "idtramite", Request.Form["idtramite"],
                        "motivo", Request.Form["motivo"],
                        "observacion", Request.Form["observacion"],
                        "reqauto", Request.Form["reqauto"],
                        "idLlamada", Request.Form["idLlamada"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "reprogramacionGuardarRecordar":
                    retorno = cx.InsertarRetorna("paSP_reprogramacionRecordarPac_guardar",
                        "idtramite", Request.Form["idtramite"],
                        "motivo", Request.Form["motivo"],
                        "observacion", Request.Form["observacion"],
                        "reqauto", Request.Form["reqauto"],
                        "idLlamada", Request.Form["idLlamada"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardarMotivoRepro":
                    retorno = cx.InsertarRetorna("paSP_MotivoRepro_guardar",
                        "motivo", Request.Form["motivo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargarMotivoReprogramacion":
                    retorno = cx.Listar("paSP_MotivoReprogramar_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "CargaUAPPaciente":
                    retorno = cx.Listar("paSP_UAP_Paciente_cargar",
                        "docPaciente", Request.Form["docPaciente"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "CalculaFechaVencimientoTramite":
                    retorno = cx.Listar("paSP_Tramite_FechaVencimiento_calcular",
                        "fechaOrden", Request.Form["fechaOrden"],
                        "vigencia", Request.Form["vigencia"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarDetallePaciente":
                    retorno = cx.Listar("paSP_Pacientes_Detalle",
                        "documento", Request.Form["documento"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargarUltimaRespuesta":
                    retorno = cx.Listar("paSP_UltimaRespuesta_Listar",
                        "idGlobalTramiteNoAsignadas", Request.Form["idGlobalTramiteNoAsignadas"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ListarInfoDetalladaTram":
                    retorno = cx.Listar("paSP_InfoDetalladaTram_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarDetalleHospitalario":
                    retorno = cx.Listar("paSP_DetalleHospitalario_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarDetalleMedicamentos":
                    retorno = cx.Listar("paSP_DetalleMedicamentos_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "DetalleDirRegional":
                    retorno = cx.Listar("paSP_DireccionamientoRegional_detalle",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarDetalleNoPos":
                    retorno = cx.Listar("paSP_DetalleNoPos_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarDetalleOdontologico":
                    retorno = cx.Listar("paSP_DetalleOdontologico_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "generalAuditoria":
                    retorno = cx.Listar("paSP_Auditoria_General_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "usuario", Request.Form["usuario"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "pacienteinicioAuditoria":
                    retorno = cx.Listar("paSP_Auditoria_PacienteInicio_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "usuario", Request.Form["usuario"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "informacionUsuarioAuditoria":
                    retorno = cx.Listar("paSP_Auditoria_Usuario_generar",
                        "usuario", Request.Form["usuario"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargueAuditoria":
                    retorno = cx.Listar("paSP_Auditoria_Cargue_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "usuario", Request.Form["usuario"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "analisisAuditoria":
                    retorno = cx.Listar("paSP_Auditoria_Analisis_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "usuario", Request.Form["usuario"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "autorizacionAuditoria":
                    retorno = cx.Listar("paSP_Auditoria_Autorizaciones_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "usuario", Request.Form["usuario"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "programacionesAuditoria":
                    retorno = cx.Listar("paSP_Auditoria_Programaciones_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "usuario", Request.Form["usuario"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "informarAuditoria":
                    retorno = cx.Listar("paSP_Auditoria_InformaPaciente_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "usuario", Request.Form["usuario"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "recordarAuditoria":
                    retorno = cx.Listar("paSP_Auditoria_RecordarPaciente_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "usuario", Request.Form["usuario"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "seguimientosolicitudesAuditoria":
                    retorno = cx.Listar("paSP_Auditoria_SeguimientoSolicitudes_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "usuario", Request.Form["usuario"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "seguimientoperiodicoAuditoria":
                    retorno = cx.Listar("paSP_Auditoria_SeguiPeriodico_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "usuario", Request.Form["usuario"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listartipotramite":
                    retorno = cx.Listar("paSP_tipotramite_cargar_editar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ListarCiudReportePrestadoresTramitesadesRegional":
                    retorno = cx.Listar("paSP_CiudadesRegional_listar",
                        "Regional", Request.Form["Regional"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ListarCategoriasCie10":
                    retorno = cx.Listar("paSP_CategoriasCie10_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ListarCodigosCie10":
                    retorno = cx.Listar("paSP_CodigosCie10Full_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "validarUsuario":
                    retorno = cx.Listar("paSP_Usuario_validar",
                        "documento", Request.Form["documento"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "validarPacienteSeguimientoPrioritario":
                    retorno = cx.Listar("paSP_PacienteValidar_listar",
                        "documento", Request.Form["documento"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaRespuestaTieneprepagada":
                    retorno = cx.Listar("paSP_RespuestaTieneprepagada_validar");
                    Response.Write(retorno);
                    break;

                case "CargaRespuesNoContactable":
                    retorno = cx.Listar("paSP_RespuestaNoContactable_validar");
                    Response.Write(retorno);
                    break;

                case "contarPacientesNuevosCompletosSP":
                    retorno = cx.Listar("paSP_PacientesNuevosCompletos_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarPacientesNuevosMasivosSP":
                    retorno = cx.Listar("paSP_PacientesNuevosMasivos_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "contarPacientesActivosSP":
                    retorno = cx.Listar("paSP_PacientesActivos_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "contarPacientesCierreSP":
                    retorno = cx.Listar("paSP_PacientesCierre_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaMes":
                    retorno = cx.Listar("paSP_Mes_cargar");
                    Response.Write(retorno);
                    break;

                case "Mes2_cargar":
                    retorno = cx.Listar("paSP_Mes2_cargar");
                    Response.Write(retorno);
                    break;

                case "listarEstadistica":
                    retorno = cx.Listar("paSP_Reporte_Estadisticas_Generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "mes", Request.Form["mes"],
                        "cargue", Request.Form["cargue"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarEstadistica_NC":

                    Thread.Sleep(3333);
                    retorno = cx.Listar("paSP_Reporte_Estadisticas_NC_Generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "mes", Request.Form["mes"],
                        "cargue", Request.Form["cargue"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listaMedico":
                    retorno = cx.Listar("paSP_Medico_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "CargarOpcion":
                    retorno = cx.Listar("paSP_Opcion_Cargar");
                    Response.Write(retorno);
                    break;

                case "cargarEstadoDiagnostico":
                    retorno = cx.Listar("paSP_EstadoDiagnotico_cargar");
                    Response.Write(retorno);
                    break;

                case "GuardaTramiteTemp2":
                    retorno = cx.InsertarRetorna("paSP_AnalisisPrimeraVez_guardar",
                        "docPaciente", Request.Form["docPaciente"],
                         "opcion", Request.Form["opcion"],
                         "especialidad", Request.Form["especialidad"],
                        "codOSI", Request.Form["codOSI"],
                        "tramite", Request.Form["tramite"],
                        "requiereAUTO", Request.Form["requiereAUTO"],
                        "aprobacion", Request.Form["aprobacion"],
                        "numAUTO", Request.Form["numAUTO"],
                        "prestador", Request.Form["prestador"],
                        "tipo", Request.Form["tipo"],
                        "nombreTramite", Request.Form["nombreTramite"],
                        "UAP", Request.Form["UAP"],
                        "codigoCie", Request.Form["codigoCie"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarSeguimiento":
                    retorno = cx.InsertarRetorna("paSP_AnalisisSeguimiento_guardar",
                        "docPaciente", Request.Form["docPaciente"],
                        "observacionSegui", Request.Form["observacionSegui"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarRegional":
                    retorno = cx.InsertarRetorna("paSP_Regionales_guardar",
                        "id", Request.Form["id"],
                        "nombre", Request.Form["nombre"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "EliminarRegional":
                    retorno = cx.InsertarRetorna("paSP_Regionales_borrar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "AsociarCiudades_guardar":
                    retorno = cx.InsertarRetorna("paSP_AsociarCiudades_guardar",
                        "CiudadAsociar", Request.Form["CiudadAsociar"],
                        "CiudadQuitar", Request.Form["CiudadQuitar"],
                        "regional", Request.Form["regional"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "RegionalMunicipio_quitar":
                    retorno = cx.InsertarRetorna("paSP_RegionalMunicipio_quitar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarPacIngresadosContactados":
                    retorno = cx.Listar("paSP_Facturacion_uno",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "listarPacGestionados":
                    retorno = cx.Listar("paSP_Facturacion_dos",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarTramitesCreados":
                    retorno = cx.Listar("paSP_Facturacion_tres",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarTramitesCreados2":
                    retorno = cx.Listar("paSP_Facturacion_siete",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarTramitesCreados3":
                    retorno = cx.Listar("paSP_Facturacion_ocho",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarPacContactadosSeguimiento":
                    retorno = cx.Listar("paSP_Facturacion_cuatro",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarCitasconSeguimiento":
                    retorno = cx.Listar("paSP_Facturacion_cinco",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarCitasReprogramadas":
                    retorno = cx.Listar("paSP_Facturacion_seis",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarPrestadorEditar":
                    retorno = cx.Listar("paSP_PrestadoresEditar_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarEditarServicio":
                    retorno = cx.Listar("paSP_PrestadorServicioEditar_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;
                //case "listarPrestadorSedeDetalle":
                //    retorno = cx.Listar("paSP_PrestadoresSedeDetalle_listar",
                //        "id", Request.Form["id"],
                //        "empresa", empresa,
                //        "responsable", responsable);
                //    Response.Write(retorno);
                //   break;
                case "listarContacto":
                    retorno = cx.Listar("paSP_PrestadorContacto_listar",
                         "id", Request.Form["id"],
                         "espe", Request.Form["espe"],
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarContactoPrestador":
                    retorno = cx.Listar("paSP_ContactoPrestador_listar",
                         "espe", Request.Form["espe"],
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "PrestadorEspecialidad_listar":
                    retorno = cx.Listar("paSP_PrestadorEspecialidad_listar",
                         "id", Request.Form["id"],
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "EnviosCorreoAuto_listar":
                    retorno = cx.Listar("paSP_EnviosCorreoAuto_listar",
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "borrarTempContacto":
                    retorno = cx.InsertarRetorna("paSP_PrestadorContactoTemp_eliminar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarContacto":
                    retorno = cx.InsertarRetorna("paSP_PrestadorContacto_guardar",
                        "id", Request.Form["id"],
                        "contacto", Request.Form["contacto"],
                        "categoria", Request.Form["categoria"],
                        "campo", Request.Form["campo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarContactoRegPres":
                    retorno = cx.InsertarRetorna("paSP_GuardarContactoRegPres_guardar",
                        "contacto", Request.Form["contacto"],
                        "categoria", Request.Form["categoria"],
                        "campo", Request.Form["campo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarFinalContacto":
                    retorno = cx.InsertarRetorna("paSP_FinalContacto_guardar",
                        "globalPrestador", Request.Form["globalPrestador"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "Especialidad_guardar":
                    retorno = cx.InsertarRetorna("paSP_PrestadorEspecialidad_guardar",
                        "id", Request.Form["id"],
                        "id_espe", Request.Form["id_espe"],
                        "especialidad", Request.Form["especialidad"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "EliminarContacto":
                    retorno = cx.InsertarRetorna("paSP_PrestadoresContacto_eliminar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "EliminarEspecialidadSede":
                    retorno = cx.InsertarRetorna("paSP_EspecialidadesSede_eliminar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarPrestadorServicio":
                    retorno = cx.Listar("paSP_PrestadorServicio_guardar",
                        "id", Request.Form["id"],
                        "codigoOsi", Request.Form["codigoOsi"],
                        "opcion", Request.Form["opcion"],
                        "requiereAuto", Request.Form["requiereAuto"],
                        "observacion", Request.Form["observacion"],
                        "codigoServi", Request.Form["codigoServi"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                //case "listarPrestadorServicio":
                //    retorno = cx.Listar("paSP_PrestadorServicio_listar",
                //         "empresa", empresa,
                //        "responsable", responsable);
                //    Response.Write(retorno);
                //    break;
                //case "listarPrestadorServicio":
                //    retorno = cx.Listar("paSP_PrestadorServicio_listar",
                //         "id", Request.Form["id"],
                //         "empresa", empresa,
                //        "responsable", responsable);
                //    Response.Write(retorno);
                //    break;

                case "eliminarServicio":
                    retorno = cx.InsertarRetorna("paSP_PrestadoresServicio_eliminar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "borrarTempServicio":
                    retorno = cx.InsertarRetorna("paSP_PrestadorServicioTemp_eliminar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarPrestadorFinal":
                    retorno = cx.InsertarRetorna("paSP_PrestadorFinal_guardar",
                        //"id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "PacienteTipoFiltro_cargar":
                    retorno = cx.Listar("paSP_PacienteTipoFiltro_cargar",
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarContactoEditar":
                    retorno = cx.Listar("paSP_PrestadorContactoEditar_listar",
                        "id", Request.Form["id"],
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "UAPvalidar":
                    retorno = cx.Listar("paSP_Uap_validar",
                        "codigo", Request.Form["codigo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarUap":
                    retorno = cx.InsertarRetorna("paSP_PrestadorUap_guardar",
                        "id", Request.Form["id"],
                        "codigoUap", Request.Form["codigoUap"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarUap":
                    retorno = cx.Listar("paSP_PrestadorUap_listar",
                         "id", Request.Form["id"],
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "borrarTempUap":
                    retorno = cx.InsertarRetorna("paSP_PrestadorUapTemp_eliminar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "EliminarUap":
                    retorno = cx.InsertarRetorna("paSP_PrestadorUap_eliminar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "GuardarUapFinal":
                    retorno = cx.InsertarRetorna("paSP_PrestadorUapFinal_guardar",
                       "id", Request.Form["id"],
                       "codiSede", Request.Form["codiSede"],
                       "idPres", Request.Form["idPres"],
                       "sedeNombre", Request.Form["sedeNombre"],
                       "ciudad", Request.Form["ciudad"],
                       "direccion", Request.Form["direccion"],
                       "empresa", empresa,
                       "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarUbicacionTemp":
                    retorno = cx.InsertarRetorna("paSP_UbicacionTemp_guardar",
                        "id", Request.Form["id"],
                        "ciudad", Request.Form["ciudad"],
                        "direccion", Request.Form["direccion"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarUbicacionTemp":
                    retorno = cx.Listar("paSP_UbicacionTemp_listar",
                         "id", Request.Form["id"],
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "borrarTempUbicacion":
                    retorno = cx.InsertarRetorna("paSP_UbicacionTemp_eliminar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "EliminarUbicacion":
                    retorno = cx.InsertarRetorna("paSP_Ubicacion_eliminar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "listarDialogoActual":
                    retorno = cx.Listar("paSP_Dialogo_Actual_listar",
                         "id", Request.Form["id"],
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarObservacionActual":
                    retorno = cx.Listar("paINI_Observacion_Actual_listar",
                         "id", Request.Form["id"],
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarDialogoActualRecepcion":
                    retorno = cx.Listar("paSP_DialogoActualRecepcion_listar",
                         "id", Request.Form["id"],
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "activarVersion":
                    retorno = cx.InsertarRetorna("paSP_Version_Dialogo_activar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "activarVersionObs":
                    retorno = cx.InsertarRetorna("paINI_Version_Observacion_activar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "listarDialogoVersion":
                    retorno = cx.Listar("paSP_Dialogo_Version_detalle",
                         "id", Request.Form["id"],
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarObservacionVersion":
                    retorno = cx.Listar("paINI_Observacion_Version_detalle",
                         "id", Request.Form["id"],
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "OrdenTemp_borrar":
                    retorno = cx.InsertarRetorna("paSP_OrdenTemp_borrar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "areasEmpresa_cargar":
                    retorno = cx.Listar("paSP_areasEmpresa_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ordenes_contar":
                    retorno = cx.Listar("paSP_ordenes_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "OrdenTemp_guardar":
                    retorno = cx.InsertarRetorna("paSP_OrdenTemp_guardar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "Orden_guardar":
                    retorno = cx.InsertarRetorna("paSP_Orden_guardar",
                        "numcaso", Request.Form["numcaso"],
                        "area", Request.Form["area"],
                        "fechaOrden", Request.Form["fechaOrden"],
                        "horaOrden", Request.Form["horaOrden"],
                        "titulo", Request.Form["titulo"],
                        "servicio", Request.Form["servicio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;



                case "Orden2_guardar":
                    retorno = cx.InsertarRetorna("paSP_Orden2_guardar",
                        "numcaso", Request.Form["numcaso"],
                        "area", Request.Form["area"],
                        "fechaOrden", Request.Form["fechaOrden"],
                        "horaOrden", Request.Form["horaOrden"],
                        "titulo", Request.Form["titulo"],
                        "servicio", Request.Form["servicio"],
                        "prioridad", Request.Form["prioridad"],
                        //Motivo Orden
                         "motivo", Request.Form["motivo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "OrdenServicio_Temp_listar":
                    retorno = cx.Listar("paSP_OrdenServicio_Temp_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "EstadoMesa_cargar":
                    retorno = cx.Listar("paSP_EstadoMesa_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "orden_eliminar":
                    retorno = cx.InsertarRetorna("paSP_Orden_eliminar",
                        "id", Request.Form["id"],
                        "satis", Request.Form["satis"],
                        "cum", Request.Form["cum"],
                        "habi", Request.Form["habi"],
                        "info", Request.Form["info"],
                        "observa", Request.Form["observa"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaInfoMensaje":
                    retorno = cx.Listar("paSP_OrdenMensaje_info_cargar",
                        "caso", Request.Form["caso"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "MensajeOrdenTemp_guardar":
                    retorno = cx.InsertarRetorna("paSP_MensajeOrdenTemp_guardar",
                        "mensaje", Request.Form["mensaje"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "MensajeOrdenServicio_Temp_listar":
                    retorno = cx.Listar("paSP_MensajeOrdenServicio_Temp_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "Mensaje_Orden_guardar":
                    retorno = cx.InsertarRetorna("paSP_Mensaje_Orden_guardar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "Orden_detalle":
                    retorno = cx.Listar("paSP_Orden_detalle",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "Orden_Archivo_detalle":
                    retorno = cx.Listar("paSP_Orden_Archivo_detalle",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "Orden_Archivo_msj_detalle":
                    retorno = cx.Listar("paSP_Orden_Archivo_msj_detalle",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "validarSede":
                    retorno = cx.Listar("paSP_Sedes_validar",
                        "codigo", Request.Form["codigo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarPrestadorServ":
                    retorno = cx.InsertarRetorna("paSP_PrestadorFinal_Guardar",
                       "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "EliminarInfoCompleGen":
                    retorno = cx.InsertarRetorna("paSP_InfoComplemetaria_eliminar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "EliminarInfoServGen":
                    retorno = cx.Listar("paSP_InfoServGeneral_eliminar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "gestionarAnalisis":
                    retorno = cx.Listar("paSP_Gestion_Analisis_InfoG_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "gestionarAnalisisCIE10":
                    retorno = cx.Listar("paSP_Gestion_Analisis_CIE10_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "gestionarAnalisisArchivo":
                    retorno = cx.Listar("paSP_Gestion_Analisis_Archivo_listar",
                        "id", Request.Form["id"],
                        "iden_arch", Request.Form["iden_arch"],
                        "tipo_arch", Request.Form["tipo_arch"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "gestionarAnalisisUAP":
                    retorno = cx.Listar("paSP_Gestion_Analisis_UAP_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "Paciente_detalle":
                    retorno = cx.Listar("paPRO_Paciente_detalle",
                        "documento", Request.Form["documento"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaSelectsAnali":
                    retorno = cx.Listar("paSP_SelectsAnali_carga",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteGeneralAdmin_generar":
                    retorno = cx.Listar("paSP_ReporteGeneralAdmin_generar",
                        "año", Request.Form["año"],
                        "mes", Request.Form["mes"],
                        "semestre", Request.Form["semestre"],
                        "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteCargadosXmes":
                    retorno = cx.Listar("paSP_PacientesCargadosXMes_listar",
                        "mes", Request.Form["mes"],
                        "anio", Request.Form["anio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteCargadosXmesGrupoEtario":
                    retorno = cx.Listar("paSP_PacientesCargadosXMesGrupoEtario_listar",
                        "mes", Request.Form["mes"],
                        "anio", Request.Form["anio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteCargadosXmesEstados":
                    retorno = cx.Listar("paSP_PacientesCargadosXMesEstados_listar",
                        "mes", Request.Form["mes"],
                        "anio", Request.Form["anio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteCargadosXmesDiagnostico":
                    retorno = cx.Listar("paSP_PacientesCargadosXMesDiagnostico_listar",
                        "mes", Request.Form["mes"],
                        "anio", Request.Form["anio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteCargadosXmesContacto":
                    retorno = cx.Listar("paSP_PacientesCargadosXMesContacto_listar",
                        "mes", Request.Form["mes"],
                        "anio", Request.Form["anio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteCanalReporteNacional":
                    retorno = cx.Listar("paSP_IndicadoresCanalReporteNacional_listar",
                        "mes", Request.Form["mes"],
                        "caso", Request.Form["caso"],
                        "anio", Request.Form["anio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteCanalReporteNacionalConsolidado":
                    retorno = cx.Listar("paSP_IndicadoresCanalReporteNacionalConsolidado_listar",
                        "mes", Request.Form["mes"],
                        "caso", Request.Form["caso"],
                        "anio", Request.Form["anio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteCanalReporteRegionales":
                    retorno = cx.Listar("paSP_IndicadoresCanalReporteRegional_listar",
                        "mes", Request.Form["mes"],
                        "anio", Request.Form["anio"],
                        "regional", Request.Form["regional"],
                        "caso", Request.Form["caso"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteCanalReporteRegionalesConsolidado":
                    retorno = cx.Listar("paSP_IndicadoresCanalReporteRegionalConsolidado_listar",
                        "mes", Request.Form["mes"],
                        "anio", Request.Form["anio"],
                        "regional", Request.Form["regional"],
                        "caso", Request.Form["caso"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteCumplimientoGestionNacional":
                    retorno = cx.Listar("paSP_IndicadoresCumplimientoGesNacional_listar",
                        "mes", Request.Form["mes"],
                        "caso", Request.Form["caso"],
                        "anio", Request.Form["anio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteCumplimientoGestionNacionalConsolidado":
                    retorno = cx.Listar("paSP_IndicadoresCumplimientoGesNacionalConsolidado_listar",
                        "mes", Request.Form["mes"],
                        "caso", Request.Form["caso"],
                        "anio", Request.Form["anio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteSeguimientoPeriodicoNacionalConsolidado":
                    retorno = cx.Listar("paSP_IndicadoresSeguimientoPeriodicoConsolidado_listar",
                        "mes", Request.Form["mes"],
                        "caso", Request.Form["caso"],
                        "anio", Request.Form["anio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteCumplimientoGestionRegional":
                    retorno = cx.Listar("paSP_IndicadoresCumplimientoGesRegional_listar",
                        "mes", Request.Form["mes"],
                        "caso", Request.Form["caso"],
                        "anio", Request.Form["anio"],
                        "regional", Request.Form["regional"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteCumplimientoGestionRegionalConsolidado":
                    retorno = cx.Listar("paSP_IndicadoresCumplimientoGesRegionalConsolidado_listar",
                        "mes", Request.Form["mes"],
                        "caso", Request.Form["caso"],
                        "anio", Request.Form["anio"],
                        "regional", Request.Form["regional"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "ReporteSeguimientoPeriodicoRegionalConsolidado":
                    retorno = cx.Listar("paSP_IndicadoresSeguimientoPeriodicoRegionalConsolidado_listar",
                        "mes", Request.Form["mes"],
                        "caso", Request.Form["caso"],
                        "anio", Request.Form["anio"],
                        "regional", Request.Form["regional"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteEficaciaNacional":
                    retorno = cx.Listar("paSP_IndicadoresEficaciaNacional_listar",
                        "mes", Request.Form["mes"],
                        "caso", Request.Form["caso"],
                        "anio", Request.Form["anio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteEficaciaNacionalConsolidado":
                    retorno = cx.Listar("paSP_IndicadoresEficaciaNacionalConsolidado_listar",
                        "mes", Request.Form["mes"],
                        "caso", Request.Form["caso"],
                        "anio", Request.Form["anio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteEficaciaRegional":
                    retorno = cx.Listar("paSP_IndicadoresEficaciaRegional_listar",
                        "mes", Request.Form["mes"],
                        "caso", Request.Form["caso"],
                        "anio", Request.Form["anio"],
                        "regional", Request.Form["regional"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "ReporteEficaciaRegionalConsolidado":
                    retorno = cx.Listar("paSP_IndicadoresEficaciaRegionalConsolidado_listar",
                        "mes", Request.Form["mes"],
                        "caso", Request.Form["caso"],
                        "anio", Request.Form["anio"],
                        "regional", Request.Form["regional"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "ReporteAdherenciaNacional":
                    retorno = cx.Listar("paSP_IndicadoresAdherenciaNacional2_listar",
                        "mes", Request.Form["mes"],
                        "caso", Request.Form["caso"],
                        "anio", Request.Form["anio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteAdherenciaNacionalConsolidado":
                    retorno = cx.Listar("paSP_IndicadoresAdherenciaNacional2Consolidado_listar",
                        "mes", Request.Form["mes"],
                        "caso", Request.Form["caso"],
                        "anio", Request.Form["anio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "ReporteAdherenciaRegional":
                    retorno = cx.Listar("paSP_IndicadoresAdherenciaRegional2_listar",
                        "mes", Request.Form["mes"],
                        "caso", Request.Form["caso"],
                        "anio", Request.Form["anio"],
                        "regional", Request.Form["regional"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteAdherenciaRegionalConsolidado":
                    retorno = cx.Listar("paSP_IndicadoresAdherenciaRegional2Consolidado_listar",
                        "mes", Request.Form["mes"],
                        "caso", Request.Form["caso"],
                        "anio", Request.Form["anio"],
                        "regional", Request.Form["regional"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteGeneralAdminConsolidado_generar":
                    retorno = cx.Listar("paSP_ReporteGeneralAdminConsolidado_generar",
                        "año", Request.Form["año"],
                        "mes", Request.Form["mes"],
                        "semestre", Request.Form["semestre"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteEtareoAdmin_generar":
                    retorno = cx.Listar("paSP_ReporteEtareoAdmin_generar",
                        "año", Request.Form["año"],
                        "mes", Request.Form["mes"],
                        "semestre", Request.Form["semestre"],
                        "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteEtareoAdminConsolidado_generar":
                    retorno = cx.Listar("paSP_ReporteEtareoAdminConsolidado_generar",
                        "año", Request.Form["año"],
                        "mes", Request.Form["mes"],
                        "semestre", Request.Form["semestre"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteCanalAdmin_generar":
                    retorno = cx.Listar("paSP_ReporteCanalAdmin_generar",
                        "año", Request.Form["año"],
                        "mes", Request.Form["mes"],
                        "semestre", Request.Form["semestre"],
                        "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteCanalAdmin_consolidado_generar":
                    retorno = cx.Listar("paSP_ReporteCanalAdmin_consolidado_generar",
                        "año", Request.Form["año"],
                        "mes", Request.Form["mes"],
                        "semestre", Request.Form["semestre"],
                        "region", Request.Form["region"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteClinicoAdmin_General":
                    retorno = cx.Listar("paSP_ReporteClinicoAdmin_General",
                        "año", Request.Form["año"],
                        "mes", Request.Form["mes"],
                        "region", Request.Form["region"],
                        "ciudad", Request.Form["ciudad"],
                        "categoriaCie", Request.Form["categoriaCie"],
                        "codigoCie", Request.Form["codigoCie"],
                        "diagnos", Request.Form["diagnos"],
                        "semestre", Request.Form["semestre"],
                        "tipo", Request.Form["tipo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "cargarUAP_Analisis_Ini":
                    retorno = cx.Listar("paSP_UAP_Anali_Ini_cargar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "prestadoresIndicadoresListar":
                    retorno = cx.Listar("paSP_reportePrestadoresIndicadores_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "prestadoresIndicadoresListar1":
                    retorno = cx.Listar("paSP_reportePrestadoresIndicadores1_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "prestadoresIndicadoresListar2":
                    retorno = cx.Listar("paSP_reportePrestadoresIndicadores2_listar",
                        "id", Request.Form["id"],
                        "filtroFlechas", Request.Form["filtroFlechas"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargarAten_Analisis_Ini":
                    retorno = cx.Listar("paSP_Aten_Anali_Ini_cargar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "cargarMedi_Analisis_Ini":
                    retorno = cx.Listar("paSP_Medi_Anali_Ini_cargar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "DevolucionLlamObs_listar":
                    retorno = cx.Listar("paSP_DevolucionLlamObs_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "EstadoGestion_cargar":
                    retorno = cx.Listar("paSP_EstadoGestion_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargarEspe_Medi_Analisis_Ini":
                    retorno = cx.Listar("paSP_Espe_Medi_Anali_Ini_cargar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "Select2XCat_cargar":
                    retorno = cx.Listar("paSP_Select2XCat_cargar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "RegionalDirecTemp_listar":
                    retorno = cx.Listar("paSP_RegionalDirecTemp_listar",
                        "regional", Request.Form["regional"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarInfoOsiTemp":
                    retorno = cx.InsertarRetorna("paSP_InfoOsiTemp_Guardar",
                        "id", Request.Form["id"],
                        "codigoOsi", Request.Form["codigoOsi"],
                        "cantidadOsi", Request.Form["cantidadOsi"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarInfoOsiTemp":
                    retorno = cx.Listar("paSP_InfoOsiTemp_Listar",
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "EliminarInformacionOsi":
                    retorno = cx.InsertarRetorna("paSP_InfoOsiTemp_Eliminar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listaCodigosOsiDetalle":
                    retorno = cx.Listar("paSP_CodigosOsi_Listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listaNumerosContactoPres":
                    retorno = cx.Listar("paSP_NumerosContactoPres_Listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaEspecialidades":
                    retorno = cx.Listar("paSP_AutoEspecialidades_Listar",
                        "idPrestador", Request.Form["idPrestador"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaEmailEspecialidades":
                    retorno = cx.Listar("paSP_AutocargaEmailEspecialidades_Listar",
                        "idPrestador", Request.Form["idPrestador"],
                        "idPrestadorEspecialidadSer", Request.Form["idPrestadorEspecialidadSer"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "borrarInfoOsiTemp":
                    retorno = cx.InsertarRetorna("paSP_InfoOsiTemp_Borrar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "enviosguardar_guardar":
                    retorno = cx.Listar("paSP_Enviosguardar_guardar_copia",
                        "observacion", Request.Form["observacion"],
                        "participantes", Request.Form["participantes"],
                        "idGloblaPrestador", Request.Form["idGloblaPrestador"],
                         "idTramiteGlobal", Request.Form["idTramiteGlobal"],
                         "globlaBanderaModulos", Request.Form["globlaBanderaModulos"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "validarCie10":
                    retorno = cx.Listar("paSP_Cie10_validar",
                        "codigo", Request.Form["codigo"],
                        "documento", Request.Form["documento"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaRespuestaSeguimiento":
                    retorno = cx.Listar("paSP_RespuestaSeguimiento_cargar");
                    Response.Write(retorno);
                    break;


                case "cargaRespuestaDevolucion":
                    retorno = cx.Listar("paSP_RespuestaDevolucion_cargar");
                    Response.Write(retorno);
                    break;

                ///////////////////////////////////////Procedimientos Antiguos Analisis/////////////////////////////////////

                case "borrarTramitesTemp_Antiguo":
                    retorno = cx.InsertarRetorna("paSP_AnalisisTemp_borrar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardaTramiteTempAntiguo":
                    retorno = cx.InsertarRetorna("paSP_AnalisisTem_Antiguo_guardar",
                       "docPaciente", Request.Form["docPaciente"],
                        "fechaR", Request.Form["fechaR"],
                        "fechaL", Request.Form["fechaL"],
                        "especialidad", Request.Form["especialidad"],
                        "codOSI", Request.Form["codOSI"],
                        "tramite", Request.Form["tramite"],
                        "requiereAUTO", Request.Form["requiereAUTO"],
                        "aprobacion", Request.Form["aprobacion"],
                        "numAUTO", Request.Form["numAUTO"],
                        "prestador", Request.Form["prestador"],
                        "tipo", Request.Form["tipo"],
                        "fechaOrden", Request.Form["fechaOrden"],
                        "vigencia", Request.Form["vigencia"],
                        "fechaVencimiento", Request.Form["fechaVencimiento"],
                        "nombreTramite", Request.Form["nombreTramite"],
                        "UAP", Request.Form["UAP"],
                        "nombreMedico", Request.Form["nombreMedico"],
                        "especialidadMedico", Request.Form["especialidadMedico"],
                        "prescripcion", Request.Form["prescripcion"],
                        "concentracion", Request.Form["concentracion"],
                        "periodicidad", Request.Form["periodicidad"],
                        "entregas", Request.Form["entregas"],
                        "institucion", Request.Form["institucion"],
                        "entidad", Request.Form["entidad"],
                        "tiempo", Request.Form["tiempo"],
                        "diagnostico", Request.Form["diagnostico"],
                        "relacion", Request.Form["relacion"],
                        "fechaProg", Request.Form["fechaProg"],
                        "ciclos", Request.Form["ciclos"],
                        "cantidad", Request.Form["cantidad"],
                        "codServicio", Request.Form["codServicio"],
                        "medicamentoNOPOS", Request.Form["medicamentoNOPOS"],
                        "historiaClinica", Request.Form["historiaClinica"],
                        "soporteNOPOS", Request.Form["soporteNOPOS"],
                        "codigoCie", Request.Form["codigoCie"],
                        "medicoOrdena", Request.Form["medicoOrdena"],
                        "especialidadTratante", Request.Form["especialidadTratante"],
                        "NumPagina", Request.Form["NumPagina"],
                        "MedicoTratante", Request.Form["MedicoTratante"],
                        "EspecMedTra", Request.Form["EspecMedTra"],
                         "anulacion", Request.Form["anulacion"],
                          "motivo_Anulacion", Request.Form["motivo_Anulacion"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarLlamadaPrestadores":
                    retorno = cx.InsertarRetorna("paSP_LlamadaPrestadores_guardar",
                        "idGlobalLlamada", Request.Form["idGlobalLlamada"],
                        "idTramiteLama", Request.Form["idTramiteLama"],
                        "globalPrestador", Request.Form["globalPrestador"],
                        "myselect", Request.Form["myselect"],
                        "nombreRespuesta", Request.Form["nombreRespuesta"],
                        "selectNoContactable", Request.Form["selectNoContactable"],
                        "nombreSelNoContactable", Request.Form["nombreSelNoContactable"],
                        "observaciongenerica", Request.Form["observaciongenerica"],
                        "selectMotivoNoHayAgenda", Request.Form["selectMotivoNoHayAgenda"],
                        "nombreselectMotivoNoHayAgenda", Request.Form["nombreselectMotivoNoHayAgenda"],
                        "aperturaAgenda", Request.Form["aperturaAgenda"],
                        "fechaAgenda", Request.Form["fechaAgenda"],
                        "horaAgenda", Request.Form["horaAgenda"],
                        "medico", Request.Form["medico"],
                        "observacionesRequiereAgenda", Request.Form["observacionesRequiereAgenda"],
                        "especialidad", Request.Form["especialidad"],
                        "contacto", Request.Form["contacto"],
                        "email", Request.Form["email"],
                        "especialidadnumero", Request.Form["especialidadnumero"],
                        "contactonumero", Request.Form["contactonumero"],
                        "telefono", Request.Form["telefono"],
                        "celular", Request.Form["celular"],
                        "selcasofortuito", Request.Form["selcasofortuito"],
                        "nombreselcasofortuito", Request.Form["nombreselcasofortuito"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarRegistrarRespuesta":
                    retorno = cx.InsertarRetorna("paSP_RegistrarRespuestaCorreo_guardar",
                        "globalIdCorreo", Request.Form["globalIdCorreo"],
                        "idTramiteGlobalRegistrar", Request.Form["idTramiteGlobalRegistrar"],
                        "idGloblaPrestadorRegistrar", Request.Form["idGloblaPrestadorRegistrar"],
                        "myselect", Request.Form["myselect"],
                        "nombreRespuesta", Request.Form["nombreRespuesta"],
                        "observaciongenerica", Request.Form["observaciongenerica"],
                        "fechaAgenda", Request.Form["fechaAgenda"],
                        "horaAgenda", Request.Form["horaAgenda"],
                        "medico", Request.Form["medico"],
                        "observacionesRequiereAgenda", Request.Form["observacionesRequiereAgenda"],
                        "especialidad", Request.Form["especialidad"],
                        "contacto", Request.Form["contacto"],
                        "email", Request.Form["email"],
                        "especialidadnumero", Request.Form["especialidadnumero"],
                        "contactonumero", Request.Form["contactonumero"],
                        "telefono", Request.Form["telefono"],
                        "celular", Request.Form["celular"],
                        "selcasofortuito", Request.Form["selcasofortuito"],
                        "nombreselcasofortuito", Request.Form["nombreselcasofortuito"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "borrarTramitesTempAntiguo":
                    retorno = cx.InsertarRetorna("paSP_TramitesTempAntiguo_borrar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarAnalisisGestionarTempAntiguo":
                    retorno = cx.Listar("paSP_AnalisisGestionarTempAntiguo_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardaAnalisisRechazoTempAntiguo":
                    retorno = cx.InsertarRetorna("paSP_AnalisisRechazoTempAntiguo_guardar",
                        "idtramite", Request.Form["idtramite"],
                        "rechazo", Request.Form["rechazo"],
                        "especialidad", Request.Form["especialidad"],
                        "motivo", Request.Form["motivo"],
                        "descripcion", Request.Form["descripcion"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "borrarAnalisisRechazoTempAntiguo":
                    retorno = cx.InsertarRetorna("paSP_AnalisisRechazo_temp_eliminar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardaAnalisisRechazoAntiguo":
                    retorno = cx.InsertarRetorna("paSP_AnalisisRechazoAntiguo_guardar",
                        "doc", Request.Form["doc"],
                        "id_archivo", Request.Form["id_archivo"],
                        "cantidad", Request.Form["cantidad"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardaAnalisisGestionAntiguo":
                    retorno = cx.InsertarRetorna("paSP_AnalisisGestionAntiguo_guardar",
                        "doc", Request.Form["doc"],
                        "id_archivo", Request.Form["id_archivo"],
                        "UAP", Request.Form["UAP"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "GuardaAnalisisGestionAntiguoV2":
                    retorno = cx.InsertarRetorna("paSP_AnalisisGestionAntiguo_guardarV2",
                        "doc", Request.Form["doc"],
                        "id_archivo", Request.Form["id_archivo"],
                        "UAP", Request.Form["UAP"],
                        "tipo", Request.Form["tipo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "NoContactablePeriodico_random":
                    retorno = cx.Listar("paSP_NoContactablePeriodico_random",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarInfoDiagnostico":
                    retorno = cx.InsertarRetorna("paSP_InfoDiagnostico_guardar",
                        "documento", Request.Form["documento"],
                        "diagnostico", Request.Form["diagnostico"],
                        "fecha", Request.Form["fecha"],
                        "observacion", Request.Form["observacion"],
                        "modulo", Request.Form["modulo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "PacienteEstadoSeguimiento_guardar":
                    retorno = cx.InsertarRetorna("paSP_PacienteEstadoSeguimiento_guardar",
                        "id", Request.Form["id"],
                        "estadoIni", Request.Form["estadoIni"],
                        "estadoFin", Request.Form["estadoFin"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "borrarArchivosSeleccionados":
                    retorno = cx.InsertarRetorna("paSP_borrarArchivosSeleccionados_borrar",
                        "codigo", Request.Form["codigo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "listarEstadisticaArchivos":
                    retorno = cx.Listar("paSP_Reporte_EstadisticasEspecialidad_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "especialidad", Request.Form["especialidad"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "EstadisticasLlamadas_Generar":
                    retorno = cx.Listar("paSP_EstadisticasLlamadas_Generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ListarCiudadesRegional":
                    retorno = cx.Listar("paSP_CiudadesRegional_listar",
                        "Regional", Request.Form["Regional"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "CargaRespuesSinCobertura":
                    retorno = cx.Listar("paSP_RespuestaSinCobertura_cargar");
                    Response.Write(retorno);
                    break;

                case "CargaMotivoSinCobertura":
                    retorno = cx.Listar("paSP_MotivoSinCobertura_cargar");
                    Response.Write(retorno);
                    break;

                case "CategoriaTipoCie10_listar":
                    retorno = cx.Listar("paSP_CategoriaTipoCie10_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "listaPacientesTodos":
                    retorno = cx.Listar("paSP_PacienteEstadosTotal_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "CiudadesXRegional_listar":
                    retorno = cx.Listar("paSP_CiudadesXRegional_listar",
                         "id", Request.Form["id"],
                         "ciudad", Request.Form["ciudad"],
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ListarArchivosPaciente":
                    retorno = cx.Listar("paSP_ArchivosPacienteEnviar_listar",
                         "globlaPaciente", Request.Form["globlaPaciente"],
                         "globlaCorreo2", Request.Form["globlaCorreo2"],
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ListarHistorialCorreos":
                    retorno = cx.Listar("paSP_HistorialCorreosDetalle_listar",
                         "globlaIdTramiteHistorial", Request.Form["globlaIdTramiteHistorial"],
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ListarDestinatariosCorreos":
                    retorno = cx.Listar("paSP_DestinatariosCorreosDetalle_listar",
                         "globalIdCorreoHistorial", Request.Form["globalIdCorreoHistorial"],
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ListarDocumentosCorreo":
                    retorno = cx.Listar("paSP_DocumentosCorreoDetalle_listar",
                         "globalIdCorreoHistorial", Request.Form["globalIdCorreoHistorial"],
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ListarRespuestaCorreo":
                    retorno = cx.Listar("paSP_RespuestaCorreoDetalle_listar",
                         "globalIdCorreoHistorial", Request.Form["globalIdCorreoHistorial"],
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarArchivosSeleccionadosPaciente":
                    retorno = cx.Listar("paSP_ArchivosSeleccionadosPaciente_listar",
                         "globlaCorreo2", Request.Form["globlaCorreo2"],
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "buscaDestinatariosCorreos":
                    retorno = cx.Listar("paSP_DestinatariosCorreos_listar",
                         "idCorreo", Request.Form["idCorreo"],
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "DestinatariosMenoresEdad":
                    retorno = cx.Listar("paSP_destinatariosMenoresEdad_listar",
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "CategoriaCie10XTipo_listar":
                    retorno = cx.Listar("paSP_CategoriaCie10XTipo_listar",
                         "codigo", Request.Form["codigo"],
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarEstados":
                    retorno = cx.Listar("paSP_ConteoEstados_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarEstadisticaArchivosGestion":
                    retorno = cx.Listar("paSP_Reporte_EstadisticasGestionArchivos_generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        //"especialidad", Request.Form["especialidad"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "conteoIndicadoresIniciales":
                    retorno = cx.Listar("paSP_Indicadores_iniciales",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "conteoIndicadoresInicialesv2":
                    retorno = cx.Listar("paSP_IndicadoresNuevo_iniciales",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "PacientesMasivoXsemana_listar":
                    retorno = cx.Listar("paSP_PacientesMasivoXsemana_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "PacientesIndividualXsemana_listar":
                    retorno = cx.Listar("paSP_PacientesIndividualXsemana_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "SeguimientoPeriodicoXsemana_listar":
                    retorno = cx.Listar("paSP_SeguimientoPeriodicoXsemana_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "SeguimientoPeriodicoXsemanaV2_listar":
                    retorno = cx.Listar("paSP_SeguimientoPeriodicoXsemanaNuevo_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "PacientesSinCobertura_indicador_inicio":
                    retorno = cx.Listar("paSP_Pacientes_sincobertura_indicador_inicio",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "PacientesFallecidos_indicador_inicio":
                    retorno = cx.Listar("paSP_Pacientes_fallecidos_indicador_inicio",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "PacientesXTrimestre_listar":
                    retorno = cx.Listar("paSP_PacientesXtrimestre_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "TramitesGestionadosXtrimestre_listar":
                    retorno = cx.Listar("paSP_PacientesActivosTotalesXedad_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "CUCTramitesGestionadosXtrimestre_listar":
                    retorno = cx.Listar("paCUC_PacientesActivosTotalesXedad_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;
                case "TramitesGestionadosXtrimestreV2_listar":
                    retorno = cx.Listar("paSP_PacientesActivosTotalesXedadNuevo_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "PacientesActivosConTramXedad_listar":
                    retorno = cx.Listar("paSP_PacientesActivosConTramXedad_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "PacientesActivosSinTramXedad_listar":
                    retorno = cx.Listar("paSP_PacientesActivosSinTramXedad_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "PacientesCerradosXTrimestre_listar":
                    retorno = cx.Listar("paSP_PacientesCerradosTotalesXedad_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "CUCPacientesCerradosXTrimestre_listar":
                    retorno = cx.Listar("paCUC_PacientesCerradosTotalesXedad_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "PacientesCerradosXTrimestreV2_listar":
                    retorno = cx.Listar("paSP_PacientesCerradosTotalesXedadNuevo_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarPacienteLite":
                    retorno = cx.Listar("paSP_PacientesGestionados_mesAnterior",
                        "documentoFil", Request.Form["documentoFil"],
                        "nombreFil", Request.Form["nombreFil"],
                        //"especialidad", Request.Form["especialidad"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarPacienteLiteSep":
                    retorno = cx.Listar("paSP_PacientesGestionados_mesAnteriorSep",
                        "documentoFil", Request.Form["documentoFil"],
                        "nombreFil", Request.Form["nombreFil"],
                        //"especialidad", Request.Form["especialidad"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarPacienteLiteMeses":
                    if (gDate == "-2" || Request.Form["semanaFil"] != "-1")
                    {

                        gDate = Request.Form["semanaFil"];
                    }

                    retorno = cx.Listar("paSP_PacientesGestionados_MesesSemanas",
                        "documentoFil", Request.Form["documentoFil"],
                        "nombreFil", Request.Form["nombreFil"],
                        "semanaFil", gDate,
                        "mes", Request.Form["mes"],
                        "anio", Request.Form["anio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    gDate = "-2";
                    break;


                case "listarPacientesGestionCitas_mesAnterior":
                    retorno = cx.Listar("paSP_PacientesCitasGestionadas_mesAnterior",
                        "documentoFil", Request.Form["documentoFil"],
                        "nombreFil", Request.Form["nombreFil"],
                        //"especialidad", Request.Form["especialidad"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarPacientesGestionCitas_mesAnteriorSep":
                    retorno = cx.Listar("paSP_PacientesCitasGestionadas_mesAnteriorSep",
                        "documentoFil", Request.Form["documentoFil"],
                        "nombreFil", Request.Form["nombreFil"],
                        //"especialidad", Request.Form["especialidad"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "PacientesActivosXedad_listar":
                    retorno = cx.Listar("paSP_PacientesActivosXedad_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "PacientesCerradosXedad_listar":
                    retorno = cx.Listar("paSP_PacientesCerradosXedad_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "EstadisticasLlamadasV2_Generar":
                    retorno = cx.Listar("paSP_EstadisticasLlamadasV5_Generar",
                        "fechaini", Request.Form["fechaini"],
                        "fechafin", Request.Form["fechafin"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;



                case "EstadisticasLlamadasV6_Generar":
                    retorno = cx.Listar("paSP_EstadisticasLlamadasV6_Generar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "EstadisticasSegPeri_Generar":
                    retorno = cx.Listar("paSP_EstadisticasSegPeri_GenerarV3",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "EstadisticasSegPeriXDias_Generar":
                    retorno = cx.Listar("paSP_EstadisticasSegPeriXDias_GenerarV2",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargarTipoArchivo":
                    retorno = cx.Listar("paSP_TipoArchivo_cargar");
                    Response.Write(retorno);
                    break;

                case "cargarTipoArchivo2":
                    retorno = cx.Listar("paSP_TipoArchivo2_cargar");
                    Response.Write(retorno);
                    break;

                case "cargarResultado":
                    retorno = cx.Listar("paSP_Resultado_cargar",
                        "tipo", Request.Form["tipo"]);
                    Response.Write(retorno);
                    break;

                case "borrarTempArchivo":
                    retorno = cx.InsertarRetorna("paSP_ArchivoTemp_borrar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "EliminarObservacionGeneral":
                    retorno = cx.InsertarRetorna("paSP_ObservacionGeneral_eliminar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "EliminarDocumentoGeneral":
                    retorno = cx.Listar("paSP_DocumentoGeneral_eliminar",
                        "id", Request.Form["id"],
                        "tipo_arch", Request.Form["tipo_arch"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "validarPaciente":
                    retorno = cx.Listar("paSP_PacientesCargueDoc_validar",
                        "documento", Request.Form["documento"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "validarCie10PacArch":
                    retorno = cx.Listar("paSP_Cie10PacArch_validar",
                        "codigo", Request.Form["codigo"],
                        "documento", Request.Form["documento"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "asignarPaciente":
                    retorno = cx.Listar("paSP_PacientesCargueDoc_Asignar",
                        "documento", Request.Form["documento"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarArchivoGeneral":
                    retorno = cx.Listar("paSP_ArchivoGeneral_Guardar",
                        "documento", Request.Form["documento"],
                        "tipoDoc", Request.Form["tipoDoc"],
                        "tipoArch", Request.Form["tipoArch"],
                        "cie10", Request.Form["cie10"],
                        "resultado", Request.Form["resultado"],
                        "TipoServicio", Request.Form["TipoServicio"],
                        "rechazo", Request.Form["rechazo"],
                        "motivo", Request.Form["motivo"],
                        "observacion", Request.Form["observacion"],
                         "cantidad", Request.Form["cantidad"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarFinalArchivo":
                    retorno = cx.Listar("paSP_ArchivoFinal_Guardar",
                         "id", Request.Form["id"],
                          "tipoArch", Request.Form["tipoArch"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardarObsArchivoGeneral":
                    retorno = cx.InsertarRetorna("paSP_ArchivoGeneObservaciones_Guardar",
                        "id", Request.Form["id"],
                        "tipoArch", Request.Form["tipoArch"],
                        "observacion", Request.Form["observacion"],
                        "modulo", Request.Form["modulo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ContadorOrdenes":
                    retorno = cx.Listar("paSP_OrdenesMedicas_Contar",
                        "id", Request.Form["id"],
                         "id_espec", Request.Form["id_espec"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ContadorResultado":
                    retorno = cx.Listar("paSP_ResultadosContar_Contar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "PermisosMenuInicio_validar":
                    retorno = cx.InsertarRetorna("paSP_PermisosMenuInicio_validar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "PermisosMenuInicio_validarV2":
                    retorno = cx.InsertarRetorna("paSP_PermisosMenuInicioNuevo_validar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardaArchivosEnviar":
                    retorno = cx.InsertarRetorna("paSP_guardaArchivosEnviar_Guardar",
                        "idDocumento", Request.Form["idDocumento"],
                        "idTipoArchivo", Request.Form["idTipoArchivo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "EliminarArchivosEnviar":
                    retorno = cx.InsertarRetorna("paSP_EliminarArchivosEnviar_Eliminar",
                        "idDocumento", Request.Form["idDocumento"],
                        "idTipoArchivo", Request.Form["idTipoArchivo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "Informe_segui_per_Generar":
                    retorno = cx.Listar("paSP_Informe_segui_per_Generar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "Informe_segui_per_Generar_2":
                    retorno = cx.Listar("paSP_Informe_segui_per_Generar_2",
                        "anio", Request.Form["anio"],
                        "mes", Request.Form["mes"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "SeguimientoPeriodicoXsemanaNo_listar":
                    retorno = cx.Listar("paSP_SeguimientoPeriodicoXsemanaNo_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "SeguimientoPeriodicoXsemanaNoV2_listar":
                    retorno = cx.Listar("paSP_SeguimientoPeriodicoXsemanaNoNuevo_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "cargaInfoRechazo":
                    retorno = cx.Listar("paSP_InfoRechazo_listar",
                       "documento", Request.Form["documento"],
                       "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaInfoRechazoTram":
                    retorno = cx.Listar("paSP_InfoRechazoTramDoc_listar",
                       "documento", Request.Form["documento"],
                       "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaInfoRechazoTramNoInfo":
                    retorno = cx.Listar("paSP_cargaInfoRechazoTramNoInfo_listar",
                        "documento", Request.Form["documento"],
                        "tipoArchivo", Request.Form["tipoArchivo"],
                        "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaLlamdaEstadoRechazo":
                    retorno = cx.Listar("paSP_LlamadaEstadosRechazo_cargar"
                       );
                    Response.Write(retorno);
                    break;

                case "GuardarLlamadaRechazo":
                    retorno = cx.InsertarRetorna("paSP_PacienteLlamarRechazo_guardar",
                         "id", Request.Form["id"],
                         "iden", Request.Form["iden"],
                         "respuesta", Request.Form["respuesta"],
                         "tipo_arch", Request.Form["tipo_arch"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cambioAutorizacionSP":
                    retorno = cx.InsertarRetorna("paSP_Tramites_cambioAutorizacion_guardar",
                        "id", Request.Form["id"],
                        "observacion", Request.Form["observacion"],
                        "motivo", Request.Form["motivo"],
                        "estado", Request.Form["estado"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarLlamadaRechazoV2":
                    retorno = cx.InsertarRetorna("paSP_PacienteLlamarRechazoV2_guardar",
                         "id", Request.Form["id"],
                         "iden", Request.Form["iden"],
                         "respuesta", Request.Form["respuesta"],
                         "tipo_arch", Request.Form["tipo_arch"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardaLlamadaFinal":
                    retorno = cx.InsertarRetorna("paSP_PacienteLlamarFinalV2_guardar",
                        "doc", Request.Form["doc"],
                        "tipo_arch", Request.Form["tipo_arch"],
                        "bandera", Request.Form["bandera"],
                        "respuesta", Request.Form["respuesta"],
                        "identidad", Request.Form["identidad"],
                        "observacion", Request.Form["observacion"],
                        "contesta_1 ", Request.Form["contesta_1 "],
                        "contesta_2 ", Request.Form["contesta_2 "],
                        "contesta_3 ", Request.Form["contesta_3 "],
                        "contesta_4 ", Request.Form["contesta_4 "],
                        "motivo_1 ", Request.Form["motivo_1 "],
                        "motivo_2 ", Request.Form["motivo_2 "],
                        "motivo_3 ", Request.Form["motivo_3 "],
                        "motivo_4 ", Request.Form["motivo_4 "],
                        "registro_1 ", Request.Form["registro_1 "],
                        "registro_2 ", Request.Form["registro_2 "],
                        "registro_3 ", Request.Form["registro_3 "],
                        "registro_4 ", Request.Form["registro_4 "],
                        "vigente_1 ", Request.Form["vigente_1 "],
                        "vigente_2 ", Request.Form["vigente_2 "],
                        "vigente_3 ", Request.Form["vigente_3 "],
                        "vigente_4 ", Request.Form["vigente_4 "],
                        "operador_cel1", Request.Form["operador_cel1"],
                        "operador_cel2", Request.Form["operador_cel2"],
                        "modulo", Request.Form["modulo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "paSP_PacienteLlamarFinalAlterno_guardar":
                    retorno = cx.InsertarRetorna("paSP_PacienteLlamarFinalAlterno_guardar",
                        "doc", Request.Form["doc"],
                        "tipo_arch", Request.Form["tipo_arch"],
                        "bandera", Request.Form["bandera"],
                        "respuesta", Request.Form["respuesta"],
                        "identidad", Request.Form["identidad"],
                        "observacion", Request.Form["observacion"],
                        "contesta_1 ", Request.Form["contesta_1 "],
                        "contesta_2 ", Request.Form["contesta_2 "],
                        "contesta_3 ", Request.Form["contesta_3 "],
                        "contesta_4 ", Request.Form["contesta_4 "],
                        "motivo_1 ", Request.Form["motivo_1 "],
                        "motivo_2 ", Request.Form["motivo_2 "],
                        "motivo_3 ", Request.Form["motivo_3 "],
                        "motivo_4 ", Request.Form["motivo_4 "],
                        "registro_1 ", Request.Form["registro_1 "],
                        "registro_2 ", Request.Form["registro_2 "],
                        "registro_3 ", Request.Form["registro_3 "],
                        "registro_4 ", Request.Form["registro_4 "],
                        "vigente_1 ", Request.Form["vigente_1 "],
                        "vigente_2 ", Request.Form["vigente_2 "],
                        "vigente_3 ", Request.Form["vigente_3 "],
                        "vigente_4 ", Request.Form["vigente_4 "],
                        "operador_cel1", Request.Form["operador_cel1"],
                        "operador_cel2", Request.Form["operador_cel2"],
                        "modulo", Request.Form["modulo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "contarRandomInformarArchivos":
                    retorno = cx.Listar("paSP_ArchivosInformarRandom_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "contarRandomInformarArchivosNC":
                    retorno = cx.Listar("paSP_ArchivosInformarNoContactable_random",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardarObsNoinformados":
                    retorno = cx.InsertarRetorna("paSP_ObservacionesNoInformados_Guardar",
                        "id", Request.Form["id"],
                        "tipoArch", Request.Form["tipoArch"],
                        "observacion", Request.Form["observacion"],
                        "modulo", Request.Form["modulo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "EliminarObsNoInformado":
                    retorno = cx.InsertarRetorna("paSP_ObservacionesNoInformados_eliminar",
                        "id", Request.Form["id"],
                        "tipoArch", Request.Form["tipoArch"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarNoInformados":
                    retorno = cx.InsertarRetorna("paSP_GuardarNoInformados_guardar",
                        "doc", Request.Form["doc"],
                        "iden", Request.Form["iden"],
                        "bandera", Request.Form["bandera"],
                        "tipo_arch", Request.Form["tipo_arch"],
                        "observacionInfo", Request.Form["observacionInfo"],
                        "modulo", Request.Form["modulo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaHistorialInfo":
                    retorno = cx.Listar("paSP_HistorialInfo_listar",
                       "documento", Request.Form["documento"],
                       "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargarNovedad":
                    retorno = cx.Listar("paSP_Novedad_Segui_Per_Audi_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardarNovedad":
                    retorno = cx.InsertarRetorna("paSP_Novedad_Segui_Per_Audi_guardar",
                         "nombre", Request.Form["nombre"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "guardarAuditoria":
                    retorno = cx.InsertarRetorna("paSP_Segui_Per_Auditoria_guardar",
                        "id", Request.Form["id"],
                         "respuesta", Request.Form["respuesta"],
                        //  "contesta_1", Request.Form["contesta_1"],
                        //"contesta_2", Request.Form["contesta_2"],
                        //"contesta_3", Request.Form["contesta_3"],
                        //"contesta_4", Request.Form["contesta_4"],
                        //"motivo_1", Request.Form["motivo_1"],
                        //"motivo_2", Request.Form["motivo_2"],
                        //"motivo_3", Request.Form["motivo_3"],
                        //"motivo_4", Request.Form["motivo_4"],
                        //"registro_1", Request.Form["registro_1"],
                        //"registro_2", Request.Form["registro_2"],
                        //"registro_3", Request.Form["registro_3"],
                        //"registro_4", Request.Form["registro_4"],
                        //"vigente_1", Request.Form["vigente_1"],
                        //"vigente_2", Request.Form["vigente_2"],
                        //"vigente_3", Request.Form["vigente_3"],
                        //"vigente_4", Request.Form["vigente_4"],
                          "observacion", Request.Form["observacion"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargarcucpaciente":
                    retorno = cx.Listar("paSP_cucpaciente_cargar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardarPacientePriori":
                    retorno = cx.Listar("paSP_SeguimientoPrioritarioPaciente_guardar",
                        "documento", Request.Form["documento"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargarSemanas":
                    retorno = cx.Listar("paSP_Semanas_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargarMeses":
                    retorno = cx.Listar("paSP_Meses_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargarAnios":
                    retorno = cx.Listar("paSP_Anios_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardaPacienteCUC":
                    retorno = cx.Listar("paSP_PacienteCUC_guardar",
                       "id", Request.Form["id"],
                        "tipodoc", Request.Form["tipodoc"],
                        "numidenti", Request.Form["numidenti"],
                        "primernombre", Request.Form["primernombre"],
                        "segundonombre", Request.Form["segundonombre"],
                        "primerapellido", Request.Form["primerapellido"],
                        "segundoapellido", Request.Form["segundoapellido"],
                        "fechana", Request.Form["fechana"],
                        "genero", Request.Form["genero"],
                        "ocupacion", Request.Form["ocupacion"],
                        "observaciong", Request.Form["observaciong"],
                        "ciudadresi", Request.Form["ciudadresi"],
                        "direccion", Request.Form["direccion"],
                        "codpostal", Request.Form["codpostal"],
                        "email", Request.Form["email"],
                        "telefono1", Request.Form["telefono1"],
                        "telefono2", Request.Form["telefono2"],
                        "celular1", Request.Form["celular1"],
                        "celular2", Request.Form["celular2"],
                        "plansanitas", Request.Form["plansanitas"],
                        "canalrepor", Request.Form["canalrepor"],
                        "checkmedicinapre", Request.Form["checkmedicinapre"],
                        "checkconocediag", Request.Form["checkconocediag"],
                        "checktel", Request.Form["checktel"],
                        "checkcel", Request.Form["checkcel"],
                        "checksms", Request.Form["checksms"],
                        "checkemail", Request.Form["checkemail"],
                        "checkmarcacion", Request.Form["checkmarcacion"],
                        "modulo", Request.Form["modulo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargarObsSeguiPer":
                    retorno = cx.Listar("paSP_ObservaPeriodico_listar",
                        "id", Request.Form["id"],
                        "modulo", Request.Form["modulo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargarObsContactoFin":
                    retorno = cx.Listar("paSP_ObservaPeriodico_listar",
                        "id", Request.Form["id"],
                        "modulo", Request.Form["modulo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargarObsContactoIni":
                    retorno = cx.Listar("paSP_ObservaPeriodico_listar",
                        "id", Request.Form["id"],
                        "modulo", Request.Form["modulo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargarObsSeguiPer_genericas":
                    retorno = cx.Listar("paSP_Observaciones_genericas_listar",
                        "id", Request.Form["id"],
                        "modulo", Request.Form["modulo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "verAuditoriaPaciente":
                    retorno = cx.Listar("paSP_AuditoriaPaciente_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "llamadaProgramacion":
                    retorno = cx.InsertarRetorna("paSP_llamadaProgramacion_guardar",
                         "id", Request.Form["id"],
                         "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "controlContactoPrestadores":
                    retorno = cx.InsertarRetorna("paSP_controlContactoPrestadores_guardar",
                        "idGlobalLlamada", Request.Form["idGlobalLlamada"],
                        "idTramiteLama", Request.Form["idTramiteLama"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "controlContactoFinalPrestadores":
                    retorno = cx.InsertarRetorna("paSP_controlContactoFinalPrestadores_guardar",
                        "idGlobalLlamada", Request.Form["idGlobalLlamada"],
                        "idTramiteLama", Request.Form["idTramiteLama"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarReporteIndicadorOct":
                    retorno = cx.Listar("paSP_ReporteIndicativoOct_listar",
                        "anio", Request.Form["anio"],
                        "mes", Request.Form["mes"],
                        "indicador", Request.Form["indicador"],
                        "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "Mes3_cargar":
                    retorno = cx.Listar("paSP_Mes3_cargar",
                         "anio", Request.Form["anio"]);
                    Response.Write(retorno);
                    break;


                case "listasFacturacion130":
                    retorno = cx.Listar("paSP_Facturacion130_listar",
                        "mes", Request.Form["mes"],
                        "anio", Request.Form["anio"],
                        "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaMesDevolucion":
                    retorno = cx.Listar("paSP_Mes4_cargar");
                    Response.Write(retorno);
                    break;

                case "contactoInicialFinal":
                    retorno = cx.Listar("paSP_contactoInicialFinal_conteo",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "AnioDevolucion_Cargar":
                    retorno = cx.Listar("paSP_AnioDevolucion_Cargar");
                    Response.Write(retorno);
                    break;

                case "listasInicialFinal":
                    retorno = cx.Listar("paSP_contactoInicialFinal_listas",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "actualizarIniInfoPaciente":
                    retorno = cx.InsertarRetorna("paSP_InicioInfoPac_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "actualizarIniInfoPaciPrograma":
                    retorno = cx.InsertarRetorna("paSP_InicioInfoPaciPrograma_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "actualizarIniInfoSeguiPer":
                    retorno = cx.InsertarRetorna("paSP_InicioInfoSeguiPer_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "actualizarIniInfoContactabilidad":
                    retorno = cx.InsertarRetorna("paSP_InicioInfoContactabilidad_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ListarIniInfoPaciente":
                    retorno = cx.Listar("paSP_InicioInfoPac_Listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ListarIniInfoPaciPrograma":
                    retorno = cx.Listar("paSP_InicioInfoPaciPrograma_Listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ListarIniInfoContactabilidad":
                    retorno = cx.Listar("paSP_InicioInfoContactabilidad_Listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                //////////////////////////////////////////////////////////////////////////////////////////////////
                //////////////////////////////////// INICIO V2 REGIONALES ////////////////////////////////////////
                //////////////////////////////////////////////////////////////////////////////////////////////////

                case "actualizarIniInfoPacienteV2":
                    retorno = cx.InsertarRetorna("paSP_InicioInfoPacNuevo_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "actualizarIniInfoPaciProgramaV2":
                    retorno = cx.InsertarRetorna("paSP_InicioInfoPaciProgramaModificado_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "actualizarIniInfoSeguiPerV2":
                    retorno = cx.InsertarRetorna("paSP_InicioInfoSeguiPerNuevo_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "actualizarIniInfoContactabilidadV2":
                    retorno = cx.InsertarRetorna("paSP_InicioInfoContactabilidadNuevo_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ListarIniInfoPacienteV2":
                    retorno = cx.Listar("paSP_InicioInfoPacNuevo_Listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ListarIniInfoPaciProgramaV2":
                    retorno = cx.Listar("paSP_InicioInfoPaciProgramaModificado_Listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ListarIniInfoContactabilidadV2":
                    retorno = cx.Listar("paSP_InicioInfoContactabilidadNuevo_Listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;



                case "Mes5_cargar":
                    retorno = cx.Listar("paSP_Mes5_cargar");
                    Response.Write(retorno);
                    break;

                case "facturaSep2017":

                    Thread.Sleep(3333);
                    //retorno = cx.Listar("paSP_Reporte_Estadisticas_NC_Generar",
                    //    "fechaini", Request.Form["fechaini"],
                    //    "fechafin", Request.Form["fechafin"],
                    //    "mes", Request.Form["mes"],
                    //    "cargue", Request.Form["cargue"],
                    //    "empresa", empresa,
                    //    "responsable", responsable);
                    Response.Write(1);
                    break;

                case "guardarUnidadBase":
                    retorno = cx.InsertarRetorna("paSP_UnidadBase_Guardar",
                        "id", Request.Form["id"],
                        "nombre_unidad", Request.Form["nombre_unidad"],
                        "tipoPresentacion", Request.Form["tipoPresentacion"],
                        "tipoViaAdministracion", Request.Form["tipoViaAdministracion"],
                        "tipoUnidadMedida", Request.Form["tipoUnidadMedida"],
                        "concentracion", Request.Form["concentracion"],
                        "responsable", responsable,
                        "empresa", empresa);
                    Response.Write(retorno);
                    break;


                case "guardarUnidadEmpaque":
                    retorno = cx.InsertarRetorna("paSP_UnidadEmpaque_Guardar",
                        "id", Request.Form["id"],
                        "nombreEmpaque", Request.Form["nombreEmpaque"],
                        "tipoUnidadBase", Request.Form["tipoUnidadBase"],
                        "equivalente", Request.Form["equivalente"],
                        "responsable", responsable,
                        "empresa", empresa);
                    Response.Write(retorno);
                    break;

                case "guardarMedicamento":
                    retorno = cx.Listar("paSP_Medicamentos_Guardar",
                        "id", Request.Form["id"],
                        "nombreMedicamento", Request.Form["nombreMedicamento"],
                        "responsable", responsable,
                        "empresa", empresa);
                    Response.Write(retorno);
                    break;

                case "guardarIndicaciones":
                    retorno = cx.InsertarRetorna("paSP_Indicaciones_Guardar",
                        "id", Request.Form["id"],
                        "indicaciones", Request.Form["indicaciones"],
                        "idGloblaMed", Request.Form["idGloblaMed"],
                        "responsable", responsable,
                        "empresa", empresa);
                    Response.Write(retorno);
                    break;

                case "borrarMedicamento":
                    retorno = cx.Listar("paSP_Medicamento_borrar",
                        "codigo", Request.Form["codigo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaUnidadBase":
                    retorno = cx.Listar("paSP_UnidadBase_cargar",
                    "id", Request.Form["id"],
                    "empresa", empresa,
                    "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "borrarUnidadBase":
                    retorno = cx.InsertarRetorna("paSP_UnidadBase_borrar",
                        "codigo", Request.Form["codigo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listaUnidaMedidaTemp":
                    retorno = cx.Listar("paSP_UnidadMedidaTemp_listar",
                    "id", Request.Form["id"],
                    "empresa", empresa,
                    "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listaUnidaEmpaqueTem":
                    retorno = cx.Listar("paSP_UnidadEmpaqueTemp_listar",
                    "id", Request.Form["id"],
                    "empresa", empresa,
                    "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listaIndicacionesTemp":
                    retorno = cx.Listar("paSP_IndicacionesTemp_listar",
                   "idGloblaMed", Request.Form["idGloblaMed"],
                    "empresa", empresa,
                    "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "borrarTemporales2":
                    retorno = cx.InsertarRetorna("paSP_BorrarTempo_borrar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaPresentacion":
                    retorno = cx.Listar("paSP_Presentacion_cargar",
                        "empresa", empresa);
                    Response.Write(retorno);
                    break;

                case "cargaViaAdministracion":
                    retorno = cx.Listar("paSP_ViaAdaministracion_cargar",
                        "empresa", empresa
                        );
                    Response.Write(retorno);
                    break;


                case "cargaUnidadMedida":
                    retorno = cx.Listar("paSP_UnidadMedida_cargar");
                    Response.Write(retorno);
                    break;

                case "guardarConteoTiempos":
                    retorno = cx.Listar("paSP_conteo_tiempos_Guardar",
                        "id", Request.Form["id"],
                        "modulo", Request.Form["modulo"],
                        "id_llamada", Request.Form["id_llamada"],
                        "checked", Request.Form["checked"],
                        "responsable", responsable,
                        "empresa", empresa);
                    Response.Write(retorno);
                    break;

                case "borrarUnidadEmpaque":
                    retorno = cx.InsertarRetorna("paMED_UNIDAD_EMPAQUE_borrar",
                        "codigo", Request.Form["codigo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "borrarIndicaciones":
                    retorno = cx.InsertarRetorna("paSP_Indicaciones_borrar",
                        "codigo", Request.Form["codigo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardaPresentacion":
                    retorno = cx.InsertarRetorna("paSP_Presentacion_Guardar",
                        "id", Request.Form["id"],
                        "nombre", Request.Form["nombre"],
                        "responsable", responsable,
                        "empresa", empresa);
                    Response.Write(retorno);
                    break;

                case "borrarPresentacion":
                    retorno = cx.InsertarRetorna("paSP_Presentacion_borrar",
                        "codigo", Request.Form["codigo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "guardarViaAdministracion":
                    retorno = cx.InsertarRetorna("paSP_ViaAdministracion_guardar",
                        "id", Request.Form["id"],
                        "nombre", Request.Form["nombre"],
                        "empresa", empresa,
                        "responsable", responsable
                        );
                    Response.Write(retorno);
                    break;

                case "borrarViaAdministracion":
                    retorno = cx.InsertarRetorna("paSP_ViaAdministracion_Eliminar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable
                        );
                    Response.Write(retorno);
                    break;

                case "cargaFrecuencia":
                    retorno = cx.Listar("paSP_Frecuencia_cargar");
                    Response.Write(retorno);
                    break;

                case "ValidaCantidadMedicamento":
                    retorno = cx.Listar("paSP_CantidadMedicamento_Validar",
                        "cantidad_concentracion", Request.Form["cantidad_concentracion"],
                        "frecuencia_dosis", Request.Form["frecuencia_dosis"],
                        "cantidad_frecuencia", Request.Form["cantidad_frecuencia"],
                        "tipo_tiempo_tratamiento", Request.Form["tipo_tiempo_tratamiento"],
                        "concentracion_medicamento", Request.Form["concentracion_medicamento"],
                        "cantidad_tiempo", Request.Form["cantidad_tiempo"],
                        "empresa", empresa);
                    Response.Write(retorno);
                    break;

                case "TramitesProgramarRandom":
                    retorno = cx.Listar("paSP_Tramite_random",
                        "estado", Request.Form["estado"],
                         "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "TramitesProgramarContactoFinalRandom":
                    retorno = cx.Listar("paSP_TramiteContactoFinal_random",
                        "estado", Request.Form["estado"],
                         "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaMotivoNoRealizacion":
                    retorno = cx.Listar("paSP_Motivo_No_Realizacion_cargar",
                          "menu", Request.Form["menu"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "cargarInfoTramite":
                    retorno = cx.Listar("paSP_InfoTramite_cargar",
                    "id", Request.Form["id"],
                    "empresa", empresa,
                    "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardaMotivoPara":
                    retorno = cx.InsertarRetorna("paSP_Motivo_No_Realizacion_guardar",
                        "motivo", Request.Form["motivo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardaMotivoNocontactoEfectivo":
                    retorno = cx.InsertarRetorna("paSP_Motivo_No_ContactoEfectivo_guardar",
                        "motivo", Request.Form["motivo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarBukeala1":
                    retorno = cx.InsertarRetorna("paSP_Bukeala1_guardar",
                        "fechacita", Request.Form["fechacita"],
                        "horacita", Request.Form["horacita"],
                        "medicocita", Request.Form["medicocita"],
                        "observacion", Request.Form["observacion"],
                        "idTramiteGlobal", Request.Form["idTramiteGlobal"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarBukeala2":
                    retorno = cx.InsertarRetorna("paSP_GuardarBukeala2_guardar",
                        "motivo", Request.Form["motivo"],
                        "nombreRespuesta", Request.Form["nombreRespuesta"],
                        "observacion", Request.Form["observacion"],
                        "idTramiteGlobal", Request.Form["idTramiteGlobal"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "GuardarNuevoMedico":
                    retorno = cx.InsertarRetorna("paSP_NuevoMedico_guardar",
                        "nombre", Request.Form["nombre"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "cargarEspecialidadesServicioAutocompletar":
                    retorno = cx.Listar("paSP_cargarEspecialidadesServicioAutocompletar_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargarEspecialidadesV2":
                    retorno = cx.Listar("paSP_EspecialidadesV2_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "cargarEspecialidadesTipoServicio":
                    retorno = cx.Listar("paSP_cargarEspecialidadesTipoServicio_cargar",
                        "especialidad", Request.Form["especialidad"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "listarDetalleRecepcion":
                    retorno = cx.Listar("paSP_DetalleRecepcion_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaMotivoRecep":
                    retorno = cx.Listar("paSP_MotivoRecepcion_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ContactoTipo_cargar":
                    retorno = cx.Listar("paSP_ContactoTipo_cargar",
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ContactoLlamadaPres_registrar":
                    retorno = cx.InsertarRetorna("paSP_ContactoLlamadaPres_registrar",
                        "id", Request.Form["id"],
                        "bnd", Request.Form["bnd"],
                        "idLlamada", Request.Form["idLlamada"],
                        "motivo", Request.Form["motivo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "GuardarOpcionLista":
                    retorno = cx.InsertarRetorna("paSP_OpcionListaChequeo_guardar",
                        "id", Request.Form["id"],
                         "id_estado", Request.Form["id_estado"],
                         "opcion", Request.Form["opcion"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "RegistrarOpcionLista":
                    retorno = cx.InsertarRetorna("paSP_RegistrarLista_guardar",
                       "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "tableroGestionListar":
                    retorno = cx.Listar("paSP_tableroGestion_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarOpcionesChequeo":
                    retorno = cx.Listar("paSP_listarOpcionesChequeo_listar",
                         "id_estado", Request.Form["id_estado"],
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "eliminarOpcionLista":
                    retorno = cx.InsertarRetorna("paSP_OpcionLista_eliminar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "PacientesXAntiguedadEtario_Listar":
                    retorno = cx.Listar("paSP_PacientesXAntiguedadEtario_Listar",
                        "bnd", Request.Form["bnd"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarpacienteAnalisis":
                    retorno = cx.InsertarRetorna("paSP_pacienteAnalisis_guardar",
                        "documento", Request.Form["documento"],
                         "cie10", Request.Form["cie10"],
                         "idListaChekeo", Request.Form["idListaChekeo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarpacienteAnalisisTemp":
                    retorno = cx.InsertarRetorna("paSP_pacienteAnalisisTemp_guardar",
                       "idListaChekeo", Request.Form["idListaChekeo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "borrarListaChequeoTemp":
                    retorno = cx.InsertarRetorna("paSP_ListaChequeo_Analisistemp_eliminar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "PacientesXAntiguedadDiagnostico_Listar":
                    retorno = cx.Listar("paSP_PacientesXAntiguedadDiagnostico_Listar",
                        "bnd", Request.Form["bnd"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargarProgramacionAccion":
                    retorno = cx.Listar("paSP_ProgramacionAccion_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargarGestionSeguimientoAccion":
                    retorno = cx.Listar("paSP_GestionSeguimientoAccion_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "GuardarProgramacionAccion":
                    retorno = cx.InsertarRetorna("paSP_ProgramacionAccion_Guardar",
                        "idGlobalTramiteNoAsignadas", Request.Form["idGlobalTramiteNoAsignadas"],
                        "myselectProgramacioAccion", Request.Form["myselectProgramacioAccion"],
                        "nombreRespuesta", Request.Form["nombreRespuesta"],
                        "prestador", Request.Form["prestador"],
                        "checkNuevoAutorizacion", Request.Form["checkNuevoAutorizacion"],
                        "obs", Request.Form["obs"],
                        "autorizacion", Request.Form["autorizacion"],
                        "prioridad", Request.Form["prioridad"],
                        "posibleFechaApertura", Request.Form["posibleFechaApertura"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardarGestorSeguimiento":
                    retorno = cx.InsertarRetorna("paSP_guardarGestorSeguimiento_Guardar",
                        "idGlobalTramiteGestorSeg", Request.Form["idGlobalTramiteGestorSeg"],
                         "idGlobalPrestadorGestorSeg", Request.Form["idGlobalPrestadorGestorSeg"],
                        "myselect", Request.Form["myselect"],
                        "nombreRespuesta", Request.Form["nombreRespuesta"],
                        "fechaAgenda", Request.Form["fechaAgenda"],
                        "horaAgenda", Request.Form["horaAgenda"],
                        "medico", Request.Form["medico"],
                        "observacionesRequierePreparacion", Request.Form["observacionesRequierePreparacion"],
                        "prestador", Request.Form["prestador"],
                        "ckekAutorizacion", Request.Form["ckekAutorizacion"],
                         "autorizacion", Request.Form["autorizacion"],
                        "obs", Request.Form["obs"],
                        "especialidad", Request.Form["especialidad"],
                         "posibleFechaApertura", Request.Form["posibleFechaApertura"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "cargarMotivoOrden":
                    retorno = cx.Listar("paSP_OrdenMotivo_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "guardarMotivo":
                    retorno = cx.Listar("paSP_MotivoOrden_guardar",
                        "motivo", Request.Form["motivo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;



                case "guardarMotivoBitacora":
                    retorno = cx.Listar("paSP_MotivoBitacora_guardar",
                        "motivo", Request.Form["motivo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "cargarMotivoBitacora":
                    retorno = cx.Listar("paSP_BitacoraMotivo_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "cargarCaso":
                    retorno = cx.Listar("paSP_CasoBitacora_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "bitacoraRol_detalle":
                    retorno = cx.Listar("paSP_bitacoraRol_detalle",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardarbitacoraRoles":
                    retorno = cx.InsertarRetorna("paSP_BitacoraRoles_guardar",
                       "motivo", Request.Form["motivo"],
                       "titulo", Request.Form["titulo"],
                       "descripcion", Request.Form["descripcion"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "eliminarTramSel":
                    retorno = cx.InsertarRetorna("paSP_TramSelecionados_eliminar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "pacienteInformarTra":
                    retorno = cx.InsertarRetorna("paSP_pacienteInformar_guardar",
                       "documento", Request.Form["documento"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargaTipoLlamada":
                    retorno = cx.Listar("paSP_TipoLlamada_cargar");
                    Response.Write(retorno);
                    break;

                case "cargaTipoLlamadaSanitas":
                    retorno = cx.Listar("paSP_TipoLlamadaSanitas_cargar");
                    Response.Write(retorno);
                    break;


                case "indicadorRegional":
                    retorno = cx.Listar("paSP_indicadorRegional_listar",
                        "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "indicadorRegionalV2":
                    retorno = cx.Listar("paSP_indicadorRegionalNuevo_listar2",
                        "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listarPronostico":
                    retorno = cx.Listar("paSP_PronosticoSeguiPer_generar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_sindxonco_contar":
                    retorno = cx.Listar("paSP_sindxonco_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_sindxonco_listar":
                    retorno = cx.Listar("paSP_sindxonco_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_Indicador_Clinico_Nacional_listar":
                    retorno = cx.Listar("paSP_Indicador_Clinico_Nacional_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_Indicador_Clinico_NacionalNivel1_listar":
                    retorno = cx.Listar("paSP_Indicador_Clinico_Nacional_Nivel1_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_Indicador_Clinico_NacionalNivel2_listar":
                    retorno = cx.Listar("paSP_Indicador_Clinico_Nacional_Nivel2_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_Indicador_Clinico_NacionalNivel3_listar":
                    retorno = cx.Listar("paSP_Indicador_Clinico_NacionalNivel3_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "capturarIps":

                    string mac = getMacAddress();
                    string myIpPublica = new WebClient().DownloadString(@"http://icanhazip.com").Trim();
                    retorno = cx.InsertarRetorna("paINI_capturaIP_Usuario",
                        "clave", Session["sesion_key"],
                        "responsable", responsable,
                        "empresa", empresa,
                        "pc2", localIP,
                        "pc", mac,
                        "ip", myIpPublica);
                    Response.Write(retorno);
                    break;

                case "ReporteCargadosXmes2":
                    retorno = cx.Listar("paSP_PacientesCargadosXMes2_listar",
                        //"mes", Request.Form["mes"],
                        //"anio", Request.Form["anio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "ReporteCargadosXmesGrupoEtario2":
                    retorno = cx.Listar("paSP_PacientesCargadosXMesGrupoEtario2_listar",
                        //"mes", Request.Form["mes"],
                        //"anio", Request.Form["anio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteCargadosXmesEstados2":
                    retorno = cx.Listar("paSP_PacientesCargadosXMesEstados2_listar",
                        //"mes", Request.Form["mes"],
                        //"anio", Request.Form["anio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteCargadosXmesDiagnostico2":
                    retorno = cx.Listar("paSP_PacientesCargadosXMesDiagnostico2_listar",
                        //"mes", Request.Form["mes"],
                        //"anio", Request.Form["anio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteCargadosXmesContacto2":
                    retorno = cx.Listar("paSP_PacientesCargadosXMesContacto2_listar",
                        //"mes", Request.Form["mes"],
                        //"anio", Request.Form["anio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "listasFacturacion130_2":
                    retorno = cx.Listar("paSP_Facturacion130_listar2",
                        //"mes", Request.Form["mes"],
                        //"anio", Request.Form["anio"],
                        "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "CapacidadInstalada_Generar":
                    retorno = cx.Listar("paSP_CapacidadInstalada_Generar",
                        "bnd", Request.Form["bnd"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "Fecha_validar":
                    retorno = cx.InsertarRetorna("paSP_Fecha_validar",
                        "fecha", Request.Form["fecha"]);
                    Response.Write(retorno);
                    break;

                case "paSP_ContactoInicialActual_listar":


                    retorno = cx.Listar("paSP_ContactoInicialActual_listar",
                        "documentoFil", Request.Form["documentoFil"],
                        "nombreFil", Request.Form["nombreFil"],
                        "semanaFil", gDate,
                        "mes", Request.Form["mes"],
                        "anio", Request.Form["anio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    //  gDate = "-2";
                    break;


                case "paSP_FechasContacto_listar":
                    retorno = cx.Listar("paSP_FechasContacto_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "paSP_itemscheckeo_listar":
                    retorno = cx.Listar("paSP_itemscheckeo_listar",
                        "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_itemscheckeo_guardar":
                    retorno = cx.InsertarRetorna("paSP_itemscheckeo_guardar",
                       "id", Request.Form["id"],
                       "doc", Request.Form["doc"],
                       "bandera", Request.Form["bandera"],
                        "respuesta", Request.Form["respuesta"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_Auditoria_Con_INI_FIN_guardar":
                    retorno = cx.InsertarRetorna("paSP_Auditoria_Con_INI_FIN_guardar",
                       "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_itemscheckeoAudi_listar":
                    retorno = cx.Listar("paSP_itemscheckeoAudi_listar",
                        "id", Request.Form["id"],
                        "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargarAreaLlamada":
                    retorno = cx.Listar("paSP_AreaLlamada_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_contactoFinalActual_listar":
                    retorno = cx.Listar("paSP_contactoFinalActual_listar",
                        "documentoFil", Request.Form["documentoFil"],
                        "nombreFil", Request.Form["nombreFil"],
                        "semanaFil", gDate,
                        "mes", Request.Form["mes"],
                        "anio", Request.Form["anio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    //  gDate = "-2";
                    break;
                case "listasFacturacion130_3":
                    retorno = cx.Listar("paSP_Facturacion130_listar3",
                         "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "PacientesCargadosSemana":
                    retorno = cx.Listar("paSP_PacientesCargadosSemana_listar",
                       "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                ////INICIO CUC///

                case "ListarIniInfoPacienteCUC":
                    retorno = cx.Listar("paCUC_InicioInfoPac_Listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ListarIniInfoPaciProgramaCUC":
                    retorno = cx.Listar("paCUC_InicioInfoPaciPrograma_Listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "conteoIndicadoresInicialesCUC":
                    retorno = cx.Listar("paCUC_Indicadores_iniciales",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "SeguimientoPeriodicoXsemanaNo_listarCUC":
                    retorno = cx.Listar("paCUC_SeguimientoPeriodicoXsemanaNo_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "SeguimientoPeriodicoXsemana_listarCUC":
                    retorno = cx.Listar("paCUC_SeguimientoPeriodicoXsemana_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "ReporteCargadosDiagnostico":
                    retorno = cx.Listar("paSP_PacientesCargadosDiagnostico3_listar",
                       "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "paSP_GestionProgChk_guardar":
                    retorno = cx.InsertarRetorna("paSP_GestionProgChk_guardar",
                         "id", Request.Form["id"],
                        "chk", Request.Form["chk"],
                        "respuesta", Request.Form["respuesta"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "AnalisisContactabilidad":
                    retorno = cx.Listar("paSP_AnalisisContactabilidad_listar",
                        "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardaCargaPQR2018":
                    retorno = cx.Listar("paSP_PQRCargue2018_guardar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "CargaMotivoAnulacion":
                    retorno = cx.Listar("paSP_MotivoAnulacion_cargar");
                    Response.Write(retorno);
                    break;

                case "paSP_TipoLlamAsig_cargar":
                    retorno = cx.Listar("paSP_TipoLlamAsig_cargar",
                        "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_DocuAlternoPrincipal_guardar":
                    retorno = cx.InsertarRetorna("paSP_DocuAlternoPrincipal_guardar",
                        "documento", Request.Form["documento"],
                        "servicio", Request.Form["servicio"],
                               "correo", Request.Form["correo"],
                        "ente", Request.Form["ente"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_analisis_alterno_guardar":
                    retorno = cx.InsertarRetorna("paSP_analisis_alterno_guardar",
                        "id", Request.Form["id"],
                       "respuesta", Request.Form["respuesta"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_Info_Persona_Alterna_listar":
                    retorno = cx.Listar("paSP_Info_Persona_Alterna_listar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "paSP_TransferirArchivo_guardar":
                    retorno = cx.InsertarRetorna("paSP_TransferirArchivo_guardar",
                        "id", Request.Form["id"],
                        "documento", Request.Form["documento"],
                       "archivo", Request.Form["archivo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cargarAprobacion":
                    retorno = cx.Listar("paCUC_Aprobacion_cargar",
                           "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "detalleTramitesInfoPaciente":
                    retorno = cx.Listar("paCUC_PacienteProgramar_detalle",
                        "idtramite", Request.Form["idtramite"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "programarSolicitudesCUC_guardar":
                    retorno = cx.InsertarRetorna("paCUC_programarSolicitudes_guardar2",
                        "id", Request.Form["id"],
                        "numautorizacion", Request.Form["numautorizacion"],
                        "prestador", Request.Form["prestador"],
                        "fechacita", Request.Form["fechacita"],
                        "horacita", Request.Form["horacita"],
                        "medicocita", Request.Form["medicocita"],
                        "tipo", Request.Form["tipo"],
                        "chk_informado", Request.Form["chk_informado"],
                        "aprobacion", Request.Form["aprobacion"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "guardarmotivoCUC":
                    retorno = cx.InsertarRetorna("paCUC_Motivo_Guardar",
                        "motivo", Request.Form["motivo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "cambioAutorizacionCUC":
                    retorno = cx.InsertarRetorna("paCUC_Tramites_cambioAutorizacion_guardar",
                        "id", Request.Form["id"],
                        "observacion", Request.Form["observacion"],
                        "motivo", Request.Form["motivo"],
                        "estado", Request.Form["estado"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_Info_Contacto_NUEVO_cargar":
                    retorno = cx.Listar("paSP_Info_Contacto_NUEVO_cargar",
                        "id", Request.Form["id"],
                           "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;



                case "paSP_InicioInfoContactabilidad2018_contar":
                    retorno = cx.InsertarRetorna("paSP_InicioInfoContactabilidad2018_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_AsignarActividades_guardar":
                    retorno = cx.InsertarRetorna("paSP_AsignarActividades_guardar",
                        "id", Request.Form["id"],
                        "usuario", Request.Form["usuario"],
                        "fecha_ini", Request.Form["fecha_ini"],
                        "fecha_fin ", Request.Form["fecha_fin"],
                        "meta", Request.Form["meta"],
                        "cat_llam", Request.Form["cat_llam"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "InfoContacto_cargar":
                    retorno = cx.Listar("paSP_InfoContacto_cargar2",
                        "id", Request.Form["id"],
                           "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;



                case "paSP_InicioInfoContactabilidad2018_Listar":
                    retorno = cx.Listar("paSP_InicioInfoContactabilidad2018_Listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_UsuariosOperaciones_validar":
                    retorno = cx.Listar("paSP_UsuariosOperaciones_validar",
                        "usuario", Request.Form["usuario"],
                        "bnd", Request.Form["bnd"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_CargaOperacional_listar":
                    retorno = cx.Listar("paSP_CargaOperacional_listar",
                        "bnd", Request.Form["bnd"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_ActividadesAsignadasHistorial_guardar":
                    retorno = cx.InsertarRetorna("paSP_ActividadesAsignadasHistorial_guardar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_Tipo_Llamada_Prio_cargar":
                    retorno = cx.Listar("paSP_Tipo_Llamada_Prio_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_HistorialAdminLlamadasConsolidado_listar":
                    retorno = cx.Listar("paSP_HistorialAdminLlamadasConsolidado_listar",
                        "documento", Request.Form["documento"],
                        "cat", Request.Form["cat"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_HistorialAdminLlamadas_listar":
                    retorno = cx.Listar("paSP_HistorialAdminLlamadas_listar",
                        "documento", Request.Form["documento"],
                        "cat", Request.Form["cat"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_HistorAdminGene_Tramites_listar":
                    retorno = cx.Listar("paSP_HistorAdminGene_Tramites_listar",
                        "docu", Request.Form["docu"],
                        "fecha_ini", Request.Form["fecha_ini"],
                        "fecha_fin", Request.Form["fecha_fin"],
                        "est_ini", Request.Form["est_ini"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_HistoriaAdmin_Tramites_listar":
                    retorno = cx.Listar("paSP_HistoriaAdmin_Tramites_listar",
                        "docu", Request.Form["docu"],
                        "fecha_ini", Request.Form["fecha_ini"],
                        "fecha_fin", Request.Form["fecha_fin"],
                        "est_ini", Request.Form["est_ini"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_InicioInfoPaciNoContac_listar":
                    retorno = cx.Listar("paSP_InicioInfoPaciNoContac_listar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_InicioInfoPaciNoContac_contar":
                    retorno = cx.Listar("paSP_InicioInfoPaciNoContac_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_EstadisticaOperacionMensual_listar":
                    retorno = cx.Listar("paSP_EstadisticaOperacionMensual_listar",
                         "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_EstadisticaOperacionesMensual2_listar":
                    retorno = cx.Listar("paSP_EstadisticaOperacionesMensual2_listar",
                         "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "paSP_EstadisticaCargueAlterno_listar":
                    retorno = cx.Listar("paSP_EstadisticaCargueAlterno_listar",
                         "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "paSP_Cantidad_Indicador_Alterno_guardar":
                    retorno = cx.InsertarRetorna("paSP_Cantidad_Indicador_Alterno_guardar",
                        "fecha", Request.Form["fecha"],
                        "correo", Request.Form["correo"],
                        "cantidad", Request.Form["cantidad"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_correo_alterno_cargar":
                    retorno = cx.Listar("paSP_correo_alterno_cargar",
                        // "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_SeguiPerNoEfectivosRandom_contar":
                    //Thread.Sleep(3333);
                    retorno = cx.Listar("paSP_SeguiPerNoEfectivosRandom_contar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_TipoTelefono_cargar":
                    //Thread.Sleep(3333);
                    retorno = cx.Listar("paSP_TipoTelefono_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "paSP_numeros_telefono_guardar":
                    retorno = cx.InsertarRetorna("paSP_numeros_telefono_guardar",
                        "id", Request.Form["id"],
                        "numero", Request.Form["numero"],
                        "tipo", Request.Form["tipo"],
                        "parentesco", Request.Form["parentesco"],
                        "nombre", Request.Form["nombre"],
                        "correo", Request.Form["correo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_numeros_telefono_listar":
                    //Thread.Sleep(3333);
                    retorno = cx.Listar("paSP_numeros_telefono_listar",
                        "id", Request.Form["id"],
                        "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "paSP_numeros_telefono_eliminar":
                    retorno = cx.InsertarRetorna("paSP_numeros_telefono_eliminar",
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_Paciente_Pest1_guardar":
                    retorno = cx.Listar("paSP_Paciente_Pest1_guardar",
                       "id", Request.Form["id"],
                        "tipodoc", Request.Form["tipodoc"],
                        "numidenti", Request.Form["numidenti"],
                        "primernombre", Request.Form["primernombre"],
                        "segundonombre", Request.Form["segundonombre"],
                        "primerapellido", Request.Form["primerapellido"],
                        "segundoapellido", Request.Form["segundoapellido"],
                        "fechana", Request.Form["fechana"],
                        "genero", Request.Form["genero"],
                        "ciudadresi", Request.Form["ciudadresi"],
                        "direccion", Request.Form["direccion"],
                        "email", Request.Form["email"],

                        "canalrepor", Request.Form["canalrepor"],
                        "checkconocediag", Request.Form["checkconocediag"],
                        "checkmedicinapre", Request.Form["checkmedicinapre"],
                        "checkmarcacion", Request.Form["checkmarcacion"],

                        "modulo", Request.Form["modulo"],

                         "ctele", Request.Form["ctele"],
                        "ccel", Request.Form["ccel"],
                        "csms", Request.Form["csms"],
                        "ccorreo", Request.Form["ccorreo"],
                        "direccion_envio", Request.Form["direccion_envio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_Paciente_Pest1_masivo_guardar":
                    retorno = cx.Listar("paSP_Paciente_Pest1_masivo_guardar",
                       "id", Request.Form["id"],
                        "tipodoc", Request.Form["tipodoc"],
                        "numidenti", Request.Form["numidenti"],
                        "primernombre", Request.Form["primernombre"],
                        "segundonombre", Request.Form["segundonombre"],
                        "primerapellido", Request.Form["primerapellido"],
                        "segundoapellido", Request.Form["segundoapellido"],
                        "fechana", Request.Form["fechana"],
                        "genero", Request.Form["genero"],
                        "ciudadresi", Request.Form["ciudadresi"],
                        "direccion", Request.Form["direccion"],
                        "email", Request.Form["email"],

                        "canalrepor", Request.Form["canalrepor"],
                        "checkconocediag", Request.Form["checkconocediag"],
                        "checkmedicinapre", Request.Form["checkmedicinapre"],
                        "checkmarcacion", Request.Form["checkmarcacion"],

                        "modulo", Request.Form["modulo"],

                         "ctele", Request.Form["ctele"],
                        "ccel", Request.Form["ccel"],
                        "csms", Request.Form["csms"],
                        "ccorreo", Request.Form["ccorreo"],
                        "cargue", Request.Form["cargue"],
                        "direccion_envio", Request.Form["direccion_envio"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "paSP_Paciente_Actualizar_guardar":
                    retorno = cx.Listar("paSP_Paciente_Actualizar_guardar",
                       "id", Request.Form["id"],
                        "ciudadresi", Request.Form["ciudadresi"],
                        "direccion", Request.Form["direccion"],
                        "email", Request.Form["email"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "paSP_EstadisticaCargue_listar":
                    retorno = cx.Listar("paSP_EstadisticaCargue_listar",
                         "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "paSP_Estadistica_Contacto_Ini_Fin_generar":
                    retorno = cx.Listar("paSP_Estadistica_Contacto_Ini_Fin_generar",
                         "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_Estadistica_Contacto_Inicial_generar":
                    retorno = cx.Listar("paSP_Estadistica_Contacto_Inicial_generar",
                         "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_Reporte_Cargue_paciente_generar":
                    retorno = cx.Listar("paSP_Reporte_Cargue_paciente_generar",
                         "anio", Request.Form["anio"],
                         "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "paSP_Reporte_Cargue_correo_generar":
                    retorno = cx.Listar("paSP_Reporte_Cargue_correo_generar",
                         "anio", Request.Form["anio"],
                         "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_Reporte_Contacto_Ini_Fin_Generar":
                    retorno = cx.Listar("paSP_Reporte_Contacto_Ini_Fin_Generar",
                         "anio", Request.Form["anio"],
                         "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "AsignarVarSesionAnio":
                    var anio = Request.Form["anio"];
                    if (!(anio.Equals("")))
                    {
                        Session["Anio_sesion"] = anio;
                    }
                    break;

                case "paSP_ReportePDF_Contacto_Ini_Fin_listar":
                    retorno = cx.Listar("paSP_ReportePDF_Contacto_Ini_Fin_listar",
                         "anio", Request.Form["anio"],
                         "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_Reporte_Cargue_paciente2_generar":
                    retorno = cx.Listar("paSP_Reporte_Cargue_paciente2_generar",
                         "anio", Request.Form["anio"],
                         "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_Reporte_Cargue_paciente3_generar":
                    retorno = cx.Listar("paSP_Reporte_Cargue_paciente3_generar",
                         "anio", Request.Form["anio"],
                         "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_Reporte_Cargue_paciente4_generar":
                    retorno = cx.Listar("paSP_Reporte_Cargue_paciente4_generar",
                        //"anio", Request.Form["anio"],
                        //"bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_info_inicial_ACT_IN_cargar":
                    retorno = cx.Listar("paSP_info_inicial_ACT_IN_cargar",
                         "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_Info_Registro_Pac_listar":
                    retorno = cx.Listar("paSP_Info_Registro_Pac_listar",
                         "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_PacienteLlamar_guardar3":
                    retorno = cx.InsertarRetorna("paSP_PacienteLlamar_guardar3",
                         "id", Request.Form["id"],
                        "respuesta", Request.Form["respuesta"],
                        "observacion", Request.Form["observacion"],
                        "intentos", Request.Form["intentos"],
                        "intentos_id", Request.Form["intentos_id"],
                        "vigente", Request.Form["vigente"],
                        "vigente_id", Request.Form["vigente_id"],
                        "contacto", Request.Form["contacto"],
                        "contacto_id", Request.Form["contacto_id"],
                        "motivoNV", Request.Form["motivoNV"],
                        "motivoNV_id", Request.Form["motivoNV_id"],
                        "operador", Request.Form["operador"],
                        "operador_id", Request.Form["operador_id"],
                        "nivelPaciente", Request.Form["nivelPaciente"],
                        "cantidad", Request.Form["cantidad"],
                        "tramitesi", Request.Form["tramitesi"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_Info_Paciente_validar":
                    retorno = cx.Listar("paSP_Info_Paciente_validar",
                         "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_radicar_elementos_cargar":
                    retorno = cx.Listar("paSP_radicar_elementos_cargar",
                         "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_archivo_radicar_temp_guardar":
                    retorno = cx.InsertarRetorna("paSP_archivo_radicar_temp_guardar",
                        "tipoSoli", Request.Form["tipoSoli"],
                        "emisor", Request.Form["emisor"],
                        "receptor", Request.Form["receptor"],
                        "tipoArch", Request.Form["tipoArch"],
                        "canTrami", Request.Form["canTrami"],
                        "solicitud", Request.Form["solicitud"],
                        "petiTrami", Request.Form["petiTrami"],
                        "motQueja", Request.Form["motQueja"],
                        "desQueja", Request.Form["desQueja"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_archivo_radicar_temp_borrar":
                    retorno = cx.InsertarRetorna("paSP_archivo_radicar_temp_borrar",
                         "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_archivo_radicar_temp_listar":
                    retorno = cx.Listar("paSP_archivo_radicar_temp_listar",
                         "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "paSP_archivo_radicar_full_guardar":
                    retorno = cx.Listar("paSP_archivo_radicar_full_guardar",
                         "documento", Request.Form["documento"],
                         "tipodoc", Request.Form["tipodoc"],
                         "nombre", Request.Form["nombre"],
                         "correo", Request.Form["correo"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "PacienteTipoFiltroAnalisisCUC_cargar":
                    retorno = cx.Listar("paSP_PacienteTipoFiltroAnalisisCUC_cargar",
                         "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_detalle_transferir_listar":
                    retorno = cx.Listar("paSP_detalle_transferir_listar",
                         "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


                case "paSP_carpeta_cargar":
                    retorno = cx.Listar("paSP_carpeta_cargar",
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_subcarpeta_cargar":
                    retorno = cx.Listar("paSP_subcarpeta_cargar",
                         "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_transferir_solicitud_guardar":
                    retorno = cx.InsertarRetorna("paSP_transferir_solicitud_guardar",
                         "id", Request.Form["id"],
                         "respuesta", Request.Form["respuesta"],
                         "subcarpeta", Request.Form["subcarpeta"],
                         "motivo", Request.Form["motivo"],
                         "area", Request.Form["area"],
                         "respon", Request.Form["respon"],
                         "priori", Request.Form["priori"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_llamada_reprogramar_guardar":
                    retorno = cx.InsertarRetorna("paSP_llamada_reprogramar_guardar",
                         "id", Request.Form["id"],
                         "fecha", Request.Form["fecha"],
                         "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_ReporteCaguePacXReg_listar":
                    retorno = cx.Listar("paSP_ReporteCaguePacXReg_listar",
                        "anio", Request.Form["anio"],
                        "mes", Request.Form["mes"],
                        "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_correo_alterno_enviar":

                    string fechac = Request.Form["fecha"];
                    string espec = Request.Form["espe"];
                    string correoc = Request.Form["correo"];
                    string pacientec = Request.Form["paciente"];

                    if (correoc != "")
                    {
                        informarbuzonnolegible(fechac, espec, correoc, pacientec);
                        Response.Write("1");
                    }
                    else
                    {
                        Response.Write(2);

                    }
                    //   Response.Write("1");

                    break;
                //case "paSP_infocargapqr_listar":
                //    retorno = cx.Listar("paSP_infocargapqr_listar",
                //         "id", Request.Form["id"],
                //        "empresa", empresa,
                //        "responsable", responsable);
                //    Response.Write(retorno);
                //    break;..

                case "paSP_EstadisticaRecepcionCorreos_Generar":
                    retorno = cx.Listar("paSP_EstadisticaRecepcionCorreos_Generar",
                         "bandera", Request.Form["bandera"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paSP_Paciente_Existe_validar":
                    retorno = cx.Listar("paSP_Paciente_Existe_validar",
                         "documento", Request.Form["documento"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;



                case "ListarSeguimiento":
                    retorno = cx.Listar("paPRO_Seguimiento_listar",
                       "documento", Request.Form["documento"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paCO_Factura_guardar":
                    retorno = cx.InsertarRetorna("paCO_Factura_guardar",
                        "id", Request.Form["id"],
                        "mesa", Request.Form["mesa"],
                        "clienteDoc", Request.Form["clienteDoc"],
                        "clienteNombre", Request.Form["clienteNombre"],
                        "clienteApell", Request.Form["clienteApell"],
                        "clienteApell2", Request.Form["clienteApell2"],
                        "camareroDoc", Request.Form["camareroDoc"],
                        "camareroNombre", Request.Form["camareroNombre"],
                        "camareroApell", Request.Form["camareroApell"],
                        "camareroApell2", Request.Form["camareroApell2"]);
                    Response.Write(retorno);
                    break;

                case "paCO_PlatosTemp_listar":
                    retorno = cx.Listar("paCO_PlatosTemp_listar",
                       "id", Request.Form["id"],
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "paCO_Mesa_cargar":
                    retorno = cx.Listar("paCO_Mesa_cargar");
                    Response.Write(retorno);
                    break;

                case "paCO_PlatoTemp_guardar":
                    retorno = cx.InsertarRetorna("paCO_PlatoTemp_guardar",
                        "precio", Request.Form["precio"],
                        "platos", Request.Form["platos"],
                         "responsable", responsable);
                    Response.Write(retorno);
                    break;


                ////////ENVÍO DE CORREOS CON ADJUNTOS////////


                case "enviarCorreosAdjuntos":
                    string cadena2 = "";
                    string cadena_global2 = "";
                    ///globales nuevas
                    string cadena1 = "";
                    string cadena1_global = "";

                    ////globales paciente
                    string cadena3 = "";
                    string cadena3_global = "";


                    ////globales parentesco
                    string cadena4 = "";
                    string cadena4_global = "";

                    ////globales tramites
                    string cadena5 = "";
                    string cadena5_global = "";

                    string ctl = "";
                    List<string> mails = new List<string>();
                    ///lista para los adjuntos
                    List<string> docs = new List<string>();
                    ///lista para paciente
                    List<string> pac = new List<string>();
                    ///lista para parentesco
                    List<string> paren = new List<string>();
                    ///lista para tramites
                    List<string> tram = new List<string>();

                    //  string idActa = Request.Form["idActa"].ToString();
                    retorno = cx.Listar("paSP_CorreosEnvio_cargar", "id", Request.Form["id"], "empresa", empresa, "responsable", responsable);
                    retorno1 = cx.Listar("paSP_AdjuntosEnvio_cargar", "id", Request.Form["id"], "empresa", empresa, "responsable", responsable);
                    retorno2 = cx.Listar("paSP_InfoPacienteEnvio_cargar", "id", Request.Form["id"], "empresa", empresa, "responsable", responsable);
                    retorno3 = cx.Listar("paSP_InfoParentescoEnvio_cargar", "id", Request.Form["id"], "empresa", empresa, "responsable", responsable);
                    retorno4 = cx.Listar("paSP_InfoTramiteEnvio_cargar", "id", Request.Form["id"], "empresa", empresa, "responsable", responsable);
                    cadena2 = retorno.Substring(retorno.IndexOf(":[") + 2);
                    cadena_global2 = cadena2.Replace("]}", "").Replace("'", "");

                    //nuevas cadenas

                    cadena1 = retorno1.Substring(retorno1.IndexOf(":[") + 2);
                    cadena1_global = cadena1.Replace("]}", "").Replace("'", "").Replace(" ", "");


                    // cadena paciente
                    cadena3 = retorno2.Substring(retorno2.IndexOf(":[") + 2);
                    cadena3_global = cadena3.Replace("]}", "").Replace("'", "");


                    // cadena parentesco
                    cadena4 = retorno3.Substring(retorno3.IndexOf(":[") + 2);
                    cadena4_global = cadena4.Replace("]}", "").Replace("'", "");

                    // cadena parentesco
                    cadena5 = retorno4.Substring(retorno4.IndexOf(":[") + 2);
                    cadena5_global = cadena5.Replace("]}", "").Replace("'", "");



                    mails = cadena_global2.Split(',').ToList<string>();


                    // Armar la lista de adjuntos
                    docs = cadena1_global.Split(',').ToList<string>();


                    // Armar la lista de paciente info
                    pac = cadena3_global.Split(',').ToList<string>();


                    // Armar la lista de parentesco info
                    paren = cadena4_global.Split(',').ToList<string>();

                    // Armar la lista de parentesco info
                    tram = cadena5_global.Split(',').ToList<string>();


                    for (int i = 0; i < mails.Count; i += 1)
                    {
                        string mail = mails[i].ToString();
                        try
                        {
                            //if (IsValidEmail(mail))
                            //{
                            enviarCorreoAdjunto(docs, mail, pac, usuario, paren, tram);
                            // }
                            // else { 

                            // }
                            ctl = "1";
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                            ctl = "0";
                        }
                    }
                    cx.Desconectar();
                    Response.Write("{'msj':" + ctl + "}");
                    break;

                ///////////////// ////////ENVÍO DE CORREOS RECORDAR////////

                case "enviarCorreosRecordar":

                    string cadenas = "";
                    string cadenas_global = "";

                    ///globales nuevas
                    //string cadenas1 = "";
                    //string cadenas1_global = "";

                    //////globales paciente
                    //string cadenas3 = "";
                    //string cadenas3_global = "";

                    ////globales parentesco


                    //List<string> mails = new List<string>();

                    //string documento = "";

                    //string nombre = "";

                    //string mailPac = "";

                    //string bandera = "";

                    //Int16 bnd = 0;

                    //List<string> documento = new List<string>();
                    //List<string> nombre = new List<string>();
                    //List<string> mailPac = new List<string>();
                    //List<string> bandera = new List<string>();

                    string documento = "";
                    string nombre = "";
                    string mailPac = "";
                    string bandera = "";

                    string[] informacion; // Declaración del array
                    informacion = new string[4]; // Instanciación del array

                    //  string idActa = Request.Form["idActa"].ToString();
                    retorno = cx.Listar("paSP_PacienteRecordarEnvioCorreo_listar", "documento", Request.Form["documento"], "empresa", empresa, "responsable", responsable);

                    cadenas = retorno.Substring(retorno.IndexOf(":[") + 2);
                    cadenas_global = cadenas.Replace("]}", "").Replace("'", "");

                    informacion = cadenas_global.Split(',');


                    documento = informacion[0];
                    // Armar la lista de paciente info
                    nombre = informacion[1];
                    // Armar la lista de parentesco info
                    mailPac = informacion[2];

                    bandera = informacion[3];





                    //        //if (IsValidEmail(mail))
                    //        //{
                    enviarCorreoAdjuntoRecordar(documento, nombre, mailPac, bandera);


                    Response.Write(retorno);

                    cx.Desconectar();
                    break;


                ////////ENVÍO DE CORREOS INFORMAR////////


                case "enviarCorreosInformar":
                    string cade = "";
                    string cade_global = "";
                    ///globales nuevas
                    string cade1 = "";
                    string cade1_global = "";





                    string ctls = "";

                    ///lista para tramites
                    List<string> tramPac = new List<string>();

                    //  string idActa = Request.Form["idActa"].ToString();
                    retorno5 = cx.Listar("paSP_InfoTramitesInformarEnvio_cargar", "documento", Request.Form["documento"], "empresa", empresa, "responsable", responsable);


                    cade1 = retorno5.Substring(retorno5.IndexOf(":[") + 2);
                    cade1_global = cade1.Replace("]}", "").Replace("'", "");

                    // Armar la lista de parentesco info
                    tram = cade1_global.Split(',').ToList<string>();

                    //////////////////////////////INFORMACION DEL PACIENTE 

                    string documentoPac = "";
                    string nombrePac = "";
                    string mailPacPac = "";
                    string banderaPac = "";
                    string tipoDocPac = "";

                    string[] informacionPac; // Declaración del array
                    informacionPac = new string[5]; // Instanciación del array

                    //  string idActa = Request.Form["idActa"].ToString();
                    retorno = cx.Listar("paSP_PacienteInformarCorreo_listar", "documento", Request.Form["documento"], "empresa", empresa, "responsable", responsable);

                    cade = retorno.Substring(retorno.IndexOf(":[") + 2);
                    cade_global = cade.Replace("]}", "").Replace("'", "");

                    informacionPac = cade_global.Split(',');


                    documentoPac = informacionPac[0];
                    // Armar la lista de paciente info
                    nombrePac = informacionPac[1];
                    // Armar la lista de parentesco info
                    mailPacPac = informacionPac[2];

                    banderaPac = informacionPac[3];

                    tipoDocPac = informacionPac[4];

                    enviarCorreoAdjuntoInformar(documentoPac, nombrePac, mailPacPac, banderaPac, usuario, tipoDocPac, tram);



                    cx.Desconectar();
                    Response.Write("{'msj':" + '1' + "}");
                    break;

                ////////////////////////////////////////////////////////////
                /////////ENVIO DE CORREOS INOFRMAR RECHAZADOS//////////////
                /////////////////////////////////////////////////////////////

                case "enviarCorreosInformarRechazados":
                    string cadenaRec = "";
                    string cadenaRec_global = "";
                    string cade2 = "";
                    string cade_global2 = "";
                    ///globales nuevas
                    string cade3 = "";
                    string cade1_global3 = "";
                    ///globales nuevas
                    string cade4 = "";
                    string cade_global4 = "";

                    string ctls1 = "";

                    ///lista para los adjuntos
                    List<string> documentosRec = new List<string>();


                    ///lista para tramites
                    List<string> tramites = new List<string>();

                    ///lista para tramites
                    List<string> tramInfoRecPac = new List<string>();

                    ///lista para tramites
                    List<string> respuestas = new List<string>();

                    //  string idActa = Request.Form["idActa"].ToString();
                    retorno1 = cx.Listar("paSP_InfoTramitesInformarRechazados_cargar", "documento", Request.Form["documento"], "empresa", empresa, "responsable", responsable);
                    cade3 = retorno1.Substring(retorno1.IndexOf(":[") + 2);
                    cade1_global3 = cade3.Replace("]}", "").Replace("'", "");

                    // Armar la lista de parentesco info
                    tramites = cade1_global3.Split(',').ToList<string>();

                    retorno2 = cx.Listar("paSP_InfoDocInformarRechazados_cargar", "documento", Request.Form["documento"], "empresa", empresa, "responsable", responsable);
                    cade2 = retorno2.Substring(retorno2.IndexOf(":[") + 2);
                    cade_global2 = cade2.Replace("]}", "").Replace("'", "");
                    // Armar la lista de parentesco info
                    documentosRec = cade_global2.Split(',').ToList<string>();

                    string documentoPacRec = "";
                    string nombrePacRec = "";
                    string mailPacRec = "";
                    string banderaRec = "";
                    string tipoDocRec = "";

                    string[] informacionPacRec; // Declaración del array
                    informacionPacRec = new string[5]; // Instanciación del array

                    //  string idActa = Request.Form["idActa"].ToString();
                    retorno = cx.Listar("paSP_PacienteInformarRechazadosCorreo_listar", "documento", Request.Form["documento"], "empresa", empresa, "responsable", responsable);
                    // retorno = cx.Listar("paSP_PacienteInformarRechazadosCorreo_listar", "documento", Request.Form["documento"], "empresa", empresa, "responsable", responsable);  

                    cadenaRec = retorno.Substring(retorno.IndexOf(":[") + 2);
                    cadenaRec_global = cadenaRec.Replace("]}", "").Replace("'", "");


                    informacionPacRec = cadenaRec_global.Split(',');


                    documentoPacRec = informacionPacRec[0];
                    // Armar la lista de paciente info
                    nombrePacRec = informacionPacRec[1];
                    // Armar la lista de parentesco info
                    mailPacRec = informacionPacRec[2];

                    banderaRec = informacionPacRec[3];

                    tipoDocRec = informacionPacRec[4];


                    retorno3 = cx.Listar("paSP_ActualizarTramLlaControlRechazo_cargar", "documento", Request.Form["documento"], "empresa", empresa, "responsable", responsable);
                    cade4 = retorno3.Substring(retorno3.IndexOf(":[") + 2);
                    cade_global4 = cade4.Replace("]}", "").Replace("'", "");
                    respuestas = cade_global4.Split(',').ToList<string>();
                    //////////////////////////////INFORMACION DEL PACIENTE 


                    enviarCorreoAdjuntoInformaRechazados(documentoPacRec, nombrePacRec, mailPacRec, banderaRec, usuario, tipoDocRec, tramites, documentosRec);



                    //cx.Desconectar();
                    Response.Write("{'msj':" + '1' + "}");
                    break;

                case "enviarCorreosInformarRechazados2Con":
                    string cadenaRec2Con = "";
                    string cadenaRec_global2Con = "";
                    string cade2Con = "";
                    string cade_global2Con = "";
                    ///globales nuevas
                    string cade32Con = "";
                    string cade32Con2 = "";
                    string cade1_global32Con = "";
                    ///globales nuevas
                    string cade42Con = "";
                    string cade_global42Con = "";



                    ///lista para los adjuntos
                    List<string> documentosRec2Con = new List<string>();


                    ///lista para tramites
                    List<string> tramites2Con = new List<string>();

                    ///lista para tramites
                    List<string> tramInfoRecPac2Con = new List<string>();

                    ///lista para tramites
                    List<string> respuestas2Con = new List<string>();

                    //  string idActa = Request.Form["idActa"].ToString();
                    retorno1 = cx.Listar("paSP_InfoTramitesInformarRechazados2Con_cargar", "documento", Request.Form["documento"], "empresa", empresa, "responsable", responsable);

                    cade32Con = retorno1.Substring(retorno1.IndexOf(":[") + 2);
                    //cade32Con2 = cade32Con.Replace(",", " ");
                    cade1_global32Con = cade32Con.Replace("]}", "").Replace("'", "");

                    // Armar la lista de parentesco info
                    tramites2Con = cade1_global32Con.Split(',').ToList<string>();

                    retorno2 = cx.Listar("paSP_InfoDocInformarRechazados2Con_cargar", "documento", Request.Form["documento"], "empresa", empresa, "responsable", responsable);
                    cade2Con = retorno2.Substring(retorno2.IndexOf(":[") + 2);
                    cade_global2Con = cade2Con.Replace("]}", "").Replace("'", "");
                    // Armar la lista de parentesco info
                    documentosRec2Con = cade_global2Con.Split(',').ToList<string>();

                    string documentoPacRec2Con = "";
                    string nombrePacRec2Con = "";
                    string mailPacRec2Con = "";
                    string banderaRec2Con = "";
                    string tipoDocRec2Con = "";

                    string[] informacionPacRec2Con; // Declaración del array
                    informacionPacRec2Con = new string[5]; // Instanciación del array

                    //  string idActa = Request.Form["idActa"].ToString();
                    retorno = cx.Listar("paSP_PacienteInformarRechazadosCorreo_listar", "documento", Request.Form["documento"], "empresa", empresa, "responsable", responsable);
                    // retorno = cx.Listar("paSP_PacienteInformarRechazadosCorreo_listar", "documento", Request.Form["documento"], "empresa", empresa, "responsable", responsable);  

                    cadenaRec2Con = retorno.Substring(retorno.IndexOf(":[") + 2);
                    cadenaRec_global2Con = cadenaRec2Con.Replace("]}", "").Replace("'", "");


                    informacionPacRec2Con = cadenaRec_global2Con.Split(',');


                    documentoPacRec2Con = informacionPacRec2Con[0];
                    // Armar la lista de paciente info
                    nombrePacRec2Con = informacionPacRec2Con[1];
                    // Armar la lista de parentesco info
                    mailPacRec2Con = informacionPacRec2Con[2];

                    banderaRec2Con = informacionPacRec2Con[3];

                    tipoDocRec2Con = informacionPacRec2Con[4];


                    retorno3 = cx.Listar("paSP_ActualizarTramLlaControlRechazo2Con_cargar", "documento", Request.Form["documento"], "empresa", empresa, "responsable", responsable);
                    cade42Con = retorno3.Substring(retorno3.IndexOf(":[") + 2);
                    cade_global42Con = cade42Con.Replace("]}", "").Replace("'", "");
                    respuestas2Con = cade_global42Con.Split(',').ToList<string>();
                    //////////////////////////////INFORMACION DEL PACIENTE 


                    enviarCorreoAdjuntoInformaRechazados(documentoPacRec2Con, nombrePacRec2Con, mailPacRec2Con, banderaRec2Con, usuario, tipoDocRec2Con, tramites2Con, documentosRec2Con);



                    //cx.Desconectar();
                    Response.Write("{'msj':" + '1' + "}");
                    break;

                ////////////////////////////ENVIO DE CORREO INFORMATIVO////////////////////////////////////////////////////////////////

                case "enviarCorreoInformativo":

                    string cadenas2 = "";
                    string cadenas2_global = "";

                    ///globales nuevas
                    //string cadenas1 = "";
                    //string cadenas1_global = "";

                    //////globales paciente
                    //string cadenas3 = "";
                    //string cadenas3_global = "";

                    ////globales parentesco


                    //List<string> mails = new List<string>();

                    //string documento = "";

                    //string nombre = "";

                    //string mailPac = "";

                    //string bandera = "";

                    //Int16 bnd = 0;

                    //List<string> documento = new List<string>();
                    //List<string> nombre = new List<string>();
                    //List<string> mailPac = new List<string>();
                    //List<string> bandera = new List<string>();

                    string documentoPaci = "";
                    string nombrePaci = "";
                    string mailPaci = "";
                    string banderaPaci = "";


                    string[] informacion2; // Declaración del array
                    informacion2 = new string[4]; // Instanciación del array

                    //  string idActa = Request.Form["idActa"].ToString();
                    retorno = cx.Listar("paSP_PacienteEnvioInstructivo_listar", "documento", Request.Form["documento"], "empresa", empresa, "responsable", responsable);

                    cadenas2 = retorno.Substring(retorno.IndexOf(":[") + 2);
                    cadenas2_global = cadenas2.Replace("]}", "").Replace("'", "");

                    informacion2 = cadenas2_global.Split(',');


                    documentoPaci = informacion2[0];
                    // Armar la lista de paciente info
                    nombrePaci = informacion2[1];
                    // Armar la lista de parentesco info
                    mailPaci = informacion2[2];
                    banderaPaci = informacion2[3];

                    //        //if (IsValidEmail(mail))
                    //        //{
                    enviarCorreoAdjuntoInformativo(documentoPaci, nombrePaci, mailPaci, banderaPaci);
                    Response.Write(retorno);
                    cx.Desconectar();
                    break;

            }
        }





        public static string ImportantData
        {
            get
            {
                return gDate;

            }
            set
            {
                gDate = value;

            }

        }
        protected void informarbuzonnolegible(string f, string esp, string co, string pa)
        {

            /*-------------------------MENSAJE DE CORREO----------------------*/
            //   co = "edwardmoralesr@gmail.com";
            //Creamos un nuevo Objeto de mensaje
            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

            //Direccion de correo electronico a la que queremos enviar el mensaje
            mmsg.To.Add(co);

            //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

            //Asunto
            mmsg.Subject = "Informe de Documento No Legible";
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

            //Direccion de correo electronico que queremos que reciba una copia del mensaje
            mmsg.Bcc.Add(co); //Opcional

            //Cuerpo del Mensaje
            mmsg.Body = "<table align='center' style='margin:7px; padding:7px; font-size:17px; color:black;'>";

            //  mmsg.Body += "<tr align='center'><td><a href='http://app.wdvisual.co/INFIM/'> <img src='http://isac.com.co/wp-content/uploads/2015/04/isac-proyetos-realizados-ingenieria-electrica-cable-aereo-manizales-logo.jpg' style= 'width:555px; cursor:pointer;' /></a></td></tr>";


            mmsg.Body += "<tr align='left'><td><h3>Buen Día - Buena Tarde. Sra(a) " + pa + "</h3><br /><b>El motivo de este correo es poder indicarle que la documentación recibida no es legible para realizar la debida gestión del servicio " + esp + "<br />con fecha de revisión " + f + ".<br />Solicitamos por favor el envío de la documentación nuevamente de manera correcta para la respectiva gestión. Gracias</b><br /><br /></td></tr>";

            mmsg.Body += "<tr align='left'><td><br /></td></tr><tr align='left'><td colspan='6'><b><br /></b></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>Cordialmente</b></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>" + Session["usu_sistema"].ToString() + "</b></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>Funcionario de EPS Sanitas</b></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>Programa de Atención Preferencial</b></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>(+57)(1)6193188</b></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>Bogotá, Colombia</b></td></tr></table>";
            //            mmsg.Body += "<tr align='center'><td><a href='http://app.wdvisual.co/SP2017/'> <img src='http://www.vetpraxis.net/wp-content/uploads/2016/01/boton-ingresar-png.png' style= 'width:150px; cursor:pointer;' /> </a>  </td></tr></table>";
            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = true; //Si no queremos que se envíe como HTML

            //Correo electronico desde la que enviamos el mensaje
            mmsg.From = new System.Net.Mail.MailAddress("rutapreferencialeps@colsanitas.com");


            /*-------------------------CLIENTE DE CORREO----------------------*/

            //Creamos un objeto de cliente de correo
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

            //Hay que crear las credenciales del correo emisor
            cliente.Credentials =
                new System.Net.NetworkCredential("rutapreferencialeps@colsanitas.com", "saxfbelteyicyoqm");

            //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail

            cliente.Port = 587;
            cliente.EnableSsl = true;


            cliente.Host = "smtp.gmail.com"; //Para Gmail "smtp.gmail.com";


            /*-------------------------ENVIO DE CORREO----------------------*/

            try
            {
                //Enviamos el mensaje      
                cliente.Send(mmsg);

            }
            catch (System.Net.Mail.SmtpException ex)
            {
                //Aquí gestionamos los errores al intentar enviar el correo
            }
        }


        protected int enviarCorreoAdjuntoInformaRechazados(string documentoPacRec, string nombrePacRec, string mailPacRec, string banderaRec, string usuario, string tipoDocRec, List<string> tramites, List<string> documentosRec)
        {
            //  string ip = Request.ServerVariables["REMOTE_ADDR"];
            //  string pc = Request.ServerVariables["REMOTE_HOST"];
            //  string server = Request.ServerVariables["SERVER_NAME"];
            //    Stream sr = new FileStream(HttpContext.Current.Server.MapPath("Archivos/ArchivosTramites/" + docs + ""), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();

            // correo.From = new System.Net.Mail.MailAddress("lcwebc2011@gmail.com");
            correo.From = new System.Net.Mail.MailAddress("programacionrutaprioritaria@gmail.com");
            correo.To.Add(mailPacRec);
            //   correo.Subject = "Envío de Documentación";
            //  correo.Body = "<b><br />Buen día<br /><br />Se envía la siguiente información<br /><br />Por favor, no dar respuesta a este correo</b>";
            //    DateTime.Now.ToUniversalTime().ToString("dd/MM/yyyy HH:mm:ss") + "\n\nSe accedió desde la siguiente ubicación:\n" + ip + "\n" + pc + "\n" + server;
            // string line = string.Join(",", tram.ToArray());

            //string[] separadas;

            //separadas = line.Split(',');

            ////

            //string line2 = string.Join(",", paren.ToArray());

            //string[] separadas2;

            //separadas2 = line2.Split(',');

            //////////////// tramites/////
            string line3 = string.Join(",", tramites.ToArray());

            string[] separadas3;

            separadas3 = line3.Split(',');


            //correo.Body += "<tr align='left'><td><br /></td></tr><tr align='left'><td colspan='6'><b><br /></b></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>Cordialmente</b></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>" + usuario + "</b></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>Funcionario de EPS Sanitas</b></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>Programa de Atención Preferencial</b></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>rutapreferencial@colsanitas.com</b></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>Bogotá, Colombia</b></td></tr></table>";
            //correo.IsBodyHtml = true;


            //si viene archivo a adjuntar
            //realizamos un recorrido por todos los adjuntos enviados en la lista
            //la lista se llena con direcciones fisicas, por ejemplo: c:/pato.txt

            correo.Subject = "Información De Documentos Enviados Para Gestión";

            if (banderaRec.Equals(" 1"))
            {
                correo.Body = "<table align='left' style='font-family:Arial;text-align:justify; width:700px;'><tr><td colspan='10'><label style='font-size: medium; font-weight: bolder;'>Buenos D&iacute;as,</label><br /><br /><br /></td></tr>";

            }
            else
            {
                correo.Body = "<table align='left' style='font-family:Arial;text-align:justify; width:700px;'><tr><td colspan='10'><label style='font-size: medium; font-weight: bolder;'>Buenas Tardes,</label><br /><br /><br /></td></tr>";
            }

            correo.Body += "<table class='left' style='font-family:Arial;text-align:justify; width:700px;'><tr><td align:left;><p style='font-size: medium; font-weight: bolder;'>Se informa que los documentos o tr&aacute;mites ac&aacute; referenciados se devuelven bajo el motivo indicado para cada uno. Adicionalmente se adjunta los documentos asociados	.</p></td></tr><tr><td align:left;><p style='font-size: medium; font-weight: bolder;'>Agradecemos su atenci&oacute;n.</p><br></td></tr><tr></tr></table>";

            correo.Body += "<tr align='left'><td><br /></td></tr><table align='left' style='font-family:Arial;text-align:center; width:700px;'><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:17px;'>Informaci&oacute;n Paciente</b></td></tr><tr><td><table align='left' style='border: 2px solid;border-color:gray; font-family:Arial;text-align:center; width:690px;' align='center'><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>TIPO DE DOCUMENTO</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + tipoDocRec + "</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>N&Uacute;MERO DE DOCUMENTO</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + documentoPacRec + "</td></tr><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>PACIENTE</td><td colspan='3' style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + nombrePacRec + "</td></tr> </table></td></tr>";

            correo.Body += " <tr align='left'><td><br /></td></tr><tr align='left'><td colspan='8'><b style='font-weight:bolder; font-size:17px;'>Informaci&oacute;n de Tr&aacute;mites</b></td></tr><tr><td><table style='border: 2px solid;border-color:gray; font-family:Arial;text-align:center; width:690px;' align='left'><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>FECHA DE REGISTRO</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>TIPO DE SERVICIO</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>NOMBRE DEL TR&Aacute;MITE</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>MOTIVO</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>DESCRIPCI&Oacute;N</td></tr><tr>";

            for (int i = 0; i < tramites.Count; i += 5)
            {

                try
                {

                    //correo.Body += "<tr align='left'><td><br /></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:17px;'>Informaci&oacute;n de Tr&aacute;mites</b></td></tr><tr><td><table style='border: 2px solid;border-color:gray; font-family:Arial;text-align:center; width:690px;' align='center'><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;' >TIPO DE SERVICIO</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;' colspan='2'>" + separadas3[i + 1] + "</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;' >ESPECIALIDAD</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;' colspan='2'>" + separadas3[i + 1] + "</td></tr><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>NOMBRE DEL TR&Aacute;ITE</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;' colspan='5'>" + separadas3[i + 3] + "</td></tr><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>REQUIERE AUTORIZACI&Oacute;N</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 11] + "</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>APROBADO</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 5] + "</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;' >N&Uacute;MERO DE AUTORIZACI&Oacute;N</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;' >" + separadas3[i + 10] + "</td></tr><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>PRESTADOR</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;' colspan='5'>" + separadas3[i + 6] + " </td></tr><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>MEDICO A AGENDAR</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'  colspan='2'>" + separadas3[i + 9] + "</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;' >FECHA ESTIMADA DE PROGRAMACI&Oacute;N </td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;' colspan='2'>" + separadas3[i + 7] + "</td></tr></table></td></tr>";
                    //correo.Body += "<tr align='left'><td><br /></td></tr><tr align='left'><td colspan='8'><b style='font-weight:bolder; font-size:17px;'>Informaci&oacute;n de Tr&aacute;mites</b></td></tr><tr><td><table style='border: 2px solid;border-color:gray; font-family:Arial;text-align:center; width:690px;' align='left'><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>TIPO DE SERVICIO</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>ESPECIALIDAD</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>NOMBRE DEL TR&Aacute;ITE</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>REQUIERE AUTORIZACI&Oacute;N</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>APROBADO</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;' >N&Uacute;MERO DE AUTORIZACI&Oacute;N</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>PRESTADOR</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>MEDICO A AGENDAR</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;' >FECHA ESTIMADA DE PROGRAMACI&Oacute;N </td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;' >HORA DE PROGRAMACI&Oacute;N </td></tr><tr><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 1] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 1] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 3] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 11] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 5] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 10] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 6] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 9] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 7] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 8] + "</td></tr></table></td></tr>";
                    correo.Body += "<tr><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 0] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 1] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 2] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 3] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 4] + "</td></tr>";
                    // }
                    // else { 

                    // }

                }
                catch (Exception ex)
                {
                    ex.Message.ToString();

                }
            }

            correo.Body += "</tr></table></td></tr>";


            if (documentosRec != null)
            {
                //agregado de archivo
                foreach (string archivo in documentosRec)
                {

                    if (archivo != "msj:0}")
                    {
                        Stream sr = new FileStream(HttpContext.Current.Server.MapPath("../Archivos/ArchivosTramites/" + @archivo + ""), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                        //comprobamos si existe el archivo y lo agregamos a los adjuntos
                        //  if (System.IO.File.Exists(@archivo))


                        correo.Attachments.Add(new Attachment(sr, @archivo));

                        //sr.Close();
                        //sr.Dispose();
                    }
                }
            }


            correo.Body += "<tr align='left'><td><br /></td></tr><tr align='left'><td colspan='6'><b><br /></b></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>Cordialmente </b><br /></td></tr><tr align='left'></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>Programa de Atenci&oacute;n Preferencial</b><br /></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>rutapreferencialeps@colsanitas.com</b><br /></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>Bogot&aacute;, Colombia</b><br /></td></tr></table>";
            correo.IsBodyHtml = true;

            //  correo.Attachments.Add(new Attachment(sr, "Acta_" + mail + ".pdf"));
            correo.Priority = System.Net.Mail.MailPriority.Normal;
            //
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

            //smtp.Credentials = new System.Net.NetworkCredential("lcwebc2011", "desarrolladorLC*");
            //smtp.Credentials = new System.Net.NetworkCredential("informarrutapreferencial@gmail.com", "qwfikhgotsuniyvo");
            smtp.Credentials = new System.Net.NetworkCredential("rutapreferencialeps@colsanitas.com", "saxfbelteyicyoqm");
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(correo);
                return 1;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return 0;
            }

            //Stream  sr1 = new FileStream(HttpContext.Current.Server.MapPath("Archivos/ArchivosTramites/"), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            //  sr1.Close();
            //  sr1.Dispose();
        }

        ///FUNCIÓN DE CORREOS Y ADJUNTOS
        ///
        protected int enviarCorreoAdjunto(List<string> docs, string mail, List<string> pac, string usuario, List<string> paren, List<string> tram)
        {
            //  string ip = Request.ServerVariables["REMOTE_ADDR"];
            //  string pc = Request.ServerVariables["REMOTE_HOST"];
            //  string server = Request.ServerVariables["SERVER_NAME"];
            //    Stream sr = new FileStream(HttpContext.Current.Server.MapPath("Archivos/ArchivosTramites/" + docs + ""), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();

            // correo.From = new System.Net.Mail.MailAddress("lcwebc2011@gmail.com");
            correo.From = new System.Net.Mail.MailAddress("programacionrutaprioritaria@gmail.com");
            correo.To.Add(mail);
            //   correo.Subject = "Envío de Documentación";
            //  correo.Body = "<b><br />Buen día<br /><br />Se envía la siguiente información<br /><br />Por favor, no dar respuesta a este correo</b>";
            //    DateTime.Now.ToUniversalTime().ToString("dd/MM/yyyy HH:mm:ss") + "\n\nSe accedió desde la siguiente ubicación:\n" + ip + "\n" + pc + "\n" + server;
            string line = string.Join(",", pac.ToArray());

            string[] separadas;

            separadas = line.Split(',');

            //

            string line2 = string.Join(",", paren.ToArray());

            string[] separadas2;

            separadas2 = line2.Split(',');

            //////////////// tramites/////
            string line3 = string.Join(",", tram.ToArray());

            string[] separadas3;

            separadas3 = line3.Split(',');

            correo.Subject = "PROGRAMACIÓN -" + separadas[3] + " " + separadas[4] + " " + separadas[5] + " " + separadas[6] + " - " + separadas[0].Substring(0, 2) + " - " + separadas[1] + "";

            correo.Body = "<table align='left' style='font-family:Arial;text-align:center; width:700px;'><tr align='left'><td colspan='6'><h3>Buen día " + separadas[16] + ", </h3></td></tr><tr align='left'><td colspan='6'><h3>Agradecemos su colaboración con la siguiente programación:</h3></td></tr><tr><td><br /></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:17px;'>Información Paciente</b></td></tr><tr><td><table style='border: 2px solid;border-color:gray; font-family:Arial;text-align:center; width:690px;' align='center'><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>TIPO DE DOCUMENTO</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas[0] + "</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>NÚMERO DE DOCUMENTO</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas[1] + "</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>FECHA DE NACIMIENTO</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas[2] + "</td></tr><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>PACIENTE</td><td colspan='3' style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas[3] + " " + separadas[4] + " " + separadas[5] + " " + separadas[6] + "</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>EDAD</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'> " + separadas[7] + " AÑOS</td></tr><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>DIRECCIÓN</td><td colspan='5' style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas[11] + "</td></tr><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>TELÉFONO 1</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas[12] + "</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>TELÉFONO 2</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas[13] + "</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>CELULAR </td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas[14] + "</td></tr></table></td></tr>";

            if (line2.Length > 10)
            {
                correo.Body += "<tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:17px;'><br />Información Acudiente</b></td></tr><tr><td><table style='border: 2px solid;border-color:gray; font-family:Arial;text-align:center; width:690px;' align='center'><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>NÚMERO DE DOCUMENTO</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas2[0] + "</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>PARENTESCO</td><td  style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas2[2] + "</td></tr><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>ACUDIENTE</td><td colspan='3' style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas2[1] + "</td></tr><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>TELÉFONO 1</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas2[3] + "</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>CELULAR </td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas2[4] + "</td></tr></table></td></tr>";
            }

            correo.Body += "<tr align='left'><td><br /></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:17px;'>Información del Trámite</b></td></tr><tr><td><table style='border: 2px solid;border-color:gray; font-family:Arial;text-align:center; width:690px;' align='center'><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;' >TIPO DE SERVICIO</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;' colspan='2'>" + separadas3[0] + "</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;' >ESPECIALIDAD</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;' colspan='2'>" + separadas3[1] + "</td></tr><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>NOMBRE DEL TRÁMITE</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;' colspan='5'>" + separadas3[2] + "</td></tr><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>REQUIERE AUTORIZACIÓN</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[3] + "</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>APROBADO</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[4] + "</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;' >NÚMERO DE AUTORIZACIÓN</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;' >" + separadas3[5] + "</td></tr><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>PRESTADOR</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;' colspan='5'>" + separadas3[6] + " </td></tr><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>MEDICO A AGENDAR</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'  colspan='2'>" + separadas3[7] + "</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;' >FECHA ESTIMADA DE PROGRAMACIÓN </td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;' colspan='2'>" + separadas3[8] + "</td></tr></table></td></tr>";



            correo.Body += "<tr align='left'><td><br /></td></tr><tr align='left'><td colspan='6'><b><br /></b></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>Cordialmente</b></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>" + usuario + "</b></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>Funcionario de EPS Sanitas</b></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>Programa de Atención Preferencial</b></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>(+57)(1)6193188</b></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>Bogotá, Colombia</b></td></tr></table>";
            correo.IsBodyHtml = true;


            //si viene archivo a adjuntar
            //realizamos un recorrido por todos los adjuntos enviados en la lista
            //la lista se llena con direcciones fisicas, por ejemplo: c:/pato.txt

            if (docs != null)
            {
                //agregado de archivo
                foreach (string archivo in docs)
                {

                    if (archivo != "msj:0}")
                    {
                        Stream sr = new FileStream(HttpContext.Current.Server.MapPath("../Archivos/ArchivosTramites/" + @archivo + ""), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                        //comprobamos si existe el archivo y lo agregamos a los adjuntos
                        //  if (System.IO.File.Exists(@archivo))


                        correo.Attachments.Add(new Attachment(sr, @archivo));

                        //sr.Close();
                        //sr.Dispose();
                    }
                }
            }


            //  correo.Attachments.Add(new Attachment(sr, "Acta_" + mail + ".pdf"));
            correo.Priority = System.Net.Mail.MailPriority.Normal;
            //
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

            //smtp.Credentials = new System.Net.NetworkCredential("lcwebc2011", "desarrolladorLC*");
            smtp.Credentials = new System.Net.NetworkCredential("programacionrutaprioritaria@gmail.com", "ehmxnzdjvrxfgrfw");
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(correo);
                return 1;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return 0;
            }

            //Stream  sr1 = new FileStream(HttpContext.Current.Server.MapPath("Archivos/ArchivosTramites/"), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            //  sr1.Close();
            //  sr1.Dispose();
        }



        protected int enviarCorreoAdjuntoRecordar(string documento, string nombre, string mailPC, string bandera)
        {
            //  string ip = Request.ServerVariables["REMOTE_ADDR"];
            //  string pc = Request.ServerVariables["REMOTE_HOST"];
            //  string server = Request.ServerVariables["SERVER_NAME"];
            //    Stream sr = new FileStream(HttpContext.Current.Server.MapPath("Archivos/ArchivosTramites/" + docs + ""), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();

            // correo.From = new System.Net.Mail.MailAddress("lcwebc2011@gmail.com");
            correo.From = new System.Net.Mail.MailAddress("recordatoriorutapreferencial@gmail.com");
            correo.To.Add(mailPC);
            //   correo.Subject = "Envío de Documentación";
            //  correo.Body = "<b><br />Buen día<br /><br />Se envía la siguiente información<br /><br />Por favor, no dar respuesta a este correo</b>";
            //    DateTime.Now.ToUniversalTime().ToString("dd/MM/yyyy HH:mm:ss") + "\n\nSe accedió desde la siguiente ubicación:\n" + ip + "\n" + pc + "\n" + server;
            //string line = string.Join(",", pac.ToArray());


            correo.Subject = "Recordatorio Envío Ordenes Médicas";
            if (bandera.Equals(" 1"))
            {
                correo.Body = "<table align='left' style='font-family:Arial;text-align:justify; width:700px;'><tr><td colspan='10'><label style='font-size: medium; font-weight: bolder;'>Buenos D&iacute;as,</label><br /><br /><br /></td></tr>";

            }
            else
            {
                correo.Body = "<table align='left' style='font-family:Arial;text-align:justify; width:700px;'><tr><td colspan='10'><label style='font-size: medium; font-weight: bolder;'>Buenas Tardes,</label><br /><br /><br /></td></tr>";
            }

            correo.Body += " <table class='left' style='font-family:Arial;text-align:justify; width:700px;'><tr><td align:left;><p style='font-size: medium; font-weight: bolder;'>De parte de la Ruta de Atenci&oacute;n Prioritaria, le recordamos el env&iacute;o al correo electr&oacute;nico de las &oacute;rdenes m&eacute;dicas pendientes para colaborarle con el tr&aacute;mite de las mismas.  Nuestro correo es rutapreferencialeps@colsanitas.com, en caso de tener alguna duda o solicitud a realizar puede efectuarlo por ese mismo medio.</p></td></tr><tr><td align:left;><p style='font-size: medium; font-weight: bolder;'>Agradecemos su atenci&oacute;n.</p><br></td></tr><tr><td><p style='font-size: medium; font-weight: bolder;'>Cordialmente,</p></td></tr><tr><td align:left;><p style='font-size: medium; font-weight: bolder;'>Ruta de Atenci&oacute;n Prioritaria</p></td></tr></table>";

            correo.IsBodyHtml = true;


            //si viene archivo a adjuntar
            //realizamos un recorrido por todos los adjuntos enviados en la lista
            //la lista se llena con direcciones fisicas, por ejemplo: c:/pato.txt


            //  correo.Attachments.Add(new Attachment(sr, "Acta_" + mail + ".pdf"));
            correo.Priority = System.Net.Mail.MailPriority.Normal;
            //
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

            //smtp.Credentials = new System.Net.NetworkCredential("lcwebc2011", "desarrolladorLC*");
            // smtp.Credentials = new System.Net.NetworkCredential("recordatoriorutapreferencial@gmail.com", "kynjeowdxmbitshi");
            smtp.Credentials = new System.Net.NetworkCredential("rutapreferencialeps@colsanitas.com", "saxfbelteyicyoqm");
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(correo);
                return 1;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return 0;
            }

            //Stream  sr1 = new FileStream(HttpContext.Current.Server.MapPath("Archivos/ArchivosTramites/"), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            //  sr1.Close();
            //  sr1.Dispose();
        }


        protected int enviarCorreoAdjuntoInformar(string documento, string nombre, string mailPC, string bandera, string usuario, string tipoDocPac, List<string> tram)
        {
            //  string ip = Request.ServerVariables["REMOTE_ADDR"];
            //  string pc = Request.ServerVariables["REMOTE_HOST"];
            //  string server = Request.ServerVariables["SERVER_NAME"];
            //    Stream sr = new FileStream(HttpContext.Current.Server.MapPath("Archivos/ArchivosTramites/" + docs + ""), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();

            // correo.From = new System.Net.Mail.MailAddress("lcwebc2011@gmail.com");
            //            correo.From = new System.Net.Mail.MailAddress("programacionrutaprioritaria@gmail.com");
            correo.From = new System.Net.Mail.MailAddress("rutapreferencialeps@colsanitas.com");
            correo.To.Add(mailPC);
            //    correo.To.Add("edwardmoralesr@gmail.com");
            //   correo.Subject = "Envío de Documentación";
            //  correo.Body = "<b><br />Buen día<br /><br />Se envía la siguiente información<br /><br />Por favor, no dar respuesta a este correo</b>";
            //    DateTime.Now.ToUniversalTime().ToString("dd/MM/yyyy HH:mm:ss") + "\n\nSe accedió desde la siguiente ubicación:\n" + ip + "\n" + pc + "\n" + server;
            // string line = string.Join(",", tram.ToArray());

            //string[] separadas;

            //separadas = line.Split(',');

            ////

            //string line2 = string.Join(",", paren.ToArray());

            //string[] separadas2;

            //separadas2 = line2.Split(',');

            //////////////// tramites/////
            string line3 = string.Join(",", tram.ToArray());

            string[] separadas3;

            separadas3 = line3.Split(',');


            //correo.Body += "<tr align='left'><td><br /></td></tr><tr align='left'><td colspan='6'><b><br /></b></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>Cordialmente</b></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>" + usuario + "</b></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>Funcionario de EPS Sanitas</b></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>Programa de Atención Preferencial</b></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>rutapreferencial@colsanitas.com</b></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>Bogotá, Colombia</b></td></tr></table>";
            //correo.IsBodyHtml = true;


            //si viene archivo a adjuntar
            //realizamos un recorrido por todos los adjuntos enviados en la lista
            //la lista se llena con direcciones fisicas, por ejemplo: c:/pato.txt

            correo.Subject = "Programación Agendamiento de Tramites";

            if (bandera.Equals(" 1"))
            {
                correo.Body = "<table align='left' style='font-family:Arial;text-align:justify; width:700px;'><tr><td colspan='10'><label style='font-size: medium; font-weight: bolder;'>Buenos D&iacute;as,</label><br /><br /><br /></td></tr>";

            }
            else
            {
                correo.Body = "<table align='left' style='font-family:Arial;text-align:justify; width:700px;'><tr><td colspan='10'><label style='font-size: medium; font-weight: bolder;'>Buenas Tardes,</label><br /><br /><br /></td></tr>";
            }

            correo.Body += "<tr align='left'><td><br /></td></tr><table align='left' style='font-family:Arial;text-align:center; width:700px;'><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:17px;'>Informaci&oacute;n Paciente</b></td></tr><tr><td><table align='left' style='border: 2px solid;border-color:gray; font-family:Arial;text-align:center; width:690px;' align='center'><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>TIPO DE DOCUMENTO</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + tipoDocPac + "</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>N&Uacute;MERO DE DOCUMENTO</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + documento + "</td></tr><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>PACIENTE</td><td colspan='3' style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + nombre + "</td></tr> </table></td></tr>";

            correo.Body += "<tr align='left'><td><br /></td></tr><tr align='left'><td colspan='8'><b style='font-weight:bolder; font-size:17px;'>Informaci&oacute;n de Tr&aacute;mites</b></td></tr>";

            for (int i = 0; i < tram.Count; i += 13)
            {

                try
                {

                    correo.Body += "<tr><td><table style='border: 2px solid;border-color:gray; font-family:Arial;text-align:center; width:690px;' align='left'><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>TIPO DE SERVICIO</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>NOMBRE DEL TR&Aacute;MITE</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>REQUIERE AUTORIZACI&Oacute;N</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>APROBADO</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;' >N&Uacute;MERO DE AUTORIZACI&Oacute;N</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>PRESTADOR</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>MEDICO A AGENDAR</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;' >FECHA ESTIMADA DE PROGRAMACI&Oacute;N </td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;' >HORA DE PROGRAMACI&Oacute;N </td></tr><tr>";

                    //correo.Body += "<tr align='left'><td><br /></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:17px;'>Informaci&oacute;n de Tr&aacute;mites</b></td></tr><tr><td><table style='border: 2px solid;border-color:gray; font-family:Arial;text-align:center; width:690px;' align='center'><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;' >TIPO DE SERVICIO</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;' colspan='2'>" + separadas3[i + 1] + "</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;' >ESPECIALIDAD</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;' colspan='2'>" + separadas3[i + 1] + "</td></tr><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>NOMBRE DEL TR&Aacute;ITE</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;' colspan='5'>" + separadas3[i + 3] + "</td></tr><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>REQUIERE AUTORIZACI&Oacute;N</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 11] + "</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>APROBADO</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 5] + "</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;' >N&Uacute;MERO DE AUTORIZACI&Oacute;N</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;' >" + separadas3[i + 10] + "</td></tr><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>PRESTADOR</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;' colspan='5'>" + separadas3[i + 6] + " </td></tr><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>MEDICO A AGENDAR</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'  colspan='2'>" + separadas3[i + 9] + "</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;' >FECHA ESTIMADA DE PROGRAMACI&Oacute;N </td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;' colspan='2'>" + separadas3[i + 7] + "</td></tr></table></td></tr>";
                    //correo.Body += "<tr align='left'><td><br /></td></tr><tr align='left'><td colspan='8'><b style='font-weight:bolder; font-size:17px;'>Informaci&oacute;n de Tr&aacute;mites</b></td></tr><tr><td><table style='border: 2px solid;border-color:gray; font-family:Arial;text-align:center; width:690px;' align='left'><tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>TIPO DE SERVICIO</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>ESPECIALIDAD</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>NOMBRE DEL TR&Aacute;ITE</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>REQUIERE AUTORIZACI&Oacute;N</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>APROBADO</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;' >N&Uacute;MERO DE AUTORIZACI&Oacute;N</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>PRESTADOR</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;'>MEDICO A AGENDAR</td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;' >FECHA ESTIMADA DE PROGRAMACI&Oacute;N </td><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;' >HORA DE PROGRAMACI&Oacute;N </td></tr><tr><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 1] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 1] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 3] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 11] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 5] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 10] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 6] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 9] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 7] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 8] + "</td></tr></table></td></tr>";
                    correo.Body += "<tr><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 1] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 3] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 11] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 5] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 10] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 6] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 9] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 7] + "</td><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;'>" + separadas3[i + 8] + "</td></tr>";
                    correo.Body += "<tr><td style='background-color:#FF7811; color:white; padding:4px; font-weight:bold;' colspan='10'>PREPARACI&Oacute;N</td></tr><tr><td style='background-color:#F2F2F2; color:black; padding:4px; font-weight:bold;' colspan='10'>" + separadas3[i + 12] + "</td></tr>";
                    correo.Body += "</table></td></tr><tr><td></td></tr><tr><td></td></tr>";
                    // correo.Body += "<br />";
                    // }
                    // else { 

                    // }

                }
                catch (Exception ex)
                {
                    ex.Message.ToString();

                }
            }

            correo.Body += "</td></tr>";

            //if (tram != null)
            //{
            //    //agregado de archivo
            //    foreach (tram)
            //    {

            //        if (tram != "msj:0}")
            //        {

            //        }
            //    }
            //}

            correo.Body += "<tr align='left'><td><br /></td></tr><tr align='left'><td colspan='6'><b><br /></b></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>Cordialmente </b><br /></td></tr><tr align='left'></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>Programa de Atenci&oacute;n Preferencial</b><br /></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>rutapreferencialeps@colsanitas.com</b><br /></td></tr><tr align='left'><td colspan='6'><b style='font-weight:bolder; font-size:15px;'>Bogot&aacute;, Colombia</b><br /></td></tr></table>";
            correo.IsBodyHtml = true;

            //  correo.Attachments.Add(new Attachment(sr, "Acta_" + mail + ".pdf"));
            correo.Priority = System.Net.Mail.MailPriority.Normal;
            //
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

            //smtp.Credentials = new System.Net.NetworkCredential("lcwebc2011", "desarrolladorLC*");
            //smtp.Credentials = new System.Net.NetworkCredential("informarrutapreferencial@gmail.com", "qwfikhgotsuniyvo");
            smtp.Credentials = new System.Net.NetworkCredential("rutapreferencialeps@colsanitas.com", "saxfbelteyicyoqm");
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(correo);
                return 1;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return 0;
            }

            //Stream  sr1 = new FileStream(HttpContext.Current.Server.MapPath("Archivos/ArchivosTramites/"), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            //  sr1.Close();
            //  sr1.Dispose();
        }


        protected int enviarCorreoAdjuntoInformativo(string documentoPaci, string nombrePaci, string mailPaci, string banderaPaci)
        {
            //  string ip = Request.ServerVariables["REMOTE_ADDR"];
            //  string pc = Request.ServerVariables["REMOTE_HOST"];
            //  string server = Request.ServerVariables["SERVER_NAME"];
            //    Stream sr = new FileStream(HttpContext.Current.Server.MapPath("Archivos/ArchivosTramites/" + docs + ""), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();

            // correo.From = new System.Net.Mail.MailAddress("lcwebc2011@gmail.com");
            correo.From = new System.Net.Mail.MailAddress("recordatoriorutapreferencial@gmail.com");
            correo.To.Add(mailPaci);
            //   correo.Subject = "Envío de Documentación";
            //  correo.Body = "<b><br />Buen día<br /><br />Se envía la siguiente información<br /><br />Por favor, no dar respuesta a este correo</b>";
            //    DateTime.Now.ToUniversalTime().ToString("dd/MM/yyyy HH:mm:ss") + "\n\nSe accedió desde la siguiente ubicación:\n" + ip + "\n" + pc + "\n" + server;
            //string line = string.Join(",", pac.ToArray());

            correo.Subject = "Envio De Instructivo";
            if (banderaPaci.Equals(" 1"))
            {
                correo.Body = "<table align='left' style='font-family:Arial;text-align:justify; width:750px;'><tr><td colspan='10'><label style='font-size: medium; font-weight: bolder;'>Buenos D&iacute;as,</label><br /><br /><br /></td></tr>";

            }
            else
            {
                correo.Body = "<table align='left' style='font-family:Arial;text-align:justify; width:750px;'><tr><td colspan='10'><label style='font-size: medium; font-weight: bolder;'>Buenas Tardes,</label><br /><br /><br /></td></tr>";
            }

            correo.Body += " <table class='left' style='font-family:Arial;text-align:justify; width:750px;'><tr><td align:left;><p style='font-size: medium; font-weight: bolder;'>Usted ha sido incluido(a) en el Programa de Atenci&oacuten Preferencial de EPS Sanitas, donde nuestro objetivo es colaborarle con las autorizaciones de citas, medicamentos, examenes, laboratorios y con la programaci&oacuten de sus citas m&eacutedicas, tambi&eacuten realizarle un acompa&ntildeamiento en su proceso de atenci&oacuten para gestionar lo que ordenan sus m&eacutedicos tratantes.<br/><br/>El programa lo ofrece EPS Sanitas y no tiene ning&uacuten costo adicional.Para solicitar gesti&oacuten sobre alg&uacuten servicio, es necesario el env&iacuteo de las &oacuterdenes m&eacutedicas digitalizadas (escaneadas o una foto legible) al correo rutapreferencialeps@colsanitas.com<br/><br/>De igual forma para resolver dudas o atender solicitudes espec&iacuteficas es necesario enviarlas por este mismo canal.<br/><br/>Quedamos a la espera del env&iacuteo de sus &oacuterdenes m&eacutedicas.</p><br/></td></tr><tr><td><p style='font-size: medium; font-weight: bolder;'>Cordialmente <br/> Programa de Atenci&oacuten Preferencial <br/>rutapreferencialeps@colsanitas.com</p></td></tr></table>";

            correo.IsBodyHtml = true;


            //si viene archivo a adjuntar
            //realizamos un recorrido por todos los adjuntos enviados en la lista
            //la lista se llena con direcciones fisicas, por ejemplo: c:/pato.txt


            //  correo.Attachments.Add(new Attachment(sr, "Acta_" + mail + ".pdf"));
            correo.Priority = System.Net.Mail.MailPriority.Normal;
            //
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

            //smtp.Credentials = new System.Net.NetworkCredential("lcwebc2011", "desarrolladorLC*");
            // smtp.Credentials = new System.Net.NetworkCredential("recordatoriorutapreferencial@gmail.com", "kynjeowdxmbitshi");
            smtp.Credentials = new System.Net.NetworkCredential("rutapreferencialeps@colsanitas.com", "saxfbelteyicyoqm");
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(correo);
                return 1;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return 0;
            }

            //Stream  sr1 = new FileStream(HttpContext.Current.Server.MapPath("Archivos/ArchivosTramites/"), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            //  sr1.Close();
            //  sr1.Dispose();
        }

        public static string getMacAddress()
        {
            // Contador para un ciclo
            int i = 0;
            // Colección de direcciones MAC
            ArrayList DireccionesMAC = new ArrayList();
            // Información de las tarjetas de red
            NetworkInterface[] interfaces = null;
            // Obtener todas las interfaces de red de la PC
            interfaces = NetworkInterface.GetAllNetworkInterfaces();
            string hola = "";
            // Validar la cantidad de tarjetas de red que tiene
            if (interfaces != null && interfaces.Length > 0)
            {
                // Recorrer todas las interfaces de red
                foreach (NetworkInterface adaptador in interfaces)
                {
                    // Obtener la dirección fisica
                    PhysicalAddress direccion = adaptador.GetPhysicalAddress();
                    // Obtener en modo de arreglo de bytes la dirección
                    byte[] bytes = direccion.GetAddressBytes();
                    // Variable que tendra la dirección visible
                    string mac_address = string.Empty;
                    // Recorrer todos los bytes de la direccion
                    for (i = 0; i < bytes.Length; i++)
                    {
                        // Pasar el byte a un formato legible para el usuario
                        mac_address += bytes[i].ToString("X2");
                        if (i != bytes.Length - 1)
                        {
                            // Agregar un separador, por formato
                            mac_address += "-";
                        }
                    }
                    // Agregar la direccion MAC a la lista
                    DireccionesMAC.Add(mac_address);
                    hola = mac_address;
                    break;
                }
            }
            return hola;
        }

    }
}
