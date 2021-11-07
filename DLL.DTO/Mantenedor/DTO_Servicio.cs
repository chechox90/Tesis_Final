using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DTO.Mantenedor
{
    public class DTO_Servicio
    {
        public int ID_SERVICIO { get; set; }
        public int ID_EMPRESA { get; set; }
        public string NOMBRE_EMPRESA { get; set; }
        public string NOMBRE_SERVICIO { get; set; }
        public Nullable<System.TimeSpan> HORARIO_INI { get; set; }
        public string HORARIO_INICIO { get; set; }
        public Nullable<System.TimeSpan> HORARIO_FIN { get; set; }
        public string HORARIO_FIN_ { get; set; }
        public bool ESTADO { get; set; }
    }
}
