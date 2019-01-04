var UpdSolicitudComision = function () {
    "use strict";
    return {
        // ---------------------------------
        //           Propiedades 
        // ---------------------------------
        init: function () {
            //this.handleTemplates();
        },

        filterDeptoOrigen: function () {
            var depto = $("#DepartamentoOrigen").val();
            return {
                padreId: parseFloat(depto)
            };
        },

        filterDeptoDestino: function () {
            var depto = $("#DepartamentoDestino").val();
            return {
                padreId: parseFloat(depto)
            };
        },
    };
}();