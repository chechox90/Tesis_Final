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
    public class DAO_Sentido : I_DAO_Sentido
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DAO_Sentido()
        {
            XmlConfigurator.Configure();
        }

        public List<DTO_Sentido> GetSentidoByAll()
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

        public int GetSentidoByNombre(string nombre, int idEmpresa)
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    int respuesta = 0;
                    var sentido = (from sen in context.SENTIDO
                                   where sen.ESTADO == true && sen.NOMBRE_SENTIDO == nombre
                                   select new DTO_Sentido
                                   {
                                       ID_SENTIDO = sen.ID_SENTIDO

                                   }).FirstOrDefault();

                    if (sentido != null)
                        respuesta = sentido.ID_SENTIDO;

                    return respuesta;

                }

            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public int SetNuevoSentido(string sentido, string sentidoCorto)
        {
            try
            {
                int respuesta = 0;
                using (SolusegEntities context = new SolusegEntities())
                {
                    using (var contextTransaction = context.Database.BeginTransaction())
                    {
                        SENTIDO newSen = new SENTIDO()
                        {
                            NOMBRE_SENTIDO = sentido,
                            NOMBRE_CORTO_SENTIDO = sentidoCorto,
                            ESTADO = true

                        };

                        context.SENTIDO.Add(newSen);
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

        public int SetEliminarSentido(int idSentido)
        {
            try
            {
                int respuesta = 0;

                using (SolusegEntities context = new SolusegEntities())
                {
                    using (var contextTransaction = context.Database.BeginTransaction())
                    {
                        SENTIDO Old = context.SENTIDO.Where(x => x.ESTADO == true && x.ID_SENTIDO == idSentido).FirstOrDefault();
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

        public int SetEditarSentido(DTO_Sentido SENTIDO)
        {
            try
            {
                int respuesta = 0;

                using (SolusegEntities context = new SolusegEntities())
                {
                    using (var contextTransaction = context.Database.BeginTransaction())
                    {
                        SENTIDO Old = context.SENTIDO.Where(x => x.ESTADO == true && x.ID_SENTIDO == SENTIDO.ID_SENTIDO).FirstOrDefault();

                        Old.ID_SENTIDO = SENTIDO.ID_SENTIDO;
                        Old.NOMBRE_SENTIDO = SENTIDO.NOMBRE_SENTIDO;
                        Old.NOMBRE_CORTO_SENTIDO = SENTIDO.NOMBRE_CORTO_SENTIDO;
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