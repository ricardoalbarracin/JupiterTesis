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
			$("#Login form").find(":submit").button('loading');
		},

		onSuccess: function (result) {
			if (result.Success) {
				window.location.href = "/Dashboard/";
			} else {
				swal({
					title: "Error",
					text: result.Message,
					type: "error"
				});

				$("#Login form").find(":submit").button('reset');
			}
		},

		onFailure: function () {
			$("#Login form").find(":submit").button('reset');
		}
	}
}();


