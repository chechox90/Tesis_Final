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
    public class DAO_Servicio : I_DAO_Servicio
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DAO_Servicio()
        {
            XmlConfigurator.Configure();
        }

        public List<DTO_Servicio> GetServicioByAllActiveForTable()
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    List<DTO_Servicio> dtoServicio = (from ser in context.SERVICIO
                                                      join em in context.EMPRESA on ser.ID_EMPRESA equals em.ID_EMPRESA
                                                      where ser.ESTADO == true
                                                      select new DTO_Servicio
                                                      {
                                                          ID_SERVICIO = ser.ID_SERVICIO,
                                                          ID_EMPRESA = ser.ID_EMPRESA,
                                                          NOMBRE_EMPRESA = em.NOMBRE_EMPRESA,
                                                          NOMBRE_SERVICIO = ser.NOMBRE_SERVICIO,
                                                          HORARIO_INICIO_SERVICO = ser.HORARIO_INICIO_SERVICO,
                                                          HORARIO_FIN_SERVICO = ser.HORARIO_FIN_SERVICO,
                                                          ESTADO = ser.ESTADO
                                                      }).ToList();

                    return dtoServicio;

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
