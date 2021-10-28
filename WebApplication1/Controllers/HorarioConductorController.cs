using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConductorEnRed.Controllers
{
    public class HorarioConductorController : Controller
    {
        // GET: HorarioConductor
        public ActionResult Index()
        {
            return View();
        }

        // GET: HorarioConductor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HorarioConductor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HorarioConductor/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: HorarioConductor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HorarioConductor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: HorarioConductor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HorarioConductor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
