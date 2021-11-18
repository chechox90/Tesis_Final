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
                                                             join ter in context.TERMINAL on reg.ID_TERMINAL_INICIO equals ter.ID_TERMINAL
                                                             where reg.ID_REGISTRO_HORARIO == idRegistroHorario
                                                             select new DTO_RegistroVueltas
                                                             {
                                                                 ID_REGISTRO_VUELTAS = reg.ID_REGISTRO_VUELTAS,
                                                                 ID_REGISTRO_HORARIO = reg.ID_REGISTRO_HORARIO,
                                                                 ID_TERMINAL_INICIO = reg.ID_TERMINAL_INICIO,
                                                                 NOMBRE_TERMINAL_INICIO = ter.NOMBRE_TERMINAL,
                                                                 FECHA_HORA_INICIO = reg.FECHA_HORA_INICIO,
                                                                 ID_TERMINAL_FIN = reg.ID_TERMINAL_FIN,
                                                                 NOMBRE_TERMINAL_FIN = ter.NOMBRE_TERMINAL,
                                                                 FECHA_HORA_FIN = reg.FECHA_HORA_FIN
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
                            ID_REGISTRO_HORARIO = list.ID_REGISTRO_HORARIO,
                            ID_TERMINAL_INICIO = list.ID_TERMINAL_INICIO,
                            FECHA_HORA_INICIO = list.FECHA_HORA_INICIO,
                            ID_TERMINAL_FIN = list.ID_TERMINAL_FIN,
                            FECHA_HORA_FIN = list.FECHA_HORA_FIN

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

    }
}
