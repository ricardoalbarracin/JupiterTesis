var InsPersona = function () {
    "use strict";
    return {
        // ---------------------------------
        //           Propiedades 
        // ---------------------------------
        

        // ---------------------------------
        //           Metodos 
        // ---------------------------------

        init: function () {
            
        },

        success: function (result) {
            if (result.Success) {
                swal({
                    title: "Correcto",
                    text: result.Message,
                    type: "success"
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