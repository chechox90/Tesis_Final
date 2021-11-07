using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DTO.Mantenedor
{
    public class DTO_Empresa
    {
        public int ID_EMPRESA { get; set; }

        public string NOMBRE_EMPRESA { get; set; }

        public string UBICACION_EMPRESA { get; set; }

        public bool ESTADO { get; set; }
    }
}
