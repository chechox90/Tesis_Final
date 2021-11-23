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


        public List<DTO_RegistroVueltas> GetRegistroVueltasByAll(int idRegistroHorario)
        {
            return I_Dao_RegistroHorario.GetRegistroVueltasByAll(idRegistroHorario);
        }

        public DTO_RegistroHorario GetRegistroHorarioByAll(int idUsuario)
        {
            return I_Dao_RegistroHorario.GetRegistroHorarioByAll(idUsuario);
        }

        public DTO_RegistroVueltas GetRegistroVueltasByAllId(int idVuelta)
        {
            return I_Dao_RegistroHorario.GetRegistroVueltasByAllId(idVuelta);
        }

        public int SetIngresaHorario(DTO_RegistroHorario list)
        {
            return I_Dao_RegistroHorario.SetIngresaHorario(list);
        }
        
        public int SetIngresaVuelta(DTO_RegistroVueltas list)
        {
            return I_Dao_RegistroHorario.SetIngresaVuelta(list);
        }

        public int SetFinalizaVuelta(DTO_RegistroVueltas list)
        {
            return I_Dao_RegistroHorario.SetFinalizaVuelta(list);
        }

        public int SetEliminarVuelta(int idVuelta)
        {
            return I_Dao_RegistroHorario.SetEliminarVuelta(idVuelta);
        }

        public int SetIngresaFinHorario(DTO_RegistroHorario list)
        {
            return I_Dao_RegistroHorario.SetIngresaFinHorario(list);
        }

        public int SetEditaVuelta(DTO_RegistroVueltas list)
        {
            return I_Dao_RegistroHorario.SetEditaVuelta(list);
        }

    }
}
