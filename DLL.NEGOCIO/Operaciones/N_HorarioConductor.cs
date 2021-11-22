using DLL.DAO.Operaciones.Interfaces;
using DLL.DTO.CargaHorario;
using DLL.NEGOCIO.Operaciones.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.NEGOCIO.Operaciones
{
    public class N_HorarioConductor : I_N_HorarioConductor
    {
        private readonly I_DAO_HorarioConductor I_DAO_HorarioConductor;

        public N_HorarioConductor(I_DAO_HorarioConductor I_DAO_HorarioConductor)
        {
            this.I_DAO_HorarioConductor = I_DAO_HorarioConductor;
        }

        public string SetGuardarHorarioConductor(List<DTO_CargarHorarioConductor> list, string nombreCarga, DateTime fechaCarga, string descripcion)
        {
            return I_DAO_HorarioConductor.SetGuardarHorarioConductor(list, nombreCarga, fechaCarga, descripcion);
        }

        public List<DTO_HorarioConductorMostrar> GetHorarioConductorByRut(string rut, DateTime fechaIni, DateTime fechaFin)
        {
            return I_DAO_HorarioConductor.GetHorarioConductorByRut(rut, fechaIni, fechaFin);
        }

        public List<DTO_HorarioConductorMostrar> GetHorarioConductorById(int idUsuario, DateTime fechaIni, DateTime fechaFin, int idTurno)
        {
            return I_DAO_HorarioConductor.GetHorarioConductorById(idUsuario,fechaIni,fechaFin, idTurno);
        }

        public DTO_HorarioConductorMostrar GetHorarioConductorByIdHorario(int idHorario)
        {
            return I_DAO_HorarioConductor.GetHorarioConductorByIdHorario(idHorario);
        }

        public string SetEditarHorarioConductor(List<DTO_HorarioConductorMostrar> list)
        {
            return I_DAO_HorarioConductor.SetEditarHorarioConductor(list);
        }


    }
}
