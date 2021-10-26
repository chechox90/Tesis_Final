using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DTO.Seguridad
{
    public class DTO_Novedades
    {
        public byte Orden { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public byte IdTipoNovedad { get; set; }
        public DTO_TIPO_NOVEDAD TipoNovedad { get; set; }
        public byte IdProyecto { get; set; }
    }

    public class DTO_TIPO_NOVEDAD
    {
        public byte Id { get; set; }
        public string Descripcion { get; set; }
        public string Color { get; set; }
    }
}
