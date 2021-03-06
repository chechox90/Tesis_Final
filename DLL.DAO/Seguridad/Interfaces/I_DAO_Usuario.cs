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
        DTO_Usuario getUsuario(int idUsuario);

        bool CambioPassword(int idUsuario, string contraseniaNueva, string contraseniaNuevaNoEncriptada);

        DTO_Usuario Autenticacion(DTO_Usuario login);

        int GetUsuarioByRut(string rut);    

    }
}
