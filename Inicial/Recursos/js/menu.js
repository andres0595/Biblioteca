/*
*  Plugin para menu de la pagina principal (home.aspx)
*/

$(function () {
    (function ($) {

        $.fn.accordion = function (custom) {
            var defaults = {
                keepOpen: false,
                startingOpen: false
            }
            var settings = $.extend({}, defaults, custom);
            if (settings.startingOpen) {
                $(settings.startingOpen).show(); 
            }

            return this.each(function () {
                var obj = $(this);
                $('li a', obj).click(function (event) {
                    var elem = $(this).next();
                    if (elem.is('ul')) {
                        event.preventDefault();
                        if (!settings.keepOpen) {
                            obj.find('ul:visible').not(elem).not(elem.parents('ul:visible')).slideUp();
                        }
                        elem.slideToggle();
                    }
                });
            });
        };
    })(jQuery);

    $('#menu').accordion({ keepOpen: false, startingOpen: '#close' }); /* si necesita que se muestre un menu por defecto se cambia el close por open*/
});


