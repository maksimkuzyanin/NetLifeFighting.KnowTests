using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	/// <summary>
	/// ����
	/// </summary>
	public class Test
	{
		public Test()
		{
			Questions = new HashSet<Question>();
		}

		/// <summary>
		/// �������������
		/// </summary>
		public virtual int TestId { get; set; }

		/// <summary>
		/// ��������
		/// </summary>
		public virtual string Title { get; set; }

		/// <summary>
		/// ���� ������������
		/// </summary>
		public virtual DateTime ActualDate { get; set; }

		/// <summary>
		/// �������
		/// </summary>
		public virtual ISet<Question> Questions { get; set; }
	}
}
