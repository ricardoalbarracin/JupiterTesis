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
        removeRow: function () {
            debugger
            var id= $('#treeview').data('kendoTreeView').dataItem($('#treeview').data('kendoTreeView').select()).id;
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
                                    $('#treeview').data('kendoTreeView').dataSource.read();

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
        onSelect: function (e) {
            $('#btnSubDependencia').show();
            $('#btnUpdDependencia').show();
            $('#btnDelDependencia').show();
            
            $('#btnSubDependencia')[0].href = '/PARAM/Dependencias/InsSubDependencia/?padreId=' + $('#treeview').data('kendoTreeView').dataItem(e.node).id
            $('#btnUpdDependencia')[0].href = '/PARAM/Dependencias/UpdDependencia/?Id=' + $('#treeview').data('kendoTreeView').dataItem(e.node).id

            $('#btnNuevo')[0].href = '/PARAM/Dependencias/InsDependencia/?Id=' + $('#treeview').data('kendoTreeView').dataItem(e.node).id

        },
    };
}();