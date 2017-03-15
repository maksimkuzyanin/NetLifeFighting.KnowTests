using System.Data.Entity.ModelConfiguration;
using NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework;

namespace NetLifeFighting.KnowTests.EntityFramework.Mapping
{
	public class TestMapping : EntityTypeConfiguration<Test>
	{
		public TestMapping()
		{
			ToTable("Test");

			HasKey(x => x.TestId);

			Property(x => x.Title).IsRequired().HasMaxLength(150);
			Property(x => x.ActualDate).IsRequired();

			HasMany(x => x.Questions).WithMany(x => x.Tests).Map(m =>
			{
				m.MapLeftKey("TestId");
				m.MapRightKey("QuestId");
				m.ToTable("TestQuestion");
			});
		}
	}
}
