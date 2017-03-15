namespace NetLifeFighting.KnowTests.Web.DTO.DTO
{
	/// <summary>
	/// Краткая информация по вопросу
	/// </summary>
	public class ShortQuestInfoDto
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		public int? QuestId { get; set; }

		/// <summary>
		/// Заголовок
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Тип ответа
		/// </summary>
		public string AnswerType { get; set; }

		/// <summary>
		/// Номер вопроса
		/// </summary>
		public int QuestNum { get; set; }
	}
}
