using System.Data.SqlClient;
using System.Linq;
using EntityFramework.BulkInsert.Extensions;
using NetLifeFighting.KnowTests.Common.Enums;
using NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework;

namespace NetLifeFighting.KnowTests.DAL.EntityFramework.Persons
{
	public class PersonDao: EntityFrameworkDao<Person>
	{
		public Person GetByName(string name)
		{
			return Query.FirstOrDefault(x => x.Nickname == name);
		}

		public Person GetByNameAndRoleType(string name, RoleType role)
		{
			return Query.FirstOrDefault(x => x.Nickname == name && x.RoleType == (int) role);
		}

		public Person[] GetAllByNameAndRoleType(string name, RoleType role)
		{
			return Query.Where(x => x.Nickname == name && x.RoleType == (int) role).ToArray();
		}

		public PersonAnswer[] GetPersonAnswers(int personId)
		{
			return Context.PersonAnswers.ToArray();
		}

		public void SaveTestResult(int personId, int testId, PersonAnswer[] personAnswers)
		{
			// почистить предыдущий результат
			Context.Database.ExecuteSqlCommand(
				@"delete from PersonAnswer where PersonId = @personId and TestId = @testId"
				, new SqlParameter("@personId", personId), new SqlParameter("@testId", testId));

			// вставить новый результат
			Context.BulkInsert(personAnswers);
		}
	}
}
