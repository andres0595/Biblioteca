using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inicial.Controlador
{
    public class ctlInicio
    {
        public bool esMenuHabilitado(string m, string menus)
        {
            string[] arrayMenus = menus.Split(';');
            for (int i = 0; i < arrayMenus.Length; i++)
            {
                if (arrayMenus[i].Split(',')[0] == m)
                    return true;
            }
            return false;
        }
    }
}