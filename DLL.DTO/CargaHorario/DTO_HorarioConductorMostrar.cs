using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DTO.CargaHorario
{
    public class DTO_HorarioConductorMostrar
    {
        public int ID_USUARIO { get; set; }
        public string NOMBRE { get; set; }
        public string SEGUNDO_NOMBRE { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
        public string RUT { get; set; }
        public string APELLIDO_PATERNO { get; set; }
        public string APELLIDO_MATERNO { get; set; }
        public string NOMBRE_JORNADA { get; set; }
        public string NOMBRE_TERMINAL { get; set; }
        public int ID_TERMINAL { get; set; }
        public int NUMERO_JORNADA { get; set; }
        public DateTime FECHA_HORA_INICIO { get; set; }
        public string FECHA_INICIO { get; set; }
        public string HORA_INICIO { get; set; }
        public bool ESTADO { get; set; }
        public string TIPO_CONTRATO { get; set; }
    }
}
