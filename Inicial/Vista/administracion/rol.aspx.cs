using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inicial.Vista.administracion
{
    public partial class rol : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Controlador.ctlInicio obj = new Controlador.ctlInicio();
            try
            {
                if (!(obj.esMenuHabilitado("0.1", Session["permisos"].ToString())))
                    Response.Redirect("../general/inicio.aspx");
            }
            catch (Exception)
            {
                Response.Redirect("../general/inicio.aspx");
            }
        }
    }
}




