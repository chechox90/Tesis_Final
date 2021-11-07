using DLL.DAO.Seguridad.Interfaces;
using DLL.DATA.SeguridadSoluinfo;
using DLL.DTO.Seguridad;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DLL.DAO.Seguridad
{
    public class DAO_Usuario : I_DAO_Usuario
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DAO_Usuario()
        {
            XmlConfigurator.Configure();
        }

        public DTO_Usuario getUsuario(int rol)
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {

                    // Obtengo datos del usuario, perfiles y permisos
                    DTO_Usuario usuario = context.USUARIOS_SISTEMA
                        .Select(x => new DTO_Usuario
                        {
                            ID_USUARIO = x.ID_USUARIO,
                            RUT = x.RUT,
                            NOMBRE = x.NOMBRE,
                            APELLIDO_PATERNO = x.APELLIDO_PATERNO,
                            APELLIDO_MATERNO = x.APELLIDO_MATERNO,
                            CORREO = x.CORREO,
                            ADMINISTRADOR = x.ADMINISTRADOR,
                            ESTADO = x.ESTADO,
                            Perfiles = x.PERFILES.Select(i => new DTO_Perfil
                            {
                                IdPerfil = i.ID_PERFIL,
                                Nombre = i.NOMBRE,
                                Estado = i.ESTADO,
                                Acciones = i.ACCIONES.Select(o => new DTO_Accion
                                {
                                    IDACCION = o.ID_ACCION,
                                    Nombre = o.NOMBRE,
                                    ItemMenu = o.ITEM_MENU,
                                    Estado = o.ESTADO,
                                    IdMenu = o.MENU.ID_MENU,
                                    IdProyecto = o.MENU.PROYECTOS.ID_PROYECTO
                                })
                                .Where(o => o.Estado == true)
                                .ToList()
                            })
                            .Where(i => i.Estado == true)
                            .ToList(),
                            PermisosEspeciales = x.PERMISOS_ESPECIALES.Select(i => new DTO_PermisosEspeciales
                            {
                                IdUsuario = i.ID_USUARIO,
                                IdProyecto = i.ID_PROYECTO,
                                IdAccion = i.ID_ACCION,
                                IdMenu = i.ACCIONES.ID_MENU,
                                TipoPermiso = i.TIPO_PERMISO
                            })
                            .Where(i => i.IdUsuario == rol)
                            .ToList()
                        })
                        .Where(x => x.ID_USUARIO == rol && x.ESTADO == true).FirstOrDefault();


                    return usuario;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public int GetUsuarioByRut(string rut)
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {

                    // Obtengo datos del usuario, perfiles y permisos
                    int usuario = context.USUARIOS_SISTEMA
                        .Select(x => new DTO_Usuario
                        {
                            ID_USUARIO = x.ID_USUARIO,
                            RUT = x.RUT,
                            NOMBRE = x.NOMBRE,
                            APELLIDO_PATERNO = x.APELLIDO_PATERNO,
                            APELLIDO_MATERNO = x.APELLIDO_MATERNO,
                            CORREO = x.CORREO,
                            ADMINISTRADOR = x.ADMINISTRADOR,
                            ESTADO = x.ESTADO,
                        }).Where(x => x.RUT == rut && x.ESTADO == true).FirstOrDefault().ID_USUARIO;


                    return usuario;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public DTO_Usuario Autenticacion(DTO_Usuario login)
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    DTO_Proyecto proyecto = GetProyecto("ConductorEnRed");

                    //Obtengo datos del usuario, perfiles y permisos
                    DTO_Usuario usuario = context.USUARIOS_SISTEMA
                        .Select(x => new DTO_Usuario
                        {
                            ID_USUARIO = x.ID_USUARIO,
                            RUT = x.RUT,
                            NOMBRE = x.NOMBRE,
                            APELLIDO_PATERNO = x.APELLIDO_PATERNO,
                            APELLIDO_MATERNO = x.APELLIDO_MATERNO,
                            CORREO = x.CORREO,
                            ADMINISTRADOR = x.ADMINISTRADOR,
                            ESTADO = x.ESTADO,
                            CLAVE = x.CLAVE,
                            Perfiles = x.PERFILES.Select(i => new DTO_Perfil
                            {
                                IdPerfil = i.ID_PERFIL,
                                Nombre = i.NOMBRE,
                                Estado = i.ESTADO,
                                Acciones = i.ACCIONES.Select(o => new DTO_Accion
                                {
                                    IDACCION = o.ID_ACCION,
                                    Nombre = o.NOMBRE,
                                    ItemMenu = o.ITEM_MENU,
                                    Estado = o.ESTADO,
                                    IdMenu = o.MENU.ID_MENU,
                                    IdProyecto = o.MENU.PROYECTOS.ID_PROYECTO
                                })
                                .Where(o => o.Estado == true)
                                .ToList()
                            }).ToList()
                            .Where(i => i.Estado == true)
                            .ToList(),
                            PermisosEspeciales = x.PERMISOS_ESPECIALES.Select(i => new DTO_PermisosEspeciales
                            {
                                IdUsuario = i.ID_USUARIO,
                                IdProyecto = i.ID_PROYECTO,
                                IdAccion = i.ID_ACCION,
                                IdMenu = i.ACCIONES.ID_MENU,
                                TipoPermiso = i.TIPO_PERMISO
                            })
                            .Where(i => i.IdUsuario == login.ID_USUARIO)
                            .ToList()
                        })
                        .Where(x => x.RUT == login.RUT && x.ESTADO == true && x.CLAVE == login.CLAVE).FirstOrDefault();



                    usuario.Proyecto = LimpiaMenuSegunPermisos(proyecto, usuario);



                    return usuario;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public bool CambioPassword(int rol, string contraseniaNueva, string contraseniaNuevaNoEncriptada)
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    USUARIOS_SISTEMA userDB = context.USUARIOS_SISTEMA.Where(x => x.ID_USUARIO == rol).FirstOrDefault();
                    userDB.CLAVE = contraseniaNueva;
                    userDB.CAMBIO_PASSWORD = DateTime.Now;
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                return false;
                throw;
            }
        }

        private DTO_Proyecto LimpiaMenuSegunPermisos(DTO_Proyecto proyecto, DTO_Usuario usuario)
        {
            if (!usuario.ADMINISTRADOR)
            {
                for (int x = 0; x < proyecto.Menus.Count(); x++)
                {

                    if (usuario.Perfiles.Where(o => o.Acciones.Where(i => i.IdMenu == proyecto.Menus[x].IdMenu && i.ItemMenu == true).Any()).Any())
                    {
                        if (usuario.PermisosEspeciales.Where(o => o.IdMenu == proyecto.Menus[x].IdMenu && o.TipoPermiso == false).Any())
                        {
                            proyecto.Menus[x].Estado = false;
                        }
                        else
                        {
                            for (var i = 0; i < proyecto.Menus[x].MenuHijo.Count(); i++)
                            {
                                if (usuario.Perfiles.Where(o => o.Acciones.Where(u => u.IdMenu == proyecto.Menus[x].MenuHijo[i].IdMenu && u.ItemMenu == true).Any()).Any())
                                {
                                    if (usuario.PermisosEspeciales.Where(o => o.IdMenu == proyecto.Menus[x].MenuHijo[i].IdMenu && o.TipoPermiso == false).Any())
                                    {
                                        proyecto.Menus[x].MenuHijo[i].Estado = false;
                                    }
                                }
                                else
                                {
                                    if (usuario.PermisosEspeciales.Where(o => o.IdMenu == proyecto.Menus[x].MenuHijo[i].IdMenu && o.TipoPermiso == true).Any())
                                        continue;
                                    else
                                        proyecto.Menus[x].MenuHijo[i].Estado = false;
                                }
                            }
                        }

                    }
                    else
                    {
                        if (usuario.Perfiles.Where(o => o.Acciones.Where(u => u.IdMenu == proyecto.Menus[x].IdMenu && u.ItemMenu == true).Any()).Any())
                        {
                            if (usuario.PermisosEspeciales.Where(o => o.IdMenu == proyecto.Menus[x].IdMenu && o.TipoPermiso == false).Any())
                            {
                                proyecto.Menus[x].Estado = false;
                            }
                        }
                        else
                        {
                            if (usuario.PermisosEspeciales.Where(o => o.IdMenu == proyecto.Menus[x].IdMenu && o.TipoPermiso == true).Any())
                                continue;
                            else
                                proyecto.Menus[x].Estado = false;
                        }
                    }
                }
            }

            proyecto.Menus = proyecto.Menus.Select(i => new DTO_Menu
            {
                IdMenu = i.IdMenu,
                IdPadre = i.IdPadre,
                Orden = i.Orden,
                Nombre = i.Nombre,
                Controlador = i.Controlador,
                IconoUrl = i.IconoUrl,
                URL = i.URL,
                Estado = i.Estado,
                //MenuHijo = i.MenuHijo.Select(o => new DTO_MenuSubMenu
                //{
                //    IdMenu = o.IdMenu,
                //    IdPadre = o.IdPadre,
                //    Orden = o.Orden,
                //    Nombre = o.Nombre,
                //    Controlador = o.Controlador,
                //    IconoUrl = o.IconoUrl,
                //    URL = o.URL,
                //    Estado = o.Estado
                //})
                //.Where(o => o.Estado == true)
                //.ToList()
            })
            .Where(i => i.Estado == true && i.IdPadre == null)
            .ToList();

            return proyecto;
        }

        private DTO_Proyecto GetProyecto(string nombreSistema)
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {
                    // Obtengo datos del proyecto
                    return context.PROYECTOS
                    .Select(x => new DTO_Proyecto
                    {
                        IdProyecto = x.ID_PROYECTO,
                        NombreInterno = x.NOMBRE_INTERNO,
                        NombreVisible = x.NOMBRE_VISIBLE,
                        UrlProyecto = x.URL_PROYECTO,
                        IconUno = x.ICON_UNO,
                        IconDos = x.ICON_DOS,
                        FechaVersion = x.FECHA_VERSION,
                        Version = x.VERSION,
                        Novedades = x.NOVEDADES.Select(i => new DTO_Novedades
                        {
                            Orden = i.ORDEN,
                            Titulo = i.TITULO,
                            Descripcion = i.DESCRIPCION,
                            IdTipoNovedad = i.ID_TIPO_NOVEDAD,
                            TipoNovedad = new DTO_TIPO_NOVEDAD
                            {
                                Id = i.TIPOS_NOVEDADES.ID_TIPO_NOVEDAD,
                                Descripcion = i.TIPOS_NOVEDADES.DESCRIPCION,
                                Color = i.TIPOS_NOVEDADES.COLOR
                            }
                        })
                        .ToList(),
                        PaginaInicio = x.PAGINA_INICIO,
                        Estado = x.ESTADO,
                        Menus = x.MENU.Select(i => new DTO_Menu
                        {
                            IdMenu = i.ID_MENU,
                            IdPadre = i.ID_PADRE,
                            Orden = i.ORDEN,
                            Nombre = i.NOMBRE,
                            Controlador = i.CONTROLADOR,
                            IconoUrl = i.ICONO_URL,
                            URL = i.URL,
                            Estado = i.ESTADO,
                            //MenuHijo = i.MENU1.Select(o => new DTO_MenuSubMenu
                            //{
                            //    IdMenu = o.ID_MENU,
                            //    IdPadre = o.ID_PADRE,
                            //    Orden = o.ORDEN,
                            //    Nombre = o.NOMBRE,
                            //    Controlador = o.CONTROLADOR,
                            //    IconoUrl = o.ICONO_URL,
                            //    URL = o.URL,
                            //    Estado = o.ESTADO
                            //})
                            //.Where(o => o.Estado == true)
                            //.ToList()
                        })
                        .Where(i => i.Estado == true && i.IdPadre == null)
                        .ToList()
                    })
                    .Where(x => x.NombreInterno.Equals(nombreSistema) && x.Estado == true)
                    .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public List<DTO_Usuario> GetAllUsuariosActivos()
        {
            try
            {
                using (SolusegEntities context = new SolusegEntities())
                {

                    // Obtengo datos del usuario, perfiles y permisos
                    List<DTO_Usuario> usuario = context.USUARIOS_SISTEMA
                        .Select(x => new DTO_Usuario
                        {
                            ID_USUARIO = x.ID_USUARIO,
                            ID_EMPRESA = x.ID_EMPRESA,
                            RUT = x.RUT,
                            NOMBRE = x.NOMBRE,
                            SEGUNDO_NOMBRE = x.SEGUNDO_NOMBRE,
                            APELLIDO_PATERNO = x.APELLIDO_PATERNO,
                            APELLIDO_MATERNO = x.APELLIDO_MATERNO,
                            CODIGO_BARRA = x.CODIGO_BARRA,
                            CORREO = x.CORREO,
                            ADMINISTRADOR = x.ADMINISTRADOR,
                            ESTADO = x.ESTADO,
                            Perfiles = x.PERFILES.Select(i => new DTO_Perfil
                            {
                                IdPerfil = i.ID_PERFIL,
                                Nombre = i.NOMBRE,
                                Estado = i.ESTADO,
                                Acciones = i.ACCIONES.Select(o => new DTO_Accion
                                {
                                    IDACCION = o.ID_ACCION,
                                    Nombre = o.NOMBRE,
                                    ItemMenu = o.ITEM_MENU,
                                    Estado = o.ESTADO,
                                    IdMenu = o.MENU.ID_MENU,
                                    IdProyecto = o.MENU.PROYECTOS.ID_PROYECTO
                                })
                                .Where(o => o.Estado == true)
                                .ToList()
                            })
                            .Where(i => i.Estado == true)
                            .ToList(),
                            PermisosEspeciales = x.PERMISOS_ESPECIALES.Select(i => new DTO_PermisosEspeciales
                            {
                                IdUsuario = i.ID_USUARIO,
                                IdProyecto = i.ID_PROYECTO,
                                IdAccion = i.ID_ACCION,
                                IdMenu = i.ACCIONES.ID_MENU,
                                TipoPermiso = i.TIPO_PERMISO
                            })
                            .Where(i => i.IdUsuario == x.ID_USUARIO)
                            .ToList()
                        })
                        .Where(x => x.ID_USUARIO == x.ID_USUARIO && x.ESTADO == true).ToList();


                    return usuario;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                throw;
            }
        }

        public int SetEliminarUsuario(int idUsuario)
        {
            try
            {
                int respuesta = 0;

                using (SolusegEntities context = new SolusegEntities())
                {
                    using (var contextTransaction = context.Database.BeginTransaction())
                    {
                        USUARIOS_SISTEMA Old = context.USUARIOS_SISTEMA.Where(x => x.ESTADO == true && x.ID_USUARIO == idUsuario).FirstOrDefault();
                        Old.ESTADO = false;
                        respuesta = context.SaveChanges();
                        contextTransaction.Commit();
                    }
                }

                return respuesta;

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
