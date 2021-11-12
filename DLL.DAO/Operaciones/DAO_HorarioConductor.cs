using DLL.DAO.Operaciones.Interfaces;
using DLL.DATA.SeguridadSoluinfo;
using DLL.DTO.CargaHorario;
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

                    // Obtengo datos del usuario, perfiles y permisos
                    List<DTO_HorarioConductorMostrar> dtoHorario = (from usr in context.USUARIOS_SISTEMA
                                                                    join hrcon in context.HORARIO_CONDUCTOR on usr.ID_USUARIO equals hrcon.ID_USUARIO
                                                                    join tic in context.TIPO_CONTRATO on usr.ID_TIPO_CONTRATO equals tic.ID_TIPO_CONTRATO                                                                    
                                                                    where (hrcon.FECHA_INICIO >= fechaIni && hrcon.FECHA_INICIO <= fechaFin)
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
                                                                        NUMERO_JORNADA = hrcon.NUMERO_JORNADA,
                                                                        FECHA_HORA_INICIO = hrcon.FECHA_INICIO,
                                                                        TIPO_CONTRATO = tic.NOMBRE_TIPO_CONTRATO
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

        public List<DTO_HorarioConductorMostrar> GetHorarioConductorById(int idUsuario, DateTime fechaIni, DateTime fechaFin,int idTurno)
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    // Obtengo datos del usuario, perfiles y permisos
                    List<DTO_HorarioConductorMostrar> dtoHorario = (from usr in context.USUARIOS_SISTEMA
                                                                    join hrcon in context.HORARIO_CONDUCTOR on usr.ID_USUARIO equals hrcon.ID_USUARIO
                                                                    join tic in context.TIPO_CONTRATO on usr.ID_TIPO_CONTRATO equals tic.ID_TIPO_CONTRATO
                                                                    join ter in context.TERMINAL on hrcon.ID_TERMINAL_INICIO equals ter.ID_TERMINAL
                                                                    where (hrcon.FECHA_INICIO >= fechaIni && hrcon.FECHA_INICIO <= fechaFin && usr.ID_USUARIO == idUsuario && hrcon.NUMERO_JORNADA == idTurno)
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
                                                                        TIPO_CONTRATO = tic.NOMBRE_TIPO_CONTRATO
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
                                                                    }).Where(x=> x.ESTADO == true).FirstOrDefault();

                    return dtoHorario;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public string SetGuardarHorarioConductor(List<DTO_CargarHorarioConductor> list, string nombreCarga, DateTime fechaCarga, string descripcion)
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
                            ID_USUARIO_CARGA = 1,
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
                                ID_USUARIO = 1,
                                ID_TERMINAL_INICIO = item.TERMINAL_INICIO,
                                FECHA_INICIO = item.FECHA_HORA_INICIO,
                                HORARIO_CUBIERTO = false,
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

        public int GetID_CARGA_HORARIO_ByIdHorario(int id_horario)
        {
            using (SolusegEntities context = new SolusegEntities())
            {

                return (from hc in context.HORARIO_CONDUCTOR where hc.ID_HORARIO == id_horario select hc.ID_HORARIO).FirstOrDefault();
            }
        }

        public string SetEditarHorarioConductor(List<DTO_CargarHorarioConductor> list)
        {
            try
            {
                string respuesta = "";
                int res = 0;
                using (SolusegEntities context = new SolusegEntities())
                {
                    using (var contextTransaction = context.Database.BeginTransaction())
                    {

                        foreach (var item in list)
                        {
                            HORARIO_CONDUCTOR newItemHorario = new HORARIO_CONDUCTOR()
                            {
                                //ID_CARGA_HORARIO = GetID_CARGA_HORARIO_ByIdHorario(id),
                                ID_USUARIO = 1,
                                ID_TERMINAL_INICIO = item.TERMINAL_INICIO,
                                FECHA_INICIO = item.FECHA_HORA_INICIO,
                                HORARIO_CUBIERTO = false,
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
    }
}
