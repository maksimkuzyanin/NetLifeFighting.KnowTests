namespace NetLifeFighting.KnowTests.Web.DTO.Person
{
	/// <summary>
	/// Информация по входу
	/// </summary>
	public class LoginInfoDto
	{
		/// <summary>
		/// Имя пользователя
		/// </summary>
		public string Nickname { get; set; }

		/// <summary>
		/// Пароль
		/// </summary>
		public string Password { get; set; }
	}
}