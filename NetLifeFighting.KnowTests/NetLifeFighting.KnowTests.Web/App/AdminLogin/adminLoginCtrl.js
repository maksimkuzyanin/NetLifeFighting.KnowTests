var app = angular.module('app');

app.controller("adminLoginCtrl", function ($scope, $http, $rootScope, $location, authentication, navigation) {

	// информация для входа
	$scope.loginInfo = {};
	// страница перехода
	$scope.nextPage = "/Admin";

	// проверяет заполненность пароля
	$scope.isNotFullLoginInfo = function () {
		return !$scope.loginInfo.password;
	};

	// входит в админку
	$scope.adminLogin = function () {
		showProcessing(); // индикация процесса

		authentication.adminLogin($scope.loginInfo)
			.then(function () {
				closeProcessing(); // завершение индикации процесса
				navigation.goToPage($scope.nextPage);
			},
			function (message) {
				closeProcessing(); // завершение индикации процесса

				alert(message);
			});
	};
});