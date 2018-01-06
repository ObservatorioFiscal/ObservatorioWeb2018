using FINALObservatorioNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FINALObservatorioNet.Controllers
{
    public class HomeController : Controller
    {
        private base999Entities _context = new base999Entities();
        public ActionResult Index()
        {
            ViewBag.Noticias = _context.view_ListaSeccionNoticia.OrderByDescending(r=>r.FechaFecha).Take(4).ToList();
            List<view_ListaSeccionPublicacion> publis= _context.view_ListaSeccionPublicacion.Where(r=>r.Destacar==true).OrderByDescending(r => r.FechaFecha).Take(4).ToList();
            foreach(var item in publis)
            {
                item.TipoPublicacion = TipoPublicacion.diccionarioTipoPublicacion.SingleOrDefault(r => r.Key == int.Parse(item.TipoPublicacion)).Value;
            }
            ViewBag.Publicaciones = publis;
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Nosotros()
        {
            return View();
        }
        public ActionResult Faq()
        {
            List<PreguntasFrecuentes> lista = _context.PreguntasFrecuentes.Where(r => r.Visible == true).ToList();
            return View(lista);
        }
        public ActionResult Contacto()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contacto(string Nombre, string Email, string Quiero, string Descripcion)
        {
            new MailController().CorreoContacto(Nombre, Quiero, Email, Descripcion).Deliver();
            return RedirectToAction("CorreoEnviado");
        }
        public ActionResult CorreoEnviado()
        {
            return View();
        }
        public ActionResult Indicadores()
        {
            return View();
        }
        public ActionResult Datasets()
        {
            List<Secciones> lista = _context.Secciones.Where(r => !string.IsNullOrEmpty(r.Archivo)).ToList();
            return View(lista);
        }

        public ActionResult CreativeCommons()
        {
            return View();
        }

        

        public JsonResult FiltroListadoIndicador(int ingreso, int indicador, int unidad)
        {
            List<Indicadores> secciones = new List<Indicadores>();
            secciones = _context.Indicadores.Where(r => r.IndicadorI == indicador && r.TipoI == ingreso && r.UnidadI == unidad).ToList();
            return Json(secciones, JsonRequestBehavior.AllowGet);
        }


    }
}