using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	[Table("QuestAnswer")]
	public class QuestAnswer
	{
		public QuestAnswer()
		{
			QuestAnswerAttachments = new HashSet<QuestAnswerAttachment>();
		}

		/// <summary>
		/// Идентификатор связки вопрос-ответ
		/// </summary>
		[Key]
		public int QuestAnswerId { get; set; }

		/// <summary>
		/// Идентификатор вопроса
		/// </summary>
		public virtual int QuestId { get; set; }

		/// <summary>
		/// Идентификатор ответа
		/// </summary>
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

		/// <summary>
		/// Пояснение к ответу
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Вложения связки вопрос-ответ
		/// </summary>
		public virtual ISet<QuestAnswerAttachment> QuestAnswerAttachments { get; set; }
	}
}
