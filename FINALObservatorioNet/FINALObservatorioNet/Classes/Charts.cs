using System;
using System.Collections.Generic;
using System.Linq;
using FINALObservatorioNet.Models;
using System.Threading.Tasks;

namespace FINALObservatorioNet.Classes
{
    public class plotly_Line
    {
        public plotly_Line()
        {
            ejeX = new List<string>();
            series = new List<Serie>();
            serieMasLarga = 0;
            layoutDolar = @"""layout"": {
                                        ""autosize"": true,
                                            ""showlegend"": false,
                                            ""paper_bgcolor"":""rgba(0, 0, 0, 0)"",
                                            ""plot_bgcolor"": ""rgba(0, 0, 0, 0)""," +
                                             //""legend"":{ +
                                             //""xanchor"": ""center"",
                                             //                ""yanchor"": ""top"",
                                             //                ""y"": -0.3, 
                                             //                ""x"": 0.5
                                             //            },
                                             @"""xaxis"": {
                                                    ""gridcolor"": ""#F1F1F1"",
                                                    ""autorange"":true,
                                                    ""fixedrange"": true,
                                                    ""exponentformat"": ""none"",
                                                    ""showexponent"": ""All""
                                             
                                                },
                                              ""yaxis"": {
                                                    ""gridcolor"": ""#AAA9A9"",
                                                    ""autorange"":true,
                                                    ""fixedrange"": true,
                                                    ""exponentformat"": ""none"",
                                                    ""tickfont"": {                                                              
                                                              ""size"": ""10""                                                             
                                                            },
                                                    ""tickprefix"": ""MM$""
                                                  
                                                  },
                                                ""separators"":"",.""
                                    }
                                ,";

            layoutPercent = @"""layout"": {
                                        ""autosize"": true,
                                            ""showlegend"": false,
                                            ""paper_bgcolor"":""rgba(0, 0, 0, 0)"",
                                            ""plot_bgcolor"": ""rgba(0, 0, 0, 0)""," +
                                              //""legend"":{
                                              //""xanchor"": ""center"",
                                              //                ""yanchor"": ""top"",
                                              //                ""y"": -0.3, 
                                              //                ""x"": 0.5
                                              //            },
                                              @"""xaxis"": {
                                                    ""gridcolor"": ""#F1F1F1"",
                                                    ""autorange"":true,
                                                    ""exponentformat"": ""none"",
                                                    ""showexponent"": ""All""
                                                },
                                              ""yaxis"": {
                                                    ""gridcolor"": ""#AAA9A9"",
                                                    ""autorange"":true,
                                                    ""exponentformat"":""none"",                                                      
                                                    ""ticksuffix"":""%""
                                                },
                                               ""separators"":"",.""
                                    }
                                 ,";
        }
        List<string> ejeX { get; set; }
        List<Serie> series { get; set; }
        public int serieMasLarga { get; set; }
        public int formatter { get; set; }
        private string layoutDolar { get; set; }
        private string layoutPercent { get; set; }

        public void addEjeX(List<string> ejeX)
        {
            this.ejeX = ejeX;
        }
        public void addSerie(string nombre, string id, string color, List<string> serie, string Visible, List<string> años)
        {
            Serie datos = new Serie();
            datos.name = nombre;
            datos.uid = id;
            datos.datosStringSerie = serie;
            datos.IsInt = false;
            datos.IsDouble = false;
            datos.color = color;
            datos.visible = Visible;
            datos.años = años;
            series.Add(datos);

            if (serie.Count > serieMasLarga)
                serieMasLarga = serie.Count;

        }
        public void addSerie(string nombre, string id, string color, List<double> serie, string Visible, List<string> años)
        {
            Serie datos = new Serie();
            datos.name = nombre;
            datos.uid = id;
            datos.datosDoubleSerie = serie;
            datos.isString = false;
            datos.IsInt = false;
            datos.color = color;
            datos.visible = Visible;
            datos.años = años;
            series.Add(datos);

            if (serie.Count > serieMasLarga)
                serieMasLarga = serie.Count;
        }
        public void addSerie(string nombre, string id, string color, List<long> serie, string Visible, List<string> años)
        {
            Serie datos = new Serie();
            datos.name = nombre;
            datos.uid = id;
            datos.datosIntSerie = serie;
            datos.isString = false;
            datos.IsDouble = false;
            datos.color = color;
            datos.visible = Visible;
            datos.años = años;
            series.Add(datos);

            if (serie.Count > serieMasLarga)
                serieMasLarga = serie.Count;
        }
        public string getJson()
        {
            string json = @"{ ""data"": [";
            string visibles = @" ""visibles"": [";
            List<string> linea = new List<string>();

            string auxX = @"{ ""x"": [";

            foreach (string datoX in ejeX)
            {
                auxX += datoX + @",";
            }
            auxX = auxX.Substring(0, auxX.Length - 1);
            auxX += @"],";

            string auxY = "";

            foreach (Serie serie in series)
            {
                auxY = @" ""visible"":" + serie.visible + @", 
                          ""y"": [";
                visibles += serie.visible.ToString().ToLower() + @",";

                int positionAux = 0;

                for (int i = 0; i < serieMasLarga; i++)
                {
                    string auxStringAño = ejeX[i];
                    if (serie.años.Contains(auxStringAño))
                    {
                        try
                        {
                            if (serie.isString)
                            {
                                auxY += serie.datosStringSerie[i - positionAux] + @",";
                            }
                            if (serie.IsDouble)
                            {
                                auxY += serie.datosDoubleSerie[i - positionAux] + @",";
                            }
                            if (serie.IsInt)
                            {
                                auxY += serie.datosIntSerie[i - positionAux] + @",";
                            }
                        }
                        catch { auxY += @"null" + @","; }
                    }
                    else
                    {
                        auxY += @"null" + @",";
                        positionAux++;
                    }
                }

                auxY = auxY.Substring(0, auxY.Length - 1) + @"],";
                auxY += @"""type"": ""scatter"",";
                auxY += @"""name"":" + @"""" + serie.name + @"""" + @",";
                auxY += @"""line"": { ""color"":" + @"""" + serie.color + @"""" + @" } ,";
                auxY += @"""connectgaps"":true,";
                auxY += @"""uid"":" + @"""" + serie.uid + @"""" + @"},";
                linea.Add(auxX + auxY);
            }

            string auxLinea = linea[linea.Count - 1];
            auxLinea = auxLinea.Substring(0, auxLinea.Length - 1);
            linea[linea.Count - 1] = auxLinea + @"],";

            visibles = visibles.Substring(0, visibles.Length - 1) + @"]}";


            foreach (string serie in linea)
            {
                json += serie;
            }

            if (formatter == 2)
            {
                json += layoutPercent;
            }
            else
            {
                json += layoutDolar;
            }

            json += visibles;

            return json;
        }
        public void Load(Grafico_Plotly_Line plotly, string tipo, string tipoRadio)
        {
            plotly_Line data = new plotly_Line();
            List<string> años = new List<string>();
            if (tipo == "porcentaje")
            {
                formatter = 2;
                if (tipoRadio == "nominal")
                {
                    foreach (Lineas_Grafico_Plotly_Line linea in plotly.Lineas_Grafico_Plotly_Line)
                    {
                        List<string> añoss = linea.Datos_Grafico_Plotly_Line.Select(r => r.Ano).ToList();
                        añoss = añoss.OrderBy(r => r).ToList();
                        años.AddRange(añoss);
                        addSerie(linea.Titulo, linea.IdLinea.ToString(), linea.Color, linea.Datos_Grafico_Plotly_Line.Select(r => r.ValorPorcentaje.Value).ToList(), linea.Visible.Value.ToString().ToLower(), añoss);
                    }
                }
                else
                {
                    foreach (Lineas_Grafico_Plotly_Line linea in plotly.Lineas_Grafico_Plotly_Line)
                    {
                        List<string> añoss = linea.Datos_Grafico_Plotly_Line.Select(r => r.Ano).ToList();
                        añoss = añoss.OrderBy(r => r).ToList();
                        años.AddRange(añoss);
                        addSerie(linea.Titulo, linea.IdLinea.ToString(), linea.Color, linea.Datos_Grafico_Plotly_Line.Select(r => r.ValorPorcentajeCapital.Value).ToList(), linea.Visible.Value.ToString().ToLower(), añoss);
                    }
                }

            }

            if (tipo == "peso")
            {
                formatter = 1;
                if (tipoRadio == "nominal")
                {
                    foreach (Lineas_Grafico_Plotly_Line linea in plotly.Lineas_Grafico_Plotly_Line)
                    {
                        List<string> añoss = linea.Datos_Grafico_Plotly_Line.Select(r => r.Ano).ToList();
                        añoss = añoss.OrderBy(r => r).ToList();
                        años.AddRange(añoss);
                        addSerie(linea.Titulo, linea.IdLinea.ToString(), linea.Color, linea.Datos_Grafico_Plotly_Line.Select(r => r.ValorPeso.Value).ToList(), linea.Visible.Value.ToString().ToLower(), añoss);
                    }
                }
                else
                {
                    foreach (Lineas_Grafico_Plotly_Line linea in plotly.Lineas_Grafico_Plotly_Line)
                    {
                        List<string> añoss = linea.Datos_Grafico_Plotly_Line.Select(r => r.Ano).ToList();
                        añoss = añoss.OrderBy(r => r).ToList();
                        años.AddRange(añoss);
                        addSerie(linea.Titulo, linea.IdLinea.ToString(), linea.Color, linea.Datos_Grafico_Plotly_Line.Select(r => r.ValorCapital.Value.ToString()).ToList(), linea.Visible.Value.ToString().ToLower(), añoss);
                    }
                }
            }

            años = años.Distinct().ToList();
            addEjeX(años);
        }
        public static plotly_Line Example()
        {
            List<string> ejeX = new List<string>();
            ejeX.Add("2010");
            ejeX.Add("2011");
            ejeX.Add("2012");
            ejeX.Add("2013");
            ejeX.Add("2014");
            ejeX.Add("2015");

            List<long> serie1 = new List<long>();
            serie1.Add(1800000);
            serie1.Add(1900000);
            serie1.Add(1850000);
            serie1.Add(2000000);
            serie1.Add(2100000);
            serie1.Add(2300000);

            List<long> serie2 = new List<long>();
            serie2.Add(1200000);
            serie2.Add(1100000);
            serie2.Add(2500000);
            serie2.Add(2600000);
            serie2.Add(2900000);
            serie2.Add(3100000);

            List<long> serie3 = new List<long>();
            serie3.Add(1600000);
            serie3.Add(1400000);
            serie3.Add(2300000);
            serie3.Add(2500000);
            serie3.Add(2500000);
            serie3.Add(3800000);

            List<long> serie4 = new List<long>();
            serie4.Add(1800000);
            serie4.Add(1600000);
            serie4.Add(2100000);
            serie4.Add(2200000);
            serie4.Add(2700000);
            serie4.Add(3500000);

            List<long> serie5 = new List<long>();
            serie5.Add(1400000);
            serie5.Add(1800000);
            serie5.Add(2800000);
            serie5.Add(2200000);
            serie5.Add(2100000);
            serie5.Add(3000000);

            plotly_Line ejemplo = new plotly_Line();
            //ejemplo.addEjeX(ejeX);
            //ejemplo.addSerie("MINISTERIO DE OBRAS PUBLICAS", "99da6d", "rgb(55, 128, 191)", serie1, "true");
            //ejemplo.addSerie("MINISTERIO DEL INTERIOR Y SEGURIDAD PUBLICA", "89da6d", "#0FFF02", serie2, "true");
            //ejemplo.addSerie("MINISTERIO DEL TRABAJO Y PREVISION SOCIAL", "79da6d", "", serie3, "true");
            //ejemplo.addSerie("MINISTERIO DE SALUD", "69da6d", "", serie4, "true");
            //ejemplo.addSerie("MINISTERIO DE VIVIENDA Y URBANISMO", "59da6d", "", serie5, "true");

            return ejemplo;

        }

        public class Serie
        {
            public Serie()
            {
                datosStringSerie = new List<string>();
                datosIntSerie = new List<long>();
                IsInt = true;
                IsDouble = true;
                isString = true;
                años = new List<string>();
            }

            public string name { get; set; }
            public string visible { get; set; }
            public string uid { get; set; }
            public string color { get; set; }
            public bool IsInt { get; set; }
            public bool IsDouble { get; set; }
            public bool isString { get; set; }
            public List<long> datosIntSerie { get; set; }
            public List<double> datosDoubleSerie { get; set; }
            public List<string> datosStringSerie { get; set; }

            public List<string> años { get; set; }

        }
    }
    public class d3Object_Bubble
    {
        public d3Object_Bubble()
        {
            elementos1 = new List<string>();
            elementos2 = new List<string>();
        }
        private List<string> elementos1;
        private List<string> elementos2;
        private string json;
        private string formatter;

        private string getCabecera()
        {
            string cabecera = @"{
                                     ""name"": ""flare"",
                                     ""children"": [";
            return cabecera;
        }
        private string getPie()
        {
            string pie = @"  ]
                                }";
            return pie;
        }
        private string createNivel1()
        {
            string childrens = "";
            for (int i = 0; i < elementos1.Count; i++)
            {
                string aux = elementos1[i];

                if (i + 1 == elementos1.Count)
                    aux = aux.Substring(0, aux.Length - 1);

                childrens += aux;
            }

            string nivel1 = @"{
                                    ""name"": ""Nivel1"",
                                    ""children"":
                                     ["
                                   + childrens +
                                 @"] 
                                   },";
            return nivel1;
        }
        private string createNivel2()
        {
            string childrens = "";
            for (int i = 0; i < elementos2.Count; i++)
            {
                string aux = elementos2[i];

                if (i + 1 == elementos2.Count)
                    aux = aux.Substring(0, aux.Length - 1);

                childrens += aux;
            }

            string nivel2 = @"{
                                    ""name"": ""Nivel2"",
                                    ""children"":
                                     ["
                                 + childrens +
                                @"] 
                                   }";
            return nivel2;
        }
        public void setFormato(string formatter)
        {
            this.formatter = formatter;
        }
        public void addElementoNV1(string name, string size, string value, string tipo, string nivel, string id, string ano, string color, string text, string tooltip)
        {
            string auxElem = @"{
                                    ""name"":" + @"""" + name + @"""" +
                             @",""size"":" + @"""" + size + @"""" +
                             @",""ano"":" + @"""" + ano + @"""" +
                             @",""value"":" + @"""" + value + @"""" +
                             @",""tipo"":" + @"""" + tipo + @"""" +
                             @",""nivel"":" + @"""" + nivel + @"""" +
                             @",""id"":" + @"""" + id + @"""" +
                             @",""color"":" + @"""" + color + @"""" +
                             @",""text"":" + @"""" + text + @"""" +
                             @",""tooltip"":" + @"""" + tooltip + @"""" + "},";

            elementos1.Add(auxElem);
        }
        public void addElementoNV2(string name, string size, string value, string tipo, string nivel, string id, string ano, string color, string text, string tooltip)
        {
            string auxElem = @"{
                                    ""name"":" + @"""" + name + @"""" +
                             @",""size"":" + @"""" + size + @"""" +
                             @",""ano"":" + @"""" + ano + @"""" +
                             @",""value"":" + @"""" + value + @"""" +
                             @",""tipo"":" + @"""" + tipo + @"""" +
                             @",""nivel"":" + @"""" + nivel + @"""" +
                             @",""id"":" + @"""" + id + @"""" +
                             @",""color"":" + @"""" + color + @"""" +
                             @",""text"":" + @"""" + text + @"""" +
                             @",""tooltip"":" + @"""" + tooltip + @"""" + "},";
            elementos2.Add(auxElem);
        }
        public string getJson()
        {
            json = getCabecera() + createNivel1() + createNivel2() + getPie();
            return json;
        }
        public void Load(Grafico_d3Object_Bubble d3Object, string formatter)
        {
            this.formatter = formatter;
            if (formatter == "peso")
            {
                foreach (Circulos_Grafico_d3Object_Bubble nv1 in d3Object.Circulos_Grafico_d3Object_Bubble)
                {
                    addElementoNV1(nv1.Titulo, nv1.Size.ToString(), nv1.valorPeso.ToString(), nv1.Tipo, "1", nv1.IdCirculo.ToString(), d3Object.ano, nv1.Color, nv1.Texto, nv1.Tooltip);
                    foreach (SubCirculos_Grafico_d3Object_Bubble nv2 in nv1.SubCirculos_Grafico_d3Object_Bubble)
                    {
                        addElementoNV2(nv2.Titulo, nv2.Size.ToString(), nv2.valorPeso.ToString(), nv2.Tipo, "2", nv2.IdDato.ToString(), d3Object.ano, nv2.Color, nv2.Texto, nv2.Tooltip);
                    }
                }
            }

            if (formatter == "porcentaje")
            {
                foreach (Circulos_Grafico_d3Object_Bubble nv1 in d3Object.Circulos_Grafico_d3Object_Bubble)
                {
                    addElementoNV1(nv1.Titulo, nv1.Size.ToString(), nv1.ValorPorcentaje.ToString(), nv1.Tipo, "1", nv1.IdCirculo.ToString(), d3Object.ano, nv1.Color, nv1.Texto, nv1.Tooltip2);
                    foreach (SubCirculos_Grafico_d3Object_Bubble nv2 in nv1.SubCirculos_Grafico_d3Object_Bubble)
                    {
                        addElementoNV2(nv2.Titulo, nv2.Size.ToString(), nv2.ValorPorcentaje.ToString(), nv2.Tipo, "2", nv2.IdDato.ToString(), d3Object.ano, nv2.Color, nv2.Texto, nv2.Tooltip2);
                    }
                }
            }
        }
    }
    public class d3Object_TreeMap
    {
        public d3Object_TreeMap()
        {
            elementos1 = new List<string>();
        }
        private List<string> elementos1;
        private string json;
        private string formatter;

        private string getCabecera()
        {
            string cabecera = @"{
                                     ""name"": ""flare"",
                                     ""children"": [";
            return cabecera;
        }
        private string getPie()
        {
            string pie = @"  ]
                                }";
            return pie;
        }
        private string createNivel1()
        {
            string childrens = "";
            for (int i = 0; i < elementos1.Count; i++)
            {
                string aux = elementos1[i];

                if (i + 1 == elementos1.Count)
                    aux = aux.Substring(0, aux.Length - 1);

                childrens += aux;
            }
            return childrens;
        }

        public void setFormato(string formatter)
        {
            this.formatter = formatter;
        }
        public void addElementoNV1(string name, string size, string nivel, string tipo, string color, string id, string idRegion)
        {
            int numero = int.Parse(size);
            string valueTooltip = string.Format(new System.Globalization.CultureInfo("is-IS"), "{0:N0}", numero);
            string auxElem = @"{
                             ""name"":" + @"""" + name + @"""" +
                             @",""size"":" + @"""" + size + @"""" +
                             @",""tipo"":" + @"""" + tipo + @"""" +
                             @",""valueTooltip"":" + @"""" + valueTooltip + @"""" +
                             @",""nivel"":" + @"""" + nivel + @"""" +
                             @",""id"":" + @"""" + id + @"""" +
                             @",""idRegion"":" + @"""" + idRegion + @"""" +
                             @",""color"":" + @"""" + color + @"""" + "},";

            elementos1.Add(auxElem);
        }
        public string getJson()
        {
            json = getCabecera() + createNivel1() + getPie();
            json = json.Remove(json.Length-1,1) + ", \"Format\":\"" +this.formatter + " \"}";
            return json;
        }
        public void Load(List<Cuadrados_d3Object_TreeMap> d3Object, string formatter)
        {
            this.formatter = formatter;

            if (formatter == "percapita")
            {
                #region percapita
                var valores = d3Object.GroupBy(r => r.Titulo).Select(r => new
                {
                    Nombre = r.First().Titulo,
                    valor = r.Sum(l => l.valorPercapita).ToString(),
                    Tipo = r.First().Tipo,
                    Color = r.First().Color,
                    Id = r.First().IdCuadrado.ToString(),
                    IdRegion = r.First().IdRegion,
                }).ToList();

                foreach (var nv1 in valores)
                {
                    addElementoNV1(nv1.Nombre, nv1.valor, "1", nv1.Tipo, nv1.Color, nv1.Id, "1");
                }
                #endregion
            }
            else
            {
                #region nominal
                var valores = d3Object.GroupBy(r => r.Titulo).Select(r => new
                {
                    Nombre = r.First().Titulo,
                    valor = r.Sum(l => l.valorNominal).ToString(),
                    Tipo = r.First().Tipo,
                    Color = r.First().Color,
                    Id = r.First().IdCuadrado.ToString(),
                    IdRegion = r.First().IdRegion,
                });

                foreach (var nv1 in valores)
                {
                    addElementoNV1(nv1.Nombre, nv1.valor, "1", nv1.Tipo, nv1.Color, nv1.Id, "1");
                }
                #endregion
            }
        }
        public void Load(List<SubCuadrados_d3Object_TreeMap> d3Object, string formatter)
        {
            this.formatter = formatter;

            if (formatter == "percapita")
            {
                #region percapita
                var valores = d3Object.GroupBy(r => r.Titulo).Select(r => new
                {
                    Nombre = r.First().Titulo,
                    valor = r.Sum(l => l.valorPercapita).ToString(),
                    Tipo = r.First().Tipo,
                    Color = r.First().Color,
                    Id = r.First().IdDato.ToString(),
                    IdRegion = r.First().IdRegion,
                }).ToList();

                foreach (var nv1 in valores)
                {
                    addElementoNV1(nv1.Nombre, nv1.valor, "1", nv1.Tipo, nv1.Color, nv1.Id, "1");
                }
                #endregion
            }
            else
            {
                #region nominal
                var valores = d3Object.GroupBy(r => r.Titulo).Select(r => new
                {
                    Nombre = r.First().Titulo,
                    valor = r.Sum(l => l.valorNominal).ToString(),
                    Tipo = r.First().Tipo,
                    Color = r.First().Color,
                    Id = r.First().IdDato.ToString(),
                    IdRegion = r.First().IdRegion,
                });

                foreach (var nv1 in valores)
                {
                    addElementoNV1(nv1.Nombre, nv1.valor, "1", nv1.Tipo, nv1.Color, nv1.Id, "1");
                }
                #endregion
            }
        }  
        public void Load(List<nivel1> d3Object,string formatter)
        {
            this.formatter = formatter;
            
            foreach (var nv1 in d3Object)
            {
                addElementoNV1222(
                    nv1.Nombre,
                    nv1.Valor.ToString(),
                    nv1.Valor2.ToString(),
                    "1", 
                    (nv1.Tipo==false)?"0":"1", 
                    "#ea9393", 
                    nv1.IdNivel1.ToString(),
                    nv1.PorcentajeGastadoAlPapa,
                    nv1.PorcentajePresupeustadoAlPapa,
                    nv1.Descripcion);
            }
        }
        public void Load(List<nivel2> d3Object, string formatter)
        {
            this.formatter = formatter;
            foreach (var nv1 in d3Object)
            {
                addElementoNV1222(
                    nv1.Nombre,
                    nv1.Valor.ToString(),
                    nv1.Valor2.ToString(),
                    "1",
                    (nv1.Tipo == false) ? "0" : "1",
                    "#ea9393",
                    nv1.IdNivel2.ToString(),
                    nv1.PorcentajeGastadoAlPapa,
                    nv1.PorcentajePresupeustadoAlPapa,
                    nv1.Descripcion
                );
            }
        }
        public void Load(List<nivel3> d3Object, string formatter)
        {
            this.formatter = formatter;
            foreach (var nv1 in d3Object)
            {
                addElementoNV1222(
                    nv1.Nombre,
                    nv1.Valor.ToString(),
                    nv1.Valor2.ToString(),
                    "1",
                    (nv1.Tipo == false) ? "0" : "1",
                    "#ea9393",
                    nv1.IdNivel3.ToString(),
                    nv1.PorcentajeGastadoAlPapa,
                    nv1.PorcentajePresupeustadoAlPapa,
                    nv1.Descripcion);
            }
        }
        public void Load(List<nivel4> d3Object, string formatter)
        {
            this.formatter = formatter;
            foreach (var nv1 in d3Object)
            {
                addElementoNV1222(
                    nv1.Nombre,
                    nv1.Valor.ToString(),
                    nv1.Valor2.ToString(),
                    "1",
                    (nv1.Tipo == false) ? "0" : "1",
                    "#ea9393",
                    nv1.IdNivel4.ToString(),
                    nv1.PorcentajeGastadoAlPapa,
                    nv1.PorcentajePresupeustadoAlPapa,
                    nv1.Descripcion);
            }
        }


        public void Load(List<nivel11> d3Object, string formatter)
        {
            this.formatter = formatter;

            foreach (var nv1 in d3Object)
            {
                addElementoNV1222(
                    nv1.Nombre,
                    nv1.Valor.ToString(),
                    nv1.Valor2.ToString(),
                    "1",
                    (nv1.Tipo == false) ? "0" : "1",
                    "#ea9393",
                    nv1.IdNivel11.ToString(),
                    nv1.PorcentajeGastadoAlPapa,
                    nv1.PorcentajePresupeustadoAlPapa,
                    nv1.Descripcion);
            }
        }
        public void Load(List<nivel22> d3Object, string formatter)
        {
            this.formatter = formatter;
            foreach (var nv1 in d3Object)
            {
                addElementoNV1222(
                    nv1.Nombre,
                    nv1.Valor.ToString(),
                    nv1.Valor2.ToString(),
                    "1",
                    (nv1.Tipo == false) ? "0" : "1",
                    "#ea9393",
                    nv1.IdNivel22.ToString(),
                    nv1.PorcentajeGastadoAlPapa,
                    nv1.PorcentajePresupeustadoAlPapa,
                    nv1.Descripcion);
            }
        }


        public void addElementoNV1222(string name, string size, string size2, string nivel, string tipo, string color, string id,string porcentaje1, string porcentaje2, string descripcion)
        {
            int numero = int.Parse(size);
            int numero2 = int.Parse(size2);
            string valueTooltip = string.Format(new System.Globalization.CultureInfo("is-IS"), "{0:N0}", numero);
            string valueTooltip2 = string.Format(new System.Globalization.CultureInfo("is-IS"), "{0:N0}", numero2);
            string auxElem = @"{
                             ""name"":" + @"""" + name + @"""" +
                             @",""size"":" + @"""" + size + @"""" +
                             @",""tipo"":" + @"""" + tipo + @"""" +
                             @",""valueTooltip1"":" + @"""" + valueTooltip + @"""" +
                             @",""valueTooltip2"":" + @"""" + valueTooltip2 + @"""" +
                             @",""porcentaje1"":" + @"""" + porcentaje1 + @"""" +
                             @",""porcentaje2"":" + @"""" + porcentaje2 + @"""" +
                             @",""descripcion"":" + @"""" + descripcion + @"""" +
                             @",""nivel"":" + @"""" + nivel + @"""" +
                             @",""id"":" + @"""" + id + @"""" +
                             @",""color"":" + @"""" + color + @"""" + "},";

            elementos1.Add(auxElem);
        }

    }



    public class amChart_HorizontalBar
    {

        public amChart_HorizontalBar()
        {
            tableJson = "";
            registros = new List<string>();
        }
        public List<string> registros;
        public string tableJson { get; set; }
        public string formatter { get; set; }
        public void addRegistro(string id, string categoria, int valor, string tooltip)
        {
            registros.Add(@"{       ""id"":" + @"""" + id + @"""" +
                                 @",""tooltip"":" + @"""" + tooltip + @"""" +
                                 @",""ciudad"":" + @"""" + categoria + @"""" +
                                 @",""inversion"":" + valor.ToString() +
                             @"},"
                             );
        }
        public void addRegistro(string id, string categoria, double valor, string tooltip)
        {
            registros.Add(@"{       ""id"":" + @"""" + id + @"""" +
                                 @",""tooltip"":" + @"""" + tooltip + @"""" +
                                 @",""ciudad"":" + @"""" + categoria + @"""" +
                                 @",""inversion"":" + valor.ToString() +
                             @"},"
                             );
        }
        public void addRegistro(string id, string categoria, int valor, string color, string tooltip)
        {
            registros.Add(@"{       ""id"":" + @"""" + id + @"""" +
                                 @",""tooltip"":" + @"""" + tooltip + @"""" +
                                 @",""ciudad"":" + @"""" + categoria + @"""" +
                                 @",""inversion"":" + valor.ToString() +
                                    @",""color"":" + @"""" + color + @"""" +
                             @"},"
                             );
        }
        public void addRegistro(string id, string categoria, double valor, string color, string tooltip)
        {
            registros.Add(@"{       ""id"":" + @"""" + id + @"""" +
                                 @",""tooltip"":" + @"""" + tooltip + @"""" +
                                 @",""ciudad"":" + @"""" + categoria + @"""" +
                                 @",""inversion"":" + valor.ToString() +
                                 @",""color"":" + @"""" + color + @"""" +
                             @"},"
                             );
        }
        public void addFila(string iniciativa, int valor)
        {
            tableJson += @"{ ""iniciativa"":" + @"""" + iniciativa.Replace("\"", "\\\"") + @"""" +
                          @",""valor"":"      + @"""" + ponerpuntos(valor) + @"""},";
        }

        public string ponerpuntos(int numero)
        {
            string number = numero.ToString();
            string result = "";
            while (number.Length > 3)
            {
                result = '.' + number.Substring(number.Length - 3) + result;
                number = number.Substring(0, number.Length - 3);
            }
            result = number + result;
            return result;
        }

        public void addFila(string iniciativa, double valor)
        {
            tableJson += @"{ ""iniciativa"":" + @"""" + iniciativa.Replace("\"", "\\\"") + @"""" +
                         @",""valor"":" + valor.ToString().Replace(",", ".") + @"},";
        }

        public string getJson()
        {
            string json = @"[";
            for (int i = 0; i < registros.Count; i++)
            {
                string aux = registros[i];

                if (i + 1 == registros.Count)
                    aux = aux.Substring(0, aux.Length - 1);

                json += aux;
            }
            json += @"]";
            return json;
        }

        public string getJsonTable()
        {
            string json = "";
            json = @"[" + tableJson.Substring(0, tableJson.Length - 1) + @"]";
            return json;
        }

        public void Load2(List<Barra_amChart_HorizontalBar> grafico, string formato)
        {
            formatter = formato;
            if (formato == "nominal")
            {
                var caso1 = grafico.GroupBy(r => new { r.Titulo }).Select(r => new { Titulo = r.Key.Titulo, valorPercapita = r.Sum(l => l.valor), tooltip = r.First().tooltip }).ToList();
                caso1 = caso1.OrderByDescending(r => r.valorPercapita).ToList();
                for (int i = 0; i < caso1.Count; i++)
                {
                    addRegistro("0", caso1[i].Titulo, caso1[i].valorPercapita.Value, caso1[i].tooltip);
                }
            }
            else
            {
                var caso1 = grafico.GroupBy(r => new { r.Titulo }).Select(r => new { Titulo = r.Key.Titulo, valorPercapita = r.Sum(l => l.ValorPercapita), tooltip = r.First().tooltip }).ToList();
                caso1 = caso1.OrderByDescending(r => r.valorPercapita).ToList();
                for (int i = 0; i < caso1.Count; i++)
                {
                    addRegistro("0", caso1[i].Titulo, caso1[i].valorPercapita.Value, caso1[i].tooltip);
                }
            }
        }

        public void Load2(Grafico_amChart_HorizontalBar grafico, string formato)
        {
            formatter = formato;
            if (formato == "nominal")
            {
                var caso1 = grafico.Barra_amChart_HorizontalBar.GroupBy(r => new { r.Titulo }).Select(r => new { Titulo = r.Key.Titulo, valorPercapita = r.Sum(l => l.valor), tooltip = r.First().tooltip }).ToList();
                caso1 = caso1.OrderByDescending(r => r.valorPercapita).ToList();
                for (int i = 0; i < caso1.Count; i++)
                {
                    addRegistro("0", caso1[i].Titulo, caso1[i].valorPercapita.Value, caso1[i].tooltip);
                }
            }
            else
            {
                var caso1 = grafico.Barra_amChart_HorizontalBar.GroupBy(r => new { r.Titulo }).Select(r => new { Titulo = r.Key.Titulo, valorPercapita = r.Sum(l => l.ValorPercapita), tooltip = r.First().tooltip }).ToList();
                caso1 = caso1.OrderByDescending(r => r.valorPercapita).ToList();
                for (int i = 0; i < caso1.Count; i++)
                {
                    addRegistro("0", caso1[i].Titulo, caso1[i].valorPercapita.Value, caso1[i].tooltip);
                }
            }
        }

        public void Load(Grafico_amChart_HorizontalBar grafico, string formato)
        {
            formatter = formato;
            if (formato == "nominal")
            {

                foreach (Barra_amChart_HorizontalBar barra in grafico.Barra_amChart_HorizontalBar)
                {
                    if (string.IsNullOrEmpty(barra.Color))
                    {
                        addRegistro(barra.IdCuadrado.ToString(), barra.Titulo, barra.valor.Value, barra.tooltip);
                    }
                    else
                    {
                        addRegistro(barra.IdCuadrado.ToString(), barra.Titulo, barra.valor.Value, barra.Color, barra.tooltip);
                    }
                }
            }
            else
            {

                foreach (Barra_amChart_HorizontalBar barra in grafico.Barra_amChart_HorizontalBar)
                {
                    if (string.IsNullOrEmpty(barra.Color))
                    {
                        addRegistro(barra.IdCuadrado.ToString(), barra.Titulo, barra.ValorPercapita.Value, barra.tooltip);
                    }
                    else
                    {
                        addRegistro(barra.IdCuadrado.ToString(), barra.Titulo, barra.ValorPercapita.Value, barra.Color, barra.tooltip);
                    }
                }
            }
        }

        public void Load(Grafico_amChart_HorizontalBar grafico)
        {
            foreach (Barra_amChart_HorizontalBar barra in grafico.Barra_amChart_HorizontalBar)
            {
                if (string.IsNullOrEmpty(barra.Color))
                {
                    addRegistro(barra.IdGraficoFK.ToString(), barra.Titulo, barra.valor.Value, barra.Titulo);
                }
                else
                {
                    addRegistro(barra.IdGraficoFK.ToString(), barra.Titulo, barra.valor.Value, barra.Color, barra.Titulo);
                }
            }
        }

        public void Load(List<Tabla_amChart_HorizontalBar> tabla, string formato)
        {
            foreach (Tabla_amChart_HorizontalBar barra in tabla)
            {
                if (formato == "nominal")
                {
                    addFila(barra.Iniciativa, barra.valorNominal.Value);
                }
                else
                {
                    addFila(barra.Iniciativa, barra.valorPercapita.Value);
                }
            }
        }
        public void Load(Dictionary<string, int> tabla, string formato)
        {
            foreach (var barra in tabla)
            {
                addFila(barra.Key, barra.Value);
                
            }
        }

        #region Examples
        public static amChart_HorizontalBar Example()
        {
            amChart_HorizontalBar example = new amChart_HorizontalBar();

            example.addRegistro("1", "Arica y Parinacota", 100000, "mensaje de prueba 1");
            example.addRegistro("2", "Tarapaca", 200000, "mensaje de prueba 2");
            example.addRegistro("3", "Antofagasta", 300000, "mensaje de prueba 3");
            example.addRegistro("4", "Atacama", 400000, "mensaje de prueba 4");
            example.addRegistro("5", "Coquimbo", 500000, "mensaje de prueba 5");
            example.addRegistro("6", "Valparaiso", 600000, "mensaje de prueba 6");
            example.addRegistro("7", "Region Metropolitana", 700000, "mensaje de prueba 7");
            example.addRegistro("8", "O´Higgins", 800000, "mensaje de prueba 8");
            example.addRegistro("9", "Maule", 2900000, "mensaje de prueba 9");
            example.addRegistro("10", "Biobio", 1000000, "mensaje de prueba 10");
            example.addRegistro("11", "Araucania", 1100000, "mensaje de prueba 11");
            example.addRegistro("12", "Los Rios", 1200000, "mensaje de prueba 12");
            example.addRegistro("13", "Los Lagos", 1300000, "mensaje de prueba 13");
            example.addRegistro("14", "Aysen", 1400000, "mensaje de prueba 14");
            example.addRegistro("15", "Magallanes", 31500000, "mensaje de prueba 15");

            return example;
        }
        public static amChart_HorizontalBar Example2()
        {
            amChart_HorizontalBar example = new amChart_HorizontalBar();

            example.addRegistro("1", "Arica y Parinacota", 1500000, "mensaje de prueba ");
            example.addRegistro("2", "Tarapaca", 1400000, "mensaje de prueba 2");
            example.addRegistro("3", "Antofagasta", 1300000, "mensaje de prueba 3");
            example.addRegistro("4", "Atacama", 1200000, "mensaje de prueba 4");
            example.addRegistro("5", "Coquimbo", 1100000, "mensaje de prueba 5");
            example.addRegistro("6", "Valparaiso", 1000000, "mensaje de prueba 6");
            example.addRegistro("7", "Region Metropolitana", 900000, "mensaje de prueba 7");
            example.addRegistro("8", "O´Higgins", 33800000, "mensaje de prueba 8");
            example.addRegistro("9", "Maule", 700000, "mensaje de prueba 9");
            example.addRegistro("10", "Biobio", 600000, "mensaje de prueba 10");
            example.addRegistro("11", "Araucania", 500000, "mensaje de prueba 11");
            example.addRegistro("12", "Los Rios", 400000, "mensaje de prueba 12");
            example.addRegistro("13", "Los Lagos", 300000, "mensaje de prueba 13");
            example.addRegistro("14", "Aysen", 200000, "mensaje de prueba 14");
            example.addRegistro("15", "Magallanes", 44100000, "mensaje de prueba 15");

            return example;
        }
        public static amChart_HorizontalBar Example3()
        {
            amChart_HorizontalBar example = new amChart_HorizontalBar();

            example.addRegistro("1", "Educacion y Cultura", 7400, "mensaje de prueba 1");
            example.addRegistro("2", "Social", 4540, "mensaje de prueba 2");
            example.addRegistro("3", "Transporte", 3500, "mensaje de prueba 3");
            example.addRegistro("4", "Vivienda", 1800, "mensaje de prueba 4");
            example.addRegistro("5", "Multisectorial", 1600, "mensaje de prueba 5");
            example.addRegistro("6", "Justicia", 13000, "mensaje de prueba 6");
            example.addRegistro("7", "Deportes", 3000, "mensaje de prueba 7");
            example.addRegistro("8", "SivoAgropecuario", 2500, "mensaje de prueba 8");
            example.addRegistro("9", "Industria, Comercio, Finanza yTurismo", 2500, "mensaje de prueba 9");
            example.addRegistro("10", "Pesca", 1500, "mensaje de prueba 10");
            example.addRegistro("11", "Agua potable y Alcantarillado", 1000, "mensaje de prueba 11");
            example.addRegistro("12", "Defensa y Seguridad", 500, "mensaje de prueba 12");
            example.addRegistro("13", "Mineria", 300, "mensaje de prueba 13");
            example.addRegistro("14", "Comunicaciones", 200, "mensaje de prueba 14");

            return example;
        }
        public static amChart_HorizontalBar Example4()
        {
            amChart_HorizontalBar example = new amChart_HorizontalBar();

            example.addRegistro("1", "Educacion y Cultura", 7040, "mensaje de prueba 1");
            example.addRegistro("2", "Social", 4504, "mensaje de prueba 2");
            example.addRegistro("3", "Transporte", 3500, "mensaje de prueba 3");
            example.addRegistro("4", "Vivienda", 1800, "mensaje de prueba 4");
            example.addRegistro("5", "Multisectorial", 1600, "mensaje de prueba 5");
            example.addRegistro("6", "Justicia", 9000, "mensaje de prueba 6");
            example.addRegistro("7", "Deportes", 3000, "mensaje de prueba 7");
            example.addRegistro("8", "SivoAgropecuario", 2500, "mensaje de prueba 8");
            example.addRegistro("9", "Industria, Comercio, Finanza yTurismo", 2000, "mensaje de prueba 9");
            example.addRegistro("10", "Pesca", 1500, "mensaje de prueba 10");
            example.addRegistro("11", "Agua potable y Alcantarillado", 1000, "mensaje de prueba 11");
            example.addRegistro("12", "Defensa y Seguridad", 500, "mensaje de prueba 12");
            example.addRegistro("13", "Mineria", 300, "mensaje de prueba 13");
            example.addRegistro("14", "Comunicaciones", 200, "mensaje de prueba 14");

            return example;
        }
        public static amChart_HorizontalBar Example5()
        {
            amChart_HorizontalBar example = new amChart_HorizontalBar();
            example.addRegistro("1", "", -300, "mensaje de prueba 1");
            example.addRegistro("2", "", -250, "mensaje de prueba 2");
            example.addRegistro("3", "", -300, "mensaje de prueba 3");
            example.addRegistro("4", "", -250, "mensaje de prueba 4");
            example.addRegistro("5", "", -200, "mensaje de prueba 5");
            example.addRegistro("6", "", -170, "mensaje de prueba 6");
            example.addRegistro("7", "", -130, "mensaje de prueba 7");
            example.addRegistro("8", "", -170, "mensaje de prueba 8");
            example.addRegistro("9", "", -130, "mensaje de prueba 9");
            return example;
        }
        public static amChart_HorizontalBar Example6()
        {
            amChart_HorizontalBar example = new amChart_HorizontalBar();
            example.addRegistro("1", "", 23.5, "mensaje de prueba 1");
            example.addRegistro("2", "", 20.5, "mensaje de prueba 2");
            example.addRegistro("3", "", 18, "mensaje de prueba 3");
            example.addRegistro("4", "", 15.4, "mensaje de prueba 4");
            example.addRegistro("5", "", 18, "mensaje de prueba 5");
            example.addRegistro("6", "", 15.4, "mensaje de prueba 6");
            example.addRegistro("7", "", 16.2, "mensaje de prueba 7");
            example.addRegistro("8", "", 15.4, "mensaje de prueba 8");
            example.addRegistro("9", "", 16.2, "mensaje de prueba 9");
            return example;
        }
        public static amChart_HorizontalBar Example7()
        {
            amChart_HorizontalBar example = new amChart_HorizontalBar();
            example.addRegistro("1", "", -1300, "mensaje de prueba 1");
            example.addRegistro("2", "", -1250, "mensaje de prueba 2");
            example.addRegistro("3", "", -1200, "mensaje de prueba 3");
            example.addRegistro("4", "", -1250, "mensaje de prueba 4");
            example.addRegistro("5", "", -1200, "mensaje de prueba 5");
            example.addRegistro("6", "", -1170, "mensaje de prueba 6");
            example.addRegistro("7", "", -1130, "mensaje de prueba 7");
            example.addRegistro("8", "", -1170, "mensaje de prueba 8");
            example.addRegistro("9", "", -1130, "mensaje de prueba 9");
            return example;
        }
        public static amChart_HorizontalBar Example8()
        {
            amChart_HorizontalBar example = new amChart_HorizontalBar();
            example.addRegistro("1", "", 33.5, "mensaje de prueba 1");
            example.addRegistro("2", "", 30.5, "mensaje de prueba 2");
            example.addRegistro("3", "", 28, "mensaje de prueba 3");
            example.addRegistro("4", "", 25.4, "mensaje de prueba 4");
            example.addRegistro("5", "", 26.2, "mensaje de prueba 5");
            example.addRegistro("6", "", 25.4, "mensaje de prueba 6");
            example.addRegistro("7", "", 26.2, "mensaje de prueba 7");
            example.addRegistro("8", "", 25.4, "mensaje de prueba 8");
            example.addRegistro("9", "", 26.2, "mensaje de prueba 9");
            return example;
        }
        public static amChart_HorizontalBar Example9()
        {
            amChart_HorizontalBar example = new amChart_HorizontalBar();
            example.addRegistro("1", "Gobierno regional 1", 100000, "#FF0F00", "mensaje de prueba 1");
            example.addRegistro("2", "Gobierno regional 2", 200000, "#FF6600", "mensaje de prueba 2");
            example.addRegistro("3", "Gobierno regional 3", 300000, "#FF9E01", "mensaje de prueba 3");
            example.addRegistro("4", "Gobierno regional 4", 400000, "#FCD202", "mensaje de prueba 4");
            example.addRegistro("5", "Gobierno regional 5", 500000, "#F8FF01", "mensaje de prueba 5");
            example.addRegistro("6", "Gobierno regional 6", 600000, "#B0DE09", "mensaje de prueba 6");
            example.addRegistro("7", "Gobierno regional 7", 700000, "#04D215", "mensaje de prueba 7");
            example.addRegistro("8", "Gobierno regional 8", 800000, "#0D8ECF", "mensaje de prueba 8");
            example.addRegistro("9", "Gobierno regional 9", 900000, "#00000F", "mensaje de prueba 9");
            example.addRegistro("10", "Gobierno regional 10", 1000000, "#8A0CCF", "mensaje de prueba 10");
            example.addRegistro("11", "Gobierno regional 11", 1100000, "#CD0D74", "mensaje de prueba 11");

            return example;
        }
        public static amChart_HorizontalBar Example10()
        {
            amChart_HorizontalBar example = new amChart_HorizontalBar();
            example.addRegistro("1", "Gobierno regional 1", 1100000, "#FF0F00", "mensaje de prueba 1");
            example.addRegistro("2", "Gobierno regional 2", 1000000, "#FF6600", "mensaje de prueba 2");
            example.addRegistro("3", "Gobierno regional 3", 900000, "#FF9E01", "mensaje de prueba 3");
            example.addRegistro("4", "Gobierno regional 4", 800000, "#FCD202", "mensaje de prueba 4");
            example.addRegistro("5", "Gobierno regional 5", 700000, "#F8FF01", "mensaje de prueba 5");
            example.addRegistro("6", "Gobierno regional 6", 600000, "#B0DE09", "mensaje de prueba 6");
            example.addRegistro("7", "Gobierno regional 7", 500000, "#04D215", "mensaje de prueba 7");
            example.addRegistro("8", "Gobierno regional 8", 400000, "#0D8ECF", "mensaje de prueba 8");
            example.addRegistro("9", "Gobierno regional 9", 300000, "#00000F", "mensaje de prueba 9");
            example.addRegistro("10", "Gobierno regional 10", 200000, "#8A0CCF", "mensaje de prueba 10");
            example.addRegistro("11", "Gobierno regional 11", 100000, "#CD0D74", "mensaje de prueba 11");

            return example;
        }

        #endregion
    }
    public class googleChart_Pie
    {
        public googleChart_Pie()
        {
            dataChart = new List<string>();
            colores = new List<string>();
            colores.Add(@"[");
        }

        public List<string> dataChart { get; set; }
        private string json { get; set; }
        public string titulo { get; set; }
        public List<string> colores { get; set; }
        public void addElemento(string descripcion, int valor, string color)
        {
            string aux = @"{""c"":[
                                    {""v"":" + @"""" + descripcion + @"""" + @"},
                                    {""v"":" + valor.ToString() + @"}
                                ]},";

            dataChart.Add(aux);
            colores.Add(@"""" + color + @"""" + @",");
        }
        public void addElemento(string descripcion, double valor, string color)
        {
            string aux = @"{""c"":[
                                    {""v"":" + @"""" + descripcion + @"""" + @"},
                                    {""v"":" + valor.ToString() + @"}
                                ]},";
            dataChart.Add(aux);
            colores.Add(@"""" + color + @"""" + @",");
        }
        public void setTitutlo(string title)
        {
            titulo = title;
        }
        public string[] getJson()
        {
            string[] json = new string[3];
            string inicio = @"{
                              ""cols"": [
                                    {""id"":"""",""type"":""string""},
                                    {""id"":"""",""type"":""number""}
                                  ],
                              ""rows"": [";
            string fin = @"]
                             }";

            json[0] = inicio;

            for (int i = 0; i < dataChart.Count; i++)
            {
                json[0] += dataChart[i];
            }

            string jsonColores = "";
            foreach (string colors in colores)
            {
                jsonColores += colors;
            }
            jsonColores = jsonColores.Substring(0, jsonColores.Length - 1);
            jsonColores += @"]";

            json[0] = json[0].Substring(0, json[0].Length - 1);
            json[0] += fin;
            json[1] = titulo;
            json[2] = jsonColores;

            return json;
        }

        public void Load(Grafico_googleChart_Pie grafico)
        {
            titulo = grafico.Titulo;
            foreach (Torta_Grafico_googleChart_Pie circulo in grafico.Torta_Grafico_googleChart_Pie)
            {
                addElemento(circulo.Titulo, circulo.Valor.Value, circulo.Color);
            }
        }
        public static googleChart_Pie Example()
        {
            googleChart_Pie gChart = new googleChart_Pie();
            gChart.setTitutlo("año 2015");
            gChart.addElemento("Vivienda", 150000, "#fff");
            gChart.addElemento("Seguridad", 30000, "#fff");
            gChart.addElemento("Educacion", 40000, "#fff");
            gChart.addElemento("Salud", 40000, "#fff");
            gChart.addElemento("Defensa", 30000, "#fff");

            return gChart;

        }
        public static googleChart_Pie Example2()
        {
            googleChart_Pie gChart = new googleChart_Pie();
            gChart.setTitutlo("año 2000");
            gChart.addElemento("Vivienda", 155000, "#fff");
            gChart.addElemento("Seguridad", 31000, "#fff");
            gChart.addElemento("Educacion", 45000, "#fff");
            gChart.addElemento("Salud", 40500, "#fff");
            gChart.addElemento("Defensa", 30500, "#fff");

            return gChart;

        }
        public static googleChart_Pie Example3()
        {
            googleChart_Pie gChart = new googleChart_Pie();
            gChart.setTitutlo("");
            gChart.addElemento("Gasto fiscal asignado a Regiones", 54, "#fff");
            gChart.addElemento("Gasto fiscal asignado a nivel Nacional", 46, "#fff");

            return gChart;

        }

        #region ejemplo json
        //string json = @"{
        //                  ""cols"": [
        //                        {""id"":"""",""type"":""string""},
        //                        {""id"":"""",""type"":""number""}
        //                      ],
        //                  ""rows"": [
        //                        {""c"":[
        //                                    {""v"":""Mushrooms""},
        //                                    {""v"":3}
        //                               ]},
        //                        {""c"":[
        //                                    {""v"":""Onions""},
        //                                    {""v"":1}
        //                              ]}
        //                      ]
        //                }";
        #endregion
    }
    public class googleChart_Line
    {
        public googleChart_Line()
        {
            dataChart = new List<Serie>();
            customTooltip = true;
            serieMasLarga = 0;
            formatter = "MM $";
        }

        private string titulo { get; set; }
        private string formatter { get; set; }
        public bool customTooltip { get; set; }
        public List<Serie> dataChart { get; set; }
        private List<string> dataColumn { get; set; }
        private List<string> _dataChart { get; set; }
        private int serieMasLarga { get; set; }
        private string json { get; set; }
        public void setTitulo(string titulo)
        {
            this.titulo = titulo;
        }
        public void setFormato(string formatter)
        {
            this.formatter = formatter;
        }
        public void setCustomTooltip(bool customToolTip)
        {
            this.customTooltip = customTooltip;
        }
        public void addSerie(string id, string nombreSerie, string tipo, List<long> datosSerie, string color, List<long> acumulado)
        {
            Serie serie = new Serie();
            serie.id = id;
            serie.nombreSerie = nombreSerie;
            serie.tipo = tipo;
            serie.color = color;
            serie.IsString = false;
            serie.IsDouble = false;
            serie.datosIntSerie = datosSerie;
            serie.acumulado = acumulado;
            dataChart.Add(serie);

            if (serieMasLarga <= datosSerie.Count)
                serieMasLarga = datosSerie.Count;

            //string inicio = @"{ ""id"":" + @"""" + nombreSerie + @"""" + @",""type"":" + @"""" + tipo + @"""" + @"},";
            //dataColumn.Add(inicio);
            //addRow(datosSerie);
        }
        public void addSerie(string id, string nombreSerie, string tipo, List<double> datosSerie, string color, List<double> acumulado)
        {
            Serie serie = new Serie();
            serie.id = id;
            serie.nombreSerie = nombreSerie;
            serie.tipo = tipo;
            serie.color = color;
            serie.IsString = false;
            serie.IsInt = false;
            serie.acumuladoDouble = acumulado;
            serie.datosDoubleSerie = datosSerie;
            dataChart.Add(serie);

            if (serieMasLarga <= datosSerie.Count)
                serieMasLarga = datosSerie.Count;

            //string inicio = @"{ ""id"":" + @"""" + nombreSerie + @"""" + @",""type"":" + @"""" + tipo + @"""" + @"},";
            //dataColumn.Add(inicio);
            //addRow(datosSerie);
        }
        public void addSerie(string id, string nombreSerie, string tipo, List<string> datosSerie, string color, List<long> acumulado)
        {
            Serie serie = new Serie();
            serie.id = id;
            serie.nombreSerie = nombreSerie;
            serie.tipo = tipo;
            serie.color = color;
            serie.IsInt = false;
            serie.IsDouble = false;
            serie.acumulado = acumulado;
            serie.datosStringSerie = datosSerie;
            dataChart.Add(serie);

            if (serieMasLarga <= datosSerie.Count)
                serieMasLarga = datosSerie.Count;

            //string inicio = @"{ ""id"":" + @"""" + nombreSerie + @"""" + @",""type"":" + @"""" + tipo + @"""" + @"},";
            //dataColumn.Add(inicio);
            //addRow(datosSerie);
        }
        private void addRow(List<int> datosSerie)
        {
            //string row = ""; ;
            //string aux = @"{ ""c"":[";

            //for (int i = 0; i < datosSerie.Count; i++)
            //{
            //    row += @"{ ""v"":" + datosSerie[i].ToString() + @"},";
            //}

            //row = row.Substring(0, row.Length - 1);

            //aux += row + @"]},";
            //dataChart.Add(aux);
        }
        private void addRow(List<string> datosSerie)
        {
            //string row = ""; ;
            //string aux = @"{ ""c"":[";

            //for (int i = 0; i < datosSerie.Count; i++)
            //{
            //    row += @"{ ""v"":" + @"""" + datosSerie[i] + @"""" +@"},";
            //}

            //row = row.Substring(0, row.Length - 1);

            //aux += row + @"]},";
            //dataChart.Add(aux);
        }
        public string[] getJson()
        {
            string[] json = new string[5];
            string aux1 = "";
            string aux2 = "";
            string aux3 = "";
            string[] listaAux = new string[serieMasLarga];
            string colores = "";

            aux1 = @"{ ""cols"": [";
            colores = @"[";
            int count = 0;
            foreach (Serie serie in dataChart)
            {
                aux1 += @"{ ""id"":" + @"""" + serie.id + @"""" + @",""label"":" + @"""" + serie.nombreSerie + @"""" + @",""type"":" + @"""" + serie.tipo + @"""" + @"},";
                if (customTooltip)
                {
                    aux1 += @"{ ""id"": """", ""role"": ""tooltip"", ""type"": ""string"", ""p"" : { ""html"" : true } },";
                    //aux1 += @"{ ""id"": """", ""role"": ""tooltip"", ""type"": ""string"", ""H1"" : { ""html"" : true } },";
                }
                if (count != 0)
                {
                    colores += @"""" + serie.color + @"""" + @",";
                }
                count++;
            }
            colores = colores.Substring(0, colores.Length - 1);
            colores += @"]";
            aux1 = aux1.Substring(0, aux1.Length - 1);
            aux1 += @"],""rows"": [";

            int posicionAux = 0;
            int posicionAux2 = dataChart.Count;

            foreach (Serie serie in dataChart)
            {
                for (int i = 0; i < serieMasLarga; i++)
                {
                    string dato = "";
                    string toolTip = "";

                    if (customTooltip)
                    {
                        if (dataChart[0].datosStringSerie[i] == null || dataChart[0].datosStringSerie[i] == "")
                        {
                            toolTip = "Serie: " + "<br/>";
                            toolTip += "Año: " + "<br/>";
                        }
                        else
                        {
                            toolTip = "Serie: " + serie.nombreSerie + "<br/>";
                            toolTip += "Año: " + dataChart[0].datosStringSerie[i] + "<br/>";
                        }
                    }

                    if (posicionAux == 0)
                    {
                        dato = @"{ ""c"":[";
                    }

                    try
                    {
                        if (serie.IsString)
                        {
                            dato += @"{ ""v"":" + @"""" + serie.datosStringSerie[i] + @"""" + @"},";

                            if (customTooltip)
                            {
                                toolTip += "Monto : ";

                                if (serie.datosStringSerie[i] != null)
                                {
                                    toolTip += serie.datosStringSerie[i] + "<br/>";
                                    toolTip += "Suma : ";// + serie.acumulado[i].ToString();
                                }
                                dato += @"{ ""v"":" + @"""" + toolTip + @"""" + @"},";
                            }
                        }
                        else
                        {
                            if (serie.IsInt)
                            {
                                dato += @"{ ""v"":" + serie.datosIntSerie[i] + @"},";

                                if (customTooltip)
                                {
                                    toolTip += "Monto MM: $";

                                    if (serie.datosIntSerie[i].ToString() != "")
                                    {
                                        toolTip += SeparadorMiles(serie.datosIntSerie[i].ToString()) + "<br/>";
                                        toolTip += "Suma MM: $" + SeparadorMiles(serie.acumulado[i].ToString());
                                    }

                                    dato += @"{ ""v"":" + @"""" + toolTip + @"""" + @"},";
                                }
                            }
                            else
                            {
                                if (serie.IsDouble)
                                {
                                    dato += @"{ ""v"":" + serie.datosDoubleSerie[i].ToString().Replace(",", ".") + @"},";

                                    if (customTooltip)
                                    {
                                        toolTip += "Porcentaje : ";

                                        if (serie.datosDoubleSerie[i].ToString() != "")
                                        {
                                            float aux = float.Parse(((serie.datosDoubleSerie[i]) * 100).ToString());
                                            toolTip += aux + "% <br/>";
                                            toolTip += "Suma %: " + ((serie.acumuladoDouble[i]) * 100).ToString() + "%";// + serie.acumulado[i].ToString();
                                        }

                                        dato += @"{ ""v"":" + @"""" + toolTip + @"""" + @"},";
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        dato += @"{ ""v"":" + @"null" + @"},";
                        if (customTooltip)
                        {
                            dato += @"{ ""v"":" + @"""" + "HOLA MUNDO" + @"""" + @"},";
                        }
                    }
                    finally
                    {

                        if (posicionAux == posicionAux2 - 1)
                        {
                            dato = dato.Substring(0, dato.Length - 1) + @"]},";
                        }

                        listaAux[i] += dato;
                    }
                }
                posicionAux++;
            }

            int recorridoPenultimo = listaAux.Length - 1;
            for (int i = 0; i < recorridoPenultimo; i++)
            {
                aux2 += listaAux[i];
            }
            aux2 += listaAux[recorridoPenultimo].Substring(0, listaAux[recorridoPenultimo].Length - 1);

            aux3 = @"]}";

            json[0] = aux1 + aux2 + aux3;
            json[1] = colores;
            json[2] = @"" + titulo + @"";
            json[3] = @"" + formatter + @"";
            json[4] = @"" + dataChart.Count + @"";

            return json;
        }

        public void Load(Grafico_googleChart_Line gChart, string tipo)
        {

            titulo = gChart.Titulo;
            List<string> años = new List<string>();

            Serie serie = new Serie();
            serie.nombreSerie = "Años";
            serie.tipo = "string";
            serie.color = "#E41ECC";
            serie.IsInt = false;
            serie.IsDouble = false;
            serie.datosStringSerie = años;
            dataChart.Add(serie);

            if (tipo == "porcentaje")
            {
                formatter = "2";
                Dictionary<string, double> openWith = new Dictionary<string, double>();
                foreach (var item in gChart.Lineas_googleChart_Line)
                {
                    foreach (var item2 in item.Datos_googleChart_Line)
                    {
                        if (openWith.Any(r => r.Key == item2.Ano))
                        {
                            openWith[item2.Ano] = openWith[item2.Ano] + item2.ValorPorcentaje.Value;
                        }
                        else
                        {
                            openWith.Add(item2.Ano, item2.ValorPorcentaje.Value);
                        }
                    }
                }

                foreach (Lineas_googleChart_Line linea in gChart.Lineas_googleChart_Line)
                {
                    años.AddRange(linea.Datos_googleChart_Line.Select(r => r.Ano).ToList());
                    addSerie(linea.IdLinea.ToString(), linea.Titulo, "number", linea.Datos_googleChart_Line.Select(r => (r.ValorPorcentaje.Value)).ToList(), linea.Color, openWith.Select(r => r.Value).ToList());
                }
            }

            if (tipo == "peso")
            {
                formatter = "1";

                Dictionary<string, long> openWith = new Dictionary<string, long>();
                foreach (var item in gChart.Lineas_googleChart_Line)
                {
                    foreach (var item2 in item.Datos_googleChart_Line)
                    {
                        if (openWith.Any(r => r.Key == item2.Ano))
                        {
                            openWith[item2.Ano] = openWith[item2.Ano] + item2.ValorPeso.Value;
                        }
                        else
                        {
                            openWith.Add(item2.Ano, item2.ValorPeso.Value);
                        }
                    }
                }
                foreach (Lineas_googleChart_Line linea in gChart.Lineas_googleChart_Line)
                {
                    años.AddRange(linea.Datos_googleChart_Line.Select(r => r.Ano).ToList());
                    addSerie(linea.IdLinea.ToString(), linea.Titulo, "number", linea.Datos_googleChart_Line.Select(r => r.ValorPeso.Value).ToList(), linea.Color, openWith.Select(r => r.Value).ToList());
                }
            }
            años = años.Distinct().ToList();
            dataChart[0].datosStringSerie = años;

            if (serieMasLarga <= años.Count)
                serieMasLarga = años.Count;

        }


        public class Serie
        {
            public Serie()
            {
                datosStringSerie = new List<string>();
                datosIntSerie = new List<long>();
                datosDoubleSerie = new List<double>();
                IsString = true;
                IsInt = true;
                IsDouble = true;
            }
            public bool IsString { get; set; }
            public bool IsInt { get; set; }
            public bool IsDouble { get; set; }
            public string id { get; set; }
            public string nombreSerie { get; set; }
            public string tipo { get; set; }
            public string color { get; set; }
            public List<long> acumulado { get; set; }
            public List<double> acumuladoDouble { get; set; }
            public List<string> datosStringSerie { get; set; }
            public List<long> datosIntSerie { get; set; }
            public List<double> datosDoubleSerie { get; set; }
        }

        public string SeparadorMiles(string numero)
        {
            int largo = numero.Length;
            string auxiliar = "";
            int contador = 0;
            for (int i = largo - 1; i >= 0; i--)
            {
                auxiliar = numero[i] + auxiliar;
                contador++;
                if (contador % 3 == 0)
                    auxiliar = "." + auxiliar;
            }
            return auxiliar;
        }
    }
    public class googleChart_Area
    {
        public googleChart_Area()
        {
            dataChart = new List<Serie>();
            customTooltip = false;
            serieMasLarga = 0;
            formatter = "$";
        }

        private string titulo { get; set; }
        private string formatter { get; set; }
        public bool customTooltip { get; set; }
        public List<Serie> dataChart { get; set; }
        private List<string> dataColumn { get; set; }
        private List<string> _dataChart { get; set; }
        private int serieMasLarga { get; set; }
        private string json { get; set; }

        public void setTitulo(string titulo)
        {
            this.titulo = titulo;
        }
        public void setFormato(string formatter)
        {
            this.formatter = formatter;
        }
        public void setCustomTooltip(bool customToolTip)
        {
            this.customTooltip = customTooltip;
        }
        public void addSerie(string id, string nombreSerie, string tipo, List<long> datosSerie, string color)
        {
            Serie serie = new Serie();
            serie.id = id;
            serie.nombreSerie = nombreSerie;
            serie.tipo = tipo;
            serie.color = color;
            serie.IsString = false;
            serie.IsDouble = false;
            serie.datosIntSerie = datosSerie;
            dataChart.Add(serie);

            if (serieMasLarga <= datosSerie.Count)
                serieMasLarga = datosSerie.Count;

            //string inicio = @"{ ""id"":" + @"""" + nombreSerie + @"""" + @",""type"":" + @"""" + tipo + @"""" + @"},";
            //dataColumn.Add(inicio);
            //addRow(datosSerie);
        }
        public void addSerie(string id, string nombreSerie, string tipo, List<double> datosSerie, string color)
        {
            Serie serie = new Serie();
            serie.id = id;
            serie.nombreSerie = nombreSerie;
            serie.tipo = tipo;
            serie.color = color;
            serie.IsString = false;
            serie.IsInt = false;
            serie.datosDoubleSerie = datosSerie;
            dataChart.Add(serie);

            if (serieMasLarga <= datosSerie.Count)
                serieMasLarga = datosSerie.Count;

            //string inicio = @"{ ""id"":" + @"""" + nombreSerie + @"""" + @",""type"":" + @"""" + tipo + @"""" + @"},";
            //dataColumn.Add(inicio);
            //addRow(datosSerie);
        }
        public void addSerie(string id, string nombreSerie, string tipo, List<string> datosSerie, string color)
        {
            Serie serie = new Serie();
            serie.id = id;
            serie.nombreSerie = nombreSerie;
            serie.tipo = tipo;
            serie.color = color;
            serie.IsInt = false;
            serie.IsDouble = false;
            serie.datosStringSerie = datosSerie;
            dataChart.Add(serie);

            if (serieMasLarga <= datosSerie.Count)
                serieMasLarga = datosSerie.Count;

            //string inicio = @"{ ""id"":" + @"""" + nombreSerie + @"""" + @",""type"":" + @"""" + tipo + @"""" + @"},";
            //dataColumn.Add(inicio);
            //addRow(datosSerie);
        }
        private void addRow(List<int> datosSerie)
        {
            //string row = ""; ;
            //string aux = @"{ ""c"":[";

            //for (int i = 0; i < datosSerie.Count; i++)
            //{
            //    row += @"{ ""v"":" + datosSerie[i].ToString() + @"},";
            //}

            //row = row.Substring(0, row.Length - 1);

            //aux += row + @"]},";
            //dataChart.Add(aux);
        }
        private void addRow(List<string> datosSerie)
        {
            //string row = ""; ;
            //string aux = @"{ ""c"":[";

            //for (int i = 0; i < datosSerie.Count; i++)
            //{
            //    row += @"{ ""v"":" + @"""" + datosSerie[i] + @"""" +@"},";
            //}

            //row = row.Substring(0, row.Length - 1);

            //aux += row + @"]},";
            //dataChart.Add(aux);
        }
        public string[] getJson()
        {
            string[] json = new string[5];
            string aux1 = "";
            string aux2 = "";
            string aux3 = "";
            string[] listaAux = new string[serieMasLarga];
            string colores = "";

            aux1 = @"{ ""cols"": [";
            colores = @"[";
            int count = 0;
            foreach (Serie serie in dataChart)
            {
                aux1 += @"{ ""id"":" + @"""" + serie.nombreSerie + @"""" + @",""label"":" + @"""" + serie.nombreSerie + @"""" + @",""type"":" + @"""" + serie.tipo + @"""" + @"},";
                if (customTooltip)
                {
                    aux1 += @"{ ""id"": """", ""role"": ""tooltip"", ""type"": ""string"", ""p"" : { ""html"" : true } },";
                }
                if (count != 0)
                {
                    colores += @"""" + serie.color + @"""" + @",";
                }
                count++;
            }
            colores = colores.Substring(0, colores.Length - 1);
            colores += @"]";
            aux1 = aux1.Substring(0, aux1.Length - 1);
            aux1 += @"],""rows"": [";

            int posicionAux = 0;
            int posicionAux2 = dataChart.Count;

            foreach (Serie serie in dataChart)
            {
                for (int i = 0; i < serieMasLarga; i++)
                {
                    string dato = "";
                    string toolTip = "";

                    if (customTooltip)
                    {
                        if (dataChart[0].datosStringSerie[i] == null || dataChart[0].datosStringSerie[i] == "")
                        {
                            toolTip = "Serie: " + "<br>";
                            toolTip += "Año: " + "<br>";
                        }
                        else
                        {
                            toolTip = "Serie: " + serie.nombreSerie + "<br>";
                            toolTip += "Año: " + dataChart[0].datosStringSerie[i] + "<br>";
                        }
                    }

                    if (posicionAux == 0)
                    {
                        dato = @"{ ""c"":[";
                    }

                    try
                    {
                        if (serie.IsString)
                        {
                            dato += @"{ ""v"":" + @"""" + serie.datosStringSerie[i] + @"""" + @"},";

                            if (customTooltip)
                            {
                                toolTip += "Monto : ";

                                if (serie.datosStringSerie[i] != null)
                                {
                                    toolTip += serie.datosStringSerie[i];
                                }
                                dato += @"{ ""v"":" + @"""" + toolTip + @"""" + @"},";
                            }
                        }
                        else
                        {
                            if (serie.IsInt)
                            {
                                dato += @"{ ""v"":" + serie.datosIntSerie[i] + @"},";

                                if (customTooltip)
                                {
                                    toolTip += "Monto : ";

                                    if (serie.datosIntSerie[i].ToString() != "")
                                    {
                                        toolTip += serie.datosIntSerie[i].ToString();
                                    }

                                    dato += @"{ ""v"":" + @"""" + toolTip + @"""" + @"},";
                                }
                            }
                            else
                            {
                                dato += @"{ ""v"":" + serie.datosDoubleSerie[i].ToString().Replace(",", ".") + @"},";

                                if (customTooltip)
                                {
                                    toolTip += "Monto : ";

                                    if (serie.datosDoubleSerie[i].ToString() != "")
                                    {
                                        toolTip += serie.datosDoubleSerie[i].ToString().Replace(",", ".");
                                    }

                                    dato += @"{ ""v"":" + @"""" + toolTip + @"""" + @"},";
                                }
                            }
                        }
                    }
                    catch
                    {
                        dato += @"{ ""v"":" + @"null" + @"},";
                        if (customTooltip)
                        {
                            dato += @"{ ""v"":" + @"""" + "HOLA MUNDO" + @"""" + @"},";
                        }
                    }
                    finally
                    {

                        if (posicionAux == posicionAux2 - 1)
                        {
                            dato = dato.Substring(0, dato.Length - 1) + @"]},";
                        }

                        listaAux[i] += dato;
                    }
                }
                posicionAux++;
            }

            int recorridoPenultimo = listaAux.Length - 1;
            for (int i = 0; i < recorridoPenultimo; i++)
            {
                aux2 += listaAux[i];
            }
            aux2 += listaAux[recorridoPenultimo].Substring(0, listaAux[recorridoPenultimo].Length - 1);

            aux3 = @"]}";

            json[0] = aux1 + aux2 + aux3;
            json[1] = colores;
            json[2] = @"" + titulo + @"";
            json[3] = @"" + formatter + @"";
            json[4] = @"" + dataChart.Count + @"";

            return json;
        }

        public void Load(Grafico_googleChart_Area gChart, string tipo)
        {
            titulo = gChart.Titulo;
            List<string> años = new List<string>();

            Serie serie = new Serie();
            serie.nombreSerie = "Años";
            serie.tipo = "string";
            serie.color = "#E41ECC";
            serie.IsInt = false;
            serie.IsDouble = false;
            serie.datosStringSerie = años;
            dataChart.Add(serie);

            if (tipo == "porcentaje")
            {
                formatter = "2";
                foreach (Area_googleChart_Area linea in gChart.Area_googleChart_Area)
                {
                    años.AddRange(linea.Datos_googleChart_Area.Select(r => r.Ano).ToList());
                    addSerie(linea.IdArea.ToString(), linea.Titulo, "number", linea.Datos_googleChart_Area.Select(r => r.ValorPorcentaje.Value).ToList(), linea.Color);
                }
            }

            if (tipo == "peso")
            {
                formatter = "1";
                foreach (Area_googleChart_Area linea in gChart.Area_googleChart_Area)
                {
                    años.AddRange(linea.Datos_googleChart_Area.Select(r => r.Ano).ToList());
                    addSerie(linea.IdArea.ToString(), linea.Titulo, "number", linea.Datos_googleChart_Area.Select(r => r.ValorPeso.Value).ToList(), linea.Color);
                }
            }
            años = años.Distinct().ToList();
            dataChart[0].datosStringSerie = años;

            if (serieMasLarga <= años.Count)
                serieMasLarga = años.Count;

        }
        public static googleChart_Area Example()
        {
            List<string> serie1 = new List<string>();
            serie1.Add("1990");
            serie1.Add("1992");
            serie1.Add("1994");
            serie1.Add("1996");
            serie1.Add("1998");
            serie1.Add("2000");
            serie1.Add("2002");
            serie1.Add("2004");
            serie1.Add("2006");
            serie1.Add("2008");


            List<long> serie2 = new List<long>();
            serie2.Add(1050125);
            serie2.Add(2135121);
            serie2.Add(3165321);
            serie2.Add(4087632);
            serie2.Add(5257974);
            serie2.Add(6125690);
            serie2.Add(6347894);
            serie2.Add(7134064);
            serie2.Add(7348532);
            serie2.Add(8153732);

            List<long> serie3 = new List<long>();
            serie3.Add(1350125);
            serie3.Add(2435121);
            serie3.Add(3365321);
            serie3.Add(4387632);
            serie3.Add(5457974);
            serie3.Add(6425690);
            serie3.Add(6647894);
            serie3.Add(7434064);
            serie3.Add(7548532);
            serie3.Add(8353732);

            List<long> serie4 = new List<long>();
            serie4.Add(1650125);
            serie4.Add(2735121);
            serie4.Add(3565321);
            serie4.Add(4687632);
            serie4.Add(5657974);
            serie4.Add(6725690);
            serie4.Add(6847894);
            serie4.Add(7534064);
            serie4.Add(7738532);
            serie4.Add(8553732);

            List<long> serie5 = new List<long>();
            serie5.Add(1850125);
            serie5.Add(2835121);
            serie5.Add(3765321);
            serie5.Add(4887632);
            serie5.Add(5957974);
            serie5.Add(6825690);
            serie5.Add(6947894);
            serie5.Add(7834064);
            serie5.Add(7948532);
            serie5.Add(8853732);

            googleChart_Area gChart = new googleChart_Area();

            gChart.setTitulo("Según clasificación funcional del gasto público Cifras en millones de Pesos de 2014");
            gChart.setFormato("$");
            gChart.setCustomTooltip(true);
            gChart.addSerie("1", "Year", "string", serie1, "#E41ECC");
            gChart.addSerie("2", "Gasto Educacion", "number", serie2, "#e7711b");
            gChart.addSerie("3", "Gasto Salud", "number", serie3, "#f1ca3a");
            gChart.addSerie("4", "Gasto Proteccion Social", "number", serie4, "#6f9654");
            gChart.addSerie("5", "Gasto Vivienda y Urbanismo", "number", serie5, "#1c91c0");

            return gChart;
        }

        public class Serie
        {
            public Serie()
            {
                datosStringSerie = new List<string>();
                datosIntSerie = new List<long>();
                datosDoubleSerie = new List<double>();
                IsString = true;
                IsInt = true;
                IsDouble = true;
            }
            public bool IsString { get; set; }
            public bool IsInt { get; set; }
            public bool IsDouble { get; set; }
            public string nombreSerie { get; set; }
            public string id { get; set; }
            public string tipo { get; set; }
            public string color { get; set; }
            public List<string> datosStringSerie { get; set; }
            public List<long> datosIntSerie { get; set; }
            public List<double> datosDoubleSerie { get; set; }
        }
    }
    public class Timeline
    {
        public Timeline()
        {
            title = new Title();
            events = new List<Event>();
        }

        public Title title { get; set; }
        public List<Event> events { get; set; }
        public void Load(Grafico_Timeline timeline)
        {
            Padre_Grafico_Timeline padre = timeline.Padre_Grafico_Timeline.First();
            title.text.text = padre.Texto;
            title.text.headline = padre.Headline;
            title.media.url = padre.Url;
            title.media.credit = padre.Credito;
            title.media.caption = padre.Caption;

            foreach (Hijo_Grafico_Timeline hijoTimeline in padre.Hijo_Grafico_Timeline)
            {
                Event evento = new Event();
                evento.text.text = hijoTimeline.Texto;
                evento.text.headline = hijoTimeline.Headline;

                evento.media.url = hijoTimeline.Url;
                evento.media.credit = hijoTimeline.Credito;
                evento.media.caption = hijoTimeline.Caption;

                evento.start_date.year = hijoTimeline.Yearr;
                evento.start_date.month = hijoTimeline.Monthh;
                evento.start_date.day = hijoTimeline.Dayy;

                events.Add(evento);
            }

        }

        public string getJson()
        {
            string json = "";

            json = @"{ ""title"":" + title.getJson() + @", ""events"": [";

            foreach (Event evento in events)
            {
                json += evento.getJsonEvent() + @",";
            }
            json = json.Substring(0, json.Length - 1) + @"]}";


            return json;
        }

        public class Title
        {
            public Title()
            {
                media = new Media();
                text = new Text();
            }

            public Media media { get; set; }
            public Text text { get; set; }
            public string getJson()
            {
                string json = "";

                json = @"{" + media.getJsonMedia() + @"," + text.getJsonText() + @"}";

                return json;
            }
        }
        public class Event
        {
            public Event()
            {
                media = new Media();
                start_date = new Start_date();
                text = new Text();
            }
            public Media media { get; set; }
            public Start_date start_date { get; set; }
            public Text text { get; set; }

            public string getJsonEvent()
            {
                string json = @"{" + media.getJsonMedia() + @"," + start_date.getJsonStart_date() + @"," + text.getJsonText() + @"}";
                return json;
            }
        }
        public class Media
        {
            public string url { get; set; }
            public string caption { get; set; }
            public string credit { get; set; }

            public string getJsonMedia()
            {
                string json = "";

                json = @" ""media"": { 
                          ""url"":" + @"""" + url + @"""" +
                          @",""caption"":" + @"""" + caption + @"""" +
                          @",""credit"":" + @"""" + credit + @"""" +
                       @"}";

                return json;
            }
        }
        public class Text
        {
            public string headline { get; set; }
            public string text { get; set; }

            public string getJsonText()
            {
                string json = "";

                json = @" ""text"": { 
                          ""text"":" + @"""" + text + @"""" + @"
                          ,""headline"":" + @"""" + headline + @"""" +
                       @"}";

                return json;
            }
        }
        public class Start_date
        {
            public string month { get; set; }
            public string day { get; set; }
            public string year { get; set; }

            public string getJsonStart_date()
            {
                string json = "";

                json = @" ""start_date"": { 
                          ""month"":" + @"""" + month + @"""" +
                          @",""day"":" + @"""" + day + @"""" +
                          @",""year"":" + @"""" + year + @"""" +
                       @"}";

                return json;
            }
        }
    }
    public class googleChart_LineasSeparadas
    {
        public googleChart_LineasSeparadas()
        {
            dataChart = new List<Serie>();
            customTooltip = false;
            serieMasLarga = 0;
            formatter = "MM $";
        }

        private string titulo { get; set; }
        private string formatter { get; set; }
        public bool customTooltip { get; set; }
        public List<Serie> dataChart { get; set; }
        private List<string> dataColumn { get; set; }
        private List<string> _dataChart { get; set; }
        private int serieMasLarga { get; set; }
        private string json { get; set; }

        public void setTitulo(string titulo)
        {
            this.titulo = titulo;
        }
        public void setFormato(string formatter)
        {
            this.formatter = formatter;
        }
        public void setCustomTooltip(bool customToolTip)
        {
            this.customTooltip = customTooltip;
        }
        public void addSerie(string id, string nombreSerie, string tipo, List<int> datosSerie, string color)
        {
            Serie serie = new Serie();
            serie.id = id;
            serie.nombreSerie = nombreSerie;
            serie.tipo = tipo;
            serie.color = color;
            serie.IsString = false;
            serie.IsDouble = false;
            serie.datosIntSerie = datosSerie;
            dataChart.Add(serie);

            if (serieMasLarga <= datosSerie.Count)
                serieMasLarga = datosSerie.Count;

            //string inicio = @"{ ""id"":" + @"""" + nombreSerie + @"""" + @",""type"":" + @"""" + tipo + @"""" + @"},";
            //dataColumn.Add(inicio);
            //addRow(datosSerie);
        }
        public void addSerie(string id, string nombreSerie, string tipo, List<double> datosSerie, string color)
        {
            Serie serie = new Serie();
            serie.id = id;
            serie.nombreSerie = nombreSerie;
            serie.tipo = tipo;
            serie.color = color;
            serie.IsString = false;
            serie.IsInt = false;
            serie.datosDoubleSerie = datosSerie;
            dataChart.Add(serie);

            if (serieMasLarga <= datosSerie.Count)
                serieMasLarga = datosSerie.Count;

            //string inicio = @"{ ""id"":" + @"""" + nombreSerie + @"""" + @",""type"":" + @"""" + tipo + @"""" + @"},";
            //dataColumn.Add(inicio);
            //addRow(datosSerie);
        }
        public void addSerie(string id, string nombreSerie, string tipo, List<string> datosSerie, string color)
        {
            Serie serie = new Serie();
            serie.id = id;
            serie.nombreSerie = nombreSerie;
            serie.tipo = tipo;
            serie.color = color;
            serie.IsInt = false;
            serie.IsDouble = false;
            serie.datosStringSerie = datosSerie;
            dataChart.Add(serie);

            if (serieMasLarga <= datosSerie.Count)
                serieMasLarga = datosSerie.Count;

            //string inicio = @"{ ""id"":" + @"""" + nombreSerie + @"""" + @",""type"":" + @"""" + tipo + @"""" + @"},";
            //dataColumn.Add(inicio);
            //addRow(datosSerie);
        }

        public string[] getJson()
        {
            string[] json = new string[5];
            string aux1 = "";
            string aux2 = "";
            string aux3 = "";
            string[] listaAux = new string[serieMasLarga];
            string colores = "";

            aux1 = @"{ ""cols"": [";
            colores = @"[";
            int count = 0;
            foreach (Serie serie in dataChart)
            {
                aux1 += @"{ ""id"":" + @"""" + serie.id + @"""" + @",""label"":" + @"""" + serie.nombreSerie + @"""" + @",""type"":" + @"""" + serie.tipo + @"""" + @"},";
                if (customTooltip)
                {
                    aux1 += @"{ ""id"": """", ""role"": ""tooltip"", ""type"": ""string"", ""p"" : { ""html"" : true } },";
                }
                if (count != 0)
                {
                    colores += @"""" + serie.color + @"""" + @",";
                }
                count++;
            }
            colores = colores.Substring(0, colores.Length - 1);
            colores += @"]";
            aux1 = aux1.Substring(0, aux1.Length - 1);
            aux1 += @"],""rows"": [";

            int posicionAux = 0;
            int posicionAux2 = dataChart.Count;

            foreach (Serie serie in dataChart)
            {
                for (int i = 0; i < serieMasLarga; i++)
                {
                    string dato = "";
                    string toolTip = "";

                    if (customTooltip)
                    {
                        if (dataChart[0].datosStringSerie[i] == null || dataChart[0].datosStringSerie[i] == "")
                        {
                            toolTip = "Serie: " + "<br>";
                            toolTip += "Año: " + "<br>";
                        }
                        else
                        {
                            toolTip = "Serie: " + serie.nombreSerie + "<br>";
                            toolTip += "Año: " + dataChart[0].datosStringSerie[i] + "<br>";
                        }
                    }

                    if (posicionAux == 0)
                    {
                        dato = @"{ ""c"":[";
                    }

                    try
                    {
                        if (serie.IsString)
                        {
                            dato += @"{ ""v"":" + @"""" + serie.datosStringSerie[i] + @"""" + @"},";

                            if (customTooltip)
                            {
                                toolTip += "Monto : ";

                                if (serie.datosStringSerie[i] != null)
                                {
                                    toolTip += serie.datosStringSerie[i];
                                }
                                dato += @"{ ""v"":" + @"""" + toolTip + @"""" + @"},";
                            }
                        }
                        else
                        {
                            if (serie.IsInt)
                            {
                                dato += @"{ ""v"":" + serie.datosIntSerie[i] + @"},";

                                if (customTooltip)
                                {
                                    toolTip += "Monto : ";

                                    if (serie.datosIntSerie[i].ToString() != "")
                                    {
                                        toolTip += serie.datosIntSerie[i].ToString();
                                    }

                                    dato += @"{ ""v"":" + @"""" + toolTip + @"""" + @"},";
                                }
                            }
                            else
                            {
                                if (serie.IsDouble)
                                {
                                    dato += @"{ ""v"":" + serie.datosDoubleSerie[i].ToString().Replace(",", ".") + @"},";

                                    if (customTooltip)
                                    {
                                        toolTip += "Monto : ";

                                        if (serie.datosDoubleSerie[i].ToString() != "")
                                        {
                                            toolTip += serie.datosDoubleSerie[i].ToString().Replace(",", ".");
                                        }

                                        dato += @"{ ""v"":" + @"""" + toolTip + @"""" + @"},";
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        dato += @"{ ""v"":" + @"null" + @"},";
                        if (customTooltip)
                        {
                            dato += @"{ ""v"":" + @"""" + "HOLA MUNDO" + @"""" + @"},";
                        }
                    }
                    finally
                    {

                        if (posicionAux == posicionAux2 - 1)
                        {
                            dato = dato.Substring(0, dato.Length - 1) + @"]},";
                        }

                        listaAux[i] += dato;
                    }
                }
                posicionAux++;
            }

            int recorridoPenultimo = listaAux.Length - 1;
            for (int i = 0; i < recorridoPenultimo; i++)
            {
                aux2 += listaAux[i];
            }
            aux2 += listaAux[recorridoPenultimo].Substring(0, listaAux[recorridoPenultimo].Length - 1);

            aux3 = @"]}";

            json[0] = aux1 + aux2 + aux3;
            json[1] = colores;
            json[2] = @"" + titulo + @"";
            json[3] = @"" + formatter + @"";
            json[4] = @"" + dataChart.Count + @"";

            return json;
        }

        public void Load(GraficoLineasSeparadas gChart, string tipo, string ministerio)
        {
            titulo = gChart.Nombre;
            List<string> años = new List<string>();

            string capital = "Gasto Capital";
            string corriente = "Gasto Corriente";

            Serie serie = new Serie();
            serie.nombreSerie = "Fecha";
            serie.tipo = "datetime";
            serie.color = "#E41ECC";
            serie.IsInt = false;
            serie.IsDouble = false;
            serie.datosStringSerie = años;

            dataChart.Add(serie);

            if (tipo == "aprobada")
            {
                foreach (PadreLineasSeparados linea in gChart.PadreLineasSeparados.Where(r => r.Titulo == ministerio))
                {
                    linea.HijoLineasSeparadas = linea.HijoLineasSeparadas.OrderBy(r => r.Ano).ThenBy(r => r.Mes).ToList();
                    addSerie(linea.IdPadre.ToString(), capital, "number", linea.HijoLineasSeparadas.Select(r => (r.MontoCapitalAprobado.Value / 100)).ToList(), linea.ColorCapital);
                    addSerie(linea.IdPadre.ToString(), corriente, "number", linea.HijoLineasSeparadas.Select(r => (r.MontoCorrienteAprobado.Value / 100)).ToList(), linea.ColorCorriente);
                    foreach (HijoLineasSeparadas subLinea in linea.HijoLineasSeparadas)
                    {
                        años.Add("Date(" + subLinea.Ano.ToString() + "," + (subLinea.Mes - 1).ToString() + ")");
                    }
                }
            }

            if (tipo == "vigente")
            {
                foreach (PadreLineasSeparados linea in gChart.PadreLineasSeparados.Where(r => r.Titulo == ministerio))
                {
                    linea.HijoLineasSeparadas = linea.HijoLineasSeparadas.OrderBy(r => r.Ano).ThenBy(r => r.Mes).ToList();
                    addSerie(linea.IdPadre.ToString(), capital, "number", linea.HijoLineasSeparadas.Select(r => (r.MontoCapitalVigente.Value / 100)).ToList(), linea.ColorCapital);
                    addSerie(linea.IdPadre.ToString(), corriente, "number", linea.HijoLineasSeparadas.Select(r => (r.MontoCorrienteVigente.Value / 100)).ToList(), linea.ColorCorriente);
                    foreach (HijoLineasSeparadas subLinea in linea.HijoLineasSeparadas)
                    {
                        años.Add("Date(" + subLinea.Ano.ToString() + "," + (subLinea.Mes - 1).ToString() + ")");
                    }
                }
            }


            años = años.Distinct().ToList();
            dataChart[0].datosStringSerie = años;

            if (serieMasLarga <= años.Count)
                serieMasLarga = años.Count;

        }

        public void Load2(List<HijoLineasSeparadas> gChart, string tipo, string colorCapital, string colorCorriente)
        {
            titulo = "";
            List<string> años = new List<string>();

            string capital = "Gasto Capital";
            string corriente = "Gasto Corriente";

            Serie serie = new Serie();
            serie.nombreSerie = "Fecha";
            serie.tipo = "datetime";
            serie.color = "#E41ECC";
            serie.IsInt = false;
            serie.IsDouble = false;
            serie.datosStringSerie = años;

            dataChart.Add(serie);

            if (tipo == "aprobada")
            {
                List<double> vCapital = new List<double>();
                List<double> vCorriente = new List<double>();

                var datos = gChart.OrderBy(r => r.Ano).ThenBy(r => r.Mes).GroupBy(r => new { meses = r.Mes, anos = r.Ano }).Select(r => new {
                    mesez = r.Key.meses,
                    anoz = r.Key.anos,
                    vCaptitalAprobado = r.Sum(p => p.MontoCapitalAprobado.Value),
                    vCaptitalCorriente = r.Sum(p => p.MontoCorrienteAprobado.Value)
                }).ToList();


                foreach (var data in datos)
                {
                    años.Add("Date(" + data.anoz.ToString() + "," + (data.mesez - 1).ToString() + ")");
                    vCapital.Add(data.vCaptitalAprobado / 100);
                    vCorriente.Add(data.vCaptitalCorriente / 100);
                }

                addSerie("1", capital, "number", vCapital, colorCapital);
                addSerie("1", corriente, "number", vCorriente, colorCorriente);
            }

            if (tipo == "vigente")
            {
                List<double> vCapital = new List<double>();
                List<double> vCorriente = new List<double>();

                var datos = gChart.OrderBy(r => r.Ano).ThenBy(r => r.Mes).GroupBy(r => new { meses = r.Mes, anos = r.Ano }).Select(r => new {
                    mesez = r.Key.meses,
                    anoz = r.Key.anos,
                    vCaptitalAprobado = r.Sum(p => p.MontoCapitalVigente.Value),
                    vCaptitalCorriente = r.Sum(p => p.MontoCorrienteVigente.Value)
                }).ToList();


                foreach (var data in datos)
                {
                    años.Add("Date(" + data.anoz.ToString() + "," + (data.mesez - 1).ToString() + ")");
                    vCapital.Add(data.vCaptitalAprobado / 100);
                    vCorriente.Add(data.vCaptitalCorriente / 100);
                }

                addSerie("1", capital, "number", vCapital, colorCapital);
                addSerie("1", corriente, "number", vCorriente, colorCorriente);
            }


            años = años.Distinct().ToList();
            dataChart[0].datosStringSerie = años;

            if (serieMasLarga <= años.Count)
                serieMasLarga = años.Count;

        }

        public class Serie
        {
            public Serie()
            {
                datosStringSerie = new List<string>();
                datosIntSerie = new List<int>();
                datosDoubleSerie = new List<double>();
                IsString = true;
                IsInt = true;
                IsDouble = true;
            }
            public bool IsString { get; set; }
            public bool IsInt { get; set; }
            public bool IsDouble { get; set; }
            public string id { get; set; }
            public string nombreSerie { get; set; }
            public string tipo { get; set; }
            public string color { get; set; }
            public List<string> datosStringSerie { get; set; }
            public List<int> datosIntSerie { get; set; }
            public List<double> datosDoubleSerie { get; set; }
        }
    }
    public class d3Object_Bubble_Pack
    {
        public d3Object_Bubble_Pack()
        {
            country = new GraficoCircularesVarios();
            options = new List<string>();
        }
        private string formatter { get; set; }
        private string comuna { get; set; }
        private GraficoCircularesVarios country { get; set; }
        private List<string> options { get; set; }
        private string option { get; set; }

        private string adSerieRegionPercapita(List<AbueloCirculosVarios> grandparents)
        {
            string abuelos = "";
            foreach (AbueloCirculosVarios abuelo in grandparents)
            {
                string aux = @"{""name"":" + @"""" + abuelo.Titulo + @"""" +
                               @",""size"":" + @"""" + abuelo.sizePercapita + @"""" +
                               @",""valor"":" + @"""" + abuelo.MontoPercapita + @"""" +
                               @",""color"":" + @"""" + abuelo.Color + @"""" +
                               @",""visibleTooltip"":" + @"""" + true + @"""" +
                               @",""tooltipGasto"":" + @"""" + abuelo.MontoPercapita.Value.ToString("N0") + @"""" +
                               @",""tooltipRegion"":" + @"""" + "" + @"""" +
                               @",""children"": [" + addSerieMunicipioPercapita(abuelo.PapaCirculosVarios.ToList()) +
                               @"]},";
                abuelos += aux;

            }

            abuelos = abuelos.Substring(0, abuelos.Length - 1);
            return abuelos;
        }

        private string addSerieMunicipioPercapita(List<PapaCirculosVarios> parents)
        {
            string padres = "";
            foreach (PapaCirculosVarios padre in parents)
            {
                string aux = @"{""name"":" + @"""" + padre.Titulo + @"""" +
                                @",""nameBus"":" + @"""" + padre.TituloConsulta + @"""" +
                            @",""texto1"":" + @"""" + padre.Proper1 + @"""" +
                            @",""texto2"":" + @"""" + padre.Proper2 + @"""" +
                                @",""size"":" + @"""" + padre.sizePercapita + @"""" +
                                @",""color"":" + @"""" + padre.Color + @"""" +
                                @",""valor"":" + @"""" + padre.MontoPercapita + @"""" +
                                @",""visibleTooltip"":" + @"""" + true + @"""" +
                                @",""tooltipGasto"":" + @"""" + padre.tooltipGastoPercapita + @"""" +
                                @",""tooltipRegion"":" + @"""" + padre.AbueloCirculosVarios.Titulo + @"""" +
                                //@",""Proper2"":" + @"""" + padre.Proper2 + @"""" +
                                @",""children"": [" + addSerieComunaPercapita(padre.HijoCirculosVarios.ToList()) +
                                @"]},";
                padres += aux;
            }
            padres = padres.Substring(0, padres.Length - 1);
            return padres;
        }

        private string addSerieComunaPercapita(List<HijoCirculosVarios> children)
        {
            string hijos = "";
            foreach (HijoCirculosVarios hijo in children)
            {
                string auxElem = @"{
                             ""name"":" + @"""" + hijo.Nombre + @"""" +
                                 @",""monto"":" + @"""" + hijo.sizePercapita + @"""" +
                                 @",""valor"":" + @"""" + hijo.MontoPercapita + @"""" +
                                 @",""size"":" + @"""" + hijo.sizePercapita + @"""" +
                                 @",""visibleTooltip"":" + @"""" + true + @"""" +
                                 @",""tooltipRegion"":" + @"""" + hijo.PapaCirculosVarios.AbueloCirculosVarios.Titulo + @"""" +
                                 @",""tooltipComuna"":" + @"""" + hijo.PapaCirculosVarios.Titulo + @"""" +
                                 @",""color"":" + @"""" + hijo.Color + @"""" + "},";
                hijos += auxElem;
            }
            hijos = hijos.Substring(0, hijos.Length - 1);
            return hijos;
        }

        private string adSerieRegion(List<AbueloCirculosVarios> grandparents)
        {
            string abuelos = "";
            foreach (AbueloCirculosVarios abuelo in grandparents)
            {
                string aux = @"{""name"":" + @"""" + abuelo.Titulo + @"""" +
                            @",""size"":" + @"""" + abuelo.Size + @"""" +
                            @",""valor"":" + @"""" + abuelo.Montonominal + @"""" +
                            @",""color"":" + @"""" + abuelo.Color + @"""" +
                            @",""visibleTooltip"":" + @"""" + true + @"""" +
                            @",""tooltipGasto"":" + @"""" + abuelo.Montonominal.Value.ToString("N0") + @"""" +
                            @",""tooltipRegion"":" + @"""" + "" + @"""" +
                            @",""children"": [" + addSerieMunicipio(abuelo.PapaCirculosVarios.ToList()) +
                            @"]},";
                abuelos += aux;

            }

            abuelos = abuelos.Substring(0, abuelos.Length - 1);
            return abuelos;
        }

        private string addSerieMunicipio(List<PapaCirculosVarios> parents)
        {
            string padres = "";
            foreach (PapaCirculosVarios padre in parents)
            {

                string aux = @"{""name"":" + @"""" + padre.Titulo + @"""" +
                            @",""nameBus"":" + @"""" + padre.TituloConsulta + @"""" +
                            @",""texto1"":" + @"""" + padre.Proper1 + @"""" +
                            @",""texto2"":" + @"""" + padre.Proper2 + @"""" +
                                    @",""size"":" + @"""" + padre.Size + @"""" +
                                    @",""color"":" + @"""" + padre.Color + @"""" +
                                    @",""valor"":" + @"""" + padre.Montonominal + @"""" +
                                    @",""visibleTooltip"":" + @"""" + true + @"""" +
                                    @",""tooltipGasto"":" + @"""" + padre.tooltipGastoNominal + @"""" +
                                    @",""tooltipRegion"":" + @"""" + padre.AbueloCirculosVarios.Titulo + @"""" +
                                    @",""children"": [" + addSerieComuna(padre.HijoCirculosVarios.ToList()) +
                                    @"]},";
                padres += aux;
            }
            padres = padres.Substring(0, padres.Length - 1);
            return padres;
        }

        private string addSerieComuna(List<HijoCirculosVarios> children)
        {
            string hijos = "";
            foreach (HijoCirculosVarios hijo in children)
            {
                string auxElem = @"{
                             ""name"":" + @"""" + hijo.Nombre + @"""" +
                                 @",""monto"":" + @"""" + hijo.Size.Value.ToString("N0") + @"""" +
                                 @",""valor"":" + @"""" + hijo.Montonominal + @"""" +
                                 @",""size"":" + @"""" + hijo.Size + @"""" +
                                 //@",""Proper1"":" + @"""" + hijo.Proper1 + @"""" +
                                 @",""visibleTooltip"":" + @"""" + true + @"""" +
                                 @",""tooltipRegion"":" + @"""" + hijo.PapaCirculosVarios.AbueloCirculosVarios.Titulo + @"""" +
                                 @",""tooltipComuna"":" + @"""" + hijo.PapaCirculosVarios.Titulo + @"""" +
                                 @",""color"":" + @"""" + hijo.Color + @"""" + "},";
                hijos += auxElem;
            }
            hijos = hijos.Substring(0, hijos.Length - 1);
            return hijos;
        }

        public string getJson()
        {
            string json = "";
            if (formatter == "nominal")
            {
                json = @"{""name"":" + @"""" + country.Titulo + @"""" +
                          @",""color"":" + @"""" + country.Color + @"""" +
                          @",""children"": [" + adSerieRegion(country.AbueloCirculosVarios.ToList()) +
                          @"]}";
            }
            else
            {
                json = @"{""name"":" + @"""" + country.Titulo + @"""" +
                          @",""color"":" + @"""" + country.Color + @"""" +
                          @",""children"": [" + adSerieRegionPercapita(country.AbueloCirculosVarios.ToList()) +
                          @"]}";
            }
            return json;
        }

        public void Load(GraficoCircularesVarios d3Object, string formato)
        {
            this.formatter = formato;
            country = d3Object;
        }
    }
    public class plotly_Line_Listbox
    {
        public plotly_Line_Listbox()
        {
            ejeX = new List<string>();
            series = new List<Serie>();
            serieMasLarga = 0;
            uid = 0;
            listaUids = new Dictionary<string, int>();
            layoutPercent = @"""layout"": {
                                        ""autosize"": true,
                                            ""showlegend"": false,
                                            ""paper_bgcolor"":""rgba(0, 0, 0, 0)"",
                                            ""plot_bgcolor"": ""rgba(0, 0, 0, 0)""," +
                                              //""legend"":{
                                              //""xanchor"": ""center"",
                                              //                ""yanchor"": ""top"",
                                              //                ""y"": -0.3, 
                                              //                ""x"": 0.5
                                              //            },
                                              @"""xaxis"": {
                                                    ""gridcolor"": ""#F1F1F1"",
                                                    ""autorange"":true,
                                                    fixedrange: true,
                                                    ""exponentformat"": ""none"",
                                                    ""showexponent"": ""All""
                                                },
                                              ""yaxis"": {
                                                    ""gridcolor"": ""#AAA9A9"",
                                                    ""autorange"":true,
                                                    fixedrange: true,
                                                    ""exponentformat"":""none"",                                                      
                                                    ""ticksuffix"":""%""
                                                },
                                               ""separators"":"",.""
                                    }
                                 ,";
            layoutDolar = @"""layout"": {
                                        ""autosize"": true,
                                            ""showlegend"": false,
                                            ""paper_bgcolor"":""rgba(0, 0, 0, 0)"",
                                            ""plot_bgcolor"": ""rgba(0, 0, 0, 0)""," +
                                             //""legend"":{ +
                                             //""xanchor"": ""center"",
                                             //                ""yanchor"": ""top"",
                                             //                ""y"": -0.3, 
                                             //                ""x"": 0.5
                                             //            },
                                             @"""xaxis"": {
                                                    ""gridcolor"": ""#F1F1F1"",
                                                    ""autorange"":true,
                                                    ""fixedrange"": true,
                                                    ""exponentformat"": ""none"",
                                                    ""showexponent"": ""All""
                                             
                                                },
                                              ""yaxis"": {
                                                    ""gridcolor"": ""#AAA9A9"",
                                                    ""autorange"":true,
                                                    ""fixedrange"": true,
                                                    ""exponentformat"": ""none"",
                                                    ""tickfont"": {                                                              
                                                              ""size"": ""10""                                                             
                                                            },
                                                    ""tickprefix"": ""MM$""
                                                  
                                                  },
                                                ""separators"":"",.""
                                    }
                                ,";
        }
        List<string> ejeX { get; set; }
        List<Serie> series { get; set; }
        public int serieMasLarga { get; set; }
        public int formatter { get; set; }
        private string layoutDolar { get; set; }
        private string layoutPercent { get; set; }
        private string categorias { get; set; }
        private string botonesInferiores { get; set; }
        private string botonesSelected { get; set; }
        private string tituloGlobal { get; set; }
        private string colorGlobal { get; set; }
        private int uid { get; set; }

        private Dictionary<string, int> listaUids { get; set; }
        public void addEjeX(List<string> ejeX)
        {
            this.ejeX = ejeX;
        }

        //public void addSerie(string nombre, string id, List<string> serie, string Visible, List<string> años)
        //{
        //    Serie datos = new Serie();
        //    datos.name = nombre;
        //    datos.uid = id;
        //    datos.datosStringSerie = serie;
        //    datos.IsInt = false;
        //    datos.IsDouble = false;
        //    //datos.color = color;
        //    datos.visible = Visible;
        //    datos.años = años;
        //    series.Add(datos);

        //    //if (serie.Count > serieMasLarga)
        //    //    serieMasLarga = serie.Count;

        //}
        public void addSerie(string nombre, string id, List<int?> serie, string Visible, List<string> años)
        {
            Serie datos = new Serie();
            datos.name = nombre;
            datos.uid = id;
            datos.datosDoubleSerie = serie;
            datos.isString = false;
            datos.IsInt = false;
            //datos.color = color;
            datos.visible = Visible;
            datos.años = años;
            series.Add(datos);

            if (serie.Count > serieMasLarga)
                serieMasLarga = serie.Count;
        }
        //public void addSerie(string nombre, string id, List<long> serie, string Visible, List<string> años)
        //{
        //    Serie datos = new Serie();
        //    datos.name = nombre;
        //    datos.uid = id;
        //    datos.datosIntSerie = serie;
        //    datos.isString = false;
        //    datos.IsDouble = false;
        //    /*datos.color = color;*/
        //    datos.visible = Visible;
        //    datos.años = años;
        //    series.Add(datos);

        //    if (serie.Count > serieMasLarga)
        //        serieMasLarga = serie.Count;
        //}

        public void createCategorias(List<Categoria_Plotly_Listbox> Region)
        {
            int uidGlobal = 0;
            listaUids.TryGetValue(tituloGlobal, out uidGlobal);

            categorias = @" ""global"":";
            categorias += @"{""nombreGlobal"":" + @"""" + tituloGlobal + @"""" + @",";
            categorias += @"""colorGlobal"":" + @"""#f032ff""" + @",";
            categorias += @"""uids"":" + @"""" + uidGlobal + @"""" + @"},";


            categorias += @" ""categorias"":[";

            var regiones = Region.Select(r => new
            {
                codigoRegion = r.Numero.Value,
                nombreRegion = r.Nombre,
                //colorRegion = r.Color,
                idRegion = r.IdCategoria
            }).Distinct().OrderBy(r => r.codigoRegion).ToList();

            int auxiliar = 0;
            foreach (var region in regiones)
            {
                int uidRegion = 0;
                listaUids.TryGetValue(region.nombreRegion, out uidRegion);
                categorias += @"{""codigoRegion"":" + @"""" + region.codigoRegion + @"""" + @",";
                categorias += @"""nombreRegion"":" + @"""" + "Promedio Reg  " + NumerosRomanos(auxiliar) + " " + region.nombreRegion + @"""" + @",";
                //categorias += @"""colorRegion"":" + @"""" + region.colorRegion + @"""" + @",";
                categorias += @"""idRegion"":" + @"""" + region.idRegion + @"""" + @",";
                categorias += @"""uids"":" + @"""" + uidRegion + @"""" + @",";
                categorias += @" ""comunas"":[";

                var comunas = Region.Single(r => r.IdCategoria == region.idRegion).SubCategoria_Plotly_Listbox.Select(r => new
                {
                    idComuna = r.IdSubCategoria,
                    //colorComuna = r.Categoria_Plotly_Listbox.Color,
                    nombreComuna = r.Nombre,
                }).Distinct().OrderBy(r => r.nombreComuna).ToList();
                foreach (var comuna in comunas)
                {
                    int uidComuna = 0;
                    listaUids.TryGetValue(comuna.idComuna.ToString(), out uidComuna);

                    categorias += @"{""idComuna"":" + @"""" + comuna.idComuna + @"""" + @",";
                    categorias += @"""nombreComuna"":" + @"""" + comuna.nombreComuna + @"""" + @",";
                    //categorias += @"""colorComuna"":" + @"""" + comuna.colorComuna + @"""" + @",";
                    categorias += @"""uids"":" + @"""" + uidComuna + @"""" + @"";
                    categorias += @"},";

                }
                categorias = categorias.Substring(0, categorias.Length - 1);
                categorias += @"]},";
                auxiliar++;
            }
            categorias = categorias.Substring(0, categorias.Length - 1);
            categorias += @"]";
        }
        public void createBotonesInferior(List<string> lineas, string botonSelected)
        {
            string auxCadena = "";
            string auxCadenaSelected = "";

            foreach (string nombre in lineas)
            {
                auxCadena += @"""" + nombre + @""",";
                if (nombre == botonSelected)
                {
                    auxCadenaSelected += @"" + "true" + @",";
                }
                else
                {
                    auxCadenaSelected += @"" + "false" + @",";
                }

            }
            auxCadena = auxCadena.Substring(0, auxCadena.Length - 1);
            auxCadenaSelected = auxCadenaSelected.Substring(0, auxCadenaSelected.Length - 1);

            botonesInferiores = @"""botonesInferior"": [" + @"""Total""," + auxCadena + @"]";
            if (botonSelected == "Total")
            {
                botonesSelected = @"""botonesInferiorSelected"": [" + @"" + "true," + auxCadenaSelected + @"]";
            }
            else
            {
                botonesSelected = @"""botonesInferiorSelected"": [" + @"" + "false," + auxCadenaSelected + @"]";
            }

        }
        public void createSeries(Grafico_Plotly_Listbox grafico, string busqueda)
        {
            bool estado1 = false;
            bool estado3 = true;
            this.tituloGlobal = "Promedio chile";
            this.colorGlobal = "blue";
            List<int?> con = new List<int?>();
            List<int?> con2 = new List<int?>();
            for (int i = 0; i < 9; i++) //EL 9 es por los 9 años que estan en la tabla 1998-2016
            {
                con.Add(0);
                con2.Add(0);
            }
            List<string> auxComunaAños = new List<string>();
            auxComunaAños.Add("2008");
            auxComunaAños.Add("2009");
            auxComunaAños.Add("2010");
            auxComunaAños.Add("2011");
            auxComunaAños.Add("2012");
            auxComunaAños.Add("2013");
            auxComunaAños.Add("2014");
            auxComunaAños.Add("2015");
            auxComunaAños.Add("2016");

            int contador = 0;
            List<int?> auxComunaValor = new List<int?>();
            foreach (var item in grafico.Categoria_Plotly_Listbox)
            {
                contador++;
                int contador2 = 0;
                foreach (var item2 in item.SubCategoria_Plotly_Listbox)
                {
                    if (busqueda != "Total")
                    {
                        Lineas_Plotly_Listbox var = item2.Lineas_Plotly_Listbox.Single(r => r.Titulo == busqueda);
                        contador2++;
                        auxComunaValor.Add(var.ano1);
                        auxComunaValor.Add(var.ano2);
                        auxComunaValor.Add(var.ano3);
                        auxComunaValor.Add(var.ano4);
                        auxComunaValor.Add(var.ano5);
                        auxComunaValor.Add(var.ano6);
                        auxComunaValor.Add(var.ano7);
                        auxComunaValor.Add(var.ano8);
                        auxComunaValor.Add(var.ano9);
                    }
                    else
                    {
                        var auxiliar = item2.Lineas_Plotly_Listbox.GroupBy(r => r.IdSubCategoriaFK).Select(r => new { ano1 = r.Sum(l => l.ano1), ano2 = r.Sum(l => l.ano2), ano3 = r.Sum(l => l.ano3), ano4 = r.Sum(l => l.ano4), ano5 = r.Sum(l => l.ano5), ano6 = r.Sum(l => l.ano6), ano7 = r.Sum(l => l.ano7), ano8 = r.Sum(l => l.ano8), ano9 = r.Sum(l => l.ano9) }).First();
                        contador2++;
                        auxComunaValor.Add(auxiliar.ano1);
                        auxComunaValor.Add(auxiliar.ano2);
                        auxComunaValor.Add(auxiliar.ano3);
                        auxComunaValor.Add(auxiliar.ano4);
                        auxComunaValor.Add(auxiliar.ano5);
                        auxComunaValor.Add(auxiliar.ano6);
                        auxComunaValor.Add(auxiliar.ano7);
                        auxComunaValor.Add(auxiliar.ano8);
                        auxComunaValor.Add(auxiliar.ano9);
                    }

                    List<int?> copia1 = new List<int?>(auxComunaValor);
                    addSerie(item2.Nombre, uid.ToString(), copia1, estado1.ToString().ToLower(), auxComunaAños);
                    listaUids.Add(item2.IdSubCategoria.ToString(), uid);
                    uid++;
                    for (int i = 0; i < auxComunaValor.Count; i++)
                    {
                        con[i] = con[i] + auxComunaValor[i];
                    }
                    auxComunaValor.Clear();
                }

                for (int i = 0; i < con.Count; i++)
                {
                    auxComunaValor.Add(Convert.ToInt32(con[i] / contador2));
                    con[i] = 0;
                }
                List<int?> copia2 = new List<int?>(auxComunaValor);
                addSerie(item.Nombre, uid.ToString(), copia2, estado1.ToString().ToLower(), auxComunaAños);
                listaUids.Add(item.Nombre, uid);
                uid++;
                for (int i = 0; i < auxComunaValor.Count; i++)
                {
                    con2[i] = con2[i] + auxComunaValor[i];
                }
                auxComunaValor.Clear();
            }
            for (int i = 0; i < con2.Count; i++)
            {
                auxComunaValor.Add(Convert.ToInt32(con2[i] / contador));
            }
            List<int?> copia3 = new List<int?>(auxComunaValor);
            addSerie(tituloGlobal, uid.ToString(), copia3, estado3.ToString().ToLower(), auxComunaAños);
            listaUids.Add(tituloGlobal, uid);
            uid++;
            addEjeX(auxComunaAños);
        }

        public string getJson()
        {
            string json = @"{ ""data"": [";
            string visibles = @" ""visibles"": [";
            List<string> linea = new List<string>();

            string auxX = @"{ ""x"": [";

            foreach (string datoX in ejeX)
            {
                auxX += datoX + @",";
            }
            auxX = auxX.Substring(0, auxX.Length - 1);
            auxX += @"],";

            string auxY = "";

            foreach (Serie serie in series)
            {
                auxY = @" ""visible"":" + serie.visible + @", 
                          ""y"": [";
                visibles += serie.visible.ToString().ToLower() + @",";
                int positionAux = 0;

                for (int i = 0; i < serieMasLarga; i++)
                {
                    string auxStringAño = ejeX[i];
                    if (serie.años.Contains(auxStringAño))
                    {
                        try
                        {
                            if (serie.isString)
                            {
                                auxY += serie.datosStringSerie[i - positionAux] + @",";
                            }
                            if (serie.IsDouble)
                            {
                                auxY += serie.datosDoubleSerie[i - positionAux] + @",";
                            }
                            if (serie.IsInt)
                            {
                                auxY += serie.datosIntSerie[i - positionAux] + @",";
                            }
                        }
                        catch (Exception ex) { var a = ex; auxY += @"null" + @","; }
                    }
                    else
                    {
                        auxY += @"null" + @",";
                        positionAux++;
                    }
                }

                auxY = auxY.Substring(0, auxY.Length - 1) + @"],";
                auxY += @"""type"": ""scatter"",";
                auxY += @"""name"":" + @"""" + serie.name + @"""" + @",";
                auxY += @"""line"": { ""color"":""#f032ff"" } ,";
                //auxY += @"""line"": { ""color"":" + @"""" + serie.color + @"""" + @" } ,";
                auxY += @"""connectgaps"":true,";
                auxY += @"""uid"":" + @"""" + serie.uid + @"""" + @"},";
                linea.Add(auxX + auxY);
            }

            string auxLinea = linea[linea.Count - 1];
            auxLinea = auxLinea.Substring(0, auxLinea.Length - 1);
            linea[linea.Count - 1] = auxLinea + @"],";

            visibles = visibles.Substring(0, visibles.Length - 1) + @"]";


            foreach (string serie in linea)
            {
                json += serie;
            }


            json += layoutDolar;

            json += visibles + @",";

            json += categorias + @",";
            json += botonesInferiores + @",";
            json += botonesSelected;
            json += @"}";
            return json;
        }
        public void Load(Grafico_Plotly_Listbox plotly, string busqueda)
        {
            List<Categoria_Plotly_Listbox> lista = plotly.Categoria_Plotly_Listbox.Distinct().ToList();
            List<string> listanombres = plotly.Lineas_Plotly_Listbox.Select(r => r.Titulo).Distinct().ToList();
            createSeries(plotly, busqueda);
            createCategorias(lista);
            createBotonesInferior(listanombres, busqueda);
        }

        public class Serie
        {
            public Serie()
            {
                datosStringSerie = new List<string>();
                datosIntSerie = new List<long>();
                IsInt = true;
                IsDouble = true;
                isString = true;
            }

            public string name { get; set; }
            public string visible { get; set; }
            public string uid { get; set; }
            //public string color { get; set; }
            public bool IsInt { get; set; }
            public bool IsDouble { get; set; }
            public bool isString { get; set; }
            public List<long> datosIntSerie { get; set; }
            public List<int?> datosDoubleSerie { get; set; }
            public List<string> datosStringSerie { get; set; }
            public List<string> años { get; set; }
        }

        public string NumerosRomanos(int numero)
        {
            switch (numero)
            {
                case 0: return "I";
                case 1: return "II";
                case 2: return "III";
                case 3: return "IV";
                case 4: return "V";
                case 5: return "VI";
                case 6: return "VII";
                case 7: return "VIII";
                case 8: return "IX";
                case 9: return "X";
                case 10: return "XI";
                case 11: return "XII";
                case 12: return "XIII";
                case 13: return "XIV";
                case 14: return "XV";
                case 15: return "XVI";
                case 16: return "XVII";
                case 17: return "XVIII";
                default: return "XXXXX";
            }
        }
    }
}
