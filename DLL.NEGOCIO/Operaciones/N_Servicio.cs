﻿using DLL.DAO.Operaciones.Interfaces;
using DLL.DTO.Mantenedor;
using DLL.NEGOCIO.Operaciones.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.NEGOCIO.Operaciones
{
    public class N_Servicio : I_N_Servicio
    {
        private readonly I_DAO_Servicio I_DAO_Servicio;
        public N_Servicio(I_DAO_Servicio I_DAO_Servicio)
        {
            this.I_DAO_Servicio = I_DAO_Servicio;
        }

        public List<DTO_Servicio> GetServicioByAllActiveForTable()
        {
            return I_DAO_Servicio.GetServicioByAllActiveForTable();
        }

        public int SetNuevoServicio(string nombreTer, string direccion, string numDire)
        {
            return I_DAO_Servicio.SetNuevoServicio(nombreTer, direccion, numDire);
        }

        public int SetEliminarServicio(int idServicio)
        {
            return I_DAO_Servicio.SetEliminarServicio(idServicio);
        }

        public int SetEditarServicio(DTO_Servicio SERVICIO)
        {
            return I_DAO_Servicio.SetEditarServicio(SERVICIO);
        }
    }
}
