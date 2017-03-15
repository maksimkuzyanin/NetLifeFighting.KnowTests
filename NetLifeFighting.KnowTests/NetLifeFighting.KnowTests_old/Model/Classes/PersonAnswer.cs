using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	/// <summary>
	/// ���������������� ������
	/// </summary>
	public class PersonAnswer
	{
		/// <summary>
		/// ������������� ������������
		/// </summary>
		public virtual int PersonId { get; set; }

		/// <summary>
		/// ������������� �����
		/// </summary>
		public virtual int TestId { get; set; }

		/// <summary>
		/// ������������� �������
		/// </summary>
		public virtual int QuestId { get; set; }

		/// <summary>
		/// ������������� ������
		/// </summary>
		public virtual int AnswerId { get; set; }

		/// <summary>
		/// ������������
		/// </summary>
		public virtual Person Person { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public virtual Test Test { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public virtual Question Question { get; set; }

		/// <summary>
		/// �����
		/// </summary>
		public virtual Answer Answer { get; set; }

		/// <summary>
		/// ��������� ������
		/// </summary>
		public virtual int? PriorityNo { get; set; }
	}
}
