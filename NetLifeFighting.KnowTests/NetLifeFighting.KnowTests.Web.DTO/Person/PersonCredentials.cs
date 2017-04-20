using NetLifeFighting.KnowTests.Common.Enums;

namespace NetLifeFighting.KnowTests.Web.DTO.Person
{
	/// <summary>
	/// Информация по входу
	/// </summary>
	public class PersonCredentials
	{
		/// <summary>
		/// Имя пользователя
		/// </summary>
		public string Nickname { get; set; }

		/// <summary>
		/// Пароль
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// роль пользователя
		/// </summary>
		public RoleType Role { get; set; }
	}
}