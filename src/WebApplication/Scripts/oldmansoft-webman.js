/*
* v0.0.6
* Copyright 2016 Oldmansoft, Inc; http://www.apache.org/licenses/LICENSE-2.0
*/
(function () {
    var menu,
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
        }
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
        return function () {
            var div = $("<div></div>");
            for (var i = 0; i < items.length; i++) {
                var a = $("<a></a>");
                a.text(items[i].text);
                a.attr("href", items[i].path);
                div.append(a);
            }
            return div.html();
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
                // fix first column width
                var maxWidth = 0;
                table.find("tbody tr td:first-child").each(function () {
                    var width = computeElementWidth($(this));
                    if (width > maxWidth) maxWidth = width;
                })
                table.find("thead tr th:first-child").width(maxWidth);
                // fix last column width
                maxWidth = 0;
                table.find("tbody tr td:last-child").each(function () {
                    var width = computeElementWidth($(this));
                    if (width > maxWidth) maxWidth = width;
                })
                table.find("thead tr th:last-child").width(maxWidth);
            }
        };
        view.node.find("." + className).DataTable(option);
    }

    this.init = function (main, defaultLink) {
        menu = new define_menu();
        $app.init(main, defaultLink).viewActived(function (view) {
            if (view.name == "open") return;
            var link = view.node.data("link");
            menu.active(link);
        }).replacePCScrollBar(false);

        $(".webman-main-panel").css("min-height", $(window).height());
        $(window).on("resize", function () {
            $(".webman-main-panel").css("min-height", $(window).height());
        });
        $(document).on("click", ".webman-datatables-checkbox", function () {
            $(this).parents("table.dataTable").find("input[type='checkbox']").prop("checked", $(this).prop("checked"));
        });
        $(document).on("click", ".dataTable-action a", function (e) {
            var behave = Number($(this).attr("data-behave")),
                path = $(this).attr("data-path");
            if (behave == 0) {
                $app.open(path);
            } else if (behave == 1) {
                $app.addHash(path);
            } else {

            }
        });
    }

    if (!window.oldmansoft) window.oldmansoft = {};
    window.oldmansoft.webman = this;
    window.$man = {
        init: this.init,
        configText: this.configText
    }
})();