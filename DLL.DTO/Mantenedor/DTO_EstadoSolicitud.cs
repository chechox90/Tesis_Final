using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DTO.Mantenedor
{
    public class DTO_EstadoSolicitud
    {
        public int ID_ESTADO_SOLICITUD { get; set; }
        public string NOMBRE_ESTADO { get; set; }
        public Nullable<bool> ESTADO { get; set; }
    }
}
