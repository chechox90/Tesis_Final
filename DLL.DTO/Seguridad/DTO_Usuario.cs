using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DTO.Seguridad
{
    public class DTO_Usuario
    {
        public DTO_Usuario() { }

        public int ID_USUARIO { get; set; }
        public int ID_COMUNA { get; set; }
        public int ID_TIPO_CONTRATO { get; set; }
        public int ID_EMPRESA { get; set; }
        public string CLAVE { get; set; }
        public string RUT { get; set; }
        public string NOMBRE { get; set; }
        public string SEGUNDO_NOMBRE { get; set; }
        public string APELLIDO_PATERNO { get; set; }
        public string APELLIDO_MATERNO { get; set; }
        public string DIRECCION { get; set; }
        public string CODIGO_BARRA { get; set; }
        public string CORREO { get; set; }
        public string CORREO_ALTERNATIVO { get; set; }
        public DateTime CAMBIO_PASSWORD { get; set; }
        public bool ADMINISTRADOR { get; set; }
        public bool ESTADO { get; set; }

        public string NombreCompleto
        {
            get { return (this.NOMBRE + " " + this.APELLIDO_PATERNO).Trim(); }
        }

        public DTO_ProyectoAgrupacion ProyectoAgrupacion { get; set; }
        public DTO_Proyecto Proyecto { get; set; }
        public List<DTO_Perfil> Perfiles { get; set; }
        public List<DTO_PermisosEspeciales> PermisosEspeciales { get; set; }
    }
}
