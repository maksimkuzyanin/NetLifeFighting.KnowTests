using AutoMapper;
using NetLifeFighting.KnowTests.Common.Helpers;
using NetLifeFighting.KnowTests.DAL.EntityFramework.Persons;
using NetLifeFighting.KnowTests.Web.DTO.Person;

namespace NetLifeFighting.KnowTests.Web.Helpers
{
	public class AuthComponent
	{
		private readonly PersonDao _personDao;

		public AuthComponent()
		{
			_personDao = new PersonDao();
		}

		/// <summary>
		/// Возвращает данные пользователя
		/// </summary>
		/// <param name="credentials">пользовательские полномочия</param>
		/// <returns></returns>
		public PersonDto GetPersonData(PersonCredentials credentials)
		{
			var person = _personDao.GetByName(credentials.Nickname);

			if (person == null)
			{
				return null;
			}

			var checkedPass = PasswordHelper.CheckPasswordHash(credentials.Password, person.Password);

			if (checkedPass)
			{
				var personData = Mapper.Map<PersonDto>(person);
				return personData;
			}
			return null;
		}
	}
}