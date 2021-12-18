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


        public int GetHorariosCubiertosCount(int idUsuario, DateTime DateIni, DateTime dateFin, int IdTerminal)
        {
            return I_DAO_HorarioConductor.GetHorariosCubiertosCount(idUsuario, DateIni, dateFin,IdTerminal);
        }

        public int GetHorariosNoCubiertosCount(int idUsuario, DateTime DateIni, DateTime dateFin,int Idterminal)
        {
            return I_DAO_HorarioConductor.GetHorariosNoCubiertosCount(idUsuario, DateIni, dateFin,Idterminal);
        }

        public List<int> GetHorasTrabajadasLibres(int idUsuario, DateTime DateIni, DateTime dateFin)
        {
            return I_DAO_HorarioConductor.GetHorasTrabajadasLibres(idUsuario, DateIni, dateFin);
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

        public List<DTO_HorarioConductorMostrar> GetHorarioConductorByIdUserNumJor(int idUsuario, DateTime fechaIni, int numeroJornada)
        {
            return I_DAO_HorarioConductor.GetHorarioConductorByIdUserNumJor(idUsuario, fechaIni, numeroJornada);
        }

        public DTO_HorarioConductorMostrar GetHorarioConductorByIdUser(int idUsuario, DateTime fechaHora)
        {
            return I_DAO_HorarioConductor.GetHorarioConductorByIdUser(idUsuario, fechaHora);
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

        public List<DTO_SolicitudCambioHorario> GetSolicitudAllFilter(DateTime fechaIni, DateTime fechaFin, int tipoSolicitud, int idTerminal)
        {
            return I_DAO_HorarioConductor.GetSolicitudAllFilter(fechaIni, fechaFin, tipoSolicitud, idTerminal);
        }

        public List<DTO_HorarioConductorMostrar> GetRegistroVueltasByAll(DateTime desde, DateTime hasta, int idterminal, string run)
        {
            return I_DAO_HorarioConductor.GetRegistroVueltasByAll(desde, hasta, idterminal, run);
        }

        public List<DTO_EstadoSolicitud> GetTipoSolicitudesRespuesta()
        {
            return I_DAO_HorarioConductor.GetTipoSolicitudesRespuesta();
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
            return I_DAO_HorarioConductor.SetGuardarHorarioConductor(list, idUsuario, nombreCarga, fechaCarga, descripcion);
        }

        public int SetCambiarEstadoCubierto(int idTurno)
        {
            return I_DAO_HorarioConductor.SetCambiarEstadoCubierto(idTurno);
        }

        public int SetIngresaRespuestaSolicitud(int idSolicitud,int idEstadoSolicitud)
        {
            return I_DAO_HorarioConductor.SetIngresaRespuestaSolicitud(idSolicitud, idEstadoSolicitud);
        }
    }
}
