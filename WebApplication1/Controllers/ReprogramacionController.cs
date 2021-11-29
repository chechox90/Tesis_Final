using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ConductorEnRed.Models;
using DLL.DTO.CargaHorario;
using DLL.DTO.Seguridad;
using DLL.DTO.Terminales;
using DLL.NEGOCIO.Operaciones.Interfaces;
using DLL.NEGOCIO.Seguridad.Interfaces;
using WebApplication1.Models.Commons;

namespace WebApplication1.Controllers
{
    public class ReprogramacionController : Controller
    {
        OleDbConnection Econ;


        private readonly I_N_HorarioConductor _i_n_HorarioConductor;
        private readonly I_N_Terminal _i_n_Terminal;
        private readonly I_N_Usuario _i_n_usuario;

        public ReprogramacionController(I_N_HorarioConductor i_n_HorarioConductor,
            I_N_Terminal i_n_Terminal,
            I_N_Usuario i_n_usuario)
        {
            this._i_n_HorarioConductor = i_n_HorarioConductor;
            this._i_n_Terminal = i_n_Terminal;
            this._i_n_usuario = i_n_usuario;

        }

        DTO_Usuario usuario = FrontUser.Get();

        [HttpPost]
        public ActionResult GetConductorHorario(string RutConductor, string fechasConsultas)
        {
            try
            {                
                RutConductor = AgregarGuionRut(RutConductor);

                if (ValidarRutCompleto(RutConductor))
                {
                    DateTime fechaIni = DateTime.Parse(fechasConsultas.Split('-')[0].Trim() + " 00:00");
                    DateTime fechaFin = DateTime.Parse(fechasConsultas.Split('-')[1].Trim() + " 23:59");

                    List<DTO_HorarioConductorMostrar> dto_Horario = _i_n_HorarioConductor.GetHorarioConductorByRutAll(RutConductor, fechaIni, fechaFin);
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
                                carga.FECHA_INICIO = dto_Horario[ind].FECHA_HORA_INICIO.ToString().Split(' ')[0];

                                carga.HORA_INICIO = dto_Horario[ind].FECHA_HORA_INICIO.ToString("dd/MM/yyyy HH:mm").Split(' ')[1];
                                carga.RUT = IngresarPuntosEnRut(dto_Horario[ind].RUT);

                                carga.FECHA_HORA_INICIO = dto_Horario[ind].FECHA_HORA_INICIO;
                                carga.ID_TERMINAL = dto_Horario[ind].ID_TERMINAL;
                                carga.NOMBRE_COMPLETO = dto_Horario[ind].NOMBRE + " " + dto_Horario[0].SEGUNDO_NOMBRE + " " + dto_Horario[0].APELLIDO_PATERNO + " " + dto_Horario[0].APELLIDO_MATERNO;
                                carga.TIPO_CONTRATO = dto_Horario[ind].TIPO_CONTRATO;
                                carga.HORARIO_CUBIERTO = dto_Horario[ind].HORARIO_CUBIERTO;
                                list.Add(carga);
                            }
                            else
                            {
                                carga.ID_HORARIO = 0;
                                carga.FECHA_INICIO = DateTime.Now.ToString("dd/MM/yyyy HH:mm").Split(' ')[0];
                                carga.HORA_INICIO = "";
                                carga.RUT = "";

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
                            ErrorMsg = "NO se ha cargado el horario aún para la semana seleccionada"
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
                    ErrorMsg = "Ups! tenemos una incidencia al buscar este R.U.N., por favor intenta nuevamente"
                });
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
        public ActionResult GetCargarTunosDisponiblesCmb()
        {
            try
            {
                string Rutusuario = usuario.RUT;
                DateTime fechaHoy = DateTime.Now;

                DateTime fechaIni = DateTime.Parse(fechaHoy.ToString().Split(' ')[0].Trim() + " 00:00");
                DateTime fechaFin = DateTime.Parse(fechaHoy.ToString().Split(' ')[0].Trim() + " 23:59");

                List<DTO_HorarioConductorMostrar> dto_Horario = _i_n_HorarioConductor.GetHorarioConductorByRut(Rutusuario, fechaIni, fechaFin);
                List<DTO_HorarioConductorMostrar> list = new List<DTO_HorarioConductorMostrar>();

                

                if (dto_Horario.Count > 0)
                {
                    //SELECCIONE
                    DTO_HorarioConductorMostrar cargaInicial = new DTO_HorarioConductorMostrar();
                    cargaInicial.ID_HORARIO = 0;
                    cargaInicial.HORA_INICIO = "Seleccione";
                    list.Add(cargaInicial);

                    for (int i = 0; i < dto_Horario.Count; i++)
                    {
                        DTO_HorarioConductorMostrar carga = new DTO_HorarioConductorMostrar();
                        if (i <= dto_Horario.Count)
                        {
                            carga.ID_HORARIO = dto_Horario[i].ID_HORARIO;
                            carga.HORA_INICIO = dto_Horario[i].FECHA_HORA_INICIO.ToString("dd/MM/yyyy HH:mm").Split(' ')[1].Trim();
                            list.Add(carga);
                        }
                    }
                }
                else
                {
                    //No existen Datos
                    DTO_HorarioConductorMostrar cargaInicial = new DTO_HorarioConductorMostrar();
                    cargaInicial.ID_HORARIO = 0;
                    cargaInicial.HORA_INICIO = "No existen datos disponibles";
                    list.Add(cargaInicial);
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

        public ActionResult CargaHorarioConductor()
        {
            try
            {
                string NombreArchivo = Request.Form["NombreCarga"];
                string FechaCarga = Request.Form["FechaCarga"];
                string comentario = Request.Form["Comenatrio"];

                string mensajeError = "";

                if (Request.Files.Count > 0)
                {
                    HttpFileCollectionBase files = Request.Files;
                    HttpPostedFileBase file = files[0];
                    string filename = file.FileName;
                    string filepath = "/ExcelFolder/" + filename;
                    file.SaveAs(Path.Combine(Server.MapPath("/excelfolder"), filename));

                    DataTable resultadoTabla = InsertExceldata(filepath, filename);
                    List<CargaArchivoModel> list = new List<CargaArchivoModel>();
                    for (int i = 1; i < resultadoTabla.Rows.Count; i++)
                    {
                        CargaArchivoModel carga = new CargaArchivoModel();

                        string rutString = resultadoTabla.Rows[i][0].ToString().Replace(".", "");
                        int sinGuion = int.Parse(rutString.Split('-')[0]);
                        string guion = resultadoTabla.Rows[i][0].ToString().Split('-')[1];

                        if (!ValidarRutCompleto(rutString))
                        {
                            mensajeError = "Se ha detecato que la fila " + (i + 1) + " no contiene un formato valido de R.U.N.";
                            break;
                        }
                        else if (!Digito(sinGuion).ToUpper().Equals(guion.ToUpper()))
                        {
                            mensajeError = "Por favor revise la fila " + (i + 1) + " el dígito verificador no es correcto";
                            break;
                        }
                        else
                        {
                            carga.RUT = IngresarPuntosEnRut(rutString);

                        }

                        carga.NOMBRE = resultadoTabla.Rows[i][1].ToString();
                        carga.APELLIDO = resultadoTabla.Rows[i][2].ToString();
                        carga.NOMBRE_TERMINAL = resultadoTabla.Rows[i][3].ToString();

                        carga.BUS_INICIO = resultadoTabla.Rows[i][4].ToString();
                        carga.NOMBRE_JORNADA = resultadoTabla.Rows[i][5].ToString();
                        carga.FECHA_INICIO = resultadoTabla.Rows[i][6].ToString();
                        carga.HORA_INICIO = resultadoTabla.Rows[i][7].ToString();

                        list.Add(carga);
                    }

                    list = list.OrderBy(x => x.FECHA_HORA_INICIO).ToList();

                    if (mensajeError != "")
                    {
                        return Json(new
                        {
                            EnableError = true,
                            ErrorTitle = "Error",
                            ErrorMsg = mensajeError
                        });
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
                            ErrorMsg = "Error en la <b>carga de datos</b>"
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        EnableError = true,
                        ErrorTitle = "Error",
                        ErrorMsg = "Error en la <b>carga de datos</b>"
                    });
                }
            }
            catch (Exception)
            {
                return Json(new
                {
                    EnableError = true,
                    ErrorTitle = "Error",
                    ErrorMsg = "Error en la <b>carga de datos</b>"
                });
            }
        }

        public static string AgregarGuionRut(string rut)
        {
            string rutConGuion = "";
            rut = rut.Replace(".", "");
            if (!rut.Contains("-"))
                rutConGuion = rut = rut.Substring(0, rut.Length - 1) + "-" + rut.Substring(rut.Length - 1, 1);
            else
                rutConGuion = rut;

            return rutConGuion;
        }

        public static bool ValidarRutCompleto(string rut)
        {

            Regex expresion = new Regex("^([0-9]+-[0-9K])$");
            string dv = rut.Substring(rut.Length - 1, 1);

            if (!expresion.IsMatch(rut))
            {
                return false;
            }
            char[] charCorte = { '-' };
            string[] rutTemp = rut.Split(charCorte);
            if (dv != Digito(int.Parse(rutTemp[0])))
            {
                return false;
            }
            return true;
        }

        public static string Digito(int rut)
        {
            int suma = 0;
            int multiplicador = 1;
            while (rut != 0)
            {
                multiplicador++;
                if (multiplicador == 8)
                    multiplicador = 2;
                suma += (rut % 10) * multiplicador;
                rut = rut / 10;
            }
            suma = 11 - (suma % 11);
            if (suma == 11)
            {
                return "0";
            }
            else if (suma == 10)
            {
                return "K";
            }
            else
            {
                return suma.ToString();
            }
        }

        private void ExcelConn(string filepath)
        {
            string constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=NO;IMEX=1;""", filepath);
            Econ = new OleDbConnection(constr);
        }

        private DataTable InsertExceldata(string fileepath, string filename)
        {
            try
            {
                string fullpath = Server.MapPath("/excelfolder/") + filename;
                ExcelConn(fullpath);
                string query = string.Format("Select * from [{0}]", "Hoja1$");
                OleDbCommand Ecom = new OleDbCommand(query, Econ);
                Econ.Open();
                DataSet ds = new DataSet();
                OleDbDataAdapter oda = new OleDbDataAdapter(query, Econ);
                Econ.Close();
                oda.Fill(ds);
                DataTable dt = ds.Tables[0];

                return dt;

            }
            catch (Exception )
            {

                throw;
            }

        }

        [HttpPost]
        public ActionResult ObtenerFechaSemanas()
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
            DateTime fechaSemActDomingo = fechaSemActLunnes.AddDays(dia * -1);
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

        [HttpPost]
        public ActionResult SetGuardarHorarioConductor()
        {
            try
            {
                string NombreArchivo = Request.Form["NombreCarga"];
                string FechaCarga = Request.Form["FechaCarga"];
                string comentario = Request.Form["Comenatrio"];

                if (Request.Files.Count > 0)
                {
                    HttpFileCollectionBase files = Request.Files;
                    HttpPostedFileBase file = files[0];
                    string filename = file.FileName;
                    string filepath = "/ExcelFolder/" + filename;
                    file.SaveAs(Path.Combine(Server.MapPath("/excelfolder"), filename));

                    DateTime FechaHoy = DateTime.Now;

                    DataTable resultadoTabla = InsertExceldata(filepath, filename);
                    List<DTO_CargarHorarioConductor> list = new List<DTO_CargarHorarioConductor>();
                    for (int i = 1; i < resultadoTabla.Rows.Count; i++)
                    {
                        DTO_CargarHorarioConductor carga = new DTO_CargarHorarioConductor();

                        string rutString = resultadoTabla.Rows[i][0].ToString().Replace(".", "");
                        int sinGuion = int.Parse(rutString.Split('-')[0]);
                        string guion = resultadoTabla.Rows[i][0].ToString().Split('-')[1];
                        if (ValidarRutCompleto(rutString))
                            carga.ID_CONDUCTOR = _i_n_usuario.GetUsuarioByRut(rutString);

                        carga.TERMINAL_INICIO = _i_n_Terminal.GetTerminalByNombre(resultadoTabla.Rows[i][3].ToString(), 1);

                        if (resultadoTabla.Rows[i][4].ToString() != "")
                            carga.BUS_INICIO = int.Parse(resultadoTabla.Rows[i][4].ToString().Trim());
                        else
                            carga.BUS_INICIO = 0;

                        if (resultadoTabla.Rows[i][5].ToString().ToUpper().Equals("UNO") || resultadoTabla.Rows[i][5].ToString().ToLower().Equals("uno"))
                            carga.NUMERO_JORNADA = 1;
                        else
                            carga.NUMERO_JORNADA = 2;

                        carga.FECHA_HORA_INICIO = DateTime.Parse(resultadoTabla.Rows[i][6].ToString() + " " + resultadoTabla.Rows[i][7].ToString());

                        list.Add(carga);
                    }

                    string resultado = _i_n_HorarioConductor.SetGuardarHorarioConductor(list,usuario.ID_USUARIO, NombreArchivo, DateTime.Parse(FechaCarga), comentario);


                    if (resultado != "")
                    {
                        return Json(new
                        {
                            EnableError = false,
                            ErrorTitle = "",
                            ErrorMsg = resultado
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            EnableError = true,
                            ErrorTitle = "Error",
                            ErrorMsg = "Error al <b>guardar los datos</b>"
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        EnableError = true,
                        ErrorTitle = "Error",
                        ErrorMsg = "Error en la <b>carga de datos</b>"
                    });
                }
            }
            catch (Exception)
            {
                return Json(new
                {
                    EnableError = true,
                    ErrorTitle = "Error",
                    ErrorMsg = "Error en la <b>carga de datos</b>"
                });
            }
        }

        [HttpPost]
        public ActionResult SetGuaradarCambioHorario(List<CargaHorarioModel> ObjetoHorario)
        {
            List<DTO_HorarioConductorMostrar> dto_horario_guardar = new List<DTO_HorarioConductorMostrar>();

            foreach (var item in ObjetoHorario)
            {
                if (item.ID_HORARIO != 0)
                {
                    DTO_HorarioConductorMostrar dto_horario = new DTO_HorarioConductorMostrar();
                    dto_horario.ID_HORARIO = item.ID_HORARIO;
                    dto_horario.ID_TERMINAL = item.ID_TERMINAL;
                    dto_horario.HORA_INICIO = item.HORA_INICIO;
                    dto_horario.COMENTARIO = item.COMENTARIO;

                    dto_horario_guardar.Add(dto_horario);
                }

            }

            var resultado = _i_n_HorarioConductor.SetEditarHorarioConductor(dto_horario_guardar);

            return Json(new
            {
                EnableError = false,
                ErrorTitle = "",
                ErrorMsg = resultado
            });
        }

    }
}
