// jquery.form 4.2.1
(function () {
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
                    // By: Oldman, fix .net mvc model
                    var blob;
                    try {
                        blob = new Blob([], { type: "application/octet-stream" });
                    } catch (e) {
                        window.BlobBuilder = window.BlobBuilder || window.WebKitBlobBuilder || window.MozBlobBuilder || window.MSBlobBuilder;
                        if (e.name == 'TypeError' && window.BlobBuilder) {
                            var builder = new BlobBuilder();
                            builder.append([]);
                            blob = builder.getBlob("application/octet-stream");
                        }
                        else {
                            alert("We're screwed, blob constructor unsupported entirely");
                        }
                    }
                    a.push({ name: n, value: blob, type: el.type });
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
})();