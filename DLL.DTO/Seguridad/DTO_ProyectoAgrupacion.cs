using System.Collections.Generic;

namespace DLL.DTO.Seguridad
{
    public class DTO_ProyectoAgrupacion
    {
        public byte IdProyectoAgrupacion { get; set; }
        public string NombreInterno { get; set; }
        public string NombreVisible { get; set; }
        public bool Estado { get; set; }
        public List<DTO_Proyecto> Proyectos { get; set; }

        public DTO_ProyectoAgrupacion() { }
    }
}
