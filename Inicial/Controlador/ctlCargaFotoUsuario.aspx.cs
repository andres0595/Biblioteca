using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Inicial.Controlador
{
    public partial class ctlCargaFotoUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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


                if ((strFileExtension == ".jpg") || (strFileExtension == "jpeg") || (strFileExtension == ".png") || (strFileExtension == "JPEG") || (strFileExtension == ".PNG") || (strFileExtension == ".JPG"))
                {
                    try
                    {
                        string ruta = "~/Fotos/";


                        DirectoryInfo carpetas = new DirectoryInfo(Server.MapPath(ruta));
                        if (!carpetas.Exists)
                        {
                            carpetas.Create();
                        }


                        strFileName = (DateTime.Now.Second) + strFileName;
                        Session["usu_foto"] = strFileName;
                        myFile.PostedFile.SaveAs(Server.MapPath(ruta + "/" + strFileName));
                        Response.Redirect("ctlCargaFotoUsuario.aspx?infog=" + strFileName, false);
                    }
                    catch (Exception)
                    {
                        lblMsg.Text = "Error al guardar la foto.";
                    }
                }
                else
                {
                    lblMsg.Text = "Solo pueden ser cargadas imagenes JPG ó PNG.";
                }
            }
            else
            {
                lblMsg.Text = "Por favor seleccione un archivo.";
            }
        }
    }
}