var DesembolsoValor = function () {
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
            this.actionTemplate = kendo.template($('#DesembolsoValor #actionTemplate').html());
        },
       

        updateDesembolso: function (id) {
            var dataF =
            {
                id: id
            }
            $.ajax({
                type: "POST",
                url: "/TRANS/SolicitudComision/UpdDesembolsoComision",
                data: dataF,
                dataType: "json",
                success: function (result) {
                    if (result.Success) {
                        swal({
                            title: "Correcto",
                            text: result.Message,
                            type: "success"
                        }, function () {
                           
                            $("#grid").data("kendoGrid").dataSource.read();
                        });

                    } else {
                        swal({
                            title: "Error",
                            text: result.Message,
                            type: "error"
                        });
                    }

                }
            }); 
        }
    };
}();