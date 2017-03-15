namespace NetLifeFighting.KnowTests.Web.DTO.DTO
{
	public class QuestsGroupDto
	{
		/// <summary>
		/// Идентификатор группы вопросов
		/// </summary>
		public int? GroupQuestId { get; set; }

		/// <summary>
		/// Сгруппированные вопросы
		/// </summary>
		public QuestionDto[] Quests { get; set; }
	}
}
