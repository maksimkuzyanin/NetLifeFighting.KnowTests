namespace NetLifeFighting.KnowTests.Web.DTO.DTO
{
	/// <summary>
	/// Подготовленное тестирование
	/// </summary>
	public class PreparedTestDto
	{
		/// <summary>
		/// Информация о тесте
		/// </summary>
		public TestDto Test { get; set; }

		/// <summary>
		/// Подготовленные вопросы
		/// </summary>
		public PreparedQuestionDto[] Questions { get; set; }
	}
}
