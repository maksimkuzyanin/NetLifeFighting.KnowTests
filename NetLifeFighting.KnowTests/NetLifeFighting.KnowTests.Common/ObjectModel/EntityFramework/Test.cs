using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetLifeFighting.KnowTests.Common.Enums;
using NetLifeFighting.KnowTests.Common.Helpers;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	/// <summary>
	/// ����
	/// </summary>
	[Table("Test")]
	public class Test
	{
		public Test()
		{
			TestQuestions = new HashSet<TestQuestion>();
		}

		/// <summary>
		/// �������������
		/// </summary>
		[Key]
		public virtual int TestId { get; set; }

		/// <summary>
		/// ��������
		/// </summary>
		public virtual string Title { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual int? MaxTime { get; set; }

		/// <summary>
		/// ������� ���������
		/// </summary>
		public virtual string LevelOfDifficulty { get; set; }

		/// <summary>
		/// ������ ������������
		/// </summary>
		public virtual string RelevanceStatus { get; set; }

		/// <summary>
		/// ���� ������������
		/// </summary>
		public virtual DateTime? RelevanceDate { get; set; }

		/// <summary>
		/// �������
		/// </summary>
		public virtual ISet<TestQuestion> TestQuestions { get; set; }
	}
}
