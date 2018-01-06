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
    public class VisualizacionController : Controller
    {
        private base999Entities _context = new base999Entities();
        private readonly Auxiliares _auxiliar = new Auxiliares();

        public async Task<ActionResult> Index(string id)
        {
            ViewBag.Id = (string.IsNullOrEmpty(id)) ? "" : id.ToString();
            ViewBag.IdNombre = (string.IsNullOrEmpty(id)) ? "" : ": " + _auxiliar.Convertiretiqueta(id);
            var seccion = await _context.Secciones.SingleOrDefaultAsync(m => m.IdSecciones == NombreSecciones.visualizacion);
            if (seccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tags = Etiquetas.diccionarioVisualizacion.ToList();
            seccion.TituloRedes = seccion.TituloRedes.Replace(" |", ViewBag.IdNombre + "|");
            return View(seccion);
        }

        public async Task<ActionResult> Repo(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }
            var secciones = await _context.Secciones
                .SingleOrDefaultAsync(m => m.UrlDireccion == id);
            if (secciones == null)
            {
                return HttpNotFound();
            }
            var reco = secciones.Recomendado.Split('-');
            List<view_ListaSeccionVisualizacion> Recomendados = new List<view_ListaSeccionVisualizacion>();
            foreach (var item in reco ){
                int aux = int.Parse(item);
                Recomendados.Add(_context.view_ListaSeccionVisualizacion.Single(r => r.Id == aux));
            }
            ViewBag.Recomendado = Recomendados;
            return View(secciones);
        }
        
        public JsonResult FiltroListadoVisualizacion(int pagina, string tags, string orden)
        {
            int i = 9 * pagina;
            List<view_ListaSeccionVisualizacion> secciones = new List<view_ListaSeccionVisualizacion>();
            if (!string.IsNullOrEmpty(tags))
            {
                switch (orden)
                {
                    case "2":
                        secciones = _context.view_ListaSeccionVisualizacion.Where(r => r.Etiqueta.Contains("-" + tags + "-")).OrderByDescending(r => r.Megusta).ThenByDescending(r => r.FechaFecha).Skip(i).Take(9).ToList();
                        break;
                    case "3":
                        secciones = _context.view_ListaSeccionVisualizacion.Where(r => r.Etiqueta.Contains("-" + tags + "-")).OrderBy(r => r.Destacar).ThenByDescending(r => r.FechaFecha).Skip(i).Take(9).ToList();
                        break;
                    default:
                        secciones = _context.view_ListaSeccionVisualizacion.Where(r => r.Etiqueta.Contains("-" + tags + "-")).OrderByDescending(r=>r.FechaFecha).ThenBy(r=>r.Id).Skip(i).Take(9).ToList();
                        break;
                }
            }
            else
            {
                switch (orden)
                {
                    case "2":
                        secciones = _context.view_ListaSeccionVisualizacion.OrderByDescending(r => r.Megusta).ThenByDescending(r => r.FechaFecha).Skip(i).Take(9).ToList();
                        break;
                    case "3":
                        secciones = _context.view_ListaSeccionVisualizacion.OrderBy(r => r.Destacar).ThenByDescending(r => r.FechaFecha).Skip(i).Take(9).ToList();
                        break;
                    default:
                        secciones = _context.view_ListaSeccionVisualizacion.OrderByDescending(r => r.FechaFecha).Skip(i).Take(9).ToList();
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