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
		[Column("LevelOfDifficulty")]
		public virtual string LevelOfDifficultyStr
		{
			get { return ((char) LevelOfDifficulty).ToString(); }
			set { LevelOfDifficulty = value.ToEnum<LevelOfDifficulty>(); }
		}

		/// <summary>
		/// ������� ���������
		/// </summary>
		[NotMapped]
		public virtual LevelOfDifficulty LevelOfDifficulty { get; set; }

		/// <summary>
		/// ������ ������������
		/// </summary>
		[Column("RelevanceStatus")]
		public virtual string RelevanceStatusStr
		{
			get { return ((char) RelevanceStatus).ToString(); }
			set { RelevanceStatus = value.ToEnum<RelevanceStatus>(); }
		}

		/// <summary>
		/// ������ ������������
		/// </summary>
		[NotMapped]
		public virtual RelevanceStatus RelevanceStatus { get; set; }
		
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
