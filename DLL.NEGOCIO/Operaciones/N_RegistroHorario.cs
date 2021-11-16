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
    public class N_RegistroHorario: I_N_RegistroHorario
    {
        private readonly I_DAO_RegistroHorario I_Dao_RegistroHorario;
        public N_RegistroHorario(I_DAO_RegistroHorario i_dao_RegistroHorario)
        {
            this.I_Dao_RegistroHorario = i_dao_RegistroHorario;
        }

        public List<DTO_RegistroHorario> GetRegistroByAll()
        {
            return I_Dao_RegistroHorario.GetRegistroByAll();
        }


    }
}
