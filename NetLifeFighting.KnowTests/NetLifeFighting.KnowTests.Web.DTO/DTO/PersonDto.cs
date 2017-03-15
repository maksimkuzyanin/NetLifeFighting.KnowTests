namespace NetLifeFighting.KnowTests.Web.DTO.DTO
{
	/// <summary>
	/// Пользователь
	/// </summary>
	public class PersonDto
	{
		/// <summary>
		/// Иденитификатор
		/// </summary>
		public int PersonId { get; set; }

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