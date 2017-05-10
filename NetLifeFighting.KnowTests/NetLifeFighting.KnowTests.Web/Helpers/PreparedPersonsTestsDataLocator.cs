using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework;
using NetLifeFighting.KnowTests.DAL.EntityFramework.Persons;
using NetLifeFighting.KnowTests.DAL.EntityFramework.Tests;
using NetLifeFighting.KnowTests.Web.DTO.Person;
using NetLifeFighting.KnowTests.Web.DTO.Test;

namespace NetLifeFighting.KnowTests.Web.Helpers
{
	/// <summary>
	/// Кэш подготовленных тестов и подготовка результатов тестов пользователя
	/// </summary>
	public class PreparedPersonsTestsDataLocator
	{
		private Dictionary<int, PreparedTestDto> _preparedTests;
		private static readonly object SyncObj = new object();
		private static PreparedPersonsTestsDataLocator _instance;

		private readonly TestDao _testDao;
		private readonly QuestionDao _questionDao;
		private readonly PersonDao _personDao;

		public PreparedPersonsTestsDataLocator()
		{
			_testDao = new TestDao();
			_questionDao = new QuestionDao();
			_personDao = new PersonDao();
		}

		/// <summary>
		/// Получает информацию о тесте
		/// </summary>
		/// <param name="testId"></param>
		/// <returns></returns>
		public PreparedTestDto GetPreparedTest(int testId)
		{
			PreparedTestDto preparedTest;
			if (_preparedTests != null && _preparedTests.TryGetValue(testId, out preparedTest))
			{
				return preparedTest;
			}

			_preparedTests = GetPreparedTestsAll().Where(x => !x.IsEmpty).ToDictionary(x => x.Test.TestId);
			if (!_preparedTests.TryGetValue(testId, out preparedTest))
			{
				return null;
			}
			return preparedTest;
		}

		/// <summary>
		/// Получает кээшированный список подготовленных тестов
		/// </summary>
		/// <returns></returns>
		public List<PreparedTestDto> GetPreparedTests()
		{
			if (_preparedTests == null)
			{
				_preparedTests = GetPreparedTestsAll().Where(x => !x.IsEmpty).ToDictionary(x => x.Test.TestId);
			}
			return _preparedTests.Values.ToList();
		}

		public List<PersonTestResultDto> GetPersonTestsResults(int personId)
		{
			return GetPersonTestsResultsAll(personId).Where(x => !x.IsEmpty).ToList();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="personAnswers">ответы пользователя</param>
		/// <returns></returns>
		public PersonTestResultDto GetPersonTestResult(PersonAnswer[] personAnswers)
		{
			var personTestResult = GetPersonTestsResults(personAnswers).FirstOrDefault(x => !x.IsEmpty);
			return personTestResult;
		}

		#region подготовка данных

		/// <summary>
		/// Подготовить тесты к выводу
		/// </summary>
		/// <returns></returns>
		private PreparedTestDto[] GetPreparedTestsAll()
		{
			// вопросы у тестов
			var testQuests = _testDao.GetTestQuestions();
			// группы ответов у вопросов
			var questAnswersGroups = _testDao.GetQuestAnswersGroups();
			// вопросы-группы
			var groupQuests = _questionDao.GetGroupQuestions();

			// массив для подготовленных ответов
			PreparedQuestionDto[] preparedQuests = new PreparedQuestionDto[testQuests.Length];
			for (int i = 0; i < testQuests.Length; i++)
			{
				var quest = testQuests[i];

				// создать вопрос
				preparedQuests[i] = new PreparedQuestionDto { Quest = Mapper.Map<QuestionDto>(quest) };

				AnswersGroupDto[] answersGroups;
				if (questAnswersGroups.TryGetValue(quest.QuestId, out answersGroups))
				{
					preparedQuests[i].AnswersGroups = answersGroups;
				}
			}

			// перегруппировка ответов с учетом групповых вопросов
			preparedQuests = preparedQuests
				// пропустить вопросы без ответов
				.Where(x => !x.HasNoAnswers)
				// сгруппировать вопросы по родителю
				.GroupBy(x => x.Quest.GroupQuestId ?? x.Quest.Questid)
				// todo: производительность ?! очень сложно!
				.Select(gr =>
				{
					Question groupQuest;
					if (groupQuests.TryGetValue(gr.Key, out groupQuest))
					{
						var preparedQuest = new PreparedQuestionDto
						{
							Quest = new QuestionDto(groupQuest.Questid, groupQuest.Title, groupQuest.Literal, groupQuest.LevelOfDifficulty, groupQuest.AnswerType),
							ChildQuestions = gr.Select(y => y.Quest).ToArray(),
							AnswersGroups = gr.SelectMany(y => y.AnswersGroups).ToArray()
						};

						var firstChild = preparedQuest.ChildQuestions[0];

						// заполнить дополнительную информацию у группового теста
						// групповой вопрос не привязывается к тестам
						preparedQuest.Quest.TestId = firstChild.TestId;
						preparedQuest.Quest.QuestNum = firstChild.QuestNum;
						preparedQuest.Quest.MaxTime = preparedQuest.ChildQuestions.Sum(x => x.MaxTime);

						return preparedQuest;
					}

					return gr.ElementAt(0);
				}).ToArray();

			// группировка по тестам
			var preparedQuestsGroups = preparedQuests.GroupBy(x => x.Quest.TestId).ToArray();
			var testQuestsGroups = testQuests.GroupBy(x => x.Test).ToDictionary(gr => gr.Key.TestId, gr => gr.Key);

			// формирование подготовленных тестов
			PreparedTestDto[] preparedTests = new PreparedTestDto[preparedQuestsGroups.Length];
			for (int i = 0; i < preparedQuestsGroups.Length; i++)
			{
				var preparedQuestsGroup = preparedQuestsGroups[i];

				// создать подготовленый тест
				preparedTests[i] = new PreparedTestDto
				{
					Questions = preparedQuestsGroup.ToArray()
				};

				Test test;
				if (testQuestsGroups.TryGetValue(preparedQuestsGroup.Key, out test))
				{
					preparedTests[i].Test = Mapper.Map<TestDto>(test);
				}
				else
				{
					// пропускать такие тесты
					preparedTests[i] = new PreparedTestDto { IsEmpty = true };
				}
			}

			return preparedTests;
		}

		/// <summary>
		/// Получает текущие пройденные тесты пользователя с ответами
		/// </summary>
		/// <param name="personId"></param>
		/// <returns></returns>
		private Dictionary<int, Dictionary<int, PersonAnswer[]>> GetPersonTestsAnswers(int personId)
		{
			var personAnswers = _personDao.GetPersonAnswers(personId);
			return GetPersonTestsAnswers(personAnswers);
		}

		private Dictionary<int, Dictionary<int, PersonAnswer[]>> GetPersonTestsAnswers(PersonAnswer[] personAnswers)
		{
			Dictionary<int, Dictionary<int, PersonAnswer[]>> personTestsDct = personAnswers
				
				// группировка по ключу идентификатор теста - идентфикатор вопроса
				.GroupBy(x => new { 
					x.Data.TestId,
					TestQuestId = x.Question.GroupQuestId ?? x.QuestId 
				})

				// группировка по ключу идентификатор теста
				.GroupBy(x => x.Key.TestId, y => new { 
					y.Key.TestQuestId,
					PersonAnswers = y.ToArray() 
				})

				.ToDictionary(
					x => x.Key, // ключ идентфикатор теста
					y => y.ToDictionary(z => z.TestQuestId, q => q.PersonAnswers) // ключ идентификатор вопроса
				);

			return personTestsDct;
		}

		/// <summary>
		/// вычисляет результат пользователя в тесте
		/// todo: пересмотреть
		/// </summary>
		/// <param name="personId"></param>
		private PersonTestResultDto[] GetPersonTestsResultsAll(int personId)
		{
			// ответы пользователя
			var personAnswers = _personDao.GetPersonAnswers(personId);
			// результаты тестов пользователя
			var personTestsResults = GetPersonTestsResults(personAnswers);

			return personTestsResults;
		}

		private PersonTestResultDto[] GetPersonTestsResults(PersonAnswer[] personAnswers)
		{
			// пользовательские ответы
			var personTestsDct = GetPersonTestsAnswers(personAnswers);
			// тесты в системе
			var preparedTests = GetPreparedTests();
			// результаты тестов пользователя
			var personTestsResults = new PersonTestResultDto[preparedTests.Count];

			for (int i = 0; i < preparedTests.Count; i++)
			{
				var preparedTest = preparedTests[i];

				// результат
				var personTestResult = new PersonTestResultDto { TestName = preparedTest.Test.Title };

				Dictionary<int, PersonAnswer[]> personQuestsDct;
				if (personTestsDct.TryGetValue(preparedTest.Test.TestId, out personQuestsDct))
				{
					personTestResult.TestId = preparedTest.Test.TestId;
					personTestResult.QuestsCount = preparedTest.QuestsCount;

					foreach (var preparedQuestion in preparedTest.Questions)
					{
						PersonAnswer[] personQuestAnswers;
						if (personQuestsDct.TryGetValue(preparedQuestion.Quest.Questid, out personQuestAnswers))
						{
							var questAnswers = preparedQuestion.AnswersGroups.SelectMany(x => x.Answers).ToArray();

							// верный ответ
							bool isRightAnswer = personQuestAnswers.All(
								x => questAnswers.Any(
									y => y.QuestId == x.QuestId && y.AnswerId == x.AnswerId && y.PriorityNo == x.PriorityNo && y.IsRight)) &&
								// количество правильных ответов совпадает с количеством ответов пользователя
								personQuestAnswers.Length == questAnswers.Count(x => x.IsRight);

							personTestResult.RightQuestsCount += Convert.ToInt32(isRightAnswer);
						}
					}
				}
				else
				{
					personTestResult.IsEmpty = true;
				}

				personTestsResults[i] = personTestResult;
			}

			return personTestsResults;
		}
		
		#endregion

		/// <summary>
		/// Экземпляр класса
		/// </summary>
		public static PreparedPersonsTestsDataLocator Instance
		{
			get
			{
				lock (SyncObj)
				{
					if (_instance == null)
					{
						return _instance = new PreparedPersonsTestsDataLocator();
					}
					return _instance;
				}
			}
		}
	}
}