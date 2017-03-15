// чтобы не ходить по ссылкам напрямую
var isHaveToLogout = true;
var haveToLogout = function() {
	return isHaveToLogout;
}

/**
 * показывает процессинговое окно
 * @returns {} 
 */
var showProcessing = function () {
	if (!$.show.processing.current) {
		$.show.processing();
	}
};

/**
 * закрывает процессинговое окно
 * @returns {} 
 */
var closeProcessing = function () {
	if ($.show.processing.current) {
		$.show.processing.current.modal('hide');
		$.show.processing.current = null;
	}
};

lalert = window.alert;

nalert = (function () {

	return function () {
		// настройки по умолчанию
		var options = {
			title: language.Common.kAttention,
			backdrop: true
		};

		// установить пользовательское сообщение
		options.message = arguments[0];

		return bootbox.alert(options);
	};

})();

window.alert = window.nalert;

var onSuccess = function(response) {

};

/*var Logout = function() {
	window.open("#/Login");
};

(function () {
	$(document).ready(function() {
		$(window).on("beforeunload", function() {
			if (!haveToLogout()) {
				return;
			}

			Logout();
		});
	});
})();*/

