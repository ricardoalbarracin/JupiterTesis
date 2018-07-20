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
            
		},
		removeRow: function (e) {
            debugger
            var treeList = $("#treeList").data("kendoTreeList");
            var dataItem = treeList.dataItem($(e.currentTarget).closest("tr"));
            var id = dataItem.id;
            swal({
                title: "Atención",
                text: "¿Está seguro que desea eliminar el registro seleccionado?",
                type: "warning",
                showCancelButton: true,
                confirmButtonClass: "btn-danger",
                confirmButtonText: "Eliminar",
                cancelButtonText: "Cancelar",
                closeOnConfirm: false,
            },
                function (isConfirm) {
                    if (isConfirm) {
                        $.ajax({
                            method: "POST",
                            url: "/PARAM/Dependencias/DelDependencia/",
                            data: {
                                Id: id,
                                __RequestVerificationToken: window.RequestVerificationToken
                            }

                        }).done(function (result) {
                            if (result.Success) {
                                swal({
                                    title: "Correcto",
                                    text: result.Message,
                                    type: "success"
                                }, function () {
                                    $('#modal').modal('hide');
                                    $('#treeList').data('kendoTreeList').dataSource.read();
                                });

                            } else {
                                swal({
                                    title: "Error",
                                    text: result.Message,
                                    type: "error"
                                });
                            }
                        });
                        
                    } 
                });
            
            
        },
        
    };
}();