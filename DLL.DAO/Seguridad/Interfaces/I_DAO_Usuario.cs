using DLL.DTO.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DAO.Seguridad.Interfaces
{
    public interface I_DAO_Usuario
    {
        DTO_Usuario getUsuario(int idUsuario, string nombreSistema);

        List<DTO_TipoContrato> GetTipoContratoCmb();

        List<DTO_Perfil> GetPerfilCmb();

        bool CambioPassword(int idUsuario, string contraseniaNueva, string contraseniaNuevaNoEncriptada);

        DTO_Usuario Autenticacion(DTO_Usuario login, string nombreSistema);

        int GetUsuarioByRut(string rut);

        List<DTO_Usuario> GetAllUsuariosActivos();

        DTO_UsuarioListar GetUsuarioActivo(int idUsuario);

        int SetEliminarUsuario(int idUser);
        
        int SetIngresaNuevoUsuario(DTO_Usuario usuario);


    }
}
