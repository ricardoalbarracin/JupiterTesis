var UpdPermisosUsuario = function () {
	"use strict";
	return {
		// ---------------------------------
		//           Propiedades 
		// ---------------------------------
		form: null,
		model: "",
		sectionId: "",
		dataForm: null,
		validator: null,
		container: null,
		actionTemplate: null,

		// ---------------------------------
		//           Metodos 
		// ---------------------------------


		init: function (container, modelType, sectionId) {

			this.sectionId = sectionId;
			this.model = modelType;
			this.container = container;
			this.form = $("#" + sectionId).find("form");
			Utils.addSectionToContainer(this.container, this.sectionId, this);
			this.handleSubmitForm();
			this.handleValidator();
			$("#UpdPermisosUsuario #GridPermisos").data("operaciones", [])
		},

		eliminarPermiso: function (e, id) {
			Utils.removeGridDataItem(e, "#UpdPermisosUsuario #GridPermisos");
			UpdPermisosUsuario.agregarOperacion(id, 0);
		},
		agregarPermiso: function (e) {
			
			e.preventDefault();
			var grid = $("#gridPermisosSistema").data("kendoGrid");

			var permiso = grid.dataItem($(e.currentTarget).closest("tr"));

			var permisoExist = $("#UpdPermisosUsuario #GridPermisos").data("kendoGrid").dataSource.data().find(function (element) {
				return element.Id == permiso.Id;
			});
			if (permisoExist) {
				swal({
					title: "Error",
					text: "El usuario ya tiene ese rol.",
					type: "error"
				});
				return;
			}
			Utils.addGridDataItem("#UpdPermisosUsuario #GridPermisos", permiso)
			UpdPermisosUsuario.agregarOperacion(permiso.Id, 1);
		},

		agregarOperacion: function (id, activo) {
			var oldActivo = 1;
			if (activo)
				oldActivo = 0;
			var operaciones = $("#UpdPermisosUsuario #GridPermisos").data("operaciones");
			var operacion = operaciones.find(function (element) {
				return element.Id == id && element.Activo == oldActivo;
			});

			if (!operacion) {
				operaciones.push({
					Id: id,
					Activo: activo
				});
			}
			else {
				operaciones.splice(operaciones.indexOf(operacion), 1);
			}
		},

		handleSubmitForm: function () {
			var _this = this;
			this.form.submit(function (e) {
				_this.dataForm = $(this).serializeObject();
				_this.dataForm["Permisos"] = $("#UpdPermisosUsuario #GridPermisos").data("operaciones");
				e.preventDefault();
			});
		},

		handleValidator: function () {
			this.form.validate();
		}
	}
}();