namespace NetLifeFighting.KnowTests.Web.DTO.Person
{
	/// <summary>
	/// Ответы пользователя
	/// </summary>
	public class PersonAnswerDto
	{
		/// <summary>
		/// Идентификатор пользователя
		/// </summary>
		public int PersonId { get; set; }

		/// <summary>
		/// Идентификатор теста
		/// </summary>
		public int TestId { get; set; }

		/// <summary>
		/// Идентификатор вопроса
		/// </summary>
		public int QuestId { get; set; }

		/// <summary>
		/// Групповой вопрос
		/// </summary>
		public int? GroupQuestId { get; set; }

		/// <summary>
		/// Идентификатор ответа
		/// </summary>
		public int AnswerId { get; set; }

		/// <summary>
		/// Приоритет ответа
		/// </summary>
		public int? PriorityNo { get; set; }
	}
}