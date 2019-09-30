using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.IO;

namespace Inicial.Controlador
{
    public partial class ctlUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            *
            * EN AJAX se necesita leer un formulario, por eso se pone en el Page_Load.
            * cuando se lee la pagina se obtiene el valor del parametro "p" que viene en la URL AJAX.
            * con este se realiza un switch para saber que acción se debe realizar y se retorna un llamado a un
            * metodo que se encuentra en la clase conexionBD en formato JSON.
            * 
            */

            if ((Session["nom_usuario"]) == null)
            {
                Response.Redirect("../vista/general/inicio.aspx");
            }

            string p = Request.Form["p"];
            string retorno = "";
            string responsable = Session["usu_sistema"].ToString();
            string nomResponsable = Session["nom_usuario"].ToString();
            string empresaUsuario = Session["nit_empresa"].ToString();
            string clave = "";
            Modelo.ConexionBD_Sql_Server cx = new Modelo.ConexionBD_Sql_Server();
            string tipoConexion = "SQL_SERVER";//cx.tipoConexion();
            switch (tipoConexion)
            {
                case "SQL_SERVER":

                    switch (p)
                    {
                        case "guardarUsu":

                            String imagen = null;
                            if (Session["usu_foto"] != null)
                            {
                                imagen = Session["usu_foto"].ToString();
                            }
                            else
                            {

                                Session["usu_foto"] = "no_disponible.jpg";
                                imagen = Session["usu_foto"].ToString();
                                imagen = "no_disponible.jpg";
                            }
                                  

                            clave = Request.Form["clave"];
                            retorno = cx.InsertarRetorna("paINI_Usuarios_guardar",
                                "edita", Request.Form["edita"],
                                "usuario", Request.Form["usuario"],
                                "nombreUsuario", Request.Form["nombreUsuario"],
                                "documento", Request.Form["documento"],
                                "clave", cifrarMd5(clave),
                                "email", Request.Form["email"],
                                "telefono", Request.Form["telefono"],
                                "direccion", Request.Form["direccion"],
                                //"area", Request.Form["area"],
                                "cargo", Request.Form["cargo"],
                                "arrayGrupo", Request.Form["grupo"],
                                "estado", Request.Form["estado"],
                                "foto", imagen,
                                "arrayRoles", Request.Form["rol"],
                                "responsable", responsable,
                                "empresa", empresaUsuario);

                            Response.Write("{'msj':" + retorno + "}");
                            if (retorno == "1")
                            {
                                EnviarCorreo();
                            }
                           
                            break;

                        case "eliminarUsu":
                            retorno = cx.Ejecutar("paINI_Usuarios_eliminar", "usuario", Request.Form["u"], "responsable", responsable);
                            Response.Write(retorno);
                            break;

                        case "editarUsu":

                            retorno = cx.InsertarRetorna("editaUsuarios",
                                                        "usuario", Request.Form["usuario"],
                                                        "nombreUsuario", Request.Form["nombreUsuario"],
                                                        "documento", Request.Form["documento"],
                                                        "clave", cifrarMd5(clave),
                                                        "email", Request.Form["email"],
                                                        "telefono", Request.Form["telefono"],
                                                        "direccion", Request.Form["direccion"],
                                                        "area", Request.Form["area"],
                                                        "cargo", Request.Form["cargo"],
                                                        "grupo", Request.Form["grupo"],
                                                        "estado", Request.Form["estado"],
                                                        "foto", Request.Form["foto"],
                                                        "arrayRoles", Request.Form["rol"],
                                                        "responsable", responsable,
                                                        "empresa", empresaUsuario);
                            Response.Write("{'msj':" + retorno + "}");
                            break;

                        case "editarPermisoUsu":
                            retorno = cx.InsertarRetorna("paINI_Usuarios_editaPermisos",
                                                "usuario", Request.Form["u"],
                                                "arrayMenuPermisos", Request.Form["m"],
                                                "responsable", responsable);
                            Response.Write("{'msj':" + retorno + "}");
                            break;

                        case "buscarUsu":
                            retorno = cx.Buscar("paINI_Usuarios_buscar", "usuario", Request.Form["u"]);
                            Response.Write(retorno);
                            break;

                        case "cargarMenuUsu":
                            retorno = cx.Listar("paINI_MenuUsuario_cargar", "usuario", responsable);
                            Response.Write(retorno);
                            break;

                        case "cargarRol":
                            retorno = cx.Listar("paINI_Roles_cargar", "usuario", responsable, "empresa", empresaUsuario);
                            Response.Write(retorno);
                            break;

                        case "genClav":
                            retorno = cx.Ejecutar("paINI_Usuarios_generarClave", "clave", cifrarMd5(Request.Form["c"]), "usuario", Request.Form["u"]);
                            if (retorno.Contains("1"))
                                enviarConfirmacion(Request.Form["u"], Request.Form["c"], Request.Form["e"]);
                            Response.Write(retorno);
                            break;

                        case "cargarCatsMenu":
                            retorno = cx.Listar("paINI_CategoriasMenus_cargar", "usuario", Request.Form["usuario"]);
                            Response.Write(retorno);
                            break;

                        case "buscarRolCatUsu":
                            retorno = cx.Buscar("paINI_RolCategoriaUsuario_buscar", "categoria", Request.Form["categoria"],
                                "usuario", Request.Form["usuario"]);
                            Response.Write(retorno);
                            break;

                        case "quitarPermsUsu":
                            retorno = cx.InsertarRetorna("paINI_PermisosUsuario_quitar", "usuario", Request.Form["usuario"],
                                "arrayMenuPermisos", Request.Form["menus"],
                                "tipo", Request.Form["tipo"],
                                "responsable", responsable);
                            Response.Write("{'msj':" + retorno + "}");
                            break;

                        case "buscarMensUsu":
                            retorno = cx.Listar("paINI_MenuUsuario_buscar", "usuario", Request.Form["usuario"],
                                "categoria", Request.Form["categoria"]);
                            Response.Write(retorno);
                            break;

                        case "cargaEditarFotoDefecto":
                            Session["usu_foto"] = "no_disponible.jpg";
                            Response.Redirect("ctlCargaFotoUsuario.aspx?infog=" + Session["usu_foto"], false);
                            break;

                        case "cargaEditarFoto":
                            retorno = cx.InsertarRetorna("paINI_Usuarios_CargaEditarFoto",
                                "usu_usuario", Request.Form["usu_usuario"]
                                );

                            Session["usu_foto"] = retorno;
                            Response.Redirect("ctlCargaFotoUsuario.aspx?infog=" + Session["usu_foto"], false);
                            break;

                        case "listarEmpresasSeleccion":
                            retorno = cx.Listar("paINI_EmpresasSeleccion_listar",
                                "usuarioSesion", responsable,
                                "usuario", Request.Form["Usuario"]);
                            Response.Write(retorno);
                            break;

                        case "asignarEmpresaUsuario":
                            retorno = cx.InsertarRetorna("paINI_EmpresaUsuario_asignar",
                                "usuario", Request.Form["usuario"],
                                "nit", Request.Form["nit"],
                                "estado", Request.Form["estado"]);
                            Response.Write(retorno);
                            break;

                        /* case "listarAplicacionesEmpresas":
                             retorno = cx.Listar("listarAplicacionesEmpresas",
                                 "usuarioSesion", responsable,
                                 "usuario", Request.Form["Usuario"],
                                 "empresa", empresaUsuario);
                             Response.Write(retorno);
                             break;*/

                        case "asignarAplicacionesEmpresa":
                            retorno = cx.InsertarRetorna("asignarAplicacionesEmpresa",
                                "usuario", Request.Form["usuario"],
                                "idAplicacion", Request.Form["idAplicacion"],
                                "estado", Request.Form["estado"],
                                "empresa", empresaUsuario);
                            Response.Write(retorno);
                            break;

                        case "cargarPermisosApps":
                            retorno = cx.Listar("cargarPermisosApps",
                                "usuario", responsable,
                                "empresa", empresaUsuario);
                            Response.Write(retorno);
                            break;

                        case "detalleUsuario":
                            retorno = cx.Listar("paINI_Usuarios_detalle",
                                "usuario", Request.Form["usuario"],
                                "empresa", empresaUsuario
                                );
                            Response.Write(retorno);
                            break;

                        case "validaRol":
                            retorno = cx.InsertarRetorna("paINI_Rol_validar",
                                "id", Request.Form["id"],
                                "empresa", empresaUsuario
                                );
                            Response.Write(retorno);
                            break;

                    }
                    break;
            }
        }

        public void EnviarCorreo()
        {

            //    string correoT = Request.Form["correoTera"];
            // string nombre = Request.Form["nombreProfesional"];

            string documento = Request.Form["documento"];
            string nombres = Request.Form["nombreUsuario"];
            string email = Request.Form["email"];
            string clave = Request.Form["clave"];



            //string id = Request.Form["id"];

            //string documentoP = Request.Form["documento"];
            //string nombreP = Request.Form["nombrepac"];
            //string servicioP = Request.Form["servicio"];
            //string numsesionesP = Request.Form["sesiReprogramar"];
            //string nombreteraT = Request.Form["nombretera"];

            //string direccioncorreo = Request.Form["direccioncorreo"];
            //string telefonocorreo = Request.Form["telefonocorreo"];
            //string numautocorreo = Request.Form["numautocorreo"];
            //string fechainicorreo = Request.Form["fechainicorreo"];
            //string frecueteracorreo = Request.Form["frecueteracorreo"];
            //string valorcopagocorreo = Request.Form["valorcopagocorreo"];
            //string frecuecopagocorreo = Request.Form["frecuecopagocorreo"];
            //string fechafincorreo = Request.Form["fechafincorreo"];


            //string nuevoprofesionalcorreo = Request.Form["selProfesional"];

            //string correonuevoprofesionalcorreo = Request.Form["correoNuevoTeracorreo"];

            /*-------------------------MENSAJE DE CORREO----------------------*/

            //Creamos un nuevo Objeto de mensaje
            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

            //Direccion de correo electronico a la que queremos enviar el mensaje
            mmsg.To.Add(email);

            //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

            //Asunto
            mmsg.Subject = "Registro de Usuario Director";
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

            //Direccion de correo electronico que queremos que reciba una copia del mensaje
            mmsg.Bcc.Add(email); //Opcional

            //Cuerpo del Mensaje
            mmsg.Body = "<table align='center' style='margin:7px; padding:7px; font-size:17px; color:black;'>";

            mmsg.Body += "<tr align='center'><td><a href='http://app.wdvisual.co/SP2017/'></a></td></tr>";
        

            mmsg.Body += "<tr align='center'><td><h1>BIENVENIDO " + nombres.ToUpper() + "</h1><br /><h3>Te haz registrado correctamente, Tus datos de ingreso son:</h3><br /><br /></td></tr>";


            mmsg.Body += "<tr align='center'><td><b>Correo Electrónico :</b>" + email + "<br /></td></tr>";
            mmsg.Body += "<tr align='center'><td><b>Contraseña:</b> " + clave + "<br /></td></tr>"; //


            mmsg.Body += "<tr align='center'><td><a href='http://app.wdvisual.co/SP2017/'> <img src='http://www.vetpraxis.net/wp-content/uploads/2016/01/boton-ingresar-png.png' style= 'width:150px; cursor:pointer;' /> </a>  </td></tr></table>";
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
            catch (System.Net.Mail.SmtpException ex)
            {
                //Aquí gestionamos los errores al intentar enviar el correo
            }

        }
        /// <summary>
        /// Envía un correo de confirmación al usuario ingresado.
        /// </summary>
        /// <param name="usuario">El nombre de usuario ingresado.</param>
        /// <param name="clave">La clave del usuario</param>
        /// <param name="email">El email para la confirmación</param>
        protected void enviarConfirmacion(string usuario, string clave, string email)
        {
            string responsable = Session["usu_sistema"].ToString();
            string nomResponsable = Session["nom_usuario"].ToString();
            string empresaUsuario = Session["nit_empresa"].ToString();
            string nuevoUsuario = "";
            string nombre = "";
            string documento = "";
            string nuevaClave = "";

            Inicial.Modelo.ConexionBD_Sql_Server cx = new Modelo.ConexionBD_Sql_Server();

            string retorno = cx.Consultar("buscarInfoNuevoUsuario",
                        "usuario", usuario,
                        "email", email,
                        "empresa", empresaUsuario);

            string[] Separado = retorno.Split(new Char[] { ',' });

            nuevoUsuario = Separado[0];
            nombre = Separado[1];
            documento = Separado[2];
            nuevaClave = Separado[3];

            string ip = Request.ServerVariables["REMOTE_ADDR"];
            string pc = Request.ServerVariables["REMOTE_HOST"];
            string server = Request.ServerVariables["SERVER_NAME"];

            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();

            correo.From = new System.Net.Mail.MailAddress("comunicacionwdvisual@gmail.com");
            correo.To.Add(email);
            correo.Subject = "Notificación de registro en " + Session["nombre_sistema"];
            correo.Body = "Estimado(a) " + nombre + ", usted se ha registrado satisfactoriamente en la aplicacion " + Session["nombre_sistema"]
                + ", a continuación enviamos de manera confidencial la clave de ingreso.\n\n"
                + "Su usuario está identificado con la siguiente información:\n\n"
                + "Usuario ingreso: " + nuevoUsuario + '\n'
                + "Número de documento: " + documento + '\n'
                + "Contraseña: " + clave + '\n'
                + "Ruta de acceso: " + Session["ruta_sistema"] + "\n\n"
                //+ "NOTA: " + usuario + "\nContraseña: " + clave + "\n\n" + "\n\nFecha y hora GMT: " +
                + "NOTA: este correo ha sido generado automaticamente, por favor no lo responda"
                + "\n\nEquipo WD VISUAL";
            //+DateTime.Now.ToUniversalTime().ToString("dd/MM/yyyy HH:mm:ss") + "\n\nSe accedió desde la siguiente ubicación:\n" + ip + "\n" + pc + "\n" + server;
            correo.IsBodyHtml = false;
            correo.Priority = System.Net.Mail.MailPriority.Normal;
            //
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

            smtp.Credentials = new System.Net.NetworkCredential("comunicacionwdvisual", "desarrolladorLC*");
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(correo);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        /// <summary>
        /// Cifra la clave en MD5 Hash
        /// </summary>
        /// <param name="clave">La clave original</param>
        /// <returns>La clave cifrada en MD5</returns>
        private string cifrarMd5(string clave)
        {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();

            byte[] data = System.Text.Encoding.ASCII.GetBytes(clave);
            data = provider.ComputeHash(data);

            string md5 = string.Empty;

            for (int i = 0; i < data.Length; i++)
                md5 += data[i].ToString("x2").ToLower();

            return md5;
        }

        public string filepath { get; set; }
    }
}
     