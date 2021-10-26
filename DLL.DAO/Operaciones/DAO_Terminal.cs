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
                                        where ter.ID_EMPRESA == idEmpresa && ter.NOMBRE_TERMINAL == nombreTerminal
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
    }
}
