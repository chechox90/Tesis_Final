using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConductorEnRed.Models
{
    public class CargaHorarioModel
    {
        public int ID_USUARIO { get; set; }
        public string RUT { get; set; }
        public int ID_TERMINAL { get; set; }
        public int NUMERO_JORNADA { get; set; }
        public string FECHA_INICIO { get; set; }
        public string HORA_INICIO { get; set; }
        public string COMENTARIO { get; set; }
    }
}