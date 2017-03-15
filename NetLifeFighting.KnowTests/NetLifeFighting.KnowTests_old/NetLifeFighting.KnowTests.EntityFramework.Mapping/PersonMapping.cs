using System.Data.Entity.ModelConfiguration;
using NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework;

namespace NetLifeFighting.KnowTests.EntityFramework.Mapping
{
	public class PersonMapping : EntityTypeConfiguration<Person>
	{
		public PersonMapping()
		{
			ToTable("Person");

			HasKey(x => x.PersonId);

			Property(x => x.Nickname).IsRequired().HasMaxLength(200);
			Property(x => x.Password).IsRequired().HasMaxLength(200);

			HasOptional(x => x.PersonAnswers).WithOptionalDependent().Map(m => m.MapKey("PersonId"));
		}
	}
}
