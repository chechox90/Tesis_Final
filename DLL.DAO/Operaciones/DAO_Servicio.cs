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

        public int SetNuevoServicio(string nombreTer, string direccion, string numDire)
        {
            try
            {
                int respuesta = 0;
                using (SolusegEntities context = new SolusegEntities())
                {
                    using (var contextTransaction = context.Database.BeginTransaction())
                    {
                        SERVICIO newSer = new SERVICIO()
                        {
                            ID_EMPRESA = 1,
                            NOMBRE_SERVICIO = nombreTer,
                            HORARIO_INICIO_SERVICO= TimeSpan.Parse(direccion),
                            HORARIO_FIN_SERVICO = TimeSpan.Parse(numDire),
                            ESTADO = true

                        };

                        context.SERVICIO.Add(newSer);
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

        public int SetEliminarServicio(int idServicio)
        {
            try
            {
                int respuesta = 0;

                using (SolusegEntities context = new SolusegEntities())
                {
                    using (var contextTransaction = context.Database.BeginTransaction())
                    {
                        SERVICIO Old = context.SERVICIO.Where(x => x.ESTADO == true && x.ID_SERVICIO == idServicio).FirstOrDefault();
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

    }
}
