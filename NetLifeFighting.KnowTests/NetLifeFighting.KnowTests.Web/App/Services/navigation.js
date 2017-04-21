﻿// отдельный сервис навигации по экранам
angular.module('app')
	.service('navigation', function navigation($location) {
		return {
			// переход на страницу
			goToPage: function(url) {
				$location.path(url);
			},

			// текущая страница
			getCurrentPage: function() {
				return $location.path();
			}
		}
	});