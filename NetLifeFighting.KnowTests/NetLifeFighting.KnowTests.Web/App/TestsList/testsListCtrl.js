var app = angular.module('app');

app.controller('testsListCtrl', function ($scope, $http, $location) {

	// получить список тестов
	$scope.init = function () {
		showProcessing(); // индикация процесса

		$http.post('api/tests')
			.then(function (response) {
				closeProcessing();

				// результат запроса
				var result = response.data;

				if (!result.isSuccess) {
					alert(result.message);
					return;
				}

				$scope.preparedTests = result.data;
			});
	};

	// прокидывает на тест
	$scope.goExam = function (testId) {
		// 
		$location.path('/Test').search({ testId: testId });
	};
});