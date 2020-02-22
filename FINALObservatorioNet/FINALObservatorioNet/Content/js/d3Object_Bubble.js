
function drawChart(notSearchLevel, tipoFamilia, dimension, formato) {
    var diameter = dimension,//TAMAÑO GRAFICO
        format = d3.format(",d"),
        color = d3.scale.category20c();

    var bubble = d3.layout.pack()
        .sort(null)
        .size([diameter, diameter])
        .padding(1.5);

    var svg = d3.select("body").select("#chart_div").append("svg")
        .attr("width", diameter)
        .attr("height", diameter)
        .attr("id", "bubbleChart")
        .attr("class", "bubble");

    var tooltip = d3.select("body")
        .append("div")
        .attr("id", "tooltip")
        .style("position", "absolute")
        .style("z-index", "10")
        .style("visibility", "hidden")
        .style("color", "white")
        .style("padding", "8px")
        .style("background-color", "rgba(0, 0, 0, 0.75)")
        .style("border-radius", "6px")
        .style("font", "12px sans-serif")
        .text("tooltip");

    var idFormato = formato;
    if (idFormato!=2) { idFormato = 1; }

    //var url = "@Url.Action("GetDatosEnQueGastaElEstado1", "ComoGastaElEstado")" + "/" + idFormato;
    d3.json("../../AjaxGraficos/GetDatosEnQueGastaElEstado1/" + idFormato, function (error, root) {
        var node = svg.selectAll(".node")
            .data(bubble.nodes(classes(root, notSearchLevel))
                .filter(function (d) { return !d.children; }))
            .enter().append("g")
            .attr("class", "node")
            .attr("nivel", function (d) { return d.nivelFamily; })
            .attr("transform", function (d) { return "translate(" + d.x + "," + d.y + ")"; });



        node.append("circle")
            .attr("r", function (d) { return d.r; })
            .style("stroke", "#f9f9f9")
            .style("stroke-width", "5")
            .style("fill", function (d) { return (d.colorFamily); })
            .style("opacity", function (d) {
                var name = $("#lastCheck").val();
                var numeroClick = $("#numeroClick").val();

                if (d.className == name && numeroClick == 3) {
                    return 1;
                }
                if (numeroClick == 3) {
                    return 0.3;
                }
                if (notSearchLevel == "Nivel2") {
                    return 1;
                }
                if (d.typeFamily == tipoFamilia) {
                    return 1;
                }
                else {
                    return 0.3;
                }
            })
            .on("mouseover", function (d) {
                tooltip.text(d.tooltip);
                //var num = d.value;
                //num = num.toString().split('').reverse().join('').replace(/(?=\d*\.?)(\d{3})/g, '$1.');
                //num = num.split('').reverse().join('').replace(/^[\.]/, '');
                //var tooltip = "Gasto total: " + num;

                //tooltip.text(d.className + ": " + tooltip);

                tooltip.style("visibility", "visible");
            })
            .on("mousemove", function () {
                return tooltip.style("top", (d3.event.pageY - 10) + "px").style("left", (d3.event.pageX + 10) + "px");
            })
            .on("mouseout", function () { return tooltip.style("visibility", "hidden"); })
            .on("click", function (d) {
                if (d.nivelFamily == 1) {
                    tooltip.style("visibility", "hidden");
                    $("#numeroClick").val(2);
                    $("#bubbleChart").remove();

                    var aux = d.value;
                    var valor = aux.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
                    document.getElementById("tituloGrafico").innerHTML = '<i class="material-icons celeste md-24">forward</i> El gasto en <strong>' + d.className + '</strong>  para el año ' + d.ano + ' fue de MM$ ' + valor;
                    $("#typeFamily").val(d.typeFamily);
                    drawChart("Nivel1", d.typeFamily, sizeDimension(), idFormato);
                    document.getElementById("texto2").style.display = "block";
                    document.getElementById("text2").innerText = d.text;
                }
                if (d.nivelFamily == 2) {
                    var numeroClick = $("#numeroClick").val();

                    if (numeroClick == 2) {
                        if (d.typeFamily == tipoFamilia) {
                            tooltip.style("visibility", "hidden")
                            $("#numeroClick").val(3);
                            $("#lastCheck").val(d.className);
                            $("#bubbleChart").remove();
                            drawChart("Nivel1", d.typeFamily, sizeDimension(), idFormato);
                            $("#typeFamily").val(d.typeFamily);
                            document.getElementById("texto3").style.display = "block";
                            document.getElementById("text3").innerText = d.text;
                            var aux = d.value;
                            var valor = aux.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
                            document.getElementById("tituloGrafico2").innerHTML = '<i class="material-icons celeste md-24">forward</i> El gasto en <strong>' + d.className + '</strong>  para el año ' + d.ano + ' fue de MM$ ' + valor;
                        }
                    }

                    if (numeroClick == 3) {
                        if (d.typeFamily == tipoFamilia) {
                            tooltip.style("visibility", "hidden");
                            $("#numeroClick").val(3);
                            $("#lastCheck").val(d.className);
                            $("#bubbleChart").remove();
                            $("#typeFamily").val(d.typeFamily);
                            drawChart("Nivel1", d.typeFamily, sizeDimension());
                            document.getElementById("texto3").style.display = "block";
                            document.getElementById("text3").innerText = d.text;
                            var aux = d.value;
                            var valor = aux.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
                            document.getElementById("tituloGrafico2").innerHTML = '<i class="material-icons celeste md-24">forward</i>El gasto en ' + d.className + '  para el año ' + d.ano + ' fue de MM$ ' + valor;
                        }
                    }
                }
            });

        var fontSize = 12;
        var tamaño = sizeDimension();
        if (tamaño < 380) {
            fontSize = 10;
        }
        node.append("text")
            .attr("dy", ".3em")
            .style("text-anchor", "middle")
            .style("pointer-events", "none")
            .style("font-size", fontSize)
            //.style("font-weight", "bold")
            .style("font-family", "lato,helvetica neue, helvetica, arial, sans-serif")
            .style("padding", "5px")
            .style("fill", "#2E2D2D")
            .style("opacity", function (d) {
                var name = $("#lastCheck").val();
                var numeroClick = $("#numeroClick").val();

                if (d.className == name && numeroClick == 3) {
                    return 1;
                }
                if (numeroClick == 3) {
                    return 0.3;
                }
                if (notSearchLevel == "Nivel2") {
                    return 1;
                }
                if (d.typeFamily == tipoFamilia) {
                    return 1;
                }
                else {
                    return 0.3;
                }
            })
            .text(function (d) {
                var largo = d.className.length * 2;
                var diametro = d.r * 2;
                if (70 < diametro) {
                    return d.className.substring(0, d.r / 3);
                }
                else {

                }
            });
    });
}
// Returns a flattened hierarchy containing all leaf nodes under the root.
function classes(root, notSearchLevel) {
    var classes = [];

    function recurse(name, node, notSearchLevel) {
        if (node.children)
            node.children.forEach(function (child) {
                if (node.name != notSearchLevel) {
                    recurse(node.name, child, notSearchLevel);
                }
            });
        else classes.push({ packageName: name, className: node.name, value: node.size, nivelFamily: node.nivel, ano: node.ano, colorFamily: node.color, id: node.id, typeFamily: node.tipo, text: node.text, tooltip: node.tooltip });
    }

    recurse(null, root, notSearchLevel);
    return { children: classes };
}

$(document).ready(function () {
    $("#numeroClick").val(1);
    $("#chart_div").empty();
    
    drawChart("Nivel2", null, sizeDimension());
})

function redraw(origen) {

    var familia = $("#typeFamily").val();
    document.getElementById("texto2").style.display = "none";
    document.getElementById("texto3").style.display = "none";

    var idFormato = $("#formatoGrafico").find(":selected").attr('id');
    var busqueda = $("#numeroClick").val();

    //if(origen.id = "botonVolver"){

    $("#numeroClick").val(1);
    $("#lastCheck").val("");
    $("#bubbleChart").remove();
    document.getElementById("tituloGrafico").innerHTML = '';

    drawChart("Nivel2", null, sizeDimension(), idFormato);
}

function redrawChange(origen) {

    var familia = $("#typeFamily").val();
    document.getElementById("texto2").style.display = "none";
    document.getElementById("texto3").style.display = "none";

    var idFormato = $("#formatoGrafico").find(":selected").attr('id');
    var busqueda = $("#numeroClick").val();

    //if(origen.id = "botonVolver"){

    $("#numeroClick").val(1);
    $("#lastCheck").val("");
    $("#bubbleChart").remove();
    document.getElementById("tituloGrafico").innerHTML = '';

    //if (busqueda == 1) {
    drawChart("Nivel2", null, sizeDimension(), idFormato);
    //}
    //else {
    //    drawChart("Nivel1", familia, sizeDimension(), idFormato);
    //}
}

$(window).resize(function () {

    var idFormato = $("#formatoGrafico").find(":selected").attr('id');
    var busqueda = $("#numeroClick").val();
    var familia = $("#typeFamily").val();

    if (busqueda == 1) {
        $("#bubbleChart").remove();
        drawChart("Nivel2", familia, sizeDimension(), idFormato);
    }
    else {
        $("#bubbleChart").remove();
        drawChart("Nivel1", familia, sizeDimension(), idFormato);
    }
});

function sizeDimension() {

    var minWidth = screen.width / 5;
    var Width = document.getElementById("chart_div").parentElement.parentElement.clientWidth - 10;
    Width = Width * 1; // 5/13 de largo de ventana

    if (minWidth < 300) {
        minWidth = 300;
    }
    if (Width < minWidth) {
        Width = minWidth;
    }
    return Width;
}