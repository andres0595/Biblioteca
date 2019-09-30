using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace Inicial.Controlador
{
    public class Mantenimiento
    {
        public Mantenimiento()
        {

        }

        public bool LoginMantenimiento(string usuario, string clave)
        {
            return ((clave.Equals(cifrarMd5(claveCifrada()))) && (cifrarMd5(usuario).Equals("ea2adde5c377cb5e09d14b71935c6f32")));
        }

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

        private string claveCifrada()
        {
            string aux = "LCweb" + ((DateTime.Now.Month) * 2 + DateTime.Now.Day);
            return aux;
        }
    }
}