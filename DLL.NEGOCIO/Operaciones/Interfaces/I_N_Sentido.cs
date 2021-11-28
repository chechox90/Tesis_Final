using DLL.DTO.Mantenedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.NEGOCIO.Operaciones.Interfaces
{
    public interface I_N_Sentido
    {
        List<DTO_Sentido> GetSentidoByAll();

        int GetSentidoByNombre(string servico, int IdEmpresa);

        int SetNuevoSentido(string servico, string sentidoCorto);

        int SetEliminarSentido(int idSntido);

        int SetEditarSentido(DTO_Sentido SENTIDO);
    }
}
