using DLL.DTO.Mantenedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.NEGOCIO.Operaciones.Interfaces
{
    public interface I_N_Bus
    {
        List<DTO_Bus> GetBusByAllActiveForTable();

        int SetNuevoBus(int idTerminal, string ppu, int numeroBus);

        int SetEliminarBus(int idBus);

        int SetEditarBus(DTO_Bus Bus);
    }
}
