var CrearUsuario = function () {
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
				"InsUsuario",
				"InsRolesUsuario",
				"InsPermisosUsuario"
			];
			// Validacion de secciones
			var isValidSections = Utils.isValidSections(CrearUsuario.facade, sectionsToValidate);
			if (!isValidSections) {
				return false;
			}

			// Salida
			var dataSections = Utils.getDataSections(CrearUsuario.facade, sectionsToValidate);
			settings.data = jQuery.param({
				dataSections: dataSections
			});
		},

		onFinish_Success: function (result) {
			if (result.Success) {
				swal({
					title: "Correcto",
					text: result.Message,
					type: "success"
				}, function () {
					window.location.href = "/SEG/Usuarios/";
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