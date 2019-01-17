var InsFacturas = function () {
    "use strict";
    return {

        init: function () {
            this.handleValidator();
        },

        handleValidator: function () {
            $.validator.unobtrusive.parse($("#InsFacturas form"));
        },
       
       

    };
}();