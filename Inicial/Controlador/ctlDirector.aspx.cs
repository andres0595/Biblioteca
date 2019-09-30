using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inicial.Controlador
{
    public partial class ctlDirector : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string retorno = "";

            Inicial.Modelo.ConexionBD_Sql_Server cx = new Modelo.ConexionBD_Sql_Server();
            string p = Request.Form["p"];

            switch (p)
            {
                case "crearSesionEmpresa":
                    Session["nit_empresa"] = Request.Form["nit"];
                    Session["nombre_empresa"] = Request.Form["razon"];
                    Session["director"] = "1";

                    retorno = cx.Buscar2("cargaLogoEmpresa", "nit", Request.Form["nit"]);
                    if (retorno.Equals(""))
                    {
                        retorno = "lcweb.png";
                        Session["imagen_empresa"] = retorno;
                    }
                    else
                    {
                        Session["imagen_empresa"] = retorno;
                    }
                    Session["salir_sistema"] = "1";

                    if (Session["permisosTemp"] != null)
                    {
                        Session["permisos"] = Session["permisosTemp"];
                        Session.Remove("permisosTemp");
                    }

                    Session.Timeout = 45;
                    Response.Write(Session["director"]);
                    break;

                case "cerrarSesion":
                    Session["salir_sistema"] = null;
                    Session.RemoveAll();
                    Session.Clear();
                    Response.Write(Session["salir_sistema"]);
                    break;

                case "cierraSesionImagen":
                    Session["imagen_empresa"] = null;
                    Session["nit_empresa"] = "";
                    Session["nombre_empresa"] = "";
                    Response.Write("1");
                    break;
            }
        }
    }
}