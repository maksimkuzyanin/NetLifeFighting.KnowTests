using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	/// <summary>
	/// файловые вложения
	/// </summary>
	[Table("FileAttachment")]
	public class FileAttachment
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		[Key]
		public virtual int AttachmentId { get; set; }

		/// <summary>
		/// Описание
		/// </summary>
		public virtual string Description { get; set; }

		/// <summary>
		/// Название
		/// </summary>
		public virtual string AFileName { get; set; }

		/// <summary>
		/// Байт-массив файла
		/// </summary>
		public virtual byte[] AFile { get; set; }
	}
}
