using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FINALObservatorioNet.Controllers
{
    public class ReddeObservadoresController : Controller
    {
        // GET: ReddeObservadores
        public ActionResult Index()
        {
            return View();
        }

        // GET: ReddeObservadores/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReddeObservadores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReddeObservadores/Create
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

        // GET: ReddeObservadores/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReddeObservadores/Edit/5
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

        // GET: ReddeObservadores/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReddeObservadores/Delete/5
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
