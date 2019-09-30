using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Inicial.Controlador
{
    public partial class ctlPaginador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["nom_usuario"]) == null)
            {
                Response.Redirect("../vista/general/inicio.aspx");
            }

            string retorno = "";
            string p = Request.Form["p"];
            string empresa = Session["nit_empresa"].ToString();
            string responsable = Session["usu_sistema"].ToString();
            string fecha = DateTime.Now.ToString();
            Boolean ctl = false;
            try
            {
                ctl = (Request.Form["reg"] != null) ? true : false;
            }
            catch (Exception)
            {
            }

            if (ctl && Request.Form["reg"].ToString() == "1" && (Session["empresa_creada"] == null || Session["empresa_creada"].ToString().Equals("")))
                Session["empresa_creada"] = "NIT";

            int pagActual = int.Parse(Request.Form["pag"]);
            int RegistrosAMostrar = 10, PagAct = pagActual;


            IDataReader dr = null;
            Modelo.ConexionBD_Sql_Server cx = new Modelo.ConexionBD_Sql_Server();
            //Inicial.Modelo.ConexionBD_ORACLE cx = new Modelo.ConexionBD_ORACLE();

            switch (cx.tipoConexion())
            {
                case "ORACLE":
                    switch (p)
                    {
                        case "listarGrupoRol":
                            dr = cx.Paginar("PKG_ROLES.listarRoles", "number", RegistrosAMostrar, "number", PagAct, "varchar2", responsable);
                            break;
                    }
                    break;

                case "SQL_SERVER":
                    switch (p)
                    {


                        case "auditoriaRol":
                            dr = cx.Paginar("listarAudRoles",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", PagAct,
                                "responsable", Request.Form["responsable"].ToUpper(),
                                "accion", Request.Form["accion"].ToUpper(),
                                "rol", Request.Form["rol"].ToUpper(),
                                "empresa", empresa,
                                "creador", responsable);
                            break;

                        case "auditoriaUsuario":
                            dr = cx.Paginar("listarAudUsuarios", "numFilas", RegistrosAMostrar, "filaInicial", PagAct,
                                "responsable", Request.Form["responsable"].ToUpper(),
                                "accion", Request.Form["accion"].ToUpper(),
                                "usuario", Request.Form["usuario"].ToUpper(),
                                "empresa", empresa,
                                "creador", responsable);
                            break;

                        case "listarGrupoRol":
                            dr = cx.Paginar("paINI_Roles_Listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "rol", Request.Form["rol"],
                                "nivel", Request.Form["nivel"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaGrupoUsuario":
                            dr = cx.Paginar("paINI_Usuarios_listar", "numFilas", RegistrosAMostrar, "filaInicial", PagAct,
                                "nombre", Request.Form["nombre"].ToUpper(),
                                "usuario", Request.Form["usuario"].ToUpper(),
                                "correo", Request.Form["correo"].ToUpper(),
                                "empresa", empresa,
                                "creador", responsable);
                            break;

                        case "conectados":
                            dr = cx.Paginar("paINI_UsuariosConectados_listar", "numFilas", RegistrosAMostrar, "filaInicial", pagActual, "empresa", empresa);
                            break;

                        case "empresa":
                            dr = cx.Paginar("paINI_Empresa_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                 "nit", Request.Form["nit"],
                                "nombre", Request.Form["nombre"],
                                "responsable", responsable);
                            break;


                        case "listaAreas":
                            dr = cx.Paginar("paINI_areas_listar",
                            "numFilas", RegistrosAMostrar,
                            "filaInicial", pagActual,
                            "nombre", Request.Form["nombre"],
                            "empresa", empresa,
                            "responsable", responsable);
                            break;

                        case "listaTodosCargos":
                            dr = cx.Paginar("paINI_Cargos_listar",
                             "numFilas", RegistrosAMostrar,
                             "filaInicial", pagActual,
                             "nombreFiltro", Request.Form["nombreFiltro"],
                             "area", Request.Form["area"],
                             "empresa", empresa,
                             "responsable", responsable);
                            break;

                        case "listarGrupos":
                            dr = cx.Paginar("paINI_Grupos_listar",
                            "numFilas", RegistrosAMostrar,
                            "filaInicial", pagActual,
                            "nombre", Request.Form["nombre"],
                            "empresa", empresa,
                            "responsable", responsable);
                            break;



                        case "listaAuditorias":
                            dr = cx.Paginar("listaAuditorias",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "fechaInicio", Request.Form["fechaInicio"],
                                "fechaFin", Request.Form["fechaFin"],
                                "responsableFiltro", Request.Form["responsableFiltro"],
                                "objeto", Request.Form["objeto"],
                                "accion", Request.Form["accion"],
                                "empresa", empresa,
                                "responsable", responsable
                                );
                            break;



                        case "listaDepartamentos":
                            dr = cx.Paginar("paINI_DepartamentosEmpresa_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "nombre", Request.Form["nombre"],
                                "empresa", empresa,
                                "responsable", responsable
                                );
                            break;


                        case "listaAreasEmpresa":
                            dr = cx.Paginar("paINI_AreasEmpresa_lista",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "nombre", Request.Form["nombre"],
                                "departamento", Request.Form["departamento"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;



                        case "listaAcividadesPaginado":
                            dr = cx.Paginar("paINI_ActividadesEconomicas_Listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                //"fechaInicio", Request.Form["fechaInicio"],
                                //"fechaFin", Request.Form["fechaFin"],
                                "nombreCodigoFil", Request.Form["codigo"],
                                "especialiadadFil", Request.Form["nombre"],
                                "empresa", empresa,
                                "responsable", responsable
                                );
                            break;

                        case "listaFacturacion":
                            dr = cx.Paginar("paINI_Facturacion_Listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "empresa", Request.Form["empresa"],
                                "responsable", responsable
                                );
                            break;

                        case "listarespecialidadesSP":
                            dr = cx.Paginar("paSP_especialidades_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "especialidad", Request.Form["especialidad"].ToUpper(),
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPrestadoresSP":
                            dr = cx.Paginar("paSP_PrestadoresSP_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "prestador", Request.Form["prestador"],
                                "nivel", Request.Form["nivel"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPrestadores":
                            dr = cx.Paginar("paSP_PrestadoresSPNu_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "prestador", Request.Form["prestador"],
                                "nivel", Request.Form["nivel"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaCIE10PacI":
                            dr = cx.Paginar("paSP_CIE10_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "codigo", Request.Form["codigo"].ToUpper(),
                                "nombre", Request.Form["nombre"].ToUpper(),
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarCategoriaCie10":
                            dr = cx.Paginar("paSP_CategoriaCie10_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "codigo", Request.Form["codigo"].ToUpper(),
                                "nombre", Request.Form["nombre"].ToUpper(),
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaOcupacionPacI":
                            dr = cx.Paginar("paSP_OcupacionPac_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "codigo", Request.Form["codigo"].ToUpper(),
                                "nombre", Request.Form["nombre"].ToUpper(),
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarCargueMasivo":
                            dr = cx.Paginar("paSP_CargueMasivo_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "fecha", Request.Form["fecha"].ToUpper(),
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPaciente":
                            dr = cx.Paginar("paSP_Pacientes_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "tipoCargue", Request.Form["tipoCargue"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacienteXcargue":

                            if (ctlGeneral.tGlobal == "-1" || Request.Form["tipoCargue"] != "-1")
                            {
                                ctlGeneral.tGlobal = Request.Form["tipoCargue"];
                            }
                            if (ctlGeneral.cGlobal == "-1" || Request.Form["semaFil"] != "-1")
                            {
                                ctlGeneral.cGlobal = Request.Form["semaFil"];
                            }
                            if (ctlGeneral.mGlobal == "-1" || Request.Form["mesFil"] != "-1")
                            {
                                ctlGeneral.mGlobal = Request.Form["mesFil"];
                            }
                            if (ctlGeneral.aGlobal == "-1" || Request.Form["anioFil"] != "-1")
                            {
                                ctlGeneral.aGlobal = Request.Form["anioFil"];
                            }



                            //if (Request.Form["tipoCargue"] == "-1" && Request.Form["semaFil"] == "-1" && Request.Form["mesFil"] == "-1" && Request.Form["anioFil"] == "-1")
                            //{
                            //    ctlGeneral.tGlobal = "-1";
                            //    ctlGeneral.cGlobal = "-1";
                            //    ctlGeneral.mGlobal = "-1";
                            //    ctlGeneral.aGlobal = "-1";

                            //}



                            dr = cx.Paginar("paSP_PacientesXcargue_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "tipoCargue", ctlGeneral.tGlobal,
                                "anioFil", ctlGeneral.aGlobal,
                                "mesFil", ctlGeneral.mGlobal,
                                "semaFil", ctlGeneral.cGlobal,
                                "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            //ctlGeneral.cGlobal = "-1";
                            //ctlGeneral.tGlobal = "-1";
                            //ctlGeneral.mGlobal = "-1";
                            //ctlGeneral.aGlobal = "-1";
                            break;

                        case "paPRO_Pacientes_listar":
                            dr = cx.Paginar("paPRO_Pacientes_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "fechaFil", Request.Form["fechaFil"],
                                "tipo", Request.Form["tipo"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "paSP_PacientesEstados2018_listar":
                            dr = cx.Paginar("paSP_PacientesEstados2018_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;
                        case "PacientesCargueMasivoSemaforo_listar":
                            dr = cx.Paginar("paSP_PacientesCargueMasivoSemaforo_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "fechaFil", Request.Form["fechaFil"],
                                "tipo", Request.Form["tipo"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarPacienteLite":
                            dr = cx.Paginar("paSP_PacientesLite_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "tipoCargue", Request.Form["tipoCargue"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacienteHistorialAdministrativo":
                            dr = cx.Paginar("paSP_PacientesHistorialAdministrativo_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarSeguimientoPrioritarioHistorial":
                            dr = cx.Paginar("paSP_SeguimientoPrioritarioHistorial_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "fechaini", Request.Form["fechaini"],
                                "fechafin", Request.Form["fechafin"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarPacientesCargados":
                            dr = cx.Paginar("paSP_PacientesCargueMasivo_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "id_cargue", Request.Form["id_cargue"],
                                "Documento", Request.Form["Documento"],
                                "Nombre", Request.Form["Nombre"],
                                "Estado", Request.Form["Estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listadiasNoHabiles":
                            dr = cx.Paginar("paSP_DiasNoHabiles_lista",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "fecha", Request.Form["fecha"],
                                 "descripcion", Request.Form["descripcion"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacientesDuplicados":
                            dr = cx.Paginar("paSP_PacientesDuplicados_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacientesRechazados":
                            dr = cx.Paginar("paSP_PacientesRechazados_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarCitaMedica":
                            dr = cx.Paginar("paSP_Citamedica_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "especialidad", Request.Form["especialidad"],
                                "codigo", Request.Form["codigo"],
                                "nombre", Request.Form["nombre"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "PacientesContactar_listar":
                            dr = cx.Paginar("paSP_PacientesContactar_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "paSP_PacientesContactar_Repro_listar":
                            dr = cx.Paginar("paSP_PacientesContactar_Repro_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacienteEstado":
                            dr = cx.Paginar("paSP_PacientesEstados_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacienteEstadoINI_FIN":
                            dr = cx.Paginar("paSP_PacienteEstadoINI_FIN_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "paSP_PacienteNoAcepta_listar":
                            dr = cx.Paginar("paSP_PacienteNoAcepta_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacienteEstado_SinCobertura":
                            dr = cx.Paginar("paSP_PacientesEstados_SinCobertura_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacientesDatosErrados":
                            dr = cx.Paginar("paSP_Pacientes_DatosErrados_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacienteEstadoLite":
                            dr = cx.Paginar("paSP_PacientesEstadosLite_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacienteConsolidado":
                            dr = cx.Paginar("paSP_PacientesConsolidado_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacienteRecepcionLlamadas":
                            dr = cx.Paginar("paSP_PacientesRecepcionLlamadas_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "fechaInicio", Request.Form["fechaInicio"],
                                "fechaFin", Request.Form["fechaFin"],
                                "bandera", Request.Form["bandera"],
                                "tipoLlamada", Request.Form["tipoLlamada"],
                                "prioridad", Request.Form["prioridad"],
                                "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacientesGestionCitasAgosto":
                            dr = cx.Paginar("paSP_PacientesGestionCitasAgosto_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacienteNoContactable":
                            dr = cx.Paginar("paSP_PacNoContactables_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "id", Request.Form["id"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacienteEstado_noContactable":
                            dr = cx.Paginar("paSP_PacientesEstados_noContactable_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "estado", Request.Form["estado"],
                                "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarVersionDial":
                            dr = cx.Paginar("paSP_Dialogo_Version_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "id", Request.Form["id"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarVersionObs":
                            dr = cx.Paginar("paINI_Observacion_Version_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "id", Request.Form["id"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarDialogos":
                            dr = cx.Paginar("paSP_Dialogo_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "codigo", Request.Form["codigo"],
                                 "nombre", Request.Form["nombre"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarObservaciones":
                            dr = cx.Paginar("paINI_Observacion_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "codigo", Request.Form["codigo"],
                                 "nombre", Request.Form["nombre"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacientesDudosos":
                            dr = cx.Paginar("paSP_PacientesDudosos_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "nombreFil", Request.Form["nombreFil"],
                                 "documentoFil", Request.Form["documentoFil"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacientesDudososSeguiPer":
                            dr = cx.Paginar("paSP_PacientesDudososPeriodico_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "nombreFil", Request.Form["nombreFil"],
                                 "documentoFil", Request.Form["documentoFil"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarPacientesNoContac":
                            dr = cx.Paginar("paSP_PacientesNoContac_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "nombreFil", Request.Form["nombreFil"],
                                 "documentoFil", Request.Form["documentoFil"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarPacientesNoContacAnt":
                            dr = cx.Paginar("paSP_PacientesNoContacAnt_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "nombreFil", Request.Form["nombreFil"],
                                 "documentoFil", Request.Form["documentoFil"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        ///Histórico No Contactable

                        case "listarPacientesNoContacHistorial":
                            dr = cx.Paginar("paSP_PacientesNoContacHistorial_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "nombreFil", Request.Form["nombreFil"],
                                 "documentoFil", Request.Form["documentoFil"],
                                 "mes", Request.Form["mes"],
                                 "anio", Request.Form["anio"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listatramiteEstado":
                            dr = cx.Paginar("paSP_TramitesEstado_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "estado", Request.Form["estado"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listatramiteEstado_NOCUC":
                            dr = cx.Paginar("paSP_TramitesEstado_NOCUC_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "estado", Request.Form["estado"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        //Listar de Informar con filtro de fecha de programación

                        case "listatramiteEstado_Informar_NOCUC":
                            dr = cx.Paginar("paSP_TramitesEstado_Informar_NOCUC_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "estado", Request.Form["estado"],
                                 "fecha", Request.Form["fecha"],
                                 "opcion", Request.Form["opcion"],
                                  "servicio", Request.Form["servicio"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listatramiteEstado_Informar_NOCUC_listar":
                            dr = cx.Paginar("paSP_TramitesEstado_Informar_NOCUC_listar2",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "estado", Request.Form["estado"],
                                 "fecha", Request.Form["fecha"],
                                 "opcion", Request.Form["opcion"],
                                  "servicio", Request.Form["servicio"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listatramiteEstado_Informar_NOCUC2_listar":
                            dr = cx.Paginar("paSP_TramitesEstado_Informar_NOCUC2_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "estado", Request.Form["estado"],
                                 "fecha", Request.Form["fecha"],
                                 "opcion", Request.Form["opcion"],
                                  "servicio", Request.Form["servicio"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listatramiteEstado_Informar_CUC_listar":
                            dr = cx.Paginar("paSP_TramitesEstado_Informar_CUC_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "estado", Request.Form["estado"],
                                 "fecha", Request.Form["fecha"],
                                 "opcion", Request.Form["opcion"],
                                  "servicio", Request.Form["servicio"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listatramiteEstado_Informar_NOCUC_listarV2":
                            dr = cx.Paginar("paSP_TramitesEstado_Informar_NOCUC_listar3",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "estado", Request.Form["estado"],
                                 "fecha", Request.Form["fecha"],
                                 "opcion", Request.Form["opcion"],
                                  "servicio", Request.Form["servicio"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarTramitePacientesActivos":
                            dr = cx.Paginar("paSP_Tramites_InfoDetallada_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "idTramite", Request.Form["idTramite"],
                                 "docPaciente", Request.Form["docPaciente"],
                                 "requerimiento", Request.Form["requerimiento"],
                                 "estado", Request.Form["estado"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarTramitesGestionadosAgosto":
                            dr = cx.Paginar("paSP_TramitesGestionadosAgosto_InfoDetallada_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "idTramite", Request.Form["idTramite"],
                                 "docPaciente", Request.Form["docPaciente"],
                                 "requerimiento", Request.Form["requerimiento"],
                                 "estado", Request.Form["estado"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarTramitesGestionadosSeptiembre":
                            dr = cx.Paginar("paSP_TramitesGestionadosSeptiembre_InfoDetallada_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "idTramite", Request.Form["idTramite"],
                                 "docPaciente", Request.Form["docPaciente"],
                                 "requerimiento", Request.Form["requerimiento"],
                                 "estado", Request.Form["estado"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarDetalleTramitePacientesActivos":
                            dr = cx.Paginar("paSP_Tramites_PacientesActivos_detalle_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "idTramite", Request.Form["idTramite"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarRecorPaci":
                            dr = cx.Paginar("paSP_PacienteRecordar_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "estado", Request.Form["estado"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarRecorPaci_NOCUC":
                            dr = cx.Paginar("paSP_PacienteRecordar_NOCUC_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "estado", Request.Form["estado"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;


                        case "listarRecorPaci2_NOCUC":
                            dr = cx.Paginar("paSP_PacienteRecordar2_NOCUC_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "estado", Request.Form["estado"],
                                  "fecha", Request.Form["fecha"],
                                 "opcion", Request.Form["opcion"],
                                  "servicio", Request.Form["servicio"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listaResgistar2Llamada":
                            dr = cx.Paginar("paSP_Resgistar2Llamada_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "estado", Request.Form["estado"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarArchivosPaciente":
                            dr = cx.Paginar("paSP_ArchivosPaciente_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "bandera", Request.Form["bandera"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "ListarArchivosPacienteCorreos":
                            dr = cx.Paginar("paSP_ArchivosPacienteCorreos_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "globlaPaciente", Request.Form["globlaPaciente"],
                                 "globlaCorreo2", Request.Form["globlaCorreo2"],
                                 "tipoArchivo", Request.Form["tipoArchivo"],
                                 "fechaFiltro", Request.Form["fechaFiltro"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarRecordarCargue":
                            dr = cx.Paginar("paSP_ArchivosPaciente_Recordar_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "bandera", Request.Form["bandera"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarSinEnvioDocumentacion":
                            dr = cx.Paginar("paSP_ArchivosPaciente_sinEnvio_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "bandera", Request.Form["bandera"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaAnalisis":
                            dr = cx.Paginar("paSP_Analisis_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "doc", Request.Form["doc"],
                                 "nombre", Request.Form["nombre"],
                                 "fechaIni", Request.Form["fechaIni"],
                                 "fechaFin", Request.Form["fechaFin"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaAnalisisCUC":
                            dr = cx.Paginar("paSP_AnalisisCUC_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "doc", Request.Form["doc"],
                                 "nombre", Request.Form["nombre"],
                                 "fechaIni", Request.Form["fechaIni"],
                                 "fechaFin", Request.Form["fechaFin"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaAnalisisDiagnostico":
                            dr = cx.Paginar("paSP_AnalisisDiagnostico_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "doc", Request.Form["doc"],
                                 "nombre", Request.Form["nombre"],
                                 "fechaIni", Request.Form["fechaIni"],
                                 "fechaFin", Request.Form["fechaFin"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listaAnalisisDiagnostico2":
                            dr = cx.Paginar("paSP_AnalisisDiagnostico2_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "doc", Request.Form["doc"],
                                 "nombre", Request.Form["nombre"],
                                 "fechaIni", Request.Form["fechaIni"],
                                 "fechaFin", Request.Form["fechaFin"],
                                 "bandera", Request.Form["bandera"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaAnalisisDiagnostico3":
                            dr = cx.Paginar("paSP_AnalisisDiagnostico3_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "doc", Request.Form["doc"],
                                 "nombre", Request.Form["nombre"],
                                 "anio", Request.Form["anio"],
                                 "mes", Request.Form["mes"],
                                 "bandera", Request.Form["bandera"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarSinTramitePaciente":
                            dr = cx.Paginar("paSP_SinTramitePaciente_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarNuevoTramitePaciente":
                            dr = cx.Paginar("paSP_NuevoTramitePaciente_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarRechazados":
                            dr = cx.Paginar("paSP_Rechazados_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "doc", Request.Form["doc"],
                                 "nombre", Request.Form["nombre"],
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "fechainiRe", Request.Form["fechainiRe"],
                                 "fechafinRe", Request.Form["fechafinRe"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "Tramites_listar":
                            dr = cx.Paginar("paSP_Tramites_parametrizados_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "especialidad", Request.Form["especialidad"],
                                 "codigo", Request.Form["codigo"],
                                 "nombre", Request.Form["nombre"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "Osi_Sanitas_listar":
                            dr = cx.Paginar("paSP_Osi_Sanitas_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "especialidad", Request.Form["especialidad"],
                                 "codigo", Request.Form["codigo"],
                                 "nombre", Request.Form["nombre"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarciudades":
                            dr = cx.Paginar("paSP_Ciudades_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                "id", Request.Form["id"],
                                "ciudad", Request.Form["ciudad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listaValidarAprobar":
                            dr = cx.Paginar("paSP_ValidarAprobar_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listaValidarAprobarCUC2":
                            dr = cx.Paginar("paSP_ValidarAprobarCUC2_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "fechainifil", Request.Form["fechainifil"],
                                "fechafinfil", Request.Form["fechafinfil"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarAutorizacionCambioAutoCUC":
                            dr = cx.Paginar("paSP_AutorizacionesCambioAutoCUC_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "fechaini", Request.Form["fechaini"],
                                "fechafin", Request.Form["fechafin"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarAutorizacionCambioAuto":
                            dr = cx.Paginar("paSP_AutorizacionesCambioAuto_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "fechaini", Request.Form["fechaini"],
                                "fechafin", Request.Form["fechafin"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listaValidarAprobarNoCUC":
                            dr = cx.Paginar("paSP_ValidarAprobarNoCUC_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;


                        case "listaValidarAprobarCUC":
                            dr = cx.Paginar("paSP_ValidarAprobarCUC_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listaProgramarSolicitud":
                            dr = cx.Paginar("paSP_ProgramarSolicitud_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listaProgramarSolicitudCUC2":
                            dr = cx.Paginar("paCUC_ProgramarSolicitud2_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listaProgramarSolicitudOptimizado":
                            dr = cx.Paginar("paSP_ProgramarSolicitud_optimizado_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listaProgramarSolicitudAdmin2":
                            dr = cx.Paginar("paSP_ProgramarSolicitudAdmin2_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "tipo_gestion", Request.Form["tipo_gestion"],
                                "fechaini_gestion", Request.Form["fechaini_gestion"],
                                "fechafin_gestion", Request.Form["fechafin_gestion"],
                                "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;


                        case "listaHistorialCorreos":
                            dr = cx.Paginar("paSP_HistorialCorreos_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "tipo_gestion", Request.Form["tipo_gestion"],
                                "fechaini_gestion", Request.Form["fechaini_gestion"],
                                "fechafin_gestion", Request.Form["fechafin_gestion"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;



                        case "listaProgramarSolicitudAdmin2Correos":
                            dr = cx.Paginar("paSP_ProgramarSolicitudAdmin2Correos_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "tipo_gestion", Request.Form["tipo_gestion"],
                                "fechaini_gestion", Request.Form["fechaini_gestion"],
                                "fechafin_gestion", Request.Form["fechafin_gestion"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "CorreosContactoFinal":
                            dr = cx.Paginar("paSP_CorreosContactoFinal_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "tipo_gestion", Request.Form["tipo_gestion"],
                                "fechaini_gestion", Request.Form["fechaini_gestion"],
                                "fechafin_gestion", Request.Form["fechafin_gestion"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listaProgramarContactoFinal":
                            dr = cx.Paginar("paSP_ProgramarContactoFinal_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "fechaini", Request.Form["fechaini"],
                                "fechafin", Request.Form["fechafin"],
                                "numautorizacion", Request.Form["numautorizacion"],
                                "prestador", Request.Form["prestador"],
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "tipo_gestion", Request.Form["tipo_gestion"],
                                "fechaini_gestion", Request.Form["fechaini_gestion"],
                                "fechafin_gestion", Request.Form["fechafin_gestion"],
                                "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listaProgramacionesNoAsignadas":
                            dr = cx.Paginar("paSP_ProgramacionesNoAsignadas_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "tipo_gestion", Request.Form["tipo_gestion"],
                                "fechaini_gestion", Request.Form["fechaini_gestion"],
                                "fechafin_gestion", Request.Form["fechafin_gestion"],
                                "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;



                        case "listaSeguimientoProgramar":
                            dr = cx.Paginar("paSP_SeguimientoProgramar_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                "documento", Request.Form["documento"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;



                        case "listarSeguimientoPeriodico":
                            dr = cx.Paginar("paSP_SeguimientoPeriodico_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarSeguimientoPrioritario":
                            dr = cx.Paginar("paSP_SeguimientoPrioritario_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarDireccionamiento":
                            dr = cx.Paginar("paSP_CargueDireccionamiento_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "fechaini", Request.Form["fechaini"],
                                "fechafin", Request.Form["fechafin"],
                                "categoria", Request.Form["categoria"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listaReprogramaciones":
                            dr = cx.Paginar("paSP_Reprogramaciones_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;


                        case "listaReprogramacionesNOCUC":
                            dr = cx.Paginar("paSP_ReprogramacionesNOCUC_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listaProgramarSanitas":
                            dr = cx.Paginar("paSP_ProgramarCasosSanitas_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;


                        case "listaProgramarSanitasNOCUC":
                            dr = cx.Paginar("paSP_ProgramarCasosSanitasNOCUC_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;


                        case "listaProgramarAdmin":
                            dr = cx.Paginar("paSP_ProgramarAdmin_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarAutorizacionCTC":
                            dr = cx.Paginar("paSP_AutorizacionesCTC_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "prestador", Request.Form["prestador"],
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;


                        case "listarAutorizacionCTCCUC":
                            dr = cx.Paginar("paSP_AutorizacionesCTCCUC_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "prestador", Request.Form["prestador"],
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;



                        case "listarAutorizacionCTCNoCUC":
                            dr = cx.Paginar("paSP_AutorizacionesCTCNoCUC_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "prestador", Request.Form["prestador"],
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarAutorizacionNS":
                            dr = cx.Paginar("paSP_AutorizacionesNS_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "prestador", Request.Form["prestador"],
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarAutorizacionNSCUC":
                            dr = cx.Paginar("paSP_AutorizacionesNSCUC_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "prestador", Request.Form["prestador"],
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarAutorizacionNSNoCUC":
                            dr = cx.Paginar("paSP_AutorizacionesNSNoCUC_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "prestador", Request.Form["prestador"],
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarAutorizacionSAN":
                            dr = cx.Paginar("paSP_AutorizacionesCS_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "prestador", Request.Form["prestador"],
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarAutorizacionSANCUC":
                            dr = cx.Paginar("paSP_AutorizacionesCSCUC_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "prestador", Request.Form["prestador"],
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarAutorizacionSANNoCUC":
                            dr = cx.Paginar("paSP_AutorizacionesCSNoCUC_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "prestador", Request.Form["prestador"],
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarAutorizacionesReprog":
                            dr = cx.Paginar("paSP_AutorizacionesReprog_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarAutorizacionesReprogCUC":
                            dr = cx.Paginar("paSP_AutorizacionesReprogCUC_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;


                        case "listarAutorizacionesReprogNoCUC":
                            dr = cx.Paginar("paSP_AutorizacionesReprogNoCUC_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "Informar_Tramites_Pendientes_listar":
                            dr = cx.Paginar("paSP_Tramites_Hijos_Paginador2_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "id", Request.Form["id"],
                                 "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "Informar_Tramites_Pendientes_Admin_listar":
                            dr = cx.Paginar("paSP_Tramites_Hijos_Paginador_Admin_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "id", Request.Form["id"],
                                 "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "Recordar_Tramites_Pendientes_listar":
                            dr = cx.Paginar("paSP_Recordar_Tramites_Pendientes_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "id", Request.Form["id"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarSolicitudesPenVyA":
                            dr = cx.Paginar("paSP_SolicitudesPenVyA_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                "documento", Request.Form["documento"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarSolicitudesPenVyA_NOCUC":
                            dr = cx.Paginar("paSP_SolicitudesPenVyA_NOCUC_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                "documento", Request.Form["documento"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listaObservacionSPVyA":
                            dr = cx.Paginar("paSP_TramitesObservaciones_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "id", Request.Form["id"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarDirHospitalario":
                            dr = cx.Paginar("paSP_DirHospitalario_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                  "regional", Request.Form["regional"],
                                 "municipio", Request.Form["municipio"],
                                 "servicios", Request.Form["servicios"],
                                 "codigoPriOp", Request.Form["codigoPriOp"],
                                 "NombrePriOp ", Request.Form["NombrePriOp"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarDirRegional":
                            dr = cx.Paginar("paSP_DirRegional_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "municipio", Request.Form["municipio"],
                                 "uap", Request.Form["uap"],
                                 "servicio", Request.Form["servicio"],
                                 "codigoEPS ", Request.Form["codigoEPS"],
                                 "codigoALEA ", Request.Form["codigoALEA"],
                                 "descripcionEPS ", Request.Form["descripcionEPS"],
                                 "NitPrestador ", Request.Form["NitPrestador"],
                                 "Nombreprestador ", Request.Form["Nombreprestador"],
                                 "Regional ", Request.Form["Regional"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarDirMedicamentos":
                            dr = cx.Paginar("paSP_DirMedicamentos_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                  "regional", Request.Form["regional"],
                                 "municipio", Request.Form["municipio"],
                                 "codigoBH", Request.Form["codigoBH"],
                                 "medicamentos", Request.Form["medicamentos"],
                                 "codigoPres ", Request.Form["codigoPres"],
                                  "NombrePres ", Request.Form["NombrePres"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarDirNoPos":
                            dr = cx.Paginar("paSP_DirNoPos_listar",
                                "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "regional", Request.Form["regional"],
                                 "municipio", Request.Form["municipio"],
                                 "servicios", Request.Form["servicios"],
                                 "codigoPres", Request.Form["codigoPres"],
                                 "NombrePres ", Request.Form["NombrePres"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarDirOdontologico":
                            dr = cx.Paginar("paSP_DirOdontologico_listar",
                                "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                  "municipio", Request.Form["municipio"],
                                 "regional", Request.Form["regional"],
                                 "unidad", Request.Form["unidad"],
                                 "servicios", Request.Form["servicios"],
                                 "CodAlea", Request.Form["CodAlea"],
                                 "eps ", Request.Form["eps"],
                                 "cod_eps ", Request.Form["cod_eps"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listaUsuarios":
                            dr = cx.Paginar("paSP_Usuarios_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listaMedicoTratante":
                            dr = cx.Paginar("paSP_MedicoTratante_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "nombre", Request.Form["nombre"].ToUpper(),
                                "especialidad", Request.Form["especialidad"].ToUpper(),
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listaAnalisisPrimeravez":
                            dr = cx.Paginar("paSP_AnalisisPrimeraVez_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "doc", Request.Form["doc"],
                                 "nombre", Request.Form["nombre"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarRegionales":
                            dr = cx.Paginar("paSP_RegionalesSP_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "regional", Request.Form["regional"],
                                 "municipio", Request.Form["municipio"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarAsignarMunicipios":
                            dr = cx.Paginar("paSP_RegionalesAsignar_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "regional", Request.Form["regional"],
                                 "departamento", Request.Form["departamento"],
                                 "municipio", Request.Form["municipio"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarMunicipiosAsignados":
                            dr = cx.Paginar("paSP_RegionalesMunicipios_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "regional", Request.Form["regional"],
                                 "departamento", Request.Form["departamento"],
                                 "municipio", Request.Form["municipio"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaObservacionSPSEGUI":
                            dr = cx.Paginar("paSP_Tramites_observaciones_listar",
                                "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                "id", Request.Form["id"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarPrestadorServicio":
                            dr = cx.Paginar("paSP_PrestadorServicio_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "id", Request.Form["id"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarServicioEditar":
                            dr = cx.Paginar("paSP_PrestadorServicioEditar_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "id", Request.Form["id"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "Ordenes_listar":
                            dr = cx.Paginar("paSP_Ordenes_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "numcaso", Request.Form["numcaso"],
                                "titulo", Request.Form["titulo"],
                                 "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "Ordenes_2listar":
                            dr = cx.Paginar("paSP_Ordenes2_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "numcaso", Request.Form["numcaso"],
                                "titulo", Request.Form["titulo"],
                                 "estado", Request.Form["estado"],
                                 "motivo", Request.Form["motivo"],
                                 "prioridad", Request.Form["prioridad"],
                                  "solicitante", Request.Form["solicitante"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "Ordenes_Historial_listar":
                            dr = cx.Paginar("paSP_Ordenes_Historial_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "numcaso", Request.Form["numcaso"],
                                "titulo", Request.Form["titulo"],
                                 "estado", Request.Form["estado"],
                                 "motivo", Request.Form["motivo"],
                                 "prioridad", Request.Form["prioridad"],
                                 "solicitante", Request.Form["solicitante"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarInfoComplementaria":
                            dr = cx.Paginar("paSP_InfoComplementaria_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "id", Request.Form["id"],
                                "sede", Request.Form["sede"],
                                "codigo", Request.Form["codigo"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarInfoSedesPres":
                            dr = cx.Paginar("paSP_InfoSedesPres_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "idPres", Request.Form["idPres"],
                                "sede", Request.Form["sede"],
                                "codigo", Request.Form["codigo"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarServicioGeneral":
                            dr = cx.Paginar("paSP_ServicioGeneral_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "id", Request.Form["id"],
                                "sede", Request.Form["sede"],
                                "codigo", Request.Form["codigo"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarPrestadorSedeDetalle":
                            dr = cx.Paginar("paSP_PrestadoresSedeDetalle_listar",
                                 "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "id", Request.Form["id"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarPrestadoresSede":
                            dr = cx.Paginar("paSP_PrestadoresSede_listar",
                                 "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "prestador", Request.Form["prestador"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "paSP_PrestadoresSedeUap_listar":
                            dr = cx.Paginar("paSP_PrestadoresSedeUap_listar",
                                 "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "prestador", Request.Form["prestador"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;


                        case "listarPrestadoresIndicadores":
                            dr = cx.Paginar("paSP_PrestadoresIndicadores_listar",
                                 "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "prestador", Request.Form["prestador"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;


                        case "listaEspeMediTratante":
                            dr = cx.Paginar("paSP_EspeMediTratante_listar",
                                 "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarMedicamentos":
                            dr = cx.Paginar("paSP_Medicamentos_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "nombre", Request.Form["nombre"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listaAnalisisAntiguo":
                            dr = cx.Paginar("paSP_AnalisisAntiguo_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "doc", Request.Form["doc"],
                                 "nombre", Request.Form["nombre"],
                                 "fechaIni", Request.Form["fechaIni"],
                                 "fechaFin", Request.Form["fechaFin"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarPacientesnoContactablePer":
                            dr = cx.Paginar("paSP_PacientesnoContactablePer_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarNoContactablePer":
                            dr = cx.Paginar("paSP_NoContactablePer_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "PacientesNoContacPeriodico_listar":
                            dr = cx.Paginar("paSP_PacientesNoContacPeriodico_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "nombreFil", Request.Form["nombreFil"],
                                 "documentoFil", Request.Form["documentoFil"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "paSP_LlamarNCSeguiPer_listar":
                            dr = cx.Paginar("paSP_LlamarNCSeguiPer_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "nombreFil", Request.Form["nombreFil"],
                                 "documentoFil", Request.Form["documentoFil"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarDocumento":
                            dr = cx.Paginar("paSP_Documento_Paciente_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                "id", Request.Form["id"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listaObservacionArchivo":
                            dr = cx.Paginar("paSP_ArchivoTempObservacion_lista",
                                  "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                "id", Request.Form["id"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarDocumentoGeneral":
                            dr = cx.Paginar("paSP_DocumentoGeneral_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                "id", Request.Form["id"],
                                "tipoArchivo", Request.Form["tipoArchivo"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listaObservacionArchivoGeneral":
                            dr = cx.Paginar("paSP_ObservacionGeneral_lista",
                                  "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                "id", Request.Form["id"],
                                "tipoArch", Request.Form["tipoArch"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarPacientesCargueDoc":
                            dr = cx.Paginar("paSP_PacientesCargueDoc_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarArchivosGeneral":
                            dr = cx.Paginar("paSP_ArchivoGeneral_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "documento", Request.Form["documento"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarOrdenEspecialidad":
                            dr = cx.Paginar("paSP_OrdenEspecialidad_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                  "documento", Request.Form["documento"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listaCIE10ArchivoGen":
                            dr = cx.Paginar("paSP_CIE10ArchivoGen_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "codigo", Request.Form["codigo"],
                                "nombre", Request.Form["nombre"],
                                "documento", Request.Form["documento"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarHijosArchivo":
                            dr = cx.Paginar("paSP_HijosArchivo_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "tipo_arch", Request.Form["tipo_arch"],
                                "especialidad", Request.Form["especialidad"],
                                "codigo", Request.Form["codigo"],
                                "tipo_doc", Request.Form["tipo_doc"],
                                "documento", Request.Form["documento"],
                                "gestion", Request.Form["gestion"],
                                "fechaIni", Request.Form["fechaIni"],
                                "fechaFin", Request.Form["fechaFin"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarPacienteArchivos":
                            dr = cx.Paginar("paSP_PacienteArchivos_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                //"tipo_arch", Request.Form["tipo_arch"],
                                //"codigo", Request.Form["codigo"],
                                //"tipo_doc", Request.Form["tipo_doc"],
                                "documentoFil", Request.Form["documentoFil"],
                                 "nombreFil", Request.Form["nombreFil"],
                                //"fechaIni", Request.Form["fechaIni"],
                                //"fechaFin", Request.Form["fechaFin"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarArchivosInformar":
                            dr = cx.Paginar("paSP_ArchivosInformar_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "documento", Request.Form["documento"],
                                 "nombre", Request.Form["nombre"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarArchivosInformarV2":
                            dr = cx.Paginar("paSP_ArchivosInformarV2_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "documento", Request.Form["documento"],
                                 "nombre", Request.Form["nombre"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarArchivosInformarNoContactable":
                            dr = cx.Paginar("paSP_ArchivosInformarNoContactable_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarArchivosInformarNoContactableV2":
                            dr = cx.Paginar("paSP_ArchivosInformarNoContactableV2_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarArchivosNoInformar":
                            dr = cx.Paginar("paSP_ArchivosNoInformar_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "documentoFil", Request.Form["documentoFil"],
                                 "nombreFil", Request.Form["nombreFil"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarArchivosNoInformarV2":
                            dr = cx.Paginar("paSP_ArchivosNoInformarV2_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "documentoFil", Request.Form["documentoFil"],
                                 "nombreFil", Request.Form["nombreFil"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listaObservacionesInfoRech":
                            dr = cx.Paginar("paSP_ObservacionesInfoRech_lista",
                                  "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                "id", Request.Form["id"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarHistorialInformar":
                            dr = cx.Paginar("paSP_InformarHistorial_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "documento", Request.Form["documento"],
                                 "nombre", Request.Form["nombre"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarPacienteCUC":
                            dr = cx.Paginar("paSP_PacientesCUC_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacienteMeses":
                            dr = cx.Paginar("paSP_PacienteMeses_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "semanaFil", Request.Form["semanaFil"],
                                "bandera", Request.Form["bandera"],
                                "mes", Request.Form["mes"],
                                "anio", Request.Form["anio"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarPacienteIndefinidoC":
                            dr = cx.Paginar("paSP_PacientesIndefinidoC_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarPacienteIndefinidoF":
                            dr = cx.Paginar("paSP_PacientesIndefinidoF_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarPacientePrepagada":
                            dr = cx.Paginar("paSP_PacientesPrepagada_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarPresentacion":
                            dr = cx.Paginar("paSP_Presentacion_listar",
                            "numFilas", RegistrosAMostrar,
                            "filaInicial", pagActual,
                            "nombre", Request.Form["nombre"],
                            "empresa", empresa,
                            "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listaViaAdministracion":
                            dr = cx.Paginar("paSP_ViaAdministracion_Listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "nombreFil", Request.Form["nombreFil"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarUnidadBasePacienteMedicamento":
                            dr = cx.Paginar("paSP_listarUnidadBasePacienteMedicamento_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "nombreFiltro", Request.Form["nombreFiltro"].ToUpper(),
                                "nombre", Request.Form["nombre"].ToUpper(),
                                "GlobalMedicamento", Request.Form["GlobalMedicamento"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarMedicos":
                            dr = cx.Paginar("paSP_Medicos_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "nombre", Request.Form["nombre"].ToUpper(),
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listarListaChequeo":
                            dr = cx.Paginar("paSP_ListaChequeoDiagnostico_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "documento", Request.Form["documento"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarListaChequeoContactabilidad":
                            dr = cx.Paginar("paSP_ListaChequeoContactabilidad_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "documento", Request.Form["documento"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarbitacoraRoles":
                            dr = cx.Paginar("paSP_bitacoraRoles_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "fechaini", Request.Form["fechaini"],
                                  "fechafin", Request.Form["fechafin"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaAnalisisV3":
                            dr = cx.Paginar("paSP_AnalisisV3_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "doc", Request.Form["doc"],
                                 "nombre", Request.Form["nombre"],
                                 "fechaIni", Request.Form["fechaIni"],
                                 "fechaFin", Request.Form["fechaFin"],
                                "tipoArchivo", Request.Form["tipoArchivo"],
                                "tipo", Request.Form["tipo"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "listaAnalisisCUCV3":
                            dr = cx.Paginar("paSP_AnalisisCUCV3_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "doc", Request.Form["doc"],
                                 "nombre", Request.Form["nombre"],
                                 "fechaIni", Request.Form["fechaIni"],
                                 "fechaFin", Request.Form["fechaFin"],
                                 "tipoArchivo", Request.Form["tipoArchivo"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "PacientesDevolucionLlamadas":
                            dr = cx.Paginar("paSP_PacientesDevolucionLlamadas_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "fechaInicio", Request.Form["fechaInicio"],
                                "fechaFin", Request.Form["fechaFin"],
                                "bandera", Request.Form["bandera"],
                                "tipoLlamada", Request.Form["tipoLlamada"],
                                "prioridad", Request.Form["prioridad"],
                                "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "paSP_ContactoInicialActualP_listar":
                            dr = cx.Paginar("paSP_ContactoInicialActualP_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "semanaFil", Request.Form["semanaFil"],
                                "bandera", Request.Form["bandera"],
                                "mes", Request.Form["mes"],
                                "anio", Request.Form["anio"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "PacientesCUCDevolucionLlamadas":
                            dr = cx.Paginar("paSP_PacientesCUCDevolucionLlamadas_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "fechaInicio", Request.Form["fechaInicio"],
                                "fechaFin", Request.Form["fechaFin"],
                                "bandera", Request.Form["bandera"],
                                "tipoLlamada", Request.Form["tipoLlamada"],
                                "prioridad", Request.Form["prioridad"],
                                "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "paSP_ContactoFinalActualP_listar":
                            dr = cx.Paginar("paSP_ContactoFinalActualP_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "semanaFil", Request.Form["semanaFil"],
                                "bandera", Request.Form["bandera"],
                                "mes", Request.Form["mes"],
                                "anio", Request.Form["anio"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;



                        case "listarCarguePQR":
                            dr = cx.Paginar("paSP_CarguePQR_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "fecha", Request.Form["fecha"].ToUpper(),
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarInfoCarguePQR":
                            dr = cx.Paginar("paSP_CargueInfoPQR_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "id", Request.Form["id"].ToUpper(),
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "paSP_Cargue_Alterno_listar":
                            dr = cx.Paginar("paSP_Cargue_Alterno_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documento", Request.Form["documento"].ToUpper(),
                                  "tiposer", Request.Form["tiposer"].ToUpper(),
                                "correo", Request.Form["correo"].ToUpper(),
                                "ente", Request.Form["ente"].ToUpper(),

                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "paSP_Buzon_NoLegibles_listar":
                            dr = cx.Paginar("paSP_Buzon_NoLegibles_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documento", Request.Form["documento"].ToUpper(),
                                  "tiposer", Request.Form["tiposer"].ToUpper(),
                                "correo", Request.Form["correo"].ToUpper(),
                                "ente", Request.Form["ente"].ToUpper(),

                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "paSP_Buzon_Alterno_listar":
                            dr = cx.Paginar("paSP_Buzon_Alterno_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documento", Request.Form["documento"].ToUpper(),
                                  "tiposer", Request.Form["tiposer"].ToUpper(),
                                "correo", Request.Form["correo"].ToUpper(),
                                "ente", Request.Form["ente"].ToUpper(),

                                "bandera", Request.Form["bandera"].ToUpper(),

                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "UsuariosOperaciones_listar":
                            dr = cx.Paginar("paSP_UsuariosOperaciones_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "usuario", Request.Form["usuario"].ToUpper(),
                                "bnd", Request.Form["bnd"].ToUpper(),
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "paSP_ActividadesAsignadas_listar":
                            dr = cx.Paginar("paSP_ActividadesAsignadas_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "usuario", Request.Form["usuario"].ToUpper(),
                                "cat_llam", Request.Form["cat_llam"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "paSP_ActividadesHistorial_listar":
                            dr = cx.Paginar("paSP_ActividadesHistorial_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "usuario", Request.Form["usuario"].ToUpper(),
                                "cat_llam", Request.Form["cat_llam"],
                                "fecha_ini", Request.Form["fecha_ini"],
                                "fecha_fin", Request.Form["fecha_fin"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "AnalisisDocumentacion":
                            dr = cx.Paginar("paSP_AnalisisDocumentacion_listar",
                                 "numFilas", RegistrosAMostrar,
                                 "filaInicial", pagActual,
                                 "doc", Request.Form["doc"],
                                 "nombre", Request.Form["nombre"],
                                 "fechaIni", Request.Form["fechaIni"],
                                 "fechaFin", Request.Form["fechaFin"],
                                "tipoServicio", Request.Form["tipoServicio"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "paSP_archivo_radicado_listado":
                            dr = cx.Paginar("paSP_archivo_radicado_listado",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "radicado", Request.Form["radicado"].ToUpper(), 
                                "bandera", Request.Form["bandera"].ToUpper(),  
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "paSP_Gestion_Documentos_listado":
                            dr = cx.Paginar("paSP_Gestion_Documentos_listado",
                                 "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "radicado", Request.Form["radicado"],
                                "bandera", Request.Form["bandera"],
                                "globalIdCarpeta", Request.Form["globalIdCarpeta"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "paSP_Gestion_Documentos_listado2":
                            dr = cx.Paginar("paSP_Gestion_Documentos_listado2",
                                 "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "radicado", Request.Form["radicado"],
                                "bandera", Request.Form["bandera"],
                                "globalIdCarpeta", Request.Form["globalIdCarpeta"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "paSP_area_pqr_listar":
                            dr = cx.Paginar("paSP_area_pqr_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "radicado", Request.Form["radicado"].ToUpper(),
                                "documento", Request.Form["documento"].ToUpper(),
                                "bandera", Request.Form["bandera"].ToUpper(),
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "paSP_Reporte_Dudoso_NoAcepta_listar":
                            dr = cx.Paginar("paSP_Reporte_Dudoso_NoAcepta_listar",
                                "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;
                    }
                    break;
            }

            int dato = -1;
            int numCols = 0;
            int numRows = 0;

            try
            {
                numCols = dr.FieldCount;
                while (dr.Read())
                {
                    dato = 1;
                    for (int i = 0; i < numCols; i++)
                    {
                        if (retorno.Equals(""))
                        {
                            retorno += "'" + dr.GetValue(i).ToString().Trim() + "'";
                        }
                        else
                        {
                            retorno += ",'" + dr.GetValue(i).ToString().Trim() + "'";
                        }
                    }
                    numRows++;
                }
            }
            catch (Exception er)
            {
                retorno = er.ToString();
            }

            cx.Desconectar();
            if (dato == -1)
            {
                Response.Write("0");
            }
            else
            {
                retorno = "'rows':" + numRows + ", 'cols':" + numCols + ",'data':[" + retorno + "]";
            }
            cx = null;
            cx = new Inicial.Modelo.ConexionBD_Sql_Server();
            int NroRegistros = -1;


            switch (cx.tipoConexion())
            {
                case "ORACLE":
                    switch (p)
                    {

                        case "listarGrupoRol":
                            NroRegistros = cx.NumeroRegistros("PKG_ROLES.numRoles",
                                "varchar2", responsable);
                            break;
                    }
                    break;

                case "SQL_SERVER":
                    switch (p)
                    {

                        case "auditoriaRol":
                            NroRegistros = cx.NumeroRegistros("numAudRoles", "responsable", Request.Form["responsable"].ToUpper(),
                                "accion", Request.Form["accion"].ToUpper(),
                                "rol", Request.Form["rol"].ToUpper(),
                                "empresa", empresa,
                                "creador", responsable);
                            break;

                        case "auditoriaUsuario":
                            NroRegistros = cx.NumeroRegistros("numAudUsuarios", "responsable", Request.Form["responsable"].ToUpper(),
                                "accion", Request.Form["accion"].ToUpper(),
                                "usuario", Request.Form["usuario"].ToUpper(),
                                "empresa", empresa,
                                "creador", responsable);
                            break;

                        case "listarGrupoRol":
                            NroRegistros = cx.NumeroRegistros("paINI_Roles_numlistar",
                                "rol", Request.Form["rol"],
                                "nivel", Request.Form["nivel"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaGrupoUsuario":
                            NroRegistros = cx.NumeroRegistros("paINI_Usuarios_numListado",
                                "nombre", Request.Form["nombre"].ToUpper(),
                                "usuario", Request.Form["usuario"].ToUpper(),
                                "correo", Request.Form["correo"].ToUpper(),
                                "empresa", empresa,
                                "creador", responsable);
                            break;

                        case "conectados":
                            NroRegistros = cx.NumeroRegistros("paINI_UsuariosConectados_num", "empresa", empresa);
                            break;

                        case "empresa":
                            NroRegistros = cx.NumeroRegistros("paINI_num_EmpresasListar",
                                 "nit", Request.Form["nit"],
                                "nombre", Request.Form["nombre"],
                                "responsable", responsable);
                            break;


                        case "listaAreas":
                            NroRegistros = cx.NumeroRegistros("paINI_Areas_numlistar",
                            "nombre", Request.Form["nombre"],
                            "empresa", empresa,
                            "responsable", responsable);
                            break;

                        case "listaTodosCargos":
                            NroRegistros = cx.NumeroRegistros("paINI_Cargos_numListar",
                                "nombreFiltro", Request.Form["nombreFiltro"],
                                "area", Request.Form["area"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarGrupos":
                            NroRegistros = cx.NumeroRegistros("paINI_Grupos_numListar",
                                "nombre", Request.Form["nombre"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;



                        case "listaAuditorias":
                            NroRegistros = cx.NumeroRegistros("numAuditorias",
                                "fechaInicio", Request.Form["fechaInicio"],
                                "fechaFin", Request.Form["fechaFin"],
                                "responsableFiltro", Request.Form["responsableFiltro"],
                                "objeto", Request.Form["objeto"],
                                 "accion", Request.Form["accion"],
                                "empresa", empresa,
                                "responsable", responsable
                                );
                            break;


                        case "listaDepartamentos":
                            NroRegistros = cx.NumeroRegistros("paINI_DepartamentosEmpresas_numListar",
                                "nombre", Request.Form["nombre"],
                                "empresa", empresa,
                                "responsable", responsable
                                );
                            break;

                        case "listaAreasEmpresa":
                            NroRegistros = cx.NumeroRegistros("paINI_AreasEmpresa_numlista",
                                "nombre", Request.Form["nombre"].ToUpper(),
                                "departamento", Request.Form["departamento"].ToUpper(),
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaAcividadesPaginado":
                            NroRegistros = cx.NumeroRegistros("paINI_ActividadesEconomicas_numlistado",

                                //"fechaInicio", Request.Form["fechaInicio"],
                                //"fechaFin", Request.Form["fechaFin"],
                                "nombreCodigoFil", Request.Form["codigo"],
                                "especialiadadFil", Request.Form["nombre"],

                                "empresa", empresa,
                                "responsable", responsable
                                );
                            break;

                        case "listaFacturacion":
                            NroRegistros = cx.NumeroRegistros("paINI_facturacion_numlistado",
                                "empresa", Request.Form["empresa"],
                                "responsable", responsable
                                );
                            break;

                        case "listarespecialidadesSP":
                            NroRegistros = cx.NumeroRegistros("paSP_especialidades_numlistado",
                                "especialidad", Request.Form["especialidad"].ToUpper(),
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPrestadoresSP":
                            NroRegistros = cx.NumeroRegistros("paSP_PrestadoresSP_num",
                                "prestador", Request.Form["prestador"].ToUpper(),
                                "nivel", Request.Form["nivel"].ToUpper(),
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPrestadores":
                            NroRegistros = cx.NumeroRegistros("paSP_PrestadoresSPNu_num",
                                "prestador", Request.Form["prestador"].ToUpper(),
                                "nivel", Request.Form["nivel"].ToUpper(),
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaCIE10PacI":
                            NroRegistros = cx.NumeroRegistros("paSP_CIE10_numlista",
                                "codigo", Request.Form["codigo"].ToUpper(),
                                "nombre", Request.Form["nombre"].ToUpper(),
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarCategoriaCie10":
                            NroRegistros = cx.NumeroRegistros("paSP_CategoriaCie10_numlista",
                                "codigo", Request.Form["codigo"].ToUpper(),
                                "nombre", Request.Form["nombre"].ToUpper(),
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaOcupacionPacI":
                            NroRegistros = cx.NumeroRegistros("paSP_OcupacionPacI_num",
                                "codigo", Request.Form["codigo"].ToUpper(),
                                "nombre", Request.Form["nombre"].ToUpper(),
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarCargueMasivo":
                            NroRegistros = cx.NumeroRegistros("paSP_CargueMasivo_numlistado",
                                "fecha", Request.Form["fecha"].ToUpper(),
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPaciente":
                            NroRegistros = cx.NumeroRegistros("paSP_Pacientes_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "tipoCargue", Request.Form["tipoCargue"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarPacienteXcargue":
                            if (ctlGeneral.tGlobal == "-1" || Request.Form["tipoCargue"] != "-1")
                            {
                                ctlGeneral.tGlobal = Request.Form["tipoCargue"];
                            }
                            if (ctlGeneral.cGlobal == "-1" || Request.Form["semaFil"] != "-1")
                            {
                                ctlGeneral.cGlobal = Request.Form["semaFil"];
                            }
                            if (ctlGeneral.mGlobal == "-1" || Request.Form["mesFil"] != "-1")
                            {
                                ctlGeneral.mGlobal = Request.Form["mesFil"];
                            }
                            if (ctlGeneral.aGlobal == "-1" || Request.Form["anioFil"] != "-1")
                            {
                                ctlGeneral.aGlobal = Request.Form["anioFil"];
                            }



                            //if (Request.Form["tipoCargue"] == "-1" && Request.Form["semaFil"] == "-1" && Request.Form["mesFil"] == "-1" && Request.Form["anioFil"] == "-1")
                            //{
                            //    ctlGeneral.tGlobal = "-1";
                            //    ctlGeneral.cGlobal = "-1";
                            //    ctlGeneral.mGlobal = "-1";
                            //    ctlGeneral.aGlobal = "-1";

                            //}


                            NroRegistros = cx.NumeroRegistros("paSP_PacientesXcargue_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "tipoCargue", Request.Form["tipoCargue"],
                                "anioFil", ctlGeneral.aGlobal,
                                "mesFil", ctlGeneral.mGlobal,
                                "semaFil", ctlGeneral.cGlobal,
                                "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            //ctlGeneral.cGlobal = "-1";
                            //ctlGeneral.tGlobal = "-1";
                            //ctlGeneral.mGlobal = "-1";
                            //ctlGeneral.aGlobal = "-1";
                            break;
                        case "paPRO_Pacientes_listar":
                            NroRegistros = cx.NumeroRegistros("paPRO_Pacientes_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "fechaFil", Request.Form["fechaFil"],
                                "tipo", Request.Form["tipo"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "paSP_PacientesEstados2018_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesEstados2018_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "PacientesCargueMasivoSemaforo_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesCargueMasivoSemaforo_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "fechaFil", Request.Form["fechaFil"],
                                "tipo", Request.Form["tipo"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacienteLite":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesLite_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "tipoCargue", Request.Form["tipoCargue"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacienteHistorialAdministrativo":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesHistorialAdministrativo_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarSeguimientoPrioritarioHistorial":
                            NroRegistros = cx.NumeroRegistros("paSP_SeguimientoPrioritarioHistorial_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "fechaini", Request.Form["fechaini"],
                                "fechafin", Request.Form["fechafin"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarPacientesDuplicados":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesDuplicados_num",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacientesRechazados":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesRechazados_num",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarPacientesCargados":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesCargueMasivo_numlistado",
                                "id_cargue", Request.Form["id_cargue"],
                                "Documento", Request.Form["Documento"],
                                "Nombre", Request.Form["Nombre"],
                                "Estado", Request.Form["Estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listadiasNoHabiles":
                            NroRegistros = cx.NumeroRegistros("paSP_DiasNoHabiles_numlistado",
                                 "fecha", Request.Form["fecha"],
                                "descripcion", Request.Form["descripcion"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarCitaMedica":
                            NroRegistros = cx.NumeroRegistros("paSP_Citamedica_num",
                                "especialidad", Request.Form["especialidad"],
                                "codigo", Request.Form["codigo"],
                                "nombre", Request.Form["nombre"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "PacientesContactar_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesContactar__numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "paSP_PacientesContactar_Repro_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesContactar_Repro_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacienteEstado":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesEstados_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarPacienteEstadoINI_FIN":
                            NroRegistros = cx.NumeroRegistros("paSP_PacienteEstadoINI_FIN_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "paSP_PacienteNoAcepta_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_PacienteNoAcepta_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacienteEstado_SinCobertura":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesEstados_SinCobertura_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacientesDatosErrados":
                            NroRegistros = cx.NumeroRegistros("paSP_Pacientes_DatosErrados_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacienteEstadoLite":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesEstadosLite_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacienteConsolidado":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesConsolidado_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacienteRecepcionLlamadas":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesRecepcionLlamadas_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "FechaInicio", Request.Form["FechaInicio"],
                                "FechaFin", Request.Form["FechaFin"],
                                "bandera", Request.Form["bandera"],
                                "tipoLlamada", Request.Form["tipoLlamada"],
                                "prioridad", Request.Form["prioridad"],
                                "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacientesGestionCitasAgosto":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesGestionCitasAgosto_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacienteNoContactable":
                            NroRegistros = cx.NumeroRegistros("paSP_PacNoContactables_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "id", Request.Form["id"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacienteEstado_noContactable":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesEstados_noContactable_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "estado", Request.Form["estado"],
                                  "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarDialogos":
                            NroRegistros = cx.NumeroRegistros("paSP_Dialogo_num",
                                 "codigo", Request.Form["codigo"],
                                 "nombre", Request.Form["nombre"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarObservaciones":
                            NroRegistros = cx.NumeroRegistros("paINI_Observacion_num",
                                 "codigo", Request.Form["codigo"],
                                 "nombre", Request.Form["nombre"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarVersionDial":
                            NroRegistros = cx.NumeroRegistros("paSP_Dialogo_Version_num",
                                 "id", Request.Form["id"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarVersionObs":
                            NroRegistros = cx.NumeroRegistros("paINI_Observacion_Version_num",
                                 "id", Request.Form["id"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacientesDudosos":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesDudosos_num",
                                 "nombreFil", Request.Form["nombreFil"],
                                 "documentoFil", Request.Form["documentoFil"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacientesDudososSeguiPer":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesDudososPeriodico_num",
                                 "nombreFil", Request.Form["nombreFil"],
                                 "documentoFil", Request.Form["documentoFil"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacientesNoContac":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesNoContac_num",
                                 "nombreFil", Request.Form["nombreFil"],
                                 "documentoFil", Request.Form["documentoFil"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacientesNoContacAnt":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesNoContacAnt_num",
                                 "nombreFil", Request.Form["nombreFil"],
                                 "documentoFil", Request.Form["documentoFil"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        ///Histórico No Contactable

                        case "listarPacientesNoContacHistorial":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesNoContacHistorial_num",
                                 "nombreFil", Request.Form["nombreFil"],
                                 "documentoFil", Request.Form["documentoFil"],
                                 "mes", Request.Form["mes"],
                                 "anio", Request.Form["anio"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listatramiteEstado":
                            NroRegistros = cx.NumeroRegistros("paSP_TramitesEstado_numlistado",
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "estado", Request.Form["estado"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listatramiteEstado_NOCUC":
                            NroRegistros = cx.NumeroRegistros("paSP_TramitesEstado_NOCUC_numlistado",
                                "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "estado", Request.Form["estado"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listatramiteEstado_Informar_NOCUC":
                            NroRegistros = cx.NumeroRegistros("paSP_TramitesEstado_Informar_NOCUC_numlistado",
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "estado", Request.Form["estado"],
                                   "fecha", Request.Form["fecha"],
                                 "opcion", Request.Form["opcion"],
                                 "servicio", Request.Form["servicio"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listatramiteEstado_Informar_NOCUC_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_TramitesEstado_Informar_NOCUC_numlistado2",
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "estado", Request.Form["estado"],
                                   "fecha", Request.Form["fecha"],
                                 "opcion", Request.Form["opcion"],
                                 "servicio", Request.Form["servicio"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listatramiteEstado_Informar_NOCUC2_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_TramitesEstado_Informar_NOCUC2_numlistado",
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "estado", Request.Form["estado"],
                                   "fecha", Request.Form["fecha"],
                                 "opcion", Request.Form["opcion"],
                                 "servicio", Request.Form["servicio"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listatramiteEstado_Informar_CUC_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_TramitesEstado_Informar_CUC_numlistado",
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "estado", Request.Form["estado"],
                                   "fecha", Request.Form["fecha"],
                                 "opcion", Request.Form["opcion"],
                                 "servicio", Request.Form["servicio"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listatramiteEstado_Informar_NOCUC_listarV2":
                            NroRegistros = cx.NumeroRegistros("paSP_TramitesEstado_Informar_NOCUC_numlistado3",
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "estado", Request.Form["estado"],
                                   "fecha", Request.Form["fecha"],
                                 "opcion", Request.Form["opcion"],
                                 "servicio", Request.Form["servicio"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarTramitePacientesActivos":
                            NroRegistros = cx.NumeroRegistros("paSP_Tramites_InfoDetallada_numlistado",
                                 "idTramite", Request.Form["idTramite"],
                                 "docPaciente", Request.Form["docPaciente"],
                                 "requerimiento", Request.Form["requerimiento"],
                                 "estado", Request.Form["estado"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarTramitesGestionadosAgosto":
                            NroRegistros = cx.NumeroRegistros("paSP_TramitesGestionadosAgosto_InfoDetallada_numlistado",
                                 "idTramite", Request.Form["idTramite"],
                                 "docPaciente", Request.Form["docPaciente"],
                                 "requerimiento", Request.Form["requerimiento"],
                                 "estado", Request.Form["estado"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarTramitesGestionadosSeptiembre":
                            NroRegistros = cx.NumeroRegistros("paSP_TramitesGestionadosSeptiembre_InfoDetallada_numlistado",
                                 "idTramite", Request.Form["idTramite"],
                                 "docPaciente", Request.Form["docPaciente"],
                                 "requerimiento", Request.Form["requerimiento"],
                                 "estado", Request.Form["estado"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarDetalleTramitePacientesActivos":
                            NroRegistros = cx.NumeroRegistros("paSP_Tramites_PacientesActivos_detalle_numlistado",
                                 "idTramite", Request.Form["idTramite"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarRecorPaci":
                            NroRegistros = cx.NumeroRegistros("paSP_PacienteRecordar_numlistado",
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "estado", Request.Form["estado"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarRecorPaci_NOCUC":
                            NroRegistros = cx.NumeroRegistros("paSP_PacienteRecordar_NOCUC_numlistado",
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "estado", Request.Form["estado"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarRecorPaci2_NOCUC":
                            NroRegistros = cx.NumeroRegistros("paSP_PacienteRecordar2_NOCUC_numlistado",
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "estado", Request.Form["estado"],
                                   "fecha", Request.Form["fecha"],
                                 "opcion", Request.Form["opcion"],
                                  "servicio", Request.Form["servicio"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listaResgistar2Llamada":
                            NroRegistros = cx.NumeroRegistros("paSP_Resgistar2Llamada_numlistado",
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "estado", Request.Form["estado"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarArchivosPaciente":
                            NroRegistros = cx.NumeroRegistros("paSP_ArchivosPaciente_num",
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "bandera", Request.Form["bandera"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "ListarArchivosPacienteCorreos":
                            NroRegistros = cx.NumeroRegistros("paSP_ArchivosPacienteCorreos_numlistado",
                                 "globlaPaciente", Request.Form["globlaPaciente"],
                                 "globlaCorreo2", Request.Form["globlaCorreo2"],
                                 "tipoArchivo", Request.Form["tipoArchivo"],
                                 "fechaFiltro", Request.Form["fechaFiltro"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarRecordarCargue":
                            NroRegistros = cx.NumeroRegistros("paSP_ArchivosPaciente_Recordar_numlistado",
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "bandera", Request.Form["bandera"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarSinEnvioDocumentacion":
                            NroRegistros = cx.NumeroRegistros("paSP_ArchivosPaciente_sinEnvio_numlistado",
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "bandera", Request.Form["bandera"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaAnalisis":
                            NroRegistros = cx.NumeroRegistros("paSP_Analisis_numlistado",
                                 "doc", Request.Form["doc"],
                                 "nombre", Request.Form["nombre"],
                                  "fechaIni", Request.Form["fechaIni"],
                                 "fechaFin", Request.Form["fechaFin"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listaAnalisisCUC":
                            NroRegistros = cx.NumeroRegistros("paSP_AnalisisCUC_numlistado",
                                 "doc", Request.Form["doc"],
                                 "nombre", Request.Form["nombre"],
                                  "fechaIni", Request.Form["fechaIni"],
                                 "fechaFin", Request.Form["fechaFin"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;
                        case "listaAnalisisDiagnostico":
                            NroRegistros = cx.NumeroRegistros("paSP_AnalisisDiagnostico_numlistado",
                                 "doc", Request.Form["doc"],
                                 "nombre", Request.Form["nombre"],
                                  "fechaIni", Request.Form["fechaIni"],
                                 "fechaFin", Request.Form["fechaFin"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listaAnalisisDiagnostico2":
                            NroRegistros = cx.NumeroRegistros("paSP_AnalisisDiagnostico2_numlistado",
                                 "doc", Request.Form["doc"],
                                 "nombre", Request.Form["nombre"],
                                  "fechaIni", Request.Form["fechaIni"],
                                 "fechaFin", Request.Form["fechaFin"],
                                 "bandera", Request.Form["bandera"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listaAnalisisDiagnostico3":
                            NroRegistros = cx.NumeroRegistros("paSP_AnalisisDiagnostico3_numlistado",
                                 "doc", Request.Form["doc"],
                                 "nombre", Request.Form["nombre"],
                                  "anio", Request.Form["anio"],
                                 "mes", Request.Form["mes"],
                                 "bandera", Request.Form["bandera"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaAnalisisPrimeravez":
                            NroRegistros = cx.NumeroRegistros("paSP_AnalisisPrimeraVez_numlistado",
                                 "doc", Request.Form["doc"],
                                 "nombre", Request.Form["nombre"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarSinTramitePaciente":
                            NroRegistros = cx.NumeroRegistros("paSP_SinTramitePaciente_num",
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarNuevoTramitePaciente":
                            NroRegistros = cx.NumeroRegistros("paSP_NuevoTramitePaciente_num",
                                 "nombre", Request.Form["nombre"],
                                 "documento", Request.Form["documento"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarRechazados":
                            NroRegistros = cx.NumeroRegistros("paSP_Rechazados_numlistado",
                                 "doc", Request.Form["doc"],
                                 "nombre", Request.Form["nombre"],
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "fechainiRe", Request.Form["fechainiRe"],
                                 "fechafinRe", Request.Form["fechafinRe"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "Tramites_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_Tramites_parametrizados_numlistado",
                                "especialidad", Request.Form["especialidad"],
                                "codigo", Request.Form["codigo"],
                                 "nombre", Request.Form["nombre"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "Osi_Sanitas_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_Osi_Sanitas_numlistado",
                                "especialidad", Request.Form["especialidad"],
                                "codigo", Request.Form["codigo"],
                                 "nombre", Request.Form["nombre"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarciudades":
                            NroRegistros = cx.NumeroRegistros("paSP_Ciudades_numlistado",
                                "id", Request.Form["id"],
                                "ciudad", Request.Form["ciudad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaValidarAprobar":
                            NroRegistros = cx.NumeroRegistros("paSP_ValidarAprobar_numlistado",
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listaValidarAprobarCUC2":
                            NroRegistros = cx.NumeroRegistros("paSP_ValidarAprobarCUC2_numlistado",
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                    "fechainifil", Request.Form["fechainifil"],
                                "fechafinfil", Request.Form["fechafinfil"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarAutorizacionCambioAutoCUC":
                            NroRegistros = cx.NumeroRegistros("paSP_AutorizacionesCambioAutoCUC_numlistado",
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "fechaini", Request.Form["fechaini"],
                                "fechafin", Request.Form["fechafin"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarAutorizacionCambioAuto":
                            NroRegistros = cx.NumeroRegistros("paSP_AutorizacionesCambioAuto_numlistado",
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "fechaini", Request.Form["fechaini"],
                                "fechafin", Request.Form["fechafin"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaValidarAprobarNoCUC":
                            NroRegistros = cx.NumeroRegistros("paSP_ValidarAprobarNoCUC_numlistado",
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaValidarAprobarCUC":
                            NroRegistros = cx.NumeroRegistros("paSP_ValidarAprobarCUC_numlistado",
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaProgramarSolicitud":
                            NroRegistros = cx.NumeroRegistros("paSP_ProgramarSolicitud_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaProgramarSolicitudCUC2":
                            NroRegistros = cx.NumeroRegistros("paCUC_ProgramarSolicitud2_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listaProgramarSolicitudOptimizado":
                            NroRegistros = cx.NumeroRegistros("paSP_ProgramarSolicitud_optimizado_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaProgramarSolicitudAdmin2":
                            NroRegistros = cx.NumeroRegistros("paSP_ProgramarSolicitudAdmin2_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "tipo_gestion", Request.Form["tipo_gestion"],
                                "fechaini_gestion", Request.Form["fechaini_gestion"],
                                "fechafin_gestion", Request.Form["fechafin_gestion"],
                                "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaHistorialCorreos":
                            NroRegistros = cx.NumeroRegistros("paSP_HistorialCorreos_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "tipo_gestion", Request.Form["tipo_gestion"],
                                "fechaini_gestion", Request.Form["fechaini_gestion"],
                                "fechafin_gestion", Request.Form["fechafin_gestion"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaProgramarContactoFinal":
                            NroRegistros = cx.NumeroRegistros("paSP_ProgramarContactoFinal_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "tipo_gestion", Request.Form["tipo_gestion"],
                                "fechaini_gestion", Request.Form["fechaini_gestion"],
                                "fechafin_gestion", Request.Form["fechafin_gestion"],
                                "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listaProgramarSolicitudAdmin2Correos":
                            NroRegistros = cx.NumeroRegistros("paSP_ProgramarSolicitudAdmin2Correos_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "tipo_gestion", Request.Form["tipo_gestion"],
                                "fechaini_gestion", Request.Form["fechaini_gestion"],
                                "fechafin_gestion", Request.Form["fechafin_gestion"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "CorreosContactoFinal":
                            NroRegistros = cx.NumeroRegistros("paSP_CorreosContactoFinal_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "tipo_gestion", Request.Form["tipo_gestion"],
                                "fechaini_gestion", Request.Form["fechaini_gestion"],
                                "fechafin_gestion", Request.Form["fechafin_gestion"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;



                        case "listaProgramacionesNoAsignadas":
                            NroRegistros = cx.NumeroRegistros("paSP_ProgramacionesNoAsignadas_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "tipo_gestion", Request.Form["tipo_gestion"],
                                "fechaini_gestion", Request.Form["fechaini_gestion"],
                                "fechafin_gestion", Request.Form["fechafin_gestion"],
                                "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listaSeguimientoProgramar":
                            NroRegistros = cx.NumeroRegistros("paSP_SeguimientoProgramar_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                 "documento", Request.Form["documento"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarSeguimientoPeriodico":
                            NroRegistros = cx.NumeroRegistros("paSP_SeguimientoPeriodico_numlistado",
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarSeguimientoPrioritario":
                            NroRegistros = cx.NumeroRegistros("paSP_SeguimientoPrioritario_numlistado",
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarDireccionamiento":
                            NroRegistros = cx.NumeroRegistros("paSP_CargueDireccionamiento_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                "fechafin", Request.Form["fechafin"],
                                "categoria", Request.Form["categoria"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaReprogramaciones":
                            NroRegistros = cx.NumeroRegistros("paSP_Reprogramaciones_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listaReprogramacionesNOCUC":
                            NroRegistros = cx.NumeroRegistros("paSP_ReprogramacionesNOCUC_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaProgramarSanitas":
                            NroRegistros = cx.NumeroRegistros("paSP_ProgramarCasosSanitas_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listaProgramarSanitasNOCUC":
                            NroRegistros = cx.NumeroRegistros("paSP_ProgramarCasosSanitasNOCUC_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "nom_tramite", Request.Form["nom_tramite"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaProgramarAdmin":
                            NroRegistros = cx.NumeroRegistros("paSP_ProgramarAdmin_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "numautorizacion", Request.Form["numautorizacion"],
                                 "prestador", Request.Form["prestador"],
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarAutorizacionCTC":
                            NroRegistros = cx.NumeroRegistros("paSP_AutorizacionesCTC_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "prestador", Request.Form["prestador"],
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarAutorizacionCTCCUC":
                            NroRegistros = cx.NumeroRegistros("paSP_AutorizacionesCTCCUC_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "prestador", Request.Form["prestador"],
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarAutorizacionCTCNoCUC":
                            NroRegistros = cx.NumeroRegistros("paSP_AutorizacionesCTCNoCUC_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "prestador", Request.Form["prestador"],
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "Informar_Tramites_Pendientes_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_Tramites_Hijos_Paginador2_numlistado",
                                 "id", Request.Form["id"],
                                 "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "Informar_Tramites_Pendientes_Admin_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_Tramites_Hijos_Paginador_Admin_numlistado",
                                 "id", Request.Form["id"],
                                 "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "Recordar_Tramites_Pendientes_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_Recordar_Tramites_Pendientes_numlistado",
                                 "id", Request.Form["id"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarAutorizacionNS":
                            NroRegistros = cx.NumeroRegistros("paSP_AutorizacionesNS_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "prestador", Request.Form["prestador"],
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarAutorizacionNSCUC":
                            NroRegistros = cx.NumeroRegistros("paSP_AutorizacionesNSCUC_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "prestador", Request.Form["prestador"],
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarAutorizacionNSNoCUC":
                            NroRegistros = cx.NumeroRegistros("paSP_AutorizacionesNSNoCUC_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "prestador", Request.Form["prestador"],
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarAutorizacionSAN":
                            NroRegistros = cx.NumeroRegistros("paSP_AutorizacionesCS_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "prestador", Request.Form["prestador"],
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarAutorizacionSANCUC":
                            NroRegistros = cx.NumeroRegistros("paSP_AutorizacionesCSCUC_numlistado",
                                "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "prestador", Request.Form["prestador"],
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarAutorizacionSANNoCUC":
                            NroRegistros = cx.NumeroRegistros("paSP_AutorizacionesCSNoCUC_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "prestador", Request.Form["prestador"],
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarAutorizacionesReprog":
                            NroRegistros = cx.NumeroRegistros("paSP_AutorizacionesReprog_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarAutorizacionesReprogCUC":
                            NroRegistros = cx.NumeroRegistros("paSP_AutorizacionesReprogCUC_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarAutorizacionesReprogNoCUC":
                            NroRegistros = cx.NumeroRegistros("paSP_AutorizacionesReprogNoCUC_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                 "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarSolicitudesPenVyA":
                            NroRegistros = cx.NumeroRegistros("paSP_SolicitudesPenVyA_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                "documento", Request.Form["documento"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarSolicitudesPenVyA_NOCUC":
                            NroRegistros = cx.NumeroRegistros("paSP_SolicitudesPenVyA_NOCUC_numlistado",
                                 "fechaini", Request.Form["fechaini"],
                                 "fechafin", Request.Form["fechafin"],
                                "documento", Request.Form["documento"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaObservacionSPVyA":
                            NroRegistros = cx.NumeroRegistros("paSP_TramitesObservaciones_num",
                                "id", Request.Form["id"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarDirHospitalario":
                            NroRegistros = cx.NumeroRegistros("paSP_DirHospitalario_numlistado",
                                 "regional", Request.Form["regional"],
                                 "municipio", Request.Form["municipio"],
                                 "servicios", Request.Form["servicios"],
                                 "codigoPriOp", Request.Form["codigoPriOp"],
                                 "NombrePriOp ", Request.Form["NombrePriOp"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarDirRegional":
                            NroRegistros = cx.NumeroRegistros("paSP_DirRegional_numlistado",
                                  "municipio", Request.Form["municipio"],
                                 "uap", Request.Form["uap"],
                                 "servicio", Request.Form["servicio"],
                                 "codigoEPS ", Request.Form["codigoEPS"],
                                 "codigoALEA ", Request.Form["codigoALEA"],
                                 "descripcionEPS ", Request.Form["descripcionEPS"],
                                 "NitPrestador ", Request.Form["NitPrestador"],
                                 "Nombreprestador ", Request.Form["Nombreprestador"],
                                 "Regional ", Request.Form["Regional"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarDirMedicamentos":
                            NroRegistros = cx.NumeroRegistros("paSP_DirMedicamentos_numlistado",
                                  "regional", Request.Form["regional"],
                                 "municipio", Request.Form["municipio"],
                                 "codigoBH", Request.Form["codigoBH"],
                                 "medicamentos", Request.Form["medicamentos"],
                                 "codigoPres ", Request.Form["codigoPres"],
                                  "NombrePres ", Request.Form["NombrePres"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarDirNoPos":
                            NroRegistros = cx.NumeroRegistros("paSP_DirNoPos_numlistado",
                                 "regional", Request.Form["regional"],
                                 "municipio", Request.Form["municipio"],
                                 "servicios", Request.Form["servicios"],
                                 "codigoPres", Request.Form["codigoPres"],
                                 "NombrePres ", Request.Form["NombrePres"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarDirOdontologico":
                            NroRegistros = cx.NumeroRegistros("paSP_DirOdontologico_numlistado",
                                  "municipio", Request.Form["municipio"],
                                 "regional", Request.Form["regional"],
                                 "unidad", Request.Form["unidad"],
                                 "servicios", Request.Form["servicios"],
                                 "CodAlea", Request.Form["CodAlea"],
                                 "eps ", Request.Form["eps"],
                                 "cod_eps ", Request.Form["cod_eps"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaUsuarios":
                            NroRegistros = cx.NumeroRegistros("paSP_Usuarios_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaMedicoTratante":
                            NroRegistros = cx.NumeroRegistros("paSP_MedicoTratante_numlistado",
                                "nombre", Request.Form["nombre"].ToUpper(),
                                "especialidad", Request.Form["especialidad"].ToUpper(),
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarRegionales":
                            NroRegistros = cx.NumeroRegistros("paSP_RegionalesSP_numlistado",
                                "regional", Request.Form["regional"].ToUpper(),
                                "municipio", Request.Form["municipio"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarAsignarMunicipios":
                            NroRegistros = cx.NumeroRegistros("paSP_RegionalesAsignar_numlistado",
                                "regional", Request.Form["regional"].ToUpper(),
                                "departamento", Request.Form["departamento"].ToUpper(),
                                "municipio", Request.Form["municipio"].ToUpper(),
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarMunicipiosAsignados":
                            NroRegistros = cx.NumeroRegistros("paSP_RegionalesMunicipios_numlistado",
                                "regional", Request.Form["regional"].ToUpper(),
                                "departamento", Request.Form["departamento"].ToUpper(),
                                "municipio", Request.Form["municipio"].ToUpper(),
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaObservacionSPSEGUI":
                            NroRegistros = cx.NumeroRegistros("paSP_Tramites_observaciones_num",
                               "id", Request.Form["id"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPrestadorServicio":
                            NroRegistros = cx.NumeroRegistros("paSP_PrestadorServicio_num",
                                 "id", Request.Form["id"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarServicioEditar":
                            NroRegistros = cx.NumeroRegistros("paSP_PrestadorServicioEditar_num",
                                "id", Request.Form["id"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "Ordenes_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_Ordenes_numlistado",
                                "numcaso", Request.Form["numcaso"],
                                "titulo", Request.Form["titulo"],
                                 "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "Ordenes_2listar":
                            NroRegistros = cx.NumeroRegistros("paSP_Ordenes2_numlistado",
                                "numcaso", Request.Form["numcaso"],
                                "titulo", Request.Form["titulo"],
                                 "estado", Request.Form["estado"],
                                 "motivo", Request.Form["motivo"],
                                 "prioridad", Request.Form["prioridad"],
                                  "solicitante", Request.Form["solicitante"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "Ordenes_Historial_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_Ordenes_Historial_numlistado",
                                "numcaso", Request.Form["numcaso"],
                                "titulo", Request.Form["titulo"],
                                 "estado", Request.Form["estado"],
                                 "motivo", Request.Form["motivo"],
                                "prioridad", Request.Form["prioridad"],
                                 "solicitante", Request.Form["solicitante"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarInfoComplementaria":
                            NroRegistros = cx.NumeroRegistros("paSP_InfoComplementaria_numlistado",
                                "id", Request.Form["id"],
                                "sede", Request.Form["sede"],
                                "codigo", Request.Form["codigo"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarInfoSedesPres":
                            NroRegistros = cx.NumeroRegistros("paSP_InfoSedesPres_numlistado",
                                 "idPres", Request.Form["idPres"],
                                "sede", Request.Form["sede"],
                                "codigo", Request.Form["codigo"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarServicioGeneral":
                            NroRegistros = cx.NumeroRegistros("paSP_ServicioGeneral_numlistado",
                                "id", Request.Form["id"],
                                "sede", Request.Form["sede"],
                                "codigo", Request.Form["codigo"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPrestadorSedeDetalle":
                            NroRegistros = cx.NumeroRegistros("paSP_PrestadoresSedeDetalle_numlistado",
                                "id", Request.Form["id"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarPrestadoresSede":
                            NroRegistros = cx.NumeroRegistros("paSP_PrestadoresSede_numlistado",
                                "prestador", Request.Form["prestador"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "paSP_PrestadoresSedeUap_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_PrestadoresSedeUap_numlistado",
                                "prestador", Request.Form["prestador"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarPrestadoresIndicadores":
                            NroRegistros = cx.NumeroRegistros("paSP_PrestadoresIndicadores_numlistado",
                                "prestador", Request.Form["prestador"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listaEspeMediTratante":
                            NroRegistros = cx.NumeroRegistros("paSP_EspeMediTratante_numlistado",
                                "especialidad", Request.Form["especialidad"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarMedicamentos":
                            NroRegistros = cx.NumeroRegistros("paSP_Medicamentos_num",
                                "nombre", Request.Form["nombre"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaAnalisisAntiguo":
                            NroRegistros = cx.NumeroRegistros("paSP_AnalisisAntiguo_numlistado",
                                 "doc", Request.Form["doc"],
                                 "nombre", Request.Form["nombre"],
                                  "fechaIni", Request.Form["fechaIni"],
                                 "fechaFin", Request.Form["fechaFin"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;



                        case "listarPacientesnoContactablePer":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesnoContactablePer_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarNoContactablePer":
                            NroRegistros = cx.NumeroRegistros("paSP_NoContactablePer_numlistado",
                                 "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "PacientesNoContacPeriodico_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_LlamarNCSeguiPer_numlistado",
                                 "nombreFil", Request.Form["nombreFil"],
                                 "documentoFil", Request.Form["documentoFil"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "paSP_LlamarNCSeguiPer_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_LlamarNCSeguiPer_numlistado",
                                 "nombreFil", Request.Form["nombreFil"],
                                 "documentoFil", Request.Form["documentoFil"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarDocumento":
                            NroRegistros = cx.NumeroRegistros("paSP_Documento_Paciente_numlistado",
                                "id", Request.Form["id"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaObservacionArchivo":
                            NroRegistros = cx.NumeroRegistros("paSP_ArchivoTempObservacion_numlistado",
                                "id", Request.Form["id"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarDocumentoGeneral":
                            NroRegistros = cx.NumeroRegistros("paSP_DocumentoGeneral_numlistado",
                                "id", Request.Form["id"],
                                "tipoArchivo", Request.Form["tipoArchivo"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaObservacionArchivoGeneral":
                            NroRegistros = cx.NumeroRegistros("paSP_ObservacionGeneral_numlistado",
                                "id", Request.Form["id"],
                                "tipoArch", Request.Form["tipoArch"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacientesCargueDoc":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesCargueDoc_numlistado",
                                "documento", Request.Form["documento"],
                                "nombre", Request.Form["nombre"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarArchivosGeneral":
                            NroRegistros = cx.NumeroRegistros("paSP_ArchivoGeneral_numlistado",
                                 "documento", Request.Form["documento"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarOrdenEspecialidad":
                            NroRegistros = cx.NumeroRegistros("paSP_OrdenEspecialidad_num",
                                 "documento", Request.Form["documento"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaCIE10ArchivoGen":
                            NroRegistros = cx.NumeroRegistros("paSP_CIE10ArchivoGen_numlistado",
                                "codigo", Request.Form["codigo"],
                                "nombre", Request.Form["nombre"],
                                "documento", Request.Form["documento"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarHijosArchivo":
                            NroRegistros = cx.NumeroRegistros("paSP_HijosArchivo_numlistado",
                                "tipo_arch", Request.Form["tipo_arch"],
                                "especialidad", Request.Form["especialidad"],
                                 "codigo", Request.Form["codigo"],
                                "tipo_doc", Request.Form["tipo_doc"],
                                "documento", Request.Form["documento"],
                                "gestion", Request.Form["gestion"],
                                "fechaIni", Request.Form["fechaIni"],
                                "fechaFin", Request.Form["fechaFin"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacienteArchivos":
                            NroRegistros = cx.NumeroRegistros("paSP_PacienteArchivos_numlistado",
                                //"tipo_arch", Request.Form["tipo_arch"],
                                // "codigo", Request.Form["codigo"],
                                //"tipo_doc", Request.Form["tipo_doc"],
                                 "documentoFil", Request.Form["documentoFil"],
                                 "nombreFil", Request.Form["nombreFil"],
                                //"fechaIni", Request.Form["fechaIni"],
                                //"fechaFin", Request.Form["fechaFin"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarArchivosInformar":
                            NroRegistros = cx.NumeroRegistros("paSP_ArchivosInformar_num",
                                 "documento", Request.Form["documento"],
                                 "nombre", Request.Form["nombre"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarArchivosInformarV2":
                            NroRegistros = cx.NumeroRegistros("paSP_ArchivosInformarV2_num",
                                 "documento", Request.Form["documento"],
                                 "nombre", Request.Form["nombre"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarArchivosInformarNoContactable":
                            NroRegistros = cx.NumeroRegistros("paSP_ArchivosInformarNoContactable_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                  "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarArchivosInformarNoContactableV2":
                            NroRegistros = cx.NumeroRegistros("paSP_ArchivosInformarNoContactableV2_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                  "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarArchivosNoInformar":
                            NroRegistros = cx.NumeroRegistros("paSP_ArchivosNoInformar_num",
                                 "documentoFil", Request.Form["documentoFil"],
                                 "nombreFil", Request.Form["nombreFil"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarArchivosNoInformarV2":
                            NroRegistros = cx.NumeroRegistros("paSP_ArchivosNoInformarV2_num",
                                 "documentoFil", Request.Form["documentoFil"],
                                 "nombreFil", Request.Form["nombreFil"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaObservacionesInfoRech":
                            NroRegistros = cx.NumeroRegistros("paSP_ObservacionesInfoRech_num",
                                 "id", Request.Form["id"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarHistorialInformar":
                            NroRegistros = cx.NumeroRegistros("paSP_InformarHistorial_num",
                                 "documento", Request.Form["documento"],
                                 "nombre", Request.Form["nombre"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacienteCUC":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesCUC_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "listarPacienteMeses":
                            NroRegistros = cx.NumeroRegistros("paSP_PacienteMeses_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "semanaFil", Request.Form["semanaFil"],
                                "bandera", Request.Form["bandera"],
                                "mes", Request.Form["mes"],
                                "anio", Request.Form["anio"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacienteIndefinidoC":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesIndefinidoC_numlistado",
                               "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacienteIndefinidoF":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesIndefinidoF_numlistado",
                               "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPacientePrepagada":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesPrepagada_numlistar",
                               "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarPresentacion":
                            NroRegistros = cx.NumeroRegistros("paSP_Presentacion_numlistado",
                            "nombre", Request.Form["nombre"],
                            "empresa", empresa,
                            "responsable", responsable);
                            break;


                        case "listaViaAdministracion":
                            NroRegistros = cx.NumeroRegistros("paSP_ViaAdministracion_numlistado",
                                "nombreFil", Request.Form["nombreFil"],
                                "empresa", empresa,
                                "responsable", responsable
                                );
                            break;

                        case "listarUnidadBasePacienteMedicamento":
                            NroRegistros = cx.NumeroRegistros("paSP_listarUnidadBasePacienteMedicamento_num",
                                "nombreFiltro", Request.Form["nombreFiltro"].ToUpper(),
                                "nombre", Request.Form["nombre"].ToUpper(),
                                "GlobalMedicamento", Request.Form["GlobalMedicamento"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarMedicos":
                            NroRegistros = cx.NumeroRegistros("paSP_Medicos_num",
                               "nombre", Request.Form["nombre"].ToUpper(),
                               "empresa", empresa,
                               "responsable", responsable);
                            break;

                        case "listarListaChequeo":
                            NroRegistros = cx.NumeroRegistros("paSP_ListaChequeoDiagnostico_num",
                               "documento", Request.Form["documento"],
                               "empresa", empresa,
                               "responsable", responsable);
                            break;

                        case "listarListaChequeoContactabilidad":
                            NroRegistros = cx.NumeroRegistros("paSP_ListaChequeoContactabilidad_num",
                               "documento", Request.Form["documento"],
                               "empresa", empresa,
                               "responsable", responsable);
                            break;

                        case "listarbitacoraRoles":
                            NroRegistros = cx.NumeroRegistros("paSP_bitacoraRoles_numlistado",
                               "fechaini", Request.Form["fechaini"],
                               "fechafin", Request.Form["fechafin"],
                               "empresa", empresa,
                               "responsable", responsable);
                            break;

                        case "listaAnalisisV3":
                            NroRegistros = cx.NumeroRegistros("paSP_AnalisisV3_numlistado",
                                 "doc", Request.Form["doc"],
                                 "nombre", Request.Form["nombre"],
                                  "fechaIni", Request.Form["fechaIni"],
                                 "fechaFin", Request.Form["fechaFin"],
                                   "tipoArchivo", Request.Form["tipoArchivo"],
                                    "tipo", Request.Form["tipo"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listaAnalisisCUCV3":
                            NroRegistros = cx.NumeroRegistros("paSP_AnalisisCUCV3_numlistado",
                                 "doc", Request.Form["doc"],
                                 "nombre", Request.Form["nombre"],
                                  "fechaIni", Request.Form["fechaIni"],
                                 "fechaFin", Request.Form["fechaFin"],
                                   "tipoArchivo", Request.Form["tipoArchivo"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "PacientesDevolucionLlamadas":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesDevolucionLlamadas_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "FechaInicio", Request.Form["FechaInicio"],
                                "FechaFin", Request.Form["FechaFin"],
                                "bandera", Request.Form["bandera"],
                                "tipoLlamada", Request.Form["tipoLlamada"],
                                "prioridad", Request.Form["prioridad"],
                                "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "paSP_ContactoInicialActualP_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_ContactoInicialActualP_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "semanaFil", Request.Form["semanaFil"],
                                "bandera", Request.Form["bandera"],
                                "mes", Request.Form["mes"],
                                "anio", Request.Form["anio"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "PacientesCUCDevolucionLlamadas":
                            NroRegistros = cx.NumeroRegistros("paSP_PacientesCUCDevolucionLlamadas_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "FechaInicio", Request.Form["FechaInicio"],
                                "FechaFin", Request.Form["FechaFin"],
                                "bandera", Request.Form["bandera"],
                                "tipoLlamada", Request.Form["tipoLlamada"],
                                "prioridad", Request.Form["prioridad"],
                                "estado", Request.Form["estado"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "paSP_ContactoFinalActualP_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_ContactoFinalActualP_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                "semanaFil", Request.Form["semanaFil"],
                                "bandera", Request.Form["bandera"],
                                "mes", Request.Form["mes"],
                                "anio", Request.Form["anio"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarCarguePQR":
                            NroRegistros = cx.NumeroRegistros("paSP_CarguePQR_numlistado",
                                "fecha", Request.Form["fecha"].ToUpper(),
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "listarInfoCarguePQR":
                            NroRegistros = cx.NumeroRegistros("paSP_CargueInfoPQR_numlistado",
                                "id", Request.Form["id"].ToUpper(),
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "paSP_Cargue_Alterno_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_Cargue_Alterno_numlistado",
                                "documento", Request.Form["documento"].ToUpper(),
                                "tiposer", Request.Form["tiposer"].ToUpper(),
                                "correo", Request.Form["correo"].ToUpper(),
                                "ente", Request.Form["ente"].ToUpper(),

                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "paSP_Buzon_NoLegibles_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_Buzon_NoLegibles_numlistado",
                                "documento", Request.Form["documento"].ToUpper(),
                                "tiposer", Request.Form["tiposer"].ToUpper(),
                                "correo", Request.Form["correo"].ToUpper(),
                                "ente", Request.Form["ente"].ToUpper(),

                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "paSP_Buzon_Alterno_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_Buzon_Alterno_numlistado",
                                "documento", Request.Form["documento"].ToUpper(),
                                "tiposer", Request.Form["tiposer"].ToUpper(),
                                "correo", Request.Form["correo"].ToUpper(),
                                "ente", Request.Form["ente"].ToUpper(),

                                "bandera", Request.Form["bandera"].ToUpper(),

                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "UsuariosOperaciones_listar":
                            NroRegistros = cx.NumeroRegistros("UsuariosOperaciones_numlistar",
                                "usuario", Request.Form["usuario"].ToUpper(),
                                "bnd", Request.Form["bnd"].ToUpper(),
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "paSP_ActividadesAsignadas_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_ActividadesAsignadas_numlistar",
                                "usuario", Request.Form["usuario"].ToUpper(),
                                "cat_llam", Request.Form["cat_llam"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "paSP_ActividadesHistorial_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_ActividadesHistorial_numlistar",
                                "usuario", Request.Form["usuario"].ToUpper(),
                                "cat_llam", Request.Form["cat_llam"],
                                "fecha_ini", Request.Form["fecha_ini"],
                                "fecha_fin", Request.Form["fecha_fin"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "AnalisisDocumentacion":
                            NroRegistros = cx.NumeroRegistros("paSP_AnalisisDocumentacion_numlistado",
                                 "doc", Request.Form["doc"],
                                 "nombre", Request.Form["nombre"],
                                  "fechaIni", Request.Form["fechaIni"],
                                 "fechaFin", Request.Form["fechaFin"],
                                 "tipoServicio", Request.Form["tipoServicio"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "paSP_archivo_radicado_listado":
                            NroRegistros = cx.NumeroRegistros("paSP_archivo_radicado_numlistado",
                                 "radicado", Request.Form["radicado"],
                                 "bandera", Request.Form["bandera"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "paSP_Gestion_Documentos_listado":
                            NroRegistros = cx.NumeroRegistros("paSP_Gestion_Documentos_numlistado",
                                 "radicado", Request.Form["radicado"],
                                 "bandera", Request.Form["bandera"],
                                 "globalIdCarpeta", Request.Form["globalIdCarpeta"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;


                        case "paSP_Gestion_Documentos_listado2":
                            NroRegistros = cx.NumeroRegistros("paSP_Gestion_Documentos2_numlistado",
                                 "radicado", Request.Form["radicado"],
                                 "bandera", Request.Form["bandera"],
                                 "globalIdCarpeta", Request.Form["globalIdCarpeta"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "paSP_area_pqr_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_area_pqr_numlistado",
                                 "radicado", Request.Form["radicado"],
                                 "documento", Request.Form["documento"],
                                 "bandera", Request.Form["bandera"],
                                 "empresa", empresa,
                                "responsable", responsable);
                            break;

                        case "paSP_Reporte_Dudoso_NoAcepta_listar":
                            NroRegistros = cx.NumeroRegistros("paSP_Reporte_Dudoso_NoAcepta_numlistado",
                                "documentoFil", Request.Form["documentoFil"],
                                "nombreFil", Request.Form["nombreFil"],
                                 "bandera", Request.Form["bandera"],
                                "empresa", empresa,
                                "responsable", responsable);
                            break;


                    }
                    break;
            }

            int PagAnt = PagAct - 1;
            int PagSig = PagAct + 1;
            double PagUlt = NroRegistros / RegistrosAMostrar;
            float res = NroRegistros % RegistrosAMostrar;

            if (res > 0)
                PagUlt = Math.Floor(PagUlt) + 1;

            if (dato != -1)
            {
                retorno += ",'PagAct': " + PagAct;
                retorno += ",'PagSig': " + PagSig;
                retorno += ",'PagAnt': " + PagAnt;
                retorno += ",'PagUlt': " + PagUlt;
                retorno += ",'Total': " + NroRegistros;
            }

            if (retorno.Length > 0)
                Response.Write("{" + retorno + "}");
        }
    }
}