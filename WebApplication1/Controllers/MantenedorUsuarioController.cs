using ConductorEnRed.ViewModels.Administracion;
using DLL.DTO.Seguridad;
using DLL.NEGOCIO.Seguridad.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Models.Commons;

namespace ConductorEnRed.Controllers
{
    public class MantenedorUsuarioController : Controller
    {
        private readonly I_N_Usuario _i_n_Usuario;

        DTO_Usuario usuario = FrontUser.Get();

        public MantenedorUsuarioController(I_N_Usuario i_n_usuario)
        {
            this._i_n_Usuario = i_n_usuario;
        }

        [HttpPost]
        public ActionResult GetUsuarios()
        {
            try
            {
                List<DTO_Usuario> DtoUsuario = new List<DTO_Usuario>();
                DtoUsuario = _i_n_Usuario.GetAllUsuariosActivos();

                List<DTO_UsuarioListar> dtoUserList = new List<DTO_UsuarioListar>();


                foreach (var item in DtoUsuario)
                {
                    DTO_UsuarioListar dtoUser = new DTO_UsuarioListar();
                    dtoUser.ID_USUARIO = item.ID_USUARIO;
                    dtoUser.RUT = item.RUT;
                    dtoUser.NOMBRE = item.NOMBRE;
                    dtoUser.SEGUNDO_NOMBRE = item.SEGUNDO_NOMBRE;
                    dtoUser.APELLIDO_PATERNO = item.APELLIDO_PATERNO;
                    dtoUser.APELLIDO_MATERNO = item.APELLIDO_MATERNO;
                    dtoUser.CORREO = item.CORREO;
                    dtoUser.CORREO_ALTERNATIVO = item.CORREO_ALTERNATIVO;
                    dtoUser.CAMBIO_PASSWORD = item.CAMBIO_PASSWORD;
                    dtoUser.NOMBRE_PERFIL = item.Perfiles.Select(x => x.Nombre).FirstOrDefault();
                    dtoUser.NOMBRE_EMPRESA = "";
                    dtoUser.DIRECCION = "";
                    dtoUser.NOMBRE_CONTRATO = "";
                    dtoUser.CODIGO_BARRA = item.CODIGO_BARRA;
                    dtoUser.ESTADO = item.ESTADO;

                    dtoUserList.Add(dtoUser);

                }




                if (dtoUserList != null)
                {
                    return Json(new { data = dtoUserList, });
                }
                else
                {
                    return Json(new
                    {
                        EnableError = true,
                        ErrorTitle = "Error",
                        ErrorMsg = "Ha ocurrido una insidencia al <b>obtener la lista de usuario</b>"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult SetEliminarUsuario(int idUsuario)
        {
            try
            {
                int response = _i_n_Usuario.SetEliminarUsuario(idUsuario);
                string alert = "";

                if (response == 1)
                {
                    alert = "success";
                    var message = "El Usuario ha sido eliminado con éxito";
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

        [HttpGet]
        public ActionResult GetEditarUsuarioActivo(int idUsuario)
        {
            DTO_UsuarioListar DtoUsuario = new DTO_UsuarioListar();
            DtoUsuario = _i_n_Usuario.GetUsuarioActivo(idUsuario);

            VM_Usuario user = new VM_Usuario();
            user.ID_USUARIO = idUsuario;
            user.RUT = DtoUsuario.RUT;
            user.NOMBRE = DtoUsuario.NOMBRE;
            user.SEGUNDO_NOMBRE = DtoUsuario.SEGUNDO_NOMBRE;
            user.APELLIDO_PATERNO = DtoUsuario.APELLIDO_PATERNO;
            user.APELLIDO_MATERNO = DtoUsuario.APELLIDO_MATERNO;
            user.CORREO = DtoUsuario.CORREO;
            user.CORREO_ALTERNATIVO = DtoUsuario.CORREO_ALTERNATIVO;
            user.CAMBIO_PASSWORD = DtoUsuario.CAMBIO_PASSWORD;
            user.NOMBRE_PERFIL = DtoUsuario.NOMBRE_PERFIL;
            user.NOMBRE_EMPRESA = "";
            user.DIRECCION = "";
            user.NOMBRE_CONTRATO = "";
            user.CODIGO_BARRA = DtoUsuario.CODIGO_BARRA;
            user.ESTADO = DtoUsuario.ESTADO;

            return View("~/Views/Administracion/EditarUsuario.cshtml", user);

        }

        [HttpPost]
        public ActionResult GetTipoContratoCmb()
        {
            try
            {
                List<DTO_TipoContrato> list = new List<DTO_TipoContrato>();
                List<DTO_TipoContrato> tipo = new List<DTO_TipoContrato>();
                tipo = _i_n_Usuario.GetTipoContratoCmb();

                DTO_TipoContrato cargaTipo = new DTO_TipoContrato();
                cargaTipo.ID_TIPO_CONTRATO = 0;
                cargaTipo.NOMBRE_TIPO_CONTRATO = "Seleccione";

                list.Add(cargaTipo);

                foreach (var item in tipo)
                {
                    DTO_TipoContrato carga = new DTO_TipoContrato();
                    carga.ID_TIPO_CONTRATO = item.ID_TIPO_CONTRATO;
                    carga.NOMBRE_TIPO_CONTRATO = item.NOMBRE_TIPO_CONTRATO;

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
        public ActionResult GetPerfilCmb()
        {
            try
            {
                List<DTO_Perfil> list = new List<DTO_Perfil>();
                List<DTO_Perfil> tipo = new List<DTO_Perfil>();
                tipo = _i_n_Usuario.GetPerfilCmb();

                DTO_Perfil cargaTipo = new DTO_Perfil();
                cargaTipo.IdPerfil = 0;
                cargaTipo.Nombre = "Seleccione";

                list.Add(cargaTipo);

                foreach (var item in tipo)
                {
                    DTO_Perfil carga = new DTO_Perfil();
                    carga.IdPerfil = item.IdPerfil;
                    carga.Nombre = item.Nombre;

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
        public ActionResult SetIngresaNuevoUsuario(List<DTO_Usuario> UsuarioNuevo)
        {
            try
            {
                string alert = "";
                DTO_Usuario newReg = new DTO_Usuario()
                {
                    ID_EMPRESA = 1,
                    ID_COMUNA = UsuarioNuevo[0].ID_COMUNA,
                    ID_TIPO_CONTRATO = UsuarioNuevo[0].ID_TIPO_CONTRATO,
                    CLAVE = HashHelper.MD5(UsuarioNuevo[0].CLAVE),
                    RUT = UsuarioNuevo[0].RUT,
                    NOMBRE = UsuarioNuevo[0].NOMBRE,
                    SEGUNDO_NOMBRE = UsuarioNuevo[0].SEGUNDO_NOMBRE,
                    APELLIDO_PATERNO = UsuarioNuevo[0].APELLIDO_PATERNO,
                    APELLIDO_MATERNO = UsuarioNuevo[0].APELLIDO_MATERNO,
                    CODIGO_BARRA = UsuarioNuevo[0].CODIGO_BARRA,
                    CORREO = UsuarioNuevo[0].CORREO,
                    CORREO_ALTERNATIVO = UsuarioNuevo[0].CORREO_ALTERNATIVO,
                    CAMBIO_PASSWORD = UsuarioNuevo[0].CAMBIO_PASSWORD,
                    ADMINISTRADOR = UsuarioNuevo[0].ADMINISTRADOR,
                    ID_PERFIL = UsuarioNuevo[0].ID_PERFIL,
                    ESTADO = true

                };

                int response = _i_n_Usuario.SetIngresaNuevoUsuario(newReg);

                if (response == 1)
                {

                    alert = "success";
                    var message = "El usuario ha sido registrado con éxito";
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
        public ActionResult SetEditaUsuario(List<DTO_Usuario> Usuario)
        {
            try
            {
                string alert = "";
                DTO_Usuario newReg = new DTO_Usuario()
                {
                    ID_EMPRESA = 1,
                    ID_USUARIO = Usuario[0].ID_USUARIO,
                    ID_COMUNA = Usuario[0].ID_COMUNA,
                    ID_TIPO_CONTRATO = Usuario[0].ID_TIPO_CONTRATO,
                    CLAVE = HashHelper.MD5(Usuario[0].CLAVE),
                    RUT = Usuario[0].RUT,
                    NOMBRE = Usuario[0].NOMBRE,
                    SEGUNDO_NOMBRE = Usuario[0].SEGUNDO_NOMBRE,
                    APELLIDO_PATERNO = Usuario[0].APELLIDO_PATERNO,
                    APELLIDO_MATERNO = Usuario[0].APELLIDO_MATERNO,
                    CODIGO_BARRA = Usuario[0].CODIGO_BARRA,
                    CORREO = Usuario[0].CORREO,
                    CORREO_ALTERNATIVO = Usuario[0].CORREO_ALTERNATIVO,
                    CAMBIO_PASSWORD = Usuario[0].CAMBIO_PASSWORD,
                    ADMINISTRADOR = Usuario[0].ADMINISTRADOR,
                    ESTADO = true

                };

                int response = _i_n_Usuario.SetEditaUsuario(newReg);

                if (response == 1)
                {

                    alert = "success";
                    var message = "El usuario ha sido editado con éxito";
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

        public ActionResult ValidarRutCompleto(string rut)
        {

            if (!rut.Contains('-'))
                rut = rut.Substring(0, rut.Length - 1) + "-" + rut.Substring(rut.Length - 1, 1);

            if (rut.Contains('.'))
                rut = rut.Replace(".", "").Trim();

            if (rut.Contains("k") || rut.Contains("K"))
                rut = rut.Replace("k", "K");

            Regex expresion = new Regex("^([0-9]+-[0-9K])$");
            string dv = rut.Substring(rut.Length - 1, 1);

            if (!expresion.IsMatch(rut))
            {
                return new JsonResult()
                {
                    Data = Json(new { data = "", message = "" })
                };
            }
            char[] charCorte = { '-' };
            string[] rutTemp = rut.Split(charCorte);
            if (dv != Digito(int.Parse(rutTemp[0])))
            {
                return new JsonResult()
                {
                    Data = Json(new { data = "El R.U.N ingresado <b>no</b> es válido, intente nuevamente.", message = "" })
                };
            }

            int runBd = _i_n_Usuario.GetUsuarioByRut(rut);
            if (runBd > 0)
            {
                return new JsonResult()
                {
                    Data = Json(new { data = "El R.U.N ingresado ya se encuentra en uso por otro <b>usuario</b>.", message = "" })
                };
            }
            rut = IngresarPuntosEnRut(rut);

            return new JsonResult()
            {
                Data = Json(new { data = rut, message = "correcto" })
            };
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

        public static string IngresarPuntosEnRut(string rut)
        {
            rut = rut.Replace("-", "");
            var primerGrupo = rut.Substring(rut.Length - 4);
            var segundoGrupo = rut.Substring(rut.Length - 7).Substring(0, 3);
            int resta = rut.Length - 7;
            var tercerGrupo = rut.Substring(0, resta);
            rut = tercerGrupo + "." + segundoGrupo + "." + primerGrupo.Substring(0, 3) + "-" + primerGrupo.Substring(3, 1);

            return rut;
        }

    }
}
