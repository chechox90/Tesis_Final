using DLL.DTO.Mantenedor;
using System;
using System.Collections.Generic;

namespace DLL.DAO.Operaciones.Interfaces
{
    public interface I_DAO_RegistroHorario
    {
        List<DTO_RegistroVueltas> GetRegistroVueltasByAll(int idRegistroHorario);
        
        List<DTO_RegistroVueltas> GetRegistroVueltasByAll(int idUsuario,DateTime desde,DateTime hasta);

        List<DTO_RegistroVueltas> GetRegistroVueltasByAllIdTerminal(int idUsuario,DateTime desde,DateTime hasta,int idTerminal);

        DTO_RegistroVueltas GetRegistroVueltasByAllId(int idVuelta);

        DTO_RegistroHorario GetRegistroHorarioByAll(int idHorario);

        int SetIngresaHorario(DTO_RegistroHorario list);

        int SetIngresaVuelta(DTO_RegistroVueltas list);
        
        int SetFinalizaVuelta(DTO_RegistroVueltas list);
        
        int SetEliminarVuelta(int idVuelta);

        int SetIngresaFinHorario(DTO_RegistroHorario list);

        int SetEditaVuelta(DTO_RegistroVueltas list);

    }
}
