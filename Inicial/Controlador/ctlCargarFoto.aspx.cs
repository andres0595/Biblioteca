using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inicial.Controlador
{
    public partial class ctlCargarFoto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if ((Session["nom_usuario"]) == null)
            //{
            //    Response.Redirect("../vista/general/inicio.aspx");
            //}
        }
        protected void UploadFile(object src, EventArgs e)
        {
            if (myFile.HasFile)
            {
                string strFileName;
                int intFileNameLength;
                string strFileExtension;

                strFileName = myFile.FileName;
                intFileNameLength = strFileName.Length;
                strFileExtension = strFileName.Substring(intFileNameLength - 4, 4);

                if ((strFileExtension == ".jpg") || (strFileExtension == ".jpeg") || (strFileExtension == ".png"))
                {
                    try
                    {
                        myFile.PostedFile.SaveAs(Server.MapPath("../Productos/" + strFileName));
                        lblMsg.Text = "La imagen se ha cargado conéxito!!!";
                        Session["direccionLogo"] = "../Productos/" + strFileName;
                        Response.Redirect("ctlCargarFoto.aspx?infog=" + strFileName);
                    }
                    catch (Exception)
                    {
                        lblMsg.Text = "No se encuentra la ruta para el archivo!";
                    }
                }
                else
                {
                    lblMsg.Text = "Solo pueden ser cargadas imagenes JPG ó PNG.";
                }
            }
            else
            {
                lblMsg.Text = "Por favor seleccione un archivo!";
            }
        }
    }
}