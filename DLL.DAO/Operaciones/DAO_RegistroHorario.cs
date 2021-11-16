using DLL.DAO.Operaciones.Interfaces;
using DLL.DATA.SeguridadSoluinfo;
using DLL.DTO.Mantenedor;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DLL.DAO.Operaciones
{
    public class DAO_RegistroHorario : I_DAO_RegistroHorario
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DAO_RegistroHorario()
        {
            XmlConfigurator.Configure();
        }

        public List<DTO_RegistroHorario> GetRegistroByAll()
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    List<DTO_RegistroHorario> dtoRegistro = (from reg in context.REGISTRO_HORARIO
                                                    where reg.ESTADO == true
                                                    select new DTO_RegistroHorario
                                                    {
                                                        ID_REGISTRO_HORARIO = reg.ID_REGISTRO_HORARIO,
                                                        ID_USUARIO = reg.ID_USUARIO,
                                                        ID_TERMINAL_INICIO = reg.ID_TERMINAL_INICIO,
                                                        FECHA_HORA_INICIO = reg.FECHA_HORA_INICIO,
                                                        ID_TERMINAL_FIN = reg.ID_TERMINAL_FIN,
                                                        FECHA_HORA_FIN = reg.FECHA_HORA_FIN,
                                                        ESTADO = reg.ESTADO
                                                    }).ToList();

                    return dtoRegistro;

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
