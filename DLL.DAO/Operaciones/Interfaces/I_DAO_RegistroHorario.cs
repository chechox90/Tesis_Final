using DLL.DTO.Mantenedor;
using System.Collections.Generic;

namespace DLL.DAO.Operaciones.Interfaces
{
    public interface I_DAO_RegistroHorario
    {
        int SetIngresaHorario(DTO_RegistroHorario list);
        
        int SetIngresaVuelta(DTO_RegistroVueltas list);

        List<DTO_RegistroVueltas> GetRegistroVueltasByAll(int idRegistroHorario);

        DTO_RegistroHorario GetRegistroHorarioByAll(int idUsuario);

    }
}
