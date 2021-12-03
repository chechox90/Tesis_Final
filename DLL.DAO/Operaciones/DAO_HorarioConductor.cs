using DLL.DAO.Operaciones.Interfaces;
using DLL.DATA.SeguridadSoluinfo;
using DLL.DTO.CargaHorario;
using DLL.DTO.Mantenedor;
using DLL.DTO.Seguridad;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DAO.Operaciones
{
    public class DAO_HorarioConductor : I_DAO_HorarioConductor
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public int GetHorariosCubiertos(DateTime DateIni, DateTime dateFin)
        {
            using (SolusegEntities context = new SolusegEntities())
            {
                DateTime date = DateTime.Now;
                return (from hc in context.HORARIO_CONDUCTOR
                        where
                          hc.FECHA_INICIO >= DateIni
                           && hc.FECHA_INICIO <= dateFin
                           && hc.HORARIO_CUBIERTO == true
                           && hc.ESTADO == true
                        select hc.HORARIO_CUBIERTO).Count();
            }
        }

        public int GetHorariosNoCubiertos(DateTime DateIni, DateTime dateFin)
        {
            using (SolusegEntities context = new SolusegEntities())
            {
                DateTime date = DateTime.Now;
                return (from hc in context.HORARIO_CONDUCTOR
                        where
                         hc.FECHA_INICIO >= DateIni && hc.FECHA_INICIO <= dateFin
                        && hc.HORARIO_CUBIERTO == false
                        && hc.ESTADO == true
                        select hc.ID_HORARIO).Count();
            }
        }

        public List<int> GetHorasTrabajadasLibres(DateTime DateIni, DateTime dateFin)
        {
            using (SolusegEntities context = new SolusegEntities())
            {
                List<int> respu_ = new List<int>();
                respu_ = context.Database.SqlQuery<int>("autorizacion.usp_get_horas_trabajadas_vs_horas_libres @p0, @p1", DateIni, dateFin).ToList();

                return respu_;
            }
        }

        public DAO_HorarioConductor()
        {
            XmlConfigurator.Configure();
        }

        public List<DTO_HorarioConductorMostrar> GetHorarioConductorByRut(string rut, DateTime fechaIni, DateTime fechaFin)
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    fechaFin = fechaFin.AddDays(1);

                    List<DTO_HorarioConductorMostrar> dtoHorario = (from hrcon in context.HORARIO_CONDUCTOR
                                                                    join usr in context.USUARIOS_SISTEMA on hrcon.ID_USUARIO equals usr.ID_USUARIO
                                                                    join ter in context.TERMINAL on hrcon.ID_TERMINAL_INICIO equals ter.ID_TERMINAL
                                                                    join car in context.CARGA_HORARIO on hrcon.ID_CARGA_HORARIO equals car.ID_CARGA_HORARIO
                                                                    where (hrcon.FECHA_INICIO >= fechaIni && hrcon.FECHA_INICIO <= fechaFin && usr.ESTADO == true)                                                                    
                                                                    select new DTO_HorarioConductorMostrar
                                                                    {
                                                                        ID_HORARIO = hrcon.ID_HORARIO,
                                                                        RUT = usr.RUT,
                                                                        NOMBRE = usr.NOMBRE,
                                                                        SEGUNDO_NOMBRE = usr.SEGUNDO_NOMBRE,
                                                                        APELLIDO_PATERNO = usr.APELLIDO_PATERNO,
                                                                        APELLIDO_MATERNO = usr.APELLIDO_MATERNO,
                                                                        ESTADO = usr.ESTADO,
                                                                        ID_TERMINAL = hrcon.ID_TERMINAL_INICIO,
                                                                        NOMBRE_TERMINAL = ter.NOMBRE_TERMINAL,
                                                                        NUMERO_JORNADA = hrcon.NUMERO_JORNADA,
                                                                        FECHA_HORA_INICIO = hrcon.FECHA_INICIO,
                                                                        FECHA_CARGA = car.FECHA_CARGA,
                                                                        HORARIO_CUBIERTO = hrcon.HORARIO_CUBIERTO
                                                                    }).Where(x => x.ESTADO == true && x.HORARIO_CUBIERTO == true).ToList();

                    List<DTO_HorarioConductorMostrar> dtoHorario2 = new List<DTO_HorarioConductorMostrar>();

                    if (dtoHorario.Count > 0)
                    {

                        foreach (var item in dtoHorario)
                        {
                            int r = 0;
                            r = context.REGISTRO_HORARIO.Where(x => x.ID_HORARIO == item.ID_HORARIO && x.ESTADO == true).Select(x => x.ID_HORARIO).FirstOrDefault();
                            if (r == 0)
                            {
                                dtoHorario2.Add(item);
                            }
                        }
                    }

                    return dtoHorario2;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public List<DTO_HorarioConductorMostrar> GetHorarioConductorByIdUser(int idUsuario, DateTime fechaIni, DateTime fechaFin)
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    List<DTO_HorarioConductorMostrar> dtoHorario = (from hrcon in context.HORARIO_CONDUCTOR
                                                                    join usr in context.USUARIOS_SISTEMA on hrcon.ID_USUARIO equals usr.ID_USUARIO
                                                                    where (hrcon.FECHA_INICIO >= fechaIni && hrcon.FECHA_INICIO <= fechaFin && usr.ESTADO == true)
                                                                    join tic in context.TIPO_CONTRATO on usr.ID_TIPO_CONTRATO equals tic.ID_TIPO_CONTRATO
                                                                    where hrcon.ID_USUARIO == idUsuario && hrcon.ESTADO == true
                                                                    select new DTO_HorarioConductorMostrar
                                                                    {
                                                                        ID_HORARIO = hrcon.ID_HORARIO,
                                                                        RUT = usr.RUT,
                                                                        NOMBRE = usr.NOMBRE,
                                                                        SEGUNDO_NOMBRE = usr.SEGUNDO_NOMBRE,
                                                                        APELLIDO_PATERNO = usr.APELLIDO_PATERNO,
                                                                        APELLIDO_MATERNO = usr.APELLIDO_MATERNO,
                                                                        ESTADO = usr.ESTADO,
                                                                        ID_TERMINAL = hrcon.ID_TERMINAL_INICIO,
                                                                        NUMERO_JORNADA = hrcon.NUMERO_JORNADA,
                                                                        FECHA_HORA_INICIO = hrcon.FECHA_INICIO,
                                                                        TIPO_CONTRATO = tic.NOMBRE_TIPO_CONTRATO,
                                                                        HORARIO_CUBIERTO = hrcon.HORARIO_CUBIERTO
                                                                    }).Where(x => x.ESTADO == true && x.HORARIO_CUBIERTO == true).ToList();

                    List<DTO_HorarioConductorMostrar> dtoHorario2 = new List<DTO_HorarioConductorMostrar>();

                    if (dtoHorario.Count > 0)
                    {

                        foreach (var item in dtoHorario)
                        {
                            int r = 0;
                            r = context.REGISTRO_HORARIO.Where(x => x.ID_HORARIO == item.ID_HORARIO && x.ESTADO == true).Select(x => x.ID_HORARIO).FirstOrDefault();
                            if (r == 0)
                            {
                                dtoHorario2.Add(item);
                            }
                        }
                    }

                    return dtoHorario2;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public List<DTO_HorarioConductorMostrar> GetHorarioConductorByIdUserNumJor(int idUsuario, DateTime fechaIni, int numeroJornada)
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    List<DTO_HorarioConductorMostrar> dtoHorario = (from hrcon in context.HORARIO_CONDUCTOR
                                                                    join usr in context.USUARIOS_SISTEMA on hrcon.ID_USUARIO equals usr.ID_USUARIO
                                                                    where (hrcon.FECHA_INICIO == fechaIni && usr.ESTADO == true)
                                                                    join tic in context.TIPO_CONTRATO on usr.ID_TIPO_CONTRATO equals tic.ID_TIPO_CONTRATO
                                                                    where hrcon.ID_USUARIO == idUsuario && hrcon.ESTADO == true && hrcon.NUMERO_JORNADA == numeroJornada
                                                                    select new DTO_HorarioConductorMostrar
                                                                    {
                                                                        ID_HORARIO = hrcon.ID_HORARIO,
                                                                        RUT = usr.RUT,
                                                                        NOMBRE = usr.NOMBRE,
                                                                        SEGUNDO_NOMBRE = usr.SEGUNDO_NOMBRE,
                                                                        APELLIDO_PATERNO = usr.APELLIDO_PATERNO,
                                                                        APELLIDO_MATERNO = usr.APELLIDO_MATERNO,
                                                                        ESTADO = usr.ESTADO,
                                                                        ID_TERMINAL = hrcon.ID_TERMINAL_INICIO,
                                                                        NUMERO_JORNADA = hrcon.NUMERO_JORNADA,
                                                                        FECHA_HORA_INICIO = hrcon.FECHA_INICIO,
                                                                        TIPO_CONTRATO = tic.NOMBRE_TIPO_CONTRATO,
                                                                        HORARIO_CUBIERTO = hrcon.HORARIO_CUBIERTO
                                                                    }).Where(x => x.ESTADO == true && x.HORARIO_CUBIERTO == true).ToList();

                    List<DTO_HorarioConductorMostrar> dtoHorario2 = new List<DTO_HorarioConductorMostrar>();

                    if (dtoHorario.Count > 0)
                    {

                        foreach (var item in dtoHorario)
                        {
                            int r = 0;
                            r = context.REGISTRO_HORARIO.Where(x => x.ID_HORARIO == item.ID_HORARIO && x.ESTADO == true).Select(x => x.ID_HORARIO).FirstOrDefault();
                            if (r == 0)
                            {
                                dtoHorario2.Add(item);
                            }
                        }
                    }

                    return dtoHorario2;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public List<DTO_HorarioConductorMostrar> GetHorarioConductorByRutAll(string rut, DateTime fechaIni, DateTime fechaFin)
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    fechaFin = fechaFin.AddDays(1);

                    List<DTO_HorarioConductorMostrar> dtoHorario = (from hrcon in context.HORARIO_CONDUCTOR
                                                                    join usr in context.USUARIOS_SISTEMA on hrcon.ID_USUARIO equals usr.ID_USUARIO
                                                                    join tic in context.TIPO_CONTRATO on usr.ID_TIPO_CONTRATO equals tic.ID_TIPO_CONTRATO
                                                                    where (hrcon.FECHA_INICIO >= fechaIni && hrcon.FECHA_INICIO <= fechaFin
                                                                    && usr.ESTADO == true
                                                                    && hrcon.ESTADO == true
                                                                    && usr.RUT == rut)
                                                                    select new DTO_HorarioConductorMostrar
                                                                    {
                                                                        ID_HORARIO = hrcon.ID_HORARIO,
                                                                        RUT = usr.RUT,
                                                                        NOMBRE = usr.NOMBRE,
                                                                        SEGUNDO_NOMBRE = usr.SEGUNDO_NOMBRE,
                                                                        APELLIDO_PATERNO = usr.APELLIDO_PATERNO,
                                                                        APELLIDO_MATERNO = usr.APELLIDO_MATERNO,
                                                                        ESTADO = usr.ESTADO,
                                                                        ID_TERMINAL = hrcon.ID_TERMINAL_INICIO,
                                                                        NUMERO_JORNADA = hrcon.NUMERO_JORNADA,
                                                                        FECHA_HORA_INICIO = hrcon.FECHA_INICIO,
                                                                        TIPO_CONTRATO = tic.NOMBRE_TIPO_CONTRATO,
                                                                        HORARIO_CUBIERTO = hrcon.HORARIO_CUBIERTO
                                                                    }).Where(x => x.ESTADO == true).ToList();


                    return dtoHorario;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public List<DTO_HorarioConductorMostrar> GetHorarioConductorByRutEstado(string rut, DateTime fechaIni, DateTime fechaFin)
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    fechaFin = fechaFin.AddDays(1);

                    List<DTO_HorarioConductorMostrar> dtoHorario = (from usr in context.USUARIOS_SISTEMA
                                                                    join hrcon in context.HORARIO_CONDUCTOR on usr.ID_USUARIO equals hrcon.ID_USUARIO
                                                                    join tic in context.TIPO_CONTRATO on usr.ID_TIPO_CONTRATO equals tic.ID_TIPO_CONTRATO
                                                                    where (hrcon.FECHA_INICIO >= fechaIni && hrcon.FECHA_INICIO <= fechaFin)
                                                                    select new DTO_HorarioConductorMostrar
                                                                    {
                                                                        ID_HORARIO = hrcon.ID_HORARIO,
                                                                        RUT = usr.RUT,
                                                                        NOMBRE = usr.NOMBRE,
                                                                        SEGUNDO_NOMBRE = usr.SEGUNDO_NOMBRE,
                                                                        APELLIDO_PATERNO = usr.APELLIDO_PATERNO,
                                                                        APELLIDO_MATERNO = usr.APELLIDO_MATERNO,
                                                                        ESTADO = usr.ESTADO,
                                                                        ID_TERMINAL = hrcon.ID_TERMINAL_INICIO,
                                                                        NUMERO_JORNADA = hrcon.NUMERO_JORNADA,
                                                                        FECHA_HORA_INICIO = hrcon.FECHA_INICIO,
                                                                        TIPO_CONTRATO = tic.NOMBRE_TIPO_CONTRATO,
                                                                        HORARIO_CUBIERTO = hrcon.HORARIO_CUBIERTO
                                                                    }).Where(x => x.RUT == rut
                                                                    && x.ESTADO == true).ToList();



                    return dtoHorario;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public List<DTO_HorarioConductorMostrar> GetHorarioConductorById(int idUsuario, DateTime fechaIni, DateTime fechaFin, int idTurno)
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    fechaFin = fechaFin.AddDays(1);

                    List<DTO_HorarioConductorMostrar> dtoHorario = (from usr in context.USUARIOS_SISTEMA
                                                                    join hrcon in context.HORARIO_CONDUCTOR on usr.ID_USUARIO equals hrcon.ID_USUARIO
                                                                    join tic in context.TIPO_CONTRATO on usr.ID_TIPO_CONTRATO equals tic.ID_TIPO_CONTRATO
                                                                    join ter in context.TERMINAL on hrcon.ID_TERMINAL_INICIO equals ter.ID_TERMINAL
                                                                    where (hrcon.FECHA_INICIO >= fechaIni && hrcon.FECHA_INICIO <= fechaFin
                                                                    && usr.ID_USUARIO == idUsuario
                                                                    && hrcon.NUMERO_JORNADA == idTurno
                                                                    && hrcon.ESTADO == true)
                                                                    select new DTO_HorarioConductorMostrar
                                                                    {
                                                                        ID_USUARIO = usr.ID_USUARIO,
                                                                        ID_HORARIO = hrcon.ID_HORARIO,
                                                                        ID_CARGA_HORARIO = hrcon.ID_CARGA_HORARIO,
                                                                        RUT = usr.RUT,
                                                                        NOMBRE = usr.NOMBRE,
                                                                        SEGUNDO_NOMBRE = usr.SEGUNDO_NOMBRE,
                                                                        APELLIDO_PATERNO = usr.APELLIDO_PATERNO,
                                                                        APELLIDO_MATERNO = usr.APELLIDO_MATERNO,
                                                                        ESTADO = usr.ESTADO,
                                                                        ID_TERMINAL = hrcon.ID_TERMINAL_INICIO,
                                                                        NOMBRE_TERMINAL = ter.NOMBRE_TERMINAL,
                                                                        NUMERO_JORNADA = hrcon.NUMERO_JORNADA,
                                                                        FECHA_HORA_INICIO = hrcon.FECHA_INICIO,
                                                                        TIPO_CONTRATO = tic.NOMBRE_TIPO_CONTRATO,
                                                                    }).Where(x => x.ID_USUARIO == idUsuario
                                                                    && x.ESTADO == true).OrderBy(x => x.FECHA_HORA_INICIO).ToList();

                    return dtoHorario;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public DTO_HorarioConductorMostrar GetHorarioConductorByIdUser(int idUsuario, DateTime fechaHora)
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {

                    DTO_HorarioConductorMostrar dtoHorario = (from usr in context.USUARIOS_SISTEMA
                                                              join hrcon in context.HORARIO_CONDUCTOR on usr.ID_USUARIO equals hrcon.ID_USUARIO
                                                              join tic in context.TIPO_CONTRATO on usr.ID_TIPO_CONTRATO equals tic.ID_TIPO_CONTRATO
                                                              join ter in context.TERMINAL on hrcon.ID_TERMINAL_INICIO equals ter.ID_TERMINAL
                                                              where (hrcon.FECHA_INICIO.Year == fechaHora.Year
                                                               && hrcon.FECHA_INICIO.Month == fechaHora.Month
                                                              && hrcon.FECHA_INICIO.Day == fechaHora.Day
                                                              && hrcon.FECHA_INICIO.Hour == fechaHora.Hour
                                                              && hrcon.FECHA_INICIO.Minute == fechaHora.Minute
                                                              && hrcon.FECHA_INICIO.Second == fechaHora.Second
                                                              && usr.ID_USUARIO == idUsuario
                                                              && hrcon.ESTADO == true)
                                                              select new DTO_HorarioConductorMostrar
                                                              {
                                                                  ID_USUARIO = usr.ID_USUARIO,
                                                                  ID_HORARIO = hrcon.ID_HORARIO,
                                                                  ID_CARGA_HORARIO = hrcon.ID_CARGA_HORARIO,
                                                                  RUT = usr.RUT,
                                                                  NOMBRE = usr.NOMBRE,
                                                                  SEGUNDO_NOMBRE = usr.SEGUNDO_NOMBRE,
                                                                  APELLIDO_PATERNO = usr.APELLIDO_PATERNO,
                                                                  APELLIDO_MATERNO = usr.APELLIDO_MATERNO,
                                                                  ESTADO = usr.ESTADO,
                                                                  ID_TERMINAL = hrcon.ID_TERMINAL_INICIO,
                                                                  NOMBRE_TERMINAL = ter.NOMBRE_TERMINAL,
                                                                  NUMERO_JORNADA = hrcon.NUMERO_JORNADA,
                                                                  FECHA_HORA_INICIO = hrcon.FECHA_INICIO,
                                                                  TIPO_CONTRATO = tic.NOMBRE_TIPO_CONTRATO,
                                                              }).Where(x => x.ID_USUARIO == idUsuario
                                                              && x.ESTADO == true).FirstOrDefault();

                    return dtoHorario;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public DTO_HorarioConductorMostrar GetHorarioConductorByIdHorario(int idHorario)
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    // Obtengo datos del usuario, perfiles y permisos
                    DTO_HorarioConductorMostrar dtoHorario = (from usr in context.USUARIOS_SISTEMA
                                                              join hrcon in context.HORARIO_CONDUCTOR on usr.ID_USUARIO equals hrcon.ID_USUARIO
                                                              join tic in context.TIPO_CONTRATO on usr.ID_TIPO_CONTRATO equals tic.ID_TIPO_CONTRATO
                                                              join ter in context.TERMINAL on hrcon.ID_TERMINAL_INICIO equals ter.ID_TERMINAL
                                                              where (hrcon.ID_HORARIO == idHorario)
                                                              select new DTO_HorarioConductorMostrar
                                                              {
                                                                  ID_USUARIO = usr.ID_USUARIO,
                                                                  RUT = usr.RUT,
                                                                  NOMBRE = usr.NOMBRE,
                                                                  SEGUNDO_NOMBRE = usr.SEGUNDO_NOMBRE,
                                                                  APELLIDO_PATERNO = usr.APELLIDO_PATERNO,
                                                                  APELLIDO_MATERNO = usr.APELLIDO_MATERNO,
                                                                  ESTADO = usr.ESTADO,
                                                                  ID_TERMINAL = hrcon.ID_TERMINAL_INICIO,
                                                                  NOMBRE_TERMINAL = ter.NOMBRE_TERMINAL,
                                                                  NUMERO_JORNADA = hrcon.NUMERO_JORNADA,
                                                                  FECHA_HORA_INICIO = hrcon.FECHA_INICIO,
                                                                  TIPO_CONTRATO = tic.NOMBRE_TIPO_CONTRATO
                                                              }).Where(x => x.ESTADO == true).FirstOrDefault();

                    return dtoHorario;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public int GetID_CARGA_HORARIO_ByIdHorario(int id_horario)
        {
            using (SolusegEntities context = new SolusegEntities())
            {

                return (from hc in context.HORARIO_CONDUCTOR where hc.ID_HORARIO == id_horario select hc.ID_HORARIO).FirstOrDefault();
            }
        }

        public List<DTO_TipoSolicitud> GetTipoSolicitudAll()
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    List<DTO_TipoSolicitud> solicitud = (from tsc in context.TIPO_SOLICITUD_CAMBIO
                                                         select new DTO_TipoSolicitud
                                                         {
                                                             ID_TIPO_SOLICITUD = tsc.ID_TIPO_SOLICITUD,
                                                             NOMBRE_SOLICITUD = tsc.NOMBRE_SOLICITUD,
                                                             ESTADO = tsc.ESTADO
                                                         }
                                                         ).ToList();

                    return solicitud;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public string SetGuardarHorarioConductor(List<DTO_CargarHorarioConductor> list, int idUsuario, string nombreCarga, DateTime fechaCarga, string descripcion)
        {
            try
            {
                string respuesta = "";
                int res = 0;
                using (SolusegEntities context = new SolusegEntities())
                {
                    using (var contextTransaction = context.Database.BeginTransaction())
                    {
                        CARGA_HORARIO newItemCarga = new CARGA_HORARIO()
                        {
                            ID_USUARIO_CARGA = idUsuario,
                            NOMBRE_CARGA = nombreCarga,
                            COMENTARIO_CARGA = descripcion,
                            FECHA_CARGA = fechaCarga,
                            ESTADO = true
                        };

                        context.CARGA_HORARIO.Add(newItemCarga);
                        var res2 = context.SaveChanges();

                        int maxIdCarga = context.CARGA_HORARIO.Max(x => x.ID_CARGA_HORARIO);

                        foreach (var item in list)
                        {
                            HORARIO_CONDUCTOR newItemHorario = new HORARIO_CONDUCTOR()
                            {
                                ID_CARGA_HORARIO = maxIdCarga,
                                ID_USUARIO = item.ID_CONDUCTOR,
                                ID_TERMINAL_INICIO = item.TERMINAL_INICIO,
                                FECHA_INICIO = item.FECHA_HORA_INICIO,
                                HORARIO_CUBIERTO = false,
                                NUMERO_JORNADA = (short)item.NUMERO_JORNADA,
                                ID_BUS_INICIO = item.BUS_INICIO,
                                ESTADO = true
                            };
                            context.HORARIO_CONDUCTOR.Add(newItemHorario);
                        }

                        res = context.SaveChanges();
                        contextTransaction.Commit();
                    }
                    int filasTotales = list.Count;


                    if (res == filasTotales)
                    {
                        respuesta = "<b>Se ha guardado con éxito los horarios</b>, el total de filas almacenadas es " + filasTotales;
                        return respuesta;
                    }
                    else
                    {
                        respuesta = "Ha ocurrido un error al guardar";
                        return respuesta;
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                return "Tenemos una incidencia al guardar los datos, intentelo nuevamente. !El equipo de Conductor En Red ya está al tanto!";
                throw;
            }


        }

        public string SetEditarHorarioConductor(List<DTO_HorarioConductorMostrar> list)
        {
            try
            {
                string respuesta = "";
                int res = 0;
                using (SolusegEntities context = new SolusegEntities())
                {
                    using (var contextTransaction = context.Database.BeginTransaction())
                    {
                        HORARIO_CONDUCTOR horario = new HORARIO_CONDUCTOR();

                        foreach (var item in list)
                        {
                            horario = context.HORARIO_CONDUCTOR.Where(x => x.ID_HORARIO == item.ID_HORARIO && x.ESTADO == true).FirstOrDefault();

                            horario.ID_TERMINAL_INICIO = item.ID_TERMINAL;
                            horario.FECHA_INICIO = DateTime.Parse(horario.FECHA_INICIO.ToString().Substring(0, 10) + " " + item.HORA_INICIO);
                            horario.HORARIO_CUBIERTO = false;
                            horario.ESTADO = true;
                        }

                        res = context.SaveChanges();
                        contextTransaction.Commit();
                    }
                    int filasTotales = list.Count;


                    if (res == filasTotales)
                    {
                        respuesta = "<b>Se ha guardado con éxito los horarios</b>, el total de filas almacenadas es " + filasTotales;
                        return respuesta;
                    }
                    else
                    {
                        respuesta = "Ha ocurrido un error al guardar";
                        return respuesta;
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                return "Tenemos una incidencia al guardar los datos, intentelo nuevamente. !El equipo de Conductor En Red ya está al tanto!";
                throw;
            }


        }

        public int SetIngresaSolicitud(DTO_SolicitudCambioHorario list)
        {
            try
            {
                int res = 0;
                using (SolusegEntities context = new SolusegEntities())
                {
                    using (var contextTransaction = context.Database.BeginTransaction())
                    {
                        SOLICITUD_CAMBIO_HORARIO solicitud = new SOLICITUD_CAMBIO_HORARIO();
                        solicitud.ID_SOLICITUD_CAMBIO = list.ID_SOLICITUD_CAMBIO;
                        solicitud.ID_HORARIO = list.ID_HORARIO_CAMBIAR;
                        solicitud.ID_TIPO_SOLICITUD = list.ID_TIPO_SOLICITUD;
                        solicitud.ID_ESTADO_SOLICITUD = list.ID_ESTADO_SOLICITUD;
                        solicitud.ID_USUARIO_SOLICITA = list.ID_USUARIO_SOLICITA;
                        solicitud.ID_USUARIO_APRUEBA = list.ID_USUARIO_APRUEBA;
                        solicitud.FECHA_REGISTRO_SOLICITUD = list.FECHA_REGISTRO_SOLICITUD;
                        solicitud.FECHA_APROBACION = list.FECHA_APROBACION;
                        solicitud.COMENTARIO_MOTIVO = list.COMENTARIO_MOTIVO;
                        solicitud.COMENTARIO_ADICIONAL = list.COMENTARIO_ADICIONAL;
                        solicitud.ESTADO = true;

                        context.SOLICITUD_CAMBIO_HORARIO.Add(solicitud);
                        res = context.SaveChanges();
                        contextTransaction.Commit();
                    }

                    return res;
                }

            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }


        }

        public List<DTO_HorarioConductorMostrar> GetRegistroVueltasByAll(DateTime desde, DateTime hasta, int idterminal, string run)
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    DateTime fechHoy = DateTime.Now;
                    int idUsuario = 0;
                    
                    if (run != "")
                        idUsuario = context.USUARIOS_SISTEMA.Where(x => x.RUT == run).FirstOrDefault().ID_USUARIO;

                    List<DTO_HorarioConductorMostrar> dtoRegistro = (from hrc in context.HORARIO_CONDUCTOR
                                                                     join ter in context.TERMINAL on hrc.ID_TERMINAL_INICIO equals ter.ID_TERMINAL
                                                                     join usr in context.USUARIOS_SISTEMA on hrc.ID_USUARIO equals usr.ID_USUARIO
                                                                     join car in context.CARGA_HORARIO on hrc.ID_CARGA_HORARIO equals car.ID_CARGA_HORARIO
                                                                     where hrc.ESTADO == true
                                                                        && ((hrc.ID_USUARIO > 0 && hrc.ID_USUARIO == idUsuario) || idUsuario == 0)
                                                                            && ((hrc.ID_TERMINAL_INICIO > 0 && hrc.ID_TERMINAL_INICIO == idterminal) || idterminal == 0)
                                                                                &&
                                                                                (hrc.FECHA_INICIO >= desde && hrc.FECHA_INICIO <= hasta)
                                                                     select new DTO_HorarioConductorMostrar
                                                                     {
                                                                         ID_HORARIO = hrc.ID_HORARIO,
                                                                         RUT = usr.RUT,
                                                                         NOMBRE = usr.NOMBRE,
                                                                         SEGUNDO_NOMBRE = usr.SEGUNDO_NOMBRE,
                                                                         APELLIDO_PATERNO = usr.APELLIDO_PATERNO,
                                                                         APELLIDO_MATERNO = usr.APELLIDO_MATERNO,
                                                                         ESTADO = usr.ESTADO,
                                                                         NOMBRE_TERMINAL = ter.NOMBRE_TERMINAL,
                                                                         NUMERO_JORNADA = hrc.NUMERO_JORNADA,
                                                                         FECHA_HORA_INICIO = hrc.FECHA_INICIO,
                                                                         FECHA_CARGA = car.FECHA_CARGA,
                                                                         HORARIO_CUBIERTO = hrc.HORARIO_CUBIERTO
                                                                     }).Where(x => x.ESTADO == true).ToList();


                    return dtoRegistro;

                }

            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

    }
}
