// логика работы с хранилищем
angular.module('app')
	.service('storage', function storage($window) {
		return {
			getOrCreate: function(name, value) {
				if (value) {
					$window.sessionStorage[name] = value;
				} else {
					value = $window.sessionStorage[name];
				}

				return value;
			}
		}
	});