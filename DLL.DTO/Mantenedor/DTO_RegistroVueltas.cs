using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DTO.Mantenedor
{
    public class DTO_RegistroVueltas
    {
        public int ID_REGISTRO_VUELTAS { get; set; }
        public int ID_REGISTRO_HORARIO { get; set; }
        public int NUMERO_VUELTA { get; set; }
        public int ID_TERMINAL_INICIO { get; set; }
        public System.DateTime FECHA_HORA_INICIO { get; set; }
        public Nullable<int> ID_TERMINAL_FIN { get; set; }
        public Nullable<System.DateTime> FECHA_HORA_FIN { get; set; }
    }
}
