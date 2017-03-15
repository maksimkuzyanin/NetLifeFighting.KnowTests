namespace NetLifeFighting.KnowTests.Web.DTO
{
	/// <summary>
	/// Ответы пользователя
	/// </summary>
	public class PersonAnswerDto
	{
		/// <summary>
		/// Тест
		/// </summary>
		public TestDto Test { get; set; }

		/// <summary>
		/// Вопрос
		/// </summary>
		public QuestionDto Question { get; set; }

		/// <summary>
		/// Ответ
		/// </summary>
		public AnswerDto Answer { get; set; }
	}
}