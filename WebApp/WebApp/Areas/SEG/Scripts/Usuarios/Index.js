var Index = function () {
    "use strict";
    return {
        // ---------------------------------
        //           Propiedades 
        // ---------------------------------
        actionTemplate: null,

        // ---------------------------------
        //           Metodos 
        // ---------------------------------

        init: function () {
            this.handleTemplates();
        },

		handleTemplates: function () {
            this.actionTemplate = kendo.template($('#Index #actionTemplate').html());
        },

        eliminar: function (e) {
            e.preventDefault();
            var grid = $("#Index").find("#grid").data("kendoGrid");
            var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
            var id = dataItem.Id;

            swal({
                title: "Alerta",
                text: "¿Esta seguro de eliminar el registro seleccionado?",
                type: "warning",
                showCancelButton: true,
				confirmButtonClass: "btn-danger",
                confirmButtonText: "Eliminar"
            }, function () {
                $.ajax({
                    method: "POST",
                    url: "/ADMIN/ActasProduccion/EliminarActaProduccion/" + id
                })
                .done(function (result) {
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
    };
}();