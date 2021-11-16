using DLL.DTO.Mantenedor;
using System.Collections.Generic;

namespace DLL.DAO.Operaciones.Interfaces
{
    public interface I_DAO_RegistroHorario
    {
        List<DTO_RegistroHorario> GetRegistroByAll();
    }
}
