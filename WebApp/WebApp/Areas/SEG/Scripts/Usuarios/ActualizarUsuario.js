var ActualizarUsuario = function () {
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
			this.actionTemplate = kendo.template($('#ActualizarUsuario #actionTemplate').html());
		},

		
	};
}();