using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FINALObservatorioNet.Models;

namespace FINALObservatorioNet.Controllers
{
    [Authorize]
    public class SeccionesController : Controller
    {
        private base999Entities db = new base999Entities();
        
        public async Task<ActionResult> Index()
        {
            return View(await db.Secciones.Where(r=>r.IdSecciones>999).ToListAsync());
        }
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Secciones secciones = await db.Secciones.FindAsync(id);
            if (secciones == null)
            {
                return HttpNotFound();
            }
            return View(secciones);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(HttpPostedFileBase Files, HttpPostedFileBase Files2, [Bind(Include = "IdSecciones,Retroalimentacion,Archivo,TituloPregunta,TituloRedes,Resumen,ImagenUrl,UrlDireccion,TituloInterna,SubTitulo,dataset,DescripcionSuperior,DescripcionCentral,DescripcionInferior,DescripcionScript,Tipo,Etiquetas,Fecha2,Destacar,Megusta,autor,TipoPublicacion,Recomendado,dataset")] Secciones secciones)
        {
            secciones.Recomendado = secciones.Recomendado ?? "";
            secciones.dataset = secciones.dataset ?? "";
            secciones.autor = secciones.autor ?? "";
            if (secciones.TipoPublicacion == null)
            {
                secciones.TipoPublicacion = "";
            }
            else
            {
                secciones.TipoPublicacion = (secciones.TipoPublicacion.Contains(' ')) ? secciones.TipoPublicacion : " " + secciones.TipoPublicacion;
            }
            if (ModelState.IsValid)
            {
                #region FICHERO
                if (Files != null)
                {
                    if (secciones.Tipo == 3)
                    {
                        string archivo = (Files.FileName).ToLower().Replace(" ", "");
                        Files.SaveAs(Server.MapPath("~/ARCHIVOS/biblioteca/" + archivo));
                        secciones.Archivo = archivo;
                        secciones.UrlDireccion = "http://observatoriofiscal.cl/ARCHIVOS/biblioteca/" + archivo;
                    }
                    else
                    {
                        string archivo = (DateTime.Now.ToString("yyyyMMdd") + "-" + Files.FileName).ToLower().Replace(" ", "");
                        Files.SaveAs(Server.MapPath("~/Content/datasets/" + archivo));
                        secciones.Archivo = archivo;
                    }
                }
                #endregion
                #region IMAGEN
                if (Files2 != null)
                {
                    string nomArchivo = secciones.IdSecciones + System.IO.Path.GetExtension(Files2.FileName);
                    Files2.SaveAs(Server.MapPath("~/Content/images/seccion/" + nomArchivo));
                    secciones.ImagenUrl = nomArchivo;
                }
                #endregion
                db.Secciones.Add(secciones);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(secciones);
        }
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Secciones secciones = await db.Secciones.FindAsync(id);
            if (secciones == null)
            {
                return HttpNotFound();
            }
            return View(secciones);
        }
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(HttpPostedFileBase Files, HttpPostedFileBase Files2, [Bind(Include = "IdSecciones,Retroalimentacion,Archivo,TituloPregunta,TituloRedes,Resumen,ImagenUrl,UrlDireccion,dataset,TituloInterna,SubTitulo,DescripcionSuperior,DescripcionCentral,DescripcionCentralM,DescripcionInferior,DescripcionScript,Tipo,Etiquetas,Fecha2,Destacar,Megusta,autor,TipoPublicacion,Recomendado,dataset")] Secciones secciones)
        {
            secciones.Recomendado= secciones.Recomendado ?? "";
            secciones.dataset = secciones.dataset ?? "";
            secciones.autor = secciones.autor ?? "";
            if (ModelState.IsValid)
            {
                #region FICHERO
                if (Files != null)
                {
                    if (!string.IsNullOrEmpty(secciones.Archivo))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/datasets/" + secciones.Archivo));
                    }
                    string archivo = (DateTime.Now.ToString("yyyyMMdd") + "-" + Files.FileName).ToLower().Replace(" ", "");
                    Files.SaveAs(Server.MapPath("~/Content/datasets/" + archivo));
                    secciones.Archivo = archivo;
                }
                #endregion
                #region IMAGEN
                if (Files2 != null)
                {
                    if (!string.IsNullOrEmpty(secciones.ImagenUrl))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/images/seccion/" + secciones.ImagenUrl));
                    }
                    string nomArchivo = secciones.IdSecciones + System.IO.Path.GetExtension(Files2.FileName);
                    Files2.SaveAs(Server.MapPath("~/Content/images/seccion/" + nomArchivo));
                    secciones.ImagenUrl = nomArchivo;
                }
                #endregion
                db.Entry(secciones).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(secciones);
        }
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Secciones secciones = await db.Secciones.FindAsync(id);
            if (secciones == null)
            {
                return HttpNotFound();
            }
            return View(secciones);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Secciones secciones = await db.Secciones.FindAsync(id);
            db.Secciones.Remove(secciones);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> IndexNoticias()
        {
            return View(await db.Noticias.OrderByDescending(r=>r.IdNoticias).ToListAsync());
        }
        public async Task<ActionResult> EditNoticias(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Noticias noticias = await db.Noticias.FindAsync(id);
            if (noticias == null)
            {
                return HttpNotFound();
            }
            return View(noticias);
        }
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> EditNoticias(HttpPostedFileBase Files, [Bind(Include = "IdNoticias,Titulo,Fuente,Url,Imagen,Visible,Descripcion,Internas,IdProyecto,Fecha")] Noticias noticias)
        {
            #region FICHERO
            if (Files != null)
            {
                System.IO.File.Delete(Server.MapPath("~/Content/images/noticia/" + noticias.Imagen));
                string nomArchivo = DateTime.Now.ToString("yyyyMMdd-hhmmss") + System.IO.Path.GetExtension(Files.FileName);
                Files.SaveAs(Server.MapPath("~/Content/images/noticia/" + nomArchivo));
                noticias.Imagen = nomArchivo;
            }
            #endregion
            if (ModelState.IsValid)
            {
                db.Entry(noticias).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("IndexNoticias");
            }
            return View(noticias);
        }


        public ActionResult CreateNoticias()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateNoticias(HttpPostedFileBase Files, [Bind(Include = "IdNoticias,Titulo,Fuente,Url,Imagen,Visible,Descripcion,Internas,IdProyecto,Fecha")] Noticias noticias)
        {
            #region FICHERO
            if (Files != null)
            {
                string nomArchivo = DateTime.Now.ToString("yyyyMMdd-hhmmss") + System.IO.Path.GetExtension(Files.FileName);
                Files.SaveAs(Server.MapPath("~/Content/images/noticia/" + nomArchivo));
                noticias.Imagen = nomArchivo;
            }
            #endregion
            if (ModelState.IsValid)
            {
                db.Noticias.Add(noticias);
                await db.SaveChangesAsync();
                return RedirectToAction("IndexNoticias");
            }

            return View(noticias);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
