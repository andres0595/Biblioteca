using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inicial.Controlador
{
    public partial class css : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string referer = Request.ServerVariables["HTTP_REFERER"];
            string errormsg = "\n ACCESO DENEGADO";
            try
            {
                if (!referer.Equals(""))
                {
                    Response.ContentType = "text/css";
                    Response.Clear();
                    Response.Write("\n /* REFERER: -" + referer + "- */ \n");

                    string t = Request.Form["t"];
                    switch (t)
                    {
                        case "1":
                            Response.WriteFile("../Recursos/css/general.css");
                            Response.WriteFile("../Recursos/css/login.css");
                            Response.WriteFile("../Recursos/css/menu.css");
                            Response.WriteFile("../Recursos/css/paginaMaestra.css");
                            break;

                        case "2":
                            Response.WriteFile("../Recursos/css/calendario/calendario.css");
                            Response.WriteFile("../Recursos/css/pestanas/demos.css");
                            Response.WriteFile("../Recursos/css/pestanas/jquery.ui.tabs.css");
                            break;

                        case "3":
                            Response.WriteFile("../Recursos/css/pestanas/demos.css");
                            Response.WriteFile("../Recursos/css/pestanas/jquery.ui.tabs.css");
                            break;
                    }
                }
                else
                    Response.Write(errormsg);
            }
            catch (Exception)
            {
                Response.Write(errormsg);
            }

            /*foreach (string s in Request.ServerVariables.AllKeys)
            {
                Response.Write(s + "=[" + Request.ServerVariables[s] + "]<br>");
            }*/
        }
    }
}