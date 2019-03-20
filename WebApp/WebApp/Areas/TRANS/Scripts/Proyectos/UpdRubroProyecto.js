var UpdRubroProyecto = function () {
    "use strict";
    return {
        // ---------------------------------
        //           Propiedades 
        // ---------------------------------
        actionTemplate: null,
        // ---------------------------------
        //           Metodos 
        // ---------------------------------

        init: function () {
            UpdRubroProyecto.handleValidator();
        },
        handleValidator: function () {
            
            $.validator.unobtrusive.parse($("#UpdRubroProyecto form"));     
            $("#UpdRubroProyecto form").validate().settings.ignore = [];
        },

        onSuccess: function (result) {
            $("#PresupuestosinAsignar").data("kendoNumericTextBox").value(result.Data)
            if (result.Success) {
                swal({
                    title: "Correcto",
                    text: result.Message,
                    type: "success"
                }, function () {
                    $('#modal2').modal('hide');
                    $("#gridRubros").data("kendoGrid").dataSource.read();
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