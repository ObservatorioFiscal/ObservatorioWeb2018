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
    public class EtiquetaController : Controller
    {
        private base999Entities _context = new base999Entities();
        private readonly Auxiliares _auxiliar = new Auxiliares();
        // GET: Etiqueta
        public ActionResult Index(string nombre)
        {
            string key = Etiquetas.diccionario.SingleOrDefault(r => r.Value == nombre).Key.ToString();
            ViewBag.Etiqueta = nombre;

            List<string> lista = new List<string>{""};
            for (int i = 1; i < 10; i++)
            {
                if (i.ToString() == key)
                {
                    lista.Add("tags-selecter");
                }
                else
                {
                    lista.Add("");
                }
                ViewBag.lista = lista;
            }

            return View();
        }


        public async Task<JsonResult> FiltroListadoEtiqueta(int pagina,string etiqueta)
        {
            etiqueta = etiqueta.Replace("__a__", "á");
            etiqueta = etiqueta.Replace("__e__", "é");
            etiqueta = etiqueta.Replace("__i__", "í");
            etiqueta = etiqueta.Replace("__o__", "ó");
            etiqueta = etiqueta.Replace("__u__", "ú");

            int i = 9 * pagina;
            List<view_ListaSeccionEtiqueta> secciones = new List<view_ListaSeccionEtiqueta>();
            if (!string.IsNullOrEmpty(etiqueta))
            {
                etiqueta = Etiquetas.diccionario.SingleOrDefault(r => r.Value == etiqueta).Key.ToString();
                secciones = await _context.view_ListaSeccionEtiqueta.Where(r => r.Etiquetas.Contains(etiqueta)).OrderByDescending(r => r.FechaFecha).Skip(i).Take(9).ToListAsync();
            }
            else
            {
                secciones = await _context.view_ListaSeccionEtiqueta.OrderByDescending(r => r.FechaFecha).Skip(i).Take(9).ToListAsync();
            }
            
            foreach (var item in secciones)
            {
                item.ImagenUrl = _auxiliar.Convertirimagen(item.ImagenUrl, item.Tipo.Value);
                item.UrlDireccion = _auxiliar.Convertirurl(item.UrlDireccion, item.Tipo.Value);
                item.Etiquetas = _auxiliar.Convertirtipo(item.Tipo.Value);
            }
            return Json(secciones, JsonRequestBehavior.AllowGet);

        }

    }
}
