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
		private QuestionDao _questionDao;

		/// <summary>
		/// конструктор
		/// </summary>
		public TestComponent()
		{
			_testDao = new TestDao();
			_questionDao = new QuestionDao();
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
			QuestRow[] rows = parser.ParseAll();
			
			// логика сохранения в базу
			// работает в режиме обновления
			var tests =_testDao.GetAll();
			// вопросы
			var quests = _questionDao.GetAll();

			// есть сгруппированные вопросы по заголовку теста
			var excelTests = rows.GroupBy(row => row.TestTitle);
			// поиск тестов уже существующих в базе, связь вопросов с ними нужно будет обновлять
			var existsTests = tests.Where(x => excelTests.Any(gr => gr.Key.Equals(x.Title))).ToArray();
			// существующие вопросы
			var existsQuests = quests.Where(x => rows.Any(y => y.QuestTitle.Equals(x.Title)));
			// идентификаторы существующих тестов
			var testIds = existsTests.Select(x => x.TestId).ToArray();
			// идентификаторы существующих вопросов
			//var questIds = 

			// чистит существующие связи с вопросами
			_testDao.ClearTestQuestions(testIds);
			// формирование сущностей для вставки в базу
			List<TestQuestion> testQuestions = new List<TestQuestion>();
			foreach (var excelTest in excelTests)
			{
				// если тест существует - взять из базы
				var newTest = existsTests.FirstOrDefault(x => x.Title.Equals(excelTest.Key)) ?? new Test { Title = excelTest.Key };
				// цикл по вопросам
				foreach (var questRow in excelTest)
				{
					
				}
			}
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
