using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	/// <summary>
	/// ���������������� ������
	/// </summary>
	[Table("PersonAnswer")]
	public class PersonAnswer
	{
		/// <summary>
		/// ������������� ������������
		/// </summary>
		[Key, Column(Order = 1)]
		public virtual int PersonId { get; set; }

		/// <summary>
		/// ������������� �����
		/// </summary>
		[Key, Column(Order = 2)]
		public virtual int TestId { get; set; }

		/// <summary>
		/// ������������� �������
		/// </summary>
		[Key, Column(Order = 3)]
		public virtual int QuestId { get; set; }

		/// <summary>
		/// ������������� ������
		/// </summary>
		[Key, Column(Order = 4)]
		public virtual int AnswerId { get; set; }

		/// <summary>
		/// ������������
		/// </summary>
		[ForeignKey("PersonId")]
		public virtual Person Person { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		[ForeignKey("TestId")]
		public virtual Test Test { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		[ForeignKey("QuestId")]
		public virtual Question Question { get; set; }

		/// <summary>
		/// �����
		/// </summary>
		[ForeignKey("AnswerId")]
		public virtual Answer Answer { get; set; }

		/// <summary>
		/// ��������� ������
		/// </summary>
		public virtual int? PriorityNo { get; set; }

		/// <summary>
		/// ����� ������ �� ������
		/// </summary>
		public virtual int AnswerTime { get; set; }
	}
}
