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
    public class AplicacionController : Controller
    {
        private base999Entities _context = new base999Entities();
        private readonly Auxiliares _auxiliar = new Auxiliares();

        public async Task<ActionResult> Index()
        {
            var seccion =await _context.Secciones.SingleOrDefaultAsync(m => m.IdSecciones == NombreSecciones.aplicacion);
            if (seccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tags = EtiquetasAplicacion.diccionario.ToList();
            return View(seccion);
        }

        public async Task<ActionResult> Cuantoconstribuyesalestado()
        {
            var seccion = await _context.Secciones.SingleOrDefaultAsync(m => m.IdSecciones == 1001);
            if (seccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tags = EtiquetasAplicacion.diccionario.ToList();
            ViewBag.IdRes = seccion.IdSecciones;
            return View(seccion);
        }

        public async Task<ActionResult> Situfueraselalcalde()
        {
            var seccion = await _context.Secciones.SingleOrDefaultAsync(m => m.IdSecciones == 1002);
            if (seccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tags = EtiquetasAplicacion.diccionario.ToList();
            ViewBag.Comunas = _context.AppMunicipalidad.Select(r => r.NombreComuna).Distinct().OrderBy(r => r);
            ViewBag.IdRes = seccion.IdSecciones;
            return View(seccion);
        }

        public async Task<ActionResult> Quizdelestado()
        {
            var seccion = await _context.Secciones.SingleOrDefaultAsync(m => m.IdSecciones == 1003);
            if (seccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tags = EtiquetasAplicacion.diccionario.ToList();
            ViewBag.Comunas = _context.AppMunicipalidad.Select(r => r.NombreComuna).Distinct().OrderBy(r => r);
            return View(seccion);
        }





        public async Task<JsonResult> FiltroListadoAplicacion(int pagina, string tags, string orden)
        {
            int i = 9 * pagina;
            List<view_ListaSeccionAplicacion> secciones = new List<view_ListaSeccionAplicacion>();

            if (!string.IsNullOrEmpty(tags))
            {
                switch (orden)
                {
                    case "2":
                        secciones = await _context.view_ListaSeccionAplicacion.Where(r => r.Etiqueta.Contains("-" + tags + "-")).OrderByDescending(r => r.Megusta).ThenByDescending(r => r.FechaFecha).Skip(i).Take(9).ToListAsync();
                        break;
                    case "3":
                        secciones = await _context.view_ListaSeccionAplicacion.Where(r => r.Etiqueta.Contains("-" + tags + "-")).OrderBy(r => r.Destacar).ThenByDescending(r => r.FechaFecha).Skip(i).Take(9).ToListAsync();
                        break;
                    default:
                        secciones = await _context.view_ListaSeccionAplicacion.Where(r => r.Etiqueta.Contains("-" + tags + "-")).OrderByDescending(r => r.FechaFecha).Skip(i).Take(9).ToListAsync();
                        break;
                }
            }
            else
            {
                switch (orden)
                {
                    case "2":
                        secciones = await _context.view_ListaSeccionAplicacion.OrderByDescending(r => r.Megusta).ThenByDescending(r => r.FechaFecha).Skip(i).Take(9).ToListAsync();
                        break;
                    case "3":
                        secciones = await _context.view_ListaSeccionAplicacion.OrderBy(r => r.Destacar).ThenByDescending(r => r.FechaFecha).Skip(i).Take(9).ToListAsync();
                        break;
                    default:
                        secciones = await _context.view_ListaSeccionAplicacion.OrderByDescending(r => r.FechaFecha).Skip(i).Take(9).ToListAsync();
                        break;
                }
            }

            foreach (var item in secciones)
            {
                item.Etiqueta = _auxiliar.Convertiretiqueta(item.Etiqueta);
            }
            return Json(secciones, JsonRequestBehavior.AllowGet);
        }
    }
}
