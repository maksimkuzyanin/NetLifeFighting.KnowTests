using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	/// <summary>
	/// ������
	/// </summary>
	[Table("Question")]
	public class Question
	{
		public Question()
		{
			Tests = new HashSet<Test>();
		}

		/// <summary>
		/// �������������
		/// </summary>
		public virtual int Questid { get; set; }

		/// <summary>
		/// ������������� ������������� ������� (������ ������������)
		/// </summary>
		public virtual int? GroupQuestId { get; set; }

		/// <summary>
		/// ������������
		/// </summary>
		public virtual string Title { get; set; }

		/// <summary>
		/// ������
		/// </summary>
		public virtual string Literal { get; set; }

		/// <summary>
		/// ���
		/// </summary>
		public virtual string AnswerType { get; set; }

		/// <summary>
		/// �����
		/// </summary>
		public virtual ISet<Test> Tests { get; set; }
	}
}
