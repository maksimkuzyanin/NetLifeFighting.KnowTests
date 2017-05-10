using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework
{
	[Table("TestData")]
	public class TestData
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		[Key]
		public virtual int DataId { get; set; }

		/// <summary>
		/// Идентификатор теста
		/// </summary>
		public virtual int TestId { get; set; }

		/// <summary>
		/// Идентификатор персоны
		/// </summary>
		public virtual int PersonId { get; set; }

		/// <summary>
		/// Тест
		/// </summary>
		[ForeignKey("TestId")]
		public virtual Test Test { get; set; }

		/// <summary>
		/// Персона
		/// </summary>
		[ForeignKey("PersonId")]
		public virtual Person Person { get; set; }
		
		/// <summary>
		/// Режим тестирования
		/// </summary>
		public virtual string TestRegime { get; set; }

		/// <summary>
		/// Время начала тестирования
		/// </summary>
		public virtual DateTime Startdate { get; set; }

		/// <summary>
		/// Время окончания тестирования
		/// </summary>
		public virtual DateTime Enddate { get; set; }

		/// <summary>
		/// Уровень сложности
		/// </summary>
		public virtual string LevelOfDifficulty { get; set; }
	}
}
