var RubrosProyecto = function () {
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
            this.handleTemplates();
		},
        handleTemplates: function () {
            this.actionTemplate = kendo.template($('#RubrosProyecto #actionTemplate').html());
        },
    };
}();