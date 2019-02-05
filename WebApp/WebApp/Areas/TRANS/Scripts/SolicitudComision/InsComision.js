var InsComision = function () {
    "use strict";
    return {
       
        init: function () {
            this.handleValidator();
        },

        handleValidator: function () {
            $.validator.unobtrusive.parse($("#InsComision form"));
        },
        onSuccess: function (result) {
            
            if (result.Success) {
                swal({
                    title: "Correcto",
                    text: result.Message,
                    type: "success"
                }, function () {
                    $('#modal').modal('hide');
                    $("#grid").data("kendoGrid").dataSource.read();
                });

            } else {
                swal({
                    title: "Error",
                    text: result.Message,
                    type: "error"
                });
            }
        },

        ChangeColaborador: function () {
            
            var valorIndividual = $("#ColaboradorId").data("kendoDropDownList").dataSource._data[0].Valor;
            //$("#ColaboradorId").data("kendoDropDownList").dataSource._data[$("#ColaboradorId").data("kendoDropDownList").selectedIndex].Valor;
            var ValorComisionTotal = valorIndividual * ($("#CantidadDias").val())
            $("#ValorComision").val(ValorComisionTotal);
        },
          
        filterProyecto: function () {            
            var Proyecto = $("#ProyectoId").val();
            return {
                ProyectoId: parseFloat(Proyecto)
                };
        },

        filterDeptoOrigen: function () {
            debugger;
            var depto = $("#DepartamentoOrigen").val();
            
            return {
                padreId: parseFloat(depto)
            };
        },

        filterDeptoDestino: function () {
            debugger;
            var depto = $("#DepartamentoDestino").val();
            return {
                padreId: parseFloat(depto)
            };
        },

        closeDate: function () {            
            var cantDias = 0;
            var ONE_DAY = 1000 * 60 * 60 * 24;
            var initDate = new Date();
            initDate = $("#FechaInicio").data("kendoDatePicker").value();
            $("#FechaFinalizacion").data("kendoDatePicker").value(initDate);
            initDate = initDate.getTime();
            var endDate = $("#FechaFinalizacion").data("kendoDatePicker").value();
            endDate = endDate.getTime();
            var difference_ms = Math.abs(endDate - initDate);
            cantDias = Math.round(difference_ms / ONE_DAY) + 1;
            
            $("#CantidadDias").val(cantDias);
            if ($("#ColaboradorId").val() != "0")
                InsComision.ChangeColaborador();
        },

        closeDateFin: function () {

            var cantDias = 0;
            var ONE_DAY = 1000 * 60 * 60 * 24;
            var initDate = new Date();
            initDate = $("#FechaInicio").data("kendoDatePicker").value();
            initDate = initDate.getTime();
            var endDate = $("#FechaFinalizacion").data("kendoDatePicker").value();
            endDate = endDate.getTime();
            if (initDate > endDate) {
                swal({
                    title: "Atención",
                    text: "La fecha inicial no puede ser mayor a la final.",
                    type: "info"
                });
                cantDias = 1;
                $("#FechaInicio").data("kendoDatePicker").value(new Date());
                $("#FechaFinalizacion").data("kendoDatePicker").value(new Date());
            }
            else {
                var difference_ms = Math.abs(endDate - initDate);
                cantDias = Math.round(difference_ms / ONE_DAY) + 1;

            }

            $("#CantidadDias").val(cantDias);
            
            if ($("#ColaboradorId").val() != "0")
                InsComision.ChangeColaborador();
        },
    };
}();