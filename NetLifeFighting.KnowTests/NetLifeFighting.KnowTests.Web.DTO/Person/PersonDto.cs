using NetLifeFighting.KnowTests.Common.Enums;

namespace NetLifeFighting.KnowTests.Web.DTO.Person
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
		/// роль пользователя
		/// </summary>
		public RoleType Role { get; set; }

		/// <summary>
		/// Ответы пользователя
		/// </summary>
		public PersonAnswerDto[] PersonAnswers { get; set; }
	}
}