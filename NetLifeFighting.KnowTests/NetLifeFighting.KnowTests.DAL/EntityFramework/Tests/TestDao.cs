using System.Collections.Generic;
using System.Linq;
using EntityFramework.BulkInsert.Extensions;
using NetLifeFighting.KnowTests.Common.Helpers;
using NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework;
using NetLifeFighting.KnowTests.Web.DTO.Test;

namespace NetLifeFighting.KnowTests.DAL.EntityFramework.Tests
{
	public class TestDao: EntityFrameworkDao<Test>
	{
		/// <summary>
		/// Получает сгруппированные ответы в вопросе
		/// todo: производительность под вопросом ?!
		/// </summary>
		/// <returns></returns>
		public Dictionary<int, AnswersGroupDto[]> GetQuestAnswersGroups()
		{
			var questAnswersGroups = Context.QuestsAnswers // достать все возможные ответы на вопросы из базы
				// селектятся поля для описания ответа
				.Select(x => new AnswerDto
				{
					AnswerId = x.AnswerId,
					QuestId = x.QuestId,
					Title = x.Answer.Title,
					Literal = x.Answer.Literal,
					IsRight = x.IsRight,
					PriorityNo = x.PriorityNo,
					GroupNum = x.GroupNum
				})
				// вначале сгруппировать по идентификатору вопроса и номеру группы (столбца) в ответе
				.GroupBy(x => new { x.QuestId, x.GroupNum })
				// создает группы из ответов
				.GroupBy(x => x.Key.QuestId, y => new { y.Key.GroupNum, Answers = y.Select(x => x) })
				// todo: сомнительный ToArray ?!
				// получить словарь групп ответов по конретному вопросу
				.ToDictionary(gr => gr.Key, gr => gr.Select(y => new AnswersGroupDto { GroupNum = y.GroupNum, Answers = y.Answers.ToArray() }).ToArray());

			return questAnswersGroups;
		}

		public TestQuestion[] GetTestQuestions()
		{
			return Context.TestsQuestions.ToArray(); // достать из базы все возможные вопросы для тестов
		}

		/// <summary>
		/// Чистит связи с вопросами
		/// </summary>
		/// <param name="testIds"></param>
		public void ClearTestQuestions(int[] testIds)
		{
			var testsParams = testIds.CommaJoin();

			Context.Database.ExecuteSqlCommand(
				string.Format(@"delete from TestQuestion where TestId in ({0})", testsParams));
		}

		public void ClearQuestAnswers(int[] questIds)
		{
			var questsParams = questIds.CommaJoin();

			Context.Database.ExecuteSqlCommand(
				string.Format(@"delete from QuestAnswer where QuestId in ({0})", questsParams));
		}

		public void SaveQuestAnswers(IEnumerable<QuestAnswer> questAnswers)
		{
			Context.BulkInsert(questAnswers);
		}

		public void SaveTestQuestions(IEnumerable<TestQuestion> testQuestions)
		{
			Context.BulkInsert(testQuestions);
		}

		public Test[] GetByTitles(IEnumerable<string> titles)
		{
			var tests = Query.Where(x => titles.Contains(x.Title)).ToArray();
			return tests;
		}
	}
}
