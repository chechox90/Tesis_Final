using DLL.NEGOCIO.Generico.Interfaces;
using System.IO;

namespace DLL.NEGOCIO.Generico
{
    public class N_Directory : I_N_Directory
    {
        public void VerificaExiste_o_CreaDirectorio(string directorio)
        {
            directorio = directorio.Replace("~/", "");
            string[] carpetas = directorio.Split('/');
            string carpetaNavegacion = "";

            foreach(string carpeta in carpetas)
            {
                carpetaNavegacion += carpeta;
                if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~/" + carpetaNavegacion)))
                    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("~/" + carpetaNavegacion));

                carpetaNavegacion += "/";
            }
        }
    }
}
