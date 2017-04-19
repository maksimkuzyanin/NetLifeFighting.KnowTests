var app = angular.module('app');

app.controller("adminLoginCtrl", function ($scope, $http, $rootScope, $location, authentication) {

	// информация для входа
	$scope.loginInfo = {};

	// проверяет заполненность логин/пароль
	$scope.isNotFullLoginInfo = function () {
		return !$scope.loginInfo.password;
	};

	// входит в систему тестирования с логин/пароль
	$scope.loginPerson = function () {
		showProcessing(); // индикация процесса

		authentication.login($scope.loginInfo)
			.then(function () {
				closeProcessing(); // завершение индикации процесса

				$location.path('/TestsList');
			},
			function (message) {
				closeProcessing(); // завершение индикации процесса

				alert(message);
			});
	};
});