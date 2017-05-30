var app = angular.module('app');

app.controller('testsListCtrl', function ($scope, $http, navigation) {

	// по умолчанию режим обучающий
	$scope.testRegime = 1;

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

	// срабатывает при выборе радиокнопки
	$scope.onChange = function (event) {
		// кроссбраузерное решение
		event = event || window.event;
		var self = event.target;
		// общий на экране режим тестирования
		$scope.testRegime = self.value;
	}

	// прокидывает на тест
	$scope.goExam = function (testId) {
		navigation.goToPage('/Test', { testId: testId, testRegime: $scope.testRegime });
	};
});