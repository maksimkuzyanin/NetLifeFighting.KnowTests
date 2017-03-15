namespace NetLifeFighting.KnowTests.Web.DTO
{
	/// <summary>
	/// Пользователь
	/// </summary>
	public class PersonDto
	{
		/// <summary>
		/// Имя пользователя
		/// </summary>
		public string Nickname { get; set; }

		/// <summary>
		/// Ответы пользователя
		/// </summary>
		public PersonAnswerDto[] PersonAnswers { get; set; }
	}
}