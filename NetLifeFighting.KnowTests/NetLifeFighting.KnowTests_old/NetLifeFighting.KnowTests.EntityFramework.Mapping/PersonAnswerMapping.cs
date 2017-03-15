using System.Data.Entity.ModelConfiguration;
using NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework;

namespace NetLifeFighting.KnowTests.EntityFramework.Mapping
{
	public class PersonAnswerMapping : EntityTypeConfiguration<PersonAnswer>
	{
		public PersonAnswerMapping()
		{
			ToTable("PersonAnswer");

			HasKey(x => new { x.PersonId, x.TestId, x.QuestId, x.AnswerId });

			Property(x => x.PriorityNo);

			HasRequired(x => x.Person).WithRequiredPrincipal().Map(m => m.MapKey("PersonId"));
			HasRequired(x => x.Test).WithRequiredPrincipal().Map(m => m.MapKey("TestId"));
			HasRequired(x => x.Question).WithRequiredPrincipal().Map(m => m.MapKey("QuestId"));
			HasRequired(x => x.Answer).WithRequiredPrincipal().Map(m => m.MapKey("AnswerId"));
		}
	}
}
