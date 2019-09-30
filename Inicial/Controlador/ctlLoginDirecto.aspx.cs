using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace Inicial.Controlador
{
    public partial class ctlLoginDirecto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string retorno = "";

            //Inicial.Modelo.ConexionBD_ORACLE cx = new Modelo.ConexionBD_ORACLE();
            Inicial.Modelo.ConexionBD_Sql_Server cx = new Modelo.ConexionBD_Sql_Server();
            string p = Request.Form["p"];
            string tipoConexion = cx.tipoConexion();

            /* Switch para evaluar el tipo de conexion (Motor de base de datos)*/

            switch (tipoConexion)
            {
                case "SQL_SERVER":

                    switch (p)
                    {
                        case "logueaUsuario":
                            retorno = cx.Login("paINI_Usuarios_logueaDirecto", "usuario", Request.Form["usuario"], "clave", Request.Form["clave"]);
                            StringBuilder m = cx.MenusPermitidos();
                            if (!(m.Equals("")))
                                Session["permisos"] = m;                            
                            Response.Write(retorno);
                            break;

                        case "crearSesion":
                            Session["usu_sistema"] = Request.Form["usu"];
                            Session["nom_usuario"] = Request.Form["nom"];
                            Session["mail_usuario"] = Request.Form["mail"];
                            Session["nit_empresa"] = Request.Form["nit"];
                            Session["salir_sistema"] = "1";
                            Session.Timeout = 30;
                            cx.Desconectar();
                            Response.Write("");
                            break;

                        case "cerrarSesion":
                            Session["salir_sistema"] = null;
                            Session.RemoveAll();
                            Session.Clear();
                            cx.Desconectar();
                            Response.Write(Session["salir_sistema"]);
                            break;

                        case "cargaMenus":
                            retorno = cx.Buscar("paINI_cargaTodosIDMenus");                            
                            Response.Write(retorno);
                            break;

                        case "cargaPermisos":
                            retorno = cx.Buscar("paINI_cargaTodosPermisosMenu", "id", Request.Form["men_id"]);                            
                            Response.Write(retorno);
                            break;
                    }
                    break;

                case "ORACLE":
                    switch (p)
                    {
                        case "logueaUsuarioDirecto":
                            retorno = cx.Login("PKG_USUARIOS.logueaUsuario", "varchar2", Request.Form["usuario"], "varchar2", Request.Form["clave"]);
                            StringBuilder m = cx.MenusPermitidos();
                            if (!(m.Equals("")))
                                Session["permisos"] = m;
                            Response.Write(retorno);
                            break;

                        case "crearSesion":
                            Session["usu_sistema"] = Request.Form["usu"];
                            Session["nom_usuario"] = Request.Form["nom"];
                            Session["mail_usuario"] = Request.Form["mail"];
                            Session["nit_empresa"] = Request.Form["nit"];
                            Session["salir_sistema"] = "1";
                            Session.Timeout = 30;
                            cx.Desconectar();
                            Response.Write("");
                            break;

                        case "cerrarSesion":
                            Session["salir_sistema"] = null;
                            Session.RemoveAll();
                            Session.Clear();
                            cx.Desconectar();
                            Response.Write(Session["salir_sistema"]);
                            break;

                        case "cargaTodosIDMenus":
                            retorno = cx.Buscar("PKG_USUARIOS." + p);                            
                            Response.Write(retorno);
                            break;

                        case "cargaTodosPermisosMenu":
                            retorno = cx.Buscar("PKG_USUARIOS." + p, "varchar2", Request.Form["men_id"]);                            
                            Response.Write(retorno);
                            break;
                    }
                    break;
            }
        }
    }
}