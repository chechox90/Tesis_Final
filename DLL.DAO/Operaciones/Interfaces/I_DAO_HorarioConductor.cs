using DLL.DTO.CargaHorario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DAO.Operaciones.Interfaces
{
    public interface I_DAO_HorarioConductor
    {
        string SetGuardarHorarioConductor(List<DTO_CargarHorarioConductor> list, string nombreCarga, DateTime fechaCarga, string descripcion);
       List<DTO_HorarioConductorMostrar> GetHorarioConductorByRut(string rut, DateTime fechaIni, DateTime fechaFin);
    }
}
