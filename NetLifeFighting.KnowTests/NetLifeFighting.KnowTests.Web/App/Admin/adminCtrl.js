var app = angular.module("app");

app.controller("adminCtrl", function ($scope) {
	$scope.importTests = function() {
		$.show.fileDialog({
			uploadUrl: "api/admin/import",
			allowedFileExtensions: ["xls", "xlsx"]
		});
	};
});