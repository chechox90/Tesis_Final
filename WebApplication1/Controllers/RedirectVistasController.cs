using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConductorEnRed.Controllers
{
    public class RedirectVistasController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Views/Programacion/CargarProgramacion.cshtml");
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

    }
}
