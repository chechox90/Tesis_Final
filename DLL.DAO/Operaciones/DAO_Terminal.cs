using DLL.DAO.Operaciones.Interfaces;
using DLL.DATA.SeguridadSoluinfo;
using DLL.DTO.Terminales;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DLL.DAO.Operaciones
{
    public class DAO_Terminal : I_DAO_Terminal
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DAO_Terminal()
        {
            XmlConfigurator.Configure();
        }

        public int GetTerminalByNombre(string nombreTerminal, int idEmpresa)
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    int dto_Terminal = (from ter in context.TERMINAL
                                        where ter.ID_EMPRESA == idEmpresa && ter.NOMBRE_TERMINAL == nombreTerminal && ter.ESTADO == true
                                        select new DTO_Terminal
                                        {
                                            ID_TERMINAL = ter.ID_TERMINAL,
                                            ID_EMPRESA = ter.ID_EMPRESA,
                                            NOMBRE_TERMINAL = ter.NOMBRE_TERMINAL
                                        }).FirstOrDefault().ID_TERMINAL;


                    return dto_Terminal;

                }


            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public int GetTerminalByNombreIdEmpresa(string nombreTerminal, int idEmpresa)
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    int respuesta = 0;
                    var terminal = (from ter in context.TERMINAL
                                        where ter.ID_EMPRESA == idEmpresa && ter.NOMBRE_TERMINAL == nombreTerminal && ter.ESTADO == true
                                        select new DTO_Terminal
                                        {
                                            ID_TERMINAL = ter.ID_TERMINAL,
                                            ID_EMPRESA = ter.ID_EMPRESA,
                                            NOMBRE_TERMINAL = ter.NOMBRE_TERMINAL
                                        }).FirstOrDefault();
                    if (terminal != null)
                        respuesta = terminal.ID_TERMINAL;

                    return respuesta;

                }


            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public List<DTO_Terminal> GetTerminalByAllActive()
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    List<DTO_Terminal> dto_Terminal = (from ter in context.TERMINAL
                                                       where ter.ESTADO == true
                                                       select new DTO_Terminal
                                                       {
                                                           ID_TERMINAL = ter.ID_TERMINAL,
                                                           NOMBRE_TERMINAL = ter.NOMBRE_TERMINAL
                                                       }).ToList();

                    return dto_Terminal;

                }

            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public DTO_Terminal GetNombreTerminal(int idProgramacion)
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    int idTerminal = context.HORARIO_CONDUCTOR.Where(x => x.ID_HORARIO == idProgramacion && x.ESTADO == true).Select(x => x.ID_TERMINAL_INICIO).FirstOrDefault();
                    DTO_Terminal dto_Terminal = (from ter in context.TERMINAL
                                                       where ter.ESTADO == true
                                                       select new DTO_Terminal
                                                       {
                                                           ID_TERMINAL = ter.ID_TERMINAL,
                                                           NOMBRE_TERMINAL = ter.NOMBRE_TERMINAL
                                                       }).Where(x => x.ID_TERMINAL == idTerminal).FirstOrDefault();

                    return dto_Terminal;

                }

            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }
        public List<DTO_Terminal> GetTerminalByAllActiveForTable()
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    List<DTO_Terminal> dto_Terminal = (from ter in context.TERMINAL
                                                       join em in context.EMPRESA on ter.ID_EMPRESA equals em.ID_EMPRESA
                                                       where ter.ESTADO == true
                                                       select new DTO_Terminal
                                                       {
                                                           ID_TERMINAL = ter.ID_TERMINAL,
                                                           NOMBRE_EMPRESA = em.NOMBRE_EMPRESA,
                                                           NOMBRE_TERMINAL = ter.NOMBRE_TERMINAL,
                                                           DIRECCION = ter.DIRECCION,
                                                           NUM_DIRECCION = ter.NUM_DIRECCION
                                                       }).ToList();

                    return dto_Terminal;

                }

            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public int SetNuevoTerminal(string nombreTer, string direccion, int numDire)
        {
            try
            {
                int respuesta = 0;
                using (SolusegEntities context = new SolusegEntities())
                {
                    using (var contextTransaction = context.Database.BeginTransaction())
                    {
                        TERMINAL newTer = new TERMINAL()
                        {
                            ID_EMPRESA = 1,
                            NOMBRE_TERMINAL = nombreTer,
                            DIRECCION = direccion,
                            NUM_DIRECCION = numDire,
                            ESTADO = true

                        };

                        context.TERMINAL.Add(newTer);
                        respuesta = context.SaveChanges();
                        contextTransaction.Commit();
                    }
                }

                return respuesta;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public int SetEliminarTerminal(int idTerminal)
        {
            try
            {
                int respuesta = 0;

                using (SolusegEntities context = new SolusegEntities())
                {
                    using (var contextTransaction = context.Database.BeginTransaction())
                    {
                        TERMINAL Old = context.TERMINAL.Where(x => x.ESTADO == true && x.ID_TERMINAL == idTerminal).FirstOrDefault();
                        Old.ESTADO = false;
                        respuesta = context.SaveChanges();
                        contextTransaction.Commit();
                    }
                }

                return respuesta;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public int SetEditarTerminal(DTO_Terminal TERMINAL)
        {
            try
            {
                int respuesta = 0;

                using (SolusegEntities context = new SolusegEntities())
                {
                    using (var contextTransaction = context.Database.BeginTransaction())
                    {
                        TERMINAL OldTer = context.TERMINAL.Where(x => x.ESTADO == true && x.ID_TERMINAL == TERMINAL.ID_TERMINAL).FirstOrDefault();

                        OldTer.ID_TERMINAL = TERMINAL.ID_TERMINAL;
                        OldTer.NOMBRE_TERMINAL = TERMINAL.NOMBRE_TERMINAL;
                        OldTer.ID_EMPRESA = TERMINAL.ID_EMPRESA;
                        OldTer.DIRECCION = TERMINAL.DIRECCION;
                        OldTer.NUM_DIRECCION = TERMINAL.NUM_DIRECCION;
                        OldTer.ESTADO = true;

                        respuesta = context.SaveChanges();
                        contextTransaction.Commit();
                    }
                }

                return respuesta;

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
