// Versión 1.2

function respuestaLogin(response) {
    if (response.status == 200) {
        if (response.responseJSON.Data.message == "ingresa") {
            $(".loading-content").css("display", "block");
        } else if (response.responseJSON.Data.message == "clavegenerica") {
            $("#Clave").val("");
            $("#RolRestablece").val($("#Rol").val());
            $("#modalPassword").modal("show");
        } else {
            $("#response").html(response.responseJSON.Data.message);
        }

        if (response.responseJSON.Data.url != "") {
            setTimeout(function () {
                window.location.href = response.responseJSON.Data.url;
            }, response.responseJSON.Data.seccondsRerfresh);
        }
    }

    setTimeout(function () {
        $("#response").html("");
    }, 4000);
}

$("#Rol").keypress(function (e) {
    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        return false;
    }
});


function showLoaderButtonSubmitLogin() {
    $(".btn-login").attr("disabled", "disabled");
    $(".btn-login").html("Verificando <span class='spinner-border spinner-border-sm' role='status' aria-hidden='true'></span>");
}

function hideLoaderButtonSubmitLogin() {
    $(".btn-login").removeAttr("disabled");
    $(".btn-login").html("Ingresar");
}

/* Caps Lock */
document.getElementById("Clave").addEventListener('keydown', function (event) {
    var caps = event.getModifierState && event.getModifierState('CapsLock');
    if (caps) {
        $('#capsLock').show().html("<span class='field-validation-error text-orange'>Bloq Mayús activado</span>");
    } else {
        $("#capsLock").hide();
    }
});