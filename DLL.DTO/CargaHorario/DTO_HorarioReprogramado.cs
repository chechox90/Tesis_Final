using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DTO.CargaHorario
{
    public class DTO_HorarioReprogramado
    {
        public int ID_USUARIO { get; set; }
        public string RUT { get; set; }         
        public int ID_TERMINAL { get; set; }
        public string NOMBRE_JORNADA { get; set; }
        public int NUMERO_JORNADA { get; set; }
        public DateTime FECHA_HORA_INICIO { get; set; }
        public string FECHA_INICIO { get; set; }
        public string HORA_INICIO { get; set; }
        public string COMENTARIO { get; set; }

    }
}
