/* Versión 1.0 */

function ShowModalNormal() {
    $("#modalNormal").modal({
        backdrop: 'static'
    });

    $("#modalNormal").modal('show');
}

function HideModalNormal() {
    $('#modalNormal').modal('hide');
}

function showLoaderButtonSubmit() {
    $(".btn-submit").attr("disabled", "disabled");
    $(".btn-submit").html("Guardando <span class='spinner-border spinner-border-sm' role='status' aria-hidden='true'></span>");
}

function hideLoaderButtonSubmit() {
    $(".btn-submit").removeAttr("disabled");
    $(".btn-submit").html("Guardar");
}