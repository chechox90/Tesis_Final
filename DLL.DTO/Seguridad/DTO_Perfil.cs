using System.Collections.Generic;

namespace DLL.DTO.Seguridad
{
    public class DTO_Perfil
    {
        public int IdPerfil { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public List<DTO_Accion> Acciones { get; set; }

        public DTO_Perfil() { }
    }
}
