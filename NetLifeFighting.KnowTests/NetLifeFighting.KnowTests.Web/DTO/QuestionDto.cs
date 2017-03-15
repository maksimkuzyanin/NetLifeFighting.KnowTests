namespace NetLifeFighting.KnowTests.Web.DTO
{
	/// <summary>
	/// Вопрос
	/// </summary>
	public class QuestionDto
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
		/// Тип ответа
		/// </summary>
		public string AnswerType { get; set; }

		/// <summary>
		/// Главный вопрос группы
		/// </summary>
		public QuestionDto GroupQuestion { get; set; }

		/// <summary>
		/// Ответы
		/// </summary>
		public AnswerDto[] Answers { get; set; }
	}
}