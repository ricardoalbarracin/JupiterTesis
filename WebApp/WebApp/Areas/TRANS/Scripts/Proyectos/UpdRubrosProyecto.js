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

	    eliminarRubro: function (e) {
            e.preventDefault();
            var grid = $("#UpdRubrosProyecto #GridRubros").data("kendoGrid");
            var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
            dataItem.OperationType = operationTypeEnum.Delete;
            UpdRubrosProyecto.agregarOperacion(dataItem);
            grid.dataSource.remove(dataItem);
        },

        actualizarRubro: function (e) {
            e.preventDefault();
            var grid = $("#GridRubros").data("kendoGrid");

            var rubro = grid.dataItem($(e.currentTarget).closest("tr"));
            var data= 
            {
                Id: rubro.Id,
                RubroId: rubro.RubroId,
                ProyectoId: rubro.ProyectoId,
                Valor: rubro.Valor,
                Saldo: rubro.Saldo
            };

            Utils.showModalBs("/TRANS/Proyectos/UpdRubroProyecto/", "",data,"modal2");
        },

        agregarOperacion: function (item) {
            var operaciones = $("#UpdRubrosProyecto #GridRubros").data("operaciones");
            var operacion = operaciones.find(function (element) {
                return element.RubroId == item.RubroId ;
            });

            if(item.OperationType == operationTypeEnum.Delete)

            {
                if (!operacion)
                    operaciones.push(item);
                else
                {
                    if(operacion.Id >0)
                    {
                        operaciones.splice(operaciones.indexOf(operacion), item);
                    }
                    else
                    {
                        operaciones.splice(operaciones.indexOf(operacion), 1);
                    }
                }
            }
            else
            {
                if (operacion) {
                    if(item.OperationType == operationTypeEnum.Insert)
                    {
                        item.Id = operacion.Id;
                        item.OperationType = operationTypeEnum.Update;
                    }
                    operaciones.splice(operaciones.indexOf(operacion), item);
                }
                else
                {
                    operaciones.push(item);
                }
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