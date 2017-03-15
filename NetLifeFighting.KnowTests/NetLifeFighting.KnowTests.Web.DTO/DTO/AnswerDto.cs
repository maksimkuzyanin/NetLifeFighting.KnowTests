namespace NetLifeFighting.KnowTests.Web.DTO.DTO
{
	/// <summary>
	/// Ответ
	/// </summary>
	public class AnswerDto
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		public int AnswerId { get; set; }

		/// <summary>
		/// Идентификатор вопроса
		/// </summary>
		public int QuestId { get; set; }

		/// <summary>
		/// Заголовок
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Литера
		/// </summary>
		public string Literal { get; set; }

		/// <summary>
		/// Верно/Не верно
		/// </summary>
		public bool IsRight { get; set; }

		/// <summary>
		/// Приоритет
		/// </summary>
		public int? PriorityNo { get; set; }

		/// <summary>
		/// Номер группы ответов
		/// </summary>
		public int? GroupNum { get; set; }
	}
}