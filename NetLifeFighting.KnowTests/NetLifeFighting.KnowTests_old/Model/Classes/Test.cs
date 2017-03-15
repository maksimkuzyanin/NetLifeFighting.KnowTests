using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	/// <summary>
	/// Тест
	/// </summary>
	public class Test
	{
		public Test()
		{
			Questions = new HashSet<Question>();
		}

		/// <summary>
		/// Идентификатор
		/// </summary>
		public virtual int TestId { get; set; }

		/// <summary>
		/// Название
		/// </summary>
		public virtual string Title { get; set; }

		/// <summary>
		/// Дата актуальности
		/// </summary>
		public virtual DateTime ActualDate { get; set; }

		/// <summary>
		/// Вопросы
		/// </summary>
		public virtual ISet<Question> Questions { get; set; }
	}
}
