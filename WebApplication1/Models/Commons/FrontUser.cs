// Versión 1.1
using DLL.DAO.Seguridad;
using DLL.DTO.Seguridad;
using DLL.NEGOCIO.Seguridad;
using DLL.NEGOCIO.Seguridad.Interfaces;
using System;
using System.Configuration;
using System.Linq;
using WebApplication1.Helpers;
using System.Web;

namespace WebApplication1.Models.Commons
{
    public class FrontUser
    {
        public static bool TienePermiso(RolesPermisos valor)
        {
            int permiso = Convert.ToInt32(valor);
            DTO_Usuario usuario = FrontUser.Get();

            if (usuario == null)
                return false;
            else
            {
                SetSessionUsuario(usuario);
                if (usuario.ADMINISTRADOR)
                    return true;
            }

            if (usuario.Perfiles.Where(x => x.Acciones.Where(i => i.IDACCION == permiso).Any()).Any())
                if (usuario.PermisosEspeciales.Where(x => x.IdAccion == permiso && x.TipoPermiso == false).Any())
                    return false;
                else
                    return true;
            else
                return usuario.PermisosEspeciales.Where(x => x.IdAccion == permiso && x.TipoPermiso == true).Any();
        }

        public static DTO_Usuario Get()
        {
            I_N_Usuario i_usuario = new N_Usuario(new DAO_Usuario());
            return i_usuario.getUsuario(SessionHelper.GetUser());
        }


        public static void SetSessionUsuario(DTO_Usuario usuario)
        {
            HttpContext context = HttpContext.Current;
            context.Session["usuario"] = usuario;
        }

        public static DTO_Usuario GetSessionUsuario()
        {
            HttpContext context = HttpContext.Current;
            DTO_Usuario usuario = context.Session["usuario"] as DTO_Usuario;
            if (usuario != null)
                return usuario;
            else
            {
                usuario = FrontUser.Get();
                if (usuario != null)
                {
                    SetSessionUsuario(usuario);
                    return usuario;
                }
            }
            return usuario;
        }

    }
}