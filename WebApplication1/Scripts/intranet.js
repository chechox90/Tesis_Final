/* Versión 1.3 */

/* Get Current Year */
$("#year").text((new Date).getFullYear());

const player1 = new Plyr('#player1', {
    ratio: '16:9',
    settings: ['captions', 'quality', 'loop'],
    autopause: true
});
const player2 = new Plyr('#player2', {
    ratio: '16:9',
    settings: ['captions', 'quality', 'loop'],
    autopause: true
});
const player3 = new Plyr('#player3', {
    ratio: '16:9',
    settings: ['captions', 'quality', 'loop'],
    autopause: true
});
const player4 = new Plyr('#player4', {
    ratio: '16:9',
    settings: ['captions', 'quality', 'loop'],
    autopause: true
});

function pauseVid() {
    player1.pause();
    player2.pause();
    player3.pause();
    player4.pause();
}

$("#btnActividad").click(function () {
    $("#modalActivity").modal('show');

    tblActual = $("#tblActividadUsuario").DataTable({
        dom: "<'modal-header flex-column flex-md-row align-items-md-center'<'d-inline-flex align-items-center mb-2 mb-md-0'<'.btnClose mr-2'><'titleActivity'>><'d-inline-flex w-100 w-md-auto'fB>>" + "<'row'<'col-12'tr>>",
        processing: true,
        destroy: true,
        scrollX: true,
        scrollY: true,
        scrollCollapse: true,
        info: false,
        paging: false,
        lengthChange: false,
        language: {
            "url": "../Plugins/DataTables/js/Spanish.json"
        },
        order: [[4, "desc"]],
        drawCallback: function () {
            $("#tblActividadUsuario_wrapper").addClass("modal-content rounded-0 h-100vh");
            $(".titleActivity").html("<h4 class='card-title pr-2'>Registro de actividad</h4>");
            $("input[aria-controls='tblActividadUsuario']").removeClass("form-control-sm");
            $(".btnClose").html('<button type="button" class="close opacity-1 pl-0" data-dismiss="modal" aria-label="Close"><i class="far fa-arrow-left"></i></button>')
        },
        initComplete: function () {
            $(function () {
                $('[data-toggle="tooltip"]').tooltip({
                    placement: "top"
                });
            });
        },
        buttons: {
            dom: {
                container: {
                    className: 'dt-buttons'
                }
            },
            buttons: [
                { extend: 'copy', className: 'btn btn-light mr-2', text: '<i class="far fa-copy"></i>', titleAttr: 'Copiar', init: function (api, node, config) { $(node).removeClass('btn-secondary').attr('data-toggle', 'tooltip') } },
                {
                    extend: 'excel', className: 'btn btn-success', text: 'Descargar Excel', customize: function (xlsx) { var source = xlsx.xl['workbook.xml'].getElementsByTagName('sheet')[0]; source.setAttribute('name', 'Hoja1'); }, init: function (api, node, config) { $(node).removeClass('btn-secondary') }
                }

            ]
        },
        columnDefs: [
            { targets: [0, 1, 2, 3, 4], orderable: false }
        ],
        ajax: {
            url: "../PlanillasControl/GetActividadDelUsuario",
            type: "POST",
            dataType: "json",
        },
        columns: [
            {
                data: null, title: "Nombre usuario", name: "NOMBRE_USUARIO", render: function (full) {
                    return full.NOMBRE_USUARIO;
                }
            },
            { data: "DESCRIPCION_ACT", title: "Descripción actividad", name: "DESCRIPCION_ACT" },
            {
                data: null, title: "Detalle", name: "ID_SEGUIMIENTO", render:
                    function (full) {
                        if (full.ID_SEGUIMIENTO != null) {
                            return "ID Registro: " + full.ID_SEGUIMIENTO;
                        } else {
                            return "";
                        }

                    }
            },
            {
                data: null, title: "Interacción", name: "FECHA_MODIFICA", render:
                    function (full) {
                        {
                            return RetornasDiasAtras(full.FECHA_MODIFICA);
                        }
                    }
            },
            {
                data: null, title: "Fecha interacción", name: "FECHA_MODIFICA",
                render: function (full) {
                    return moment(full.FECHA_MODIFICA).format('DD/MM/YYYY HH:mm:ss');
                }
            }
        ]
    });
    $.fn.DataTable.ext.pager.numbers_length = 5;
});

function RetornasDiasAtras(fecha1) {

    var f1 = moment(fecha1).format('DD/MM/YYYY HH:mm:ss')
    var f2 = moment(new Date).format('DD/MM/YYYY HH:mm:ss')
    var conexion = "";

    var f1Dia = f1.substring(0, 2);
    var f1Hor = f1.substring(11, 13);
    var f1Min = f1.substring(14, 16);
    var f1Seg = f1.substring(17, 19);

    var f2Dia = f2.substring(0, 2);
    var f2Hor = f2.substring(11, 13);
    var f2Min = f2.substring(14, 16);
    var f2Seg = f2.substring(17, 19);

    //esto puede pasar solo en el cambio de hora que rige el territorio nacional de restar (-1 hora) 

    if (f1Hor > f2Hor) {
        if (f1Min > f2Min) {
            var seg = (f2Seg - f1Seg);
            conexion = seg + " segundos"

        } else {
            var min = (f2Min - f1Min);
            conexion = min + " minutos";
        }
    } else {
        var hor = (f2Hor - f1Hor);
        conexion = hor + " horas";
    }

    if (f1Dia == f2Dia) {
        if (f1Hor == f2Hor) {
            if (f1Min == f2Min) {
                var seg = (f2Seg - f1Seg);
                conexion = seg + " segundos"

            } else {
                var min = (f2Min - f1Min);
                conexion = min + " minutos";
            }
        } else {
            var hor = (f2Hor - f1Hor);
            conexion = hor + " horas";
        }
    } else {
        var dia = (f2Dia - f1Dia);
        conexion = dia + " días";
    }

    var mensajefinal = "Hace " + conexion + " atrás";
    return mensajefinal;
}

/* To Top Button */

var btn = $('#toTop');

$('.content').scroll(function () {
    if ($(this).scrollTop() > 50) {
        $('#toTop').css('transform', 'translateX(0)');
    } else {
        $('#toTop').css('transform', 'translateX(5rem)');
    }
});

btn.on('click', function (e) {
    e.preventDefault();
    $('.content').animate({ scrollTop: 0 }, '300');
});

function ShowModalPassword() {
    $("#modalPassword").modal({
        backdrop: 'static'
    });

    $("#modalPassword").modal('show');
}

function HideModalPassword() {
    $('#modalPassword').modal('hide');
}

function respuestaFormAjaxModalPassword(response) {
    if (response.status == 200) {

        if (response.responseJSON.Data.typeAlert != null && response.responseJSON.Data.typeAlert != "") {
            if (response.responseJSON.Data.typeAlert == "success") {

                HideModalPassword();
                setTimeout(function () {
                    alertSuccess(response.responseJSON.Data.message);
                }, 300);

                if (response.responseJSON.Data.url != "" && response.responseJSON.Data.url != undefined) {
                    setTimeout(function () {
                        window.location.href = response.responseJSON.Data.url;                        
                    }, 2000);
                }

            } else if (response.responseJSON.Data.typeAlert == "danger") {
                alertDanger(response.responseJSON.Data.message);
            }
        } else {
            $("#msg").html(response.responseJSON.Data.message);
        }

    }
}

function showLoaderButtonSubmitCambioPassword() {
    $(".btn-submit-cambio-password").attr("disabled", "disabled");
    $(".btn-submit-cambio-password").html("Guardando <span class='spinner-border spinner-border-sm' role='status' aria-hidden='true'></span>");
}

function hideLoaderButtonSubmitCambioPassword() {
    $(".btn-submit-cambio-password").removeAttr("disabled");
    $(".btn-submit-cambio-password").html("Guardar");
}

$('#selectTabs').on('change', function (e) {
    $('#pills-tab-ida li a').eq($(this).val()).tab('show');
});

/* Modal Over Modal */

//var modal_lv = 0;
//$('body').on('show.bs.modal', function (e) {
//    if (modal_lv > 0)
//        $(e.target).css('zIndex', 1051 + modal_lv);
//    modal_lv++;
//}).on('hidden.bs.modal', function () {
//    if (modal_lv > 0)
//        modal_lv--;
//});

/* Tooltips */

$(function () {
    $('[data-toggle="tooltip"]').tooltip({
        container: 'body',
        trigger: 'hover',
        boundary: 'window'
    });
});

// Hide Tooltip on Click nav-link

$(".nav-link").click(function () {
    $('[data-toggle="tooltip"]').tooltip('hide');
});

/* Popovers */

$(function () {
    $('[data-toggle="popover"]').popover({
        container: 'body',
        trigger: 'hover',
        boundary: 'window'
    });
});

/* Fullscreen */

// Get into full screen
function GoInFullscreen(element) {
    if (element.requestFullscreen)
        element.requestFullscreen();
    else if (element.mozRequestFullScreen)
        element.mozRequestFullScreen();
    else if (element.webkitRequestFullscreen)
        element.webkitRequestFullscreen();
    else if (element.msRequestFullscreen)
        element.msRequestFullscreen();
}

// Get out of full screen
function GoOutFullscreen() {
    if (document.exitFullscreen)
        document.exitFullscreen();
    else if (document.mozCancelFullScreen)
        document.mozCancelFullScreen();
    else if (document.webkitExitFullscreen)
        document.webkitExitFullscreen();
    else if (document.msExitFullscreen)
        document.msExitFullscreen();
}

// Is currently in full screen or not
function IsFullScreenCurrently() {
    var full_screen_element = document.fullscreenElement || document.webkitFullscreenElement || document.mozFullScreenElement || document.msFullscreenElement || null;

    // If no element is in full-screen
    if (full_screen_element === null)
        return false;
    else
        return true;
}

// On Click Maximize - Minimize

$("#togglerScreen").on('click', function () {
    if (IsFullScreenCurrently())
        GoOutFullscreen();
    else
        GoInFullscreen($("#resize-content").get(0));
});

$(document).on('fullscreenchange webkitfullscreenchange mozfullscreenchange MSFullscreenChange', function () {
    if (IsFullScreenCurrently()) {
        $("#togglerScreen i").removeClass('fa-expand');
        $("#togglerScreen i").addClass('fa-compress');
    }
    else {
        $("#togglerScreen i").removeClass('fa-compress');
        $("#togglerScreen i").addClass('fa-expand');
    }
});

/* Sidebar Toggler */

$(".toggler").click(function () {
    $(".sidebar").toggleClass("toggled");
    $(".content").toggleClass("content-toggled");
    // Toggler Sidebar Links & Rotate Button Toggler
    if ($('.sidebar').hasClass('toggled')) {
        $(".sidebar .nav-link").next().collapse("hide");
        setTimeout(function () {
            $(".toggler i").addClass("rotated");
        }, 400);
    } else {
        $('.sidebar .nav-link-active').next().collapse("show");
        $(".toggler i").removeClass("rotated");
    }
});

// jquery toggle whole attribute
$.fn.toggleAttr = function (attr, val) {
    var test = $(this).attr(attr);
    if (test) {
        // if attrib exists with ANY value, still remove it
        $(this).removeAttr(attr);
    } else {
        $(this).attr(attr, val);
    }
    return this;
};
// jquery toggle just the attribute value
$.fn.toggleAttrVal = function (attr, val1, val2) {
    var test = $(this).attr(attr);
    if (test === val1) {
        $(this).attr(attr, val2);
        return this;
    }
    if (test === val2) {
        $(this).attr(attr, val1);
        return this;
    }
    // default to val1 if neither
    $(this).attr(attr, val1);
    return this;
};

// Sidebar Toggled Hover Links

$('.sidebar .nav-item').hover(function () { // :not(:first-of-type) Agregar al tener collapse

    var collapseHover = $(".sidebar").hasClass("toggled");

    if ($(window).width() >= 768 && collapseHover === true) {

        //$(this).children().toggleAttr('data-toggle', 'collapse'); // Agregar al tener Collapse

        $(this).toggleClass('hover-open');

    }

});

/* Dropdown Slide */

$('.dropdown').on('show.bs.dropdown', function (e) {
    $(this).find('.dropdown-menu').first().stop(true, true).slideDown(200);
});

$('.dropdown').on('hide.bs.dropdown', function (e) {
    $(this).find('.dropdown-menu').first().stop(true, true).slideUp(200);
});

/* Carga Archivos */

function fileAdded(val) {
    $(".files input").css("color", "#00CA18");
    $(".files").addClass('added').attr('data-content', 'Archivo Añadido');
}

// Selected Dropdown

//$(".dropdown-menu a").click(function(){
//  var selText = $(this).text();
//  $(this).parents('.dropdown').find(".select").html(selText+" <i class='fas fa-angle-down'></i>");
//});

//$(".dropdown-menu a").click(function(){
//  var selText = $(this).text();
//  $(this).parents('.dropdown').find("#modulo").html("<span class='text-link'>"+selText+" <i class='fas fa-angle-down'></i>"+"</span>");
//});

/* Datepicker */

$(document).ready(function () {
    $(function () {
        $('.datepicker').datepicker({
            //showAnim: 'blind',
            duration: 'fast',
            firstDay: 1,
            dateFormat: 'dd-mm-yy',
            prevText: '',
            nextText: '',
            monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
            monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
            dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sabado'],
            dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'],
            dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab'],
        });
    });
});

/* Chart */

//var ctx = document.getElementById("chartBar");
//var myChart = new Chart(ctx, {
//  type: 'bar',
//  data: {
//    labels: ["Lourdes", "Lo Marcoleta"],
//    datasets: [{
//      label: 'Ejemplo',
//      data: [12, 19],
//      backgroundColor: [
//        '#00CA18',
//        'rgba(0, 202, 24, .2)'
//      ],
//      borderWidth: 0
//    }]
//  },
//  options: {
//    legend: {
//      position: 'bottom',
//      labels: {
//        fontFamily: "'Roboto', sans-serif",
//        fontColor: '#6E7B8F',
//        fontSize: 12,
//        padding: 30,
//        usePointStyle: true
//      }
//    },
//    scales: {
//      xAxes: [{
//        gridLines: {
//          display: false,
//          drawBorder: false,
//        },
//        ticks: {
//          fontFamily: "'Roboto', sans-serif",
//          fontColor: '#000'
//        }
//      }],
//      yAxes: [{
//        gridLines: {
//          color: '#EBF0F6',
//          zeroLineColor: '#EBF0F6',
//          borderDash: [6]
//          display: false,
//          drawBorder: false,
//        },
//        ticks: {
//          beginAtZero: true,
//          fontFamily: "'Roboto', sans-serif",
//          fontColor: '#000',
//          fontSize: 14
//        }
//      }]
//    },
//    tooltips: {
//      xPadding: 10,
//      yPadding: 10,
//      titleFontColor: '#FFF',
//      bodyFontColor: '#FFF',
//      footerFontColor: '#FFF',
//      backgroundColor: '#000'
//    }
//  }
//});

//var ctx = document.getElementById("chartDoughnut");
//var myChart = new Chart(ctx, {
//  type: 'doughnut',
//  data: {
//    labels: ["Lourdes", "Lo Marcoleta"],
//    datasets: [{
//      label: '# of Votes',
//      data: [12, 19],
//      backgroundColor: [
//        '#00CA18',
//        'rgba(0, 202, 24, .2)'
//      ],
//      borderWidth: 0
//    }]
//  },
//  options: {
//    legend: {
//      position: 'bottom',
//      labels: {
//        fontFamily: "'Roboto', sans-serif",
//        fontColor: '#6E7B8F',
//        fontSize: 12,
//        padding: 30,
//        usePointStyle: true
//      }
//    },
//    tooltips: {
//      xPadding: 10,
//      yPadding: 10,
//      titleFontColor: '#FFF',
//      bodyFontColor: '#FFF',
//      footerFontColor: '#FFF',
//      backgroundColor: '#000'
//    }
//  }
//});

//new Chart(document.getElementById("chartLine"), {
//  type: 'line',
//  data: {
//    labels: [1500, 1600, 1700, 1750, 1800, 1850, 1900, 1950, 1999, 2050],
//    datasets: [{
//      data: [2000, 3000, 4000, 4500, 2000, 3500, 2700, 2500, 3200, 5000],
//      label: "Lourdes",
//      pointRadius: 5,
//      pointBorderWidth: 3,
//      backgroundColor: "rgba(0, 202, 24, .2)",
//      borderColor: "rgba(0, 202, 24, .2)",
//      pointBackgroundColor: "rgba(0, 202, 24, .2)",
//      pointBorderColor: "#FFF",
//      pointHoverBorderColor: "rgba(0, 202, 24, .2)",
//      fill: false
//    }, {
//      data: [5000, 4500, 5000, 6000, 3000, 5500, 3700, 3000, 4200, 6000],
//      label: "Lo Marcoleta",
//      pointRadius:5,
//      pointBorderWidth: 3,
//      backgroundColor: "#00CA18",
//      borderColor: "#00CA18",
//      pointBackgroundColor: "#00CA18",
//      pointBorderColor: "#FFF",
//      pointHoverBorderColor: "rgba(0, 202, 24, .2)",
//      fill: false
//    }]
//  },
//  options: {
//    legend: {
//      position: 'bottom',
//      labels: {
//        fontFamily: "'Roboto', sans-serif",
//        fontColor: '#6E7B8F',
//        fontSize: 12,
//        padding: 30,
//        usePointStyle: true
//      }
//    },
//    scales: {
//      xAxes: [{
//        gridLines: {
//          display: false,
//          drawBorder: false,
//        },
//        ticks: {
//          fontFamily: "'Roboto', sans-serif",
//          fontColor: '#000'
//        }
//      }],
//      yAxes: [{
//        gridLines: {
//          color: '#EBF0F6',
//          zeroLineColor: '#EBF0F6',
//          borderDash: [6]
//          display: false,
//          drawBorder: false,
//        },
//        ticks: {
//          beginAtZero: true,
//          fontFamily: "'Roboto', sans-serif",
//          fontColor: '#000',
//          fontSize: 14
//        }
//      }]
//    },
//    tooltips: {
//      xPadding: 10,
//      yPadding: 10,
//      titleFontColor: '#FFF',
//      bodyFontColor: '#FFF',
//      footerFontColor: '#FFF',
//      backgroundColor: '#000'
//    }
//  }
//});