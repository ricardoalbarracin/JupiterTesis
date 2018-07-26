var UpdRubrosProyecto = function () {
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
			$("#UpdRubrosProyecto #GridRubros").data("operaciones", [])
		},

		eliminarPermiso: function (e, id) {
			Utils.removeGridDataItem(e, "#UpdRubrosProyecto #GridRubros");
			UpdRubrosProyecto.agregarOperacion(id, 0);
		},

		agregarPermiso: function (e) {
			
			e.preventDefault();
			var grid = $("#gridRubrosSistema").data("kendoGrid");

			var permiso = grid.dataItem($(e.currentTarget).closest("tr"));

			var permisoExist = $("#UpdRubrosProyecto #GridRubros").data("kendoGrid").dataSource.data().find(function (element) {
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
			Utils.addGridDataItem("#UpdRubrosProyecto #GridRubros", permiso)
			UpdRubrosProyecto.agregarOperacion(permiso.Id, 1);
			Utils.toast("success", "Se ha agregado el permiso correctamente.");
		},

		agregarOperacion: function (id, activo) {
			var oldActivo = 1;
			if (activo)
				oldActivo = 0;
			var operaciones = $("#UpdRubrosProyecto #GridRubros").data("operaciones");
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

		Validate: function () {
			return UpdRubrosProyecto.form.valid();
		},
		handleSubmitForm: function () {
			var _this = this;
			this.form.submit(function (e) {
				_this.dataForm = $(this).serializeObject();
				_this.dataForm["ListRubros"] = $("#UpdRubrosProyecto #GridRubros").data("operaciones");
				e.preventDefault();
			});
		},

		handleValidator: function () {
			$.validator.unobtrusive.parse($("#UpdRubrosProyecto form"));
		}
	}
}();