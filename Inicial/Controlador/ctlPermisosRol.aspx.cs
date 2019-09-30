using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inicial.Controlador.paginador
{
    public partial class ctlPermisosRol : System.Web.UI.Page
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
           // int ctlURL = 0;

            Modelo.ConexionBD_Sql_Server cx = new Modelo.ConexionBD_Sql_Server();

            switch (p)
            {
                case "cargaPermisos":
                    retorno = cx.Listar("paINI_RolesPermisos_carga", 
                        "usuario", responsable,
                        "empresa", empresa);
                    //retorno = "{'msj':1, 'data':['1','visita 1','2','visita 2','3','visita 3','4','visita 4']}";
                    Response.Write(retorno);
                    break;

                case "guardar":
                    retorno = cx.InsertarRetorna("paINI_PermisosRol_guarda", 
                        "rol", Request.Form["rol"],
                        "arrayMenuPermisos", Request.Form["menus"],
                        "responsable", responsable);
                    Response.Write("{'msj':" + retorno + "}");
                    break;

                case "cargaCategorias":
                    retorno = cx.Listar("paINI_CategoriasMenus_cargar",
                        "usuario", responsable);
                    Response.Write(retorno);
                    break;
            }
        }
    }
}