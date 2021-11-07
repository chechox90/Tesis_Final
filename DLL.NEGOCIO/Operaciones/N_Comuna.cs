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
    public class N_Comuna : I_N_Comuna
    {
        private readonly I_DAO_Comuna I_DAO_Comuna;
        public N_Comuna(I_DAO_Comuna I_DAO_Comuna)
        {
            this.I_DAO_Comuna = I_DAO_Comuna;
        }

        public List<DTO_Comuna> GetComunaByAllActiveForTable()
        {
            return I_DAO_Comuna.GetComunaByAllActiveForTable();
        }

        public int SetNuevaComuna(string nomComuna)
        {
            return I_DAO_Comuna.SetNuevaComuna(nomComuna);
        }

        public int SetEliminarComuna(int idComuna)
        {
            return I_DAO_Comuna.SetEliminarComuna(idComuna);
        }

        public int SetEditarComuna(DTO_Comuna COMUNA)
        {
            return I_DAO_Comuna.SetEditarComuna(COMUNA);
        }

    }
}
