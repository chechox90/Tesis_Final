using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WebApplication1.Helpers
{
    public class CreateFolderHelper
    {
        public static void CreateFolders(string ruta)
        {
            string[] folders = ruta.Split('/');
            string rutaConcat = "";
            for (int x = 0; x < folders.Length; x++)
            {
                if (x == 0)
                    rutaConcat = folders[x];
                else
                    rutaConcat = rutaConcat + "/" + folders[x];

                if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~/" + rutaConcat)))
                    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("~/" + rutaConcat));
            }
        }
    }
}
