using DLL.DAO.Operaciones.Interfaces;
using DLL.DTO.Mantenedor;
using DLL.NEGOCIO.Operaciones.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.NEGOCIO.Operaciones
{
    public class N_Servicio: I_N_Servicio
    {
        private readonly I_DAO_Servicio I_DAO_Servicio;
        public N_Servicio(I_DAO_Servicio I_DAO_Servicio)
        {
            this.I_DAO_Servicio = I_DAO_Servicio;
        }

        public List<DTO_Servicio> GetServicioByAllActiveForTable()
        {
            return I_DAO_Servicio.GetServicioByAllActiveForTable();
        }
    }
}
