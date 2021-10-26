using System;
using System.Collections.Generic;

namespace DLL.DTO.Seguridad
{
    public class DTO_Proyecto
    {
        public short IdProyecto { get; set; }
        public string NombreInterno { get; set; }
        public string NombreVisible { get; set; }
        public DateTime FechaVersion { get; set; }
        public string Version { get; set; }
        public List<DTO_Novedades> Novedades { get; set; }
        public string PaginaInicio { get; set; }
        public string UrlProyecto { get; set; }
        public string IconUno { get; set; }
        public string IconDos { get; set; }
        public bool Estado { get; set; }
        public List<DTO_Menu> Menus { get; set; }

        public DTO_Proyecto() { }
    }
}
