document.write('<script src="../../Content/lib/amchart/amcharts.js"></script>');
document.write('<script src="../../Content/lib/amchart/light.js"></script>');
document.write('<script src="../../Content/lib/amchart/serial.js"></script>');

google.charts.load("current", { packages: ["corechart"] });
    google.charts.setOnLoadCallback(drawChart3);

    var dataDrawChart;
    var dataDrawChar2 = '';
    function ajaxDrawChart(parameter)
    {
        $.getJSON("../../../AjaxGraficos/GetDatosCifrasGenerales1/" + parameter , function (data) {
            dataDrawChart = data;
            drawChart(data)
            
        });
    }

    function ajaxDrawChart2(parameter)
    {
        $.getJSON("../../../AjaxGraficos/GetDatosCifrasGenerales2/" + parameter , function (data) {
            dataDrawChart2 = data;
            drawChart2(data)
            
        });
    }

    function drawChart(dataJson,dataScript) {
        var data = '';

        if (dataScript==null) {
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
                    "balloonText": " [[tooltip]]: [[value]]",
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
                    "gridCount":3
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

        chart.addListener("rollOverGraphItem", handleOver);
        chart.addListener("rollOutGraphItem", handleOut);

        function handleOver(event) {

            var fila1 = event.item.columnGraphics.node.childNodes[0];
            fila1.setAttribute("stroke", "#254250");
            fila1.setAttribute("fill", "#254250");

            var index = event.index;

            var grafico2 = document.getElementById("chart_div2");
            var fila2 = grafico2.childNodes[0].childNodes[0].childNodes[0].childNodes[7].childNodes[0].childNodes[0].childNodes[index].childNodes[0]; //.childNodes[index].firstChild.
            fila2.setAttribute("stroke", "#254250");
            fila2.setAttribute("fill", "#254250");
        }

        function handleOut(event) {

            var fila1 = event.item.columnGraphics.node.childNodes[0];
            fila1.setAttribute("stroke", "#67b7dc");
            fila1.setAttribute("fill", "#67b7dc");

            var index = event.index;

            var grafico2 = document.getElementById("chart_div2");
            var fila2 = grafico2.childNodes[0].childNodes[0].childNodes[0].childNodes[7].childNodes[0].childNodes[0].childNodes[index].childNodes[0]; //.childNodes[index].firstChild.
            fila2.setAttribute("stroke", "#67b7dc");
            fila2.setAttribute("fill", "#67b7dc");
        }
    }

    function drawChart2(dataJson,dataScript) {

        var data = '';

        if (dataScript==null) {
            data = dataJson
        }
        else {
            data = dataScript;
        }

        var chart = AmCharts.makeChart("chart_div2", {
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
                    "balloonText": "[[tooltip]]: [[value]]",
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
            "dataProvider":data,
            "export": {
                "enabled": true
            }

        });

        chart.addListener("rollOverGraphItem", handleOver);
        chart.addListener("rollOutGraphItem", handleOut);

        function handleOver(event) {

            var fila1 = event.item.columnGraphics.node.childNodes[0];
            fila1.setAttribute("stroke", "#254250");
            fila1.setAttribute("fill", "#254250");

            var index = event.index;

            var grafico2 = document.getElementById("chart_div");
            var fila2 = grafico2.childNodes[0].childNodes[0].childNodes[0].childNodes[7].childNodes[0].childNodes[0].childNodes[index].childNodes[0]; //.childNodes[index].firstChild.
            fila2.setAttribute("stroke", "#254250");
            fila2.setAttribute("fill", "#254250");
        }

        function handleOut(event) {

            var fila1 = event.item.columnGraphics.node.childNodes[0];
            fila1.setAttribute("stroke", "#67b7dc");
            fila1.setAttribute("fill", "#67b7dc");

            var index = event.index;

            var grafico2 = document.getElementById("chart_div");
            var fila2 = grafico2.childNodes[0].childNodes[0].childNodes[0].childNodes[7].childNodes[0].childNodes[0].childNodes[index].childNodes[0]; //.childNodes[index].firstChild.
            fila2.setAttribute("stroke", "#67b7dc");
            fila2.setAttribute("fill", "#67b7dc");
        }
    }

    function drawChart3() {

        var dat = "";
        var jsonData = $.ajax({
            url: "GetDatosCifrasGenerales3",
            data: { 'id': dat },
            dataType: "json",
            async: false
        }).responseJSON;
        var colores = JSON.parse(jsonData.colores);
        var data = new google.visualization.DataTable(jsonData.dataChart);
        var titulo = jsonData.title;
        document.getElementById("tituloPieChart").innerHTML = '% del gasto del Gobierno Central que es asignado a regiones, acorde al PROPIR';
        var formatter = new google.visualization.NumberFormat({ prefix: 'MM$ ', formatType: 'long', fractionDigits: 0, groupingSymbol: "." });
        for (i = 0; i < 2; i++) {
            formatter.format(data, i);
        }
        formats = 'MM$ #,###';

        var options = {
            title: '',
            legend: 'bottom',
            colors: colores,
            backgroundColor: 'transparent',
            chartArea: { 'width': '80%', 'height': '65%'},
            vAxis: {
                format: formats
            }
        };
        //CALCULO LARGO DE GRAFICO
        var minHeight = document.getElementById("chart_div").parentElement.clientHeight * 0.3;
        var Height = document.getElementById("chart_div").parentElement.clientHeight * 0.8;

        if (minHeight < 200) {
            minHeight = 200;
        }

        document.getElementById("chart_div3").style.minHeight = minHeight + "px";
        document.getElementById("chart_div3").style.height = Height + "px";

        var chart = new google.visualization.PieChart(document.getElementById('chart_div3'));

        chart.draw(data, options);
    }

    $(document).ready(function () {
        var porcentajeDeUso = 0.8;
        var Widht1 = document.getElementById("chart_div").parentElement.clientWidth * (porcentajeDeUso + 0.1);
        var Heigth1 = document.getElementById("chart_div").parentElement.clientHeight * (porcentajeDeUso + 0.1);
        if (Widht1 < 300) {
            Widht1 = 300;
        }
        if (Heigth1 < 400) {
            Heigth1 = 400;
        }
        var Widht2 = document.getElementById("chart_div2").parentElement.clientWidth * (porcentajeDeUso + 0.1);
        var Heigth2 = document.getElementById("chart_div2").parentElement.clientHeight * (porcentajeDeUso + 0.1);
        if (Widht2 < 300) {
            Widht2 = 300;
        }
        if (Heigth2 < 400) {
            Heigth2 = 400;
        }
        document.getElementById("chart_div").style.height = Heigth1 + "px";
        document.getElementById("chart_div").style.width = Widht1 + "px";
        ajaxDrawChart("hola");
        //drawChart(dataDrawChart);
        document.getElementById("chart_div2").style.height = Heigth2 + "px";
        document.getElementById("chart_div2").style.width = Widht2 + "px";
        ajaxDrawChart2("hola");

    });

    $(window).resize(function () {
        var porcentajeDeUso = 0.8;

        var Widht1 = document.getElementById("chart_div").parentElement.clientWidth * porcentajeDeUso;
        var Heigth1 = document.getElementById("chart_div").parentElement.clientHeight * porcentajeDeUso;

        if (Widht1 < 300) {
            Widht1 = 300;
        }

        if (Heigth1 < 400) {
            Heigth1 = 400;
        }

        var Widht2 = document.getElementById("chart_div2").parentElement.clientWidth * porcentajeDeUso;
        var Heigth2 = document.getElementById("chart_div2").parentElement.clientHeight * porcentajeDeUso;

        if (Widht2 < 300) {
            Widht2 = 300;
        }

        if (Heigth2 < 400) {
            Heigth2 = 400;
        }

        document.getElementById("chart_div").style.height = Heigth1 + "px";
        document.getElementById("chart_div").style.width = Widht1 + "px";
        drawChart(dataDrawChart);

        document.getElementById("chart_div2").style.height = Heigth2 + "px";
        document.getElementById("chart_div2").style.width = Widht2 + "px";
        drawChart2(dataDrawChart2);

        drawChart3();
    });
