document.write('<input type="hidden" id="valorRadio" name="valorRadio" />');
document.write('<input type="hidden" id="heighh" name="heighh" />');
document.write('<input type="hidden" id="widthh" name="widthh" />');
document.write('<input type="hidden" id="valorColor" value="1" />');

document.write('<script src="https://cdn.plot.ly/plotly-latest.min.js"></script>');

    var layout = "";
    var botonesSuperioresDataGlobal;
    var botonesSuperioresDataOtros;

    function CrearBotonesRegiones(data,global)
    {
        botonesSuperioresDataOtros = data;
        botonesSuperioresDataGlobal = global;

        var legend = document.getElementById('legend');
        var lis;
        $('#legend').empty();

        var label = global.nombreGlobal;
        lis = document.createElement('li');
        lis.id = global.uids;
        lis.style.cssText = "border: 1px solid black;margin: 5px 10px;padding:5px;min-width:100px;max-width:200px;white-space:nowrap;overflow:hidden;text-overflow:ellipsis;";
        lis.setAttribute("uid", global.uids);
        lis.setAttribute("onclick", "ocultarLinea(this);return false;");
        lis.innerHTML = '<span class="glyphicon glyphicon-remove" aria-hidden="true"> </span> <div class="legendMarker" style="margin-left: -25 ;background-color:' + global.colorGlobal + ';"></div>' + "&nbsp;&nbsp;" + label;
        legend.appendChild(lis);


        $("#botonesRegion").empty();
        var contenedor = document.getElementById("botonesRegion");

        contenedor.className = "text-center";
        contenedor.style = "margin-top:10px;margin-bottom:10px;";

        for (i = 0; i < data.length; i++) {

            var posicion = data[i].codigoRegion;
            var div = document.createElement("div");
            div.className = "btn-group dropdown";
            div.id = "region" + posicion.toString();
            div.setAttribute("role", "group");
            div.style = "margin-right: 9px;"

            var button = document.createElement("button");
            button.type = "button";
            button.className = "btn btn-default dropdown-toggle";
            button.setAttribute("onclick", "mostrarLinea(this);return false;");
            button.setAttribute("data-toggle", "dropdown");
            button.setAttribute("aria-haspopup", "true");
            button.setAttribute("aria-expanded", "true");
            button.setAttribute("uid", data[i].uids);
            //button.setAttribute("color", data[i].colorRegion);
            button.setAttribute("titulo", data[i].nombreRegion);
            button.textContent = NumerosRomanos(posicion);
            var ul = document.createElement("ul");
            ul.className = "dropdown-menu corto";
            ul.setAttribute("aria-labelledby", "region" + posicion.toString());

            var cantidadComunas = data[i].comunas.length;
            for (j = 0; j < cantidadComunas ; j++) {
                var info = data[i].comunas[j];
                var nombreElemento = info.nombreComuna;
                var idElemento = info.idComuna;

                var li = document.createElement("li");
                li.setAttribute("onclick", "mostrarLinea(this);return false;");
                //li.setAttribute("region", i.toString());
                //li.setAttribute("comuna", j.toString());
                //li.setAttribute("idComuna", idElemento);
                li.setAttribute("uid", info.uids);
                li.setAttribute("color", info.colorComuna);
                li.setAttribute("titulo", info.nombreComuna);

                var a = document.createElement("a");
                a.href = "#";
                a.textContent = nombreElemento;

                var separador = document.createElement("li");
                separador.setAttribute("role", "separator");
                separador.className = "divider";

                li.appendChild(a);
                ul.appendChild(li);
                if ((j + 1) < cantidadComunas)
                    ul.appendChild(separador);
            }

            div.appendChild(button);
            div.appendChild(ul);
            contenedor.appendChild(div);
        }
    }

    function NumerosRomanos(numero)
    {
        switch (numero)
        {
            case "1": return "I"; break;
            case "2": return "II"; break;
            case "3": return "III"; break;
            case "4": return "IV"; break;
            case "5": return "V"; break;
            case "6": return "VI"; break;
            case "7": return "VII"; break;
            case "8": return "VIII"; break;
            case "9": return "IX"; break;
            case "10": return "X"; break;
            case "11": return "XI"; break;
            case "12": return "XII"; break;
            case "13": return "XIII"; break;
            case "14": return "XIV"; break;
            case "15": return "XV"; break;
            case "16": return "XVI"; break;
            case "17": return "XVII"; break;
            case "18": return "XVIII"; break;
        }
    }

    function ocultarLinea(numero) {
        var id = numero.attributes.uid.value;

        var update = {
            opacity: 0,
            visible: false,
            showlegend: false
        };
        Plotly.restyle('chart_div', update, [id]);

        document.getElementById(id).remove();
    }

    function mostrarLinea(numero) {

        var id = numero.attributes.uid.value;
        var titulo = numero.attributes.titulo.value;
        var aux = document.getElementById("valorColor").value;
        aux = parseInt(aux);   
        var color= Colores(parseInt(aux));
        aux++;
        if (aux > 16) {
            aux = 1;
        }
        document.getElementById("valorColor").value = aux;
        //var color = numero.attributes.color.value;

        var update = {
            opacity: 1,
            visible: true,
            showlegend: false,
            'marker.color': color,
            line: {
                'marker.color': color,
            }
        };
        Plotly.restyle('chart_div', update, [id]);


        var leg = document.getElementById(id);

        if (leg == null) {
            var legend = document.getElementById('legend');
            var lis = document.createElement('li');
            var label = numero.nombreGlobal;

            lis.id = id;
            lis.style.cssText = "border: 1px solid black;margin: 5px 10px;padding:5px;min-width:100px;max-width:200px;white-space:nowrap;overflow:hidden;text-overflow:ellipsis;";
            lis.setAttribute("uid", id);
            lis.setAttribute("onclick", "ocultarLinea(this);return false;");
            lis.innerHTML = '<span class="glyphicon glyphicon-remove" aria-hidden="true"> </span> <div class="legendMarker" style="margin-left: -25 ;background-color:' + color + ';"></div>' + "&nbsp;&nbsp;" + titulo;
            legend.appendChild(lis);
        }
        else {
            document.getElementById(id).remove();
            var legend = document.getElementById('legend');
            var lis = document.createElement('li');
            var label = numero.nombreGlobal;

            lis.id = id;
            lis.style.cssText = "border: 1px solid black;margin: 5px 10px;padding:5px;min-width:100px;max-width:200px;white-space:nowrap;overflow:hidden;text-overflow:ellipsis;";
            lis.setAttribute("uid", id);
            lis.setAttribute("onclick", "ocultarLinea(this);return false;");
            lis.innerHTML = '<span class="glyphicon glyphicon-remove" aria-hidden="true"> </span> <div class="legendMarker" style="margin-left: -25 ;background-color:' + color + ';"></div>' + "&nbsp;&nbsp;" + titulo;
            legend.appendChild(lis);
        }

    }

    function Colores(aux) {
        switch (aux) {
            case 1:
                return "#800000";
            case 2:
                return "#FF0000";
            case 3:
                return "#FFa500";
            case 4:
                return "#FFFF00";
            case 5:
                return "#808000";
            case 6:
                return "#800080";
            case 7:
                return "#FF00FF";
            case 8:
                return "#00FF00";
            case 9:
                return "#0080000";
            case 10:
                return "#000080";
            case 11:
                return "#0000FF";
            case 12:
                return "#00FFFF";
            case 13:
                return "#008080";
            case 14:
                return "#000000";
            case 15:
                return "#C0C0C0";
            case 16:
                return "#808080";

        
        }
    }


    function drawChart(filtroBotonInferior) {

        //document.getElementById("chart_div").innerHTML = '';
        //document.getElementById("chart_div").innerHTML = '<img id="cargaa" src="../../images/Preloader.gif" class="img-responsive" />';
        var jsonData = $.ajax({
            url: "../../../AjaxGraficos/GetDatosComoGastaTuMunicipio2",
            data: { 'id': "", 'busqueda': filtroBotonInferior },
            dataType: "json",
            async: false,
            error: function (xhr, status, error) {
                console.log(xhr);
                console.log(status);
                console.log(error);
            }
        }).responseJSON;
        $("#cargaa").addClass("hidden");
        Plotly.newPlot('chart_div', jsonData.data, jsonData.layout, { displayModeBar: false });

        layout = jsonData.layout;

        if (filtroBotonInferior == null) {
            var widt = document.getElementById("chart_div").parentElement.clientWidth * 1 -10;// -10 margen del div row
            var heigh = document.getElementById("chart_div").parentElement.clientHeight * 1;
            document.getElementById("heighh").value = heigh;
            document.getElementById("widthh").value = widt;
        }
        else {
            var widt = document.getElementById("widthh").value;
            var heigh = document.getElementById("heighh").value;
        }
        var update = {
            width: widt,
            showlegend: false,
            height: heigh,
            margin: {
                l: 60,
                r: 50,
                b: 50,
                t: 60,
                pad: 0
            }
        }

        Plotly.relayout('chart_div', update);

        var div1 = document.getElementById("iconos");
        var div = document.createElement("div");
        div.id = "lineaBotones";
        div.name = "lineaBotones";
        div1.appendChild(div);

        var cars =
            ["../../Content/images/tiempoTotal.png",
                "../../Content/images/tiempoInt.png",
            "../../Content/images/tiempoprog.png",
            "../../Content/images/tiempoEdu.png",
            "../../Content/images/tiemposalud.png",
            "../../Content/images/tiemposerv.png"];

        CrearBotonesRegiones(jsonData.categorias,jsonData.global)

        var botonesInferior = jsonData.botonesInferior;
        var botonesInferiorSelected = jsonData.botonesInferiorSelected;

        for (i = 0 ; i < botonesInferior.length; i++) {

            var boton = document.createElement("img");

            if (botonesInferiorSelected[i] == true) {
                boton.className = "classseleccionado";
            }
            else {
                boton.className = "classinsseleccionado";
            }

            boton.src = cars[i];// "../../images/app10.png";
            boton.value = botonesInferior[i].toString();
            boton.name = botonesInferior[i].toString();
            boton.setAttribute("onclick", "CambiarFiltro(this)");
            div.appendChild(boton);
        }


    }

    function CambiarFiltro(filtroBotonInferior)
    {
        document.getElementById("lineaBotones").remove();
        filtroBotonInferior = filtroBotonInferior.name;
        drawChart(filtroBotonInferior);
        $("#textonombre").html(filtroBotonInferior);
    }

    $(document).ready(function () {
        drawChart();

        $(".dropdown-toggle").click(function () {
            if ($(this).parent().hasClass("open")) {
                $(this).parent().removeClass("open");
            }
            else {
                $(this).parent().addClass("open");
            }
            
        });
    });

    $(window).resize(function () {
        //drawChart2();
    });

    function drawChart2()
    {
        var widt = document.getElementById("chart_div").parentElement.clientWidth * 0.9;
        document.getElementById("widthh").value = widt ;
        var heigh = document.getElementById("heighh").value;

        var update = {
            width: widt,
            height: heigh
        }
        Plotly.relayout('chart_div', update);
    }