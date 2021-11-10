﻿using ConductorEnRed.ViewModels.Administracion;
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
        public ActionResult GetUsuarioActivo(int idUsuario)
        {
            DTO_UsuarioListar DtoUsuario = new DTO_UsuarioListar();
            DtoUsuario = _i_n_Usuario.GetUsuarioActivo(idUsuario);

            VM_Usuario user = new VM_Usuario();
            user.ID_USUARIO = DtoUsuario.ID_USUARIO;
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

    }
}
