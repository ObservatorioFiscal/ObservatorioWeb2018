using FINALObservatorioNet.Classes;
using FINALObservatorioNet.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FINALObservatorioNet.Controllers
{
    public class NoticiaController : Controller
    {
        private base999Entities _context = new base999Entities();
        private readonly Auxiliares _auxiliar = new Auxiliares();

        public async Task<ActionResult> Index()
        {
            var seccion = await _context.Secciones.SingleOrDefaultAsync(m => m.IdSecciones == NombreSecciones.noticia);
            if (seccion == null)
            {
                return HttpNotFound();
            }
            return View(seccion);
        }

        public JsonResult FiltroListadoNoticia(int pagina, string tags)
        {
            int i = 9 * pagina;
            List<view_ListaSeccionNoticia> secciones = new List<view_ListaSeccionNoticia>();
            
            if (!string.IsNullOrEmpty(tags))
            {
                bool aux = (tags == "1") ? true : false;
                secciones = _context.view_ListaSeccionNoticia.Where(r => r.Internas == aux).OrderByDescending(r => r.FechaFecha).Skip(i).Take(9).ToList();
            }
            else
            {
                secciones = _context.view_ListaSeccionNoticia.OrderByDescending(r => r.FechaFecha).Skip(i).Take(9).ToList();
            }
            return Json(secciones, JsonRequestBehavior.AllowGet);

        }
    }
}
