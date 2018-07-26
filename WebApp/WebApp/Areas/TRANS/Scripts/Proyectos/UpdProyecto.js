var UpdProyecto = function () {
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
            $.validator.unobtrusive.parse($("#UpdProyecto form"));
        },

        Validate: function () {

            UpdProyecto.form.validate().settings.ignore = [];
            return UpdProyecto.form.valid();
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

        }
		
    };
}();