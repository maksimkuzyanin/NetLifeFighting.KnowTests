using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	[Table("QuestAnswer")]
	public class QuestAnswer
	{
		/// <summary>
		/// Идентификатор вопроса
		/// </summary>
		[Key, Column(Order = 1)]
		public virtual int QuestId { get; set; }

		/// <summary>
		/// Идентификатор ответа
		/// </summary>
		[Key, Column(Order = 2)]
		public virtual int AnswerId { get; set; }

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
		/// Верно/неверно
		/// </summary>
		[Required]
		public virtual bool IsRight { get; set; }

		/// <summary>
		/// Приоритет
		/// </summary>
		public virtual int? PriorityNo { get; set; }

		/// <summary>
		/// Номер группы ответов
		/// </summary>
		public virtual int? GroupNum { get; set; }
	}
}
