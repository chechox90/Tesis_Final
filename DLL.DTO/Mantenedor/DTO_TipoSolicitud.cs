using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DTO.Mantenedor
{
    public class DTO_TipoSolicitud
    {
        public int ID_TIPO_SOLICITUD { get; set; }
        public string NOMBRE_SOLICITUD { get; set; }
        public bool ESTADO { get; set; }
    }
}
