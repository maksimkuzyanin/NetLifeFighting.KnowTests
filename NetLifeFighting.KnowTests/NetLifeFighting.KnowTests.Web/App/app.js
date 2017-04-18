var app = angular.module('app', ['ngRoute', 'ui.bootstrap'])
	.config(function($routeProvider) {

		// настройка маршрутизации
		$routeProvider
			.when('/Login', { templateUrl: 'Login/Index.html', controller: 'loginCtrl' })
			.when('/TestsList', { templateUrl: 'TestsList/Index.html', controller: 'testsListCtrl' })
			.when('/Test', { templateUrl: 'Test/Index.html', controller: 'testCtrl' })
			.when('/Result', { templateUrl: 'Result/Index.html', controller: 'resultCtrl' })
			.when('/AdminLogin', { templateUrl: 'AdminLogin/Index.html', controller: 'adminLoginCtrl' })
			.when('/Admin', { templateUrl: 'Admin/Index.html', controller: 'adminCtrl' });

		$routeProvider.otherwise({ redirectTo: '/Login' });
	})

	.controller('MenuController', function ($scope, $location, $window, authentication) {

		$scope.showMenu = function () {
			return $location.path() !== '/Login';
		};

		$scope.goToPage = function(path) {
			$location.path(path);
		};

		$scope.logout = function() {
			authentication.logout()
				.then(function() {
					$scope.goToPage('/Login');
				});
		};
	})

	.run(function (authentication, application, $rootScope, $location) {
		$rootScope.$on('$locationChangeStart', function (scope, next, current) {
			authentication.init();

			$rootScope.personInfo = authentication.getPerson();

			if (!authentication.isLoggedIn()) {
				$location.path('/Login');
			}
		});
	});

