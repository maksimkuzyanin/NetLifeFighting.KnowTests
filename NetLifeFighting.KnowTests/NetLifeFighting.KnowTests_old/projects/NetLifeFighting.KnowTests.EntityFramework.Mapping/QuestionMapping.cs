using System.Data.Entity.ModelConfiguration;
using NetLifeFighting.KnowTests.Common.ObjectModel.EntityFramework;

namespace NetLifeFighting.KnowTests.EntityFramework.Mapping
{
	public class QuestionMapping: EntityTypeConfiguration<Question>
	{
		public QuestionMapping()
		{
			ToTable("Question");

			HasKey(x => x.Questid);

			Property(x => x.GroupQuestId);
			Property(x => x.Title).IsRequired().HasMaxLength(500);
			Property(x => x.Literal).HasMaxLength(10);
			Property(x => x.AnswerType).IsRequired().HasMaxLength(1);

			HasMany(x => x.Tests).WithMany(x => x.Questions).Map(m =>
			{
				m.MapLeftKey("QuestId");
				m.MapRightKey("TestId");
				m.ToTable("TestQuestion");
			});
		}
	}
}
