// BootstrapValidator v0.5.3
(function ($) {
    $.fn.bootstrapValidator.i18n.regexp = $.extend($.fn.bootstrapValidator.i18n.regexp || {}, {
        'default': 'Please enter a value matching the pattern'
    });

    $.fn.bootstrapValidator.validators.regexp = {
        html5Attributes: {
            message: 'message',
            regexp: 'regexp'
        },

        enableByHtml5: function ($field) {
            var pattern = $field.attr('pattern');
            if (pattern) {
                return {
                    regexp: pattern
                };
            }

            return false;
        },

        /**
         * Check if the element value matches given regular expression
         *
         * @param {BootstrapValidator} validator The validator plugin instance
         * @param {jQuery} $field Field element
         * @param {Object} options Consists of the following key:
         * - regexp: The regular expression you need to check
         * @returns {Boolean}
         */
        validate: function (validator, $field, options) {
            var values = [];
            if ($field.attr('type') == "file") {
                var files = $field.get(0).files;
                if (files.length == 0) return true;
                for (var i = 0; i < files.length; i++) {
                    values.push(files[i].name);
                }
            } else {
                var value = $field.val();
                if (value === '') {
                    return true;
                }
                values.push(value);
            }

            for (var i = 0; i < values.length; i++) {
                var regexp = ('string' === typeof options.regexp) ? new RegExp(options.regexp) : options.regexp;
                if (!regexp.test(values[i])) {
                    return false;
                }
            }
            return true;
        }
    };
}(window.jQuery));