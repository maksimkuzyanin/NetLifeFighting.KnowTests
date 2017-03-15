using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	/// <summary>
	/// Ответ
	/// </summary>
	[Table("Answer")]
	public class Answer
	{
		public Answer()
		{
			QuestsAnswers = new HashSet<QuestAnswer>();
		}

		/// <summary>
		/// Идентификатор
		/// </summary>
		[Key]
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
		/// Ответы
		/// </summary>
		public virtual ISet<QuestAnswer> QuestsAnswers { get; set; } 
	}
}
