using System.Linq;
using System.Web.Http;
using AutoMapper;
using NetLifeFighting.KnowTests.Common;
using NetLifeFighting.KnowTests.Common.Abstraction.Result;
using NetLifeFighting.KnowTests.Common.Helpers;
using NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework;
using NetLifeFighting.KnowTests.DAL.EntityFramework.Persons;
using NetLifeFighting.KnowTests.Web.DTO.Person;

namespace NetLifeFighting.KnowTests.Web.Controllers
{
	/// <summary>
	/// Для работы с тестируемым пользователем
	/// </summary>
	[RoutePrefix("app/api/persons")] // todo: разобраться с построением пути
	public class PersonController : ApiController
	{
		private readonly PersonDao _personDao;

		public PersonController()
		{
			_personDao = new PersonDao();
		}

		[HttpPost]
		[Route("login")]
		public Result<PersonDto> GetPerson([FromBody]LoginInfoDto loginInfoDto)
		{
			var person = _personDao.GetByName(loginInfoDto.Nickname);

			if (person == null)
			{
				return new Result<PersonDto>(ResultStatus.Failure, Messages.kEntryErr);
			}

			var isValidPassword = PasswordHelper.CheckPasswordHash(loginInfoDto.Password, person.Password);

			if (!isValidPassword)
			{
				return new Result<PersonDto>(ResultStatus.Failure, Messages.kEntryErr);
			}

			PersonDto dto = Mapper.Map<PersonDto>(person);

			return new Result<PersonDto>(dto);
		}
	}
}
