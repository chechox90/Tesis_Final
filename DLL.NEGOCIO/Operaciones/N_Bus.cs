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
    public class N_Bus : I_N_Bus
    {
        private readonly I_DAO_Bus I_DAO_Bus;
        public N_Bus(I_DAO_Bus I_DAO_Bus)
        {
            this.I_DAO_Bus = I_DAO_Bus;
        }

        public List<DTO_Bus> GetBusByAllActiveForTable()
        {
            return I_DAO_Bus.GetBusByAllActiveForTable();
        }

        public int SetNuevoBus(int idTerminal, string ppu, int numeroBus)
        {
            return I_DAO_Bus.SetNuevoBus(idTerminal,ppu,numeroBus);
        }

        public int SetEliminarBus(int idBus)
        {
            return I_DAO_Bus.SetEliminarBus(idBus);
        }

        public int SetEditarBus(DTO_Bus Bus)
        {
            return I_DAO_Bus.SetEditarBus(Bus);
        }

    }
}
