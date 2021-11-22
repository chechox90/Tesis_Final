using DLL.DTO.CargaHorario;
using DLL.DTO.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.NEGOCIO.Seguridad.Interfaces
{
    public interface I_N_Usuario
    {
        DTO_Usuario getUsuario(int rol, string nombreSistema);

        bool CambioPassword(int rol, string contraseniaNueva, string contraseniaNuevaNoEncriptada);

        DTO_Usuario Autenticacion(DTO_Usuario login, string nombreSistema);

        int GetUsuarioByRut(string rut);

        List<DTO_Usuario> GetAllUsuariosActivos();

        int SetEliminarUsuario(int idUser);

        DTO_UsuarioListar GetUsuarioActivo(int idUsuario);
    }
}
