﻿/*
* v0.1.11
* Copyright 2016 Oldmansoft, Inc; http://www.apache.org/licenses/LICENSE-2.0
*/
if (!window.oldmansoft) window.oldmansoft = {};
window.oldmansoft.webman = new (function () {
    var $this = this,
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
        denied: "Permission denied",
        please_select_item: "Please select item.",
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

    function dealSubmitResult(data) {
        if (data.Message) {
            $app.alert(data.Message, data.Success ? undefined : text.warning).ok(function () {
                if (data.Path != null) {
                    document.location = data.Path;
                }
            });
        } else if (data.Path != null) {
            document.location = data.Path;
        }
    }

    this.configText = function (fn) {
        if (typeof fn == "function") fn(text);
    }

    this.setLoginSubmit = function (loginForm, seedPath, accountInput, passwordInput) {
        $(loginForm).submit(function () {
            var loading = $app.loading(),
                account,
                password,
                seedResponse,
                passwordHash,
                doubleHash;

            account = $.trim($(accountInput).val());
            password = $(passwordInput).val();
            seedResponse = $.ajax({ url: seedPath + "?" + new Date().getTime(), async: false });
            if (seedResponse.status != 200) {
                $app.alert(seedResponse.statusText);
                return false;
            }
            passwordHash = sha256(account.toLowerCase() + password);
            doubleHash = sha256(passwordHash.toUpperCase() + seedResponse.responseText);

            $.post($(this).attr("action"), {
                Account: account,
                Hash: doubleHash
            }).done(function (data) {
                loading.hide();
                dealSubmitResult(data);
            }).fail(function (error) {
                loading.hide();
                $app.alert($(error.responseText).eq(1).text(), error.statusText);
            });

            return false;
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
                a.attr("data-tips", items[i].tips);
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
            dom: "<'table-content'<'col-sm-6'f><'col-sm-6 text-right'l>>rt<'table-content'<'col-sm-6'i><'col-sm-6 text-right'p>>",
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
                post = $(this).attr("data-post") == "1",
                tips = $(this).attr("data-tips"),
                ids = getDataTableSelectedIds($(this)),
                loading;

            function execute(){
                if (behave == 0) {
                    if (post) {
                        $app.open(path, { selectedId: ids });
                    } else {
                        $app.open(path);
                    }
                } else if (behave == 1) {
                    if (!post || ids.length == 0) {
                        $app.addHash(path);
                    } else {
                        for (var i = 0; i < ids.length; i++) {
                            ids[i] = encodeURIComponent(ids[i]);
                        }
                        $app.addHash(path + "?selectedId=" + ids.join("&selectedId="));
                    }
                } else {
                    loading = $app.loading();
                    if (post) {
                        $.post(path, {
                            selectedId: ids
                        }).done(function (data) {
                            dealSubmitResult(data);
                        }).fail(dealAjaxError).always(function () { loading.hide(); });
                    } else {
                        $.get(path).done(function (data) {
                            dealSubmitResult(data);
                        }).fail(dealAjaxError).always(function () { loading.hide(); });
                    }
                }
            }

            if (post && ids.length == 0) {
                $app.alert(text.please_select_item);
                return;
            }
            if (tips) {
                $app.confirm(tips).yes(execute);
                return;
            }
            execute();
        });
        $(document).on("click", ".dataTable-item-action a", function (e) {
            var behave = Number($(this).attr("data-behave")),
                path = $(this).attr("data-path"),
                tips = $(this).attr("data-tips"),
                id = getDataTableItemId($(this)),
                loading;

            function execute() {
                if (behave == 0) {
                    $app.open(path, { selectedId: id });
                } else if (behave == 1) {
                    $app.addHash(path + "?selectedId=" + id);
                } else {
                    loading = $app.loading();
                    $.post(path, {
                        selectedId: id
                    }).done(function (data) {
                        dealSubmitResult(data);
                    }).fail(dealAjaxError).always(function () { loading.hide(); });
                }
            }

            if (tips) {
                $app.confirm(tips).yes(execute);
                return;
            }
            execute();
        });
        $(document).on("submit", "form", function (e) {
            var form,
                jqxhr,
                loading;
            e.preventDefault();

            loading = $app.loading();
            form = $(this).ajaxSubmit();
            jqxhr = form.data("jqxhr");
            jqxhr.done(function (data) {
                loading.hide();
                dealSubmitResult(data);
            }).fail(function (error) {
                loading.hide();
                $app.alert($(error.responseText).eq(1).text(), error.statusText);
            });
        });
    }

    window.$man = {
        init: $this.init,
        configText: $this.configText
    }
})();