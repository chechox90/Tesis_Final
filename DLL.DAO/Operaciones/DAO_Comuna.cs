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
    public class DAO_Comuna : I_DAO_Comuna
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DAO_Comuna()
        {
            XmlConfigurator.Configure();
        }

        public List<DTO_Comuna> GetComunaByAllActiveForTable()
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    List<DTO_Comuna> dtoComuna = (from co in context.COMUNA
                                                  where co.ESTADO == true
                                                  select new DTO_Comuna
                                                  {
                                                      ID_COMUNA = co.ID_COMUNA,
                                                      NOMBRE_COMUNA = co.NOMBRE_COMUNA,
                                                      ESTADO = co.ESTADO
                                                  }).ToList();

                    return dtoComuna;

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