var MESSAGE_TYPE_INFORMATION = "info";
var MESSAGE_TYPE_ERROR = "error";
var MESSAGE_TYPE_WARNING = "warning";
var MESSAGE_TYPE_CONFIRMATION = "confirmation";
var MESSAGE_TYPE_SUCCESS = "success";

/*function de Loading WebMarcacion*/
function WebShowLoading() {
    // body...
    $(".modal-webloading").css("display", "block");
    //$("#loading").modal({backdrop: 'static', keyboard: false});
}

function WebHideLoading() {
    // body...
    $(".modal-webloading").css("display", "none");
    //$("#loading").modal('hide');
}

function WebNotifyAsBlock(tipo, message, title) {
    if (MESSAGE_TYPE_INFORMATION == tipo) {
        toastr[tipo](message, title)
    } else if (MESSAGE_TYPE_ERROR == tipo) {
        toastr[tipo](message, title)
    } else if (MESSAGE_TYPE_WARNING == tipo) {
        toastr[tipo](message, title)
    } else if (MESSAGE_TYPE_SUCCESS == tipo) {
        toastr[tipo](message, title)
    }

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": 300,
        "hideDuration": 1000,
        "timeOut": 5000,
        "extendedTimeOut": 1000,
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
}