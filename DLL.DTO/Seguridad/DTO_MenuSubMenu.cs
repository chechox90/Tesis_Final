
namespace DLL.DTO.Seguridad
{
    public class DTO_MenuSubMenu
    {
        public int IdMenu { get; set; }
        public int? IdPadre { get; set; }
        public int Orden { get; set; }
        public string Nombre { get; set; }
        public string Controlador { get; set; }
        public string IconoUrl { get; set; }
        public string URL { get; set; }
        public bool Estado { get; set; }

        public DTO_MenuSubMenu() { }
    }
}
