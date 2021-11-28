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
                                                          HORARIO_INI = ser.HORARIO_INI,
                                                          HORARIO_FIN = ser.HORARIO_FIN,
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

        public int GetServicioByNombre(string nombre)
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    int respuesta = 0;
                    var servicio = (from ser in context.SERVICIO
                                    where ser.ESTADO == true && ser.NOMBRE_SERVICIO == nombre
                                    select new DTO_Servicio
                                    {
                                        ID_SERVICIO = ser.ID_SERVICIO,
                                    }).FirstOrDefault();
                    if (servicio != null)
                        respuesta = servicio.ID_SERVICIO;

                    return respuesta;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public int SetNuevoServicio(string nombreTer, string horario_ini, string horario_fin)
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
                            HORARIO_INI = TimeSpan.Parse(horario_ini),
                            HORARIO_FIN = TimeSpan.Parse(horario_fin),
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

        public int SetEditarServicio(DTO_Servicio SERVICIO)
        {
            try
            {
                int respuesta = 0;

                using (SolusegEntities context = new SolusegEntities())
                {
                    using (var contextTransaction = context.Database.BeginTransaction())
                    {
                        SERVICIO Old = context.SERVICIO.Where(x => x.ESTADO == true && x.ID_SERVICIO == SERVICIO.ID_SERVICIO).FirstOrDefault();

                        Old.ID_SERVICIO = Old.ID_SERVICIO;
                        Old.ID_EMPRESA = Old.ID_EMPRESA;
                        Old.NOMBRE_SERVICIO = SERVICIO.NOMBRE_SERVICIO;
                        Old.HORARIO_INI = SERVICIO.HORARIO_INI;
                        Old.HORARIO_FIN = SERVICIO.HORARIO_FIN;
                        Old.ESTADO = true;
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
