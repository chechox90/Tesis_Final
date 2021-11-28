using DLL.DTO.Mantenedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DAO.Operaciones.Interfaces
{
    public interface I_DAO_Bus
    {
        List<DTO_Bus> GetBusByAllActiveForTable();

        int GetBusByNombre(string nombre);

        int SetNuevoBus(int idTerminal, string ppu, int numeroBus);

        int SetEliminarBus(int idBus);

        int SetEditarBus(DTO_Bus Bus);


    }
}
