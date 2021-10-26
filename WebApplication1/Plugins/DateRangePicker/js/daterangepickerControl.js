/* Versión 1.0 */

/* Date Range Picker Single */

$(function () {
    $('.dateinput').daterangepicker({
        opens: "left",
        singleDatePicker: true,
        showDropdowns: true,
        minYear: 2018,
        maxYear: 2020,
        startDate: moment().startOf('day'),
        //maxDate: moment().startOf('day'),
        locale: {
            format: 'DD-MM-YYYY',
            separator: ' — ',
            daysOfWeek: [
                "Do",
                "Lu",
                "Ma",
                "Mi",
                "Ju",
                "Vi",
                "Sa"
            ],
            monthNames: [
                "Enero",
                "Febrero",
                "Marzo",
                "Abril",
                "Mayo",
                "Junio",
                "Julio",
                "Agosto",
                "Septiembre",
                "Octubre",
                "Noviembre",
                "Diciembre"
            ],
            firstDay: 1
        }
    });
});

/* Date Range Picker Double */

//$(function () {
//    $('.dateinputDouble').daterangepicker({
//        opens: "left",
//        showDropdowns: false,
//        minYear: 2018,
//        maxYear: 2020,
//        startDate: moment().startOf('hour'),
//        endDate: moment().startOf('hour').add(56, 'hour'),
//        cancelClass: "btn-outline-light",
//        locale: {
//            format: 'DD-MM-YYYY',
//            separator: ' — ',
//            daysOfWeek: [
//                "Do",
//                "Lu",
//                "Ma",
//                "Mi",
//                "Ju",
//                "Vi",
//                "Sa"
//            ],
//            monthNames: [
//                "Enero",
//                "Febrero",
//                "Marzo",
//                "Abril",
//                "Mayo",
//                "Junio",
//                "Julio",
//                "Agosto",
//                "Septiembre",
//                "Octubre",
//                "Noviembre",
//                "Diciembre"
//            ],
//            firstDay: 1,
//            applyLabel: "Aplicar",
//            cancelLabel: "Cancelar"
//        }
//    });
//});

/* Date Range Picker Full */

//$(function () {
//    $('.dateinput').daterangepicker({
//        opens: "left",
//        showDropdowns: false,
//        minYear: 2018,
//        maxYear: 2020,
//        startDate: moment().startOf('hour'),
//        endDate: moment().startOf('hour').add(56, 'hour'),
//        cancelClass: "btn-outline-light",
//        ranges: {
//            'Hoy': [moment(), moment()],
//            'Ayer': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
//            'Últimos 7 días': [moment().subtract(6, 'days'), moment()],
//            'Últimos 30 días': [moment().subtract(29, 'days'), moment()],
//            'Mes Actual': [moment().startOf('month'), moment().endOf('month')],
//            'Mes Anterior': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
//        },
//        locale: {
//            format: 'DD-MM-YYYY',
//            separator: ' — ',
//            daysOfWeek: [
//                "Do",
//                "Lu",
//                "Ma",
//                "Mi",
//                "Ju",
//                "Vi",
//                "Sa"
//            ],
//            monthNames: [
//                "Enero",
//                "Febrero",
//                "Marzo",
//                "Abril",
//                "Mayo",
//                "Junio",
//                "Julio",
//                "Agosto",
//                "Septiembre",
//                "Octubre",
//                "Noviembre",
//                "Diciembre"
//            ],
//            firstDay: 1,
//            applyLabel: "Aplicar",
//            cancelLabel: "Cancelar",
//            customRangeLabel: "Personalizar"
//        }
//    });
//});