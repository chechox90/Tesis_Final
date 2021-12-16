﻿using DLL.DAO.Operaciones.Interfaces;
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

        public int GetComunaByNombre(string nombre)
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    int respuesta = 0;
                    var comuna = (from co in context.COMUNA
                                  where co.NOMBRE_COMUNA == nombre && co.ESTADO == true
                                  select new DTO_Comuna
                                  {
                                      ID_COMUNA = co.ID_COMUNA,

                                  }).FirstOrDefault();

                    if (comuna != null)
                        respuesta = comuna.ID_COMUNA;

                    return respuesta;

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
                            ID_COMUNA = 1,
                            NOMBRE_COMUNA = nomComuna,
                            MOTIVO_EDICION = null,
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

        public int SetEditarComuna(DTO_Comuna COMUNA)
        {
            try
            {
                int respuesta = 0;
                using (SolusegEntities context = new SolusegEntities())
                {
                    using (var contextTransaction = context.Database.BeginTransaction())
                    {
                        COMUNA Old = context.COMUNA.Where(x => x.ESTADO == true && x.ID_COMUNA == COMUNA.ID_COMUNA).FirstOrDefault();

                        Old.ID_COMUNA = COMUNA.ID_COMUNA;
                        Old.NOMBRE_COMUNA = COMUNA.NOMBRE_COMUNA;
                        Old.ESTADO = true;

                        respuesta = context.SaveChanges();
                        contextTransaction.Commit();

                    }
                }
                if (respuesta == 0)
                {
                    respuesta = -1;
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