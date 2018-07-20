var InsDependencia = function () {
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
            $.validator.unobtrusive.parse($("#InsDependencia form"));
        },
        onSuccess: function (result) {
            if (result.Success) {
                swal({
                    title: "Correcto",
                    text: result.Message,
                    type: "success"
                }, function () {
                    $('#modal').modal('hide');
                    $('#treeList').data('kendoTreeList').dataSource.read();
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