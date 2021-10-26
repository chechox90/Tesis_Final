using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class ErrorPageController : Controller
    {
        public ActionResult ErrorMessage()
        {
            return View();
        }
    }
}
