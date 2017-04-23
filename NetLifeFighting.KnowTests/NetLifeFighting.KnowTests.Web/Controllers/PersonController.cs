using System.Web.Http;
using NetLifeFighting.KnowTests.Common;
using NetLifeFighting.KnowTests.Common.Abstraction.Result;
using NetLifeFighting.KnowTests.Common.Enums;
using NetLifeFighting.KnowTests.DAL.EntityFramework.Persons;
using NetLifeFighting.KnowTests.Web.DTO.Person;
using NetLifeFighting.KnowTests.Web.Helpers;

namespace NetLifeFighting.KnowTests.Web.Controllers
{
	/// <summary>
	/// Для работы с тестируемым пользователем
	/// </summary>
	[RoutePrefix("app/api/persons")] // todo: разобраться с построением пути
	public class PersonController : ApiController
	{
		private readonly PersonDao _personDao;

		/// <summary>
		/// компонента аутентификации
		/// </summary>
		private readonly AuthComponent _authComponent;

		public PersonController()
		{
			_personDao = new PersonDao();
			_authComponent = new AuthComponent();
		}

		[HttpPost]
		[Route("student/login")]
		public Result<PersonDto> GetPerson([FromBody]PersonCredentials credentials)
		{
			// инициализация полномочий
			credentials.Role = RoleType.Student;

			// данные администратора
			var personData = _authComponent.GetPersonData(credentials);

			if (personData == null)
			{
				return new Result<PersonDto>(ResultStatus.Failure, Messages.kEntryErr);
			}
			return new Result<PersonDto>(personData);
		}
	}
}
