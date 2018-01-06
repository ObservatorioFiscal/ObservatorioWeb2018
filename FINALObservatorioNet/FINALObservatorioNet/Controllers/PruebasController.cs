using FINALObservatorioNet.Classes;
using FINALObservatorioNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FINALObservatorioNet.Controllers
{
    public class PruebasController : Controller
    {
        private base999Entities _context = new base999Entities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            return View();
        }


        public ActionResult Index3()
        {
            return View();
        }

        public ActionResult Index4()
        {
            return View();
        }

        public ActionResult GetDatosQuienInvierte29999nivel1(string id)
        {
            #region FINAL
            d3Object_TreeMap d3 = new d3Object_TreeMap();

            string[] array;
            bool FUNCIONALECONOMICO = false;
            string NIVEL = "";
            int PADRE = 0;

            #region parseo Datos
            try
            {
                array = id.Split(';');
                FUNCIONALECONOMICO = (array[0] == "0") ? false : true;
                NIVEL = array[1];
                PADRE = Convert.ToInt32(array[2]);
            }
            catch { }
            #endregion

            switch (NIVEL)
            {
                case "1":
                    List<nivel1> lista1 = _context.nivel1.Where(r => r.Tipo == FUNCIONALECONOMICO).OrderByDescending(r=>r.Valor).ToList();
                    
                    d3.Load(lista1, "0");
                    break;
                case "2":
                    List<nivel2> lista2 = _context.nivel2.Where(r => r.Fk_IdNivel1 == PADRE && r.Tipo == FUNCIONALECONOMICO).OrderByDescending(r => r.Valor).ToList();
                    if (lista2.Count < 1)
                    {
                        lista2 = _context.nivel2.Where(r => r.Fk_IdNivel1 == PADRE && r.Tipo != FUNCIONALECONOMICO).OrderByDescending(r => r.Valor).ToList();
                        if (lista2.Count < 1)
                        {
                            d3.Load(lista2, "2");// 2= NO hay mas detalle
                        }
                        else
                        {
                            d3.Load(lista2, "1");// 1= Hay pero en otra categoria
                        }
                         
                    }
                    else
                    {
                        if(!_context.nivel2.Any(r=>r.Fk_IdNivel1==PADRE && r.Tipo != FUNCIONALECONOMICO))
                        {
                            d3.Load(lista2, "3"); // 1= Solo hay en la que eligio
                        }
                        else
                        {
                            d3.Load(lista2, "0");// 0= Hay normal
                        }
                    }
                    break;
                case "3":

                    List<nivel3> lista3 = _context.nivel3.Where(r => r.Fk_IdNivel2 == PADRE && r.Tipo == FUNCIONALECONOMICO).OrderByDescending(r => r.Valor).ToList();
                    if (lista3.Count < 1)
                    {
                        lista3 = _context.nivel3.Where(r => r.Fk_IdNivel2 == PADRE && r.Tipo != FUNCIONALECONOMICO).OrderByDescending(r => r.Valor).ToList();
                        if (lista3.Count < 1)
                        {
                            d3.Load(lista3, "2");// 2= NO hay mas detalle
                        }
                        else
                        {
                            d3.Load(lista3, "1");
                        }
                    }
                    else
                    {
                        if (!_context.nivel3.Any(r => r.Fk_IdNivel2 == PADRE && r.Tipo != FUNCIONALECONOMICO))
                        {
                            d3.Load(lista3, "3");
                        }
                        else
                        {
                            d3.Load(lista3, "0");
                        }
                    }
                    break;
                case "4":
                    List<nivel4> lista4 = _context.nivel4.Where(r => r.Fk_IdNivel3 == PADRE && r.Tipo == FUNCIONALECONOMICO).OrderByDescending(r => r.Valor).ToList();
                    if (lista4.Count < 1)
                    {
                        lista4 = _context.nivel4.Where(r => r.Fk_IdNivel3 == PADRE && r.Tipo != FUNCIONALECONOMICO).OrderByDescending(r => r.Valor).ToList();
                        if (lista4.Count < 1)
                        {
                            d3.Load(lista4, "2");// 2= NO hay mas detalle
                        }
                        else
                        {
                            d3.Load(lista4, "1");
                        }
                    }
                    else
                    {
                        if (!_context.nivel4.Any(r => r.Fk_IdNivel3 == PADRE && r.Tipo != FUNCIONALECONOMICO))
                        {
                            d3.Load(lista4, "3");
                        }
                        else
                        {
                            d3.Load(lista4, "0");
                        }
                    }
                    break;
            }

            string json = d3.getJson();

            return this.Content(json, "application/json");
            #endregion


        }

        public ActionResult GetDatosQuienInvierte29999nivel1INGRESOS(string id)
        {
            #region FINAL
            d3Object_TreeMap d3 = new d3Object_TreeMap();

            string[] array;
            bool FUNCIONALECONOMICO = false;
            string NIVEL = "";
            int PADRE = 0;

            #region parseo Datos
            try
            {
                array = id.Split(';');
                FUNCIONALECONOMICO = (array[0] == "0") ? false : true;
                NIVEL = array[1];
                PADRE = Convert.ToInt32(array[2]);
            }
            catch { }
            #endregion

            switch (NIVEL)
            {
                case "1":
                    List<nivel11> lista1 = _context.nivel11.Where(r => r.Tipo == FUNCIONALECONOMICO).OrderByDescending(r => r.Valor).ToList();

                    d3.Load(lista1, "0");
                    break;
                case "2":
                    List<nivel22> lista2 = _context.nivel22.Where(r => r.Fk_IdNivel11 == PADRE && r.Tipo == FUNCIONALECONOMICO).OrderByDescending(r => r.Valor).ToList();
                    if (lista2.Count < 1)
                    {
                        lista2 = _context.nivel22.Where(r => r.Fk_IdNivel11 == PADRE && r.Tipo != FUNCIONALECONOMICO).OrderByDescending(r => r.Valor).ToList();
                        if (lista2.Count < 1)
                        {
                            d3.Load(lista2, "2");// 2= NO hay mas detalle
                        }
                        else
                        {
                            d3.Load(lista2, "1");// 1= Hay pero en otra categoria
                        }

                    }
                    else
                    {
                        if (!_context.nivel2.Any(r => r.Fk_IdNivel1 == PADRE && r.Tipo != FUNCIONALECONOMICO))
                        {
                            d3.Load(lista2, "3"); // 1= Solo hay en la que eligio
                        }
                        else
                        {
                            d3.Load(lista2, "0");// 0= Hay normal
                        }
                    }
                    break;
            }

            string json = d3.getJson();

            return this.Content(json, "application/json");
            #endregion


        }


        

    }
}
