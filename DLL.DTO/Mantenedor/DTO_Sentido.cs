using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DTO.Mantenedor
{
    public class DTO_Sentido
    {
        public int ID_SENTIDO { get; set; }
        public string NOMBRE_SENTIDO { get; set; }
        public string NOMBRE_CORTO_SENTIDO { get; set; }
        public Nullable<bool> ESTADO { get; set; }
    }
}
