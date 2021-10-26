using DLL.DAO.Operaciones.Interfaces;
using DLL.DTO.Terminales;
using DLL.NEGOCIO.Operaciones.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.NEGOCIO.Operaciones
{
    public class N_Terminal : I_N_Terminal
    {
        private readonly I_DAO_Terminal I_DAO_Terminal;
        public N_Terminal(I_DAO_Terminal I_DAO_Terminal)
        {
            this.I_DAO_Terminal = I_DAO_Terminal;
        }

        public int GetTerminalByNombre(string nombreTerminal, int idEmpresa)
        {
            return I_DAO_Terminal.GetTerminalByNombre(nombreTerminal, idEmpresa);
        }

        public List<DTO_Terminal> GetTerminalByAllActive()
        {
            return I_DAO_Terminal.GetTerminalByAllActive();
        }
        public List<DTO_Terminal> GetTerminalByAllActiveForTable()
        {
            return I_DAO_Terminal.GetTerminalByAllActiveForTable();
        }
    }
}
