using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	/// <summary>
	/// Пользовательские ответы
	/// </summary>
	[Table("PersonAnswer")]
	public class PersonAnswer
	{
		/// <summary>
		/// Идентификатор пользователя
		/// </summary>
		[Key, Column(Order = 1)]
		public virtual int PersonId { get; set; }

		/// <summary>
		/// Идентификатор теста
		/// </summary>
		[Key, Column(Order = 2)]
		public virtual int TestId { get; set; }

		/// <summary>
		/// Идентификатор вопроса
		/// </summary>
		[Key, Column(Order = 3)]
		public virtual int QuestId { get; set; }

		/// <summary>
		/// Идентификатор ответа
		/// </summary>
		[Key, Column(Order = 4)]
		public virtual int AnswerId { get; set; }

		/// <summary>
		/// Пользователь
		/// </summary>
		[ForeignKey("PersonId")]
		public virtual Person Person { get; set; }

		/// <summary>
		/// Тест
		/// </summary>
		[ForeignKey("TestId")]
		public virtual Test Test { get; set; }

		/// <summary>
		/// Вопрос
		/// </summary>
		[ForeignKey("QuestId")]
		public virtual Question Question { get; set; }

		/// <summary>
		/// Ответ
		/// </summary>
		[ForeignKey("AnswerId")]
		public virtual Answer Answer { get; set; }

		/// <summary>
		/// Приоритет ответа
		/// </summary>
		public virtual int? PriorityNo { get; set; }

		/// <summary>
		/// Время ответа на вопрос
		/// </summary>
		public virtual int AnswerTime { get; set; }
	}
}
