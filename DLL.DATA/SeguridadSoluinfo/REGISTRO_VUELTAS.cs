//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DLL.DATA.SeguridadSoluinfo
{
    using System;
    using System.Collections.Generic;
    
    public partial class REGISTRO_VUELTAS
    {
        public int ID_REGISTRO_VUELTAS { get; set; }
        public int ID_REGISTRO_HORARIO { get; set; }
        public int NUMERO_VUELTA { get; set; }
        public int ID_TERMINAL_INICIO { get; set; }
        public System.DateTime FECHA_HORA_INICIO { get; set; }
        public Nullable<int> ID_TERMINAL_FIN { get; set; }
        public Nullable<System.DateTime> FECHA_HORA_FIN { get; set; }
    
        public virtual REGISTRO_HORARIO REGISTRO_HORARIO { get; set; }
    }
}
