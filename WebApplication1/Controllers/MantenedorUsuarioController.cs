using ConductorEnRed.ViewModels.Administracion;
using DLL.DTO.Seguridad;
using DLL.NEGOCIO.Seguridad.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConductorEnRed.Controllers
{
    public class MantenedorUsuarioController : Controller
    {
        private readonly I_N_Usuario _i_n_Usuario;

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

            return View("~/Views/Administracion/EditarUsuario.cshtml",user);
           
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
                    CLAVE = UsuarioNuevo[0].CLAVE,
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
                    CLAVE = Usuario[0].CLAVE,
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
    }
}
