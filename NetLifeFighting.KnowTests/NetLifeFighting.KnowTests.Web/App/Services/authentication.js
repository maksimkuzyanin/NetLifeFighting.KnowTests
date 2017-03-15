angular.module('app')
	.service('authentication', function authentication($q, $http, $timeout, $window) {
		var authenticatedPerson = null;

		return {

			// нужно при обновлении страницы
			init: function() {
				if ($window.sessionStorage.authenticatedPerson) {
					authenticatedPerson = JSON.parse($window.sessionStorage.authenticatedPerson);
				}
			},

			getPerson: function() {
				return authenticatedPerson;
			},

			exists: function() {
				return authenticatedPerson != null;
			},

			isLoggedIn: function () {
				return this.exists();
			},

			login: function(credentials) {
				var deferred = $q.defer();

				$http.post('api/persons/login', credentials)
					.then(function (response) {
						var result = response.data;

						if (!result.isSuccess) {
							deferred.reject(result.message);
							return;
						}

						var person = result.data;

						$timeout(function() {
							authenticatedPerson = person;
							// для глобального доступа
							$window.sessionStorage.authenticatedPerson = JSON.stringify(authenticatedPerson);
							deferred.resolve(person);
						}, 1000);
					});

				return deferred.promise;
			},

			logout: function () {
				var deferred = $q.defer();

				$window.sessionStorage.authenticatedPerson = null;
				authenticatedPerson = null;

				deferred.resolve();
				return deferred.promise;
			}
		};
	});