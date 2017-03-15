namespace NetLifeFighting.KnowTests.Web.DTO
{
	/// <summary>
	/// Ответ
	/// </summary>
	public class AnswerDto
	{
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
	}
}