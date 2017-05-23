﻿/*
* v0.1.27
* Copyright 2016 Oldmansoft, Inc; http://www.apache.org/licenses/LICENSE-2.0
*/
if (!window.oldmansoft) window.oldmansoft = {};
window.oldmansoft.webman = new (function () {
    var $this = this,
        menu,
        text,
        dealMethod,
        linkBehave;

    text = {
        dataTable: {
            info: "Showing _START_ to _END_ of _TOTAL_ entries",
            infoEmpty: "Empty",
            emptyTable: "No data available in table",
            processing: "Processing...",
            paginate: {
                first: "First",
                last: "Last",
                next: "Next",
                previous: "Previous"
            }
        },
        error: "Error",
        success: "Success",
        tips: "Tips",
        denied: "Permission denied",
        please_select_item: "Please select item.",
    }

    dealMethod = {
        form: 0,
        call: 1
    }

    linkBehave = {
        link: 0,
        open: 1,
        call: 2,
        self: 3,
        blank: 4,
        script: 5
    }

    function bindUrlParamter(path, parameter) {
        if (!parameter) return path;
        if (!path) return "?" + parameter;
        if (path.indexOf("?") > -1) return path + "&" + parameter;
        return path + "?" + parameter;
    }

    function define_menu() {
        var store = [];
        $(".webman-left-panel ul.side-menu li").each(function () {
            var item = { level: 0, node: $(this).children("a") };
            store.push(item);
            item.node.click(function () {
                if ($(this).parent().hasClass("expand")) {
                    $(this).parent().removeClass("expand");
                    $(this).find(".arrow").removeClass("fa-minus-circle").addClass("fa-plus-circle");
                } else {
                    $(this).parent().addClass("expand");
                    $(this).find(".arrow").removeClass("fa-plus-circle").addClass("fa-minus-circle");
                }
                resetMainHeight();
            });
            if ($(this).children("ul").length > 0) {
                item.node.append($("<i></i>").addClass("arrow").addClass("fa").addClass("fa-plus-circle"));
            } else if (item.node.attr("href") == undefined) {
                $(this).hide();
            }
        });

        function setBreadcrumb(links, view, defaultLink) {
            var i, j, link, node, text, currentLinks, a, icon;
            node = $(".webman-breadcrumb ul");
            node.empty();
            for (i = 0; i < links.length; i++) {
                link = links[i];
                if (link == "") link = defaultLink;
                text = null;
                icon = null;
                for (j = 0; j < store.length; j++) {
                    if (link == store[j].node.attr("href")) {
                        text = store[j].node.children("span").text();
                        icon = store[j].node.children("i").clone();
                        break;
                    }
                }

                currentLinks = links.slice(0, i + 1);
                currentLinks.splice(0, 0, "");
                a = $("<a></a>");
                if (i < links.length - 1) {
                    a.attr("href", currentLinks.join("#"));
                }
                if (!text) {
                    if (view.node.data("link") == links[i]) {
                        text = view.node.find(".webman-panel header h2").text();
                        icon = view.node.find(".webman-panel header>i");
                    } else {
                        text = "..";
                    }
                }
                if (i < links.length - 1 && icon) a.append(icon);
                a.append($("<span></span>").text(text));
                node.append($("<li></li>").append(a));
            }
        }

        function findNode(link, defaultLink) {
            var i;
            if (link == "") link = defaultLink;
            for (i = 0; i < store.length; i++) {
                if (link == store[i].node.attr("href")) {
                    return store[i].node.parent();
                }
            }
        }

        this.active = function (links, view, defaultLink) {
            var node, result;
            setBreadcrumb(links, view, defaultLink);
            $(".webman-left-panel ul.side-menu li").removeClass("active");
            node = findNode(links[links.length - 1], defaultLink);
            if (node) {
                node.addClass("active").parentsUntil(".side-menu", "li").addClass("expand").children("a").children(".arrow").removeClass("fa-plus-circle").addClass("fa-minus-circle");
                result = true;
            } else {
                result = false;
            }
            resetMainHeight();
            return result;
        }
    }

    function dealSubmitResultAction(data, method) {
        var operator,
            loading;
        if (data.CloseOpen && method == dealMethod.form) {
            $app.close();
        }
        if (data.NewData) {
            operator = $(".main-view").last().data("operator");
            if (operator) {
                operator.draw(false);
            } else {
                $app.reload();
            }
        } else if (data.Path != null) {
            if (data.Behave == linkBehave.link) {
                $app.sameHash(data.Path);
            } else if (data.Behave == linkBehave.open) {
                $app.open(data.Path);
            } else if (data.Behave == linkBehave.call) {
                loading = $app.loading();
                $.get(data.Path).done(function (data) {
                    dealSubmitResult(data, dealMethod.call);
                }).fail(dealAjaxError).always(function () { loading.hide(); });
            } else if (data.Behave == linkBehave.self) {
                document.location = data.Path;
            } else if (data.Behave == linkBehave.blank) {
                window.open(data.Path);
            }
        }
    }

    function dealSubmitResult(data, method) {
        if (data.Message) {
            $app.alert(data.Message, data.Success ? text.success : text.tips).ok(function () {
                dealSubmitResultAction(data, method);
            });
        } else {
            dealSubmitResultAction(data, method);
        }
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

    function submitForm(form) {
        var loading,
            action = form.attr("action"),
            target = form.attr("target");

        if (target == "_call") {
            loading = $app.loading();
            form.ajaxSubmit().data("jqxhr").done(function (data) {
                loading.hide();
                dealSubmitResult(data, dealMethod.form);
            }).fail(function (error) {
                loading.hide();
                $app.alert($(error.responseText).eq(1).text(), error.statusText);
            });
        } else if (target == "_open") {
            $app.open(action, form.serialize());
        } else if (!target) {
            $app.hash(action + "?" + form.serialize().replace(/%23/g, ''));
        } else {
            return true;
        }
        return false;
    }

    function resetMainHeight() {
        var leftHeight = $(".webman-left-panel").height(),
            windowHeight = $(window).height();
        $(".webman-main-panel").css("min-height", leftHeight > windowHeight ? leftHeight : windowHeight);
    }

    this.configText = function (fn) {
        if (typeof fn == "function") fn(text);
    }

    this.setLoginSubmit = function (loginForm, seedPath, accountInput, passwordInput) {
        $(accountInput).on("keypress", function () {
            $(this).parent().parent().removeClass("has-error");
        });
        $(accountInput).on("change", function () {
            $(this).parent().parent().removeClass("has-error");
        });
        $(passwordInput).on("keypress", function () {
            $(this).parent().parent().removeClass("has-error");
        });
        $(passwordInput).on("change", function () {
            $(this).parent().parent().removeClass("has-error");
        });
        $(loginForm).submit(function () {
            var loading,
                account,
                password,
                seedResponse,
                passwordHash,
                doubleHash;

            account = $.trim($(accountInput).val());
            password = $(passwordInput).val();

            if (account == "") {
                $(accountInput).parent().parent().addClass("has-error");
            }
            if (password == "") {
                $(passwordInput).parent().parent().addClass("has-error");
            }
            if (account == "" || password == "") {
                return false;
            }

            loading = $app.loading()
            seedResponse = $.ajax({ url: seedPath + "?" + new Date().getTime(), async: false });
            if (seedResponse.status != 200) {
                loading.hide();
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
                data.NewData = false;
                data.CloseOpen = false;
                data.Behave = linkBehave.self;
                dealSubmitResult(data, dealMethod.form);
            }).fail(function (error) {
                loading.hide();
                $app.alert($(error.responseText).eq(1).text(), error.statusText);
            });

            return false;
        });
    }

    this.setFormValidate = function (view, className, fields) {
        if (!fields) return;
        var form = view.node.find("." + className);
        form.bootstrapValidator({
            fields: fields
        }).on('success.form.bv', function (e) {
            if (!submitForm($(e.target))) {
                e.preventDefault();
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
            drawCallback: function () {
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
        },
            node;
        node = view.node.find("." + className);
        node.data("datatable", node.DataTable(option));
    }

    this.init = function (main, defaultLink) {
        oldmansoft.webapp.configTarget(function (target) {
            target["_call"] = function (href) {
                var loading = $app.loading();
                $.get(href).done(function (data) {
                    dealSubmitResult(data, dealMethod.call);
                }).fail(dealAjaxError).always(function () { loading.hide(); });
            }
        });
        menu = new define_menu();
        $app.init(main, defaultLink).viewActived(function (view) {
            if (view.name == "open") return;
            menu.active(oldmansoft.webapp.hashes(), view, defaultLink);
        }).replacePCScrollBar(true);

        resetMainHeight();
        $(window).on("resize", resetMainHeight);
        $(".webman-bar").on("click", function () {
            var body = $("body"), className = "mini-nav";
            if (body.hasClass(className)) {
                body.removeClass(className);
            } else {
                body.addClass(className);
            }
        });
        $(".side-menu>li>a").on("click", function (e) {
            if ("ontouchmove" in document && $(window).width() <= 768) {
                var menu = $(".side-menu"),
                    index = $(".side-menu>li>a").index($(this));
                if (menu.data("click") != index) {
                    menu.data("click", index);
                    return false;
                } else {
                    $(document).click();
                }
            }
        });
        $(document).on("click", ".webman-datatables-checkbox", function () {
            $(this).parents("table.dataTable").find("input[type='checkbox']").prop("checked", $(this).prop("checked"));
        });
        $(document).on("click", ".dataTable-action a", function (e) {
            var action_nothing = 0,
                action_supportParameter = 1,
                action_needSelected = 2,
                behave = Number($(this).attr("data-behave")),
                path = $(this).attr("data-path"),
                action = Number($(this).attr("data-action")),
                tips = $(this).attr("data-tips"),
                ids = getDataTableSelectedIds($(this)),
                node = $(e.target).parents(".webman-body").find("table.dataTable");

            function execute() {
                var loading;
                if (behave == linkBehave.open) {
                    if ((action & action_supportParameter) == action_supportParameter) {
                        $app.open(path, { SelectedId: ids });
                    } else {
                        $app.open(path);
                    }
                } else if (behave == linkBehave.link) {
                    if ((action & action_supportParameter) == action_nothing || ids.length == 0) {
                        $app.addHash(path);
                    } else {
                        for (var i = 0; i < ids.length; i++) {
                            ids[i] = encodeURIComponent(ids[i]);
                        }
                        $app.addHash(bindUrlParamter(path, "SelectedId=" + ids.join("&SelectedId=")));
                    }
                } else if (behave == linkBehave.call) {
                    loading = $app.loading();
                    if ((action & action_supportParameter) == action_supportParameter) {
                        $.post(path, {
                            SelectedId: ids
                        }).done(function (data) {
                            dealSubmitResult(data, dealMethod.call);
                        }).fail(dealAjaxError).always(function () { loading.hide(); });
                    } else {
                        $.get(path).done(function (data) {
                            dealSubmitResult(data, dealMethod.call);
                        }).fail(dealAjaxError).always(function () { loading.hide(); });
                    }
                } else if (behave == linkBehave.self) {
                    if ((action & action_supportParameter) == action_nothing || ids.length == 0) {
                        document.location = path;
                    } else {
                        for (var i = 0; i < ids.length; i++) {
                            ids[i] = encodeURIComponent(ids[i]);
                        }
                        document.location = bindUrlParamter(path, "SelectedId=" + ids.join("&SelectedId="));
                    }
                } else if (behave == linkBehave.blank) {
                    if ((action & action_supportParameter) == action_nothing || ids.length == 0) {
                        window.open(path);
                    } else {
                        for (var i = 0; i < ids.length; i++) {
                            ids[i] = encodeURIComponent(ids[i]);
                        }
                        window.open(bindUrlParamter(path, "SelectedId=" + ids.join("&SelectedId=")));
                    }
                } else if (behave == linkBehave.script) {
                    $man.parameter = ids;
                    eval(path);
                }
            }

            if ((action & action_needSelected) == action_needSelected && ids.length == 0) {
                $app.alert(text.please_select_item);
                return;
            }
            if (tips) {
                $app.confirm(tips).yes(execute);
            } else {
                execute();
            }
            node.parents(".main-view").data("operator", node.data("datatable"));
        });
        $(document).on("click", ".dataTable-item-action a", function (e) {
            var behave = Number($(this).attr("data-behave")),
                path = $(this).attr("data-path"),
                tips = $(this).attr("data-tips"),
                id = getDataTableItemId($(this)),
                node = $(e.target).parents("table.dataTable");

            function execute() {
                var loading;
                if (behave == linkBehave.open) {
                    $app.open(path, { SelectedId: id });
                } else if (behave == linkBehave.link) {
                    $app.addHash(bindUrlParamter(path, "SelectedId=" + id));
                } else if (behave == linkBehave.call) {
                    loading = $app.loading();
                    $.post(path, {
                        SelectedId: id
                    }).done(function (data) {
                        dealSubmitResult(data, dealMethod.call);
                    }).fail(dealAjaxError).always(function () { loading.hide(); });
                } else if (behave == linkBehave.self) {
                    document.location = bindUrlParamter(path, "SelectedId=" + id);
                } else if (behave == linkBehave.blank) {
                    window.open(bindUrlParamter(path, "SelectedId=" + id));
                } else if (behave == linkBehave.script) {
                    $man.parameter = id;
                    eval(path);
                }
            }

            if (tips) {
                $app.confirm(tips).yes(execute);
            } else {
                execute();
            }
            node.parents(".main-view").data("operator", node.data("datatable"));
        });
        $(document).on("submit", "form:not(.bv-form)", function (e) {
            if (!submitForm($(this))) {
                e.preventDefault();
            }
        });
        $(".webman-main-panel header form i.fa-search").on("click", function () {
            submitForm($(this).parents("form"));
        });
    }

    window.$man = {
        init: $this.init,
        configText: $this.configText,
        parameter: null
    }
})();