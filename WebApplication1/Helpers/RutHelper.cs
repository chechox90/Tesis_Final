using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ConductorEnRed.Helpers
{
    public class RutHelper
    {
        public static bool ValidarRutCompleto(string rut)
        {
            string rutConGuion = "";
            rut = rut.Replace(".", "");
            if (!rut.Contains("-"))
                rutConGuion = rut = rut.Substring(0, rut.Length - 1) + "-" + rut.Substring(rut.Length - 1, 1);
            else
                rutConGuion = rut;

            Regex expresion = new Regex("^([0-9]+-[0-9K])$");
            string dv = rut.Substring(rutConGuion.Length - 1, 1);

            if (!expresion.IsMatch(rut))
            {
                return false;
            }
            char[] charCorte = { '-' };
            string[] rutTemp = rut.Split(charCorte);
            if (dv != Digito(int.Parse(rutTemp[0])))
            {
                return false;
            }
            return true;
        }

        public static string Digito(int rut)
        {
            int suma = 0;
            int multiplicador = 1;
            while (rut != 0)
            {
                multiplicador++;
                if (multiplicador == 8)
                    multiplicador = 2;
                suma += (rut % 10) * multiplicador;
                rut = rut / 10;
            }
            suma = 11 - (suma % 11);
            if (suma == 11)
            {
                return "0";
            }
            else if (suma == 10)
            {
                return "K";
            }
            else
            {
                return suma.ToString();
            }
        }
    }
}