using System;
using System.Collections.Generic;
using DLL.DTO.Seguridad;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConductorEnRed.ViewModels.Administracion;

namespace ConductorEnRed.Controllers
{
    public class RedirectVistasController : Controller
    {
        public ActionResult HomeCargaProgramacion()
        {
            return View("~/Views/Programacion/CargarProgramacion.cshtml");
        }
        
        public ActionResult HomeModificarReprogramacion()
        {
            return View("~/Views/Programacion/Reprogramacion.cshtml");
        }

        //Horario conductor
        public ActionResult HomeVerHorario()
        {
            return View("~/Views/HorarioConductor/VerHorario.cshtml");
        }

        //Mantenedores
        public ActionResult HomeTerminales()
        {
            return View("~/Views/Mantenedores/Terminal.cshtml");
        }

        public ActionResult HomeServicios()
        {
            return View("~/Views/Mantenedores/Servicio.cshtml");
        }

        public ActionResult HomeComunas()
        {
            return View("~/Views/Mantenedores/Comuna.cshtml");
        }


        public ActionResult HomeBuses()
        {
            return View("~/Views/Mantenedores/Bus.cshtml");
        }

        public ActionResult HomeSentido()
        {
            return View("~/Views/Mantenedores/Sentido.cshtml");
        }

        public ActionResult HomeUsuario()
        {
            return View("~/Views/Administracion/ListarUsuarios.cshtml");
        }

        public ActionResult HomeEditarUsuario()
        {
            return View("~/Views/Administracion/EditarUsuario.cshtml");
        }

        public ActionResult HomeNuevoUsuario()
        {
            return View("~/Views/Administracion/Usuario.cshtml");
        }

        public ActionResult HomeSolicitudes()
        {
            return View("~/Views/HorarioConductor/Solicitudes.cshtml");
        }

        public ActionResult HomeRuta()
        {
            return View("~/Views/HorarioConductor/Ruta.cshtml");
        }

        public ActionResult HomeDashboard()
        {
            return View("~/Views/Administracion/Dashboard.cshtml");
        }

        public ActionResult HomeReporteHorarioCargado()
        {
            return View("~/Views/Reportes/Reporte_Horario_Cargado.cshtml");
        }
    }
}
