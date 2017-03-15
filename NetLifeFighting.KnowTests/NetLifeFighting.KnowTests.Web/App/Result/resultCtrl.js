var app = angular.module('app');

app.controller('resultCtrl', function ($scope, $http, $rootScope, $location, $window, $q, authentication) {

	$scope.init = function () {
		// пользователь
		$scope.person = authentication.getPerson();

		$http.get('api/tests/results/' + $scope.person.personId)
			.then(function(response) {
				var result = response.data;

				if (!result.isSuccess) {
					alert(result.message);
					return;
				};

				$scope.testsResults = result.data;
			});
	};
});