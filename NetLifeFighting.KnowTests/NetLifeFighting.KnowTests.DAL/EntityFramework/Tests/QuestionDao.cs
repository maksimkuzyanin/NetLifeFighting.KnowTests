using System.Collections.Generic;
using System.Linq;
using NetLifeFighting.KnowTests.Common.Enums;
using NetLifeFighting.KnowTests.Common.Helpers;
using NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework;

namespace NetLifeFighting.KnowTests.DAL.EntityFramework.Tests
{
	public class QuestionDao: EntityFrameworkDao<Question>
	{
		/// <summary>
		/// Отфильтровывает вопросы с типом соответствие,
		/// которые являются вопросами-группами
		/// </summary>
		public Dictionary<int, Question> GetGroupQuestions()
		{
			string conformity = ((char) AnswerType.Conformity).ToString();

			var groupQuestionsDct = Query
				.Where(x => x.AnswerType == conformity)
				.ToDictionary(x => x.Questid);

			return groupQuestionsDct;
		}

		/// <summary>
		/// Чистит связи с вопросами
		/// </summary>
		public void ClearQuestAnswers(int[] questIds)
		{
			var questsParams = questIds.CommaJoin();

			Context.Database.ExecuteSqlCommand(
				string.Format(@"delete from QuestAnswer where QuestId in ({0})", questsParams));
		}

		public Question[] GetByTitles(IEnumerable<string> titles)
		{
			var quests = Query.Where(x => titles.Contains(x.Title)).ToArray();
			return quests;
		}
	}

	public class AnswerDao : EntityFrameworkDao<Answer>
	{
		public Answer[] GetByTitles(IEnumerable<string> titles)
		{
			var answers = Query.Where(x => titles.Contains(x.Title)).ToArray();
			return answers;
		}
	}
}
