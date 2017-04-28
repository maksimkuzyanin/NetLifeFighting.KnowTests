using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using NetLifeFighting.ImportExcel;
using NetLifeFighting.KnowTests.Common.Enums;
using NetLifeFighting.KnowTests.Common.Helpers;
using NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework;
using NetLifeFighting.KnowTests.DAL.EntityFramework.Tests;

namespace NetLifeFighting.KnowTests.Components
{
	/// <summary>
	/// 
	/// </summary>
	public class TestComponent
	{
		/// <summary>
		/// 
		/// </summary>
		private readonly TestDao _testDao;

		/// <summary>
		/// 
		/// </summary>
		private readonly QuestionDao _questionDao;

		private readonly AnswerDao _answerDao;

		/// <summary>
		/// конструктор
		/// </summary>
		public TestComponent()
		{
			_testDao = new TestDao();
			_questionDao = new QuestionDao();
			_answerDao = new AnswerDao();
		}

		/// <summary>
		/// Импортирует тесты
		/// </summary>
		/// <param name="fileBytes"></param>
		/// <param name="type"></param>
		public void ImportTests(byte[] fileBytes, [Optional][DefaultParameterValue(ImportType.Simple)] ImportType type)
		{
			// сообщения об ошибках
			const string errMessageTest = "Ошибка сохранения теста \"{0}\"";
			const string errMessageQuest = "Ошибка сохранения вопроса \"{0}\"";
			const string errMessageAnswer = "Ошибка сохранения ответа \"{0}\"";

			// шаблон импорта
			Dictionary<string, int> templ = GetImportTemplate(type);
			// парсер для ексель
			ExcelParser parser = new ExcelParser(fileBytes, templ);
			// строки с информацией по вопросам
			QuestRow[] questRows = parser.ParseAll();
			
			// логика сохранения в базу
			// работает в режиме обновления

			// равно как и вопросы каждый ответ уникален в рамках существущей модели
			// и лишь имеет связь с конкретным вопросом
			Dictionary<string, Answer> currentAnswers = _answerDao.GetAll().ToDictionary(x => x.Title);

			// заголовки ответов
			var answerTitles = questRows
				.SelectMany(x => x.Answers)
				.Select(GetTitle)
				.Distinct();

			var setAnswers = new List<Answer>();
			foreach (var answerTitle in answerTitles)
			{
				Answer setAnswer;
				if (currentAnswers.TryGetValue(answerTitle, out setAnswer))
				{
					continue;
				}
				setAnswer = new Answer { Title = answerTitle };
				setAnswers.Add(setAnswer);
			}
			// сохранить новые ответы
			_answerDao.Save(setAnswers);
			// текущие ответы
			currentAnswers = _answerDao.GetAll().ToDictionary(x => x.Title);

			// текущие вопросы
			var currentQuests = _questionDao.GetAll().ToDictionary(x => x.Title);

			// заголовки вопросов
			var questTitles = questRows
				.Select(x => GetTitle(x.QuestTitle))
				.Distinct();

			var setQuests = new List<Question>();
			foreach (var questTitle in questTitles)
			{
				Question setQuest;
				if (currentQuests.TryGetValue(questTitle, out setQuest))
				{
					// у существующих вопросов почистить привязки с ответами
					setQuest.QuestsAnswers.Clear();
				}
				else
				{
					setQuest = new Question { Title = questTitle, AnswerType = "S", LevelOfDifficulty = "S"};
				}
				setQuests.Add(setQuest);
			}
			// сохранение вопросов без привязок к ответам
			_questionDao.Save(setQuests);
			// текущие вопросы
			currentQuests = _questionDao.GetAll().ToDictionary(x => x.Title);

			// текущие тесты
			var currentTests = _testDao.GetAll().ToDictionary(x => x.Title);
			// сгруппированные вопросы по заголовку теста
			var excelTests = questRows.GroupBy(row => row.TestTitle).ToArray();

			var setTests = new List<Test>();
			foreach (var excelTest in excelTests)
			{
				string testTitle = excelTest.Key;
				Test setTest;

				if (currentTests.TryGetValue(testTitle, out setTest))
				{
					setTest.TestQuestions.Clear();
				}
				else
				{
					setTest = new Test { Title = testTitle, LevelOfDifficulty = "S", RelevanceStatus = "R" };
				}
				setTests.Add(setTest);
			}
			// сохранение тестов
			_testDao.Save(setTests);
			// текущие тесты
			currentTests = _testDao.GetAll().ToDictionary(x => x.Title);

			// сохранение привязок
			// список тестов с вопросами
			var testQuestions = new List<TestQuestion>();
			// разбор ексель-файла
			foreach (var excelTest in excelTests)
			{
				string testTitle = excelTest.Key;

				Test test;
				if (!currentTests.TryGetValue(testTitle, out test))
				{
					throw new ApplicationException(string.Format(errMessageTest, testTitle));
				}

				foreach (var questRow in excelTest)
				{
					Question quest;
					if (!currentQuests.TryGetValue(questRow.QuestTitle, out quest))
					{
						throw new ApplicationException(string.Format(errMessageQuest, questRow.QuestTitle));
					}

					var questAnswers = new HashSet<QuestAnswer>();
					foreach (var answerDescription in questRow.Answers)
					{
						string answerLiteral = GetLiteral(answerDescription);
						string answerTitle = GetTitle(answerDescription);

						Answer answer;
						if (!currentAnswers.TryGetValue(answerTitle, out answer))
						{
							throw new ApplicationException(string.Format(errMessageAnswer, answerTitle));
						}
						var questAnswer = new QuestAnswer { QuestId = quest.Questid, AnswerId = answer.AnswerId, Question = quest, Answer = answer };
						questAnswers.Add(questAnswer);
					}

					quest.QuestsAnswers = questAnswers;
					// сформировать связь тест-вопрос
					var testQuestion = new TestQuestion
					{
						TestId = test.TestId,
						QuestId = quest.Questid,
						Test = test,
						Question = quest,
						QuestNum = questRow.QuestNum
					};
					testQuestions.Add(testQuestion);
				}
			}
			// сохранить полученные данные в базу
			_testDao.SaveTestQuestions(testQuestions);
			
		}

		private string GetLiteral(string description)
		{
			const int maxLiteralLength = 2;

			char[] chars = description.TakeWhile(x => !x.Equals('.')).ToArray();

			var literal = new string(chars);

			if (literal.Length > maxLiteralLength)
			{
				// литера отсутствует
				return null;
			}
			return literal;
		}

		private string GetTitle(string description)
		{
			var literal = GetLiteral(description);

			if (literal.IsNullOrEmpty())
			{
				// если отсутствует литера - вернуть полное описание
				return description;
			}

			var templ = string.Format("{0}. ", literal);
			var title = description.Replace(templ, string.Empty).Trim();

			return title;
		}

		/// <summary>
		/// получает шаблон для импорта
		/// </summary>
		/// <returns></returns>
		private Dictionary<string, int> GetImportTemplate(ImportType importType)
		{
			// объект с описанием полей ексцеля
			var importDescription = new ImportDescription();

			// привязка полей к номерам столбцов
			string[] importMappings = importDescription.GetOptionalImportMappings(importType).ToArray();

			// шаблон
			var importTemplate = importMappings
				.Select((s, i) => new { ColumnName = s, CulumnNum = i + 1 })
				.ToDictionary(x => x.ColumnName, y => y.CulumnNum);

			return importTemplate;
		}
	}
}
