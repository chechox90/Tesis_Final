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
    
    public partial class SOLICITUD_CAMBIO_HORARIO
    {
        public int ID_SOLICITUD_CAMBIO { get; set; }
        public int ID_HORARIO { get; set; }
        public int ID_TIPO_SOLICITUD { get; set; }
        public int ID_ESTADO_SOLICITUD { get; set; }
        public int ID_USUARIO_SOLICITA { get; set; }
        public Nullable<int> ID_USUARIO_APRUEBA { get; set; }
        public System.DateTime FECHA_REGISTRO_SOLICITUD { get; set; }
        public Nullable<System.DateTime> FECHA_APROBACION { get; set; }
        public string COMENTARIO_MOTIVO { get; set; }
        public string COMENTARIO_ADICIONAL { get; set; }
        public bool ESTADO { get; set; }
    
        public virtual ESTADO_SOLICITUD ESTADO_SOLICITUD { get; set; }
        public virtual HORARIO_CONDUCTOR HORARIO_CONDUCTOR { get; set; }
        public virtual TIPO_SOLICITUD_CAMBIO TIPO_SOLICITUD_CAMBIO { get; set; }
    }
}
