using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DTO.Mantenedor
{
    public class DTO_SolicitudCambioHorario
    {
        public int ID_SOLICITUD_CAMBIO { get; set; }
        public int ID_TIPO_SOLICITUD { get; set; }
        public int ID_HORARIO_CAMBIAR { get; set; }
        public int ID_ESTADO_SOLICITUD { get; set; }
        public int ID_USUARIO_SOLICITA { get; set; }
        public int? ID_USUARIO_APRUEBA { get; set; }
        public System.DateTime FECHA_REGISTRO_SOLICITUD { get; set; }
        public Nullable<System.DateTime> FECHA_APROBACION { get; set; }
        public string COMENTARIO_MOTIVO { get; set; }
        public string COMENTARIO_ADICIONAL { get; set; }
        public bool ESTADO { get; set; }

    }
}
