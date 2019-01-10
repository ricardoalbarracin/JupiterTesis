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

        onSuccess: function (result) {
            if (result.Success) {
                swal({
                    title: "Correcto",
                    text: result.Message,
                    type: "success"
                }, function () {
                    $('#modal').modal('hide');
                    $("#grid").data("kendoGrid").dataSource.read();
                });

            } else {
                swal({
                    title: "Error",
                    text: result.Message,
                    type: "error"
                });
            }

        }
    };
}();