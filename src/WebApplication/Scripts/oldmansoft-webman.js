/*
* v0.1.7
* Copyright 2016 Oldmansoft, Inc; http://www.apache.org/licenses/LICENSE-2.0
*/
if (!window.oldmansoft) window.oldmansoft = {};
window.oldmansoft.webman = new (function () {
    var self = this,
        menu,
        text;

    text = {
        dataTable: {
            info: "Showing _START_ to _END_ of _TOTAL_ entries",
            infoEmpty: "Empty",
            processing: "Processing...",
            paginate: {
                first: "First",
                last: "Last",
                next: "Next",
                previous: "Previous"
            }
        },
        error: "Error",
        warning: "Warning",
        denied: "Permission denied"
    }

    function define_menu() {
        var store = [];
        $(".webman-left-panel nav ul li a").each(function () {
            var item = { level: 0, node: $(this) };
            store.push(item);
        });

        this.active = function (link) {
            $(".webman-left-panel nav ul li a").removeClass("active");
            for (var i = 0; i < store.length; i++) {
                if (store[i].node.attr("href") == link) {
                    store[i].node.addClass("active");

                    $(".webman-breadcrumb ul").empty();
                    $(".webman-breadcrumb ul").append($("<li></li>").text(store[i].node.text()));
                    return true;
                }
            }
            return false;
        }
    }

    this.configText = function (fn) {
        if (typeof fn == "function") fn(text);
    }

    this.setLoginSubmit = function (loginForm, seedPath, accountInput, passwordInput, doubleHashInput) {
        $(loginForm).submit(function () {
            var fakePassword = "I love you. It's not password.";
            if ($(passwordInput).val() != fakePassword) {
                var seedResponse = $.ajax({ url: seedPath + "?" + new Date().getTime(), async: false });
                if (seedResponse.status != 200) {
                    alert(seedResponse.statusText);
                    return false;
                }

                var hash = sha256($.trim($(accountInput).val().toLowerCase()) + $(passwordInput).val());
                var doubleHash = sha256(hash.toUpperCase() + seedResponse.responseText);

                $(doubleHashInput).val(doubleHash);
                $(passwordInput).val(fakePassword);
            }
        });
    }

    this.setDataTableColumnCheckbox = function (data) {
        var input = $("<input type='checkbox'/>");
        input.val(data);
        return input.wrap('<div></div>').parent().html();
    }

    this.setDataTableColumnIndex = function (data, type, row, meta) {
        return meta.settings._iDisplayStart + meta.row + 1;
    }

    this.setDataTableColumnOperate = function (items) {
        return function (data) {
            var div = $("<div></div>");
            div.addClass("dataTable-item-action");
            div.attr("data-id", data);
            for (var i = 0; i < items.length; i++) {
                var a = $("<a></a>");
                a.text(items[i].text);
                a.attr("data-path", items[i].path);
                a.attr("data-behave", items[i].behave);
                div.append(a);
            }
            return div.wrap('<div></div>').parent().html();
        }
    }

    this.setDataTable = function (view, className, source, columns) {
        function computeElementWidth(item) {
            var width = 0;
            item.children().each(function () {
                width += $(this).outerWidth(true)
            });
            return width;
        }
        var option = {
            processing: true,
            serverSide: true,
            ajax: {
                url: source,
                type: 'POST'
            },
            columns: columns,
            retrieve: true,
            searching: false,
            lengthChange: false,
            autoWidth: false,
            ordering: false,
            language: text.dataTable,
            dom: "<'box-content'<'col-sm-6'f><'col-sm-6 text-right'l><'clearfix'>>rt<'box-content'<'col-sm-6'i><'col-sm-6 text-right'p><'clearfix'>>",
            initComplete: function () {
                var table = view.node.find("." + className);

                var maxWidth = 0;
                table.find("tbody tr td:first-child").each(function () {
                    var width = computeElementWidth($(this));
                    if (width > maxWidth) maxWidth = width;
                })
                if (maxWidth > 0) {
                    table.find("thead tr th:first-child").width(maxWidth);
                }

                maxWidth = 0;
                table.find("tbody tr td:last-child").each(function () {
                    var width = computeElementWidth($(this).children());
                    if (width > maxWidth) maxWidth = width;
                })
                if (maxWidth > 0) {
                    table.find("thead tr th:last-child").width(maxWidth);
                }
            }
        };
        view.node.find("." + className).DataTable(option);
    }

    function getDataTableSelectedIds(a) {
        var ids = [];
        a.parent().next().find("tbody tr td:first-child input[type='checkbox']").each(function () {
            if ($(this).prop("checked")) {
                ids.push($(this).val());
            }
        });
        return ids;
    }

    function getDataTableItemId(a) {
        return a.parent().attr("data-id");
    }

    function dealAjaxError(jqXHR, textStatus, errorThrown) {
        if (jqXHR.status == 401) {
            $app.alert(text.denied, text.error);
        } else {
            $app.alert(errorThrown, text.error);
        }
    }

    this.init = function (main, defaultLink) {
        menu = new define_menu();
        $app.init(main, defaultLink).viewActived(function (view) {
            if (view.name == "open") return;
            var link = view.node.data("link");
            menu.active(link);
        }).replacePCScrollBar(true);

        $(".webman-main-panel").css("min-height", $(window).height());
        $(window).on("resize", function () {
            $(".webman-main-panel").css("min-height", $(window).height());
        });
        $(document).on("click", ".webman-datatables-checkbox", function () {
            $(this).parents("table.dataTable").find("input[type='checkbox']").prop("checked", $(this).prop("checked"));
        });
        $(document).on("click", ".dataTable-action a", function (e) {
            var behave = Number($(this).attr("data-behave")),
                path = $(this).attr("data-path"),
                need = $(this).attr("data-need") == "1",
                ids = getDataTableSelectedIds($(this));

            if (need && ids.length == 0) return;
            if (behave == 0) {
                $app.open(path, { selectedId: ids });
            } else if (behave == 1) {
                if (!need || ids.length == 0) {
                    $app.addHash(path);
                } else {
                    for (var i = 0; i < ids.length; i++) {
                        ids[i] = encodeURIComponent(ids[i]);
                    }
                    $app.addHash(path + "?selectedId=" + ids.join("&selectedId="));
                }
            } else {
                var loading = $app.loading();
                $.post(path, { selectedId: ids }).done(function (result) {
                    if (result.Success) {
                        if (result.Message) {
                            $app.alert(result.Message);
                        }
                    } else {
                        $app.alert(result.Message, text.warning);
                    }
                }).fail(dealAjaxError).always(function () { loading.hide(); });
            }
        });
        $(document).on("click", ".dataTable-item-action a", function (e) {
            var behave = Number($(this).attr("data-behave")),
                path = $(this).attr("data-path");
            if (behave == 0) {
                $app.open(path, { selectedId: getDataTableItemId($(this)) });
            } else if (behave == 1) {
                $app.addHash(path + "?selectedId=" + getDataTableItemId($(this)));
            } else {
                var loading = $app.loading();
                $.post(path, { selectedId: getDataTableItemId($(this)) }).done(function (result) {
                    if (result.Success) {
                        if (result.Message) {
                            $app.alert(result.Message);
                        }
                    } else {
                        $app.alert(result.Message, text.warning);
                    }
                }).fail(dealAjaxError).always(function () { loading.hide(); });
            }
        });
    }

    window.$man = {
        init: self.init,
        configText: self.configText
    }
})();