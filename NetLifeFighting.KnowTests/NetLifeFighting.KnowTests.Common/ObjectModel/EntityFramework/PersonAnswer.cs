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
		/// ������������� ������ � ������������
		/// </summary>
		[Key, Column(Order = 1)]
		public virtual int DataId { get; set; }

		/// <summary>
		/// ������������� �������
		/// </summary>
		[Key, Column(Order = 2)]
		public virtual int QuestId { get; set; }

		/// <summary>
		/// ������������� ������
		/// </summary>
		[Key, Column(Order = 3)]
		public virtual int AnswerId { get; set; }

		/// <summary>
		/// ������ ������������
		/// </summary>
		[ForeignKey("DataId")]
		public virtual TestData Data { get; set; }

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
