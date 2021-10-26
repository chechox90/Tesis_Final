using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DTO.CargaHorario
{
  public  class DTO_CargarHorarioConductor
    {
        public int TERMINAL_INICIO { get; set; }
        public int ID_CONDUCTOR { get; set; }
        public int BUS_INICIO { get; set; }
        public int NUMERO_JORNADA { get; set; }
        public string NOMBRE_CARGA { get; set; }
        public string DESCRIPCION_CARGA { get; set; }
        public DateTime FECHA_HORA_INICIO { get; set; }
    }
}
