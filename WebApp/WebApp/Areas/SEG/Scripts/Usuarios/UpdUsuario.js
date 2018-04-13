var UpdUsuario = function () {
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

		handleValidator: function () {
			this.form.validate();
        },

        resetPassword: function () {
            var data = $('#UpdUsuario').find("form").serializeArray();
            swal({
                title: "Alerta",
                text: "¿Esta seguro de restablecer la contraseña?",
                type: "warning",
                showCancelButton: true,
                confirmButtonClass: "btn-danger",
                confirmButtonText: "Eliminar"
            }, function () {
                $.ajax({
                    method: "POST",
                    url: "/SEG/Usuarios/ResetPassword/",
                    data: data,
                })
                    .done(function (result) {
                        debugger
                        if (result.Success) {
                            swal("Correcto", result.Message, "success");
                            grid.dataSource.read();

                        } else {
                            swal({
                                title: "Error",
                                text: result.Message,
                                type: "error"
                            });
                        }
                    });
            });
        }
	}
}();