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

        public bool CambioPassword(int rol, string contraseniaNueva, string contraseniaNuevaNoEncriptada)
        {
            return I_DAO_Usuario.CambioPassword(rol, contraseniaNueva, contraseniaNuevaNoEncriptada);
        }

        public DTO_Usuario getUsuario(int rol)
        {
            return I_DAO_Usuario.getUsuario(rol);
        }

        public DTO_Usuario Autenticacion(DTO_Usuario login)
        {
            return I_DAO_Usuario.Autenticacion(login);
        }

        public int GetUsuarioByRut(string rut)
        {
            return I_DAO_Usuario.GetUsuarioByRut(rut);
        }

        

    }
}
