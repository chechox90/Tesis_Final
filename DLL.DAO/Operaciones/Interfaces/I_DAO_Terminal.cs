using DLL.DTO.Terminales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DAO.Operaciones.Interfaces
{
    public interface I_DAO_Terminal
    {
        int GetTerminalByNombre(string nombreTerminal, int idEmpresa);
        
        List<DTO_Terminal> GetTerminalByAllActive();
        
        List<DTO_Terminal> GetTerminalByAllActiveForTable();

        int SetNuevoTerminal(string nombreTer, string direccion, int numDire);

        int SetEliminarTerminal(int idTer);

        int SetEditarTerminal(DTO_Terminal TERMINAL);

    }
}
