using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;

namespace Inicial.Controlador
{
    public partial class ctlCambiarClave : System.Web.UI.Page
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
            string usuario = Session["usu_sistema"].ToString();

            Modelo.ConexionBD_Sql_Server cx = new Modelo.ConexionBD_Sql_Server();
            //Inicial.Modelo.ConexionBD_ORACLE cx = new Modelo.ConexionBD_ORACLE();

            string tipoConexion = cx.tipoConexion();

            switch (tipoConexion)
            {
                case "SQL_SERVER":
                    switch (p)
                    {
                        case "cambio":
                            retorno = cx.Ejecutar("paINI_cambiaClaveUsuario", "clave", cifrarMd5(Request.Form["c"]), "actual", cifrarMd5(Request.Form["ca"]), "usuario", usuario);
                            if (retorno.Contains("1"))
                                enviarConfirmacion(usuario, Request.Form["c"], Session["mail_usuario"].ToString());
                            Response.Write(retorno);
                            break;
                    }
                    break;

                case "ORACLE":
                    switch (p)
                    {
                        case "cambio":
                            retorno = cx.Ejecutar("PKG_CLAVE.cambiaClaveUsuario", "varchar2", cifrarMd5(Request.Form["c"]), "varchar2", usuario);
                            if (retorno.Contains("1"))
                                enviarConfirmacion(usuario, Request.Form["c"], Session["mail_usuario"].ToString());
                            Response.Write(retorno);
                            break;
                    }
                    break;
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
            string ip = Request.ServerVariables["REMOTE_ADDR"];
            string pc = Request.ServerVariables["REMOTE_HOST"];
            string server = Request.ServerVariables["SERVER_NAME"];

            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();

            /*-------------------------MENSAJE DE CORREO----------------------*/

            //Creamos un nuevo Objeto de mensaje
            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

            //Direccion de correo electronico a la que queremos enviar el mensaje
            mmsg.To.Add(email);

            //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

            //Asunto
            mmsg.Subject = "Activación de Contraseña";
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

            //Direccion de correo electronico que queremos que reciba una copia del mensaje
            mmsg.Bcc.Add(email); //Opcional

            //Cuerpo del Mensaje
            mmsg.Body = "<table align='center' style='margin:7px; padding:7px; font-size:17px; color:black;'>";

            //mmsg.Body += "<tr align='center'><td><a href='http://app.wdvisual.co/SP2017/'> <img src='http://www.scuatro.co/' style= 'width:555px; cursor:pointer;' /></a></td></tr>";
            mmsg.Body += "<tr align='center'><td><a href='http://app.wdvisual.co/SP2017/'></a></td></tr>";


            mmsg.Body += "<tr align='center'><td><h1>HOLA " + usuario.ToUpper() + "</h1><br /><h3>Se ha registrado una Nueva Contraseña<br />Tus datos para iniciar sesión son:</h3><br /></td></tr>";


            mmsg.Body += "<tr align='center'><td><b>Correo Electrónico:</b> " + email + "<br /></td></tr>";
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
    }
}