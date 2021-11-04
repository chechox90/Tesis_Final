using DLL.DTO.Mantenedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.NEGOCIO.Operaciones.Interfaces
{
    public interface I_N_Servicio
    {
        List<DTO_Servicio> GetServicioByAllActiveForTable();
        int SetNuevoServicio(string nombreTer, string direccion, string numDire);

        int SetEliminarServicio(int idServicio);
    }
}
