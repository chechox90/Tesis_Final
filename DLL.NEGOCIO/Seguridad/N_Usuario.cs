using DLL.DAO.Seguridad.Interfaces;
using DLL.DTO.CargaHorario;
using DLL.DTO.Seguridad;
using DLL.NEGOCIO.Seguridad.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.NEGOCIO.Seguridad
{
    public class N_Usuario : I_N_Usuario
    {
        private readonly I_DAO_Usuario I_DAO_Usuario;

        public N_Usuario(I_DAO_Usuario I_DAO_Usuario)
        {
            this.I_DAO_Usuario = I_DAO_Usuario;
        }


        public DTO_Usuario getUsuario(int rol, string nombreSistema)
        {
            return I_DAO_Usuario.getUsuario(rol, nombreSistema);
        } 
        
        public List<DTO_Perfil> GetPerfilCmb()
        {
            return I_DAO_Usuario.GetPerfilCmb();
        }

        public List<DTO_TipoContrato> GetTipoContratoCmb()
        {
            return I_DAO_Usuario.GetTipoContratoCmb();
        }

        public DTO_Usuario Autenticacion(DTO_Usuario login, string nombreSistema)
        {
            return I_DAO_Usuario.Autenticacion(login, nombreSistema);
        }

        public int GetUsuarioByRut(string rut)
        {
            return I_DAO_Usuario.GetUsuarioByRut(rut);
        }

        public List<DTO_Usuario> GetAllUsuariosActivos()
        {
            return I_DAO_Usuario.GetAllUsuariosActivos();
        }

        public DTO_UsuarioListar GetUsuarioActivo(int idUsuario)
        {
            return I_DAO_Usuario.GetUsuarioActivo(idUsuario);
        }
        
        public bool CambioPassword(int rol, string contraseniaNueva, string contraseniaNuevaNoEncriptada)
        {
            return I_DAO_Usuario.CambioPassword(rol, contraseniaNueva, contraseniaNuevaNoEncriptada);
        }

        public int SetEliminarUsuario(int idUser)
        {
            return I_DAO_Usuario.SetEliminarUsuario(idUser);
        }

        public int SetIngresaNuevoUsuario(DTO_Usuario usuario)
        {
            return I_DAO_Usuario.SetIngresaNuevoUsuario(usuario);
        }

    }

}