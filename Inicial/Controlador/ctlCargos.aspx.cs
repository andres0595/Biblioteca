﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inicial.Controlador
{
    public partial class ctlCargos : System.Web.UI.Page
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

            Modelo.ConexionBD_Sql_Server maestraCx = new Modelo.ConexionBD_Sql_Server();
            switch (p)
            {
                case "guardarCargos":
                    retorno = maestraCx.InsertarRetorna("paINI_Cargos_guarda", // nombre del procedimiento almacenado pa + Ala(tabla -- Alarmas--) + NombreProcedimiento almacenado  

                        "id", Request.Form["id"],
                        "nombre", Request.Form["nombre"].ToUpper(),
                        "descripcion", Request.Form["descripcion"].ToUpper(),
                        "area", Request.Form["area"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;

                case "borrarCargos":
                    retorno = maestraCx.InsertarRetorna("paINI_Cargos_borrar", // nombre del procedimiento almacenado pa + Mau(tabla) + NombreProcedimiento almacenado  
                        "id", Request.Form["id"],
                        "empresa", empresa,
                        "responsable", responsable);
                    Response.Write(retorno);
                    break;


            }
        }
    }
}