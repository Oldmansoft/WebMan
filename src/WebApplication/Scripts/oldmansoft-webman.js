﻿/*
* v0.0.3
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
        return "<input type='checkbox' value='" + data + "'/>";
    }

    this.setDataTableColumnIndex = function (data, type, row, meta) {
        return meta.settings._iDisplayStart + meta.row + 1;
    }

    this.setDataTable = function (view, className, source, columns) {
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
            dom: "<'box-content'<'col-sm-6'f><'col-sm-6 text-right'l><'clearfix'>>rt<'box-content'<'col-sm-6'i><'col-sm-6 text-right'p><'clearfix'>>"
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
    }

    if (!window.oldmansoft) window.oldmansoft = {};
    window.oldmansoft.webman = this;
    window.$man = {
        init: this.init,
        configText: this.configText
    }
})();