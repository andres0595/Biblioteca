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

                        case "paBLI_solicitudes_listar":
                            dr = cx.Paginar("paBLI_solicitudes_listar",
                                 "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "fechaSolicitud", Request.Form["fechaSolicitud"],
                                "tipolibro", Request.Form["tipolibro"]);
                            Response.Write(retorno);
                            break;

                        case "paBLI_libros_listar":
                            dr = cx.Paginar("paBLI_libros_listar",
                                 "numFilas", RegistrosAMostrar,
                                "filaInicial", pagActual,
                                "codigolibro", Request.Form["codigolibro"],
                                "nombrelibro", Request.Form["nombrelibro"]);
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

                        case "paBLI_solicitudes_listar":
                            NroRegistros = cx.NumeroRegistros("paBLI_solicitudes_numlistado",
                               "fechaSolicitud", Request.Form["fechaSolicitud"],
                                "tipolibro", Request.Form["tipolibro"]);
                            break;


                        case "paBLI_libros_listar":
                            NroRegistros = cx.NumeroRegistros("paBLI_libros__numlistado",                                
                                "codigolibro", Request.Form["codigolibro"],
                                "nombrelibro", Request.Form["nombrelibro"]);
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