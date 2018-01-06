using ActionMailer.Net.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FINALObservatorioNet.Controllers
{
    public class MailController : MailerBase
    {
        // GET: Mail
        public EmailResult CorreoContacto(string nombre, string quiero, string email, string comentario)
        {
            To.Add("luis@observatoriofiscal.cl");
            ViewBag.solucion = "";
            From = "Contacto Web" + ". <contacto@observatoriofiscal.cl>";
            Subject = "Contacto web.";
            ViewBag.nombre = nombre;
            ViewBag.empresa = quiero;
            ViewBag.email = email;
            ViewBag.comentario = comentario;
            var worke = true;
            return Email("CorreoContacto", worke);
        }
    }
}