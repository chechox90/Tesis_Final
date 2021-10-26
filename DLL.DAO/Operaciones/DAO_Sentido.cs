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
    public class DAO_Sentido: I_DAO_Sentido
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DAO_Sentido()
        {
            XmlConfigurator.Configure();
        }

        public List<DTO_Sentido> GetSentidoByAllActiveForTable()
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    List<DTO_Sentido> dtoSentido = (from sen in context.SENTIDO
                                                    where sen.ESTADO == true
                                                    select new DTO_Sentido
                                                    {
                                                        ID_SENTIDO = sen.ID_SENTIDO,
                                                        NOMBRE_SENTIDO = sen.NOMBRE_SENTIDO,
                                                        NOMBRE_CORTO_SENTIDO = sen.NOMBRE_CORTO_SENTIDO,
                                                        ESTADO = sen.ESTADO
                                                    }).ToList();

                    return dtoSentido;

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