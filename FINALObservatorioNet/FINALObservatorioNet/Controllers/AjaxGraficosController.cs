using FINALObservatorioNet.Classes;
using FINALObservatorioNet.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FINALObservatorioNet.Controllers
{
    public class AjaxGraficosController : Controller
    {
        private base999Entities _context = new base999Entities();

        public ActionResult GetDatosEnQueGastaElEstado1(string id)
        {
            Grafico_d3Object_Bubble infoGrafico = _context.Grafico_d3Object_Bubble.Find(8);
            d3Object_Bubble grafico = new d3Object_Bubble();
            if (id == "2")
            {
                grafico.Load(infoGrafico, "porcentaje");
            }
            else
            {
                grafico.Load(infoGrafico, "peso");
            }
            string json = grafico.getJson();
            return this.Content(json, "application/json");
        }
        public ActionResult GetDatosComoHaCambiadoElGasto1(string id, string formato)
        {
            Grafico_googleChart_Line infoGrafico = _context.Grafico_googleChart_Line.Find(21);

            googleChart_Line grafico = new googleChart_Line();
            if (formato == "2")
            {
                grafico.Load(infoGrafico, "porcentaje");
            }
            else
            {
                grafico.Load(infoGrafico, "peso");
            }

            string[] aux = grafico.getJson();

            var json = new
            {
                data = aux[0],
                colores = aux[1],
                title = aux[2],
                formato = aux[3],
                cantidad = aux[4]
            };

            return this.Json(json, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDatosComoHaCambiadoElGasto1_Button1(string id, string color, string formato)
        {
            string auxs = id + color;
            Grafico_googleChart_Line infoGrafico = new Grafico_googleChart_Line();

            if (id == "1")
                infoGrafico = _context.Grafico_googleChart_Line.Find(21); //10

            if (id == "2")
                infoGrafico = _context.Grafico_googleChart_Line.Find(18);

            if (id == "3")
                infoGrafico = _context.Grafico_googleChart_Line.Find(19);

            if (id == "4")
                infoGrafico = _context.Grafico_googleChart_Line.Find(20);

            googleChart_Line grafico = new googleChart_Line();
            if (formato == "2")
            {
                grafico.Load(infoGrafico, "porcentaje");
            }
            else
            {
                grafico.Load(infoGrafico, "peso");
            }

            string[] aux = grafico.getJson();
            var json = new
            {
                data = aux[0],
                colores = aux[1],
                title = aux[2],
                formato = aux[3],
                cantidad = aux[4]
            };

            return this.Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDatosCifrasGenerales1(string id)
        {
            Grafico_amChart_HorizontalBar infografico = _context.Grafico_amChart_HorizontalBar.Find(2);
            amChart_HorizontalBar amChart = new amChart_HorizontalBar();
            amChart.Load(infografico);
            string json = amChart.getJson();

            return this.Content(json, "application/json");
        }
        public ActionResult GetDatosCifrasGenerales2(string id)
        {
            Grafico_amChart_HorizontalBar infografico = _context.Grafico_amChart_HorizontalBar.Find(3);

            amChart_HorizontalBar amChart = new amChart_HorizontalBar();
            amChart.Load(infografico);
            string json = amChart.getJson();

            return this.Content(json, "application/json");
        }

        public ActionResult GetDatosQuienInvierte29999nivel1(string id)
        {
            d3Object_TreeMap d3 = new d3Object_TreeMap();

            string[] array;
            string idRegion = "";
            string tipoDato = "";

            #region parseo Datos
            try
            {
                array = id.Split(';');
                idRegion = array[0];
                idRegion = idRegion.Replace(" ", "");
                tipoDato = array[1];
                tipoDato = tipoDato.Replace(" ", "");
            }
            catch { }
            #endregion

            Grafico_d3Object_TreeMap infoGrafico = _context.Grafico_d3Object_TreeMap.Find(10);
            List<Cuadrados_d3Object_TreeMap> lista2 = new List<Cuadrados_d3Object_TreeMap>();

            if (!string.IsNullOrEmpty(idRegion))
            {
                lista2 = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idRegion).ToList();
            }
            else
            {
                lista2 = infoGrafico.Cuadrados_d3Object_TreeMap.ToList();
            }
            d3Object_TreeMap grafico = new d3Object_TreeMap();
            grafico.Load(lista2, tipoDato);
            string json = grafico.getJson();

            return this.Content(json, "application/json");
        }
        public ActionResult GetDatosQuienInvierte29999nivel2(string id)
        {
            d3Object_TreeMap d3 = new d3Object_TreeMap();

            string[] array;
            string idRegion = "";
            string tipoDato = "";
            string Familia = "";
            #region parseo Datos
            try
            {
                array = id.Split(';');
                idRegion = array[0];
                tipoDato = array[1];
                Familia = array[2];
            }
            catch { }
            #endregion

            List<Cuadrados_d3Object_TreeMap> lista2 = _context.Grafico_d3Object_TreeMap.Find(10).Cuadrados_d3Object_TreeMap.Where(r => r.Tipo == Familia).ToList();
            List<SubCuadrados_d3Object_TreeMap> lista3 = new List<SubCuadrados_d3Object_TreeMap>();

            if (!string.IsNullOrEmpty(idRegion))
            {
                lista2 = lista2.Where(r => r.IdRegion == idRegion).ToList();
            }

            foreach (var item in lista2)
            {
                foreach (var item2 in item.SubCuadrados_d3Object_TreeMap)
                {
                    lista3.Add(item2);
                }
            }

            d3Object_TreeMap grafico = new d3Object_TreeMap();
            grafico.Load(lista3, tipoDato);

            string json = grafico.getJson();

            return this.Content(json, "application/json");
        }
        public ActionResult GetDatosQuienInvierte29999nivel3(string id)
        {
            d3Object_TreeMap d3 = new d3Object_TreeMap();

            string[] array;
            string idRegion = "";
            string idTipoFamilia = "";
            string tipoDato = "";
            string nivel = "";

            #region parseo Datos
            try
            {
                array = id.Split(';');
                idRegion = array[0];
                idRegion = idRegion.Replace(" ", "");
                if (array[1] == " ")
                {
                    idTipoFamilia = "";
                }
                else
                {
                    idTipoFamilia = array[1];
                }
                tipoDato = array[2];
                tipoDato = tipoDato.Replace(" ", "");
                nivel = array[2];
            }
            catch { }
            #endregion

            Grafico_d3Object_TreeMap infoGrafico = _context.Grafico_d3Object_TreeMap.Find(10);

            List<SubCuadrados_d3Object_TreeMap> lista2 = new List<SubCuadrados_d3Object_TreeMap>();

            if (string.IsNullOrEmpty(idTipoFamilia) && string.IsNullOrEmpty(idRegion))
            {
                var lista = infoGrafico.Cuadrados_d3Object_TreeMap.Select(R => R.IdCuadrado).ToList();
                foreach (var item in lista)
                {
                    var lista3 = _context.SubCuadrados_d3Object_TreeMap.Where(r => r.IdCirculoFK == item).ToList();
                    foreach (var item2 in lista3)
                    {
                        lista2.Add(item2);
                    }
                }
            }

            if (!string.IsNullOrEmpty(idTipoFamilia) && string.IsNullOrEmpty(idRegion))
            {
                var lista = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.Titulo == idTipoFamilia).Select(R => R.IdCuadrado).ToList();
                foreach (var item in lista)
                {
                    var lista3 = _context.SubCuadrados_d3Object_TreeMap.Where(r => r.IdCirculoFK == item).ToList();
                    foreach (var item2 in lista3)
                    {
                        lista2.Add(item2);
                    }
                }
            }


            if (string.IsNullOrEmpty(idTipoFamilia) && !string.IsNullOrEmpty(idRegion))
            {
                var lista = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idRegion).Select(R => R.IdCuadrado).ToList();
                foreach (var item in lista)
                {
                    var lista3 = _context.SubCuadrados_d3Object_TreeMap.Where(r => r.IdCirculoFK == item).ToList();
                    foreach (var item2 in lista3)
                    {
                        lista2.Add(item2);
                    }
                }
            }

            if (!string.IsNullOrEmpty(idTipoFamilia) && !string.IsNullOrEmpty(idRegion))
            {
                var lista = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.Titulo == idTipoFamilia && r.IdRegion == idRegion).Select(R => R.IdCuadrado).ToList();
                foreach (var item in lista)
                {
                    var lista3 = _context.SubCuadrados_d3Object_TreeMap.Where(r => r.IdCirculoFK == item).ToList();
                    foreach (var item2 in lista3)
                    {
                        lista2.Add(item2);
                    }
                }
            }

            d3Object_TreeMap grafico = new d3Object_TreeMap();
            //grafico.Load(infoGrafico, tipoDato, lista2, idRegion);

            string json = grafico.getJson();

            return this.Content(json, "application/json");
        }
        public ActionResult GetDatosQuienInvierte19999(string parametroTreemap, string idCiudad, string tipoValor, string servicio)
        {
            Grafico_d3Object_TreeMap infoGrafico = _context.Grafico_d3Object_TreeMap.Find(10);
            Cuadrados_d3Object_TreeMap cuadrados = new Cuadrados_d3Object_TreeMap();


            if (!string.IsNullOrEmpty(servicio))
            {
                for (int i = 0; i < infoGrafico.Cuadrados_d3Object_TreeMap.Count; i++)
                {
                    List<SubCuadrados_d3Object_TreeMap> lista = infoGrafico.Cuadrados_d3Object_TreeMap.Skip(i).First().SubCuadrados_d3Object_TreeMap.Where(r => r.Titulo != servicio).ToList();
                    while (lista.Count > 0)
                    {
                        SubCuadrados_d3Object_TreeMap item = lista.First();
                        infoGrafico.Cuadrados_d3Object_TreeMap.Skip(i).First().SubCuadrados_d3Object_TreeMap.Remove(item);
                        lista.Remove(lista.First());
                    }
                    lista = null;
                }
            }

            var datos = infoGrafico.Cuadrados_d3Object_TreeMap.GroupBy(r => new { r.IdRegion }).Select(r => new { Region = r.Key.IdRegion, Total = r.Sum(l => l.SubCuadrados_d3Object_TreeMap.Sum(k => k.valorPercapita)) }).OrderByDescending(r => r.Total);

            if (string.IsNullOrEmpty(parametroTreemap) && string.IsNullOrEmpty(idCiudad))
            {
                if (tipoValor == "percapita")
                {
                    datos = infoGrafico.Cuadrados_d3Object_TreeMap.GroupBy(r => new { r.IdRegion }).Select(r => new { Region = r.Key.IdRegion, Total = r.Sum(l => l.SubCuadrados_d3Object_TreeMap.Sum(k => k.valorPercapita)) }).OrderByDescending(r => r.Total);
                }
                else
                {
                    datos = infoGrafico.Cuadrados_d3Object_TreeMap.GroupBy(r => new { r.IdRegion }).Select(r => new { Region = r.Key.IdRegion, Total = r.Sum(l => l.SubCuadrados_d3Object_TreeMap.Sum(k => k.valorNominal)) }).OrderByDescending(r => r.Total);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(parametroTreemap) && string.IsNullOrEmpty(idCiudad))
                {
                    if (tipoValor == "percapita")
                    {
                        datos = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.Tipo == parametroTreemap).GroupBy(r => new { r.IdRegion }).Select(r => new { Region = r.Key.IdRegion, Total = r.Sum(l => l.SubCuadrados_d3Object_TreeMap.Sum(k => k.valorPercapita)) }).OrderByDescending(r => r.Total);
                    }
                    else
                    {
                        datos = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.Tipo == parametroTreemap).GroupBy(r => new { r.IdRegion }).Select(r => new { Region = r.Key.IdRegion, Total = r.Sum(l => l.SubCuadrados_d3Object_TreeMap.Sum(k => k.valorNominal)) }).OrderByDescending(r => r.Total);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(parametroTreemap) && !string.IsNullOrEmpty(idCiudad))
                    {
                        if (tipoValor == "percapita")
                        {
                            datos = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).GroupBy(r => new { r.IdRegion }).Select(r => new { Region = r.Key.IdRegion, Total = r.Sum(l => l.SubCuadrados_d3Object_TreeMap.Sum(k => k.valorPercapita)) }).OrderByDescending(r => r.Total);
                        }
                        else
                        {
                            datos = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).GroupBy(r => new { r.IdRegion }).Select(r => new { Region = r.Key.IdRegion, Total = r.Sum(l => l.SubCuadrados_d3Object_TreeMap.Sum(k => k.valorNominal)) }).OrderByDescending(r => r.Total);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(parametroTreemap) && !string.IsNullOrEmpty(idCiudad))
                        {
                            if (tipoValor == "percapita")
                            {
                                datos = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.Tipo == parametroTreemap && r.IdRegion == idCiudad).GroupBy(r => new { r.IdRegion }).Select(r => new { Region = r.Key.IdRegion, Total = r.Sum(l => l.SubCuadrados_d3Object_TreeMap.Sum(k => k.valorPercapita)) }).OrderByDescending(r => r.Total);
                            }
                            else
                            {
                                datos = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.Tipo == parametroTreemap && r.IdRegion == idCiudad).GroupBy(r => new { r.IdRegion }).Select(r => new { Region = r.Key.IdRegion, Total = r.Sum(l => l.SubCuadrados_d3Object_TreeMap.Sum(k => k.valorNominal)) }).OrderByDescending(r => r.Total);
                            }
                        }
                    }
                }
            }



            string[] arrayPorcentaje = { "0%", "0%", "0%", "0%", "0%", "0%", "0%", "0%", "0%", "0%", "0%", "0%", "0%", "0%", "0%" };
            int[] arrayIntensidadColor = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double total = 1;
            double propir = datos.Sum(r => r.Total).Value;// infoGrafico.Cuadrados_d3Object_TreeMap.Sum(r => r.valorNominal).Value; //LUISMI
            total = propir; //PARA QUE EL MAPA MUESTRE PORCENTAJE SEGUN EL NIVEL.
            //try
            //{
            //    if (tipoValor == "percapita")
            //    {
            //        total = infoGrafico.Cuadrados_d3Object_TreeMap.Sum(r => r.valorPercapita).Value;
            //    }
            //    else
            //    {
            //        total = infoGrafico.Cuadrados_d3Object_TreeMap.Sum(r => r.valorNominal).Value;
            //    }

            //}
            //catch { } //PARA QUE EL MAPA MUESTRE PORCENTAJE SEGUN EL TOTAL.

            //int intColor = 15;
            int intColor = 1;
            int cantidadColores = 1;
            foreach (var data in datos)
            {
                #region Cases
                switch (data.Region)
                {
                    case "CL-AP":
                        arrayIntensidadColor[0] = intColor;
                        arrayPorcentaje[0] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-TA":
                        arrayIntensidadColor[1] = intColor;
                        arrayPorcentaje[1] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-AN":
                        arrayIntensidadColor[2] = intColor;
                        arrayPorcentaje[2] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-AT":
                        arrayIntensidadColor[3] = intColor;
                        arrayPorcentaje[3] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-CO":
                        arrayIntensidadColor[4] = intColor;
                        arrayPorcentaje[4] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-VS":
                        arrayIntensidadColor[5] = intColor;
                        arrayPorcentaje[5] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-RM":
                        arrayIntensidadColor[6] = intColor;
                        arrayPorcentaje[6] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-LI":
                        arrayIntensidadColor[7] = intColor;
                        arrayPorcentaje[7] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-ML":
                        arrayIntensidadColor[8] = intColor;
                        arrayPorcentaje[8] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-BI":
                        arrayIntensidadColor[9] = intColor;
                        arrayPorcentaje[9] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-AR":
                        arrayIntensidadColor[10] = intColor;
                        arrayPorcentaje[10] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-LR":
                        arrayIntensidadColor[11] = intColor;
                        arrayPorcentaje[11] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-LL":
                        arrayIntensidadColor[12] = intColor;
                        arrayPorcentaje[12] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-AI":
                        arrayIntensidadColor[13] = intColor;
                        arrayPorcentaje[13] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-MA":
                        arrayIntensidadColor[14] = intColor;
                        arrayPorcentaje[14] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;
                }
                intColor++;
                cantidadColores++;
                #endregion
            }
            string auxPropir = string.Format("{0:N0}", propir);
            auxPropir = auxPropir.Replace(",", ".");

            var json = new
            {
                dataCantColores = cantidadColores,
                dataIntColor = arrayIntensidadColor,
                dataPorcentaje = arrayPorcentaje,
                dataPROPIR = auxPropir
            };
            return this.Json(json, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTituloPropirQuienInvierte39999(string tipoValor, string idCiudad, string optCategoria, string servicio)
        {

            Grafico_d3Object_TreeMap infoGrafico = _context.Grafico_d3Object_TreeMap.Find(10);
            Cuadrados_d3Object_TreeMap Cuadrado = new Cuadrados_d3Object_TreeMap();

            double propir = 1;
            string prefijoValor = "";
            string valorCategoria = "";
            string textoSiRegion = "";
            string textoservicio = "";

            #region prefijo
            try
            {
                if (tipoValor == "percapita")
                {
                    propir = infoGrafico.Cuadrados_d3Object_TreeMap.Sum(r => r.valorPercapita).Value;
                    prefijoValor = "$";
                }
                else
                {
                    propir = infoGrafico.Cuadrados_d3Object_TreeMap.Sum(r => r.valorNominal).Value;
                    prefijoValor = "MM$";
                }

            }
            catch { }

            #endregion

            double valor;

            #region valor MINISTERIO Y SERVICIO
            if (!string.IsNullOrEmpty(optCategoria))
            {
                if (!string.IsNullOrEmpty(idCiudad))
                {
                    if (tipoValor == "percapita")
                    {
                        List<Cuadrados_d3Object_TreeMap> valores = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad && r.Titulo == optCategoria).ToList();
                        valor = valores.Sum(r => r.valorPercapita).Value;
                        valorCategoria = "<br>MINISTERIO: " + optCategoria + " " + prefijoValor + string.Format("{0:N0}", valor);
                        if (!string.IsNullOrEmpty(servicio))
                        {
                            valor = 0;
                            foreach (var item in valores)
                            {
                                valor = valor + item.SubCuadrados_d3Object_TreeMap.Where(r => r.Titulo == servicio).Sum(r => r.valorPercapita).Value;
                            }

                            textoservicio = "<br>SERVICIO: " + servicio + " " + prefijoValor + string.Format("{0:N0}", valor);
                        }
                    }
                    else
                    {
                        List<Cuadrados_d3Object_TreeMap> valores = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad && r.Titulo == optCategoria).ToList();
                        valor = valores.Sum(r => r.valorNominal).Value;
                        valorCategoria = "<br>MINISTERIO: " + optCategoria + " " + prefijoValor + string.Format("{0:N0}", valor);
                        if (!string.IsNullOrEmpty(servicio))
                        {
                            valor = 0;
                            foreach (var item in valores)
                            {
                                valor = valor + item.SubCuadrados_d3Object_TreeMap.Where(r => r.Titulo == servicio).Sum(r => r.valorNominal).Value;
                            }

                            textoservicio = "<br>SERVICIO: " + servicio + " " + prefijoValor + string.Format("{0:N0}", valor);
                        }
                    }
                }
                else
                {
                    if (tipoValor == "percapita")
                    {
                        List<Cuadrados_d3Object_TreeMap> valores = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.Titulo == optCategoria).ToList();
                        valor = valores.Sum(r => r.valorPercapita).Value;
                        valorCategoria = "<br>MINISTERIO: " + optCategoria + " " + prefijoValor + string.Format("{0:N0}", valor);
                        if (!string.IsNullOrEmpty(servicio))
                        {
                            valor = 0;
                            foreach (var item in valores)
                            {
                                valor = valor + item.SubCuadrados_d3Object_TreeMap.Where(r => r.Titulo == servicio).Sum(r => r.valorPercapita).Value;
                            }

                            textoservicio = "<br>SERVICIO: " + servicio + " " + prefijoValor + string.Format("{0:N0}", valor);
                        }
                    }
                    else
                    {
                        List<Cuadrados_d3Object_TreeMap> valores = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.Titulo == optCategoria).ToList();
                        valor = valores.Sum(r => r.valorNominal).Value;
                        valorCategoria = "<br>MINISTERIO: " + optCategoria + " " + prefijoValor + string.Format("{0:N0}", valor);
                        if (!string.IsNullOrEmpty(servicio))
                        {
                            valor = 0;
                            foreach (var item in valores)
                            {
                                valor = valor + item.SubCuadrados_d3Object_TreeMap.Where(r => r.Titulo == servicio).Sum(r => r.valorNominal).Value;
                            }

                            textoservicio = "<br>SERVICIO: " + servicio + " " + prefijoValor + string.Format("{0:N0}", valor);
                        }
                    }
                }
            }
            #endregion


            #region Cases REGION
            switch (idCiudad)
            {
                case "CL-AP":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorPercapita).Value;
                        textoSiRegion = "<br>REGION: ARICA " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorNominal).Value;
                        textoSiRegion = "<br>REGION: ARICA " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-TA":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorPercapita).Value;
                        textoSiRegion = "<br>REGION: TARAPACA " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorNominal).Value;
                        textoSiRegion = "<br>REGION: TARAPACA " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-AN":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorPercapita).Value;
                        textoSiRegion = "<br>REGION: ANTOFAGASTA " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorNominal).Value;
                        textoSiRegion = "<br>REGION: ANTOFAGASTA " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-AT":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorPercapita).Value;
                        textoSiRegion = "<br>REGION: ATACAMA " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorNominal).Value;
                        textoSiRegion = "<br>REGION: ATACAMA " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-CO":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorPercapita).Value;
                        textoSiRegion = "<br>REGION: COQUIMBO " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorNominal).Value;
                        textoSiRegion = "<br>REGION: COQUIMBO " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-VS":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorPercapita).Value;
                        textoSiRegion = "<br>REGION: VALPARAISO " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorNominal).Value;
                        textoSiRegion = "<br>REGION: VALPARAISO " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-RM":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorPercapita).Value;
                        textoSiRegion = "<br>REGION: SANTIAGO " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorNominal).Value;
                        textoSiRegion = "<br>REGION: SANTIAGO " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-LI":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorPercapita).Value;
                        textoSiRegion = "<br>REGION: OHIGGINS " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorNominal).Value;
                        textoSiRegion = "<br>REGION: OHIGGINS " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-ML":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorPercapita).Value;
                        textoSiRegion = "<br>REGION: MAULE " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorNominal).Value;
                        textoSiRegion = "<br>REGION: MAULE " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-BI":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorPercapita).Value;
                        textoSiRegion = "<br>REGION: BIOBIO " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorNominal).Value;
                        textoSiRegion = "<br>REGION: BIOBIO " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-AR":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorPercapita).Value;
                        textoSiRegion = "<br>REGION: ARAUCANIA " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorNominal).Value;
                        textoSiRegion = "<br>REGION: ARAUCANIA " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-LR":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorPercapita).Value;
                        textoSiRegion = "<br>REGION: LOS RIOS " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorNominal).Value;
                        textoSiRegion = "<br>REGION: LOS RIOS " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-LL":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorPercapita).Value;
                        textoSiRegion = "<br>REGION: LOS LAGOS " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorNominal).Value;
                        textoSiRegion = "<br>REGION: LOS LAGOS " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-AI":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorPercapita).Value;
                        textoSiRegion = "<br>REGION: AYSEN " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorNominal).Value;
                        textoSiRegion = "<br>REGION: AYSEN " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-MA":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorPercapita).Value;
                        textoSiRegion = "<br>REGION: MAGALLAES " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Cuadrados_d3Object_TreeMap.Where(r => r.IdRegion == idCiudad).Sum(r => r.valorNominal).Value;
                        textoSiRegion = "<br>REGION: MAGALLAES " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;
            }

            #endregion



            string nacionalPropir = "NACIONAL: " + prefijoValor + " " + string.Format("{0:N0}", propir);
            nacionalPropir = nacionalPropir.Replace(",", ".");
            textoSiRegion = textoSiRegion.Replace(",", ".");
            valorCategoria = valorCategoria.Replace(",", ".");
            textoservicio = textoservicio.Replace(",", ".");

            if (valorCategoria == "")
            {
                valorCategoria = "<br /><br />";
                if (textoSiRegion == "")
                {
                    valorCategoria = valorCategoria + "<br /><br />";
                }
            }
            else
            {
                if (textoservicio == "")
                {
                    valorCategoria = valorCategoria + "<br />";
                }
            }

            string cadenaFinal = nacionalPropir + " " + textoSiRegion + " " + valorCategoria + " " + textoservicio;

            var json = new
            {
                dataPROPIR = cadenaFinal
            };

            return this.Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDatosEnQueSeInvierte(string idCiudad, string tipoValor)
        {
            Grafico_amChart_HorizontalBar barra = _context.Grafico_amChart_HorizontalBar.Find(16);
            Barra_amChart_HorizontalBar cuadrados = new Barra_amChart_HorizontalBar();

            var datos = barra.Barra_amChart_HorizontalBar.GroupBy(r => new { r.Idregion }).Select(r => new { Region = r.Key.Idregion, Total = r.Sum(l => l.valor) });
            datos = datos.OrderByDescending(r => r.Total);
            if (string.IsNullOrEmpty(idCiudad))
            {
                if (tipoValor == "percapita")
                {
                    datos = barra.Barra_amChart_HorizontalBar.GroupBy(r => new { r.Idregion }).Select(r => new { Region = r.Key.Idregion, Total = r.Sum(l => l.ValorPercapita) }).OrderByDescending(r => r.Total);
                }
                else
                {
                    datos = barra.Barra_amChart_HorizontalBar.GroupBy(r => new { r.Idregion }).Select(r => new { Region = r.Key.Idregion, Total = r.Sum(l => l.valor) }).OrderByDescending(r => r.Total);
                }
            }
            else
            {
                if (tipoValor == "percapita")
                {
                    datos = barra.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).GroupBy(r => new { r.Idregion }).Select(r => new { Region = r.Key.Idregion, Total = r.Sum(l => l.ValorPercapita) }).OrderByDescending(r => r.Total);
                }
                else
                {
                    datos = barra.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).GroupBy(r => new { r.Idregion }).Select(r => new { Region = r.Key.Idregion, Total = r.Sum(l => l.valor) }).OrderByDescending(r => r.Total);
                }
            }

            string[] arrayPorcentaje = { "0%", "0%", "0%", "0%", "0%", "0%", "0%", "0%", "0%", "0%", "0%", "0%", "0%", "0%", "0%" };
            int[] arrayIntensidadColor = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double total = 1;
            double propir = barra.Barra_amChart_HorizontalBar.Sum(r => r.valor).Value;
            try
            {
                if (tipoValor == "percapita")
                {
                    total = barra.Barra_amChart_HorizontalBar.Sum(r => r.ValorPercapita).Value;
                }
                else
                {
                    total = barra.Barra_amChart_HorizontalBar.Sum(r => r.valor).Value;
                }

            }
            catch { }

            int intColor = 1;
            int cantidadColores = 1;
            foreach (var data in datos)
            {
                #region Cases
                switch (data.Region)
                {
                    case "CL-AP":
                        arrayIntensidadColor[0] = intColor;
                        arrayPorcentaje[0] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-TA":
                        arrayIntensidadColor[1] = intColor;
                        arrayPorcentaje[1] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-AN":
                        arrayIntensidadColor[2] = intColor;
                        arrayPorcentaje[2] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-AT":
                        arrayIntensidadColor[3] = intColor;
                        arrayPorcentaje[3] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-CO":
                        arrayIntensidadColor[4] = intColor;
                        arrayPorcentaje[4] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-VS":
                        arrayIntensidadColor[5] = intColor;
                        arrayPorcentaje[5] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-RM":
                        arrayIntensidadColor[6] = intColor;
                        arrayPorcentaje[6] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-LI":
                        arrayIntensidadColor[7] = intColor;
                        arrayPorcentaje[7] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-ML":
                        arrayIntensidadColor[8] = intColor;
                        arrayPorcentaje[8] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-BI":
                        arrayIntensidadColor[9] = intColor;
                        arrayPorcentaje[9] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-AR":
                        arrayIntensidadColor[10] = intColor;
                        arrayPorcentaje[10] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-LR":
                        arrayIntensidadColor[11] = intColor;
                        arrayPorcentaje[11] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-LL":
                        arrayIntensidadColor[12] = intColor;
                        arrayPorcentaje[12] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-AI":
                        arrayIntensidadColor[13] = intColor;
                        arrayPorcentaje[13] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;

                    case "CL-MA":
                        arrayIntensidadColor[14] = intColor;
                        arrayPorcentaje[14] = Math.Round((data.Total.Value * 100 / total), 2).ToString() + "%";
                        break;
                }
                intColor++;
                cantidadColores++;
                #endregion
            }

            string auxPropir = string.Format("{0:N0}", propir);
            auxPropir = auxPropir.Replace(",", ".");

            var json = new
            {
                dataCantColores = cantidadColores,
                dataIntColor = arrayIntensidadColor,
                dataPorcentaje = arrayPorcentaje,
                dataPROPIR = auxPropir
            };
            return this.Json(json, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDatosEnQueSeInvierte2(string id)
        {
            string[] datos = id.Split(';');
            string ids = datos[0];
            string formato = datos[1];

            if (!string.IsNullOrEmpty(ids))
            {
                List<Barra_amChart_HorizontalBar> barra = _context.Grafico_amChart_HorizontalBar.Find(16).Barra_amChart_HorizontalBar.Where(r => r.Idregion == ids).ToList();
                amChart_HorizontalBar amChart = new amChart_HorizontalBar();
                amChart.Load2(barra, formato);
                string json = amChart.getJson();
                return this.Content(json, "application/json");
            }
            else
            {
                Grafico_amChart_HorizontalBar barra = _context.Grafico_amChart_HorizontalBar.Find(16);
                amChart_HorizontalBar amChart = new amChart_HorizontalBar();
                amChart.Load2(barra, formato);
                string json = amChart.getJson();
                return this.Content(json, "application/json");
            }

            //amChart_HorizontalBar amChart = new amChart_HorizontalBar();
            //amChart.Load2(barra, formato);
            ////amChart_HorizontalBar amChart = new amChart_HorizontalBar();
            ////if (string.IsNullOrEmpty(id))
            ////{
            ////    amChart = amChart_HorizontalBar.Example3();
            ////}
            ////else
            ////{
            ////    amChart = amChart_HorizontalBar.Example4();
            ////}

            //string json = amChart.getJson();
            //return this.Content(json, "application/json");
        }
        public ActionResult GetDatosEnQueSeInvierte3(string id, string formato, string region)
        {
            string titulo = id;
            List<Tabla_amChart_HorizontalBar> infoGrafico = new List<Tabla_amChart_HorizontalBar>();
            List<Tabla_amChart_HorizontalBar> infoGraficoAux = new List<Tabla_amChart_HorizontalBar>();
            int max = 100;
            Grafico_amChart_HorizontalBar barra = _context.Grafico_amChart_HorizontalBar.Find(16);

            List<int> lista = new List<int>();
            if (!string.IsNullOrEmpty(region))
            {
                infoGraficoAux = barra.Barra_amChart_HorizontalBar.Where(r => r.Titulo == titulo).SelectMany(r => r.Tabla_amChart_HorizontalBar).ToList();
            }
            else
            {
                infoGraficoAux = barra.Barra_amChart_HorizontalBar.Where(r => r.Titulo == titulo).SelectMany(r => r.Tabla_amChart_HorizontalBar).ToList();
            }


            List<Tabla_amChart_HorizontalBar> vistafinal = new List<Tabla_amChart_HorizontalBar>();
            amChart_HorizontalBar amChart = new amChart_HorizontalBar();
            if (formato == "nominal")
            {
                var final = infoGraficoAux.GroupBy(r => new { r.Iniciativa }).Select(r => new { key = r.Key.Iniciativa, value = r.Sum(l => l.valorNominal.Value) }).OrderByDescending(r => r.value).Take(max).ToList();
                amChart.Load(final.ToDictionary(x => x.key, x => x.value), formato);
            }
            else
            {
                var final = infoGraficoAux.GroupBy(r => new { r.Iniciativa }).Select(r => new { key = r.Key.Iniciativa, value = r.Sum(l => l.valorPercapita.Value) }).OrderByDescending(r => r.value).Take(max).ToList();
                amChart.Load(final.ToDictionary(x => x.key, x => x.value), formato);
            }
            string json = amChart.getJsonTable();
            return this.Content(json, "application/json");
        }
        public ActionResult GetTituloPropirEnQueSeInvierte3(string tipoValor, string idCiudad, string optCategoria)
        {
            Grafico_amChart_HorizontalBar infoGrafico = _context.Grafico_amChart_HorizontalBar.Find(16);
            Barra_amChart_HorizontalBar Cuadrado = new Barra_amChart_HorizontalBar();

            double propir = 1;
            string prefijoValor = "";
            string valorCategoria = "";
            string textoSiRegion = "";

            try
            {
                if (tipoValor == "percapita")
                {
                    propir = infoGrafico.Barra_amChart_HorizontalBar.Sum(r => r.ValorPercapita).Value;
                    prefijoValor = "$";
                }
                else
                {
                    propir = infoGrafico.Barra_amChart_HorizontalBar.Sum(r => r.valor).Value;
                    prefijoValor = "MM$";
                }

            }
            catch { }

            double valor;
            if (!string.IsNullOrEmpty(optCategoria))
            {
                if (!string.IsNullOrEmpty(idCiudad))
                {
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad && r.Titulo == optCategoria).Sum(r => r.ValorPercapita).Value;
                        valorCategoria = "<br>CATEGORÍA: " + optCategoria + " " + prefijoValor + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad && r.Titulo == optCategoria).Sum(r => r.valor).Value;
                        valorCategoria = "<br>CATEGORÍA: " + optCategoria + " " + prefijoValor + string.Format("{0:N0}", valor);
                    }
                }
                else
                {
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Titulo == optCategoria).Sum(r => r.ValorPercapita).Value;
                        valorCategoria = "<br>CATEGORÍA: " + optCategoria + " " + prefijoValor + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Titulo == optCategoria).Sum(r => r.valor).Value;
                        valorCategoria = "<br>CATEGORÍA: " + optCategoria + " " + prefijoValor + string.Format("{0:N0}", valor);
                    }
                }
            }


            #region Cases
            switch (idCiudad)
            {
                case "CL-AP":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.ValorPercapita).Value;
                        textoSiRegion = "<br>REGION: ARICA " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.valor).Value;
                        textoSiRegion = "<br>REGION: ARICA " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-TA":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.ValorPercapita).Value;
                        textoSiRegion = "<br>REGION: TARAPACA " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.valor).Value;
                        textoSiRegion = "<br>REGION: TARAPACA " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-AN":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.ValorPercapita).Value;
                        textoSiRegion = "<br>REGION: ANTOFAGASTA " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.valor).Value;
                        textoSiRegion = "<br>REGION: ANTOFAGASTA " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-AT":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.ValorPercapita).Value;
                        textoSiRegion = "<br>REGION: ATACAMA " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.valor).Value;
                        textoSiRegion = "<br>REGION: ATACAMA " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-CO":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.ValorPercapita).Value;
                        textoSiRegion = "<br>REGION: COQUIMBO " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.valor).Value;
                        textoSiRegion = "<br>REGION: COQUIMBO " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-VS":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.ValorPercapita).Value;
                        textoSiRegion = "<br>REGION: VALPARAISO " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.valor).Value;
                        textoSiRegion = "<br>REGION: VALPARAISO " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-RM":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.ValorPercapita).Value;
                        textoSiRegion = "<br>REGION: SANTIAGO " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.valor).Value;
                        textoSiRegion = "<br>REGION: SANTIAGO " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-LI":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.ValorPercapita).Value;
                        textoSiRegion = "<br>REGION: OHIGGINS " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.valor).Value;
                        textoSiRegion = "<br>REGION: OHIGGINS " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-ML":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.ValorPercapita).Value;
                        textoSiRegion = "<br>REGION: MAULE " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.valor).Value;
                        textoSiRegion = "<br>REGION: MAULE " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-BI":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.ValorPercapita).Value;
                        textoSiRegion = "<br>REGION: BIOBIO " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.valor).Value;
                        textoSiRegion = "<br>REGION: BIOBIO " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-AR":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.ValorPercapita).Value;
                        textoSiRegion = "<br>REGION: ARAUCANIA " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.valor).Value;
                        textoSiRegion = "<br>REGION: ARAUCANIA " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-LR":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.ValorPercapita).Value;
                        textoSiRegion = "<br>REGION: LOS RIOS " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.valor).Value;
                        textoSiRegion = "<br>REGION: LOS RIOS " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-LL":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.ValorPercapita).Value;
                        textoSiRegion = "<br>REGION: LOS LAGOS " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.valor).Value;
                        textoSiRegion = "<br>REGION: LOS LAGOS " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-AI":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.ValorPercapita).Value;
                        textoSiRegion = "<br>REGION: AYSEN " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.valor).Value;
                        textoSiRegion = "<br>REGION: AYSEN " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;

                case "CL-MA":
                    if (tipoValor == "percapita")
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.ValorPercapita).Value;
                        textoSiRegion = "<br>REGION: MAGALLAES " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    else
                    {
                        valor = infoGrafico.Barra_amChart_HorizontalBar.Where(r => r.Idregion == idCiudad).Sum(r => r.valor).Value;
                        textoSiRegion = "<br>REGION: MAGALLAES " + prefijoValor + " " + string.Format("{0:N0}", valor);
                    }
                    break;
            }

            #endregion

            string nacionalPropir = "NACIONAL: " + prefijoValor + " " + string.Format("{0:N0}", propir);
            nacionalPropir = nacionalPropir.Replace(",", ".");
            textoSiRegion = textoSiRegion.Replace(",", ".");
            valorCategoria = valorCategoria.Replace(",", ".");

            if (valorCategoria == "")
            {
                valorCategoria = "<br /><br />";
                if (textoSiRegion == "")
                {
                    valorCategoria = valorCategoria + "<br /><br />";
                }
            }
            else
            {
                if (textoSiRegion == "")
                {
                    valorCategoria = valorCategoria + "<br /><br />";
                }
            }

            string cadenaFinal = nacionalPropir + " " + textoSiRegion + " " + valorCategoria;

            var json = new
            {
                dataPROPIR = cadenaFinal
            };

            return this.Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDatosMunicipio1(string id)
        {
            string tipoValor = id;
            string oli = TempData["tipo"] as string;
            if (oli != null)
            {
                if (oli == id)
                {
                    string json2 = TempData["datos"] as string;
                    TempData["datos"] = json2;
                    TempData["tipo"] = tipoValor;
                    return this.Content(json2, "application/json");
                }
            }

            #region VALOR PARA MOSTRAR
            //GraficoCircularesVarios infoGrafico = _context.GraficoCircularesVarios.Find(13);//
            //d3Object_Bubble_Pack grafico = new d3Object_Bubble_Pack();//
            //if (tipoValor == "percapita")//
            //{//
            //    grafico.Load(infoGrafico, "percapita");//
            //}//
            //else//
            //{//
            //    grafico.Load(infoGrafico, "nominal");//
            //}//
            //string json = grafico.getJson();//
            #endregion
            #region FINAL EN PRODUCCION
            string json = _context.Json.Find(2).JsonVistas.Single(r => r.Titulo == id).Texto;

            TempData["datos"] = json;
            TempData["tipo"] = tipoValor;

            //JsonVistas auxiliar = new JsonVistas();//
            //auxiliar.IdJsonFK = 2;//
            //auxiliar.Titulo = id;//
            //auxiliar.Texto = json;//
            //_context.JsonVistas.Add(auxiliar);//
            //_context.SaveChanges();//
            #endregion
            return this.Content(json, "application/json");
        }
        public ActionResult GetDatosMunicipio2()
        {
            List<DatosSoloUnoCirculosVarios> opciones1 = _context.DatosSoloUnoCirculosVarios.Where(r => r.IdGraficoFK == 13).OrderBy(r => r.numeroOpcion).ToList();
            List<DatosdupliCirculosVarios> opciones2 = _context.DatosdupliCirculosVarios.Where(r => r.IdGraficoFK == 13).OrderBy(r => r.numeroOpcion).ToList();

            int cantidad = opciones1.Count;
            string[] arrayId1 = new string[cantidad];
            string[] arrayNumOp1 = new string[cantidad];
            string[] arrayNombre1 = new string[cantidad];

            int cantidad2 = opciones2.Count;
            string[] arrayId2 = new string[cantidad2];
            string[] arrayNumOp2 = new string[cantidad2];
            string[] arrayNombre2 = new string[cantidad2];

            int count = 0;
            foreach (DatosSoloUnoCirculosVarios opcion1 in opciones1)
            {
                arrayId1[count] = opcion1.IdDatos.ToString();
                arrayNumOp1[count] = opcion1.numeroOpcion.ToString();
                arrayNombre1[count] = opcion1.Titulo.ToString();
                count++;
            }

            count = 0;
            foreach (DatosdupliCirculosVarios opcion2 in opciones2)
            {
                arrayId2[count] = opcion2.IdDatos.ToString();
                arrayNumOp2[count] = opcion2.numeroOpcion.ToString();
                arrayNombre2[count] = opcion2.Titulo.ToString();
                count++;
            }


            var datosOp1 = new
            {
                ids = arrayId1,
                nombre = arrayNombre1,
                numeroOpcion = arrayNumOp1,
                elementos = cantidad
            };


            var datosOp2 = new
            {
                ids = arrayId2,
                nombre = arrayNombre2,
                numeroOpcion = arrayNumOp2,
                elementos = cantidad2
            };

            var json = new
            {
                opcion1 = datosOp1,
                opcion2 = datosOp2
            };

            return this.Json(json, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetDatosComoGastaTuMunicipio2(string id, string busqueda)
        {

            if (string.IsNullOrEmpty(busqueda) || busqueda == "")
                busqueda = "Total";

            string json = _context.Json.Find(1).JsonVistas.Single(r => r.Titulo == busqueda).Texto;

           // Grafico_Plotly_Listbox infoGrafico = _context.Grafico_Plotly_Listbox.Find(4);//
           // plotly_Line_Listbox grafico = new plotly_Line_Listbox();//

           //grafico.Load(infoGrafico, busqueda);//
           // string json = grafico.getJson();//

           // JsonVistas auxiliar = new JsonVistas();//
           // auxiliar.IdJsonFK = 1;//
           // auxiliar.Titulo = busqueda;//
           // auxiliar.Texto = json;//
           // _context.JsonVistas.Add(auxiliar);//
           // _context.SaveChanges();//

            return this.Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult ConsultaApp(string sueldo, string sexo, string tipo, string alcohol, string bebida, string fuma, string auto)
        {
            int sueldo1 = int.Parse(sueldo);
            bool sexo1 = (sexo == "0") ? false : true;
            decimal tipo1 = Convert.ToDecimal(tipo);
            decimal alcohol1 = Convert.ToDecimal(alcohol);
            decimal bebida1 = Convert.ToDecimal(bebida);
            decimal fuma1 = Convert.ToDecimal(fuma);
            decimal auto1 = Convert.ToDecimal(auto);
            var item = _context.DatosAPP.FirstOrDefault(r => r.Ingreso == sueldo1 &&
                                                        r.Genero == sexo1 &&
                                                        r.Personaje == tipo1 &&
                                                        r.Alcoholicas == alcohol1 &&
                                                        r.Analcoholicas == bebida1 &&
                                                        r.Tabaco == fuma1 &&
                                                        r.Combustitble == auto1
                                                        );
            List<string> lista = new List<string>();
            lista.Add("Servicios Públicos Generales");
            lista.Add("Defensa");
            lista.Add("Orden Público y Seguridad");
            lista.Add("Asuntos Económicos");
            lista.Add("Protección del Medio Ambiente");
            lista.Add("Vivienda y Servicios Comunitarios");
            lista.Add("Salud");
            lista.Add("Actividades Recreativas, Cultura y Religión");
            lista.Add("Educación");
            lista.Add("Protección Social");
            if (item != null)
            {
                List<int> listaTODO = new List<int>();
                var item2 = _context.DatosApp2.ToList();
                #region COMENTADO
                //int app1  = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Protección Social").First().valorCategoria);
                //int app2  = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Asuntos Económicos").First().valorCategoria);
                //int app3  = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Defensa").First().valorCategoria);
                //int app4  = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Protección del Medio Ambiente").First().valorCategoria);
                //int app5  = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Vivienda y Servicios Comunitarios").First().valorCategoria);
                //int app6  = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Salud").First().valorCategoria);
                //int app7  = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Servicios Públicos Generales").First().valorCategoria);
                //int app8  = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Orden Público y Seguridad").First().valorCategoria);
                //int app9  = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Educación").First().valorCategoria);
                //int app10 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Actividades Recreativas, Cultura y Religión").First().valorCategoria);

                //int datoA1S1 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Protección Social").First().valorCategoria * item2.Where(r => r.subcategoria == "Desempleo").First().valorsubcategoria);
                //int datoA1S2 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Protección Social").First().valorCategoria * item2.Where(r => r.subcategoria == "Edad Avanzada").First().valorsubcategoria); 
                //int datoA1S3 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Protección Social").First().valorCategoria * item2.Where(r => r.subcategoria == "Enfermedad e Incapacidad").First().valorsubcategoria); 
                //int datoA1S4 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Protección Social").First().valorCategoria * item2.Where(r => r.subcategoria == "Familia e Hijos").First().valorsubcategoria); 

                //int datoA2S1 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Asuntos Económicos").First().valorCategoria * item2.Where(r => r.subcategoria == "Agricultura, Silvicultura, Pesca y Caza").First().valorsubcategoria); 
                //int datoA2S2 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Asuntos Económicos").First().valorCategoria * item2.Where(r => r.subcategoria == "Asuntos Económicos, Comerciales y Laborales en General").First().valorsubcategoria); 
                //int datoA2S3 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Asuntos Económicos").First().valorCategoria * item2.Where(r => r.subcategoria == "Asuntos Económicos n.e.p.").First().valorsubcategoria); 
                //int datoA2S4 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Asuntos Económicos").First().valorCategoria * item2.Where(r => r.subcategoria == "Combustibles y Energía").First().valorsubcategoria); 
                //int datoA2S5 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Asuntos Económicos").First().valorCategoria * item2.Where(r => r.subcategoria == "Comunicaciones").First().valorsubcategoria); 
                //int datoA2S6 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Asuntos Económicos").First().valorCategoria * item2.Where(r => r.subcategoria == "Investigación y Desarrollo relacionados con Asuntos Económicos").First().valorsubcategoria); 
                //int datoA2S7 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Asuntos Económicos").First().valorCategoria * item2.Where(r => r.subcategoria == "Minería, Manufacturas y Construcción").First().valorsubcategoria); 
                //int datoA2S8 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Asuntos Económicos").First().valorCategoria * item2.Where(r => r.subcategoria == "Otras Industrias").First().valorsubcategoria); 
                //int datoA2S9 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Asuntos Económicos").First().valorCategoria * item2.Where(r => r.subcategoria == "Transporte").First().valorsubcategoria); 

                //int datoA3S1 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Defensa").First().valorCategoria * item2.Where(r => r.subcategoria == "Defensa Militar").First().valorsubcategoria); 
                //int datoA3S2 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Defensa").First().valorCategoria * item2.Where(r => r.subcategoria == "Investigación y Desarrollo relacionados con la Defensa").First().valorsubcategoria); 

                //int datoA4S1 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Protección del Medio Ambiente").First().valorCategoria * item2.Where(r => r.subcategoria == "Protección a la diversidad Biológica y del Paisaje").First().valorsubcategoria); 
                //int datoA4S2 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Protección del Medio Ambiente").First().valorCategoria * item2.Where(r => r.subcategoria == "Protección del Medio Ambiente n.e.p.").First().valorsubcategoria); 
                //int datoA4S3 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Protección del Medio Ambiente").First().valorCategoria * item2.Where(r => r.subcategoria == "Reducción de la Contaminación").First().valorsubcategoria); 

                //int datoA5S1 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Vivienda y Servicios Comunitarios").First().valorCategoria * item2.Where(r => r.subcategoria == "Abastecimiento de Agua").First().valorsubcategoria); 
                //int datoA5S2 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Vivienda y Servicios Comunitarios").First().valorCategoria * item2.Where(r => r.subcategoria == "Desarrollo Comunitario").First().valorsubcategoria); 
                //int datoA5S3 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Vivienda y Servicios Comunitarios").First().valorCategoria * item2.Where(r => r.subcategoria == "Urbanización").First().valorsubcategoria); 
                //int datoA5S4 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Vivienda y Servicios Comunitarios").First().valorCategoria * item2.Where(r => r.subcategoria == "Vivienda y Servicios Comunitarios n.e.p.").First().valorsubcategoria); 

                //int datoA6S1 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Salud").First().valorCategoria * item2.Where(r => r.subcategoria == "Productos, Útiles y Equipos Médicos").First().valorsubcategoria); 
                //int datoA6S2 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Salud").First().valorCategoria * item2.Where(r => r.subcategoria == "Salud n.e.p.").First().valorsubcategoria); 
                //int datoA6S3 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Salud").First().valorCategoria * item2.Where(r => r.subcategoria == "Servicios de Salud Pública").First().valorsubcategoria); 
                //int datoA6S4 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Salud").First().valorCategoria * item2.Where(r => r.subcategoria == "Servicios Hospitalarios").First().valorsubcategoria); 
                //int datoA6S5 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Salud").First().valorCategoria * item2.Where(r => r.subcategoria == "Servicios para Pacientes Externos").First().valorsubcategoria); 

                //int datoA7S1 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Servicios Públicos Generales").First().valorCategoria * item2.Where(r => r.subcategoria == "Ayuda Económica Exterior").First().valorsubcategoria); 
                //int datoA7S2 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Servicios Públicos Generales").First().valorCategoria * item2.Where(r => r.subcategoria == "Investigación Básica").First().valorsubcategoria); 
                //int datoA7S3 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Servicios Públicos Generales").First().valorCategoria * item2.Where(r => r.subcategoria == "Servicios Generales").First().valorsubcategoria); 
                //int datoA7S4 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Servicios Públicos Generales").First().valorCategoria * item2.Where(r => r.subcategoria == "Servicios Públicos Generales n.e.p.").First().valorsubcategoria); 

                //int datoA8S1 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Orden Público y Seguridad").First().valorCategoria * item2.Where(r => r.subcategoria == "Orden Público y Seguridad n.e.p.").First().valorsubcategoria); 
                //int datoA8S2 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Orden Público y Seguridad").First().valorCategoria * item2.Where(r => r.subcategoria == "Prisiones").First().valorsubcategoria); 
                //int datoA8S3 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Orden Público y Seguridad").First().valorCategoria * item2.Where(r => r.subcategoria == "Servicios de Policía").First().valorsubcategoria); 
                //int datoA8S4 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Orden Público y Seguridad").First().valorCategoria * item2.Where(r => r.subcategoria == "Servicios de Protección contra Incendios").First().valorsubcategoria); 
                //int datoA8S5 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Orden Público y Seguridad").First().valorCategoria * item2.Where(r => r.subcategoria == "Tribunales de Justicia").First().valorsubcategoria); 

                //int datoA9S1 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Educación").First().valorCategoria * item2.Where(r => r.subcategoria == "Enseñanza n.e.p.").First().valorsubcategoria); 
                //int datoA9S2 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Educación").First().valorCategoria * item2.Where(r => r.subcategoria == "Enseñanza no atribuible a ningun nivel").First().valorsubcategoria); 
                //int datoA9S3 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Educación").First().valorCategoria * item2.Where(r => r.subcategoria == "Enseñanza Preescolar, Primaria y Secundaria").First().valorsubcategoria); 
                //int datoA9S4 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Educación").First().valorCategoria * item2.Where(r => r.subcategoria == "Enseñanza Terciaria").First().valorsubcategoria); 
                //int datoA9S5 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Educación").First().valorCategoria * item2.Where(r => r.subcategoria == "Servicios Auxiliares de la Educación").First().valorsubcategoria); 

                //int datoA10S1 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Actividades Recreativas, Cultura y Religión").First().valorCategoria * item2.Where(r => r.subcategoria == "Servicios Culturales").First().valorsubcategoria); 
                //int datoA10S2 = Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Actividades Recreativas, Cultura y Religión").First().valorCategoria * item2.Where(r => r.subcategoria == "Servicios Recreativos y Deportivos").First().valorsubcategoria);
                #endregion
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Protección Social").First().valorCategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Asuntos Económicos").First().valorCategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Defensa").First().valorCategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Protección del Medio Ambiente").First().valorCategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Vivienda y Servicios Comunitarios").First().valorCategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Salud").First().valorCategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Servicios Públicos Generales").First().valorCategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Orden Público y Seguridad").First().valorCategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Educación").First().valorCategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Actividades Recreativas, Cultura y Religión").First().valorCategoria));

                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Protección Social").First().valorCategoria * item2.Where(r => r.subcategoria == "Desempleo").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Protección Social").First().valorCategoria * item2.Where(r => r.subcategoria == "Edad Avanzada").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Protección Social").First().valorCategoria * item2.Where(r => r.subcategoria == "Enfermedad e Incapacidad").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Protección Social").First().valorCategoria * item2.Where(r => r.subcategoria == "Familia e Hijos").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Protección Social").First().valorCategoria * item2.Where(r => r.subcategoria == "Vivienda").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Protección Social").First().valorCategoria * item2.Where(r => r.subcategoria == "Exclusión Social").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Protección Social").First().valorCategoria * item2.Where(r => r.subcategoria == "Investigación y Desarrollo relacionados con Protección Social").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Protección Social").First().valorCategoria * item2.Where(r => r.subcategoria == "Protección Social n.e.p").First().valorsubcategoria));

                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Asuntos Económicos").First().valorCategoria * item2.Where(r => r.subcategoria == "Agricultura, Silvicultura, Pesca y Caza").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Asuntos Económicos").First().valorCategoria * item2.Where(r => r.subcategoria == "Asuntos Económicos, Comerciales y Laborales en General").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Asuntos Económicos").First().valorCategoria * item2.Where(r => r.subcategoria == "Asuntos Económicos n.e.p.").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Asuntos Económicos").First().valorCategoria * item2.Where(r => r.subcategoria == "Combustibles y Energía").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Asuntos Económicos").First().valorCategoria * item2.Where(r => r.subcategoria == "Comunicaciones").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Asuntos Económicos").First().valorCategoria * item2.Where(r => r.subcategoria == "Investigación y Desarrollo relacionados con Asuntos Económicos").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Asuntos Económicos").First().valorCategoria * item2.Where(r => r.subcategoria == "Minería, Manufacturas y Construcción").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Asuntos Económicos").First().valorCategoria * item2.Where(r => r.subcategoria == "Otras Industrias").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Asuntos Económicos").First().valorCategoria * item2.Where(r => r.subcategoria == "Transporte").First().valorsubcategoria));

                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Defensa").First().valorCategoria * item2.Where(r => r.subcategoria == "Defensa Militar").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Defensa").First().valorCategoria * item2.Where(r => r.subcategoria == "Investigación y Desarrollo relacionados con la Defensa").First().valorsubcategoria));

                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Protección del Medio Ambiente").First().valorCategoria * item2.Where(r => r.subcategoria == "Protección a la diversidad Biológica y del Paisaje").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Protección del Medio Ambiente").First().valorCategoria * item2.Where(r => r.subcategoria == "Protección del Medio Ambiente n.e.p.").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Protección del Medio Ambiente").First().valorCategoria * item2.Where(r => r.subcategoria == "Reducción de la Contaminación").First().valorsubcategoria));

                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Vivienda y Servicios Comunitarios").First().valorCategoria * item2.Where(r => r.subcategoria == "Abastecimiento de Agua").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Vivienda y Servicios Comunitarios").First().valorCategoria * item2.Where(r => r.subcategoria == "Desarrollo Comunitario").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Vivienda y Servicios Comunitarios").First().valorCategoria * item2.Where(r => r.subcategoria == "Urbanización").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Vivienda y Servicios Comunitarios").First().valorCategoria * item2.Where(r => r.subcategoria == "Vivienda y Servicios Comunitarios n.e.p.").First().valorsubcategoria));

                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Salud").First().valorCategoria * item2.Where(r => r.subcategoria == "Productos, Útiles y Equipos Médicos").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Salud").First().valorCategoria * item2.Where(r => r.subcategoria == "Salud n.e.p.").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Salud").First().valorCategoria * item2.Where(r => r.subcategoria == "Servicios de Salud Pública").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Salud").First().valorCategoria * item2.Where(r => r.subcategoria == "Servicios Hospitalarios").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Salud").First().valorCategoria * item2.Where(r => r.subcategoria == "Servicios para Pacientes Externos").First().valorsubcategoria));

                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Servicios Públicos Generales").First().valorCategoria * item2.Where(r => r.subcategoria == "Ayuda Económica Exterior").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Servicios Públicos Generales").First().valorCategoria * item2.Where(r => r.subcategoria == "Investigación Básica").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Servicios Públicos Generales").First().valorCategoria * item2.Where(r => r.subcategoria == "Servicios Generales").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Servicios Públicos Generales").First().valorCategoria * item2.Where(r => r.subcategoria == "Servicios Públicos Generales n.e.p.").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Servicios Públicos Generales").First().valorCategoria * item2.Where(r => r.subcategoria == "Organos Ejecutivos y Legislativos, Asuntos Financieros,Fiscales y Exteriores").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Servicios Públicos Generales").First().valorCategoria * item2.Where(r => r.subcategoria == "Transacciones de la Deuda Pública").First().valorsubcategoria));

                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Orden Público y Seguridad").First().valorCategoria * item2.Where(r => r.subcategoria == "Orden Público y Seguridad n.e.p.").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Orden Público y Seguridad").First().valorCategoria * item2.Where(r => r.subcategoria == "Prisiones").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Orden Público y Seguridad").First().valorCategoria * item2.Where(r => r.subcategoria == "Servicios de Policía").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Orden Público y Seguridad").First().valorCategoria * item2.Where(r => r.subcategoria == "Servicios de Protección contra Incendios").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Orden Público y Seguridad").First().valorCategoria * item2.Where(r => r.subcategoria == "Tribunales de Justicia").First().valorsubcategoria));

                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Educación").First().valorCategoria * item2.Where(r => r.subcategoria == "Enseñanza n.e.p.").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Educación").First().valorCategoria * item2.Where(r => r.subcategoria == "Enseñanza no atribuible a ningun nivel").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Educación").First().valorCategoria * item2.Where(r => r.subcategoria == "Enseñanza Preescolar, Primaria y Secundaria").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Educación").First().valorCategoria * item2.Where(r => r.subcategoria == "Enseñanza Terciaria").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Educación").First().valorCategoria * item2.Where(r => r.subcategoria == "Servicios Auxiliares de la Educación").First().valorsubcategoria));

                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Actividades Recreativas, Cultura y Religión").First().valorCategoria * item2.Where(r => r.subcategoria == "Servicios Culturales").First().valorsubcategoria));
                listaTODO.Add(Convert.ToInt32(Convert.ToDouble(item.TotalImpuesto) * item2.Where(r => r.Categoria == "Actividades Recreativas, Cultura y Religión").First().valorCategoria * item2.Where(r => r.subcategoria == "Servicios Recreativos y Deportivos").First().valorsubcategoria));

                int valorfinal = Convert.ToInt32(item.TotalImpuesto);
                int valorfinal2 = Convert.ToInt32(item.ImpRenta);
                int valorfinal3 = valorfinal - valorfinal2;// Convert.ToInt32(item.ImpConsumo);
                return Json(new { valor1 = valorfinal, valor2 = valorfinal2, valor3 = valorfinal3, valor4 = listaTODO });
            }
            return Json("Por favor complete los datos.");
        }

        public ActionResult GetDatosAPPMunicipalidades(int valor1, int valor2, int valor3, int valor4, int valor5)
        {
            RegistroAppMunicipal itemes = new RegistroAppMunicipal();
            itemes.Educacion = valor1;
            itemes.Interno = valor3;
            itemes.Cultural = valor4;
            itemes.Salud = valor2;
            itemes.Comunidad = valor5;
            itemes.Ip = Request.UserHostAddress;
            _context.RegistroAppMunicipal.Add(itemes);
            _context.SaveChanges();
            List<AppMunicipalidad> lista = _context.AppMunicipalidad.ToList();
            int errormenoriD = 99999999;
            int IdMenor = 0;
            foreach (var item in lista)
            {
                int aux1 = Math.Abs(item.AEducacion.Value - valor1);
                int aux2 = Math.Abs(item.ASalud.Value - valor2);
                int aux3 = Math.Abs(item.AInterno.Value - valor3);
                int aux4 = Math.Abs(item.ACultural.Value - valor4);
                int aux5 = Math.Abs(item.AComunidad.Value - valor5);
                aux1 = aux1 * aux1;
                aux2 = aux2 * aux2;
                aux3 = aux3 * aux3;
                aux4 = aux4 * aux4;
                aux5 = aux5 * aux5;
                int promedio = Convert.ToInt32((aux1 + aux2 + aux3 + aux4 + aux5) / 5);
                if (errormenoriD > promedio)
                {
                    errormenoriD = promedio;
                    IdMenor = item.IdAppMunicipalidad;
                }
            }
            if (errormenoriD < 26)
            {
                AppMunicipalidad valor = lista.Single(r => r.IdAppMunicipalidad == IdMenor);
                int Suma = valor.BEducacion.Value + valor.BComunidad.Value + valor.BCultural.Value + valor.BInterno.Value + valor.BSalud.Value;
                var enviar = new
                {
                    Nombre = valor.NombreComuna,
                    Educacion = valor.AEducacion,
                    Interno = valor.AInterno,
                    Cultural = valor.ACultural,
                    Salud = valor.ASalud,
                    Comunidad = valor.AComunidad,
                    Poblacion = valor.Poblacion,
                    Alcalde = valor.Alcalde,
                    Partido = valor.Partido,
                    Votos = valor.Votos,
                    Reeleccion = valor.Reelecciones,
                    Pobreza = valor.PobrezaPorciento,
                    Distancia = valor.Distancia,
                    Total = Suma
                };
                return this.Json(enviar, JsonRequestBehavior.AllowGet);
            }
            return this.Json(false, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SetAPPMunicipal(int valor1, int valor2, int valor3, int valor4, int valor5, string valorC)
        {
            RegistroAppMunicipal item = new RegistroAppMunicipal();
            item.Educacion = valor1;
            item.Interno = valor3;
            item.Cultural = valor4;
            item.Salud = valor2;
            item.Comunidad = valor5;
            item.NombreComuna = valorC;
            item.Ip = Request.UserHostAddress;
            _context.RegistroAppMunicipal.Add(item);
            _context.SaveChanges();
            return this.Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult formulario1(string IDVISTA, int VAL)
        {
            string ip = Request.UserHostAddress;
            int valor = Convert.ToInt32(IDVISTA);
            DateTime auxfecha = DateTime.Now.AddDays(-1);
            FeedSocial hola = _context.FeedSocial.SingleOrDefault(r => r.Ipp == ip && r.IdSeccionesFK == valor && r.Fecha > auxfecha);
            if (hola == null)
            {
                hola = new FeedSocial();
                hola.Ipp = ip.ToString();
                hola.IdSeccionesFK = valor;
                hola.Fecha = DateTime.Now;
                hola.SiNo = Convert.ToBoolean(VAL);
                _context.FeedSocial.Add(hola);

                Secciones sec = _context.Secciones.Find(int.Parse(IDVISTA));
                sec.Megusta = sec.Megusta + 1;
                _context.Entry(sec).State = EntityState.Modified;
                _context.SaveChanges();
                return this.Json(true, JsonRequestBehavior.AllowGet);
            }
            return this.Json(false, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult formulario2(string IDVISTA, int VAL)
        {
            string ip = Request.UserHostAddress;
            int valor = Convert.ToInt32(IDVISTA);
            DateTime auxfecha = DateTime.Now.AddDays(-1);
            FeedSocial hola = _context.FeedSocial.SingleOrDefault(r => r.Ipp == ip && r.IdSeccionesFK == valor && r.Fecha > auxfecha);
            if (hola != null)
            {
                hola.PorQue = VAL;
                _context.Entry(hola).State = EntityState.Modified;
                _context.SaveChanges();
                return this.Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(false, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult formulario4(string IDVISTA, string SUG)
        {
            string ip = Request.UserHostAddress;
            int valor = Convert.ToInt32(IDVISTA);
            DateTime auxfecha = DateTime.Now.AddDays(-1);
            FeedSocial hola = _context.FeedSocial.SingleOrDefault(r => r.Ipp == ip && r.IdSeccionesFK == valor && r.Fecha > auxfecha);
            if (hola != null)
            {
                hola.Texto = SUG;
                _context.Entry(hola).State = EntityState.Modified;
                _context.SaveChanges();
                return this.Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult formulario3(string IDVISTA, string SUG)
        {
            string ip = Request.UserHostAddress;
            int valor = Convert.ToInt32(IDVISTA);
            DateTime auxfecha = DateTime.Now.AddDays(-1);
            FeedSocial2 hola = _context.FeedSocial2.SingleOrDefault(r => r.Ip == ip && r.IdSeccionesFK == valor && r.Fecha > auxfecha);
            if (hola == null)
            {
                hola = new FeedSocial2();
                hola.Ip = ip.ToString();
                hola.Otros = SUG;
                hola.IdSeccionesFK = valor;
                hola.Fecha = DateTime.Now;
                _context.FeedSocial2.Add(hola);
                _context.SaveChanges();

            }
            else
            {
                return this.Json(false, JsonRequestBehavior.AllowGet);
            }
            return this.Json(true, JsonRequestBehavior.AllowGet);
        }



    }
}
