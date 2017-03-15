using System.Linq;
using NetLifeFighting.KnowTests.Common.Enums;

namespace NetLifeFighting.KnowTests.Web.DTO.Test
{
	/// <summary>
	/// Подготовленный вопрос
	/// </summary>
	public class PreparedQuestionDto
	{
		/// <summary>
		/// Основной вопрос
		/// </summary>
		public QuestionDto Quest { get; set; }

		/// <summary>
		/// Вопросы-дети
		/// </summary>
		public QuestionDto[] ChildQuestions { get; set; }

		/// <summary>
		/// Группы ответов
		/// </summary>
		public AnswersGroupDto[] AnswersGroups { get; set; }

		/// <summary>
		/// Не имеет ответов
		/// </summary>
		public bool HasNoAnswers
		{
			get { return AnswersGroups == null || !AnswersGroups.Any(); }
		}

		/// <summary>
		/// Вопрос групповой
		/// </summary>
		public bool IsGroup
		{
			get { return Quest.AnswerType == ((char) AnswerType.Conformity).ToString(); }
		}
	}
}
