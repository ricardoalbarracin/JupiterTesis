var InsSubject = function () {
    "use strict";
    return {
        // ---------------------------------
        //           Propiedades 
        // ---------------------------------
        

        // ---------------------------------
        //           Metodos 
        // ---------------------------------

        init: function () {
            this.handleValidator();
        },

        handleValidator: function () {
            $.validator.unobtrusive.parse($("#InsSubject form"));
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