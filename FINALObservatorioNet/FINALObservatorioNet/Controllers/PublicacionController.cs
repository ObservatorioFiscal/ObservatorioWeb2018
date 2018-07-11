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
    public class PublicacionController : Controller
    {
        private base999Entities _context = new base999Entities();
        private readonly Auxiliares _auxiliar = new Auxiliares();

        public async Task<ActionResult> Index(string id)
        {
            var seccion = await _context.Secciones.SingleOrDefaultAsync(m => m.IdSecciones == NombreSecciones.publicacion);
            if (seccion == null)
            {
                return HttpNotFound();
            }
            Dictionary<string, string> lista = new Dictionary<string, string>
            {
                {"", "Todas"},
                {"1", "Columnas"},
                {"2", "Estudios"},
                {"3", "Infografías"},
                {"4", "Informes"},
                {"5", "Principios"},
                {"6", "Reportajes"},
                {"7", "Análisis"}
            };
            ViewBag.tipo = new SelectList(lista, "key", "value", id);

            Dictionary<string, string> lista2 = new Dictionary<string, string>
            {
                {"", "Todos"},
                {"1", "Guillermo Patillo"},
                {"2", "Jeannette von Wolfersdorff"},
                {"3", "Jose Mora"},
                {"4", "Manuel Henriquez"},
                {"5", "Orlando Rojas"},
                {"6", "Matias Jara"},
                {"7", "Paula Diaz"}
            };
            ViewBag.autor = new SelectList(lista2, "key", "value", id);

            ViewBag.Tags = EtiquetasPublicacion.diccionario.ToList();
            ViewBag.i = (string.IsNullOrEmpty(id)) ? "" : id;
            ViewBag.IdNombre = (string.IsNullOrEmpty(id)) ? "" : ": " + _auxiliar.ConvertirtipoPublicacion(id);
            seccion.TituloRedes = seccion.TituloRedes.Replace(" |", ViewBag.IdNombre + " |");


            return View(seccion);
        }

        public async Task<JsonResult> FiltroListadoPublicacion(int pagina, string tags, string orden, string autor, string tipo)
        {
            int i = 9 * pagina;
            List<view_ListaSeccionPublicacion> secciones = new List<view_ListaSeccionPublicacion>();

            tipo = (tipo == "") ? " " : tipo;
            autor = (autor == "") ? " " : autor;

            if (!string.IsNullOrEmpty(tags))
            {
                switch (orden)
                {
                    case "2":
                        secciones = await _context.view_ListaSeccionPublicacion.Where(r => r.autor.Contains(autor) && r.TipoPublicacion.Contains(tipo) && r.Etiqueta.Contains("-" + tags + "-")).OrderByDescending(r => r.Megusta).ThenByDescending(r => r.FechaFecha).Skip(i).Take(9).ToListAsync();
                        break;
                    case "3":
                        secciones = await _context.view_ListaSeccionPublicacion.Where(r => r.autor.Contains(autor) && r.TipoPublicacion.Contains(tipo) && r.Etiqueta.Contains("-" + tags + "-")).OrderBy(r => r.Destacar).ThenByDescending(r => r.FechaFecha).Skip(i).Take(9).ToListAsync();
                        break;
                    default:
                        secciones = await _context.view_ListaSeccionPublicacion.Where(r => r.autor.Contains(autor) && r.TipoPublicacion.Contains(tipo) && r.Etiqueta.Contains("-" + tags + "-")).OrderByDescending(r => r.FechaFecha).Skip(i).Take(9).ToListAsync();
                        break;
                }
            }
            else
            {
                switch (orden)
                {
                    case "2":
                        secciones = await _context.view_ListaSeccionPublicacion.Where(r => r.autor.Contains(autor) && r.TipoPublicacion.Contains(tipo)).OrderByDescending(r => r.Megusta).ThenByDescending(r => r.FechaFecha).Skip(i).Take(9).ToListAsync();
                        break;
                    case "3":
                        secciones = await _context.view_ListaSeccionPublicacion.Where(r => r.autor.Contains(autor) && r.TipoPublicacion.Contains(tipo)).OrderBy(r => r.Destacar).ThenByDescending(r => r.FechaFecha).Skip(i).Take(9).ToListAsync();
                        break;
                    default:
                        secciones = await _context.view_ListaSeccionPublicacion.Where(r => r.autor.Contains(autor) && r.TipoPublicacion.Contains(tipo)).OrderByDescending(r => r.FechaFecha).Skip(i).Take(9).ToListAsync();
                        break;
                }
            }





            foreach (var item in secciones)
            {
                item.Etiqueta = _auxiliar.Convertiretiqueta(item.Etiqueta);
                item.autor = _auxiliar.Convertirautor(item.autor);
                item.TipoPublicacion = TipoPublicacion.diccionarioTipoPublicacion.SingleOrDefault(r=>r.Key== int.Parse(item.TipoPublicacion)).Value;
            }
            return Json(secciones, JsonRequestBehavior.AllowGet);
        }
    }
}