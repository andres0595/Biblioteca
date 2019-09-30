using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inicial.Vista.general
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if ((Session["nom_usuario"]) != null)
                {
                    Response.Redirect("inicio.aspx");
                }
            }
            catch (Exception)
            {
                //Response.Redirect("login.aspx");
            }
        }
    }
}