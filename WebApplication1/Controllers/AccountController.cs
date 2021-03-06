// Versión 1.1

using DLL.DTO.Seguridad;
using DLL.NEGOCIO.Seguridad.Interfaces;
using System;
using System.Configuration;
using System.Web.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Models.Commons;
using WebApplication1.Tags;
using WebApplication1.ViewModels.Account;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly I_N_Usuario _i_n_usuario;

        public AccountController(I_N_Usuario i_n_usuario)
        {
            this._i_n_usuario = i_n_usuario;
        }

        [NoLoginAttribute]
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [AutenticadoAttribute]
        public ActionResult CambioPassword()
        {
            return PartialView();
        }

        [HttpPost]
        [AutenticadoAttribute]
        public ActionResult CambioPassword(CambioPasswordViewModel model)
        {
            string message = "";
            string url = "";
            string typeAlert = "";
            if (ModelState.IsValid)
            {
                DTO_Usuario usuario = new DTO_Usuario();

                string password = HashHelper.MD5(model.ClaveActual);
                usuario.RUT = FrontUser.GetSessionUsuario().RUT;
                usuario.CLAVE = password;

                usuario = _i_n_usuario.Autenticacion(usuario);

                if (usuario != null)
                {
                    if (usuario.ID_USUARIO.ToString() != model.ClaveNueva)
                    {
                        string passwordNueva = HashHelper.MD5(model.ClaveNueva);
                        bool cambioPassword = _i_n_usuario.CambioPassword(usuario.ID_USUARIO, passwordNueva, model.ClaveNueva);

                        if (cambioPassword)
                        {
                            message = @"Contraseña Cambiada exitosamente, se cerrará la sesión por seguridad";
                            typeAlert = "success";
                            SessionHelper.DestroyUserSession();
                            url = "/Account/Salir";
                        }
                        else
                        {
                            message = @"Ocurrió un problema al intentar guardar su Nueva Contraseña";
                            typeAlert = "danger";
                        }
                    }
                    else
                    {
                        message = @"<span class='text-danger field-validation-error' data-valmsg-replace='true'>
                                La contraseña nueva no puede ser igual a su Rol
                              </span>";
                    }
                }
                else
                {
                    message = @"<span class='text-danger field-validation-error' data-valmsg-replace='true'>
                                La contraseña actual ingresada no es correcta, intente nuevamente.
                              </span>";
                }

            }

            return new JsonResult()
            {
                Data = Json(new { message = message, url = url, typeAlert = typeAlert })
            };
        }

        [HttpPost]
        public ActionResult RestablecerPassword(RestablecerPasswordViewModel model)
        {
            string message = "";
            string typeAlert = "";

            if (ModelState.IsValid)
            {
                DTO_Usuario usuario = new DTO_Usuario();

                string password = HashHelper.MD5(model.RutRestablece.ToString());
                usuario.RUT = model.RutRestablece;
                usuario.CLAVE = password;

                usuario = _i_n_usuario.Autenticacion(usuario);

                if (usuario != null)
                {
                    if (usuario.RUT != model.ClaveNueva)
                    {
                        string passwordNueva = HashHelper.MD5(model.ClaveNueva);
                        bool cambioPassword = _i_n_usuario.CambioPassword(usuario.ID_USUARIO, passwordNueva, model.ClaveNueva);

                        if (cambioPassword)
                        {
                            message = @"Contraseña Cambiada exitosamente";
                            typeAlert = "success";                          
                        }
                        else
                        {
                            message = @"Ocurrió un problema al intentar guardar su Nueva Contraseña";
                            typeAlert = "danger";
                        }
                    }
                    else
                    {
                        message = @"<span class='text-danger field-validation-error' data-valmsg-replace='true'>
                                La contraseña nueva no puede ser igual a su Rol
                              </span>";
                    }
                }
                else
                {
                    message = @"<span class='text-danger field-validation-error' data-valmsg-replace='true'>
                                Este rol no tiene clave genérica
                              </span>";
                }

            }

            return new JsonResult()
            {
                Data = Json(new { message = message, typeAlert = typeAlert })
            };
        }

        [NoLoginAttribute]
        public ActionResult Autenticar(LoginViewModel model)
        {
            string message = "", url = "";
               int seccondsRerfresh = 0;
              var rm = new ResponseModel();

             bool claveGenerica = false;

            if (ModelState.IsValid)
            {
                DTO_Usuario usuario = new DTO_Usuario();

                string password = HashHelper.MD5(model.Clave);
                usuario.RUT = model.Rut;
                usuario.CLAVE = password;

                usuario = _i_n_usuario.Autenticacion(usuario);

                if (usuario != null && usuario.Perfiles.Count != 0)
                {
                    if ((usuario.ID_USUARIO).ToString() != model.Clave)
                    {
                        SessionHelper.AddUserToSession(usuario.ID_USUARIO.ToString());
                        rm.SetResponse(true);

                        FrontUser.SetSessionUsuario(usuario);
                    }
                    else
                    {
                        claveGenerica = true;
                        //message = @"clavegenerica";
                    }

                }
                else
                {
                    rm.SetResponse(false, "Acceso denegado al sistema");
                }

                if (claveGenerica)
                {
                    message = @"clavegenerica";
                }
                else
                {
                    if (rm.response)
                    {
                        message = @"ingresa";
                        string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                        url = baseUrl + usuario.Proyecto.PaginaInicio;
                    }
                    else if (message == "" && usuario == null)
                        message = @"<span class='text-danger field-validation-error' data-valmsg-replace='true'>
                                El rol o contraseña ingresada no es correcta, intente nuevamente.
                              </span>";
                    else
                    {
                        message = @"<span class='text-danger field-validation-error' data-valmsg-replace='true'>
                                Acceso denegado al sistema.
                              </span>";
                    }
                }

            }
            else
            {
                string mensaje = @"<span class='text-danger field-validation-error' data-valmsg-replace='true'>
                                    Debe llenar los campos para poder autenticarse.
                                    </span>";
                rm.SetResponse(false, mensaje);
                message = mensaje;
            }

            return new JsonResult()
            {
                Data = Json(new { message = message, url = url, seccondsRerfresh = seccondsRerfresh })
            };
        }

        [AutenticadoAttribute]
        public ActionResult Salir()
        {

            SessionHelper.DestroyUserSession();

            Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
            Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-10);

            return Redirect("~/");
        }

    }
}