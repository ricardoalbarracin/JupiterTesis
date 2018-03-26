/* Global definitions
------------------------------------------------ */
var modal = $('#modal');
var modal2 = $('#modal2');
var windowOne = null;


/* Application Controller
------------------------------------------------ */
var App = function () {
    "use strict";
    return {
		init: function () {
			console.log("((init))");
			this.handleMenu();
            this.handleModal();
            this.handleModalConfig();
			this.handleValidatorConfig();
			this.handleButtons();
        },

		handleButtons: function () {
			jQuery(":button").__proto__.reset = function () {
				this.html(this.attr('text-value'));
				this.prop('disabled', false);
				this.removeAttr('text-value')
			};
			jQuery(":button").__proto__.loading = function () {
				var $this = $(this);
				this.attr('text-value', this.html())
				this.prop('disabled', true);
				var loadingText = '<i class="fa fa-circle-o-notch fa-spin"></i> Cargando...';
				if ($(this).html() !== loadingText) {
					$this.data('original-text', $(this).html());
					$this.html(loadingText);
				}
			};
		},
        handleModalConfig: function () {
            console.log("((handleModalConfig))");
            $(document).on('show.bs.modal', '.modal', function (event) {
                var zIndex = 3000 + (10 * $('.modal:visible').length);
                $(this).css('z-index', zIndex);
                setTimeout(function() {
                    $('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
                }, 0);
            });           
        },

        /* Configurador de eventos para los modales 
        ---------------------------------------------------------------*/
        handleModal: function () {
            console.log("((handleModal))");
            // Limpia los eventos asociados para elementos ya existentes, asi evita duplicación
            $("a[data-modal]").unbind("click");
            $("a[data-modal2]").unbind("click");

            // Evita cachear las transaccione Ajax previas
            $.ajaxSetup({ cache: false });

            var onClick = function (e) {
                var modalName = e.data;
                var currentModal = modalName == "modal2" ? modal2 : modal;
                var dataModalValue = $(this).data(modalName);
                var modalContent = currentModal.find(".modal-content");

                modalContent.load(this.href, function (response, status, xhr) {
                    switch (status) {
                        case "success":
                            currentModal.modal({ backdrop: 'static', keyboard: false }, 'show');

                            if (dataModalValue == "modal-lg") {
                                currentModal.find(".modal-dialog").addClass("modal-lg");
                            }
                            else if (dataModalValue == "modal-xl") {
                                currentModal.find(".modal-dialog").addClass("modal-xl");
                            }
                            else {
                                currentModal.find(".modal-dialog").removeClass("modal-lg");
                                currentModal.find(".modal-dialog").removeClass("modal-xl");
                            }

                            break;

                        case "error":
                            var message = "Error de ejecución: " + xhr.status + " " + xhr.statusText;
                            console.error(message);
                            break;
                    }

                    App.handleModal();
                    //App.handleDatepickerReadonly();
                });

                return false;
            }

            // Configura evento del link para aquellos para los que desean abrir modales
            $("a[data-modal]").bind("click", "modal", onClick);
            $("a[data-modal2]").bind("click", "modal2", onClick);
        },

        handleValidatorConfig: function () {
            // valida campo fecha con formato de la cultura actual
            $.validator.methods.date = function (value, element) {
                //return this.optional(element) || Globalize.parseDate(value, "d/M/y", "en");
                return this.optional(element) || kendo.parseDate(value);
            }

            $.validator.setDefaults({ ignore: '' });
        },


		handleMenu: function () {
            console.log("((handleMenu))");
			$("a.active").closest("ul").closest("li").addClass("open")
        }
    };
}();





