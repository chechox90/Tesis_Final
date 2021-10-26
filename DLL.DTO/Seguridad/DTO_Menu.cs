using System.Collections.Generic;

namespace DLL.DTO.Seguridad
{
    public class DTO_Menu
    {
        public int IdMenu { get; set; }
        public int? IdPadre { get; set; }
        public int Orden { get; set; }
        public string Nombre { get; set; }
        public string Controlador { get; set; }
        public string IconoUrl { get; set; }
        public string URL { get; set; }
        public bool Estado { get; set; }
        public List<DTO_MenuSubMenu> MenuHijo { get; set; }

        public DTO_Menu() { }
    }
}
