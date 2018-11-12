var InsPerson = function () {
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
		
		},

		Validate: function () {
            return InsPerson.form.valid();
		},
		handleSubmitForm: function () {
			var _this = this;
			this.form.submit(function (e) {
				_this.dataForm = $(this).serializeObject();
				e.preventDefault();
			});
		},

		handleValidator: function () {
            $.validator.unobtrusive.parse($("#InsPerson form"));
		}
	}
}();