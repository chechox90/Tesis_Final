using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConductorEnRed.ViewModels.Administracion
{
    public class VM_Usuario
    {
        public int ID_USUARIO { get; set; }
        public int ID_EMPRESA { get; set; }
        public string RUT { get; set; }
        public string NOMBRE { get; set; }
        public string SEGUNDO_NOMBRE { get; set; }
        public string APELLIDO_PATERNO { get; set; }
        public string APELLIDO_MATERNO { get; set; }
        public string DIRECCION { get; set; }
        public string CORREO { get; set; }
        public string CORREO_ALTERNATIVO { get; set; }
        public DateTime CAMBIO_PASSWORD { get; set; }
        public string NOMBRE_PERFIL { get; set; }
        public string NOMBRE_EMPRESA { get; set; }
        public string NOMBRE_CONTRATO { get; set; }
        public string CODIGO_BARRA { get; set; }
        public bool ESTADO { get; set; }
    }
}