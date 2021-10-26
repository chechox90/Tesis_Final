using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DTO.Terminales
{
    public class DTO_Terminal
    {
        public int ID_TERMINAL { get; set; }
        public int ID_EMPRESA { get; set; }
        public string NOMBRE_EMPRESA { get; set; }
        public string NOMBRE_TERMINAL { get; set; }
        public string DIRECCION { get; set; }
        public Nullable<int> NUM_DIRECCION { get; set; }
        public bool ESTADO { get; set; }
    }
}
