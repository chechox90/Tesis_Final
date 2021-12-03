﻿using DLL.DTO.CargaHorario;
using DLL.DTO.Mantenedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.NEGOCIO.Operaciones.Interfaces
{
    public interface I_N_HorarioConductor
    {
        int GetHorariosCubiertos(DateTime DateIni, DateTime dateFin);

        int GetHorariosNoCubiertos(DateTime DateIni, DateTime dateFin);

        List<int> GetHorasTrabajadasLibres(DateTime DateIni, DateTime dateFin);
        
        string SetGuardarHorarioConductor(List<DTO_CargarHorarioConductor> list, int idUsuario, string nombreCarga, DateTime fechaCarga, string descripcion);

        List<DTO_HorarioConductorMostrar> GetHorarioConductorByRut(string rut, DateTime fechaIni, DateTime fechaFin);

        List<DTO_HorarioConductorMostrar> GetHorarioConductorByIdUser(int idUsuario, DateTime fechaIni, DateTime fechaFin);

        List<DTO_HorarioConductorMostrar> GetHorarioConductorByIdUserNumJor(int idUsuario, DateTime fechaIni, int numeroJornada);

        DTO_HorarioConductorMostrar GetHorarioConductorByIdUser(int idUsuario, DateTime fechaHora);

        List<DTO_HorarioConductorMostrar> GetHorarioConductorByRutAll(string rut, DateTime fechaIni, DateTime fechaFin);

        List<DTO_HorarioConductorMostrar> GetHorarioConductorById(int idUsuario, DateTime fechaIni, DateTime fechaFin, int idTurno);
        
        DTO_HorarioConductorMostrar GetHorarioConductorByIdHorario(int idHorario);

        string SetEditarHorarioConductor(List<DTO_HorarioConductorMostrar> list);

        int SetIngresaSolicitud(DTO_SolicitudCambioHorario list);

        List<DTO_TipoSolicitud> GetTipoSolicitudAll();

        List<DTO_HorarioConductorMostrar> GetRegistroVueltasByAll(DateTime desde, DateTime hasta, int idterminal, string run);
    }
}
