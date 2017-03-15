using System;

namespace NetLifeFighting.KnowTests.Web.DTO.DTO
{
	/// <summary>
	/// Информация о тесте
	/// </summary>
	public class TestInfoDto
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		public int TestId { get; set; }

		/// <summary>
		/// Название
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// максимальное время выполнения
		/// </summary>
		public int? MaxTime { get; set; }

		/// <summary>
		/// Уровень сложности
		/// </summary>
		public string LevelOfDifficulty { get; set; }

		/// <summary>
		/// Статус
		/// </summary>
		public string RelevanceStatus { get; set; }

		/// <summary>
		/// Дата актуальности
		/// </summary>
		public DateTime? RelevanceDate { get; set; }
	}
}
