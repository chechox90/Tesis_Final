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
    public class N_Sentido : I_N_Sentido
    {
        private readonly I_DAO_Sentido I_DAO_Sentido;
        public N_Sentido(I_DAO_Sentido I_DAO_Sentido)
        {
            this.I_DAO_Sentido = I_DAO_Sentido;
        }

        public List<DTO_Sentido> GetSentidoByAll()
        {
            return I_DAO_Sentido.GetSentidoByAll();
        }

        public int SetNuevoSentido(string servicio, string servicioCorto)
        {
            return I_DAO_Sentido.SetNuevoSentido(servicio, servicioCorto);
        }

        public int SetEliminarSentido(int idSentido)
        {
            return I_DAO_Sentido.SetEliminarSentido(idSentido);
        }

        public int SetEditarSentido(DTO_Sentido SENTIDO)
        {
            return I_DAO_Sentido.SetEditarSentido(SENTIDO);
        }

    }
}
