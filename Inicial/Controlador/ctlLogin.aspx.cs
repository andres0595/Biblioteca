using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;


namespace Inicial.Controlador
{
    public partial class ctlLogin : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string retorno = "";

            //Inicial.Modelo.ConexionBD_ORACLE cx = new Modelo.ConexionBD_ORACLE();
            Inicial.Modelo.ConexionBD_Sql_Server cx = new Modelo.ConexionBD_Sql_Server();
            string p = Request.Form["p"];
            string tipoConexion = cx.tipoConexion();

            string empresa = "900352864-1";
            string responsable = "NULL";

            /* Switch para evaluar el tipo de conexion (Motor de base de datos)*/

            switch (tipoConexion)
            {
                case "SQL_SERVER":

                    switch (p)
                    {



                       

                      

                        case "cargarTipoDocumento":
                            retorno = cx.Listar("paINI_TipoDocumento_cargar"
                                );
                            Response.Write(retorno);
                            break;

                        case "cargarGenero":
                            retorno = cx.Listar("paINI_Genero_cargar"
                                );
                            Response.Write(retorno);
                            break;

                      
                        case "cargaPais":
                            retorno = cx.Listar("paINI_Pais_cargar");
                            Response.Write(retorno);
                            break;

                        case "cargaDepto":
                            retorno = cx.Listar("paINI_Departamento_cargar", "pais", Request.Form["pais"]);
                            Response.Write(retorno);
                            break;

                        case "cargaMpio":
                            retorno = cx.Listar("paINI_Municipio_cargar", "depto", Request.Form["depto"]);
                            Response.Write(retorno);
                            break;

                        case "cargaDeptoLista":
                            retorno = cx.Listar("cargaDepartamentoLista", "pais", Request.Form["pais"]);
                            Response.Write(retorno);
                            break;

                        case "cargaMpioLista":
                            retorno = cx.Listar("cargaMunicipioLista", "depto", Request.Form["departamento"]);
                            Response.Write(retorno);
                            break;

                        case "cargaLocal":
                            retorno = cx.Listar("paINI_Localidad_cargar", "local", Request.Form["local"]);
                            Response.Write(retorno);
                            break;

                        case "cargarBarrio":
                            retorno = cx.Listar("paINI_Barrio_cargar", "barrio", Request.Form["barrio"]);
                            Response.Write(retorno);
                            break;

                        case "loguear":
                            retorno = cx.Login("paINI_Usuarios_loguea", "usuario", Request.Form["usuario"], "clave", Request.Form["clave"]);
                            StringBuilder m = cx.MenusPermitidos();
                            if (!(m.Equals("")))
                            {
                                Session["permisos"] = m;
                            }
                            Response.Write(retorno);
                            break;

                        case "paCO_Usuarios_logeo":
                            retorno = cx.Login("paCO_Usuarios_logeo",
                                "usuario", Request.Form["usuario"],
                                "clave", Request.Form["clave"]);
                            Response.Write(retorno);
                            break;

                        case "loguear_Regional":
                            retorno = cx.Login("paINI_Usuarios_loguea2", "usuario", Request.Form["usuario"], "clave", Request.Form["clave"]);
                            StringBuilder o = cx.MenusPermitidos();
                            if (!(o.Equals("")))
                            {
                                Session["permisos"] = o;
                            }
                            Response.Write(retorno);
                            break;


                        case "sesion":

                            Session["nom_usuario"] = Request.Form["nom"];
                            Session["usu_sistema"] = Request.Form["usu"];
                            Session["mail_usuario"] = Request.Form["mail"];
                            Session["nit_empresa"] = Request.Form["nit"];
                            Session["sesion_key"] = Request.Form["key"];

                            Session["sesion_documento"] = Request.Form["documento"];
                            Session["sesion_telefono"] = Request.Form["telefono"];
                            Session["sesion_area"] = Request.Form["area"];
                            Session["sesion_cargo"] = Request.Form["cargo"];
                            Session["sesion_rol"] = Request.Form["rol"];
                            Session["sesion_foto"] = Request.Form["foto"];

                            Session["nombre_empresa"] = Request.Form["nombreEmpresa"];
                            Session["sesion_departamento"] = Request.Form["departamento"];

                            Session["salir_sistema"] = "1";
                            Session["ubicacion"] = "Inicio";

                            retorno = cx.Buscar2("paINI_Empresa_cargaLogo", "nit", Request.Form["nit"]);
                            if (retorno.Equals(""))
                            {
                                Session["imagen_empresa"] = "lcweb.png";
                            }
                            else
                            {
                                Session["imagen_empresa"] = retorno;
                            }

                            Session.Timeout = 45;
                            cx.Desconectar();
                            Response.Write("");
                            break;

                        case "termina":
                            retorno = cx.Ejecutar("paINI_Usuarios_cerrarSesion", "clave", Session["sesion_key"]);
                            Session["salir_sistema"] = null;
                            Session.RemoveAll();
                            Session.Clear();
                            cx.Desconectar();
                            Response.Write(retorno);
                            break;

                        case "cargaIDmenus":
                            retorno = cx.Buscar("paINI_cargaTodosIDMenus");
                            Response.Write(retorno);
                            break;

                        case "solicitaClave":
                            string email = Request.Form["email"];
                            retorno = cx.InsertarRetorna("paINI_recuperaClave", "email", email, "clave", Request.Form["claveE"]);
                            if (retorno.Equals("1"))
                            {
                                enviarConfirmacion(Request.Form["clave"], email);
                            }
                            Response.Write("{'data':" + retorno + "}");
                            break;

                        case "cargaPermisosMenu":
                            retorno = cx.Buscar("paINI_cargaTodosPermisosMenu", "id", Request.Form["men_id"]);
                            Response.Write(retorno);
                            break;

                        case "cambiarUbicacion":
                            if (Session["hijos"] != null)
                            {
                                Session["ubicacion"] += Request.Form["ubic"];
                            }
                            else
                                Session["ubicacion"] = Request.Form["ubic"];
                            break;

                        case "cambiarUbicacionH":
                            Session["ubicacion"] = Request.Form["ubic"];
                            Session["hijos"] = Request.Form["hijos"];
                            break;

                        case "verificaSesion":
                            retorno = (Session["usu_sistema"] != null) ? "{'msj':1}" : "{'msj':0}";
                            Response.Write(retorno);
                            break;

                        case "desconectar":
                            retorno = cx.Ejecutar("paINI_Usuarios_cerrarSesion", "clave", Request.Form["clave"]);
                            Response.Write(retorno);
                            break;



                    }
                    break;

                case "ORACLE":
                    switch (p)
                    {
                        case "loguea":
                            retorno = cx.Login("PKG_USUARIOS.logueaUsuario", "varchar2", Request.Form["usuario"], "varchar2", Request.Form["clave"]);
                            StringBuilder m = cx.MenusPermitidos();
                            if (!(m.Equals("")))
                            {
                                Session["permisos"] = m;
                            }
                            Response.Write(retorno);
                            break;

                        case "sesion":
                            Session["usu_sistema"] = Request.Form["usu"];
                            Session["nom_usuario"] = Request.Form["nom"];
                            Session["mail_usuario"] = Request.Form["mail"];
                            Session["nit_empresa"] = Request.Form["nit"];
                            Session["sesion_key"] = Request.Form["key"]; 
                            Session["salir_sistema"] = "1";
                            Session["ubicacion"] = "Inicio";
                            Response.Write("");
                            break;

                        case "termina":
                            Session["salir_sistema"] = null;
                            Session.RemoveAll();
                            Session.Clear();
                            Response.Write(Session["salir_sistema"]);
                            break;

                        case "cargaTodosIDMenus":
                            retorno = cx.Buscar("PKG_USUARIOS." + p);
                            Response.Write(retorno);
                            break;

                        case "recuperaClave":
                            string email = Request.Form["email"];
                            retorno = cx.InsertarRetorna("PKG_USUARIOS." + p, "varchar2", email, "varchar2", Request.Form["claveE"]);
                            if (retorno.Equals("1"))
                            {
                                enviarConfirmacion(Request.Form["clave"], email);
                            }
                            Response.Write("{'data':" + retorno + "}");
                            break;

                        case "cargaTodosPermisosMenu":
                            retorno = cx.Buscar("PKG_USUARIOS." + p, "varchar2", Request.Form["men_id"]);
                            Response.Write(retorno);
                            break;

                        case "cambiarUbicacion":
                            Session["ubicacion"] = Request.Form["ubic"];
                            break;

                        case "verificaSesion":
                            retorno = (Session["usu_sistema"] != null) ? "{'msj':1}" : "{'msj':0}";
                            Response.Write(retorno);
                            break;
                    }
                    break;
            }
        }
        
        public String buscaSesionUsuario()
        {
            String retorno = "";
            if (Session["usu_sistema"] != null)
            {
                Inicial.Modelo.ConexionBD_Sql_Server cx = new Modelo.ConexionBD_Sql_Server(1);
                retorno = cx.InsertarRetorna("buscaSesionUsuario", "usuario", Session["usu_sistema"], "key", Session["sesion_key"]);
            }
            else
            {
                retorno = "1";
            }
            return retorno;
        }

        public void cerrarSesion()
        {
            Session["salir_sistema"] = null;
            Session.RemoveAll();
            Session.Clear();
        }


        /// <summary>
        /// Envía un correo de confirmación al usuario ingresado.
        /// </summary>
        /// <param name="usuario">El nombre de usuario ingresado.</param>
        /// <param name="clave">La clave del usuario</param>
        /// <param name="email">El email para la confirmación</param>
        protected void enviarConfirmacion(string clave, string email)
        {
            string ip = Request.ServerVariables["REMOTE_ADDR"];
            string pc = Request.ServerVariables["REMOTE_HOST"];
            string server = Request.ServerVariables["SERVER_NAME"];

            /*-------------------------MENSAJE DE CORREO----------------------*/

            //Creamos un nuevo Objeto de mensaje
            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

            //Direccion de correo electronico a la que queremos enviar el mensaje
            mmsg.To.Add(email);

            //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

            //Asunto
            mmsg.Subject = "Recuperar Clave Usuario";
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

            //Direccion de correo electronico que queremos que reciba una copia del mensaje
            mmsg.Bcc.Add(email); //Opcional

            //Cuerpo del Mensaje
            mmsg.Body = "<table align='center' style='margin:30px; padding:30px; font-size:17px; color:black;'>";



            mmsg.Body += "<tr align='center'><td><h3>Se ha cambiado con éxito tu contraseña.</h3></td></tr>";
            mmsg.Body += "<tr align='center'><td><br />Nueva contraseña: <b>" + clave + "</b></td></tr>";
            mmsg.Body += "<tr align='center'><td><br />Fecha y hora GMT: <b>" + DateTime.Now.ToUniversalTime().ToString("dd/MM/yyyy HH:mm:ss") + "</b></td</tr>";
            mmsg.Body += "<tr align='center'><td><br />Se accedió desde la siguiente ubicación:\n<b>" + ip + "\n" + pc + "\n" + server+"</b></td</tr>";
            

            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = true; //Si no queremos que se envíe como HTML

            //Correo electronico desde la que enviamos el mensaje
            mmsg.From = new System.Net.Mail.MailAddress("comunicacionwdvisual@gmail.com");


            /*-------------------------CLIENTE DE CORREO----------------------*/

            //Creamos un objeto de cliente de correo
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

            //Hay que crear las credenciales del correo emisor
            cliente.Credentials =
                new System.Net.NetworkCredential("comunicacionwdvisual@gmail.com", "ppcscjbcvszxcuna");

            //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail

            cliente.Port = 587;
            cliente.EnableSsl = true;


            cliente.Host = "smtp.gmail.com"; //Para Gmail "smtp.gmail.com";


            /*-------------------------ENVIO DE CORREO----------------------*/

            try
            {
                //Enviamos el mensaje      
                cliente.Send(mmsg);
            }
            catch (System.Net.Mail.SmtpException)
            {
                //Aquí gestionamos los errores al intentar enviar el correo
            }


        }

       
            
      
    }
}