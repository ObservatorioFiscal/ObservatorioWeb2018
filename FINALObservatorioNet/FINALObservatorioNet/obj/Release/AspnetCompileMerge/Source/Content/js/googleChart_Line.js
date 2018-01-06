//<script type="text/javascript" src="~/Scripts/loader.js"></script>

    google.charts.load('current', { 'packages': ['corechart'], 'language': 'es' })
    google.charts.setOnLoadCallback(drawChart);

    $(document).ready(function () {
        $("#lineaBotones").append('<input type="hidden" id="nivel" name="nivel" /><input type= "hidden" id= "tipo" name= "tipo" /><input type="hidden" id="boton" name="boton" /><input type="hidden" id="botonActual" name="botonActual" />');

    });

    function drawChart(alto, ancho) {

        document.getElementById("boton").value = 1;
        document.getElementById("nivel").value = 1;
        
        if (alto == null)
        {
            //CALCULO LARGO DE GRAFICO
            var minHeight = screen.height / 3;
            var Height = document.getElementById("chart_div").parentElement.clientHeight * 1; // 2/3 de largo de ventana
            var Widht = document.getElementById("chart_div").parentElement.clientWidth * 0.97; // 2/3 de largo de ventana

            if (Height < 300) { Height = 300; }
            if (Widht < 300) { Widht = 300; }

            if (minHeight < 200) {
                minHeight = 200;
            }
        }

        else {
            var Height = alto;
            var Widht = ancho;

            if (Height < 300) { Height = 300; }
            if (Widht < 300) { Widht = 300; }

            if (minHeight < 200) {
                minHeight = 200;
            }
        }

        document.getElementById("chart_div").style.minHeight = minHeight + "px";
        document.getElementById("chart_div").style.height = Height + "px";

        var chartDiv = document.getElementById('chart_div');

        var dat = "parametro";
        var idFormato = $("#formatoGrafico").find(":selected").attr('id');
        if(idFormato != 2){idFormato = 1;}

        var jsonData = $.ajax({
            url: "../../AjaxGraficos/GetDatosComoHaCambiadoElGasto1",
            data: { 'id': dat ,'formato':idFormato},
            dataType: "json",
            async: false
        }).responseJSON;

        var data = new google.visualization.DataTable(jsonData.data);

        var formato = jsonData.formato;
        var cantidad = jsonData.cantidad;
        var formats;

        if (formato == "1") {
            var formatter = new google.visualization.NumberFormat({ prefix: 'MM$', formatType: 'long', fractionDigits: 0, groupingSymbol: "."});
            for (i = 0; i < cantidad; i++)
            {
                formatter.format(data, i);
            }
            formats = 'MM$ #,###';
        }

        if (formato == "2") {
            var formatter = new google.visualization.NumberFormat({ pattern: '#.##%', formatType: 'long', fractionDigits: 3, groupingSymbol: "." });
            for (i = 0; i < cantidad; i++) {
                formatter.format(data, i);
            }
            formats = '#.### %';
        }

        var colores = JSON.parse(jsonData.colores);
        var titulo = '';//jsonData.title;

        var legend = document.getElementById('legend');
        var lis = [];
        var total = 0;
        $('#legend').empty();
        var j = 1;
        for (var i = 1; i < data.ng.length; i++) {
            var label = data.ng[i].label;
            if(label!=null){
                lis[i] = document.createElement('li');
                lis[i].style.cssText = "text-align: left;min-width:200px;max-width:200px;white-space:nowrap;overflow:hidden;text-overflow:ellipsis;display:inline-block";
                lis[i].id = 'legend_' + data.getValue(i, 0);
                lis[i].innerHTML = '<div class="legendMarker" style="margin-left: -25 ;background-color:' + colores[j - 1] + ';"></div>' + "&nbsp;&nbsp;" + label;
                j++;
                legend.appendChild(lis[i]);
            }
        }

        var classicOptions = {
            title: titulo,
            width: Widht,
            height: Height,
            chartArea: {'width': '80%', 'height': '60%','left':150,'top':50},
            backgroundColor: 'transparent',
            tooltip: {
                isHtml: true,
                textStyle: {
                    color: "#4e4e4e", 
                    fontName: "<global-font-name>", 
                    fontSize: "<global-font-size>"
                }
            },
            legend: 'none',
            lineWidth:4,
            colors: colores,
            seriesType:'area',
            isStacked: true,
            vAxes: {
                // Adds titles to each axis.
                0: { title: '' },
                1: { title: '' }
            },
            hAxis: {
                    textStyle: {
                        fontSize: 9
                    }
            },
            vAxis: {
                format: formats,
            },
        };


        function drawClassicChart() {
            var classicChart = new google.visualization.AreaChart(chartDiv);
            classicChart.draw(data, classicOptions);

            google.visualization.events.addListener(classicChart, 'select', function () {
                var selectedItem = classicChart.getSelection()[0];
                if (selectedItem) {
                    var value2 = data.getColumnId(selectedItem.column);
                    var posicion = selectedItem.column / 2;
                    var colorLine = colores[posicion-1];

                    //window.location.href = 'Url.Action("ComoHaCambiadoElGasto", new { id = "ID"})'.replace("ID", value2);
                    document.getElementById("chart_div").style.display = 'none';
                    document.getElementById("chart_divSub").style.display = 'block';
                    document.getElementById("tipo").value = value2;

                    drawChartSub(value2,colorLine,Height,Widht);
                }
            });
        }

        drawClassicChart();

        //var paths = document.getElementsByTagName("path");
        //for (i = 0; i < paths.length; i++) {
        //    var path = paths[i].attributes.fill.nodeValue;
        //    //var path = paths[i].attributes.fill-opacity.nodeValue;
        //    //paths[i].attributes.fill.nodeValue = "#f9f9f9";
        //    paths[i].attributes.item(3).nodeValue = "0.0";
        //}


        //#f9f9f9

    }

    function drawChartSub(id, colorBackground, alto, ancho) {
        //CALCULO LARGO DE GRAFICO
        document.getElementById("boton").value = 0;
        document.getElementById("nivel").value = 2;
        //:D
        alto = null;
        $('#chart_div').empty();
        $('#chart_divSub').empty();
        if (alto != null) {
            var minHeight = screen.height / 3;
            var Height = document.getElementById("chart_divSub").parentElement.clientHeight * 1; // 2/3 de largo de ventana
            var Widht = document.getElementById("chart_divSub").parentElement.clientWidth * 0.98; // 2/3 de largo de ventana

            if (minHeight < 200) {
                minHeight = 200;
            }
        }
        else {
            var Height = alto;
            var Widht = ancho; // 2/3 de largo de ventana

            if (Height < 300) { Height = 300; }
            if (Widht < 300) { Widht = 300; }

            if (minHeight < 200) {
                minHeight = 200;
            }

        }
        document.getElementById("chart_divSub").style.minHeight = minHeight + "px";
        document.getElementById("chart_divSub").style.height = Height + "px";
        document.getElementById("chart_divSub").style.widht = Widht + "px";

        var idFormato = $("#formatoGrafico").find(":selected").attr('id');
        if(idFormato != 2){idFormato = 1;}

        var jsonData = $.ajax({
            url: "GetDatosComoHaCambiadoElGasto1_ClickLine1",
            data: { 'id': id, 'color': "",'formato':idFormato },
            dataType: "json",
            async: false
        }).responseJSON;

        var data = new google.visualization.DataTable(jsonData.data);

        var formato = jsonData.formato;
        var cantidad = jsonData.cantidad;
        var formats;

        if (formato == "1") {
            var formatter = new google.visualization.NumberFormat({ prefix: 'MM$', formatType: 'long', fractionDigits: 0, groupingSymbol: "." });
            for (i = 0; i < cantidad; i++) {
                formatter.format(data, i);
            }
            formats = 'MM$ #,###';
        }

        if (formato == "2") {
            var formatter = new google.visualization.NumberFormat({ pattern: '#.##%', formatType: 'long', fractionDigits: 3, groupingSymbol: "." });
            for (i = 0; i < cantidad; i++) {
                formatter.format(data, i);
            }
            formats = '#.### %';
        }

        var colores = JSON.parse(jsonData.colores);
        var titulo = '';

        var legend = document.getElementById('legend');
        var lis = [];
        var total = 0;
        $('#legend').empty();
        var j = 1;
        for (var i = 1; i < data.ng.length; i++) {
            var label = data.ng[i].label;
            if (label != null) {
                lis[i] = document.createElement('li');
                lis[i].style.cssText = "text-align: left;min-width:200px;max-width:200px;white-space:nowrap;overflow:hidden;text-overflow:ellipsis;display:inline-block";
                lis[i].id = 'legend_' + data.getValue(i, 0);
                lis[i].innerHTML = '<div class="legendMarker" style="margin-left: -25 ;background-color:' + colores[j - 1] + ';"></div>' + "&nbsp;&nbsp;" + label;
                j++;
                legend.appendChild(lis[i]);
            }
        }

        var options = {
            title: titulo,
            legend: 'none',
            chartArea: { 'width': '80%', 'height': '60%', 'left': 150, 'top': 50 },
            backgroundColor: 'transparent',
            seriesType: 'area',
            lineWidth: 4,
            isStacked: true,
            tooltip: { isHtml: true },
            colors: colores,
            hAxis: { title: 'Año', titleTextStyle: { color: '#333' } },
            vAxis: { format: formats }
            ,
            hAxis: {
                textStyle: {
                    fontSize: 9
                }
            }
        };

        var chart = new google.visualization.AreaChart(document.getElementById('chart_divSub'));
        chart.draw(data, options);

        google.visualization.events.addListener(chart, 'select', function () {
            var selectedItem = chart.getSelection()[0];
            if (selectedItem) {
                var value2 = data.getColumnLabel(selectedItem.column);

                //window.location.href = 'Url.Action("ComoHaCambiadoElGasto", new { id = "ID"})'.replace("ID", value2);
                document.getElementById("chart_divSub").style.display = 'none';
                document.getElementById("chart_div").style.display = 'block';
                drawChart();
            }
        });
    }

    function drawChartBoton(data)
    {
        var stack = true;
        if (data.id == 'button3') {
            stack = false;
        }


        document.getElementById('chart_div').innerHTML = '';
        document.getElementById('chart_divSub').innerHTML = '';
        //$('#chart_div').empty();
        //$('#chart_divSub').empty();
        if (data != null) {
            var idButton = data.id;
            document.getElementById("botonActual").value = idButton;
        }
        else {
            var idButton = document.getElementById("botonActual").value;
        }
            document.getElementById("chart_div").style.display = 'block';
            document.getElementById("chart_divSub").style.display = 'none';

            $('#chart_div').empty();
            $('#chart_divSub').empty();
            var auxiliar = data.id.substr(data.id.length - 1, 1);
            document.getElementById("boton").value = data.id.substr(data.id.length-1, 1);
            document.getElementById("nivel").value = 1;


            //CALCULO LARGO DE GRAFICO
            var minHeight = screen.height / 3;
            var Height = document.getElementById("chart_div").parentElement.clientHeight * 1; // 2/3 de largo de ventana
            var Widht = document.getElementById("chart_div").parentElement.clientWidth * 0.98; // 2/3 de largo de ventana

            if (Height < 300) { Height = 300; }
            if (Widht < 300) { Widht = 300; }

            if (minHeight < 200) {
                minHeight = 200;
            }

            document.getElementById("chart_div").style.minHeight = minHeight + "px";
            document.getElementById("chart_div").style.height = Height + "px";

            var chartDiv = document.getElementById('chart_div');

            var idBoton;
            if (idButton == "button1")
            {
                for(var a=1; a<5;a++)
                {
                    $("#button" + a).removeClass("classseleccionado");
                    $("#button" + a).removeClass("classinsseleccionado");
                }
                $("#button1").addClass("classseleccionado");
                $("#button2").addClass("classinsseleccionado");
                $("#button3").addClass("classinsseleccionado");
                $("#button4").addClass("classinsseleccionado");

                idBoton = 1;
            }
            if (idButton == "button2")
            {
                for (var a = 1; a < 5; a++) {
                    $("#button" + a).removeClass("classseleccionado");
                    $("#button" + a).removeClass("classinsseleccionado");
                }
                $("#button1").addClass("classinsseleccionado");
                $("#button2").addClass("classseleccionado");
                $("#button3").addClass("classinsseleccionado");
                $("#button4").addClass("classinsseleccionado");
                idBoton = 2;
            }
            if (idButton == "button3")
            {
                for (var a = 1; a < 5; a++) {
                    $("#button" + a).removeClass("classseleccionado");
                    $("#button" + a).removeClass("classinsseleccionado");
                }
                $("#button1").addClass("classinsseleccionado");
                $("#button2").addClass("classinsseleccionado");
                $("#button3").addClass("classseleccionado");
                $("#button4").addClass("classinsseleccionado");
                idBoton = 3;
            }
            if (idButton == "button4")
            {
                for (var a = 1; a < 5; a++) {
                    $("#button" + a).removeClass("classseleccionado");
                    $("#button" + a).removeClass("classinsseleccionado");
                }
                $("#button1").addClass("classinsseleccionado");
                $("#button2").addClass("classinsseleccionado");
                $("#button3").addClass("classinsseleccionado");
                $("#button4").addClass("classseleccionado");
                idBoton = 4;
            }


            var idFormato = $("#formatoGrafico").find(":selected").attr('id');
            if(idFormato != 2){idFormato = 1;}

            var jsonData = $.ajax({
                url: "../../AjaxGraficos/GetDatosComoHaCambiadoElGasto1_Button1",
                data: { 'id': idBoton, 'color': "" ,'formato':idFormato},
                dataType: "json",
                async: false
            }).responseJSON;

            var data = new google.visualization.DataTable(jsonData.data);
            var formato = jsonData.formato;
            var cantidad = jsonData.cantidad;
            var formats;

            if (formato == "1") {
                var formatter = new google.visualization.NumberFormat({ prefix: 'MM$', formatType: 'long', fractionDigits: 0, groupingSymbol: "." });
                for (i = 0; i < cantidad; i++) {
                    formatter.format(data, i);
                }
                formats = 'MM$ #,###';
            }

            if (formato == "2") {
                var formatter = new google.visualization.NumberFormat({ pattern: '#.##%', formatType: 'long', fractionDigits: 3, groupingSymbol: "." });
                for (i = 0; i < cantidad; i++) {
                    formatter.format(data, i);
                }
                formats = '#.### %';
            }

            var colores = JSON.parse(jsonData.colores);

            var legend = document.getElementById('legend');
            var lis = [];
            var total = 0;
            $('#legend').empty();
            var j = 1;
            for (var i = 1; i < data.ng.length; i++) {
                var label = data.ng[i].label;
                if (label != null) {
                    lis[i] = document.createElement('li');
                    lis[i].style.cssText = "text-align: left;min-width:200px;max-width:200px;white-space:nowrap;overflow:hidden;text-overflow:ellipsis;display:inline-block";
                    lis[i].id = 'legend_' + data.getValue(i, 0);
                    lis[i].innerHTML = '<div class="legendMarker" style="margin-left: -25 ;background-color:' + colores[j - 1] + ';"></div>' + "&nbsp;&nbsp;" + label;
                    j++;
                    legend.appendChild(lis[i]);
                }
            }

            var classicOptions = {
                title: '',
                width: Widht,
                height: Height,
                chartArea: { 'width': '80%', 'height': '60%', 'left': 150, 'top': 50 },
                backgroundColor: 'transparent',
                tooltip: { isHtml: true },
                lineWidth: 4,
                colors: colores,
                legend: 'none',
                seriesType: 'area',
                isStacked: stack,
                vAxes: {
                    // Adds titles to each axis.
                    0: { title: '' },
                    1: { title: '' }
                },
                vAxis: {
                    format:formats
                },
                hAxis: {
                    textStyle: {
                        fontSize:9
                    }
                }
            };


            function drawClassicChart() {
                var classicChart = new google.visualization.AreaChart(chartDiv);
                classicChart.draw(data, classicOptions);
            }
            drawClassicChart();

            //var paths = document.getElementsByTagName("path");
            //for (i = 0; i < paths.length; i++) {
            //    var path = paths[i].attributes.fill.nodeValue;
            //    //var path = paths[i].attributes.fill-opacity.nodeValue;
            //    //paths[i].attributes.fill.nodeValue = "#f9f9f9";
            //    paths[i].attributes.item(3).nodeValue = "0.0";
            //}

    }

    function backAll() {
        for (var a = 1; a < 5; a++) {
            $("#button" + a).removeClass("classseleccionado");
            $("#button" + a).removeClass("classinsseleccionado");
        }
        $("#button1").addClass("classseleccionado");
        $("#button2").addClass("classinsseleccionado");
        $("#button3").addClass("classinsseleccionado");
        $("#button4").addClass("classinsseleccionado");
        var idFormato = $("#formatoGrafico").find(":selected").attr('id');
        if(idFormato != 2){idFormato = 1;}

        document.getElementById("chart_div").innerHTML = '';
        document.getElementById("chart_div").style.display = 'block';
        document.getElementById("chart_divSub").style.display = 'none';
        drawChart(null,null,null,idFormato);
    }

    function redraw()
    {
        var tipo = document.getElementById("boton").value;

        if (tipo != 1) {
            var data = { 'id': "button" + tipo };
            drawChartBoton(data);
        }
        else {
            var nivel = document.getElementById("nivel").value;
            if (nivel == 1) {
                drawChart( );
            }
            if (nivel == 2) {
                var tipo = document.getElementById("tipo").value;
                drawChartSub(tipo);
            }
        }
    }

    $(window).resize(function () {

        var tipo = document.getElementById("boton").value;

        if (tipo == 1) {
            $('#chart_div').empty();
            drawChartBoton();
        }
        else {
            var nivel = document.getElementById("nivel").value;
            if (nivel == 1) {

                $('#chart_div').empty();
                drawChart();
            }
            if (nivel == 2) {
                $('#chart_divSub').empty();
                var tipo = document.getElementById("tipo").value;
                drawChartSub(tipo);
            }
        }

    });

