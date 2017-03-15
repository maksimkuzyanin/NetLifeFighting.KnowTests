using System.ComponentModel.DataAnnotations;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	/// <summary>
	/// �����
	/// </summary>
	public class Answer
	{
		/// <summary>
		/// �������������
		/// </summary>
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
		/// �����, �� �����
		/// </summary>
		[Required]
		public virtual bool IsRight { get; set; }

		/// <summary>
		/// ���������
		/// </summary>
		public virtual int? PriorityNo { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public virtual Question Question { get; set; }
	}
}
