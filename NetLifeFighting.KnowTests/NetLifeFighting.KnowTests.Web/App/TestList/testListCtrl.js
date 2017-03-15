var app = angular.module('app');

app.controller('testListCtrl', function ($scope, $http, $rootScope, $location) {

	// получить список тестов
	$scope.init = function () {
		showProcessing(); // индикация процесса

		$http.get('api/tests')
			.then(function (response) {
				closeProcessing();

				// результат запроса
				var result = response.data;

				if (!result.isSuccess) {
					return alert(result.message);
				}

				$rootScope.tests = result.data;
			});
	};

	// прокидывает на тест
	$scope.goExam = function (testId) {
		// 
		$location.path('/Exam').search({ testId: testId });
	};
});