using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NetLifeFighting.ImportExcel;
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
			// шаблон импорта
			Dictionary<string, int> templ = GetImportTemplate(type);
			// парсер для ексель
			ExcelParser parser = new ExcelParser(fileBytes, templ);
			// строки с информацией по вопросам
			QuestRow[] questRows = parser.ParseAll();
			
			// логика сохранения в базу
			// работает в режиме обновления

			// вопросы
			var quests = _questionDao.GetAll().ToArray();

			// идентификаторы существующих вопросов
			var existsQuestsIds = quests
				.Where(x => questRows.Any(y => y.QuestTitle.Equals(x.Title)))
				.Select(x => x.Questid)
				.ToArray();

			// чистит связи вопрос-ответ
			_questionDao.ClearQuestAnswers(existsQuestsIds);

			// вопросы в базе уникальны
			Dictionary<string, Question> questsDct = quests.ToDictionary(x => x.Title);
			// равно как и вопросы каждый ответ уникален в рамках существущей модели
			// и лишь имеет связь с конкретным вопросом
			Dictionary<string, Answer> answers = _answerDao.GetAll().ToDictionary(x => x.Title);

			// тесты
			var tests =_testDao.GetAll().ToArray();
			// тесты в базе уникальны
			var testsDct = tests.ToDictionary(x => x.Title);
			// есть сгруппированные вопросы по заголовку теста
			var excelTests = questRows.GroupBy(row => row.TestTitle);

			// идентификаторы существующих тестов
			var existsTestsIds = tests
				.Where(x => questRows.Any(y => y.TestTitle.Equals(x.Title)))
				.Select(x => x.TestId)
				.ToArray();

			// чистит существующие связи с тест-вопрос
			_testDao.ClearTestQuestions(existsTestsIds);

			// список тестов с вопросами
			var testQuestions = new List<TestQuestion>();
			// разбор ексель-файла
			foreach (var excelTest in excelTests)
			{
				Test newTest;
				if (!testsDct.TryGetValue(excelTest.Key, out newTest))
				{
					newTest = new Test { Title = excelTest.Key };
				}

				foreach (var questRow in excelTest)
				{
					Question newQuest;
					if (!questsDct.TryGetValue(questRow.QuestTitle, out newQuest))
					{
						newQuest = new Question { Title = questRow.QuestTitle };
					}

					var questAnswers = new HashSet<QuestAnswer>();
					foreach (var answer in questRow.Answers)
					{
						Answer newAnswer;
						if (!answers.TryGetValue(answer, out newAnswer))
						{
							newAnswer = new Answer { Title = answer };
						}
						var questAnswer = new QuestAnswer { Question = newQuest, Answer = newAnswer };
						questAnswers.Add(questAnswer);
					}

					newQuest.QuestsAnswers = questAnswers;
					// сформировать связь тест-вопрос
					var testQuestion = new TestQuestion { Test = newTest, Question = newQuest, QuestNum = questRow.QuestNum };
					testQuestions.Add(testQuestion);
				}
			}
			// сохранить полученные данные в базу
			_testDao.SaveTestQuestions(testQuestions);
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
