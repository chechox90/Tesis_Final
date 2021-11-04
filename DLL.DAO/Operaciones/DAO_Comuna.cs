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

        public int SetNuevaComuna(string nomComuna)
        {
            try
            {
                int respuesta = 0;
                using (SolusegEntities context = new SolusegEntities())
                {
                    using (var contextTransaction = context.Database.BeginTransaction())
                    {
                        COMUNA newCom = new COMUNA()
                        {
                            NOMBRE_COMUNA = nomComuna,
                            ESTADO = true
                        };

                        context.COMUNA.Add(newCom);
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

        public int SetEliminarComuna(int idComuna)
        {
            try
            {
                int respuesta = 0;
                using (SolusegEntities context = new SolusegEntities())
                {
                    using (var contextTransaction = context.Database.BeginTransaction())
                    {
                        COMUNA Old = context.COMUNA.Where(x => x.ESTADO == true && x.ID_COMUNA == idComuna).FirstOrDefault();
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