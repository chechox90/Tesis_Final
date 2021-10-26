
namespace DLL.DTO.Seguridad
{
    public class DTO_Accion
    {
        public int IDACCION { get; set; }
        public string Nombre { get; set; }
        public bool ItemMenu { get; set; }
        public bool Estado { get; set; }
        public int IdMenu { get; set; }
        public short IdProyecto { get; set; }

        public DTO_Accion() { }
    }
}
