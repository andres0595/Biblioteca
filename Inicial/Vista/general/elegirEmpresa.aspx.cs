using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inicial.Vista.general
{
    public partial class elegirEmpresa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Controlador.ctlInicio obj = new Controlador.ctlInicio();
            try
            {
                if (Session["permisos"] != null)
                {
                    if (!(obj.esMenuHabilitado("2.2", Session["permisos"].ToString())) && !(Session["nit_empresa"].ToString().Equals("NIT")))
                    {
                        Response.Redirect("../general/inicio.aspx");
                    }
                }
                else
                {
                    if (Session["usu_sistema"] == null)
                    {
                        Response.Redirect("../general/login.aspx");
                    }
                }
                //Response.Redirect("../general/login.aspx");
            }
            catch (Exception)
            {
                Response.Redirect("../general/login.aspx");
            }
        }
    }
}