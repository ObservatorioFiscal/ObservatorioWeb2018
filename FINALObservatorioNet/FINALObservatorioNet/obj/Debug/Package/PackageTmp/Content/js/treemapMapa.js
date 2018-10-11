document.write('<input type="hidden" id="idRegion" name="idRegion" />');
document.write('<input type="hidden" id="tipoTreemap" name="tipoTreemap" />');
document.write('<input type="hidden" id="notSearchTreemap" name="notSearchTreemap" />');
document.write('<input type="hidden" id="nivel999" name="nivel999" value="1" />');
document.write('<input type="hidden" id="baseColor" name="baseColor" />');
document.write('<input type="hidden" id="valorRadio" name="valorRadio" />');
document.write('<input type="hidden" id="idTreemap" name="idTreemap" />');
document.write('<input type="hidden" id="colortemporal" name="colortemporal" />');
document.write('<input type="hidden" id="serviciotempo" name="serviciotempo" />');
document.write('<input type="hidden" id="IdCuadrado" name="IdCuadrado" />');



document.write('<link rel="stylesheet" type="text/css" href="../../Content/lib/ammap/ammap.css">');
document.write('<script src="../../Content/lib/ammap/ammap.js"></script>');
document.write('<script src="../../Content/lib/ammap/maps/js/chileHigh.js"></script>');
document.write('<script src="../../Content/lib/ammap/maps/js/ChileSVG.js"></script>');
document.write('<script src="../../Content/lib/amchart/amcharts.js"></script>');



    function classes(root,tipoFamilia) {
        var classes = [];

        function recurse(name, node, tipoFamilia) {
            if (node.children) node.children.forEach(function (child) {
                recurse(node.name, child,null);
            });
            else classes.push({
                packageName: name,
                className: node.name,
                fondo: node.color,
                value: node.size,
                nivelFamily: node.nivel,
                tipoFamilia: node.tipo,
                valueTooltip: node.valueTooltip,
                id:node.id
            });
        }

        recurse(null, root,tipoFamilia);
        return {
            children: classes
        };
    }
    function drawChart(tipoFamilia, idRegion)
    {
        if (idRegion == null) { idRegion = ""; }
        var RadioButton = (document.getElementById("valorRadio").value != "percapita") ? "nominal" : "percapita";
        var margin = { top: 10, right: 10, bottom: 10, left: 10 };
        var width = document.getElementById("chart_div").parentElement.clientWidth - margin.left - margin.right;
        var height = document.getElementById("chart_div").parentElement.clientHeight - margin.top - margin.bottom;
        var treemap = d3.layout.treemap().size([width, height]).sticky(true).sort(function (a, b) {return a.value - b.value;}).value(function (d) { return d.value; });
        var div = d3.select("body").select("#chart_div").append("div").attr("id", "treeChart").style("position", "relative").style("height", (height + margin.top + margin.bottom) + "px").style("top", margin.top + "px");
        var tooltip = d3.select("body").append("div").attr('class', 'tooltiptremap');
        var url = "";
        switch (document.getElementById("nivel999").value) {
            case "1":
                url = "../../../AjaxGraficos/GetDatosQuienInvierte29999nivel1/" + idRegion + ";" + RadioButton;
                break;
            case "2":
                url = "../../../AjaxGraficos/GetDatosQuienInvierte29999nivel2/" + idRegion + ";" + RadioButton + ";" + tipoFamilia;
                break;
            case "3":
                url = "../../../AjaxGraficos/GetDatosQuienInvierte29999nivel3/" + idRegion + ";" + tipoFamilia + ";" + RadioButton;
                break;
        }
            d3.json(url, function (error, root) {
                var node = div.selectAll(".node")
                  .data(treemap.nodes(classes(root, null)))
                  .enter().append("div")
                  .attr("class", "node")
                  .attr("Id", function (d) {
                      return d.id;
                  })
                  .call(position)
                  .style("background", function (d)
                  {
                      return d.fondo;
                  })
                  .on("mouseover", function (d) {
                      tooltip.append("p").attr("id", "texto1").text("Ministerio: " + d.className);
                      tooltip.append("p").attr("id", "texto2").text("Servicio: " + d.tipoFamilia);
                      if ($("input[name=optradio]:checked").val() == "nominal") {
                          tooltip.append("p").attr("id", "texto3").text("Total: MM $" + d.valueTooltip);
                      }
                      else{
                          tooltip.append("p").attr("id", "texto3").text("Total: $" + d.valueTooltip);
                      }
                      tooltip.style("visibility", "visible");
                  })
                  .on("mousemove", function () {
                      return tooltip.style("top", (d3.event.pageY - 10) + "px").style("left", (d3.event.pageX + 10) + "px");
                  })
                  .on("mouseout", function () {
                      tooltip.style("visibility", "hidden");
                      $("#texto1").remove();
                      $("#texto2").remove();
                      $("#texto3").remove();

                      return 1;
                  })
                  .on("click", function (d) {
                      switch (document.getElementById("nivel999").value) {
                          case "1":
                              document.getElementById("nivel999").value = "2";
                              document.getElementById("baseColor").value = d.fondo;
                              $("#texto1").remove();
                              $("#texto2").remove();
                              $("#texto3").remove();
                              tooltip.style("visibility", "hidden");
                              document.getElementById("treeChart").remove();
                              document.getElementById("tipoTreemap").value = d.tipoFamilia;
                              document.getElementById("idTreemap").value = d.className;
                              document.getElementById("colortemporal").value = d.fondo;
                              var idRegion = document.getElementById("idRegion").value;
                              drawChart(d.tipoFamilia, idRegion);
                              drawChartMap(d.fondo, d.tipoFamilia, idRegion, null, null);
                              break;
                          case "2":
                              document.getElementById("nivel999").value = "3";
                              document.getElementById("baseColor").value = d.fondo;
                              document.getElementById("IdCuadrado").value = d.id;
                              $("#texto1").remove();
                              $("#texto2").remove();
                              $("#texto3").remove();
                              tooltip.style("visibility", "hidden");
                              document.getElementById("tipoTreemap").value = d.tipoFamilia;
                              document.getElementById("idTreemap").value = d.className;
                              var idRegion = document.getElementById("idRegion").value;
                              var idauxiliar = d.id;
                              var colorauxiliar = d.fondo;
                              document.getElementById("colortemporal").value = colorauxiliar;
                              document.getElementById("serviciotempo").value = d.className;
                              var elementosObtenidos = document.getElementsByClassName("node");
                              for (var i = 1; i < elementosObtenidos.length;i++){
                                  elementosObtenidos[i].style.backgroundColor = '#dedede';
                              }
                              document.getElementById(d.id).style.background = colorauxiliar;
                              getTitulo(d.className);
                              if (idRegion !=null){
                                  drawChartMap(d.fondo, d.tipoFamilia, idRegion, null, d.className);
                              }
                                break;
                          case "3":
                              document.getElementById("nivel999").value = "3";
                              document.getElementById("baseColor").value = d.fondo;
                              document.getElementById("IdCuadrado").value = d.id;
                              $("#texto1").remove();
                              $("#texto2").remove();
                              $("#texto3").remove();
                              tooltip.style("visibility", "hidden");
                              document.getElementById("tipoTreemap").value = d.tipoFamilia;
                              document.getElementById("idTreemap").value = d.className;
                              var idRegion = document.getElementById("idRegion").value;
                              var idauxiliar = d.id;
                              var colorauxiliar = document.getElementById("colortemporal").value;
                              var elementosObtenidos = document.getElementsByClassName("node");
                              for (var i = 1; i < elementosObtenidos.length; i++) {
                                  elementosObtenidos[i].style.backgroundColor = '#dedede';
                              }
                              document.getElementById(d.id).style.background = colorauxiliar;
                              document.getElementById("serviciotempo").value = d.className;
                              getTitulo(d.className)
                              if (idRegion != null) {
                                  drawChartMap(d.fondo, d.tipoFamilia, idRegion, null, d.className);
                              }
                                break;
                      }
                  })

                node.append("text")
                .attr("class","textos")
                .text(function (d) {
                    if (d.className != null) {
                        var largo = d.className.length;
                        var x = d.dx;
                        var y = d.dy;
                        if (((largo / 3) < d.dx) && (d.dy > 80)) {
                            return d.children ? null : d.className;
                        }
                        else {
                        }
                    }
                });
        });
        function position() {
            this.style("left", function (d) { return d.x + "px"; })
                .style("top", function (d) { return d.y + "px"; })
                .style("width", function (d) { return Math.max(0, d.dx - 1) + "px"; })
                .style("height", function (d) { return Math.max(0, d.dy - 1) + "px"; });
        }
        getTitulo();
    }

    function drawChartMap(baseColor, parametroTreemap, idRegion, back, servicio) {

        var getUrl = window.location;
        var baseUrl = getUrl.protocol + "//" + getUrl.host + "/" + getUrl.pathname.split('/')[1] + "/" + getUrl.pathname.split('/')[2] + "/";

        if (baseColor == null && idRegion == null && back == null) {
            var minHeight = document.getElementById("chart_divMap").parentElement.clientHeight * 0.3;
            var Height = document.getElementById("chart_divMap").parentElement.clientHeight * 1.40;
            var Width = document.getElementById("chart_divMap").parentElement.clientWidth * 0.45;
        }
        else {
            var minHeight = document.getElementById("chart_divMap").style.height;
            var Height = document.getElementById("chart_divMap").style.height;
            var Width = document.getElementById("chart_divMap").style.width;
        }
        
        var limit = 200;
        var marginLeft = ($(window).width()) / 12; // 2/3 de largo de ventana
        marginLeft = -3.3 * marginLeft;

        if (minHeight < limit) {
            minHeight = limit;
        }
        if (Height < limit) {
            Height = limit;
        }
        if (Height * (2 / 3) > Width) {
            Width = Width * 2;
        }

        document.getElementById("chart_divMap").style.minHeight = minHeight + "px";
        document.getElementById("chart_divMap").style.height = Height + "px";
        document.getElementById("chart_divMap").style.width = Width + "px";
        document.getElementById("chart_divMap").style.marginLeft = "auto";
        document.getElementById("chart_divMap").style.marginRight = "auto";
        var idCuadrado = document.getElementById("idTreemap").value;
        var radio = document.getElementById("valorRadio").value;
        var jsonData = $.ajax({
            url: "../../AjaxGraficos/GetDatosQuienInvierte19999",
            data: { 'parametroTreemap': parametroTreemap, 'idCiudad': idRegion, 'tipoValor': radio, 'servicio': servicio },
            dataType: "json",
            async: false
        }).responseJSON;



        //COLOR FINAL / COLOR BASE / CANTIDAD DE COLORES
        if (baseColor != null) {
            var colores = generateColor('#ffffff', baseColor, jsonData.dataCantColores);
            if (jsonData.dataCantColores > 5) {
                var colores = generateColor(colores[12], baseColor, jsonData.dataCantColores);
            }
            document.getElementById("cuadraditomax").style.backgroundColor = baseColor;
            document.getElementById("cuadraditomin").style.backgroundColor = colores[12];
        } else {
            var colores = generateColor('#ffffff', '#F21818', jsonData.dataCantColores);
            document.getElementById("cuadraditomax").style.backgroundColor = '#F21818';
            document.getElementById("cuadraditomin").style.backgroundColor = '#ffb9b9';
        }

        var region = document.getElementById("idRegion").value;
        var contador;
        if (region != "") {
            for (var item = 0; item < 15; item++) {
                jsonData.dataIntColor[item] = 0;
            }

            switch (region) {
                case "CL-AP":
                    jsonData.dataIntColor[0] = 1;
                    break;
                case "CL-TA":
                    jsonData.dataIntColor[1] = 1;
                    break;
                case "CL-AN":
                    jsonData.dataIntColor[2] = 1;
                    break;
                case "CL-AT":
                    jsonData.dataIntColor[3] = 1;
                    break;
                case "CL-CO":
                    jsonData.dataIntColor[4] = 1;
                    break;
                case "CL-VS":
                    jsonData.dataIntColor[5] = 1;
                    break;
                case "CL-RM":
                    jsonData.dataIntColor[6] = 1;
                    break;
                case "CL-LI":
                    jsonData.dataIntColor[7] = 1;
                    break;
                case "CL-ML":
                    jsonData.dataIntColor[8] = 1;
                    break;
                case "CL-BI":
                    jsonData.dataIntColor[9] = 1;
                    break;
                case "CL-AR":
                    jsonData.dataIntColor[10] = 1;
                    break;
                case "CL-LR":
                    jsonData.dataIntColor[11] = 1;
                    break;
                case "CL-LL":
                    jsonData.dataIntColor[12] = 1;
                    break;
                case "CL-AI":
                    jsonData.dataIntColor[13] = 1;
                    break;
                case "CL-MA":
                    jsonData.dataIntColor[14] = 1;
                    break;
            }
        }


        var map = AmCharts.makeChart("chart_divMap", {
            type: "map",
            zoomOnDoubleClick: false,
            dragMap: false,
            autoResize: false,
            fontSize:12,
            balloon: {//nombre region
                color: "#000000",
                fontSize: 12
            },

            dataProvider: {
                map: "ChileSVG",
                images: [{

                }],
                areas: [
                    { id: "CL-AP", title: "Arica y Parinacota " + jsonData.dataPorcentaje[0], selectable: true, "color": colores[jsonData.dataIntColor[0]] },
                    { id: "CL-TA", title: "Tarapacá "           + jsonData.dataPorcentaje[1], selectable: true, "color": colores[jsonData.dataIntColor[1]] },
                    { id: "CL-AN", title: "Antofagasta "        + jsonData.dataPorcentaje[2], selectable: true, "color": colores[jsonData.dataIntColor[2]] },
                    { id: "CL-AT", title: "Atacama "            + jsonData.dataPorcentaje[3], selectable: true, "color": colores[jsonData.dataIntColor[3]] },
                    { id: "CL-CO", title: "Coquimbo "           + jsonData.dataPorcentaje[4], selectable: true, "color": colores[jsonData.dataIntColor[4]] },
                    { id: "CL-VS", title: "Valparaíso "         + jsonData.dataPorcentaje[5], selectable: true, "color": colores[jsonData.dataIntColor[5]] },
                    { id: "CL-RM", title: "R.M. Santiago "      + jsonData.dataPorcentaje[6], selectable: true, "color": colores[jsonData.dataIntColor[6]] },
                    { id: "CL-LI", title: "O'Higgins "          + jsonData.dataPorcentaje[7], selectable: true, "color": colores[jsonData.dataIntColor[7]] },
                    { id: "CL-ML", title: "Maule "              + jsonData.dataPorcentaje[8], selectable: true, "color": colores[jsonData.dataIntColor[8]] },
                    { id: "CL-BI", title: "Bío-Bío "            + jsonData.dataPorcentaje[9], selectable: true, "color": colores[jsonData.dataIntColor[9]] },
                    { id: "CL-AR", title: "Araucanía "          + jsonData.dataPorcentaje[10], selectable: true, "color": colores[jsonData.dataIntColor[10]] },
                    { id: "CL-LR", title: "Los Ríos "           + jsonData.dataPorcentaje[11], selectable: true, "color": colores[jsonData.dataIntColor[11]] },
                    { id: "CL-LL", title: "Los Lagos "          + jsonData.dataPorcentaje[12], selectable: true, "color": colores[jsonData.dataIntColor[12]] },
                    { id: "CL-AI", title: "Aysén "              + jsonData.dataPorcentaje[13], selectable: true, "color": colores[jsonData.dataIntColor[13]] },
                    { id: "CL-MA", title: "Magallanes "         + jsonData.dataPorcentaje[14], selectable: true, "color": colores[jsonData.dataIntColor[14]] },
                ]
            },

            areasSettings: {
                autoZoom: false,
                selectedColor: "#FAF607",
                rollOverOutlineColor: "#0851FB"
            },
            zoomControl: {
                zoomControlEnabled: false,
                homeButtonEnabled: false

            },
        });

        map.fontSize = 10;
        map.addListener("clickMapObject", function (event) {

            var idRegion = event.mapObject.id;
            var titulo = event.mapObject.enTitle;
            var tipoTreemap = document.getElementById("tipoTreemap").value;
            if (tipoTreemap == "" || tipoTreemap == undefined) { tipoTreemap = null;}

            document.getElementById("idRegion").value = idRegion;
            document.getElementById("tituloMapa").innerHTML = titulo;
            var baseColor = document.getElementById("colortemporal").value;
            if (baseColor == "") { baseColor = null; }
            var servicio = document.getElementById("serviciotempo").value;

            drawChartMap(baseColor, tipoTreemap, idRegion, null, servicio)
            document.getElementById("treeChart").remove();
            if (document.getElementById("nivel999").value ==3)
                document.getElementById("nivel999").value = 2;
            drawChart(tipoTreemap, idRegion)

        });

        map.balloon.offsetX = -20;
    }

    function generateColor(colorStart, colorEnd, colorCount) {

        function hex(c) {
            var s = "0123456789abcdef";
            var i = parseInt(c);
            if (i == 0 || isNaN(c))
                return "00";
            i = Math.round(Math.min(Math.max(0, i), 255));
            return s.charAt((i - i % 16) / 16) + s.charAt(i % 16);
        }

        /* Convert an RGB triplet to a hex string */
        function convertToHex(rgb) {
            return hex(rgb[0]) + hex(rgb[1]) + hex(rgb[2]);
        }

        /* Remove '#' in color hex string */
        function trim(s) { return (s.charAt(0) == '#') ? s.substring(1, 7) : s }

        /* Convert a hex string to an RGB triplet */
        function convertToRGB(hex) {
            var color = [];
            color[0] = parseInt((trim(hex)).substring(0, 2), 16);
            color[1] = parseInt((trim(hex)).substring(2, 4), 16);
            color[2] = parseInt((trim(hex)).substring(4, 6), 16);
            return color;
        }

        // The beginning of your gradient
        var start = convertToRGB(colorStart);

        // The end of your gradient
        var end = convertToRGB(colorEnd);

        // The number of colors to compute
        var len = colorCount;

        //Alpha blending amount
        var alpha = 0.0;

        var saida = [];
        saida.push("#656565");

        for (i = 0; i < len; i++) {
            var c = [];
            alpha += (1.0 / len);

            c[0] = start[0] * alpha + (1 - alpha) * end[0];
            c[1] = start[1] * alpha + (1 - alpha) * end[1];
            c[2] = start[2] * alpha + (1 - alpha) * end[2];

            saida.push("#" + convertToHex(c));


        }

        return saida;

    }

    function cambioTipoDato(radio) {
        document.getElementById("valorRadio").value = radio.value;
        var tituloCuadroTreemap = document.getElementById("idTreemap").value;
        var tipoTreemap = document.getElementById("tipoTreemap").value;
        var baseColor = document.getElementById("baseColor").value;
        var idRegion = document.getElementById("idRegion").value;
        var serviciotempo = document.getElementById("serviciotempo").value;
        var idcuadrado = document.getElementById("IdCuadrado").value;
        if (tituloCuadroTreemap == "") { tituloCuadroTreemap = null; }
        if(baseColor == ""){baseColor = null;}
        if(idRegion == ""){idRegion = null;}

        if (serviciotempo == "") {
            document.getElementById("treeChart").remove();
            drawChart(tipoTreemap, idRegion);
        }
        else {
            document.getElementById("serviciotempo").value = "";
            document.getElementById("nivel999").value = "2";
            document.getElementById("treeChart").remove();

            drawChart(tipoTreemap, idRegion);
        }
        drawChartMap(baseColor, tipoTreemap, idRegion, true);
        getTitulo(serviciotempo);
    }

    function back() {
        document.getElementById("baseColor").value = "";
        document.getElementById("tipoTreemap").value = "";
        document.getElementById("idRegion").value = "";
        document.getElementById("tituloMapa").innerHTML = "";
        document.getElementById("idTreemap").value = "";
        document.getElementById("treeChart").remove();
        document.getElementById("nivel999").value = "1";
        drawChart();
        drawChartMap(null, null, null,true,null);
    }

    function getTitulo(servicio) {

        var tipoValor = document.getElementById("valorRadio").value;
        if (tipoValor == null) { tipoValor = "";}

        var idCiudad = document.getElementById("idRegion").value;
        if (idCiudad == null) { idCiudad = ""; }

        var optCategoria = document.getElementById("tipoTreemap").value;
        if (optCategoria == null) { optCategoria = ""; }

        var jsonData = $.ajax({
            url: "../../AjaxGraficos/GetTituloPropirQuienInvierte39999",
            data: { 'tipoValor': tipoValor, 'idCiudad': idCiudad, 'optCategoria': optCategoria, 'servicio': servicio },
            dataType: "json",
            async: false
        }).responseJSON;
        document.getElementById("PROPIR").innerHTML = jsonData.dataPROPIR;
    }

    $(window).resize(function () {
        var tipoTreemap = document.getElementById("idTreemap").value;
        var baseColor = document.getElementById("baseColor").value;
        var idRegion = document.getElementById("idRegion").value;
        var notSearchTreemap = document.getElementById("notSearchTreemap").value;
        document.getElementById("treeChart").remove();
        if (tipoTreemap == "") { tipoTreemap = null; }
        if (baseColor == "") { baseColor = null; }
        if (idRegion == "") { idRegion = null; }
        if (notSearchTreemap == "") { notSearchTreemap = null; }

        drawChart(notSearchTreemap, tipoTreemap, idRegion);
        //drawChartMap(baseColor, tipoTreemap, idRegion);
    });

    $(document).ready(function () {
        drawChartMap();
        drawChart();
    });
