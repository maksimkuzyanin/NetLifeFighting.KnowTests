using System.ComponentModel.DataAnnotations;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	/// <summary>
	/// Ответ
	/// </summary>
	public class Answer
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		public virtual int AnswerId { get; set; }

		/// <summary>
		/// Формулировка
		/// </summary>
		public virtual string Title { get; set; }

		/// <summary>
		/// Литера
		/// </summary>
		public virtual string Literal { get; set; }

		/// <summary>
		/// Верно, не верно
		/// </summary>
		[Required]
		public virtual bool IsRight { get; set; }

		/// <summary>
		/// Приоритет
		/// </summary>
		public virtual int? PriorityNo { get; set; }

		/// <summary>
		/// Вопрос
		/// </summary>
		public virtual Question Question { get; set; }
	}
}
