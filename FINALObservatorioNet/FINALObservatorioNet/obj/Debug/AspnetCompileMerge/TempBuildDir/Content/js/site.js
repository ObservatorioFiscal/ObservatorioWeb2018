function mostrarfeed2() {
    $("#comentarioF").removeClass("hidden");
    $("#linkform2").addClass("hidden");
    }
function formulario1(val) {
    var IDVISTA = $("#IdVista").val();
    $.ajax({
    cache: false,
            async: false,
            type: 'POST',
            data: {
                IDVISTA: $("#IdVista").val(),
                    VAL: val
            },
            url: '../../ajaxGraficos/formulario1',
            dataType: 'json',
            success: function (data, textStatus, jqXHR) {
                if (data == true) {
                    if (val == 1) {
                        $("#retro1").addClass("hidden");
                        $("#retro3").removeClass("hidden");
                    }
                    else {
                        $("#retro1").addClass("hidden");
                        $("#retro2").removeClass("hidden");
                    }

                }
                else {
                        $("#retro1").addClass("hidden");
                        $("#retro4").removeClass("hidden");
                }
            },
            error: function (ID) {
            }
    });
}
function formulario2(val) {
    var IDVISTA = $("#IdVista").val();
    $.ajax({
cache: false,
        async: false,
        type: 'POST',
        data: {
IDVISTA: $("#IdVista").val(),
            VAL: val
        },
        url: '../../ajaxGraficos/formulario2',
        dataType: 'json',
        success: function (data, textStatus, jqXHR) {
            if (data == true) {
                if (val == 4) {
$("#retro2").addClass("hidden");
$("#retro5").removeClass("hidden");
                }
                else {
$("#retro2").addClass("hidden");
$("#retro6").removeClass("hidden");
                }
            }
            else {
$("#retro2").addClass("hidden");
$("#retro4").removeClass("hidden");
            }
        },
        error: function (ID) {

}
});
}
function formulario4() {
    var IDVISTA = $("#IdVista").val();
    var SUGERENCIA = $("#notegustofed").val();
    $.ajax({
cache: false,
        async: false,
        type: 'POST',
        data: {
IDVISTA: $("#IdVista").val(),
            SUG: SUGERENCIA
        },
        url: '../../ajaxGraficos/formulario4',
        dataType: 'json',
        success: function (data, textStatus, jqXHR) {
            if (data == true) {
$("#retro5").addClass("hidden");
$("#retro6").removeClass("hidden");
            }
            else {
$("#retro5").addClass("hidden");
$("#retro4").removeClass("hidden");
            }
        },
        error: function (ID) {

}
});
}
function formulario3() {
    var IDVISTA = $("#IdVista").val();
    var SUGERENCIA = $("#sugerenciafed").val();
    $.ajax({
cache: false,
        async: false,
        type: 'POST',
        data: {
IDVISTA: $("#IdVista").val(),
            SUG: SUGERENCIA
        },
        url: '../../ajaxGraficos/formulario3',
        dataType: 'json',
        success: function (data, textStatus, jqXHR) {
            if (data == true) {
$("#unretro1").addClass("hidden");
$("#unretro2").removeClass("hidden");
            }
            else {
$("#unretro1").addClass("hidden");
$("#unretro3").removeClass("hidden");
            }
        },
        error: function (ID) {

}
});
}
function redesclick(aux) {
    var url = "";
    switch (aux) {
        case 'twitter':
            url = "https://twitter.com/?status=";
            url = url + "Me gusta esta página " + window.location.href;
            break;
        case 'facebook':
            url = "https://www.facebook.com/sharer/sharer.php?u=";
            url = url + window.location.href;
            break;
        case 'linkedin':
            url = "http://www.linkedin.com/shareArticle?url=";
            url = url + window.location.href;
            break;
    }
    windowObjectReference = window.open(
        url,
        "DescriptiveWindowName",
        "resizable,scrollbars,status"
    );
}
