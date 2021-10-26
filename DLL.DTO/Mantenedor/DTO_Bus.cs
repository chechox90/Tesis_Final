using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DTO.Mantenedor
{
    public class DTO_Bus
    {
        public int ID_BUS { get; set; }
        public int ID_TERMINAL { get; set; }
        public string NOMBRE_TERMINAL { get; set; }
        public int ID_INTERNO_BUS { get; set; }
        public string PPU { get; set; }
        public bool ESTADO { get; set; }

    }
}
