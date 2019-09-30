using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inicial.Controlador
{
    public partial class ctlCargaLogoEmpresa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Modelo.ConexionBD_Sql_Server cx = new Modelo.ConexionBD_Sql_Server();
            if (Request.QueryString.Get("reg") != null)
            {
                if (Request.QueryString.Get("reg").ToString().Equals("0"))
                {
                    string retorno = cx.Buscar("paINI_InfoLogoEmpresa_carga", "nit", Session["nit_empresa"]);
                    string varTemp = retorno.Substring(retorno.IndexOf(":[") + 3, retorno.Length - (retorno.IndexOf(":[") + 6));
                    if (!varTemp.Equals(""))
                        Session["logo_empresa"] = varTemp;
                }
                else
                {
                    Session["logo_empresa"] = "";
                }
            }
        }

        private Boolean validarArchivos(FileUpload arc)
        {
            FileUpload val = arc;
            if ((arc.PostedFile.ContentType == "image/jpeg") || (arc.PostedFile.ContentType == "image/png"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void UploadFile(object src, EventArgs e)
        {
            if (myFile.HasFile)
            {
                if ((myFile.HasFile) && (validarArchivos(myFile)))
                {
                    try
                    {
                        string nomDocumento = "";
                        nomDocumento = myFile.FileName.Replace(' ', '_');
                        //myFile.PostedFile.SaveAs(Server.MapPath("../") + "Archivos/" + empresa + "/" + sede + "/documentos/infograma.pdf");
                        myFile.PostedFile.SaveAs(Server.MapPath("../logos/" + nomDocumento));
                        lblMsg.Text = "La imagen se ha cargado con &eacute;xito!!!";
                        Session["logo_empresa"] = nomDocumento;
                        Response.Redirect("ctlCargaLogoEmpresa.aspx?logo=" + nomDocumento);

                        //Session.Remove("direccionLogo");
                    }
                    catch (Exception)
                    {
                        lblMsg.Text = "No se encuentra la ruta para el archivo!";
                    }
                }
                else
                {
                    lblMsg.Text = "Solo pueden ser cargadas imagenes JPG o PNG.";
                }
            }
            else
            {
                lblMsg.Text = "Por favor seleccione un archivo!";
            }
        }
    }
}