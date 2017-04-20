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

		$scope.isAdmin = function () {
			if ($location.path() === "/AdminLogin") {
				return true;
			}

			// роль администратора
			var adminRole = 1;

			var person = authentication.getPerson();
			if (person == null) {
				return false;
			}

			return person.roleType === adminRole;
		};

		$scope.showMenu = function () {
			if ($location.path() === "/Login" || $scope.isAdmin()) {
				// не показывать меню, если страница логина обычного пользователя
				// или пользователь работает в админке - там функциональность простенькая
				return false;
			}
			return true;
		};

		$scope.goToPage = function(path) {
			$location.path(path);
		};

		$scope.logout = function() {
			authentication.logout()
				.then(function() {
					$scope.goToPage("/Login");
				});
		};
	})

	.run(function (authentication, application, navigation, $rootScope, $window) {
		$rootScope.$on('$locationChangeStart', function (scope, next, current) {
			authentication.init();

			// страницы входа
			var loginPages = ['/Login', '/AdminLogin'];
			// текущая страница
			var currentPage = navigation.getCurrentPage();

			var restrictedPage = $.inArray(currentPage, loginPages) === -1;

			if (!restrictedPage) {
				// запомнить, чтобы правильно переходить
				$window.sessionStorage.LoginPage = currentPage;
			}

			$rootScope.personInfo = authentication.getPerson();

			var currentLoginPage = $window.sessionStorage.LoginPage || '/Login';

			if (!authentication.isLoggedIn()) {
				navigation.goToPage(currentLoginPage);
			}
		});
	});

