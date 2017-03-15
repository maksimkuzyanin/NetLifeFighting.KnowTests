using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	/// <summary>
	/// Пользовательские ответы
	/// </summary>
	public class PersonAnswer
	{
		/// <summary>
		/// Идентификатор пользователя
		/// </summary>
		public virtual int PersonId { get; set; }

		/// <summary>
		/// Идентификатор теста
		/// </summary>
		public virtual int TestId { get; set; }

		/// <summary>
		/// Идентификатор вопроса
		/// </summary>
		public virtual int QuestId { get; set; }

		/// <summary>
		/// Идентификатор ответа
		/// </summary>
		public virtual int AnswerId { get; set; }

		/// <summary>
		/// Пользователь
		/// </summary>
		public virtual Person Person { get; set; }

		/// <summary>
		/// Тест
		/// </summary>
		public virtual Test Test { get; set; }

		/// <summary>
		/// Вопрос
		/// </summary>
		public virtual Question Question { get; set; }

		/// <summary>
		/// Ответ
		/// </summary>
		public virtual Answer Answer { get; set; }

		/// <summary>
		/// Приоритет ответа
		/// </summary>
		public virtual int? PriorityNo { get; set; }
	}
}
