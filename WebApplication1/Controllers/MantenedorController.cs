using DLL.DTO.Mantenedor;
using DLL.DTO.Terminales;
using DLL.NEGOCIO.Operaciones.Interfaces;
using DLL.NEGOCIO.Seguridad.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConductorEnRed.Controllers
{
    public class MantenedorController : Controller
    {
        private readonly I_N_Terminal _i_n_Terminal;
        private readonly I_N_Usuario _i_n_usuario;
        private readonly I_N_Comuna _i_n_Comuna;
        private readonly I_N_Bus _i_n_Bus;
        private readonly I_N_Servicio _i_n_Servicio;
        private readonly I_N_Sentido _i_n_Sentido;

        public MantenedorController(I_N_Terminal i_n_Terminal,
           I_N_Usuario i_n_usuario,
            I_N_Bus i_n_bus,
            I_N_Comuna i_n_comuna,
            I_N_Sentido i_n_sentido,
            I_N_Servicio i_n_servicio)
        {
            this._i_n_Terminal = i_n_Terminal;
            this._i_n_usuario = i_n_usuario;
            this._i_n_Comuna = i_n_comuna;
            this._i_n_Bus = i_n_bus;
            this._i_n_Servicio = i_n_servicio;
            this._i_n_Sentido = i_n_sentido;
        }

        #region Terminales
        public ActionResult GetTerminalesActivos()
        {
            try
            {
                List<DTO_Terminal> DtoTerminal = new List<DTO_Terminal>();
                DtoTerminal = _i_n_Terminal.GetTerminalByAllActiveForTable();

                if (DtoTerminal != null)
                {
                    return Json(new { data = DtoTerminal, });
                }
                else
                {
                    return Json(new
                    {
                        EnableError = true,
                        ErrorTitle = "Error",
                        ErrorMsg = "Ha ocurrido una insidencia al <b>obtener los terminales</b>"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult GetCargarTerminalesCmb()
        {
            try
            {
                List<DTO_Terminal> DtoTerminal = new List<DTO_Terminal>();
                List<DTO_Terminal> list = new List<DTO_Terminal>();
                DtoTerminal = _i_n_Terminal.GetTerminalByAllActive();

                if (DtoTerminal != null)
                {
                    DTO_Terminal cargaTer0 = new DTO_Terminal();
                    cargaTer0.ID_TERMINAL = 0;
                    cargaTer0.NOMBRE_TERMINAL = "Seleccione";

                    list.Add(cargaTer0);

                    foreach (var item in DtoTerminal)
                    {
                        DTO_Terminal carga = new DTO_Terminal();
                        carga.ID_TERMINAL = item.ID_TERMINAL;
                        carga.NOMBRE_TERMINAL = item.NOMBRE_TERMINAL;

                        list.Add(carga);
                    }


                    return Json(new
                    {
                        data = list,
                        ErrorMsg = "",
                        JsonRequestBehavior.AllowGet
                    });
                }
                else
                {
                    return Json(new
                    {
                        EnableError = true,
                        ErrorTitle = "Error",
                        ErrorMsg = "Ha ocurrido una insidencia al <b>obtener los terminales</b>"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public ActionResult SetGuardarNuevoTerminal(string nombreTer, string direccion, int numDire)
        {
            try
            {
                int response = _i_n_Terminal.SetNuevoTerminal(nombreTer, direccion, numDire);
                string alert = "";

                if (response == 1)
                {
                    alert = "success";
                    var message = "El terminal ha sido guardado con éxito";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }
                else
                {
                    alert = "danger";
                    var message = "Ha ocurrido una incidencia, inténtelo más tarde";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult SetEliminarTerminal(int idTerminal)
        {
            try
            {
                int response = _i_n_Terminal.SetEliminarTerminal(idTerminal);
                string alert = "";

                if (response == 1)
                {
                    alert = "success";
                    var message = "El terminal ha sido eliminado con éxito";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }
                else
                {
                    alert = "danger";
                    var message = "Ha ocurrido una incidencia, inténtelo más tarde";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult SetEditarTerminal(DTO_Terminal TERMINAL)
        {
            try
            {
                TERMINAL.ID_TERMINAL = TERMINAL.ID_TERMINAL;
                TERMINAL.NOMBRE_TERMINAL = TERMINAL.NOMBRE_TERMINAL;
                TERMINAL.ID_EMPRESA = 1;
                TERMINAL.DIRECCION = TERMINAL.DIRECCION;
                TERMINAL.NUM_DIRECCION = TERMINAL.NUM_DIRECCION;
            
                int response = _i_n_Terminal.SetEditarTerminal(TERMINAL);
                string alert = "";

                if (response == 1)
                {
                    alert = "success";
                    var message = "El terminal ha sido editado con éxito";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }
                else
                {
                    alert = "danger";
                    var message = "Ha ocurrido una incidencia, inténtelo más tarde";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion



        #region Buses
        public ActionResult GetBusesActivos()
        {
            try
            {
                List<DTO_Bus> dtoBus = new List<DTO_Bus>();
                dtoBus = _i_n_Bus.GetBusByAllActiveForTable();
                string alert = "";

                if (dtoBus != null)
                {
                    return Json(new { data = dtoBus, });
                }
                else
                {
                    alert = "danger";
                    var message = "Ha ocurrido una incidencia, inténtelo más tarde";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult SetGuardarNuevoBus(int idTerminal, string ppu, int numeroBus)
        {
            try
            {
                string alert = "";

                if (ValidarBus(idTerminal, ppu) == "")
                {
                    int response = _i_n_Bus.SetNuevoBus(idTerminal, ppu, numeroBus);

                    if (response == 1)
                    {

                        alert = "success";
                        var message = "El bus ha sido guardado con éxito";
                        return new JsonResult()
                        {
                            Data = Json(new { alert = alert, message = message })
                        };
                    }
                    else
                    {
                        alert = "danger";
                        var message = "Ha ocurrido una incidencia, inténtelo más tarde";
                        return new JsonResult()
                        {
                            Data = Json(new { alert = alert, message = message })
                        };
                    }

                }
                else
                {
                    alert = "danger";
                    var message = "Ha ocurrido una incidencia, inténtelo más tarde";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }


            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult SetEliminarBus(int idBus)
        {
            try
            {
                int response = _i_n_Bus.SetEliminarBus(idBus);
                string alert = "";

                if (response == 1)
                {
                    alert = "success";
                    var message = "El bus ha sido elimando con éxito";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }
                else
                {
                    alert = "danger";
                    var message = "Ha ocurrido una incidencia, inténtelo más tarde";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult SetEditarBus(DTO_Bus Bus)
        {
            try
            {
                int response = _i_n_Bus.SetEditarBus(Bus);
                string alert = "";

                if (response == 1)
                {
                    alert = "success";
                    var message = "El bus ha sido editado con éxito";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }
                else
                {
                    alert = "danger";
                    var message = "Ha ocurrido una incidencia, inténtelo más tarde";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        private static string ValidarBus(int idTerminal, string ppu)
        {
            string respuesta = "";

            if (idTerminal == 0)
                respuesta = "Debe seleccionar un terminal para continuar";

            if (ppu == "")
                respuesta = "Debe ingresar una PPU";

            return respuesta;
        }

        #endregion



        #region Comunas
        public ActionResult GetComunasCmb()
        {
            try
            {
                List<DTO_Comuna> dtoComuna = new List<DTO_Comuna>();
                dtoComuna = _i_n_Comuna.GetComunaByAllActiveForTable();

                if (dtoComuna != null)
                {
                    return Json(new { data = dtoComuna.ToList().OrderBy(x => x.NOMBRE_COMUNA), });
                }
                else
                {
                    return Json(new
                    {
                        EnableError = true,
                        ErrorTitle = "Error",
                        ErrorMsg = "Ha ocurrido una insidencia al <b>obtener los terminales</b>"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult SetNuevaComuna(string nomComuna)
        {
            try
            {
                string alert = "";


                int response = _i_n_Comuna.SetNuevaComuna(nomComuna);

                if (response == 1)
                {

                    alert = "success";
                    var message = "La comuna se ha sido guardada con éxito";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }
                else
                {
                    alert = "danger";
                    var message = "Ha ocurrido una incidencia, inténtelo más tarde";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult SetEliminarComuna(int idComuna)
        {
            try
            {
                string alert = "";

                int response = _i_n_Comuna.SetEliminarComuna(idComuna);

                if (response == 1)
                {

                    alert = "success";
                    var message = "La comuna se ha sido guardada con éxito";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }
                else
                {
                    alert = "danger";
                    var message = "Ha ocurrido una incidencia, inténtelo más tarde";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult SetEditarComuna(DTO_Comuna COMUNA)
        {
            try
            {
                int response = _i_n_Comuna.SetEditarComuna(COMUNA);
                string alert = "";

                if (response == 1)
                {
                    alert = "success";
                    var message = "El Comuna ha sido editado con éxito";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }
                else
                {
                    alert = "danger";
                    var message = "Ha ocurrido una incidencia, inténtelo más tarde";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion



        #region Servicios

        public ActionResult GetServiciosActivos()
        {
            try
            {
                List<DTO_Servicio> dtoServicio = new List<DTO_Servicio>();
                dtoServicio = _i_n_Servicio.GetServicioByAllActiveForTable();

                List<DTO_Servicio> list = new List<DTO_Servicio>();
                foreach (var item in dtoServicio)
                {
                    DTO_Servicio carga = new DTO_Servicio();
                    carga.ID_SERVICIO = item.ID_SERVICIO;
                    carga.ID_EMPRESA = item.ID_EMPRESA;
                    carga.NOMBRE_EMPRESA = item.NOMBRE_EMPRESA;
                    carga.NOMBRE_SERVICIO = item.NOMBRE_SERVICIO;

                    carga.HORARIO_INICIO = item.HORARIO_INI.ToString().Substring(0, 5);
                    carga.HORARIO_FIN_ = item.HORARIO_FIN.ToString().Substring(0, 5);
                    list.Add(carga);
                }
                list = list.OrderBy(x => x.NOMBRE_SERVICIO).ToList();

                if (list != null)
                {
                    return Json(new { data = list, });
                }
                else
                {
                    return Json(new
                    {
                        EnableError = true,
                        ErrorTitle = "Error",
                        ErrorMsg = "Ha ocurrido una insidencia al <b>obtener los terminales</b>"
                    });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult SetGuardarNuevoServicio(string nombreSer, string horaIni, string horaFin)
        {
            try
            {
                string alert = "";


                int response = _i_n_Servicio.SetNuevoServicio(nombreSer, horaIni, horaFin);

                if (response == 1)
                {

                    alert = "success";
                    var message = "El Servicio ha sido guardado con éxito";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }
                else
                {
                    alert = "danger";
                    var message = "Ha ocurrido una incidencia, inténtelo más tarde";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult SetEliminarServicio(int idServicio)
        {
            try
            {
                int response = _i_n_Servicio.SetEliminarServicio(idServicio);
                string alert = "";

                if (response == 1)
                {
                    alert = "success";
                    var message = "El Servicio ha sido elimando con éxito";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }
                else
                {
                    alert = "danger";
                    var message = "Ha ocurrido una incidencia, inténtelo más tarde";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult SetEditarServicio(DTO_Servicio SERVICIO)
        {
            try
            {
                int response = _i_n_Servicio.SetEditarServicio(SERVICIO);
                string alert = "";

                if (response == 1)
                {
                    alert = "success";
                    var message = "El Servicio ha sido elimando con éxito";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }
                else
                {
                    alert = "danger";
                    var message = "Ha ocurrido una incidencia, inténtelo más tarde";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion


        #region Sentido

        public ActionResult GetSentidoCmb()
        {
            try
            {
                List<DTO_Sentido> list = new List<DTO_Sentido>();
                List<DTO_Sentido> sentido = new List<DTO_Sentido>();
                sentido = _i_n_Sentido.GetSentidoByAll();

                DTO_Sentido cargaSen = new DTO_Sentido();
                cargaSen.ID_SENTIDO = 0;
                cargaSen.NOMBRE_SENTIDO = "Seleccione";
                cargaSen.NOMBRE_CORTO_SENTIDO = "Seleccione";

                list.Add(cargaSen);

                foreach (var item in sentido)
                {
                    DTO_Sentido carga = new DTO_Sentido();
                    carga.ID_SENTIDO = item.ID_SENTIDO;
                    carga.NOMBRE_SENTIDO = item.NOMBRE_SENTIDO;
                    carga.NOMBRE_CORTO_SENTIDO = item.NOMBRE_CORTO_SENTIDO;

                    list.Add(carga);
                }


                return Json(new
                {
                    data = list,
                    ErrorMsg = "",
                    JsonRequestBehavior.AllowGet
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult SetNuevoSentido(string sentido,string sentidoCorto)
        {
            try
            {
                string alert = "";


                int response = _i_n_Sentido.SetNuevoSentido(sentido,sentidoCorto);

                if (response == 1)
                {

                    alert = "success";
                    var message = "El Sentido se ha sido guardada con éxito";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }
                else
                {
                    alert = "danger";
                    var message = "Ha ocurrido una incidencia, inténtelo más tarde";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult SetEliminarSentido(int idSentido)
        {
            try
            {
                string alert = "";

                int response = _i_n_Sentido.SetEliminarSentido(idSentido);

                if (response == 1)
                {

                    alert = "success";
                    var message = "La Sentido se ha sido guardada con éxito";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }
                else
                {
                    alert = "danger";
                    var message = "Ha ocurrido una incidencia, inténtelo más tarde";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult SetEditarSentido(DTO_Sentido SENTIDO)
        {
            try
            {
                int response = _i_n_Sentido.SetEditarSentido(SENTIDO);
                string alert = "";

                if (response == 1)
                {
                    alert = "success";
                    var message = "El Sentido ha sido editado con éxito";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }
                else
                {
                    alert = "danger";
                    var message = "Ha ocurrido una incidencia, inténtelo más tarde";
                    return new JsonResult()
                    {
                        Data = Json(new { alert = alert, message = message })
                    };
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion



    }

}
