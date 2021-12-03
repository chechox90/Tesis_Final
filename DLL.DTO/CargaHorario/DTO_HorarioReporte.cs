using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DTO.CargaHorario
{
    public class DTO_HorarioReporte
    {
        public string NOMBRE_COMPLETO { get; set; }
        public string RUT { get; set; }
        public DateTime FECHA_CARGA { get; set; }
        public string NOMBRE_TERMINAL { get; set; }
        public string NUMERO_JORNADA { get; set; }
        public DateTime FECHA_HORA_INICIO { get; set; }
        public string HORARIO_CUBIERTO { get; set; }
    }
}
