using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ConductorEnRed.Models;
using DLL.DTO.CargaHorario;
using DLL.DTO.Terminales;
using DLL.NEGOCIO.Operaciones.Interfaces;
using DLL.NEGOCIO.Seguridad.Interfaces;

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

        public ActionResult Index()
        {
            return View("~/Views/Programacion/CargarProgramacion.cshtml");
        }


        public ActionResult ModificarReprogramacion(string id)
        {
            return View("~/Views/Programacion/Reprogramacion.cshtml");
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult GetConductorHorario(string RutConductor)
        {
            try
            {
                if (ValidaRut(RutConductor))
                {
                    List<DTO_HorarioConductorMostrar> dto_Horario = _i_n_HorarioConductor.GetHorarioConductorByRut(RutConductor);
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
                                carga.RUT = dto_Horario[ind].RUT;

                                carga.FECHA_HORA_INICIO = dto_Horario[ind].FECHA_HORA_INICIO;
                                carga.ID_TERMINAL = dto_Horario[ind].ID_TERMINAL;
                                carga.NOMBRE_COMPLETO = dto_Horario[ind].NOMBRE + " " + dto_Horario[0].SEGUNDO_NOMBRE + " " + dto_Horario[0].APELLIDO_PATERNO + " " + dto_Horario[0].APELLIDO_MATERNO;
                                list.Add(carga);
                            }
                            else
                            {

                                carga.FECHA_INICIO = DateTime.Now.ToString("dd/MM/yyyy HH:mm").Split(' ')[0];

                                carga.HORA_INICIO = "--:--";
                                carga.RUT = "";

                                carga.FECHA_HORA_INICIO = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                                carga.ID_TERMINAL = 0;
                                carga.NOMBRE_COMPLETO = "";

                                list.Add(carga);
                            }
                            ind++;
                        }
                    }
                    
                    list = list.OrderByDescending(x => x.FECHA_HORA_INICIO).ToList();

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
                        ErrorMsg = "El R.U.N. ingresado <b>no es válido</b>"
                    });
                }
            }
            catch (Exception ex)
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

                        if (!ValidaRut(rutString))
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
                            carga.RUT = rutString;

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
            catch (Exception ex)
            {
                return Json(new
                {
                    EnableError = true,
                    ErrorTitle = "Error",
                    ErrorMsg = "Error en la <b>carga de datos</b>"
                });
            }
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

        public static bool ValidaRut(string rut)
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
            catch (Exception ex)
            {

                throw;
            }

        }

        public ActionResult ObtenerFechaLunes(string FECHA)
        {
            DateTime fechaHoy = DateTime.Now;

            int dia = Convert.ToInt32(fechaHoy.DayOfWeek);
            dia = dia - 1;
            DateTime fechaInicioSemana = fechaHoy.AddDays((dia) * (-1));

            return Json(new
            {
                data = fechaInicioSemana.ToString(),
                ErrorMsg = "",
                JsonRequestBehavior.AllowGet
            });
        }


        [HttpPost]
        public ActionResult SetGuardarHorarioConductor()
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

                    DateTime FechaHoy = DateTime.Now;

                    DataTable resultadoTabla = InsertExceldata(filepath, filename);
                    List<DTO_CargarHorarioConductor> list = new List<DTO_CargarHorarioConductor>();
                    for (int i = 1; i < resultadoTabla.Rows.Count; i++)
                    {
                        DTO_CargarHorarioConductor carga = new DTO_CargarHorarioConductor();

                        string rutString = resultadoTabla.Rows[i][0].ToString().Replace(".", "");
                        int sinGuion = int.Parse(rutString.Split('-')[0]);
                        string guion = resultadoTabla.Rows[i][0].ToString().Split('-')[1];
                        if (ValidaRut(rutString))
                            carga.ID_CONDUCTOR = _i_n_usuario.GetUsuarioByRut(rutString);

                        carga.TERMINAL_INICIO = _i_n_Terminal.GetTerminalByNombre(resultadoTabla.Rows[i][3].ToString(), 1);

                        if (resultadoTabla.Rows[i][4].ToString() != "")
                            carga.BUS_INICIO = int.Parse(resultadoTabla.Rows[i][4].ToString().Trim());
                        else
                            carga.BUS_INICIO = 0;

                        if (resultadoTabla.Rows[i][5].ToString().ToUpper().Equals("UNO"))
                            carga.NUMERO_JORNADA = 1;
                        else
                            carga.NUMERO_JORNADA = 1;

                        carga.FECHA_HORA_INICIO = DateTime.Parse(resultadoTabla.Rows[i][6].ToString() + " " + resultadoTabla.Rows[i][7].ToString());

                        list.Add(carga);
                    }

                    list = list.OrderBy(x => x.FECHA_HORA_INICIO).ToList();

                    string resultado = _i_n_HorarioConductor.SetGuardarHorarioConductor(list, NombreArchivo, DateTime.Parse(FechaCarga), comentario);


                    if (resultado != "")
                    {
                        return Json(new
                        {
                            EnableError = false,
                            ErrorTitle = "Correcto",
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
            catch (Exception ex)
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
        public ActionResult SetGuaradarCambioHorario(List<DTO_HorarioReprogramado> ObjetoHorario)
        {
            var ObHor = ObjetoHorario;

            return Json(new
            {
                EnableError = false,
                ErrorTitle = "Correcto",
                ErrorMsg = ""
            });
        }

    }
}
