using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	/// <summary>
	/// Реализует связь многие ко многим Тест-Вопрос
	/// </summary>
	[Table("TestQuestion")]
	public class TestQuestion
	{
		/// <summary>
		/// Идентификатор теста
		/// </summary>
		[Key, Column(Order = 1)]
		public virtual int TestId { get; set; }

		/// <summary>
		/// Идентификатор вопроса
		/// </summary>
		[Key, Column(Order = 2)]
		public virtual int QuestId { get; set; }

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
		/// Номер вопроса
		/// </summary>
		public virtual int QuestNum { get; set; }

		/// <summary>
		/// Максимальное время на вопрос
		/// </summary>
		public virtual int? MaxTime { get; set; }
	}
}