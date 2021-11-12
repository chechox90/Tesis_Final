using ConductorEnRed.ViewModels.Mantenedores;
using DLL.DTO.CargaHorario;
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
        public ActionResult GetHorarioIndividualTurnoUno()
        {
            try
            {
                string fechaSemana = ObtenerFechaSemanas();

                DateTime fechaIni = DateTime.Parse(fechaSemana.Split('-')[0].ToString());
                DateTime fechaFin = DateTime.Parse(fechaSemana.Split('-')[1].ToString());

                List<DTO_HorarioConductorMostrar> dto_Horario = _i_n_HorarioConductor.GetHorarioConductorById(1, fechaIni, fechaFin, 1);
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
                            carga.ID_CARGA_HORARIO = 0;
                            carga.NOMBRE_DIA = DateTime.Now.AddDays(ind).ToString("dddd");
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
                    data = list.OrderBy(x => x.NOMBRE_DIA),
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

                string fechaSemana = ObtenerFechaSemanas();

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

        public static string ObtenerFechaSemanas()
        {
            DateTime fechaHoy = DateTime.Now;

            int dia = Convert.ToInt32(fechaHoy.DayOfWeek);
            dia = dia - 1;

            string fechaList = "";

            //semana actual de lunes a domingo
            DateTime fechaSemActLunnes = fechaHoy.AddDays((dia) * (-1));
            DateTime fechaSemActDomingo = fechaHoy.AddDays((dia) * (-1)).AddDays(6);
            fechaList = fechaSemActLunnes.ToString("dd/MM/yyyy") + " - " + fechaSemActDomingo.ToString("dd/MM/yyyy");

            return fechaList;

        }

        [HttpGet]
        public ActionResult SetEditHorario(int idHorario)
        {
            DTO_HorarioConductorMostrar dto_horario = new DTO_HorarioConductorMostrar();
            dto_horario = _i_n_HorarioConductor.GetHorarioConductorByIdHorario(idHorario);

            VM_HORARIO_SOLICITUD Horario = new VM_HORARIO_SOLICITUD();
            Horario.ID_HORARIO = dto_horario.ID_HORARIO;
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
            Horario.ESTADO = dto_horario.ESTADO;

            return View("~/Views/HorarioConductor/Solicitudes.cshtml", Horario);

        }

    }
}
