
    
    

document.write('<input id="tipoValor" name="tipoValor" type="hidden" />');
document.write('<input id="contenedorColapse" name="contenedorColapse" type="hidden" value="1" />');
document.write('<input id="typeFamily" name="typeFamily" type="hidden" />');






    function separadordemile(number){
        var result = '';
        number = number.toString();
        while( number.length > 3 )
        {
            result = '.' + number.substr(number.length - 3) + result;
            number = number.substring(0, number.length - 3);
        }
        result = number + result;
        return result;
    }
    function drawChart() {
        document.getElementById("chart_div").innerHTML = '';
        document.getElementById("chart_div").innerHTML = '<p class="text-center" id="cargaa"><img src="../../Content/images/Preloader.gif" class="img-fluid" /></p>';
        var Width = document.getElementById("chart_div").parentElement.clientWidth;

        var margin = 10,
            diameter = Width;

        var pack = d3.layout.pack()
            .padding(2)
            .size([diameter - margin, diameter - margin])
            .value(function (d) { return d.valor; })

        var svg = d3.select("body").select("#chart_div").append("svg")
            .attr("width", diameter - margin)
            .attr("height", diameter - margin)
            .append("g")
            .attr("transform", "translate(" + diameter / 2 + "," + diameter / 2 + ")");

        var tooltip = d3.select("body")
            .append("div")
            .style("position", "absolute")
            .style("z-index", "10")
            .style("visibility", "hidden")
            .style("color", "white")
            .style("padding", "8px")
            .style("background-color", "rgba(0, 0, 0, 0.75)")
            .style("border-radius", "6px")
            .style("font", "12px sans-serif")
            .text("tooltip");

        var tipoValor = document.getElementById("tipoValor").value;
        var comuna = document.getElementById("comuna").value;
        var ids = "";

        var opt1 = document.getElementsByClassName("optInteres");
        var opt2 = document.getElementsByClassName("optPartido");
        var valueColapse = document.getElementById("contenedorColapse").value;

        if (valueColapse == "1") {
            for (i = 0; i < opt1.length; i++) {
                if (opt1[i].checked == true) {
                    //ids += "opt1-" + opt1[i].id + "-num1Option-" + opt1[i].name + "|";
                    ids += "opt1-" + opt1[i].id + "|";
                }
            }
        }
        else {
            for (i = 0; i < opt2.length; i++) {
                if (opt2[i].checked == true) {
                    //ids += "opt2-" + opt2[i].id + "-num2Option-" + opt2[i].name + "|";
                    ids += "opt2-" + opt2[i].id + "|";
                }
            }
        }

        var texto = document.getElementById("comuna").value;
        texto = texto.toLowerCase()
        texto = texto.replace("á", "a");
        texto = texto.replace("é", "e");
        texto = texto.replace("í", "i");
        texto = texto.replace("ó", "o");
        texto = texto.replace("ú", "u");
        texto = texto.replace(" ", "");

        var id = tipoValor;
        var url = "../../../AjaxGraficos/GetDatosMunicipio1/" + id;
        d3.json(url, function (error, root) {
            if (error)
                throw error;
            $("#cargaa").addClass("hidden");
            var focus = root,
                nodes = pack.nodes(root),
                view;

            var circle = svg.selectAll("circle")
                .data(nodes)
                .enter().append("circle")
                .attr("nombre", function (d) { return d.name })
                .attr("class", function (d) {
                    if (d.visibleTooltip == "True") {
                        return "node";
                    }
                    else {
                        return d.parent ? d.children ? "node" : "node node--leaf" : "node node--root";
                    }
                })
                .style("fill", function (d) { return d.color })
                .on("click", function (d) {
                    if (focus !== d) zoom(d), d3.event.stopPropagation();

                });

            var text = svg.selectAll("text")
                .data(nodes)
                .enter().append("text")
                .attr("class", "label")
                .style("fill-opacity", function (d) { return d.parent === root ? 1 : 0; })
                .style("display", function (d) { return d.parent === root ? "" : "none"; })
                .text(function (d) { return (d.name /*+ ": " + d.valor*/); });

            var node = svg.selectAll("circle,text");
            node.on("mouseover", function (d) {
                if (d.visibleTooltip == "True") {
                    if(d.tooltipComuna != null){
                        tooltip.html("REGION: " + d.tooltipRegion + "<br/>" + "MUNICIPIO: " + d.tooltipComuna + "<br/>" + "Area: " + d.name + "<br/>" + "GASTO MONTO: $" + separadordemile(d.value));
                    }
                    else{
                        if (d.tooltipRegion == "") {
                            tooltip.html("REGION: " + d.name + "<br/>" /*+ "GASTO MONTO: $" + d.tooltipGasto*/);
                        }
                        else {
                            tooltip.html("REGION: " + d.tooltipRegion + "<br/>" + "MUNICIPIO: " + d.name + "<br/>" + "GASTO MONTO: " + d.tooltipGasto);
                        }
                    }
                    tooltip.style("visibility", "visible");
                }
            })
                .on("mousemove", function (d) {
                    if (d.visibleTooltip == "True") {
                        return tooltip.style("top", (d3.event.pageY - 10) + "px").style("left", (d3.event.pageX + 10) + "px");
                    }
                })
                .on("mouseout", function (d) {
                    if (d.visibleTooltip == "True") {
                        return tooltip.style("visibility", "hidden");
                    }
                })


            d3.select("body").on("click", function () { zoom(root); });

            zoomTo([root.x, root.y, root.r * 2 + margin]);

            function zoom(d) {
                var focus0 = focus; focus = d;

                var transition = d3.transition()
                    .duration(d3.event.altKey ? 7500 : 750)
                    .tween("zoom", function (d) {
                        var i = d3.interpolateZoom(view, [focus.x, focus.y, focus.r * 2 + margin]);
                        return function (t) { zoomTo(i(t)); };
                    });

                transition.selectAll("text")
                  .filter(function (d) { return d.parent === focus || this.style.display === "inline"; })
                    .style("fill-opacity", function (d) { return d.parent === focus ? 1 : 0; })
                    .each("start", function (d) { if (d.parent === focus) this.style.display = "inline"; })
                    .each("end", function (d) { if (d.parent !== focus) this.style.display = "none"; });
            }

            function zoomTo(v) {
                var k = diameter / v[2]; view = v;
                node.attr("transform", function (d) { return "translate(" + (d.x - v[0]) * k + "," + (d.y - v[1]) * k + ")"; });
                circle.attr("r", function (d) { return d.r * k; });
            }
        });
        

        d3.select(self.frameElement).style("height", diameter + "px");
    }

    function collapse(contenedor) {
        var id = contenedor.id;

        if (id == "collapse1") {
            $("#ContenedorOpciones").toggle();
            $("#ContenedorOpciones2").toggle();
            var opt1 = document.getElementsByClassName("optInteres");
            for (i = 0; i < opt1.length; i++) {
                opt1[i].checked = false;
            }
            if (document.getElementById("contenedorColapse").value == "1") {
                document.getElementById("contenedorColapse").value = "2";
                $("#triangulo1").removeClass("glyphicon-chevron-down");
                $("#triangulo1").addClass("glyphicon-chevron-right");
                $("#triangulo2").removeClass("glyphicon-chevron-right");
                $("#triangulo2").addClass("glyphicon-chevron-down");
            }
            else {
                $("#triangulo2").removeClass("glyphicon-chevron-down");
                $("#triangulo2").addClass("glyphicon-chevron-right");
                $("#triangulo1").removeClass("glyphicon-chevron-right");
                $("#triangulo1").addClass("glyphicon-chevron-down");
                
                document.getElementById("contenedorColapse").value = "1";
            }
        }
        else{
            $("#ContenedorOpciones").toggle();
            $("#ContenedorOpciones2").toggle();
            var opt2 = document.getElementsByClassName("optPartido");
            for (i = 0; i < opt2.length; i++) {
                opt2[i].checked = false;
            }

            
            if (document.getElementById("contenedorColapse").value == "2") {
                $("#triangulo2").removeClass("glyphicon-chevron-down");
                $("#triangulo2").addClass("glyphicon-chevron-right");
                $("#triangulo1").removeClass("glyphicon-chevron-right");
                $("#triangulo1").addClass("glyphicon-chevron-down");
                document.getElementById("contenedorColapse").value = "1";

            }
            else {
                $("#triangulo1").removeClass("glyphicon-chevron-down");
                $("#triangulo1").addClass("glyphicon-chevron-right");
                $("#triangulo2").removeClass("glyphicon-chevron-right");
                $("#triangulo2").addClass("glyphicon-chevron-down");
                document.getElementById("contenedorColapse").value = "2";
            }
        }


    }

    function drawOptions()
    {
        var jsonData = $.ajax({
            url: "../../../AjaxGraficos/GetDatosMunicipio2",
            dataType: "json",
            async: false
        }).responseJSON;

        var opciones1 = jsonData.opcion1;
        var opciones2 = jsonData.opcion2;

        var elementos1 = "";
        for (i = 0; i < opciones1.elementos; i++) {
            var id = opciones1.ids[i];
            var nombre = opciones1.nombre[i];
            //var opcion = opciones1.numeroOpcion[i];
            elementos1 += '<div><label class="radio"><input type="radio" class="optInteres" name="optPartidoRadio" id="' + id + '" onclick="cambioTipoDato(this)" value="false" checked>'+nombre+'</label></div>'




            //elementos1 += '<tr> <td  style="padding-bottom:10px;" ><input class="optInteres" type="radio" id="' + id + '" name="optPartidoRadio" onclick="cambioTipoDato(this)" value="false">&nbsp;' + nombre + '</td></tr>'
        }
        document.getElementById("ContenedorOpciones").innerHTML = elementos1;

        var elementos2 = "";
        for (i = 0; i < opciones2.elementos; i++) {
            var id = opciones2.ids[i];
            var nombre = opciones2.nombre[i];
            //var opcion = opciones2.numeroOpcion[i];
            //elementos2 += '<tr> <td  style="padding-bottom:10px;" ><input class="optPartido" type="radio" id="' + id + '" name="optPartidoRadio" onclick="cambioTipoDato(this)" value="false">&nbsp;' + nombre + '</td></tr>'

            elementos2 += '<div><label class="radio"><input type="radio" class="optPartido" name="optPartidoRadio" id="' + id + '" onclick="cambioTipoDato(this)" value="false" checked>' + nombre + '</label></div>'
        }
        document.getElementById("ContenedorOpciones2").innerHTML = elementos2;
    }

    function cambioTipoDato(dato)
    {
        $("#comuna").val("");
        if (dato.value == "false") {
            dato.value = "true";
        }
        else {
            dato.value = "false";
            dato.checked = false;
        }

        busquedaComunaFinal();
    }

    function cambioTipoValor(dato)
    {
        var value = document.getElementById("tipoValor").value;
        if (value == "nominal") {
            document.getElementById("tipoValor").value = "percapita";
        }
        else {
            document.getElementById("tipoValor").value = "nominal";
        }
        var opt1 = document.getElementsByClassName("optInteres");
        var opt2 = document.getElementsByClassName("optPartido");
        for (i = 0; i < opt1.length; i++) {
            opt1[i].checked = false;
        }
        for (i = 0; i < opt2.length; i++) {
            opt2[i].checked = false;
        }
        //document.getElementById("tipoValor").value = "";
        drawChart();
    }

    function busquedaComuna(e) {
        if (document.getElementById("comuna").value.length>3) {
            if (document.getElementById("comuna").value != "") {
                var opt1 = document.getElementsByClassName("optInteres");
                var opt2 = document.getElementsByClassName("optPartido");

                for (i = 0; i < opt1.length; i++) {
                    opt1[i].checked = false;
                }
                for (i = 0; i < opt2.length; i++) {
                    opt2[i].checked = false;
                }
                busquedaComunaFinal();
            }
        }
    }

    function busquedaComunaFinal() {
            document.getElementById("chart_div").innerHTML = '';
            document.getElementById("chart_div").innerHTML = '<p class="text-center" id="cargaa"><img src="../../Content/images/Preloader.gif" class="img-fluid" /></p>';
            var Width = document.getElementById("chart_div").parentElement.clientWidth;

            var margin = 20,
                diameter = Width;

            var pack = d3.layout.pack()
                .padding(2)
                .size([diameter - margin, diameter - margin])
                .value(function (d) { return d.valor; })

            var svg = d3.select("body").select("#chart_div").append("svg")
                .attr("width", diameter)
                .attr("height", diameter)
                .append("g")
                .attr("transform", "translate(" + diameter / 2 + "," + diameter / 2 + ")");

            var tooltip = d3.select("body")
                .append("div")
                .style("position", "absolute")
                .style("z-index", "10")
                .style("visibility", "hidden")
                .style("color", "white")
                .style("padding", "8px")
                .style("background-color", "rgba(0, 0, 0, 0.75)")
                .style("border-radius", "6px")
                .style("font", "12px sans-serif")
                .text("tooltip");

            var tipoValor = document.getElementById("tipoValor").value;
            var comuna = document.getElementById("comuna").value;
            var ids1 = "";
            var ids2 = "";
            var opt1 = document.getElementsByClassName("optInteres");
            var opt2 = document.getElementsByClassName("optPartido");
            var valueColapse = document.getElementById("contenedorColapse").value;

            if (valueColapse == "1") {
                for (i = 0; i < opt1.length; i++) {
                    if (opt1[i].checked == true) {
                        ids1 += "-" + opt1[i].id + "-";
                    }
                }
            }
            else {
                for (i = 0; i < opt2.length; i++) {
                    if (opt2[i].checked == true) {
                        ids2 += "-" + opt2[i].id + "-";
                    }
                }
            }

            var texto = document.getElementById("comuna").value;
            texto = texto.toLowerCase()
            texto = texto.replace("á", "a");
            texto = texto.replace("é", "e");
            texto = texto.replace("í", "i");
            texto = texto.replace("ó", "o");
            texto = texto.replace("ú", "u");
            texto = texto.replace(" ", "");

            var id = tipoValor;
            var url = "../../../AjaxGraficos/GetDatosMunicipio1/" + id;
            d3.json(url, function (error, root) {
                if (error)
                    throw error;
                $("#cargaa").addClass("hidden");
                var focus = root,
                    nodes = pack.nodes(root),
                    view;

                if(texto!=""){
                    var circle = svg.selectAll("circle")
                    .data(nodes)
                    .enter().append("circle")
                    .attr("nombre", function (d) { return d.name })
                    .attr("class", function (d) {
                        if (d.visibleTooltip == "True") {
                            return "node";
                        }
                        else {
                            return d.parent ? d.children ? "node" : "node node--leaf" : "node node--root";
                        }
                    })
                    .style("fill", function (d) {
                        if (d.nameBus !=null &&d.nameBus.includes(texto)) {
                            return "#c853dd";
                        }
                        else {
                            return d.color;
                        }
                    })
                    .on("click", function (d) {
                        if (focus !== d) zoom(d), d3.event.stopPropagation();

                    });
                }
                else if (ids1 != "") {
                    var circle = svg.selectAll("circle")
                    .data(nodes)
                    .enter().append("circle")
                    .attr("nombre", function (d) { return d.name })
                    .attr("class", function (d) {
                        if (d.visibleTooltip == "True") {
                            return "node";
                        }
                        else {
                            return d.parent ? d.children ? "node" : "node node--leaf" : "node node--root";
                        }
                    })
                    .style("fill", function (d) {
                        if (d.texto2 != null && d.texto2.includes(ids1)) {
                            return "#c853dd";
                        }
                        else {
                            return d.color;
                        }
                    })
                    .on("click", function (d) {
                        if (focus !== d) zoom(d), d3.event.stopPropagation();

                    });
                }
                else if (ids2 != "") {
                    ids2 = ids2.substr(1);
                    ids2 = ids2.slice(0, -1);
                    var circle = svg.selectAll("circle")
                    .data(nodes)
                    .enter().append("circle")
                    .attr("nombre", function (d) { return d.name })
                    .attr("class", function (d) {
                        if (d.visibleTooltip == "True") {
                            return "node";
                        }
                        else {
                            return d.parent ? d.children ? "node" : "node node--leaf" : "node node--root";
                        }
                    })
                    .style("fill", function (d) {
                        if (d.texto1 != null && d.texto1.includes(ids2)) {
                            return "#c853dd";
                        }
                        else {
                            return d.color;
                        }
                    })
                    .on("click", function (d) {
                        if (focus !== d) zoom(d), d3.event.stopPropagation();

                    });
                }
                else {
                    var circle = svg.selectAll("circle")
                    .data(nodes)
                    .enter().append("circle")
                    .attr("nombre", function (d) { return d.name })
                    .attr("class", function (d) {
                        if (d.visibleTooltip == "True") {
                            return "node";
                        }
                        else {
                            return d.parent ? d.children ? "node" : "node node--leaf" : "node node--root";
                        }
                    })
                    .style("fill", function (d) {
                        return d.color;
                    })
                    .on("click", function (d) {
                        if (focus !== d) zoom(d), d3.event.stopPropagation();

                    });
                }

                var text = svg.selectAll("text")
                    .data(nodes)
                    .enter().append("text")
                    .attr("class", "label")
                    .style("fill-opacity", function (d) { return d.parent === root ? 1 : 0; })
                    .style("display", function (d) { return d.parent === root ? "" : "none"; })
                    .text(function (d) { return (d.name); });

                var node = svg.selectAll("circle,text");
                node.on("mouseover", function (d) {
                    if (d.visibleTooltip == "True") {
                        if (d.tooltipComuna != null) {
                            tooltip.html("REGION: " + d.tooltipRegion + "<br/>" + "MUNICIPIO: " + d.tooltipComuna + "<br/>" + "Area: " + d.name + "<br/>" + "GASTO MONTO [sum]: " + d.value);
                        }
                        else {
                            if (d.tooltipRegion == "") {
                                tooltip.html("REGION: " + d.name + "<br/>" + "GASTO MONTO [sum]: " + d.tooltipGasto);
                            }
                            else {
                                tooltip.html("REGION: " + d.tooltipRegion + "<br/>" + "MUNICIPIO: " + d.name + "<br/>" + "GASTO MONTO [sum]: " + d.tooltipGasto);
                            }
                        }
                        tooltip.style("visibility", "visible");
                    }
                })
                    .on("mousemove", function (d) {
                        if (d.visibleTooltip == "True") {
                            return tooltip.style("top", (d3.event.pageY - 10) + "px").style("left", (d3.event.pageX + 10) + "px");
                        }
                    })
                    .on("mouseout", function (d) {
                        if (d.visibleTooltip == "True") {
                            return tooltip.style("visibility", "hidden");
                        }
                    })


                d3.select("body").on("click", function () { zoom(root); });

                zoomTo([root.x, root.y, root.r * 2 + margin]);

                function zoom(d) {
                    var focus0 = focus; focus = d;

                    var transition = d3.transition()
                        .duration(d3.event.altKey ? 7500 : 750)
                        .tween("zoom", function (d) {
                            var i = d3.interpolateZoom(view, [focus.x, focus.y, focus.r * 2 + margin]);
                            return function (t) { zoomTo(i(t)); };
                        });

                    transition.selectAll("text")
                      .filter(function (d) { return d.parent === focus || this.style.display === "inline"; })
                        .style("fill-opacity", function (d) { return d.parent === focus ? 1 : 0; })
                        .each("start", function (d) { if (d.parent === focus) this.style.display = "inline"; })
                        .each("end", function (d) { if (d.parent !== focus) this.style.display = "none"; });
                }

                function zoomTo(v) {
                    var k = diameter / v[2]; view = v;
                    node.attr("transform", function (d) { return "translate(" + (d.x - v[0]) * k + "," + (d.y - v[1]) * k + ")"; });
                    circle.attr("r", function (d) { return d.r * k; });
                }
            });


            d3.select(self.frameElement).style("height", diameter + "px");

            
    }


    $(document).ready(function () {
        document.getElementById("tipoValor").value = "nominal";
        drawChart();

        drawOptions();
        $("#ContenedorOpciones2").toggle();
        document.getElementById("contenedorColapse").value = "1";
    })

    $(window).resize(function () {

        drawChart();
    });