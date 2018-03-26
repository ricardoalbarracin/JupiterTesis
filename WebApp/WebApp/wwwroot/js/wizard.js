;(function ($, window, document, undefined) {
    "use strict";

    // The plugin constructor
    function Wizard(element) {
        // Merge user settings with default, recursively
        // Main container element
        this.main = $(element);
        // Navigation bar element
        this.nav = this.main.children('ul');
        // Step anchor elements
        this.steps = $("li > a", this.nav);
        // Content container
        this.container = this.main.children('div');
        // Content pages
        this.pages = this.container.children('div');
        // Active step index
        this.current_index = null;
		// Call initial method
        this.init();
    }

    $.extend(Wizard.prototype, {

		init: function () {
			
			$("li", this.nav).addClass('disabled')
            // Assign plugin events
            this._setEvents();
			
            var idx = 0;
            
			// Show the initial step
            this._showStep(idx);
        },

        // PRIVATE FUNCTIONS

		      
		_setEvents: function () {
			
            // Anchor click event
            var mi = this;
            $(this.steps).on("click", function (e) {
                e.preventDefault();
                
                var idx = mi.steps.index(this);
                
                if (idx !== mi.current_index) {
                    if (mi.options.anchorSettings.enableAllAnchors !== false && mi.options.anchorSettings.anchorClickable !== false) {
                        mi._showStep(idx);
                    } else {
                        if (mi.steps.eq(idx).parent('li').hasClass('done')) {
                            mi._showStep(idx);
                        }
                    }
                }
            });

            // Next button event
            $('.sw-btn-next', this.main).on("click", function (e) {
                e.preventDefault();
                mi._showNext();
            });

            // Previous button event
            $('.sw-btn-prev', this.main).on("click", function (e) {
                e.preventDefault();
                mi._showPrevious();
            });

            //// Keyboard navigation event
            //if (this.options.keyNavigation) {
            //    $(document).keyup(function (e) {
            //        mi._keyNav(e);
            //    });
            //}            //// Keyboard navigation event
            //if (this.options.keyNavigation) {
            //    $(document).keyup(function (e) {
            //        mi._keyNav(e);
            //    });
            //}
			          

            return true;
        },
        _showNext: function () {
            var si = this.current_index + 1;
            
            for (var i = si; i < this.steps.length; i++) {
                if (!this.steps.eq(i).parent('li').hasClass('hidden')) {
                    si = i;
                    break;
                }
            }

            if (this.steps.length <= si) {
                
                    return false;
               
            }
            this._showStep(si);
            return true;
        },
        _showPrevious: function () {
            var si = this.current_index - 1;
            
            for (var i = si; i >= 0; i--) {
                if ( !this.steps.eq(i).parent('li').hasClass('hidden')) {
                    si = i;
                    break;
                }
            }
            if (0 > si) {
               
                    return false;
               
            }
            this._showStep(si);
            return true;
        },
		_showStep: function (idx) {
			
            // If step not found, skip
            if (!this.steps.eq(idx)) {
                return false;
            }
            // If current step is requested again, skip
            if (idx == this.current_index) {
                return false;
            }
            
            if (this.steps.eq(idx).parent('li').hasClass('hidden')) {
                return false;
            }
			// Load step content
			
			this._loadStepContent(idx);

			this.steps.parent('li').removeClass('active');
			this.steps.eq(idx).parent('li').addClass('active')

			
            return true;
        },
		_loadStepContent: function (idx) {
			
            var mi = this;
            // Get current step elements
            var curTab = this.steps.eq(this.current_index);
            // Get the direction of step navigation
            var stepDirection = '';
            var elm = this.steps.eq(idx);
            

            if (this.current_index !== null && this.current_index !== idx) {
                stepDirection = this.current_index < idx ? "forward" : "backward";
            }

            // Trigger "leaveStep" event
            if (this.current_index !== null && this._triggerEvent("leaveStep", [curTab, this.current_index, stepDirection]) === false) {
                return false;
            }

            
            this._transitPage(idx);
            
            return true;
        },
		_transitPage: function (idx) {
			
            var mi = this;
            // Get current step elements
            var curTab = this.steps.eq(this.current_index);
            var curPage = curTab.length > 0 ? $(curTab.attr("href"), this.main) : null;
            // Get step to show elements
            var selTab = this.steps.eq(idx);
            var selPage = selTab.length > 0 ? $(selTab.attr("href"), this.main) : null;
            // Get the direction of step navigation
            var stepDirection = '';
            if (this.current_index !== null && this.current_index !== idx) {
                stepDirection = this.current_index < idx ? "forward" : "backward";
            }

            var stepPosition = 'middle';
            if (idx === 0) {
                stepPosition = 'first';
            } else if (idx === this.steps.length - 1) {
                stepPosition = 'final';
            }

           
            this.pages.finish();
            
                // normal fade
                if (curPage && curPage.length > 0) {
                    curPage.fadeOut('fast', '', function () {
						selPage.fadeIn('fast', '', function () {
							$(this).show().addClass('show');
                        });
                    });
                } else {
					selPage.fadeIn('fast', '', function () {
						$(this).show().addClass('show');;
                    });
                }
            
            // Update the current index
            this.current_index = idx;

            // Trigger "showStep" event
            this._triggerEvent("showStep", [selTab, this.current_index, stepDirection, stepPosition]);
            return true;
        },

        // HELPER FUNCTIONS
        _keyNav: function (e) {
            var mi = this;
            // Keyboard navigation
            switch (e.which) {
                case 37:
                    // left
                    mi._showPrevious();
                    e.preventDefault();
                    break;
                case 39:
                    // right
                    mi._showNext();
                    e.preventDefault();
                    break;
                default:
                    return; // exit this handler for other keys
            }
        },
        _triggerEvent: function (name, params) {
            // Trigger an event
            var e = $.Event(name);
            this.main.trigger(e, params);
            if (e.isDefaultPrevented()) {
                return false;
            }
            return e.result;
        },
        _loader: function (action) {
            switch (action) {
                case 'show':
                    this.main.addClass('sw-loading');
                    break;
                case 'hide':
                    this.main.removeClass('sw-loading');
                    break;
                default:
                    this.main.toggleClass('sw-loading');
            }
        },
        // PUBLIC FUNCTIONS

		next: function () {
			
            this._showNext();
        },
        prev: function () {
            this._showPrevious();
        },
        reset: function () {
            // Trigger "beginReset" event
            if (this._triggerEvent("beginReset") === false) {
                return false;
            }

            // Reset all elements and classes
            this.container.stop(true);
            this.pages.stop(true);
			this.pages.hide().removeClass('show');
            this.current_index = null;
            
            $(".sw-toolbar", this.main).remove();
            this.steps.removeClass();
            this.steps.parents('li').removeClass();
            this.steps.data('has-content', false);
            this.init();

            // Trigger "endReset" event
            this._triggerEvent("endReset");
        },
        
    });

    // Wrapper for the plugin
	$.fn.wizard = function (options) {
		
        var args = arguments;
        var instance;

        if (options === undefined || typeof options === 'object') {
            return this.each(function () {
                if (!$.data(this, "wizard")) {
                    $.data(this, "wizard", new Wizard(this, options));
                }
            });
        } else if (typeof options === 'string' && options[0] !== '_' && options !== 'init') {
            instance = $.data(this[0], 'wizard');

            if (options === 'destroy') {
                $.data(this, 'wizard', null);
            }

            if (instance instanceof Wizard && typeof instance[options] === 'function') {
                return instance[options].apply(instance, Array.prototype.slice.call(args, 1));
            } else {
                return this;
            }
        }
    };
})(jQuery, window, document);
