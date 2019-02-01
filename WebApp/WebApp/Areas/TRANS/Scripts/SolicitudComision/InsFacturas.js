var InsFacturas = function () {
    var valorComision = 0;
    "use strict";
    return {

        actionTemplateEliminar: null,

        init: function () {
            this.handleValidator();
            valorComision = $("#ValorComision").val();
            this.handleTemplates();
           
        },

        handleValidator: function () {
            $.validator.unobtrusive.parse($("#InsFacturas form"));
        },
        handleTemplates: function () {
            this.actionTemplateEliminar = kendo.template($('#actionTemplateEliminar').html());
        },

        
        CleanForm: function()
        {
            $("#Nit").val("");
            $("#RazonSocial").val("");
            $("#ValorFactura").val("");
            $("#ConceptoId").data("kendoDropDownList").select(-1);
        },

        removeRow: function (e) {
            
            var grid = $("#gridFacturas").data("kendoGrid");
            var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
            valorComision = parseFloat(valorComision) + parseFloat(dataItem.ValorFactura);
            $("#SubTotal").val("");
            $("#SubTotal").val(valorComision);
            Utils.removeGridDataItem(e, "#gridFacturas");
        },

        Agregar: function () {
            
            var validator = $("#InsFacturas").kendoValidator().data("kendoValidator").validate();
            var valorFacturaIndividual = $("#ValorFactura").val();
            
            var conceptolista = $("#ConceptoId").data("kendoDropDownList");
            if (valorFacturaIndividual <= valorComision)
            {
                valorComision = valorComision - valorFacturaIndividual;
                $("#SubTotal").val(valorComision);
                Utils.setGridDataSource("#gridFacturas", Utils.getGridDataSource("#gridFacturas"));
                Utils.addGridDataItem("#gridFacturas",
                    {
                        FechaFactura: $("#FechaFactura").data("kendoDatePicker").value(),
                        RazonSocial: $("#RazonSocial").val(),
                        ConceptoId: $("#ConceptoId").data("kendoDropDownList").value(),
                        ConceptoDescripcion: $("#ConceptoId").data("kendoDropDownList").text(),
                        ValorFactura: $("#ValorFactura").val(),
                        Nit: $("#Nit").val()                        
                    });
                $('#gridFacturas').data('kendoGrid').refresh();
                this.CleanForm();
              
            }
            else
            {
                swal({
                    title: "Error",
                    text: "El valor de la comision ha sido superado",
                    type: "error"
                });
            }
        },
        

        Guardar: function (id) {
            debugger
            var dataF =
            {
                comisionId: $('#ComisionId').val(),
                listFacturas: Utils.getGridDataSource("#gridFacturas"),
                legalizacionId: $('#Id').val(),
            };
                $.ajax({
                    type: "POST",
                    url: "/TRANS/SolicitudComision/InsLegalizacion",
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