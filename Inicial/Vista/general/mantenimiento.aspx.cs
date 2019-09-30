using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inicial.Vista.general
{
    public partial class mantenimiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["mantenimiento"] == null)
                    Response.Redirect("login_mantenimiento.aspx");
                else
                {
                    if (Session["salir_mantenimiento"] == null)
                    {
                        Session.Remove("mantenimiento");
                        Session.Remove("salir_mantenimiento");
                        Response.Redirect(Page.ResolveUrl("login_mantenimiento.aspx"));
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("login_mantenimiento.aspx");
            }

        }
    }
}