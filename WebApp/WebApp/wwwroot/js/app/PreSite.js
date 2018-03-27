/* Global definitions
----------------------------------------------  */
var notification;
var idleTimer, idleCountdownTimer;
var notificationHub;

/* Funciones globales
------------------------------------------------------ */

// Función para serializar formularios a objetos json
$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
}

// Función que retorna una cadena con acentos ignorados
var ignoreAccents = (function () {
    var map = {
        'à': 'a', 'á': 'a', 'â': 'a', 'ã': 'a', 'ä': 'a', 'å': 'a', // a
        'ç': 'c',                                                   // c
        'è': 'e', 'é': 'e', 'ê': 'e', 'ë': 'e',                     // e
        'ì': 'i', 'í': 'i', 'î': 'i', 'ï': 'i',                     // i
        'ñ': 'n',                                                   // n
        'ò': 'o', 'ó': 'o', 'ô': 'o', 'õ': 'o', 'ö': 'o', 'ø': 'o', // o
        'ß': 's',                                                   // s
        'ù': 'u', 'ú': 'u', 'û': 'u', 'ü': 'u',                     // u
        'ÿ': 'y'                                                    // y
    };

    return function ignoreAccents(s) {
        if (!s) { return ''; }
        var ret = '';
        for (var i = 0; i < s.length; i++) {
            ret += map[s.charAt(i)] || s.charAt(i);
        }
        return ret;
    };
}());

/* Class Utils
------------------------------------------------------ */
var Utils = function () {
    "use strict"
    return {
        // Adiciona item a un kendogrid
        addGridDataItem: function (targetId, dataItem) {
            var grid = $(targetId).data("kendoGrid");
            grid.dataSource.add(dataItem);
        },

        // Elimina item de un kendogrid 
        removeGridDataItem: function (e, targetId) {
            e.preventDefault();
            var grid = $(targetId).data("kendoGrid");
            var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
            grid.dataSource.remove(dataItem);
        },

        // Retorna lista de items de un kendogrid 
        getGridDataSource: function (targetId) {
            var grid = $(targetId).data("kendoGrid");
            var jsonData = jQuery.parseJSON(JSON.stringify(grid.dataSource.data()));
            return jsonData;
        },

        // Retorna item de un kendogrid
        getGridDataItem: function (e, targetId) {
            e.preventDefault();
            var grid = $(targetId).data("kendoGrid");
            var dataItem = grid.dataItem($(e.currentTarget).closest("tr"));
            return dataItem;
        },

        // Actualiza contenido de un kendogrid con un objeto javascript
        setGridDataSource: function (targetId, jsonData) {
            var grid = $(targetId).data("kendoGrid");
            grid.dataSource.data(jsonData);
        },

        // Adiciona una sección a un contenedor, esto es usado comumente en contextos de autoguardado
        addSectionToContainer: function (sessionsContainer, sectionId, sectionObj) {
            sessionsContainer.facade[sectionId] = sectionObj;
        },

        // Valida las secciones especificadas retornando un booleano, esto es usado comumente en contextos de autoguardado
        isValidSections: function (facade, sectionsToValidate) {
            var dataSections;

            for (var idx in facade) {
                if ($.inArray(idx, sectionsToValidate) >= 0) {
					var sectionObj = facade[idx];
					var isValid = sectionObj.form.valid()
                    if (!isValid) {
                        return false;
                    }
                }
            }

            return true
        },

        // Valida las secciones especificadas retornando la seccion que ha fallado, esto es usado comumente en contextos de autoguardado
        isValidSectionsExt: function (facade, sectionsToValidate) {
            var dataSections;
            var result = {
                isValid: false,
                sectionId: ""
            };

            for (var idx in facade) {
                if ($.inArray(idx, sectionsToValidate) >= 0) {
                    var sectionObj = facade[idx];
                    var isValid = sectionObj.validateForm();
                    if (!isValid) {
                        result.isValid = false;
                        result.sectionId = idx;
                        return result;
                    }
                }
            }

            result.isValid = true;
            return result
        },

        // Retorna lista de secciones especificadas, esto es usado comumente en contextos de autoguardado
        getDataSections: function (facade, sectionsToValidate) {
            var dataSections;
            var sections = {};

            for (var idx in facade) {
                if ($.inArray(idx, sectionsToValidate) >= 0) {
                    var sectionObj = facade[idx];
                    sectionObj.form.submit();
                    sections[idx] = {
                        SectionId: sectionObj.sectionId,
                        Model: sectionObj.model,
                        DataForm: JSON.stringify(sectionObj.dataForm)
                    };
                }
            }

            dataSections = {
                Sections: sections
            };

            return dataSections;
        },

        // Selecciona con checkbox todos los registros del KendoGrid.
        // NOTA: el modelo del grid debe tener una propiedad llamada "Selected" de tipo "bool"
        selectAllCheckBox: function (event, dsFilterType) {
            var grid = $(event.currentTarget).closest(".k-grid.k-widget").data('kendoGrid');
            var checked = event.target.checked;

            // En caso de especificar "VIEW" seleccionará los elementos de la vista actual o filtrados
            var ds;
            if (dsFilterType == "VIEW") {
                ds = grid.dataSource.view();
            } else {
                ds = grid.dataSource.data();
            }

            $.each(ds, function (index, obj) {
                obj.Selected = checked;
            });

            grid.refresh();
        },

        // Selecciona un item del checkbox del KendoGrid
        // NOTA: el modelo del grid debe tener una propiedad llamada "Selected" de tipo "bool"
        selectItemCheckBox: function (event) {
            // lectura de check (true o false)
            var checked = event.target.checked;

            // referencia de objeto kendoGrid
            var grid = $(event.currentTarget).closest(".k-grid.k-widget").data('kendoGrid');

            // seleccion de fila actual
            var tr = $(event.currentTarget).closest("tr");

            // actualización de columna "Selected" necesaria en el modelo y persistiendola en el Datasource 
            var dataItem = grid.dataItem(tr);
            dataItem.Selected = checked;
        },

        // Obtiene lista de archivos adicionados al control KendoUpload
        getKendoUploadFileList: function (target) {
            var that = $(target).data("kendoUpload");
            var filesData;
            var allFiles = [];
            var listItems = that.wrapper.find('.k-file');
            for (var i = 0; i < listItems.length; i++) {
                filesData = $(listItems[i]).data('fileNames');
                if (filesData) {
                    for (var j = 0; j < filesData.length; j++) {
                        allFiles.push(filesData[j]);
                    }
                }
            }
            return allFiles;
        }
    };
}();

/* 02. Configura formato por defecto de kendo datepicker y kendo datetimepicker*/
function handleKendoDateFormat_Pre() {
    console.log("((handleKendoDateFormat_Pre))");

    kendo.ui.DatePicker.prototype.options.format = "dd/MM/yyyy";
    kendo.ui.DateTimePicker.prototype.options.format = "dd/MM/yyyy HH:mm";

    kendo.ui.DatePicker.fn.options.format = "dd/MM/yyyy";
    kendo.ui.DateTimePicker.fn.options.format = "dd/MM/yyyy HH:mm";
}

/* 03. Configura filtro por defecto para las grillas que contienen campos de fecha y texto en "Is after or equal to", "Contains" */
function handleKendoStringAndDateFilter_Pre() {
    console.log("((handleKendoStringAndDateFilter_Pre))");

    // for menu
    var stringOps = kendo.ui.FilterMenu.prototype.options.operators.string;
    var startWithOp = { startswith: "Starts with" };
    kendo.ui.FilterMenu.prototype.options.operators.string = $.extend({}, startWithOp, stringOps);

    var dateOps = kendo.ui.FilterMenu.prototype.options.operators.date;
    var gteOp = { gte: "Is after or equal to" };
    kendo.ui.FilterMenu.prototype.options.operators.date = $.extend({}, gteOp, dateOps);

    //kendo.ui.FilterMenu.fn.options.extra = true;

    // for cell row
    var cellmenu = kendo.ui.FilterCell.prototype.options;
    cellmenu.minLength = 50;
    stringOps = cellmenu.operators.string;
    cellmenu.operators.string = $.extend({}, startWithOp, stringOps);
    cellmenu.messages.isFalse = "F";
    cellmenu.messages.isTrue = "T";

    var dateOps = cellmenu.operators.date;
    cellmenu.operators.date = $.extend({}, gteOp, dateOps);

    // deshabilita autocompletado en filtros de celda para la grilla (nota: afecta los tipos de dato datetime y number revisar antes)
    //kendo.ui.FilterCell.fn.options.template = function (e) {
    //    console.log(e);
    //    e.element.kendoAutoComplete({
    //        serverFiltering: false,
    //        valuePrimitive: false,
    //        noDataTemplate: ''
    //    });
    //};
}

/* 04. Configura filtro de celda "row" con el relog para especificar el la hora y minutos */
function handleDateTimeColumnFilter_Pre(args) {
    console.log("((handleDateTimeColumnFilter_Pre))");
    args.element.kendoDateTimePicker();
}

/* 05. Configura la cultura en español de los componentes de kendo*/
function handleKendoCulture_Pre() {
    console.log("((handleKendoCulture_Pre))");
    kendo.culture("es");
}

/* 06. Configura Jquery validator */
function handleJqueryValidator_Pre() {
    $.validator.setDefaults({
        ignore: []
    });
}

/* 07. Configura handleFilterMenuInit */
function handleFilterMenuInit_Pre(e) {    
    console.log("((handleFilterMenuInit_Pre))");
    
    var type = e.sender.dataSource.options.schema.model.fields[e.field].type

    if (type == "date") {
        e.container.getKendoPopup().bind("open", function (evt) {
            try {            
                var firstValueDropDown = e.container.find("select:eq(0)").data("kendoDropDownList");
                firstValueDropDown.value("gte");
                firstValueDropDown.trigger("change");

                var logicDropDown = e.container.find("select:eq(1)").data("kendoDropDownList");
                logicDropDown.value("and");

                var secondValueDropDown = e.container.find("select:eq(2)").data("kendoDropDownList");
                secondValueDropDown.value("lte");
                secondValueDropDown.trigger("change");
                
                setTimeout(function () {
                    firstValueDropDown.wrapper.hide();
                    logicDropDown.wrapper.hide();
                    secondValueDropDown.wrapper.hide();

                    e.container.find(".k-filter-help-text:eq(0)").text("Desde");
                    e.container.find(".k-filter-help-text:eq(1)").remove();
                    e.container.find(".k-widget.k-dropdown.k-header:eq(1)").before("<div class='k-filter-help-text'>Hasta</div>");
                });
            } catch (e) {
                // Este caso se da para filtros extras en falso
            }

        });
    }

}

/* 08. Configura handleKendoGridGeneral_Pre */
function handleKendoGridGeneral_Pre(e) {
    console.log("((handleKendoGridGeneral_Pre))");
    
    // Se extiende el componente KendoGrid
    var oInit = kendo.ui.Grid.fn.init;
    kendo.ui.Grid = kendo.ui.Grid.extend({
        init: function () {
            oInit.apply(this, arguments);
            
            // manejador de evento "dataBound"
            this.bind('dataBound', function (e) {
                try {
                    // se actualiza mensaje cuando no hay registros
                    $(".k-grid-norecords-template").text("No hay registros.");
                    // se captura los eventos de botones para modales ajax
                    handleAjaxModal();
                } catch (e) {
                    // Se dispara el evento dataBound al inicializar aunque no este presente la función handleAjaxModal
                };
            });

            // manejador de evento "filterMenuInit"
            this.bind('filterMenuInit', function (e) {
                handleFilterMenuInit_Pre(e);
            });
        }
    });
    kendo.ui.plugin(kendo.ui.Grid);

    // Mesanje de paginador.
    kendo.ui.Pager.prototype.options.messages.display = "Elementos mostrados {0}-{1}";
}

