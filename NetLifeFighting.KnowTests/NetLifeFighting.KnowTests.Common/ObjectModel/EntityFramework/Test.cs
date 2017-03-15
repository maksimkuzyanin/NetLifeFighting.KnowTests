using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetLifeFighting.KnowTests.Common.Enums;
using NetLifeFighting.KnowTests.Common.Helpers;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	/// <summary>
	/// Тест
	/// </summary>
	[Table("Test")]
	public class Test
	{
		public Test()
		{
			TestQuestions = new HashSet<TestQuestion>();
		}

		/// <summary>
		/// Идентификатор
		/// </summary>
		[Key]
		public virtual int TestId { get; set; }

		/// <summary>
		/// Название
		/// </summary>
		public virtual string Title { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual int? MaxTime { get; set; }

		/// <summary>
		/// Уровень сложности
		/// </summary>
		[Column("LevelOfDifficulty")]
		public virtual string LevelOfDifficultyStr
		{
			get { return ((char) LevelOfDifficulty).ToString(); }
			set { LevelOfDifficulty = value.ToEnum<LevelOfDifficulty>(); }
		}

		/// <summary>
		/// Уровень сложности
		/// </summary>
		[NotMapped]
		public virtual LevelOfDifficulty LevelOfDifficulty { get; set; }

		/// <summary>
		/// Статус актуальности
		/// </summary>
		[Column("RelevanceStatus")]
		public virtual string RelevanceStatusStr
		{
			get { return ((char) RelevanceStatus).ToString(); }
			set { RelevanceStatus = value.ToEnum<RelevanceStatus>(); }
		}

		/// <summary>
		/// Статус актуальности
		/// </summary>
		[NotMapped]
		public virtual RelevanceStatus RelevanceStatus { get; set; }
		
		/// <summary>
		/// Дата актуальности
		/// </summary>
		public virtual DateTime? RelevanceDate { get; set; }

		/// <summary>
		/// Вопросы
		/// </summary>
		public virtual ISet<TestQuestion> TestQuestions { get; set; }
	}
}
