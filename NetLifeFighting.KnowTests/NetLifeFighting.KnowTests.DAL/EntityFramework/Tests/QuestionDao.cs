using System.Collections.Generic;
using System.Linq;
using NetLifeFighting.KnowTests.Common.Enums;
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
				.Where(x => x.AnswerTypeStr == conformity)
				.ToDictionary(x => x.Questid);

			return groupQuestionsDct;
		}
	}
}
