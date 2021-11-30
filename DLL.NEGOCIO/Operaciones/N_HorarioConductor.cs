using DLL.DAO.Operaciones.Interfaces;
using DLL.DTO.CargaHorario;
using DLL.DTO.Mantenedor;
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


        public int GetHorariosCubiertos()
        {
            return I_DAO_HorarioConductor.GetHorariosCubiertos();
        }

        public int GetHorariosNoCubiertos()
        {
            return I_DAO_HorarioConductor.GetHorariosNoCubiertos();
        }

        public List<int> GetHorasTrabajadasLibres(DateTime DateIni, DateTime dateFin)
        {
            return I_DAO_HorarioConductor.GetHorasTrabajadasLibres(DateIni, dateFin);
        }

        public N_HorarioConductor(I_DAO_HorarioConductor I_DAO_HorarioConductor)
        {
            this.I_DAO_HorarioConductor = I_DAO_HorarioConductor;
        }

        public List<DTO_HorarioConductorMostrar> GetHorarioConductorByRut(string rut, DateTime fechaIni, DateTime fechaFin)
        {
            return I_DAO_HorarioConductor.GetHorarioConductorByRut(rut, fechaIni, fechaFin);
        }

        public List<DTO_HorarioConductorMostrar> GetHorarioConductorByIdUser(int idUsuario, DateTime fechaIni, DateTime fechaFin)
        {
            return I_DAO_HorarioConductor.GetHorarioConductorByIdUser(idUsuario, fechaIni, fechaFin);
        }

        public DTO_HorarioConductorMostrar GetHorarioConductorByIdUser(int idUsuario,DateTime fechaHora)
        {
            return I_DAO_HorarioConductor.GetHorarioConductorByIdUser(idUsuario,fechaHora);
        }

        public List<DTO_HorarioConductorMostrar> GetHorarioConductorByRutAll(string rut, DateTime fechaIni, DateTime fechaFin)
        {
            return I_DAO_HorarioConductor.GetHorarioConductorByRutAll(rut, fechaIni, fechaFin);
        }

        public List<DTO_HorarioConductorMostrar> GetHorarioConductorById(int idUsuario, DateTime fechaIni, DateTime fechaFin, int idTurno)
        {
            return I_DAO_HorarioConductor.GetHorarioConductorById(idUsuario, fechaIni, fechaFin, idTurno);
        }

        public DTO_HorarioConductorMostrar GetHorarioConductorByIdHorario(int idHorario)
        {
            return I_DAO_HorarioConductor.GetHorarioConductorByIdHorario(idHorario);
        }

        public List<DTO_TipoSolicitud> GetTipoSolicitudAll()
        {
            return I_DAO_HorarioConductor.GetTipoSolicitudAll();
        }

        public string SetEditarHorarioConductor(List<DTO_HorarioConductorMostrar> list)
        {
            return I_DAO_HorarioConductor.SetEditarHorarioConductor(list);
        }

        public int SetIngresaSolicitud(DTO_SolicitudCambioHorario list)
        {
            return I_DAO_HorarioConductor.SetIngresaSolicitud(list);
        }

        public string SetGuardarHorarioConductor(List<DTO_CargarHorarioConductor> list, int idUsuario, string nombreCarga, DateTime fechaCarga, string descripcion)
        {
            return I_DAO_HorarioConductor.SetGuardarHorarioConductor(list, idUsuario,nombreCarga, fechaCarga, descripcion);
        }

    }
}
