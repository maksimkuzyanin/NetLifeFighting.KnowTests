using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
			Tests = new HashSet<Test>();
		}

		/// <summary>
		/// Идентификатор
		/// </summary>
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
		/// Тип
		/// </summary>
		public virtual string AnswerType { get; set; }

		/// <summary>
		/// Тесты
		/// </summary>
		public virtual ISet<Test> Tests { get; set; }
	}
}
