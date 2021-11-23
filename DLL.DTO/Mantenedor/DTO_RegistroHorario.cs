using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DTO.Mantenedor
{
    public class DTO_RegistroHorario
    {
        public int ID_REGISTRO_HORARIO { get; set; }
        public int ID_HORARIO { get; set; }
        public int ID_USUARIO { get; set; }
        public int ID_TERMINAL_INICIO { get; set; }
        public string NOMBRE_TERMINAL { get; set; }
        public System.DateTime FECHA_HORA_INICIO { get; set; }
        public Nullable<int> ID_TERMINAL_FIN { get; set; }
        public Nullable<System.DateTime> FECHA_HORA_FIN { get; set; }
        public bool ESTADO { get; set; }
    }
}
