namespace NetLifeFighting.KnowTests.Web.DTO.DTO
{
	/// <summary>
	/// Ответы пользователя
	/// </summary>
	public class PersonAnswerDto
	{
		/// <summary>
		/// Идентификатор теста
		/// </summary>
		public int TestId { get; set; }

		/// <summary>
		/// Идентификатор вопроса
		/// </summary>
		public int QuestId { get; set; }

		/// <summary>
		/// Идентификатор ответа
		/// </summary>
		public int AnswerId { get; set; }
	}
}