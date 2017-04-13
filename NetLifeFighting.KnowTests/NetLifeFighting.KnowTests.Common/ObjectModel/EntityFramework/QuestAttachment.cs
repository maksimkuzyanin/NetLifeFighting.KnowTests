using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	/// <summary>
	/// Вложения вопроса
	/// </summary>
	[Table("QuestAttachment")]
	public class QuestAttachment
	{
		/// <summary>
		/// Идентификатор вопроса
		/// </summary>
		[Key, Column(Order = 1)]
		public virtual int QuestId { get; set; }

		/// <summary>
		/// Идентификатор вложения
		/// </summary>
		[Key, Column(Order = 2)]
		public virtual int AttachmentId { get; set; }

		/// <summary>
		/// Описание к вложению
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Порядок вложений одного вопроса
		/// </summary>
		public int? OrderNo { get; set; }

		/// <summary>
		/// Вопрос
		/// </summary>
		[ForeignKey("QuestId")]
		public virtual Question Question { get; set; }

		/// <summary>
		/// Вложение
		/// </summary>
		[ForeignKey("AttachmentId")]
		public virtual FileAttachment Attachment { get; set; }
	}
}
