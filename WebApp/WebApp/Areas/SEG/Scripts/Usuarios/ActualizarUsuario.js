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
			$('#wizard').wizard({
				selected: 0,
				theme: 'default',
				transitionEffect: 'fade',
				showStepURLhash: false,
				toolbarSettings: {
					toolbarButtonPosition: 'none',
					showPreviousButton: "false",
					showNextButton: "false"
				}
			});
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