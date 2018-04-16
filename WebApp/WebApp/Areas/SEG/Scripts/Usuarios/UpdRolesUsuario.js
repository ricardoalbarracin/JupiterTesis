

var UpdRolesUsuario = function () {
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
			$("#UpdRolesUsuario #GridRoles").data("operaciones", [])
		},

		eliminarRole: function (e, id) {
			Utils.removeGridDataItem(e, "#UpdRolesUsuario #GridRoles");
			UpdRolesUsuario.agregarOperacion(id, 0);
		},
		agregarRole: function (e) {
			
			e.preventDefault();
			var grid = $("#gridRolesSistema").data("kendoGrid");
			
			var role = grid.dataItem($(e.currentTarget).closest("tr"));

			var roleExist = $("#UpdRolesUsuario #GridRoles").data("kendoGrid").dataSource.data().find(function (element) {
				return element.Id == role.Id;
			});
			if (roleExist) {
				swal({
					title: "Error",
					text: "El usuario ya tiene ese rol.",
					type: "error"
				});
				return;
			}
			Utils.addGridDataItem("#UpdRolesUsuario #GridRoles", role)
			UpdRolesUsuario.agregarOperacion(role.Id, 1);
		},

		agregarOperacion: function (id, activo) {
			var oldActivo = 1;
			if (activo)
				oldActivo = 0;
			var operaciones = $("#UpdRolesUsuario #GridRoles").data("operaciones");
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
				_this.dataForm["ListRoles"] = $("#UpdRolesUsuario #GridRoles").data("operaciones");
				e.preventDefault();
			});
		},

		handleValidator: function () {
			this.form.validate();
		}
	}
}();