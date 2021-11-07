using DLL.DTO.Mantenedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.NEGOCIO.Operaciones.Interfaces
{
    public interface I_N_Comuna
    {
        List<DTO_Comuna> GetComunaByAllActiveForTable();

        int SetNuevaComuna(string nomComuna);

        int SetEliminarComuna(int idComuna,string motivo);

        int SetEditarComuna(DTO_Comuna COMUNA);
    }
}
