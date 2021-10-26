using DLL.DAO.Operaciones.Interfaces;
using DLL.DATA.SeguridadSoluinfo;
using DLL.DTO.Mantenedor;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DAO.Operaciones
{
    public class DAO_Bus : I_DAO_Bus
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DAO_Bus()
        {
            XmlConfigurator.Configure();
        }

        public List<DTO_Bus> GetBusByAllActiveForTable()
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    List<DTO_Bus> dtoBus = (from bus in context.BUS
                                            join ter in context.TERMINAL on bus.ID_TERMINAL equals ter.ID_TERMINAL
                                            where bus.ESTADO == true
                                            select new DTO_Bus
                                            {
                                                ID_BUS = bus.ID_BUS,
                                                ID_TERMINAL = bus.ID_TERMINAL,
                                                NOMBRE_TERMINAL = ter.NOMBRE_TERMINAL,
                                                ID_INTERNO_BUS = bus.ID_INTERNO_BUS,
                                                PPU = bus.PPU,
                                                ESTADO = bus.ESTADO
                                            }).ToList();

                    return dtoBus;

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
