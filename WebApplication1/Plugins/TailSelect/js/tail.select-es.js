/*
 |  tail.select - The vanilla solution to make your HTML select fields AWESOME!
 |  @file       ./langs/tail.select-es.js
 |  @author     SamBrishes <sam@pytes.net>
 |  @version    0.5.16 - Beta
 |
 |  @website    https://github.com/pytesNET/tail.select
 |  @license    X11 / MIT License
 |  @copyright  Copyright © 2014 - 2019 SamBrishes, pytesNET <info@pytes.net>
 */
/*
 |  Translator:     elPesecillo - (https://github.com/elPesecillo)
 |  GitHub:         https://github.com/pytesNET/tail.select/issues/41
 */
;(function(factory){
   if(typeof(define) == "function" && define.amd){
       define(function(){
           return function(select){ factory(select); };
       });
   } else {
       if(typeof(window.tail) != "undefined" && window.tail.select){
           factory(window.tail.select);
       }
   }
}(function(select){
    select.strings.register("es", {
        all: "Todos",
        none: "Ninguno",
        empty: "No hay opciones disponibles",
        emptySearch: "No encontrado",
        //limit: "No puedes seleccionar mas opciones",
        placeholder: "Seleccionar",
        placeholderMulti: "Selecciona hasta: límite de opciones...",
        search: "\uf002  Buscar",
        disabled: "Este campo esta deshabilitado"
    });
    return select;
}));
