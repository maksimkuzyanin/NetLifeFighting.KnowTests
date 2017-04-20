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

			setSessionAuthPerson: function () {
				if (authenticatedPerson) {
					// для глобального доступа
					$window.sessionStorage.authenticatedPerson = JSON.stringify(authenticatedPerson);
				}
			},

			commonDeferLogin: function (url, credentials) {
				var deferred = $q.defer();
				var self = this;

				$http.post(url, credentials)
					.then(function (response) {
						var result = response.data;

						if (!result.isSuccess) {
							deferred.reject(result.message);
							return;
						}

						var person = result.data;

						$timeout(function() {
							authenticatedPerson = person;
							self.setSessionAuthPerson();
							deferred.resolve(person);
						}, 1000);
					});

				return deferred.promise;
			},

			// сделал в этом сервисе более частные методы,
			// при этом общую логику вынес для универсальности
			login: function (credentials) {

				var studentRole = 1;

				var url = "api/persons/student/login";

				credentials.roleType = studentRole;

				return this.commonDeferLogin(url, credentials);
			},

			adminLogin: function (credentials) {

				var adminName = "admin";
				var adminRole = 1;

				var url = "api/persons/admin/login";

				credentials.nickname = adminName;
				credentials.roleType = adminRole;

				return this.commonDeferLogin(url, credentials);
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