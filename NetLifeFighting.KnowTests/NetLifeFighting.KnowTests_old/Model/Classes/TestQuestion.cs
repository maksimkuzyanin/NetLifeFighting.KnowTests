using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	/// <summary>
	/// Реализует связь многие ко многим Тест-Вопрос
	/// </summary>
	public class TestQuestion
	{
		/// <summary>
		/// Идентификатор теста
		/// </summary>
		public virtual int TestId { get; set; }

		/// <summary>
		/// Идентификатор вопроса
		/// </summary>
		public virtual int QuestId { get; set; }

		/// <summary>
		/// Тест
		/// </summary>
		public virtual Test Test { get; set; }

		/// <summary>
		/// Вопрос
		/// </summary>
		public virtual Question Question { get; set; }

		/// <summary>
		/// Номер вопроса
		/// </summary>
		public virtual int QuestNum { get; set; }
	}
}