﻿var InsRolesUsuario = function () {
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
			$("#InsRolesUsuario #GridRoles").data("operaciones", [])
		},

		eliminarRole: function (e, id) {
			Utils.removeGridDataItem(e, "#InsRolesUsuario #GridRoles");
			InsRolesUsuario.agregarOperacion(id, 0);
		},
		agregarRole: function (e) {
			
			e.preventDefault();
			var grid = $("#gridRolesSistema").data("kendoGrid");
			
			var role = grid.dataItem($(e.currentTarget).closest("tr"));

			var roleExist = $("#InsRolesUsuario #GridRoles").data("kendoGrid").dataSource.data().find(function (element) {
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
			Utils.addGridDataItem("#InsRolesUsuario #GridRoles", role)
			InsRolesUsuario.agregarOperacion(role.Id, 1);
			Utils.toast("success", "Se ha agregado el role correctamente.");
		},

		agregarOperacion: function (id, activo) {
			var oldActive = 1;
			if (activo)
				oldActive = 0;
			var operaciones = $("#InsRolesUsuario #GridRoles").data("operaciones");
			var operacion = operaciones.find(function (element) {
				return element.Id == id && element.Active == oldActive;
			});

			if (!operacion) {
				operaciones.push({
					Id: id,
					Active: activo
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
				_this.dataForm["ListRoles"] = $("#InsRolesUsuario #GridRoles").data("operaciones");
				e.preventDefault();
			});
		},

		Validate: function () {
			return InsRolesUsuario.form.valid();
		},

		handleValidator: function () {
			$.validator.unobtrusive.parse($("#InsRolesUsuario form"));
			
		}
	}
}();