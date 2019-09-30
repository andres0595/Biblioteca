using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inicial.Controlador
{
    public partial class ctlRol : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
           *
           * EN AJAX se necesita leer un formulario, por eso se pone en el Page_Load.
           * cuando se lee la pagina se obtiene el valor del parametro "p" que viene en la URL AJAX.
           * con este se realiza un switch para saber que acción se debe realizar y se retorna un llamado a un
           * metodo que se encuentra en la clase conexioncx_XXX en formato JSON.
           * 
           * XXX -> Motor de Base de datos
            */

            if ((Session["nom_usuario"]) == null)
            {
                Response.Redirect("../vista/general/inicio.aspx");
            }

            /*
          *
          * EN AJAX se necesita leer un formulario, por eso se pone en el Page_Load.
          * cuando se lee la pagina se obtiene el valor del parametro "p" que viene en la URL AJAX.
          * con este se realiza un switch para saber que acción se debe realizar y se retorna un llamado a un
          * metodo que se encuentra en la clase conexioncx_XXX en formato JSON.
          * 
          * XXX -> Motor de Base de datos
           */
            string p = Request.Form["p"];
            string retorno = "";
            string responsable = Session["usu_sistema"].ToString();
            string nomResponsable = Session["nom_usuario"].ToString();
            int ctlURL = 0;

            /*Se descomenta cuando se trabaja con ORACLE y se comentan las lineas de los demas motores de base de datos*/
            Inicial.Modelo.ConexionBD_Sql_Server cx = new Modelo.ConexionBD_Sql_Server();
            //Inicial.Modelo.ConexionBD_ORACLE cx = new Modelo.ConexionBD_ORACLE();
            string tipoConexion = cx.tipoConexion();

            switch (tipoConexion)
            {
                case "SQL_SERVER":

                    switch (p)
                    {
                        case "guardar":
                            retorno = cx.InsertarRetorna("paINI_Rol_guarda",
                                "rol", Request.Form["rol"],
                                "nivel", Request.Form["nivel"],
                                "des", Request.Form["des"],
                                "responsable", responsable);
                              //  Response.Write(retorno);
                           Response.Write("{'msj':" + retorno + "}");
                            break;

                        case "eliminar":
                            retorno = cx.Ejecutar("paINI_eliminaRol",
                                "id", Request.Form["id"]);
                              //  "responsable", responsable);
                              Response.Write(retorno);
                            break;

                        case "editar":
                            retorno = cx.Ejecutar("editaRol", 
                                "id", Request.Form["id"], 
                                "rol", Request.Form["rol"], 
                                "nivel", Request.Form["nivel"],
                                "descripcion", Request.Form["des"],
                                "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "buscar":
                            retorno = cx.Buscar("paINI_Rol_busca",
                                "rol", Request.Form["rol"]);
                            Response.Write(retorno);
                            break;

                        case "cargaMenusDisponibles":
                            retorno = cx.Listar("paINI_Rol_cargaMenusDisponibles",
                                "usuario", responsable);
                            Response.Write(retorno);
                            break;

                        case "cargaMenusCategoria":
                            retorno = cx.Listar("paINI_MenusCategoriaRol_cargar",
                                "categoria", Request.Form["categoria"],
                                "usuario", responsable);
                            Response.Write(retorno);
                            break;

                        case "buscaCategoria":
                            retorno = cx.Buscar("paINI_RolCategoria_busca",
                                "categoria", Request.Form["categoria"],
                                "rol", Request.Form["rol"]);
                            Response.Write(retorno);
                            break;

                        case "quitaPermisos":
                            retorno = cx.InsertarRetorna("paINI_Rol_quitaPermisos",
                                "rol", Request.Form["rol"],
                                "arrayMenuPermisos", Request.Form["menus"],
                                "tipo", Request.Form["tipo"],
                                "responsable", responsable);
                            Response.Write("{'msj':" + retorno + "}");
                            break;
                    }
                    break;

                case "ORACLE":
                    switch (p)
                    {
                        case "guardaRol":
                            ctlURL = int.Parse(Request.Form["ctl"]);
                            switch (ctlURL)
                            {
                                case 0:
                                    retorno = cx.InsertarRetorna("PKG_ROLES." + p, "varchar2", Request.Form["rol"], "varchar2", Request.Form["des"], "varchar2", Request.Form["menus"], "varchar2", responsable);
                                    break;

                                case 1:
                                    if (Session["URL"] != null)
                                        Session["URL"] += Request.Form["menus"];
                                    else
                                        Session["URL"] = Request.Form["menus"];
                                    break;

                                case 2:
                                    Session["URL"] += Request.Form["menus"];
                                    retorno = cx.InsertarRetorna("PKG_ROLES." + p, "varchar2", Request.Form["rol"], "varchar2", Request.Form["des"], "varchar2", Session["URL"], "varchar2", responsable);
                                    break;
                            }
                            if (ctlURL != 1)
                            {
                                Session["URL"] = null;
                                Response.Write("{'msj':" + retorno + "}");
                            }
                            break;

                        case "eliminaRol":
                            retorno = cx.Ejecutar("PKG_ROLES." + p, "number", Request.Form["id"], "varchar2", Request.Form["rol"], "varchar2", responsable);
                            Response.Write(retorno);
                            break;

                        case "editaRol":
                            ctlURL = int.Parse(Request.Form["ctl"]);
                            switch (ctlURL)
                            {
                                case 0:
                                    retorno = cx.InsertarRetorna("PKG_ROLES." + p, "number", Request.Form["id"], "varchar2", Request.Form["rol"], "varchar2", Request.Form["des"], "varchar2", Request.Form["menus"], "varchar2", responsable);
                                    break;

                                case 1:
                                    if (Session["URL"] != null)
                                        Session["URL"] += Request.Form["menus"];
                                    else
                                        Session["URL"] = Request.Form["menus"];
                                    break;

                                case 2:
                                    Session["URL"] += Request.Form["menus"];
                                    retorno = cx.InsertarRetorna("PKG_ROLES." + p, "number", Request.Form["id"], "varchar2", Request.Form["rol"], "varchar2", Request.Form["des"], "varchar2", Session["URL"], "varchar2", responsable);
                                    break;
                            }
                            if (ctlURL != 1)
                            {
                                Session["URL"] = null;
                                Response.Write("{'msj':" + retorno + "}");
                            }
                            break;

                        case "buscar":
                            retorno = cx.Buscar("PKG_ROLES.buscaRol", "varchar2", Request.Form["rol"]);
                            Response.Write(retorno);
                            break;

                        case "cargaMenusDisponibles":
                            retorno = cx.Listar("PKG_ROLES.cargaMenusDisponiblesRol", "varchar2", responsable);
                            Response.Write(retorno);
                            break;
                    }
                    break;
            }
        }
    }
}