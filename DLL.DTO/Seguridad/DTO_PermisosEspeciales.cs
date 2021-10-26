
namespace DLL.DTO.Seguridad
{
    public class DTO_PermisosEspeciales
    {
        public int IdUsuario { get; set; }
        public short IdProyecto { get; set; }
        public int IdAccion { get; set; }
        public int IdMenu { get; set; }
        public bool TipoPermiso { get; set; }

        public DTO_PermisosEspeciales() { }
    }
}
