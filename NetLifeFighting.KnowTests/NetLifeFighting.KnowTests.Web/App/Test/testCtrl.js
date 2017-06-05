var app = angular.module('app');

app.controller('testCtrl', function ($scope, $http, $rootScope, $location, $window, $q, authentication, storage) {

	// приватные методы 

	// возвращает текущий индекс
	var getCurrentPreparedQuestIndex = function () {

		if ($window.sessionStorage.currentPreparedQuestIndex) {
			return $window.sessionStorage.currentPreparedQuestIndex;
		}

		return 0;
	};

	// инкрементирует текущий индекс
	var incCurrentPreparedQuestIndex = function () {
		$window.sessionStorage.currentPreparedQuestIndex = ++$scope.currentPreparedQuestIndex;
	};

	var getCurrentPersonAnswers = function () {
		if ($window.sessionStorage.personAnswers) {
			return JSON.parse($window.sessionStorage.personAnswers);
		}
		return [];
	};

	// устанавливает ответы пользователя в хранилище
	var setCurrentPersonAnswers = function(personAnswers) {
		if (!personAnswers || !personAnswers.length) {
			return;
		}

		$window.sessionStorage.personAnswers = JSON.stringify(personAnswers);
	};

	// получает результат тестирования
	var getResult = function () {
		var deferred = $q.defer();
		var currentPersonAnswers = getCurrentPersonAnswers();

		if (!currentPersonAnswers.length) {
			var emptyResult = {
				isEmptyResult: true
			};

			$scope.testResult = emptyResult;
			$window.sessionStorage.testResult = JSON.stringify(emptyResult);

			deferred.resolve();
		} else {

			showProcessing(); // индикация процесса

			$http.post('api/tests/result', currentPersonAnswers)
				.then(function(response) {
					closeProcessing();

					var result = response.data;
					if (!result.isSuccess) {
						alert(result.message);
						return;
					}

					$scope.testResult = result.data;
					$window.sessionStorage.testResult = JSON.stringify(result.data);

					deferred.resolve();
				});
		}

		return deferred.promise;
	};

	var clearSessionStorage = function() {
		$window.sessionStorage.removeItem('currentPreparedQuestIndex');
		$window.sessionStorage.removeItem('personAnswers');
		$window.sessionStorage.removeItem('testResult');
		$window.sessionStorage.removeItem('testId');
		$window.sessionStorage.removeItem('testRegime');
	};

	$scope.goToPage = function (path) {
		$location.path(path);
	};

	// инициализация страницы
	$scope.init = function () {
		// параметры запроса
		var searchObject = $location.search();

		// идентификатор теста
		$scope.testId = searchObject["testId"];
		// режим тестирования
		$scope.testRegime = searchObject["testRegime"];

		// в случае если из параметризованной строки будет удален параметр - идентификатор теста,
		// взять его из хранилища
		// переинициализация
		$scope.testId = storage.getOrCreate("testId", $scope.testId);
		$scope.testRegime = storage.getOrCreate("testRegime", $scope.testRegime);

		// вычисление режима тестирования
		$scope.trainingMode = $scope.testRegime == 1;
		$scope.examMode = $scope.testRegime == 2;
		$scope.timeExamMode = $scope.testRegime == 3;

		// пользователь
		$scope.person = authentication.getPerson();

		// ответы пользователя
		$scope.personAnswers = getCurrentPersonAnswers();

		$scope.testResult = (function() {
			if ($window.sessionStorage.testResult) {
				return JSON.parse($window.sessionStorage.testResult);
			}
			return null;
		})();

		// показать результат
		$scope.showResult = $scope.testResult != null;

		if ($scope.showResult) {
			return;
		}

		showProcessing(); // индикация процесса

		// получает информацию по выбранному тесту
		$http.post('api/tests/' + $scope.testId)
			.then(function (response) {
				closeProcessing();

				var result = response.data;

				if (!result.isSuccess) {
					alert(result.message);
					return;
				};

				// текущий открытый тест
				$scope.preparedTest = result.data;

				// вопросы
				$scope.preparedQuestions = _.sortBy($scope.preparedTest.questions, function (preparedQuest) {
					return preparedQuest.quest.questNum;
				});

				// текущий индекс
				$scope.currentPreparedQuestIndex = getCurrentPreparedQuestIndex();

				// текущий вопрос
				$scope.currentPreparedQuest = $scope.preparedQuestions[$scope.currentPreparedQuestIndex];

				// крайний индекс в массиве вопросов
				$scope.lastPreparedQuestIndex = $scope.preparedQuestions.length - 1;
			});
	};

	// в случае ухода со страницы очистить хранилище
	$scope.$on('$locationChangeStart', function (event, next, current) {
		clearSessionStorage();
	});

	$scope.pushAnswer = function () {
		// не выбран
		var noSelect = -1;
		// тип ответа на текущий вопрос
		var answerType = $scope.currentPreparedQuest.quest.answerType;
		// элементы ответов на странице
		var questAnswers = $('.test-question-answer');
		// ответы на текущий вопрос
		var currentPersonAnswers;

		// todo: пересмотреть
		if (answerType === AnswerType.CONFORMITY) {
			var groupQuestId = $('input[name="PREPAREDQUESTID"]').val();

			currentPersonAnswers = _.toArray(questAnswers.map(function () {
				var questId = $(this).find('input[name="QUESTID"]').val();
				var answerId = $(this).find('select[name="ANSWER"]').val();

				if (answerId === noSelect) {
					return;
				}

				return {
					personId: $scope.person.personId,
					testId: $scope.testId,
					questId: questId,
					groupQuestId: groupQuestId,
					answerId: answerId
				};
			}));
		}
		else {
			currentPersonAnswers = _.toArray(questAnswers.map(function () {
				var priorityNo = null;
				var inpAnswer = $(this).find('input[name="ANSWERID"]');
				var checked = inpAnswer.prop('checked');

				if (!checked) {
					return;
				}

				var answerId = inpAnswer.val();

				if (answerType === AnswerType.PRIORITY) {
					priorityNo = $(this).find('select[name="PRIORITYNO"]').val();
				}

				return {
					personId: $scope.person.personId,
					testId: $scope.testId,
					questId: $scope.currentPreparedQuest.quest.questid,
					answerId: answerId,
					priorityNo: priorityNo
				};
			}));
		}

		if (!currentPersonAnswers || !currentPersonAnswers.length) {
			return;
		}

		$scope.personAnswers = $.merge($scope.personAnswers, currentPersonAnswers);
		// установить текущие ответы пользователя
		setCurrentPersonAnswers($scope.personAnswers);
	};

	// следующий вопрос
	$scope.next = function () {

		// добавляет текущий ответ пользователя
		$scope.pushAnswer();

		// следующий вопрос
		incCurrentPreparedQuestIndex();

		if ($scope.currentPreparedQuestIndex > $scope.lastPreparedQuestIndex) {
			getResult().then(function() {
				$scope.showResult = true;
			});
			return;
		}

		$scope.currentPreparedQuest = $scope.preparedQuestions[$scope.currentPreparedQuestIndex];
	};

	// утвердить ответ, если тренировочный режим
	$scope.approve = function () {
		// текущий идентификатор вопроса
		var currQuestId = $scope.currentPreparedQuest.quest.questid;

		// ? параметры и строка запроса
		$http.post("api/tests/approve/" + currQuestId)
			.then(function(response) {
				// здесь добавить цвет
			});
	}

	// сохраняет результаты тестов
	$scope.save = function () {
		if (!$scope.personAnswers.length) {
			return;
		}

		showProcessing();
		$http.post('api/tests/result/save', JSON.stringify($scope.personAnswers))
			.then(function () {
				closeProcessing();
				$scope.goToPage('/Result');
			});
	};
});