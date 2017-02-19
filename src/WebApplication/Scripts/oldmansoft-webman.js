/*
* v0.0.1
* Copyright 2016 Oldmansoft, Inc; http://www.apache.org/licenses/LICENSE-2.0
*/
(function () {
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

	if (!window.oldmansoft) {
		window.oldmansoft = {};
	}
	window.oldmansoft.webman = this;
	window.oldmansoft.$man = {
	}
})();