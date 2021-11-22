using DLL.DAO.Operaciones.Interfaces;
using DLL.DATA.SeguridadSoluinfo;
using DLL.DTO.Mantenedor;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DLL.DAO.Operaciones
{
    public class DAO_RegistroHorario : I_DAO_RegistroHorario
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DAO_RegistroHorario()
        {
            XmlConfigurator.Configure();
        }

        public List<DTO_RegistroVueltas> GetRegistroVueltasByAll(int idRegistroHorario)
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    List<DTO_RegistroVueltas> dtoRegistro = (from reg in context.REGISTRO_VUELTAS
                                                             where reg.ID_REGISTRO_HORARIO == idRegistroHorario && reg.ESTADO == true
                                                             select new DTO_RegistroVueltas
                                                             {
                                                                 ID_REGISTRO_VUELTAS = reg.ID_REGISTRO_VUELTAS,
                                                                 ID_BUS_INICIO = reg.ID_BUS_INICIO,
                                                                 FECHA_HORA_INICIO = reg.FECHA_HORA_INICIO,
                                                                 ID_TERMINAL_INICIO = reg.ID_TERMINAL_INICIO,
                                                                 ID_SENTIDO_INICIO = reg.ID_SENTIDO_INICIO,
                                                                 ID_SERVICIO_FIN = reg.ID_SERVICIO_INICIO,
                                                                 
                                                                 ID_SENTIDO_FIN = reg.ID_SENTIDO_FIN,
                                                                 ID_TERMINAL_FIN = reg.ID_TERMINAL_FIN,
                                                                 FECHA_HORA_FIN = reg.FECHA_HORA_FIN,
                                                                // ID_BUS_FIN = reg.ID_BUS_FIN,
                                                                 ESTADO = reg.ESTADO,

                                                             }).ToList();

                    return dtoRegistro;

                }

            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public DTO_RegistroVueltas GetRegistroVueltasByAllId(int idVuelta)
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    DTO_RegistroVueltas dtoRegistro = (from reg in context.REGISTRO_VUELTAS
                                                       join ter in context.TERMINAL on reg.ID_TERMINAL_INICIO equals ter.ID_TERMINAL
                                                       join ser in context.SERVICIO on reg.ID_SERVICIO_INICIO equals ser.ID_SERVICIO
                                                       join sen in context.SENTIDO on reg.ID_SENTIDO_INICIO equals sen.ID_SENTIDO
                                                       join b in context.BUS on reg.ID_BUS_INICIO equals b.ID_BUS
                                                       where reg.ESTADO == true
                                                       select new DTO_RegistroVueltas
                                                       {
                                                           ID_REGISTRO_VUELTAS = reg.ID_REGISTRO_VUELTAS,
                                                           NOMBRE_SERVICIO_INICIO = ser.NOMBRE_SERVICIO,
                                                           NUMERO_BUS_INICIO = b.ID_INTERNO_BUS,
                                                           SEN_INI_CORTO = sen.NOMBRE_CORTO_SENTIDO,
                                                           NOMBRE_TERMINAL_INICIO = ter.NOMBRE_TERMINAL,
                                                           FECHA_HORA_INICIO = reg.FECHA_HORA_INICIO,
                                                           NOMBRE_SERVICIO_FIN = context.SERVICIO.Where(s => s.ID_SERVICIO == reg.ID_SERVICIO_FIN).Select(s => s.NOMBRE_SERVICIO).FirstOrDefault(),
                                                           NOMBRE_TERMINAL_FIN = context.TERMINAL.Where(x => x.ID_TERMINAL == reg.ID_TERMINAL_FIN).Select(x => x.NOMBRE_TERMINAL).FirstOrDefault(),
                                                           NUMERO_BUS_FIN = context.BUS.Where(b => b.ID_BUS == reg.ID_BUS_BUS).Select(b => b.ID_INTERNO_BUS).FirstOrDefault(),
                                                           SEN_FIN_CORTO = context.SENTIDO.Where(se => se.ID_SENTIDO == reg.ID_SENTIDO_FIN).Select(se => se.NOMBRE_CORTO_SENTIDO).FirstOrDefault(),
                                                           FECHA_HORA_FIN = reg.FECHA_HORA_FIN,
                                                           ESTADO = reg.ESTADO
                                                       }).Where(x => x.ID_REGISTRO_VUELTAS == idVuelta && x.ESTADO == true).FirstOrDefault();

                    return dtoRegistro;

                }

            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }


        public DTO_RegistroHorario GetRegistroHorarioByAll(int idUsuario)
        {
            try
            {
                DateTime fechaHoy = DateTime.Now;
                using (SolusegEntities context = new SolusegEntities())
                {
                    DTO_RegistroHorario dtoRegistro = (from reg in context.REGISTRO_HORARIO
                                                       where reg.ESTADO == true && reg.ID_USUARIO == idUsuario
                                                       && reg.FECHA_HORA_INICIO.Year == fechaHoy.Year
                                                       && reg.FECHA_HORA_INICIO.Month == fechaHoy.Month
                                                       && reg.FECHA_HORA_INICIO.Day == fechaHoy.Day
                                                       select new DTO_RegistroHorario
                                                       {
                                                           ID_REGISTRO_HORARIO = reg.ID_REGISTRO_HORARIO,
                                                           ID_USUARIO = reg.ID_USUARIO,
                                                           ID_TERMINAL_INICIO = reg.ID_TERMINAL_INICIO,
                                                           FECHA_HORA_INICIO = reg.FECHA_HORA_INICIO,
                                                           ID_TERMINAL_FIN = reg.ID_TERMINAL_FIN,
                                                           FECHA_HORA_FIN = reg.FECHA_HORA_FIN,
                                                           ESTADO = reg.ESTADO
                                                       }).FirstOrDefault();

                    return dtoRegistro;

                }

            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public int SetIngresaHorario(DTO_RegistroHorario list)
        {
            try
            {
                int respuesta = 0;
                using (SolusegEntities context = new SolusegEntities())
                {
                    using (var contextTransaction = context.Database.BeginTransaction())
                    {
                        REGISTRO_HORARIO newReg = new REGISTRO_HORARIO()
                        {
                            ID_REGISTRO_HORARIO = list.ID_REGISTRO_HORARIO,
                            ID_USUARIO = list.ID_USUARIO,
                            ID_TERMINAL_INICIO = list.ID_TERMINAL_INICIO,
                            FECHA_HORA_INICIO = list.FECHA_HORA_INICIO,
                            ID_TERMINAL_FIN = list.ID_TERMINAL_FIN,
                            FECHA_HORA_FIN = list.FECHA_HORA_FIN,
                            ESTADO = true

                        };

                        context.REGISTRO_HORARIO.Add(newReg);
                        respuesta = context.SaveChanges();
                        contextTransaction.Commit();
                    }

                    return respuesta;

                }

            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public int SetIngresaVuelta(DTO_RegistroVueltas list)
        {
            try
            {
                int respuesta = 0;
                using (SolusegEntities context = new SolusegEntities())
                {
                    using (var contextTransaction = context.Database.BeginTransaction())
                    {
                        REGISTRO_VUELTAS newReg = new REGISTRO_VUELTAS()
                        {
                            ID_REGISTRO_VUELTAS = 1,
                            ID_REGISTRO_HORARIO = list.ID_REGISTRO_HORARIO,
                            ID_BUS_INICIO = list.ID_BUS_INICIO,
                            ID_SERVICIO_INICIO = list.ID_SERVICIO_INICIO,
                            ID_SENTIDO_INICIO = list.ID_SENTIDO_INICIO,
                            ID_TERMINAL_INICIO = list.ID_TERMINAL_INICIO,
                            FECHA_HORA_INICIO = list.FECHA_HORA_INICIO,
                            ID_SENTIDO_FIN = null,
                            ID_TERMINAL_FIN = null,
                            FECHA_HORA_FIN = null,
                            ESTADO = true

                        };

                        context.REGISTRO_VUELTAS.Add(newReg);
                        respuesta = context.SaveChanges();
                        contextTransaction.Commit();
                    }

                    return respuesta;

                }

            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public int SetFinalizaVuelta(DTO_RegistroVueltas list)
        {
            try
            {
                int respuesta = 0;
                using (SolusegEntities context = new SolusegEntities())
                {
                    using (var contextTransaction = context.Database.BeginTransaction())
                    {
                        REGISTRO_VUELTAS reg = new REGISTRO_VUELTAS();
                        reg = context.REGISTRO_VUELTAS.Where(x => x.ID_REGISTRO_VUELTAS == list.ID_REGISTRO_VUELTAS).FirstOrDefault();
                        reg.ID_SERVICIO_FIN = list.ID_SERVICIO_FIN;
                        reg.ID_SENTIDO_FIN = list.ID_SENTIDO_FIN;
                        reg.ID_TERMINAL_FIN = list.ID_TERMINAL_FIN;
                        reg.FECHA_HORA_FIN = list.FECHA_HORA_FIN;
                        reg.ID_BUS_BUS = list.ID_BUS_FIN;

                        respuesta = context.SaveChanges();
                        contextTransaction.Commit();
                    }

                    return respuesta;

                }

            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public int SetEliminarVuelta(int idVuelta)
        {
            try
            {
                int respuesta = 0;
                using (SolusegEntities context = new SolusegEntities())
                {
                    using (var contextTransaction = context.Database.BeginTransaction())
                    {
                        REGISTRO_VUELTAS reg = new REGISTRO_VUELTAS();
                        reg = context.REGISTRO_VUELTAS.Where(x => x.ID_REGISTRO_VUELTAS == idVuelta).FirstOrDefault();
                        reg.ESTADO = false;

                        respuesta = context.SaveChanges();
                        contextTransaction.Commit();
                    }

                    return respuesta;

                }

            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public int SetIngresaFinHorario(DTO_RegistroHorario list)
        {
            try
            {
                int respuesta = 0;
                using (SolusegEntities context = new SolusegEntities())
                {
                    using (var contextTransaction = context.Database.BeginTransaction())
                    {
                        REGISTRO_HORARIO reg = new REGISTRO_HORARIO();
                        reg = context.REGISTRO_HORARIO.Where(x => x.ID_REGISTRO_HORARIO == list.ID_REGISTRO_HORARIO).FirstOrDefault();
                        reg.ID_USUARIO = 1;
                        reg.ID_TERMINAL_INICIO = reg.ID_TERMINAL_INICIO;
                        reg.FECHA_HORA_INICIO = reg.FECHA_HORA_INICIO;
                        reg.ID_TERMINAL_FIN = list.ID_TERMINAL_FIN;
                        reg.FECHA_HORA_FIN = list.FECHA_HORA_FIN;
                        reg.ESTADO = list.ESTADO;

                        respuesta = context.SaveChanges();
                        contextTransaction.Commit();
                    }

                    return respuesta;

                }

            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public int SetIditaVuelta(DTO_RegistroVueltas list)
        {
            try
            {
                int respuesta = 0;
                using (SolusegEntities context = new SolusegEntities())
                {
                    using (var contextTransaction = context.Database.BeginTransaction())
                    {
                        REGISTRO_VUELTAS reg = new REGISTRO_VUELTAS();
                        reg = context.REGISTRO_VUELTAS.Where(x => x.ID_REGISTRO_VUELTAS == list.ID_REGISTRO_VUELTAS).FirstOrDefault();

                        reg.ID_REGISTRO_VUELTAS = 1;
                        reg.ID_REGISTRO_HORARIO = list.ID_REGISTRO_HORARIO;
                        reg.ID_BUS_INICIO = list.ID_BUS_INICIO;
                        reg.ID_SERVICIO_INICIO = list.ID_SERVICIO_INICIO;
                        reg.ID_SENTIDO_INICIO = list.ID_SENTIDO_INICIO;
                        reg.ID_TERMINAL_INICIO = list.ID_TERMINAL_INICIO;
                        reg.FECHA_HORA_INICIO = list.FECHA_HORA_INICIO;
                        reg.ID_SERVICIO_FIN = list.ID_SERVICIO_FIN;
                        reg.ID_SENTIDO_FIN = list.ID_SENTIDO_FIN;
                        reg.ID_TERMINAL_FIN = list.ID_TERMINAL_FIN;
                        reg.FECHA_HORA_FIN = list.FECHA_HORA_FIN;
                        reg.ID_BUS_BUS = list.ID_BUS_FIN;
                        reg.ESTADO = true;

                        respuesta = context.SaveChanges();
                        contextTransaction.Commit();
                    }
                }

                return respuesta;


            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

    }
}