/* Versión 1.0 */

function OnSuccess(result) {
    //alert('HTTP Status Code: ' + data.param1 + "  " + data.param2);
    if (result.EnableError) {
        // Setting.  
        alertDanger(result.ErrorMsg, 400);
    }
    //else if (result.EnableSuccess) {
    //    // Setting.  
    //    alert(result.SuccessMsg);
    //}
}

function OnFailure(data) {
    alert('HTTP Status Code: ' + data.param1);
    // sweetListadoAlertas(data.param1, data.param2);
}

/* Alerta Error */

function alertDanger(msg) {
    const Toast = Swal.mixin({
        toast: true,
        position: 'bottom',
        showConfirmButton: false,
        showCloseButton: true,
        background: '#EF3737',
        timer: 4000,
        customClass: 'error-toast'
    });

    Toast.fire({
        type: 'error',
        html: msg
    })
}

/* Alerta Exitosa */

function alertSuccess(msg) {
    const Toast = Swal.mixin({
        toast: true,
        position: 'bottom',
        showConfirmButton: false,
        showCloseButton: true,
        background: '#00CA18',
        timer: 4000,
        customClass: 'success-toast'
    });

    Toast.fire({
        type: 'success',
        html: msg
    })
}