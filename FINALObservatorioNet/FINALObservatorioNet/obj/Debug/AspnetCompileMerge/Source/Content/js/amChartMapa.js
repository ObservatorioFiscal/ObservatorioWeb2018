document.write('<input type="hidden" id="tablaActiva" name="tablaActiva" />');
document.write('<input type="hidden" id="valorRadio" name="valorRadio" />');
document.write('<input type="hidden" id="idRegion" name="idRegion" />');
document.write('<input type="hidden" id="tipoTreemap" name="tipoTreemap" />');
document.write('<input type="hidden" id="notSearchTreemap" name="notSearchTreemap" />');
document.write('<input type="hidden" id="baseColor" name="baseColor" />');
document.write('<input type="hidden" id="idBarra" name="idBarra" />');

document.write('<link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-table/1.10.1/bootstrap-table.min.css">');
document.write('<link href="../../Content/lib/ammap/ammap.css" rel="stylesheet" />');
document.write('<script src="../../Content/lib/ammap/ammap.js"></script>');
document.write('<script src="../../Content/lib/ammap/maps/js/chileHigh.js"></script>');
document.write('<script src="../../Content/lib/ammap/maps/js/ChileSVG.js"></script>');
document.write('<script src="../../Content/lib/amchart/amcharts.js"></script>');
document.write('<script src="../../Content/lib/amchart/light.js"></script>');
document.write('<script src="../../Content/lib/amchart/serial.js"></script>');
document.write('<script src="http://cdnjs.cloudflare.com/ajax/libs/bootstrap-table/1.10.1/bootstrap-table.min.js"></script>');




    var dataDrawChart;

    function ajaxDrawChart(idRegion,tipoValor)
    {
        if (idRegion == null) { idRegion = ""; }
        if (tipoValor == null) { tipoValor = ""; }

        var parameter = idRegion + ";" + tipoValor;

        $.getJSON("../../../AjaxGraficos/GetDatosEnQueSeInvierte2/"+ parameter,function (data){
            drawChart(data)
            dataDrawChart = data;
        });
    }

    function drawChart(dataJson, dataScript) {

        var data = '';
        var porcentajeDeUso = 0.9;

        var Widht1 = document.getElementById("chart_div").parentElement.clientWidth * porcentajeDeUso;
        var Heigth1 = 650;// document.getElementById("chart_div").parentElement.clientWidth * 0.7;

        if (Widht1 < 300) {
            Widht1 = 300;
        }

        if (Heigth1 < 400) {
            Heigth1 = 400;
        }

        document.getElementById("chart_div").style.height = Heigth1 + "px";
        document.getElementById("chart_div").style.width = Widht1 + "px";


        if (dataScript == null) {
            data = dataJson
        }
        else {
            data = dataScript;
        }

        var chart = AmCharts.makeChart("chart_div", {
            "type": "serial",
            "theme": "light",
            "categoryField": "ciudad",
            "rotate": true,
            "startDuration": 1,
            "numberFormatter": { "precision": -1, "decimalSeparator": ",", "thousandsSeparator": "." },
            "categoryAxis": {
                "gridPosition": "start",
                "position": "left"
            },
            "trendLines": [],
            "graphs": [
                {
                    "balloonText": "[[tooltip]] [[value]]",
                    "fillAlphas": 0.8,
                    "id": "AmGraph-1",
                    "lineAlpha": 0.2,
                    "title": "Inversion",
                    "type": "column",
                    "valueField": "inversion"
                }
            ],
            "guides": [],
            "valueAxes": [
                {
                    "id": "ValueAxis-1",
                    "position": "top",
                    "axisAlpha": 0,
                    "autoGridCount": false,
                    "gridCount": 3
                }
            ],
            "allLabels": [],
            "balloon": {},
            "titles": [],
            "dataProvider": data,
            "export": {
                "enabled": true
            }

        });
        chart.responsive = {
            "enabled": true
        };
        chart.addListener("clickGraphItem", clickItem);

        function clickItem(event) {

            var id = event.item.dataContext.ciudad;
            document.getElementById("idBarra").value = id;
            //var categoria = event.item.dataContext.ciudad;
            //var valor = event.item.dataContext.inversion;
            //var fila1 = event.item.columnGraphics.node.childNodes[0];
            //alert(id + "holamund");

            try {
                //document.getElementById("tablaGrafico").remove();
                //document.getElementById("subTabla").innerHTML = '';
                $('.bootstrap-table').remove();
                $('.clearfix').remove();
            }
            catch(excepcion){}
            ajaxTable(id);

            document.getElementById("chart_div").style.display = 'none';
            getTitulo();
        }

    }

    function drawChartMap(baseColor, parametroTreemap, idRegion, back) {

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

        var marginTop = 10;
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
        document.getElementById("chart_divMap").style.marginTop = marginTop + "px";

        var radio = document.getElementById("valorRadio").value;
        var jsonData = $.ajax({
            url: "../../AjaxGraficos/GetDatosEnqueSeInvierte",
            data: { 'parametroTreemap': parametroTreemap, 'idCiudad': idRegion, 'tipoValor': radio },
            dataType: "json",
            async: false
        }).responseJSON;

        //document.getElementById("PROPIR").innerHTML = "NACIONAL: " +  "$" + jsonData.dataPROPIR;

        //COLOR FINAL / COLOR BASE / CANTIDAD DE COLORES
        if (baseColor != null) {
            var colores = generateColor('#ffffff', baseColor, jsonData.dataCantColores);
            var colores = generateColor(colores[12], baseColor, jsonData.dataCantColores);
            document.getElementById("cuadraditomax").style.backgroundColor = baseColor;
            document.getElementById("cuadraditomin").style.backgroundColor = colores[12];
        } else {
            //var colores = generateColor('#97DAEB', '#00B3E3', jsonData.dataCantColores);
            var colores = generateColor('#ffffff', '#F21818', jsonData.dataCantColores);
            document.getElementById("cuadraditomax").style.backgroundColor = '#F21818';
            document.getElementById("cuadraditomin").style.backgroundColor = '#ffb9b9';
        }

        var map = AmCharts.makeChart("chart_divMap", {
            type: "map",
            zoomOnDoubleClick: false,
            dragMap: false,
            autoResize: false,

            balloon: {//nombre region
                color: "#000000"
            },

            dataProvider: {
                map: "ChileSVG",
                images: [{

                }],
                areas: [
                    { id: "CL-AP", title: "Arica y Parinacota " + jsonData.dataPorcentaje[0], selectable: true, "color": colores[jsonData.dataIntColor[0]] },
                    { id: "CL-TA", title: "Tarapacá " + jsonData.dataPorcentaje[1], selectable: true, "color": colores[jsonData.dataIntColor[1]] },
                    { id: "CL-AN", title: "Antofagasta " + jsonData.dataPorcentaje[2], selectable: true, "color": colores[jsonData.dataIntColor[2]] },
                    { id: "CL-AT", title: "Atacama " + jsonData.dataPorcentaje[3], selectable: true, "color": colores[jsonData.dataIntColor[3]] },
                    { id: "CL-CO", title: "Coquimbo " + jsonData.dataPorcentaje[4], selectable: true, "color": colores[jsonData.dataIntColor[4]] },
                    { id: "CL-VS", title: "Valparaíso " + jsonData.dataPorcentaje[5], selectable: true, "color": colores[jsonData.dataIntColor[5]] },
                    { id: "CL-RM", title: "R.M. Santiago " + jsonData.dataPorcentaje[6], selectable: true, "color": colores[jsonData.dataIntColor[6]] },
                    { id: "CL-LI", title: "O'Higgins " + jsonData.dataPorcentaje[7], selectable: true, "color": colores[jsonData.dataIntColor[7]] },
                    { id: "CL-ML", title: "Maule " + jsonData.dataPorcentaje[8], selectable: true, "color": colores[jsonData.dataIntColor[8]] },
                    { id: "CL-BI", title: "Bío-Bío " + jsonData.dataPorcentaje[9], selectable: true, "color": colores[jsonData.dataIntColor[9]] },
                    { id: "CL-AR", title: "Araucanía " + jsonData.dataPorcentaje[10], selectable: true, "color": colores[jsonData.dataIntColor[10]] },
                    { id: "CL-LR", title: "Los Ríos " + jsonData.dataPorcentaje[11], selectable: true, "color": colores[jsonData.dataIntColor[11]] },
                    { id: "CL-LL", title: "Los Lagos " + jsonData.dataPorcentaje[12], selectable: true, "color": colores[jsonData.dataIntColor[12]] },
                    { id: "CL-AI", title: "Aysén " + jsonData.dataPorcentaje[13], selectable: true, "color": colores[jsonData.dataIntColor[13]] },
                    { id: "CL-MA", title: "Magallanes " + jsonData.dataPorcentaje[14], selectable: true, "color": colores[jsonData.dataIntColor[14]] },
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

        map.fontSize = 12;
        map.addListener("clickMapObject", function (event) {
            document.getElementById("idBarra").value = "";
            var val = document.getElementById("valorRadio").value;
            var idRegion = event.mapObject.id;
            var titulo = event.mapObject.enTitle;
            //var tipoTreemap = document.getElementById("tipoTreemap").value;

            //if (tipoTreemap == "" || tipoTreemap == undefined) { tipoTreemap = null; }
            
            //var notSearchTreemap = document.getElementById("notSearchTreemap").value;
            document.getElementById("idRegion").value = idRegion;
            document.getElementById("tituloMapa").innerHTML = titulo;

            //var baseColor = document.getElementById("baseColor").value

            //if (baseColor == "") { baseColor = null; }

            document.getElementById("chart_div").style.display = 'block';
            document.getElementById("chart_div").innerHTML = '';
            document.getElementById("tablaActiva").value = 0;

            try {
                $('.bootstrap-table').remove();
                $('.clearfix').remove();
                $('#tablaGrafico').remove();
            } catch (exception) { }

            ajaxDrawChart(idRegion, val);
            drawChartMap(null, null, idRegion);

        });

        map.balloon.offsetX = -20;
        getTitulo();
    }

    function getTitulo() {

        var tipoValor = document.getElementById("valorRadio").value;
        if (tipoValor == null) { tipoValor = ""; }

        var idCiudad = document.getElementById("idRegion").value;
        if (idCiudad == null) { idCiudad = ""; }

        var optCategoria = document.getElementById("idBarra").value;
        if (optCategoria == null) { optCategoria = ""; }

        var jsonData = $.ajax({
            url: "../../AjaxGraficos/GetTituloPropirEnQueSeInvierte3",
            data: { 'tipoValor': tipoValor, 'idCiudad': idCiudad, 'optCategoria': optCategoria },
            dataType: "json",
            async: false
        }).responseJSON;
        document.getElementById("PROPIR").innerHTML = jsonData.dataPROPIR;
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

    function ajaxTable(id)
    {
        var radio = document.getElementById("valorRadio").value;
        document.getElementById("tablaActiva").value = 1;
        var div1 = document.getElementById("subTabla");
        var div = document.createElement("table");
        div.id = "tablaGrafico";
        div.name = "tablaGrafico";
        div.className = "table table-bordered";
        div1.appendChild(div);
        var idRegion = document.getElementById("idRegion").value;
        if (idRegion == null) { idRegion = ""; }
        var data = $.ajax({
            url: "../../AjaxGraficos/GetDatosEnQueSeInvierte3",
            data: { 'id': id, 'region': idRegion, 'formato': radio },
            dataType: "json",
            async: false
        }).responseText;

        json = JSON.parse(data)
        data = json;

        if (radio == "nominal") {
            $("#tablaGrafico").bootstrapTable({
                data: data,
                columns: [{
                    field: 'iniciativa',
                    title: 'Iniciativa'
                }, {
                    field: 'valor',
                    title: 'Valor nominal (MM $)'
                }]
            });
        }
        else
        {
            $("#tablaGrafico").bootstrapTable({
                data: data,
                columns: [{
                    field: 'iniciativa',
                    title: 'Iniciativa'
                }, {
                    field: 'valor',
                    title: 'Valor percapita ($)'
                }]
            });
        }

        $("#tablaGrafico tbody tr td:nth-child(2)").addClass("text-right");
        $("#subTabla").append('<p class="text-center"> En esta lista se muestra los primeros 100 elementos, para ver el detalle descargue el Dataset.</p >');
        $("#subTabla").removeClass("hidden");
    }

    $(window).resize(function () {
        var idRegion = document.getElementById("idRegion").value;
        var val = document.getElementById("valorRadio").value;

        if (idRegion == null) { idregion = ""; }
        if (val == null) { val = ""; }

        ajaxDrawChart(idRegion, val);
        drawChartMap(null, null, idRegion);
    });

    $(document).ready(function () {
        document.getElementById("tablaActiva").value = 0;
        document.getElementById("valorRadio").value = "nominal";
        var val = document.getElementById("valorRadio").value;

        ajaxDrawChart(null,val);
        drawChartMap();

        $("#tablaGrafico tbody tr td:eq(1)").addClass("class", "text-right");
            
    });

    function back()
    {
        $("#subTabla").addClass("hidden");
        document.getElementById("chart_div").style.display = 'block';
        document.getElementById("tablaActiva").value = 0;
        try {
            $('.bootstrap-table').remove();
            $('.clearfix').remove();
            $('#tablaGrafico').remove();
        } catch (exception) { }

        document.getElementById("idRegion").value = '';
        var val = document.getElementById("valorRadio").value;
        ajaxDrawChart(null, val);
        drawChartMap(null, null, null, "1111");
    }

    function cambioTipoDato(radio) {
        var val = radio.value;
        document.getElementById("valorRadio").value = val;

        try {
            $('.bootstrap-table').remove();
            $('.clearfix').remove();
            var id = document.getElementById("idBarra").value;
            $('#tablaGrafico').remove();
            if (document.getElementById("tablaActiva").value == 1) {
                ajaxTable(id);
            }
        }
        catch (excepcion) {
        }

        var idRegion = document.getElementById("idRegion").value;
        if (idRegion == '') { idRegion = null; }
        ajaxDrawChart(idRegion, val);

    }