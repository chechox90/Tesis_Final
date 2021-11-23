using DLL.DTO.Mantenedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.NEGOCIO.Operaciones.Interfaces
{
    public interface I_N_RegistroHorario
    {
        List<DTO_RegistroVueltas> GetRegistroVueltasByAll(int idRegistroHorario);

        DTO_RegistroVueltas GetRegistroVueltasByAllId(int idVuelta);

        DTO_RegistroHorario GetRegistroHorarioByAll(int idUsuario);

        int SetIngresaHorario(DTO_RegistroHorario list);

        int SetIngresaVuelta(DTO_RegistroVueltas list);

        int SetFinalizaVuelta(DTO_RegistroVueltas list);
        
        int SetEliminarVuelta(int idVuelta);

        int SetEditaVuelta(DTO_RegistroVueltas list);

        int SetIngresaFinHorario(DTO_RegistroHorario list);
    }
}
