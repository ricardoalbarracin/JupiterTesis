var InsStudent = function () {
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

		handleSubmitForm: function () {
			
			var _this = this;
			this.form.submit(function (e) {
				_this.dataForm = $(this).serializeObject();
				e.preventDefault();
			});
		},

		Validate: function () {
            return InsStudent.form.valid();
		},

		handleValidator: function () {
			$.validator.unobtrusive.parse($("#InsUsuario form"));
		},

	}
}();