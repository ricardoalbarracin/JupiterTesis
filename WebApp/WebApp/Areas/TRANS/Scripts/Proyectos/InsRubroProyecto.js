var InsRubroProyecto = function () {
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
            InsRubroProyecto.handleValidator();
            
		},
        handleValidator: function () {
            
            $.validator.unobtrusive.parse($("#InsRubroProyecto form"));     
            $("#InsRubroProyecto form").validate().settings.ignore = [];
        },
        onSuccess: function (result) {
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