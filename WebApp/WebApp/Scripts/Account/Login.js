var Login = function () {
	"use strict"
	return {
		// ---------------------------------
		//           Propiedades 
		// ---------------------------------
		//..

		// ---------------------------------
		//           Metodos 
		// ---------------------------------
		init: function () {
			console.log("((init))");
			this.handleValidator();
		},

		handleValidator: function () {
			$.validator.unobtrusive.parse($("#Login form"));
		},

		onBegin: function () {
			
			$("#Login form").find(":submit").loading();
		},

		onSuccess: function (result) {
			if (result.Success) {
				window.location.href = "/";
			} else {
				swal({
					title: "Error",
					text: result.Message,
					type: "error"
				});

				$("#Login form").find(":submit").reset();
			}
		},

		onFailure: function () {
			$("#Login form").find(":submit").reset();
		}
	}
}();


