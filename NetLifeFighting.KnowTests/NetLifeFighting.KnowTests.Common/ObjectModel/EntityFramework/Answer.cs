using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	/// <summary>
	/// �����
	/// </summary>
	[Table("Answer")]
	public class Answer
	{
		public Answer()
		{
			QuestsAnswers = new HashSet<QuestAnswer>();
		}

		/// <summary>
		/// �������������
		/// </summary>
		[Key]
		public virtual int AnswerId { get; set; }

		/// <summary>
		/// ������������
		/// </summary>
		public virtual string Title { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public virtual string Literal { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public virtual ISet<QuestAnswer> QuestsAnswers { get; set; } 
	}
}
