using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DTO.Seguridad
{
    public class DTO_TipoContrato
    {
        public int ID_TIPO_CONTRATO { get; set; }
        public string NOMBRE_TIPO_CONTRATO { get; set; }
        public string NOMBRE_CORTO_CONTRATRO { get; set; }
        public System.TimeSpan CANTIDAD_HORAS_CONTRATO { get; set; }
        public bool ESTADO { get; set; }
    }
}
