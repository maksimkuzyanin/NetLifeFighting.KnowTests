(function ($) {
	var opts = {};

	$.show = {
		defaults: {
			okText: language.Common.kOk,
			noText: language.Common.kNo,
			errorTitle: language.Common.kErrorMsgEmotional,
			messageTitel: language.Common.kDescription,
			confirmationTitle: language.Common.kAttention,
			processingTitle: language.Common.kWait,
			processingText: language.Common.kProcessing,
			selfErrorText: language.Common.kPageErrWrongCall,
			closeButton: false
		},

		message: function(message, title, options) {
			if (typeof message === 'undefined') {
				return $.show.error(opts.selfErrorText + ' "$.show.message"');
			}

			if (typeof options === 'undefined') {
				options = {};
			}

			var defOpts = {
				title: typeof title === 'string' ? title : opts.messageTitle,
				message: message,
				type: bootbox.TYPE_INFO,
				buttons: {
					confirm: {
						label: '<span class="glyphicon glyphicon-ok-sign"></span> ' + opts.okText,
						className: 'btn-default'
					}
				}
			};

			var msgOptions = $.extend({}, defOpts, options);
			var messageDialog = bootbox.dialog(msgOptions);

			//messageDialog.init(function() { // не сработало ?!
				messageDialog.addClass(msgOptions.type);
			//});

			messageDialog.isMessage = true;

			return messageDialog;
		},

		confirmation: function(message, titile, objButtons) {
			var buttons, cfrmOpts, defButtons, deferred;

			if (typeof message === 'undefined') {
				return $.show.error(opts.selfErrorText + ' "$.show.confirmation" (p1)');
			}

			// кнопки по умолчанию
			defButtons = {
				confirm: {
					label: opts.okText,
					className: 'btn-primary'
				},
				cancel: {
					label: opts.noText,
					className: 'btn-default'
				}
			};

			// todo: возможно нужна будет логика для мапинга кнопок
			buttons = defButtons;

			// объект для асинхронного конфирма
			deferred = $.Deferred();

			cfrmOpts = {
				title: typeof titile === 'string' ? titile : opts.confirmationTitle,
				message: message,
				type: bootbox.TYPE_PRIMARY,
				buttons: buttons,
				callback: function (result) {
					result ? deferred.resolve() : deferred.reject();
				}
			};

			var confirm = bootbox.confirm(cfrmOpts);
			confirm.addClass(cfrmOpts.type);

			return deferred.promise();
		},

		alert: function(message, options) {
			return $.show.message(message, language.Common.kAttention, options);
		},
		
		longWork: function(message, title, buttons) {
			var content =
				'<div>' +
					'<div>{message}</div>' +
					'<div class="progress progress-striped active">' +
						'<div class="progress-bar progress-bar-warning" style="width: 75%"></div>' +
					'</div>' +
				'</div>';

			if (typeof message === 'undefined') {
				return $.show.error(opts.selfErrorText + ' "$.show.longWork"');
			}

			if (typeof title === 'undefined') {
				title = language.Common.kPleaseWait;
			}

			content = content.replace('{message}', message);

			return bootbox.dialog({
				message: content,
				title: title,
				buttons: buttons,
				closeButton: opts.closeButton
			});
		},

		error: function(message) {
			return $.show.message(message, opts.errorTitle, {
				type: bootbox.TYPE_DANGER
			});
		},

		processing: function(message) {
			return $.show.processing.current = $.show.longWork(message != null ? message : opts.processingText, "<span class=\"glyphicon glyphicon-time\"></span> " + opts.processingTitle);
		},

		fileDialog: function (options) {
			var cancelBtn = function (dialog) {
				return dialog.close();
			};
			var applyBtn = function (dialog) {
			};

			var content =
				"<div>" +
					"<label class='upload-area' style='width:100%;text-align:center;' for='fileupload'>" +
						"<input id='fileupload' name='fileupload' type='file' style='display:none;' multiple='true'>" +
						"<i class='fa fa-cloud-upload fa-3x'></i>" +
						"<br />" +
						"Göz at" +
					"</label>" +
					"<br />" +
					"<span style='margin-left:5px !important;' id='fileList'></span>" +
				 "</div>" +
				 "<div class='clearfix'></div>";
			content = content.replace('{0}', typeof options.contentHtml !== 'undefined' ? options.contentHtml : "");
			content = content.replace('{1}', typeof options.additionalContent !== 'undefined' ? options.additionalContent : "");

			bootbox.dialog({
				message: content,
				title: options.title,
				buttons: {
					confirm: {
						label: language.Common.kOk,
						className: 'btn-primary',
						callback: applyBtn
					},
					cancel: {
						label: language.Common.kCancel,
						className: 'btn-default',
						callback: cancelBtn
					}
				}
			});
		}
	};

	opts = $.extend({}, $.show.defaults);
	window.alert = $.show.alert;

})(jQuery);