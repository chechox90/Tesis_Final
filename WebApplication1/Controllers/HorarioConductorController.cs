using ConductorEnRed.ViewModels.Mantenedores;
using DLL.DTO.CargaHorario;
using DLL.DTO.Mantenedor;
using DLL.DTO.Seguridad;
using DLL.NEGOCIO.Operaciones.Interfaces;
using DLL.NEGOCIO.Seguridad.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Commons;

namespace ConductorEnRed.Controllers
{
    public class HorarioConductorController : Controller
    {
        private readonly I_N_HorarioConductor _i_n_HorarioConductor;
        private readonly I_N_Terminal _i_n_Terminal;
        private readonly I_N_Usuario _i_n_usuario;

        DTO_Usuario usuario = FrontUser.Get();

        public HorarioConductorController(I_N_HorarioConductor i_n_HorarioConductor,
            I_N_Terminal i_n_Terminal,
            I_N_Usuario i_n_usuario)
        {
            this._i_n_HorarioConductor = i_n_HorarioConductor;
            this._i_n_Terminal = i_n_Terminal;
            this._i_n_usuario = i_n_usuario;

        }

        [HttpPost]
        public ActionResult SetIngresarSolictud(int idHorarioCambiar,int tipoSolicitud,string motivoSolictud, string motivoOpcional)
        {
            DTO_SolicitudCambioHorario solicitud = new DTO_SolicitudCambioHorario();
            solicitud.ID_SOLICITUD_CAMBIO = 0;
            solicitud.ID_HORARIO_CAMBIAR = idHorarioCambiar;
            solicitud.ID_TIPO_SOLICITUD = tipoSolicitud;
            solicitud.ID_ESTADO_SOLICITUD = 1;
            solicitud.ID_USUARIO_SOLICITA = 1;
            solicitud.ID_USUARIO_APRUEBA = null;
            solicitud.FECHA_REGISTRO_SOLICITUD = DateTime.Now;
            solicitud.FECHA_APROBACION = null;
            solicitud.COMENTARIO_MOTIVO = motivoSolictud;
            solicitud.COMENTARIO_ADICIONAL = motivoOpcional;
            solicitud.ESTADO = true;

            int respuesta =  _i_n_HorarioConductor.SetIngresaSolicitud(solicitud);
            string alert = "";

            if (respuesta == 1)
            {
                alert = "success";
                var message = "La solicitud ha sido enviada con éxito. Redirigiendo a la página anterior...";
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
        
        public static string IngresarPuntosEnRut(string RUN)
        {
            RUN = RUN.Replace("-", "");
            var primerGrupo = RUN.Substring(RUN.Length - 4);
            var segundoGrupo = RUN.Substring(RUN.Length - 7).Substring(0, 3);
            int resta = RUN.Length - 7;
            var tercerGrupo = RUN.Substring(0, resta);
            string rut = tercerGrupo + "." + segundoGrupo + "." + primerGrupo.Substring(0, 3) + "-" + primerGrupo.Substring(3, 1);

            return rut;
        }

        [HttpPost]
        public ActionResult GetHorarioIndividualTurnoUno(string fechaSemanaActual)
        {
            try
            {
                string fechaSemana = "";
                if (fechaSemanaActual == null)
                    fechaSemana = ObtenerFechaSemana();
                else
                    fechaSemana = fechaSemanaActual;

                DateTime fechaIni = DateTime.Parse(fechaSemana.Split('-')[0].ToString());
                DateTime fechaFin = DateTime.Parse(fechaSemana.Split('-')[1].ToString());

                List<DTO_HorarioConductorMostrar> dto_Horario = _i_n_HorarioConductor.GetHorarioConductorById(1, fechaIni, fechaFin, 1);
                List<DTO_HorarioConductorMostrar> list = new List<DTO_HorarioConductorMostrar>();

                if (dto_Horario.Count > 0)
                {
                    int ind = 0;
                    DateTime fechaAnterior = DateTime.Now;
                    for (int i = 1; i < 8; i++)
                    {

                        DTO_HorarioConductorMostrar carga = new DTO_HorarioConductorMostrar();
                        if (i <= dto_Horario.Count)
                        {
                            fechaAnterior = dto_Horario[ind].FECHA_HORA_INICIO;
                            carga.NUM_ROW = i;
                            carga.ID_HORARIO = dto_Horario[ind].ID_HORARIO;
                            carga.ID_CARGA_HORARIO = dto_Horario[ind].ID_CARGA_HORARIO;
                            carga.NOMBRE_DIA = dto_Horario[ind].FECHA_HORA_INICIO.ToString("dddd");
                            carga.FECHA_HORA_INICIO = dto_Horario[ind].FECHA_HORA_INICIO;
                            carga.NOMBRE_TERMINAL = dto_Horario[ind].NOMBRE_TERMINAL;
                            carga.HORA_INICIO = dto_Horario[ind].FECHA_HORA_INICIO.ToString("dd/MM/yyyy HH:mm").Split(' ')[1];
                            carga.RUT = IngresarPuntosEnRut(dto_Horario[ind].RUT);
                            carga.NOMBRE_COMPLETO = dto_Horario[ind].NOMBRE + " " + dto_Horario[0].SEGUNDO_NOMBRE + " " + dto_Horario[0].APELLIDO_PATERNO + " " + dto_Horario[0].APELLIDO_MATERNO;
                            list.Add(carga);
                        }
                        else
                        {
                            carga.ID_HORARIO = 0;
                            carga.NUM_ROW = i;
                            carga.ID_CARGA_HORARIO = 0;
                            carga.NOMBRE_DIA = fechaAnterior.AddDays(1).ToString("dddd");
                            carga.FECHA_INICIO = DateTime.Now.ToString("dd/MM/yyyy HH:mm").Split(' ')[0];
                            carga.NOMBRE_TERMINAL = "";
                            carga.HORA_INICIO = "";

                            fechaAnterior = fechaAnterior.AddDays(1);
                            carga.RUT = "";
                            carga.NOMBRE_COMPLETO = "";

                            list.Add(carga);
                        }
                        ind++;
                    }
                }


                return Json(new
                {
                    data = list.OrderBy(x => x.NUM_ROW),
                    ErrorMsg = "",
                    JsonRequestBehavior.AllowGet
                });


            }
            catch (Exception)
            {

                return Json(new
                {
                    EnableError = true,
                    ErrorTitle = "Error",
                    ErrorMsg = "El R.U.N. ingresado <b>no es válido</b>"
                });
            }

        }

        [HttpPost]
        public ActionResult GetHorarioIndividualTurnoDos()
        {
            try
            {

                string fechaSemana = ObtenerFechaSemana();

                DateTime fechaIni = DateTime.Parse(fechaSemana.Split('-')[0].ToString());
                DateTime fechaFin = DateTime.Parse(fechaSemana.Split('-')[1].ToString());

                List<DTO_HorarioConductorMostrar> dto_Horario = _i_n_HorarioConductor.GetHorarioConductorById(1, fechaIni, fechaFin, 2);
                List<DTO_HorarioConductorMostrar> list = new List<DTO_HorarioConductorMostrar>();

                if (dto_Horario.Count > 0)
                {
                    int ind = 0;
                    for (int i = 1; i < 15; i++)
                    {
                        DTO_HorarioConductorMostrar carga = new DTO_HorarioConductorMostrar();
                        if (i <= dto_Horario.Count)
                        {
                            carga.ID_HORARIO = dto_Horario[ind].ID_HORARIO;
                            carga.ID_CARGA_HORARIO = dto_Horario[ind].ID_CARGA_HORARIO;
                            carga.NOMBRE_DIA = dto_Horario[ind].FECHA_HORA_INICIO.DayOfWeek.ToString("dddd");
                            carga.FECHA_HORA_INICIO = dto_Horario[ind].FECHA_HORA_INICIO;
                            carga.NOMBRE_TERMINAL = dto_Horario[ind].NOMBRE_TERMINAL;
                            carga.HORA_INICIO = dto_Horario[ind].FECHA_HORA_INICIO.ToString("dd/MM/yyyy HH:mm").Split(' ')[1];
                            carga.RUT = IngresarPuntosEnRut(dto_Horario[ind].RUT);
                            carga.NOMBRE_COMPLETO = dto_Horario[ind].NOMBRE + " " + dto_Horario[0].SEGUNDO_NOMBRE + " " + dto_Horario[0].APELLIDO_PATERNO + " " + dto_Horario[0].APELLIDO_MATERNO;
                            list.Add(carga);
                        }
                        else
                        {
                            carga.ID_HORARIO = 0;
                            carga.ID_CARGA_HORARIO = 0;
                            carga.NOMBRE_DIA = dto_Horario[ind].FECHA_HORA_INICIO.DayOfWeek.ToString("dddd");
                            carga.FECHA_INICIO = DateTime.Now.ToString("dd/MM/yyyy HH:mm").Split(' ')[0];
                            carga.NOMBRE_TERMINAL = "";
                            carga.HORA_INICIO = "";
                            carga.RUT = "";
                            carga.NOMBRE_COMPLETO = "";

                            list.Add(carga);
                        }
                        ind++;
                    }
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

                return Json(new
                {
                    EnableError = true,
                    ErrorTitle = "Error",
                    ErrorMsg = "El R.U.N. ingresado <b>no es válido</b>"
                });
            }

        }

        public static string ObtenerFechaSemana()
        {
            DateTime fechaHoy = DateTime.Now;

            int dia = Convert.ToInt32(fechaHoy.DayOfWeek);
            dia = dia - 1;

            string fechaList = "";

            if (dia == -1)
            {
                DateTime fechaSemActLunes = fechaHoy.AddDays(6 * -1);
                DateTime fechaSemActDomingo = fechaHoy.AddDays(6 * -1).AddDays(6);
                fechaList = fechaSemActLunes.ToString("dd/MM/yyyy") + " - " + fechaSemActDomingo.ToString("dd/MM/yyyy");
            }
            else
            {
                //semana actual de lunes a domingo
                DateTime fechaSemActLunnes = fechaHoy.AddDays(dia * -1);
                DateTime fechaSemActDomingo = fechaHoy.AddDays((dia) * (-1)).AddDays(6);
                fechaList = fechaSemActLunnes.ToString("dd/MM/yyyy") + " - " + fechaSemActDomingo.ToString("dd/MM/yyyy");
            }



            return fechaList;

        }

        public ActionResult ObtenerFechaBotonesClick(string fechaSemanas)
        {
            DateTime fechaHoy = DateTime.Parse(fechaSemanas.Split('-')[0].ToString().Trim());


            int dia = Convert.ToInt32(fechaHoy.DayOfWeek);
            dia = dia - 1;


            if (dia == -1)
            {
                dia = Convert.ToInt32(fechaHoy.AddDays(-1).DayOfWeek);
            }

            string fechaList = "";

            List<string> list = new List<string>();

            //semana anterior a la fecha de la semana actual
            DateTime fechaSemActLunnes = fechaHoy.AddDays((dia) * (-1)).AddDays(-7);
            DateTime fechaSemActDomingo = fechaSemActLunnes.AddDays((dia) * (-1)).AddDays(6);
            fechaList = fechaSemActLunnes.ToString("dd/MM/yyyy") + " - " + fechaSemActDomingo.ToString("dd/MM/yyyy");
            list.Add(fechaList);

            //semana actual de lunes a domingo
            fechaSemActLunnes = fechaHoy.AddDays((dia) * (-1));
            fechaSemActDomingo = fechaHoy.AddDays((dia) * (-1)).AddDays(6);
            fechaList = fechaSemActLunnes.ToString("dd/MM/yyyy") + " - " + fechaSemActDomingo.ToString("dd/MM/yyyy");
            list.Add(fechaList);

            //semana siguiente a la semana actual
            fechaSemActLunnes = fechaHoy.AddDays((dia) * (-1)).AddDays(7);
            fechaSemActDomingo = fechaHoy.AddDays((dia) * (-1)).AddDays(13);
            fechaList = fechaSemActLunnes.ToString("dd/MM/yyyy") + " - " + fechaSemActDomingo.ToString("dd/MM/yyyy");
            list.Add(fechaList);

            if (list.Count > 1)
            {
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
                    ErrorMsg = "Se ha producido un error por favor <b>vuelva a intentarlo</b>"
                });
            }

        }

        [HttpPost]
        public ActionResult ObtenerFechaBotonesInicial()
        {
            DateTime fechaHoy = DateTime.Now;

            int dia = Convert.ToInt32(fechaHoy.DayOfWeek);
            dia = dia - 1;

            if (dia == -1)
            {
                dia = Convert.ToInt32(fechaHoy.AddDays(-1).DayOfWeek);
            }

            string fechaList = "";

            List<string> list = new List<string>();

            //semana anterior a la fecha de la semana actual
            DateTime fechaSemActLunnes = fechaHoy.AddDays(dia * -1).AddDays(-7);
            DateTime fechaSemActDomingo = fechaSemActLunnes.AddDays(6);
            fechaList = fechaSemActLunnes.ToString("dd/MM/yyyy") + " - " + fechaSemActDomingo.ToString("dd/MM/yyyy");
            list.Add(fechaList);

            //semana actual de lunes a domingo
            fechaSemActLunnes = fechaHoy.AddDays((dia) * (-1));
            fechaSemActDomingo = fechaHoy.AddDays((dia) * (-1)).AddDays(6);
            fechaList = fechaSemActLunnes.ToString("dd/MM/yyyy") + " - " + fechaSemActDomingo.ToString("dd/MM/yyyy");
            list.Add(fechaList);

            //semana siguiente a la semana actual
            fechaSemActLunnes = fechaHoy.AddDays((dia) * (-1)).AddDays(7);
            fechaSemActDomingo = fechaHoy.AddDays((dia) * (-1)).AddDays(13);
            fechaList = fechaSemActLunnes.ToString("dd/MM/yyyy") + " - " + fechaSemActDomingo.ToString("dd/MM/yyyy");
            list.Add(fechaList);

            string fechaFormaList = fechaHoy.AddDays((dia) * (-1)).ToString("dd/MM/yyyy");
            list.Add(fechaFormaList);

            if (list.Count > 1)
            {
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
                    ErrorMsg = "Se ha producido un error por favor <b>vuelva a intentarlo</b>"
                });
            }

        }

        [HttpGet]
        public ActionResult SetEditHorario(int idHorario)
        {
            DTO_HorarioConductorMostrar dto_horario = new DTO_HorarioConductorMostrar();
            dto_horario = _i_n_HorarioConductor.GetHorarioConductorByIdHorario(idHorario);

            VM_HORARIO_SOLICITUD Horario = new VM_HORARIO_SOLICITUD();
            Horario.ID_HORARIO = idHorario;
            Horario.ID_USUARIO = dto_horario.ID_USUARIO;
            Horario.ID_CARGA_HORARIO = dto_horario.ID_CARGA_HORARIO;
            Horario.NOMBRE = dto_horario.NOMBRE;
            Horario.SEGUNDO_NOMBRE = dto_horario.SEGUNDO_NOMBRE;
            Horario.RUT = dto_horario.RUT;
            Horario.APELLIDO_PATERNO = dto_horario.APELLIDO_PATERNO;
            Horario.APELLIDO_MATERNO = dto_horario.APELLIDO_MATERNO;
            Horario.NOMBRE_TERMINAL = dto_horario.NOMBRE_TERMINAL;
            Horario.ID_TERMINAL = dto_horario.ID_TERMINAL;
            Horario.NUMERO_JORNADA = dto_horario.NUMERO_JORNADA;
            Horario.FECHA_HORA_INICIO = dto_horario.FECHA_HORA_INICIO;
            Horario.FECHA_INICIO = DateTime.Now.ToString().Split(' ')[0];
            Horario.HORA_INICIO = DateTime.Now.ToString().Split(' ')[1];
            Horario.ESTADO = dto_horario.ESTADO;

            return View("~/Views/HorarioConductor/Solicitudes.cshtml", Horario);

        }

        [HttpPost]
        public ActionResult GetTipoSolicitudesCmb()
        {
            List<DTO_TipoSolicitud> listdto = _i_n_HorarioConductor.GetTipoSolicitudAll();
            List<DTO_TipoSolicitud> list = new List<DTO_TipoSolicitud>();

            DTO_TipoSolicitud tipoSolicitudselect = new DTO_TipoSolicitud();
            tipoSolicitudselect.ID_TIPO_SOLICITUD = 0;
            tipoSolicitudselect.NOMBRE_SOLICITUD = "Seleccione";
            list.Add(tipoSolicitudselect);

            foreach (var item in listdto)
            {
                DTO_TipoSolicitud tipoSolicitud = new DTO_TipoSolicitud();
                tipoSolicitud.ID_TIPO_SOLICITUD = item.ID_TIPO_SOLICITUD;
                tipoSolicitud.NOMBRE_SOLICITUD = item.NOMBRE_SOLICITUD;

                list.Add(tipoSolicitud);
            }


            return Json(new
            {
                data = list,
                ErrorMsg = "",
                JsonRequestBehavior.AllowGet
            });

        }

    }
}
