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
		public virtual string LevelOfDifficulty { get; set; }

		/// <summary>
		/// Статус актуальности
		/// </summary>
		public virtual string RelevanceStatus { get; set; }

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
