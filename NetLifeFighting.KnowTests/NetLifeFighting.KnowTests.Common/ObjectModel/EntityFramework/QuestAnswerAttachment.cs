using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	/// <summary>
	/// Вложения к связке вопрос-ответ
	/// </summary>
	[Table("QuestAnswerAttachment")]
	public class QuestAnswerAttachment
	{
		/// <summary>
		/// Идентификатор связки вопрос-ответ
		/// </summary>
		[Key, Column(Order = 1)]
		public virtual int QuestAnswerId { get; set; }

		/// <summary>
		/// Идентификатор вложения
		/// </summary>
		[Key, Column(Order = 2)]
		public virtual int AttachmentId { get; set; }

		/// <summary>
		/// Описание к вложению
		/// </summary>
		public virtual string Description { get; set; }

		/// <summary>
		/// Порядок вложений в рамках одной связки вопрос-ответ
		/// </summary>
		public virtual int? OrderNo { get; set; }

		/// <summary>
		/// Вопрос-ответ
		/// </summary>
		[ForeignKey("QuestAnswerId")]
		public virtual QuestAnswer QuestAnswer { get; set; }

		/// <summary>
		/// Вложение
		/// </summary>
		[ForeignKey("AttachmentId")]
		public virtual FileAttachment Attachment { get; set; }
	}
}
