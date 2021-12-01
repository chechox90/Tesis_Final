using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConductorEnRed.Models
{
    public class CargaArchivoModel
    {
        public string RUT { get; set; }
        public int NUMERO_JORNADA { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO { get; set; }
        public string NOMBRE_TERMINAL { get; set; }
        public string NOMBRE_JORNADA { get; set; }
        public string BUS_INICIO { get; set; }
        public string FECHA_INICIO { get; set; }
        public string HORA_INICIO { get; set; }
        public DateTime FECHA_HORA_INICIO { get; set; }
    }
}