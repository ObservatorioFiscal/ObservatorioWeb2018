//Fijar valores para que la presentación de valores sean en español
var es_ES = 
{
    "decimal": ",",
    "thousands": ".",
    "grouping": [3],
    "currency": ["$", ""],
    "dateTime": "%a %b %e %X %Y",
    "date": "%d/%m/%Y",
    "time": "%H:%M:%S",
    "periods": ["AM", "PM"],
    "days": ["Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado"],
    "shortDays": ["Dom", "Lun", "Mar", "Mi", "Jue", "Vie", "Sab"],
    "months": ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
    "shortMonths": ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"]
};

var ES = d3.formatDefaultLocale(es_ES);

$(document).ready(function() 
{
    var js;
    $.getJSON('../../../../Content/data/level3.json', callbackmydata);

    function callbackmydata ( data ) 
    {
        js = data;
        var texto = "Aceites_vegetales_y_grasas_comestibles";

        var data = $.map(js, function (obj) 
        {
            obj.id = obj.id
            return obj;
        });

        var data = $.map(js, function (obj) 
        {
            obj.text = obj.text
            return obj;
        });

        $('.categorias').select2(
            {
            theme: "bootstrap",
            placeholder: 'Aceites vegetales y grasas comestibles',
            allowClear: true,
            data: data,
            templateResult: function(data) 
            {
                return data.text;
            },
            sorter: function(data) 
            {
                return data.sort(function(a, b) 
                {
                    return a.text < b.text ? -1 : a.text > b.text ? 1 : 0;
                });
            }
            })
        .on("select2:select", function (e) 
        { 
            $('.select2-selection__rendered li.select2-selection__choice').sort(function(a, b) 
            {
                return $(a).text() < $(b).text() ? -1 : $(a).text() > $(b).text() ? 1 : 0;
            })
            .prependTo('.select2-selection__rendered');
        });
        grafo(texto);

        $('.categorias').on('change', function() 
        {
            //elimino svg y limpio el select para poner nuevas comunas
            d3.selectAll("svg").remove();
            $(".selector").html("");
            $(".selector").append('<option></option>');
            var datos = $(".categorias option:selected").text().replace(/\s/g, "_");
            grafo(datos);

        });

    }

});


function grafo(texto){
    // Creamos la variable svg para saber ancho y alto.
    var svg = d3.select("#canvas").append("svg");
    var elmnt = document.getElementById("canvas");
    var width = elmnt.clientWidth;
    var height = width/1.5;
    var radius = 20;

    // Creamos variable con escala de 10 colores estándar de d3
    var color = d3.scaleOrdinal(d3.schemeSet2);

    if (texto == "") {
        texto = 'Aceites_vegetales_y_grasas_comestibles';
    }
    texto = texto.replace("/", "");
    var urlLUIS = "";
    urlLUIS = "../../../../Content/data/categorias/" + texto + ".json";

    // carga de datos de las categorías
    var graph = d3.json(urlLUIS, function(error, graph)
    {
        if (error) throw error;

        // Variables de listbox a ser utilizadas en el grafo
        var data = '';
        if (texto == '')
        {
            var data = graph.Aceites_vegetales_y_grasas_comestibles[0]
        } 
        else 
        {
            var data = graph[texto][0]
        };

        // selector de municipios 
        // para resaltar ciudades
        var nodos = data.nodes;
        var dato2 = nodos.filter(function (n){return n.group === 1});
        var datos = $.map(dato2, function (obj)
        {
            obj.id = obj.id
            return obj;
        });
        var datos = $.map(dato2, function (obj) 
        {
            obj.text = obj.id
            return obj;
        });

        $(".selector").select2(
        {
            theme: "bootstrap",
            placeholder: '?Dónde está mi comuna¿',
            allowClear: true,
            data: datos,
            templateResult: function(data)
            {
                return data.text;
            },
            sorter: function(data)
            {
                return data.sort(function(a, b)
                {
                    return a.text < b.text ? -1 : a.text > b.text ? 1 : 0;
                });
            }
        })
        .on("select2:select", function (e)
        { 
            $('.select2-selection__rendered li.select2-selection__choice').sort(function(a, b)
            {
                return $(a).text() < $(b).text() ? -1 : $(a).text() > $(b).text() ? 1 : 0;
            })
            .prependTo('.select2-selection__rendered');
        });

        //máximo y mínimo
        var monto_max = d3.max(data.nodes, function(d)
        {
            return +d.monto;
        })
        console.log(monto_max);
        var monto_min = d3.min(data.nodes, function(d) 
        {
            return +d.monto;
        })
        console.log(monto_min);

        // Creamos la simulación y especificamos los links, la gravedad, la ubicación y la la distancia de overlaping
        var simulation = d3.forceSimulation()
        .nodes(data.nodes)
        .force("link", d3.forceLink().id(function(d) { return d.id; }))
        .force("charge", d3.forceManyBody().strength(1))
        .force("center", d3.forceCenter(width / 2, height / 2))
        .force("collision", d3.forceCollide().radius(function(d) { return (((d.monto - monto_min)/(monto_max - monto_min)*50) +8);}).iterations(10));

        // Reubicación del grafo en el centro y escaldo al 20% del tamaño total
        var g = svg.append("g")
        .attr("class", "everything");

        // Creación de los links, y fijación del grosor en base a la cantidad de órdenes de compra
        var link = g.append("g")
        .attr("class", "links")
        .selectAll("line")
        .data(data.links)
        .enter().append("line")
        .attr("id", "lineas")
        .attr("stroke-width", function(d) { return Math.log(d.monto/900000); });

        // Creación de los nodos y fijación de su tamaño en base al monto transado por el nodo y su color en base al tipo
        var nodes = g.append("g")
        .attr("class", "nodes");
        var node = nodes.selectAll("node")
        .data(data.nodes)
        .enter().append("g");
        var circle = node.append("circle")
        .attr("r", function (d) { if (monto_max == monto_min) { return Math.log(d.monto) } else { return ((d.monto - monto_min) / (monto_max - monto_min) * 40) }; }) 
        .attr("fill", function(d) { return color(d.group); })
        .attr("id", function(d){
        return "c" + (d.id.replace(/\s/g, ""));})
        .attr("class", "circle")
        .on('mouseover', fade(0.1))
        .on('mouseout', fade(1))
        

        // Añadir leyendas 
        // Colores 
        var legend = svg.selectAll(".legend")
        
        .data(color.domain())
        .enter().append("g")
        .attr("class", "legend")
        .attr("transform", function(d, i) { return "translate(0," + i * 20 + ")"; });
        console.log(legend);    
        legend.append("rect")
        .attr("x", window.innerWidth - window.innerWidth/30)
        .attr("y", window.innerHeight - window.innerHeight/3.5)
        .attr("width", 18)
        .attr("height", 18)
        .style("fill", color)
        .style("stroke", "white")
        .style("stroke-width", "1");

        legend.append("text")
        .attr("x", window.innerWidth - window.innerWidth/23)
        .style("font-family", "Roboto")
        .style("font-weight", "100")
        .style("fill", "black")
        .attr("y", window.innerHeight - window.innerHeight/3.65)
        .attr("dy", ".35em")
        .style("text-anchor", "end")
        .text(function(d) { if (d == 1) {return "Municipio"} else {return "Proveedor"}; });

        // Leyenda de circulos
        function tamaño(d) { return ((d.monto - monto_min)/(monto_max - monto_min)*40);}
        var numberFormat = d3.format(",.0f");
        var t_max = d3.max(nodos, function(d) 
        {
            return +d.monto;
        })
        var t_min = d3.min(nodos, function(d) 
        {
            return +d.monto
        });
        var linearSize = d3.scaleLinear().domain([numberFormat(t_min/1000000),numberFormat(t_max/1000000)]).range([1, 30]);


        g.append("g")
        .attr("class", "legendSize")
        .style("font-family","Roboto")
        .style("font-weight", "200")
        .style("fill", "grey")
        .attr("transform", "translate("+ ((10)) +", "+ (window.innerHeight - window.innerHeight/3.15)+")")

        g.append("text")
        .html("Monto (millones $)")
        .style("font-family","Roboto")
        .style("font-weight", "200")
        .style("fill", "grey")
        .attr("transform", "translate("+ ((10)) +","+ (window.innerHeight - window.innerHeight/2.95)+")");

        var legendSize = d3.legendSize()
        .scale(linearSize)
        .shape('circle')
        .shapePadding(32)
        .labelOffset(30)
        .orient('horizontal');

        svg.select(".legendSize")
        .call(legendSize);

        // Resaltar donde está mi comuna
        function highlightNode()
        {
            var node = d3.selectAll("circle");
            var comuna = $(".selector option:selected").text();
            var selectedVal = comuna.replace(/\s/g, "");
            var theNode = "#c"+selectedVal;


            if (selectedVal == "") 
            {
                node.style("stroke", "white").style("stroke-width", "1");
            } 
            else
            {
                var selected = node.filter(function (d, i) 
                {
                    return d.id != comuna;
                });
                selected.style("opacity", "0");
                var link = d3.selectAll("#lineas")
                link.style("opacity", "0");
                d3.selectAll("circle, #lineas").transition()
                .duration(3500)
                .style("opacity", 1);
            }
        }

        // Iniciamos la simulación 
        simulation
        .nodes(data.nodes)
        .on("tick", ticked);
        
        simulation
        .force("link")
        .links(data.links)
        .distance(30);
        
        simulation.alphaDecay(0.08);

        // Definimos los div de cada tooltip
        // primero el tooltip de nodos
        var tooltip = d3.select('#tooltip')            
        .append('div')                             
        .attr('class', 'tooltip')
        .style("opacity", "1")
        .style("z-index", "10")
        .style("right", "0");; 

        tooltip.append('div')                        
        .attr('class', 'nodo_titulo tool_head');

        tooltip.append('div')                        
        .attr('class', 'nodo_tipo tool_titulo');

        tooltip.append('div')                        
        .attr('class', 'nodo_tipo_v tool_valor');

        tooltip.append('div')                        
        .attr('class', 'nodo_nombre tool_titulo');

        tooltip.append('div')                        
        .attr('class', 'nodo_nombre_v tool_valor');

        tooltip.append('div')                        
        .attr('class', 'nodo_monto_t tool_titulo');                     

        tooltip.append('div')                       
        .attr('class', 'monto_t tool_valor');        

        tooltip.append('div')                        
        .attr('class', 'nodo_cant_t tool_titulo');                     

        tooltip.append('div')                       
        .attr('class', 'cant_t tool_valor');

        //luego el tooltip de links
        var tooltip2 = d3.select('#tooltip')            
        .append('div')                             
            .attr('class', 'tooltip')
            .style("opacity", "1")
            .style("right", "0")
            .style("z-index", "10");   

        tooltip2.append('div')                        
        .attr('class', 'nodo_titulo tool_head');

        tooltip2.append('div')                        
        .attr('class', 'nodo_tipo tool_titulo2')
        .style("background", "rgb(252, 141, 98)");

        tooltip2.append('div')                        
        .attr('class', 'nodo_tipo_v tool_valor');

        tooltip2.append('div')                        
        .attr('class', 'nodo_nombre tool_titulo2')
        .style("background", "rgb(102, 194, 165)");

        tooltip2.append('div')                        
        .attr('class', 'nodo_nombre_v tool_valor');

        tooltip2.append('div')                        
        .attr('class', 'nodo_monto_t tool_titulo');                     

        tooltip2.append('div')                       
        .attr('class', 'monto_t tool_valor');        

        tooltip2.append('div')                        
        .attr('class', 'nodo_cant_t tool_titulo');                     

        tooltip2.append('div')                       
        .attr('class', 'cant_t tool_valor');

        // Función para traer al frente
        d3.selection.prototype.moveToFront = function() 
        {
            return this.each(function()
            {
                this.parentNode.appendChild(this);
            });
        };

        // Tooltip y label en Nodos
        node.on('mouseover', function(d) 
        {
            //selecciono el nodo
            var l = d3.select(this); 

            //traigo el nodo al frente
            l.moveToFront();

            // Le añado el texto
            var text = l
            .append("text")
            .attr("class", "texto_nodos")
            .attr("y",-10)
            .attr("text-anchor", "start")
            .text(function(d) 
            {
                return d.id;
            });

            // aparición del tooltip
            var Tipo = '', numberFormat = d3.format(",.0f");
            // aparición del label
            d3.select(".texto_nodos").style("visibility","visible")

            // Llenado del tooltip
            if(d.group == 1){Tipo = "Municipio"} else {Tipo = "Proveedor"};
            tooltip.select('.nodo_titulo').html("Datos del Nodo");                
            tooltip.select('.nodo_tipo').html("Tipo:");                
            tooltip.select('.nodo_tipo_v').html(Tipo);                 
            tooltip.select('.nodo_nombre').html("Nombre:");                                
            tooltip.select('.nodo_nombre_v').html(d.id);
            tooltip.select('.nodo_monto_t').html("Monto:");                
            tooltip.select('.monto_t').html(numberFormat(d.monto));
            tooltip.select('.nodo_cant_t').html("Cantidad de OC:");                
            tooltip.select('.cant_t').html((d.cantidad));	                   
            tooltip.style('display', 'block');
        });

        // Tooltip y label en links
        link.on('mouseover', function(k) 
        {
            var numberFormat = d3.format(",.0f");
            // Llenado del tooltip
            tooltip2.select('.nodo_titulo').html("Datos del Link");
            tooltip2.select('.nodo_tipo').html("Proveedor:");                
            tooltip2.select('.nodo_tipo_v').html(k.source.id);                 
            tooltip2.select('.nodo_nombre').html("Municipio:");                                
            tooltip2.select('.nodo_nombre_v').html(k.target.id);
            tooltip2.select('.nodo_monto_t').html("Monto:");                
            tooltip2.select('.monto_t').html(numberFormat(k.monto));
            tooltip2.select('.nodo_cant_t').html("Cantidad de OC:");                
            tooltip2.select('.cant_t').html((k.cantidad));	                   
            tooltip2.style('display', 'block');                          
        });

        // quitar tooltips y labels al sacar el puntero
        node.on('mouseout', function()
        { 
            // quitar el nombre del nodo al salir.
            d3.selectAll(".texto_nodos").remove();
            tooltip.style('display', 'none');                           
        });

        link.on('mouseout', function()
        { 
            tooltip2.style('display', 'none');                           
        });


        // Funciones para hacer highlight
        var linkedByIndex = {};

        data.links.forEach(function(d) 
        {
            linkedByIndex[d.source.index + "," + d.target.index] = 1;
        });

        function isConnected(a, b) 
        {
            return linkedByIndex[a.index + "," + b.index] || linkedByIndex[b.index + "," + a.index] || a.index == b.index;
        }

        function fade(opacity) 
        {
            var thisOpacity;
            return function(d) 
            {
                node.style("stroke-opacity", function(o) 
                {
                    thisOpacity = isConnected(d, o) ? 1 : opacity;
                    this.setAttribute('fill-opacity', thisOpacity);
                    return thisOpacity;
                })
                .append("text")
                .attr("class", "texto_nodos")
                .attr("y", -10)
                .attr("text-anchor", "left")
                .text(function(o) 
                {
                    if (isConnected(d, o) == 1)
                    {
                        return o.id
                    } 
                    else
                    {
                        return ""
                    };
                });

                link.style("stroke-opacity", function(o) 
                {
                    return o.source === d || o.target === d ? 1 : opacity; 
                });
            }
        }
        // Funciones de búsqueda de nodo del selector
        $('.selector').on('change', function ()
        {
            var datos = $("#search option:selected").text();
            if(datos == "") 
            {
            } 
            else 
            {
                var datos = $("#search option:selected").text();
                highlightNode();
            }
        });
        resize();
        d3.select(window).on("resize", resize);

        // Funcion para ubicar nodos y links
        function ticked()
        {
            node
            .attr("transform", function(d) 
            {
                d.x = Math.max(radius, Math.min(width - radius, d.x));
                d.y = Math.max(radius, Math.min(height, d.y));
                return "translate(" + d.x + "," + d.y + ")" 
            });

            link
            .attr("x1", function(d) { return d.source.x; })
            .attr("y1", function(d) { return d.source.y; })
            .attr("x2", function(d) { return d.target.x; }) 
            .attr("y2", function(d) { return d.target.y; });

        }

        // Ajuste tamaño de svg, para cuadrar con el scale
        function resize()
        {
            var elmnt = document.getElementById("canvas");
            width = elmnt.clientWidth,
                height = width/1.5;
            svg.attr("width", width).attr("height", height);
        }

        // Función de inicio de arrastre
        

    });

};
//----------------------------*******************+++++++++++++++++++++çççççççççççççç<<<<<<<<<<<<<<<<<<<
