var ActualizarUsuario = function () {
	"use strict";
	return {
		// ---------------------------------
		//           Propiedades 
		// ---------------------------------
		facade: {},

		// ---------------------------------
		//           Metodos 
		// ---------------------------------

		init: function () {
		},

		// Finish
		onFinish_Begin: function (jqXHR, settings) {
			
			var sectionsToValidate = [
				"UpdUsuario",
				"UpdRolesUsuario",
				"UpdPermisosUsuario"
			];

			// Validacion de secciones
			var isValidSections = Utils.isValidSections(ActualizarUsuario.facade, sectionsToValidate);
			if (!isValidSections) {
				return false;
			}

			// Salida
			var dataSections = Utils.getDataSections(ActualizarUsuario.facade, sectionsToValidate);
			settings.data = jQuery.param({
				dataSections: dataSections
			});
		},

		onFinish_Success: function (result) {
			
		}

		
	};
}();