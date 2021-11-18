using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConductorEnRed.ViewModels.Mantenedores
{
    public class VM_Registro_Horario
    {
        public int ID_REGISTRO_HORARIO { get; set; }
        public int ID_USUARIO { get; set; }
        public int ID_TERMINAL_INICIO { get; set; }
        public System.DateTime FECHA_HORA_INICIO { get; set; }
        public string HORA_INICIO { get; set; }
        public Nullable<int> ID_TERMINAL_FIN { get; set; }
        public Nullable<System.DateTime> FECHA_HORA_FIN { get; set; }
        public string HORA_FIN { get; set; }
        public bool ESTADO { get; set; }
    }
}