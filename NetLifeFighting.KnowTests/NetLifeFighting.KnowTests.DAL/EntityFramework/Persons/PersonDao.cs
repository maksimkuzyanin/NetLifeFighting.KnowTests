using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using EntityFramework.BulkInsert.Extensions;
using NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework;

namespace NetLifeFighting.KnowTests.DAL.EntityFramework.Persons
{
	public class PersonDao: EntityFrameworkDao<Person>
	{
		public Person GetByName(string name)
		{
			return Query.FirstOrDefault(x => x.Nickname == name);
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
