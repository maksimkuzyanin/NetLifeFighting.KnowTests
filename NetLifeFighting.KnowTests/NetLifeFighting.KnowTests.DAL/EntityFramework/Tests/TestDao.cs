using System.Collections.Generic;
using System.Linq;
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
	}
}
