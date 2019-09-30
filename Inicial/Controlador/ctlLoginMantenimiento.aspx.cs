using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Security.Cryptography;

namespace Inicial.Controlador
{
    public partial class ctlLoginMantenimiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string retorno = "";
            bool ctl = false;
            Mantenimiento cx = new Mantenimiento();
            string p = Request.Form["p"];

            /* Switch para evaluar el tipo de conexion (Motor de base de datos)*/

            switch (p)
            {
                case "logueaUsuarioMantenimiento":
                    ctl = cx.LoginMantenimiento(Request.Form["usuario"].ToString(), Request.Form["clave"].ToString());
                    if (ctl)
                    {
                        retorno = "{'msj':1}";
                        Session["mantenimiento"] = "MANTENIMIENTO";
                        Session["nom_usuario"] = "MANTENIMIENTO";
                        Session["salir_mantenimiento"] = "OK";
                    }
                    else
                        retorno = "{'msj':0}";

                    Response.Write(retorno);
                    break;

                case "cerrarSesionMantenimiento":
                    Session["mantenimiento"] = null;
                    Session["salir_mantenimiento"] = null;
                    Response.Write("");
                    break;
            }
        }
    }
}