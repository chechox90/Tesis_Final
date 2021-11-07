using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DTO.Mantenedor
{
    public class DTO_Comuna
    {
        public int ID_COMUNA { get; set; }
        public string NOMBRE_COMUNA { get; set; }
        public string MOTIVO_EDICION { get; set; }
        public Nullable<bool> ESTADO { get; set; }

    }
}
