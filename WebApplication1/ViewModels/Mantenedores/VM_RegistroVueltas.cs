using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConductorEnRed.ViewModels.Mantenedores
{
    public class VM_RegistroVueltas
    {
        public int ID_REGISTRO_VUELTAS { get; set; }
        public int ID_REGISTRO_HORARIO { get; set; }
        public int ID_BUS_INICIO { get; set; }
        public int ID_SERVICIO { get; set; }
        public int NUMERO_VUELTA { get; set; }
        public int ID_TERMINAL_INICIO { get; set; }
        public string NOMBRE_TERMINAL_INICIO { get; set; }
        public int ID_SENTIDO_INICIO { get; set; }
        public System.DateTime FECHA_HORA_INICIO { get; set; }
        public string HORA_INICIO { get; set; }
        public Nullable<int> ID_SENTIDO_FIN { get; set; }
        public int ID_BUS_FIN { get; set; }
        public Nullable<int> ID_TERMINAL_FIN { get; set; }
        public string NOMBRE_TERMINAL_FIN { get; set; }
        public Nullable<System.DateTime> FECHA_HORA_FIN { get; set; }
        public string HORA_FIN { get; set; }
        public bool ESTADO { get; set; }
    }
}