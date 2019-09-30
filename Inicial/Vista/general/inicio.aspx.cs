using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inicial.Vista.general
{
    public partial class inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["salir_sistema"] == null)
                {
                    Session.RemoveAll();
                    Session.Clear();
                    Session.Abandon();
                    Response.Redirect(Page.ResolveUrl("login.aspx"));
                }
                /*else if (Session["permisosTemp"] != null && Session["nit_empresa"].ToString().Equals("NIT"))
                {
                    Response.Redirect(Page.ResolveUrl("elegirEmpresa.aspx"));
                }*/
            }
            catch (Exception)
            {
                Response.Redirect("login.aspx");
            }
        }
    }
}