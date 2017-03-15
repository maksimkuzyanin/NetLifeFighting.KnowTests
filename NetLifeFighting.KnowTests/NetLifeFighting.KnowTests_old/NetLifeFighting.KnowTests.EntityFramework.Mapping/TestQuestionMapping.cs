using System.Data.Entity.ModelConfiguration;
using NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework;

namespace NetLifeFighting.KnowTests.EntityFramework.Mapping
{
	public class TestQuestionMapping : EntityTypeConfiguration<TestQuestion>
	{
		public TestQuestionMapping()
		{
			ToTable("TestQuestion");

			HasKey(x => new { x.TestId, x.QuestId });

			Property(x => x.QuestNum).IsRequired();

			HasRequired(x => x.Test).WithRequiredPrincipal().Map(m => m.MapKey("TestId"));
			HasRequired(x => x.Question).WithRequiredPrincipal().Map(m => m.MapKey("QuestId"));
		}
	}
}
