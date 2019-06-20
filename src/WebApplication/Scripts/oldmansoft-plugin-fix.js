function findTemporaryTargetField($field) {
    var form = $field.parentsUntil("body", "form"),
        tempFor = $field.attr("data-temporary-for");
    if (!tempFor) return $field;

    return form.find("input[name=" + tempFor + "]").filter(function (index) {
        return !$(this).attr("data-temporary-for");
    });
}

// jQuery Form Plugin 4.2.1
(function ($) {
    /**
    * Feature detection
    */
    var feature = {};

    feature.fileapi = $('<input type="file">').get(0).files !== undefined;
    feature.formdata = (typeof window.FormData !== 'undefined');
    $.fn.formToArray = function (semantic, elements, filtering) {
        var a = [];

        if (this.length === 0) {
            return a;
        }

        var form = this[0];
        var formId = this.attr('id');
        var els = (semantic || typeof form.elements === 'undefined') ? form.getElementsByTagName('*') : form.elements;
        var els2;

        if (els) {
            els = $.makeArray(els); // convert to standard array
        }

        // #386; account for inputs outside the form which use the 'form' attribute
        // FinesseRus: in non-IE browsers outside fields are already included in form.elements.
        if (formId && (semantic || /(Edge|Trident)\//.test(navigator.userAgent))) {
            els2 = $(':input[form="' + formId + '"]').get(); // hat tip @thet
            if (els2.length) {
                els = (els || []).concat(els2);
            }
        }

        if (!els || !els.length) {
            return a;
        }

        if ($.isFunction(filtering)) {
            els = $.map(els, filtering);
        }

        var i, j, n, v, el, max, jmax;

        for (i = 0, max = els.length; i < max; i++) {
            el = els[i];
            n = el.name;
            if (!n || el.disabled) {
                continue;
            }
            // Oldman: ignore temporary
            if ($(el).attr("data-temporary") == "temporary") {
                continue;
            }

            if (semantic && form.clk && el.type === 'image') {
                // handle image inputs on the fly when semantic == true
                if (form.clk === el) {
                    a.push({ name: n, value: $(el).val(), type: el.type });
                    a.push({ name: n + '.x', value: form.clk_x }, { name: n + '.y', value: form.clk_y });
                }
                continue;
            }

            v = $.fieldValue(el, true);
            if (v && v.constructor === Array) {
                if (elements) {
                    elements.push(el);
                }
                for (j = 0, jmax = v.length; j < jmax; j++) {
                    a.push({ name: n, value: v[j] });
                }

            } else if (feature.fileapi && el.type === 'file') {
                if (elements) {
                    elements.push(el);
                }

                var files = el.files;

                if (files.length) {
                    for (j = 0; j < files.length; j++) {
                        a.push({ name: n, value: files[j], type: el.type });
                    }
                } else {
                    // #180
                    a.push({ name: n, value: '', type: el.type });
                }

            } else if (v !== null && typeof v !== 'undefined') {
                if (elements) {
                    elements.push(el);
                }
                a.push({ name: n, value: v, type: el.type, required: el.required });
            }
        }

        if (!semantic && form.clk) {
            // input type=='image' are not found in elements array! handle it here
            var $input = $(form.clk), input = $input[0];

            n = input.name;

            if (n && !input.disabled && input.type === 'image') {
                a.push({ name: n, value: $input.val() });
                a.push({ name: n + '.x', value: form.clk_x }, { name: n + '.y', value: form.clk_y });
            }
        }

        return a;
    };
}(window.jQuery));

// BootstrapValidator v0.5.3
(function ($) {
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
            var values = [],
                fields = findTemporaryTargetField($field),
                field,
                files,
                value,
                regexp,
                i,
                j;
            for (i = 0; i < fields.length; i++) {
                field = fields.eq(i);
                if (field.attr('type') == "file") {
                    files = field.get(0).files;
                    for (j = 0; j < files.length; j++) {
                        values.push(files[j].name);
                    }
                } else {
                    value = field.val();
                    if (value === '') {
                        return true;
                    }
                    values.push(value);
                }
            }

            regexp = ('string' === typeof options.regexp) ? new RegExp(options.regexp) : options.regexp;
            for (i = 0; i < values.length; i++) {
                if (!regexp.test(values[i])) {
                    return false;
                }
            }
            return true;
        }
    };

    $.fn.bootstrapValidator.validators.notEmpty = {
        enableByHtml5: function ($field) {
            var required = $field.attr('required') + '';
            return ('required' === required || 'true' === required);
        },

        /**
         * Check if input value is empty or not
         *
         * @param {BootstrapValidator} validator The validator plugin instance
         * @param {jQuery} $field Field element
         * @param {Object} options
         * @returns {Boolean}
         */
        validate: function (validator, $field, options) {
            var type = $field.attr('type'),
                fields,
                i,
                delInputs;
            if ('radio' === type || 'checkbox' === type) {
                return validator
                            .getFieldElements($field.attr('data-bv-field'))
                            .filter(':checked')
                            .length > 0;
            }

            if ('number' === type && $field.get(0).validity && $field.get(0).validity.badInput === true) {
                return true;
            }

            if ('file' === type) {
                if ($field.hasClass("template-mulit-file-input")) {
                    delInputs = $field.parent().parent().find(".del-file-input");
                    for (i = 0; i < delInputs.length; i++) {
                        if ($.trim(delInputs.eq(i).val()) === '0') return true;
                    }

                    fields = findTemporaryTargetField($field);
                    for (i = 0; i < fields.length; i++) {
                        if ($.trim(fields.eq(i).val()) !== '') return true;
                    }
                    return false;
                } else if ($field.hasClass("single-file-input")) {
                    if ($.trim($field.val()) === '') {
                        if ($field.parent().find(".del-file-input").val() === '1') {
                            return false;
                        }
                        if ($field.parent().find("a").length == 0) {
                            return false;
                        }
                    }
                    return true;
                }
            }

            if ($field.hasClass("input") && $field.parent().hasClass("tagsinput")) {
                fields = findTemporaryTargetField($field);
                return fields.length > 0;
            }

            return $.trim($field.val()) !== '';
        }
    };
}(window.jQuery));

// fix Unable to preventDefault inside passive event listener
(function ($, undefined) {
    if (jQuery.fn.jquery != "1.10.2") return;
    var
        passiveSupported = false,
        core_strundefined = typeof undefined,
        core_rnotwhite = /\S+/g,
        rtypenamespace = /^([^.]*)(?:\.(.+)|)$/;

    try {
        var options = Object.defineProperty({}, "passive", {
            get: function () {
                passiveSupported = true;
            }
        });
        window.addEventListener("test", null, options);
    } catch (err) { }

    jQuery.event.add = function (elem, types, handler, data, selector) {
        var tmp, events, t, handleObjIn,
            special, eventHandle, handleObj,
            handlers, type, namespaces, origType,
            elemData = jQuery._data(elem);

        // Don't attach events to noData or text/comment nodes (but allow plain objects)
        if (!elemData) {
            return;
        }

        // Caller can pass in an object of custom data in lieu of the handler
        if (handler.handler) {
            handleObjIn = handler;
            handler = handleObjIn.handler;
            selector = handleObjIn.selector;
        }

        // Make sure that the handler has a unique ID, used to find/remove it later
        if (!handler.guid) {
            handler.guid = jQuery.guid++;
        }

        // Init the element's event structure and main handler, if this is the first
        if (!(events = elemData.events)) {
            events = elemData.events = {};
        }
        if (!(eventHandle = elemData.handle)) {
            eventHandle = elemData.handle = function (e) {
                // Discard the second event of a jQuery.event.trigger() and
                // when an event is called after a page has unloaded
                return typeof jQuery !== core_strundefined && (!e || jQuery.event.triggered !== e.type) ?
                    jQuery.event.dispatch.apply(eventHandle.elem, arguments) :
                    undefined;
            };
            // Add elem as a property of the handle fn to prevent a memory leak with IE non-native events
            eventHandle.elem = elem;
        }

        // Handle multiple events separated by a space
        types = (types || "").match(core_rnotwhite) || [""];
        t = types.length;
        while (t--) {
            tmp = rtypenamespace.exec(types[t]) || [];
            type = origType = tmp[1];
            namespaces = (tmp[2] || "").split(".").sort();

            // There *must* be a type, no attaching namespace-only handlers
            if (!type) {
                continue;
            }

            // If event changes its type, use the special event handlers for the changed type
            special = jQuery.event.special[type] || {};

            // If selector defined, determine special event api type, otherwise given type
            type = (selector ? special.delegateType : special.bindType) || type;

            // Update special based on newly reset type
            special = jQuery.event.special[type] || {};

            // handleObj is passed to all event handlers
            handleObj = jQuery.extend({
                type: type,
                origType: origType,
                data: data,
                handler: handler,
                guid: handler.guid,
                selector: selector,
                needsContext: selector && jQuery.expr.match.needsContext.test(selector),
                namespace: namespaces.join(".")
            }, handleObjIn);

            // Init the event handler queue if we're the first
            if (!(handlers = events[type])) {
                handlers = events[type] = [];
                handlers.delegateCount = 0;

                // Only use addEventListener/attachEvent if the special events handler returns false
                if (!special.setup || special.setup.call(elem, data, namespaces, eventHandle) === false) {
                    // Bind the global event handler to the element
                    if (elem.addEventListener) {
                        elem.addEventListener(type, eventHandle, passiveSupported ? { passive: false } : false);

                    } else if (elem.attachEvent) {
                        elem.attachEvent("on" + type, eventHandle);
                    }
                }
            }

            if (special.add) {
                special.add.call(elem, handleObj);

                if (!handleObj.handler.guid) {
                    handleObj.handler.guid = handler.guid;
                }
            }

            // Add to the element's handler list, delegates in front
            if (selector) {
                handlers.splice(handlers.delegateCount++, 0, handleObj);
            } else {
                handlers.push(handleObj);
            }

            // Keep track of which events have ever been used, for event optimization
            jQuery.event.global[type] = true;
        }

        // Nullify elem to prevent memory leaks in IE
        elem = null;
    }
})(window.jQuery);