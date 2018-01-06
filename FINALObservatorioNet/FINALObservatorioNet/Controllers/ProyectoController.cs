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
    public class ProyectoController : Controller
    {
        private base999Entities _context = new base999Entities();
        private readonly Auxiliares _auxiliar = new Auxiliares();

        public async Task<ActionResult> Index()
        {
            var seccion = await _context.Secciones.SingleOrDefaultAsync(m => m.IdSecciones == NombreSecciones.proyecto);
            if (seccion == null)
            {
                return HttpNotFound();
            }
            return View(seccion);
        }

        public async Task<ActionResult> DatosEntendibles()
        {
            var seccion = await _context.Secciones.SingleOrDefaultAsync(m => m.IdSecciones == 4002);
            ViewBag.noticias = _context.view_ListaSeccionNoticia.Where(r => r.IdProyecto == 4002).OrderByDescending(r=>r.FechaFecha).ToList();
            if (seccion == null)
            {
                return HttpNotFound();
            }
            return View(seccion);
        }

        public async Task<ActionResult> GastoAmigable()
        {
            var seccion = await _context.Secciones.SingleOrDefaultAsync(m => m.IdSecciones == 4003);
            ViewBag.noticias = _context.view_ListaSeccionNoticia.Where(r => r.IdProyecto == 4003).OrderByDescending(r => r.FechaFecha).ToList();
            if (seccion == null)
            {
                return HttpNotFound();
            }
            return View(seccion);
        }

        public async Task<ActionResult> ComprasPublicas()
        {
            var seccion = await _context.Secciones.SingleOrDefaultAsync(m => m.IdSecciones == 4001);
            ViewBag.noticias = _context.view_ListaSeccionNoticia.Where(r => r.IdProyecto == 4001).OrderByDescending(r => r.FechaFecha).ToList();
            if (seccion == null)
            {
                return HttpNotFound();
            }
            return View(seccion);
        }
    }
}