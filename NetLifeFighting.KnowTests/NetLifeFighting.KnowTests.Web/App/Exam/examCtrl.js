var app = angular.module('app');

app.controller('examCtrl', function ($scope, $http, $rootScope, $location) {

	$scope.init = function() {
		var searchObject = $location.search();

		$scope.testId = searchObject["testId"];
	};

});