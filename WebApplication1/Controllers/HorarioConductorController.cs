using DLL.DTO.CargaHorario;
using DLL.DTO.Seguridad;
using DLL.NEGOCIO.Operaciones.Interfaces;
using DLL.NEGOCIO.Seguridad.Interfaces;
using System;
using System.Collections.Generic;
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
        public ActionResult GetHorarioIndividual()
        {
            try
            {

                if (true)
                {
                    string fechaSemana = ObtenerFechaSemanas();

                    DateTime fechaIni = DateTime.Parse(fechaSemana.Split('-')[0].ToString());
                    DateTime fechaFin = DateTime.Parse(fechaSemana.Split('-')[1].ToString());
                    //int idUsuario = usuario.ID_USUARIO;

                    List<DTO_HorarioConductorMostrar> dto_Horario = _i_n_HorarioConductor.GetHorarioConductorById(1, fechaIni, fechaFin);
                    List<DTO_HorarioConductorMostrar> list = new List<DTO_HorarioConductorMostrar>();

                    if (dto_Horario.Count > 0)
                    {
                        int ind = 0;
                        for (int i = 1; i < 15; i++)
                        {
                            DTO_HorarioConductorMostrar carga = new DTO_HorarioConductorMostrar();
                            if (i <= dto_Horario.Count)
                            {
                                carga.ID_USUARIO = dto_Horario[ind].ID_USUARIO;
                                carga.FECHA_INICIO = dto_Horario[ind].FECHA_HORA_INICIO.ToString().Split(' ')[0];

                                carga.HORA_INICIO = dto_Horario[ind].FECHA_HORA_INICIO.ToString("dd/MM/yyyy HH:mm").Split(' ')[1];
                                carga.RUT = IngresarPuntosEnRut(dto_Horario[ind].RUT);
                                carga.FECHA_HORA_INICIO = dto_Horario[ind].FECHA_HORA_INICIO;
                                carga.NOMBRE_TERMINAL = dto_Horario[ind].NOMBRE_TERMINAL;
                                carga.ID_TERMINAL = dto_Horario[ind].ID_TERMINAL;
                                carga.NOMBRE_COMPLETO = dto_Horario[ind].NOMBRE + " " + dto_Horario[0].SEGUNDO_NOMBRE + " " + dto_Horario[0].APELLIDO_PATERNO + " " + dto_Horario[0].APELLIDO_MATERNO;
                                carga.TIPO_CONTRATO = dto_Horario[ind].TIPO_CONTRATO;
                                list.Add(carga);
                            }
                            else
                            {

                                carga.FECHA_INICIO = DateTime.Now.ToString("dd/MM/yyyy HH:mm").Split(' ')[0];

                                carga.HORA_INICIO = "--:--";
                                carga.RUT = "";
                                carga.NOMBRE_TERMINAL = "";
                                carga.FECHA_HORA_INICIO = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                                carga.ID_TERMINAL = 0;
                                carga.NOMBRE_COMPLETO = "";

                                list.Add(carga);
                            }
                            ind++;
                        }
                    }

                    if (list.Count > 0)
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
                            ErrorMsg = "No existen datos para <b> mostrar</b>"
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        EnableError = true,
                        ErrorTitle = "Error",
                        ErrorMsg = "El R.U.N. ingresado <b>no es válido</b>"
                    });
                }
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

    }
}
