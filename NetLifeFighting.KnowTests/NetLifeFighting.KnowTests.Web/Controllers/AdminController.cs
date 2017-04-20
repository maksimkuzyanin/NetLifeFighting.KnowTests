using System.Web.Http;
using NetLifeFighting.KnowTests.Common;
using NetLifeFighting.KnowTests.Common.Abstraction.Result;
using NetLifeFighting.KnowTests.Common.Enums;
using NetLifeFighting.KnowTests.Web.DTO.Person;
using NetLifeFighting.KnowTests.Web.Helpers;

namespace NetLifeFighting.KnowTests.Web.Controllers
{
	/// <summary>
	/// Для работы с админкой
	/// </summary>
	[RoutePrefix("app/api/admin")] // todo: разобраться с построением пути
    public class AdminController : ApiController
    {
		/// <summary>
		/// Имя администратора
		/// </summary>
	    const string AdminName = "admin";

		/// <summary>
		/// компонента аутентификации
		/// </summary>
		private readonly AuthComponent _authComponent;

	    public AdminController()
	    {
		    _authComponent = new AuthComponent();
	    }

		[HttpPost]
		[Route("login")]
	    public Result<PersonDto> AdminLogin([FromBody] PersonCredentials credentials)
		{
			// инициализация полномочий
			credentials.Nickname = AdminName;
			credentials.Role = RoleType.Admin;

			// данные администратора
			var adminData = _authComponent.GetPersonData(credentials);

			if (adminData == null)
			{
				return new Result<PersonDto>(ResultStatus.Failure, Messages.kEntryErr);
			}
			return new Result<PersonDto>(adminData);
		}
    }
}
