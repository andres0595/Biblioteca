using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Management;
using System.Data.Sql;
using System.Net;
using System.Text;

/// <summary>
/// Se realiza la conexion a la base de datos y los llamados a los procedimientos almacenados
/// </summary>
namespace Inicial.Modelo
{
    public class ConexionBD_Sql_Server
    {
        private SqlConnection cnn;
        private string sConexion;
        StringBuilder menus = new StringBuilder("");

        /// <summary>
        /// Initializes a new instance of the <see cref="ConexionBD"/> class.
        /// </summary>
        public ConexionBD_Sql_Server()
        {
            Controlador.ctlLogin objLogin = new Controlador.ctlLogin();
            String sesionValida = objLogin.buscaSesionUsuario();
            if (sesionValida.Equals("1"))
            {
                sConexion = System.Configuration.ConfigurationManager.ConnectionStrings["conexionBD_Sql_Server"].ConnectionString;
                cnn = new SqlConnection(sConexion);
            }
            else
            {
                objLogin.cerrarSesion();
                Desconectar();
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ConexionBD"/> class.
        /// </summary>
        public ConexionBD_Sql_Server(int tipo)
        {
            sConexion = System.Configuration.ConfigurationManager.ConnectionStrings["conexionBD_Sql_Server"].ConnectionString;
            cnn = new SqlConnection(sConexion);
        }


        /// <summary>
        /// Gets the cadena conexion.
        /// </summary>
        /// <value>The cadena conexion.</value>
        public String cadenaConexion
        {
            get
            {
                return sConexion;
            }
        }

        /// <summary>
        /// Ejecutar cualquier procedimiento almacenado.
        /// </summary>
        /// <param name="pa">Procedimiento Almacenado.</param>
        /// <param name="parametros">Parametros necesarios para la ejecución del procedimiento almacenado.</param>
        /// <returns></returns>
        /// 
        public String ejecutarCargaMasiva(string pa, params object[] parametros)
        {
            string res = "0";
            try
            {
                IDbDataParameter par = null;
                IDbCommand c = cnn.CreateCommand();
                c.Connection = cnn;
                c.CommandTimeout = 700;
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = pa;
                for (int i = 0; i < parametros.Length; i += 2)
                {
                    par = c.CreateParameter();
                    par.ParameterName = "@" + parametros[i];
                    par.Value = parametros[i + 1];
                    c.Parameters.Add(par);
                }
                cnn.Open();
                IDataReader dr = c.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    res = dr.GetValue(0).ToString().Trim();
                }
                Desconectar();
            }
            catch (SqlException e)
            {
                res = "0";
            }
            return res;
        }
        public String Ejecutar(string pa, params object[] parametros)
        {
            string jSon = "";
            try
            {
                IDbDataParameter par = null;
                IDbCommand c = cnn.CreateCommand();
                c.Connection = cnn;
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = pa;
                for (int i = 0; i < parametros.Length; i += 2)
                {
                    par = c.CreateParameter();
                    par.ParameterName = "@" + parametros[i];
                    par.Value = parametros[i + 1];
                    c.Parameters.Add(par);
                }
                cnn.Open();
                IDataReader dr = c.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.RecordsAffected > 0)
                    jSon += "{'msj': 1}";
                else
                    jSon += "{'msj': 0}";
            }
            catch (SqlException)
            {
                jSon = "{\"msj\": -1}";
            }
            Desconectar();
            return jSon;
        }

        /// <summary>
        /// Inserta un registro y retorna la PK del mismo
        /// </summary>
        /// <param name="pa">Procedimiento almacenado</param>
        /// <param name="parametros">Array con los datos a insertar</param>
        /// <returns>Retorna el id del regsitro insertado</returns>
        public String InsertarRetorna(string pa, params object[] parametros)
        {
            String retorno = "";
            try
            {
                IDbDataParameter par = null;
                IDbCommand c = cnn.CreateCommand();
                c.Connection = cnn;
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = pa;
                for (int i = 0; i < parametros.Length; i += 2)
                {
                    par = c.CreateParameter();
                    par.ParameterName = "@" + parametros[i];
                    par.Value = parametros[i + 1];
                    c.Parameters.Add(par);
                }
                cnn.Open();
                IDataReader dr = c.ExecuteReader(CommandBehavior.CloseConnection);
                retorno = "0";
                while (dr.Read())
                {
                    retorno = dr.GetValue(0).ToString().Trim();
                }
            }
            
             
            
            catch (SqlException)
            {
                retorno = "-1"; //+ e.Message;
            }
            Desconectar();
            return retorno;
        }


        public String EjecutaRetorna(string pa, params object[] parametros)
        {
            String retorno = "";
            try
            {
                IDbDataParameter par = null;
                IDbCommand c = cnn.CreateCommand();
                c.Connection = cnn;
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = pa;
                for (int i = 0; i < parametros.Length; i += 2)
                {
                    par = c.CreateParameter();
                    par.ParameterName = "@" + parametros[i];
                    par.Value = parametros[i + 1];
                    c.Parameters.Add(par);
                }
                cnn.Open();
                IDataReader dr = c.ExecuteReader(CommandBehavior.CloseConnection);
                retorno = "{'msj': 0}*";
                while (dr.Read())
                {
                    retorno = "{'msj':" + dr.GetValue(0).ToString().Trim() + "}*" + dr.GetValue(1).ToString().Trim();
                }                Desconectar();}
            catch (SqlException )
            {
                retorno = "{'msj': -1}*"; //+ e.Message;
            }
            return retorno;
        }

        /// <summary>
        /// Autentica a un usuario que quiere acceder al sistema
        /// </summary>
        /// <param name="pa">Procedimiento almacenado</param>
        /// <param name="parametros">parametros con usuario y clave ingresados por el usuario</param>
        /// <returns>retorna los datos correspondientes a este usuario </returns>
        public String Login(string pa, params object[] parametros)
        {
            string jSon = "";
            string jSon2 = "";
            try
            {
                IDbDataParameter par = null;
                IDbCommand c = cnn.CreateCommand();
                c.Connection = cnn;
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = pa;
                for (int i = 0; i < parametros.Length; i += 2)
                {
                    par = c.CreateParameter();
                    par.ParameterName = "@" + parametros[i];
                    par.Value = parametros[i + 1];
                    c.Parameters.Add(par);
                }
                cnn.Open();
                IDataReader dr = c.ExecuteReader(CommandBehavior.CloseConnection);
                string menTemp = "";
                bool ctl = true;
                while (dr.Read())
                {
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        if (ctl)
                        {
                            if (i != 4)
                            {
                                if (jSon2.Equals(""))
                                    jSon2 += "'" + dr.GetValue(i).ToString().Trim() + "'";
                                else
                                    jSon2 += ",'" + dr.GetValue(i).ToString().Trim() + "'";
                            }
                            else
                            {
                                menTemp = dr.GetValue(i + 1).ToString().Trim();
                                string[] per = menTemp.Split(',');
                                if (menus.Length == 0)
                                {
                                    menus.Append(dr.GetValue(i));
                                }
                                else
                                {
                                    menus.Append(";" + dr.GetValue(i));
                                }
                                for (int k = 0; k < per.Length; k++)
                                {
                                    menus.Append("," + per[k]);
                                }
                                i++;
                            }
                        }
                        else
                        {
                            i = 4;
                            menTemp = dr.GetValue(i + 1).ToString().Trim();
                            string[] per = menTemp.Split(',');
                            if (menus.Equals(""))
                            {
                                menus.Append(dr.GetValue(i));
                            }
                            else
                            {
                                menus.Append(";" + dr.GetValue(i));
                            }
                            for (int k = 0; k < per.Length; k++)
                            {
                                menus.Append("," + per[k]);
                            }
                            i = dr.FieldCount;
                        }
                    }
                    ctl = false;
                }
                if (jSon2 != "")
                {
                    jSon += "{'msj': 1,";
                    jSon += "'data':[" + jSon2 + "]}";
                }
                else
                {
                    jSon += "{'msj': 0}";                }}
            catch (SqlException)
            {
                jSon = "{\"msj\": -1}";
            }
            Desconectar();
            return jSon;
        }

        public String Consultar(string pa, params object[] parametros)
        {
           // string jSon = "";
            string jSon2 = "";
            try
            {
                IDbDataParameter par = null;
                IDbCommand c = cnn.CreateCommand();
                c.Connection = cnn;
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = pa;

                for (int i = 0; i < parametros.Length; i += 2)
                {
                    par = c.CreateParameter();
                    par.ParameterName = "@" + parametros[i];
                    par.Value = parametros[i + 1];
                    c.Parameters.Add(par);
                }

                cnn.Open();
                IDataReader dr = c.ExecuteReader(CommandBehavior.CloseConnection);
                int numRows = 0;
                int count = dr.FieldCount;
                while (dr.Read())
                {
                    for (int i = 0; i < count; i++)
                    {
                        if ((numRows == 0) && (i == 0))
                        {
                            jSon2 += dr.GetValue(i).ToString().Trim();
                        }
                        else
                        {
                            jSon2 += "," + dr.GetValue(i).ToString().Trim();
                        }
                    }
                    numRows++;
                }}
            catch (SqlException)
            {
                jSon2 = "1";
            }
            Desconectar();
            return jSon2;
        }

        /// <summary>
        /// Buscar cualquier información por algun parametro.
        /// </summary>
        /// <param name="pa">Procedimiento Almacenado.</param>
        /// <param name="parametros">Parametros para filtrar la busqueda.</param>
        /// <returns></returns>
        /// 
        public String Buscar(string pa, params object[] parametros)
        {
            string jSon = "";
            string jSon2 = "";
            try
            {
                IDbDataParameter par = null;
                IDbCommand c = cnn.CreateCommand();
                c.Connection = cnn;
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = pa;
                for (int i = 0; i < parametros.Length; i += 2)
                {
                    par = c.CreateParameter();
                    par.ParameterName = "@" + parametros[i];
                    par.Value = parametros[i + 1];
                    c.Parameters.Add(par);
                }
                cnn.Open();
                IDataReader dr = c.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    if (!jSon2.Equals(""))
                        jSon2 += ",";
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        jSon2 += "'" + dr.GetValue(i).ToString().Trim() + "'";
                        if (i < dr.FieldCount - 1)
                        {
                            jSon2 += ",";
                        }
                    }
                }
                if (jSon2 != "")
                {
                    jSon += "{'msj': 1,";
                    jSon += "'data':[" + jSon2 + "]}";
                }
                else
                {
                    jSon += "{'msj': 0}";
                }            }
            catch (SqlException)
            {
                jSon = "{\"msj\": -1}";
            }
            Desconectar();
            return jSon;
        }


        public String Buscar2(string pa, params object[] parametros)
        {
            string jSon2 = "";
            try
            {
                IDbDataParameter par = null;
                IDbCommand c = cnn.CreateCommand();
                c.Connection = cnn;
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = pa;
                for (int i = 0; i < parametros.Length; i += 2)
                {
                    par = c.CreateParameter();
                    par.ParameterName = "@" + parametros[i];
                    par.Value = parametros[i + 1];
                    c.Parameters.Add(par);
                }
                cnn.Open();
                IDataReader dr = c.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    if (!jSon2.Equals(""))
                    {
                        jSon2 += ",";
                    }
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        jSon2 += dr.GetValue(i).ToString().Trim();
                        if (i < dr.FieldCount - 1)
                        {
                            jSon2 += ",";
                        }
                    }                }            }
            catch (SqlException e)
            { 
                jSon2 = "{\"msj\": -2,\"data\":[\"Error\":\"" + e.Message + "\"]}";
            }
            this.Desconectar();
            return jSon2;
        }

        /// <summary>
        /// Lista los registros que cumplan con las condiciones de los parametros.
        /// </summary>
        /// <param name="pa">Procedimiento Almacenado.</param>
        /// <param name="parametros">Parametros para tener en ceunta para la busqueda.</param>
        /// <returns>Registros en formato JSON</returns>
        public String Listar(string pa, params object[] parametros)
        {
            string jSon = "";
            string jSon2 = "";
            try
            {
                IDbDataParameter par = null;
                IDbCommand c = cnn.CreateCommand();
                c.Connection = cnn;
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = pa;

                for (int i = 0; i < parametros.Length; i += 2)
                {
                    par = c.CreateParameter();
                    par.ParameterName = "@" + parametros[i];
                    par.Value = parametros[i + 1];
                    c.Parameters.Add(par);
                }

                cnn.Open();
                IDataReader dr = c.ExecuteReader(CommandBehavior.CloseConnection);
                int numRows = 0;
                int count = dr.FieldCount;
                while (dr.Read())
                {
                    for (int i = 0; i < count; i++)
                    {
                        if ((numRows == 0) && (i == 0))
                        {
                            jSon2 += "'" + dr.GetValue(i).ToString().Trim() + "'";
                        }
                        else
                        {
                            jSon2 += ", '" + dr.GetValue(i).ToString().Trim() + "'";
                        }
                    }
                    numRows++;
                }
                if (jSon2 != "")
                {
                    jSon = "{'msj':1, 'cols':" + count + ", 'rows':" + numRows + ", 'data':[" + jSon2 + "]}";
                }
                else
                {
                    jSon = "{'msj':0}";
                }
            }
            catch (SqlException)
            {
                jSon = "{\"msj\": -1}";
            }
            Desconectar();
            return jSon;
        }

        /// <summary>
        /// Lista los registros que obtenga el procedimiento alnmacenado.
        /// </summary>
        /// <param name="pa">Procedimiento Almacenado.</param>
        /// <returns>Registros en formato JSON</returns>
        public String Listar(string pa)
        {
            string jSon = "";
            string jSon2 = "";
            try
            {
                IDbCommand c = cnn.CreateCommand();
                c.Connection = cnn;
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = pa;

                cnn.Open();
                IDataReader dr = c.ExecuteReader(CommandBehavior.CloseConnection);
                int numRows = 0;
                int count = dr.FieldCount;
                while (dr.Read())
                {
                    for (int i = 0; i < count; i++)
                    {
                        if ((numRows == 0) && (i == 0))
                        {
                            if ((dr.GetValue(i) is Int16) || (dr.GetValue(i) is Int32))
                            {
                                jSon2 += dr.GetValue(i).ToString().Trim();
                            }
                            else
                            {
                                jSon2 += "'" + dr.GetValue(i).ToString().Trim() + "'";
                            }
                        }
                        else
                        {
                            if ((dr.GetValue(i) is Int16) || (dr.GetValue(i) is Int32))
                            {
                                jSon2 += ", " + dr.GetValue(i).ToString().Trim();
                            }
                            else
                            {
                                jSon2 += ", '" + dr.GetValue(i).ToString().Trim() + "'";
                            }
                        }
                    }
                    numRows++;
                }
                if (jSon2 != "")
                {
                    jSon = "{'msj':1, 'rows':" + numRows + ", 'data':[" + jSon2 + "]}";
                }
                else
                {
                    jSon = "{'msj':0}";
                }
            }
            catch (SqlException)
            {
                jSon = "{\"msj\": -1}";
            }
            Desconectar();
            return jSon;
        }

        /// <summary>
        /// Retorna datos segun patrones de busqueda para paginar
        /// </summary>
        /// <param name="pa">Procedimiento Almacenado</param>
        /// <param name="parametros">Parametros con los patrones de busqueda</param>
        /// <returns>Registros en formato JSON</returns>
        public IDataReader Paginar(string pa, params object[] parametros)
        {
            try
            {
                IDbDataParameter par = null;
                IDbCommand c = cnn.CreateCommand();
                c.Connection = cnn;
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = pa;
                for (int i = 0; i < parametros.Length; i += 2)
                {
                    par = c.CreateParameter();
                    par.ParameterName = "@" + parametros[i];
                    par.Value = parametros[i + 1];
                    c.Parameters.Add(par);
                }
                cnn.Open();
                IDataReader dr = c.ExecuteReader(CommandBehavior.CloseConnection);
                //Desconectar();
                return dr;
            }
            //    catch (SqlException e)

            //catch (SqlException e)

                catch (SqlException)

            {
                Desconectar();
                return null;
            }   
        }

        /// <summary>
        /// Retorna el numero de registros que se encuentren en una tabla especifica
        /// </summary>
        /// <param name="pa">Nombre del procedimiento almacenado</param>
        /// <returns>Entero representativo del numero de registros</returns>
        public int NumeroRegistros(string pa)
        {
            try
            {
                IDbCommand c = cnn.CreateCommand();
                c.Connection = cnn;
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = pa;
                cnn.Open();
                IDataReader dr = c.ExecuteReader(CommandBehavior.CloseConnection);
                int num = 0;
                if (dr.Read())
                    num = int.Parse(dr.GetValue(0).ToString());
                Desconectar();
                return num;
            }
            catch (SqlException)
            {
                Desconectar();
                return -1;
            }
        }

        /// <summary>
        /// Retorna el numero de registros que existen en una tabla
        /// </summary>
        /// <param name="pa">Procedimiento Almacenado</param>
        /// <returns>Registros en formato JSON</returns>
        public int NumeroRegistros(string pa, params object[] parametros)
        {
            try
            {
                IDbDataParameter par = null;
                IDbCommand c = cnn.CreateCommand();
                c.Connection = cnn;
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = pa;

                for (int i = 0; i < parametros.Length; i += 2)
                {
                    par = c.CreateParameter();
                    par.ParameterName = "@" + parametros[i];
                    par.Value = parametros[i + 1];
                    c.Parameters.Add(par);
                }

                cnn.Open();
                IDataReader dr = c.ExecuteReader(CommandBehavior.CloseConnection);
                int num = 0;
                if (dr.Read())
                {
                    num = int.Parse(dr.GetValue(0).ToString());
                }
                Desconectar();
                return num;
            }
            catch (SqlException)
            {
                Desconectar();
                return -1;
            }
        }

        public void Desconectar()
        {
            if (cnn != null)
                cnn.Close();
        }

        public string tipoConexion()
        {
            return "SQL_SERVER";
        }

        public StringBuilder MenusPermitidos()
        {
            return menus;
        }

        /*-----------------------------------------------------------------------------------------------*/
        /*--------------------------------------------   METODOS  ---------------------------------------*/
        /*-----------------------------------------  MANTENIMIENTO  -------------------------------------*/
        /*-----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Buscar cualquier información por algun parametro.
        /// </summary>
        /// <param name="pa">Procedimiento Almacenado.</param>
        /// <param name="parametros">Parametros para filtrar la busqueda.</param>
        /// <returns></returns>
        /// 
        public String BuscarMantenimiento(string pa, params object[] parametros)
        {
            string jSon = "";
            string jSon2 = "";
            try
            {
                IDbDataParameter par = null;
                IDbCommand c = cnn.CreateCommand();
                c.Connection = cnn;
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = pa;
                for (int i = 0; i < parametros.Length; i += 2)
                {
                    par = c.CreateParameter();
                    par.ParameterName = "@" + parametros[i];
                    par.Value = parametros[i + 1];
                    c.Parameters.Add(par);
                }
                cnn.Open();
                IDataReader dr = c.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    if (!jSon2.Equals(""))
                    {
                        jSon2 += ",";
                    }
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        jSon2 += "'" + dr.GetValue(i).ToString().Trim().Replace('\'', '#').Replace('"', '#') + "'";
                        if (i < dr.FieldCount - 1)
                        {
                            jSon2 += ",";
                        }
                    }
                }
                if (jSon2 != "")
                {
                    jSon += "{'msj': 1,";
                    jSon += "'data':[" + jSon2 + "]}";
                }
                else
                {
                    jSon += "{'msj': 0}";
                }
            }
            catch (SqlException e)
            {
                string error = e.Message.Replace('\'', ' ').Replace('"', ' ');
                jSon = "{\"msj\": \"-1\"}";
            }
            Desconectar();
            return jSon;
        }


        /// <summary>
        /// Lista los registros que cumplan con las condiciones de los parametros.
        /// </summary>
        /// <param name="pa">Procedimiento Almacenado.</param>
        /// <param name="parametros">Parametros para tener en ceunta para la busqueda.</param>
        /// <returns>Registros en formato JSON</returns>
        public String ListarMantenimiento(string pa, params object[] parametros)
        {
            string jSon = "";
            string jSon2 = "";
            try
            {
                IDbDataParameter par = null;
                IDbCommand c = cnn.CreateCommand();
                c.Connection = cnn;
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = pa;

                for (int i = 0; i < parametros.Length; i += 2)
                {
                    par = c.CreateParameter();
                    par.ParameterName = "@" + parametros[i];
                    par.Value = parametros[i + 1];
                    c.Parameters.Add(par);
                }

                cnn.Open();
                IDataReader dr = c.ExecuteReader(CommandBehavior.CloseConnection);
                int numRows = 0;
                int count = dr.FieldCount;
                while (dr.Read())
                {
                    for (int i = 0; i < count; i++)
                    {
                        if ((numRows == 0) && (i == 0))
                        {
                            jSon2 += "'" + dr.GetValue(i).ToString().Trim() + "'";
                        }
                        else
                        {
                            jSon2 += ", '" + dr.GetValue(i).ToString().Trim() + "'";
                        }
                    }
                    numRows++;
                }
                if (jSon2 != "")
                {
                    jSon = "{'msj':11, 'rows':" + numRows + ", 'fields':" + count + ", 'data':[" + jSon2 + "]}";
                }
                else
                {
                    jSon = "{'msj':00}";
                }
            }
            catch (SqlException e)
            {
                string error = e.Message.Replace('\'', ' ').Replace('"', ' ');
                jSon = "{'msj':-1}";
            }
            Desconectar();
            return jSon;
        }

        /// <summary>
        /// Inserta un registro y retorna la PK del mismo
        /// </summary>
        /// <param name="pa">Procedimiento almacenado</param>
        /// <param name="parametros">Array con los datos a insertar</param>
        /// <returns>Retorna el id del regsitro insertado</returns>
        public String ejecutarMantenimiento(string pa, params object[] parametros)
        {
            string jSon = "";
            try
            {
                IDbDataParameter par = null;
                IDbCommand c = cnn.CreateCommand();
                c.Connection = cnn;
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = pa;
                for (int i = 0; i < parametros.Length; i += 2)
                {
                    par = c.CreateParameter();
                    par.ParameterName = "@" + parametros[i];
                    par.Value = parametros[i + 1];
                    c.Parameters.Add(par);
                }
                cnn.Open();
                IDataReader dr = c.ExecuteReader(CommandBehavior.CloseConnection);
                int afect = dr.RecordsAffected;
                if (afect > 0)
                {
                    jSon += "{'msj': 1, 'rows':" + afect + "}";
                }
                else
                {
                    jSon += "{'msj': 0}";
                }
            }
            catch (SqlException e)
            {
                string error = e.Message.Replace('\'', ' ').Replace('"', ' ');
                jSon = "{\"msj\": -1}";
            }
            Desconectar();
            return jSon;
        }
    }
}