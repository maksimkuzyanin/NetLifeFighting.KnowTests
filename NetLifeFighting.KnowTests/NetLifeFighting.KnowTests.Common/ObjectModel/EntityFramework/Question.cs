using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetLifeFighting.KnowTests.Common.Enums;
using NetLifeFighting.KnowTests.Common.Helpers;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	/// <summary>
	/// Вопрос
	/// </summary>
	[Table("Question")]
	public class Question
	{
		public Question()
		{
			TestQuestions = new HashSet<TestQuestion>();
			QuestsAnswers = new HashSet<QuestAnswer>();
			QuestAttachments = new HashSet<QuestAttachment>();
		}

		/// <summary>
		/// Идентификатор
		/// </summary>
		[Key]
		public virtual int Questid { get; set; }

		/// <summary>
		/// Идентификатор родительского вопроса (пример соответствие)
		/// </summary>
		public virtual int? GroupQuestId { get; set; }

		/// <summary>
		/// Формулировка
		/// </summary>
		public virtual string Title { get; set; }

		/// <summary>
		/// Литера
		/// </summary>
		public virtual string Literal { get; set; }

		/// <summary>
		/// Уровень сложности
		/// </summary>
		public virtual string LevelOfDifficulty { get; set; }

		/// <summary>
		/// Тип
		/// </summary>
		public virtual string AnswerType { get; set; }

		/// <summary>
		/// Тесты
		/// </summary>
		public virtual ISet<TestQuestion> TestQuestions { get; set; }

		/// <summary>
		/// Ответы
		/// </summary>
		public virtual ISet<QuestAnswer> QuestsAnswers { get; set; }

		/// <summary>
		/// Вложения вопроса
		/// </summary>
		public virtual ISet<QuestAttachment> QuestAttachments { get; set; }
	}
}
